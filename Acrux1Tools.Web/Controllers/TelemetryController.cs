using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acrux1Payloads;
using Acrux1Tools.Web.Helpers;
using Acrux1Tools.Web.Models.Telemetry;
using Microsoft.AspNetCore.Mvc;
using SatnogsApi;
using MoreLinq;
using System.IO;
using CsvHelper;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using Acrux1Tools.Web.Configuration;
using SatnogsApi.Models.SatnogsDb;
using SatnogsApi.Models.SatnogsNetwork;
using System.Net.Http;

namespace Acrux1Tools.Web.Controllers
{
    public class TelemetryController : Controller
    {
        private readonly ISatnogsDbApi satnogsDbApi;
        private readonly ISatnogsNetworkApi satnogsNetworkApi;
        private readonly IMemoryCache memoryCache;
        private readonly ApplicationSettings settings;

        public TelemetryController(
            ISatnogsDbApi satnogsDbApi,
            ISatnogsNetworkApi satnogsNetworkApi,
            IMemoryCache memoryCache,
            ApplicationSettings settings)
        {
            this.satnogsDbApi = satnogsDbApi;
            this.satnogsNetworkApi = satnogsNetworkApi;
            this.memoryCache = memoryCache;
            this.settings = settings;
        }

        public async Task<IActionResult> Index(int? satelliteId, int pageLimit = 150)
        {
            int satId = satelliteId ?? settings.DefaultSatelliteId;

            var getSatellitesTask = GetSatellites(satId);
            var getTelemetryTask = GetTelemetryRows(satId);

            (List<SatelliteEntry> satellites, DateTimeOffset satLastUpdated) = await getSatellitesTask;

            List<TelemetryRow> telemetries;
            DateTimeOffset lastUpdated;
            try
            {
                (telemetries, lastUpdated) = await getTelemetryTask;
            }
            catch (Exception ex)
            {
                telemetries = new List<TelemetryRow>();
                lastUpdated = default;

                ViewBag.Error = $"Could not get telemetry. {ex.Message}";
            }

            SatelliteEntry satellite = satellites.FirstOrDefault();

            // Fixup image URL
            //
            try
            {
                satellite.Image = new UriBuilder(satellite.Image)
                {
                    Scheme = Uri.UriSchemeHttps,
                    Port = -1
                }.Uri.ToString();
            }
            catch
            {

            }

            ListTelemetryViewModel viewModel = new ListTelemetryViewModel()
            {
                SatelliteId = satId,
                Satellite = satellite,
                Telemetries = telemetries.Take(pageLimit).ToList(),
                PageLimit = pageLimit,
                LastUpdated = lastUpdated
            };

            return View(viewModel);
        }

        private async Task<(List<TelemetryRow> Telemetries, DateTimeOffset LastUpdated)> GetTelemetryRows(int satelliteId)
        {
            if (memoryCache.TryGetValue(GetTelemetryCacheKey(satelliteId), out TelemetryCacheEntry cachedTelemetryEntry))
            {
                return (cachedTelemetryEntry.Telemetries, cachedTelemetryEntry.Created);
            }

            // Get the observations for the satellite
            //List<ObservationEntry> observationEntries = (await satnogsNetworkApi.GetAllObservations(satelliteId, "good", cachedTelemetryEntry?.Telemetries?.GroupBy(te => te.Observation).Select(g => g.Key)))
            List<ObservationEntry> observationEntries = (await satnogsNetworkApi.GetAllObservations(satelliteId, "good"))
                .Where(oe => oe.DemodData.Any())
                .ToList();

            List<TelemetryRow> telemetryRows = (await Task.WhenAll(observationEntries.Select(oe => GetTelemetryRowsForObservation(oe))))
                .SelectMany(l => l)
                .OrderByDescending(tr => tr.DemodData?.Timestamp)
                .ToList();

            TelemetryCacheEntry telemetryEntry = new TelemetryCacheEntry(telemetryRows);

            if (telemetryEntry.Telemetries.Count > 0)
            {
                memoryCache.Set(GetTelemetryCacheKey(satelliteId), telemetryEntry, TimeSpan.FromMinutes(30));
            }

            return (telemetryEntry.Telemetries, telemetryEntry.Created);
        }

        private Task<TelemetryRow[]> GetTelemetryRowsForObservation(ObservationEntry observationEntry) {
            return Task.WhenAll(observationEntry.DemodData.Select(dde => GetTelemetryRowForDemodedData(observationEntry, dde)));
        }

        private async Task<TelemetryRow> GetTelemetryRowForDemodedData(ObservationEntry observationEntry, ObservationEntry.DemodDataEntry demodedData) {
            HttpContent httpContents = await satnogsNetworkApi.GetObservationData(observationEntry.Id, demodedData.ResourceName);
            byte[] data = await httpContents.ReadAsByteArrayAsync();

            var fecResult = FecHelpers.DecodePayload(data, 16, 0, false);
            var beaconDecoded = BeaconDecoder.DecodeBeacon(fecResult.PayloadCorrected ?? fecResult.PayloadUncorrected);

            return new TelemetryRow()
            {
                Observation = observationEntry,
                DemodData = demodedData,
                FecDecodeResult = fecResult,
                Acrux1Beacon = beaconDecoded
            };
        }

        private static string GetTelemetryCacheKey(int satelliteId) => $"Telemetry-{satelliteId}";
        private static string GetSatelliteCacheKey(int? noradCatId) => $"Satellite-{noradCatId?.ToString() ?? "ALL"}";

        private async Task<(List<SatelliteEntry> Satellites, DateTimeOffset LastUpdated)> GetSatellites(int? noradCatId)
        {

            if (memoryCache.TryGetValue(GetSatelliteCacheKey(noradCatId), out SatelliteCacheEntry satelliteCacheEntry))
            {
                return (satelliteCacheEntry.Satellites, satelliteCacheEntry.Created);
            }

            var satelliteEntries = await satnogsDbApi.GetSatellites(noradCatId);

            satelliteCacheEntry = new SatelliteCacheEntry(satelliteEntries);
            memoryCache.Set(GetSatelliteCacheKey(noradCatId), satelliteEntries, TimeSpan.FromMinutes(5));

            return (satelliteCacheEntry.Satellites, satelliteCacheEntry.Created);
        }

        private class SatelliteCacheEntry
        {
            public List<SatelliteEntry> Satellites { get; private set; }
            public DateTimeOffset Created { get; private set; }

            public SatelliteCacheEntry(List<SatelliteEntry> satellites)
            {
                this.Satellites = satellites;
                this.Created = DateTimeOffset.Now;
            }
        }

        private class TelemetryCacheEntry
        {
            public List<TelemetryRow> Telemetries { get; private set; }
            public DateTimeOffset Created { get; private set; }

            public TelemetryCacheEntry(List<TelemetryRow> telemetries)
            {
                this.Telemetries = telemetries;
                this.Created = DateTimeOffset.Now;
            }

            public void Deconstruct(out List<TelemetryRow> telemetries, out DateTimeOffset created)
            {
                telemetries = Telemetries;
                created = Created;
            }
        }

        public async Task<IActionResult> DownloadCsv(int satelliteId)
        {
            Stream stream = new MemoryStream();

            var telemetry = (await GetTelemetryRows(satelliteId)).Telemetries.OrderBy(tr => tr.DemodData.Timestamp).ToList();

            if (telemetry.Count <= 0)
            {
                return NotFound();
            }

            // Write the telemetry to the memory stream in CSV format
            //
            using (var writer = new StreamWriter(stream, leaveOpen: true))
            using (var csv = new CsvWriter(writer, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ","
            }, true))
            {
                csv.WriteRecords(telemetry);
            }

            // Seek the stream back to the beginning
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "text/csv", $"telemetry-{satelliteId}-{DateTimeOffset.Now.ToString("s")}.csv"); // returns a FileStreamResult
        }
    }
}
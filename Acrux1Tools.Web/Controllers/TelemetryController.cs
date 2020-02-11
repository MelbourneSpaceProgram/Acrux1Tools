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

namespace Acrux1Tools.Web.Controllers
{
    public class TelemetryController : Controller
    {
        private readonly ISatnogsDbApi satnogsApi;
        private readonly IMemoryCache memoryCache;
        private readonly ApplicationSettings settings;

        public TelemetryController(ISatnogsDbApi satnogsApi, IMemoryCache memoryCache, ApplicationSettings settings)
        {
            this.satnogsApi = satnogsApi;
            this.memoryCache = memoryCache;
            this.settings = settings;
        }

        public async Task<IActionResult> Index(int? satelliteId)
        {
            int satId = satelliteId ?? settings.DefaultSatelliteId;

            var getSatellitesTask = GetSatellites(satId);
            var getTelemetryTask = GetTelemetryRows(satId);

            (List<SatelliteEntry> satellites, DateTimeOffset satLastUpdated) = await getSatellitesTask;

            List<TelemetryRow> telemetry;
            DateTimeOffset lastUpdated;
            try
            {
                (telemetry, lastUpdated) = await getTelemetryTask;
            }
            catch (Exception ex)
            {
                telemetry = new List<TelemetryRow>();
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
                Telemetry = telemetry,
                LastUpdated = lastUpdated
            };

            return View(viewModel);
        }

        private async Task<(List<TelemetryRow> Telemetry, DateTimeOffset LastUpdated)> GetTelemetryRows(int satelliteId)
        {
            if (memoryCache.TryGetValue(GetTelemetryCacheKey(satelliteId), out TelemetryCacheEntry cachedTelemetryEntry))
            {
                return (cachedTelemetryEntry.Telemetries, cachedTelemetryEntry.Created);
            }

            var telemetryEntries = await satnogsApi.GetAllTelemetry(satelliteId);

            List<TelemetryRow> freshTelemetry = telemetryEntries.Select(t =>
                {
                    var fecResult = FecHelpers.DecodePayload(t.Frame, 16, 0, false);
                    var beaconDecoded = BeaconDecoder.DecodeBeacon(fecResult.PayloadCorrected ?? fecResult.PayloadUncorrected);
                    return new TelemetryRow()
                    {
                        SatnogsTelemetry = t,
                        FecDecodeResult = fecResult,
                        Acrux1Beacon = beaconDecoded
                    };
                }).OrderByDescending(tr => tr.SatnogsTelemetry.Timestamp).ToList();

            cachedTelemetryEntry = new TelemetryCacheEntry(freshTelemetry);
            memoryCache.Set(GetTelemetryCacheKey(satelliteId), cachedTelemetryEntry, TimeSpan.FromMinutes(5));

            return (cachedTelemetryEntry.Telemetries, cachedTelemetryEntry.Created);
        }

        private static string GetTelemetryCacheKey(int satelliteId) => $"Telemetry-cache-{satelliteId}";
        private static string GetSatelliteCacheKey(int? noradCatId) => $"Satellite-cache-{noradCatId?.ToString() ?? "ALL"}";

        private async Task<(List<SatelliteEntry> Satellites, DateTimeOffset LastUpdated)> GetSatellites(int? noradCatId)
        {

            if (memoryCache.TryGetValue(GetSatelliteCacheKey(noradCatId), out SatelliteCacheEntry satelliteCacheEntry))
            {
                return (satelliteCacheEntry.Satellites, satelliteCacheEntry.Created);
            }

            var satelliteEntries = await satnogsApi.GetSatellites(noradCatId);

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

            var telemetry = (await GetTelemetryRows(satelliteId)).Telemetry.OrderBy(tr => tr.SatnogsTelemetry.Timestamp).ToList();

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
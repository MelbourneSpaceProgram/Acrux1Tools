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

namespace Acrux1Tools.Web.Controllers
{
    public class TelemetryController : Controller
    {
        private readonly ISatnogsApi satnogsApi;
        private readonly IMemoryCache memoryCache;

        public TelemetryController(ISatnogsApi satnogsApi, IMemoryCache memoryCache)
        {
            this.satnogsApi = satnogsApi;
            this.memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index(int? satelliteId)
        {
            int satId = satelliteId ?? 99964;

            (var telemetry, var lastUpdated) = await GetTelemetryRows(satId);

            ListTelemetryViewModel viewModel = new ListTelemetryViewModel()
            {
                SatelliteId = satId,
                Telemetry = telemetry,
                LastUpdated = lastUpdated
            };

            return View(viewModel);
        }

        private async Task<(List<TelemetryRow> Telemetry, DateTimeOffset LastUpdated)> GetTelemetryRows(int satelliteId)
        {
            if (memoryCache.TryGetValue(GetCacheKey(satelliteId), out TelemetryCacheEntry cachedTelemetryEntry)) {
                return (cachedTelemetryEntry.Telemetries, cachedTelemetryEntry.Created);
            }

            var telemetryEntries = await satnogsApi.GetAllTelemetry(satelliteId);

            List<TelemetryRow> freshTelemetry = telemetryEntries.Select(t =>
                {
                    var fecResult = FecHelpers.DecodePayload(t.Frame, 16, 0, false);
                    return new TelemetryRow()
                    {
                        SatnogsTelemetry = t,
                        FecDecodeResult = fecResult,
                        Acrux1Beacon = BeaconDecoder.DecodeBeacon(fecResult.PayloadCorrected ?? fecResult.PayloadUncorrected)
                    };
                }).OrderByDescending(tr => tr.SatnogsTelemetry.Timestamp).ToList();

            cachedTelemetryEntry = new TelemetryCacheEntry(freshTelemetry);
            memoryCache.Set(GetCacheKey(satelliteId), cachedTelemetryEntry, TimeSpan.FromMinutes(5));

            return (cachedTelemetryEntry.Telemetries, cachedTelemetryEntry.Created);
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

        private static string GetCacheKey(int satelliteId) => $"Telemetry-cache-{satelliteId}";

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
            using (var csv = new CsvWriter(writer, new CsvHelper.Configuration.Configuration()
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
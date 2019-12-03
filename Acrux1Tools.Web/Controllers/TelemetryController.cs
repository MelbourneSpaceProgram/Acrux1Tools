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

namespace Acrux1Tools.Web.Controllers
{
    public class TelemetryController : Controller
    {
        private readonly ISatnogsApi satnogsApi;

        public TelemetryController(ISatnogsApi satnogsApi)
        {
            this.satnogsApi = satnogsApi;
        }

        public async Task<IActionResult> Index(int? satelliteId)
        {
            int satId = satelliteId ?? 99964;

            ListTelemetryViewModel viewModel = new ListTelemetryViewModel()
            {
                SatelliteId = satId,
                Telemetry = await GetTelemetryRows(satId)
            };

            return View(viewModel);
        }

        private async Task<List<TelemetryRow>> GetTelemetryRows(int satelliteId)
        {
            var telemetry = await satnogsApi.GetAllTelemetry(satelliteId);

            return telemetry.Select(t =>
                {
                    var fecResult = FecHelpers.DecodePayload(t.Frame, 16, 0, false);
                    return new TelemetryRow()
                    {
                        SatnogsTelemetry = t,
                        FecDecodeResult = fecResult,
                        Acrux1Beacon = BeaconDecoder.DecodeBeacon(fecResult.PayloadCorrected ?? fecResult.PayloadUncorrected)
                    };
                }).OrderByDescending(tr => tr.SatnogsTelemetry.Timestamp).ToList();
        }

        public async Task<IActionResult> DownloadCsv(int satelliteId)
        {
            Stream stream = new MemoryStream();

            var telemetry = (await GetTelemetryRows(satelliteId)).OrderBy(tr => tr.SatnogsTelemetry.Timestamp).ToList();

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
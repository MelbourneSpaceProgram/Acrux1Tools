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

            List<TelemetryEntry> telemetry = new List<TelemetryEntry>();

            int pagesAtOnce = 16;

            for (int i = 1; i < 256; i+=pagesAtOnce)
            {
                var tasks = new List<Task<List<TelemetryEntry>>>();

                for (int j = 0; j < pagesAtOnce; j++) {
                    var task = satnogsApi.GetTelemetry(satId, i + j);
                    tasks.Add(task);
                }

                bool end = false;

                foreach (var task in tasks)
                {
                    try
                    {
                        telemetry.AddRange(await task);
                    }
                    catch (Exception ex) when (ex.Message.Contains("404"))
                    {
                        end = true;
                    }
                }

                if (end)
                {
                    break;
                }
            }

            telemetry = telemetry.OrderByDescending(t => t.Timestamp).DistinctBy(t => t.Timestamp).ToList();

            ListTelemetryViewModel viewModel = new ListTelemetryViewModel()
            {
                SatelliteId = satId,
                Telemetry = telemetry.Select(t =>
                {
                    var fecResult = FecHelpers.DecodePayload(t.Frame, 16, 0, false);
                    return new TelemetryRow()
                    {
                        SatnogsTelemetry = t,
                        FecDecodeResult = fecResult,
                        Acrux1Beacon = BeaconDecoder.DecodeBeacon(fecResult.PayloadCorrected ?? fecResult.PayloadUncorrected)
                    };
                }).ToList()
            };

            return View(viewModel);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acrux1Payloads;
using Acrux1Tools.Web.Helpers;
using Acrux1Tools.Web.Models.Telemetry;
using Microsoft.AspNetCore.Mvc;
using SatnogsApi;

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

            List<TelemetryEntry> telemetry = await satnogsApi.GetTelemetry(satId);
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
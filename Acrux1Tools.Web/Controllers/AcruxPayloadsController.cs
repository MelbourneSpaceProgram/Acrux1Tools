using Acrux1Payloads;
using Acrux1Tools.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Acrux1Tools.Web.Models.AcruxPayloads;

namespace Acrux1Tools.Web.Controllers
{
    public class AcruxPayloadsController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Decode));
        }

        public IActionResult Decode(DecodeViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Decode))]
        public IActionResult DecodePost(DecodeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Preamble length is 16 because beacons have an Ax25 frame in the first 16 bytes which does not participate
            // in the FEC block.
            //
            var fecResult = FecHelpers.DecodePayload(viewModel.HexPayloadOriginal, 16, 0, false);
            var beaconDecoded = BeaconDecoder.DecodeBeacon(fecResult.PayloadCorrected ?? fecResult.PayloadUncorrected);

            viewModel.FecDecodeResult = fecResult;
            viewModel.Acrux1Beacon = beaconDecoded;

            return View(viewModel);
        }
    }
}
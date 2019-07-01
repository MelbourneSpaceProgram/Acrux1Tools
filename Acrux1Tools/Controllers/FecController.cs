using Acrux1Tools.Web.Models.Fec;
using Microsoft.AspNetCore.Mvc;
using ReedSolomon;
using System;
using static Acrux1Tools.Web.Helpers.HexHelpers;

namespace Acrux1Tools.Web.Controllers
{
    public class FecController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Decode()
        {
            return View(new DecodeViewModel());
        }

        [HttpPost]
        [ActionName(nameof(Decode))]
        public IActionResult FecDecodePost(DecodeViewModel viewModel)
        {
            viewModel.Error = null;

            if (string.IsNullOrWhiteSpace(viewModel.HexPayloadOriginal))
            {
                viewModel.Error = "Packet was empty";
                return View(viewModel);
            }

            byte[] inputPayload = HexadecimalStringToByteArray(viewModel.HexPayloadOriginal);

            if (viewModel.PreambleCount < 0)
            {
                viewModel.Error = $"Preamble count {viewModel.PreambleCount} was less than 0";
                return View(viewModel);
            }

            if (viewModel.PreambleCount > inputPayload.Length)
            {
                viewModel.Error = $"Preamble count {viewModel.PreambleCount} was larger than block size {inputPayload.Length}";
                return View(viewModel);
            }

            if (inputPayload.Length - viewModel.PreambleCount < Rs8.BlockLength)
            {
                viewModel.Error = $"Block was less than {Rs8.BlockLength} bytes";
                return View(viewModel);
            }

            // Remove preamble from decode
            //
            Span<byte> mainPayload = inputPayload.AsSpan(viewModel.PreambleCount, 255);

            viewModel.HexPayloadUncorrected = ByteArrayToHexString(mainPayload);

            Span<byte> correctedPayload = new byte[mainPayload.Length];
            mainPayload.CopyTo(correctedPayload);

            viewModel.ErrorsCorrectedCount = Rs8.Decode(correctedPayload, null, viewModel.DualBasis);
            viewModel.HexPayloadCorrected = ByteArrayToHexString(correctedPayload);

            if (viewModel.ErrorsCorrectedCount < 0)
            {
                viewModel.Error = "Too many errors in payload to correct (errors > 16)";
            }

            return View(viewModel);
        }
    }
}
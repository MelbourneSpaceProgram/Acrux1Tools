using Acrux1Tools.Web.Helpers;
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
            if (!ModelState.IsValid)
            {
                viewModel.Error = "Input errors";
                return View(viewModel);
            }

            var fecResult = FecHelpers.DecodePayload(viewModel.HexPayloadOriginal, viewModel.PreambleLength, viewModel.VirtualFillLength, viewModel.DualBasis);

            if (fecResult.Success)
            {
                viewModel.PayloadUncorrected = fecResult.PayloadUncorrected;
                viewModel.BlockUncorrected = fecResult.BlockUncorrected;
                viewModel.PayloadCorrected = fecResult.PayloadCorrected;
                viewModel.BlockCorrected = fecResult.BlockCorrected;
                viewModel.ErrorsCorrectedCount = fecResult.ErrorsCorrectedCount;
            }
            else
            {
                viewModel.Error = fecResult.Error;
                return View(viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Encode()
        {
            return View(new DecodeViewModel());
        }

        [HttpPost]
        [ActionName(nameof(Encode))]
        public IActionResult FecEncodePost(DecodeViewModel viewModel)
        {
            return View();
        }
    }
}
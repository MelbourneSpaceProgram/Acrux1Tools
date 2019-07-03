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
            if(!ModelState.IsValid)
            {
                viewModel.Error = "Input errors";
                return View(viewModel);
            }

            viewModel.Error = null;

            if (string.IsNullOrWhiteSpace(viewModel.HexPayloadOriginal))
            {
                viewModel.Error = "Packet was empty";
                return View(viewModel);
            }

            Span<byte> inputPayload = HexadecimalStringToByteArray(viewModel.HexPayloadOriginal);

            if (viewModel.PreambleLength < 0)
            {
                viewModel.Error = $"Preamble count {viewModel.PreambleLength} was less than 0";
                return View(viewModel);
            }

            if(viewModel.VirtualFillLength < 0)
            {
                viewModel.Error = $"Virtual fill count {viewModel.VirtualFillLength} was less than 0";
                return View(viewModel);
            }

            int inputBlockLengthCalculated = inputPayload.Length - viewModel.PreambleLength + viewModel.VirtualFillLength;

            if (inputBlockLengthCalculated != Rs8.BlockLength)
            {
                viewModel.Error = $"Payload ({inputPayload.Length}) - Preamble ({viewModel.PreambleLength}) + Virtual fill ({viewModel.VirtualFillLength}) must equal block length ({inputBlockLengthCalculated} != {Rs8.BlockLength})";
                return View(viewModel);
            }

            // Remove preamble from decode
            //

            Span<byte> inputAfterPreamble = inputPayload.Slice(viewModel.PreambleLength);
            Span<byte> mainBlock = stackalloc byte[Rs8.BlockLength];
            Span<byte> mainBlockData = mainBlock.Slice(viewModel.VirtualFillLength);

            // Copy the input data after the preamble to the post-virtual-fill region of the data block
            //
            inputAfterPreamble.CopyTo(mainBlockData);
            
            // Save state before decode
            //
            viewModel.PayloadUncorrected = inputPayload.ToArray();
            viewModel.BlockUncorrected = mainBlock.ToArray();

            // Decode the block
            //
            viewModel.ErrorsCorrectedCount = Rs8.Decode(mainBlock, null, viewModel.DualBasis);

            // Copy the decoded data back after the original preamble
            //
            mainBlockData.CopyTo(inputAfterPreamble);

            // Save decoded state
            //
            viewModel.PayloadCorrected = inputPayload.ToArray();
            viewModel.BlockCorrected = mainBlock.ToArray();

            if (viewModel.ErrorsCorrectedCount < 0)
            {
                viewModel.Error = "Too many errors in payload to correct (errors > 16)";
            }

            return View(viewModel);
        }
    }
}
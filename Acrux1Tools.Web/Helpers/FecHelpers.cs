using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ReedSolomon;
using static Acrux1Tools.Web.Helpers.HexHelpers;

namespace Acrux1Tools.Web.Helpers
{
    public static class FecHelpers
    {
        public static FecDecodeResult DecodePayload(string inputPayload, int preambleLength, int virtualFillLength, bool dualBasis)
        {
            if (string.IsNullOrWhiteSpace(inputPayload))
            {
                return new FecDecodeResult()
                {
                    Success = false,
                    PreambleLength = preambleLength,
                    VirtualFillLength = virtualFillLength,
                    DualBasis = dualBasis,
                    Error = "Packet was empty"
                };
            }

            Span<byte> inputPayloadBytes = HexadecimalStringToByteArray(inputPayload);

            return DecodePayload(inputPayloadBytes, preambleLength, virtualFillLength, dualBasis);
        }

        public static FecDecodeResult DecodePayload(Span<byte> inputPayload, int preambleLength, int virtualFillLength, bool dualBasis)
        {
            FecDecodeResult result = new FecDecodeResult
            {
                PreambleLength = preambleLength,
                VirtualFillLength = virtualFillLength,
                DualBasis = dualBasis,
                PayloadUncorrected = inputPayload.ToArray()
            };

            if (preambleLength < 0)
            {
                result.Success = false;
                result.Error = $"Preamble count {preambleLength} was less than 0";
                return result;
            }

            if (virtualFillLength < 0)
            {
                result.Success = false;
                result.Error = $"Virtual fill count {virtualFillLength} was less than 0";
                return result;
            }

            int inputBlockLengthCalculated = inputPayload.Length - preambleLength + virtualFillLength;

            if (inputBlockLengthCalculated != Rs8.BlockLength)
            {
                result.Success = false;
                result.Error = $"Payload ({inputPayload.Length}) - Preamble ({preambleLength}) + Virtual fill ({virtualFillLength}) must equal block length ({inputBlockLengthCalculated} != {Rs8.BlockLength})";
                return result;
            }

            // Remove preamble from decode
            //

            Span<byte> inputAfterPreamble = inputPayload.Slice(preambleLength);
            Span<byte> mainBlock = stackalloc byte[Rs8.BlockLength];
            Span<byte> mainBlockData = mainBlock.Slice(virtualFillLength);

            // Copy the input data after the preamble to the post-virtual-fill region of the data block
            //
            inputAfterPreamble.CopyTo(mainBlockData);

            // Save state before decode
            //
            result.BlockUncorrected = mainBlock.ToArray();

            // Decode the block
            //
            result.ErrorsCorrectedCount = Rs8.Decode(mainBlock, null, dualBasis);

            // Copy the decoded data back after the original preamble
            //
            mainBlockData.CopyTo(inputAfterPreamble);

            // Save decoded state
            //
            result.PayloadCorrected = inputPayload.ToArray();
            result.BlockCorrected = mainBlock.ToArray();

            if (result.ErrorsCorrectedCount < 0)
            {
                result.Success = false;
                result.Error = "Too many errors in payload to correct (errors > 16)";
                return result;
            }
            else
            {
                result.Success = true;
                return result;
            }
        }
    }

    public class FecDecodeResult
    {
        [Display(Name = "FEC successful")]
        public bool Success { get; set; }
        [Display(Name = "FEC error")]
        public string Error { get; set; }
        [Display(Name = "FEC preamble length")]
        public int PreambleLength { get; set; }
        [Display(Name = "FEC virtual fill length")]
        public int VirtualFillLength { get; set; }
        [Display(Name = "FEC dual basis")]
        public bool DualBasis { get; set; }
        [Display(Name = "Payload uncorrected")]
        public byte[] PayloadUncorrected { get; set; }
        [Display(Name = "Block uncorrected")]
        public byte[] BlockUncorrected { get; set; }
        [Display(Name = "Payload corrected")]
        public byte[] PayloadCorrected { get; set; }
        [Display(Name = "Block corrected")]
        public byte[] BlockCorrected { get; set; }
        [Display(Name = "FEC corrections")]
        public int ErrorsCorrectedCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Acrux1Tools.Web.Models.Fec
{
    public class DecodeViewModel
    {
        [Required]
        [Display(Name = "Payload Input")]
        public string HexPayloadOriginal { get; set; }
        [Display(Name = "Preamble Length")]
        public int PreambleLength { get; set; }
        [Display(Name = "Virtual Fill Length")]
        public int VirtualFillLength { get; set; }
        [Display(Name = "Dual Basis Mode")]
        public bool DualBasis { get; set; }

        [Display(Name = "Error")]
        public string Error { get; set; }
        [Display(Name = "Payload Uncorrected")]
        public byte[] PayloadUncorrected { get; set; }
        [Display(Name = "Payload Corrected")]
        public byte[] PayloadCorrected { get; set; }
        [Display(Name = "Block Uncorrected")]
        public byte[] BlockUncorrected { get; set; }
        [Display(Name = "Block Corrected")]
        public byte[] BlockCorrected { get; set; }
        [Display(Name = "Errors Corrected")]
        public int ErrorsCorrectedCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Acrux1Payloads;
using Acrux1Tools.Web.Helpers;
using Acrux1Tools.Web.Models.Telemetry;

namespace Acrux1Tools.Web.Models.Acrux1
{
    public class DecodeViewModel
    {
        [Required]
        [Display(Name = "Payload Input")]
        public string HexPayloadOriginal { get; set; }
        [Display(Name = "Preamble Length")]
        public int PreambleLength { get; set; }
        [Display(Name = "Error")]
        public string Error { get; set; }

        public FecDecodeResult FecDecodeResult { get; set; }
        public BeaconData Acrux1Beacon { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Acrux1Payloads;
using Acrux1Tools.Web.Helpers;
using Acrux1Tools.Web.Models.Telemetry;

namespace Acrux1Tools.Web.Models.AcruxPayloads
{
    public class DecodeViewModel
    {
        [Required]
        [Display(Name = "Payload Input", Description = "Full hex encoded beacon payload, including 16 byte Ax25 header")]
        public string HexPayloadOriginal { get; set; }

        public FecDecodeResult FecDecodeResult { get; set; }
        public BeaconData Acrux1Beacon { get; set; }
    }
}

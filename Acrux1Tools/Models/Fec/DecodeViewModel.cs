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
        public string HexPayloadOriginal { get; set; }
        public int PreambleCount { get; set; }
        public int PaddingCount { get; set; }
        public bool DualBasis { get; set; }

        public string Error { get; set; }
        public string HexPayloadUncorrected { get; set; }
        public string HexPayloadCorrected { get; set; }
        public int ErrorsCorrectedCount { get; set; }
    }
}

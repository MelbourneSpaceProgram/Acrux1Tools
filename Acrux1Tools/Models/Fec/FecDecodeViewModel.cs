using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acrux1Tools.Models.Fec
{
    public class FecDecodeViewModel
    {
        public string HexPayloadOriginal { get; set; }
        public int PreambleCount { get; set; }
        public int PaddingCount { get; set; }
        public bool DualBasis { get; set; }

        public bool DecodeSuccess { get; set; }
        public string HexPayloadCorrected { get; set; }
        public int ErrorsCorrectedCount { get; set; }
    }
}

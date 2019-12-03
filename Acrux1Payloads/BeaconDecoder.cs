using System;
using System.Collections.Generic;
using System.Text;
using Acrux1Payloads.Kaitai;
using Kaitai;

namespace Acrux1Payloads
{
    public static class BeaconDecoder
    {
        public static Acrux1beacon DecodeBeacon(byte[] payload)
        {
            return new Acrux1beacon(new KaitaiStream(payload));
        }
    }
}

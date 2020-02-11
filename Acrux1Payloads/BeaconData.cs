using System;
using System.Collections.Generic;
using System.Text;

namespace Acrux1Payloads
{
    public class BeaconData
    {
        //
        // AX25 Data
        //

        public string DestinationCallsign { get; set; }
        public string SourceCallsign { get; set; }
        public int SourceSsid { get; set; }
        public int DestinationSsid { get; set; }

        //
        // MSP Payload Data
        //
        public int TxCount { get; set; }
        public int RxCount { get; set; }
        public int RxValid { get; set; }
        public byte PayloadType { get; set; }
        public double? Comouti1 { get; set; }
        public int Comouti1Raw { get; set; }
        public double? Comoutv1 { get; set; }
        public int Comoutv1Raw { get; set; }
        public double? Comouti2 { get; set; }
        public int Comouti2Raw { get; set; }
        public double? Comoutv2 { get; set; }
        public int Comoutv2Raw { get; set; }
        public double? Comt2 { get; set; }
        public int Comt2Raw { get; set; }
        public double? Epsadcbatv1 { get; set; }
        public int Epsadcbatv1Raw { get; set; }
        public double? Epsloadi1 { get; set; }
        public int Epsloadi1Raw { get; set; }
        public double? Epsadcbatv2 { get; set; }
        public int Epsadcbatv2Raw { get; set; }
        public double? Epsboostini2 { get; set; }
        public int Epsboostini2Raw { get; set; }
        public double? Epsrail1 { get; set; }
        public int Epsrail1Raw { get; set; }
        public double? Epsrail2 { get; set; }
        public int Epsrail2Raw { get; set; }
        public double? Epstoppanelv { get; set; }
        public int EpstoppanelvRaw { get; set; }
        public double? Epstoppaneli { get; set; }
        public int EpstoppaneliRaw { get; set; }
        public double? Epst1 { get; set; }
        public int Epst1Raw { get; set; }
        public double? Epst2 { get; set; }
        public int Epst2Raw { get; set; }
        public double? Xposv { get; set; }
        public int XposvRaw { get; set; }
        public double? Xposi { get; set; }
        public int XposiRaw { get; set; }
        public double? Xpost1 { get; set; }
        public int Xpost1Raw { get; set; }
        public double? Yposv { get; set; }
        public int YposvRaw { get; set; }
        public double? Yposi { get; set; }
        public int YposiRaw { get; set; }
        public double? Ypost1 { get; set; }
        public int Ypost1Raw { get; set; }
        public double? Xnegv { get; set; }
        public int XnegvRaw { get; set; }
        public double? Xnegi { get; set; }
        public int XnegiRaw { get; set; }
        public double? Xnegt1 { get; set; }
        public int Xnegt1Raw { get; set; }
        public double? Ynegv { get; set; }
        public int YnegvRaw { get; set; }
        public double? Ynegi { get; set; }
        public int YnegiRaw { get; set; }
        public double? Ynegt1 { get; set; }
        public int Ynegt1Raw { get; set; }
        public double? Znegv { get; set; }
        public int ZnegvRaw { get; set; }
        public double? Znegi { get; set; }
        public int ZnegiRaw { get; set; }
        public double? Znegt1 { get; set; }
        public int Znegt1Raw { get; set; }
        public double? Zpost { get; set; }
        public int ZpostRaw { get; set; }
        public ulong Cdhtime { get; set; }
        public ulong Swcdhlastreboot { get; set; }
        public int Swsequence { get; set; }
        public string Outreachmessage { get; set; }
    }
}

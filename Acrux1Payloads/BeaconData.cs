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
        public double Comouti1 { get; set; }
        public double Comoutv1 { get; set; }
        public double Comouti2 { get; set; }
        public double Comoutv2 { get; set; }
        public double Comt2 { get; set; }
        public double Epsadcbatv1 { get; set; }
        public double Epsloadi1 { get; set; }
        public double Epsadcbatv2 { get; set; }
        public double Epsboostini2 { get; set; }
        public double Epsrail1 { get; set; }
        public double Epsrail2 { get; set; }
        public double Epstoppanelv { get; set; }
        public double Epstoppaneli { get; set; }
        public double Epst1 { get; set; }
        public double Epst2 { get; set; }
        public double Xposv { get; set; }
        public double Xposi { get; set; }
        public double Xpost1 { get; set; }
        public double Yposv { get; set; }
        public double Yposi { get; set; }
        public double Ypost1 { get; set; }
        public double Xnegv { get; set; }
        public double Xnegi { get; set; }
        public double Xnegt1 { get; set; }
        public double Ynegv { get; set; }
        public double Ynegi { get; set; }
        public double Ynegt1 { get; set; }
        public double Znegv { get; set; }
        public double Znegi { get; set; }
        public double Znegt1 { get; set; }
        public double Zpost { get; set; }
        public ulong Cdhtime { get; set; }
        public ulong Swcdhlastreboot { get; set; }
        public int Swsequence { get; set; }
        public string Outreachmessage { get; set; }
    }
}

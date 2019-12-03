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
        public short Epsrail1 { get; set; }
        public short Epsrail2 { get; set; }
        public double Epstoppanelv { get; set; }
        public double Epstoppaneli { get; set; }
        public double Epst1 { get; set; }
        public double Epst2 { get; set; }
        public short Xposv { get; set; }
        public short Xposi { get; set; }
        public short Xpost1 { get; set; }
        public short Yposv { get; set; }
        public short Yposi { get; set; }
        public short Ypost1 { get; set; }
        public short Xnegv { get; set; }
        public short Xnegi { get; set; }
        public short Xnegt1 { get; set; }
        public short Ynegv { get; set; }
        public short Ynegi { get; set; }
        public short Ynegt1 { get; set; }
        public short Znegv { get; set; }
        public short Znegi { get; set; }
        public short Znegt1 { get; set; }
        public short Zpost { get; set; }
        public ulong Cdhtime { get; set; }
        public ulong Swcdhlastreboot { get; set; }
        public int Swsequence { get; set; }
        public string Outreachmessage { get; set; }
    }
}

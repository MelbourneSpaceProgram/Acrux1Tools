using System;
using System.Collections.Generic;
using System.Text;
using Acrux1Payloads.Kaitai;
using Kaitai;
using static Acrux1Payloads.Kaitai.Acrux1beacon;

namespace Acrux1Payloads
{
    public static class BeaconDecoder
    {
        public static double VoltageMaxValue = 20;
        public static double CurrentMaxValue = 20;
        public static double TemperatureMaxValue = 128;


        public static BeaconData DecodeBeacon(byte[] payload)
        {
            Acrux1beacon decodedBeacon;
            try
            {
                decodedBeacon = new Acrux1beacon(new KaitaiStream(payload));
            }
            catch
            {
                return null;
            }

            MspPayloadTDescriptor mspPayload = (decodedBeacon.Ax25Frame.Payload as UiFrameDescriptor)?.MspPayload;

            if (mspPayload is null)
            {
                return null;
            }

            return new BeaconData()
            {
                DestinationCallsign = decodedBeacon.Ax25Frame.Ax25Header.DestCallsignRaw.CallsignRor.Callsign,
                DestinationSsid = decodedBeacon.Ax25Frame.Ax25Header.DestSsidRaw.Ssid,
                SourceCallsign = decodedBeacon.Ax25Frame.Ax25Header.SrcCallsignRaw.CallsignRor.Callsign,
                SourceSsid = decodedBeacon.Ax25Frame.Ax25Header.SrcSsidRaw.Ssid,

                TxCount = mspPayload.TxCount,
                RxCount = mspPayload.RxCount,
                RxValid = mspPayload.RxValid,
                PayloadType = mspPayload.PayloadType,
                Comouti1 = ShortToCurrent(mspPayload.Comouti1),
                Comoutv1 = ShortToVoltage(mspPayload.Comoutv1),
                Comouti2 = ShortToCurrent(mspPayload.Comouti2),
                Comoutv2 = ShortToVoltage(mspPayload.Comoutv2),
                Comt2 = ShortToTemperature(mspPayload.Comt2),
                Epsadcbatv1 = ShortToVoltage(mspPayload.Epsadcbatv1),
                Epsloadi1 = ShortToCurrent(mspPayload.Epsloadi1),
                Epsadcbatv2 = ShortToVoltage(mspPayload.Epsadcbatv2),
                Epsboostini2 = ShortToCurrent(mspPayload.Epsboostini2),
                Epsrail1 = mspPayload.Epsrail1,
                Epsrail2 = mspPayload.Epsrail2,
                Epstoppanelv = ShortToVoltage(mspPayload.Epstoppanelv),
                Epstoppaneli = ShortToCurrent(mspPayload.Epstoppaneli),
                Epst1 = ShortToTemperature(mspPayload.Epst1),
                Epst2 = ShortToTemperature(mspPayload.Epst2),
                Xposv = mspPayload.Xposv,
                Xposi = mspPayload.Xposi,
                Xpost1 = mspPayload.Xpost1,
                Yposv = mspPayload.Yposv,
                Yposi = mspPayload.Yposi,
                Ypost1 = mspPayload.Ypost1,
                Xnegv = mspPayload.Xnegv,
                Xnegi = mspPayload.Xnegi,
                Xnegt1 = mspPayload.Xnegt1,
                Ynegv = mspPayload.Ynegv,
                Ynegi = mspPayload.Ynegi,
                Ynegt1 = mspPayload.Ynegt1,
                Znegv = mspPayload.Znegv,
                Znegi = mspPayload.Znegi,
                Znegt1 = mspPayload.Znegt1,
                Zpost = mspPayload.Zpost,
                Cdhtime = mspPayload.Cdhtime,
                Swcdhlastreboot = mspPayload.Swcdhlastreboot,
                Swsequence = mspPayload.Swsequence,
                Outreachmessage = mspPayload.Outreachmessage,
            };
        }

        private static double ShortToVoltage(short voltageRaw) => (double)voltageRaw * VoltageMaxValue / short.MaxValue;
        private static double ShortToCurrent(short voltageRaw) => (double)voltageRaw * CurrentMaxValue / short.MaxValue;
        private static double ShortToTemperature(short voltageRaw) => (double)voltageRaw * TemperatureMaxValue / short.MaxValue;
    }
}

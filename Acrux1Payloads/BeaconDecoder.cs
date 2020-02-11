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
        public static readonly double VoltageMaxValue = 20;
        public static readonly double CurrentMaxValue = 20;
        public static readonly double TemperatureMaxValue = 128;
        public static readonly short ShortErrorFloorValue = short.MinValue + 1;

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

            var beaconData = new BeaconData()
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
                Comouti1Raw = mspPayload.Comouti1,
                Comoutv1 = ShortToVoltage(mspPayload.Comoutv1),
                Comoutv1Raw = mspPayload.Comoutv1,
                Comouti2 = ShortToCurrent(mspPayload.Comouti2),
                Comouti2Raw = mspPayload.Comouti2,
                Comoutv2 = ShortToVoltage(mspPayload.Comoutv2),
                Comoutv2Raw = mspPayload.Comoutv2,
                Comt2 = ShortToTemperature(mspPayload.Comt2),
                Comt2Raw = mspPayload.Comt2,
                Epsadcbatv1 = ShortToVoltage(mspPayload.Epsadcbatv1),
                Epsadcbatv1Raw = mspPayload.Epsadcbatv1,
                Epsloadi1 = ShortToCurrent(mspPayload.Epsloadi1),
                Epsloadi1Raw = mspPayload.Epsloadi1,
                Epsadcbatv2 = ShortToVoltage(mspPayload.Epsadcbatv2),
                Epsadcbatv2Raw = mspPayload.Epsadcbatv2,
                Epsboostini2 = ShortToCurrent(mspPayload.Epsboostini2),
                Epsboostini2Raw = mspPayload.Epsboostini2,
                Epsrail1 = ShortToVoltage(mspPayload.Epsrail1),
                Epsrail1Raw = mspPayload.Epsrail1,
                Epsrail2 = ShortToVoltage(mspPayload.Epsrail2),
                Epsrail2Raw = mspPayload.Epsrail2,
                Epstoppanelv = ShortToVoltage(mspPayload.Epstoppanelv),
                EpstoppanelvRaw = mspPayload.Epstoppanelv,
                Epstoppaneli = ShortToCurrent(mspPayload.Epstoppaneli),
                EpstoppaneliRaw = mspPayload.Epstoppaneli,
                Epst1 = ShortToTemperature(mspPayload.Epst1),
                Epst1Raw = mspPayload.Epst1,
                Epst2 = ShortToTemperature(mspPayload.Epst2),
                Epst2Raw = mspPayload.Epst2,
                Xposv = ShortToVoltage(mspPayload.Xposv),
                XposvRaw = mspPayload.Xposv,
                Xposi = ShortToCurrent(mspPayload.Xposi),
                XposiRaw = mspPayload.Xposi,
                Xpost1 = ShortToTemperature(mspPayload.Xpost1),
                Xpost1Raw = mspPayload.Xpost1,
                Yposv = ShortToVoltage(mspPayload.Yposv),
                YposvRaw = mspPayload.Yposv,
                Yposi = ShortToCurrent(mspPayload.Yposi),
                YposiRaw = mspPayload.Yposi,
                Ypost1 = ShortToTemperature(mspPayload.Ypost1),
                Ypost1Raw = mspPayload.Ypost1,
                Xnegv = ShortToVoltage(mspPayload.Xnegv),
                XnegvRaw = mspPayload.Xnegv,
                Xnegi = ShortToCurrent(mspPayload.Xnegi),
                XnegiRaw = mspPayload.Xnegi,
                Xnegt1 = ShortToTemperature(mspPayload.Xnegt1),
                Xnegt1Raw = mspPayload.Xnegt1,
                Ynegv = ShortToVoltage(mspPayload.Ynegv),
                YnegvRaw = mspPayload.Ynegv,
                Ynegi = ShortToCurrent(mspPayload.Ynegi),
                YnegiRaw = mspPayload.Ynegi,
                Ynegt1 = ShortToTemperature(mspPayload.Ynegt1),
                Ynegt1Raw = mspPayload.Ynegt1,
                Znegv = ShortToVoltage(mspPayload.Znegv),
                ZnegvRaw = mspPayload.Znegv,
                Znegi = ShortToCurrent(mspPayload.Znegi),
                ZnegiRaw = mspPayload.Znegi,
                Znegt1 = ShortToTemperature(mspPayload.Znegt1),
                Znegt1Raw = mspPayload.Znegt1,
                Zpost = ShortToTemperature(mspPayload.Zpost),
                ZpostRaw = mspPayload.Zpost,
                Cdhtime = mspPayload.Cdhtime,
                Swcdhlastreboot = mspPayload.Swcdhlastreboot,
                Swsequence = mspPayload.Swsequence,
                Outreachmessage = mspPayload.Outreachmessage,
            };

            return beaconData;
        }

        private static double? ShortToVoltage(short voltageRaw) => voltageRaw <= ShortErrorFloorValue ? (double?)null : voltageRaw * VoltageMaxValue / short.MaxValue;
        private static double? ShortToCurrent(short currentRaw) => currentRaw <= ShortErrorFloorValue ? (double?)null : currentRaw * CurrentMaxValue / short.MaxValue;
        private static double? ShortToTemperature(short temperatureRaw) => temperatureRaw <= ShortErrorFloorValue ? (double?)null : temperatureRaw * TemperatureMaxValue / short.MaxValue;
    }
}

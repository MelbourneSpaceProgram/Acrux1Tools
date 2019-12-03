// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using Kaitai;

namespace Acrux1Payloads.Kaitai
{

    /// <summary>
    /// :field dest_callsign: ax25_frame.ax25_header.dest_callsign_raw.callsign_ror.callsign
    /// :field src_callsign: ax25_frame.ax25_header.src_callsign_raw.callsign_ror.callsign
    /// :field src_ssid: ax25_frame.ax25_header.src_ssid_raw.ssid
    /// :field dest_ssid: ax25_frame.ax25_header.dest_ssid_raw.ssid
    /// :field ctl: ax25_frame.ax25_header.ctl
    /// :field pid: ax25_frame.payload.pid
    /// :field tx_count: ax25_frame.payload.msp_payload.tx_count
    /// :field rx_count: ax25_frame.payload.msp_payload.rx_count
    /// :field rx_valid: ax25_frame.payload.msp_payload.rx_valid
    /// :field payload_type: ax25_frame.payload.msp_payload.payload_type
    /// :field comouti1: ax25_frame.payload.msp_payload.comouti1
    /// :field comoutv1: ax25_frame.payload.msp_payload.comoutv1
    /// :field comouti2: ax25_frame.payload.msp_payload.comouti2
    /// :field comoutv2: ax25_frame.payload.msp_payload.comoutv2
    /// :field comt2: ax25_frame.payload.msp_payload.comt2
    /// :field epsadcbatv1: ax25_frame.payload.msp_payload.epsadcbatv1
    /// :field epsloadi1: ax25_frame.payload.msp_payload.epsloadi1
    /// :field epsadcbatv2: ax25_frame.payload.msp_payload.epsadcbatv2
    /// :field epsboostini2: ax25_frame.payload.msp_payload.epsboostini2
    /// :field epsrail1: ax25_frame.payload.msp_payload.epsrail1
    /// :field epsrail2: ax25_frame.payload.msp_payload.epsrail2
    /// :field epstoppanelv: ax25_frame.payload.msp_payload.epstoppanelv
    /// :field epstoppaneli: ax25_frame.payload.msp_payload.epstoppaneli
    /// :field epst1: ax25_frame.payload.msp_payload.epst1
    /// :field epst2: ax25_frame.payload.msp_payload.epst2
    /// :field xposv: ax25_frame.payload.msp_payload.xposv
    /// :field xposi: ax25_frame.payload.msp_payload.xposi
    /// :field xpost1: ax25_frame.payload.msp_payload.xpost1
    /// :field yposv: ax25_frame.payload.msp_payload.yposv
    /// :field yposi: ax25_frame.payload.msp_payload.yposi
    /// :field ypost1: ax25_frame.payload.msp_payload.ypost1
    /// :field xnegv: ax25_frame.payload.msp_payload.xnegv
    /// :field xnegi: ax25_frame.payload.msp_payload.xnegi
    /// :field xnegt1: ax25_frame.payload.msp_payload.xnegt1
    /// :field ynegv: ax25_frame.payload.msp_payload.ynegv
    /// :field ynegi: ax25_frame.payload.msp_payload.ynegi
    /// :field ynegt1: ax25_frame.payload.msp_payload.ynegt1
    /// :field znegv: ax25_frame.payload.msp_payload.znegv
    /// :field znegi: ax25_frame.payload.msp_payload.znegi
    /// :field znegt1: ax25_frame.payload.msp_payload.znegt1
    /// :field zpost: ax25_frame.payload.msp_payload.zpost
    /// :field cdhtime: ax25_frame.payload.msp_payload.cdhtime
    /// :field swcdhlastreboot: ax25_frame.payload.msp_payload.swcdhlastreboot
    /// :field swsequence: ax25_frame.payload.msp_payload.swsequence
    /// :field outreachmessage: ax25_frame.payload.msp_payload.outreachmessage
    /// </summary>
    public partial class Acrux1beacon : KaitaiStruct
    {
        public static Acrux1beacon FromFile(string fileName)
        {
            return new Acrux1beacon(new KaitaiStream(fileName));
        }

        public Acrux1beacon(KaitaiStream p__io, KaitaiStruct p__parent = null, Acrux1beacon p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
            _ax25Frame = new Ax25FrameDescriptor(m_io, this, m_root);
        }
        public partial class UiFrameDescriptor : KaitaiStruct
        {
            public static UiFrameDescriptor FromFile(string fileName)
            {
                return new UiFrameDescriptor(new KaitaiStream(fileName));
            }

            public UiFrameDescriptor(KaitaiStream p__io, Acrux1beacon.Ax25FrameDescriptor p__parent = null, Acrux1beacon p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _pid = m_io.ReadU1();
                _mspPayload = new MspPayloadTDescriptor(m_io, this, m_root);
                _zeroPadding = m_io.ReadBytes(91);
                _fec8RsChecksum = m_io.ReadBytes(32);
            }
            private byte _pid;
            private MspPayloadTDescriptor _mspPayload;
            private byte[] _zeroPadding;
            private byte[] _fec8RsChecksum;
            private Acrux1beacon m_root;
            private Acrux1beacon.Ax25FrameDescriptor m_parent;
            public byte Pid { get { return _pid; } }
            public MspPayloadTDescriptor MspPayload { get { return _mspPayload; } }
            public byte[] ZeroPadding { get { return _zeroPadding; } }
            public byte[] Fec8RsChecksum { get { return _fec8RsChecksum; } }
            public Acrux1beacon M_Root { get { return m_root; } }
            public Acrux1beacon.Ax25FrameDescriptor M_Parent { get { return m_parent; } }
        }
        public partial class SsidMaskDescriptor : KaitaiStruct
        {
            public static SsidMaskDescriptor FromFile(string fileName)
            {
                return new SsidMaskDescriptor(new KaitaiStream(fileName));
            }

            public SsidMaskDescriptor(KaitaiStream p__io, Acrux1beacon.Ax25HeaderDescriptor p__parent = null, Acrux1beacon p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_ssid = false;
                _read();
            }
            private void _read()
            {
                _ssidMask = m_io.ReadU1();
            }
            private bool f_ssid;
            private int _ssid;
            public int Ssid
            {
                get
                {
                    if (f_ssid)
                        return _ssid;
                    _ssid = (int) (((SsidMask & 15) >> 1));
                    f_ssid = true;
                    return _ssid;
                }
            }
            private byte _ssidMask;
            private Acrux1beacon m_root;
            private Acrux1beacon.Ax25HeaderDescriptor m_parent;
            public byte SsidMask { get { return _ssidMask; } }
            public Acrux1beacon M_Root { get { return m_root; } }
            public Acrux1beacon.Ax25HeaderDescriptor M_Parent { get { return m_parent; } }
        }
        public partial class Ax25HeaderDescriptor : KaitaiStruct
        {
            public static Ax25HeaderDescriptor FromFile(string fileName)
            {
                return new Ax25HeaderDescriptor(new KaitaiStream(fileName));
            }

            public Ax25HeaderDescriptor(KaitaiStream p__io, Acrux1beacon.Ax25FrameDescriptor p__parent = null, Acrux1beacon p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _destCallsignRaw = new CallsignRawDescriptor(m_io, this, m_root);
                _destSsidRaw = new SsidMaskDescriptor(m_io, this, m_root);
                _srcCallsignRaw = new CallsignRawDescriptor(m_io, this, m_root);
                _srcSsidRaw = new SsidMaskDescriptor(m_io, this, m_root);
                _ctl = m_io.ReadU1();
            }
            private CallsignRawDescriptor _destCallsignRaw;
            private SsidMaskDescriptor _destSsidRaw;
            private CallsignRawDescriptor _srcCallsignRaw;
            private SsidMaskDescriptor _srcSsidRaw;
            private byte _ctl;
            private Acrux1beacon m_root;
            private Acrux1beacon.Ax25FrameDescriptor m_parent;
            public CallsignRawDescriptor DestCallsignRaw { get { return _destCallsignRaw; } }
            public SsidMaskDescriptor DestSsidRaw { get { return _destSsidRaw; } }
            public CallsignRawDescriptor SrcCallsignRaw { get { return _srcCallsignRaw; } }
            public SsidMaskDescriptor SrcSsidRaw { get { return _srcSsidRaw; } }
            public byte Ctl { get { return _ctl; } }
            public Acrux1beacon M_Root { get { return m_root; } }
            public Acrux1beacon.Ax25FrameDescriptor M_Parent { get { return m_parent; } }
        }
        public partial class MspPayloadTDescriptor : KaitaiStruct
        {
            public static MspPayloadTDescriptor FromFile(string fileName)
            {
                return new MspPayloadTDescriptor(new KaitaiStream(fileName));
            }

            public MspPayloadTDescriptor(KaitaiStream p__io, Acrux1beacon.UiFrameDescriptor p__parent = null, Acrux1beacon p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _txCount = m_io.ReadU1();
                _rxCount = m_io.ReadU1();
                _rxValid = m_io.ReadU1();
                _payloadType = m_io.ReadU1();
                _comouti1 = m_io.ReadS2le();
                _comoutv1 = m_io.ReadS2le();
                _comouti2 = m_io.ReadS2le();
                _comoutv2 = m_io.ReadS2le();
                _comt2 = m_io.ReadS2le();
                _epsadcbatv1 = m_io.ReadS2le();
                _epsloadi1 = m_io.ReadS2le();
                _epsadcbatv2 = m_io.ReadS2le();
                _epsboostini2 = m_io.ReadS2le();
                _epsrail1 = m_io.ReadS2le();
                _epsrail2 = m_io.ReadS2le();
                _epstoppanelv = m_io.ReadS2le();
                _epstoppaneli = m_io.ReadS2le();
                _epst1 = m_io.ReadS2le();
                _epst2 = m_io.ReadS2le();
                _xposv = m_io.ReadS2le();
                _xposi = m_io.ReadS2le();
                _xpost1 = m_io.ReadS2le();
                _yposv = m_io.ReadS2le();
                _yposi = m_io.ReadS2le();
                _ypost1 = m_io.ReadS2le();
                _xnegv = m_io.ReadS2le();
                _xnegi = m_io.ReadS2le();
                _xnegt1 = m_io.ReadS2le();
                _ynegv = m_io.ReadS2le();
                _ynegi = m_io.ReadS2le();
                _ynegt1 = m_io.ReadS2le();
                _znegv = m_io.ReadS2le();
                _znegi = m_io.ReadS2le();
                _znegt1 = m_io.ReadS2le();
                _zpost = m_io.ReadS2le();
                _cdhtime = m_io.ReadU8le();
                _swcdhlastreboot = m_io.ReadU8le();
                _swsequence = m_io.ReadU2le();
                _outreachmessage = System.Text.Encoding.GetEncoding("ASCII").GetString(m_io.ReadBytes(48));
            }
            private byte _txCount;
            private byte _rxCount;
            private byte _rxValid;
            private byte _payloadType;
            private short _comouti1;
            private short _comoutv1;
            private short _comouti2;
            private short _comoutv2;
            private short _comt2;
            private short _epsadcbatv1;
            private short _epsloadi1;
            private short _epsadcbatv2;
            private short _epsboostini2;
            private short _epsrail1;
            private short _epsrail2;
            private short _epstoppanelv;
            private short _epstoppaneli;
            private short _epst1;
            private short _epst2;
            private short _xposv;
            private short _xposi;
            private short _xpost1;
            private short _yposv;
            private short _yposi;
            private short _ypost1;
            private short _xnegv;
            private short _xnegi;
            private short _xnegt1;
            private short _ynegv;
            private short _ynegi;
            private short _ynegt1;
            private short _znegv;
            private short _znegi;
            private short _znegt1;
            private short _zpost;
            private ulong _cdhtime;
            private ulong _swcdhlastreboot;
            private ushort _swsequence;
            private string _outreachmessage;
            private Acrux1beacon m_root;
            private Acrux1beacon.UiFrameDescriptor m_parent;
            public byte TxCount { get { return _txCount; } }
            public byte RxCount { get { return _rxCount; } }
            public byte RxValid { get { return _rxValid; } }
            public byte PayloadType { get { return _payloadType; } }
            public short Comouti1 { get { return _comouti1; } }
            public short Comoutv1 { get { return _comoutv1; } }
            public short Comouti2 { get { return _comouti2; } }
            public short Comoutv2 { get { return _comoutv2; } }
            public short Comt2 { get { return _comt2; } }
            public short Epsadcbatv1 { get { return _epsadcbatv1; } }
            public short Epsloadi1 { get { return _epsloadi1; } }
            public short Epsadcbatv2 { get { return _epsadcbatv2; } }
            public short Epsboostini2 { get { return _epsboostini2; } }
            public short Epsrail1 { get { return _epsrail1; } }
            public short Epsrail2 { get { return _epsrail2; } }
            public short Epstoppanelv { get { return _epstoppanelv; } }
            public short Epstoppaneli { get { return _epstoppaneli; } }
            public short Epst1 { get { return _epst1; } }
            public short Epst2 { get { return _epst2; } }
            public short Xposv { get { return _xposv; } }
            public short Xposi { get { return _xposi; } }
            public short Xpost1 { get { return _xpost1; } }
            public short Yposv { get { return _yposv; } }
            public short Yposi { get { return _yposi; } }
            public short Ypost1 { get { return _ypost1; } }
            public short Xnegv { get { return _xnegv; } }
            public short Xnegi { get { return _xnegi; } }
            public short Xnegt1 { get { return _xnegt1; } }
            public short Ynegv { get { return _ynegv; } }
            public short Ynegi { get { return _ynegi; } }
            public short Ynegt1 { get { return _ynegt1; } }
            public short Znegv { get { return _znegv; } }
            public short Znegi { get { return _znegi; } }
            public short Znegt1 { get { return _znegt1; } }
            public short Zpost { get { return _zpost; } }
            public ulong Cdhtime { get { return _cdhtime; } }
            public ulong Swcdhlastreboot { get { return _swcdhlastreboot; } }
            public ushort Swsequence { get { return _swsequence; } }
            public string Outreachmessage { get { return _outreachmessage; } }
            public Acrux1beacon M_Root { get { return m_root; } }
            public Acrux1beacon.UiFrameDescriptor M_Parent { get { return m_parent; } }
        }
        public partial class CallsignDescriptor : KaitaiStruct
        {
            public static CallsignDescriptor FromFile(string fileName)
            {
                return new CallsignDescriptor(new KaitaiStream(fileName));
            }

            public CallsignDescriptor(KaitaiStream p__io, Acrux1beacon.CallsignRawDescriptor p__parent = null, Acrux1beacon p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _callsign = System.Text.Encoding.GetEncoding("ASCII").GetString(m_io.ReadBytes(6));
            }
            private string _callsign;
            private Acrux1beacon m_root;
            private Acrux1beacon.CallsignRawDescriptor m_parent;
            public string Callsign { get { return _callsign; } }
            public Acrux1beacon M_Root { get { return m_root; } }
            public Acrux1beacon.CallsignRawDescriptor M_Parent { get { return m_parent; } }
        }
        public partial class IFrameDescriptor : KaitaiStruct
        {
            public static IFrameDescriptor FromFile(string fileName)
            {
                return new IFrameDescriptor(new KaitaiStream(fileName));
            }

            public IFrameDescriptor(KaitaiStream p__io, Acrux1beacon.Ax25FrameDescriptor p__parent = null, Acrux1beacon p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _pid = m_io.ReadU1();
                _ax25Info = m_io.ReadBytesFull();
            }
            private byte _pid;
            private byte[] _ax25Info;
            private Acrux1beacon m_root;
            private Acrux1beacon.Ax25FrameDescriptor m_parent;
            public byte Pid { get { return _pid; } }
            public byte[] Ax25Info { get { return _ax25Info; } }
            public Acrux1beacon M_Root { get { return m_root; } }
            public Acrux1beacon.Ax25FrameDescriptor M_Parent { get { return m_parent; } }
        }
        public partial class CallsignRawDescriptor : KaitaiStruct
        {
            public static CallsignRawDescriptor FromFile(string fileName)
            {
                return new CallsignRawDescriptor(new KaitaiStream(fileName));
            }

            public CallsignRawDescriptor(KaitaiStream p__io, Acrux1beacon.Ax25HeaderDescriptor p__parent = null, Acrux1beacon p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                __raw__raw_callsignRor = m_io.ReadBytes(6);
                __raw_callsignRor = m_io.ProcessRotateLeft(__raw__raw_callsignRor, 8 - (1), 1);
                var io___raw_callsignRor = new KaitaiStream(__raw_callsignRor);
                _callsignRor = new CallsignDescriptor(io___raw_callsignRor, this, m_root);
            }
            private CallsignDescriptor _callsignRor;
            private Acrux1beacon m_root;
            private Acrux1beacon.Ax25HeaderDescriptor m_parent;
            private byte[] __raw__raw_callsignRor;
            private byte[] __raw_callsignRor;
            public CallsignDescriptor CallsignRor { get { return _callsignRor; } }
            public Acrux1beacon M_Root { get { return m_root; } }
            public Acrux1beacon.Ax25HeaderDescriptor M_Parent { get { return m_parent; } }
            public byte[] M_RawM_RawCallsignRor { get { return __raw__raw_callsignRor; } }
            public byte[] M_RawCallsignRor { get { return __raw_callsignRor; } }
        }
        public partial class Ax25FrameDescriptor : KaitaiStruct
        {
            public static Ax25FrameDescriptor FromFile(string fileName)
            {
                return new Ax25FrameDescriptor(new KaitaiStream(fileName));
            }

            public Ax25FrameDescriptor(KaitaiStream p__io, Acrux1beacon p__parent = null, Acrux1beacon p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _ax25Header = new Ax25HeaderDescriptor(m_io, this, m_root);
                switch ((Ax25Header.Ctl & 19)) {
                case 0: {
                    _payload = new IFrameDescriptor(m_io, this, m_root);
                    break;
                }
                case 3: {
                    _payload = new UiFrameDescriptor(m_io, this, m_root);
                    break;
                }
                case 19: {
                    _payload = new UiFrameDescriptor(m_io, this, m_root);
                    break;
                }
                case 16: {
                    _payload = new IFrameDescriptor(m_io, this, m_root);
                    break;
                }
                case 18: {
                    _payload = new IFrameDescriptor(m_io, this, m_root);
                    break;
                }
                case 2: {
                    _payload = new IFrameDescriptor(m_io, this, m_root);
                    break;
                }
                }
            }
            private Ax25HeaderDescriptor _ax25Header;
            private KaitaiStruct _payload;
            private Acrux1beacon m_root;
            private Acrux1beacon m_parent;
            public Ax25HeaderDescriptor Ax25Header { get { return _ax25Header; } }
            public KaitaiStruct Payload { get { return _payload; } }
            public Acrux1beacon M_Root { get { return m_root; } }
            public Acrux1beacon M_Parent { get { return m_parent; } }
        }
        private Ax25FrameDescriptor _ax25Frame;
        private Acrux1beacon m_root;
        private KaitaiStruct m_parent;

        /// <remarks>
        /// Reference: <a href="https://www.tapr.org/pub_ax25.html">Source</a>
        /// </remarks>
        public Ax25FrameDescriptor Ax25Frame { get { return _ax25Frame; } }
        public Acrux1beacon M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}

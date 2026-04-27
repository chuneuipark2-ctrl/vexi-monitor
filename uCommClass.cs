using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;  
using System.Windows.Forms;

//[TPacketHeaderRec, TPacketBodyRec, TPacketFooterRec, TPacketRec]
//패킷 구조체
//[TPacketClass] 
//CRC 계산, 패킷 => byte array 반환 등
//[TStreamBufClass]
//데이터수신 버퍼
//[TParsingRec]
//데이터 파싱 관련 변수 구조체
//[TPollingRec]
//폴링 관련 변수 구조체
//[TPACKETStruct]
//CMD별 데이터를 저장하기 위한 구조체
//[TCOMMDataManager]
//통신 클래스
//UDP 소켓, 시리얼포트 
//수신 데이터 파싱, 폴링등 전반적인 통신 관리

namespace VEXI
{
    public delegate void TOnCommDataReceived(byte TmpCommType, string IP, ushort Port, ushort Len);
    public delegate void TOnDebugging(string captionStr, string debugStr);

    public delegate void TOnPacketReceived(byte TmpCommType, bool CRCOK, TPacketClass PaketObj, byte RevCRC1, byte RevCRC2, byte RevETX);
    public delegate void TOnPacketSended(byte TmpCommType, byte[] PaketBytes);
    public delegate void TCheckManualCtrl();


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TPacketHeaderRec
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] StartCode;
        public byte SrcType;
        public byte SrcID;
        public byte DstType;
        public byte DstID;
        public byte Seq;
        public byte bypass1;
        public byte bypass2;
        public byte CMD1;
        public ushort Len; //DATA 길이 + 1 (CMD2 길이)
        public byte CMD2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TPacketBodyRec
    {
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = ConstClass.U_PACKET_DATAMAX_SIZE - 1)]
        public byte[] Data;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TPacketFooterRec
    {
        public ushort CRC;
        public byte ETX;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TPacketRec
    {
        public TPacketHeaderRec fpacketHeader;
        public TPacketBodyRec fpacketBody;
        public TPacketFooterRec fpacketFooter;
    }


    public class TPacketClass 
    {
        private TPacketRec fpacketRec;
        private ushort RecvCRC;
        public bool IsCorrectPacket;

        public TPacketClass()
        {
            fpacketRec.fpacketHeader.StartCode = new byte[4];
            fpacketRec.fpacketHeader.StartCode[0] = 0x16;
            fpacketRec.fpacketHeader.StartCode[1] = 0x16;
            fpacketRec.fpacketHeader.StartCode[2] = 0x16;
            fpacketRec.fpacketHeader.StartCode[3] = 0x16;
            fpacketRec.fpacketHeader.SrcType = 0x00;
            fpacketRec.fpacketHeader.SrcID = 0x00;
            fpacketRec.fpacketHeader.DstType = 0x00;
            fpacketRec.fpacketHeader.DstID = 0x00;
            fpacketRec.fpacketHeader.Seq = 0x00;
            fpacketRec.fpacketHeader.bypass1 = 0x00;
            fpacketRec.fpacketHeader.bypass2 = 0x00;
            fpacketRec.fpacketHeader.CMD1 = 0x00;
            fpacketRec.fpacketHeader.Len = 0x0001;
            fpacketRec.fpacketHeader.CMD2 = 0x00;
            fpacketRec.fpacketFooter.CRC = 0x0000;
            fpacketRec.fpacketFooter.ETX = 0xF5;
            RecvCRC = 0x0000;
        }

        public TPacketClass(byte[] bytes)
        {
            fpacketRec.fpacketHeader.StartCode = new byte[4];
            fpacketRec.fpacketHeader.StartCode[0] = bytes[0];
            fpacketRec.fpacketHeader.StartCode[1] = bytes[1];
            fpacketRec.fpacketHeader.StartCode[2] = bytes[2];
            fpacketRec.fpacketHeader.StartCode[3] = bytes[3];
            fpacketRec.fpacketHeader.SrcType = bytes[4];
            fpacketRec.fpacketHeader.SrcID = bytes[5];
            fpacketRec.fpacketHeader.DstType = bytes[6];
            fpacketRec.fpacketHeader.DstID = bytes[7];
            fpacketRec.fpacketHeader.Seq = bytes[8];
            fpacketRec.fpacketHeader.bypass1 = bytes[9];
            fpacketRec.fpacketHeader.bypass2 = bytes[10];
            fpacketRec.fpacketHeader.CMD1 = bytes[11];
            fpacketRec.fpacketHeader.Len = BitConverter.ToUInt16(bytes, 12);
            fpacketRec.fpacketHeader.CMD2 = bytes[14];

            RecvCRC = BitConverter.ToUInt16(bytes, 15 + fpacketRec.fpacketHeader.Len - 1);

            if (fpacketRec.fpacketHeader.Len > 1)
            {
                fpacketRec.fpacketBody.Data = Enumerable.Repeat<byte>(0, fpacketRec.fpacketHeader.Len - 1).ToArray<byte>();
                //fpacketRec.fpacketBody.Data = Enumerable.Repeat<byte>(0, ConstClass.U_PACKET_DATAMAX_SIZE - 1).ToArray<byte>();
                Array.Copy(bytes, 15, fpacketRec.fpacketBody.Data, 0, fpacketRec.fpacketHeader.Len - 1);
            }

            DoCalc_CRC();

            IsCorrectPacket = (bytes[bytes.Length - 1] == 0xF5) && (RecvCRC == fpacketRec.fpacketFooter.CRC);
        }

        public void SetHeader(byte[] bytes)
        {
            fpacketRec.fpacketHeader.SrcType = bytes[0];
            fpacketRec.fpacketHeader.SrcID = bytes[1];
            fpacketRec.fpacketHeader.DstType = bytes[2];
            fpacketRec.fpacketHeader.DstID = bytes[3];
            fpacketRec.fpacketHeader.Seq = bytes[4];
            fpacketRec.fpacketHeader.bypass1 = bytes[5];
            fpacketRec.fpacketHeader.bypass2 = bytes[6];
            fpacketRec.fpacketHeader.CMD1 = bytes[7];
            fpacketRec.fpacketHeader.Len = BitConverter.ToUInt16(bytes, 8);
            fpacketRec.fpacketHeader.CMD2 = bytes[10];


            if (fpacketRec.fpacketHeader.Len > 1)
            {
                fpacketRec.fpacketBody.Data = Enumerable.Repeat<byte>(0, fpacketRec.fpacketHeader.Len - 1).ToArray<byte>();
            }
            else
            {
                DoCalc_CRC();
            }
        }

        public byte CMD1
        {
            get { return fpacketRec.fpacketHeader.CMD1; }
        }
        public byte CMD2
        {
            get { return fpacketRec.fpacketHeader.CMD2; }
        }

        public byte SEQ
        {
            get { return fpacketRec.fpacketHeader.Seq; }
        }
        
        public byte SrcDevType
        {
            get { return fpacketRec.fpacketHeader.SrcType; }
        }
        public byte SrcID
        {
            get { return fpacketRec.fpacketHeader.SrcID; }
        }

        public byte Get_CMD2()
        {
            return fpacketRec.fpacketHeader.CMD2;
        }

        public void SetHeader(byte TmpSrcType, byte TmpSrcID, byte TmpDestType, byte TmpDestID, byte TmpSeq, byte TmpCMD1, byte TmpCMD2, ushort TmpLen)
        {
            fpacketRec.fpacketHeader.SrcType = TmpSrcType;
            fpacketRec.fpacketHeader.SrcID = TmpSrcID;
            fpacketRec.fpacketHeader.DstType = TmpDestType;
            fpacketRec.fpacketHeader.DstID = TmpDestID;
            fpacketRec.fpacketHeader.Seq = TmpSeq;
            fpacketRec.fpacketHeader.bypass1 = 0x00;
            fpacketRec.fpacketHeader.bypass2 = 0x00;
            fpacketRec.fpacketHeader.CMD1 = TmpCMD1;
            fpacketRec.fpacketHeader.Len = TmpLen;
            fpacketRec.fpacketHeader.CMD2 = TmpCMD2;


            if (fpacketRec.fpacketHeader.Len > 1)
            {
                fpacketRec.fpacketBody.Data = Enumerable.Repeat<byte>(0, fpacketRec.fpacketHeader.Len - 1).ToArray<byte>();
            }
            else
            {
                DoCalc_CRC();
            }
        }

        public void DoCalc_CRC()
        {
            fpacketRec.fpacketFooter.CRC = CalcCRC();
            fpacketRec.fpacketFooter.ETX = 0xF5;
        }

        public void SetBody(object TmpDataRec)
        {
            Global_Class.UTIL_StructObjectToByteArray(TmpDataRec, fpacketRec.fpacketBody.Data);

            DoCalc_CRC();
        }

        public void SetBody(UInt16 TmpDataLen,  object TmpDataRec)
        {
            Global_Class.UTIL_StructObjectToByteArray(TmpDataLen, TmpDataRec, fpacketRec.fpacketBody.Data);

            DoCalc_CRC();            
        }

        public void SetBody(byte[] bytes)
        {
            Array.Copy(bytes, 0, fpacketRec.fpacketBody.Data, 0, fpacketRec.fpacketHeader.Len - 1);

            DoCalc_CRC();
        }

        public void SetBody(byte Data)
        {
            fpacketRec.fpacketBody.Data[0] = Data;

            DoCalc_CRC();
        }

        //참고용으로 구현해 본 것임
        public void SetBody2(object Rec)
        {
            // 비관리 메모리 영역에 구조체 크기만큼의 메모리를 할당한다.
            IntPtr buff = Marshal.AllocHGlobal(fpacketRec.fpacketHeader.Len - 1);

            // 할당된 구조체 객체의 주소를 구한다.
            Marshal.StructureToPtr(Rec, buff, false);

            // 구조체 객체를 배열에 복사
            Marshal.Copy(buff, fpacketRec.fpacketBody.Data, 0, fpacketRec.fpacketHeader.Len - 1);

            // 비관리 메모리 영역에 할당했던 메모리를 해제함
            Marshal.FreeHGlobal(buff);

            DoCalc_CRC();
        }


        public void SetTotalbytes(byte[] bytes)
        {
            fpacketRec.fpacketHeader.StartCode[0] = bytes[0];
            fpacketRec.fpacketHeader.StartCode[1] = bytes[1];
            fpacketRec.fpacketHeader.StartCode[2] = bytes[2];
            fpacketRec.fpacketHeader.StartCode[3] = bytes[3];
            fpacketRec.fpacketHeader.SrcType = bytes[4];
            fpacketRec.fpacketHeader.SrcID = bytes[5];
            fpacketRec.fpacketHeader.DstType = bytes[6];
            fpacketRec.fpacketHeader.DstID = bytes[7];
            fpacketRec.fpacketHeader.Seq = bytes[8];
            fpacketRec.fpacketHeader.bypass1 = bytes[9];
            fpacketRec.fpacketHeader.bypass2 = bytes[10];
            fpacketRec.fpacketHeader.CMD1 = bytes[11];
            fpacketRec.fpacketHeader.Len = BitConverter.ToUInt16(bytes, 12);
            fpacketRec.fpacketHeader.CMD2 = bytes[14];

            RecvCRC = BitConverter.ToUInt16(bytes, 15 + fpacketRec.fpacketHeader.Len - 1);

            if (fpacketRec.fpacketHeader.Len > 1)
            {
                fpacketRec.fpacketBody.Data = Enumerable.Repeat<byte>(0, fpacketRec.fpacketHeader.Len - 1).ToArray<byte>();
                Array.Copy(bytes, 15, fpacketRec.fpacketBody.Data, 0, fpacketRec.fpacketHeader.Len - 1);
            }

            fpacketRec.fpacketFooter.CRC = RecvCRC;
            fpacketRec.fpacketFooter.ETX = 0xF5;

            IsCorrectPacket = true;
        }

        public void CrcCompare(byte[] bytes)
        {
            ushort Src_CRC;
            Src_CRC = (ushort)(bytes[1] * 256 + bytes[0]);

            IsCorrectPacket = (bytes[2] == 0xF5) && (Src_CRC == fpacketRec.fpacketFooter.CRC);
        }

        private ushort CalcCRC()
        {
            ushort fcrc = 0;
            ushort i = GetTotalSize();
            byte[] Tmptotalbytes = GetTotalBytes();
            fcrc = Global_Class.UTIL_CheckSum(Tmptotalbytes, 4, (ushort)(i - 7));
            return fcrc;
        }

        public byte[] GetTotalBytes()
        {
            int len_Header = Marshal.SizeOf(typeof(TPacketHeaderRec));
            int len_Body = fpacketRec.fpacketHeader.Len - 1;//TestPacketStrunct.fpacketBody.Data.Length;
            int len_Footer = Marshal.SizeOf(typeof(TPacketFooterRec));
            int len_Total = len_Header + len_Body + len_Footer;

            byte[] bytes = new byte[len_Total];


            IntPtr buff;
            buff = Marshal.AllocHGlobal(len_Header);
            Marshal.StructureToPtr(fpacketRec.fpacketHeader, buff, false);
            Marshal.Copy(buff, bytes, 0, len_Header);
            Marshal.FreeHGlobal(buff);

            if (fpacketRec.fpacketHeader.Len > 1)
            {
                Array.Copy(fpacketRec.fpacketBody.Data, 0, bytes, len_Header, len_Body);
            }
            buff = Marshal.AllocHGlobal(len_Footer);
            Marshal.StructureToPtr(fpacketRec.fpacketFooter, buff, false);
            Marshal.Copy(buff, bytes, len_Header + len_Body, len_Footer);
            Marshal.FreeHGlobal(buff);

            return bytes;
        }
        public byte[] GetDataBytes()
        {
            ushort i = GetDataSize();


            if (i < 1)
            {
                return null;
            }
            else
            {
                byte[] bytes = new byte[i];

                Array.Copy(fpacketRec.fpacketBody.Data, 0, bytes, 0, i);
                return bytes;
            }
        }

        public ushort GetTotalSize()
        {
            ushort i = 0;
            i = (ushort)(fpacketRec.fpacketHeader.Len + (Marshal.SizeOf(typeof(TPacketHeaderRec)) + Marshal.SizeOf(typeof(TPacketFooterRec)) - 1));
            return i;
        }
        public ushort GetDataSize()
        {
            ushort i = 0;
            //i = fpacketRec.fpacketHeader.DataLen;
            i = (ushort)(fpacketRec.fpacketHeader.Len - 1);
            return i;
        }
    }

    public class TStreamBufClass
    {
        private byte[] m_buffer = new byte[ConstClass.U_RECV_MAX_SIZE];
        public int W_offset = -1;
        public int R_offset = -1;
        public byte BufferID = 0;


        public TStreamBufClass()
        {
            ResetOffset();
        }


        public void ResetOffset()
        {
            W_offset = -1;
            R_offset = -1;
        }

        public void Writebyte(byte value)
        {
            W_offset += 1;
            if (W_offset > (ConstClass.U_RECV_MAX_SIZE - 1))
            {
                W_offset = 0;
            }

            m_buffer[W_offset] = value;
        }
        public void Writebytes(byte[] value)
        {
            ushort srcLen = (ushort)value.Length;
           

            for (ushort i = 0; i < srcLen; i++)
            {
                W_offset += 1;
                if (W_offset > (ConstClass.U_RECV_MAX_SIZE - 1))
                {
                    W_offset = 0;
                }

                m_buffer[W_offset] = value[i];
            }

        }

        public void Writebytes(byte[] value, ushort arrayLen)
        {
            ushort srcLen = arrayLen;
            

            for (ushort i = 0; i < srcLen; i++)
            {
                W_offset += 1;
                if (W_offset > (ConstClass.U_RECV_MAX_SIZE - 1))
                {
                    W_offset = 0;
                }

                m_buffer[W_offset] = value[i];
            }
        }

        public ushort ReadDataCount()
        {
            ushort TmpDatacount = 0;
            if (W_offset != -1)
            {
                if (W_offset != R_offset)
                {
                    if (W_offset > R_offset)
                    {
                        TmpDatacount = (ushort)(W_offset - R_offset);
                    }
                    else
                    {
                        TmpDatacount = (ushort)((ConstClass.U_RECV_MAX_SIZE - 1) - R_offset + 1 + W_offset);
                    }
                }
            }

            return TmpDatacount;
        }

        public byte Readbyte(ref byte data)
        {
            byte readcount = 0;
            if (W_offset != -1)
            {
                if (W_offset != R_offset)
                {
                    readcount = 1;

                    R_offset += 1;
                    if (R_offset > (ConstClass.U_RECV_MAX_SIZE - 1))
                    {
                        R_offset = 0;
                    }
                    data = m_buffer[R_offset];
                }
                else
                {
                    data = 0;
                }
            }
            return readcount;
        }

        public ushort Readbytes(ushort readLen, ref byte[] data)
        {
            byte readcount = 0;
            if (W_offset != -1)
            {
                for (ushort i = 0; i < readLen; i++)
                {
                    if (W_offset != R_offset)
                    {
                        readcount += 1 ;

                        R_offset += 1;
                        if (R_offset > (ConstClass.U_RECV_MAX_SIZE - 1))
                        {
                            R_offset = 0;
                        }

                        data[i] = m_buffer[R_offset];
                    }
                }
            }
            return readcount;
        }

    }

    public struct TParsingRec
    {
        public byte PacketMode;
        public ushort DataLen;
        public ushort ReadByteCnt;
        public byte[] ParsingBuffer;
    }

    public struct TPollingRec
    {
        public DateTime TxCheckTime;
        public DateTime TxLastTime;
        public DateTime TxRepeatCtrlCheckTime;
        public bool IsPolingStop; //디버깅 용도때문에 필요해서
        public bool IsPolingEnable; //다운로드 폴링 멈춤
        public bool IsPolingDelayStop; //일시적으로 폴링 멈춤
        public bool IsTestPolling; //Test 화면 볼때는 Test 상태도 자동 갱신하기 위해
        public bool IsInvertorPolling; //Invertor 화면 볼때는 Invertor 상태도 자동 갱신하기 위해
        public bool IsWcsMapPolling; //WcsMap 화면 볼때는 WcsMap 상태도 자동 갱신하기 위해
        public byte PollingFlag;
        public byte PollingStFlag;

        public DateTime SendTime;
        public DateTime ReceiveTime;
    }

    public struct TSemiCmdRec
    {
        public UInt32 Random_WorkNum;
    }

    public struct TPollingDelayRec
    {
        public bool ISDelay;
        public DateTime TxDateTime;
    }

    public class TCOMMDataManager
    {
        public bool IsRealUse = false;

        public byte CommInterval = 50;
        
        public VEXI_DEFS.TDEV_REC DevRec;
        public byte LoggingMode = 0;
        public int Logginginterval = 500;
        private sbyte saveSec = -1;
        private string CommDataSaveDir;
        private string SpeedDataSaveDir;
        private DateTime DataSaveTime;
        private List<string> DevStSaveBuffer = new List<string>();
        private string[] WriteBuffer = new string[100];

        private TParsingRec ParsingRec;
        private TPollingRec PollingRec;
        private TSemiCmdRec SemiCmdRec;
        private TPollingDelayRec PollingDelayRec;

        private TStreamBufClass[] fStreamBufClasses = new TStreamBufClass[3];
        private List<byte[]> UserTxDataList = new List<byte[]>();
        private byte fCientCount;
        private Socket Sck;
        private SerialPort fSerialPort;
        private Timer packetTimer;
        private TPacketClass RxPacket = new TPacketClass();
        private TPacketClass TxPacket = new TPacketClass();
        private AsyncCallback UDPReadCallBack = null;

        private bool IsTxErr = false;
        private byte CommMode;
        private object UDPlockObject = new object();
        private byte UDPReadFlag;
        private byte TXSEQ = 0;

        private byte UserSelect_DevType;
        private byte UserSelect_DevID;
        private byte Real_DevType;
        private byte Real_DevID;

        byte[] Readbuffer = new byte[ConstClass.U_RECV_MAX_SIZE];

        private EndPoint RemotePoint = new IPEndPoint(IPAddress.Any, ConstClass.MCUConnect_PORT);
        private EndPoint RecvRemotePoint = new IPEndPoint(IPAddress.Any, ConstClass.MCUConnect_PORT);
        IAsyncResult AsyncResult_UDP;


        private ushort _RemtePort; 

        public event TOnDebugging OnDebugging;
        //public event TOnCommDataReceived OnCommDataReceived;
        public event TOnPacketReceived OnPacketReceived;
        public event TOnPacketSended OnPacketSended;
        public event TCheckManualCtrl OnCheckJogCtrl;

        public TCOMMDataManager(string TmpSaveDir)
        {
            _RemtePort = ConstClass.MCUConnect_PORT;

            CommDataSaveDir = TmpSaveDir + "\\LOGGING\\";
            SpeedDataSaveDir = TmpSaveDir + "\\SPEED_LOGGING\\";

            SemiCmdRec.Random_WorkNum = 0x00010000;

            PollingRec.IsPolingStop = false;
            PollingRec.IsPolingEnable = true;
            PollingRec.IsTestPolling = false;
            PollingRec.IsInvertorPolling = false;
            PollingRec.PollingFlag = 0;
            PollingRec.PollingStFlag = 0;
            

            ParsingRec.PacketMode = ConstClass.PasingStartCode;
            ParsingRec.DataLen = 0;
            ParsingRec.ReadByteCnt = 0;

            fSerialPort = new SerialPort();
            fSerialPort.BaudRate = (int)115200;
            fSerialPort.DataReceived += new SerialDataReceivedEventHandler(EventDataReceived);

            packetTimer = new Timer();
            packetTimer.Interval = 20;
            packetTimer.Tick += new EventHandler(EventPacketTimer);
            packetTimer.Enabled = true;
        }

        ~TCOMMDataManager()
        {
            SaveCommDataLogging(true, UserSelect_DevType);
            packetTimer.Enabled = false;
            if (Sck != null)
            {
                Sck.Shutdown(SocketShutdown.Both);
                Sck.Close();
            }
            fSerialPort.Close();
        }

        public byte COMM_Mode
        {
            get { return CommMode; }
        }
        public byte UserDataCount
        {
            get { return (byte) UserTxDataList.Count; }
        }

        public ushort RemotePort
        {
            get { return _RemtePort; }
            set { _RemtePort = value; }
        }

        // 선택한 장치 타입이 SRM, RTV, EMS가 아닌 경우 수신된 장치 타입이 SRM, RTV, EMS 라면 수신된 장치 타입을 반환
        //수신된 장치 타입도 SRM, RTV, EMS 가 아니라면 선택한 장치 타입값 그대로 반환 
        public byte SelectDestDevType_WithOutANY
        {
            get
            {
                if ((UserSelect_DevType == ConstClass.TYPE_SRM) || (UserSelect_DevType == ConstClass.TYPE_RTV) || (UserSelect_DevType == ConstClass.TYPE_EMS))
                {
                    return UserSelect_DevType;
                }
                else
                {
                    if ((Real_DevType == ConstClass.TYPE_SRM) || (Real_DevType == ConstClass.TYPE_RTV) || (Real_DevType == ConstClass.TYPE_EMS))
                    {
                        return Real_DevType;
                    }
                    else
                    {
                        return UserSelect_DevType;
                    }
                }
            }
        }

        public byte SelectDestDevType
        {
            get { return UserSelect_DevType; }
            set
            {
                if (value != UserSelect_DevType)
                {

                    DevRec.Flag_In_DevStatus = false;
                    DevRec.Flag_In_8110 = false;
                    DevRec.Flag_In_8112 = false;
                }
                UserSelect_DevType = value;
                Real_DevType = value;
            }
        }
        public byte SelectDestDevID
        {
            get { return UserSelect_DevID; }
            set {
                if (value != UserSelect_DevID)
                {
                    DevRec.Flag_In_DevStatus = false;
                    DevRec.Flag_In_8110 = false;
                    DevRec.Flag_In_8112 = false;
                }
                UserSelect_DevID = value;
                Real_DevID = value; }
        }

        public byte RX_DestDevType
        {
            get { return Real_DevType; }
            set { if (value != RX_DestDevType)
                  {
                    
                    DevRec.Flag_In_DevStatus = false;
                    DevRec.Flag_In_8110 = false;
                    DevRec.Flag_In_8112 = false;
                  }
                  Real_DevType = value;
                  }
        }
        public byte RX_DestDevID
        {
            get { return Real_DevID; }
            set {
                if (value != Real_DevID)
                {
                    DevRec.Flag_In_DevStatus = false;
                    DevRec.Flag_In_8110 = false;
                    DevRec.Flag_In_8112 = false;
                }
                Real_DevID = value; 
            }
        }
        

        public bool ISPollingEnable
        {

            get { return PollingRec.IsPolingEnable; }
            set { PollingRec.IsPolingEnable = value; }
        }

        public bool ISPolingStop
        {

            get { return PollingRec.IsPolingStop; }
            set { PollingRec.IsPolingStop = value; }
        }

        //설정 후 보드에 시간을 줘야 할 때 사용됨. (보드가 내부 메모리에 저장할 시간을 줘야 하는 이유에서 사용)
        public bool ISPolingDelayStop
        {

            get { return PollingDelayRec.ISDelay; }
            set { PollingDelayRec.ISDelay = value; }
        }

        public DateTime PolingDelayTxTime
        {

            get { return PollingDelayRec.TxDateTime; }
            set { PollingDelayRec.TxDateTime = value; }
        }

        public bool ISTestPolling
        {

            get { return PollingRec.IsTestPolling; }
            set { PollingRec.IsTestPolling = value; }
        }

        public bool ISInvertorPolling
        {

            get { return PollingRec.IsInvertorPolling; }
            set { PollingRec.IsInvertorPolling = value; }
        }

        public bool ISWcsMapPolling
        {

            get { return PollingRec.IsWcsMapPolling; }
            set { PollingRec.IsWcsMapPolling = value; }
        }

        

        public string SerialPort
        {
            get { return fSerialPort.PortName; }
        }

        public string UDPIP
        {
            get { return ((IPEndPoint)RemotePoint).Address.ToString(); }
        }

        public bool ISCOMM_ResponsGood
        {
            get
            {
                if (DateTime.Compare(PollingRec.SendTime, DateTime.Now) > 0)
                {
                    PollingRec.SendTime = DateTime.Now;
                }
                TimeSpan ts = DateTime.Now - PollingRec.SendTime;
                if ((CommSt == 0) || (ts.TotalSeconds > 3))
                {
                    return false;
                }
                else
                {
                    if (DateTime.Compare(PollingRec.SendTime, PollingRec.ReceiveTime) > 0)
                    {
                        ts = PollingRec.SendTime - PollingRec.ReceiveTime;
                        if (ts.TotalSeconds > 3)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    } else
                    {
                        return true;
                    }
                    
                }
            }
        }

        public byte CommSt
        {
            get
            {
                if ((CommMode != ConstClass.COMM_SERIAL) && (CommMode != ConstClass.COMM_UDP))
                {
                    return 0;
                }
                else
                {
                    if (CommMode == ConstClass.COMM_SERIAL)
                    {
                        if (fSerialPort.IsOpen) return 1;
                        else return 0;

                    }
                    else
                    {
                        if (Sck != null) return 2;
                        else return 0;
                    }
                }
            }
        }

        public bool TxErrSt
        {
            get 
            { return IsTxErr; }
        }

        public UInt32 Random_WorkNum_AndInc
        {
            get { return SemiCmdRec.Random_WorkNum++; }
        }

        public void ADD_TxUserData(byte[] TmpData)
        {
            if (CommSt != 0)
            {
                if (UserTxDataList.Count > 20)
                {
                    UserTxDataList.Clear();
                }
                UserTxDataList.Add(TmpData);
            } else
            {
                UserTxDataList.Clear();
            }
        }

        public void RefreshTxRepeatCtrlCheckTime()
        {
            PollingRec.TxRepeatCtrlCheckTime = DateTime.Now;
        }

        public byte ADD_TxUserData(byte TmpSrcType, byte TmpSrcID, byte TmpCMD1, byte TmpCMD2, byte TmpDataBody)
        {
            if (CommSt != 0)
            {
                TXSEQ++;
                if (IsRealUse)
                {
                    TxPacket.SetHeader(TmpSrcType, TmpSrcID, Real_DevType, Real_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(1 + 1));
                } else
                {
                    TxPacket.SetHeader(TmpSrcType, TmpSrcID, UserSelect_DevType, UserSelect_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(1 + 1));
                }
                TxPacket.SetBody(TmpDataBody);
                ADD_TxUserData(TxPacket.GetTotalBytes());
            }
            return TXSEQ;
        }

        public byte ADD_TxUserData(byte TmpSrcType, byte TmpSrcID, byte TmpCMD1, byte TmpCMD2)
        {
            if (CommSt != 0)
            {
                TXSEQ++;
                if (IsRealUse)
                {
                    TxPacket.SetHeader(TmpSrcType, TmpSrcID, Real_DevType, Real_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(1));
                }
                else
                {
                    TxPacket.SetHeader(TmpSrcType, TmpSrcID, UserSelect_DevType, UserSelect_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(1));
                }
                TxPacket.DoCalc_CRC();
                ADD_TxUserData(TxPacket.GetTotalBytes());
            }
            return TXSEQ;
        }
        public byte ADD_TxUserData(byte TmpSrcType, byte TmpSrcID, byte TmpCMD1, byte TmpCMD2, byte[] TmpDataBody)
        {
            if (CommSt != 0)
            {
                TXSEQ++;
                if (IsRealUse)
                {
                    TxPacket.SetHeader(TmpSrcType, TmpSrcID, Real_DevType, Real_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(TmpDataBody.Length + 1));
                } else
                {
                    TxPacket.SetHeader(TmpSrcType, TmpSrcID, UserSelect_DevType, UserSelect_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(TmpDataBody.Length + 1));
                }

                TxPacket.SetBody(TmpDataBody);
                ADD_TxUserData(TxPacket.GetTotalBytes());
            }
            return TXSEQ;
        }

        public void ADD_TxUserDataBeforeClear(byte TmpSrcType, byte TmpSrcID, byte TmpCMD1, byte TmpCMD2, Object TmpRec)
        {
            if (CommSt != 0)
            {

                TXSEQ++;
                if (IsRealUse)
                {
                    TxPacket.SetHeader(TmpSrcType, TmpSrcID, Real_DevType, Real_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(Marshal.SizeOf(TmpRec) + 1));
                } else
                {
                    TxPacket.SetHeader(TmpSrcType, TmpSrcID, UserSelect_DevType, UserSelect_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(Marshal.SizeOf(TmpRec) + 1));
                }
                TxPacket.SetBody(TmpRec);

                UserTxDataList.Clear();
                ADD_TxUserData(TxPacket.GetTotalBytes());
            }
        }

        public void ADD_TxUserData(byte TmpSrcType, byte TmpSrcID, byte TmpCMD1, byte TmpCMD2, Object TmpRec)
        {
            if (CommSt != 0)
            {

                TXSEQ++;
                if (IsRealUse)
                {
                    TxPacket.SetHeader(TmpSrcType, TmpSrcID, Real_DevType, Real_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(Marshal.SizeOf(TmpRec) + 1));
                } else
                {
                    TxPacket.SetHeader(TmpSrcType, TmpSrcID, UserSelect_DevType, UserSelect_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(Marshal.SizeOf(TmpRec) + 1));
                }
                TxPacket.SetBody(TmpRec);
                ADD_TxUserData(TxPacket.GetTotalBytes());
            }
        }

        public void ADD_TxUserData_UserSeletDev(byte TmpSrcType, byte TmpSrcID, byte TmpCMD1, byte TmpCMD2, Object TmpRec)
        {
            if (CommSt != 0)
            {

                TXSEQ++;
                TxPacket.SetHeader(TmpSrcType, TmpSrcID, UserSelect_DevType, UserSelect_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(Marshal.SizeOf(TmpRec) + 1));
                TxPacket.SetBody(TmpRec);
                ADD_TxUserData(TxPacket.GetTotalBytes());
            }
        }

        public void ADD_TxUserData(byte TmpSrcType, byte TmpSrcID, byte TmpCMD1, byte TmpCMD2, UInt16 TmpTxLen, Object TmpRec)
        {
            if (CommSt != 0)
            {

                TXSEQ++;
                if (IsRealUse)
                {
                    TxPacket.SetHeader(TmpSrcType, TmpSrcID, Real_DevType, Real_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(TmpTxLen + 1));
                } else
                {
                    TxPacket.SetHeader(TmpSrcType, TmpSrcID, UserSelect_DevType, UserSelect_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(TmpTxLen + 1));
                }
                TxPacket.SetBody(TmpTxLen, TmpRec);
                ADD_TxUserData(TxPacket.GetTotalBytes());
            }
        }

        public void ADD_TxUserDataOnlyOne(byte TmpSrcType, byte TmpSrcID, byte TmpCMD1, byte TmpCMD2, byte TmpData)
        {
            if (CommSt != 0)
            {
                UserTxDataList.Clear();
                TXSEQ++;
                if (IsRealUse)
                {
                    TxPacket.SetHeader(TmpSrcType, TmpSrcID, Real_DevType, Real_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(2));
                } else
                {
                    TxPacket.SetHeader(TmpSrcType, TmpSrcID, UserSelect_DevType, UserSelect_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(2));
                }
                TxPacket.SetBody(TmpData);
                ADD_TxUserData(TxPacket.GetTotalBytes());
            }
        }

        public void ADD_TxUserZeroData(byte TmpSrcType, byte TmpSrcID, byte TmpCMD1, byte TmpCMD2, ushort DataCount)
        {
            if (CommSt != 0)
            {
                TXSEQ++;
                if (IsRealUse)
                {
                    TxPacket.SetHeader(TmpSrcType, TmpSrcID, Real_DevType, Real_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(DataCount + 1));
                } else
                {
                    TxPacket.SetHeader(TmpSrcType, TmpSrcID, UserSelect_DevType, UserSelect_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(DataCount + 1));
                }
                TxPacket.DoCalc_CRC();
                ADD_TxUserData(TxPacket.GetTotalBytes());
            }
        }

        public void StopComm()
        {
            if (fSerialPort.IsOpen)
            {
                fSerialPort.Close();
            }

            if (Sck != null)
            {
                Sck.Shutdown(SocketShutdown.Both);
                Sck.Close();
                Sck = null;
            }
            PollingRec.ReceiveTime = DateTime.MinValue;
            UserTxDataList.Clear();
        }

        public void SetCommMode(byte TmpcommMode, string PortName, string TmpRemoteIP, ushort TmpLocalPort)
        {
            IsTxErr = false;
            switch (CommMode)
            {
                case ConstClass.COMM_SERIAL:
                    if (fSerialPort.IsOpen)
                    {
                        fSerialPort.Close();
                    }
                    break;
                case ConstClass.COMM_UDP:
                    if (Sck != null)
                    {
                        Sck.Shutdown(SocketShutdown.Both);
                        Sck.Close();
                        Sck = null;
                    }
                    break;
            }

            PollingRec.ReceiveTime = DateTime.MinValue;
            UserTxDataList.Clear();

            CommMode = TmpcommMode;
            switch (CommMode)
            {
                case ConstClass.COMM_SERIAL:
                    SetPortNumAndOpen(PortName);
                    break;
                case ConstClass.COMM_UDP:

                    UDPReadFlag = 0;
                    ((IPEndPoint)RemotePoint).Address = IPAddress.Parse(TmpRemoteIP);
                    ((IPEndPoint)RemotePoint).Port = (int)_RemtePort;

                    Sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    Sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    break;
            }

        }
        public void SetPortNumAndOpen(string PortName)
        {

            if (fSerialPort.IsOpen)
            {
                fSerialPort.Close();
            }

            fSerialPort.PortName = PortName;

            try
            {
                fSerialPort.Open();

            }
            catch (Exception exp)
            {
                //MessageBox.Show(exp.ToString());
            }
        }

        void EventPacketTimer(object sender, EventArgs e)
        {
            TStreamBufClass TmpStreamBufClass = null;
            ushort TmpDataCount = 0;
            ushort Loop;
            byte readvalue = 0;
            TimeSpan ts;
            TimeSpan PollingDelayts;

            #region 수신데이터 처리            
            switch (CommMode)
            {
                case ConstClass.COMM_SERIAL:
                    TmpStreamBufClass = this.Get_StreamBufClass(ConstClass.COMM_UDP);

                    if (TmpStreamBufClass != null)
                    {
                        if (TmpStreamBufClass.ReadDataCount() > 0)
                        {
                            TmpStreamBufClass.ResetOffset();
                        }
                    }

                    TmpStreamBufClass = this.Get_StreamBufClass(ConstClass.COMM_SERIAL);

                    if (TmpStreamBufClass != null)
                    {
                        TmpDataCount = TmpStreamBufClass.ReadDataCount();
                    }
                    break;
                case ConstClass.COMM_UDP:
                    TmpStreamBufClass = this.Get_StreamBufClass(ConstClass.COMM_SERIAL);

                    if (TmpStreamBufClass != null)
                    {
                        if (TmpStreamBufClass.ReadDataCount() > 0)
                        {
                            TmpStreamBufClass.ResetOffset();
                        }
                    }

                    TmpStreamBufClass = this.Get_StreamBufClass(ConstClass.COMM_UDP);

                    if (TmpStreamBufClass != null)
                    {
                        if (TmpStreamBufClass.ReadDataCount() > 0)
                        {
                            TmpDataCount = TmpStreamBufClass.ReadDataCount();
                        }
                    }
                    break;
            }

            if ((TmpDataCount > 0) && (TmpStreamBufClass != null))
            {
                for (Loop = 0; Loop < TmpDataCount; Loop++)
                {
                    TmpStreamBufClass.Readbyte(ref readvalue);
                    switch (ParsingRec.PacketMode)
                    {
                        case ConstClass.PasingStartCode:
                            Do_StartCodeCheck(readvalue);
                            break;
                        case ConstClass.PasingHeader:
                            Do_HeaderCheck(readvalue);
                            break;
                        case ConstClass.PasingBody:
                            Do_BodyCheck(readvalue);
                            break;
                        case ConstClass.PasingEndCode:
                            Do_EndCodeCheck(readvalue);
                            break;
                    }
                }

            }
            #endregion

            #region 송신 데이터 처리
            if (DateTime.Compare(PollingRec.TxCheckTime, DateTime.Now) > 0)
            {
                PollingRec.TxCheckTime = DateTime.Now;
            }
            ts = DateTime.Now - PollingRec.TxCheckTime;

            if (ts.TotalMilliseconds >= CommInterval)
            {
                PollingRec.TxCheckTime = DateTime.Now;

                if (DateTime.Compare(PollingRec.TxRepeatCtrlCheckTime, DateTime.Now) > 0)
                {
                    PollingRec.TxRepeatCtrlCheckTime = DateTime.Now;
                }
                ts = DateTime.Now - PollingRec.TxRepeatCtrlCheckTime;
                if (ts.TotalMilliseconds >= 200)
                {
                    PollingRec.TxRepeatCtrlCheckTime = DateTime.Now;
                    if (OnCheckJogCtrl != null)
                    {
                        OnCheckJogCtrl();
                    }
                }


                if (UserTxDataList.Count > 0)
                {
                    switch (CommMode)
                    {
                        case ConstClass.COMM_SERIAL:
                            break;
                        case ConstClass.COMM_UDP:
                            PollingDelayRec.ISDelay = false;

                            break;
                    }


                    if (DateTime.Compare(PollingRec.TxLastTime, DateTime.Now) > 0)
                    {
                        PollingRec.TxLastTime = DateTime.Now;
                    }
                    ts = DateTime.Now - PollingRec.TxLastTime;
                    if (ts.TotalMilliseconds >= (CommInterval * 2))
                    {
                        PollingRec.IsPolingDelayStop = false;
                        if (PollingDelayRec.ISDelay)
                        {
                            PollingDelayts = DateTime.Now - PollingDelayRec.TxDateTime;
                            if (PollingDelayts.TotalMilliseconds < 3000)
                            {
                                PollingRec.IsPolingDelayStop = true;
                            }
                        }

                        if (!PollingRec.IsPolingDelayStop)
                        {
                            PollingDelayRec.ISDelay = false;
                        }
                        
                        if ((PollingRec.IsPolingEnable) && (!PollingRec.IsPolingStop) && (!PollingRec.IsPolingDelayStop))
                        {

                            if (PollingRec.PollingFlag % 2 == 0)
                            {
                                switch (CommMode)
                                {
                                    case ConstClass.COMM_SERIAL:
                                        if (Serial_Send(UserTxDataList[0]))
                                        {
                                            PollingRec.TxLastTime = DateTime.Now;
                                            UserTxDataList.RemoveAt(0);
                                        } else
                                        {
                                            UserTxDataList.RemoveAt(0);
                                        }
                                        break;
                                    case ConstClass.COMM_UDP:
                                        if (UDP_Send(UserTxDataList[0]))
                                        {
                                            PollingRec.TxLastTime = DateTime.Now;
                                            UserTxDataList.RemoveAt(0);
                                        } else
                                        {
                                            UserTxDataList.RemoveAt(0);
                                        }
                                        
                                        break;
                                }
                            }
                            else
                            {
                                if (TxStatusPolling(true))
                                {
                                    PollingRec.TxLastTime = DateTime.Now;
                                }
                            }
                        } else
                        {
                            switch (CommMode)
                            {
                                case ConstClass.COMM_SERIAL:
                                    if (Serial_Send(UserTxDataList[0]))
                                    {
                                        PollingRec.TxLastTime = DateTime.Now;
                                        UserTxDataList.RemoveAt(0);
                                    } else
                                    {
                                        UserTxDataList.RemoveAt(0);
                                    }
                                    break;
                                case ConstClass.COMM_UDP:
                                    if (UDP_Send(UserTxDataList[0]))
                                    {
                                        PollingRec.TxLastTime = DateTime.Now;
                                        UserTxDataList.RemoveAt(0);
                                    } else
                                    {
                                        UserTxDataList.RemoveAt(0);
                                    }
                                    break;
                            }
                        }

                        ++PollingRec.PollingFlag;
                        if (PollingRec.PollingFlag > 11) PollingRec.PollingFlag = 0;
                    }
                }
                else
                {
                    if (DateTime.Compare(PollingRec.TxLastTime, DateTime.Now) > 0)
                    {
                        PollingRec.TxLastTime = DateTime.Now;
                    }
                    ts = DateTime.Now - PollingRec.TxLastTime;
                    if (ts.TotalMilliseconds >= 200)
                    {
                        PollingRec.IsPolingDelayStop = false;
                        if (PollingDelayRec.ISDelay)
                        {
                            PollingDelayts = DateTime.Now - PollingDelayRec.TxDateTime;
                            if (PollingDelayts.TotalMilliseconds < 3000)
                            {
                                PollingRec.IsPolingDelayStop = true;
                            }
                        }

                        if (!PollingRec.IsPolingDelayStop)
                        {
                            PollingDelayRec.ISDelay = false;
                        }

                        //IsPolingEnable : false 설정 시 폴링이 나가지 않도록도 할 수 있는 Flag (F/W 다운로드 중과 같은 필요에 의해.)
                        if ((PollingRec.IsPolingEnable) && (!PollingRec.IsPolingStop) && (!PollingRec.IsPolingDelayStop))
                        {
                            if (ISTestPolling)
                            {

                                if (PollingRec.PollingFlag % 2 == 0)
                                {
                                    if (TxStatusPolling(true))
                                    {
                                        PollingRec.TxLastTime = DateTime.Now;
                                    }
                                }
                                else
                                {
                                    if (TxTestStatusReq())
                                    {
                                        PollingRec.TxLastTime = DateTime.Now;
                                    }
                                }
                                ++PollingRec.PollingFlag;
                                if (PollingRec.PollingFlag > 11) PollingRec.PollingFlag = 0;
                            } else if (ISInvertorPolling)
                            {
                                
                                if (PollingRec.PollingFlag % 2 == 0)
                                {
                                    if (TxStatusPolling(true))
                                    {
                                        PollingRec.TxLastTime = DateTime.Now;
                                    }
                                }
                                else
                                {
                                    if (TxInvertorStatusReq())
                                    {
                                        PollingRec.TxLastTime = DateTime.Now;
                                    }
                                }
                                ++PollingRec.PollingFlag;
                                if (PollingRec.PollingFlag > 11) PollingRec.PollingFlag = 0;
                            }
                            else if (ISWcsMapPolling)
                            {

                                if (PollingRec.PollingFlag % 2 == 0)
                                {
                                    if (TxStatusPolling(true))
                                    {
                                        PollingRec.TxLastTime = DateTime.Now;
                                    }
                                }
                                else
                                {
                                    if (TxTWCSMapReq())
                                    {
                                        PollingRec.TxLastTime = DateTime.Now;
                                    }
                                }
                                ++PollingRec.PollingFlag;
                                if (PollingRec.PollingFlag > 11) PollingRec.PollingFlag = 0;
                            }
                            else
                            {
                                if (TxStatusPolling(false))
                                {
                                    PollingRec.TxLastTime = DateTime.Now;
                                }
                                ++PollingRec.PollingFlag;
                                if (PollingRec.PollingFlag > 11) PollingRec.PollingFlag = 0;
                            }
                        }
                    }
                }
            }
            #endregion
        }

        bool TxStatusPolling(bool TmpForceDevStPoll)
        {
            bool result = false;
            
            TXSEQ++;
            if (((PollingRec.PollingStFlag % 3) == 1) && (!TmpForceDevStPoll))
            {

                if (IsRealUse)
                {
                    TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, Real_DevType, Real_DevID, TXSEQ, ConstClass.CMD1_01, ConstClass.CMD2_10, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_BasicStReq)) + 1));
                } else
                {
                    TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, UserSelect_DevType, UserSelect_DevID, TXSEQ, ConstClass.CMD1_01, ConstClass.CMD2_10, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_BasicStReq)) + 1));
                }
                TxPacket.DoCalc_CRC();

            }
            else
            {
                switch (UserSelect_DevType)
                {
                    case ConstClass.TYPE_SRM:
                        if (IsRealUse)
                        {
                            TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, Real_DevType, Real_DevID, TXSEQ, ConstClass.CMD1_00, ConstClass.CMD2_30, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_StatusReq)) + 1));
                        } else
                        {
                            TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, UserSelect_DevType, UserSelect_DevID, TXSEQ, ConstClass.CMD1_00, ConstClass.CMD2_30, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_StatusReq)) + 1));
                        }
                        DevRec.srm_REC_DevStReq.IsIgnore = 0x00;
                        DevRec.srm_REC_DevStReq.SystemUTCTime = Global_Class.UTIL_GetUnixTimeStampFromLocalTime(DateTime.Now);
                        TxPacket.SetBody(DevRec.srm_REC_DevStReq);
                        break;
                    case ConstClass.TYPE_RTV:
                        if (IsRealUse)
                        {
                            TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, Real_DevType, Real_DevID, TXSEQ, ConstClass.CMD1_00, ConstClass.CMD2_30, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_REC_StatusReq)) + 1));
                        } else
                        {
                            TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, UserSelect_DevType, UserSelect_DevID, TXSEQ, ConstClass.CMD1_00, ConstClass.CMD2_30, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_REC_StatusReq)) + 1));
                        }
                        DevRec.rtv_REC_DevStReq.IsIgnore = 0x00;
                        DevRec.rtv_REC_DevStReq.SystemUTCTime = Global_Class.UTIL_GetUnixTimeStampFromLocalTime(DateTime.Now);
                        TxPacket.SetBody(DevRec.rtv_REC_DevStReq);
                        break;
                    case ConstClass.TYPE_EMS:
                        if (IsRealUse)
                        {
                            TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, Real_DevType, Real_DevID, TXSEQ, ConstClass.CMD1_00, ConstClass.CMD2_30, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_REC_StatusReq)) + 1));
                        }
                        else
                        {
                            TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, UserSelect_DevType, UserSelect_DevID, TXSEQ, ConstClass.CMD1_00, ConstClass.CMD2_30, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_REC_StatusReq)) + 1));
                        }
                        DevRec.ems_REC_DevStReq.IsIgnore = 0x00;
                        DevRec.ems_REC_DevStReq.SystemUTCTime = Global_Class.UTIL_GetUnixTimeStampFromLocalTime(DateTime.Now);
                        TxPacket.SetBody(DevRec.ems_REC_DevStReq);
                        break;
                    default:
                        if (IsRealUse)
                        {
                            TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, Real_DevType, Real_DevID, TXSEQ, ConstClass.CMD1_00, ConstClass.CMD2_30, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_StatusReq)) + 1));
                        }
                        else
                        {
                            TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, UserSelect_DevType, UserSelect_DevID, TXSEQ, ConstClass.CMD1_00, ConstClass.CMD2_30, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_StatusReq)) + 1));
                        }
                        DevRec.srm_REC_DevStReq.IsIgnore = 0x00;
                        DevRec.srm_REC_DevStReq.SystemUTCTime = Global_Class.UTIL_GetUnixTimeStampFromLocalTime(DateTime.Now);
                        TxPacket.SetBody(DevRec.srm_REC_DevStReq);
                        break;
                }
            }

            PollingRec.PollingStFlag++;
            if (PollingRec.PollingStFlag > 11) PollingRec.PollingStFlag = 0;


            switch (CommMode)
            {
                case ConstClass.COMM_SERIAL:
                    result = Serial_Send(TxPacket.GetTotalBytes());
                    break;
                case ConstClass.COMM_UDP:
                    result = UDP_Send(TxPacket.GetTotalBytes());
                    break;
            }

            return result;
        }


        bool TxTestStatusReq()
        {
            bool result = false;
            TXSEQ++;
            if (IsRealUse)
            {
                TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, Real_DevType, Real_DevID, TXSEQ, ConstClass.CMD1_01, ConstClass.CMD2_12, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_TestStReq)) + 1));
            } else
            {
                TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, UserSelect_DevType, UserSelect_DevID, TXSEQ, ConstClass.CMD1_01, ConstClass.CMD2_12, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_TestStReq)) + 1));
            }
            TxPacket.DoCalc_CRC();

            switch (CommMode)
            {
                case ConstClass.COMM_SERIAL:
                    result = Serial_Send(TxPacket.GetTotalBytes());
                    break;
                case ConstClass.COMM_UDP:
                    result = UDP_Send(TxPacket.GetTotalBytes());
                    break;
            }

            return result;
        }

        bool TxTWCSMapReq()
        {
            bool result = false;
            TXSEQ++;
            if (IsRealUse)
            {
                TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, Real_DevType, Real_DevID, TXSEQ, ConstClass.CMD1_01, ConstClass.CMD2_3D, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_WCSMapReq)) + 1));
            }
            else
            {
                TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, UserSelect_DevType, UserSelect_DevID, TXSEQ, ConstClass.CMD1_01, ConstClass.CMD2_3D, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_WCSMapReq)) + 1));
            }
            TxPacket.DoCalc_CRC();

            switch (CommMode)
            {
                case ConstClass.COMM_SERIAL:
                    result = Serial_Send(TxPacket.GetTotalBytes());
                    break;
                case ConstClass.COMM_UDP:
                    result = UDP_Send(TxPacket.GetTotalBytes());
                    break;
            }

            return result;
        }

        bool TxInvertorStatusReq()
        {
            bool result = false;
            TXSEQ++;

            if (IsRealUse)
            {
                switch (UserSelect_DevType)
                {
                    case ConstClass.TYPE_SRM:
                        TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, Real_DevType, Real_DevID, TXSEQ, ConstClass.CMD1_00, ConstClass.CMD2_32, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_InvertorReq)) + 1));
                        break;
                    case ConstClass.TYPE_RTV:
                    case ConstClass.TYPE_EMS:
                        TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, Real_DevType, Real_DevID, TXSEQ, ConstClass.CMD1_00, ConstClass.CMD2_32, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TEMSRTV_REC_InvertorReq)) + 1));
                        break;
                }

            } else
            {
                switch (UserSelect_DevType)
                {
                    case ConstClass.TYPE_SRM:
                        TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, UserSelect_DevType, UserSelect_DevID, TXSEQ, ConstClass.CMD1_00, ConstClass.CMD2_32, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_InvertorReq)) + 1));
                        break;
                    case ConstClass.TYPE_RTV:
                    case ConstClass.TYPE_EMS:
                        TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, UserSelect_DevType, UserSelect_DevID, TXSEQ, ConstClass.CMD1_00, ConstClass.CMD2_32, (ushort)(Marshal.SizeOf(typeof(VEXI_DEFS.TEMSRTV_REC_InvertorReq)) + 1));
                        break;
                }
            }
            TxPacket.DoCalc_CRC();

            switch (CommMode)
            {
                case ConstClass.COMM_SERIAL:
                    result = Serial_Send(TxPacket.GetTotalBytes());
                    break;
                case ConstClass.COMM_UDP:
                    result = UDP_Send(TxPacket.GetTotalBytes());
                    break;
            }

            return result;
        }
        void Do_StartCodeCheck(byte TmpReadValue)
        {
            if (TmpReadValue == ConstClass.CODE_START)
            {
                ParsingRec.ReadByteCnt++;
                if (ParsingRec.ReadByteCnt >= 4)
                {
                    ParsingRec.PacketMode = ConstClass.PasingHeader;
                    ParsingRec.ReadByteCnt = 0;
                    Array.Resize(ref ParsingRec.ParsingBuffer, ConstClass.U_PACKET_HEADER_SIZE);
                    //ParsingBuffer = new byte[ConstClass.U_PACKET_HEADER_SIZE];
                }
            }
            else
            {
                ParsingRec.ReadByteCnt = 0;
            }
        }

        void Do_HeaderCheck(byte TmpReadValue)
        {
            ushort TmpLen = 0;
            ParsingRec.ReadByteCnt++;
            if (ParsingRec.ReadByteCnt >= ConstClass.U_PACKET_HEADER_SIZE)
            {
                ParsingRec.ParsingBuffer[ParsingRec.ReadByteCnt - 1] = TmpReadValue;
                TmpLen = BitConverter.ToUInt16(ParsingRec.ParsingBuffer, 8);
                if ((TmpLen == 0) || (TmpLen > ConstClass.U_PACKET_DATAMAX_SIZE))
                {
                    ParsingRec.PacketMode = ConstClass.PasingStartCode;
                    ParsingRec.ReadByteCnt = 0;
                }
                else if (TmpLen == 1)
                {
                    RxPacket.SetHeader(ParsingRec.ParsingBuffer);
                    ParsingRec.PacketMode = ConstClass.PasingEndCode;
                    ParsingRec.ReadByteCnt = 0;
                    ParsingRec.DataLen = TmpLen;
                    Array.Resize(ref ParsingRec.ParsingBuffer, ConstClass.U_PACKET_END_SIZE);
                    //ParsingBuffer = new byte[ConstClass.U_PACKET_END_SIZE];
                }
                else
                {

                    RxPacket.SetHeader(ParsingRec.ParsingBuffer);
                    ParsingRec.PacketMode = ConstClass.PasingBody;
                    ParsingRec.ReadByteCnt = 0;
                    ParsingRec.DataLen = TmpLen;
                    Array.Resize(ref ParsingRec.ParsingBuffer, ParsingRec.DataLen - 1);
                    //ParsingBuffer = new byte[DataLen];
                }
            }
            else
            {
                ParsingRec.ParsingBuffer[ParsingRec.ReadByteCnt - 1] = TmpReadValue;
            }
        }
        void Do_BodyCheck(byte TmpReadValue)
        {
            ParsingRec.ReadByteCnt++;
            if (ParsingRec.ReadByteCnt >= (ParsingRec.DataLen - 1))
            {
                ParsingRec.ParsingBuffer[ParsingRec.ReadByteCnt - 1] = TmpReadValue;
                RxPacket.SetBody(ParsingRec.ParsingBuffer);
                ParsingRec.PacketMode = ConstClass.PasingEndCode;
                ParsingRec.ReadByteCnt = 0;
                Array.Resize(ref ParsingRec.ParsingBuffer, ConstClass.U_PACKET_END_SIZE);
                //ParsingBuffer = new byte[ConstClass.U_PACKET_END_SIZE];
            }
            else
            {
                ParsingRec.ParsingBuffer[ParsingRec.ReadByteCnt - 1] = TmpReadValue;
            }
        }

        void Do_EndCodeCheck(byte TmpReadValue)
        {
            try
            {
                ParsingRec.ReadByteCnt++;
                if (ParsingRec.ReadByteCnt >= ConstClass.U_PACKET_END_SIZE)
                {
                    ParsingRec.ParsingBuffer[ParsingRec.ReadByteCnt - 1] = TmpReadValue;
                    RxPacket.CrcCompare(ParsingRec.ParsingBuffer);

                    PollingRec.ReceiveTime = DateTime.Now;

                    ParsingRec.PacketMode = ConstClass.PasingStartCode;
                    ParsingRec.ReadByteCnt = 0;

                    if (OnPacketReceived != null)
                    {
                        OnPacketReceived(CommMode, RxPacket.IsCorrectPacket, RxPacket, ParsingRec.ParsingBuffer[0], ParsingRec.ParsingBuffer[1], ParsingRec.ParsingBuffer[2]);
                    }
                    //
                }
                else
                {
                    ParsingRec.ParsingBuffer[ParsingRec.ReadByteCnt - 1] = TmpReadValue;
                }
            } catch
            {
                ParsingRec.PacketMode = ConstClass.PasingStartCode;
                ParsingRec.ReadByteCnt = 0;
            }

        }


        void EventDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            TStreamBufClass TmpStreamBufClass;
            int ReadLen = fSerialPort.BytesToRead;

            if (ReadLen > 0)
            {
                fSerialPort.Read(Readbuffer, 0, ReadLen);

                TmpStreamBufClass = this.Get_StreamBufClass(ConstClass.COMM_SERIAL);

                if (TmpStreamBufClass != null)
                {
                    TmpStreamBufClass.Writebytes(Readbuffer, (ushort)ReadLen);

                    //if (OnCommDataReceived != null)
                    //{
                    //    OnCommDataReceived(ConstClass.COMM_SERIAL,"",0, (ushort)ReadLen);
                    //}
                }
            }
        }


        public bool Set_dev_REC_BasicSt(byte devtype, byte devid, byte[] datas)
        {
            //장치 기본 상태는 장치타입과 무관
            int test = Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_BasicStRes));
            if (test < datas.Length) return false;

            DevRec.Flag_In_8110 = true;
            DevRec.dev_REC_BasicSt = (VEXI_DEFS.TDEV_REC_BasicStRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TDEV_REC_BasicStRes));
            return true;
        }

        public bool Set_dev_REC_TestSt(byte devtype, byte devid, byte[] datas)
        {
            //Test Data는 장치타입과 무관
            int test = Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_TestStRes));
            //if (test != datas.Length) return false;
            if (test < datas.Length) return false;

            DevRec.Flag_In_8112 = true;
            //DevRec.dev_REC_TestSt = (VEXI_DEFS.TDEV_REC_TestStRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TDEV_REC_TestStRes));
            DevRec.dev_REC_TestSt = (VEXI_DEFS.TDEV_REC_TestStRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TDEV_REC_TestStRes));
            return true;
        }

        public bool Set_dev_REC_WCSData(byte devtype, byte devid, byte[] datas)
        {
            //WCS Data는 장치타입과 무관
            int test = Marshal.SizeOf(typeof(VEXI_DEFS.TMOVEX_WCS_DataRec));
            if (test != datas.Length) return false;
            return true;
        }

        public bool Check_DEV_REC_Graph(byte devtype, byte devid, byte[] datas)
        {
            //Graph Data는 장치타입과 무관
            int test = Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_Graph));
            if (test != datas.Length) return false;
            return true;
        }

        public bool Check_Dev_REC_DevIOConfig(byte devtype, byte devid, byte[] datas)
        {
            int TmpLen;

            TmpLen = Marshal.SizeOf(typeof(VEXI_DEFS.DevUnion_IOConfig));
            //if (TmpLen != datas.Length) return false;
            if (TmpLen < datas.Length) return false;
            return true;

        }

        public bool Check_DEV_DevConfig(byte devtype, byte devid, byte[] datas)
        {
            int TmpLen;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    TmpLen = Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_DevConfigRes));
                    if (TmpLen != datas.Length) return false;

                    return true;
                case ConstClass.TYPE_RTV:
                    TmpLen = Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_DevConfigRes));
                    if (TmpLen != datas.Length) return false;

                    return true;
                case ConstClass.TYPE_EMS:
                    TmpLen = Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_DevConfigRes));
                    if (TmpLen != datas.Length) return false;

                    return true;
                default:
                    return false;
            }

        }

        public void SaveCommDataLogging(bool TmpIsForce, byte DevType)
        {
            int TmpCount = 0;

            if (DevStSaveBuffer.Count == 0) return;

            if ((TmpIsForce) || (DevStSaveBuffer.Count >= 50))
            {
                if (TmpIsForce)
                {
                    TmpCount = Math.Min(100, DevStSaveBuffer.Count);
                } else
                { 
                    TmpCount = Math.Min(50, DevStSaveBuffer.Count);
                }

                if (TmpCount == 0) return;

                for (int i = 0; i < TmpCount; i++)
                {
                    WriteBuffer[i] = DevStSaveBuffer[0].ToString();
                    DevStSaveBuffer.RemoveAt(0);
                }

                if (TmpIsForce)
                {
                    DevStSaveBuffer.Clear();

                }

                try
                {
                    switch (DevType)
                    {
                        case ConstClass.TYPE_SRM: Global_Class.Logging_CommDataInsert(CommDataSaveDir, "SRM", WriteBuffer, TmpCount); break;
                        case ConstClass.TYPE_RTV: Global_Class.Logging_CommDataInsert(CommDataSaveDir, "RTV", WriteBuffer, TmpCount); break;
                        case ConstClass.TYPE_EMS: Global_Class.Logging_CommDataInsert(CommDataSaveDir, "EMS", WriteBuffer, TmpCount); break;
                        default: Global_Class.Logging_CommDataInsert(CommDataSaveDir, "SRM", WriteBuffer, TmpCount); break;
                    }
                }
                catch
                {

                }

            }
        }

        public void SaveSpeedDataLogging(bool TmpIsForce, byte DevType)
        {
            int TmpCount = 0;

            if (DevStSaveBuffer.Count == 0) return;

            if ((TmpIsForce) || (DevStSaveBuffer.Count >= 50))
            {
                if (TmpIsForce)
                {
                    TmpCount = Math.Min(100, DevStSaveBuffer.Count);
                }
                else
                {
                    TmpCount = Math.Min(50, DevStSaveBuffer.Count);
                }

                if (TmpCount == 0) return;

                for (int i = 0; i < TmpCount; i++)
                {
                    WriteBuffer[i] = DevStSaveBuffer[0].ToString();
                    DevStSaveBuffer.RemoveAt(0);
                }

                if (TmpIsForce)
                {
                    DevStSaveBuffer.Clear();

                }

                try
                {
                    switch (DevType)
                    {
                        case ConstClass.TYPE_SRM: Global_Class.Logging_SpeedDataInsert(SpeedDataSaveDir, "SRM", WriteBuffer, TmpCount); break;
                        case ConstClass.TYPE_RTV: Global_Class.Logging_SpeedDataInsert(SpeedDataSaveDir, "RTV", WriteBuffer, TmpCount); break;
                        case ConstClass.TYPE_EMS: Global_Class.Logging_SpeedDataInsert(SpeedDataSaveDir, "EMS", WriteBuffer, TmpCount); break;
                        default: Global_Class.Logging_SpeedDataInsert(SpeedDataSaveDir, "SRM", WriteBuffer, TmpCount); break;
                    }
                }
                catch
                {

                }

            }
        }

        //상태 데이터 수신시 로깅모드이고 DIO 값 변경시 저장 옵션이 체크 되어 있다면 DIO 값 변경이 있는지 확인하여 저장 버퍼에 ADD
        private unsafe void Process_DEVSt_LoggingDataCheck(byte DevType, byte[] datas)
        {
            bool isSave = false;


            if (LoggingMode == 2)
            {

                DateTime PCtime = DateTime.Now;
                switch (DevType)
                {
                    
                    case ConstClass.TYPE_SRM:
                        VEXI_DEFS.TSRM_StatusRes TmpSRMSt;
                        TmpSRMSt = (VEXI_DEFS.TSRM_StatusRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TSRM_StatusRes));
                        PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(TmpSRMSt.SystemUTCTime);

                        for (int i = 0; i < 16; i++)
                        {
                            if (TmpSRMSt.IO_Digital_IN[i] != DevRec.srm_REC_SRMSt.IO_Digital_IN[i])
                            {
                                isSave = true;
                                break;
                            }
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            if (TmpSRMSt.IO_Digital_OUT[i] != DevRec.srm_REC_SRMSt.IO_Digital_OUT[i])
                            {
                                isSave = true;
                                break;
                            }
                        }
                        break;
                    case ConstClass.TYPE_RTV:
                        VEXI_DEFS.TRTV_StatusRes TmpRTVSt;
                        TmpRTVSt = (VEXI_DEFS.TRTV_StatusRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TRTV_StatusRes));
                        PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(TmpRTVSt.SystemUTCTime);

                        for (int i = 0; i < 9; i++)
                        {
                            
                            if (TmpRTVSt.IO_Digital_IN[i] != DevRec.rtv_REC_RTVSt.IO_Digital_IN[i])
                            {
                                isSave = true;
                                break;
                            }
                        }
                        for (int i = 0; i < 7; i++)
                        {
                            if (TmpRTVSt.IO_Digital_OUT[i] != DevRec.rtv_REC_RTVSt.IO_Digital_OUT[i])
                            {
                                isSave = true;
                                break;
                            }
                        }
                        break;

                    case ConstClass.TYPE_EMS:
                        VEXI_DEFS.TEMS_StatusRes TmpEMSSt;
                        TmpEMSSt = (VEXI_DEFS.TEMS_StatusRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TEMS_StatusRes));
                        PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(TmpEMSSt.SystemUTCTime);

                        for (int i = 0; i < 10; i++)
                        {

                            if (TmpEMSSt.IO_Digital_IN[i] != DevRec.ems_REC_EMSSt.IO_Digital_IN[i])
                            {
                                isSave = true;
                                break;
                            }
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            if (TmpEMSSt.IO_Digital_OUT[i] != DevRec.ems_REC_EMSSt.IO_Digital_OUT[i])
                            {
                                isSave = true;
                                break;
                            }
                        }
                        break;
                }


                if (isSave)
                {
                    DataSaveTime = DateTime.Now;

                    StringBuilder DataText = new StringBuilder();

                    DataText.Append("[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] ");
                    DataText.Append("[" + PCtime.ToString("HH:mm:ss.fff") + "] ");

                    for (int i = 0; i < datas.Length; i++)
                    {
                        DataText.Append(string.Format("{0:X2} ", datas[i]));
                    }

                    DevStSaveBuffer.Add(DataText.ToString());
                }
            }
        }

        public unsafe void Process_DEVSt_LoggingIntervalCheck(byte DevType)
        {
            if ((DevType == ConstClass.TYPE_SRM) || (DevType == ConstClass.TYPE_RTV) || (DevType == ConstClass.TYPE_EMS))
            {

                bool isSave = false;
                TimeSpan ts;
                if (LoggingMode > 0)
                {
                    if (LoggingMode == 3)
                    {
                        if (DateTime.Compare(DataSaveTime, DateTime.Now) > 0)
                        {
                            DataSaveTime = DateTime.Now;
                        }
                        ts = DateTime.Now - DataSaveTime;
                        if (ts.TotalMilliseconds >= (Logginginterval - 50))
                        {
                            if (saveSec == -1)
                            {
                                saveSec = (sbyte)DateTime.Now.Second;
                                isSave = true;
                            } else
                            {
                                if (saveSec != (sbyte)DateTime.Now.Second)
                                {
                                    saveSec = (sbyte)DateTime.Now.Second;
                                    isSave = true;
                                }
                            }
                        }

                        if (isSave)
                        {
                            DataSaveTime = DateTime.Now;

                            ts = DateTime.Now - DevRec.Time_In_DevStatus;

                            StringBuilder DataText = new System.Text.StringBuilder();

                            DataText.Append(DataSaveTime.ToString("yyyy-MM-dd") + "," + DataSaveTime.ToString("HH:mm:ss") + ",");

                            switch (DevType)
                            {

                                case ConstClass.TYPE_SRM:
                                    if (ts.TotalMilliseconds > 1000)
                                    {
                                        DataText.Append("통신이상,통신이상");
                                        DevStSaveBuffer.Add(DataText.ToString());
                                    }
                                    else
                                    {
                                        DataText.Append(String.Format("{0}", DevRec.srm_REC_SRMSt.Drive_DisPosition.Now_Speed) + "," + String.Format("{0}", DevRec.srm_REC_SRMSt.Updown_DisPosition.Now_Speed));
                                        DevStSaveBuffer.Add(DataText.ToString());
                                    }

                                    break;
                                case ConstClass.TYPE_RTV:
                                    if (ts.TotalMilliseconds > 1000)
                                    {
                                        DataText.Append("통신이상,통신이상");
                                        DevStSaveBuffer.Add(DataText.ToString());
                                    }
                                    else
                                    {
                                        DataText.Append(String.Format("{0}", DevRec.rtv_REC_RTVSt.Drive_DisPosition.Now_Speed));
                                        DevStSaveBuffer.Add(DataText.ToString());
                                    }

                                    
                                    break;
                                case ConstClass.TYPE_EMS:
                                    if (ts.TotalMilliseconds > 1000)
                                    {
                                        DataText.Append("통신이상,통신이상");
                                        DevStSaveBuffer.Add(DataText.ToString());
                                    }
                                    else
                                    {
                                        DataText.Append(String.Format("{0}", DevRec.ems_REC_EMSSt.Drive_DisPosition.Now_Speed));
                                        DevStSaveBuffer.Add(DataText.ToString());
                                    }


                                    break;
                            }
                        }

                        SaveSpeedDataLogging(false, DevType);
                    }
                    else
                    {
                        if (DateTime.Compare(DataSaveTime, DateTime.Now) > 0)
                        {
                            DataSaveTime = DateTime.Now;
                        }
                        ts = DateTime.Now - DataSaveTime;
                        if (ts.TotalMilliseconds >= (Logginginterval - 50))
                        {
                            isSave = true;
                        }

                        if (isSave)
                        {
                            DataSaveTime = DateTime.Now;

                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(DevRec.srm_REC_SRMSt.SystemUTCTime);

                            StringBuilder DataText = new StringBuilder();

                            DataText.Append("[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] ");
                            DataText.Append("[" + PCtime.ToString("HH:mm:ss.fff") + "] ");

                            switch (DevType)
                            {

                                case ConstClass.TYPE_SRM:
                                    fixed (byte* Ptr = &DevRec.srm_REC_SRMSt.Reserved_1)
                                    {
                                        int TmpLen = Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_StatusRes));
                                        for (int i = 0; i < TmpLen; i++)
                                        {
                                            DataText.Append(string.Format("{0:X2} ", *(Ptr + i)));
                                        }
                                        DevStSaveBuffer.Add(DataText.ToString());
                                    }
                                    break;
                                case ConstClass.TYPE_RTV:
                                    fixed (byte* Ptr = &DevRec.rtv_REC_RTVSt.Reserved_1)
                                    {
                                        int TmpLen = Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_StatusRes));
                                        for (int i = 0; i < TmpLen; i++)
                                        {
                                            DataText.Append(string.Format("{0:X2} ", *(Ptr + i)));
                                        }
                                        DevStSaveBuffer.Add(DataText.ToString());
                                    }
                                    break;
                                case ConstClass.TYPE_EMS:
                                    fixed (byte* Ptr = &DevRec.ems_REC_EMSSt.Reserved_1)
                                    {
                                        int TmpLen = Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_StatusRes));
                                        for (int i = 0; i < TmpLen; i++)
                                        {
                                            DataText.Append(string.Format("{0:X2} ", *(Ptr + i)));
                                        }
                                        DevStSaveBuffer.Add(DataText.ToString());
                                    }
                                    break;
                            }
                        }

                        SaveCommDataLogging(false, DevType);
                    }
                }
            }
        }

        public bool Set_DEV_REC_DevSt(byte devtype, byte devid, byte[] datas)
        {
            // 수신된 데이터와 선언된 구조체간의 크기를 비교 (Vexi 프로토콜과 MCU 프로토콜이 동일한지 확인하는 최소한의 체크)
            // 같은지를 비교하기도 하고 선언된 구조체보다 작은지를 확인하기도 한다
            // 작은지를 확인하는 건 프로토콜 수정으로 구조체 크기가 커졌는데
            // 해당 사항이 반영 안된 MCU 와의 연동 때문이다. (이런 이유로 프로토콜 수정시 항목 추가가 필요하다면 중간에 Field를 넣지 말고 - Reserved 활용은 괜찮음 - 맨끝에 추가하여야 한다)

            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_StatusRes));
                    //if (test != datas.Length) return false;
                    if (test < datas.Length) return false;

                    DevRec.Time_In_DevStatus = DateTime.Now;
                    DevRec.Flag_In_DevStatus = true;
                    if (LoggingMode == 2)
                    {
                        Process_DEVSt_LoggingDataCheck(devtype, datas);
                    }

                    DevRec.srm_REC_SRMSt = (VEXI_DEFS.TSRM_StatusRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TSRM_StatusRes));

                    return true;
                case ConstClass.TYPE_RTV:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_StatusRes));
                    //if (test != datas.Length) return false;
                    if (test < datas.Length) return false;

                    DevRec.Time_In_DevStatus = DateTime.Now;
                    DevRec.Flag_In_DevStatus = true;
                    if (LoggingMode == 2)
                    {
                        Process_DEVSt_LoggingDataCheck(devtype, datas);
                    }

                    DevRec.rtv_REC_RTVSt = (VEXI_DEFS.TRTV_StatusRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TRTV_StatusRes));
                    return true;
                case ConstClass.TYPE_EMS:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_StatusRes));
                    //if (test != datas.Length) return false;
                    if (test < datas.Length) return false;

                    DevRec.Time_In_DevStatus = DateTime.Now;
                    DevRec.Flag_In_DevStatus = true;
                    if (LoggingMode == 2)
                    {
                        Process_DEVSt_LoggingDataCheck(devtype, datas);
                    }

                    DevRec.ems_REC_EMSSt = (VEXI_DEFS.TEMS_StatusRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TEMS_StatusRes));
                    return true;
                default:
                    return false;
            }
        }

        public bool Check_Dev_REC_OpInfo(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_OpInfoRes));
                    if (test != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_RTV:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_OpInfoRes));
                    if (test != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_EMS:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_OpInfoRes));
                    if (test != datas.Length) return false;
                    return true;
                default:
                    return false;
            }
        }



        public bool Check_DEV_CtrlRes_2Byte(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                case ConstClass.TYPE_RTV:
                case ConstClass.TYPE_EMS:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_CtrlRes_2Byte));
                    if (test != datas.Length) return false;
                    return true;
                default:
                    return false;
            }
        }

        public bool Check_DEV_CtrlRes_3Byte(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                case ConstClass.TYPE_RTV:
                case ConstClass.TYPE_EMS:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_CtrlRes_3Byte));
                    if (test != datas.Length) return false;
                    return true;
                default:
                    return false;
            }

        }

        public bool Check_DEV_REC_DEBUG(byte devtype, byte devid, byte[] datas)
        {
            //LOG Data는 장치타입과 무관

            VEXI_DEFS.TDEV_REC_LogHeader dev_REC_LogHeader;
            byte HeaderLen = (byte)Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader));
            UInt16 TotalDataLen_exp;
            dev_REC_LogHeader = (VEXI_DEFS.TDEV_REC_LogHeader)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TDEV_REC_LogHeader), 0, HeaderLen);


            //데이터의 길이는 Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader)) + dev_REC_LogHeader.LogCount * (로그데이터 1개의 사이즈)
            switch (dev_REC_LogHeader.DataType)
            {
                case 1:
                    TotalDataLen_exp = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader)) + dev_REC_LogHeader.LogCount * (Marshal.SizeOf(typeof(VEXI_DEFS.TDebugLogItem_1))));
                    break;
                case 2:
                    TotalDataLen_exp = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader)) + dev_REC_LogHeader.LogCount * (Marshal.SizeOf(typeof(VEXI_DEFS.TDebugLogItem_2))));
                    break;
                case 3:
                    TotalDataLen_exp = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader)) + dev_REC_LogHeader.LogCount * (Marshal.SizeOf(typeof(VEXI_DEFS.TDebugLogItem_1))));
                    break;
                case 4:
                    TotalDataLen_exp = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader)) + dev_REC_LogHeader.LogCount * (Marshal.SizeOf(typeof(VEXI_DEFS.TDebugLogItem_1))));
                    break;
                default:
                    TotalDataLen_exp = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader)) + dev_REC_LogHeader.LogCount * (Marshal.SizeOf(typeof(VEXI_DEFS.TDebugLogItem_1))));
                    break;

            }

            if (datas.Length != TotalDataLen_exp)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Check_DEV_REC_LOG(byte devtype, byte devid, byte[] datas)
        {
            //LOG Data는 장치타입과 무관

            VEXI_DEFS.TDEV_REC_LogHeader dev_REC_LogHeader;
            byte HeaderLen = (byte)Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader));
            UInt16 TotalDataLen_exp;
            dev_REC_LogHeader = (VEXI_DEFS.TDEV_REC_LogHeader)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TDEV_REC_LogHeader), 0, HeaderLen);


            //데이터의 길이는 Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader)) + dev_REC_LogHeader.LogCount * (로그데이터 1개의 사이즈)
            switch (dev_REC_LogHeader.DataType)
            {
                case 0:
                    TotalDataLen_exp = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader)) + dev_REC_LogHeader.LogCount * (Marshal.SizeOf(typeof(VEXI_DEFS.TLOGItemHeader)) + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_SRM_00))));
                    break;
                case 1:
                    TotalDataLen_exp = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader)) + dev_REC_LogHeader.LogCount * (Marshal.SizeOf(typeof(VEXI_DEFS.TLOGItemHeader)) + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_SRM_01))));
                    break;
                case 2:
                    TotalDataLen_exp = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader)) + dev_REC_LogHeader.LogCount * (Marshal.SizeOf(typeof(VEXI_DEFS.TLOGItemHeader)) + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_SRM_02))));
                    break;
                case 10:
                    TotalDataLen_exp = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader)) + dev_REC_LogHeader.LogCount * (Marshal.SizeOf(typeof(VEXI_DEFS.TLOGItemHeader)) + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_RTV_10))));
                    break;
                case 11:
                    TotalDataLen_exp = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader)) + dev_REC_LogHeader.LogCount * (Marshal.SizeOf(typeof(VEXI_DEFS.TLOGItemHeader)) + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_RTV_11))));
                    break;
                case 20:
                    TotalDataLen_exp = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader)) + dev_REC_LogHeader.LogCount * (Marshal.SizeOf(typeof(VEXI_DEFS.TLOGItemHeader)) + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_EMS_20))));
                    break;
                case 21:
                    TotalDataLen_exp = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader)) + dev_REC_LogHeader.LogCount * (Marshal.SizeOf(typeof(VEXI_DEFS.TLOGItemHeader)) + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_EMS_21))));
                    break;
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37: TotalDataLen_exp = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader)) + dev_REC_LogHeader.LogCount * (Marshal.SizeOf(typeof(VEXI_DEFS.TLOGItemHeader)) + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_30)))); break;
                default: TotalDataLen_exp = (UInt16)(Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader)) + dev_REC_LogHeader.LogCount * (Marshal.SizeOf(typeof(VEXI_DEFS.TLOGItemHeader)) + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_30)))); break;

            }

            if (datas.Length != TotalDataLen_exp)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Check_SRM_JobCtrlRes(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_JobCTRLRES));
                    if (test != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_RTV_JobCtrlRes(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_REC_JobCTRLRES));
                    if (test != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_EMS_JobCtrlRes(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_REC_JobCTRLRES));
                    if (test != datas.Length) return false;
                    return true;
                default:
                    return false;
            }
        }


        public bool Check_SRM_InvertorParam(byte devtype, byte devid, byte[] datas)
        {   
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_InvertorParamResCtrl));
                    if (test != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_SRM_REC_InvertorInfo(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_InvertorRes));
                    if (test != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }
        public bool Check_SRM_InvertorParamCtrlRes(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_InvertorParamCtrlRes));
                    if (test != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_SRM_REC_RackPosition(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                //가변길이 데이터
                case ConstClass.TYPE_SRM:
                    //Header 길이만큼도 안 왔으면
                    if (datas.Length < Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellPosition_Header))) return false;
                    VEXI_DEFS.TSRM_CellPosition_Header Header = (VEXI_DEFS.TSRM_CellPosition_Header)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TSRM_CellPosition_Header));

                    //와야하는 길이는 Header + (Position 데이터 Count * int 사이즈) 여야 한다
                    ushort count = (ushort)(Header.EndNo - Header.StartNo + 1);
                    int CalcLen = Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellPosition_Header)) + (count * Marshal.SizeOf(typeof(int)));
                    if (CalcLen != datas.Length) return false;

                    return true;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_SRM_REC_RackPositionCtrl(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    if (Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellPositionCTRLRes)) != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_RTV_REC_Position(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                //가변길이 데이터
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    //Header 길이만큼도 안 왔으면

                    if (datas.Length < Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_PositionParam_Header))) return false;
                    VEXI_DEFS.TRTV_PositionParam_Header Header = (VEXI_DEFS.TRTV_PositionParam_Header)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TRTV_PositionParam_Header));

                    ushort count = (ushort)(Header.PositionCount);
                    int CalcLen = Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_PositionParam_Header)) + (count * Marshal.SizeOf(typeof(int)));
                    if (CalcLen > datas.Length) return false;

                    return true;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_RTV_REC_PositionCtrl(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:

                    if (Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_PositionSetCTRLRes)) != datas.Length) return false;
                    return true;
                    
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_EMS_REC_Position(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                //가변길이 데이터
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    //Header 길이만큼도 안 왔으면

                    if (datas.Length < Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_PositionParam_Header))) return false;
                    VEXI_DEFS.TEMS_PositionParam_Header Header = (VEXI_DEFS.TEMS_PositionParam_Header)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TEMS_PositionParam_Header));

                    ushort count = (ushort)(Header.PositionCount);
                    int CalcLen = Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_PositionParam_Header)) + (count * Marshal.SizeOf(typeof(int)));
                    if (CalcLen > datas.Length) return false;

                    return true;
                default:
                    return false;
            }
        }

        public bool Check_EMS_REC_PositionCtrl(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    if (Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_PositionSetCTRLRes)) != datas.Length) return false;
                    return true;
                default:
                    return false;
            }
        }


        public bool Check_SRM_REC_RackOffset(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                //가변길이 데이터
                case ConstClass.TYPE_SRM:
                    //Header 길이만큼도 안 왔으면
                    if (datas.Length < Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellOffset_Header))) return false;
                    VEXI_DEFS.TSRM_CellOffset_Header Header = (VEXI_DEFS.TSRM_CellOffset_Header)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TSRM_CellOffset_Header));

                    //와야하는 길이는 Header + (Position 데이터 Count * int 사이즈) 여야 한다
                    byte count = Header.ItemCount;
                    int CalcLen = Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellOffset_Header)) + (count * Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellOffsetRec)));
                    if (CalcLen != datas.Length) return false;

                    return true;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_SRM_REC_RackOffsetCtrl(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    if (Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellOffsetCTRLRes)) != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_EMSRTV_REC_InvertorInfo(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TEMSRTV_REC_InvertorRes));
                    if (test != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_EMS:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TEMSRTV_REC_InvertorRes));
                    if (test != datas.Length) return false;
                    return true;
                default:
                    return false;
            }
        }


        public bool Check_RTV_REC_AreaSpeed(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                //가변길이 데이터
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    //Header 길이만큼도 안 왔으면
                    if (datas.Length < Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_SpeedAreaParamHeaderRec))) return false;
                    VEXI_DEFS.TRTV_SpeedAreaParamHeaderRec Header = (VEXI_DEFS.TRTV_SpeedAreaParamHeaderRec)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TRTV_SpeedAreaParamHeaderRec));

                    
                    ushort count = (ushort)(Header.AreaCount);
                    int CalcLen = Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_SpeedAreaParamHeaderRec)) + (count * Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_SpeedAreaGroupConfigRec)));
                    if (CalcLen > datas.Length) return false;

                    return true;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_RTV_REC_AreaSpeedCtrl(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:

                    if (Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_SpeedAreaGroupCTRLRes)) != datas.Length) return false;
                    return true;

                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }


        public bool Check_EMS_REC_AreaSpeed(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                //가변길이 데이터
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    //Header 길이만큼도 안 왔으면
                    if (datas.Length < Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_SpeedAreaParamHeaderRec))) return false;
                    VEXI_DEFS.TEMS_SpeedAreaParamHeaderRec Header = (VEXI_DEFS.TEMS_SpeedAreaParamHeaderRec)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TEMS_SpeedAreaParamHeaderRec));


                    ushort count = (ushort)(Header.AreaCount);
                    int CalcLen = Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_SpeedAreaParamHeaderRec)) + (count * Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_SpeedAreaGroupConfigRec)));
                    if (CalcLen > datas.Length) return false;

                    return true;
                default:
                    return false;
            }
        }

        public bool Check_EMS_REC_AreaSpeedCtrl(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    if (Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_SpeedAreaGroupCTRLRes)) != datas.Length) return false;
                    return true;
                default:
                    return false;
            }
        }


        public bool Check_RTV_REC_StationCtrl(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:

                    if (Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_StationParamCTRLRes)) != datas.Length) return false;
                    return true;

                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_EMS_REC_StationCtrl(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    if (Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_StationParamCTRLRes)) != datas.Length) return false;
                    return true;
                default:
                    return false;
            }
        }

        public bool Check_SRM_REC_StationParam(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    int CalcLen = Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_StationParam));
                    if (CalcLen != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_RTV_REC_StationParam(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    if (datas.Length < Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_StationParamHeaderRec))) return false;
                    VEXI_DEFS.TRTV_StationParamHeaderRec Header = (VEXI_DEFS.TRTV_StationParamHeaderRec)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TRTV_StationParamHeaderRec));


                    ushort count = (ushort)(Header.stationCount);
                    int CalcLen = Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_StationParamHeaderRec)) + (count * Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_StationConfigRec)));
                    if (CalcLen > datas.Length) return false;


                    return true;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_EMS_REC_StationParam(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    if (datas.Length < Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_StationParamHeaderRec))) return false;
                    VEXI_DEFS.TEMS_StationParamHeaderRec Header = (VEXI_DEFS.TEMS_StationParamHeaderRec)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TEMS_StationParamHeaderRec));

                    ushort count = (ushort)(Header.stationCount);
                    int CalcLen = Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_StationParamHeaderRec)) + (count * Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_StationConfigRec)));
                    if (CalcLen > datas.Length) return false;
                    return true;
                default:
                    return false;
            }
        }

        public bool Check_SRM_SpecialRackSt(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    if (Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_SpecialRack)) != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_SRM_NoUseRackSt(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    if (Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_NoUseRack)) != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }
        public bool Check_SRM_DriveParamRes(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_DriveParamRes));
                    if (test != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_RTV_DriveParamRes(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_DriveParamRes));
                    if (test!= datas.Length) return false;
                    return true;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_EMS_DriveParamRes(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_DriveParamRes));
                    if (test != datas.Length) return false;
                    return true;
                default:
                    return false;
            }
        }

        public bool Check_SRM_LiftParamRes(byte devtype, byte devid, byte[] datas)
        {
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    if (Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_LiftParamRes)) != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_EMS_LiftParamRes(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_LiftParamRes));
                    //if (test != datas.Length) return false;
                    if (test > datas.Length) return false;
                    return true;
                default:
                    return false;
            }
        }

        public bool Check_SRM_ForkParamRes(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_ForkParamRes));
                    if (test != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_RTV_FeedParamRes(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_FeedParamRes));
                    if (test != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_SRM_CtrlParamRes(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CTRLParamRes));
                    if (test != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_RTV_CtrlParamRes(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_CTRLParamRes));
                    if (test != datas.Length) return false;
                    return true;
                case ConstClass.TYPE_EMS:
                    return false;
                default:
                    return false;
            }
        }

        public bool Check_EMS_CtrlParamRes(byte devtype, byte devid, byte[] datas)
        {
            int test;
            switch (devtype)
            {
                case ConstClass.TYPE_SRM:
                    return false;
                case ConstClass.TYPE_RTV:
                    return false;
                case ConstClass.TYPE_EMS:
                    test = Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_CTRLParamRes));
                    if (test != datas.Length) return false;
                    return true;
                default:
                    return false;
            }
        }
        public void Serial_Send(byte TmpCMD1, byte TmpCMD2, object TmpDataRec)
        {
            TXSEQ++;
            if (IsRealUse)
            {
                TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, Real_DevType, Real_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(Marshal.SizeOf(TmpDataRec) + 1));
            } else
            {
                TxPacket.SetHeader(ConstClass.TYPE_02, 0x00, UserSelect_DevType, UserSelect_DevID, TXSEQ, TmpCMD1, TmpCMD2, (ushort)(Marshal.SizeOf(TmpDataRec) + 1));
            }
            TxPacket.SetBody(TmpDataRec);
            Serial_Send(TxPacket.GetTotalBytes(), TxPacket.GetTotalSize());
        }

        public void Serial_Send(byte[] TmpData, ushort Len)
        {
            if (fSerialPort.IsOpen)
            {
                try
                {
                    fSerialPort.Write(TmpData, 0, Len);
                    IsTxErr = false;
                }
                catch
                {
                    IsTxErr = true;
                }
            }
        }

        public bool Serial_Send(byte[] TmpData)
        {
            if (fSerialPort.IsOpen)
            {
                try
                {
                    fSerialPort.Write(TmpData, 0, TmpData.Length);
                    IsTxErr = false;
                    PollingRec.SendTime = DateTime.Now;
                    if (OnPacketSended != null)
                    {
                        OnPacketSended(CommMode, TmpData);
                    }
                    return true;
                }
                catch
                {
                    IsTxErr = true;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void UDP_Read()
        {
            //if (Sck.IsBound)
            if (Sck != null)
            {

                if (Sck.IsBound)
                {
                    if (UDPReadCallBack == null)
                    {
                        UDPReadCallBack = new AsyncCallback(MessageCallBack);
                    }

                    ((IPEndPoint)RecvRemotePoint).Address = ((IPEndPoint)RemotePoint).Address;
                    ((IPEndPoint)RecvRemotePoint).Port = (int)_RemtePort;


                    if (AsyncResult_UDP != null)
                    {
                        Do_SckReadEnd(AsyncResult_UDP, false);
                    }

                    try
                    {
                        
                        UDPReadFlag = 1;
                        AsyncResult_UDP = Sck.BeginReceiveFrom(Readbuffer, 0, Readbuffer.Length, SocketFlags.None,
                                              ref RecvRemotePoint, UDPReadCallBack, Readbuffer);
                    }
                    catch (Exception exp)
                    {
                    }
                }

            }
        }

        unsafe private void MessageCallBack(IAsyncResult aResult)
        {
            Do_SckReadEnd(aResult, true);

        }

        public bool UDP_Send(byte[] TmpData)
        {
            try
            {
                if (Sck != null)
                {
                    PollingRec.SendTime = DateTime.Now;
                    Sck.SendTo(TmpData, RemotePoint);
                    IsTxErr = false;
                    if (OnPacketSended != null)
                    {
                        OnPacketSended(CommMode, TmpData);
                    }
                }
                return true;
            }
            catch
            {
                IsTxErr = true;
                return false; 
            }
        }

        unsafe private void Do_SckReadEnd(IAsyncResult aResult, bool isCallback)
        {
            lock (UDPlockObject)
            {
                if (UDPReadFlag == 1)
                {
                    try
                    {
                        if (Sck != null)
                        {

                            int size = Sck.EndReceiveFrom(aResult, ref RecvRemotePoint);


                            if (size > 0)
                            {

                                UDPReadFlag = 2;
                                //IPEndPoint remoteIpEndPoint = RecvRemotePoint as IPEndPoint;
                                TStreamBufClass TmpStreamBufClass;

                                TmpStreamBufClass = this.Get_StreamBufClass(ConstClass.COMM_UDP);

                                if (TmpStreamBufClass != null)
                                {
                                    TmpStreamBufClass.Writebytes((byte[])aResult.AsyncState, (ushort)size);
                                }
                            }
                            

                            AsyncResult_UDP = null;

                        }
                    }
                    catch (Exception exp)
                    {
                    }
                }
            }
        }

        public void ADD_DataBuffer(byte TmpBufferID)
        {
            bool IsExist = false;
            if (fCientCount > 0)
            {
                for (int i = 0; i < fCientCount; i++)
                {
                    if ((fStreamBufClasses[i].BufferID == TmpBufferID))
                    {
                        IsExist = true;
                        break;
                    }

                }
            }
            if (!IsExist)
            {
                fStreamBufClasses[fCientCount] = new TStreamBufClass();
                fStreamBufClasses[fCientCount].BufferID = TmpBufferID;
                fCientCount++;
            }
        }


        private TStreamBufClass Get_StreamBufClass(byte TmpBufferID)
        {
            TStreamBufClass Result = null;

            if (fCientCount > 0)
            {
                for (int i = 0; i < fCientCount; i++)
                {
                    if ((fStreamBufClasses[i].BufferID == TmpBufferID))
                    {
                        Result = fStreamBufClasses[i];
                        break;
                    }

                }
            }

            return Result;
        }

        public void Writebyte(byte TmpBufferID, byte value)
        {
            TStreamBufClass Result = Get_StreamBufClass(TmpBufferID);

            if (Result != null)
            {
                Result.Writebyte(value);
            }
        }

        public void Writebytes(byte TmpBufferID, byte[] value)
        {
            TStreamBufClass Result = Get_StreamBufClass(TmpBufferID);

            if (Result != null)
            {
                Result.Writebytes(value);
            }
        }

        public ushort ReadDataCount(byte TmpBufferID)
        {
            TStreamBufClass Result = Get_StreamBufClass(TmpBufferID);
            if (Result != null)
            {
                return Result.ReadDataCount();
            }
            else
            {
                return 0;
            }
        }

        public byte Readbyte(byte TmpBufferID, ref byte data)
        {
            TStreamBufClass Result = Get_StreamBufClass(TmpBufferID);
            byte i = 0;
            if (Result != null)
            {
                i = Result.Readbyte(ref data);
            }

            return i;
        }

        public ushort Readbytes(byte TmpBufferID, ushort readCount, ref byte[] data)
        {
            TStreamBufClass Result = Get_StreamBufClass(TmpBufferID);
            ushort i = 0;
            if (Result != null)
            {
                i = Result.Readbytes(readCount, ref data);
            }

            return i;
        }

        public void ResetOffset(byte TmpBufferID)
        {
            TStreamBufClass Result = Get_StreamBufClass(TmpBufferID);
            if (Result != null)
            {
                Result.W_offset = 0;
                Result.R_offset = 0;
            }
        }
    }

}


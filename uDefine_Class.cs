using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace VEXI
{

    #region const
    //[ConstClass]
    //상수 선언, 사용자 타입 선언 클래스
    //static class로 객체 생성없이 ConstClass 로 사용한다.
    public static class ConstClass
    {
        // 패킷 관련
        public const int U_PACKET_START_SIZE = 4;
        public const int U_PACKET_HEADER_SIZE = 11;
        public const int U_PACKET_DATAMAX_SIZE = 1500;


        public const int U_PACKET_END_SIZE = 3;
        public const int U_PACKET_MAX_SIZE = U_PACKET_START_SIZE + U_PACKET_HEADER_SIZE + U_PACKET_DATAMAX_SIZE + U_PACKET_END_SIZE;
        public const int U_RECV_MAX_SIZE = U_PACKET_MAX_SIZE * 5;


        // TimeOut 관련
        public const int TIMEOUT_Polling = 1000;
        public const int TIMEOUT_RES = 5000;

        // 통신 타입
        public const byte COMM_UDP = 1;
        public const byte COMM_SERIAL = 2;
        public const byte COMM_TEST = 3;

        // 패킷 파싱
        public const byte PasingStartCode = 1;
        public const byte PasingHeader = 2;
        public const byte PasingBody = 3;
        public const byte PasingEndCode = 4;

        public const byte CODE_START = 0x16;
        public const byte CODE_END = 0xF5;

        //응답코드
        public const byte CODE_ACK = 0x00;
        public const byte CODE_NACK = 0x01;


        // ASCII
        public const char U_NUL = '\u0000';
        public const char U_LF = '\x000A'; //Line Feed 줄바꿈
        public const char U_CR = '\x000D'; //Carriage Return 커서르 제일앞으로 이동


        // UDP Port
        public const ushort MCUConnect_PORT = 11000;
        //public const ushort MCUConnect_PORT = 9000;
        public const ushort WIFIConnect_PORT = 9000;
        //public const ushort WIFIConnect_PORT = 11000;


        // 통신 : ID TYPE
        public const byte TYPE_00 = 0x00;  // 지상반, VCS
        public const byte TYPE_01 = 0x01;  // WCS
        public const byte TYPE_02 = 0x02;  // GUI
        public const byte TYPE_RTV = 0x40;  // RTV
        public const byte TYPE_EMS = 0x50;  // EMS
        public const byte TYPE_SRM = 0x60;  // SRM


        /*
         Command1과 Command2를 조합해서 명령을쏜다.
         프로토콜 EXCEL에 COMMAND1 번과 COMMAND2번을 조합해서 COMMAND를 만든다고 나와있다.
        */


        // 통신 : CMD
        
        /// <summary>
        /// 명령 1파트
        /// </summary>
        public const byte GUICMD = 0x01; 
        public const byte CMD1_00 = 0x00; //요청
        public const byte CMD1_80 = 0x80; //응답
        public const byte CMD1_C0 = 0xC0; //보고
        public const byte CMD1_40 = 0x40; //보고확인
        public const byte CMD1_01 = CMD1_00 | GUICMD; 
        public const byte CMD1_81 = CMD1_80 | GUICMD;
        public const byte CMD1_C1 = CMD1_C0 | GUICMD;
        public const byte CMD1_41 = CMD1_40 | GUICMD;


        /// <summary>
        /// 명령 2파트
        /// </summary>

        public const byte CMD2_10 = 0x10; // 기본정보 상태 SRM, RTV, EMS
        public const byte CMD2_11 = 0x11; // 기본정보 제어 SRM, RTV, EMS
        public const byte CMD2_12 = 0x12; // TEST 상태 SRM, RTV, EMS
        public const byte CMD2_13 = 0x13; // TEST 제어 SRM, RTV, EMS
        public const byte CMD2_14 = 0x14; // FUNC 제어 SRM, RTV, EMS
        public const byte CMD2_15 = 0x15; // 그래프 데이터 조회 SRM, RTV, EMS
        public const byte CMD2_16 = 0x16; // 출력 수동 제어 SRM, RTV, EMS

        public const byte CMD2_20 = 0x20; // 다운로드 시작 SRM, RTV, EMS
        public const byte CMD2_21 = 0x21; // 다운로드 데이터 SRM, RTV, EMS
        public const byte CMD2_22 = 0x22; // 다운로드 확인 SRM, RTV, EMS
        public const byte CMD2_23 = 0x23; // MCU I/O 형상 상태
        public const byte CMD2_24 = 0x24; // MCU I/O 형상 설정
        public const byte CMD2_25 = 0x25; // 장치고유설정 상태 SRM
        public const byte CMD2_26 = 0x26; // 장치고유설정 제어 SRM

        public const byte CMD2_30 = 0x30; // 상태요구(상태정보, 운행정보, 인버터 정보) SRM, RTV
        public const byte CMD2_31 = 0x31; // 운행정보 SRM, RTV
        public const byte CMD2_32 = 0x32; // 인버터 정보 SRM, RTV
        public const byte CMD2_34 = 0x34; // 알람로그 정보 SRM, RTV
        public const byte CMD2_35 = 0x35; // 알람로그 삭제 SRM, RTV
        public const byte CMD2_36 = 0x36;
        public const byte CMD2_37 = 0x37;
        public const byte CMD2_3D = 0x3D;

        public const byte CMD2_40 = 0x40; // 반송 지령 [구분작업](이동, LOAD, UNLOAD) SRM, RTV
        public const byte CMD2_41 = 0x41; // 반송 지령 [반송작업](이동, 입고, 출고, 랙간이동, 스테이션 이동) SRM, RTV
        public const byte CMD2_44 = 0x44; // 원점설정

        public const byte CMD2_50 = 0x50; // 시작 SRM, RTV
        public const byte CMD2_51 = 0x51; // 홈복귀 SRM, RTV
        public const byte CMD2_52 = 0x52; // 이상리셋 SRM, RTV
        public const byte CMD2_53 = 0x53; // 작업삭제 SRM, RTV
        public const byte CMD2_54 = 0x54; // 정지 SRM, RTV
        public const byte CMD2_55 = 0x55; // 비상정지 SRM, RTV
        public const byte CMD2_56 = 0x56; // 일시정지 SRM, RTV
        public const byte CMD2_57 = 0x57; // 복구(일시정지) SRM, RTV
        public const byte CMD2_58 = 0x58; // 모드 설정 SRM, RTV
        public const byte CMD2_59 = 0x59; // 보수위치 이동 SRM, RTV

        public const byte CMD2_60 = 0x60;
        public const byte CMD2_61 = 0x61;
        public const byte CMD2_62 = 0x62;
        public const byte CMD2_63 = 0x63;

        public const byte CMD2_80 = 0x80; // 원점확인, 수동명령 SRM, RTV

        public const byte CMD2_90 = 0x90; // 인버터 파라미터 설정 조회 SRM
        public const byte CMD2_91 = 0x91; // 인버터 파라미터 설정 변경 SRM
        public const byte CMD2_94 = 0x94; // 렉설정조회 SRM, 레일설정 조회 RTV
        public const byte CMD2_95 = 0x95; // 렉설정 변경 SRM, 레일설정 변경 RTV
        public const byte CMD2_96 = 0x96; // 셀 오프셋 조회 SRM
        public const byte CMD2_97 = 0x97; // 셀 오프셋 변경 SRM
        public const byte CMD2_98 = 0x98; // 스테이션 설정 조회 SRM, RTV
        public const byte CMD2_99 = 0x99; // 스테이션 설정 변경 SRM, RTV
        public const byte CMD2_9A = 0x9A; // 구간 주행 설정 조회 RTV
        public const byte CMD2_9B = 0x9B; // 구간 주행 설정 변경 RTV
        public const byte CMD2_9C = 0x9C; // 금지렉 설정조회 SRM
        public const byte CMD2_9D = 0x9D; // 금지렉 설정 변경 SRM
        public const byte CMD2_9E = 0x9E; // 스페셜렉 설정 조회 SRM
        public const byte CMD2_9F = 0x9F; // 스페셜렉 설정 변경 SRM


        public const byte CMD2_A0 = 0xA0; // 설정 초기화 SRM
        public const byte CMD2_A1 = 0xA1; // 제어설정 조회 SRM
        public const byte CMD2_A2 = 0xA2; // 제어설정 변경 SRMM
        public const byte CMD2_A3 = 0xA3; // 주행 드라이브 설정 조회 SRM, RTV
        public const byte CMD2_A4 = 0xA4; // 주행 드라이브 설정 변경 SRM, RTV
        public const byte CMD2_A5 = 0xA5; // 승강 드라이브 설정 조회 SRM
        public const byte CMD2_A6 = 0xA6; // 승강 드라이브 설정 변경 SRM, RTV
        public const byte CMD2_A7 = 0xA7; // 포크 드라이브 설정 조회 SRM 피딩 드라이브 설정 조회 RTV
        public const byte CMD2_A8 = 0xA8; // 포크 드라이브 설정 변경 SRM 피딩 드라이브 설정 변경 RTV


        //반송 지령 CMD
        public const byte SEMI_NONE = 0x00;
        public const byte SEMI_MOVE = 0x01;
        public const byte SEMI_TaskLoading = 0x02;
        public const byte SEMI_TaskUnLoading = 0x03;
        public const byte SEMI_INPUT = 0x12;
        public const byte SEMI_OUTPUT = 0x13;
        public const byte SEMI_RtoR = 0x14;
        public const byte SEMI_StoS = 0x15;
        public const byte SEMI_ChangeR = 0x16;
        public const byte SEMI_ChangeS = 0x17;
        public const byte SEMI_Loading = 0x18;
        public const byte SEMI_UnLoading = 0x19;
        public const byte SEMI_Sticky = 0x1A;

        //다운로드 Nack
        public const string Nakc_0x11 = "0x11 에러(현재 다른 장비로부터 다운로드 진행중)";
        public const string Nakc_0x12 = "0x12 에러(장비 동작 중)";
        public const string Nakc_0x13 = "0x13 에러(장비 자동 모드)";
        public const string Nakc_0x14 = "0x14 에러(프로그램 타입 이상)";
        public const string Nakc_0x15 = "0x15 에러(프로그램 사이즈 이상)";
        public const string Nakc_0x17 = "0x17 에러(다운로드 타입 이상)";
        public const string Nakc_0x18 = "0x18 에러(메모리 지우기 실패)";
        public const string Nakc_0x19 = "0x19 에러(외부 플래시 인식 안됨)";

        public const string Nakc_0x21 = "0x21 에러(수신위치가 틀림)";
        public const string Nakc_0x22 = "0x22 에러(패킷사이즈가 벗어남)";
        public const string Nakc_0x23 = "0x23 에러(플래쉬메모리 기록이 안됨)";
        public const string Nakc_0x24 = "0x24 에러(프로그램코드가 틀림)";
        public const string Nakc_0x25 = "0x25 에러(다운로드 시작상태가 아님)";

        public const string Nakc_0x31 = "0x31 에러(수신사이즈가 틀림)";
        public const string Nakc_0x32 = "0x32 에러(수신CRC가 틀림)";
        public const string Nakc_0x33 = "0x33 에러(프로그램코드가 틀림)";
        public const string Nakc_0x34 = "0x34 에러(플래쉬메모리 기록이 안됨)";
        public const string Nakc_0x35 = "0x35 에러(다운로드 시작상태가 아님)";

        public static string[] RTV_ABB_InvertorStNames =
        {
            "Bit.11 Internal limit active",
            "Bit.10 Target reached",
            "Bit.9 Remote",
            "Bit.7 Warning",
            "Bit.6 Switch on disabled",
            "Bit.5 Quick stop",
            "Bit.4 Voltage enabled",
            "Bit.3 Fault",
            "Bit.2 Operation enabled",
            "Bit.1 Switched on",
            "Bit.0 Ready to switch on",
            "Actual speed",
            "AccDeltaTime",
            "DecDeltaTime",
            "OperationMode"
        };
        public static string[] RTV_ABB_InvertorCtrlNames =
        {
            "Bit.8 Halt",
            "Bit.7 Fault reset",
            "Bit.6 Ramp function generator use ref",
            "Bit.5 Ramp function generator unlock",
            "Bit.4 Ramp function generator enable",
            "Bit.3 Enable operation",
            "Bit.2 Quick stop",
            "Bit.1 Enable voltage",
            "Bit.0 Switch on",
            "DemandVelocity",
            "ActualVelocity",
            "ActualTorgue",
            "Setpoint deceleration",
            "ErrorCode",
            "OperationMode"
        };

        public static string[] RTV_SEW_InvertorStNames =
        {
            "Bit.15 MOVIKIT Handshake Out",
            "Bit.14 Standby mode active",
            "Bit.12 SW limit switches inactive",
            "Bit.11 Setpoint/actual speed comparison signal active",
            "Bit.10 Drive train 2 active",
            "Bit.9 Warning",
            "Bit.8 Fault",
            "Bit.7 In position signal active",
            "Bit.6 New relative position applied",
            "Bit.5 Active drive referenced",
            "Bit.4 Motor running",
            "Bit.3 Brake released",
            "Bit.2 Output stage enabled",
            "Bit.1 STO inactive",
            "Bit.0 Ready",
            "Actual speed",
            "Status/ main fault - subfault",
            "actual torque",
            "Bit.2 Limit SW 1NC",
            "Bit.1 Limit SW 2NC",
            "Bit.0 Output stage enabled",
            "Actual application mode (Vel Reserved)",
            "Actual position (Vel Reserved)"
        };

        public static string[] RTV_SEW_InvertorCtrlNames =
        {
            "Bit.15 MOVIKIT Handshake In",
            "Bit.14 Activate standby mode",
            "Bit.13 Activate output stage inhibit",
            "Bit.12 Disable SW limit switches",
            "Bit.10 Activate drive train 2",
            "Bit.8 Fault reset",
            "Bit.7 Start/stop with fieldbus ramp",
            "Bit.6 Apply relative position",
            "Bit.5 Jog negative",
            "Bit.4 Jog positive",
            "Bit.3 Release brake",
            "Bit.1 Enable/application stop",
            "Bit.0 Enable/emergency stop",
            "Setpoint speed",
            "Acceleration",
            "Deceleration",
            "Bit.0 inhibit",
            "Setpoint application mode (Vel Reserved)",
            "Target position (Vel Reserved)",
            "Jerk limit"
        };

        //SRM DI List
        public static string[,] SRM_DI_Names_1 =
        {
            {"EM","0","00"                  ,"0"  },
            {"AUTO","0","01"                ,"0"  },
            {"INSP","0","02"                ,"0"  },
            {"RDF","0","03"                 ,"0"  },
            {"LST","0","04"                 ,"0"  },
            {"TST","0","05"                 ,"0"  },
            {"MFLT","0","06"                ,"0"  },
            {"GOV","0","07"                 ,"0"  },
            {"MCF","1","00"                 ,"0"  },
            {"MC1F","1","01"                ,"0"  },
            {"PDOOR","1","02"               ,"0"  },
            {"PTH","1","03"                 ,"0"  },
            {"MCTMF","1","04"               ,"0"  },
            {"MCFMF","1","05"               ,"0"  },
            {"T1PSF","1","06"               ,"0"  },
            {"T1OSO","1","07"               ,"0"  },
            {"LBMMSF1","1","08"             ,"0"  },
            {"TBMMSF1","1","09"             ,"0"  },
            {"FBMMSF1","1","10"             ,"0"  },
            {"CPTF","1","11"                ,"0"  },
            {"TDF","1","12"                 ,"0"  },
            {"TDR","1","13"                 ,"0"  },
            {"THP","1","14"                 ,"0"  },
            {"TSP","1","15"                 ,"0"  },
            {"CFLT","2","00"                ,"0"  },
            {"CRD","2","01"                 ,"0"  },
            {"MC2F","2","02"                ,"0"  },
            {"MCLMF","2","03"               ,"0"  },
            {"MCFM2F","2","04"              ,"0"  },
            {"T1PSF","2","05"               ,"0"  },
            {"T1OSO","2","06"               ,"0"  },
            {"FBMMSF2","2","07"             ,"0"  },
            {"CVOK1","2","08"               ,"0"  },
            {"CVOK2","2","09"               ,"0"  },
            {"CVOK3","2","10"               ,"0"  },
            {"CVOK4","2","11"               ,"0"  },
            {"CVOK5","2","12"               ,"0"  },
            {"CVOK6","2","13"               ,"0"  },
            {"CVOK7","2","14"               ,"0"  },
            {"CVOK8","2","15"               ,"0"  },
            {"FAN_FALT","3","00"                 ,"0"  },
            {"TS1-ENB","3","01"          ,"0"  },
            {"TS2-ENB","3","02"             ,"0"  },
            {"RVDR1","3","03"             ,"0"  },
            {"RVDR2","3","04"               ,"0"  },
            {"RVDL1","3","05"             ,"0"  },
            {"RVDL2","3","06"               ,"0"  },
            {"LBMMSF2","3","07"             ,"0"  },
            {"TBMMSF2","3","08"             ,"0"  },
            {"F1ENC","4","32"               ,"0"  },
            {"LDU","4","00"                 ,"0"  },
            {"LDD","4","01"                 ,"0"  },
            {"LHP","4","02"                 ,"0"  },
            {"LSP","4","03"                 ,"0"  },
            {"GOX1","5","00"                ,"0"  },
            {"GOXH1","5","01"               ,"0"  },
            {"GOXM1","5","02"               ,"0"  },
            {"GOXS1","5","03"               ,"0"  },
            {"GWL1","5","04"                ,"0"  },
            {"GWR1","5","05"                ,"0"  },
            {"GWLe1","5","06"               ,"1"  },
            {"GWRe1","5","07"               ,"1"  },
            {"GDFL1","5","08"               ,"0"  },
            {"GDFR1","5","09"               ,"0"  },
            {"GDRL1","5","10"               ,"0"  },
            {"GDRR1","5","11"               ,"0"  },
            {"GHL1","5","12"                ,"0"  },
            {"GHR1","5","13"                ,"0"  },
            {"FOKL1","5","14"               ,"0"  },
            {"FOKR1","5","15"               ,"0"  },
            {"FEL1","5","16"                ,"0"  },
            {"FER1","5","17"                ,"0"  },
            {"FCL1","5","18"                ,"0"  },
            {"FCR1","5","19"                ,"0"  },
            {"DSTL1","5","20"               ,"0"  },
            {"DSTR1","5","21"               ,"0"  },
            {"DSTLe1","5","22"              ,"1"  },
            {"DSTRe1","5","23"              ,"1"  },
            {"RTF","5","24"                 ,"0"  },
            {"RTR","5","25"                 ,"0"  },
            {"RTF2","5","26"                ,"0"  },
            {"RTR2","5","27"                ,"0"  },
            {"GOX2","6","00"                ,"0"  },
            {"GOXH2","6","01"               ,"0"  },
            {"GOXM2","6","02"               ,"0"  },
            {"GOXS2","6","03"               ,"0"  },
            {"GWL2","6","04"                ,"0"  },
            {"GWR2","6","05"                ,"0"  },
            {"GWLe2","6","06"               ,"1"  },
            {"GWRe2","6","07"               ,"1"  },
            {"GDFL2","6","08"               ,"0"  },
            {"GDFR2","6","09"               ,"0"  },
            {"GDRL2","6","10"               ,"0"  },
            {"GDRR2","6","11"               ,"0"  },
            {"GHL2","6","12"                ,"0"  },
            {"GHR2","6","13"                ,"0"  },
            {"FOKL2","6","14"               ,"0"  },
            {"FOKR2","6","15"               ,"0"  },
            {"FEL2","6","16"                ,"0"  },
            {"FER2","6","17"                ,"0"  },
            {"FCL2","6","18"                ,"0"  },
            {"FCR2","6","19"                ,"0"  },
            {"DSTL2","6","20"               ,"0"  },
            {"DSTR2","6","21"               ,"0"  },
            {"DSTLe2","6","22"              ,"1"  },
            {"DSTRe2","6","23"              ,"1"  },
            {"FML1","6","24"                ,"0"  },
            {"FMR1","6","25"                ,"0"  },
            {"FHL1","6","26"                ,"0"  },
            {"FHR1","6","27"                ,"0"  },
            {"FML2","6","28"                ,"0"  },
            {"FMR2","6","29"                ,"0"  },
            {"FHL2","6","30"                ,"0"  },
            {"FHR2","6","31"                ,"0"  },
            {"ODSTL1","7","00"              ,"0"  },
            {"ODSTR1","7","01"              ,"0"  },
            {"DSTLR1","7","02"              ,"0"  },
            {"DSTRR1","7","03"              ,"0"  },
            {"ODSTL2","7","04"              ,"0"  },
            {"ODSTR2","7","05"              ,"0"  },
            {"DSTLR2","7","06"              ,"0"  },
            {"DSTRR2","7","07"              ,"0"  }

        };

        public static string[,] SRM_DI_Names_2 =
        {
            {"LSTH","7","08"              ,"0"  },
            {"LSTE","7","09"              ,"0"  },
            {"LD","7","10"                ,"0"  },
            {"TSTH","7","11"              ,"0"  },
            {"TSTE","7","12"              ,"0"  },
            {"TD","7","13"                ,"0"  },
            {"FK1_FAULT","7","14"         ,"0"  },
            {"FK1_STOP","7","15"          ,"0"  },
            {"FK2_FAULT","7","16"         ,"0"  },
            {"FK2_STOP","7","17"          ,"0"  },
            {"SAFETY FLT","7","18"        ,"0"  }
        };


        //SRM DO List
        public static string[,] SRM_DO_Names_1 =
        {
          {"IINH","0","00" },
          {"FCD","0","02"},
          {"BL","0","03"},
          {"RED","0","04"},
          {"YEL","0","05"},
          {"GRN","0","06"},
          {"SUD","0","07"},
          {"MCE","1","00"},
          {"MCUB","1","01"},
          {"PLAMP","1","02"},
          {"PFAN","1","03"},
          {"MCTM","1","04"},
          {"MCFM1","1","05"},
          {"T1FSPC","1","06"},
          {"T1SPO","1","07"},
          {"MCFB1","1","10"},
          {"COSE","2","00"},
          {"CENB","2","01"},
          {"CRST","2","02"},
          {"MCLM","2","03"},
          {"MCFM2","2","04"},
          {"LFSPC","2","05"},
          {"LSPO","2","06"},
          {"MCFB2","2","07"},
          {"CVNO1","2","08"},
          {"CVNO2","2","09"},
          {"CVNO3","2","10"},
          {"CVNO4","2","11"},
          {"CVNO5","2","12"},
          {"CVNO6","2","13"},
          {"CVNO7","2","14"},
          {"CVNO8","2","15"},
          {"RDE","3","00"},
          {"DEVICE_RST","3","01"},
          {"LED_RD","4","00"},
          {"LED_GR","4","01"},
          {"LED_BU","4","02"},
          {"FK1_JOG","4","03"},
          {"FK1_JOG_R","4","04"},
          {"FK1_JOG_L","4","05"},
          {"FK1_RESET","4","06"},
          {"FK1_STO1","4","07"},
          {"FK1_STO2","4","08"},
        };

        public static string[,] SRM_DO_Names_2 =
        {
          {"FK2_JOG","4","09"},
          {"FK2_JOG_R","4","10"},
          {"FK2_JOG_L","4","11"},
          {"FK2_RESET","4","12"},
          {"FK2_STO1","4","13"},
          {"FK2_STO2","4","14"},
          {"WHI","4","15"},
          {"LSTH","4","16"},
          {"LSTE","4","17"}
        };


//RTV DI List
public static string[,] RTV_DI_Names =
        {
            {"EMERGENCY STOP"                                         ,"0","00"                  ,"0"  },
            {"전방 범퍼"                                              ,"0","01"                  ,"0"  },
            {"후방 범퍼"                                              ,"0","02"                  ,"0"  },
            {"DRIVE ENABLE"                                           ,"0","03"                  ,"0"  },
            {"SAFETY INPUT"                                           ,"0","04"                  ,"0"  },
            {"SAFETY CIRCUIT NORMAL"                                  ,"0","05"                  ,"0"  },
            {"FORCED RESET CIRCUIT NORMAL"                            ,"0","06"                  ,"0"  },
            {"광모뎀 이상"                                            ,"0","07"                  ,"0"  },
            {"주행1 모터MC 상태"                                      ,"0","08"                  ,"0"  },
            {"주행1 브레이크MC 상태"                                  ,"0","09"                  ,"0"  },
            {"피딩1 모터MC 상태"                                      ,"0","10"                  ,"0"  },
            {"피딩1 브레이브MC 상태"                                  ,"0","11"                  ,"0"  },
            {"주행2 모터MC 상태"                                      ,"0","12"                  ,"0"  },
            {"주행2 브레이크MC 상태"                                  ,"0","13"                  ,"0"  },
            {"피딩2 모터MC 상태"                                      ,"0","14"                  ,"0"  },
            {"피딩2 브레이브MC 상태"                                  ,"0","15"                  ,"0"  },
            {"피딩1 이재요구1(CRQ1)"                                  ,"0","16"                  ,"0"  },
            {"피딩1 이재가능1(CAL1)"                                  ,"0","17"                  ,"0"  },
            {"피딩1 이재완료1(CFN1)"                                  ,"0","18"                  ,"0"  },
            {"피딩1 C/V 수동1(CMA1)"                                  ,"0","19"                  ,"0"  },
            {"피딩1 이재요구2(CRQ1)"                                  ,"0","20"                  ,"0"  },
            {"피딩1 이재가능2(CAL1)"                                  ,"0","21"                  ,"0"  },
            {"피딩1 이재완료2(CFN1)"                                  ,"0","22"                  ,"0"  },
            {"피딩1 C/V 수동2(CMA1)"                                  ,"0","23"                  ,"0"  },
            {"피딩2 이재요구1(CRQ1)"                                  ,"0","24"                  ,"0"  },
            {"피딩2 이재가능1(CAL1)"                                  ,"0","25"                  ,"0"  },
            {"피딩2 이재완료1(CFN1)"                                  ,"0","26"                  ,"0"  },
            {"피딩2 C/V 수동1(CMA1)"                                  ,"0","27"                  ,"0"  },
            {"피딩2 이재요구2(CRQ1)"                                  ,"0","28"                  ,"0"  },
            {"피딩2 이재가능2(CAL1)"                                  ,"0","29"                  ,"0"  },
            {"피딩2 이재완료2(CFN1)"                                  ,"0","30"                  ,"0"  },
            {"피딩2 C/V 수동2(CMA1)"                                  ,"0","31"                  ,"0"  },
            {"주행 브레이크 수동 개방"                                ,"1","00"                  ,"0"  },
            {"피딩 브레이크 수동 개방"                                ,"1","01"                  ,"0"  },
            {"주행 정위치1"                                           ,"1","02"                  ,"0"  },
            {"주행 정위치2"                                           ,"1","03"                  ,"0"  },
            {"DC Fault"                                               ,"1","04"                  ,"0"  },
            {"주행 전진 감속"                                         ,"1","05"                  ,"0"  },
            {"주행 후진 감속"                                         ,"1","06"                  ,"0"  },
            {"주행 전방 Limit"                                        ,"1","07"                  ,"0"  },
            {"주행 후방 Limit"                                        ,"1","08"                  ,"0"  },
            {"피딩1 화물이탈(좌)"                                     ,"1","09"                  ,"0"  },
            {"피딩1 화물이탈(우)"                                     ,"1","10"                  ,"0"  },
            {"피딩1 화물감지1"                                        ,"1","11"                  ,"0"  },
            {"피딩1 화물감지2"                                        ,"1","12"                  ,"0"  },
            {"피딩1 화물감속(좌)"                                     ,"1","13"                  ,"0"  },
            {"피딩1 화물감속(우)"                                     ,"1","14"                  ,"0"  },
            {"피딩2 화물이탈(좌)"                                     ,"1","15"                  ,"0"  },
            {"피딩2 화물이탈(우)"                                     ,"1","16"                  ,"0"  },
            {"피딩2 화물감지1"                                        ,"1","17"                  ,"0"  },
            {"피딩2 화물감지2"                                        ,"1","18"                  ,"0"  },
            {"피딩2 화물감속(좌)"                                     ,"1","19"                  ,"0"  },
            {"피딩2 화물감속(우)"                                     ,"1","20"                  ,"0"  },
            {"주행1 RUN"                                              ,"1","21"                  ,"0"  },
            {"주행1 FLT"                                              ,"1","22"                  ,"0"  },
            {"주행1 ZERO"                                             ,"1","23"                  ,"0"  },
            {"피딩1 RUN"                                              ,"1","24"                  ,"0"  },
            {"피딩1 FLT"                                              ,"1","25"                  ,"0"  },
            {"피딩1 ZERO"                                             ,"1","26"                  ,"0"  },
            {"주행2 RUN"                                              ,"1","27"                  ,"0"  },
            {"주행2 FLT"                                              ,"1","28"                  ,"0"  },
            {"주행2 ZERO"                                             ,"1","29"                  ,"0"  },
            {"피딩2 RUN"                                              ,"1","30"                  ,"0"  },
            {"피딩2 FLT"                                              ,"1","31"                  ,"0"  },
            {"피딩2 ZERO"                                             ,"2","00"                  ,"0"  },
            {"비접촉 RDY"                                             ,"2","01"                  ,"0"  },
            {"비접촉 FLT"                                             ,"2","02"                  ,"0"  },
            {"거리센서 1"                                             ,"2","03"                  ,"0"  },
            {"거리센서 2"                                             ,"2","04"                  ,"0"  },
            {"전방 측역센서 검출1"                                         ,"2","05"                  ,"0"  },
            {"전방 측역센서 검출2"                                         ,"2","06"                  ,"0"  },
            {"전방 측역센서 검출3"                                         ,"2","07"                  ,"0"  },
            {"전방 측역센서 고장"                                          ,"2","08"                  ,"0"  },
            {"전방 측역센서 검출4"                                         ,"2","09"                  ,"0"  },
            {"전방 측역센서 검출5"                                         ,"2","10"                  ,"0"  },
            {"전방 측역센서 검출6"                                         ,"2","11"                  ,"0"  },
            {"전방 측역센서 검출7"                                         ,"2","12"                  ,"0"  },
            {"전방 측역센서 검출8"                                         ,"2","13"                  ,"0"  },
            {"거리센서 1 (후방)"                                      ,"2","14"                  ,"0"  },
            {"거리센서 2 (후방)"                                      ,"2","15"                  ,"0"  },
            {"피딩1 OVERLOAD"                                         ,"2","16"                  ,"0"  },
            {"피딩2 OVERLOAD"                                         ,"2","17"                  ,"0"  },
            {"FAN FAULT"                                              ,"2","18"                  ,"0"  },
            {"EMS HP"                                                 ,"2","19"                  ,"0"  },
            {"EMS OPL"                                                ,"2","20"                  ,"0"  },
            {"EMS OPR"                                                ,"2","21"                  ,"0"  },
            {"후방 측역센서 검출1"                                    ,"2","22"                  ,"0"  },
            {"후방 측역센서 검출2"                                    ,"2","23"                  ,"0"  },
            {"후방 측역센서 검출3"                                    ,"2","24"                  ,"0"  },
            {"후방 측역센서 고장"                                     ,"2","25"                  ,"0"  },
            {"후방 측역센서 검출4"                                    ,"2","26"                  ,"0"  },
            {"후방 측역센서 검출5"                                    ,"2","27"                  ,"0"  },
            {"후방 측역센서 검출6"                                    ,"2","28"                  ,"0"  },
            {"후방 측역센서 검출7"                                    ,"2","29"                  ,"0"  },
            {"후방 측역센서 검출8"                                    ,"2","30"                  ,"0"  }
        };

        //RTV DO List
        public static string[,] RTV_DO_Names_1 =
        {
            {"OPERATION READY"                         ,"0","00"                  ,"0"  },
            {"SAFETY OUTPUT"                           ,"0","01"                  ,"0"  },
            {"SAFETY CIRCUIT RESET"                    ,"0","02"                  ,"0"  },
            {"FORCED RESET"                            ,"0","03"                  ,"0"  },
            {"전방 LAMP RED"                           ,"0","04"                  ,"0"  },
            {"전방 LAMP GREEN"                         ,"0","05"                  ,"0"  },
            {"전방 LAMP BLUE"                          ,"0","06"                  ,"0"  },
            {"전방 BUZZER"                             ,"0","07"                  ,"0"  },
            {"후방 LAMP RED"                           ,"0","08"                  ,"0"  },
            {"후방 LAMP GREEN"                         ,"0","09"                  ,"0"  },
            {"후방 LAMP BLUE"                          ,"0","10"                  ,"0"  },
            {"후방 BUZZER"                             ,"0","11"                  ,"0"  },
            {"주행1 모터MC 제어"                       ,"0","12"                  ,"0"  },
            {"주행1 브레이크MC 제어"                   ,"0","13"                  ,"0"  },
            {"피딩1 모터MC 제어"                       ,"0","14"                  ,"0"  },
            {"피딩1 브레이크MC 제어"                   ,"0","15"                  ,"0"  },
            {"주행2 모터MC 제어"                       ,"0","16"                  ,"0"  },
            {"주행2 브레이크MC 제어"                   ,"0","17"                  ,"0"  },
            {"피딩2 모터MC 제어"                       ,"0","18"                  ,"0"  },
            {"피딩2 브레이크MC 제어"                   ,"0","19"                  ,"0"  },
            {"피딩1 이재요구1(RRQ1)"                   ,"0","20"                  ,"0"  },
            {"피딩1 이재가능1(RAL1)"                   ,"0","21"                  ,"0"  },
            {"피딩1 이재완료1(RFN1)"                   ,"0","22"                  ,"0"  },
            {"피딩1 RTV도작1(RAR1)"                    ,"0","23"                  ,"0"  },
            {"피딩1 이재요구2(RRQ1)"                   ,"0","24"                  ,"0"  },
            {"피딩1 이재가능2(RAL1)"                   ,"0","25"                  ,"0"  },
            {"피딩1 이재완료2(RFN1)"                   ,"0","26"                  ,"0"  },
            {"피딩1 RTV도작2(RAR2)"                    ,"0","27"                  ,"0"  },
            {"피딩2 이재요구1(RRQ1)"                   ,"0","28"                  ,"0"  },
            {"피딩2 이재가능1(RAL1)"                   ,"0","29"                  ,"0"  },
            {"피딩2 이재완료1(RFN1)"                   ,"0","30"                  ,"0"  },
            {"피딩2 RTV도작1(RAR1)"                    ,"0","31"                  ,"0"  },
            {"피딩2 이재요구2(RRQ1)"                   ,"1","00"                  ,"0"  },
            {"피딩2 이재가능2(RAL1)"                   ,"1","01"                  ,"0"  },
            {"피딩2 이재완료2(RFN1)"                   ,"1","02"                  ,"0"  },
            {"피딩2 RTV도작2(RAR2)"                    ,"1","03"                  ,"0"  },
            {"인버터1 리셋"                            ,"1","04"                  ,"0"  },
            {"인버터1 모터선택"                        ,"1","05"                  ,"0"  },
            {"주행1 전진"                              ,"1","06"                  ,"0"  },
            {"주행1 후진"                              ,"1","07"                  ,"0"  },
            {"주행1 속도1"                             ,"1","08"                  ,"0"  },
            {"주행1 속도2"                             ,"1","09"                  ,"0"  },
            {"주행1 속도3"                             ,"1","10"                  ,"0"  },
            {"피딩1 전진"                              ,"1","11"                  ,"0"  },
            {"피딩1 후진"                              ,"1","12"                  ,"0"  },
            {"피딩1 속도1"                             ,"1","13"                  ,"0"  },
            {"피딩1 속도2"                             ,"1","14"                  ,"0"  },
            {"인버터2 리셋"                            ,"1","15"                  ,"0"  },
            {"인버터2 모터선택"                        ,"1","16"                  ,"0"  },
            {"주행2 전진"                              ,"1","17"                  ,"0"  },
            {"주행2 후진"                              ,"1","18"                  ,"0"  },
            {"주행2 속도1"                             ,"1","19"                  ,"0"  },
            {"주행2 속도2"                             ,"1","20"                  ,"0"  },
            {"주행2 속도3"                             ,"1","21"                  ,"0"  },
            {"피딩2 전진"                              ,"1","22"                  ,"0"  },
            {"피딩2 후진"                              ,"1","23"                  ,"0"  },
            {"피딩2 속도1"                             ,"1","24"                  ,"0"  },
            {"피딩2 속도2"                             ,"1","25"                  ,"0"  },
            {"전방 측역센서 감지설정1"                 ,"1","26"                  ,"0"  },
            {"전방 측역센서 감지설정2"                 ,"1","26"                  ,"0"  },
            {"전방 측역센서 감지설정3"                 ,"1","26"                  ,"0"  },
            {"전방 측역센서 감지설정5"                 ,"1","26"                  ,"0"  },
            {"전방 측역센서 감지설정4"                 ,"1","26"                  ,"0"  },
        };
        public static string[,] RTV_DO_Names_2 =
        {
            {"주행 INVH"                      ,"1","27"                  ,"0"  },
            {"피딩1 INVH"                     ,"1","28"                  ,"0"  },
            {"피딩2 INVH"                     ,"1","29"                  ,"0"  },
            {"Tower Lamp Red"                 ,"1","30"                  ,"0"  },
            {"Tower Lamp Yellow"              ,"1","31"                  ,"0"  },
            {"Tower Lamp Green"               ,"2","0"                   ,"0"  },
            {"Tower Lamp White"               ,"2","1"                   ,"0"  },
            {"Tower Lamp Blue"                ,"2","2"                   ,"0"  },
            {"Tower Lamp BU"                  ,"2","3"                   ,"0"  },
            {"EMS HP LED"                     ,"2","4"                   ,"0"  },
            {"EMS OPL LED"                    ,"2","5"                   ,"0"  },
            {"EMS OPR LED"                    ,"2","6"                   ,"0"  },
            {"후방 측역센서 감지설정1"        ,"2","6"                   ,"0"  },
            {"후방 측역센서 감지설정2"        ,"2","6"                   ,"0"  },
            {"후방 측역센서 감지설정3"        ,"2","6"                   ,"0"  },
            {"후방 측역센서 감지설정4"        ,"2","6"                   ,"0"  },
            {"후방 측역센서 감지설정5"        ,"2","6"                   ,"0"  }
        };

        //EMS DI List
        public static string[,] EMS_DI_Names =
        {
            {"EMERGENCY STOP"                     ,"0","00"                  ,"0"  },
            {"SAFETY INPUT"                       ,"0","01"                  ,"0"  },
            {"SAFETY CIRCUIT NORMAL"              ,"0","02"                  ,"0"  },
            {"FORCED RESET CIRCUIT NORMAL"        ,"0","03"                  ,"0"  },
            {"TROLLEY CONTROL(S상제어)"           ,"0","04"                  ,"0"  },
            {"DRIVE ENABLE"                       ,"0","05"                  ,"0"  },
            {"전방 범퍼"                          ,"0","06"                  ,"0"  },
            {"후방 범퍼"                          ,"0","07"                  ,"0"  },
            {"CAGE HOME_A"                        ,"0","08"                  ,"0"  },
            {"CAGE HOME_B"                        ,"0","09"                  ,"1"  },
            {"HLD1"                               ,"0","10"                  ,"0"  },
            {"HLD2"                               ,"0","11"                  ,"0"  },
            {"HLD3"                               ,"0","12"                  ,"0"  },
            {"HLD4"                               ,"0","13"                  ,"0"  },
            {"TRAV_OVR_F"                         ,"0","14"                  ,"0"  },
            {"TRAV_OVR_R"                         ,"0","15"                  ,"0"  },
            {"T_DETECT1"                          ,"0","16"                  ,"0"  },
            {"T_DETECT2"                          ,"0","17"                  ,"1"  },
            {"T_DETECT3"                          ,"0","18"                  ,"1"  },
            {"TOX"                                ,"0","19"                  ,"0"  },
            {"T_ESC_R"                            ,"0","20"                  ,"0"  },
            {"T_ESC_L"                            ,"0","21"                  ,"0"  },
            {"CHK_HOME"                           ,"0","22"                  ,"0"  },
            {"CHK_END"                            ,"0","23"                  ,"0"  },
            {"CHK_FAULT"                          ,"0","24"                  ,"0"  },
            {"CHK_INPOS"                          ,"0","25"                  ,"0"  },
            {"CHK_READY"                          ,"0","26"                  ,"0"  },
            {"CAGE_GUIDE_HP"                      ,"0","27"                  ,"0"  },
            {"ST_INTERLOCK_I1"                    ,"0","28"                  ,"0"  },
            {"ST_INTERLOCK_I2"                    ,"0","29"                  ,"0"  },
            {"ST_INTERLOCK_I3"                    ,"0","30"                  ,"0"  },
            {"ST_INTERLOCK_I4"                    ,"0","31"                  ,"0"  },
            {"ST_INTERLOCK_I5"                    ,"1","00"                  ,"0"  },
            {"ST_INTERLOCK_I6"                    ,"1","01"                  ,"0"  },
            {"ST_INTERLOCK_I7"                    ,"1","02"                  ,"0"  },
            {"ST_INTERLOCK_I8"                    ,"1","03"                  ,"0"  },
            {"Laser Scanner_I1_F"                 ,"1","04"                  ,"0"  },
            {"Laser Scanner_I2_F"                 ,"1","05"                  ,"0"  },
            {"Laser Scanner_I3_F"                 ,"1","06"                  ,"0"  },
            {"Laser Scanner_I4_F"                 ,"1","07"                  ,"0"  },
            {"Laser Scanner_I5_F"                 ,"1","08"                  ,"0"  },
            {"Laser Scanner_I6_F"                 ,"1","09"                  ,"0"  },
            {"Laser Scanner_I7_F"                 ,"1","10"                  ,"0"  },
            {"Laser Scanner_I8_F"                 ,"1","11"                  ,"0"  },
            {"Laser Scanner_I1_R"                 ,"1","12"                  ,"0"  },
            {"Laser Scanner_I2_R"                 ,"1","13"                  ,"0"  },
            {"Laser Scanner_I3_R"                 ,"1","14"                  ,"0"  },
            {"Laser Scanner_I4_R"                 ,"1","15"                  ,"0"  },
            {"Laser Scanner_I5_R"                 ,"1","16"                  ,"0"  },
            {"Laser Scanner_I6_R"                 ,"1","17"                  ,"0"  },
            {"Laser Scanner_I7_R"                 ,"1","18"                  ,"0"  },
            {"Laser Scanner_I8_R"                 ,"1","19"                  ,"0"  },

            {"CHK_PIO_GO"                         ,"1","20"                  ,"0"  },
            {"ST_PIO_GO"                          ,"1","21"                  ,"0"  },


            {"CHK_DETECT1"                      ,"1","22"                  ,"0"  },
            {"CHK_DETECT2"                      ,"1","23"                  ,"1"  },
            {"CHK_DETECT3"                      ,"1","24"                  ,"1"  },
            {"CAGE_SPARE_I4"                      ,"1","25"                  ,"0"  },
            {"CAGE_SPARE_I5"                      ,"1","26"                  ,"0"  },
            {"CAGE_SPARE_I6"                      ,"1","27"                  ,"0"  },
            {"CAGE_SPARE_I7"                      ,"1","28"                  ,"0"  },
            {"CAGE_SPARE_I8"                      ,"1","29"                  ,"0"  },
            {"CAGE_SPARE_I9"                      ,"1","30"                  ,"0"  },
            {"CAGE_SPARE_I10"                     ,"1","31"                  ,"0"  }
        };

        //EMS DO List
        public static string[,] EMS_DO_Names =
        {
            {"OPERATION READY"          ,"0","00"                  ,"0"  },
            {"SAFETY OUTPUT"            ,"0","01"                  ,"0"  },
            {"SAFETY CIRCUIT RESET"     ,"0","02"                  ,"0"  },
            {"FORCED RESET"             ,"0","03"                  ,"0"  },
            {"주행 INVH"                ,"0","04"                  ,"0"  },
            {"승강 INVH"                ,"0","05"                  ,"0"  },
            {"CHK_ACT_M1"               ,"0","06"                  ,"0"  },
            {"CHK_ACT_M2"               ,"0","07"                  ,"0"  },
            {"CHK_ACT_M3"               ,"0","08"                  ,"0"  },
            {"CHK_ACT_M4"               ,"0","09"                  ,"0"  },
            {"CHK_STOP"                 ,"0","10"                  ,"0"  },
            {"CHK_RESET"                ,"0","11"                  ,"0"  },
            {"ST_INTERLOCK_O1"          ,"0","12"                  ,"0"  },
            {"ST_INTERLOCK_O2"          ,"0","13"                  ,"0"  },
            {"ST_INTERLOCK_O3"          ,"0","14"                  ,"0"  },
            {"ST_INTERLOCK_O4"          ,"0","15"                  ,"0"  },
            {"ST_INTERLOCK_O5"          ,"0","16"                  ,"0"  },
            {"ST_INTERLOCK_O6"          ,"0","17"                  ,"0"  },
            {"ST_INTERLOCK_O7"          ,"0","18"                  ,"0"  },
            {"ST_INTERLOCK_O8"          ,"0","19"                  ,"0"  },
            {"Laser Scanner_O1_F"       ,"0","20"                  ,"0"  },
            {"Laser Scanner_O2_F"       ,"0","21"                  ,"0"  },
            {"Laser Scanner_O3_F"       ,"0","22"                  ,"0"  },
            {"Laser Scanner_O4_F"       ,"0","23"                  ,"0"  },
            {"Laser Scanner_O5_F"       ,"0","24"                  ,"0"  },
            {"Laser Scanner_O6_F"       ,"0","25"                  ,"0"  },
            {"Laser Scanner_O7_F"       ,"0","26"                  ,"0"  },
            {"Laser Scanner_O8_F"       ,"0","27"                  ,"0"  },
            {"Laser Scanner_O9_F"       ,"0","28"                  ,"0"  },
            {"Laser Scanner_O10_F"      ,"0","29"                  ,"0"  },
            {"Laser Scanner_O11_F"      ,"0","30"                  ,"0"  },
            {"Laser Scanner_O12_F"      ,"0","31"                  ,"0"  },
            {"Laser Scanner_O1_R"       ,"1","00"                  ,"0"  },
            {"Laser Scanner_O2_R"       ,"1","01"                  ,"0"  },
            {"Laser Scanner_O3_R"       ,"1","02"                  ,"0"  },
            {"Laser Scanner_O4_R"       ,"1","03"                  ,"0"  },
            {"Laser Scanner_O5_R"       ,"1","04"                  ,"0"  },
            {"Laser Scanner_O6_R"       ,"1","05"                  ,"0"  },
            {"Laser Scanner_O7_R"       ,"1","06"                  ,"0"  },
            {"Laser Scanner_O8_R"       ,"1","07"                  ,"0"  },
            {"Laser Scanner_O9_R"       ,"1","08"                  ,"0"  },
            {"Laser Scanner_O10_R"      ,"1","09"                  ,"0"  },
            {"Laser Scanner_O11_R"      ,"1","10"                  ,"0"  },
            {"Laser Scanner_O12_R"      ,"1","11"                  ,"0"  },
            {"SRN_RD"                   ,"1","12"                  ,"0"  },
            {"SRN_GN"                   ,"1","13"                  ,"0"  },
            {"SRN_BU"                   ,"1","14"                  ,"0"  },
            {"SRN_BZ"                   ,"1","15"                  ,"0"  },
            {"SFY_LIGHT"                ,"1","16"                  ,"0"  },
            {"BZ"                       ,"1","17"                  ,"0"  },
            {"LED"                      ,"1","18"                  ,"0"  },
            {"LED_RED_R"                ,"1","19"                  ,"0"  },
            {"LED_GREEN_R"              ,"1","20"                  ,"0"  },
            {"LED_BLUE_R"               ,"1","21"                  ,"0"  },
            {"LED_RED_L"                ,"1","22"                  ,"0"  },
            {"LED_GREEN_L"              ,"1","23"                  ,"0"  },
            {"LED_BLUE_L"               ,"1","24"                  ,"0"  },
            {"CAGE_SPARE_O1"            ,"1","25"                  ,"0"  },
            {"CAGE_SPARE_O2"            ,"1","26"                  ,"0"  },
            {"CAGE_SPARE_O3"            ,"1","27"                  ,"0"  },
            {"CAGE_SPARE_O4"            ,"1","28"                  ,"0"  },
            {"CAGE_SPARE_O5"            ,"1","29"                  ,"0"  },
            {"CAGE_SPARE_O6"            ,"1","30"                  ,"0"  },
            {"CAGE_SPARE_O7"            ,"1","31"                  ,"0"  },
            {"CAGE_SPARE_O8"            ,"2","00"                  ,"0"  },
            {"CAGE_SPARE_O9"            ,"2","01"                  ,"0"  },
            {"CAGE_SPARE_O10"           ,"2","02"                  ,"0"  },
            {"CAGE_SPARE_O11"           ,"2","03"                  ,"0"  },
            {"CAGE_SPARE_O12"           ,"2","04"                  ,"0"  }
        };


        public static string[,] MOVEX_WCS_ADDR_DEF =
        //Addr RTV EMS1 EMS2
        {
            {"7000","B","B"  ,"B" },
            {"7001","W","W"  ,"W"  },
            {"7002","B","B"  ,"B"  },
            {"7003","W","W"  ,"W"  },
            {"7004","W","W"  ,"W"  },
            {"7005","W","W"  ,"W"  },
            {"7006","W","W"  ,"W"  },
            {"7007","W","W"  ,"W" },
            {"7008","W","W"  ,"W" },
            {"7009","W","W"  ,"W" },
            {"7010","W","W"  ,"W" },
            {"7011","W","W"  ,"W"  },
            {"7012","B","B"  ,"B"  },
            {"7013","W","W"  ,"W"  },
            {"7014","W","W"  ,"W"  },
            {"7015","W","W"  ,"W"  },
            {"7016","W","W"  ,"W"  },
            {"7017","W","W"  ,"W"  },
            {"7018","W","W"  ,"W"  },
            {"7019","W","W"  ,"W"  },
            {"7020","W","W"  ,"W"  },
            {"7021","W","W"  ,"W"  },
            {"7022","W","W"  ,"W"  },
            {"7023","W","W"  ,"W"  },
            {"7024","W","W"  ,"W"  },
            {"7025","B","B"  ,"B"  },
            {"7026","W","W"  ,"WL"  },
            {"7027","W","W"  ,"WH"  },
            {"7028","W","W"  ,"WL"  },
            {"7029","W","W"  ,"WH"  },
            {"7030","B","B"  ,"B"  },
            {"7031","W","W"  ,"W"  },
            {"7032","W","W"  ,"W"  },
            {"7033","W","W"  ,"W"  },
            {"7034","W","W"  ,"W"  },
            {"7035","W","W"  ,"W"  },
            {"7036","W","W"  ,"W"  },
            {"7037","W","W"  ,"W"  },
            {"7038","W","W"  ,"W"  },
            {"7039","W","W"  ,"W"  },
            {"7040","W","B"  ,"W"  },
            {"7041","W","B"  ,"W"  },
            {"7042","W","B"  ,"W"  },
            {"7043","W","B"  ,"W"  },
            {"7044","W","B"  ,"W"  },
            {"7045","W","B"  ,"W"  },
            {"7046","W","B"  ,"W"  },
            {"7047","W","B"  ,"W"  },
            {"7048","W","B"  ,"W"  },
            {"7049","W","B"  ,"W"  },
            {"7050","W","B"  ,"W"  },
            {"7051","W","B"  ,"W"  },
            {"7052","W","B"  ,"W"  },
            {"7053","W","B"  ,"W"  },
            {"7054","W","B"  ,"W"  },
            {"7055","W","B"  ,"W"  },
            {"7056","W","B"  ,"W"  },
            {"7057","W","B"  ,"W"  },
            {"7058","W","B"  ,"W"  },
            {"7059","W","B"  ,"W"  },
            {"7060","W","B"  ,"W"  },
            {"7061","W","B"  ,"W"  },
            {"7062","W","B"  ,"W"  },
            {"7063","W","B"  ,"W"  },
            {"7064","W","B"  ,"W"  },
            {"7065","W","B"  ,"W"  },
            {"7066","W","B"  ,"W"  },
            {"7067","W","B"  ,"W"  },
            {"7068","W","B"  ,"W"  },
            {"7069","W","B"  ,"W"  },
            {"7070","W","B"  ,"W"  },
            {"7071","W","B"  ,"W"  },
            {"7072","W","B"  ,"W"  },
            {"7073","W","B"  ,"W"  },
            {"7074","W","B"  ,"W"  },
            {"7075","W","B"  ,"W"  },
            {"7076","W","B"  ,"W"  },
            {"7077","W","B"  ,"W"  },
            {"7078","W","B"  ,"W"  },
            {"7079","W","B"  ,"W"  },
            {"7080","W","B"  ,"W"  },
            {"7081","W","B"  ,"W"  },
            {"7082","W","B"  ,"W"  },
            {"7083","W","B"  ,"W"  },
            {"7084","W","B"  ,"W"  },
            {"7085","W","B"  ,"W"  },
            {"7086","W","B"  ,"W"  },
            {"7087","W","B"  ,"W"  },
            {"7088","W","B"  ,"W"  },
            {"7089","W","B"  ,"W"  },
            {"7090","W","W"  ,"W"  },
            {"7091","W","W"  ,"W"  },
            {"7092","W","W"  ,"W"  },
            {"7093","W","W"  ,"W"  },
            {"7094","W","W"  ,"W"  },
            {"7095","W","W"  ,"W"  },
            {"7096","W","W"  ,"W"  },
            {"7097","W","W"  ,"W"  },
            {"7098","W","W"  ,"W"  },
            {"7099","W","W"  ,"W"  },
            {"7500","B","B"  ,"B"  },
            {"7501","W","W"  ,"W"  },
            {"7502","W","W"  ,"W"  },
            {"7503","W","W"  ,"W"  },
            {"7504","W","W"  ,"W"  },
            {"7505","W","W"  ,"W"  },
            {"7506","W","W"  ,"W"  },
            {"7507","W","W"  ,"W"  },
            {"7508","B","B"  ,"B"  },
            {"7509","B","B"  ,"B"  },
            {"7510","W","W"  ,"W"  },
            {"7511","W","W"  ,"W"  },
            {"7512","W","W"  ,"W"  },
            {"7513","W","W"  ,"W"  },
            {"7514","W","W"  ,"W"  },
            {"7515","B","B"  ,"B"  },
            {"7516","W","W"  ,"W"  },
            {"7517","W","W"  ,"W"  },
            {"7518","W","W"  ,"W"  },
            {"7519","W","W"  ,"W"  },
            {"7520","WL","WL","WL" },
            {"7521","WH","WH","WH" },
            {"7522","WL","WL","WL" },
            {"7523","WH","WH","WH" },
            {"7524","WL","WL","WL" },
            {"7525","WH","WH","WH" },
            {"7526","WL","WL","WL" },
            {"7527","WH","WH","WH" },
            {"7528","W","W"  ,"W"   },
            {"7529","W","W"  ,"W"   },
            {"7530","WL","WL","WL" },
            {"7531","WH","WH","WH" },
            {"7532","WL","WL","WL" },
            {"7533","WH","WH","WH" },
            {"7534","WL","WL","WL" },
            {"7535","WH","WH","WH" },
            {"7536","WL","WL","WL" },
            {"7537","WH","WH","WH" },
            {"7538","W","W"  ,"W" },
            {"7539","W","W"  ,"W" },
            {"7540","WL","WL","WL" },
            {"7541","WH","WH","WH" },
            {"7542","WL","WL","WL" },
            {"7543","WH","WH","WH" },
            {"7544","WL","WL","WL" },
            {"7545","WH","WH","WH" },
            {"7546","WL","WL","WL" },
            {"7547","WH","WH","WH" },
            {"7548","W","W"  ,"W" },
            {"7549","W","W"  ,"W" },
            {"7550","W","W"  ,"W" },
            {"7551","W","W"  ,"W" },
            {"7552","W","W"  ,"W" },
            {"7553","W","W"  ,"W" },
            {"7554","W","W"  ,"W" },
            {"7555","W","W"  ,"W" },
            {"7556","W","W"  ,"W" },
            {"7557","W","W"  ,"W" },
            {"7558","W","W"  ,"W" },
            {"7559","W","W"  ,"W" },
            {"7560","W","W"  ,"W" },
            {"7561","W","W"  ,"W" },
            {"7562","W","W"  ,"W" },
            {"7563","W","W"  ,"W" },
            {"7564","W","W"  ,"W" },
            {"7565","W","W"  ,"W" },
            {"7566","W","W"  ,"W" },
            {"7567","W","W"  ,"W" },
            {"7568","W","W"  ,"W" },
            {"7569","W","W"  ,"W" },
            {"7570","W","W"  ,"W" },
            {"7571","W","W"  ,"W" },
            {"7572","W","W"  ,"W" },
            {"7573","W","W"  ,"W" },
            {"7574","W","W"  ,"W" },
            {"7575","W","W"  ,"W" },
            {"7576","W","W"  ,"W" },
            {"7577","W","W"  ,"W" },
            {"7578","W","W"  ,"W" },
            {"7579","W","W"  ,"W" },
            {"7580","W","W"  ,"W" },
            {"7581","W","W"  ,"W" },
            {"7582","W","W"  ,"W" },
            {"7583","W","W"  ,"W" },
            {"7584","W","W"  ,"W" },
            {"7585","W","W"  ,"W" },
            {"7586","W","W"  ,"W" },
            {"7587","W","W"  ,"W" },
            {"7588","W","W"  ,"W" },
            {"7589","W","W"  ,"W" },
            {"7590","W","W"  ,"W" },
            {"7591","W","W"  ,"W" },
            {"7592","W","W"  ,"W" },
            {"7593","W","W"  ,"W" },
            {"7594","W","W"  ,"W" },
            {"7595","W","W"  ,"W" },
            {"7596","W","W"  ,"W" },
            {"7597","W","W"  ,"W" },
            {"7598","W","W"  ,"W" },
            {"7599","W","W"  ,"W" },
            {"7600","B","B"  ,"B" },
            {"7601","W","W"  ,"W" },
            {"7602","B","B"  ,"B" },
            {"7603","W","W"  ,"W" },
            {"7604","W","W"  ,"W" },
            {"7605","W","W"  ,"W" },
            {"7606","W","W"  ,"W" },
            {"7607","W","W"  ,"W" },
            {"7608","W","W"  ,"W" },
            {"7609","W","W"  ,"W" },
            {"7610","W","W"  ,"W" },
            {"7611","W","W"  ,"W" },
            {"7612","B","B"  ,"B" },
            {"7613","W","W"  ,"W" },
            {"7614","W","W"  ,"W" },
            {"7615","W","W"  ,"W" },
            {"7616","W","W"  ,"W" },
            {"7617","W","W"  ,"W" },
            {"7618","W","W"  ,"W" },
            {"7619","W","W"  ,"W" },
            {"7620","W","W"  ,"W" },
            {"7621","W","W"  ,"W" },
            {"7622","W","W"  ,"W" },
            {"7623","W","W"  ,"W" },
            {"7624","W","W"  ,"W" },
            {"7625","B","B"  ,"B" },
            {"7626","W","W"  ,"W" },
            {"7627","W","W"  ,"W" },
            {"7628","W","W"  ,"W" },
            {"7629","W","W"  ,"W" },
            {"7630","B","B"  ,"B" },
            {"7631","W","W"  ,"W" },
            {"7632","W","W"  ,"W" },
            {"7633","W","W"  ,"W" },
            {"7634","W","W"  ,"W" },
            {"7635","W","W"  ,"W" },
            {"7636","W","W"  ,"W" },
            {"7637","W","W"  ,"W" },
            {"7638","W","W"  ,"W" },
            {"7639","W","W"  ,"W" },
            {"7640","W","B"  ,"W" },
            {"7641","W","B"  ,"W" },
            {"7642","W","B"  ,"W" },
            {"7643","W","B"  ,"W" },
            {"7644","W","B"  ,"W" },
            {"7645","W","B"  ,"W" },
            {"7646","W","B"  ,"W" },
            {"7647","W","B"  ,"W" },
            {"7648","W","B"  ,"W" },
            {"7649","W","B"  ,"W" },
            {"7650","W","B"  ,"W" },
            {"7651","W","B"  ,"W" },
            {"7652","W","B"  ,"W" },
            {"7653","W","B"  ,"W" },
            {"7654","W","B"  ,"W" },
            {"7655","W","B"  ,"W" },
            {"7656","W","B"  ,"W" },
            {"7657","W","B"  ,"W" },
            {"7658","W","B"  ,"W" },
            {"7659","W","B"  ,"W" },
            {"7660","W","B"  ,"W" },
            {"7661","W","B"  ,"W" },
            {"7662","W","B"  ,"W" },
            {"7663","W","B"  ,"W" },
            {"7664","W","B"  ,"W" },
            {"7665","W","B"  ,"W" },
            {"7666","W","B"  ,"W" },
            {"7667","W","B"  ,"W" },
            {"7668","W","B"  ,"W" },
            {"7669","W","B"  ,"W" },
            {"7670","W","B"  ,"W" },
            {"7671","W","B"  ,"W" },
            {"7672","W","B"  ,"W" },
            {"7673","W","B"  ,"W" },
            {"7674","W","B"  ,"W" },
            {"7675","W","B"  ,"W" },
            {"7676","W","B"  ,"W" },
            {"7677","W","B"  ,"W" },
            {"7678","W","B"  ,"W" },
            {"7679","W","B"  ,"W" },
            {"7680","W","B"  ,"W" },
            {"7681","W","B"  ,"W" },
            {"7682","W","B"  ,"W" },
            {"7683","W","B"  ,"W" },
            {"7684","W","B"  ,"W" },
            {"7685","W","B"  ,"W" },
            {"7686","W","B"  ,"W" },
            {"7687","W","B"  ,"W" },
            {"7688","W","B"  ,"W" },
            {"7689","W","B"  ,"W" },
            {"7690","W","W"  ,"W" },
            {"7691","W","W"  ,"W" },
            {"7692","W","W"  ,"W" },
            {"7693","W","W"  ,"W" },
            {"7694","W","W"  ,"W" },
            {"7695","W","W"  ,"W" },
            {"7696","W","W"  ,"W" },
            {"7697","W","W"  ,"W" },
            {"7698","W","W"  ,"W" },
            {"7699","W","W"  ,"W" },
            {"7700","WL","WL","WL" },
            {"7701","WH","WH","WH" },
            {"7702","WL","WL","WL" },
            {"7703","WH","WH","WH" },
            {"7704","WL","WL","WL" },
            {"7705","WH","WH","WH" },
            {"7706","WL","WL","WL" },
            {"7707","WH","WH","WH" },
            {"7708","W","W"  ,"W" },
            {"7709","W","W"  ,"W" },
            {"7710","WL","WL","WL" },
            {"7711","WH","WH","WH" },
            {"7712","WL","WL","WL" },
            {"7713","WH","WH","WH" },
            {"7714","WL","WL","WL" },
            {"7715","WH","WH","WH" },
            {"7716","WL","WL","WL" },
            {"7717","WH","WH","WH" },
            {"7718","W","W"  ,"W" },
            {"7719","W","W"  ,"W" },
            {"7720","WL","WL","WL" },
            {"7721","WH","WH","WH" },
            {"7722","WL","WL","WL" },
            {"7723","WH","WH","WH" },
            {"7724","WL","WL","WL" },
            {"7725","WH","WH","WH" },
            {"7726","WL","WL","WL" },
            {"7727","WH","WH","WH" },
            {"7728","W","W"  ,"W" },
            {"7729","W","W"  ,"W" },
            {"7730","WL","WL","WL" },
            {"7731","WH","WH","WH" },
            {"7732","WL","WL","WL" },
            {"7733","WH","WH","WH" },
            {"7734","WL","WL","WL" },
            {"7735","WH","WH","WH" },
            {"7736","WL","WL","WL" },
            {"7737","WH","WH","WH" },
            {"7738","W","W"  ,"W"},
            {"7739","W","W"  ,"W"},
            {"7740","W","W"  ,"W"},
            {"7741","W","W"  ,"W"},
            {"7742","W","W"  ,"W"},
            {"7743","W","W"  ,"W"},
            {"7744","W","W"  ,"W"},
            {"7745","W","W"  ,"W"},
            {"7746","W","W"  ,"W"},
            {"7747","W","W"  ,"W"},
            {"7748","W","W"  ,"W"},
            {"7749","W","W"  ,"W"},
            {"7750","W","W"  ,"W"},
            {"7751","W","W"  ,"W"},
            {"7752","W","W"  ,"W"},
            {"7753","W","W"  ,"W"},
            {"7754","W","W"  ,"W"},
            {"7755","W","W"  ,"W"},
            {"7756","W","W"  ,"W"},
            {"7757","W","W"  ,"W"},
            {"7758","W","W"  ,"W"},
            {"7759","W","W"  ,"W"},
            {"7760","WL","WL","WL"},
            {"7761","WH","WH","WH"},
            {"7762","WL","WL","WL"},
            {"7763","WH","WH","WH"},
            {"7764","WL","WL","WL"},
            {"7765","WH","WH","WH"},
            {"7766","WL","WL","WL"},
            {"7767","WH","WH","WH"},
            {"7768","W","W"  ,"W" },
            {"7769","W","W"  ,"W" },
            {"7770","W","W"  ,"W" },
            {"7771","W","W"  ,"W" },
            {"7772","W","W"  ,"W" },
            {"7773","W","W"  ,"W" },
            {"7774","W","W"  ,"W" },
            {"7775","W","W"  ,"W" },
            {"7776","W","W"  ,"W" },
            {"7777","W","W"  ,"W" },
            {"7778","W","W"  ,"W" },
            {"7779","W","W"  ,"W" },
            {"7780","WL","WL","WL" },
            {"7781","WH","WH","WH" },
            {"7782","WL","WL","WL" },
            {"7783","WH","WH","WH" },
            {"7784","WL","WL","WL" },
            {"7785","WH","WH","WH" },
            {"7786","WL","WL","WL" },
            {"7787","WH","WH","WH" },
            {"7788","WL","WL","WL" },
            {"7789","WH","WH","WH" },
            {"7790","WL","WL","WL" },
            {"7791","WH","WH","WH" },
            {"7792","WL","WL","WL" },
            {"7793","WH","WH","WH" },
            {"7794","WL","WL","WL" },
            {"7795","WH","WH","WH" },
            {"7796","WL","WL","WL" },
            {"7797","WH","WH","WH" },
            {"7798","W","W"  ,"W"},
            {"7799","W","W"  ,"W"}

        };

        public static string[,] MOVEX_WCS_RTVDetail_DEF =
        {
        {"7000.0","Feed2 Loading Priority (트윈베드 우선순위 필)"},
        {"7000.1","Feed2 Unloading Priority (트윈베드 우선순위 필)"},
        { "7000.2","SPARE"},
        { "7000.3","SPARE"},
        { "7000.4","SPARE"},
        { "7000.5","SPARE"},
        { "7000.6","SPARE"},
        { "7000.7","SPARE"},
        { "7000.8","SPARE"},
        { "7000.9","SPARE"},
        { "7000.10","SPARE"},
        { "7000.11","SPARE"},
        { "7000.12","SPARE"},
        { "7000.13","SPARE"},
        { "7000.14","SPARE"},
        { "7000.15","SPARE"},
        { "7001","Feed1 Job Number"},
        { "7002.0","Feed1 Move Command"},
        { "7002.1","Feed1 Loading Command"},
        { "7002.2","Feed1 Unloading Command"},
        { "7002.3","SPARE"},
        { "7002.4","SPARE"},
        { "7002.5","SPARE"},
        { "7002.6","SPARE"},
        { "7002.7","SPARE"},
        { "7002.8","SPARE"},
        { "7002.9","SPARE"},
        { "7002.10","SPARE"},
        { "7002.11","SPARE"},
        { "7002.12","SPARE"},
        { "7002.13","SPARE"},
        { "7002.14","SPARE"},
        { "7002.15","SPARE"},
        { "7003","Feed1 Dest S/T No"},
        { "7004","SPARE"},
        { "7005","SPARE"},
        { "7006","SPARE"},
        { "7007","SPARE"},
        { "7008","SPARE"},
        { "7009","SPARE"},
        { "7010","SPARE"},
        { "7011","Feed2 Job Number"},
        { "7012.0","Feed2 Move Command"},
        { "7012.1","Feed2 Storage Command"},
        { "7012.2","Feed2 Retrieval Command"},
        { "7012.3","SPARE"},
        { "7012.4","SPARE"},
        { "7012.5","SPARE"},
        { "7012.6","SPARE"},
        { "7012.7","SPARE"},
        { "7012.8","SPARE"},
        { "7012.9","SPARE"},
        { "7012.10","SPARE"},
        { "7012.11","SPARE"},
        { "7012.12","SPARE"},
        { "7012.13","SPARE"},
        { "7012.14","SPARE"},
        { "7012.15","SPARE"},
        { "7013","Feed2 Dest S/T No"},
        { "7014","SPARE"},
        { "7015","SPARE"},
        { "7016","SPARE"},
        { "7017","SPARE"},
        { "7018","SPARE"},
        { "7019","SPARE"},
        { "7020","SPARE"},
        { "7021","SPARE"},
        { "7022","SPARE"},
        { "7023","SPARE"},
        { "7024","SPARE"},
        { "7025.0","Heart beat"},
        { "7025.1","SPARE"},
        { "7025.2","Alarm Reset"},
        { "7025.3","All Data Clear"},
        { "7025.4","Feed1 Data Clear"},
        { "7025.5","Feed2 Data Clear"},
        { "7025.6","SPARE"},
        { "7025.7","SPARE"},
        { "7025.8","RTV Data Report OK"},
        { "7025.9","SPARE"},
        { "7025.10","RTV Auto Request"},
        { "7025.11","RTV Manual Request"},
        { "7025.12","SPARE"},
        { "7025.13","SPARE"},
        { "7025.14","RTV Cycle Stop"},
        { "7025.15","RTV Emergency Stop"},
        { "7026","SPARE"},
        { "7027","SPARE"},
        { "7028","SPARE"},
        { "7029","SPARE"},
        { "7030.0","SPARE"},
        { "7030.1","SPARE"},
        { "7030.2","SPARE"},
        { "7030.3","SPARE"},
        { "7030.4","SPARE"},
        { "7030.5","SPARE"},
        { "7030.6","SPARE"},
        { "7030.7","SPARE"},
        { "7030.8","SPARE"},
        { "7030.9","SPARE"},
        { "7030.10","SPARE"},
        { "7030.11","SPARE"},
        { "7030.12","SPARE"},
        { "7030.13","SPARE"},
        { "7030.14","SPARE"},
        { "7030.15","SPARE"},
        { "7031","SPARE"},
        { "7032","SPARE"},
        { "7033","SPARE"},
        { "7034","SPARE"},
        { "7035","SPARE"},
        { "7036","SPARE"},
        { "7037","SPARE"},
        { "7038","SPARE"},
        { "7039","SPARE"},
        { "7040","SPARE"},
        { "7041","SPARE"},
        { "7042","SPARE"},
        { "7043","SPARE"},
        { "7044","SPARE"},
        { "7045","SPARE"},
        { "7046","SPARE"},
        { "7047","SPARE"},
        { "7048","SPARE"},
        { "7049","SPARE"},
        { "7050","SPARE"},
        { "7051","SPARE"},
        { "7052","SPARE"},
        { "7053","SPARE"},
        { "7054","SPARE"},
        { "7055","SPARE"},
        { "7056","SPARE"},
        { "7057","SPARE"},
        { "7058","SPARE"},
        { "7059","SPARE"},
        { "7060","SPARE"},
        { "7061","SPARE"},
        { "7062","SPARE"},
        { "7063","SPARE"},
        { "7064","SPARE"},
        { "7065","SPARE"},
        { "7066","SPARE"},
        { "7067","SPARE"},
        { "7068","SPARE"},
        { "7069","SPARE"},
        { "7070","SPARE"},
        { "7071","SPARE"},
        { "7072","SPARE"},
        { "7073","SPARE"},
        { "7074","SPARE"},
        { "7075","SPARE"},
        { "7076","SPARE"},
        { "7077","SPARE"},
        { "7078","SPARE"},
        { "7079","SPARE"},
        { "7080","SPARE"},
        { "7081","SPARE"},
        { "7082","SPARE"},
        { "7083","SPARE"},
        { "7084","SPARE"},
        { "7085","SPARE"},
        { "7086","SPARE"},
        { "7087","SPARE"},
        { "7088","SPARE"},
        { "7089","SPARE"},
        { "7090","SPARE"},
        { "7091","SPARE"},
        { "7092","SPARE"},
        { "7093","SPARE"},
        { "7094","SPARE"},
        { "7095","SPARE"},
        { "7096","SPARE"},
        { "7097","SPARE"},
        { "7098","SPARE"},
        { "7099","SPARE"},
        { "7500.0","RTV Job Request"},
        { "7500.1","Feed1 Job Exist"},
        { "7500.2","Feed2 Job Exist"},
        { "7500.3","RTV Busy"},
        { "7500.4","Feed1 Goods Detect"},
        { "7500.5","Feed2 Goods Detect"},
        { "7500.6","Feed1 Job Complete"},
        { "7500.7","Feed2 Job Complete"},
        { "7500.8","SPARE"},
        { "7500.9","SPARE"},
        { "7500.10","RTV Alarm"},
        { "7500.11","RTV Recoverable Alarm"},
        { "7500.12","RTV Home Position"},
        { "7500.13","SPARE"},
        { "7500.14","SPARE"},
        { "7500.15","SPARE"},
        { "7501","Main Alarm Code"},
        { "7502","Sub Alarm Code"},
        { "7503","RTV Current Station"},
        { "7504","SPARE"},
        { "7505","SPARE"},
        { "7506","SPARE"},
        { "7507","SPARE"},
        { "7508.0","SPARE"},
        { "7508.1","Feed1 Left Moving"},
        { "7508.2","SPARE"},
        { "7508.3","SPARE"},
        { "7508.4","SPARE"},
        { "7508.5","Feed1 Right Moving"},
        { "7508.6","SPARE"},
        { "7508.7","SPARE"},
        { "7508.8","SPARE"},
        { "7508.9","Feed2 Left Moving"},
        { "7508.10","SPARE"},
        { "7508.11","SPARE"},
        { "7508.12","SPARE"},
        { "7508.13","Feed2 Right Moving"},
        { "7508.14","SPARE"},
        { "7508.15","SPARE"},
        { "7509.0","Travel Motor Busy"},
        { "7509.1","SPARE"},
        { "7509.2","Feed1 Motor Busy"},
        { "7509.3","Feed2 Motor Busy"},
        { "7509.4","SPARE"},
        { "7509.5","SPARE"},
        { "7509.6","SPARE"},
        { "7509.7","SPARE"},
        { "7509.8","SPARE"},
        { "7509.9","SPARE"},
        { "7509.10","SPARE"},
        { "7509.11","SPARE"},
        { "7509.12","SPARE"},
        { "7509.13","SPARE"},
        { "7509.14","SPARE"},
        { "7509.15","SPARE"},
        { "7510","Response Code (미사용)"},
        { "7511","SPARE"},
        { "7512","SPARE"},
        { "7513","SPARE"},
        { "7514","SPARE"},
        { "7515.0","SPARE"},
        { "7515.1","SPARE"},
        { "7515.2","SPARE"},
        { "7515.3","SPARE"},
        { "7515.4","Travel Dest In-Position"},
        { "7515.5","SPARE"},
        { "7515.6","SPARE"},
        { "7515.7","SPARE"},
        { "7515.8","SPARE"},
        { "7515.9","SPARE"},
        { "7515.10","SPARE"},
        { "7515.11","SPARE"},
        { "7515.12","SPARE"},
        { "7515.13","SPARE"},
        { "7515.14","SPARE"},
        { "7515.15","SPARE"},
        { "7516","SPARE"},
        { "7517","SPARE"},
        { "7518","SPARE"},
        { "7519","SPARE"},
        { "7520","Travel Current Position (mm)"},
        { "7521","Travel Current Position (mm)"},
        { "7522","SPARE"},
        { "7523","SPARE"},
        { "7524","SPARE"},
        { "7525","SPARE"},
        { "7526","SPARE"},
        { "7527","SPARE"},
        { "7528","SPARE"},
        { "7529","SPARE"},
        { "7530","Travel Dest Position (mm)"},
        { "7531","Travel Dest Position (mm)"},
        { "7532","SPARE"},
        { "7533","SPARE"},
        { "7534","SPARE"},
        { "7535","SPARE"},
        { "7536","SPARE"},
        { "7537","SPARE"},
        { "7538","SPARE"},
        { "7539","SPARE"},
        { "7540","Travel Current Speed (m/min)"},
        { "7541","Travel Current Speed (m/min)"},
        { "7542","SPARE"},
        { "7543","SPARE"},
        { "7544","Feed1 Current Speed (m/min)"},
        { "7545","Feed1 Current Speed (m/min)"},
        { "7546","Feed2 Current Speed (m/min)"},
        { "7547","Feed2 Current Speed (m/min)"},
        { "7548","SPARE"},
        { "7549","SPARE"},
        { "7550","SPARE"},
        { "7551","SPARE"},
        { "7552","SPARE"},
        { "7553","SPARE"},
        { "7554","SPARE"},
        { "7555","SPARE"},
        { "7556","SPARE"},
        { "7557","SPARE"},
        { "7558","SPARE"},
        { "7559","SPARE"},
        { "7560","SPARE"},
        { "7561","SPARE"},
        { "7562","SPARE"},
        { "7563","SPARE"},
        { "7564","SPARE"},
        { "7565","SPARE"},
        { "7566","SPARE"},
        { "7567","SPARE"},
        { "7568","SPARE"},
        { "7569","SPARE"},
        { "7570","SPARE"},
        { "7571","SPARE"},
        { "7572","SPARE"},
        { "7573","SPARE"},
        { "7574","SPARE"},
        { "7575","SPARE"},
        { "7576","SPARE"},
        { "7577","SPARE"},
        { "7578","SPARE"},
        { "7579","SPARE"},
        { "7580","SPARE"},
        { "7581","SPARE"},
        { "7582","SPARE"},
        { "7583","SPARE"},
        { "7584","SPARE"},
        { "7585","SPARE"},
        { "7586","SPARE"},
        { "7587","SPARE"},
        { "7588","SPARE"},
        { "7589","SPARE"},
        { "7590","SPARE"},
        { "7591","SPARE"},
        { "7592","SPARE"},
        { "7593","SPARE"},
        { "7594","SPARE"},
        { "7595","SPARE"},
        { "7596","SPARE"},
        { "7597","SPARE"},
        { "7598","SPARE"},
        { "7599","SPARE"},
        { "7600.0","Feed2 Loading Priority "},
        { "7600.1","Feed2 Unloading Priority"},
        { "7600.2","SPARE"},
        { "7600.3","SPARE"},
        { "7600.4","SPARE"},
        { "7600.5","SPARE"},
        { "7600.6","SPARE"},
        { "7600.7","SPARE"},
        { "7600.8","SPARE"},
        { "7600.9","SPARE"},
        { "7600.10","SPARE"},
        { "7600.11","SPARE"},
        { "7600.12","SPARE"},
        { "7600.13","SPARE"},
        { "7600.14","SPARE"},
        { "7600.15","SPARE"},
        { "7601","Feed1 Job Number"},
        { "7602.0","Feed1 Move Command"},
        { "7602.1","Feed1 Loading Command"},
        { "7602.2","Feed1 Unloading Command"},
        { "7602.3","SPARE"},
        { "7602.4","SPARE"},
        { "7602.5","SPARE"},
        { "7602.6","SPARE"},
        { "7602.7","SPARE"},
        { "7602.8","SPARE"},
        { "7602.9","SPARE"},
        { "7602.10","SPARE"},
        { "7602.11","SPARE"},
        { "7602.12","SPARE"},
        { "7602.13","SPARE"},
        { "7602.14","SPARE"},
        { "7602.15","SPARE"},
        { "7603","Feed1 Dest S/T No"},
        { "7604","SPARE"},
        { "7605","SPARE"},
        { "7606","SPARE"},
        { "7607","SPARE"},
        { "7608","SPARE"},
        { "7609","SPARE"},
        { "7610","SPARE"},
        { "7611","Feed2 Job Number"},
        { "7612.0","Feed2 Move Command"},
        { "7612.1","Feed2 Loading Command"},
        { "7612.2","Feed2 Unloading Command"},
        { "7612.3","SPARE"},
        { "7612.4","SPARE"},
        { "7612.5","SPARE"},
        { "7612.6","SPARE"},
        { "7612.7","SPARE"},
        { "7612.8","SPARE"},
        { "7612.9","SPARE"},
        { "7612.10","SPARE"},
        { "7612.11","SPARE"},
        { "7612.12","SPARE"},
        { "7612.13","SPARE"},
        { "7612.14","SPARE"},
        { "7612.15","SPARE"},
        { "7613","Feed2 Dest S/T No"},
        { "7614","SPARE"},
        { "7615","SPARE"},
        { "7616","SPARE"},
        { "7617","SPARE"},
        { "7618","SPARE"},
        { "7619","SPARE"},
        { "7620","SPARE"},
        { "7621","SPARE"},
        { "7622","SPARE"},
        { "7623","SPARE"},
        { "7624","SPARE"},
        { "7625.0","Heart beat"},
        { "7625.1","SPARE"},
        { "7625.2","Alarm Reset Ack"},
        { "7625.3","All Data Clear Ack"},
        { "7625.4","Feed1 Data Clear Ack"},
        { "7625.5","Feed2 Data Clear Ack"},
        { "7625.6","SPARE"},
        { "7625.7","SPARE"},
        { "7625.8","RTV Data Report OK Status"},
        { "7625.9","RTV Auto Ready Status"},
        { "7625.10","RTV Auto Status"},
        { "7625.11","RTV Manual Status"},
        { "7625.12","SPARE"},
        { "7625.13","SPARE"},
        { "7625.14","RTV Cycle Stop Ack"},
        { "7625.15","RTV Emergency Stop Ack"},
        { "7626","SPARE"},
        { "7627","SPARE"},
        { "7628","SPARE"},
        { "7629","SPARE"},
        { "7630.0","Feed1 Manual Job Complete"},
        { "7630.1","Feed2 Manual Job Complete"},
        { "7630.2","SPARE"},
        { "7630.3","SPARE"},
        { "7630.4","Feed1 Manual Data Clear"},
        { "7630.5","Feed2 Manual Data Clear"},
        { "7630.6","SPARE"},
        { "7630.7","SPARE"},
        { "7630.8","SPARE"},
        { "7630.9","SPARE"},
        { "7630.10","SPARE"},
        { "7630.11","SPARE"},
        { "7630.12","SPARE"},
        { "7630.13","SPARE"},
        { "7630.14","SPARE"},
        { "7630.15","SPARE"},
        { "7631","SPARE"},
        { "7632","SPARE"},
        { "7633","SPARE"},
        { "7634","SPARE"},
        { "7635","SPARE"},
        { "7636","SPARE"},
        { "7637","SPARE"},
        { "7638","SPARE"},
        { "7639","SPARE"},
        { "7640","SPARE"},
        { "7641","SPARE"},
        { "7642","SPARE"},
        { "7643","SPARE"},
        { "7644","SPARE"},
        { "7645","SPARE"},
        { "7646","SPARE"},
        { "7647","SPARE"},
        { "7648","SPARE"},
        { "7649","SPARE"},
        { "7650","SPARE"},
        { "7651","SPARE"},
        { "7652","SPARE"},
        { "7653","SPARE"},
        { "7654","SPARE"},
        { "7655","SPARE"},
        { "7656","SPARE"},
        { "7657","SPARE"},
        { "7658","SPARE"},
        { "7659","SPARE"},
        { "7660","SPARE"},
        { "7661","SPARE"},
        { "7662","SPARE"},
        { "7663","SPARE"},
        { "7664","SPARE"},
        { "7665","SPARE"},
        { "7666","SPARE"},
        { "7667","SPARE"},
        { "7668","SPARE"},
        { "7669","SPARE"},
        { "7670","SPARE"},
        { "7671","SPARE"},
        { "7672","SPARE"},
        { "7673","SPARE"},
        { "7674","SPARE"},
        { "7675","SPARE"},
        { "7676","SPARE"},
        { "7677","SPARE"},
        { "7678","SPARE"},
        { "7679","SPARE"},
        { "7680","SPARE"},
        { "7681","SPARE"},
        { "7682","SPARE"},
        { "7683","SPARE"},
        { "7684","SPARE"},
        { "7685","SPARE"},
        { "7686","SPARE"},
        { "7687","SPARE"},
        { "7688","SPARE"},
        { "7689","SPARE"},
        { "7690","SPARE"},
        { "7691","SPARE"},
        { "7692","SPARE"},
        { "7693","SPARE"},
        { "7694","SPARE"},
        { "7695","SPARE"},
        { "7696","SPARE"},
        { "7697","SPARE"},
        { "7698","SPARE"},
        { "7699","SPARE"},
        { "7700","Travel Setting Speed (m/min)"},
        { "7701","Travel Setting Speed (m/min)"},
        { "7702","SPARE"},
        { "7703","SPARE"},
        { "7704","Feed1 Setting Speed (m/min)"},
        { "7705","Feed1 Setting Speed (m/min)"},
        { "7706","Feed2 Setting Speed (m/min)"},
        { "7707","Feed2 Setting Speed (m/min)"},
        { "7708","SPARE"},
        { "7709","SPARE"},
        { "7710","Travel Setting Acceleration (mm/sec^2)"},
        { "7711","Travel Setting Acceleration (mm/sec^2)"},
        { "7712","SPARE"},
        { "7713","SPARE"},
        { "7714","Feed1 Setting Acceleration (mm/sec^2)"},
        { "7715","Feed1 Setting Acceleration (mm/sec^2)"},
        { "7716","Feed2 Setting Acceleration (mm/sec^2)"},
        { "7717","Feed2 Setting Acceleration (mm/sec^2)"},
        { "7718","SPARE"},
        { "7719","SPARE"},
        { "7720","Travel Setting Deceleration (mm/sec^2)"},
        { "7721","Travel Setting Deceleration (mm/sec^2)"},
        { "7722","SPARE"},
        { "7723","SPARE"},
        { "7724","Feed1 Setting Deceleration (mm/sec^2)"},
        { "7725","Feed1 Setting Deceleration (mm/sec^2)"},
        { "7726","Feed2 Setting Deceleration (mm/sec^2)"},
        { "7727","Feed2 Setting Deceleration (mm/sec^2)"},
        { "7728","SPARE"},
        { "7729","SPARE"},
        { "7730","Travel Setting Jerk (mm/sec^3)"},
        { "7731","Travel Setting Jerk (mm/sec^3)"},
        { "7732","SPARE"},
        { "7733","SPARE"},
        { "7734","Feed1 Setting Jerk (mm/sec^3)"},
        { "7735","Feed1 Setting Jerk (mm/sec^3)"},
        { "7736","Feed2 Setting Jerk (mm/sec^3)"},
        { "7737","Feed2 Setting Jerk (mm/sec^3)"},
        { "7738","SPARE"},
        { "7739","SPARE"},
        { "7740","Travel Moving Before Delay (ms)"},
        { "7741","Travel Moving After Delay (ms)"},
        { "7742","Feed Moving Before Delay (ms)"},
        { "7743","Feed Moving After Delay (ms)"},
        { "7744","SPARE"},
        { "7745","SPARE"},
        { "7746","SPARE"},
        { "7747","SPARE"},
        { "7748","SPARE"},
        { "7749","SPARE"},
        { "7750","SPARE"},
        { "7751","SPARE"},
        { "7752","SPARE"},
        { "7753","SPARE"},
        { "7754","SPARE"},
        { "7755","SPARE"},
        { "7756","SPARE"},
        { "7757","SPARE"},
        { "7758","SPARE"},
        { "7759","SPARE"},
        { "7760","Travel Moter Torque (%)"},
        { "7761","Travel Moter Torque (%)"},
        { "7762","SPARE"},
        { "7763","SPARE"},
        { "7764","Feed1 Moter Torque (%)"},
        { "7765","Feed1 Moter Torque (%)"},
        { "7766","Feed2 Moter Torque (%)"},
        { "7767","Feed2 Moter Torque (%)"},
        { "7768","SPARE"},
        { "7769","SPARE"},
        { "7770","SPARE"},
        { "7771","SPARE"},
        { "7772","SPARE"},
        { "7773","SPARE"},
        { "7774","SPARE"},
        { "7775","SPARE"},
        { "7776","SPARE"},
        { "7777","SPARE"},
        { "7778","SPARE"},
        { "7779","SPARE"},
        { "7780","Operation Time (Sec)"},
        { "7781","Operation Time (Sec)"},
        { "7782","Travel Operation Time (Sec)"},
        { "7783","Travel Operation Time (Sec)"},
        { "7784","SPARE"},
        { "7785","SPARE"},
        { "7786","Feed1 Operation Time (Sec)"},
        { "7787","Feed1 Operation Time (Sec)"},
        { "7788","Feed2 Operation Time (Sec)"},
        { "7789","Feed2 Operation Time (Sec)"},
        { "7790","Travel Brake Open Count"},
        { "7791","Travel Brake Open Count"},
        { "7792","SPARE"},
        { "7793","SPARE"},
        { "7794","Feed1 Brake Open Count"},
        { "7795","Feed1 Brake Open Count"},
        { "7796","Feed2 Brake Open Count"},
        { "7797","Feed2 Brake Open Count"},
        { "7798","SPARE"},
        { "7799","SPARE"} };

        public static string[,] MOVEX_WCS_EMS1Detail_DEF =
        {
            {"7000.0","SPARE"},
            {"7000.1","SPARE"},
            {"7000.2","SPARE"},
            {"7000.3","SPARE"},
            {"7000.4","SPARE"},
            {"7000.5","SPARE"},
            {"7000.6","SPARE"},
            {"7000.7","SPARE"},
            {"7000.8","SPARE"},
            {"7000.9","SPARE"},
            {"7000.10","SPARE"},
            {"7000.11","SPARE"},
            {"7000.12","SPARE"},
            {"7000.13","SPARE"},
            {"7000.14","SPARE"},
            {"7000.15","SPARE"},
            {"7001","Loader1 Job Number"},
            {"7002.0","Loader1 Move Command"},
            {"7002.1","Loader1 Loading Command"},
            {"7002.2","Loader1 Unloading Command"},
            {"7002.3","SPARE"},
            {"7002.4","SPARE"},
            {"7002.5","SPARE"},
            {"7002.6","SPARE"},
            {"7002.7","SPARE"},
            {"7002.8","SPARE"},
            {"7002.9","SPARE"},
            {"7002.10","SPARE"},
            {"7002.11","SPARE"},
            {"7002.12","SPARE"},
            {"7002.13","SPARE"},
            {"7002.14","Loader1 Ignore Interlock Signal"},
            {"7002.15","Loader1 Allow empty receiving/shipping"},
            {"7003","Loader1 Dest S/T No"},
            {"7004","Loader1 Tire Chucking Width"},
            {"7005","Loader1 Tire Chucking Height"},
            {"7006","SPARE"},
            {"7007","SPARE"},
            {"7008","SPARE"},
            {"7009","SPARE"},
            {"7010","SPARE"},
            {"7011","Loader2 Job Number"},
            {"7012.0","Loader2 Move Command"},
            {"7012.1","Loader2 Loading Command"},
            {"7012.2","Loader2 Unloading Command"},
            {"7012.3","SPARE"},
            {"7012.4","SPARE"},
            {"7012.5","SPARE"},
            {"7012.6","SPARE"},
            {"7012.7","SPARE"},
            {"7012.8","SPARE"},
            {"7012.9","SPARE"},
            {"7012.10","SPARE"},
            {"7012.11","SPARE"},
            {"7012.12","SPARE"},
            {"7012.13","SPARE"},
            {"7012.14","Loader2 Ignore Interlock Signal"},
            {"7012.15","Loader2 Allow empty receiving/shipping"},
            {"7013","Loader2 Dest S/T No"},
            {"7014","Loader2 Tire Chucking Width"},
            {"7015","Loader2 Tire Chucking Height"},
            {"7016","SPARE"},
            {"7017","SPARE"},
            {"7018","SPARE"},
            {"7019","SPARE"},
            {"7020","SPARE"},
            {"7021","SPARE"},
            {"7022","SPARE"},
            {"7023","SPARE"},
            {"7024","SPARE"},
            {"7025.0","Heart beat"},
            {"7025.1","SPARE"},
            {"7025.2","Alarm Reset"},
            {"7025.3","All Data Clear"},
            {"7025.4","Loader1 Data Clear"},
            {"7025.5","Loader2 Data Clear"},
            {"7025.6","SPARE"},
            {"7025.7","SPARE"},
            {"7025.8","EMS Data Report OK"},
            {"7025.9","SPARE"},
            {"7025.10","EMS Auto Request"},
            {"7025.11","EMS Manual Request"},
            {"7025.12","SPARE"},
            {"7025.13","SPARE"},
            {"7025.14","EMS Cycle Stop"},
            {"7025.15","EMS Emergency Stop"},
            {"7026","SPARE"},
            {"7027","SPARE"},
            {"7028","SPARE"},
            {"7029","SPARE"},
            {"7030.0","SPARE"},
            {"7030.1","SPARE"},
            {"7030.2","SPARE"},
            {"7030.3","SPARE"},
            {"7030.4","SPARE"},
            {"7030.5","SPARE"},
            {"7030.6","SPARE"},
            {"7030.7","SPARE"},
            {"7030.8","SPARE"},
            {"7030.9","SPARE"},
            {"7030.10","SPARE"},
            {"7030.11","SPARE"},
            {"7030.12","SPARE"},
            {"7030.13","SPARE"},
            {"7030.14","SPARE"},
            {"7030.15","SPARE"},
            {"7031","SPARE"},
            {"7032","SPARE"},
            {"7033","SPARE"},
            {"7034","SPARE"},
            {"7035","SPARE"},
            {"7036","SPARE"},
            {"7037","SPARE"},
            {"7038","SPARE"},
            {"7039","SPARE"},
            {"70XX.0","IN 1"},
            {"70XX.1","IN 2 로딩가능(Chuck)"},
            {"70XX.2","IN 3"},
            {"70XX.3","IN 4 언로딩가능(Chuck)"},
            {"70XX.4","IN 5"},
            {"70XX.5","IN 6"},
            {"70XX.6","IN 7"},
            {"70XX.7","IN 8"},
            {"70XX.8","OUT 1"},
            {"70XX.9","OUT 2 구동불가(Chuck)"},
            {"70XX.10","OUT 3"},
            {"70XX.11","OUT 4"},
            {"70XX.12","OUT 5"},
            {"70XX.13","OUT 6"},
            {"70XX.14","OUT 7"},
            {"70XX.15","OUT 8"},
            {"7090","SPARE"},
            {"7091","SPARE"},
            {"7092","SPARE"},
            {"7093","SPARE"},
            {"7094","SPARE"},
            {"7095","SPARE"},
            {"7096","SPARE"},
            {"7097","SPARE"},
            {"7098","SPARE"},
            {"7099","SPARE"},
            {"7500.0","EMS Job Request"},
            {"7500.1","Loader1 Job Exist"},
            {"7500.2","Loader2 Job Exist"},
            {"7500.3","EMS Busy"},
            {"7500.4","Loader1 Goods Detect"},
            {"7500.5","Loader2 Goods Detect"},
            {"7500.6","Loader1 Job Complete"},
            {"7500.7","Loader2 Job Complete"},
            {"7500.8","SPARE"},
            {"7500.9","SPARE"},
            {"7500.10","EMS Alarm"},
            {"7500.11","EMS Recoverable Alarm"},
            {"7500.12","EMS Home Position"},
            {"7500.13","SPARE"},
            {"7500.14","SPARE"},
            {"7500.15","SPARE"},
            {"7501","Main Alarm Code"},
            {"7502","Sub Alarm Code"},
            {"7503","EMS Current Station"},
            {"7504","SPARE"},
            {"7505","SPARE"},
            {"7506","SPARE"},
            {"7507","SPARE"},
            {"7508.0","SPARE"},
            {"7508.1","Loader Down Moving"},
            {"7508.2","SPARE"},
            {"7508.3","Loader Up Moving"},
            {"7508.4","SPARE"},
            {"7508.5","SPARE"},
            {"7508.6","SPARE"},
            {"7508.7","SPARE"},
            {"7508.8","SPARE"},
            {"7508.9","Gripper Catch Moving"},
            {"7508.10","SPARE"},
            {"7508.11","Gripper Uncatch Moving"},
            {"7508.12","SPARE"},
            {"7508.13","SPARE"},
            {"7508.14","SPARE"},
            {"7508.15","SPARE"},
            {"7509.0","Travel Motor Busy"},
            {"7509.1","Loader Motor Busy"},
            {"7509.2","Gripper Motor Busy"},
            {"7509.3","SPARE"},
            {"7509.4","SPARE"},
            {"7509.5","SPARE"},
            {"7509.6","SPARE"},
            {"7509.7","SPARE"},
            {"7509.8","SPARE"},
            {"7509.9","SPARE"},
            {"7509.10","SPARE"},
            {"7509.11","SPARE"},
            {"7509.12","SPARE"},
            {"7509.13","SPARE"},
            {"7509.14","SPARE"},
            {"7509.15","SPARE"},
            {"7510","Response Code (미사용)"},
            {"7511","SPARE"},
            {"7512","SPARE"},
            {"7513","SPARE"},
            {"7514","SPARE"},
            {"7515.0","SPARE"},
            {"7515.1","Loader Home Position"},
            {"7515.2","Gripper Home Position"},
            {"7515.3","SPARE"},
            {"7515.4","Travel Dest In-Position"},
            {"7515.5","Loader Dest In-Position"},
            {"7515.6","Gripper Dest In-Position"},
            {"7515.7","SPARE"},
            {"7515.8","SPARE"},
            {"7515.9","SPARE"},
            {"7515.10","SPARE"},
            {"7515.11","SPARE"},
            {"7515.12","SPARE"},
            {"7515.13","SPARE"},
            {"7515.14","SPARE"},
            {"7515.15","SPARE"},
            {"7516","SPARE"},
            {"7517","SPARE"},
            {"7518","SPARE"},
            {"7519","SPARE"},
            {"7520","Travel Current Position (mm)"},
            {"7521","Travel Current Position (mm)"},
            {"7522","Loader Current Position (mm)"},
            {"7523","Loader Current Position (mm)"},
            {"7524","SPARE"},
            {"7525","SPARE"},
            {"7526","SPARE"},
            {"7527","SPARE"},
            {"7528","SPARE"},
            {"7529","SPARE"},
            {"7530","Travel Dest Position (mm)"},
            {"7531","Travel Dest Position (mm)"},
            {"7532","Loader Dest Position (mm)"},
            {"7533","Loader Dest Position (mm)"},
            {"7534","SPARE"},
            {"7535","SPARE"},
            {"7536","SPARE"},
            {"7537","SPARE"},
            {"7538","SPARE"},
            {"7539","SPARE"},
            {"7540","Travel Current Speed (m/min)"},
            {"7541","Travel Current Speed (m/min)"},
            {"7542","Loader Current Speed (m/min)"},
            {"7543","Loader Current Speed (m/min)"},
            {"7544","SPARE"},
            {"7545","SPARE"},
            {"7546","SPARE"},
            {"7547","SPARE"},
            {"7548","SPARE"},
            {"7549","SPARE"},
            {"7550","SPARE"},
            {"7551","SPARE"},
            {"7552","SPARE"},
            {"7553","SPARE"},
            {"7554","SPARE"},
            {"7555","SPARE"},
            {"7556","SPARE"},
            {"7557","SPARE"},
            {"7558","SPARE"},
            {"7559","SPARE"},
            {"7560","SPARE"},
            {"7561","SPARE"},
            {"7562","SPARE"},
            {"7563","SPARE"},
            {"7564","SPARE"},
            {"7565","SPARE"},
            {"7566","SPARE"},
            {"7567","SPARE"},
            {"7568","SPARE"},
            {"7569","SPARE"},
            {"7570","SPARE"},
            {"7571","SPARE"},
            {"7572","SPARE"},
            {"7573","SPARE"},
            {"7574","SPARE"},
            {"7575","SPARE"},
            {"7576","SPARE"},
            {"7577","SPARE"},
            {"7578","SPARE"},
            {"7579","SPARE"},
            {"7580","SPARE"},
            {"7581","SPARE"},
            {"7582","SPARE"},
            {"7583","SPARE"},
            {"7584","SPARE"},
            {"7585","SPARE"},
            {"7586","SPARE"},
            {"7587","SPARE"},
            {"7588","SPARE"},
            {"7589","SPARE"},
            {"7590","SPARE"},
            {"7591","SPARE"},
            {"7592","SPARE"},
            {"7593","SPARE"},
            {"7594","SPARE"},
            {"7595","SPARE"},
            {"7596","SPARE"},
            {"7597","SPARE"},
            {"7598","SPARE"},
            {"7599","SPARE"},
            {"7600.0","SPARE"},
            {"7600.1","SPARE"},
            {"7600.2","SPARE"},
            {"7600.3","SPARE"},
            {"7600.4","SPARE"},
            {"7600.5","SPARE"},
            {"7600.6","SPARE"},
            {"7600.7","SPARE"},
            {"7600.8","SPARE"},
            {"7600.9","SPARE"},
            {"7600.10","SPARE"},
            {"7600.11","SPARE"},
            {"7600.12","SPARE"},
            {"7600.13","SPARE"},
            {"7600.14","SPARE"},
            {"7600.15","SPARE"},
            {"7601","Loader1 Job Number"},
            {"7602.0","Loader1 Move Command"},
            {"7602.1","Loader1 Loading Command"},
            {"7602.2","Loader1 Unloading Command"},
            {"7602.3","SPARE"},
            {"7602.4","SPARE"},
            {"7602.5","SPARE"},
            {"7602.6","SPARE"},
            {"7602.7","SPARE"},
            {"7602.8","SPARE"},
            {"7602.9","SPARE"},
            {"7602.10","SPARE"},
            {"7602.11","SPARE"},
            {"7602.12","SPARE"},
            {"7602.13","SPARE"},
            {"7602.14","Loader1 Ignore Interlock Signal"},
            {"7602.15","Loader1 Allow empty receiving/shipping"},
            {"7603","Loader1 Dest S/T No"},
            {"7604","Loader1 Tire Chucking Width"},
            {"7605","Loader1 Tire Chucking Height"},
            {"7606","SPARE"},
            {"7607","SPARE"},
            {"7608","SPARE"},
            {"7609","SPARE"},
            {"7610","SPARE"},
            {"7611","Loader2 Job Number"},
            {"7612.0","Loader2 Move Command"},
            {"7612.1","Loader2 Loading Command"},
            {"7612.2","Loader2 Unloading Command"},
            {"7612.3","SPARE"},
            {"7612.4","SPARE"},
            {"7612.5","SPARE"},
            {"7612.6","SPARE"},
            {"7612.7","SPARE"},
            {"7612.8","SPARE"},
            {"7612.9","SPARE"},
            {"7612.10","SPARE"},
            {"7612.11","SPARE"},
            {"7612.12","SPARE"},
            {"7612.13","SPARE"},
            {"7612.14","Loader2 Ignore Interlock Signal"},
            {"7612.15","Loader2 Allow empty receiving/shipping"},
            {"7613","Loader2 Dest S/T No"},
            {"7614","Loader2 Tire Chucking Width"},
            {"7615","Loader2 Tire Chucking Height"},
            {"7616","SPARE"},
            {"7617","SPARE"},
            {"7618","SPARE"},
            {"7619","SPARE"},
            {"7620","SPARE"},
            {"7621","SPARE"},
            {"7622","SPARE"},
            {"7623","SPARE"},
            {"7624","SPARE"},
            {"7625.0","Heart beat"},
            {"7625.1","SPARE"},
            {"7625.2","Alarm Reset Ack"},
            {"7625.3","All Data Clear Ack"},
            {"7625.4","Loader1 Data Clear Ack"},
            {"7625.5","Loader2 Data Clear Ack"},
            {"7625.6","SPARE"},
            {"7625.7","SPARE"},
            {"7625.8","EMS Data Report OK Status"},
            {"7625.9","EMS Auto Ready Status"},
            {"7625.10","EMS Auto Status"},
            {"7625.11","EMS Manual Status"},
            {"7625.12","SPARE"},
            {"7625.13","SPARE"},
            {"7625.14","EMS Cycle Stop Ack"},
            {"7625.15","EMS Emergency Stop Ack"},
            {"7626","SPARE"},
            {"7627","SPARE"},
            {"7628","SPARE"},
            {"7629","SPARE"},
            {"7630.0","Loader1 Manual Job Complete"},
            {"7630.1","Loader2 Manual Job Complete"},
            {"7630.2","SPARE"},
            {"7630.3","SPARE"},
            {"7630.4","Loader1 Manual Data Clear"},
            {"7630.5","Loader2 Manual Data Clear"},
            {"7630.6","SPARE"},
            {"7630.7","SPARE"},
            {"7630.8","SPARE"},
            {"7630.9","SPARE"},
            {"7630.10","SPARE"},
            {"7630.11","SPARE"},
            {"7630.12","SPARE"},
            {"7630.13","SPARE"},
            {"7630.14","SPARE"},
            {"7630.15","SPARE"},
            {"7631","SPARE"},
            {"7632","SPARE"},
            {"7633","SPARE"},
            {"7634","SPARE"},
            {"7635","SPARE"},
            {"7636","SPARE"},
            {"7637","SPARE"},
            {"7638","SPARE"},
            {"7639","SPARE"},
            {"76XX.0","IN 1"},
            {"76XX.1","IN 2 로딩가능(Chuck)"},
            {"76XX.2","IN 3"},
            {"76XX.3","IN 4 언로딩가능(Chuck)"},
            {"76XX.4","IN 5"},
            {"76XX.5","IN 6"},
            {"76XX.6","IN 7"},
            {"76XX.7","IN 8"},
            {"76XX.8","OUT 1"},
            {"76XX.9","OUT 2 구동불가(Chuck)"},
            {"76XX.10","OUT 3"},
            {"76XX.11","OUT 4"},
            {"76XX.12","OUT 5"},
            {"76XX.13","OUT 6"},
            {"76XX.14","OUT 7"},
            {"76XX.15","OUT 8"},
            {"7690","SPARE"},
            {"7691","SPARE"},
            {"7692","SPARE"},
            {"7693","SPARE"},
            {"7694","SPARE"},
            {"7695","SPARE"},
            {"7696","SPARE"},
            {"7697","SPARE"},
            {"7698","SPARE"},
            {"7699","SPARE"},
            {"7700","Travel Setting Speed (m/min)"},
            {"7701","Travel Setting Speed (m/min)"},
            {"7702","Loader Setting Speed (m/min)"},
            {"7703","Loader Setting Speed (m/min)"},
            {"7704","SPARE"},
            {"7705","SPARE"},
            {"7706","SPARE"},
            {"7707","SPARE"},
            {"7708","SPARE"},
            {"7709","SPARE"},
            {"7710","Travel Setting Acceleration (mm/sec^2)"},
            {"7711","Travel Setting Acceleration (mm/sec^2)"},
            {"7712","Loader Setting Acceleration (mm/sec^2)"},
            {"7713","Loader Setting Acceleration (mm/sec^2)"},
            {"7714","SPARE"},
            {"7715","SPARE"},
            {"7716","SPARE"},
            {"7717","SPARE"},
            {"7718","SPARE"},
            {"7719","SPARE"},
            {"7720","Travel Setting Deceleration (mm/sec^2)"},
            {"7721","Travel Setting Deceleration (mm/sec^2)"},
            {"7722","Loader Setting Deceleration (mm/sec^2)"},
            {"7723","Loader Setting Deceleration (mm/sec^2)"},
            {"7724","SPARE"},
            {"7725","SPARE"},
            {"7726","SPARE"},
            {"7727","SPARE"},
            {"7728","SPARE"},
            {"7729","SPARE"},
            {"7730","Travel Setting Jerk (mm/sec^3)"},
            {"7731","Travel Setting Jerk (mm/sec^3)"},
            {"7732","Loader Setting Jerk (mm/sec^3)"},
            {"7733","Loader Setting Jerk (mm/sec^3)"},
            {"7734","SPARE"},
            {"7735","SPARE"},
            {"7736","SPARE"},
            {"7737","SPARE"},
            {"7738","SPARE"},
            {"7739","SPARE"},
            {"7740","Travel Moving Before Delay (ms)"},
            {"7741","Travel Moving After Delay (ms)"},
            {"7742","Loader Down Moving Before Delay (ms)"},
            {"7743","Loader Down Moving After Delay (ms)"},
            {"7744","Loader Up Moving Before Delay (ms)"},
            {"7745","Loader Up Moving After Delay (ms)"},
            {"7746","Gripper Catch Moving Before Delay (ms)"},
            {"7747","Gripper Catch Moving After Delay (ms)"},
            {"7748","Gripper Uncatch Moving Before Delay (ms)"},
            {"7749","Gripper Uncatch Moving After Delay (ms)"},
            {"7750","SPARE"},
            {"7751","SPARE"},
            {"7752","SPARE"},
            {"7753","SPARE"},
            {"7754","SPARE"},
            {"7755","SPARE"},
            {"7756","SPARE"},
            {"7757","SPARE"},
            {"7758","SPARE"},
            {"7759","SPARE"},
            {"7760","Travel Moter Torque (%)"},
            {"7761","Travel Moter Torque (%)"},
            {"7762","Loader Moter Torque (%)"},
            {"7763","Loader Moter Torque (%)"},
            {"7764","Gripper Moter Torque (%)"},
            {"7765","Gripper Moter Torque (%)"},
            {"7766","SPARE"},
            {"7767","SPARE"},
            {"7768","SPARE"},
            {"7769","SPARE"},
            {"7770","SPARE"},
            {"7771","SPARE"},
            {"7772","SPARE"},
            {"7773","SPARE"},
            {"7774","SPARE"},
            {"7775","SPARE"},
            {"7776","SPARE"},
            {"7777","SPARE"},
            {"7778","SPARE"},
            {"7779","SPARE"},
            {"7780","Operation Time (Sec)"},
            {"7781","Operation Time (Sec)"},
            {"7782","Travel Operation Time (Sec)"},
            {"7783","Travel Operation Time (Sec)"},
            {"7784","Loader Operation Time (Sec)"},
            {"7785","Loader Operation Time (Sec)"},
            {"7786","Gripper Operation Time (Sec)"},
            {"7787","Gripper Operation Time (Sec)"},
            {"7788","SPARE"},
            {"7789","SPARE"},
            {"7790","Travel Brake Open Count"},
            {"7791","Travel Brake Open Count"},
            {"7792","Loader Brake Open Count"},
            {"7793","Loader Brake Open Count"},
            {"7794","Gripper Brake Open Count"},
            {"7795","Gripper Brake Open Count"},
            {"7796","SPARE"},
            {"7797","SPARE"},
            {"7798","SPARE"},
            {"7799","SPARE"}
        };

        public static string[,] MOVEX_WCS_EMS2Detail_DEF =
        {
            {"7000.0","SPARE"},
            {"7000.1","SPARE"},
            {"7000.2","SPARE"},
            {"7000.3","SPARE"},
            {"7000.4","SPARE"},
            {"7000.5","SPARE"},
            {"7000.6","SPARE"},
            {"7000.7","SPARE"},
            {"7000.8","SPARE"},
            {"7000.9","SPARE"},
            {"7000.10","SPARE"},
            {"7000.11","SPARE"},
            {"7000.12","SPARE"},
            {"7000.13","SPARE"},
            {"7000.14","SPARE"},
            {"7000.15","SPARE"},
            {"7001","Loader1 Job Number"},
            {"7002.0","Loader1 Move Command"},
            {"7002.1","Loader1 Loading Command"},
            {"7002.2","Loader1 Unloading Command"},
            {"7002.3","Loader1 Loading & Unloading Command"},
            {"7002.4","SPARE"},
            {"7002.5","SPARE"},
            {"7002.6","SPARE"},
            {"7002.7","SPARE"},
            {"7002.8","SPARE"},
            {"7002.9","SPARE"},
            {"7002.10","SPARE"},
            {"7002.11","SPARE"},
            {"7002.12","SPARE"},
            {"7002.13","SPARE"},
            {"7002.14","Loader1 Ignore Interlock Signal"},
            {"7002.15","Loader1 Allow empty receiving/shipping"},
            {"7003","Loader1 Dest S/T No"},
            {"7004","Loader1 Tire Inner Size Code"},
            {"7005","Loader1 Loading Hoist Height (mm)"},
            {"7006","SPARE"},
            {"7007","Loader1 Unloading S/T No"},
            {"7008","Loader1 Unloading Hoist Height (mm)"},
            {"7009","SPARE"},
            {"7010","SPARE"},
            {"7011","SPARE"},
            {"7012.0","SPARE"},
            {"7012.1","SPARE"},
            {"7012.2","SPARE"},
            {"7012.3","SPARE"},
            {"7012.4","SPARE"},
            {"7012.5","SPARE"},
            {"7012.6","SPARE"},
            {"7012.7","SPARE"},
            {"7012.8","SPARE"},
            {"7012.9","SPARE"},
            {"7012.10","SPARE"},
            {"7012.11","SPARE"},
            {"7012.12","SPARE"},
            {"7012.13","SPARE"},
            {"7012.14","SPARE"},
            {"7012.15","SPARE"},
            {"7013","SPARE"},
            {"7014","SPARE"},
            {"7015","SPARE"},
            {"7016","SPARE"},
            {"7017","SPARE"},
            {"7018","SPARE"},
            {"7019","SPARE"},
            {"7020","SPARE"},
            {"7021","SPARE"},
            {"7022","SPARE"},
            {"7023","SPARE"},
            {"7024","SPARE"},
            {"7025.0","Heart beat"},
            {"7025.1","SPARE"},
            {"7025.2","Alarm Reset"},
            {"7025.3","All Data Clear"},
            {"7025.4","Loader1 Data Clear"},
            {"7025.5","SPARE"},
            {"7025.6","Time Sync Request"},
            {"7025.7","SPARE"},
            {"7025.8","EMS Data Report OK"},
            {"7025.9","SPARE"},
            {"7025.10","EMS Auto Request"},
            {"7025.11","EMS Manual Request"},
            {"7025.12","EMS Dest Change Request"},
            {"7025.13","SPARE"},
            {"7025.14","EMS Cycle Stop"},
            {"7025.15","EMS Emergency Stop"},
            {"7026","Front EMS Travel Position (mm)"},
            {"7027","Front EMS Travel Position (mm)"},
            {"7028","Rear EMS Travel Position (mm)"},
            {"7029","Rear EMS Travel Position (mm)"},
            {"7030.0","SPARE"},
            {"7030.1","SPARE"},
            {"7030.2","SPARE"},
            {"7030.3","SPARE"},
            {"7030.4","SPARE"},
            {"7030.5","SPARE"},
            {"7030.6","SPARE"},
            {"7030.7","SPARE"},
            {"7030.8","SPARE"},
            {"7030.9","SPARE"},
            {"7030.10","SPARE"},
            {"7030.11","SPARE"},
            {"7030.12","SPARE"},
            {"7030.13","SPARE"},
            {"7030.14","SPARE"},
            {"7030.15","SPARE"},
            {"7031","Time Sync Year"},
            {"7032","Time Sync Month"},
            {"7033","Time Sync Day"},
            {"7034","Time Sync Hour"},
            {"7035","Time Sync Minute"},
            {"7036","Time Sync Second"},
            {"7037","Time Sync day of the week"},
            {"7500.0","EMS Job Request"},
            {"7500.1","Loader1 Job Exist"},
            {"7500.2","SPARE"},
            {"7500.3","EMS Busy"},
            {"7500.4","Loader1 Goods Detect"},
            {"7500.5","SPARE"},
            {"7500.6","Loader1 Job Complete"},
            {"7500.7","SPARE"},
            {"7500.8","SPARE"},
            {"7500.9","SPARE"},
            {"7500.10","EMS Alarm"},
            {"7500.11","EMS Recoverable Alarm"},
            {"7500.12","SPARE"},
            {"7500.13","EMS Move Only Mode Status"},
            {"7500.14","SPARE"},
            {"7500.15","EMS Command Change Enable"},
            {"7501","Main Alarm Code"},
            {"7502","Sub Alarm Code"},
            {"7503","EMS Current Station"},
            {"7504","SPARE"},
            {"7505","SPARE"},
            {"7506","SPARE"},
            {"7507","SPARE"},
            {"7508.0","Loader1 Down In-Position"},
            {"7508.1","Loader1 Down Moving"},
            {"7508.2","Loader1 Up In-Position"},
            {"7508.3","Loader1 Up Moving"},
            {"7508.4","Gripper1 Catch In-Position (미사용)"},
            {"7508.5","Gripper1 Catch Moving"},
            {"7508.6","Gripper1 Uncatch In-Position"},
            {"7508.7","Gripper1 Uncatch Moving"},
            {"7508.8","SPARE"},
            {"7508.9","SPARE"},
            {"7508.10","SPARE"},
            {"7508.11","SPARE"},
            {"7508.12","SPARE"},
            {"7508.13","SPARE"},
            {"7508.14","SPARE"},
            {"7508.15","SPARE"},
            {"7509.0","Travel Motor Busy"},
            {"7509.1","Loader1 Motor Busy"},
            {"7509.2","Gripper1 Motor Busy"},
            {"7509.3","SPARE"},
            {"7509.4","SPARE"},
            {"7509.5","SPARE"},
            {"7509.6","SPARE"},
            {"7509.7","SPARE"},
            {"7509.8","SPARE"},
            {"7509.9","SPARE"},
            {"7509.10","SPARE"},
            {"7509.11","SPARE"},
            {"7509.12","SPARE"},
            {"7509.13","SPARE"},
            {"7509.14","SPARE"},
            {"7509.15","SPARE"},
            {"7510","Response Code (미사용)"},
            {"7511","Current Operation Number"},
            {"7512","Alarm Operation Number"},
            {"7513","SPARE"},
            {"7514","SPARE"},
            {"7515.0","SPARE"},
            {"7515.1","Loader1 Home Position"},
            {"7515.2","Gripper1 Home Position"},
            {"7515.3","SPARE"},
            {"7515.4","Travel Dest In-Position"},
            {"7515.5","Loader1 Dest In-Position"},
            {"7515.6","Gripper1 Dest In-Position"},
            {"7515.7","SPARE"},
            {"7515.8","SPARE"},
            {"7515.9","SPARE"},
            {"7515.10","SPARE"},
            {"7515.11","SPARE"},
            {"7515.12","SPARE"},
            {"7515.13","SPARE"},
            {"7515.14","SPARE"},
            {"7515.15","SPARE"},
            {"7516","SPARE"},
            {"7517","SPARE"},
            {"7518","SPARE"},
            {"7519","SPARE"},
            {"7520","Travel Current Position (mm)"},
            {"7521","Travel Current Position (mm)"},
            {"7522","Loader1 Current Position (mm)"},
            {"7523","Loader1 Current Position (mm)"},
            {"7524","SPARE"},
            {"7525","SPARE"},
            {"7526","SPARE"},
            {"7527","SPARE"},
            {"7528","SPARE"},
            {"7529","SPARE"},
            {"7530","Travel Dest Position (mm)"},
            {"7531","Travel Dest Position (mm)"},
            {"7532","Loader1 Dest Position (mm)"},
            {"7533","Loader1 Dest Position (mm)"},
            {"7534","SPARE"},
            {"7535","SPARE"},
            {"7536","SPARE"},
            {"7537","SPARE"},
            {"7538","SPARE"},
            {"7539","SPARE"},
            {"7540","Travel Current Speed (m/min)"},
            {"7541","Travel Current Speed (m/min)"},
            {"7542","Loader1 Current Speed (m/min)"},
            {"7543","Loader1 Current Speed (m/min)"},
            {"7544","SPARE"},
            {"7545","SPARE"},
            {"7546","SPARE"},
            {"7547","SPARE"},
            {"7548","SPARE"},
            {"7549","SPARE"},
            {"7550","SPARE"},
            {"7551","SPARE"},
            {"7552","SPARE"},
            {"7553","SPARE"},
            {"7554","SPARE"},
            {"7555","SPARE"},
            {"7556","SPARE"},
            {"7557","SPARE"},
            {"7558","SPARE"},
            {"7559","SPARE"},
            {"7560","SPARE"},
            {"7561","SPARE"},
            {"7562","SPARE"},
            {"7563","SPARE"},
            {"7564","SPARE"},
            {"7565","SPARE"},
            {"7566","SPARE"},
            {"7567","SPARE"},
            {"7568","SPARE"},
            {"7569","SPARE"},
            {"7570","SPARE"},
            {"7571","SPARE"},
            {"7572","SPARE"},
            {"7573","SPARE"},
            {"7574","SPARE"},
            {"7575","SPARE"},
            {"7576","SPARE"},
            {"7577","SPARE"},
            {"7578","SPARE"},
            {"7579","SPARE"},
            {"7580","SPARE"},
            {"7581","SPARE"},
            {"7582","SPARE"},
            {"7583","SPARE"},
            {"7584","SPARE"},
            {"7585","SPARE"},
            {"7586","SPARE"},
            {"7587","SPARE"},
            {"7588","SPARE"},
            {"7589","SPARE"},
            {"7590","SPARE"},
            {"7591","SPARE"},
            {"7592","SPARE"},
            {"7593","SPARE"},
            {"7594","SPARE"},
            {"7595","SPARE"},
            {"7596","SPARE"},
            {"7597","SPARE"},
            {"7598","SPARE"},
            {"7599","SPARE"},
            {"7600.0","SPARE"},
            {"7600.1","SPARE"},
            {"7600.2","SPARE"},
            {"7600.3","SPARE"},
            {"7600.4","SPARE"},
            {"7600.5","SPARE"},
            {"7600.6","SPARE"},
            {"7600.7","SPARE"},
            {"7600.8","SPARE"},
            {"7600.9","SPARE"},
            {"7600.10","SPARE"},
            {"7600.11","SPARE"},
            {"7600.12","SPARE"},
            {"7600.13","SPARE"},
            {"7600.14","SPARE"},
            {"7600.15","SPARE"},
            {"7601","Loader1 Job Number"},
            {"7602.0","Loader1 Move Command"},
            {"7602.1","Loader1 Loading Command"},
            {"7602.2","Loader1 Unloading Command"},
            {"7602.3","Loader1 Loading & Unloading Command"},
            {"7602.4","SPARE"},
            {"7602.5","SPARE"},
            {"7602.6","SPARE"},
            {"7602.7","SPARE"},
            {"7602.8","SPARE"},
            {"7602.9","SPARE"},
            {"7602.10","SPARE"},
            {"7602.11","SPARE"},
            {"7602.12","SPARE"},
            {"7602.13","SPARE"},
            {"7602.14","Loader1 Ignore Interlock Signal"},
            {"7602.15","Loader1 Allow empty receiving/shipping"},
            {"7603","Loader1 Loading / Move S/T No"},
            {"7604","Loader1 Loading Tire Inner Size Code"},
            {"7605","Loader1 Loading Hoist Height (mm)"},
            {"7606","SPARE"},
            {"7607","Loader1 Unloading S/T No"},
            {"7608","Loader1 Unloading Hoist Height (mm)"},
            {"7609","SPARE"},
            {"7610","SPARE"},
            {"7611","SPARE"},
            {"7612.0","SPARE"},
            {"7612.1","SPARE"},
            {"7612.2","SPARE"},
            {"7612.3","SPARE"},
            {"7612.4","SPARE"},
            {"7612.5","SPARE"},
            {"7612.6","SPARE"},
            {"7612.7","SPARE"},
            {"7612.8","SPARE"},
            {"7612.9","SPARE"},
            {"7612.10","SPARE"},
            {"7612.11","SPARE"},
            {"7612.12","SPARE"},
            {"7612.13","SPARE"},
            {"7612.14","SPARE"},
            {"7612.15","SPARE"},
            {"7613","SPARE"},
            {"7614","SPARE"},
            {"7615","SPARE"},
            {"7616","SPARE"},
            {"7617","SPARE"},
            {"7618","SPARE"},
            {"7619","SPARE"},
            {"7620","SPARE"},
            {"7621","SPARE"},
            {"7622","SPARE"},
            {"7623","SPARE"},
            {"7624","SPARE"},
            {"7625.0","Heart beat"},
            {"7625.1","SPARE"},
            {"7625.2","Alarm Reset Ack"},
            {"7625.3","All Data Clear Ack"},
            {"7625.4","Loader1 Data Clear Ack"},
            {"7625.5","SPARE"},
            {"7625.6","SPARE"},
            {"7625.7","SPARE"},
            {"7625.8","EMS Data Report OK Status"},
            {"7625.9","EMS Auto Ready Status"},
            {"7625.10","EMS Auto Status"},
            {"7625.11","EMS Manual Status"},
            {"7625.12","EMS Dest Change Request Ack"},
            {"7625.13","EMS Dest Change Request Nak"},
            {"7625.14","EMS Cycle Stop Ack"},
            {"7625.15","EMS Emergency Stop Ack"},
            {"7626","SPARE"},
            {"7627","SPARE"},
            {"7628","SPARE"},
            {"7629","SPARE"},
            {"7630.0","Loader1 Manual Job Complete"},
            {"7630.1","SPARE"},
            {"7630.2","SPARE"},
            {"7630.3","SPARE"},
            {"7630.4","Loader1 Manual Data Clear"},
            {"7630.5","SPARE"},
            {"7630.6","SPARE"},
            {"7630.7","SPARE"},
            {"7630.8","SPARE"},
            {"7630.9","SPARE"},
            {"7630.10","SPARE"},
            {"7630.11","SPARE"},
            {"7630.12","SPARE"},
            {"7630.13","SPARE"},
            {"7630.14","SPARE"},
            {"7630.15","SPARE"},
            {"7631","Time Sync Year"},
            {"7632","Time Sync Month"},
            {"7633","Time Sync Day"},
            {"7634","Time Sync Hour"},
            {"7635","Time Sync Minute"},
            {"7636","Time Sync Second"},
            {"7637","Time Sync day of the week"}
        };

        //통신데이터를 화면에 찍을 때 Space를 넣어찍을지 붙여서 찍을지에 대한 옵션 설정을 위한 Data Type
        public enum TWithSpaceFlag : byte
        {
            WithSpace = 1, WithOutSpace
        }

        //byte를 Nibble 단위로 반환할 때 상위인지 하위인지를 명시하기 위한 Data Type
        public enum THiLoNibble : byte
        {
            Nibble_Hi = 1, Nibble_Low
        }

        //장치 모드값에 대한 enum (프로토콜 정의값과 일치시키셔 구조체내에 Data Type으로 사용하도록 한다)
        public enum TControlMode : byte
        {
            CtrlMode_Manual = 1, CtrlMode_Semi, CtrlMode_Auto
        }

        //다운로드는 시작-데이터-완료 단계로 구성되는데 이를 나타내는 Data Type
        public enum TDownloadMode : byte
        {
            DWMode_None, DWMode_Start, DWMode_Data, DWMode_End
        }

        //다운로드가 종료되는 이유는 성공, 무응답, Nack 응답, 재전송횟수초과, 사용자 중지 이고 이를 나타내는 Data Type
        public enum TDownloadStopReason : byte
        {
            reason_Success, reason_NoResponse, reason_NackReason, reason_ManyRepeat, reasn_UserStop
        }
    }
    #endregion



    /*! 장치 로그 처리 클래스 */
    unsafe public class DEVLogManager
    {
        public UInt16 TotalCount;
        public byte AlarmCodeType;
        public byte DataType;
        public byte SignedFlag;
        private List<VEXI_DEFS.TLogUnionRec> DataList = new List<VEXI_DEFS.TLogUnionRec>();

        public DEVLogManager(string TmpPath)
        {
        }

        public void ADDLog(VEXI_DEFS.TLogUnionRec LogItem)
        {
            DataList.Add(LogItem);
        }

        public void ClearLogList()
        {
            if (DataList != null)
            {
                DataList.Clear();
            }
        }

        ~DEVLogManager()
        {
            ClearLogList();
            DataList = null;
        }

        public int LogItemCount
        {
            get
            {
                if (DataList != null) return DataList.Count;
                else return 0;
            }
        }

        public bool LogItem(int Tmpindex, ref VEXI_DEFS.TLogUnionRec TmpLogItem)
        {
            if (DataList != null)
            {
                if (Tmpindex < DataList.Count)
                {
                    TmpLogItem = DataList[Tmpindex];
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
        
        public void LoadLogFromFile(string TmpFileName)
        {
            ClearLogList();
            
            byte TmpDataLen = 16;
            using (BinaryReader br = new BinaryReader(File.Open(TmpFileName, FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    TotalCount = br.ReadUInt16();
                    DataType = br.ReadByte();
                    if (DataType > 200)
                    {
                        DataType = (byte)(DataType - 200);
                        AlarmCodeType = 1;
                    } else
                    {
                        AlarmCodeType = 0;
                    }
                    SignedFlag = br.ReadByte();

                    switch (DataType)
                    {
                        case 0:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                            TmpDataLen = 16; // 4+ 1 + 1+ 1+ 9
                            break;
                        case 1:
                            TmpDataLen = 16 + 29;
                            break;
                        case 2:
                            TmpDataLen = 18 + 29;
                            break;
                        case 10:
                            TmpDataLen = 16 + 6;
                            break;
                        case 11:
                            TmpDataLen = 16 + 20;
                            break;
                        case 20:
                            TmpDataLen = 16 + 6;
                            break;
                        case 21:
                            TmpDataLen = 16 + 20;
                            break;
                        default: TmpDataLen = 16;
                            break;
                    }

                    byte[] Savebytes;
                    VEXI_DEFS.TLogUnionRec TmpLogItem;
                    if (TotalCount > 0)
                    {
                        Savebytes = new byte[TmpDataLen];
                        for (int i = 0; i < TotalCount; i++)
                        {
                            Savebytes = br.ReadBytes(TmpDataLen);
                            TmpLogItem = new VEXI_DEFS.TLogUnionRec();
                            Global_Class.UTIL_ByteArrayToBytePtr(Savebytes, (byte*)&TmpLogItem.LogItemHeader.LogTime, 0, 0, 4);
                            Global_Class.UTIL_ByteArrayToBytePtr(Savebytes, (byte*)&TmpLogItem.LogItemHeader.Code_1, 4, 0, 3);
                            Global_Class.UTIL_ByteArrayToBytePtr(Savebytes, (byte*)&TmpLogItem.LogRec_30, 7, 0, (TmpDataLen - 7));
                            
                            ADDLog(TmpLogItem);

                        }
                    }
                }
                finally
                {
                    br.Close();
                }
            }
        }

        public void SaveLogToFile(string TmpFileName)
        {
            byte TmpDataLen = 15;
            using (BinaryWriter br = new BinaryWriter(File.Open(TmpFileName, FileMode.Create, FileAccess.Write)))
            {
                try
                {
                    br.Seek(0, SeekOrigin.Begin);
                    br.Write((UInt16)DataList.Count); //2byte

                    if (AlarmCodeType == 1)
                    {
                        if (DataType < 200)
                        {
                            br.Write((byte)(DataType + 200));   //1byte
                        } else
                        {
                            br.Write(DataType);   //1byte
                        }
                    }
                    else
                    {
                        br.Write(DataType);   //1byte
                    }
                    br.Write(SignedFlag); //1byte

                    byte[] HeaderSavebytes;
                    byte[] BodySavebytes;
                    switch (DataType)
                    {
                        case 0:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                            TmpDataLen = 9;
                            break;
                        case 1:
                            TmpDataLen = 9 + 29;
                            break;
                        case 2:
                            TmpDataLen = 11 + 29;
                            break;
                        case 10:
                            TmpDataLen = 15;
                            break;
                        case 11:
                            TmpDataLen = 9 + 20;
                            break;
                        case 20:
                            TmpDataLen = 15;
                            break;
                        case 21:
                            TmpDataLen = 9 + 20;
                            break;
                        default:
                            TmpDataLen = 9;
                            break;
                    }
                    HeaderSavebytes = new byte[7];
                    BodySavebytes   = new byte[TmpDataLen];
                    VEXI_DEFS.TLogUnionRec LogItem;
                    for (int i = 0; i < DataList.Count; i++)
                    {
                        LogItem = DataList[i];
                        Global_Class.UTIL_StructObjectToByteArray(LogItem.LogItemHeader, HeaderSavebytes);
                        switch (DataType)
                        {
                            //case 0:
                            //case 10:
                            case 30:
                            case 31:
                            case 32:
                            case 33:
                            case 34:
                            case 35:
                            case 36:
                            case 37:
                                Global_Class.UTIL_StructObjectToByteArray(TmpDataLen, LogItem.LogRec_30, BodySavebytes);
                                break;
                            case 0:
                                Global_Class.UTIL_StructObjectToByteArray(TmpDataLen, LogItem.LogSRM00Rec, BodySavebytes);
                                break;
                            case 1:
                                Global_Class.UTIL_StructObjectToByteArray(TmpDataLen, LogItem.LogSRM01Rec, BodySavebytes);
                                break;
                            case 2:
                                Global_Class.UTIL_StructObjectToByteArray(TmpDataLen, LogItem.LogSRM02Rec, BodySavebytes);
                                break;
                            case 10:
                                Global_Class.UTIL_StructObjectToByteArray(TmpDataLen, LogItem.LogRTVRec, BodySavebytes);
                                break;
                            case 11:
                                Global_Class.UTIL_StructObjectToByteArray(TmpDataLen, LogItem.LogRTVIORec, BodySavebytes);
                                break;
                            case 20:
                                Global_Class.UTIL_StructObjectToByteArray(TmpDataLen, LogItem.LogEMSRec, BodySavebytes);
                                break;
                            case 21:
                                Global_Class.UTIL_StructObjectToByteArray(TmpDataLen, LogItem.LogEMSIORec, BodySavebytes);
                                break;
                            default:
                                Global_Class.UTIL_StructObjectToByteArray(TmpDataLen, LogItem.LogRec_30, BodySavebytes);
                                break;
                        }
                        

                        br.Write(HeaderSavebytes);
                        br.Write(BodySavebytes);
                    }
                }
                finally
                {
                    br.Close();
                }
            }
        }

        public void LoadDebugFromFile(string TmpFileName)
        {
            ClearLogList();

            byte TmpDataLen = 4 + 32;
            using (BinaryReader br = new BinaryReader(File.Open(TmpFileName, FileMode.Open, FileAccess.Read)))
            {
                try
                {
                    TotalCount = br.ReadUInt16();
                    DataType = br.ReadByte();

                    switch (DataType)
                    {
                        case 1:
                        case 2:
                        case 3:
                            TmpDataLen = 4 + 32;
                            break;
                        default:
                            TmpDataLen = 4 + 32;
                            break;
                    }

                    byte[] Savebytes;
                    VEXI_DEFS.TLogUnionRec TmpLogItem;
                    if (TotalCount > 0)
                    {
                        Savebytes = new byte[TmpDataLen];
                        for (int i = 0; i < TotalCount; i++)
                        {
                            Savebytes = br.ReadBytes(TmpDataLen);
                            TmpLogItem = new VEXI_DEFS.TLogUnionRec();
                            Global_Class.UTIL_ByteArrayToBytePtr(Savebytes, (byte*)&TmpLogItem.DebugItem_1.LogTime, 0, 0, TmpDataLen);
                            ADDLog(TmpLogItem);
                        }
                    }
                }
                finally
                {
                    br.Close();
                }
            }
        }

        public void SaveDebuugToFile(string TmpFileName)
        {
            byte TmpDataLen = 4 + 32;
            using (BinaryWriter br = new BinaryWriter(File.Open(TmpFileName, FileMode.Create, FileAccess.Write)))
            {
                try
                {
                    br.Seek(0, SeekOrigin.Begin);
                    br.Write((UInt16)DataList.Count); //2byte
                    br.Write(DataType);   //1byte

                    byte[] BodySavebytes;
                    switch (DataType)
                    {
                        case 1:
                        case 2:
                        case 3:
                            TmpDataLen = 4 + 32;
                            break;
                        default:
                            TmpDataLen = 4 + 32;
                            break;
                    }
                    BodySavebytes = new byte[TmpDataLen];
                    VEXI_DEFS.TLogUnionRec LogItem;
                    for (int i = 0; i < DataList.Count; i++)
                    {
                        LogItem = DataList[i];
                        switch (DataType)
                        {
                            //case 0:
                            //case 10:
                            case 1:
                            case 2:
                            case 3:
                                Global_Class.UTIL_StructObjectToByteArray(TmpDataLen, LogItem.DebugItem_1, BodySavebytes);
                                break;
                            default:
                                Global_Class.UTIL_StructObjectToByteArray(TmpDataLen, LogItem.DebugItem_1, BodySavebytes);
                                break;
                        }

                        br.Write(BodySavebytes);
                    }
                }
                finally
                {
                    br.Close();
                }
            }
        }

    }



    /*! 통신 데이터 및 기능 처리를 위한 각종 구조체 정의 클래스 */
    //[VEXI_DEFS]
    //프로토콜 데이터 관련 구조체, List 선언 (패킷 관련 구조체는 uCommClass.cs에 선언함)
    //고정버퍼, 포인트 사용으로 unsafe로 선언
    unsafe public class VEXI_DEFS
    {
        #region  로그 구조체
        /*!
         * 프로토콜 "0x003X 알람로그" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLogItemCellRec
        {
            public byte Station;
            public byte Row;
            public UInt16 Bay;
            public byte Level;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_SRM_00
        {
            public TLogItemCellRec ItemCell;
            public byte Work1_Type;
            public byte Work1_Step;
            public byte Work2_Type;
            public byte Work2_Step;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_SRM_01
        {
            public TLOGType_SRM_00 Log;

            public fixed byte DIO[29];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_SRM_02
        {
            public TLogItemCellRec ItemCell;
            public byte Work1_Type;
            public byte Work1_Step;
            public UInt32 Work1_JobNumber;

            public UInt32 Position;
            public Int16 Speed;

            public UInt16 MainCode;
            public UInt16 SubCode;
            public UInt16 FBCode;

            public fixed byte DIO[29];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_RTV_Basic
        {
            public byte Station;
            public byte Reserved_Station;
            public byte Position;
            public byte Reserved_Position;
            public byte WorkCode;
            public byte Work1_Type;
            public byte Work1_Step;
            public byte Work2_Type;
            public byte Work2_Step;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_RTV_10
        {
            public TLOGType_RTV_Basic Log;

            public UInt32 PositionMM;
            public Int16 Speed;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_RTV_11
        {
            public TLOGType_RTV_Basic Log;

            public fixed byte DIO[22];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_EMS_Basic
        {
            public UInt32 WorkNum;
            public UInt16 FromStation;
            public UInt16 FromPos;
            public UInt16 ToStation;
            public UInt16 ToPos;
            public byte WorkCode;
            public byte Work_St;
            public byte Work_Step;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_EMS_20
        {
            public TLOGType_EMS_Basic Log;

            public byte ChuckingW;
            public byte CargoH;
            public byte DriveInv_MainCode;
            public byte DriveInv_SubCode;
            public UInt32 Drive_Postion;
            public Int16 Drive_Speed;
            public byte LiftInv_MainCode;
            public byte LiftInv_SubCode;
            public UInt32 Lift_Postion;
            public Int16 Lift_Speed;

        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_EMS_21
        {
            public TLOGType_EMS_Basic Log;

            public byte InvertorErr_Main;
            public byte InvertorErr_Sub;
            public fixed byte DIO[20];
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_30
        {
            public byte Logvalue_0;
            public byte Logvalue_1;
            public byte Logvalue_2;
            public byte Logvalue_3;
            public byte Logvalue_4;
            public byte Logvalue_5;
            public byte Logvalue_6;
            public byte Logvalue_7;
            public byte Reserved;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_31
        {
            public byte Logvalue_0;
            public byte Logvalue_1;
            public byte Logvalue_2;
            public byte Logvalue_3;
            public byte Logvalue_4;
            public Int32 Logvalue_5;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_32
        {
            public byte Logvalue_0;
            public byte Logvalue_1;
            public byte Logvalue_2;
            public Int16 Logvalue_3;
            public Int32 Logvalue_4;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_33
        {
            public byte Logvalue_0;
            public byte Logvalue_1;
            public byte Logvalue_2;
            public Int16 Logvalue_3;
            public Int16 Logvalue_4;
            public Int16 Logvalue_5;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_34
        {
            public byte Logvalue_0;
            public Int16 Logvalue_1;
            public Int16 Logvalue_2;
            public Int16 Logvalue_3;
            public Int16 Logvalue_4;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_35
        {
            public byte Logvalue_0;
            public Int16 Logvalue_1;
            public Int16 Logvalue_2;
            public Int32 Logvalue_3;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_36
        {
            public byte Logvalue_0;
            public Int32 Logvalue_1;
            public Int32 Logvalue_2;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGType_37
        {
            public byte Logvalue_0;
            public byte Logvalue_1;
            public byte Logvalue_2;
            public byte Logvalue_3;
            public byte Logvalue_4;
            public Int16 Logvalue_5;
            public Int16 Logvalue_6;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDebugLogItem_1
        {
            public UInt32 LogTime;
            public byte Debug_1;
            public byte Debug_2;
            public byte Debug_3;
            public sbyte Debug_4;
            public byte Debug_5;
            public byte Debug_6;
            public byte Debug_7;
            public byte Debug_8;
            public UInt16 Debug_9;
            public Int32 Debug_10;
            public Int32 Debug_11;
            public byte Debug_12;
            public byte Debug_13;
            public byte Debug_14;
            public sbyte Debug_15;
            public Int16 Debug_16;
            public Int16 Debug_17;
            public UInt32 Debug_18;
            public Int32 Debug_19;
            public Int32 Debug_20;
            public Int64 Debug_21;
            public Int64 Debug_22;
            public Int64 Debug_23;
            public Int64 Debug_24;
            public Int64 Debug_25;
            public Int64 Debug_26;
            public Int64 Debug_27;
            public Int64 Debug_28;
            public Int64 Debug_29;
            public Int64 Debug_30;
            public Int64 Debug_31;
            public Int64 Debug_32;
            public Int64 Debug_33;
            public Int64 Debug_34;
            public Int64 Debug_35;
            public Int64 Debug_36;
            public Int64 Debug_37;
            public Int64 Debug_38;
            public Int64 Debug_39;
            public Int64 Debug_40;
            public Int64 Debug_41;
            public Int64 Debug_42;
            public Int64 Debug_43;
            public Int64 Debug_44;
            public Int64 Debug_45;
            public Int64 Debug_46;
            public Int64 Debug_47;
            public Int64 Debug_48;
            public Int64 Debug_49;
            public Int64 Debug_50;
            public Int64 Debug_51;
            public Int64 Debug_52;
            public Int64 Debug_53;
            public Int64 Debug_54;
            public Int64 Debug_55;
            public Int64 Debug_56;
            public Int32 Debug_57;
            public Int32 Debug_58;
            public sbyte Debug_59;
            public sbyte Debug_60;
            public sbyte Debug_61;
            public sbyte Debug_62;
            public sbyte Debug_63;
            public sbyte Debug_64;
            public sbyte Debug_65;
            public sbyte Debug_66;
            public sbyte Debug_67;
            public Int16 Debug_68;
            public Int16 Debug_69;
            public Int16 Debug_70;
            public Int32 Debug_71;
            public Int32 Debug_72;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDebugLogItem_2
        {
            public UInt32 LogTime;

            public byte Debug_1;
            public Int16 Debug_2;
            public Int16 Debug_3;
            public Int32 Debug_4;
            public Int32 Debug_5;
            public Int32 Debug_6;
            public UInt16 Debug_7;
            public Int32 Debug_8;
            public UInt16 Debug_9;
            public UInt16 Debug_10;
            public byte Debug_11;
            public byte Debug_12;
            public byte Debug_13;
            public byte Debug_14;
            public byte Debug_15;

            public byte Debug_16;
            public byte Debug_17;
            public byte Debug_18;
            public byte Debug_19;

            public Int16 Debug_20;
            public Int16 Debug_21;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TTESTRecord
        {
            public double a;
            public double b;
            public double c;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TLOGItemHeader
        {
            public UInt32 LogTime;
            public byte Code_1;
            public byte Code_2;
            public byte Code_3;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct TLogUnionRec
        {
            [FieldOffset(0)] public TLOGItemHeader LogItemHeader;
            [FieldOffset(0)] public TDebugLogItem_1 DebugItem_1;
            [FieldOffset(0)] public TDebugLogItem_2 DebugItem_2;


            [FieldOffset(7)] public TLOGType_SRM_00 LogSRM00Rec;
            [FieldOffset(7)] public TLOGType_SRM_01 LogSRM01Rec;
            [FieldOffset(7)] public TLOGType_SRM_02 LogSRM02Rec;

            [FieldOffset(7)] public TLOGType_RTV_10 LogRTVRec;
            [FieldOffset(7)] public TLOGType_RTV_11 LogRTVIORec;

            [FieldOffset(7)] public TLOGType_EMS_20 LogEMSRec;
            [FieldOffset(7)] public TLOGType_EMS_21 LogEMSIORec;

            [FieldOffset(7)] public TLOGType_30 LogRec_30;
            [FieldOffset(7)] public TLOGType_31 LogRec_31;
            [FieldOffset(7)] public TLOGType_32 LogRec_32;
            [FieldOffset(7)] public TLOGType_33 LogRec_33;
            [FieldOffset(7)] public TLOGType_34 LogRec_34;
            [FieldOffset(7)] public TLOGType_35 LogRec_35;
            [FieldOffset(7)] public TLOGType_36 LogRec_36;
            [FieldOffset(7)] public TLOGType_37 LogRec_37;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_LogReq
        {
            public byte LogKind;
            public byte ReqKind;
            public UInt16 LogIndex;
            public UInt32 Req_StartTime;
            public UInt32 Req_EndTime;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_DebugReq
        {
            public byte Reserved;
            public byte ReqKind;
            public UInt16 LogIndex;
            public UInt32 Req_StartTime;
            public UInt32 Req_EndTime;
            public UInt16 WantLogCount;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_LogHeader
        {
            public UInt16 TotalLogCount;
            public UInt16 LogIndex;
            public byte LogCount;
            public byte DataType;
            public byte signedFlag;
        }
        #endregion

        #region  장치 그래프 데이터 구조체
        /*!
         * 프로토콜 "0x0115 그래프 데이터 조회" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_GraphReq
        {
            public byte ReqCommand_Type;
            public byte Graph_Type;
            public byte Graph_Reserved;
            public byte Save_Type;
            public byte Save_interval;
            public UInt32 Save_Position;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_Graph
        {
            public byte Graph_Type;
            public byte Graph_Reserved;
            public byte Save_Type;
            public byte Save_interval;
            public UInt32 Save_Position;

            //public fixed Int16 Data[700];
            public fixed Int32 Data[350];
        }
        #endregion

        #region  출력 수동제어 구조체
        /*!
         * 프로토콜 "0x0116 출력 수동 제어" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_DO_TEST
        {
            public byte CtrlFlag;
            public byte OutMode_ID;
            public byte OutMode_Value;
            public byte OutCtrl_ID;
            public byte OutCtrl_Value;
        }
        #endregion

        #region  서브 구조체
        /*!
         * 서브 구조체는 데이터를 그룹화 하기 위한 목적으로 사용한다
         * 서브 구조체를 사용하여 데이터를 정의하면 소스의 가독성도 높아지고 여러 CMD에 쓰이는 데이터 그룹인 경우에는 CMD 구조체 선언시 생산성도 높아진다
         * 모든 데이터를 그룹화 할 필요는 없다. 의미상으로 필요하거나 여러 CMD에 반복적으로 쓰이는 데이터에 대해서 그룹화를 한다.
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TErrorCodeRec
        {
            public byte MainCode;
            public byte SubCode;
            public UInt16 PosCode;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRMPositionCMDRec
        {
            public byte Station;
            public byte Row;
            public UInt16 BayID;
            public byte LevelID;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRMPositionCellRec
        {
            public byte Station;
            public UInt16 BayID;
            public byte LevelID;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TUnsignedPositionDistanceRec
        {
            public byte St_1;
            public byte St_2;
            public fixed byte Reserved_1[2];
            public UInt32 Now_Position;
            public UInt16 Speed;
            public UInt32 Dest_Position;
            public fixed byte Reserved_2[2];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRMForksignedPositionDistanceRec
        {
            public byte St_1;
            public byte St_2;
            //public fixed byte Reserved_1[2];
            public byte HaveItemType;
            public byte ItemExist;
            public Int32 Now_Position;
            public Int16 Now_Speed;
            public Int32 Dest_Position;
            public Int16 Dest_Speed;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTVFeedsignedPositionDistanceRec
        {
            public byte St_1;
            public byte St_2;
            public fixed byte Reserved_1[6];
            public Int16 Now_Speed;
            public fixed byte Reserved_2[4];
            public Int16 Dest_Speed;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRMDriveLiftsignedPositionDistanceRec
        {
            public byte St_1;
            public byte St_2;
            public byte DecelNo_1;
            public byte DecelNo_2;
            public Int32 Now_Position;
            public Int16 Now_Speed;
            public Int32 Dest_Position;
            public Int16 Dest_Speed;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTVDrivesignedPositionDistanceRec
        {
            public byte St_1;
            public byte St_2;
            public byte Reserved_1;
            public byte Reserved_2;
            public Int32 Now_Position;
            public Int16 Now_Speed;
            public Int32 Dest_Position;
            public Int16 Dest_Speed;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMSDrivesignedPositionDistanceRec
        {
            public byte St_1;
            public byte St_2;
            public Int32 Now_Position;
            public Int16 Now_Speed;
            public UInt32 Dest_Position;
            public Int16 Dest_Speed;
            public byte AlarmCode1;
            public byte AlarmCode2;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMSCageStRec
        {
            public byte St_1;
            public byte St_2;
            public byte ActionCode;
            public byte MoveTargetNo;
            public byte St_3;
            public byte Reserved_4;
            public byte Reserved_5;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTVDriveAreaInfoRec
        {
            public byte AreaNo;
            public byte FrontRegionSt;
            public byte Area_Type;
            public UInt32 Start_MM;
            public UInt32 End_MM;
            public UInt16 MaxSpeed;
            public byte PrevAreaIndex;
            public byte NextAreaIndex;
            public byte Sensorindex;
            public byte FrontRegion;

            public UInt16 Coll_Stop;
            public UInt16 Coll_Start;

            public byte RearRegion;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMSDriveAreaInfoRec
        {
            public byte AreaNo;
            public byte RegionSt;
            public byte Area_Type;
            public UInt32 Start_MM;
            public UInt32 End_MM;
            public UInt16 MaxSpeed;
            public byte PrevAreaIndex;
            public byte NextAreaIndex;
            public byte Sensorindex;
            public byte Region;

            public UInt16 Coll_Stop;
            public UInt16 Coll_Start;

            public fixed byte Reserved[10];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTVWCS14byteRec
        {
            public UInt16 StatusRx_Count;
            public UInt16 ControlRx_Count;
            public byte ST_1;
            public byte ST_2;
            public byte CTRL_1;
            public byte CTRL_2;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRMForkPositionRec
        {
            public TSRMPositionCellRec PositionCellRec;
            public sbyte Fork;
            public byte CurrentPosition_1;
            public byte CurrentPosition_2;
            public byte RowID;
            public byte Reserved;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTVPointRec
        {
            public byte Station;
            public byte Station_Reserved;
            public byte Position;
            public byte Position_Reserved;
            //public byte Reserved;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTVFeedPositionRec
        {
            public TRTVPointRec PointRec;
            public byte CurrentPosition;
            public byte StationInfo;
            public fixed byte Reserved[2];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMSPointRec
        {
            public byte Station;
            public byte Station_Reserved;
            public byte Position;
            public byte Position_Reserved;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMSDrivePositionRec
        {
            public TEMSPointRec PointRec;
            public byte CurrentPosition;
            public byte StationInfo;
            public fixed byte Reserved[2];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTVCollisionStRec
        {
            public UInt16 RxTime;
            public byte RTV_ID;
            public byte PostionType;
            public UInt32 GapOtherCar;
            public UInt16 StopDistance;
            public UInt16 StartDistance;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMSCollisionStRec
        {
            public UInt16 RxTime;
            public byte EMS_ID;
            public byte PostionType;
            public UInt32 GapOtherCar;
            public UInt16 StopDistance;
            public UInt16 StartDistance;
            public byte DriveSt;
            public Int16 Speed;
            public byte NowPosition;
            public byte DestPosition;

        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRMJobStatusRec
        {
            public UInt32 Item_JobNumber;
            //public byte taskIndex;
            public byte ItemType;
            public TSRMPositionCMDRec Item_From;
            public TSRMPositionCMDRec Item_To;
            public byte Item_CMD_Code;
            public byte Item_Do_Status;
            public byte Item_Do_Step;

            public UInt32 Move_JobNumber;
            public TSRMPositionCMDRec Move_To;
            public byte Move_Do_Status;
            public byte Move_Do_Step;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTVJobStatusRec
        {
            public UInt32 Item_JobNumber;
            public byte taskIndex;
            public TRTVPointRec Item_From;
            public TRTVPointRec Item_To;
            public byte Item_CMD_Code;
            public byte Item_Do_Status;
            public byte Item_Do_Step;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMSJobStatusRec
        {
            public UInt32 Item_JobNumber;
            //public byte taskIndex;
            public TEMSPointRec Item_From;
            public TEMSPointRec Item_To;
            public byte Item_CMD_Code;
            public byte Item_Do_Status;
            public byte Item_Do_Step;
            public byte Chucking_Width;
            public UInt16 Loading_height;
            public UInt16 UnLoading_height;
            public byte CmdOption;
            public fixed byte Reserved_1[4];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMSMoveStatusRec
        {
            public UInt32 Item_JobNumber;
            //public byte taskIndex;
            public TEMSPointRec Item_To;
            public byte Item_CMD_Code;
            public byte Item_Do_Status;
            public byte Item_Do_Step;
            public fixed byte Reserved_1[10];
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_TaskJobItem
        {
            public byte Cmd;
            public byte Reserved1;
            public byte Fork;
            public TSRMPositionCMDRec To;
            public byte itemType;
            public byte WorkStatus;// 상태에서만 유효한 필드. 제어시에는 Reserved 임
            public byte Reserved2;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_TaskJobItem
        {
            public byte Cmd;
            public byte Reserved1;
            public byte Feed;
            public TRTVPointRec To;
            public byte Reserved2;
            public byte WorkStatus;// 상태에서만 유효한 필드. 제어시에는 Reserved 임
            public byte Reserved3;
            public byte Reserved4;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_TaskJobItem
        {
            public byte Cmd;
            public byte Reserved1;
            public byte Feed;
            public TEMSPointRec To;
            public byte Reserved2;
            public byte WorkStatus;// 상태에서만 유효한 필드. 제어시에는 Reserved 임
            public byte Reserved3;
            public byte Reserved4;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDSPInstallInfoRec
        {
            public byte Install_Type;
            public byte Install_Count;
            public fixed byte DSP1_IP[4];
            public fixed byte DSP1_Reserved[2];
            public fixed byte DSP2_IP[4];
            public fixed byte DSP2_Reserved[2];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_REC_Interlock
        {
            public byte StationIndex;
            public byte Reserved;
            public byte Data;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_REC_Interlock
        {
            public byte CVToEMS;
            public byte EMSToCV;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_CellPosition_Header
        {
            public byte RackType;
            public UInt16 BayCount;
            public UInt16 LevelCount;
            public byte DataType;
            public byte StartNo;
            public byte EndNo;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_PositionParam_Header
        {
            public byte RailType;
            public byte Barcode_Direct;
            public UInt32 Barcode_Ref;
            public fixed byte Reserved1[10];
            public UInt32 Ref_Start;
            public UInt32 Ref_End;

            public byte Home_SpeedType;
            public UInt32 Home_Position;
            public byte Service_SpeedType;
            public UInt32 Service_Position;

            public fixed byte Reserved2[20];


            public byte PositionCount;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_PositionParam_Header
        {
            public byte RailType;
            public byte Barcode_Direct;
            public UInt32 Barcode_Ref;
            public fixed byte Reserved1[10];
            public UInt32 Ref_Start;
            public UInt32 Ref_End;

            public byte Home_SpeedType;
            public byte Home_Position_1;
            public byte Home_Position_2;
            public byte Home_Position_3;
            public byte Home_Position_4;
            public fixed byte Reserved2[20];

            public byte Service_SpeedType;
            public UInt32 Service_Position;
            public fixed byte Reserved3[20];


            public byte PositionCount;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_CellOffset_Header
        {
            public byte DevType;
            public UInt16 TotalCount;
            public UInt16 Nowindex;
            public byte ItemCount;

        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_StationConfigHeaderRec
        {
            public byte stationCount;
            public byte InterlockType;
            public fixed byte Reserved[16];
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_StationParamHeaderRec
        {
            public byte InterlockType;
            public byte InterlockSensorDirect;
            public byte Accept_ManualInterlock;

            public fixed byte Reserved1[10];

            public byte stationCount;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_SpeedAreaParamHeaderRec
        {
            public UInt16 RTV_Width;
            public UInt16 LineArea_StopWidth;
            public UInt16 LineArea_StartWidth;
            public UInt16 RoundArea_StopWidth;
            public UInt16 RoundArea_StartWidth;
            public byte FrontRTVPosition_TimeOut1;
            public byte FrontRTVPosition_TimeOut2;
            public UInt16 StopbyLiadrTimeOut;
            public UInt16 Stop_OffsetTime;
            public UInt16 Stop_OffsetMaxDistance;
            public byte Lidar_Off_SafetyTime;
            public byte DriveStopSensor;
            public byte DriveDecelSensor;
            public byte ErrRef_LowSpeedValue;
            public UInt16 ErrRef_LowSpeedMin;

            public byte LidarSensorInstall;
            public fixed byte Reserved1[5];

            public byte AreaCount;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_StationParamHeaderRec
        {
            public byte InterlockType;
            public byte Reserved1;

            public fixed byte Reserved2[10];

            public byte stationCount;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_SpeedAreaParamHeaderRec
        {
            public UInt16 EMS_Width;
            public UInt16 LineArea_StopWidth;
            public UInt16 LineArea_StartWidth;
            public UInt16 RoundArea_StopWidth;
            public UInt16 RoundArea_StartWidth;
            public byte FrontEMSPosition_TimeOut1;
            public byte FrontEMSPosition_TimeOut2;
            public UInt16 StopbyLiadrTimeOut;
            public UInt16 Stop_OffsetTime;
            public UInt16 Stop_OffsetMaxDistance;
            public byte Lidar_Off_ContinueTime;
            public byte LidarSensorInstall;
            public byte DriveDecelSensor_Reserved;
            public byte ErrRef_LowSpeedValue;
            public UInt16 ErrRef_LowSpeedMin;

            public fixed byte Reserved1[6];

            public byte AreaCount;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TNetworkInfoRec
        {
            public fixed byte DevIP[4];
            public fixed byte DevSubnet[4];
            public fixed byte DevGateway[4];
            public fixed byte DevMacAddr[6];
        }
        #endregion

        #region SRM 상태 조회 / 응답 구조체
        /*!
         * 프로토콜 "0x0030 SRM 상태 조회" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_REC_StatusReq
        {
            public byte IsIgnore;
            public fixed byte Reserved_1[5];
            public UInt32 SystemUTCTime;
            public ConstClass.TControlMode ControllerMode;
            public byte ControllerSt;
            public fixed byte CV_Interlock[8];
            public fixed byte Dev_Interlock[8];
            //public fixed byte HMIData[54];
            public fixed byte Reserved[5];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_StatusRes
        {
            public byte Reserved_1;
            public byte Reserved_2;
            public byte PGVersion;
            public fixed byte FWversion[4];
            public UInt32 SystemUTCTime;

            public fixed byte ProjectID[6];
            public byte GroupID;
            public UInt16 HogiID;

            public UInt16 DevDetailtype;
            public ConstClass.TControlMode ControllerMode;

            public byte ControllerSt;
            public fixed byte CV_Interlock[8];
            public fixed byte Dev_Interlock[8];

            public byte DevMode;
            public byte DevSt_1;
            public byte DevSt_2;
            public byte ActionCode;
            public TErrorCodeRec ErrorCode;

            public TSRMForkPositionRec Fork1_Position;
            public TSRMForkPositionRec Fork2_Position;

            public TSRMPositionCMDRec Fork1_DestCell;
            public fixed byte Reserved_5[2];
            public TSRMPositionCMDRec Fork2_DestCell;
            public fixed byte Reserved_6[2];

            //public byte loafactor;
            //public fixed byte Reserved_7[11];
            public fixed byte HMI_IP[4];
            public byte HMI_Status;
            public UInt16 HMI_Button1;
            public UInt32 HMI_Button2;

            public TSRMDriveLiftsignedPositionDistanceRec Drive_DisPosition;
            public TSRMDriveLiftsignedPositionDistanceRec Updown_DisPosition;
            public TSRMForksignedPositionDistanceRec Fork1_DisPosition;
            public TSRMForksignedPositionDistanceRec Fork2_DisPosition;

            public TSRMJobStatusRec FF1_Job;
            public TSRMJobStatusRec FF2_Job;


            public UInt32 Task_JobNumber;
            public byte Task_JobStatus;
            public byte Reserved_8;
            //구조체에 대한 고정배열 선언이 안됨
            //가변배열로 해서 new로 생성은 가능하지만 그렇게 하면 구조체에 대한 주소, 크기 접근이 안되고.
            //TSRM_TaskJobItem 사이즈가 변경되면 TSRM_TaskJobItem 배열 크기로 변경되어야 한다
            public TSRM_TaskJobItem TaskJobItem_1;
            public fixed byte otherTaskJobItem[19 * 11]; //TaskJobItem_2 ~ TaskJobItem_20 은 멤버변수로 아니고 포인터로 접근할 것이기 때문에 할당만 해준다

            //IO는 추후에 상세 정의
            public fixed byte IO_Digital_IN[19];
            public fixed byte IO_Reserved_1[1];
            public fixed byte IO_Digital_OUT[7];
            public fixed byte IO_Reserved_2[2];


            public fixed byte IO_Digital_OUTMode_1[5];
            public byte InvArr_M_Drive;
            public byte InvArr_S_Drive;
            public byte InvArr_M_Lift;
            public byte InvArr_S_Lift;
            public byte InvArr_M_Fork1;
            public byte InvArr_S_Fork1;
            public byte InvArr_M_Fork2;
            public byte InvArr_S_Fork2;

            public fixed byte IO_Digital_OUTMode_2[4];
            public fixed byte IO_Reserved_8[7];

            public UInt32 SI_1;
            public UInt32 SI_2;
            public UInt32 SI_3;
            public UInt32 SI_4;
            public UInt32 SI_5;
            public UInt32 SI_6;
            public UInt32 SI_7;
            public UInt32 SI_8;
            public UInt32 SI_9;
            public UInt32 SI_10;
            public UInt32 SI_11;
            public UInt32 SI_12;
            public UInt32 SI_13;
            public UInt32 SI_14;
            public UInt32 SI_15;
            public UInt32 SI_16;
            public UInt16 SI_17;
            public UInt16 SI_18;
            public UInt16 SI_19;
            public UInt16 SI_20;
            public UInt16 SI_21;
            public UInt16 SI_22;
            public UInt16 SI_23;
            public UInt16 SI_24;
            public UInt16 SI_25;
            public UInt16 SI_26;
            public UInt16 SI_27;
            public UInt16 SI_28;
            public UInt16 SI_29;
            public UInt16 SI_30;
            public UInt16 SI_31;
            public UInt16 SI_32;
            public UInt32 SI_33;
            public UInt32 SI_34;
            public UInt32 SI_35;
            public UInt32 SI_36;
            public UInt32 SI_37;
            public UInt32 SI_38;
            public UInt32 SI_39;
            public UInt32 SI_40;
            public UInt32 SI_41;
            public UInt32 SI_42;
            public UInt32 SI_43;
            public UInt32 SI_44;
            public UInt32 SI_45;

        }
        #endregion

        #region RTV 상태 조회 / 응답 구조체
        /*!
         * 프로토콜 "0x0030 RTV 상태 조회" 참조
         */

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_REC_StatusReq
        {
            public byte IsIgnore;
            public UInt32 SystemUTCTime;
            public byte Reserved1;
            public byte ControllerSt;
            //구조체에 대한 고정배열 선언이 안됨
            //가변배열로 해서 new로 생성은 가능하지만 그렇게 하면 구조체에 대한 주소, 크기 접근이 안되고.
            //TRTV_REC_Interlock 사이즈가 변경되면 TRTV_REC_Interlock 배열 크기로 변경되어야 한다
            public TRTV_REC_Interlock CV_Interlock;
            public fixed byte OtherCV_Interlock[3 * 3];
            public TRTV_REC_Interlock Dev_Interlock;
            public fixed byte OtherDev_Interlock[3 * 3];
            public fixed byte Reserved2[5];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_StatusRes
        {
            public byte Reserved_1;
            public byte Reserved_2;
            public byte PGVersion;
            public fixed byte FWversion[4];
            public UInt32 SystemUTCTime;

            public fixed byte ProjectID[6];
            public byte GroupID;
            public UInt16 HogiID;

            public byte RailType;
            public byte AlarmCodeType;
            public byte Reserved_3;

            public byte ControllerSt;
            public TRTV_REC_Interlock CV_Interlock;
            public fixed byte OtherCV_Interlock[3 * 3];
            public TRTV_REC_Interlock Dev_Interlock;
            public fixed byte OtherDev_Interlock[3 * 3];

            public byte DevMode;
            public byte DevSt_1;
            public byte DevSt_2;
            public byte ActionCode;
            public TErrorCodeRec ErrorCode;

            public TRTVFeedPositionRec Feed1_Position;
            public TRTVFeedPositionRec Feed2_Position;

            public TRTVPointRec Feed1_Dest;
            public TRTVPointRec Feed2_Dest;

            public TRTVPointRec CanWorkStation;

            public TRTVDriveAreaInfoRec DriveAreaInfo;

            public TRTVDrivesignedPositionDistanceRec Drive_DisPosition;
            public TRTVFeedsignedPositionDistanceRec Feed1_DisPosition;
            public TRTVFeedsignedPositionDistanceRec Feed2_DisPosition;

            public TRTVJobStatusRec FF1_Job;
            public TRTVJobStatusRec FF2_Job;


            public UInt32 Task_JobNumber;
            public byte Task_JobStatus;
            public byte Reserved_8;
            //구조체에 대한 고정배열 선언이 안됨
            //가변배열로 해서 new로 생성은 가능하지만 그렇게 하면 구조체에 대한 주소, 크기 접근이 안되고.
            //TRTV_TaskJobItem 사이즈가 변경되면 TRTV_TaskJobItem 배열 크기로 변경되어야 한다
            public TRTV_TaskJobItem TaskJobItem_1;
            public fixed byte otherTaskJobItem[19 * 11]; //TaskJobItem_2 ~ TaskJobItem_20 은 멤버변수로 아니고 포인터로 접근할 것이기 때문에 할당만 해준다

            //IO는 추후에 상세 정의
            public fixed byte IO_Digital_IN[12];
            public fixed byte IO_Digital_OUT[10];
            public fixed byte IO_Digital_OUTMode_1[8];

            public byte KEYIN_St_1;
            public byte KEYIN_St_2;
            public byte KEYIN_St_3;
            public byte Reserved_9;

            public TRTVWCS14byteRec RTVWCS14byte;


            public UInt16 BarcodeErrCount;

            public TRTVCollisionStRec BeforeCar_Collision;
            public TRTVCollisionStRec AfterCar_Collision;

            public fixed byte IO_Digital_OUTMode_2[2];
            public fixed byte Reserved_10[4];

            public byte RearRegion;
            public fixed byte Reserved_11[3];

        }
        #endregion

        #region EMS 상태 조회 / 응답 구조체
        /*!
         * 프로토콜 "0x0030 EMS 상태 조회" 참조
         */

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_REC_StatusReq
        {
            public byte IsIgnore;
            public UInt32 SystemUTCTime;
            public byte Reserved1;
            public byte ControllerSt;
            //구조체에 대한 고정배열 선언이 안됨
            //가변배열로 해서 new로 생성은 가능하지만 그렇게 하면 구조체에 대한 주소, 크기 접근이 안되고.
            //TRTV_REC_Interlock 사이즈가 변경되면 TRTV_REC_Interlock 배열 크기로 변경되어야 한다
            //public TEMS_REC_Interlock CV_Interlock;
            //public fixed byte OtherCV_Interlock[3 * 3];
            //public TEMS_REC_Interlock Dev_Interlock;
            //public fixed byte OtherDev_Interlock[3 * 3];

            public fixed byte Reserved2[24];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_StatusRes
        {
            public byte Reserved_1;
            public byte Reserved_2;
            public byte PGVersion;
            public fixed byte FWversion[4];
            public UInt32 SystemUTCTime;

            public fixed byte ProjectID[6];
            public byte GroupID;
            public UInt16 HogiID;

            public byte RailType;
            public fixed byte Reserved_3[2];

            public byte ControllerSt;
            //public TEMS_REC_Interlock CV_Interlock;
            //public fixed byte OtherCV_Interlock[3 * 3];
            //public TEMS_REC_Interlock Dev_Interlock;
            //public fixed byte OtherDev_Interlock[3 * 3];
            public fixed byte Reserved2[24];

            public byte DevMode;
            public byte DevSt_1;
            public byte DevSt_2;
            public byte ActionCode;
            public TErrorCodeRec ErrorCode;
            public fixed byte Reserved_12[4];

            public TEMSDrivePositionRec Drive_Position;

            public TEMSPointRec Drive_Dest;

            public TEMSPointRec CanWorkStation;

            public TEMSDriveAreaInfoRec DriveAreaInfo;

            public TEMSDrivesignedPositionDistanceRec Drive_DisPosition;
            public TEMSDrivesignedPositionDistanceRec Lift_DisPosition;
            public TEMSCageStRec Cage_St;

            public TEMSJobStatusRec Work_Job;
            public TEMSMoveStatusRec Move_Job;


            //public UInt32 Task_JobNumber;
            //public byte Task_JobStatus;
            //public byte Reserved_8;
            //구조체에 대한 고정배열 선언이 안됨
            //가변배열로 해서 new로 생성은 가능하지만 그렇게 하면 구조체에 대한 주소, 크기 접근이 안되고.
            //TEMS_TaskJobItem 사이즈가 변경되면 TEMS_TaskJobItem 배열 크기로 변경되어야 한다
            //public TEMS_TaskJobItem TaskJobItem_1;
            //public fixed byte otherTaskJobItem[19 * 11]; //TaskJobItem_2 ~ TaskJobItem_20 은 멤버변수로 아니고 포인터로 접근할 것이기 때문에 할당만 해준다

            //IO는 추후에 상세 정의
            public fixed byte IO_Digital_IN[10];
            public fixed byte IO_Digital_IN_Reserved[10];
            public fixed byte IO_Digital_OUT[10];
            public fixed byte IO_Digital_OUT_Reserved[10];

            public fixed byte IO_Digital_OUTMode[10];
            public fixed byte IO_Digital_OUTMode_Reserved[5];

            public byte KEYIN_St_1;
            public byte KEYIN_St_2;
            public byte KEYIN_St_3;

            public byte PositionSensorSt;
            public byte PositionSensorErrorNum;
            public UInt16 PositionSensorErrorCount;
            
            public TEMSCollisionStRec BeforeCar_Collision;
            public TEMSCollisionStRec AfterCar_Collision;

            public UInt16 invetorst2_DriveToke;
            public UInt16 invetorst2_LiftToke;
            public fixed byte invetorst2_Reserved[20];

            public TEMS_REC_Interlock InterlockData;
            public fixed byte OtherCV_Interlock[2 * 49];

            public byte front_LidarArea;
            public byte front_LidarSt;
            public byte Rear_LidarArea;
            public byte Rear_LidarSt;
            public fixed byte Reserved_10[6];

        }

        #endregion

        #region SRM 장치 구조 구조체
        /*!
         * 프로토콜 "0x0125 장치 구조 조회_SRM" 참조
         * 프로토콜 "0x0126 장치 구조 제어_SRM" 참조
         * 모든 CMD에 대한 구조체를 모두 정의할 필요는 없다.
         * CMD의 데이터가 단순한 경우 (byte 데이터 1~2개 정도이거나 Reserved 데이터만 있는 경우) 구조체 정의를 생략하기도 한다.
         * 단, 이런 경우에도 구조체를 선언해두면 데이터 사이즈를 참조할 수 있는 장점도 있고 데이터가 변경되었을 때 수정이 편한 면이 있다.
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_DevConfigReq
        {
            public fixed byte Reserved[20];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_DevConfigRes
        {
            public byte ForkCount;
            public byte MoveDriveCount;
            public byte MoveDriveType;
            public byte ForkDriveType;
            public byte InvertorType;
            public byte ForkInvertor;
            public byte ForkSensor;
            public byte MovePositionSensor;
            public byte UpdownPositionSensor;
            public byte LampType;
            public byte ForkEncoderType;
            public byte ModeSwitchUse;
            public fixed byte Reserved[13];

            public TDSPInstallInfoRec DSPInstallInfoRec;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_DevConfigCtrl
        {
            public fixed byte CtrlFlag[3];
            public TSRM_DevConfigRes Data;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_CtrlRes_2Byte
        {
            public byte CtrlResult;
            public byte NackReason;
        }
        #endregion

        #region RTV 장치 구조 구조체
        /*!
        * 조회 요청 구조체는 TDEV_REC_DevConfigReq 를 사용한다. 
        */


        /*! 서브클래스 */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_INVERTORType
        {
            public byte WheelCount;
            public byte FeedType;
            public byte InvertorCount;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_DevConfigRes
        {
            public byte RTVsType;
            public byte InvetorKind;
            public byte MovePositionSensor;

            public TRTV_INVERTORType InvertorType;
            public byte Front_LampType;
            public byte Rear_LampType;

            public byte ModeSwitchUse;
            public fixed byte Reserved3[16];
            public TDSPInstallInfoRec DSPInstallInfoRec;

            
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_DevConfigCtrl
        {
            public fixed byte CtrlFlag[3];
            public TRTV_DevConfigRes Data;
        }

        /*!
        * 제어 응답 구조체는 TDEV_CtrlRes_2Byte 를 사용한다. 
        */
        #endregion

        #region EMS 장치 구조 구조체
        /*!
        * 조회 요청 구조체는 TDEV_REC_DevConfigReq 를 사용한다. 
        */

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_DevConfigRes
        {
            public byte EMSsType;
            public byte InvetorKind;
            public byte MovePositionSensor;
            public byte CageType;
            public byte LampType;
            public byte LiftType;

            public byte ModeSwitchUse;
            public fixed byte Reserved1[18];

            public TDSPInstallInfoRec DSPInstallInfoRec;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_DevConfigCtrl
        {
            public fixed byte CtrlFlag[3];
            public TEMS_DevConfigRes Data;
        }

        /*!
        * 제어 응답 구조체는 TDEV_CtrlRes_2Byte 를 사용한다. 
        */
        #endregion

        #region SRM 인버터 파라미터 설정 구조체
        /*!
         * 프로토콜 "0x0090_0x0091_인버터 파라미터" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_InvertorParamReq
        {
            public byte DevType;
            public byte Invertor_Index;
            public ushort Addr_Main;
            public ushort Addr_Sub;
        }

        /*!
         * 조회 응답, 설정 동일 구조체 사용 (프로토콜이 동일함)
         */
        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct TSRM_InvertorParamResCtrl
        {
            [FieldOffset(0)] public byte DevType;
            [FieldOffset(1)] public byte Invertor_Index;
            [FieldOffset(2)] public ushort Addr_Main;
            [FieldOffset(4)] public ushort Addr_Sub;
            [FieldOffset(6)] public UInt32 U_Value;
            [FieldOffset(6)] public Int32 S_Value;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_InvertorParamCtrlRes
        {
            public byte DevType;
            public byte Invertor_Index;
            public byte CtrlResult;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_CtrlRes_3Byte
        {
            public byte CtrlResult;
            public byte NackItem;
            public byte NackReason;
        }
        #endregion

        #region SRM 랙설정 구조체
        /*!
         * 프로토콜 "0x0094_랙설정 조회" 참조
         */

        /*!
         * 조회는 구조체 선언 안하고 byte 배열로 처리함 
         * Form_SRMRack private void Request_CellPosition(byte DataType, byte Startindex, byte Endindex) 참조
         */

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_CellPositionRES
        {
            public TSRM_CellPosition_Header Header;
            public fixed int Position[256];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_CellPositionCTRL
        {
            public TSRM_CellPosition_Header Header;
            public fixed int Position[256];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_CellPositionCTRLRes
        {
            public TSRM_CellPosition_Header Header;
            public byte CtrlResult;
            public byte NackReason;
        }
        #endregion

        #region RTV 주행레일 설정 구조체
        /*!
         * 프로토콜 "0x0094_레일 주행 설정 조회" 참조
         * 프로토콜 "0x0095_레일 주행 설정 변경" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_PositionSetParamReq
        {
            public fixed byte Reserved[10];
        }

        /*!
         * 조회 응답, 설정 동일 구조체 사용 (프로토콜이 동일함)
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_PositionSetParam
        {
            public TRTV_PositionParam_Header Header;
            public fixed UInt32 Position[200];
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_PositionSetCTRLRes
        {
            public byte Count;
            public fixed byte Reserved[4];
            public byte CtrlResult;
            public byte NackReason;
        }
        #endregion

        #region EMS 주행레일 설정 구조체
        /*!
         * 프로토콜 "0x0094_레일 주행 설정 조회" 참조
         * 프로토콜 "0x0095_레일 주행 설정 변경" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_PositionSetParamReq
        {
            public fixed byte Reserved[10];
        }

        /*!
         * 조회 응답, 설정 동일 구조체 사용 (프로토콜이 동일함)
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_PositionSetParam
        {
            public TEMS_PositionParam_Header Header;
            //public fixed UInt32 Position[200];
            public fixed UInt32 Position[100];
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_PositionSetCTRLRes
        {
            public byte Count;
            public fixed byte Reserved[4];
            public byte CtrlResult;
            public byte NackReason;
        }
        #endregion
        #region SRM Cell Offset 설정 구조체
        /*!
         * 프로토콜 "0x0096_셀오프셋 조회" 참조
         * 프로토콜 "0x0097_셀오프셋 변경" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_CellOffsetREQ
        {
            public byte DevType;
            public UInt16 ReqIndex;
        }

        /*!
        * 서브 구조체
        */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_CellOffsetRec
        {
            public byte Bay;
            public byte Level;
            public sbyte Left_Travel_Offset;
            public sbyte Left_Lift_Offset;
            public sbyte Left_Fork_Offset;
            public sbyte Right_Travel_Offset;
            public sbyte Right_Lift_Offset;
            public sbyte Right_Fork_Offset;
        }

        /*!
         * 조회 응답, 설정 동일 구조체 사용 (프로토콜이 동일함)
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_CellOffset
        {
            public TSRM_CellOffset_Header Header;
            public TSRM_CellOffsetRec SRM_CellOffsetRec;
            //구조체에 대한 고정배열 선언이 안됨
            //public TSRM_CellOffsetRec SRM_CellOffsetRec[255];  <= 안됨
            //구조체에 대한 Sizeof가 여기서는 안됨
            //public fixed byte OtherCellOffsetItem[254 * Sizeof(TSRM_CellOffsetRec)]; <= 안됨
            //가변배열로 해서 new로 생성은 가능하지만 그렇게 하면 구조체에 대한 주소, 크기 접근이 안되고.
            //TSRM_CellOffsetRec 사이즈가 변경되면 TSRM_CellOffsetRec 배열 크기로 변경되어야 한다
            public fixed byte OtherCellOffsetItem[254 * 8];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_CellOffsetCTRLRes
        {
            public byte DevType;
            public UInt16 TotalCount;
            public UInt16 Startindex;
            public byte TxCount;
            public byte CtrlResult;
            public byte NackReason;
        }
        #endregion

        #region SRM Station 설정 구조체
        /*!
         * 프로토콜 "0x0098_스테이션 설정 조회" 참조
         * 프로토콜 "0x0099_스테이션 설정 변경" 참조
         */

        /*!
         * 조회는 데이터가 없으므로 구조체 선언 안함
         */

        /*!
        * 서브 구조체
        */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_StationConfigRec
        {
            public byte station_Type;
            public byte Item_Type;
            public Int32 Travel;
            public Int32 Lift;
            public Int16 ForkDepth;
            public Byte  LevelUp_Offset;
            public SByte LevelDn_Offset;
            public byte interLockNo;
            public byte Delay_Time;
            public byte UseIsExistItem;
            public byte OutinterLockNo;
            public fixed byte Reserved[2];
        }

        /*!
         * 조회 응답, 설정 동일 구조체 사용 (프로토콜이 동일함)
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_StationParam
        {
            public TSRM_StationConfigHeaderRec Header;

            public TSRM_StationConfigRec Station1;
            //구조체에 대한 고정배열 선언이 안됨
            //public TSRM_StationConfigRec StationRec[50];  <= 안됨
            //구조체에 대한 Sizeof가 여기서는 안됨
            //public fixed byte OtherStationItem[49 * Sizeof(TSRM_StationConfigRec)]; <= 안됨
            //가변배열로 해서 new로 생성은 가능하지만 그렇게 하면 구조체에 대한 주소, 크기 접근이 안되고.
            //TSRM_StationConfigRec 사이즈가 변경되면 TSRM_StationConfigRec 배열 크기로 변경되어야 한다
            public fixed byte OtherStationItem[49 * 20];
        }

        /*!
         * 설정 응답은 TDEV_CtrlRes_3Byte 구조체 사용
         */
        #endregion

        #region RTV Station 설정 구조체
        /*!
         * 프로토콜 "0x0098_스테이션 정보 조회_1" 참조
         * 프로토콜 "0x0099_스테이션 설정 변경_1" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_StationConfigReq
        {
            public fixed byte Reserved[10];
        }

        /*!
        * 서브 구조체
        */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_StationConfigRec
        {
            public byte station_Type;
            public UInt16 P_ID;
            public UInt16 InterlockCommID;
        }

        /*!
         * 조회 응답, 설정 동일 구조체 사용 (프로토콜이 동일함)
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_StationParam
        {
            public TRTV_StationParamHeaderRec Header;

            public TRTV_StationConfigRec Station1;
            //구조체에 대한 고정배열 선언이 안됨
            //public TRTV_StationConfigRec StationRec[200];  <= 안됨
            //구조체에 대한 Sizeof가 여기서는 안됨
            //public fixed byte OtherStationItem[199 * Sizeof(TRTV_StationConfigRec)]; <= 안됨
            //가변배열로 해서 new로 생성은 가능하지만 그렇게 하면 구조체에 대한 주소, 크기 접근이 안되고.
            //TRTV_StationConfigRec 사이즈가 변경되면 TRTV_StationConfigRec 배열 크기로 변경되어야 한다
            public fixed byte OtherStationItem[199 * 5];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_StationParamCTRLRes
        {
            public byte AreaCount;
            public byte CtrlResult;
            public byte FailNo;
            public byte Resreved1;
            public byte NackReason;
        }
        #endregion

        #region EMS Station 설정 구조체
        /*!
         * 프로토콜 "0x0098_스테이션 정보 조회_1" 참조
         * 프로토콜 "0x0099_스테이션 설정 변경_1" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_StationConfigReq
        {
            public fixed byte Reserved[10];
        }

        /*!
        * 서브 구조체
        */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_StationConfigRec
        {
            public byte station_Type;
            public UInt16 P_ID;
            public UInt16 LIFT_Position;

            public byte Loading_Crip1;
            public byte Loading_Crip2;
            public byte Loading_Crip3;
            public byte Loading_Crip4;
            public byte UnLoading_Crip1;
            public byte UnLoading_Crip2;
            public byte UnLoading_Crip3;
            public byte UnLoading_Crip4;
            public byte SudaeDown_Crip;
        }

        /*!
         * 조회 응답, 설정 동일 구조체 사용 (프로토콜이 동일함)
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_StationParam
        {
            public TEMS_StationParamHeaderRec Header;

            public TEMS_StationConfigRec Station1;
            //구조체에 대한 고정배열 선언이 안됨
            //public TRTV_StationConfigRec StationRec[200];  <= 안됨
            //구조체에 대한 Sizeof가 여기서는 안됨
            //public fixed byte OtherStationItem[99 * Sizeof(TRTV_StationConfigRec)]; <= 안됨
            //가변배열로 해서 new로 생성은 가능하지만 그렇게 하면 구조체에 대한 주소, 크기 접근이 안되고.
            //TRTV_StationConfigRec 사이즈가 변경되면 TRTV_StationConfigRec 배열 크기로 변경되어야 한다
            public fixed byte OtherStationItem[99 * 14];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_StationParamCTRLRes
        {
            public byte AreaCount;
            public byte CtrlResult;
            public byte FailNo;
            public byte Resreved1;
            public byte NackReason;
        }
        #endregion

        #region RTV 구간 설정 구조체
        /*!
         * 프로토콜 "0x009A_구간 주행 설정 조회" 참조
         * 프로토콜 "0x009B_구간 주행 설정 변경" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_SpeedAreaGroupConfigReq
        {
            public fixed byte Reserved[10];
        }

        /*!
        * 서브 구조체
        */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_SpeedAreaGroupConfigRec
        {
            public byte Area_Type;
            public UInt32 Start_MM;
            public UInt32 End_MM;
            public UInt16 MaxSpeed;
            public byte PrevAreaIndex;
            public byte NextAreaIndex;
            public byte Sensorindex;
            public byte FrontRegion;
            public UInt16 StopDistance;
            public UInt16 StartDistance;


            public byte RearRegion;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_SpeedAreaGroupParam
        {
            public TRTV_SpeedAreaParamHeaderRec Header;


            public TRTV_SpeedAreaGroupConfigRec Area1;
            //구조체에 대한 고정배열 선언이 안됨
            //public TRTV_SpeedAreaGroupConfigRec AreaRec[50];  <= 안됨
            //구조체에 대한 Sizeof가 여기서는 안됨
            //public fixed byte OtherSpeedAreaItem[199 * Sizeof(TRTV_SpeedAreaGroupConfigRec)]; <= 안됨
            //가변배열로 해서 new로 생성은 가능하지만 그렇게 하면 구조체에 대한 주소, 크기 접근이 안되고.
            //TRTV_SpeedAreaGroupConfigRec 사이즈가 변경되면 TRTV_SpeedAreaGroupConfigRec 배열 크기로 변경되어야 한다
            public fixed byte OtherSpeedAreaItem[49 * 20];
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_SpeedAreaGroupCTRLRes
        {
            public byte AreaCount;
            public byte CtrlResult;
            public byte FailNo;
            public byte Resreved1;
            public byte NackReason;
        }
        #endregion

        #region EMS 구간 설정 구조체
        /*!
         * 프로토콜 "0x009A_구간 주행 설정 조회" 참조
         * 프로토콜 "0x009B_구간 주행 설정 변경" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_SpeedAreaGroupConfigReq
        {
            public fixed byte Reserved[10];
        }

        /*!
        * 서브 구조체
        */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_SpeedAreaGroupConfigRec
        {
            public byte Area_Type;
            public UInt32 Start_MM;
            public UInt32 End_MM;
            public UInt16 MaxSpeed;
            public byte PrevAreaIndex;
            public byte NextAreaIndex;
            public byte Sensorindex;
            public byte Region;
            public UInt16 StopDistance;
            public UInt16 StartDistance;

            public byte Region2_CtrlType;
            public byte Region3_CtrlType;

            public fixed byte Reserved[3];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_SpeedAreaGroupParam
        {
            public TEMS_SpeedAreaParamHeaderRec Header;


            public TEMS_SpeedAreaGroupConfigRec Area1;
            //구조체에 대한 고정배열 선언이 안됨
            //public TRTV_SpeedAreaGroupConfigRec AreaRec[50];  <= 안됨
            //구조체에 대한 Sizeof가 여기서는 안됨
            //public fixed byte OtherSpeedAreaItem[199 * Sizeof(TRTV_SpeedAreaGroupConfigRec)]; <= 안됨
            //가변배열로 해서 new로 생성은 가능하지만 그렇게 하면 구조체에 대한 주소, 크기 접근이 안되고.
            //TRTV_SpeedAreaGroupConfigRec 사이즈가 변경되면 TRTV_SpeedAreaGroupConfigRec 배열 크기로 변경되어야 한다
            public fixed byte OtherSpeedAreaItem[39 * 24];
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_SpeedAreaGroupCTRLRes
        {
            public byte AreaCount;
            public byte CtrlResult;
            public byte FailNo;
            public byte Resreved1;
            public byte NackReason;
        }
        #endregion

        #region SRM 금지랙 설정 구조체
        /*!
         * 프로토콜 "0x009C_금지랙 설정 조회" 참조
         * 프로토콜 "0x009D_금지랙 설정 변경" 참조
         */

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_REC_NoUseRackReq
        {
            public fixed byte Reserved[20];
        }

        /*!
        * 서브 구조체
        */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_NoUseRackItem
        {
            public byte RowID; //0,1~
            public UInt16 BayID; //0,1~
            public byte Level_ID; //0,1~
            public byte Reserved;
        }

        /*!
         * 조회 응답, 설정 동일 구조체 사용 (프로토콜이 동일함)
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_NoUseRack
        {
            public byte SetCount;
            public byte Reserved_1;
            public TSRM_NoUseRackItem NoUseRack1;
            //구조체에 대한 고정배열 선언이 안됨
            //public TSRM_NoUseRackItem NoUseRack[100];  <= 안됨
            //구조체에 대한 Sizeof가 여기서는 안됨
            //public fixed byte OtherNoUseRack[99 * Sizeof(TSRM_NoUseRackItem)]; <= 안됨
            //가변배열로 해서 new로 생성은 가능하지만 그렇게 하면 구조체에 대한 주소, 크기 접근이 안되고.
            //TSRM_NoUseRackItem 사이즈가 변경되면 TSRM_NoUseRackItem 배열 크기로 변경되어야 한다
            public fixed byte OtherNoUseRack[99 * 5];
        }

        /*!
        * 제어 응답 구조체는 TDEV_CtrlRes_2Byte 를 사용한다. 
        */
        #endregion

        #region SRM 스페셜랙 설정 구조체
        /*!
         * 프로토콜 "00x009E_스페셜랙 설정 조회" 참조
         * 프로토콜 "0x009F_스페셜랙 설정 변경" 참조
         */

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_REC_SpecialRackReq
        {
            public fixed byte Reserved[20];
        }

        /*!
        * 서브 구조체
        */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_SpecialRackItem
        {
            public byte ItemType;
            //public byte Reserved_1;
            public byte Row; //1~
            public UInt16 BayID; //1~
            public byte Level_ID; //1~
            public fixed byte Reserved[4];
        }

        /*!
         * 조회 응답, 설정 동일 구조체 사용 (프로토콜이 동일함)
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_SpecialRack
        {
            public byte SetCount;
            public byte Reserved;
            public TSRM_SpecialRackItem SpecialRack1;
            //구조체에 대한 고정배열 선언이 안됨
            //public TSRM_SpecialRackItem SpecialRack[100];  <= 안됨
            //구조체에 대한 Sizeof가 여기서는 안됨
            //public fixed byte OtherSpecialRack[99 * Sizeof(TSRM_SpecialRackItem)]; <= 안됨
            //가변배열로 해서 new로 생성은 가능하지만 그렇게 하면 구조체에 대한 주소, 크기 접근이 안되고.
            //TSRM_SpecialRackItem 사이즈가 변경되면 TSRM_SpecialRackItem 배열 크기로 변경되어야 한다
            public fixed byte OtherSpecialRack[99 * 9];
        }
        /*!
        * 제어 응답 구조체는 TDEV_CtrlRes_2Byte 를 사용한다. 
        */
        #endregion

        #region SRM 인버터 정보 구조체
        /*!
         * 프로토콜 "0x0032 인버터_정보" 참조
         */

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_REC_InvertorReq
        {
            public fixed byte Reserved[20];
        }

        /*!
        * 서브 구조체
        */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_InvertorStRec
        {
            public byte St;
            public byte St_Fault;
            public Int32 St_Position;
            public Int16 St_Speed;
            public Int16 St_torque;
            public UInt16 St_MainErrCode;
            public UInt16 St_SubErrCode;
            public UInt16 St_BlockErrCode;
            public Int32 St_Output_Current;
            public Int32 St_DCLink_Volt;
            public Int32 St_radiation_Temp;
            public Int32 St_Motor_Temp;
            public UInt32 St_Motor_Encoder;
            public UInt32 St_Exteranl_Encoder;
            public UInt32 St_Sum_energy_consumed;
            public UInt32 St_Sum_regenerative_energy;


            public UInt32 St_Actual_Reference_offset;
            public UInt32 St_Actual_Position_gain;
            public UInt16 St_Actual_Position_window;

            public fixed byte Reserved[30];
        }

        /*!
        * 서브 구조체
        */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_InvertorCtrlRec
        {
            public UInt16 Ctrl;
            public Int32 Ctrl_Position;
            public Int16 Ctrl_Speed;
            public Int16 Ctrl_Accel;
            public Int16 Ctrl_Decel;
            public Int16 Ctrl_JerkTime;

            public UInt32 Ctrl_Reference_offset;
            public UInt32 Ctrl_Position_gain;
            public UInt16 Ctrl_Position_window;

            public fixed byte Reserved[30];

        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_REC_InvertorRes
        {
            public TSRM_InvertorStRec Invertor_1_St;
            //TSRM_InvertorStRec 사이즈가 변경되면 TSRM_InvertorStRec 배열 크기로 변경되어야 한다 (5 x 사이즈)
            public fixed byte otherInvertir_St[5 * 88];
            public TSRM_InvertorCtrlRec Invertor_1_Ctrl;
            //TSRM_InvertorCtrlRec 사이즈가 변경되면 TSRM_InvertorCtrlRec 배열 크기로 변경되어야 한다 (5 x 사이즈)
            public fixed byte otherInvertir_Ctrl[5 * 54];
        }
        #endregion

        #region 운행 정보 구조체
        /*!
         * 프로토콜 "0x0031 운행정보" 참조
         */

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_OPInfoReq
        {
            public byte ReqType;
            public fixed byte Reserved[19];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_TimeInfoReq
        {
            public UInt32 StartPosition_Drive;
            public UInt32 StartPosition_UpDown;
            public UInt32 FromPosition_Drive;
            public UInt32 FromPosition_UpDown;
            public UInt32 ToPosition_Drive;
            public UInt32 ToPosition_UpDown;
            public fixed byte Reserved[20];

            public UInt32 Time_1;
            public UInt32 Time_2;
            public UInt32 Time_3;
            public UInt32 Time_4;
            public UInt32 Time_5;
            public UInt32 Time_6;
            public UInt32 Time_7;
            public UInt32 Time_8;
            public UInt32 Time_9;
            public UInt32 Time_10;
            public UInt32 Time_11;
            public UInt32 Time_12;
            public UInt32 Time_13;
            public UInt32 Time_14;
            public UInt32 Time_15;
            public UInt32 Time_16;
            public UInt32 Time_17;
            public UInt32 Time_18;
            public UInt32 Time_19;
            public UInt32 Time_20;
            public UInt32 Time_21;
            public UInt32 Time_22;
            public UInt32 Time_23;
            public UInt32 Time_24;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_OpInfoRes
        {
            public UInt32 DriveTime_Hour;
            public UInt32 LiftTime_Hour;
            public UInt32 Fork1Time_Hour;
            public UInt32 Fork2Time_Hour;

            public UInt32 DriveDistance_KM;
            public UInt32 LiftDistance_KM;
            public UInt32 Fork1Distance_KM;
            public UInt32 Fork2Distance_KM;

            public UInt32 DriveCount;
            public UInt32 LiftCount;
            public UInt32 Fork1Count;
            public UInt32 Fork2Count;

            public fixed byte Reserved[20];


            public TDEV_REC_TimeInfoReq Timeinfo;
        }
        #endregion

        #region 기본정보 조회/설정 구조체
        /*!
         * 프로토콜 "0x0110 기본정보 조회" 참조
         * 프로토콜 "0x0111 기본정보 제어" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_BasicStReq
        {
            public fixed byte Reserved[20];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_BasicStRes
        {
            public byte Reserved1;
            public byte PGVersion;
            public fixed byte FWversion[4];
            public UInt32 SystemUTCTime;
            public TNetworkInfoRec Network_1;
            public TNetworkInfoRec Network_2;


            public fixed byte Bootversion[4];
            public fixed byte Reserved3[32];

            public fixed byte ProjectID[6];
            public byte GroupID;
            public UInt16 HogiID;
            public fixed byte Reserved4[10];

            public byte ModeSwitch;
            public byte IDSwitch; //0x99 => 99
            public fixed byte LinkSt[4];
            public fixed byte Reserved5[29];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_BasicCtrl
        {
            public fixed byte CtrlFlag[4];
            public UInt32 SystemUTCTime;
            public TNetworkInfoRec Network_1;
            public TNetworkInfoRec Network_2;
            public TNetworkInfoRec Reserved_Network;

            public fixed byte ProjectID[6];
            public byte GroupID;
            public UInt16 HogiID;

            public fixed byte Reserved3[30];
        }

        /*! 제어응답은 처리를 안해서 구조체 선언 생략 */
        #endregion

        #region 테스트 조회/제어 구조체, 펑션 제어 구조체 [디버깅 용도의 CMD 임]
        /*!
         * 프로토콜 "0x0112 테스트제어 상태" 참조
         * 프로토콜 "00x0113 테스트제어 설정" 참조
         * 프로토콜 "0x0114 펑션 제어" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_TestStReq
        {
            public fixed byte Reserved[20];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_TestStRes
        {
            public fixed Int32 St[32];
            public fixed Int32 Ctrl[16];
            public fixed Int32 St_2[32];
            //public byte InvCount;
            //public UnionRec InvUnion;
            //public fixed byte InvSt[320];
            //public fixed byte InvCtrl[320];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_TestCtrl
        {
            public fixed byte CtrlFlag[2];
            public fixed Int32 Ctrl[16];
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_FunctionCtrl
        {
            public byte CtrlIndex;
            public Int32 CtrlValue;
        }

        /*! 제어응답은 처리를 안해서 구조체 선언 생략 */
        #endregion

        #region DI/DO 조회/제어 구조체
        /*!
         * 프로토콜 "0x0123 DIO 조회" 참조
         * 프로토콜 "0x0124 DIO 설정" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_IOConfigReq
        {
            public fixed byte Reserved[20];
        }

        /*! 서브 구조체 */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct REC_DIConfig
        {
            public byte EthercatID;
            public byte Pin;
            public byte Type;
            public byte Chattering;
            public byte Dual;
        }

        /*! 서브 구조체 */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct REC_DOConfig
        {
            public byte EthercatID;
            public byte Pin;
            public byte Type;
        }

        /*!
        * 조회 응답, 설정 동일 구조체 사용 (프로토콜이 동일함)
        */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct EMS_IOConfig
        {
            public REC_Scan_EthercatSlave Scan_EthercatSlave1;
            public fixed byte other_Scan_EthercatSlave1[9 * 2];
            public fixed byte Reserved1[10];

            public fixed byte EthercatBoard[10];
            public fixed byte Reserved2[5];

            public REC_DIConfig DIConfig;
            public fixed byte other_DIConfig[5 * 69]; // DIConfi_2 ~ DIConfi_69은 멤버변수로 아니고 포인터로 접근할 것이기 때문에 할당만 해준다

            public fixed byte Reserved3[170];

            public REC_DOConfig DOConfig;
            public fixed byte other_DOConfig[3 * 69]; // DOConfi_2 ~ DOConfi_69 은 멤버변수로 아니고 포인터로 접근할 것이기 때문에 할당만 해준다

            public fixed byte Reserved4[69];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RTV_IOConfig
        {
            public REC_Scan_EthercatSlave Scan_EthercatSlave1;
            public fixed byte other_Scan_EthercatSlave1[9 * 2];
            public fixed byte Reserved1[10];

            public fixed byte EthercatBoard[10];
            public fixed byte Reserved2[5];

            public REC_DIConfig DIConfig_1;
            //public fixed byte other_DIConfig[5 * 121]; // DIConfi_2 ~ DIConfi_122 은 멤버변수로 아니고 포인터로 접근할 것이기 때문에 할당만 해준다
            public fixed byte other_DIConfig[5 * 100]; // DIConfi_2 ~ DIConfi_122 은 멤버변수로 아니고 포인터로 접근할 것이기 때문에 할당만 해준다

            public REC_DOConfig DOConfig_64;
            public fixed byte other_DOConfig_2[3 * 34]; // DOConfi_2 ~ DOConfi_62 은 멤버변수로 아니고 포인터로 접근할 것이기 때문에 할당만 해준다

            public REC_DOConfig DOConfig_1;
            //public fixed byte other_DOConfig[3 * 53]; // DOConfi_2 ~ DOConfi_53 은 멤버변수로 아니고 포인터로 접근할 것이기 때문에 할당만 해준다
            public fixed byte other_DOConfig[3 * 62]; // DOConfi_2 ~ DOConfi_62 은 멤버변수로 아니고 포인터로 접근할 것이기 때문에 할당만 해준다
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SRM_IOConfig
        {
            public REC_Scan_EthercatSlave Scan_EthercatSlave1;
            public fixed byte other_Scan_EthercatSlave1[9 * 2];
            public fixed byte Reserved1[10];

            public fixed byte EthercatBoard[10];
            public fixed byte Reserved2[5];

            // DI 1 ~ 122 
            public REC_DIConfig DIConfig_1;
            public fixed byte other_DIConfig[5 * 121]; 

            // DO 1 ~ 43
            public REC_DOConfig DOConfig_1;
            public fixed byte other_DOConfig[3 * 42];

            // DI 123 ~ 134 
            //public REC_DIConfig DIConfig_123;
            //public fixed byte other_DIConfig_2[5 * 11]; 

            // DI 123 ~ 149
            public REC_DIConfig DIConfig_123;
            public fixed byte other_DIConfig_2[5 * 26]; 

            // DO 44 ~ 69
            public REC_DOConfig DOConfig_44;
            public fixed byte other_DOConfig_2[3 * 25]; 
        }

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct DevUnion_IOConfig
        {
            [FieldOffset(0)] public RTV_IOConfig RTVIO;
            [FieldOffset(0)] public SRM_IOConfig SRMIO;
            [FieldOffset(0)] public EMS_IOConfig EMSIO;
        }

        /*! 제어응답은 구조체 선언 생략하고 처리 함*/
        #endregion

        #region SRM 제어, 주행, 승강, 포크 파라미터 조회/제어 구조체
        /*!
         * 프로토콜 "0x00A1 제어 설정 조회" 참조
         * 프로토콜 "0x00A2 제어 설정 변경" 참조
         * 프로토콜 "0x00A3 주행 드라이브 설정 조회" 참조
         * 프로토콜 "0x00A4 주행 드라이브 설정 변경" 참조
         * 프로토콜 "0x00A5 승강 드라이브 설정 조회" 참조
         * 프로토콜 "0x00A6 승강 드라이브 설정 변경" 참조
         * 프로토콜 "0x00A7 포크 드라이브 설정 조회" 참조
         * 프로토콜 "0x00A8 포크 드라이브 설정 변경" 참조
         */

        /*! 조회 구조체는 공통 사용 */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_REC_PARAMReq
        {
            public fixed byte Reserved[20];
        }
        /*! 서브 구조체 */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRMLevelDelayTimeRec
        {
            public byte DelayTime;
            public byte Start;
            public byte End;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_CTRLParamRes
        {
            public byte SafetyPlug_ProcessType;
            public byte USE_GisangDoorSense;
            public byte AlarmUse_GisangDoorSense;
            public byte AlarmUse_OpticModem;
            public byte AutoModeChangeCheck;
            public byte DriveLift_Sequence;
            public byte NoItemToHome;
            public byte AlarmUse_ital_forking;
            public byte Reserved;
            public byte interlockAlarm_Auto;
            public byte interlockAlarm_Manual;
            public byte Forking_lift_BrakeOn_UseFlag;
            public UInt16 Forking_ReturnRef_OperCount;
            public fixed byte Reserved_1[40];

            public byte RetryInPositon_DriveCount;
            public UInt16 RetryInPositon_DriveOffset;
            public byte RetryInPositon_LiftCount;
            public UInt16 RetryInPositon_LiftOffset;
            public byte RetryInPositon_ForkCount;
            public UInt16 RetryInPositon_ForkOffset;

            public UInt16 Loading_DelayTime_beforMove;
            public UInt16 Loading_DelayTime_afterMove;
            public UInt16 Loading_DelayTime_beforForkOut;
            public UInt16 Loading_DelayTime_afterForkOut;
            public UInt16 Loading_DelayTime_beforForkUpDown;
            public UInt16 Loading_DelayTime_afterForkUpDown;
            public UInt16 Loading_DelayTime_beforForkIn;
            public UInt16 Loading_DelayTime_afterForkIn;
            public fixed byte Loading_DelayTime_Reserved[24];

            public UInt16 UnLoading_DelayTime_beforMove;
            public UInt16 UnLoading_DelayTime_afterMove;
            public UInt16 UnLoading_DelayTime_beforForkOut;
            public UInt16 UnLoading_DelayTime_afterForkOut;
            public UInt16 UnLoading_DelayTime_beforForkUpDown;
            public UInt16 UnLoading_DelayTime_afterForkUpDown;
            public UInt16 UnLoading_DelayTime_beforForkIn;
            public UInt16 UnLoading_DelayTime_afterForkIn;
            public fixed byte UnLoading_DelayTime_Reserved[24];

            public UInt16 Fork_Ref_DelayTime;
            public fixed byte Reserved_2[20];

            public UInt16 Setup_TimeOut_DriveRef;
            public UInt16 Setup_TimeOut_LiftRef;
            public UInt16 Setup_TimeOut_ForkRef;
            public fixed byte Reserved_3[34];

            public UInt16 OP_TimeOut_ManualCtrl;
            public UInt16 OP_TimeOut_GoHome;
            public fixed byte Reserved_4[36];

            public UInt16 AutoOP_TimeOut_Move;
            public UInt16 AutoOP_TimeOut_ForkOut;
            public UInt16 AutoOP_TimeOut_ForkUpDown;
            public UInt16 AutoOP_TimeOut_ForkIn;
            public UInt16 AutoOP_TimeOut_InterLock;
            public UInt16 AutoOP_TimeOut_ItemLoadUnLoad;
            public fixed byte Reserved_5[28];

            public UInt16 AutoInit_ForceMode;

            public UInt16 Fan_DoTime;
            public byte Fan_IsTempSensor;
            public fixed byte Reserved_6[10];

            public UInt16 Buzzer_Error_Time;
            public UInt16 Buzzer_Warnning_Time;
            public UInt16 Buzzer_AutoModeOn_Time;
            public UInt16 Buzzer_AutoModeOff_Time;
            public byte Buzzer_AutoMode_RepeatCount;
            public fixed byte Reserved_7[10];

            public fixed byte Reserved_8[16];

            public byte Reserved_9;

            public byte DelayTimeCount;

            public TSRMLevelDelayTimeRec LevelDelay_1_St;
            //TSRMLevelDelayTimeRec 사이즈가 변경되면 TSRMLevelDelayTimeRec 배열 크기로 변경되어야 한다 (9 x 사이즈)
            public fixed byte otherLevelDelay[9 * 3];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_CTRLParamCTRL
        {
            public fixed byte CtrlFlag[5];
            public fixed byte Reserved_0[29];

            public TSRM_CTRLParamRes ParamItemsRec;
        }

        /*! 서브 구조체 */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRMSpeedParamRec
        {
            public UInt16 Speed;
            public UInt16 Accel;
            public UInt16 Decel;
            public UInt16 jerk;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_DriveParamRes
        {
            public TSRMSpeedParamRec Speed_Auto_High;
            public TSRMSpeedParamRec Speed_Auto_Middle;
            public TSRMSpeedParamRec Speed_Auto_Low;

            public TSRMSpeedParamRec Speed_Manual_Middle;
            public TSRMSpeedParamRec Speed_Manual_Low;

            public TSRMSpeedParamRec Speed_Force;
            public TSRMSpeedParamRec Speed_Creep;
            public TSRMSpeedParamRec Speed_RefSet;
            public TSRMSpeedParamRec Speed_Emergency;
            public TSRMSpeedParamRec Speed_Auto_Decel1;
            public TSRMSpeedParamRec Speed_Auto_Decel2;

            public TSRMSpeedParamRec Speed_Reserved1;
            public TSRMSpeedParamRec Speed_Reserved2;
            public TSRMSpeedParamRec Speed_Reserved3;
            public TSRMSpeedParamRec Speed_Reserved4;
            public TSRMSpeedParamRec Speed_Reserved5;

            public byte CurrentPos_Offset;
            public byte CurrentPos_histerisis;

            public UInt16 ManualOp_TokeAlarm;
            public UInt32 ManualOp_Startmm;
            public UInt32 ManualOp_Endmm;
            public fixed byte ManualOp_Reserved[20];

            public UInt16 breakOpenContinueTime;

            public Int32 Reserved_1;

            public byte Home_SpeedType;
            public byte Home_Position_Station;
            public byte Home_Position_Bay;
            public fixed byte Home_Reserved[2];

            public byte Maintance_SpeedType;
            public UInt32 Maintance_Position;

            public byte DecelSensorOpSet_1;
            public byte DecelSensorOpSet_2;

            public byte SoftLimit_DetectSet;
            public UInt32 SoftLimit_HomePos;
            public UInt32 SoftLimit_EndPos;

            public byte SoftDecel_DetectSet;
            public UInt16 SoftDecel_Offset;
            public UInt32 SoftDecel_StopDistance_1;
            public UInt32 SoftDecel_StopDistance_2;
            public UInt32 SoftDecel_StopDistance_3;
            public UInt32 SoftDecel_StopDistance_4;

            public byte RefSetDog_DetectSet;
            public UInt16 RefSetDog_Offset;
            public UInt32 RefSetDog_Home;
            public UInt32 RefSetDog_Home_Reserved;
            public UInt32 RefSetDog_End;
            public UInt32 RefSetDog_End_Reserved;

            public byte DecelDog_DetectSet;
            public UInt16 DecelDog_Offset;
            public UInt32 DecelDog_Front1_Pos1;
            public UInt32 DecelDog_Front1_Pos2;
            public UInt32 DecelDog_Front2_Pos;
            public UInt32 DecelDog_Front2_Pos_Reserved;
            public UInt32 DecelDog_Rear1_Pos1;
            public UInt32 DecelDog_Rear1_Pos2;
            public UInt32 DecelDog_Rear2_Pos;
            public UInt32 DecelDog_Rear2_Pos_Reserved;

            public Int32 Invertor_Ref;
            public UInt32 Invertor_Gain;
            public byte Invertor_DifferLimit;
            public byte Invertor_ParamUse;

            //public fixed byte Reserved[749];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_DriveParamCTRL
        {
            public fixed byte CtrlFlag[5];
            public fixed byte Reserved_0[30];

            public TSRM_DriveParamRes ParamItemsRec;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_LiftParamRes
        {
            public TSRMSpeedParamRec Speed_Auto_High;
            public TSRMSpeedParamRec Speed_Auto_Middle;
            public TSRMSpeedParamRec Speed_Auto_Low;

            public TSRMSpeedParamRec Speed_Manual_Middle;
            public TSRMSpeedParamRec Speed_Manual_Low;

            public TSRMSpeedParamRec Speed_Force;
            public TSRMSpeedParamRec Speed_Creep;
            public TSRMSpeedParamRec Speed_RefSet;
            public TSRMSpeedParamRec Speed_Emergency;
            public TSRMSpeedParamRec Speed_Auto_Decel1;
            public TSRMSpeedParamRec Speed_Auto_Decel2;

            public TSRMSpeedParamRec Speed_AutoForkOut;
            public TSRMSpeedParamRec Speed_ManualForkOut;
            public TSRMSpeedParamRec Speed_Sticky;
            public TSRMSpeedParamRec Speed_Reserved4;
            public TSRMSpeedParamRec Speed_Reserved5;

            public byte CurrentPos_Offset;
            public byte CurrentPos_histerisis;

            public UInt16 ManualOp_TokeAlarm;
            public Int32 ManualOp_Startmm;
            public Int32 ManualOp_Endmm;
            public fixed byte ManualOp_Reserved[20];

            public UInt16 breakOpenContinueTime;

            public Int32 Reserved_1;

            public fixed byte Home_Reserved[5];

            public byte Maintance_SpeedType;
            public Int32 Maintance_Position;

            public byte DecelSensorOpSet_1;
            public byte DecelSensorOpSet_2;

            public byte SoftLimit_DetectSet;
            public Int32 SoftLimit_HomePos;
            public Int32 SoftLimit_EndPos;

            public byte SoftDecel_DetectSet;
            public UInt16 SoftDecel_Offset;
            public Int32 SoftDecel_StopDistance_1;
            public Int32 SoftDecel_StopDistance_2;
            public Int32 SoftDecel_StopDistance_3;
            public Int32 SoftDecel_StopDistance_4;

            public byte RefSetDog_DetectSet;
            public UInt16 RefSetDog_Offset;
            public Int32 RefSetDog_Home;
            public Int32 RefSetDog_Home_Reserved;
            public Int32 RefSetDog_End;
            public Int32 RefSetDog_End_Reserved;

            public byte DecelDog_DetectSet;
            public UInt16 DecelDog_Offset;
            public Int32 DecelDog_Front1_Pos1;
            public Int32 DecelDog_Front1_Pos2;
            public Int32 DecelDog_Front2_Pos;
            public Int32 DecelDog_Front2_Pos_Reserved;
            public Int32 DecelDog_Rear1_Pos1;
            public Int32 DecelDog_Rear1_Pos2;
            public Int32 DecelDog_Rear2_Pos;
            public Int32 DecelDog_Rear2_Pos_Reserved;

            public byte jerk_ForkOut_Set;
            public UInt16 jerk_ForkOut_DefTime;
            public UInt16 jerk_ForkOut_SetTime;
            public UInt16 jerk_ForkOut_GoodTime;

            public Int32 Invertor_Ref;
            public UInt32 Invertor_Gain;
            public byte Invertor_DifferLimit;
            public byte Invertor_ParamUse;

            public Byte LevelUp_Offset;
            public SByte LevelDn_Offset;

            public byte DoubleGap;

            //public fixed byte Reserved[742];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_LiftParamCTRL
        {
            public fixed byte CtrlFlag[5];
            public fixed byte Reserved_0[30];

            public TSRM_LiftParamRes ParamItemsRec;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_ForkParamRes
        {
            public TSRMSpeedParamRec Speed_Auto_High;
            public TSRMSpeedParamRec Speed_Auto_Middle;
            public TSRMSpeedParamRec Speed_Auto_Low;

            public TSRMSpeedParamRec Speed_Manual_Middle;
            public TSRMSpeedParamRec Speed_Manual_Low;

            public TSRMSpeedParamRec Speed_Force;
            public TSRMSpeedParamRec Speed_Creep;
            public TSRMSpeedParamRec Speed_RefSet;
            public TSRMSpeedParamRec Speed_Emergency;
            public TSRMSpeedParamRec Speed_Auto_Decel1;
            public TSRMSpeedParamRec Speed_Auto_Decel2;

            public TSRMSpeedParamRec Speed_Reserved1;
            public TSRMSpeedParamRec Speed_Reserved2;
            public TSRMSpeedParamRec Speed_Reserved3;
            public TSRMSpeedParamRec Speed_Reserved4;
            public TSRMSpeedParamRec Speed_Reserved5;

            public byte CurrentPos_Offset;
            public byte CurrentPos_histerisis;

            public UInt16 ManualOp_TokeAlarm;
            public Int32 ManualOp_Leftmm;
            public UInt32 ManualOp_Rightmm;
            public byte ManualOp_Creepmm;
            public byte MotorDirection;
            public fixed byte ManualOp_Reserved[19];

            public byte Encoder_Direct;
            public byte Encoder_InputPulse;
            public UInt32 Encoder_Preset;
            public UInt32 Encoder_Pulsebee;

            public byte RefPositionOffset_FCL;
            public byte RefPositionOffset_FCR;
            public byte RefPositionType;
            public byte Reserved_2;

            public byte AutoForkDecel_Set;
            public fixed Int16 AutoForkDecel_pos[24];

            public UInt16 TwinFork_Gap;
            public fixed byte TwinFork_Reserved[20];

            public byte ForkCurrenPosition_Define;
            public fixed byte ForkCurrenPosition_Define_Reserved[2];

            public Int16 FHL;
            public Int16 FML;
            public Int16 FEL;
            public Int16 FHR;
            public Int16 FMR;
            public Int16 FER;

            public Int32 Invertor_Ref;
            public UInt32 Invertor_Gain;
            public byte Invertor_DifferLimit;
            public byte Invertor_ParamUse;

            //public fixed byte Reserved[748];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_ForkParamCTRL
        {
            public fixed byte CtrlFlag[5];
            public fixed byte Reserved_0[30];

            public TSRM_ForkParamRes ParamItemsRec;
        }

        /*! 제어응답은 구조체 선언 생략하고 처리 함*/
        #endregion

        #region RTV 인버터 정보 구조체
        /*!
         * 프로토콜 "0x0032 인버터_정보" 참조
         */

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMSRTV_REC_InvertorReq
        {
            public fixed byte Reserved[20];
        }

        /*!
        * 서브 구조체
        */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_ABBInvertorStRec
        {
            public UInt16 St_bit;
            public Int16 Value_1;
            public UInt16 Value_2;
            public UInt16 Value_3;
            public UInt16 Value_4;
            public fixed byte Reserved[30];
        }

        /*!
        * 서브 구조체
        */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_ABBInvertorCtrlRec
        {
            public UInt16 Ctrl_bit;
            public Int16 Value_1;
            public Int16 Value_2;
            public Int16 Value_3;
            public Int16 Value_4;
            public UInt16 Value_5;
            public byte Value_6;
           
            public fixed byte Reserved[27];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_ABBREC_InvertorRes
        {
            
            public TRTV_ABBInvertorStRec Invertor_1_St;
            //TRTV_ABBInvertorStRec 사이즈가 변경되면 TRTV_InvertorStRec 배열 크기로 변경되어야 한다 (5 x 사이즈)
            public fixed byte otherInvertir_St[5 * 40];
            public TRTV_ABBInvertorCtrlRec Invertor_1_Ctrl;
            //TRTV_ABBInvertorCtrlRec 사이즈가 변경되면 TRTV_InvertorCtrlRec 배열 크기로 변경되어야 한다 (5 x 사이즈)
            public fixed byte otherInvertir_Ctrl[5 * 40];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMSRTV_SEWInvertorStRec
        {
            public UInt16 St_bit_1;
            public Int16 Value_1;
            public UInt16 Value_2;
            public Int16 Value_3;
            public UInt16 St_bit_2;
            public UInt16 Value_4;
            public Int32 Value_5;
            public fixed byte Reserved[24];
        }

        /*!
        * 서브 구조체
        */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMSRTV_SEWInvertorCtrlRec
        {
            public UInt16 Ctrl_bit_1;
            public Int16 Value_1;
            public UInt16 Value_2;
            public UInt16 Value_3;
            public UInt16 Ctrl_bit_2;
            public UInt16 Value_4;
            public Int32 Value_5;
            public UInt16 Value_6;
            public fixed byte Reserved[22];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMSRTV_SEWREC_InvertorRes
        {

            public TEMSRTV_SEWInvertorStRec Invertor_1_St;
            //TRTV_SEWInvertorStRec 사이즈가 변경되면 TRTV_InvertorStRec 배열 크기로 변경되어야 한다 (5 x 사이즈)
            public fixed byte otherInvertir_St[5 * 40];
            public TEMSRTV_SEWInvertorCtrlRec Invertor_1_Ctrl;
            //TRTV_SEWInvertorCtrlRec 사이즈가 변경되면 TRTV_InvertorCtrlRec 배열 크기로 변경되어야 한다 (5 x 사이즈)
            public fixed byte otherInvertir_Ctrl[5 * 40];
        }

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct TEMSRTV_REC_InvertorRes
        {
            [FieldOffset(0)] public byte InvertorType;
            [FieldOffset(1)] public TRTV_ABBREC_InvertorRes ABB_INV;
            [FieldOffset(1)] public TEMSRTV_SEWREC_InvertorRes SEW_INV;
        }
        #endregion


        #region RTV 제어, 주행, 피딩 파라미터 조회/제어 구조체
        /*!
         * 프로토콜 "0x00A1 제어 설정 조회_1" 참조
         * 프로토콜 "0x00A2 제어 설정 변경_1" 참조
         * 프로토콜 "0x00A3 주행 드라이브 설정 조회_1 조회" 참조
         * 프로토콜 "0x00A4 주행 드라이브 설정 변경_1" 참조
         * 프로토콜 "0x00A7 피딩 드라이브 설정 조회
         * 프로토콜 "0x00A8 피딩 드라이브 설정 변경" 참조
         */

        /*! 조회 구조체는 공통 사용 */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_REC_PARAMReq
        {
            public fixed byte Reserved[20];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_CTRLParamRes
        {
            public byte SafetyPlug_ProcessType;
            public byte Collision_ProcessType;

            public byte AlarmUse_OpticModem;
            public byte AlarmUse_LimitOver;
            public byte DriveRef_Type;
            public UInt16 DriveRef_Offset;
            public byte DeSpeedArea_Type;
            public UInt16 DeSpeedArea_Offset;
            public byte AlarmUse_OverItemOnFeeding;
            public byte AlarmUse_InterlcokTimeOut;
            public fixed byte Reserved_1[40];

            public byte InPosition_Offset;
            public byte InPosition_Hist;
            public byte RetryInPosition_Count;
            public UInt16 RetryInPosition_Range;
            public UInt16 CripRange;
            public byte CurrentDecel_OffsetTime;
            public UInt16 CurrentDecel_OffsetMaxDistance;
            public UInt16 CurrentLowSpeedDistance;
            public byte Reserved_2;

            public UInt16 ChangeMC_DelayTime;
            public UInt16 InvertorOn_DelayTime;
            public UInt16 OP_TimeOut_ManualCtrl;
            public fixed byte Reserved_5[38];

            public UInt16 Loading_DelayTime_beforMove;
            public UInt16 Loading_DelayTime_afterMove;
            public UInt16 Loading_DelayTime_beforFeed;
            public UInt16 Loading_DelayTime_afterFeed;
            public UInt16 Loading_DelayTime_Done;
            public fixed byte Reserved_3[30];

            public UInt16 UnLoading_DelayTime_beforMove;
            public UInt16 UnLoading_DelayTime_afterMove;
            public UInt16 UnLoading_DelayTime_beforFeed;
            public UInt16 UnLoading_DelayTime_afterFeed;
            public UInt16 UnLoading_DelayTime_Done;
            public fixed byte Reserved_4[30];


            public UInt16 AutoOP_TimeOut_Move;
            public UInt16 AutoOP_TimeOut_Interlock;
            public UInt16 AutoOP_TimeOut_LoadFeed;
            public UInt16 AutoOP_TimeOut_UnLoadFeed;
            public UInt16 AutoOP_TimeOut_ItemCheckLoad;
            public UInt16 AutoOP_TimeOut_ItemCheckUnLoad;
            public UInt16 AutoOP_TimeOut_Crip;
            public fixed byte Reserved_6[26];

            public UInt16 AutoInit_ForceMode;

            public UInt16 WifiControllerTime;

            public UInt16 Buzzer_Error_Time;
            public UInt16 Buzzer_Warnning_Time;
            public UInt16 Buzzer_AutoModeOn_Time;
            public UInt16 Buzzer_AutoModeOff_Time;
            public byte Buzzer_AutoMode_RepeatCount;
            public fixed byte Reserved_7[10];

        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_CTRLParamCTRL
        {
            public fixed byte CtrlFlag[4];
            public fixed byte Reserved_0[20];

            public TRTV_CTRLParamRes ParamItemsRec;
        }

        /*! 서브 구조체 */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTVSpeedParamRec
        {
            public UInt16 Speed;
            public UInt16 Accel;
            public UInt16 Decel;
            public UInt16 A_jerk;
            public UInt16 D_jerk;

            public fixed byte Reserved[4];
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_DriveParamRes
        {
            public TRTVSpeedParamRec Speed_Auto_High;
            public TRTVSpeedParamRec Speed_Auto_Middle;
            public TRTVSpeedParamRec Speed_Auto_Low;

            public TRTVSpeedParamRec Speed_Manual_Middle;
            public TRTVSpeedParamRec Speed_Manual_Low;

            public TRTVSpeedParamRec Speed_Force;
            public TRTVSpeedParamRec Speed_Creep;
            public TRTVSpeedParamRec Speed_RefSet;
            public TRTVSpeedParamRec Speed_Emergency;
            public TRTVSpeedParamRec Speed_Auto_Decel1;
            public TRTVSpeedParamRec Speed_Auto_Decel2;

            public TRTVSpeedParamRec Speed_Collision;
            public TRTVSpeedParamRec Speed_Reserved2;
            public TRTVSpeedParamRec Speed_Reserved3;
            public TRTVSpeedParamRec Speed_Reserved4;
            public TRTVSpeedParamRec Speed_Reserved5;

            public UInt16 MAX_RPM;
            public UInt16 CALC_MPM;
            public UInt16 CALC_RPM;
            public byte MotorDirection;

            public UInt16 ManualOp_TokeAlarm;
            public UInt16 breakOpenContinueTime;
            public fixed byte Reserved1[20];

            public UInt16 InvertorGain;
            public fixed byte Reserved2[20];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_DriveParamCTRL
        {
            public fixed byte CtrlFlag[4];
            public fixed byte Reserved_0[30];

            public TRTV_DriveParamRes ParamItemsRec;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_FeedParamRes
        {
            public TRTVSpeedParamRec Speed_Auto_High;
            public TRTVSpeedParamRec Speed_Auto_Middle;
            public TRTVSpeedParamRec Speed_Auto_Low;

            public TRTVSpeedParamRec Speed_Manual_Middle;
            public TRTVSpeedParamRec Speed_Manual_Low;

            public TRTVSpeedParamRec Speed_Force;
            public TRTVSpeedParamRec Speed_Emergency;

            public TRTVSpeedParamRec Speed_Reserved1;
            public TRTVSpeedParamRec Speed_Reserved2;
            public TRTVSpeedParamRec Speed_Reserved3;
            public TRTVSpeedParamRec Speed_Reserved4;
            public TRTVSpeedParamRec Speed_Reserved5;

            public UInt16 MAX_RPM;
            public UInt16 CALC_MPM;
            public UInt16 CALC_RPM;
            public byte Feed1_MotorDirection;

            public UInt16 ManualOp_TokeAlarm;
            public UInt16 breakOpenContinueTime;
            public byte Feed2_MotorDirection;
            public fixed byte Reserved1[19];

            public UInt16 InvertorGain;
            public fixed byte Reserved2[20];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_FeedParamCTRL
        {
            public fixed byte CtrlFlag[4];
            public fixed byte Reserved_0[30];

            public TRTV_FeedParamRes ParamItemsRec;
        }

        /*! 제어응답은 구조체 선언 생략하고 처리 함*/
        #endregion

        #region EMS 제어, 주행, 승하강 파라미터 조회/제어 구조체
        /*!
         * 프로토콜 "0x00A1 제어 설정 조회_1" 참조
         * 프로토콜 "0x00A2 제어 설정 변경_1" 참조
         * 프로토콜 "0x00A3 주행 드라이브 설정 조회_1 조회" 참조
         * 프로토콜 "0x00A4 주행 드라이브 설정 변경_1" 참조
         * 프로토콜 "0x00A7 피딩 드라이브 설정 조회
         * 프로토콜 "0x00A8 피딩 드라이브 설정 변경" 참조
         */

        /*! 조회 구조체는 공통 사용 */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_REC_PARAMReq
        {
            public fixed byte Reserved[20];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_CTRLParamRes
        {
            public byte SafetyPlug_ProcessType;
            public byte Collision_ProcessType;

            public byte AutoHome_Use;
            public UInt16 AutoHome_Position;
            public byte AutoHome_WaitTime;

            public byte Loading_LiftBreakSet;
            public byte BuzzerAtLiftDown;
            public fixed byte Reserved_1[21];

            public byte AlarmUse_DriveLimitSetErr;
            public byte AlarmUse_InterlockErr;
            public byte AlarmUse_InterlcokTimeOut;
            public byte AlarmUse_InterLockPIO;
            public byte AlarmUse_CagePIO;
            public fixed byte Reserved_2[22];


            public UInt16 Loading_DelayTime_beforMove;
            public UInt16 Loading_DelayTime_afterMove;
            public UInt16 Loading_DelayTime_beforLiftDown;
            public UInt16 Loading_DelayTime_afterLiftDown;
            public UInt16 Loading_DelayTime_beforLoading;
            public UInt16 Loading_DelayTime_afterLoading;
            public UInt16 Loading_DelayTime_beforLiftUp;
            public UInt16 Loading_DelayTime_afterLiftUp;
            public UInt16 Loading_DelayTime_WorkDone;
            public fixed byte Reserved_3[22];

            public UInt16 UnLoading_DelayTime_beforMove;
            public UInt16 UnLoading_DelayTime_afterMove;
            public UInt16 UnLoading_DelayTime_beforLiftDown;
            public UInt16 UnLoading_DelayTime_afterLiftDown;
            public UInt16 UnLoading_DelayTime_beforUnLoading;
            public UInt16 UnLoading_DelayTime_afterUnLoading;
            public UInt16 UnLoading_DelayTime_beforLiftUp;
            public UInt16 UnLoading_DelayTime_afterLiftUp;
            public UInt16 UnLoading_DelayTime_WorkDone;
            public fixed byte Reserved_4[22];

            public UInt16 TimeOut_Set_LiftRef;
            public UInt16 TimeOut_Set_ChuckRef;
            public fixed byte Reserved_5[36];

            public UInt16 TimeOut_ManualCmdRx;
            public fixed byte Reserved_6[38];


            public UInt16 AutoOP_TimeOut_ChuckingRetry;
            public UInt16 AutoOP_TimeOut_Reserved2;
            public UInt16 AutoOP_TimeOut_Reserved3;
            public UInt16 AutoOP_TimeOut_LoadInterlock;
            public UInt16 AutoOP_TimeOut_UnLoadInterlock;
            public UInt16 AutoOP_TimeOut_Load;
            public UInt16 AutoOP_TimeOut_UnLoad;
            public UInt16 AutoOP_TimeOut_LoadingItemDectect;
            public UInt16 AutoOP_TimeOut_UnLoadingItemDectect;
            public UInt16 AutoOP_TimeOut_MoveCrip;
            public UInt16 AutoOP_TimeOut_LiftCrip;
            public UInt16 AutoOP_TimeOut_InterlockPIO;
            public UInt16 AutoOP_TimeOut_CagePIO;
            public fixed byte Reserved_7[14];

            public UInt16 Time_Release_ForceMode;
            public UInt16 Time_Polling_GMC;

            public UInt16 Buzzer_Error_Time;
            public UInt16 Buzzer_Warnning_Time;
            public UInt16 Buzzer_AutoModeOn_Time;
            public UInt16 Buzzer_AutoModeOff_Time;
            public byte Buzzer_AutoMode_RepeatCount;
            public fixed byte Reserved_8[10];

            public fixed byte Reserved_9[100];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_CTRLParamCTRL
        {
            public fixed byte CtrlFlag[4];
            public fixed byte Reserved_0[20];

            public TEMS_CTRLParamRes ParamItemsRec;
        }

        /*! 서브 구조체 */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMSSpeedParamRec
        {
            public UInt16 Speed;
            public UInt16 Accel;
            public UInt16 Decel;
            public UInt16 A_jerk;
            public UInt16 D_jerk;

            public fixed byte Reserved[4];
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_DriveParamRes
        {
            public TEMSSpeedParamRec Speed_Auto_High;
            public TEMSSpeedParamRec Speed_Auto_Middle;
            public TEMSSpeedParamRec Speed_Auto_Low;

            public TEMSSpeedParamRec Speed_Manual_Middle;
            public TEMSSpeedParamRec Speed_Manual_Low;

            public TEMSSpeedParamRec Speed_Force;
            public TEMSSpeedParamRec Speed_Creep;
            public TEMSSpeedParamRec Speed_RefSet;
            public TEMSSpeedParamRec Speed_Emergency;
            public TEMSSpeedParamRec Speed_Auto_Decel1;
            public TEMSSpeedParamRec Speed_Auto_Decel2;

            public TEMSSpeedParamRec Speed_Collision;
            public TEMSSpeedParamRec Speed_RetryRef;
            public TEMSSpeedParamRec Speed_Reserved3;
            public TEMSSpeedParamRec Speed_Reserved4;
            public TEMSSpeedParamRec Speed_Reserved5;

            public UInt16 MAX_RPM;
            public UInt16 CALC_MPM;
            public UInt16 CALC_RPM;
            public byte MotorDirection;

            public UInt16 ManualOp_TokeAlarm;
            public UInt16 breakOpenContinueTime;
            public fixed byte Reserved1[20];

            public Int32 Invertor_Reference;
            public UInt32 Invertor_PositionGain;
            public byte Invetor_Positiontolerance;
            public byte Invetor_Param_Use;

            public byte InPosition_Offset;
            public byte InPosition_Hist;
            public byte RetryInPosition_Count;
            public UInt16 RetryInPosition_Range;
            public UInt16 CripRange;
            public byte CurrentDecel_OffsetTime;
            public UInt16 CurrentDecel_OffsetMaxDistance;
            public UInt16 CurrentLowSpeedDistance;
            public byte RetryCripRange;

            public fixed byte Reserved3[50];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_DriveParamCTRL
        {
            public fixed byte CtrlFlag[4];
            public fixed byte Reserved_0[30];

            public TEMS_DriveParamRes ParamItemsRec;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_LiftParamRes
        {
            public TEMSSpeedParamRec Speed_Auto_High;
            public TEMSSpeedParamRec Speed_Auto_Middle;
            public TEMSSpeedParamRec Speed_Auto_Low;

            public TEMSSpeedParamRec Speed_Manual_Middle;
            public TEMSSpeedParamRec Speed_Manual_Low;

            public TEMSSpeedParamRec Speed_Force;
            public TEMSSpeedParamRec Speed_Emergency;

            public TEMSSpeedParamRec Speed_RefCheck;
            public TEMSSpeedParamRec Speed_StartCrip;
            public TEMSSpeedParamRec Speed_EndCrip;
            public TEMSSpeedParamRec Speed_Reserved4;
            public TEMSSpeedParamRec Speed_Reserved5;

            public UInt16 MAX_RPM;
            public UInt16 CALC_MPM;
            public UInt16 CALC_RPM;
            public byte MotorDirection;

            public UInt16 ManualOp_TokeAlarm;
            public UInt16 ManualOp_Startmm;
            public UInt16 ManualOp_Endmm;
            public UInt16 breakOpenContinueTime;
            public fixed byte Reserved1[20];

            public Int32 Invertor_Reference;
            public UInt32 Invertor_PositionGain;
            public byte Invetor_Positiontolerance;
            public byte Invetor_Param_Use;

            public byte CurrenctPostion_Offset;
            public byte CurrenctPostion_His;
            public byte CurrenctPostion_RetryCount;
            public UInt16 CurrenctPostion_RetryRange;
            public UInt16 CurrenctDownPostion_Offset;
            public fixed byte Reserved2[8];

            public UInt16 Loading_DownStartCrip;
            public UInt16 Loading_DownEndCrip;
            public UInt16 Loading_UpStartCrip;
            public UInt16 Loading_UpEndCrip;
            public UInt16 UnLoading_DownStartCrip;
            public UInt16 UnLoading_DownEndCrip;
            public UInt16 UnLoading_UpStartCrip;
            public UInt16 UnLoading_UpEndCrip;

            public fixed byte Reserved3[2];


            public byte Lift_HomeAOffset;
            public byte Lift_HomeBOffset;
            public byte Lift_RefSEnsor;
            public byte Lift_HomeOffset_AllCtrlCan;

            public Int16 Cargo_height_Loading_1;
            public Int16 Cargo_height_Loading_2;
            public Int16 Cargo_height_Loading_3;
            public Int16 Cargo_height_Loading_4;
            public Int16 Cargo_height_Loading_5;
            public Int16 Cargo_height_Loading_6;
            public Int16 Cargo_height_Loading_7;
            public Int16 Cargo_height_Loading_8;
            public Int16 Cargo_height_Loading_9;
            public fixed byte Reserved4[12];

            public Int16 Cargo_height_UnLoading_1;
            public Int16 Cargo_height_UnLoading_2;
            public Int16 Cargo_height_UnLoading_3;
            public Int16 Cargo_height_UnLoading_4;
            public Int16 Cargo_height_UnLoading_5;
            public Int16 Cargo_height_UnLoading_6;
            public Int16 Cargo_height_UnLoading_7;
            public Int16 Cargo_height_UnLoading_8;
            public Int16 Cargo_height_UnLoading_9;
            public fixed byte Reserved5[12];

            public fixed byte Reserved6[10];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_LiftParamCTRL
        {
            public fixed byte CtrlFlag[4];
            public fixed byte Reserved_0[30];

            public TEMS_LiftParamRes ParamItemsRec;
        }

        /*! 제어응답은 구조체 선언 생략하고 처리 함*/
        #endregion

        #region SRM 반송 지령 구조체
        /*!
         * 프로토콜 "0x0040_SRM 반송 지령" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_REC_TaskJobCTRL
        {
            public UInt32 taskWorkNum;
            public byte OptionFlag;
            public byte Reserved1;

            //구조체에 대한 고정배열 선언이 안됨
            //가변배열로 해서 new로 생성은 가능하지만 그렇게 하면 구조체에 대한 주소, 크기 접근이 안되고.
            //TSRM_TaskJobItem 사이즈가 변경되면 TSRM_TaskJobItem 배열 크기로 변경되어야 한다
            public TSRM_TaskJobItem TaskJobItem_1;
            public fixed byte otherTaskJobItem[19 * 11]; //TaskJobItem_2 ~ TaskJobItem_20 은 멤버변수로 아니고 포인터로 접근할 것이기 때문에 할당만 해준다
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SRM_REC_JobCTRL
        {
            public byte CMD;
            public byte OptionFlag;
            public fixed byte Reserved[4];

            public UInt32 Work1_Num;
            public TSRMPositionCMDRec Work1_From;
            public TSRMPositionCMDRec Work1_To;
            public byte Work1_ItermType;
            public fixed byte Reserved1[5];

            public UInt32 Work2_Num;
            public TSRMPositionCMDRec Work2_From;
            public TSRMPositionCMDRec Work2_To;
            public byte Work2_ItermType;
            public fixed byte Reserved2[5];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRM_REC_JobCTRLRES
        {
            public byte CMD;
            public UInt32 Work1_Num;
            public UInt32 Work2_Num;
            public byte ResultRes;

            public byte Work1_ResultRes;
            public byte Work2_ResultRes;
            public byte Reserved;
        }
        #endregion

        #region RTV 반송 지령 구조체
        /*!
         * 프로토콜 "00x0040 RTV 반송 지령" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_REC_TaskJobCTRL
        {
            public UInt32 taskWorkNum;
            public fixed byte Reserved1[2];

            //구조체에 대한 고정배열 선언이 안됨
            //가변배열로 해서 new로 생성은 가능하지만 그렇게 하면 구조체에 대한 주소, 크기 접근이 안되고.
            //TRTV_TaskJobItem 사이즈가 변경되면 TRTV_TaskJobItem 배열 크기로 변경되어야 한다
            public TRTV_TaskJobItem TaskJobItem_1;
            public fixed byte otherTaskJobItem[19 * 11]; //TaskJobItem_2 ~ TaskJobItem_20 은 멤버변수로 아니고 포인터로 접근할 것이기 때문에 할당만 해준다
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RTV_REC_JobCTRL
        {
            public byte CMD;
            public fixed byte Reserved[5];

            public UInt32 Work1_Num;
            public TRTVPointRec Work1_From;
            public TRTVPointRec Work1_To;
            public fixed byte Reserved1[8];

            public UInt32 Work2_Num;
            public TRTVPointRec Work2_From;
            public TRTVPointRec Work2_To;
            public fixed byte Reserved2[8];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTV_REC_JobCTRLRES
        {
            public byte CMD;
            public UInt32 Work1_Num;
            public UInt32 Work2_Num;
            public byte ResultRes;

            public byte Work1_ResultRes;
            public byte Work2_ResultRes;
            public byte Reserved;
        }
        #endregion

        #region EMS 반송 지령 구조체
        /*!
         * 프로토콜 "00x0040 EMS 반송 지령" 참조
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_REC_TaskJobCTRL
        {
            public UInt32 taskWorkNum;
            public fixed byte Reserved1[2];

            //구조체에 대한 고정배열 선언이 안됨
            //가변배열로 해서 new로 생성은 가능하지만 그렇게 하면 구조체에 대한 주소, 크기 접근이 안되고.
            //TRTV_TaskJobItem 사이즈가 변경되면 TRTV_TaskJobItem 배열 크기로 변경되어야 한다
            public TEMS_TaskJobItem TaskJobItem_1;
            public fixed byte otherTaskJobItem[19 * 11]; //TaskJobItem_2 ~ TaskJobItem_20 은 멤버변수로 아니고 포인터로 접근할 것이기 때문에 할당만 해준다
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct EMS_REC_JobCTRL
        {
            public byte CMD;
            public byte OptionFlag;
            public fixed byte Reserved[4];

            public UInt32 Work_Num;
            public TEMSPointRec Work_From;
            public TEMSPointRec Work_To;
            public byte Chucking_Width;
            public UInt16 Loading_height;
            public UInt16 UnLoading_height;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMS_REC_JobCTRLRES
        {
            public byte CMD;
            public UInt32 Work_Num;
            public byte ResultRes;

            public byte Work_ResultRes;
            public fixed byte Reserved1[5];
        }
        #endregion


        #region 수동운전 지령 구조체
        /*!
         * 프로토콜 "0x0080 수동명령" 참조 — 송신 시 CMD1=0x00, CMD2=0x80 과 함께 본 구조체를 바디로 직렬화(Form_Main.Do_JogCtrl → ADD_TxUserData).
         * CtrlFlag[0]: 축 선택 비트 — 주행 0x01, 승강 0x02, Fork1 0x04, Fork2 0x08, 동시 0x0C 등(Do_JogCtrl switch와 동일).
         * Drive / Updown / Fork1 / Fork2: 문서의 축별 방향·속도 단계 코드(Do_JogCtrl의 case 숫자).
         * LowSpeed_Ref: 수동 속도 프로파일 번호(RTV는 Form_RTV_CTL.RtvManualJog_LowSpeedRefFromTag에서 설정).
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_ManualCtrl
        {
            public fixed byte CtrlFlag[2];
            public byte Drive;
            public byte Updown;
            public byte Fork1;
            public byte Fork2;
            public byte PositionMove;
            public fixed byte Reserved[8];
            //public byte Fork_Ref;
            public byte Fork1_IOCtrl;
            public byte Fork2_IOCtrl;
            public byte LowSpeed_Ref;
        }
        #endregion

        #region MOVEX WCS Memory Map

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_WCSMapReq
        {
            public fixed byte Reserved[20];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TMOVEX_WCS_DataRec
        {
            public UInt32 RX_WCSDataTime;
            public fixed UInt16 WCSData[100];
            public UInt32 TX_DEVDataTime_1;
            public UInt32 TX_DEVDataTime_2;
            public fixed UInt16 DEVData[300];
        }
        #endregion


            #region 0x004x ~ 0x007x
            /*!
             * 프로토콜 "0x004x ~ 0x007x" 참조
             */
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TDEV_REC_SensorScanCtrl
        {
            public byte StartStop;
            public byte Mode;
            public fixed byte Reserved[50];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct REC_Scan_EthercatSlave
        {
            public byte BoardType;
            public byte Slave_IDSwitch;
        }
        #endregion

        #region  장치 상태 관리 구조체
        /*! 서브 구조체 */
        public struct TManual_DEV_CtrlBuf
        {
            // CtrlTypeValue / CtrlTypeValue_before: UI 버튼 Tag와 동일한 조그 종류 번호 → Do_JogCtrl에서 TDEV_ManualCtrl로 변환.
            // 0 : 정지(MouseUp에서 CtrlTypeValue=0, before=직전 Tag)
            // 11 : 저속전진 12 : 저속후진 13 : 중속전진 14 : 중속후진
            // 21 : 저속상승 22 : 저속하강 23 : 중속상승 24 : 중속하강
            // 31 : Fork1 중심 32 : Fork1 좌 33 : Fork1 우
            // RTV Form: Fork1 고속 34·35, Fork2 저속 42·43·고속 44·45, 동시 72~75 등 확장(Do_JogCtrl case 참고).
            // 41 : Fork2 중심 42 : Fork2 좌 43 : Fork2 우
            public byte CtrlTypeValue_OLD;
            public byte CtrlTypeValue;
            public byte CtrlTypeValue_before;
            //public byte ForkRef;
            public byte LowSpeedRef;
        }

        public unsafe struct TDEV_REC
        {
            //Flag 변수는 필요에 의해 선언한다
            //해당 타입의 응답이 있었는지 유무를 확인하여서 뭔가를 해주어야 하는 경우
            public bool Flag_In_DevStatus;
            public bool Flag_In_8110;
            public bool Flag_In_8112;

            public DateTime Time_In_DevStatus;

            public TManual_DEV_CtrlBuf Manual_DEV_CtrlRec;

            //모든 CMD에 대한 구조체 변수를 선언할 필요는 없다
            //구조체 선언이 필요한 경우 byte array로 데이터에 접근하는 것보다 구조체내 멤버 이름으로 접근하는 것이 좀 더 용이한 경우
            //데이터를 1회적으로 처리하지 않고 버퍼에 담아놓고계속 접근하는 경우등 필요한 경우에만 선언하면 된다.
            //구조체 변수는 선언하지 않고 구조체 자체만 정의 (타입 정의만) 하여 쓰기도 한다. 구조체의 사이즈만을 참조하는 용도로만 사용.
            public TSRM_REC_StatusReq srm_REC_DevStReq;
            public TRTV_REC_StatusReq rtv_REC_DevStReq;
            public TEMS_REC_StatusReq ems_REC_DevStReq;

            public TSRM_StatusRes srm_REC_SRMSt;
            public TRTV_StatusRes rtv_REC_RTVSt;
            public TEMS_StatusRes ems_REC_EMSSt;
            
            public TDEV_REC_BasicStRes dev_REC_BasicSt;
            public TDEV_REC_TestStRes dev_REC_TestSt;
            public TDEV_ManualCtrl dev_REC_ManualCtrl;

            public TMOVEX_WCS_DataRec movexWCSRec;


            public TSRM_REC_JobCTRLRES srm_REC_JobCtrlRes;

        }

        //**********************************************************************
        #endregion


        #region  Total 설정 파일 클래스
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSaveCheckRec
        {
            public byte Save_Flag;
            public long Save_Update;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TSRMToTalFile_Header
        {
            public byte fileType;
            public TSaveCheckRec SRM_CFG_Flag;
            public TSaveCheckRec IO_CFG_Flag;
            public TSaveCheckRec CTRL_PARAM_Flag;
            public TSaveCheckRec LIFT_PARAM_Flag;
            public TSaveCheckRec DRIVE_PARAM_Flag;
            public TSaveCheckRec FORK_PARAM_Flag;
            public TSaveCheckRec NRACK_Flag;
            public TSaveCheckRec SRACK_Flag;
            public TSaveCheckRec RACK_CFG_Flag_Reserved;
            public TSaveCheckRec STATION_CFG_Flag;
            public TSaveCheckRec POSITION_LEFT_CFG_Flag;
            public TSaveCheckRec POSITION_RIGHT_CFG_Flag;
            public TSaveCheckRec OFFSET_CFG_Flag;
            public byte reserved;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class SRM_CellOffset_Total
        {
            public byte DevType;
            public UInt16 TotalCount;
            public TSRM_CellOffsetRec[] SRM_CellOffsetRec = new TSRM_CellOffsetRec[128 * 256];
            //public byte[] OtherCellOffsetItem = new byte[(32768 - 1) * 8];
            //public fixed byte OtherCellOffsetItem[(32768 - 1) * 8];
            //public fixed byte OtherCellOffsetItem[(10 - 1) * 8];
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class TSRM_ToTalFile
        {
            private string myFileName;

            private TSRMToTalFile_Header Header;
            public byte[] MCU_CFG_DATA;
            public byte[] IO_CFG_DATA;
            public byte[] CTRL_PARAM_DATA;
            public byte[] LIFT_PARAM_DATA;
            public byte[] DRIVE_PARAM_DATA;
            public byte[] FORK_PARAM_DATA;
            public byte[] NRACK_DATA;
            public byte[] SRACK_DATA;
            public byte[] RACK_CFG_DATA_Reserved;
            public byte[] STATION_CFG_DATA;
            public byte[] Level_L_POSITION_CFG_DATA;
            public byte[] Bay_L_POSITION_CFG_DATA;
            public byte[] Level_R_POSITION_CFG_DATA;
            public byte[] Bay_R_POSITION_CFG_DATA;
            public byte[] OFFSET_CFG__DATA;


            private static ushort HeaderLen = (ushort) Marshal.SizeOf(typeof(VEXI_DEFS.TSRMToTalFile_Header));
            private static ushort DataLen_1 = 100; //MCU_CFG_DATAv
            private static ushort DataLen_2 = 1000; //IO_CFG_DATA
            private static ushort DataLen_3 = 1500; //CTRL_PARAM_DATA
            private static ushort DataLen_4 = 1500; //LIFT_PARAM_DATA
            private static ushort DataLen_5 = 1500; //DRIVE_PARAM_DATA
            private static ushort DataLen_6 = 1500; //FORK_PARAM_DATA
            private static ushort DataLen_7 = 1500; //NRACK_DATA
            private static ushort DataLen_8 = 1500; //SRACK_DATA
            private static ushort DataLen_9 = 500; //RACK_CFG_DATA_Reserved
            private static ushort DataLen_10 = 1500; //STATION_CFG_DATA
            private static ushort DataLen_11 = 1500; //Level L POSITION_CFG_DATA
            private static ushort DataLen_12 = 1500; //Bay L POSITION_CFG_DATA
            private static ushort DataLen_13 = 1500; //Level R POSITION_CFG_DATA
            private static ushort DataLen_14 = 1500; //Bay R POSITION_CFG_DATA
            private static uint DataLen_15 = 3 + 32768 * 8; //OFFSET_CFG__DATA

            public TSRM_ToTalFile(string TmpFileName)
            {
                myFileName = TmpFileName;
                MCU_CFG_DATA = new byte[DataLen_1];
                IO_CFG_DATA = new byte[DataLen_2];
                CTRL_PARAM_DATA = new byte[DataLen_3];
                LIFT_PARAM_DATA = new byte[DataLen_4];
                DRIVE_PARAM_DATA = new byte[DataLen_5];
                FORK_PARAM_DATA = new byte[DataLen_6];
                NRACK_DATA = new byte[DataLen_7];
                SRACK_DATA = new byte[DataLen_8];
                RACK_CFG_DATA_Reserved = new byte[DataLen_9];
                STATION_CFG_DATA = new byte[DataLen_10];
                Level_L_POSITION_CFG_DATA = new byte[DataLen_11];
                Bay_L_POSITION_CFG_DATA = new byte[DataLen_12];
                Level_R_POSITION_CFG_DATA = new byte[DataLen_13];
                Bay_R_POSITION_CFG_DATA = new byte[DataLen_14];
                OFFSET_CFG__DATA = new byte[DataLen_15];
            }

            public TSRM_ToTalFile()
            {
                myFileName = "";
                MCU_CFG_DATA = new byte[DataLen_1];
                IO_CFG_DATA = new byte[DataLen_2];
                CTRL_PARAM_DATA = new byte[DataLen_3];
                LIFT_PARAM_DATA = new byte[DataLen_4];
                DRIVE_PARAM_DATA = new byte[DataLen_5];
                FORK_PARAM_DATA = new byte[DataLen_6];
                NRACK_DATA = new byte[DataLen_7];
                SRACK_DATA = new byte[DataLen_8];
                RACK_CFG_DATA_Reserved = new byte[DataLen_9];
                STATION_CFG_DATA = new byte[DataLen_10];
                Level_L_POSITION_CFG_DATA = new byte[DataLen_11];
                Bay_L_POSITION_CFG_DATA = new byte[DataLen_12];
                Level_R_POSITION_CFG_DATA = new byte[DataLen_13];
                Bay_R_POSITION_CFG_DATA = new byte[DataLen_14];
                OFFSET_CFG__DATA = new byte[DataLen_15];
            }

            public string FileName
            {
                get { return myFileName; }
                set { myFileName = value; }
            }

            public TSRMToTalFile_Header myHeader
            {
                get { return Header; }
            }

            private bool createFile(byte TmpFileType)
            {
                if (myFileName == "") return false;


                UInt16 SumLen = (UInt16) (HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6 + DataLen_7 + DataLen_8 + DataLen_9 + DataLen_10 + DataLen_11 + DataLen_12 + DataLen_13 + DataLen_14);

                using (BinaryWriter br = new BinaryWriter(File.Open(myFileName, FileMode.Create, FileAccess.Write)))
                {
                    try
                    {
                        br.Seek(0, SeekOrigin.Begin);
                        byte[] Savebytes = new byte[SumLen];
                        Global_Class.UTIL_ByteArray_clear(Savebytes, SumLen);
                        Savebytes[0] = TmpFileType;
                        br.Write(Savebytes);
                    }
                    finally
                    {
                        br.Close();
                    }
                }
                return true;
            }

            public bool Read_Header(byte TmpFileType)
            {
                Header.SRM_CFG_Flag.Save_Flag = 0;
                Header.IO_CFG_Flag.Save_Flag = 0;
                Header.CTRL_PARAM_Flag.Save_Flag = 0;
                Header.LIFT_PARAM_Flag.Save_Flag = 0;
                Header.DRIVE_PARAM_Flag.Save_Flag = 0;
                Header.FORK_PARAM_Flag.Save_Flag = 0;
                Header.NRACK_Flag.Save_Flag = 0;
                Header.SRACK_Flag.Save_Flag = 0;
                Header.RACK_CFG_Flag_Reserved.Save_Flag = 0;
                Header.STATION_CFG_Flag.Save_Flag = 0;
                Header.POSITION_LEFT_CFG_Flag.Save_Flag = 0;
                Header.POSITION_RIGHT_CFG_Flag.Save_Flag = 0;
                Header.OFFSET_CFG_Flag.Save_Flag = 0;


                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);

                        if (TmpFileType == Savebytes[0])
                        {
                            Header = (VEXI_DEFS.TSRMToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRMToTalFile_Header));
                            TmpResult = true;
                        }
                        else
                        {
                            TmpResult = false;
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Read_MCU_CFG(ref TSRM_DevConfigCtrl srm_cfg)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TSRMToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRMToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_SRM)
                        {
                            if (Header.SRM_CFG_Flag.Save_Flag == 1)
                            {
                                Savebytes = brRead.ReadBytes(DataLen_1);
                                srm_cfg = (VEXI_DEFS.TSRM_DevConfigCtrl)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_DevConfigCtrl));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_MCU_CFG(TSRM_DevConfigCtrl srm_cfg)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_SRM);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_SRM);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_DevConfigCtrl))];
                        Global_Class.UTIL_StructObjectToByteArray(srm_cfg, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }

                return true;
            }

            public bool Read_IO_CFG(ref SRM_IOConfig IO_cfg)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TSRMToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRMToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_SRM)
                        {
                            if (Header.IO_CFG_Flag.Save_Flag == 1)
                            {
                                brRead.ReadBytes(DataLen_1);
                                Savebytes = brRead.ReadBytes(DataLen_2);
                                IO_cfg = (VEXI_DEFS.SRM_IOConfig)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.SRM_IOConfig));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_IO_CFG(SRM_IOConfig IO_CFG)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_SRM);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_SRM);
                        brWrite.Seek(9 * 1 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1, SeekOrigin.Begin);
                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.SRM_IOConfig))];
                        Global_Class.UTIL_StructObjectToByteArray(IO_CFG, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_SRM_CTRL_PARAM(ref TSRM_CTRLParamCTRL CTRL_PARAM)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {

                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TSRMToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRMToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_SRM)
                        {
                            if (Header.CTRL_PARAM_Flag.Save_Flag == 1)
                            {
                                //SRM_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                Savebytes = brRead.ReadBytes(DataLen_3);
                                CTRL_PARAM = (VEXI_DEFS.TSRM_CTRLParamCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_CTRLParamCTRL));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }


            public bool Write_SRM_CTRL_PARAM(TSRM_CTRLParamCTRL CTRL_PARAM)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_SRM);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_SRM);
                        brWrite.Seek(9 * 2 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CTRLParamCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(CTRL_PARAM, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }


            public bool Read_SRM_LIFT_PARAM(ref TSRM_LiftParamCTRL LiftParamCTRL)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TSRMToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRMToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_SRM)
                        {
                            if (Header.LIFT_PARAM_Flag.Save_Flag == 1)
                            {
                                //SRM_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                Savebytes = brRead.ReadBytes(DataLen_4);
                                LiftParamCTRL = (VEXI_DEFS.TSRM_LiftParamCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_LiftParamCTRL));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_SRM_LIFT_PARAM(TSRM_LiftParamCTRL LIFT_PARAM)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_SRM);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_SRM);
                        brWrite.Seek(9 * 3 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_LiftParamCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(LIFT_PARAM, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }

                return true;
            }

            public bool Read_SRM_DRIVE_PARAM(ref TSRM_DriveParamCTRL DRIVE_PARAM)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TSRMToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRMToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_SRM)
                        {
                            if (Header.DRIVE_PARAM_Flag.Save_Flag == 1)
                            {
                                //SRM_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //LIFT_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                Savebytes = brRead.ReadBytes(DataLen_5);
                                DRIVE_PARAM = (VEXI_DEFS.TSRM_DriveParamCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_DriveParamCTRL));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_SRM_DRIVE_PARAM(TSRM_DriveParamCTRL DRIVE_PARAM)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_SRM);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_SRM);
                        brWrite.Seek(9 * 4 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_DriveParamCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(DRIVE_PARAM, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_SRM_ForkParamCTRL(ref TSRM_ForkParamCTRL ForkParamCTRL)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TSRMToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRMToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_SRM)
                        {
                            if (Header.FORK_PARAM_Flag.Save_Flag == 1)
                            {
                                //SRM_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //LIFT_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_5);
                                Savebytes = brRead.ReadBytes(DataLen_6);
                                ForkParamCTRL = (VEXI_DEFS.TSRM_ForkParamCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_ForkParamCTRL));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }
            public bool Write_SRM_ForkParamCTRL(TSRM_ForkParamCTRL ForkParamCTRL)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_SRM);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_SRM);
                        brWrite.Seek(9 * 5 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_ForkParamCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(ForkParamCTRL, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_SRM_NoUseRack(ref TSRM_NoUseRack NoUseRack)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TSRMToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRMToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_SRM)
                        {
                            if (Header.NRACK_Flag.Save_Flag == 1)
                            {
                                //SRM_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //LIFT_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_5);
                                //FORK_PARAM 영역
                                brRead.ReadBytes(DataLen_6);
                                Savebytes = brRead.ReadBytes(DataLen_7);
                                NoUseRack = (VEXI_DEFS.TSRM_NoUseRack)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_NoUseRack));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_SRM_NoUseRack(TSRM_NoUseRack NoUseRack)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_SRM);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_SRM);
                        brWrite.Seek(9 * 6 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_NoUseRack))];
                        Global_Class.UTIL_StructObjectToByteArray(NoUseRack, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_SRM_SpecialRack(ref TSRM_SpecialRack SpecialRack)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TSRMToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRMToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_SRM)
                        {
                            if (Header.SRACK_Flag.Save_Flag == 1)
                            {
                                //SRM_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //LIFT_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_5);
                                //FORK_PARAM 영역
                                brRead.ReadBytes(DataLen_6);
                                //NRACK 영역
                                brRead.ReadBytes(DataLen_7);
                                Savebytes = brRead.ReadBytes(DataLen_8);
                                SpecialRack = (VEXI_DEFS.TSRM_SpecialRack)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_SpecialRack));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }


            public bool Write_SRM_SpecialRack(TSRM_SpecialRack SpecialRack)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_SRM);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_SRM);
                        brWrite.Seek(9 * 7 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6 + DataLen_7, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_SpecialRack))];
                        Global_Class.UTIL_StructObjectToByteArray(SpecialRack, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_SRM_StationParam(ref TSRM_StationParam StationParam)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TSRMToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRMToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_SRM)
                        {
                            if (Header.STATION_CFG_Flag.Save_Flag == 1)
                            {
                                //SRM_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //LIFT_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_5);
                                //FORK_PARAM 영역
                                brRead.ReadBytes(DataLen_6);
                                //NRACK 영역
                                brRead.ReadBytes(DataLen_7);
                                //SRACK 영역
                                brRead.ReadBytes(DataLen_8);
                                //RACK_CFG
                                brRead.ReadBytes(DataLen_9);
                                Savebytes = brRead.ReadBytes(DataLen_10);
                                StationParam = (VEXI_DEFS.TSRM_StationParam)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_StationParam));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_SRM_StationParam(TSRM_StationParam StationParam)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_SRM);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_SRM);
                        brWrite.Seek(9 * 9 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6 + DataLen_7 + DataLen_8 + DataLen_9, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_StationParam))];
                        Global_Class.UTIL_StructObjectToByteArray(StationParam, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_SRM_LevelLPositionCTRL(ref TSRM_CellPositionCTRL PositionCTRL)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {

                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TSRMToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRMToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_SRM)
                        {
                            if (Header.POSITION_LEFT_CFG_Flag.Save_Flag == 1)
                            {
                                //SRM_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //LIFT_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_5);
                                //FORK_PARAM 영역
                                brRead.ReadBytes(DataLen_6);
                                //NRACK 영역
                                brRead.ReadBytes(DataLen_7);
                                //SRACK 영역
                                brRead.ReadBytes(DataLen_8);
                                //RACK_CFG
                                brRead.ReadBytes(DataLen_9);
                                //STATION_CFG 영역
                                brRead.ReadBytes(DataLen_10);
                                Savebytes = brRead.ReadBytes(DataLen_11);
                                PositionCTRL = (VEXI_DEFS.TSRM_CellPositionCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_CellPositionCTRL));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Read_SRM_LevelRPositionCTRL(ref TSRM_CellPositionCTRL PositionCTRL)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {

                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TSRMToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRMToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_SRM)
                        {
                            if (Header.POSITION_RIGHT_CFG_Flag.Save_Flag == 1)
                            {
                                //SRM_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //LIFT_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_5);
                                //FORK_PARAM 영역
                                brRead.ReadBytes(DataLen_6);
                                //NRACK 영역
                                brRead.ReadBytes(DataLen_7);
                                //SRACK 영역
                                brRead.ReadBytes(DataLen_8);
                                //RACK_CFG
                                brRead.ReadBytes(DataLen_9);
                                //STATION_CFG 영역
                                brRead.ReadBytes(DataLen_10);
                                //Level Left 영역
                                brRead.ReadBytes(DataLen_11);
                                //Bay Left 영역
                                brRead.ReadBytes(DataLen_12);
                                Savebytes = brRead.ReadBytes(DataLen_13);
                                PositionCTRL = (VEXI_DEFS.TSRM_CellPositionCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_CellPositionCTRL));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }
            public bool Write_SRM_LevelLPositionCTRL(TSRM_CellPositionCTRL PositionCTRL)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_SRM);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_SRM);
                        brWrite.Seek(9 * 10 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6 + DataLen_7 + DataLen_8 + DataLen_9 + DataLen_10, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellPositionCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(PositionCTRL, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Write_SRM_LevelRPositionCTRL(TSRM_CellPositionCTRL PositionCTRL)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_SRM);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_SRM);
                        brWrite.Seek(9 * 11 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6 + DataLen_7 + DataLen_8 + DataLen_9 + DataLen_10 + DataLen_11 + DataLen_12, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellPositionCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(PositionCTRL, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_SRM_BayLPositionCTRL(ref TSRM_CellPositionCTRL PositionCTRL)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {

                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TSRMToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRMToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_SRM)
                        {
                            if (Header.POSITION_LEFT_CFG_Flag.Save_Flag == 1)
                            {
                                //SRM_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //LIFT_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_5);
                                //FORK_PARAM 영역
                                brRead.ReadBytes(DataLen_6);
                                //NRACK 영역
                                brRead.ReadBytes(DataLen_7);
                                //SRACK 영역
                                brRead.ReadBytes(DataLen_8);
                                //RACK_CFG
                                brRead.ReadBytes(DataLen_9);
                                //STATION_CFG 영역
                                brRead.ReadBytes(DataLen_10);
                                //POSITION_CFG LEVEL 영역
                                brRead.ReadBytes(DataLen_11);
                                Savebytes = brRead.ReadBytes(DataLen_12);
                                PositionCTRL = (VEXI_DEFS.TSRM_CellPositionCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_CellPositionCTRL));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Read_SRM_BayRPositionCTRL(ref TSRM_CellPositionCTRL PositionCTRL)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {

                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TSRMToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRMToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_SRM)
                        {
                            if (Header.POSITION_RIGHT_CFG_Flag.Save_Flag == 1)
                            {
                                //SRM_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //LIFT_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_5);
                                //FORK_PARAM 영역
                                brRead.ReadBytes(DataLen_6);
                                //NRACK 영역
                                brRead.ReadBytes(DataLen_7);
                                //SRACK 영역
                                brRead.ReadBytes(DataLen_8);
                                //RACK_CFG
                                brRead.ReadBytes(DataLen_9);
                                //STATION_CFG 영역
                                brRead.ReadBytes(DataLen_10);
                                //POSITION_CFG LEVEL Left 영역
                                brRead.ReadBytes(DataLen_11);
                                //POSITION_CFG Bay Left 영역
                                brRead.ReadBytes(DataLen_12);
                                //POSITION_CFG LEVEL Right 영역
                                brRead.ReadBytes(DataLen_13);
                                Savebytes = brRead.ReadBytes(DataLen_14);
                                PositionCTRL = (VEXI_DEFS.TSRM_CellPositionCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_CellPositionCTRL));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }


            public bool Write_SRM_BayLPositionCTRL(TSRM_CellPositionCTRL PositionCTRL)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_SRM);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_SRM);
                        brWrite.Seek(9 * 10 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6 + DataLen_7 + DataLen_8 + DataLen_9 + DataLen_10 + DataLen_11, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellPositionCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(PositionCTRL, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }

                return true;
            }

            public bool Write_SRM_BayRPositionCTRL(TSRM_CellPositionCTRL PositionCTRL)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_SRM);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_SRM);
                        brWrite.Seek(9 * 11 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6 + DataLen_7 + DataLen_8 + DataLen_9 + DataLen_10 + DataLen_11 + DataLen_12 + DataLen_13, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CellPositionCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(PositionCTRL, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }

                return true;
            }



            public bool Read_SRM_OFFSET_CFG(ref SRM_CellOffset_Total CellOffset_Total)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {

                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TSRMToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRMToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_SRM)
                        {
                            if (Header.OFFSET_CFG_Flag.Save_Flag == 1)
                            {
                                //SRM_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //LIFT_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_5);
                                //FORK_PARAM 영역
                                brRead.ReadBytes(DataLen_6);
                                //NRACK 영역
                                brRead.ReadBytes(DataLen_7);
                                //SRACK 영역
                                brRead.ReadBytes(DataLen_8);
                                //RACK_CFG
                                brRead.ReadBytes(DataLen_9);
                                //STATION_CFG 영역
                                brRead.ReadBytes(DataLen_10);
                                //POSITION_CFG LEVEL Left 영역
                                brRead.ReadBytes(DataLen_11);
                                //POSITION_CFG Bay Left 영역
                                brRead.ReadBytes(DataLen_12);
                                //POSITION_CFG LEVEL Right 영역
                                brRead.ReadBytes(DataLen_13);
                                //POSITION_CFG Bay Right 영역
                                brRead.ReadBytes(DataLen_14);

                                Savebytes = brRead.ReadBytes(3);



                                CellOffset_Total.DevType = Savebytes[0];
                                CellOffset_Total.TotalCount = (ushort)((Savebytes[1] & 0x00FF) | ((Savebytes[2] << 8) & (0xFF00)));

                                if (CellOffset_Total.TotalCount > 0)
                                {

                                    for (int i = 0; i < CellOffset_Total.TotalCount; i++)
                                    {
                                        Savebytes = brRead.ReadBytes(8);
                                        CellOffset_Total.SRM_CellOffsetRec[i] = (VEXI_DEFS.TSRM_CellOffsetRec)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_CellOffsetRec));
                                    }
                                }
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_SRM_OFFSET_CFG(SRM_CellOffset_Total CellOffset_Total)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_SRM);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_SRM);
                        brWrite.Seek(9 * 12 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6 + DataLen_7 + DataLen_8 + DataLen_9 + DataLen_10 + DataLen_11 + DataLen_12 + DataLen_13 + DataLen_14, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[CellOffset_Total.TotalCount * 8 + 3];


                        Savebytes[0] = CellOffset_Total.DevType;
                        Savebytes[1] = (byte)(CellOffset_Total.TotalCount & 0x00FF);
                        Savebytes[2] = (byte)((CellOffset_Total.TotalCount >> 8) & 0x00FF);
                        if (CellOffset_Total.TotalCount > 0)
                        {
                            for (int i = 0; i < CellOffset_Total.TotalCount; i++)
                            {
                                Global_Class.UTIL_StructObjectToByteArray(CellOffset_Total.SRM_CellOffsetRec[i], Savebytes, 3 + (i) * 8);
                            }

                        }

                        brWrite.Write(Savebytes);

                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }

                return true;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRTVToTalFile_Header
        {
            public byte fileType;
            public TSaveCheckRec MCU_CFG_Flag;
            public TSaveCheckRec IO_CFG_Flag;
            public TSaveCheckRec CTRL_PARAM_Flag;
            public TSaveCheckRec DRIVE_PARAM_Flag;
            public TSaveCheckRec FEED_PARAM_Flag;
            public TSaveCheckRec RACK_CFG_Flag_Reserved;
            public TSaveCheckRec STATION_CFG_Flag;
            public TSaveCheckRec POSITION_CFG_Flag;
            public TSaveCheckRec AREASPEED_CFG_Flag;
            public byte reserved;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class TRTV_ToTalFile
        {
            private string myFileName;

            private TRTVToTalFile_Header Header;
            public byte[] MCU_CFG_DATA;
            public byte[] IO_CFG_DATA;
            public byte[] CTRL_PARAM_DATA;
            public byte[] DRIVE_PARAM_DATA;
            public byte[] FEED_PARAM_DATA;
            public byte[] RACK_CFG_DATA_Reserved;
            public byte[] STATION_CFG_DATA;
            public byte[] POSITION_CFG_DATA;
            public byte[] AREASPEED_CFG_DATA;


            private static ushort HeaderLen = (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TRTVToTalFile_Header));
            private static ushort DataLen_1 = 100; //MCU_CFG_DATAv
            private static ushort DataLen_2 = 1000; //IO_CFG_DATA
            private static ushort DataLen_3 = 1500; //CTRL_PARAM_DATA
            private static ushort DataLen_4 = 1500; //DRIVE_PARAM_DATA
            private static ushort DataLen_5 = 1500; //FEED_PARAM_DATA
            private static ushort DataLen_6 = 500; //RACK_CFG_DATA_Reserved
            private static ushort DataLen_7 = 2000; //STATION_CFG_DATA
            private static ushort DataLen_8 = 2000; //POSITION_CFG_DATA
            private static ushort DataLen_9 = 2000; //ROUND_CFG__DATA

            public TRTV_ToTalFile(string TmpFileName)
            {
                myFileName = TmpFileName;
                MCU_CFG_DATA = new byte[DataLen_1];
                IO_CFG_DATA = new byte[DataLen_2];
                CTRL_PARAM_DATA = new byte[DataLen_3];
                DRIVE_PARAM_DATA = new byte[DataLen_4];
                FEED_PARAM_DATA = new byte[DataLen_5];
                RACK_CFG_DATA_Reserved = new byte[DataLen_6];
                STATION_CFG_DATA = new byte[DataLen_7];
                POSITION_CFG_DATA = new byte[DataLen_8];
                AREASPEED_CFG_DATA = new byte[DataLen_9];
            }

            public TRTV_ToTalFile()
            {
                myFileName = "";
                MCU_CFG_DATA = new byte[DataLen_1];
                IO_CFG_DATA = new byte[DataLen_2];
                CTRL_PARAM_DATA = new byte[DataLen_3];
                DRIVE_PARAM_DATA = new byte[DataLen_4];
                FEED_PARAM_DATA = new byte[DataLen_5];
                RACK_CFG_DATA_Reserved = new byte[DataLen_6];
                STATION_CFG_DATA = new byte[DataLen_7];
                POSITION_CFG_DATA = new byte[DataLen_8];
                AREASPEED_CFG_DATA = new byte[DataLen_9];
            }

            public string FileName
            {
                get { return myFileName; }
                set { myFileName = value; }
            }

            public TRTVToTalFile_Header myHeader
            {
                get { return Header; }
            }

            private bool createFile(byte TmpFileType)
            {
                if (myFileName == "") return false;


                UInt16 SumLen = (UInt16)(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6 + DataLen_7 + DataLen_8 + DataLen_9);

                using (BinaryWriter br = new BinaryWriter(File.Open(myFileName, FileMode.Create, FileAccess.Write)))
                {
                    try
                    {
                        br.Seek(0, SeekOrigin.Begin);
                        byte[] Savebytes = new byte[SumLen];
                        Global_Class.UTIL_ByteArray_clear(Savebytes, SumLen);
                        Savebytes[0] = TmpFileType;
                        br.Write(Savebytes);
                    }
                    finally
                    {
                        br.Close();
                    }
                }
                return true;
            }

            public bool Read_Header(byte TmpFileType)
            {
                Header.MCU_CFG_Flag.Save_Flag = 0;
                Header.IO_CFG_Flag.Save_Flag = 0;
                Header.CTRL_PARAM_Flag.Save_Flag = 0;
                Header.DRIVE_PARAM_Flag.Save_Flag = 0;
                Header.FEED_PARAM_Flag.Save_Flag = 0;
                Header.RACK_CFG_Flag_Reserved.Save_Flag = 0;
                Header.STATION_CFG_Flag.Save_Flag = 0;
                Header.POSITION_CFG_Flag.Save_Flag = 0;
                Header.AREASPEED_CFG_Flag.Save_Flag = 0;


                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);

                        if (TmpFileType == Savebytes[0])
                        {
                            Header = (VEXI_DEFS.TRTVToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TRTVToTalFile_Header));
                            TmpResult = true;
                        }
                        else
                        {
                            TmpResult = false;
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Read_MCU_CFG(ref TRTV_DevConfigCtrl rtv_cfg)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TRTVToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TRTVToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_RTV)
                        {
                            if (Header.MCU_CFG_Flag.Save_Flag == 1)
                            {
                                Savebytes = brRead.ReadBytes(DataLen_1);
                                rtv_cfg = (VEXI_DEFS.TRTV_DevConfigCtrl)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TRTV_DevConfigCtrl));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }


            public bool Write_MCU_CFG(TRTV_DevConfigCtrl rtv_cfg)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_RTV);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_RTV);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_DevConfigCtrl))];
                        Global_Class.UTIL_StructObjectToByteArray(rtv_cfg, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }
                }
                return true;
            }

            public bool Read_IO_CFG(ref RTV_IOConfig IO_cfg)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TRTVToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TRTVToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_RTV)
                        {
                            if (Header.IO_CFG_Flag.Save_Flag == 1)
                            {
                                brRead.ReadBytes(DataLen_1);
                                Savebytes = brRead.ReadBytes(DataLen_2);
                                IO_cfg = (VEXI_DEFS.RTV_IOConfig)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.RTV_IOConfig));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_IO_CFG(RTV_IOConfig IO_CFG)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_RTV);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_RTV);
                        brWrite.Seek(9 * 1 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1, SeekOrigin.Begin);
                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.RTV_IOConfig))];
                        Global_Class.UTIL_StructObjectToByteArray(IO_CFG, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_RTV_CTRL_PARAM(ref TRTV_CTRLParamCTRL CTRL_PARAM)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {

                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TRTVToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TRTVToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_RTV)
                        {
                            if (Header.CTRL_PARAM_Flag.Save_Flag == 1)
                            {
                                //MCU_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                Savebytes = brRead.ReadBytes(DataLen_3);
                                CTRL_PARAM = (VEXI_DEFS.TRTV_CTRLParamCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TRTV_CTRLParamCTRL));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_RTV_CTRL_PARAM(TRTV_CTRLParamCTRL CTRL_PARAM)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_RTV);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_RTV);
                        brWrite.Seek(9 * 2 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_CTRLParamCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(CTRL_PARAM, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_RTV_DRIVE_PARAM(ref TRTV_DriveParamCTRL DRIVE_PARAM)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TRTVToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TRTVToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_RTV)
                        {
                            if (Header.DRIVE_PARAM_Flag.Save_Flag == 1)
                            {
                                //DEV_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                Savebytes = brRead.ReadBytes(DataLen_4);
                                DRIVE_PARAM = (VEXI_DEFS.TRTV_DriveParamCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TRTV_DriveParamCTRL));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }


            public bool Write_RTV_DRIVE_PARAM(TRTV_DriveParamCTRL DRIVE_PARAM)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_RTV);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_RTV);
                        brWrite.Seek(9 * 3 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_DriveParamCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(DRIVE_PARAM, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_RTV_FeedParamCTRL(ref TRTV_FeedParamCTRL FeedParamCTRL)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TRTVToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TRTVToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_RTV)
                        {
                            if (Header.FEED_PARAM_Flag.Save_Flag == 1)
                            {
                                //DEV_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                Savebytes = brRead.ReadBytes(DataLen_5);
                                FeedParamCTRL = (VEXI_DEFS.TRTV_FeedParamCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TRTV_FeedParamCTRL));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_RTV_FeedParamCTRL(TRTV_FeedParamCTRL FeedParamCTRL)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_RTV);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_RTV);
                        brWrite.Seek(9 * 4 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_FeedParamCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(FeedParamCTRL, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_RTV_StationParam(ref TRTV_StationParam StationParam)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TRTVToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TRTVToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_RTV)
                        {
                            if (Header.STATION_CFG_Flag.Save_Flag == 1)
                            {
                                //MCU_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                //FEED_PARAM 영역
                                brRead.ReadBytes(DataLen_5);
                                //RACK_CFG
                                brRead.ReadBytes(DataLen_6);
                                Savebytes = brRead.ReadBytes(DataLen_7);
                                StationParam = (VEXI_DEFS.TRTV_StationParam)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TRTV_StationParam));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_RTV_StationParam(TRTV_StationParam StationParam)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_RTV);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_RTV);
                        brWrite.Seek(9 * 6 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_StationParam))];
                        Global_Class.UTIL_StructObjectToByteArray(StationParam, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_RTV_PositionParam(ref TRTV_PositionSetParam PositionParam)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {

                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TRTVToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TRTVToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_RTV)
                        {
                            if (Header.POSITION_CFG_Flag.Save_Flag == 1)
                            {
                                //MCU_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                //FEED_PARAM 영역
                                brRead.ReadBytes(DataLen_5);
                                //RACK_CFG
                                brRead.ReadBytes(DataLen_6);
                                //STATION_CFG 영역
                                brRead.ReadBytes(DataLen_7);
                                Savebytes = brRead.ReadBytes(DataLen_8);
                                PositionParam = (VEXI_DEFS.TRTV_PositionSetParam)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TRTV_PositionSetParam));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_RTV_PositionParam(TRTV_PositionSetParam PositionParam)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_RTV);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_RTV);
                        brWrite.Seek(9 * 7 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6 + DataLen_7, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_PositionSetParam))];
                        Global_Class.UTIL_StructObjectToByteArray(PositionParam, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }



            public bool Read_RTV_SpeedAreaParam(ref TRTV_SpeedAreaGroupParam SpeedAreaParam)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TRTVToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TRTVToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_RTV)
                        {
                            if (Header.AREASPEED_CFG_Flag.Save_Flag == 1)
                            {
                                //MCU_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                //FEED_PARAM 영역
                                brRead.ReadBytes(DataLen_5);
                                //RACK_CFG
                                brRead.ReadBytes(DataLen_6);
                                //Station_CFG
                                brRead.ReadBytes(DataLen_7);
                                //POSTIOM_CFG
                                brRead.ReadBytes(DataLen_8);
                                Savebytes = brRead.ReadBytes(DataLen_9);
                                SpeedAreaParam = (VEXI_DEFS.TRTV_SpeedAreaGroupParam)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TRTV_SpeedAreaGroupParam));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_RTV_SpeedAreaParam(TRTV_SpeedAreaGroupParam SpeedAreaParam)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_RTV);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_RTV);
                        brWrite.Seek(9 * 8 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6 + DataLen_7 + DataLen_8, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_SpeedAreaGroupParam))];
                        Global_Class.UTIL_StructObjectToByteArray(SpeedAreaParam, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TEMSToTalFile_Header
        {
            public byte fileType;
            public TSaveCheckRec MCU_CFG_Flag;
            public TSaveCheckRec IO_CFG_Flag;
            public TSaveCheckRec CTRL_PARAM_Flag;
            public TSaveCheckRec DRIVE_PARAM_Flag;
            public TSaveCheckRec LIFT_PARAM_Flag;
            public TSaveCheckRec RACK_CFG_Flag_Reserved;
            public TSaveCheckRec STATION_CFG_Flag;
            public TSaveCheckRec POSITION_CFG_Flag;
            public TSaveCheckRec AREASPEED_CFG_Flag;
            public byte reserved;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class TEMS_ToTalFile
        {
            private string myFileName;

            private TEMSToTalFile_Header Header;
            public byte[] MCU_CFG_DATA;
            public byte[] IO_CFG_DATA;
            public byte[] CTRL_PARAM_DATA;
            public byte[] DRIVE_PARAM_DATA;
            public byte[] LIFT_PARAM_DATA;
            public byte[] RACK_CFG_DATA_Reserved;
            public byte[] STATION_CFG_DATA;
            public byte[] POSITION_CFG_DATA;
            public byte[] AREASPEED_CFG_DATA;


            private static ushort HeaderLen = (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TRTVToTalFile_Header));
            private static ushort DataLen_1 = 100; //MCU_CFG_DATAv
            private static ushort DataLen_2 = 1000; //IO_CFG_DATA
            private static ushort DataLen_3 = 1500; //CTRL_PARAM_DATA
            private static ushort DataLen_4 = 1500; //DRIVE_PARAM_DATA
            private static ushort DataLen_5 = 1500; //LIFT_PARAM_DATA
            private static ushort DataLen_6 = 500; //RACK_CFG_DATA_Reserved
            private static ushort DataLen_7 = 2000; //STATION_CFG_DATA
            private static ushort DataLen_8 = 2000; //POSITION_CFG_DATA
            private static ushort DataLen_9 = 2000; //ROUND_CFG__DATA


            public TEMS_ToTalFile(string TmpFileName)
            {
                myFileName = TmpFileName;
                MCU_CFG_DATA = new byte[DataLen_1];
                IO_CFG_DATA = new byte[DataLen_2];
                CTRL_PARAM_DATA = new byte[DataLen_3];
                DRIVE_PARAM_DATA = new byte[DataLen_4];
                LIFT_PARAM_DATA = new byte[DataLen_5];
                RACK_CFG_DATA_Reserved = new byte[DataLen_6];
                STATION_CFG_DATA = new byte[DataLen_7];
                POSITION_CFG_DATA = new byte[DataLen_8];
                AREASPEED_CFG_DATA = new byte[DataLen_9];
            }

            public TEMS_ToTalFile()
            {
                myFileName = "";
                MCU_CFG_DATA = new byte[DataLen_1];
                IO_CFG_DATA = new byte[DataLen_2];
                CTRL_PARAM_DATA = new byte[DataLen_3];
                DRIVE_PARAM_DATA = new byte[DataLen_4];
                LIFT_PARAM_DATA = new byte[DataLen_5];
                RACK_CFG_DATA_Reserved = new byte[DataLen_6];
                STATION_CFG_DATA = new byte[DataLen_7];
                POSITION_CFG_DATA = new byte[DataLen_8];
                AREASPEED_CFG_DATA = new byte[DataLen_9];
            }

            public string FileName
            {
                get { return myFileName; }
                set { myFileName = value; }
            }

            public TEMSToTalFile_Header myHeader
            {
                get { return Header; }
            }

            private bool createFile(byte TmpFileType)
            {
                if (myFileName == "") return false;


                UInt16 SumLen = (UInt16)(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6 + DataLen_7 + DataLen_8 + DataLen_9);

                using (BinaryWriter br = new BinaryWriter(File.Open(myFileName, FileMode.Create, FileAccess.Write)))
                {
                    try
                    {
                        br.Seek(0, SeekOrigin.Begin);
                        byte[] Savebytes = new byte[SumLen];
                        Global_Class.UTIL_ByteArray_clear(Savebytes, SumLen);
                        Savebytes[0] = TmpFileType;
                        br.Write(Savebytes);
                    }
                    finally
                    {
                        br.Close();
                    }
                }
                return true;
            }

            public bool Read_Header(byte TmpFileType)
            {
                Header.MCU_CFG_Flag.Save_Flag = 0;
                Header.IO_CFG_Flag.Save_Flag = 0;
                Header.CTRL_PARAM_Flag.Save_Flag = 0;
                Header.DRIVE_PARAM_Flag.Save_Flag = 0;
                Header.LIFT_PARAM_Flag.Save_Flag = 0;
                Header.RACK_CFG_Flag_Reserved.Save_Flag = 0;
                Header.STATION_CFG_Flag.Save_Flag = 0;
                Header.POSITION_CFG_Flag.Save_Flag = 0;
                Header.AREASPEED_CFG_Flag.Save_Flag = 0;


                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);

                        if (TmpFileType == Savebytes[0])
                        {
                            Header = (VEXI_DEFS.TEMSToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TEMSToTalFile_Header));
                            TmpResult = true;
                        }
                        else
                        {
                            TmpResult = false;
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Read_MCU_CFG(ref TEMS_DevConfigCtrl ems_cfg)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TEMSToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TEMSToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_EMS)
                        {
                            if (Header.MCU_CFG_Flag.Save_Flag == 1)
                            {
                                Savebytes = brRead.ReadBytes(DataLen_1);
                                ems_cfg = (VEXI_DEFS.TEMS_DevConfigCtrl)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TEMS_DevConfigCtrl));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }


            public bool Write_MCU_CFG(TEMS_DevConfigCtrl ems_cfg)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_EMS);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_EMS);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_DevConfigCtrl))];
                        Global_Class.UTIL_StructObjectToByteArray(ems_cfg, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }
                }
                return true;
            }

            public bool Read_IO_CFG(ref EMS_IOConfig IO_cfg)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TEMSToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TEMSToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_EMS)
                        {
                            if (Header.IO_CFG_Flag.Save_Flag == 1)
                            {
                                brRead.ReadBytes(DataLen_1);
                                Savebytes = brRead.ReadBytes(DataLen_2);
                                IO_cfg = (VEXI_DEFS.EMS_IOConfig)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.EMS_IOConfig));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_IO_CFG(EMS_IOConfig IO_CFG)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_EMS);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_EMS);
                        brWrite.Seek(9 * 1 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1, SeekOrigin.Begin);
                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.EMS_IOConfig))];
                        Global_Class.UTIL_StructObjectToByteArray(IO_CFG, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_EMS_CTRL_PARAM(ref TEMS_CTRLParamCTRL CTRL_PARAM)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {

                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TEMSToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TEMSToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_EMS)
                        {
                            if (Header.CTRL_PARAM_Flag.Save_Flag == 1)
                            {
                                //MCU_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                Savebytes = brRead.ReadBytes(DataLen_3);
                                CTRL_PARAM = (VEXI_DEFS.TEMS_CTRLParamCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TEMS_CTRLParamCTRL));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_EMS_CTRL_PARAM(TEMS_CTRLParamCTRL CTRL_PARAM)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_EMS);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_EMS);
                        brWrite.Seek(9 * 2 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_CTRLParamCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(CTRL_PARAM, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_EMS_DRIVE_PARAM(ref TEMS_DriveParamCTRL DRIVE_PARAM)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TEMSToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TEMSToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_EMS)
                        {
                            if (Header.DRIVE_PARAM_Flag.Save_Flag == 1)
                            {
                                //DEV_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                Savebytes = brRead.ReadBytes(DataLen_4);
                                DRIVE_PARAM = (VEXI_DEFS.TEMS_DriveParamCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TEMS_DriveParamCTRL));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }


            public bool Write_EMS_DRIVE_PARAM(TEMS_DriveParamCTRL DRIVE_PARAM)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_EMS);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_EMS);
                        brWrite.Seek(9 * 3 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_DriveParamCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(DRIVE_PARAM, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_EMS_LIFT_PARAM(ref TEMS_LiftParamCTRL LiftParamCTRL)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TEMSToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TEMSToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_EMS)
                        {
                            if (Header.LIFT_PARAM_Flag.Save_Flag == 1)
                            {
                                //DEV_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                Savebytes = brRead.ReadBytes(DataLen_5);
                                LiftParamCTRL = (VEXI_DEFS.TEMS_LiftParamCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TEMS_LiftParamCTRL));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_EMS_LIFT_PARAM(TEMS_LiftParamCTRL LiftParamCTRL)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_EMS);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_EMS);
                        brWrite.Seek(9 * 4 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_LiftParamCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(LiftParamCTRL, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_EMS_StationParam(ref TEMS_StationParam StationParam)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TEMSToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TEMSToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_EMS)
                        {
                            if (Header.STATION_CFG_Flag.Save_Flag == 1)
                            {
                                //MCU_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                //LIFT_PARAM 영역
                                brRead.ReadBytes(DataLen_5);
                                //RACK_CFG
                                brRead.ReadBytes(DataLen_6);
                                Savebytes = brRead.ReadBytes(DataLen_7);
                                StationParam = (VEXI_DEFS.TEMS_StationParam)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TEMS_StationParam));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_EMS_StationParam(TEMS_StationParam StationParam)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_EMS);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_EMS);
                        brWrite.Seek(9 * 6 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_StationParam))];
                        Global_Class.UTIL_StructObjectToByteArray(StationParam, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

            public bool Read_EMS_PositionParam(ref TEMS_PositionSetParam PositionParam)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {

                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TEMSToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TEMSToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_EMS)
                        {
                            if (Header.POSITION_CFG_Flag.Save_Flag == 1)
                            {
                                //MCU_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                //LIFT_PARAM 영역
                                brRead.ReadBytes(DataLen_5);
                                //RACK_CFG
                                brRead.ReadBytes(DataLen_6);
                                //STATION_CFG 영역
                                brRead.ReadBytes(DataLen_7);
                                Savebytes = brRead.ReadBytes(DataLen_8);
                                PositionParam = (VEXI_DEFS.TEMS_PositionSetParam)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TEMS_PositionSetParam));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_EMS_PositionParam(TEMS_PositionSetParam PositionParam)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_EMS);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_EMS);
                        brWrite.Seek(9 * 7 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6 + DataLen_7, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_PositionSetParam))];
                        Global_Class.UTIL_StructObjectToByteArray(PositionParam, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }



            public bool Read_EMS_SpeedAreaParam(ref TEMS_SpeedAreaGroupParam SpeedAreaParam)
            {
                if (myFileName == "") return false;

                bool TmpResult = false;
                if (!File.Exists(myFileName))
                {
                    return false;
                }

                using (BinaryReader brRead = new BinaryReader(File.Open(myFileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        byte[] Savebytes = brRead.ReadBytes(HeaderLen);
                        Header = (VEXI_DEFS.TEMSToTalFile_Header)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TEMSToTalFile_Header));

                        if (Header.fileType == ConstClass.TYPE_EMS)
                        {
                            if (Header.AREASPEED_CFG_Flag.Save_Flag == 1)
                            {
                                //MCU_CFG 영역
                                brRead.ReadBytes(DataLen_1);
                                //IO_CFG 영역
                                brRead.ReadBytes(DataLen_2);
                                //CTRL_PARAM 영역
                                brRead.ReadBytes(DataLen_3);
                                //DRIVE_PARAM 영역
                                brRead.ReadBytes(DataLen_4);
                                //LIFT_PARAM 영역
                                brRead.ReadBytes(DataLen_5);
                                //RACK_CFG
                                brRead.ReadBytes(DataLen_6);
                                //Station_CFG
                                brRead.ReadBytes(DataLen_7);
                                //POSTIOM_CFG
                                brRead.ReadBytes(DataLen_8);
                                Savebytes = brRead.ReadBytes(DataLen_9);
                                SpeedAreaParam = (VEXI_DEFS.TEMS_SpeedAreaGroupParam)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TEMS_SpeedAreaGroupParam));
                                TmpResult = true;
                            }
                        }
                    }
                    finally
                    {
                        brRead.Close();

                    }
                    return TmpResult;
                }
            }

            public bool Write_EMS_SpeedAreaParam(TEMS_SpeedAreaGroupParam SpeedAreaParam)
            {
                if (myFileName == "") return false;

                if (!File.Exists(myFileName))
                {
                    createFile(ConstClass.TYPE_EMS);
                }

                using (BinaryWriter brWrite = new BinaryWriter(File.Open(myFileName, FileMode.Open, FileAccess.Write)))
                {
                    try
                    {
                        brWrite.Seek(0, SeekOrigin.Begin);
                        brWrite.Write(ConstClass.TYPE_EMS);
                        brWrite.Seek(9 * 8 + 1, SeekOrigin.Begin);
                        brWrite.Write((byte)0x01);
                        brWrite.Write(DateTime.Now.Ticks);
                        brWrite.Seek(HeaderLen + DataLen_1 + DataLen_2 + DataLen_3 + DataLen_4 + DataLen_5 + DataLen_6 + DataLen_7 + DataLen_8, SeekOrigin.Begin);

                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_SpeedAreaGroupParam))];
                        Global_Class.UTIL_StructObjectToByteArray(SpeedAreaParam, Savebytes);
                        brWrite.Write(Savebytes);
                    }
                    finally
                    {
                        brWrite.Close();
                    }

                }
                return true;
            }

        }
        #endregion
    }


}

using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;   //DllImport
using System.Text;
using System.Windows.Forms;


//[Global_Class]
//프로그램 작성에 유용한 UTIL 함수들이 구성된 클래스
//고정버퍼, 포인트 사용으로 unsafe로 선언
//함수 사용은 Static인 경우 Global_Class 로 호출, static인 아닌 경우 Global_Class 객체로 호출하여 사용하여야 한다.
namespace VEXI
{

    unsafe public class Global_Class
    {

        static ushort[] crc16Tbl =
        {
        0x0000,0x1021,0x2042,0x3063,0x4084,0x50A5,0x60C6,0x70E7,0x8108,0x9129,0xA14A,0xB16B,0xC18C,0xD1AD,0xE1CE,0xF1EF,0x1231,0x0210,0x3273,

        0x2252,0x52B5,0x4294,0x72F7,0x62D6,0x9339,0x8318,0xB37B,0xA35A,0xD3BD,0xC39C,0xF3FF,0xE3DE,0x2462,0x3443,0x0420,0x1401,0x64E6,0x74C7,

        0x44A4,0x5485,0xA56A,0xB54B,0x8528,0x9509,0xE5EE,0xF5CF,0xC5AC,0xD58D,0x3653,0x2672,0x1611,0x0630,0x76D7,0x66F6,0x5695,0x46B4,0xB75B,

        0xA77A,0x9719,0x8738,0xF7DF,0xE7FE,0xD79D,0xC7BC,0x48C4,0x58E5,0x6886,0x78A7,0x0840,0x1861,0x2802,0x3823,0xC9CC,0xD9ED,0xE98E,0xF9AF,

        0x8948,0x9969,0xA90A,0xB92B,0x5AF5,0x4AD4,0x7AB7,0x6A96,0x1A71,0x0A50,0x3A33,0x2A12,0xDBFD,0xCBDC,0xFBBF,0xEB9E,0x9B79,0x8B58,0xBB3B,

        0xAB1A,0x6CA6,0x7C87,0x4CE4,0x5CC5,0x2C22,0x3C03,0x0C60,0x1C41,0xEDAE,0xFD8F,0xCDEC,0xDDCD,0xAD2A,0xBD0B,0x8D68,0x9D49,0x7E97,0x6EB6,

        0x5ED5,0x4EF4,0x3E13,0x2E32,0x1E51,0x0E70,0xFF9F,0xEFBE,0xDFDD,0xCFFC,0xBF1B,0xAF3A,0x9F59,0x8F78,0x9188,0x81A9,0xB1CA,0xA1EB,0xD10C,

        0xC12D,0xF14E,0xE16F,0x1080,0x00A1,0x30C2,0x20E3,0x5004,0x4025,0x7046,0x6067,0x83B9,0x9398,0xA3FB,0xB3DA,0xC33D,0xD31C,0xE37F,0xF35E,

        0x02B1,0x1290,0x22F3,0x32D2,0x4235,0x5214,0x6277,0x7256,0xB5EA,0xA5CB,0x95A8,0x8589,0xF56E,0xE54F,0xD52C,0xC50D,0x34E2,0x24C3,0x14A0,

        0x0481,0x7466,0x6447,0x5424,0x4405,0xA7DB,0xB7FA,0x8799,0x97B8,0xE75F,0xF77E,0xC71D,0xD73C,0x26D3,0x36F2,0x0691,0x16B0,0x6657,0x7676,

        0x4615,0x5634,0xD94C,0xC96D,0xF90E,0xE92F,0x99C8,0x89E9,0xB98A,0xA9AB,0x5844,0x4865,0x7806,0x6827,0x18C0,0x08E1,0x3882,0x28A3,0xCB7D,

        0xDB5C,0xEB3F,0xFB1E,0x8BF9,0x9BD8,0xABBB,0xBB9A,0x4A75,0x5A54,0x6A37,0x7A16,0x0AF1,0x1AD0,0x2AB3,0x3A92,0xFD2E,0xED0F,0xDD6C,0xCD4D,

        0xBDAA,0xAD8B,0x9DE8,0x8DC9,0x7C26,0x6C07,0x5C64,0x4C45,0x3CA2,0x2C83,0x1CE0,0x0CC1,0xEF1F,0xFF3E,0xCF5D,0xDF7C,0xAF9B,0xBFBA,0x8FD9,

        0x9FF8,0x6E17,0x7E36,0x4E55,0x5E74,0x2E93,0x3EB2,0x0ED1,0x1EF0

        };

        public string RootDIR;
        public System.Windows.Forms.Form owner;
        public static DateTime epochStart = new DateTime(1970, 1, 1);


        //**************************************************
        #region 알람명 함수
        public static string UTIL_EMSWarnningName(byte Code1, byte Code2, ushort Code3)
        {
            string WarnningName_Text = "";

            return WarnningName_Text;
        }

        public static string UTIL_RTVWarnningName(byte Code1, byte Code2, ushort Code3)
        {
            string WarnningName_Text = "";

            return WarnningName_Text;
        }

        public static string UTIL_SRMWarnningName(byte Code1, byte Code2, ushort Code3)
        {
            string WarnningName_Text = "";
            UInt16 SumCode = (UInt16)(Code1 * 100 + Code2);

            switch (SumCode)
            {
                case 101: WarnningName_Text = "주행 금지 -포크 중심 아님"; break;
                case 102: WarnningName_Text = "주행 금지 -포크 중심 아님"; break;
                case 103: WarnningName_Text = "주행 금지 -포크 중심 아님"; break;
                case 104: WarnningName_Text = "주행 금지 -포크 좌측 진출"; break;
                case 105: WarnningName_Text = "주행 금지 -포크 좌측 진출"; break;
                case 106: WarnningName_Text = "주행 금지 -포크 우측 진출"; break;
                case 107: WarnningName_Text = "주행 금지 -포크 우측 진출"; break;
                case 108: WarnningName_Text = "주행 금지 -시작 위치 도달"; break;
                case 109: WarnningName_Text = "주행 금지 -끝 위치 도달"; break;
                case 110: WarnningName_Text = "주행 금지 -방향 반대"; break;

                case 201: WarnningName_Text = "승강 금지 - 포크 중심 아님"; break;
                case 202: WarnningName_Text = "승강 금지 -포크 중심 아님"; break;
                case 203: WarnningName_Text = "승강 금지 -포크 중심 아님"; break;
                case 204: WarnningName_Text = "승강 금지 -포크 좌측 진출"; break;
                case 205: WarnningName_Text = "승강 금지 -포크 좌측 진출"; break;
                case 206: WarnningName_Text = "승강 금지 -포크 우측 진출"; break;
                case 207: WarnningName_Text = "승강 금지 -포크 우측 진출"; break;
                case 208: WarnningName_Text = "승강 금지 -시작 위치 도달"; break;
                case 209: WarnningName_Text = "승강 금지 -끝 위치 도달"; break;
                case 210: WarnningName_Text = "승강 금지 -방향 반대"; break;
                case 211: WarnningName_Text = "승강 금지 - 포크 좌 정위치 아님"; break;
                case 212: WarnningName_Text = "승강 금지 - 포크 우 정위치 아님"; break;
                case 213: WarnningName_Text = "승강 금지 - 포크 좌 Harf 정위치 아님"; break;
                case 214: WarnningName_Text = "승강 금지 -포크 좌 Full 정위치 아님"; break;
                case 215: WarnningName_Text = "승강 금지 -포크 우 Harf 정위치 아님"; break;
                case 216: WarnningName_Text = "승강 금지 -포크 우 Full 정위치 아님"; break;

                case 301: WarnningName_Text = "포크 구동 금지 - 주행 정위치 아님"; break;
                case 302: WarnningName_Text = "포크 구동 금지 - 승강 정위치 아님"; break;
                case 303: WarnningName_Text = "포크 구동 금지 - 랙 간섭"; break;
                case 304: WarnningName_Text = "포크 구동 금지 -이중입고"; break;
                case 305: WarnningName_Text = "포크 구동 금지 - 이중입고"; break;
                case 306: WarnningName_Text = "포크 구동 금지 - 이중입고"; break;
                case 307: WarnningName_Text = "포크 구동 금지 - 선입품감지"; break;
                case 308: WarnningName_Text = "포크 구동 금지 - 좌측 끝위치 도달"; break;
                case 309: WarnningName_Text = "포크 구동 금지 - 우측 끝위치 도달"; break;
                case 310: WarnningName_Text = "포크 구동 금지 - 방향 반대"; break;
                case 311: WarnningName_Text = "포크 구동 금지 -금지랙"; break;
                case 312: WarnningName_Text = "포크 구동 금지 - 스페셜랙"; break;
                case 313: WarnningName_Text = "포크 구동 금지 - 인터락이상"; break;

                case 1001: WarnningName_Text = "자동모드 실패 -포크 중심 아님"; break;
                case 1002: WarnningName_Text = "자동모드 실패 -포크 중심 아님"; break;
                case 1003: WarnningName_Text = "자동모드 실패 -포크 중심 아님"; break;
                case 1004: WarnningName_Text = "자동모드 실패 -포크 좌측 진출"; break;
                case 1005: WarnningName_Text = "자동모드 실패 -포크 좌측 진출"; break;
                case 1006: WarnningName_Text = "자동모드 실패 -포크 우측 진출"; break;
                case 1007: WarnningName_Text = "자동모드 실패 -포크 우측 진출"; break;
            }

            return WarnningName_Text;
        }


        public static string UTIL_WarnningName(byte DevType, byte Code1, byte Code2, ushort Code3)
        {
            return "";
        }

        public static string UTIL_EMSAlarmName(byte Code1, byte Code2, ushort Code3)
        {
            string AlarmName_Text = "";

            return AlarmName_Text;
        }
        public static string UTIL_RTVAlarmName(byte Code1, byte Code2, ushort Code3)
        {
            string AlarmName_Text = Code1.ToString() + "-" + Code2.ToString() + "-" + Code3.ToString();

            UInt16 SumCode = (UInt16)(Code1 * 100 + Code2);

            switch (SumCode)
            {
                case 1:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "보드 리셋[저전압]"; break;
                        case 3: AlarmName_Text = "보드 리셋[Watchdog]"; break;
                        case 4: AlarmName_Text = "보드 리셋[소프트웨어]"; break;
                        case 5: AlarmName_Text = "보드 리셋[전원 차단]"; break;
                        case 6: AlarmName_Text = "보드 리셋[하드웨어]"; break;
                    }
                    break; 

                case 102: AlarmName_Text = "비상 정지"; break;
                case 104: AlarmName_Text = "안전플러그 동작"; break;
                case 201: AlarmName_Text = "전방 범퍼 동작"; break;
                case 202: AlarmName_Text = "후방 범퍼 동작"; break;
                case 203: AlarmName_Text = "전방, 후방 범퍼 동작"; break;


                case 301:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "이동지령중 좌측화물이탈"; break;
                        case 2: AlarmName_Text = "이동지령중 우측화물이탈"; break;
                    }
                    break;

                case 302:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "화물적재 후 좌측화물이탈"; break;
                        case 2: AlarmName_Text = "화물적재 후 우측화물이탈"; break;
                    }
                    break;

                case 303:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "화물이재 후 좌측화물이탈"; break;
                        case 2: AlarmName_Text = "화물이재 후 우측화물이탈"; break;
                    }
                    break;
                case 304:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "화물이재 전 좌측화물이탈"; break;
                        case 2: AlarmName_Text = "화물이재 전 우측화물이탈"; break;
                    }
                    break;
                case 305:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "화물이재 전 좌측화물이탈"; break;
                        case 2: AlarmName_Text = "화물이재 전 우측화물이탈"; break;
                    }
                    break;

                case 501: AlarmName_Text = "위치센서 에러"; break;
                case 502: AlarmName_Text = "위치센서 거리급변"; break;
                case 601: AlarmName_Text = "인버터1 이상(인버터 알람)"; break;
                case 602: AlarmName_Text = "인버터1 이상(인버터 구동 실패)"; break;
                case 603: AlarmName_Text = "인버터1 이상(인버터 구동 실패)"; break;
                case 604: AlarmName_Text = "인버터1 이상(정지속도 이상)"; break;
                case 605: AlarmName_Text = "인버터1 이상(인버터연결끊김)"; break;

                case 608:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "인버터1 이상 (대차이동 이상) - 시작실패"; break;
                        case 2: AlarmName_Text = "인버터1 이상 (대차이동 이상) - 반대이동"; break;
                    }
                    break;

                case 701: AlarmName_Text = "주행MC / 절체 동작불량"; break;
                case 702: AlarmName_Text = "피딩MC / 절체 동작불량"; break;

                case 801:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "피딩 시간 초과(C / V 인터락 신호 이상)"; break;
                        case 2: AlarmName_Text = "피딩 시간 초과(C / V 이재요구 타임아웃)"; break;
                        case 4: AlarmName_Text = "피딩 시간 초과(C / V 이재가능 타임아웃)"; break;
                    }
                    break;

                case 802:
                    switch (Code3)
                    {
                        case 2: AlarmName_Text = "이재 / 적재 시간 초과(적재 타임아웃)"; break;
                        case 3: AlarmName_Text = "이재 / 적재 시간 초과(C/ V 이재 요구 신호 이상)"; break;
                        case 5: AlarmName_Text = "이재 / 적재 시간 초과(C/ V 이재 완료 타임아웃)"; break;
                    }
                    break;

                case 803: AlarmName_Text = "화물 이재 완료전, 컨베이어 완료 신호 ON"; break;
                case 901: AlarmName_Text = "EtherCAT 통신 설정 이상"; break;
                case 902: AlarmName_Text = "EtherCAT 통신 초기화 실패(Slave ID 이상)"; break;
                case 903: AlarmName_Text = "EtherCAT 슬레이브 개수 이상"; break;
                case 904: AlarmName_Text = "EtherCAT 연결 끊김"; break;
                case 905: AlarmName_Text = "EtherCAT 통신 초기화 실패(Slave Type 이상)"; break;

                case 1001:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "브레이크개방 이상(MC 동작 신호 이상)"; break;
                        case 2: AlarmName_Text = "브레이크개방 이상(브레이크 동작 신호 이상)"; break;
                        case 3: AlarmName_Text = "브레이크개방 이상(수동 주행 브레이크 이상)"; break;
                    }
                    break;

                case 1101:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "정위치 정지 이상(정위치 미달)"; break;
                        case 4: AlarmName_Text = "정위치 정지 이상(정위치 초과)"; break;
                    }
                    break;

                case 1102: AlarmName_Text = "정위치 정지 이상(크립동작 시간 초과)"; break;
                case 1201: AlarmName_Text = "인버터2 이상(인버터알람)"; break;
                case 1204: AlarmName_Text = "인버터2 이상(정지속도 이상)"; break;
                case 1303: AlarmName_Text = "화물 이재 후 화물 감지"; break;
                case 1401: AlarmName_Text = "충돌방지 동작중 전방대차 위치확인 안됨"; break;
                case 1402: AlarmName_Text = "주행 불가능 위치 명령 수신"; break;
                case 1403: AlarmName_Text = "라이더 물체 감지 후 정지 대기 시간 초과"; break;
                case 1404: AlarmName_Text = "주행 정지 센서 감지 정지"; break;
                case 1405:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "감속구간 속도 이상 - 직선"; break;
                        case 2: AlarmName_Text = "감속구간 속도 이상 - 곡선"; break;
                        default: AlarmName_Text = "감속구간 속도 이상"; break;
                    }
                    break;
                case 1406:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "곡선구간 속도 이상"; break;
                        case 2: AlarmName_Text = "주행 제어 이상 - 충돌방지"; break;
                        case 3: AlarmName_Text = "주행 제어 이상 - 전방대차통신두절"; break;
                        case 4: AlarmName_Text = "주행 제어 이상 - 라이다감지"; break;
                    }
                    break;

                case 1501:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "기본 설정 데이터 이상 - 데이터 손상. CRC 오류"; break;
                        case 1: AlarmName_Text = "기본 설정 데이터 이상 - 데이터 기본값 설정"; break;
                        case 2: AlarmName_Text = "기본 설정 데이터 이상 - 백업 설정으로 복구"; break;
                    }
                    break;


                case 1502:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "장치 구조 설정 이상 - 데이터 손상. CRC 오류"; break;
                        case 1: AlarmName_Text = "장치 구조 설정 이상 - 데이터 기본값 설정"; break;
                    }
                    break;

                case 1503:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "DIO 설정 이상 - 데이터 손상. CRC 오류"; break;
                        case 1: AlarmName_Text = "DIO 설정 이상 - 데이터 기본값 설정"; break;
                    }
                    break;

                case 1504:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "제어 설정 이상 - 데이터 손상. CRC 오류"; break;
                        case 1: AlarmName_Text = "제어 설정 이상 - 데이터 기본값 설정"; break;
                    }
                    break;

                case 1505:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "주행 드라이브 설정 이상 - 데이터 손상. CRC 오류"; break;
                        case 1: AlarmName_Text = "주행 드라이브 설정 이상 - 데이터 기본값 설정"; break;
                    }
                    break;

                case 1506:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "피딩 드라이브 설정 이상 - 데이터 손상. CRC 오류"; break;
                        case 1: AlarmName_Text = "피딩 드라이브 설정 이상 - 데이터 기본값 설정"; break;
                    }
                    break;

                case 1507:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "레일 주행 설정 이상 - 데이터 손상. CRC 오류"; break;
                        case 1: AlarmName_Text = "레일 주행 설정 이상 - 데이터 기본값 설정"; break;
                    }
                    break;

                case 1508:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "구간 설정 이상 - 데이터 손상. CRC 오류"; break;
                        case 1: AlarmName_Text = "구간 설정 이상 - 데이터 기본값 설정"; break;
                    }
                    break;

                case 1509:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "스테이션 설정 이상 - 데이터 손상. CRC 오류"; break;
                        case 1: AlarmName_Text = "스테이션 설정 이상 - 데이터 기본값 설정"; break;
                    }
                    break;
            }

            return AlarmName_Text;
        }

        public static string UTIL_SRMAlarmName(byte Code1, byte Code2, ushort Code3)
        {
            string AlarmName_Text = "";
            UInt16 SumCode = (UInt16)(Code1 * 100 + Code2);

            {
                switch (SumCode)
                {
                    case    1: 
                        switch (Code3)
                        {
                            case 1: AlarmName_Text = "보드 리셋(저전압)"; break;
                            case 3: AlarmName_Text = "보드 리셋(Watchdog)"; break;
                            case 4: AlarmName_Text = "보드 리셋(소프트웨어)"; break;
                            case 5: AlarmName_Text = "보드 리셋(전원 차단)"; break;
                            case 6: AlarmName_Text = "보드 리셋(하드웨어)"; break;
                        }
                        break;

                    case  301: AlarmName_Text = "지상반 비상정지"; break;
                    case  302: AlarmName_Text = "WCS 비상정지"; break;

                    case  601: AlarmName_Text = "안전플러그 동작"; break;

                    case 1001: AlarmName_Text = "승강 로프 텐션 이상(전방)"; break;
                    case 1002: AlarmName_Text = "승강 로프 텐션 이상(후방)"; break;
                    case 1003: AlarmName_Text = "조속기 동작 감지"; break;

                    case 1101: AlarmName_Text = "동작 목표값 이상(주행 목표값 초과)"; break;
                    case 1102: AlarmName_Text = "동작 목표값 이상(주행 목표값 미달)"; break;
                    case 1105: AlarmName_Text = "동작 목표값 이상(승강 목표값 초과)"; break;
                    case 1106: AlarmName_Text = "동작 목표값 이상(승강 목표값 미달)"; break;
                    case 1109: AlarmName_Text = "동작 목표값 이상(포크1 목표값 초과)"; break;
                    case 1110: AlarmName_Text = "동작 목표값 이상(포크1 목표값 미달)"; break;

                    case 2001: AlarmName_Text = "화물 가로폭 이탈 이상(포크1 좌측 GWL1)"; break;
                    case 2002: AlarmName_Text = "화물 가로폭 이탈 이상(포크1 우측 GWR1)";break;
                    case 2003: AlarmName_Text = "화물 가로폭 이탈 이상(포크1 좌측 GWL1e)";break;
                    case 2004: AlarmName_Text = "화물 가로폭 이탈 이상(포크1 우측 GWR1e)";break;
                    case 2005: AlarmName_Text = "화물 가로폭 이탈 이상(포크1 좌측 GWL1)";break;
                    case 2006: AlarmName_Text = "화물 가로폭 이탈 이상(포크1 우측 GWR1)";break;
                    case 2007: AlarmName_Text = "화물 가로폭 이탈 이상(포크1 좌측 GWL1)";break;
                    case 2008: AlarmName_Text = "화물 가로폭 이탈 이상(포크1 우측 GWR1)";break;
                    case 2009: AlarmName_Text = "화물 가로폭 이탈 이상(포크1 좌측 GWL1)";break;
                    case 2010: AlarmName_Text = "화물 가로폭 이탈 이상(포크1 우측 GWR1)";break;

                    case 2101: AlarmName_Text = "화물 세로폭 이탈(포크1 전방좌측 GDFL1)"; break;
                    case 2102: AlarmName_Text = "화물 세로폭 이탈(포크1 전방우측 GDFR1)";break;
                    case 2103: AlarmName_Text = "화물 세로폭 이탈(포크1 후방좌측 GDRL1)";break;
                    case 2104: AlarmName_Text = "화물 세로폭 이탈(포크1 후방우측 GDRR1)";break;
                    case 2105: AlarmName_Text = "화물 높이폭 이탈(포크1 좌측 GHL1)";break;
                    case 2106: AlarmName_Text = "화물 높이폭 이탈(포크1 우측 GHR1)";break;

                    
                    case 2301: AlarmName_Text = "스페셜 랙 이상"; break;
                    
                    case 2601: AlarmName_Text = "비상정지 (승강 리미트스위치 동작)"; break;
                    case 2602: AlarmName_Text = "비상정지 (주행 리미트스위치 동작)"; break;

                    case 3303: AlarmName_Text = "포크 센서 이상(포크1 중심 아님)"; break;
                    case 3305: AlarmName_Text = "포크 센서 이상(포크1 승강 전 FEL1 미감지)"; break;
                    case 3307: AlarmName_Text = "포크 센서 이상(포크1 승강 전 FER1 미감지)"; break;

                    case 3309:
                        switch (Code3)
                        {
                            case 1: AlarmName_Text = "포크 센서 이상(포크1 승강 후 FHL1 미감지)"; break;
                            case 2: AlarmName_Text = "포크 센서 이상(포크1 승강 후 FML1 미감지)"; break;
                            case 3: AlarmName_Text = "포크 센서 이상(포크1 승강 후 FEL1 미감지)"; break;
                            case 4: AlarmName_Text = "포크 센서 이상(포크1 승강 후 FHR1 미감지)"; break;
                            case 5: AlarmName_Text = "포크 센서 이상(포크1 승강 후 FMR1 미감지)"; break;
                            case 6: AlarmName_Text = "포크 센서 이상(포크1 승강 후 FER1 미감지)"; break;
                        }
                        break;

                    case 3311:
                        switch (Code3)
                        {
                            case 1: AlarmName_Text = "포크 센서 이상 (포크1 중심 후 FCL1 미감지)"; break;
                            case 2: AlarmName_Text = "포크 센서 이상 (포크1 중심 후, FCR1 미감지)"; break;
                        }
                        break;

                    case 3312: AlarmName_Text = "포크 센서 이상(포크1 좌측 진출)"; break;
                    case 3313: AlarmName_Text = "포크 센서 이상(포크1 우측 진출)"; break;
                    case 3319: AlarmName_Text = "포크 센서 이상(포크1 승강 전 FHL1 미감지)"; break;
                    case 3321: AlarmName_Text = "포크 센서 이상(포크1 승강 전 FHR1 미감지)"; break;
                    case 3323: AlarmName_Text = "포크 센서 이상(포크1 승강 전 FML1 미감지)"; break;
                    case 3325: AlarmName_Text = "포크 센서 이상(포크1 승강 전 FMR1 미감지)"; break;
                    case 3327:
                        switch (Code3)
                        {
                            case 1: AlarmName_Text = "포크 센서 이상(포크1 중심 전 FCL1 감지)"; break;
                            case 2: AlarmName_Text = "포크 센서 이상(포크1 중심 전 FCR1 감지)"; break;
                        }
                        break;

                    case 3801: AlarmName_Text = "승강 감속 센서 이상"; break;
                    case 3901: AlarmName_Text = "주행 감속 센서 이상"; break;

                    case 4401: AlarmName_Text = "포크 인버터 1 이상"; break;
                    case 4402: AlarmName_Text = "포크 인버터 1 이상(외부엔코더)"; break;
                    case 4404: AlarmName_Text = "포크 인버터 1 이상(제어실패)"; break;
                    case 4405: AlarmName_Text = "포크 인버터 1 이상(과부하)"; break;
                    case 4601: AlarmName_Text = "주행 인버터 1 이상"; break;
                    case 4602: AlarmName_Text = "주행 인버터 1 이상(외부엔코더)"; break;
                    case 4604: AlarmName_Text = "주행 인버터 1 이상(제어실패)"; break;
                    case 4605: AlarmName_Text = "주행 인버터 1 이상(과부하)"; break;
                    case 4701: AlarmName_Text = "승강 인버터 이상"; break;
                    case 4702: AlarmName_Text = "승강 인버터 이상(외부엔코더)"; break;
                    case 4704: AlarmName_Text = "승강 인버터 이상(제어실패)"; break;
                    case 4705: AlarmName_Text = "승강 인버터 이상(과부하)"; break;

                    case 5301: AlarmName_Text = "승강 감속 이상 - 상승 감속"; break;
                    case 5302: AlarmName_Text = "승강 감속 이상 - 하강 감속"; break;
                    case 5401: AlarmName_Text = "주행 감속 이상 - 전진 감속"; break;
                    case 5402: AlarmName_Text = "주행 감속 이상 - 후진 감속"; break;

                    case 6001: AlarmName_Text = "좌측 이중입고(포크1 DSTL1 감지)"; break;
                    case 6004: AlarmName_Text = "좌측 이중입고(포크1 DSTL1 감지)"; break;
                    case 6013: AlarmName_Text = "좌측 이중입고(포크1 DSTLR1 감지)"; break;
                    case 6014: AlarmName_Text = "좌측 이중입고(포크1 선입고 화물 감지)"; break;
                    case 6015: AlarmName_Text = "좌측 이중입고(포크1 DSTLe1 감지)"; break;
                    case 6017: AlarmName_Text = "좌측 이중입고(포크1 DSTLe1 감지)"; break;

                    case 6101: AlarmName_Text = "우측 이중입고(포크1 DSTR1 감지)"; break;
                    case 6104: AlarmName_Text = "우측 이중입고(포크1 DSTR1 감지)"; break;
                    case 6113: AlarmName_Text = "우측 이중입고(포크1 DSTRR1 감지)"; break;
                    case 6114: AlarmName_Text = "우측 이중입고(포크1 선입고 화물 감지)"; break;
                    case 6115: AlarmName_Text = "우측 이중입고(포크1 DSTRe1 감지)"; break;
                    case 6117: AlarmName_Text = "우측 이중입고(포크1 DSTRe1 감지)"; break;

                    case 6302: AlarmName_Text = "화물 이상 2(작업 완료 후 화물감지)"; break;
                    case 6303: AlarmName_Text = "화물 이상 2(작업 시작 전 화물감지)"; break;
                    case 6304: AlarmName_Text = "화물 이상 2(목적지 변경 중 화물 미감지)"; break;

                    case 6401: AlarmName_Text = "공출고 / 공입고(포크1 적재완료 후 화물 미감지)"; break;
                    case 6404: AlarmName_Text = "공출고 / 공입고(포크1 적재완료 후 화물 미감지)"; break;

                    case 6501: AlarmName_Text = "LOADED (적재 전/이재완료 후 화물 감지)"; break;

                    case 6601: AlarmName_Text = "작업명령 이상(금지랙)"; break;
                    case 6602: AlarmName_Text = "작업명령 이상(Level 정보 이상)"; break;
                    case 6603: AlarmName_Text = "작업명령 이상(Row 정보 이상)"; break;
                    case 6604: AlarmName_Text = "작업명령 이상(Bay 정보 이상)"; break;
                    case 6605: AlarmName_Text = "작업명령 이상(Station 정보 이상)"; break;
                    case 6606: AlarmName_Text = "작업명령 이상(Station 타입 이상)"; break;
                    case 6607: AlarmName_Text = "작업명령 이상(Station 타입 이상)"; break;

                    case 8001:
                        switch (Code3)
                        {
                            case 1: AlarmName_Text = "브레이크 해제 이상(주행)"; break;
                            case 2: AlarmName_Text = "브레이크 해제 이상(승강)"; break;
                        }
                        break;

                    case 8002: AlarmName_Text = "주행 시간초과 이상"; break;
                    case 8003: AlarmName_Text = "승강 시간초과 이상"; break;
                    case 8004: AlarmName_Text = "승강 시간초과 이상(포크 진출 후 상승)"; break;
                    case 8005: AlarmName_Text = "승강 시간초과 이상(포크 진출 후 하강)"; break;

                    case 8101: AlarmName_Text = "FORKING 시간초과 이상(OUT)"; break;
                    case 8102: AlarmName_Text = "FORKING 시간초과 이상(IN)"; break;
                    case 8103: AlarmName_Text = "브레이크 해제 이상(포크1)"; break;

                    case 8201: AlarmName_Text = "STATION 대기시간 초과 이상"; break;

                    case 8901: AlarmName_Text = "원점확인 이상 - 승강"; break;
                    case 8902: AlarmName_Text = "원점확인 이상 - 주행"; break;
                    case 8903: AlarmName_Text = "원점확인 이상 - 포크"; break;

                    case 9601: AlarmName_Text = "광모뎀 이상"; break;
                    case 9701: AlarmName_Text = "기상반 도어 열림"; break;
                    case 9801: AlarmName_Text = "기상반 비상정지"; break;
                    case 10001 : AlarmName_Text = "인버터 통신 이상"; break;
                    case 10101 : AlarmName_Text = "이더켓 통신 이상(초기화 실패)"; break;
                    case 10102 : AlarmName_Text = "이더켓 통신 이상(연결 개수)"; break;
                    case 10103 : AlarmName_Text = "이더켓 통신 이상(ID 설정)"; break;
                    case 10104 : AlarmName_Text = "이더켓 통신 이상(타입 설정)"; break;
                    case 10110 : AlarmName_Text = "이더켓 통신 이상(연결끊김)"; break;
                    
                    case 10201: AlarmName_Text = "브레이크 전원 MMS트립(주행)"; break;
                    case 10202: AlarmName_Text = "브레이크 전원 MMS트립(승강)"; break;
                    case 10203: AlarmName_Text = "브레이크 전원 MMS트립(포크1)"; break;


                    case 11001: AlarmName_Text = "설정 데이터 이상(장치 기본 정보)"; break;
                    case 11002: AlarmName_Text = "설정 데이터 이상(장치 구조)"; break;
                    case 11003: AlarmName_Text = "설정 데이터 이상(MCU 입출력)"; break;
                    case 11004: AlarmName_Text = "설정 데이터 이상(랙 설정)"; break;
                    case 11005: AlarmName_Text = "설정 데이터 이상(셀 오프셋)"; break;
                    case 11006: AlarmName_Text = "설정 데이터 이상(스테이션)"; break;
                    case 11007: AlarmName_Text = "설정 데이터 이상(금지랙)"; break;
                    case 11008: AlarmName_Text = "설정 데이터 이상(스페셜랙)"; break;
                    case 11009: AlarmName_Text = "설정 데이터 이상(제어 설정)"; break;
                    case 11010: AlarmName_Text = "설정 데이터 이상(주행 드라이브)"; break;
                    case 11011: AlarmName_Text = "설정 데이터 이상(승강 드라이브)"; break;
                    case 11012: AlarmName_Text = "설정 데이터 이상(포크 드라이브)"; break;

                }
            }
            return AlarmName_Text;
        }
        #endregion
        #region UTIL 함수
        #region network 
        public static bool UTIL_IsValid_IP(string tmpIP, out IPAddress ReturnIP)
        {
            ReturnIP = null;

            //if (tmpIP.Trim() == "0.0.0.0") return false;

            try
            {
                return IPAddress.TryParse(tmpIP, out ReturnIP);
                
            }
            catch
            {
                return false;
            }
        }

        public static bool UTIL_IsValid_MAC(string tmpmac, out PhysicalAddress ReturnMac)
        {
            ReturnMac = null;

            //if (tmpmac.Trim() == "00-00-00-00-00-00") return false;
            try
            {
                ReturnMac = PhysicalAddress.Parse(tmpmac);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 시간 함수
        public static DateTime UTIL_GetLocalTimeFromUnixTimeStamp(uint TmpValue)
        {
            DateTime PCtime = epochStart.AddSeconds(TmpValue);
            TimeZoneInfo systemTimeZone = TimeZoneInfo.Local;
            PCtime = TimeZoneInfo.ConvertTimeFromUtc(PCtime, systemTimeZone);
            return PCtime;
        }
        public static uint UTIL_GetUnixTimeStampFromLocalTime(DateTime localTime)
        {
            TimeZoneInfo systemTimeZone = TimeZoneInfo.Local;
            //DateTime UTCTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, systemTimeZone);
            DateTime UTCTime = TimeZoneInfo.ConvertTimeToUtc(localTime, systemTimeZone);
            TimeSpan ts = UTCTime - Global_Class.epochStart;
            return (uint)ts.TotalSeconds;
        }
        #endregion


        #region 디렉토리, 파일 함수
        public static bool UTIL_Dir_create(string dir)
        {
            try
            {
                DirectoryInfo dinfo = new DirectoryInfo(dir);

                if (dinfo.Exists == false)
                    dinfo.Create();
            }
            catch
            {
                return false;
            }

            return true;
        }


        public static bool UTIL_Dir_exists(string dir)
        {
            bool result = false;

            try
            {
                DirectoryInfo dir_info = new DirectoryInfo(dir);

                result = dir_info.Exists;
            }
            catch
            {
                return false;
            }

            return result;
        }


        public static bool UTIL_Dir_delete(string dir)
        {
            try
            {
                DirectoryInfo dinfo = new DirectoryInfo(dir);

                if (dinfo.Exists == true)
                    dinfo.Delete(true);
            }
            catch
            {
                return false;
            }

            return true;
        }


        public static bool UTIL_File_exists(string fname)
        {
            bool result = false;

            try
            {
                FileInfo finfo = new FileInfo(fname);


                result = finfo.Exists;
            }
            catch (Exception)
            {
                return false;
            }

            return result;
        }


        public static long UTIL_File_size(string fname)
        {
            long result;

            try
            {
                FileInfo finfo = new FileInfo(fname);

                result = finfo.Length;
            }
            catch
            {
                return 0;
            }

            return result;
        }


        public static bool UTIL_File_remove(string fname)
        {
            try
            {
                FileInfo finfo = new FileInfo(fname);

                finfo.Delete();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool UTIL_File_copyto(string src, string dst)
        {
            try
            {
                FileInfo finfo = new FileInfo(src);

                finfo.CopyTo(dst);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool UTIL_File_moveto(string src, string dst)
        {
            try
            {
                FileInfo finfo = new FileInfo(src);

                finfo.MoveTo(dst);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool UTIL_File_overwrite(string src, string dst)
        {
            try
            {
                FileInfo finfo = new FileInfo(src);

                finfo.CopyTo(dst, true);
            }
            catch
            {
                return false;
            }

            return true;
        }
        #endregion

        #region 로깅 함스
        public static bool Logging_CommDataInsert(string TmpLoggingDir, string PrefixStr, string[] msgList, int Count)
        {
            DateTime dt = DateTime.Now;
            string writeMsg;
            string dat, tim, dir, src, dst;

            dat = dt.ToString("yyyyMMdd");
            tim = dt.ToString("HHmmss");

            dir = TmpLoggingDir;

            src = dir + PrefixStr + "_" + dat + ".TXT";
            dst = dir + PrefixStr + "_" + dat + '_' + tim + ".TXT";

            try
            {
                // 이 부분은 여러 프로세서에서 동시에 생성을 시도할수 있는 문제가 있는지 확인 필요
                UTIL_Dir_create(dir);

                string path = string.Format(src);
                StreamWriter sw = new StreamWriter(path, true);

                for (int Loop = 0; Loop < Count; Loop++)
                {
                    writeMsg = msgList[Loop].Trim('\0') + "\x0d\x0a";
                    sw.Write(writeMsg);
                }

                sw.Close();

                if (UTIL_File_exists(src) == true)
                {
                    if (UTIL_File_size(src) > (1024 * 1024 * 15))    // 15 MB
                    {
                        if (UTIL_File_copyto(src, dst) == true)
                        {
                            if (!UTIL_File_remove(src))
                            {
                                UTIL_File_remove(dst);
                            } else
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        public static bool Logging_SpeedDataInsert(string TmpLoggingDir, string PrefixStr, string[] msgList, int Count)
        {
            DateTime dt = DateTime.Now;
            string dat, dir, src;
            dat = dt.ToString("yyyyMMdd");
            

            dir = TmpLoggingDir;

            src = dir + PrefixStr + "_" + dat + ".CSV";

            try
            {
                // 이 부분은 여러 프로세서에서 동시에 생성을 시도할수 있는 문제가 있는지 확인 필요
                UTIL_Dir_create(dir);

                string path = string.Format(src);
                //StreamWriter sw = new StreamWriter(path, true);
                StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);

                for (int Loop = 0; Loop < Count; Loop++)
                {
                    sw.WriteLine(msgList[Loop]);
                }

                sw.Close();
                            }
            catch
            {
            }
            return false;
        }
        #endregion

        #region Endian 관련 함수
        public static byte UTIL_LoByte(Int16 src)
        {
            return (byte)(src & 0xFF);
        }

        public static byte UTIL_HiByte(Int16 src)
        {
            return (byte)((src >> 8) & 0xFF);
        }

        public static Int16 UTIL_LoWord(Int32 src)
        {
            return (Int16)(src & 0xFFFF);
        }

        public static Int16 UTIL_HiWord(Int32 src)
        {
            return (Int16)((src >> 16) & 0xFFFF);
        }

        public static Int16 UTIL_ConvertHiLo(Int16 src)
        {
            byte HiB = UTIL_HiByte(src);
            byte LoB = UTIL_LoByte(src);

            Int16 Tmp = (Int16)((LoB << 8) & 0xff00);
            Tmp = (Int16)(Tmp | (Int16)(HiB & 0x00ff));

            return Tmp;
        }

        public static int UTIL_ConvertHiLo(int src)
        {
            Int16 HiW = UTIL_HiWord(src);
            Int16 LoW = UTIL_LoWord(src);

            byte HH = UTIL_HiByte(HiW);
            byte HL = UTIL_LoByte(HiW);
            byte LH = UTIL_HiByte(LoW);
            byte LL = UTIL_LoByte(LoW);

            int Value = (int)((LL << 24) & 0xff000000) | ((LH << 16) & 0x00ff0000) | ((HL << 8) & 0x0000ff00) | ((HH) & 0x000000ff);

            return Value;
        }
        #endregion

        #region String, HexString 관련 함수
        public static string UTIL_NULLStringTo(bool trimFlag, object src, string replace_str)
        {
            if (src == null) return replace_str;
            if ((string)src == "\0") return replace_str;
            if (src.ToString().Length == 0) return replace_str;
            else
            {
                if (trimFlag)
                {
                    return src.ToString().Trim();
                }
                else
                {
                    return src.ToString();
                }
            }
        }

        public static string UTIL_StrLeft(string Text, int TextLenth)
        {
            string ConvertText;
            if (Text.Length < TextLenth)
            {
                TextLenth = Text.Length;
            }

            ConvertText = Text.Substring(0, TextLenth);

            return ConvertText;
        }

        public static string UTIL_StrRight(string Text, int TextLenth)
        {
            string ConvertText;
            if (Text.Length < TextLenth)
            {
                TextLenth = Text.Length;
            }

            ConvertText = Text.Substring(Text.Length - TextLenth, TextLenth);

            return ConvertText;
        }

        public static string UTIL_StrMid(string Text, int Strint, int Endint)
        {
            string ConvertText;
            if (Strint < Text.Length || Endint < Text.Length)
            {
                ConvertText = Text.Substring(Strint, Endint);
                return ConvertText;
            }


            return Text;
        }

        public static int UTIL_StrToIntDef(string src, int tmpexceptvalue)
        {
            string buff = src.ToString().Trim();

            if (src == "") return tmpexceptvalue;
            if (src == null) return tmpexceptvalue;

            int len = src.Length;
            for (int i = 0; i < len; i++)
            {
                if (buff[i] == '-') continue;
                if (buff[i] < '0' || buff[i] > '9') return tmpexceptvalue;
            }


            try
            {
                return Convert.ToInt32(buff);
            }
            catch
            {
                return tmpexceptvalue;
            }

        }

        public static UInt32 UTIL_StrToUInt32Def(string src, UInt32 tmpexceptvalue)
        {
            string buff = src.ToString().Trim();

            if (src == "") return tmpexceptvalue;
            if (src == null) return tmpexceptvalue;

            int len = src.Length;
            for (int i = 0; i < len; i++)
            {
                if (buff[i] == '-') return tmpexceptvalue;
                if (buff[i] < '0' || buff[i] > '9') return tmpexceptvalue;
            }

            try
            {
                return Convert.ToUInt32(buff);
            }
            catch
            {
                return tmpexceptvalue;
            }

        }

        public static float UTIL_StrToFloatDef(string src, float tmpexceptvalue)
        {
            string buff = src.ToString().Trim();

            if (src == "") return tmpexceptvalue;
            if (src == null) return tmpexceptvalue;

            int len = src.Length;
            for (int i = 0; i < len; i++)
            {
                if (buff[i] == '.') continue;
                if (buff[i] == '-') continue;
                if (buff[i] < '0' || buff[i] > '9') return tmpexceptvalue;
            }

            try
            {
                return Convert.ToSingle(buff);
            } catch
            {
                return tmpexceptvalue;
            }
        }

        public static int UTIL_HexStrToIntDef(string src, int tmpexceptvalue)
        {
            string buff = src.ToString().ToUpper().Trim();

            if (src == "") return tmpexceptvalue;
            if (src == null) return tmpexceptvalue;

            int len = src.Length;
            for (int i = 0; i < len; i++)
            {
                if (!((buff[i] >= '0' && buff[i] <= '9') ||
                      (buff[i] >= 'A' && buff[i] <= 'F')))
                    return tmpexceptvalue;
            }


            try
            {
                return Convert.ToInt32(buff, 16);
            }
            catch
            {
                return tmpexceptvalue;
            }
        }

        public static string UTIL_UnicodeStringOfBytes(byte[] b)
        {
            return Encoding.Unicode.GetString(b);
        }

        public static string UTIL_DefaultStringOfBytes(byte[] b)
        {
            return Encoding.Default.GetString(b);
        }

        public static string UTIL_GetUTF8StringOfBytes(byte[] b)
        {
            return Encoding.UTF8.GetString(b);
        }

        public static void UTIL_StrToByteArray(string src, byte[] target, int inx_start)
        {
            short i = 0;
            int cnt = inx_start;
            char[] ch = src.ToCharArray();

            for (i = 0; i < ch.Length; i++)
                target[cnt++] = (byte)ch[i];
        }
        #endregion


        #region Bit 관련 함수
        public static bool BitStatus(UInt32 value, byte bitindex)
        {
            UInt32 CompareValue = (UInt32)(0x00000001 << bitindex);
            return ((value & CompareValue) != 0);
        }

        public static bool BitStatus(UInt16 value, byte bitindex)
        {
            UInt16 CompareValue = (UInt16)(0x0001 << bitindex);
            return ((value & CompareValue) != 0);
        }

        public static bool BitStatus(byte value, byte bitindex)
        {
            byte CompareValue = (byte)(0x01 << bitindex);
            return ((value & CompareValue) != 0);
        }
        #endregion

        #region char 관련 함수
        public static byte[] UTIL_CharToUniCodeBytes(char dat, int inx_start, ref byte len)
        {
            int cnt = inx_start;


            char[] ch = dat.ToString().ToCharArray();
            byte[] tmpby = Encoding.Unicode.GetBytes(ch);

            len = (byte)tmpby.Length;
            return tmpby;
        }

        public static byte[] UTIL_CharToDefaultBytes(char dat, int inx_start, ref byte len)
        {
            int cnt = inx_start;


            char[] ch = dat.ToString().ToCharArray();
            byte[] tmpby = Encoding.Default.GetBytes(ch);

            len = (byte)tmpby.Length;
            return tmpby;
        }

        public static byte[] UTIL_CharToUTF8Bytes(char dat, int inx_start, ref byte len)
        {
            int cnt = inx_start;


            char[] ch = dat.ToString().ToCharArray();
            byte[] tmpby = Encoding.UTF8.GetBytes(ch);

            len = (byte)tmpby.Length;
            return tmpby;
        }
        #endregion

        #region 데이터 화면표시를 위한 변환 관련 함수
        public static string UTIL_ByteToPVerstr(byte by)
        {
            string rtn = string.Format("{0:X2}", by);
            rtn = UTIL_StrMid(rtn, 0, 1) + '.' + UTIL_StrMid(rtn, 1, 1);
            return rtn;
        }

        public static string UTIL_ByteToFVerstr(byte* by)
        {
            string rtn = string.Format("{0:X2}", by[0]);
            rtn = UTIL_StrMid(rtn, 0, 1) + '.' + UTIL_StrMid(rtn, 1, 1) + '(' +
                  string.Format("{0:X2}{1:X2}{2:X2}", by[1], by[2], by[3]) + ')';
            return rtn;
        }
        #endregion

        #region 버퍼 초기화 함수

        public static void UTIL_Byteptr_LinearTest(byte* sbuff, int len)
        {
            for (int i = 0; i < len; i++)
                sbuff[i] = (byte)i;
        }

        public static void UTIL_Byteptr_Fill(byte* sbuff, int len, byte fillData)
        {
            for (int i = 0; i < len; i++)
                sbuff[i] = fillData;
        }

        public static void UTIL_Byteptr_clear(byte* sbuff, int len)
        {
            for (int i = 0; i < len; i++)
                sbuff[i] = 0x00;
        }

        public static void UTIL_ByteArray_clear(byte[] sbuff, int len)
        {
            for (int i = 0; i < len; i++)
                sbuff[i] = 0x00;
        }

        public static void UTIL_CharArray_Clear(char[] sbuff, int len)
        {
            for (int i = 0; i < len; i++)
                sbuff[i] = '\x0000';
        }

        public static void UTIL_CharPtr_Clear(char* sbuff, int len)
        {
            for (int i = 0; i < len; i++)
                sbuff[i] = '\x0000';
        }

        #endregion

        #region ToHexStr 함수
        public static string Util_ByteToHexStr(byte src)
        {
            string rtn = string.Format("{0:X2}", src);
            int len = rtn.Length;
            if (len > 2) rtn = rtn.Substring(0, 2);
            return rtn;
        }

        public static string Util_IntToHexStr(int src)
        {
            string rtn = string.Format("{0:X8}", src);
            int len = rtn.Length;
            if (len > 8) rtn = rtn.Substring(0, 8);
            return rtn;
        }

        public static string UTIL_Int16ToHexStr(Int16 src)
        {
            string rtn = string.Format("{0:X4}", src);
            int len = rtn.Length;
            if (len > 4) rtn = rtn.Substring(0, 4);
            return rtn;
        }

        public static string UTIL_Int32ToHexStr(int src)
        {
            string rtn = string.Format("{0:X8}", src);
            int len = rtn.Length;
            if (len > 8) rtn = rtn.Substring(0, 8);
            return rtn;
        }

        public static string UTIL_BytePtrToHexStr(byte* src, int len, ConstClass.TWithSpaceFlag sp, byte groupcount)
        {
            byte TmpCount = 0;
            string rtn = "";
            string strText = "";


            for (int i = 0; i < len; i++)
            {
                strText += Util_ByteToHexStr(src[i]);
                if (sp == ConstClass.TWithSpaceFlag.WithSpace) strText += " ";

                TmpCount++;

                if (TmpCount == groupcount)
                {
                    strText += "    ";
                    TmpCount = 0;
                }
            }
            rtn = strText;
            return rtn;
        }

        public static string UTIL_BytePtrToHexStr(byte* src, int len, ConstClass.TWithSpaceFlag sp)
        {
            string rtn = "";
            string strText = "";

            for (int i = 0; i < len; i++)
            {
                strText += Util_ByteToHexStr(src[i]);
                if (sp == ConstClass.TWithSpaceFlag.WithSpace) strText += " ";
            }
            rtn = strText;
            return rtn;
        }

        public static string UTIL_byteArrayToHexStr(byte[] src, int len, ConstClass.TWithSpaceFlag sp)
        {
            string rtn = "";
            string strText = "";

            for (int i = 0; i < len; i++)
            {
                strText += Util_ByteToHexStr(src[i]);
                if (sp == ConstClass.TWithSpaceFlag.WithSpace) strText += " ";
            }
            rtn = strText;
            return rtn;
        }

        public static string UTIL_IntPtrToHexStr(int* src, int len, ConstClass.TWithSpaceFlag sp)
        {
            string rtn = "";
            string strText = "";

            for (int i = 0; i < len; i++)
            {
                strText += UTIL_Int32ToHexStr(src[i]);
                if (sp == ConstClass.TWithSpaceFlag.WithSpace) strText += " ";
            }
            rtn = strText;
            return rtn;
        }

        #endregion

        #region 변환 함수
        public static void UTIL_BytesToBytePtr(byte[] sbuff, byte* tbuff)
        {
            for (int i = 0; i < sbuff.Length; i++)
                tbuff[i] = sbuff[i];
        }
        public static void UTIL_BytesToBytePtr(byte[] sbuff, byte* tbuff, byte CopyLen)
        {
            for (int i = 0; i < CopyLen; i++)
            {
                if (i < sbuff.Length)
                {
                    tbuff[i] = sbuff[i];
                } else
                {
                    tbuff[i] = 0x00;
                }
            }
        }

        public static void UTIL_BytePtrToByteArray(byte* sbuff, byte[] tbuff)
        {
            for (int i = 0; i < tbuff.Length; i++)
                tbuff[i] = (byte)sbuff[i];
        }

        public static byte UTIL_GetNibbleByteOfChar(char ch, ConstClass.THiLoNibble hiLo)
        {
            byte value = 0;

            if (ch >= 'A' && ch <= 'F') value += (byte)(ch - 'A' + 10);
            if (ch >= 'a' && ch <= 'f') value += (byte)(ch - 'a' + 10);
            if (ch >= '0' && ch <= '9') value += (byte)(ch - '0');

            if (hiLo == ConstClass.THiLoNibble.Nibble_Hi)
            {
                return (byte)(value * 0x10);
            }
            else
            {
                return value;
            }
        }

        public static object UTIL_BytesToStructure(byte[] data, Type type, int sno, int struc_len)
        {
            int a;
            IntPtr buff = Marshal.AllocHGlobal(struc_len); // 배열의 크기만큼 비관리 메모리 영역에 메모리를 할당한다.

            for (int i = 0; i < struc_len; i++)
            {
                Marshal.WriteByte(buff, i, 0);
            }

            Marshal.Copy(data, sno, buff, struc_len); // 배열에 저장된 데이터를 위에서 할당한 메모리 영역에 복사한다.
            object obj = Marshal.PtrToStructure(buff, type); // 복사된 데이터를 구조체 객체로 변환한다.
            Marshal.FreeHGlobal(buff); // 비관리 메모리 영역에 할당했던 메모리를 해제함
            a = Marshal.SizeOf(obj);
            if (a != struc_len)// (((PACKET_DATA)obj).TotalBytes != data.Length) // 구조체와 원래의 데이터의 크기 비교
            {
                return null; // 크기가 다르면 null 리턴
            }
            return obj; // 구조체 리턴
        }

        public static object UTIL_BytesToStructure(byte[] data, Type type, int sno, int struc_len, int array_len)
        {
            int a;
            //IntPtr buff = Marshal.AllocHGlobal(struc_len); // 배열의 크기만큼 비관리 메모리 영역에 메모리를 할당한다.
            IntPtr buff = Marshal.AllocHGlobal(struc_len); // 배열의 크기만큼 비관리 메모리 영역에 메모리를 할당한다.

            for (int i = 0; i < struc_len; i++)
            {
                Marshal.WriteByte(buff, i, 0);
            }

            if (struc_len < array_len) array_len = struc_len;

            Marshal.Copy(data, sno, buff, array_len); // 배열에 저장된 데이터를 위에서 할당한 메모리 영역에 복사한다.
            object obj = Marshal.PtrToStructure(buff, type); // 복사된 데이터를 구조체 객체로 변환한다.
            Marshal.FreeHGlobal(buff); // 비관리 메모리 영역에 할당했던 메모리를 해제함
            a = Marshal.SizeOf(obj);
            return obj; // 구조체 리턴
        }

        public static object UTIL_BytesToStructure(byte[] bytearray, Type type)
        {
            int len = Marshal.SizeOf(type);
            int srclen = bytearray.Length;

            
            if (len > srclen) return UTIL_BytesToStructure(bytearray, type, 0, len, srclen);
            else              return UTIL_BytesToStructure(bytearray, type, 0, len);
        }

        public static object UTIL_BytesToStructure(byte[] bytearray, int srcLen, Type type)
        {
            int len = Marshal.SizeOf(type);
            return UTIL_BytesToStructure(bytearray, type, 0, len, srcLen);
        }


        public static void UTIL_CharPtrToCharPtr(char* sbuff, char* tbuff, int sno, int tno, int len)
        {
            for (int i = 0; i < len; i++)
                tbuff[i + tno] = sbuff[i + sno];
        }

        public static void UTIL_CharArrayToCharArray(char[] sbuff, char[] tbuff, int sno, int tno, int len)
        {
            for (int i = 0; i < len; i++)
                tbuff[i + tno] = sbuff[i + sno];
        }

        public static string UTIL_GetStringOfCharArray(char[] sbuff)
        {
            string edata = "";

            for (int i = 0; i < sbuff.Length; i++)
            {
                if (sbuff[i] != '\0')           //  \0 인경우 제외
                    edata += sbuff[i];
                edata = UTIL_NULLStringTo(true, edata, "");
            }

            return edata;
        }

        public static string UTIL_GetStringOfCharPtr(char* sbuff, int len)
        {
            string edata = "";

            for (int i = 0; i < len; i++)
            {
                if (sbuff[i] != '\0')           //  \0 인경우 제외
                    edata += sbuff[i];
                edata = UTIL_NULLStringTo(true, edata, "");
            }

            return edata;
        }

        public static string UTIL_GetStringOfCharPtr(char* sbuff, int len, char OldChar, char NewChar)
        {
            string edata = "";

            for (int i = 0; i < len; i++)
            {
                if (sbuff[i] == OldChar)
                    sbuff[i] = NewChar;
                if (sbuff[i] != '\0')           //  \0 인경우 제외
                    edata += sbuff[i];

                edata = UTIL_NULLStringTo(false, edata, "");
            }

            return edata;
        }

        public static void UTIL_IntPtrToIntArray(int* sbuff, int[] tbuff, int sno, int tno, int len)
        {
            for (int i = 0; i < len; i++)
                tbuff[i + tno] = sbuff[i + sno];
        }

        public static void UTIL_StructObjectToByteArray(object Srcdata, byte[] Target, int tno)
        {

            int datasize = Marshal.SizeOf(Srcdata);//((PACKET_DATA)obj).TotalBytes; // 구조체에 할당된 메모리의 크기를 구한다.

            IntPtr buff = Marshal.AllocHGlobal(datasize); // 비관리 메모리 영역에 구조체 크기만큼의 메모리를 할당한다.

            Marshal.StructureToPtr(Srcdata, buff, false); // 할당된 구조체 객체의 주소를 구한다.

            //byte[] data = new byte[datasize]; // 구조체가 복사될 배열

            Marshal.Copy(buff, Target, tno, datasize); // 구조체 객체를 배열에 복사

            Marshal.FreeHGlobal(buff); // 비관리 메모리 영역에 할당했던 메모리를 해제함
        }

        public static void UTIL_StructObjectToByteArray(object Srcdata, byte[] Target)
        {

            int datasize = Marshal.SizeOf(Srcdata);//((PACKET_DATA)obj).TotalBytes; // 구조체에 할당된 메모리의 크기를 구한다.

            IntPtr buff = Marshal.AllocHGlobal(datasize); // 비관리 메모리 영역에 구조체 크기만큼의 메모리를 할당한다.

            Marshal.StructureToPtr(Srcdata, buff, false); // 할당된 구조체 객체의 주소를 구한다.

            //byte[] data = new byte[datasize]; // 구조체가 복사될 배열

            Marshal.Copy(buff, Target, 0, datasize); // 구조체 객체를 배열에 복사

            Marshal.FreeHGlobal(buff); // 비관리 메모리 영역에 할당했던 메모리를 해제함
        }

        public static void UTIL_StructObjectToByteArray(UInt16 TmpTxLen, object Srcdata, byte[] Target)
        {

            //구조체 전체를 날리는 것이 아닌 경우이다
            
            int datasize = Marshal.SizeOf(Srcdata);

            IntPtr buff = Marshal.AllocHGlobal(datasize); // 비관리 메모리 영역에 구조체 크기만큼의 메모리를 할당한다.

            Marshal.StructureToPtr(Srcdata, buff, false); // 할당된 구조체 객체의 주소를 구한다.

            //byte[] data = new byte[datasize]; // 구조체가 복사될 배열

            Marshal.Copy(buff, Target, 0, TmpTxLen); // 구조체 객체를 배열에 복사

            Marshal.FreeHGlobal(buff); // 비관리 메모리 영역에 할당했던 메모리를 해제함
        }

        public static void UTIL_ByteArrayToBytePtr(byte[] sbuff, byte* tbuff, int sno, int tno, int len)
        {
            try
            {
                for (int i = 0; i < len; i++) tbuff[i + tno] = sbuff[i + sno];
            } catch
            {
                for (int i = 0; i < len; i++) tbuff[i + tno] = 0x00;
            }
        }

        #endregion

        #region CheckSum 함수
        public static ushort UTIL_CheckSum(ushort fcrc, byte[] Tmpbytes, ushort TmpStart, ushort ushortLen)
        {
            ushort crc_index = 0;
            for (int i = TmpStart; i <= TmpStart + ushortLen - 1; i++)
            {
                crc_index = (ushort)((fcrc >> 8) ^ (Tmpbytes[i]));
                fcrc = (ushort)((fcrc << 8) ^ crc16Tbl[crc_index]);
            }
            //fcrc = (ushort)(((fcrc >> 8) & 0x00ff) | ((fcrc << 8) & 0xff00));
            return fcrc;
        }

        public static ushort UTIL_CheckSum(byte[] Tmpbytes, ushort TmpStart, ushort ushortLen)
        {
            ushort fcrc = 0;
            ushort crc_index = 0;
            for (int i = TmpStart; i <= TmpStart + ushortLen - 1; i++)
            {
                crc_index = (ushort)((fcrc >> 8) ^ (Tmpbytes[i]));
                fcrc = (ushort)((fcrc << 8) ^ crc16Tbl[crc_index]);
            }
            fcrc = (ushort)(((fcrc >> 8) & 0x00ff) | ((fcrc << 8) & 0xff00));
            return fcrc;
        }

        #endregion

        #region Code=>Text
        public static string UTIL_GetJobStepTextAsValue(byte value)
        {
            switch (value)
            {
                case 0x00: return ("지령없음");
                case 0x01: return ("지령수신"); 
                case 0x02: return ("From 위치로 이동중"); 
                case 0x03: return ("From 위치 도착"); 
                case 0x04: return ("From 위치에서 포크진입"); 
                case 0x05: return ("From 위치에서 캐리지상승"); 
                case 0x06: return ("From 위치에서 포크복귀"); 
                case 0x07: return ("To 위치로 이동중"); 
                case 0x08: return ("To 위치 도착"); 
                case 0x09: return ("To 위치에서 포크진입"); 
                case 0x0A: return ("To 위치에서 캐리지하강"); 
                case 0x0B: return ("To 위치에서 포크복귀");
                case 0x0C: return ("화물 적재 완료");
                case 0x0D: return ("화물 이재 완료");
                default: return (string.Format("{0:X2}", value)); 
            }
        }

        public static string UTIL_GetTaskStepTextAsValue(byte value)
        {
            switch (value)
            {
                case 0x00: return ("지령없음");
                case 0x81: return ("지령수신"); 
                case 0x82: return ("위치로 이동중"); 
                case 0x83: return ("위치 도착"); 
                case 0x84: return ("loading 포크진입"); 
                case 0x85: return ("loading 캐리지상승"); 
                case 0x86: return ("loading 포크복귀"); 
                case 0x87: return ("Unloading 포크진입"); 
                case 0x88: return ("Unloading 캐리지하강"); 
                case 0x89: return ("Unloading 포크복귀");
                case 0x8A: return ("loading 완료");
                case 0x8B: return ("Unloading 완료");
                default: return (string.Format("{0:X2}", value)); 
            }
        }


        public static string UTIL_GetJobTextAsValue(byte value)
        {
            switch (value)
            {
                case ConstClass.SEMI_NONE: return ("작업없음");
                case ConstClass.SEMI_MOVE: return ("Move");
                case ConstClass.SEMI_TaskLoading: return ("Loading");
                case ConstClass.SEMI_TaskUnLoading: return ("Unloading");
                case ConstClass.SEMI_INPUT: return ("입고");
                case ConstClass.SEMI_OUTPUT: return ("출고");
                case ConstClass.SEMI_RtoR: return("랙간반송");
                case ConstClass.SEMI_StoS: return ("스테이션간반송");
                case ConstClass.SEMI_ChangeR: return ("랙목적지변경");
                case ConstClass.SEMI_ChangeS: return ("스테이션목적지변경");
                case ConstClass.SEMI_Loading: return ("적재");
                case ConstClass.SEMI_UnLoading: return ("이재");
                default: return (string.Format("{0:X2}", value));
            }
        }


        public static string UTIL_RTVActionStText(byte Code)
        {
            string DevActionStText = string.Format("0x{0:X2}", Code);

            switch (Code)
            {
                case 0: DevActionStText = "[대기상태] 수동운전 모드"; break;
                case 10: DevActionStText = "[주행] 전진"; break;
                case 11: DevActionStText = "[주행] 후진"; break;
                case 12: DevActionStText = "[주행] 전진 크립"; break;
                case 13: DevActionStText = "[주행] 후진 크립"; break;
                case 14: DevActionStText = "[주행] 정지"; break;
                case 15: DevActionStText = "[주행] Retry"; break;
                case 16: DevActionStText = "[주행] 감속센서 감속"; break;
                case 17: DevActionStText = "[주행] 감속영역 감속"; break;
                case 18: DevActionStText = "[주행] 센서&&영역 감속"; break;
                case 19: DevActionStText = "[주행] 충돌방비 감속"; break;
                case 20: DevActionStText = "[주행] 충돌방지 정지"; break;
                case 21: DevActionStText = "[주행] 인터록 정지"; break;
                case 22: DevActionStText = "[주행] Comm 정지"; break;
                case 23: DevActionStText = "[주행] 비상정지"; break;


                case 30: DevActionStText = "[적재] 피딩 동작 대기"; break;
                case 31: DevActionStText = "[적재] 피딩 동작중"; break;
                case 32: DevActionStText = "[적재] 피딩 크립"; break;
                case 33: DevActionStText = "[적재] 피딩 정지"; break;
                case 34: DevActionStText = "[적재] 이동에러 지시 대기"; break;
                case 35: DevActionStText = "[적재] 화물탑재전 주행에러 지시대기"; break;
                case 36: DevActionStText = "[적재] 리트라이 지시대기"; break;
                case 37: DevActionStText = "[적재] 화물탑재 이상 지시 대기"; break;

                case 40: DevActionStText = "[적재] 피딩 동작 대기"; break;
                case 41: DevActionStText = "[적재] 피딩 동작중"; break;
                case 42: DevActionStText = "[적재] 피딩 크립"; break;
                case 43: DevActionStText = "[적재] 피딩 정지"; break;
                case 44: DevActionStText = "[적재] 이동에러 지시 대기"; break;
                case 45: DevActionStText = "[적재] 화물이재전 주행에러 지시대기"; break;
                case 46: DevActionStText = "[적재] 리트라이 지시대기"; break;
                case 47: DevActionStText = "[적재] 화물이재 이상 지시 대기"; break;
            }
            return DevActionStText;
        }

        #endregion
        #endregion

        #region Debug File 함수
        public void DEBUG_Insert(string msg)
        {
            DateTime dt = DateTime.Now;
            string valu;
            string dat, tim, dir, nam, src, dst;

            dat = dt.ToString("yyyyMMdd");
            tim = dt.ToString("HHmmss");

            dir = RootDIR + "\\DEBUG\\" + dat;
            nam = "\\Debug_";

            src = dir + nam + dat + ".TXT";
            dst = dir + nam + dat + '_' + tim + ".TXT";

            try
            {
                UTIL_Dir_create(dir);

                string path = string.Format(src);

                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    valu = "[" + dt.ToString("HH:mm:ss.fff") + "] " + msg.Trim('\0') + "\x0d\x0a";
                    sw.Write(valu);
                    sw.Close();

                }


                if (UTIL_File_exists(src) == true)
                    if (UTIL_File_size(src) > (1024 * 1024 * 10))    // 10 MB
                        if (UTIL_File_copyto(src, dst) == true)
                            if (UTIL_File_remove(src) == false)
                                UTIL_File_remove(dst);
            }
            catch
            {
            }
        }

        #endregion


        #region Message 함수
        public void MsgBox_Confirm_OK(string msg)
        {
            string caption = "Confirmation";
            MessageBoxButtons buttons = MessageBoxButtons.OK;

            MessageBox.Show(this.owner, msg, caption, buttons, MessageBoxIcon.Exclamation);
        }

        public void MsgBox_Info(string msg, string typ)
        {
            MessageBoxIcon icon;
            string caption;

            if (typ.ToUpper() == "I")
            {
                caption = "Information";
                icon = MessageBoxIcon.Information;
            }
            else
                if (typ.ToUpper() == "W")
            {
                caption = "Warning";
                icon = MessageBoxIcon.Warning;
            }
            else
            {
                caption = "Information";
                icon = MessageBoxIcon.Information;
            }

            MessageBoxButtons buttons = MessageBoxButtons.OK;


            MessageBox.Show(this.owner, msg, caption, buttons, icon);
        }


        public bool MsgBox_Confirm_YN(string msg)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            string caption = "Confirmation";

            result = MessageBox.Show(this.owner, msg, caption, buttons, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                return true;
            }
            return false;

        }

        public bool MsgBox_Confirm_OKCancel(IWin32Window TmpOwner, string msg)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result;
            string caption = "Confirmation";

            result = MessageBox.Show(TmpOwner, msg, caption, buttons, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.OK)
            {
                return true;
            }
            return false;

        }
        #endregion



    }



}

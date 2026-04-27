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
            string WarnningName_Text = Code1.ToString() + "-" + Code2.ToString() + "-" + Code3.ToString();
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
                case 101: WarnningName_Text = "주행 금지 - 포크 중심 아님(FCL 센서)"; break;
                case 102: WarnningName_Text = "주행 금지 - 포크 중심 아님(FCR 센서)"; break;
                case 103: WarnningName_Text = "주행 금지 - 포크 중심 아님(FCL/FCR 센서)"; break;
                case 104: WarnningName_Text = "주행 금지 - 포크 좌측 진출(위치)"; break;
                case 105: WarnningName_Text = "주행 금지 - 포크 우측 진출(위치)"; break;
                case 106: WarnningName_Text = "주행 금지 - 포크 좌측 진출(센서)"; break;
                case 107: WarnningName_Text = "주행 금지 - 포크 우측 진출(센서)"; break;
                case 108: WarnningName_Text = "주행 금지 - 시작 위치 도달"; break;
                case 109: WarnningName_Text = "주행 금지 - 끝 위치 도달"; break;
                case 110: WarnningName_Text = "주행 금지 - 방향 반대"; break;

                case 201: WarnningName_Text = "승강 금지 - 포크 중심 아님(FCL 센서)"; break;
                case 202: WarnningName_Text = "승강 금지 - 포크 중심 아님(FCR 센서)"; break;
                case 203: WarnningName_Text = "승강 금지 - 포크 중심 아님(FCL/FCR 센서)"; break;
                case 204: WarnningName_Text = "승강 금지 - 포크 좌측 진출(위치)"; break;
                case 205: WarnningName_Text = "승강 금지 - 포크 우측 진출(위치)"; break;
                case 206: WarnningName_Text = "승강 금지 - 포크 좌측 진출(센서)"; break;
                case 207: WarnningName_Text = "승강 금지 - 포크 우측 진출(센서)"; break;
                case 208: WarnningName_Text = "승강 금지 - 시작 위치 도달"; break;
                case 209: WarnningName_Text = "승강 금지 - 끝 위치 도달"; break;
                case 210: WarnningName_Text = "승강 금지 - 방향 반대"; break;
                case 211: WarnningName_Text = "승강 금지 - 포크 좌 정위치 아님"; break;
                case 212: WarnningName_Text = "승강 금지 - 포크 우 정위치 아님"; break;
                case 213: WarnningName_Text = "승강 금지 - 포크 좌 Harf 정위치 아님"; break;
                case 214: WarnningName_Text = "승강 금지 - 포크 좌 Full 정위치 아님"; break;
                case 215: WarnningName_Text = "승강 금지 - 포크 우 Harf 정위치 아님"; break;
                case 216: WarnningName_Text = "승강 금지 - 포크 우 Full 정위치 아님"; break;

                case 301: WarnningName_Text = "포크 구동 금지 - 주행 정위치 아님"; break;
                case 302: WarnningName_Text = "포크 구동 금지 - 승강 정위치 아님"; break;
                case 303: WarnningName_Text = "포크 구동 금지 - 랙 간섭"; break;
                case 304: WarnningName_Text = "포크 구동 금지 - 이중입고(DSTL1 / DSTR1)"; break;
                case 305: WarnningName_Text = "포크 구동 금지 - 이중입고(DSTLe1 / DSTRe1"; break;
                case 306: WarnningName_Text = "포크 구동 금지 - 이중입고(DSTLR1 / DSTRR1)"; break;
                case 307: WarnningName_Text = "포크 구동 금지 - 선입품감지"; break;
                case 308: WarnningName_Text = "포크 구동 금지 - 좌측 끝위치 도달"; break;
                case 309: WarnningName_Text = "포크 구동 금지 - 우측 끝위치 도달"; break;
                case 310: WarnningName_Text = "포크 구동 금지 - 방향 반대"; break;
                case 311: WarnningName_Text = "포크 구동 금지 - 금지랙"; break;
                case 312: WarnningName_Text = "포크 구동 금지 - 스페셜랙"; break;
                case 313: WarnningName_Text = "포크 구동 금지 - 인터락이상"; break;

                case 1000: WarnningName_Text = "자동모드상태에서 수동조작"; break;
                case 1001: WarnningName_Text = "자동모드 실패 - 포크 중심 아님(FCL 센서)"; break;
                case 1002: WarnningName_Text = "자동모드 실패 - 포크 중심 아님(FCR 센서)"; break;
                case 1003: WarnningName_Text = "자동모드 실패 - 포크 중심 아님(FCL/FCR 센서)"; break;
                case 1004: WarnningName_Text = "자동모드 실패 - 포크 좌측 진출(위치)"; break;
                case 1005: WarnningName_Text = "자동모드 실패 - 포크 우측 진출(위치)"; break;
                case 1006: WarnningName_Text = "자동모드 실패 - 포크 좌측 진출(센서)"; break;
                case 1007: WarnningName_Text = "자동모드 실패 - 포크 우측 진출(센서)"; break;

                case 1500: WarnningName_Text = "기상반 수동모드 - 명령 수행 거부"; break;
                case 1501: WarnningName_Text = "기상반 수동모드 - 주행 수동 운전 거부"; break;
                case 1502: WarnningName_Text = "기상반 수동모드 - 승강 수동 운전 거부"; break;
                case 1503: WarnningName_Text = "기상반 수동모드 - 포크 수동 운전 거부"; break;
                case 1504: WarnningName_Text = "기상반 수동모드 - 주행 위치 이동 거부"; break;
                case 1505: WarnningName_Text = "기상반 수동모드 - 승강 위치 이동 거부"; break;
                case 1506: WarnningName_Text = "기상반 수동모드 - 포크 위치 이동 거부"; break;
            }

            return WarnningName_Text;
        }


        public static string UTIL_EMSAlarmName(byte Code1, byte Code2, ushort Code3)
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
                    
                case 101: AlarmName_Text  = "주행 인버터 오류"; break;
                case 102: AlarmName_Text  = "주행 인버터 통신 끊김"; break;
                case 103: AlarmName_Text  = "주행 인버터 통신 연결 실패"; break;
                case 104: AlarmName_Text  = "운전중 주행인버터 오류"; break;
                case 110: AlarmName_Text  = "주행 인버터 STO 동작"; break;
                case 111: AlarmName_Text  = "주행 인버터 메인 전원 이상"; break;
                case 201: AlarmName_Text  = "EtherCAT 연결 실패"; break;
                case 202: AlarmName_Text  = "EtherCAT Slave 연결 이상"; break;
                case 203: AlarmName_Text  = "EtherCAT Slave ID 이상"; break;
                case 204: AlarmName_Text  = "EtherCAT Slave 타입 이상"; break;
                case 205: AlarmName_Text  = "EtherCAT 통신 끊김 (DIO1)"; break;
                case 206: AlarmName_Text = "EtherCAT 통신 끊김 (DIO2)"; break;
                case 207: AlarmName_Text = "EtherCAT 통신 끊김 (DIO3)"; break;
                case 208: AlarmName_Text = "EtherCAT 통신 끊김 (DIO4)"; break;
                case 209: AlarmName_Text = "EtherCAT 통신 끊김 (DIO5)"; break;
                case 210: AlarmName_Text = "EtherCAT 통신 타임아웃"; break;
                case 304: AlarmName_Text  = "전진 이동 시간 초과"; break;
                case 305: AlarmName_Text  = "후진 이동 시간 초과";break;
                case 306: AlarmName_Text  = "역방향 주행 감지";break;
                case 307: AlarmName_Text  = "주행중 위치값 변화 없음"; break;
                case 311: AlarmName_Text  = "주행 목적 위치 정보 오류";break;
                case 312: AlarmName_Text  = "정지 동작 타임아웃";break;
                case 401: AlarmName_Text  = "주행 위치 리더기 오류";break;
                case 402: AlarmName_Text  = "주행 위치값 급변";break;
                case 403: AlarmName_Text  = "주행 위치값 이상 1";break;
                case 404: AlarmName_Text  = "주행 위치값 이상 2"; break;
                case 801: AlarmName_Text  = "주행 중 승강밸트 텐션 이상 -HLD1 감지";break;
                case 802: AlarmName_Text  = "주행 중 승강밸트 텐션 이상 -HLD2 감지";break;
                case 803: AlarmName_Text  = "주행 중 승강밸트 텐션 이상 -HLD3 감지";break;
                case 804: AlarmName_Text  = "주행 중 승강밸트 텐션 이상 -HLD4 감지";break;
                case 1001: AlarmName_Text = "승강 인버터 오류";break;
                case 1003: AlarmName_Text = "승강 인버터 통신 연결 실패";break;
                case 1004: AlarmName_Text = "승강 인버터 운전 중 오류 발생";break;
                case 1005: AlarmName_Text = "승강 인버터 모터 제어 실패";break;
                case 1006: AlarmName_Text = "승강 인버터 과전류 발생";break;
                case 1010: AlarmName_Text = "승강 인버터 STO 동작";break;
                case 1011: AlarmName_Text = "승강 인버터 메인 전원 이상";break;
                
                case 1101: AlarmName_Text = "그리퍼 원점 미설정"; break;
                case 1102: AlarmName_Text = "그리퍼 모터 알람 "; break;
                case 1103: AlarmName_Text = "그리퍼 자동 동작 시간 초과"; break;
                case 1104: AlarmName_Text = "그리퍼 수동 동작 시간 초과"; break;
                case 1105: AlarmName_Text = "그리퍼 홈 이동 후, 홈 미감지"; break;
                case 1106: AlarmName_Text = "그리퍼 열림 동작 중, 그리퍼 홈 감지"; break;
                case 1107: AlarmName_Text = "그리퍼 열림 동작 중, 그리퍼 END 감지"; break;
                case 1108: AlarmName_Text = "그리퍼 화물 감지로 원점 설정 실패"; break;
                case 1109: AlarmName_Text = "그리퍼 홈이동 실패(화물감지)"; break;
                case 1110: AlarmName_Text = "자동모드 변경 시, 그리퍼 홈위치 아님"; break;
                case 1111: AlarmName_Text = "그리퍼 열림 화물감지 시간 초과"; break;
                case 1112: AlarmName_Text = "그리퍼 홈이동 재시도 초과"; break;
                case 1113: AlarmName_Text = "그리퍼 홈 설정 타임아웃"; break;
                case 1114: AlarmName_Text = "그리퍼 위치 이동 타임아웃"; break;
                case 1115: AlarmName_Text = "반송 명령 수신 시, 그리퍼 홈위치 아님"; break;
                case 1116: AlarmName_Text = "적재 명령 수신 시, 그리퍼 홈위치 아님"; break;

                //case 1201: AlarmName_Text = "승강 원점 설정 시간 초과"; break;
                //case 1202: AlarmName_Text = "CAGE Home A 센서 설정 이상"; break;
                //case 1203: AlarmName_Text = "CAGE Home B 센서 설정 이상"; break;
                case 1204: AlarmName_Text = "상승전 Cage Home A 센서 감지"; break;
                case 1205: AlarmName_Text = "상승전 Cage Home B 센서 감지"; break;
                case 1206: AlarmName_Text = "수동 상승중 Cage Home A 센서 감지"; break;
                case 1207: AlarmName_Text = "수동 상승중 Cage Home B 센서 감지"; break;
                case 1210: AlarmName_Text = "승강 원점 미설정"; break;
                case 1211: AlarmName_Text = "승강 원점 설정 타임아웃"; break;
                case 1212: AlarmName_Text = "승강 원점 실패(Cage Home A 미감지)"; break;
                case 1213: AlarmName_Text = "승강 원점 실패(Cage Home B 미감지)"; break;
                case 1214: AlarmName_Text = "CAGE Home A 센서 설정 이상"; break;
                case 1215: AlarmName_Text = "CAGE Home B 센서 설정 이상"; break;


                case 1301: AlarmName_Text = "타이어 감지 센서 이상1"; break;

                case 1401: AlarmName_Text = "타이어 높이 센서 감지 이상1"; break;
                case 1402: AlarmName_Text = "타이어 높이 센서 감지 이상2"; break;
                case 1403: AlarmName_Text = "타이어 높이 센서 감지 이상3"; break;

                case 1501: AlarmName_Text = "Guide Home 센서 감지 이상1"; break;
                case 1502: AlarmName_Text = "Guide Home 센서 감지 이상2"; break;

                case 1801: AlarmName_Text = "승강 중 승강밸트 텐션 HLD1 감지";break;
                case 1802: AlarmName_Text = "승강 중 승강밸트 텐션 HLD2 감지";break;
                case 1803: AlarmName_Text = "승강 중 승강밸트 텐션 HLD3 감지";break;
                case 1804: AlarmName_Text = "승강 중 승강밸트 텐션 HLD4 감지";break;

                case 2001: AlarmName_Text = "주행 전 Hoisting Carriage Home 위치 아님";break;
                case 2002: AlarmName_Text = "주행 중 Hoisting Carriage Home 위치 아님";break;
                case 2003: AlarmName_Text = "자동 모드 변경 시 Hoisting Carriage Home 위치 아님";break;
                case 2004: AlarmName_Text = "적재 재시작 시 Hoisting Carriage Home 위치 아님";break;
                case 2005: AlarmName_Text = "이재 재시작 시 Hoisting Carriage Home 위치 아님";break;
                case 2006: AlarmName_Text = "이동 재시작 시 Hoisting Carriage Home 위치 아님";break;
                case 2007: AlarmName_Text = "작업 완료처리 시 Hoisting Carriage Home 위치 아님"; break;
                //case 2010: AlarmName_Text = "Hoisting Carriage 원점 미설정";break;
                //case 2011: AlarmName_Text = "Hoisting Carriage 원점 설정 타임아웃";break;

                case 2701: AlarmName_Text = "주행 중 좌측 화물 이탈";break;
                case 2702: AlarmName_Text = "주행 중 우측 화물 이탈";break;
                case 3501: AlarmName_Text = "이재 완료 후, 화물 감지";break;
                case 3502: AlarmName_Text = "작업이 없는 상태에서 화물감지";break; //삭제 예정
                case 3601: AlarmName_Text = "적재 완료 후, 화물 미감지";break;
                case 3602: AlarmName_Text = "이재 이동 중, 화물 미감지";break;
                case 3603: AlarmName_Text = "Catch 동작 중, 화물 미감지";break;
                case 3604: AlarmName_Text = "Catch 승강 시, 화물 미감지";break;
                case 3605: AlarmName_Text = "Catch 이동 중, 화물 미감지"; break;
                case 3701: AlarmName_Text = "승강 중 좌측 화물 이탈";break;
                case 3702: AlarmName_Text = "승강 중 우측 화물 이탈";break;
                
                case 4101: AlarmName_Text = "전진 주행 정위치 초과"; break;
                case 4102: AlarmName_Text = "전진 주행 정위치 미달"; break;
                case 4103: AlarmName_Text = "주행 정위치 정지 크립 운전 시간 초과";break;
                case 4104: AlarmName_Text = "주행 정위치 정지 재시도 실패";break;
                case 4105: AlarmName_Text = "후진 주행 정위치 초과"; break;
                case 4106: AlarmName_Text = "후진 주행 정위치 미달"; break;
                case 4107: AlarmName_Text = "적재 하강 전 주행 정위치 이상"; break;
                case 4108: AlarmName_Text = "이재 하강 전 주행 정위치 이상"; break;

                case 4201: AlarmName_Text = "하강 정위치 이상";break;
                case 4202: AlarmName_Text = "상승 정위치 이상";break;
                case 4301: AlarmName_Text = "전방 주행 범퍼 감지";break;
                case 4302: AlarmName_Text = "후방 주행 범퍼 감지";break;
                case 4303: AlarmName_Text = "전방, 후방 주행 범퍼 감지";break;

                case 4401: AlarmName_Text = "전진 오버런 센서 감지"; break;
                case 4402: AlarmName_Text = "후진 오버런 센서 감지"; break;

                case 4501: AlarmName_Text = "전방대차 위치확인 안됨"; break;
                case 4502: AlarmName_Text = "이동 불가 위치 명령 수신"; break;
                case 4503: AlarmName_Text = "라이더 정지 대기 시간 초과"; break;
                //case 4504: AlarmName_Text = "주행 정지 센서 감지 정지"; break;
                //case 4505: AlarmName_Text = "감속 구간 속도 이상"; break;
                //case 4506: AlarmName_Text = "곡선구간 속도 이상"; break;
                case 4508: AlarmName_Text = "충돌방지 정지 - 전방 라이더 감지"; break;
                case 4509: AlarmName_Text = "충돌방지 정지 - 후방 라이더 감지"; break;
                case 4510: AlarmName_Text = "충돌방지 순서 설정 이상[GMC 충돌방지 미사용으로 설정]"; break;
                case 4511: AlarmName_Text = "충돌방지 순서 설정 이상[GMC 충돌방지 순서설정 되지 않음]"; break;
                case 4512: AlarmName_Text = "충돌방지 순서 설정 이상[GMC 충돌방지 순서설정에 해당 호기 누락]"; break;
                case 4513: AlarmName_Text = "충돌방지 설정 이상[충돌방지 미설정 상태인데 상대 호기 위치 정보가 확인됨]"; break;
                case 4514: AlarmName_Text = "충돌방지 설정 이상[충돌방지 설정 상태인데, 상대 호기 위치가 주행 범위 밖임]"; break;

                case 4701: AlarmName_Text = "전방 라이더 장애 "; break;
                case 4702: AlarmName_Text = "후방 라이더 장애 "; break;

                case 5101: AlarmName_Text = "비상 정지 스위치 동작"; break;
                case 5102: AlarmName_Text = "비상 정지 명령 수신";break;
                case 5401: AlarmName_Text = "주행 중 Cycle Stop 명령 수신";break;
                case 5402: AlarmName_Text = "승강 중 Cycle Stop 명령 수신";break;
                case 5403: AlarmName_Text = "명령대기 상태에서 Cycle Stop 명령 수신";break;
                case 5501: AlarmName_Text = "GMC 통신 타임 아웃";break;
                case 5502: AlarmName_Text = "Host Computer 통신 타임 아웃";break;

                case 6101: AlarmName_Text = "이동 명령 스테이션 위치 정보 이상"; break;
                case 6102: AlarmName_Text = "이재 스테이션 위치로 적재명령 수신"; break;
                case 6103: AlarmName_Text = "적재 스테이션 위치로 이재명령 수신"; break;
                case 6104: AlarmName_Text = "반송 명령 적재 스테이션 이상"; break;
                case 6105: AlarmName_Text = "반송 명령 이재 스테이션 이상"; break;
                case 6106: AlarmName_Text = "목적지 변경 위치 이상"; break;
                case 6107: AlarmName_Text = "반송 명령 적재 스테이션 높이 이상"; break;
                case 6108: AlarmName_Text = "반송 명령 이재 스테이션 높이 이상"; break;
                case 6109: AlarmName_Text = "반송 명령 처리 불가"; break;

                case 6401: AlarmName_Text = "적재 명령 수신 시, 화물 감지";break;
                case 6402: AlarmName_Text = "반송 명령 수신 시, 화물 감지";break;
                case 6403: AlarmName_Text = "화물 적재 이동 시작 전, 화물 감지"; break;
                case 6501: AlarmName_Text = "이재 명령 수신 시, 화물 미감지";break;
                //case 6502: AlarmName_Text = "이재 시작 시, 화물 미감지";break;
                case 6601: AlarmName_Text = "Hoisting Carriage PIO Go 신호 이상";break;
                case 6701: AlarmName_Text = "승강 중 하강 가능 인터락 신호 OFF";break;
                case 6702: AlarmName_Text = "승강 중 상승 가능 인터락 신호 OFF";break;
                case 7001: AlarmName_Text = "적재 하강가능 인터락 타임아웃";break;
                case 7002: AlarmName_Text = "적재 상승가능 인터락 타임아웃";break;
                case 7003: AlarmName_Text = "이재 하강가능 인터락 타임아웃";break;
                case 7004: AlarmName_Text = "이재 상승가능 인터락 타임아웃";break;
                case 7005: AlarmName_Text = "적재가능 인터락 대기시간 초과";break;
                case 7006: AlarmName_Text = "이재가능 인터락 대기시간 초과";break;
                case 7007: AlarmName_Text = "하강 중, 적재가능 신호 OFF"; break;
                case 7008: AlarmName_Text = "승강 비상정지 인터락 감지"; break;

                case 7601: AlarmName_Text = "적재 화물 감지 타임아웃"; break;
                case 7602: AlarmName_Text = "이재 화물 미감지 타임아웃"; break;

                case 9501: AlarmName_Text = "기본 정보 데이터 손상"; break;
                case 9502: AlarmName_Text = "장치 구조 설정 데이터 손상"; break;
                case 9503: AlarmName_Text = "MCU 입출력 설정 데이터 손상"; break;
                case 9504: AlarmName_Text = "제어 설정 데이터 손상"; break;
                case 9505: AlarmName_Text = "주행 드라이버 설정 데이터 손상"; break;
                case 9506: AlarmName_Text = "승강 드라이버 설정 데이터 손상"; break;
                case 9507: AlarmName_Text = "레일 주행 설정 데이터 손상"; break;
                case 9508: AlarmName_Text = "구간 설정 데이터 손상"; break;
                case 9509: AlarmName_Text = "스테이션 설정 데이터 손상"; break;
                case 9510: AlarmName_Text = "장치 구조 설정 초기화"; break;
                case 9511: AlarmName_Text = "MCU 입출력 미설정"; break;
                case 9512: AlarmName_Text = "제어 설정 초기화"; break;
                case 9513: AlarmName_Text = "주행 드라이버 설정 초기화"; break;
                case 9514: AlarmName_Text = "승강 드라이버 설정 초기화"; break;
                case 9515: AlarmName_Text = "레일 주행 설정 미설정"; break;
                case 9516: AlarmName_Text = "구간 설정 미설정"; break;
                case 9517: AlarmName_Text = "스테이션 설정 미설정"; break;
            }

            return AlarmName_Text;

        }
        public static string UTIL_RTVAlarmName_MemoryMap(byte Code1, byte Code2, ushort Code3)
        {
            string AlarmName_Text = Code1.ToString() + "-" + Code2.ToString() + "-" + Code3.ToString();

            UInt16 SumCode = (UInt16)(Code1 * 100 + Code2);

            switch (SumCode)
            {
                case 1   : AlarmName_Text = "보드 리셋[저전압]"; break;
                case 3   : AlarmName_Text = "보드 리셋[Watchdog]"; break;
                case 4   : AlarmName_Text = "보드 리셋[소프트웨어]"; break;
                case 5   : AlarmName_Text = "보드 리셋[전원 차단]"; break;
                case 6   : AlarmName_Text = "보드 리셋[하드웨어]"; break;
                case 102 : AlarmName_Text = "비상 정지"; break;
                case 104 : AlarmName_Text = "안전플러그 동작"; break;
                case 105 : AlarmName_Text = "비상 정지 스위치 동작(후방)"; break;
                case 106 : AlarmName_Text = "비상 정지 스위치 동작(전방 우측)"; break;
                case 107 : AlarmName_Text = "비상 정지 스위치 동작(전방 좌측)"; break;
                case 201 : AlarmName_Text = "전방 범퍼 동작"; break; 
                case 202 : AlarmName_Text = "후방 범퍼 동작"; break;
                case 203 : AlarmName_Text = "전방, 후방 범퍼 동작"; break;

                case 301:  AlarmName_Text = "주행 전 좌측화물이탈"; break;
                case 302 : AlarmName_Text = "주행 전 우측화물이탈"; break;
                case 303 : AlarmName_Text = "주행 중 좌측화물이탈"; break;
                case 304 : AlarmName_Text = "주행 중 우측화물이탈"; break;
                case 305 : AlarmName_Text = "주행 완료 후, 좌측화물이탈"; break;
                case 306 : AlarmName_Text = "주행 완료 후, 우측화물이탈"; break;
                case 311 : AlarmName_Text = "수동 주행 전 좌측화물이탈"; break;
                case 312 : AlarmName_Text = "수동 주행 전 우측화물이탈"; break;
                case 313:  AlarmName_Text = "수동 주행 중 좌측화물이탈"; break;
                case 314:  AlarmName_Text = "수동 주행 중 우측화물이탈"; break;
                case 315 : AlarmName_Text = "수동 피딩 중 좌측화물이탈"; break;
                case 316 : AlarmName_Text = "수동 피딩 중 우측화물이탈"; break;
                case 321 : AlarmName_Text = "적재 전 좌측화물이탈"; break;
                case 322 : AlarmName_Text = "적재 전 우측화물이탈"; break;
                case 323 : AlarmName_Text = "적재 중 좌측화물이탈"; break;
                case 324 : AlarmName_Text = "적재 중 우측화물이탈"; break;
                case 325 : AlarmName_Text = "적재 완료 후, 좌측화물이탈"; break;
                case 326 : AlarmName_Text = "적재 완료 후, 우측화물이탈"; break;
                case 331 : AlarmName_Text = "이재 전 좌측화물이탈"; break;
                case 332 : AlarmName_Text = "이재 전 우측화물이탈"; break;
                case 333 : AlarmName_Text = "이재 중 좌측화물이탈"; break;
                case 334 : AlarmName_Text = "이재 중 우측화물이탈"; break;
                case 335 : AlarmName_Text = "이재 완료 후, 좌측화물이탈"; break;
                case 336 : AlarmName_Text = "이재 완료 후, 우측화물이탈"; break;
                case 401 : AlarmName_Text = "(더블)피딩2 주행 전 좌측화물이탈"; break;
                case 402 : AlarmName_Text = "(더블)피딩2 주행 전 우측화물이탈"; break;
                case 403 : AlarmName_Text = "(더블)피딩2 주행 중 좌측화물이탈"; break;
                case 404 : AlarmName_Text = "(더블)피딩2 주행 중 우측화물이탈"; break;
                case 405 : AlarmName_Text = "(더블)피딩2 주행 완료 후, 좌측화물이탈"; break;
                case 406 : AlarmName_Text = "(더블)피딩2 주행 완료 후, 우측화물이탈"; break;
                case 411 : AlarmName_Text = "(더블)피딩2 수동 주행 전 좌측화물이탈"; break;
                case 412 : AlarmName_Text = "(더블)피딩2 수동 주행 전 우측화물이탈"; break;
                case 413:  AlarmName_Text = "(더블)피딩2 수동 주행 중 좌측화물이탈"; break;
                case 414:  AlarmName_Text = "(더블)피딩2 수동 주행 중 우측화물이탈"; break;
                case 415 : AlarmName_Text = "(더블)피딩2 수동 피딩 중 좌측화물이탈"; break;
                case 416 : AlarmName_Text = "(더블)피딩2 수동 피딩 중 우측화물이탈"; break;
                case 421 : AlarmName_Text = "(더블)피딩2 적재 전 좌측화물이탈"; break;
                case 422 : AlarmName_Text = "(더블)피딩2 적재 전 우측화물이탈"; break;
                case 423 : AlarmName_Text = "(더블)피딩2 적재 중 좌측화물이탈"; break;
                case 424 : AlarmName_Text = "(더블)피딩2 적재 중 우측화물이탈"; break;
                case 425 : AlarmName_Text = "(더블)피딩2 적재 완료 후, 좌측화물이탈"; break;
                case 426 : AlarmName_Text = "(더블)피딩2 적재 완료 후, 우측화물이탈"; break;
                case 431 : AlarmName_Text = "(더블)피딩2 이재 전 좌측화물이탈"; break;
                case 432 : AlarmName_Text = "(더블)피딩2 이재 전 우측화물이탈"; break;
                case 433 : AlarmName_Text = "(더블)피딩2 이재 중 좌측화물이탈"; break;
                case 434 : AlarmName_Text = "(더블)피딩2 이재 중 우측화물이탈"; break;
                case 435 : AlarmName_Text = "(더블)피딩2 이재 완료 후, 좌측화물이탈"; break;
                case 436 : AlarmName_Text = "(더블)피딩2 이재 완료 후, 우측화물이탈"; break;
                case 501 : AlarmName_Text = "위치센서 에러"; break;
                case 502 : AlarmName_Text = "위치센서 거리급변"; break;
                case 601 : AlarmName_Text = "인버터1 이상(인버터 알람)"; break;
                case 602 : AlarmName_Text = "인버터1 이상(인버터 구동 실패)"; break;
                case 603 : AlarmName_Text = "인버터1 이상(인버터 구동 실패)"; break;
                case 604 : AlarmName_Text = "인버터1 이상(정지속도 이상)"; break;
                case 605 : AlarmName_Text = "인버터1 이상(인버터연결끊김)"; break;
                case 608 : AlarmName_Text = "인버터1 이상(대차이동 없음)"; break;
                case 609 : AlarmName_Text = "인버터1 이상(Safety 입력없음)"; break;
                case 610 : AlarmName_Text = "인버터1 이상(대차반대이동)"; break;
                case 611 : AlarmName_Text = "인버터1 이상(주행 불가 상태)"; break;
                case 701 : AlarmName_Text = "주행MC / 절체 동작불량"; break;
                case 702 : AlarmName_Text = "피딩MC / 절체 동작불량"; break;

                case 801: AlarmName_Text = "전방 피딩 회전 감지 이상"; break;
                case 802: AlarmName_Text = "후방 피딩 회전 감지 이상"; break;
                
                case 811: AlarmName_Text = "(자동)적재 연동 전 이상(C/V 인터락 ON)"; break;
                case 812: AlarmName_Text = "(자동)적재 연동 중 이상(C/V 이재요구신호 OFF)"; break;
                case 813: AlarmName_Text = "(자동)적재 연동 중 이상(화물감속신호 ON 전환없음)"; break;
                case 814: AlarmName_Text = "(자동)적재 완료처리 이상(C/V 이재요구신호 ON)"; break;
                case 821: AlarmName_Text = "(자동)이재 연동 전 이상(C/V 인터락 ON)"; break;
                case 822: AlarmName_Text = "(자동)이재 연동 중 이상(C/V 이재가능신호 OFF)"; break;
                case 823: AlarmName_Text = "(자동)이재 연동 중 이상(C/V 이재완료신호 OFF)"; break;
                case 824: AlarmName_Text = "(자동)이재 완료처리 이상(C/V 이재가능신호 ON)"; break;
                case 831: AlarmName_Text = "(수동)적재 연동 전 이상(C/V 수동신호 OFF 전환)"; break;
                case 832: AlarmName_Text = "(수동)적재 연동 중 이상(C/V 수동신호 OFF 전환)"; break;
                case 833: AlarmName_Text = "(수동)적재 연동 중 이상(C/V 이재요구신호 OFF 전환)"; break;
                case 841: AlarmName_Text = "(수동)이재 연동 전 이상(C/V 수동신호 OFF 전환)"; break;
                case 842: AlarmName_Text = "(수동)이재 연동 중 이상(C/V 수동신호 OFF 전환)"; break;
                case 843: AlarmName_Text = "(수동)이재 연동 중 이상(C/V 이재가능신호 OFF 전환)"; break;

                case 901 : AlarmName_Text = "EtherCAT 통신 설정 이상"; break;
                case 902 : AlarmName_Text = "EtherCAT 통신 초기화 실패(Slave ID 이상)"; break;
                case 903 : AlarmName_Text = "EtherCAT 슬레이브 개수 이상"; break;
                case 904 : AlarmName_Text = "EtherCAT 연결 끊김(HNS1)"; break;
                case 905 : AlarmName_Text = "EtherCAT 통신 초기화 실패(Slave Type 이상)"; break;
                case 906 : AlarmName_Text = "EtherCAT 수신 타임아웃"; break;
                case 907 : AlarmName_Text = "EtherCAT 연결 끊김(HNS2)"; break;
                case 908 : AlarmName_Text = "EtherCAT 연결 끊김(HNS3)"; break;
                case 909 : AlarmName_Text = "EtherCAT 연결 끊김(HNS4)"; break;
                case 910 : AlarmName_Text = "EtherCAT 연결 끊김(HNS5)"; break;
                case 1001: AlarmName_Text = "브레이크개방 이상(MC 동작 신호 이상)"; break;
                case 1002: AlarmName_Text = "브레이크개방 이상(브레이크 동작 신호 이상)"; break;
                case 1003: AlarmName_Text = "브레이크개방 이상(수동 주행 브레이크 이상)"; break;
                case 1004: AlarmName_Text = "자동 / 수동 선택 스위치 이상"; break;
                case 1101: AlarmName_Text = "정위치 정지 이상(정위치 미달)"; break;
                case 1102: AlarmName_Text = "정위치 정지 이상(크립동작 시간 초과)"; break;
                case 1103: AlarmName_Text = "정위치 정지 이상(정위치 초과)"; break;
                case 1201: AlarmName_Text = "피딩 인버터 이상(인버터알람)"; break;
                case 1202: AlarmName_Text = "피딩2 인버터 이상(인버터알람)"; break;
                case 1204: AlarmName_Text = "피딩 이상(정지속도 이상)"; break;
                case 1205: AlarmName_Text = "피딩2 이상(정지속도 이상)"; break;
                case 1206: AlarmName_Text = "피딩 인버터 이상(구동 신호 미감지)"; break;
                case 1207: AlarmName_Text = "피딩2 인버터 이상(구동 신호 미감지)"; break;
                case 1303: AlarmName_Text = "화물 이재 후 화물 감지"; break;
                case 1401: AlarmName_Text = "충돌방지 동작중 전방대차 위치확인 안됨"; break;
                case 1402: AlarmName_Text = "주행 불가능 위치 명령 수신"; break;
                case 1403: AlarmName_Text = "라이더 물체 감지 후 정지 대기 시간 초과"; break;
                case 1404: AlarmName_Text = "주행 정지 센서 감지 정지"; break;
                case 1405: AlarmName_Text = "감속 구간 속도 이상"; break;
                case 1406: AlarmName_Text = "곡선구간 속도 이상"; break;
                case 1407: AlarmName_Text = "주행 제어 이상(충돌방지)"; break;
                case 1408: AlarmName_Text = "주행 제어 이상(전방대차통신두절)"; break;
                case 1409: AlarmName_Text = "주행 제어 이상(라이다감지)"; break;

                case 1410: AlarmName_Text = "충돌방지 순서 설정 이상[GMC 충돌방지 미사용으로 설정]"; break;
                case 1411: AlarmName_Text = "충돌방지 순서 설정 이상[GMC 충돌방지 순서설정 되지 않음]"; break;
                case 1412: AlarmName_Text = "충돌방지 순서 설정 이상[GMC 충돌방지 순서설정에 해당 호기 누락]"; break;
                case 1413: AlarmName_Text = "충돌방지 설정 이상[충돌방지 미설정 상태인데 상대 호기 위치 정보가 확인됨]"; break;
                case 1414: AlarmName_Text = "충돌방지 설정 이상[충돌방지 설정 상태인데, 상대 호기 위치가 주행 범위 밖임]"; break;

                case 1501: AlarmName_Text = "기본 설정 데이터 이상"; break;
                case 1502: AlarmName_Text = "장치 구조 설정 이상"; break;
                case 1503: AlarmName_Text = "DIO 설정 이상"; break;
                case 1504: AlarmName_Text = "제어 설정 이상"; break;
                case 1505: AlarmName_Text = "주행 드라이브 설정 이상"; break;
                case 1506: AlarmName_Text = "피딩 드라이브 설정 이상"; break;
                case 1507: AlarmName_Text = "레일 주행 설정 이상"; break;
                case 1508: AlarmName_Text = "구간 설정 이상"; break;
                case 1509: AlarmName_Text = "스테이션 설정 이상"; break;
                case 1510: AlarmName_Text = "기본 설정 데이터 이상"; break;
                case 1511: AlarmName_Text = "기본 설정 데이터 이상"; break;
                case 1512: AlarmName_Text = "장치 구조 설정 이상"; break;
                case 1513: AlarmName_Text = "DIO 설정 이상"; break;
                case 1514: AlarmName_Text = "제어 설정 이상"; break;
                case 1515: AlarmName_Text = "주행 드라이브 설정 이상"; break;
                case 1516: AlarmName_Text = "피딩 드라이브 설정 이상"; break;
                case 1517: AlarmName_Text = "레일 주행 설정 이상"; break;
                case 1518: AlarmName_Text = "구간 설정 이상"; break;
                case 1519: AlarmName_Text = "스테이션 설정 이상"; break;
                case 1601: AlarmName_Text = "사이클 스탑"; break;
            }

            return AlarmName_Text;
        }

        public static string UTIL_RTVAlarmName_14Bytes(byte Code1, byte Code2, ushort Code3)
        {
            string AlarmName_Text = Code1.ToString() + "-" + Code2.ToString() + "-" + Code3.ToString();

            UInt16 SumCode = (UInt16)(Code1 * 100 + Code2);

            switch (SumCode)
            {
                case 1:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "보드 리셋(저전압)"; break;
                        case 3: AlarmName_Text = "보드 리셋(Watchdog)"; break;
                        case 4: AlarmName_Text = "보드 리셋(소프트웨어)"; break;
                        case 5: AlarmName_Text = "보드 리셋(전원 차단)"; break;
                        case 6: AlarmName_Text = "보드 리셋(하드웨어)"; break;
                    };
                    break;

                case 102: AlarmName_Text = "비상 정지"; break;
                case 104: AlarmName_Text = "안전플러그 동작"; break;
                case 105: AlarmName_Text = "비상 정지 스위치 동작(후방)"; break;
                case 106: AlarmName_Text = "비상 정지 스위치 동작(전방 우측)"; break;
                case 107: AlarmName_Text = "비상 정지 스위치 동작(전방 좌측)"; break;
                case 201: AlarmName_Text = "전방 범퍼 동작"; break;
                case 202: AlarmName_Text = "후방 범퍼 동작"; break;
                case 203: AlarmName_Text = "전방, 후방 범퍼 동작"; break;

                case 301: 
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "이동중 좌측화물이탈"; break;
                        case 2: AlarmName_Text = "이동중 우측화물이탈(싱글배드타입)"; break;
                        case 3: AlarmName_Text = "(피딩1 좌측) 주행중 화물이탈센서 감지 "; break;
                    };
                    break;

                case 302:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "화물 적재 완료 후, 좌측화물이탈"; break;
                        case 2: AlarmName_Text = "화물 적재 완료 후, 우측화물이탈"; break;
                        case 3: AlarmName_Text = "(피딩1 우측) 주행중 화물이탈센서 감지 "; break;
                    };
                    break;

                case 303:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "화물 이재 완료 후, 좌측화물이탈"; break;
                        case 2: AlarmName_Text = "화물 이재 완료 후, 우측화물이탈"; break;
                        case 3: AlarmName_Text = "(피딩1 좌측) 화물 적재 완료 후, 화물이탈 센서 감지"; break;
                    };
                    break;

                case 304:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "화물 이재 전 좌측화물이탈"; break;
                        case 2: AlarmName_Text = "화물 이재 전 우측화물이탈"; break;
                        case 3: AlarmName_Text = "(피딩1 우측) 화물 적재 완료 후, 화물이탈 센서 감지"; break;
                    };
                    break;

                case 305:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "화물 적재 전 좌측화물이탈"; break;
                        case 2: AlarmName_Text = "화물 적재 전 우측화물이탈"; break;
                        case 3: AlarmName_Text = "(피딩1 좌측)화물 이재 완료 후, 화물이탈 센서 감지"; break;
                    };
                    break;
                    
                case 306:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "화물 적재 중 좌측화물이탈"; break;
                        case 2: AlarmName_Text = "화물 적재 중 우측화물이탈"; break;
                        case 3: AlarmName_Text = "(피딩1 우측)화물 이재 완료 후, 화물이탈 센서 감지"; break;
                    };
                    break;

                case 307:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "화물 이재 전 좌측화물이탈"; break;
                        case 2: AlarmName_Text = "화물 이재 전 우측화물이탈"; break;
                        case 3: AlarmName_Text = "(피딩1 좌측)화물 적재 전 화물이탈."; break;
                    };
                    break;

                case 308:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩1 우측)화물 적재 전 화물이탈."; break;
                    };
                    break;
                case 309:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩1 좌측)화물 적재 중 화물이탈."; break;
                    };
                    break;
                case 310:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩1 우측)화물 적재 중 화물이탈."; break;
                    };
                    break;
                case 311:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩1 좌측)화물 이재 전 화물이탈."; break;
                    };
                    break;
                case 312:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩1 우측)화물 이재 전 화물이탈."; break;
                    };
                    break;
                case 313: AlarmName_Text = "피딩 동작 중, 우측 이탈 감지"; break;
                case 314: AlarmName_Text = "피딩 동작 중, 좌측 이달 감지"; break;

                case 401:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩2 좌측) 주행중 화물이탈센서 감지 "; break;

                    };
                    break;
                case 402:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩2 우측) 주행중 화물이탈센서 감지 "; break;

                    };
                    break;
                case 403:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩2 좌측) 화물 적재 완료 후, 화물이탈 센서 감지"; break;

                    };
                    break;
                case 404:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩2 우측) 화물 적재 완료 후, 화물이탈 센서 감지"; break;

                    };
                    break;
                case 405:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩2 좌측)화물 이재 완료 후, 화물이탈 센서 감지"; break;

                    };
                    break;
                case 406:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩2 우측)화물 이재 완료 후, 화물이탈 센서 감지"; break;

                    };
                    break;
                case 407:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩2 좌측)화물 적재 전 화물이탈."; break;

                    };
                    break;
                case 408:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩2 우측)화물 적재 전 화물이탈."; break;

                    };
                    break;
                case 409:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩2 좌측)화물 적재 중 화물이탈."; break;

                    };
                    break;
                case 410:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩2 우측)화물 적재 중 화물이탈."; break;

                    };
                    break;
                case 411:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩2 좌측)화물 이재 전 화물이탈."; break;

                    };
                    break;
                case 412:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "(피딩2 우측)화물 이재 전 화물이탈."; break;

                    };
                    break;
                case 501: AlarmName_Text = "위치센서 에러"; break;

                case 502: AlarmName_Text = "위치센서 거리급변"; break;
                case 503: AlarmName_Text = "위치센서 최대 범위 초과"; break;
                case 601: AlarmName_Text = "인버터1 이상(인버터 알람)"; break;
                case 602: AlarmName_Text = "인버터1 이상(인버터 구동 실패)"; break;
                case 603: AlarmName_Text = "인버터1 이상(인버터 구동 실패)"; break;
                case 604: AlarmName_Text = "인버터1 이상(정지속도 이상)"; break;
                case 605: AlarmName_Text = "인버터1 이상(인버터연결끊김)"; break;

                case 608:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "인버터1 이상(대차이동 이상_이동불가)"; break;
                        case 2: AlarmName_Text = "인버터1 이상(대차이동 이상_역이동)"; break;
                    };
                    break;

                case 609:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "인버터1 이상(Safety 입력없음)"; break;

                    };
                    break;
                case 701: AlarmName_Text = "주행MC/절체 동작불량"; break;
                case 702: AlarmName_Text = "피딩MC/절체 동작불량"; break;

                case 801:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "피딩 시간 초과(C/V 인터락 신호 이상)"; break;
                        case 2: AlarmName_Text = "피딩 시간 초과(C/V 이재요구 타임아웃)"; break;
                        case 4: AlarmName_Text = "피딩 시간 초과(C/V 이재가능 타임아웃)"; break;
                    };
                    break;
                    
                case 802:
                    switch (Code3)
                    {
                        case 2: AlarmName_Text = "이재/적재 시간 초과(적재 타임아웃)"; break;
                        case 3: AlarmName_Text = "이재/적재 시간 초과(C/V 이재 요구 신호 이상)"; break;
                        case 5: AlarmName_Text = "이재/적재 시간 초과(C/V 이재 완료 타임아웃)"; break;
                    };
                    break;
                case 803: AlarmName_Text = "화물 이재 완료전, 컨베이어 완료 신호 ON"; break;
                case 804: AlarmName_Text = "전방 피딩 회전 감지 이상"; break;
                case 805: AlarmName_Text = "후방 피딩 회전 감지 이상"; break;

                case 901: AlarmName_Text = "EtherCAT 통신 설정 이상"; break;
                case 902: AlarmName_Text = "EtherCAT 통신 초기화 실패(Slave ID 이상)"; break;
                case 903: AlarmName_Text = "EtherCAT 슬레이브 개수 이상"; break;
                case 904: AlarmName_Text = "EtherCAT 연결 끊김(HNS1)"; break;
                case 905: AlarmName_Text = "EtherCAT 통신 초기화 실패(Slave Type 이상)"; break;
                case 906: AlarmName_Text = "EtherCAT 수신 타임아웃"; break;
                case 907: AlarmName_Text = "EtherCAT 연결 끊김(HNS2)"; break;
                case 908: AlarmName_Text = "EtherCAT 연결 끊김(HNS3)"; break;
                case 909: AlarmName_Text = "EtherCAT 연결 끊김(HNS4)"; break;
                case 910: AlarmName_Text = "EtherCAT 연결 끊김(HNS5)"; break;

                case 1001:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "브레이크개방 이상(MC 동작 신호 이상)"; break;
                        case 2: AlarmName_Text = "브레이크개방 이상(브레이크 동작 신호 이상)"; break;
                        case 3: AlarmName_Text = "브레이크개방 이상(수동 주행 브레이크 이상)"; break;
                    };
                    break;
                case 1101:
                    switch (Code3)
                    {
                        case 3: AlarmName_Text = "정위치 정지 이상(정위치 미달)"; break;
                        case 4: AlarmName_Text = "정위치 정지 이상(정위치 초과)"; break;
                    };
                    break;

                case 1102: AlarmName_Text = "정위치 정지 이상(크립동작 시간 초과)"; break;

                case 1103:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "정위치 정지 이상(오버런 전진)"; break;
                        case 2: AlarmName_Text = "정위치 정지 이상(오버런 후진)"; break;
                    };
                    break;
                case 1201: AlarmName_Text = "피딩 인버터 이상(인버터알람)"; break;
                case 1202: AlarmName_Text = "피딩2 인버터 이상(인버터알람)"; break;
                case 1204: AlarmName_Text = "피딩 이상(정지속도 이상)"; break;
                case 1205: AlarmName_Text = "피딩2 이상(정지속도 이상)"; break;
                case 1206: AlarmName_Text = "피딩 인버터 이상(구동 신호 미감지)"; break;
                case 1207: AlarmName_Text = "피딩2 인버터 이상(구동 신호 미감지)"; break;

                case 1303: AlarmName_Text = "화물 이재 후 화물 감지"; break;

                case 1401: AlarmName_Text = "충돌방지 동작중 전방대차 위치확인 안됨"; break;
                case 1402: AlarmName_Text = "주행 불가능 위치 명령 수신"; break;
                case 1403: AlarmName_Text = "라이더 물체 감지 후 정지 대기 시간 초과"; break;
                case 1404: AlarmName_Text = "주행 정지 센서 감지 정지"; break;
                case 1405: AlarmName_Text = "감속 구간 속도 이상"; break;

                case 1406:
                    switch (Code3)
                    {
                        case 1: AlarmName_Text = "곡선구간 속도 이상"; break;
                        case 2: AlarmName_Text = "주행 제어 이상(충돌방지)"; break;
                        case 3: AlarmName_Text = "주행 제어 이상(전방대차통신두절)"; break;
                        case 4: AlarmName_Text = "주행 제어 이상(라이다감지)"; break;
                    };
                    break;

                case 1407: AlarmName_Text = "충돌방지 설정 이상(해당호기 미설정 )"; break;
                case 1408: AlarmName_Text = "충돌방지 설정 이상(미설정 )"; break;
                case 1409: AlarmName_Text = "충돌방지 설정 이상(GMC 충돌방지 미사용 )"; break;

                case 1410:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "전방대차 위치값 이상(바코드 인식 불가)"; break;
                        case 1: AlarmName_Text = "전방대차 위치값 이상(주행 범위 초과)"; break;
                    };
                    break;

                case 1501:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "기본 설정 데이터 이상(데이터 손상)"; break;
                        case 1: AlarmName_Text = "기본 설정 데이터 이상(기본값 설정)"; break;
                        case 2: AlarmName_Text = "기본 설정 데이터 이상(백업설정 복구)"; break;
                    };
                    break;

                case 1502:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "장치 구조 설정 이상(데이터 손상)"; break;
                        case 1: AlarmName_Text = "장치 구조 설정 이상(미설정)"; break;
                    };
                    break;

                case 1503:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "DIO 설정 이상(데이터 손상)"; break;
                        case 1: AlarmName_Text = "DIO 설정 이상(미설정)"; break;
                    };
                    break;

                case 1504:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "제어 설정 이상(데이터 손상)"; break;
                        case 1: AlarmName_Text = "제어 설정 이상(미설정)"; break;
                    };
                    break;

                case 1505:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "주행 드라이브 설정 이상(데이터 손상)"; break;
                        case 1: AlarmName_Text = "주행 드라이브 설정 이상(미설정)"; break;
                    };
                    break;


                case 1506:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "피딩 드라이브 설정 이상(데이터 손상)"; break;
                        case 1: AlarmName_Text = "피딩 드라이브 설정 이상(미설정)"; break;
                    };
                    break;

                case 1507:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "레일 주행 설정 이상(데이터 손상)"; break;
                        case 1: AlarmName_Text = "레일 주행 설정 이상(미설정)"; break;
                    };
                    break;

                case 1508:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "구간 설정 이상(데이터 손상)"; break;
                        case 1: AlarmName_Text = "구간 설정 이상(미설정)"; break;
                    };
                    break;

                case 1509:
                    switch (Code3)
                    {
                        case 0: AlarmName_Text = "스테이션 설정 이상(데이터 손상)"; break;
                        case 1: AlarmName_Text = "스테이션 설정 이상(미설정)"; break;
                    };
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
                            case 1: AlarmName_Text = "보드 리셋[저전압]"; break;
                            case 3: AlarmName_Text = "보드 리셋[Watchdog]"; break;
                            case 4: AlarmName_Text = "보드 리셋[소프트웨어]"; break;
                            case 5: AlarmName_Text = "보드 리셋[전원 차단]"; break;
                            case 6: AlarmName_Text = "보드 리셋[하드웨어]"; break;
                        }
                        break;
                    case  301: AlarmName_Text = "지상반 비상정지"; break;
                    case  302: AlarmName_Text = "WCS 비상정지"; break;
                    case  303: AlarmName_Text = "WCS Cycle 정지"; break;

                    case  601: AlarmName_Text = "안전플러그 동작"; break;

                    case 1001: AlarmName_Text = "승강 전방 로프 텐션 이상"; break;
                    case 1002: AlarmName_Text = "승강 후방 로프 텐션 이상"; break;
                    case 1003: AlarmName_Text = "조속기 동작 감지"; break;
                    case 1101: AlarmName_Text = "주행 정위치 초과"; break;
                    case 1102: AlarmName_Text = "주행 정위치 미달";break;
                    case 1103: AlarmName_Text = "주행 정위치 이상"; break;
                    case 1105: AlarmName_Text = "승강 정위치 초과";break;
                    case 1106: AlarmName_Text = "승강 정위치 미달";break;
                    case 1107: AlarmName_Text = "승강 정위치 이상"; break;
                    case 1109: AlarmName_Text = "포크1 정위치 초과";break;
                    case 1110: AlarmName_Text = "포크1 정위치 미달";break;

                    case 1201: AlarmName_Text = "랙 포스트 감지[우측 전방 이상]"; break;
                    case 1202: AlarmName_Text = "랙 포스트 감지[우측 후방 이상]"; break;
                    case 1203: AlarmName_Text = "랙 포스트 감지[좌측 전방 이상]"; break;
                    case 1204: AlarmName_Text = "랙 포스트 감지[좌측 후방 이상]"; break;


                    case 2001: AlarmName_Text = "화물 가로폭 좌측 이탈[포크1 중심 - GWL1 감지]"; break;;
                    case 2002: AlarmName_Text = "화물 가로폭 우측 이탈[포크1 중심 - GWR1 감지]";break;
                    case 2003: AlarmName_Text = "화물 가로폭 좌측 이탈[포크1 중심 - GWL1e 감지]";break;;
                    case 2004: AlarmName_Text = "화물 가로폭 우측 이탈[포크1 중심 - GWR1e 감지]";break;;
                    case 2005: AlarmName_Text = "화물 가로폭 좌측 이탈[포크1 우OUT - GWL1 감지]";break;
                    case 2006: AlarmName_Text = "화물 가로폭 우측 이탈[포크1 좌OUT - GWR1 감지]";break;
                    case 2007: AlarmName_Text = "화물 가로폭 좌측 이탈[포크1 우IN - GWL1 감지]";break;
                    case 2008: AlarmName_Text = "화물 가로폭 우측 이탈[포크1 좌IN - GWR1 감지]";break;
                    case 2009: AlarmName_Text = "화물 가로폭 좌측 이탈[포크1 이재 후 - GWL1 감지]";break;
                    case 2010: AlarmName_Text = "화물 가로폭 우측 이탈[포크1 이재 후 - GWR1 감지]";break;

                    case 2101: AlarmName_Text = "화물 세로폭  이탈[포크1 전방좌측]";break;
                    case 2102: AlarmName_Text = "화물 세로폭 이탈[포크1 전방우측]"; break;
                    case 2103: AlarmName_Text = "화물 세로폭 이탈[포크1 후방좌측]"; break;
                    case 2104: AlarmName_Text = "화물 세로폭 이탈[포크1 후방우측]"; break;
                    case 2105: AlarmName_Text = "화물 높이폭 이탈[포크1 좌측]"; break;
                    case 2106: AlarmName_Text = "화물 높이폭 이탈[포크1 우측]"; break;
                    case 2301: AlarmName_Text = "스페셜 랙 이상"; break;
                    case 2601: AlarmName_Text = "비상정지[승강 L / S 동작]";break;
                    case 2602: AlarmName_Text = "비상정지[주행 L / S 동작]";break;

                    case 3303: AlarmName_Text = "포크1 센서 이상[중심 아님]"; break;
                    case 3305: AlarmName_Text = "포크1 센서 이상[승강 전 - FEL1 미감지]";break;
                    case 3307: AlarmName_Text = "포크1 센서 이상[승강 전 - FER1 미감지]";break;
                    case 3309: AlarmName_Text = "포크1 센서 이상[승강 후 - FHL1 미감지]"; break;
                    case 3310: AlarmName_Text = "포크1 센서 이상[승강 후 - FML1 미감지]"; break;
                    case 3311: AlarmName_Text = "포크1 센서 이상[중심 이동 후 - FCL1 미감지]"; break;
                    case 3312: AlarmName_Text = "포크1 센서 이상[좌측 진출]"; break;
                    case 3313: AlarmName_Text = "포크1 센서 이상[우측 진출]"; break;
                    case 3314: AlarmName_Text = "포크1 센서 이상[중심 이동 후 - FCR1 미감지]"; break;
                    case 3315: AlarmName_Text = "포크1 센서 이상[승강 후 - FEL1 미감지]"; break;
                    case 3316: AlarmName_Text = "포크1 센서 이상[승강 후 - FHR1 미감지]"; break;
                    case 3317: AlarmName_Text = "포크1 센서 이상[승강 후 - FMR1 미감지]"; break;
                    case 3318: AlarmName_Text = "포크1 센서 이상[승강 후 - FER1 미감지]"; break;
                    case 3319: AlarmName_Text = "포크1 센서 이상[승강 전, FHL1 미감지]"; break;
                    case 3321: AlarmName_Text = "포크1 센서 이상[승강 전, FHR1 미감지]"; break;
                    case 3323: AlarmName_Text = "포크1 센서 이상[승강 전, FML1 미감지]"; break;
                    case 3325: AlarmName_Text = "포크1 센서 이상[승강 전, FMR1 미감지]"; break;
                    case 3327: AlarmName_Text = "포크1 센서 이상[중심 이동 전, FCL1 감지]"; break;
                    case 3328: AlarmName_Text = "포크1 센서 이상[중심 이동 전, FCR1 감지]"; break;

                    case 3801: AlarmName_Text = "승강 감속 센서 이상"; break;
                    case 3901: AlarmName_Text = "주행 감속 센서 이상"; break;
                    case 4401: AlarmName_Text = "포크1 인버터 이상[인버터 에러]"; break;
                    case 4402: AlarmName_Text = "포크1 인버터 이상[외부엔코더]";break;
                    case 4403: AlarmName_Text = "포크1 인버터 이상[STO 미감지]"; break;
                    case 4404: AlarmName_Text = "포크1 인버터 이상[제어실패]";break;
                    case 4405: AlarmName_Text = "포크1 인버터 이상[과부하]";break;
                    case 4601: AlarmName_Text = "주행 인버터 이상[인버터 에러]";break;
                    case 4602: AlarmName_Text = "주행 인버터 이상[외부엔코더]";break;
                    case 4603: AlarmName_Text = "주행 인버터 이상[STO 미감지]"; break;
                    case 4604: AlarmName_Text = "주행 인버터 이상[제어실패]";break;
                    case 4605: AlarmName_Text = "주행 인버터 이상[과부하]";break;
                    case 4701: AlarmName_Text = "승강 인버터 이상[인버터 에러]";break;
                    case 4702: AlarmName_Text = "승강 인버터 이상[외부엔코더]";break;
                    case 4703: AlarmName_Text = "승강 인버터 이상[STO 미감지]"; break;
                    case 4704: AlarmName_Text = "승강 인버터 이상[제어실패]";break;
                    case 4705: AlarmName_Text = "승강 인버터 이상[과부하]";break;
                    case 5301: AlarmName_Text = "승강 감속 이상[상승 감속 도그 감지시 속도 이상]";break;
                    case 5302: AlarmName_Text = "승강 감속 이상[하강 감속 도그 감지시 속도 이상]";break;
                    case 5401: AlarmName_Text = "주행 감속 이상[전진 감속 도그 감지시 속도 이상]";break;
                    case 5402: AlarmName_Text = "주행 감속 이상[후진 감속 도그 감지시 속도 이상]";break;
                    case 6001: AlarmName_Text = "좌측 이중입고[포크1 렉 DSTL1 감지]";break;
                    case 6004: AlarmName_Text = "좌측 이중입고[포크1 스테이션 DSTL1 감지]";break;
                    case 6013: AlarmName_Text = "좌측 이중입고[포크1 DSTLR1 감지]";break;
                    case 6014: AlarmName_Text = "좌측 이중입고[포크1 선입고 화물 ODSTL1 감지]";break;
                    case 6015: AlarmName_Text = "좌측 이중입고[포크1 랙 DSTLe1 감지]";break;
                    case 6017: AlarmName_Text = "좌측 이중입고[포크1 스테이션 DSTLe1 감지]";break;
                    case 6101: AlarmName_Text = "우측 이중입고[포크1 렉 DSTR1 감지]";break;
                    case 6104: AlarmName_Text = "우측 이중입고[포크1 렉 DSTR1 감지]";break;
                    case 6113: AlarmName_Text = "우측 이중입고[포크1 DSTRR1 감지]";break;
                    case 6114: AlarmName_Text = "우측 이중입고[포크1 선입고 화물 ODSTR1 감지]";break;
                    case 6115: AlarmName_Text = "우측 이중입고[포크1 랙 DSTRe1 감지]";break;
                    case 6117: AlarmName_Text = "우측 이중입고[포크1 스테이션 DSTRe1 감지]";break;
                    case 6302: AlarmName_Text = "화물 이상 2[이재 작업 완료 후 화물 감지]";break;
                    case 6303: AlarmName_Text = "화물 이상 2[적재 작업 전 화물 감지]";break;
                    case 6304: AlarmName_Text = "화물 이상 2[이재 작업 전 화물 미감지]";break;
                    case 6305: AlarmName_Text = "화물 이상 2 [포크1 Sticky 작업 전 화물 감지]"; break;
                    case 6306: AlarmName_Text = "화물 이상 2 [포크1 이재 진출 완료 후 화물 감지]"; break;
                    case 6307: AlarmName_Text = "화물 이상 2 [포크1 이재 후 복귀중 화물 감지]"; break;
                    case 6401: AlarmName_Text = "공출고 / 공입고[렉에서 적재 완료 후 화물 미감지]";break;
                    case 6404: AlarmName_Text = "공출고 / 공입고[스테이션에서 적재 완료 후 화물 미감지]";break;
                    case 6501: AlarmName_Text = "LOADED[작업번호없는 화물 감지]";break;
                    case 6601: AlarmName_Text = "작업명령 이상[금지렉으로 작업명령 수신]";break;
                    case 6602: AlarmName_Text = "작업명령 이상[Level 정보 이상]";break;
                    case 6603: AlarmName_Text = "작업명령 이상[Row 정보 이상]";break;
                    case 6604: AlarmName_Text = "작업명령 이상[Bay 정보 이상]";break;
                    case 6605: AlarmName_Text = "작업명령 이상[Station 정보 이상]";break;
                    case 6606: AlarmName_Text = "작업명령 이상[출고 Station으로 입고명령 수신]";break;
                    case 6607: AlarmName_Text = "작업명령 이상[입고 Station으로 출고명령 수신]";break;

                    case 8001: AlarmName_Text =  "주행 브레이크 해제 이상 [주행]"; break;
                    case 8002: AlarmName_Text =  "주행 시간초과 이상"; break;
                    case 8003: AlarmName_Text =  "승강 시간초과 이상"; break;
                    case 8004: AlarmName_Text =  "화물 적재 승강 시간초과 이상"; break;
                    case 8005: AlarmName_Text =  "화물 이재 승강 시간초과 이상"; break;
                    case 8006: AlarmName_Text =  "승강 브레이크 해제 이상 [승강]"; break;
                    case 8101: AlarmName_Text =  "포크1 진출 시간초과 이상"; break;
                    case 8102: AlarmName_Text =  "포크1 중심 시간초과 이상"; break;
                    case 8103: AlarmName_Text =  "포크1 브레이브 해제 이상"; break;
                    case 8201: AlarmName_Text =  "STATION 대기시간 초과 이상"; break;
                    case 8901: AlarmName_Text =   "승강 원점확인 이상"; break;
                    case 8902: AlarmName_Text =   "주행 원점확인 이상"; break;
                    case 8903: AlarmName_Text =   "포크1 원점확인 이상"; break;
                    case 9601: AlarmName_Text =   "광모뎀 이상"; break;
                    case 9701: AlarmName_Text  =  "기상반 도어 열림"; break;
                    case 9801: AlarmName_Text  =  "기상반 비상정지 버튼 동작"; break;
                    case 10001: AlarmName_Text =  "인버터 통신 이상"; break;
                    case 10101: AlarmName_Text =  "EtherCAT 연결 실패"; break;
                    case 10102: AlarmName_Text =  "EtherCAT Slave 연결 이상"; break;
                    case 10103: AlarmName_Text =  "EtherCAT Slave ID 이상"; break;
                    case 10104: AlarmName_Text =  "EtherCAT Slave 타입 이상"; break;
                    case 10110: AlarmName_Text =  "EtherCAT 통신 끊김(DIO1)"; break;
                    case 10111: AlarmName_Text =  "EtherCAT 통신 끊김(DIO2)"; break;
                    case 10112: AlarmName_Text =  "EtherCAT 통신 끊김(DIO3)"; break;
                    case 10113: AlarmName_Text =  "EtherCAT 통신 끊김(DIO4)"; break;
                    case 10114: AlarmName_Text =  "EtherCAT 통신 끊김(DIO5)"; break;
                    case 10201: AlarmName_Text =  "주행 브레이크 전원 MMS트립"; break;
                    case 10202: AlarmName_Text =  "승강 브레이크 전원 MMS트립"; break;
                    case 10203: AlarmName_Text =  "포크1 브레이크 전원 MMS트립"; break;
                    case 11001: AlarmName_Text =  "설정 데이터 이상[장치 기본 정보]"; break;
                    case 11002: AlarmName_Text =  "설정 데이터 이상[장치 구조]"; break;
                    case 11003: AlarmName_Text =  "설정 데이터 이상[MCU 입출력]"; break;
                    case 11004: AlarmName_Text =  "설정 데이터 이상[랙 설정]"; break;
                    case 11005: AlarmName_Text =  "설정 데이터 이상[셀 오프셋]"; break;
                    case 11006: AlarmName_Text =  "설정 데이터 이상[스테이션]"; break;
                    case 11007: AlarmName_Text =  "설정 데이터 이상[금지랙]"; break;
                    case 11008: AlarmName_Text =  "설정 데이터 이상[스페셜랙]"; break;
                    case 11009: AlarmName_Text =  "설정 데이터 이상[제어 설정]"; break;
                    case 11010: AlarmName_Text =  "설정 데이터 이상[주행 드라이브]"; break;
                    case 11011: AlarmName_Text =  "설정 데이터 이상[승강 드라이브]"; break;
                    case 11012: AlarmName_Text =  "설정 데이터 이상[포크 드라이브]";  break;
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

        public static object UTIL_BytesToStructure_FF(byte[] data, Type type, int sno, int struc_len)
        {
            int a;
            IntPtr buff = Marshal.AllocHGlobal(struc_len); // 배열의 크기만큼 비관리 메모리 영역에 메모리를 할당한다.

            for (int i = 0; i < struc_len; i++)
            {
                Marshal.WriteByte(buff, i, 0xFF);
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

        public static object UTIL_BytesToStructure_FF(byte[] data, Type type, int sno, int struc_len, int array_len)
        {
            int a;
            //IntPtr buff = Marshal.AllocHGlobal(struc_len); // 배열의 크기만큼 비관리 메모리 영역에 메모리를 할당한다.
            IntPtr buff = Marshal.AllocHGlobal(struc_len); // 배열의 크기만큼 비관리 메모리 영역에 메모리를 할당한다.

            for (int i = 0; i < struc_len; i++)
            {
                Marshal.WriteByte(buff, i, 0xFF);
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

        public static object UTIL_BytesToStructure_FF(byte[] bytearray, Type type)
        {
            int len = Marshal.SizeOf(type);
            int srclen = bytearray.Length;


            if (len > srclen) return UTIL_BytesToStructure_FF(bytearray, type, 0, len, srclen);
            else return UTIL_BytesToStructure_FF(bytearray, type, 0, len);
        }

        public static object UTIL_BytesToStructure(byte[] bytearray, int srcLen, Type type)
        {
            int len = Marshal.SizeOf(type);
            return UTIL_BytesToStructure(bytearray, type, 0, len, srcLen);
        }

        public static object UTIL_BytesToStructure_FF(byte[] bytearray, int srcLen, Type type)
        {
            int len = Marshal.SizeOf(type);
            return UTIL_BytesToStructure_FF(bytearray, type, 0, len, srcLen);
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
        public static string UTIL_GetSRMJobStepTextAsValue(byte value)
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
                case 0x0E :return ("Home 위치로 이동중");
                case 0x0F: return ("Home 위치 도착");
                case 0x10: return ("Sticky 목적 위치로 이동중");
                case 0x11: return ("Sticky 목적 도착");
                case 0x12: return ("Sticky 목적 위치에서 포크진입");
                case 0x13: return ("Sticky 목적 위치에서 케리지 상승");
                case 0x14: return ("Sticky 목적 위치에서 케리지 하강");
                case 0x15: return ("Sticky 목적 위치에서 포크복귀");
                case 0x16:return ("Sticky 동작 완료");
                default: return (string.Format("{0:X2}", value));
            }
        }

        public static string UTIL_GetRTVJobStepTextAsValue(byte value)
        {
            switch (value)
            {
                case 0x00: return ("지령없음");
                case 0x01: return ("지령수신");
                case 0x02: return ("From 위치로 이동중");
                case 0x03: return ("From 위치 도착");
                case 0x07: return ("To 위치로 이동중");
                case 0x08: return ("To 위치 도착");
                case 0x0C: return ("화물 적재 완료");
                case 0x0D: return ("화물 이재 완료");
                default: return (string.Format("{0:X2}", value));
            }
        }

        public static string UTIL_GetEMSJobStepTextAsValue(byte value)
        {
            switch (value)
            {
                case 0x00: return ("지령없음");
                case 0x01: return ("지령수신");
                case 0x02: return ("From 위치로 이동");
                case 0x03: return ("From 위치 도착");
                case 0x04: return ("From 위치 하강");
                case 0x05: return ("From 위치 적재");
                case 0x06: return ("From 위치 상승");
                case 0x0F: return ("화물 적재 완료");
                case 0x11: return ("To 위치로 이동");
                case 0x12: return ("To 위치 도착");
                case 0x13: return ("To 위치 하강");
                case 0x14: return ("To 위치 이재");
                case 0x15: return ("To 위치 상승");
                case 0x1F: return ("화물 이재 완료");
                default: return (string.Format("{0:X2}", value));
            }
        }

 
        public static string UTIL_GetSRMTaskStepTextAsValue(byte value)
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

        public static string UTIL_GetRTVTaskStepTextAsValue(byte value)
        {
            return (string.Format("{0:X2}", value));
        }

        public static string UTIL_GetEMSTaskStepTextAsValue(byte value)
        {
            return (string.Format("{0:X2}", value));
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
                case ConstClass.SEMI_Sticky: return ("Sticky");
                default: return (string.Format("{0:X2}", value));
            }
        }


        public static string UTIL_RTVActionStText(byte Code)
        {
            string DevActionStText = string.Format("0x{0:X2}", Code);

            switch (Code)
            {
                case  0: DevActionStText = "[대기상태] 수동운전 모드"; break;
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


        public static string UTIL_EMSActionStText(byte Code)
        {
            string DevActionStText = string.Format("0x{0:X2}", Code);

           return DevActionStText;
        }

        public static string UTIL_SRMActionStText(byte Code)
        {
            string DevActionStText = string.Format("0x{0:X2}", Code);

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

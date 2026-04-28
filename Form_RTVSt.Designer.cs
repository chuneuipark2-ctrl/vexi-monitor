
namespace VEXI
{
    partial class Form_RTVSt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)// 오버라이드를 하는이유 부모껏도 실행하고 내가 특수하게 정의한 함수도 실행하기 위해서
                                                       // bool disposing 
                                                       // true : 사용자가 명시적으로 창을 닫거나 Close()를 호출했을 때입니다. (관리되는 자원 정리)
                                                       // false: 시스템이 알아서 메모리를 정리할 때(가비지 컬렉터)입니다.
        {
            if (disposing && (components != null)) // disposing  true : 사용자가 직접 정리하라고 시킨 상황인지 확인합니다.
                                                   // components != null: 화면 위에 올려둔 버튼, 타이머, 통신 컨트롤 같은 '컴포넌트'들이 메모리에 실제로 존재하는지 확인합니다. (없는데 지우려 하면 에러가 나니까요.)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        /*
         
이 코드는 C# WinForm 개발에서 아주 중요한 '자원 정리(Dispose) 패턴'입니다. 폼(화면)이나 컨트롤이 닫힐 때, 메모리에서 깔끔하게 사라지도록 도와주는 일종의 '뒷정리 담당자'라고 보시면 됩니다.

핵심적인 부분 위주로 쪼개서 설명해 드릴게요.

1. Dispose 메서드의 역할
프로그램이 실행되면서 사용한 메모리나 하드웨어 자원(통신 포트, 이미지 등)을 "이제 다 썼으니 반납하겠다"라고 선언하는 함수입니다.

2. 코드 한 줄씩 뜯어보기
① protected override void Dispose(bool disposing)
protected: 이 클래스와 이를 상속받은 자식들만 쓸 수 있게 보호하겠다는 뜻입니다.

override: 부모(Form 클래스)가 이미 가지고 있는 Dispose 기능을 내 상황에 맞게 재정의해서 쓰겠다는 뜻입니다.

bool disposing:

true: 사용자가 명시적으로 창을 닫거나 Close()를 호출했을 때입니다. (관리되는 자원 정리)

false: 시스템이 알아서 메모리를 정리할 때(가비지 컬렉터)입니다.

② if (disposing && (components != null))
disposing: 사용자가 직접 정리하라고 시킨 상황인지 확인합니다.

components != null: 화면 위에 올려둔 버튼, 타이머, 통신 컨트롤 같은 '컴포넌트'들이 메모리에 실제로 존재하는지 확인합니다. (없는데 지우려 하면 에러가 나니까요.)

③ components.Dispose();
화면에 붙어있던 모든 부품(컴포넌트)들에게 "너희도 이제 퇴근해!"라고 명령하며 자원을 해제합니다.

④ base.Dispose(disposing);
base는 나의 부모(기본 Form 클래스)를 의미합니다. "내가 챙길 건 다 챙겼으니, 나머지는 부모님이 알아서 정리해 주세요"라고 넘기는 마지막 단계입니다.

3. 왜 이게 필요한가요? (물류 제어 관점)
물류 설비 제어 프로그램을 짤 때 특히 중요합니다.

만약 EtherCAT 통신이나 로그 기록용 파일을 열어두었는데, 이 Dispose가 제대로 안 되면 창을 닫아도 배경에서 프로그램이 통신 포트를 계속 꽉 잡고 있게 됩니다.

그러면 프로그램을 다시 켰을 때 "이미 사용 중인 포트입니다"라는 에러가 나거나, 메모리 부족으로 장비가 멈출 수 있습니다.

💡 요약하자면
이 코드는 "이 화면이 꺼질 때, 연결된 모든 장비와 부품들을 안전하게 끄고 메모리를 비워라"라는 표준 매뉴얼입니다. 보통 Visual Studio가 자동으로 생성해주는 코드이니, 구조체(struct)처럼 직접 다 짤 필요는 없지만 "뒷정리용 코드구나"라는 것만 알고 계시면 됩니다!

추가 팁: 아까 Git 에러 때문에 .vs 폴더 지우는 건 성공하셨나요? 만약 폴더 삭제 후에 프로젝트를 다시 열면 이 Dispose 코드들이 있는 Designer.cs 파일들도 다시 정상적으로 인식될 겁니다. 건투를 빕니다!
          
         */


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_RCS_InterLockOut_4_Data = new System.Windows.Forms.Label();
            this.lbl_RCS_InterLockOut_3_Data = new System.Windows.Forms.Label();
            this.lbl_RCS_InterLockOut_2_Data = new System.Windows.Forms.Label();
            this.lbl_RCS_InterLockOut_1_Data = new System.Windows.Forms.Label();
            this.lbl_RCS_InterLockOut_4_Index = new System.Windows.Forms.Label();
            this.lbl_RCS_InterLockOut_3_Index = new System.Windows.Forms.Label();
            this.lbl_RCS_InterLockOut_2_Index = new System.Windows.Forms.Label();
            this.lbl_RCS_InterLockOut_1_Index = new System.Windows.Forms.Label();
            this.lbl_RCS_InterLockIn_4_Data = new System.Windows.Forms.Label();
            this.lbl_RCS_InterLockIn_3_Data = new System.Windows.Forms.Label();
            this.lbl_RCS_InterLockIn_2_Data = new System.Windows.Forms.Label();
            this.lbl_RCS_InterLockIn_1_Data = new System.Windows.Forms.Label();
            this.lbl_RCS_InterLockIn_4_Index = new System.Windows.Forms.Label();
            this.lbl_RCS_InterLockIn_3_Index = new System.Windows.Forms.Label();
            this.lbl_RCS_InterLockIn_2_Index = new System.Windows.Forms.Label();
            this.lbl_RCS_InterLockIn_1_Index = new System.Windows.Forms.Label();
            this.lbl_RCSSt_0 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_DevMode_Auto = new System.Windows.Forms.Label();
            this.lbl_DevMode_Manual = new System.Windows.Forms.Label();
            this.lbl_DevMode_Force = new System.Windows.Forms.Label();
            this.lbl_DevMode_Setup = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbl_Dev_AlarmCodeType = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.lbl_Dev_FanFault = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.lblSystemTimeUTC = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lbl_Dev_Emergency = new System.Windows.Forms.Label();
            this.lbl_Dev_InvertorConn = new System.Windows.Forms.Label();
            this.lbl_Dev_Start = new System.Windows.Forms.Label();
            this.lbl_DevEmergencySwitch = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.lbl_DevmodeSwitch_1 = new System.Windows.Forms.Label();
            this.lbl_DevmodeSwitch_0 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lblHOGINum = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblGroupNum = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblProjectNo = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lbl_RTV_RailType = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lbl_Dev_ActionCode = new System.Windows.Forms.Label();
            this.lbl_Dev_Error = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbl_OSt_38 = new System.Windows.Forms.Label();
            this.lbl_O_Title_38 = new System.Windows.Forms.Label();
            this.lbl_IOSt_38 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_38 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.lbl_OSt_37 = new System.Windows.Forms.Label();
            this.lbl_O_Title_37 = new System.Windows.Forms.Label();
            this.lbl_OSt_36 = new System.Windows.Forms.Label();
            this.lbl_OSt_35 = new System.Windows.Forms.Label();
            this.lbl_OSt_34 = new System.Windows.Forms.Label();
            this.lbl_OSt_33 = new System.Windows.Forms.Label();
            this.lbl_O_Title_36 = new System.Windows.Forms.Label();
            this.lbl_O_Title_35 = new System.Windows.Forms.Label();
            this.lbl_O_Title_34 = new System.Windows.Forms.Label();
            this.lbl_O_Title_33 = new System.Windows.Forms.Label();
            this.lbl_O_Title_32 = new System.Windows.Forms.Label();
            this.lbl_OSt_32 = new System.Windows.Forms.Label();
            this.lbl_O_Title_28 = new System.Windows.Forms.Label();
            this.lbl_O_Title_29 = new System.Windows.Forms.Label();
            this.lbl_O_Title_30 = new System.Windows.Forms.Label();
            this.lbl_O_Title_31 = new System.Windows.Forms.Label();
            this.lbl_OSt_28 = new System.Windows.Forms.Label();
            this.lbl_OSt_29 = new System.Windows.Forms.Label();
            this.lbl_OSt_30 = new System.Windows.Forms.Label();
            this.lbl_OSt_31 = new System.Windows.Forms.Label();
            this.lbl_OSt_16 = new System.Windows.Forms.Label();
            this.lbl_OSt_15 = new System.Windows.Forms.Label();
            this.lbl_OSt_14 = new System.Windows.Forms.Label();
            this.lbl_OSt_13 = new System.Windows.Forms.Label();
            this.lbl_OSt_12 = new System.Windows.Forms.Label();
            this.lbl_OSt_11 = new System.Windows.Forms.Label();
            this.lbl_OSt_10 = new System.Windows.Forms.Label();
            this.lbl_OSt_9 = new System.Windows.Forms.Label();
            this.lbl_OSt_8 = new System.Windows.Forms.Label();
            this.lbl_OSt_7 = new System.Windows.Forms.Label();
            this.lbl_OSt_6 = new System.Windows.Forms.Label();
            this.lbl_OSt_5 = new System.Windows.Forms.Label();
            this.lbl_OSt_4 = new System.Windows.Forms.Label();
            this.lbl_OSt_3 = new System.Windows.Forms.Label();
            this.lbl_OSt_2 = new System.Windows.Forms.Label();
            this.lbl_OSt_1 = new System.Windows.Forms.Label();
            this.lbl_O_Title_16 = new System.Windows.Forms.Label();
            this.lbl_O_Title_15 = new System.Windows.Forms.Label();
            this.lbl_O_Title_14 = new System.Windows.Forms.Label();
            this.lbl_O_Title_13 = new System.Windows.Forms.Label();
            this.lbl_O_Title_17 = new System.Windows.Forms.Label();
            this.lbl_O_Title_12 = new System.Windows.Forms.Label();
            this.lbl_O_Title_18 = new System.Windows.Forms.Label();
            this.lbl_O_Title_11 = new System.Windows.Forms.Label();
            this.lbl_O_Title_19 = new System.Windows.Forms.Label();
            this.lbl_O_Title_10 = new System.Windows.Forms.Label();
            this.lbl_O_Title_20 = new System.Windows.Forms.Label();
            this.lbl_O_Title_9 = new System.Windows.Forms.Label();
            this.lbl_O_Title_21 = new System.Windows.Forms.Label();
            this.lbl_O_Title_8 = new System.Windows.Forms.Label();
            this.lbl_O_Title_22 = new System.Windows.Forms.Label();
            this.lbl_O_Title_7 = new System.Windows.Forms.Label();
            this.lbl_O_Title_23 = new System.Windows.Forms.Label();
            this.lbl_O_Title_6 = new System.Windows.Forms.Label();
            this.lbl_O_Title_24 = new System.Windows.Forms.Label();
            this.lbl_O_Title_5 = new System.Windows.Forms.Label();
            this.lbl_O_Title_25 = new System.Windows.Forms.Label();
            this.lbl_O_Title_4 = new System.Windows.Forms.Label();
            this.lbl_O_Title_26 = new System.Windows.Forms.Label();
            this.lbl_O_Title_3 = new System.Windows.Forms.Label();
            this.lbl_O_Title_27 = new System.Windows.Forms.Label();
            this.lbl_O_Title_2 = new System.Windows.Forms.Label();
            this.lbl_O_Title_1 = new System.Windows.Forms.Label();
            this.lbl_OSt_17 = new System.Windows.Forms.Label();
            this.lbl_OSt_18 = new System.Windows.Forms.Label();
            this.lbl_OSt_19 = new System.Windows.Forms.Label();
            this.lbl_OSt_20 = new System.Windows.Forms.Label();
            this.lbl_OSt_21 = new System.Windows.Forms.Label();
            this.lbl_OSt_22 = new System.Windows.Forms.Label();
            this.lbl_OSt_23 = new System.Windows.Forms.Label();
            this.lbl_OSt_24 = new System.Windows.Forms.Label();
            this.lbl_OSt_25 = new System.Windows.Forms.Label();
            this.lbl_OSt_26 = new System.Windows.Forms.Label();
            this.lbl_OSt_27 = new System.Windows.Forms.Label();
            this.lbl_IOSt_37 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_37 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lbl_IOSt_36 = new System.Windows.Forms.Label();
            this.lbl_IOSt_35 = new System.Windows.Forms.Label();
            this.lbl_IOSt_34 = new System.Windows.Forms.Label();
            this.lbl_IOSt_33 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_36 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_35 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_34 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_33 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_32 = new System.Windows.Forms.Label();
            this.lbl_IOSt_32 = new System.Windows.Forms.Label();
            this.label230 = new System.Windows.Forms.Label();
            this.label229 = new System.Windows.Forms.Label();
            this.label228 = new System.Windows.Forms.Label();
            this.label227 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_28 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_29 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_30 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_31 = new System.Windows.Forms.Label();
            this.lbl_IOSt_28 = new System.Windows.Forms.Label();
            this.lbl_IOSt_29 = new System.Windows.Forms.Label();
            this.lbl_IOSt_30 = new System.Windows.Forms.Label();
            this.lbl_IOSt_31 = new System.Windows.Forms.Label();
            this.lbl_IOSt_16 = new System.Windows.Forms.Label();
            this.lbl_IOSt_15 = new System.Windows.Forms.Label();
            this.lbl_IOSt_14 = new System.Windows.Forms.Label();
            this.lbl_IOSt_13 = new System.Windows.Forms.Label();
            this.label289 = new System.Windows.Forms.Label();
            this.lbl_IOSt_12 = new System.Windows.Forms.Label();
            this.label241 = new System.Windows.Forms.Label();
            this.lbl_IOSt_11 = new System.Windows.Forms.Label();
            this.label240 = new System.Windows.Forms.Label();
            this.lbl_IOSt_10 = new System.Windows.Forms.Label();
            this.label239 = new System.Windows.Forms.Label();
            this.lbl_IOSt_9 = new System.Windows.Forms.Label();
            this.label238 = new System.Windows.Forms.Label();
            this.lbl_IOSt_8 = new System.Windows.Forms.Label();
            this.label237 = new System.Windows.Forms.Label();
            this.lbl_IOSt_7 = new System.Windows.Forms.Label();
            this.label236 = new System.Windows.Forms.Label();
            this.lbl_IOSt_6 = new System.Windows.Forms.Label();
            this.label235 = new System.Windows.Forms.Label();
            this.lbl_IOSt_5 = new System.Windows.Forms.Label();
            this.label234 = new System.Windows.Forms.Label();
            this.lbl_IOSt_4 = new System.Windows.Forms.Label();
            this.label233 = new System.Windows.Forms.Label();
            this.lbl_IOSt_3 = new System.Windows.Forms.Label();
            this.label232 = new System.Windows.Forms.Label();
            this.lbl_IOSt_2 = new System.Windows.Forms.Label();
            this.label231 = new System.Windows.Forms.Label();
            this.lbl_IOSt_1 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_16 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_15 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_14 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_13 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_17 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_12 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_18 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_11 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_19 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_10 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_20 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_9 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_21 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_8 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_22 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_7 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_23 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_6 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_24 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_5 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_25 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_4 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_26 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_3 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_27 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_2 = new System.Windows.Forms.Label();
            this.lbl_IO_Title_1 = new System.Windows.Forms.Label();
            this.label274 = new System.Windows.Forms.Label();
            this.label275 = new System.Windows.Forms.Label();
            this.label276 = new System.Windows.Forms.Label();
            this.lbl_IOSt_17 = new System.Windows.Forms.Label();
            this.label277 = new System.Windows.Forms.Label();
            this.lbl_IOSt_18 = new System.Windows.Forms.Label();
            this.label278 = new System.Windows.Forms.Label();
            this.lbl_IOSt_19 = new System.Windows.Forms.Label();
            this.label279 = new System.Windows.Forms.Label();
            this.lbl_IOSt_20 = new System.Windows.Forms.Label();
            this.label280 = new System.Windows.Forms.Label();
            this.lbl_IOSt_21 = new System.Windows.Forms.Label();
            this.label281 = new System.Windows.Forms.Label();
            this.lbl_IOSt_22 = new System.Windows.Forms.Label();
            this.label282 = new System.Windows.Forms.Label();
            this.lbl_IOSt_23 = new System.Windows.Forms.Label();
            this.label283 = new System.Windows.Forms.Label();
            this.lbl_IOSt_24 = new System.Windows.Forms.Label();
            this.label284 = new System.Windows.Forms.Label();
            this.lbl_IOSt_25 = new System.Windows.Forms.Label();
            this.label285 = new System.Windows.Forms.Label();
            this.lbl_IOSt_26 = new System.Windows.Forms.Label();
            this.label286 = new System.Windows.Forms.Label();
            this.lbl_IOSt_27 = new System.Windows.Forms.Label();
            this.label287 = new System.Windows.Forms.Label();
            this.label288 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.gb_Outputgroup = new System.Windows.Forms.GroupBox();
            this.rb_DIO_DigitalOut_3 = new System.Windows.Forms.RadioButton();
            this.rb_DIO_DigitalOut_2 = new System.Windows.Forms.RadioButton();
            this.rb_DIO_DigitalOut_1 = new System.Windows.Forms.RadioButton();
            this.gb_Inputgroup = new System.Windows.Forms.GroupBox();
            this.rb_DIO_DigitalIn_3 = new System.Windows.Forms.RadioButton();
            this.rb_DIO_DigitalIn_2 = new System.Windows.Forms.RadioButton();
            this.rb_DIO_DigitalIn_1 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbl_DriveRearAreaInfo_RegionSt_0 = new System.Windows.Forms.Label();
            this.lbl_DriveRearAreaInfo_RegionSt_1 = new System.Windows.Forms.Label();
            this.lbl_DriveRearAreaInfo_RegionSt_2 = new System.Windows.Forms.Label();
            this.lbl_DriveSt2_2 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.btnBarCodeErrCountInit = new System.Windows.Forms.Button();
            this.lbl_Drive_CurrentStation_Feed2 = new System.Windows.Forms.Label();
            this.lbl_Drive_CurrentStation_Feed1 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.lbl_DriveBarcodeErrCount = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.lbl_DriveFrontAreaInfo_RegionSt_0 = new System.Windows.Forms.Label();
            this.lbl_DriveFrontAreaInfo_RegionSt_1 = new System.Windows.Forms.Label();
            this.lbl_DriveFrontAreaInfo_RegionSt_2 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.lbl_Feed2_DestSpeed = new System.Windows.Forms.Label();
            this.lbl_Feed1_DestSpeed = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_Drive_DestSpeed = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_Feed2_Speed = new System.Windows.Forms.Label();
            this.lbl_Feed1_Speed = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.lbl_Drive_CurrentPos_Feed2 = new System.Windows.Forms.Label();
            this.lbl_Drive_CurrentPos_Feed1 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.lbl_Feed2St2_1 = new System.Windows.Forms.Label();
            this.lbl_Feed1St2_1 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lbl_Feed2St1_5 = new System.Windows.Forms.Label();
            this.lbl_Feed1St1_5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_Feed2St1_6 = new System.Windows.Forms.Label();
            this.lbl_Feed1St1_6 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.lbl_Feed2St1_4 = new System.Windows.Forms.Label();
            this.lbl_Feed1St1_4 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.lbl_Feed2St2_0 = new System.Windows.Forms.Label();
            this.lbl_Feed2St1_0 = new System.Windows.Forms.Label();
            this.lbl_Feed2St1_1 = new System.Windows.Forms.Label();
            this.lbl_Feed2St1_2 = new System.Windows.Forms.Label();
            this.lbl_Feed2St1_3 = new System.Windows.Forms.Label();
            this.lbl_Feed2_Dest = new System.Windows.Forms.Label();
            this.lbl_Feed2_Pos = new System.Windows.Forms.Label();
            this.lbl_Feed1St2_0 = new System.Windows.Forms.Label();
            this.lbl_Feed1St1_0 = new System.Windows.Forms.Label();
            this.lbl_Feed1St1_1 = new System.Windows.Forms.Label();
            this.lbl_Feed1St1_2 = new System.Windows.Forms.Label();
            this.lbl_Feed1St1_3 = new System.Windows.Forms.Label();
            this.lbl_Feed1_Dest = new System.Windows.Forms.Label();
            this.lbl_Feed1_Pos = new System.Windows.Forms.Label();
            this.label170 = new System.Windows.Forms.Label();
            this.label171 = new System.Windows.Forms.Label();
            this.label172 = new System.Windows.Forms.Label();
            this.label173 = new System.Windows.Forms.Label();
            this.label174 = new System.Windows.Forms.Label();
            this.label177 = new System.Windows.Forms.Label();
            this.label178 = new System.Windows.Forms.Label();
            this.lbl_DriveSt2_1 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.lbl_Dev_Maintance = new System.Windows.Forms.Label();
            this.lbl_Dev_Home = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.lbl_Drive_Destination = new System.Windows.Forms.Label();
            this.lbl_Drive_Speed = new System.Windows.Forms.Label();
            this.lbl_Drive_Position = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.label132 = new System.Windows.Forms.Label();
            this.lbl_DriveSt2_0 = new System.Windows.Forms.Label();
            this.lbl_DriveSt1_0 = new System.Windows.Forms.Label();
            this.lbl_DriveSt1_1 = new System.Windows.Forms.Label();
            this.lbl_DriveSt1_2 = new System.Windows.Forms.Label();
            this.lbl_DriveSt1_3 = new System.Windows.Forms.Label();
            this.lbl_DriveSt1_4 = new System.Windows.Forms.Label();
            this.lbl_CanWork_StationIndex = new System.Windows.Forms.Label();
            this.lbl_DriveSt1_6 = new System.Windows.Forms.Label();
            this.lbl_DriveSt1_5 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label117 = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label113 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbl_Key_1_0 = new System.Windows.Forms.Label();
            this.lbl_Key_1_1 = new System.Windows.Forms.Label();
            this.lbl_Key_1_2 = new System.Windows.Forms.Label();
            this.lbl_Key_1_3 = new System.Windows.Forms.Label();
            this.lbl_Key_1_4 = new System.Windows.Forms.Label();
            this.lbl_Key_2_0 = new System.Windows.Forms.Label();
            this.lbl_Key_2_1 = new System.Windows.Forms.Label();
            this.lbl_Key_2_2 = new System.Windows.Forms.Label();
            this.lbl_Key_2_3 = new System.Windows.Forms.Label();
            this.lbl_Key_2_4 = new System.Windows.Forms.Label();
            this.lbl_Key_2_5 = new System.Windows.Forms.Label();
            this.lbl_Key_1_5 = new System.Windows.Forms.Label();
            this.lbl_Key_1_6 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label77 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.lbl_WCS14_CTRL2_Dest = new System.Windows.Forms.Label();
            this.lbl_WCS14_CTRL1_0 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.lbl_WCS14_CTRL1_2 = new System.Windows.Forms.Label();
            this.lbl_WCS14_CTRL1_3 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.lbl_WCS14_CTRL1_4 = new System.Windows.Forms.Label();
            this.lbl_WCS14_CTRL1_5 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.lbl_WCS14_CTRL1_6 = new System.Windows.Forms.Label();
            this.lbl_WCS14_CTRL1_7 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.lbl_WCS14_ST2_Position = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.lbl_WCS14_ST2_7 = new System.Windows.Forms.Label();
            this.lbl_WCS14_ST1_Speed = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.lbl_WCS14_ST1_2 = new System.Windows.Forms.Label();
            this.lbl_WCS14_ST1_3 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.lbl_WCS14_ST1_4 = new System.Windows.Forms.Label();
            this.lbl_WCS14_ST1_5 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.lbl_WCS14_ST1_6 = new System.Windows.Forms.Label();
            this.lbl_WCS14_ST1_7 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lbl_WCS14_ControlRx_Count = new System.Windows.Forms.Label();
            this.lbl_WCS14_StatusRx_Count = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.gb_Outputgroup.SuspendLayout();
            this.gb_Inputgroup.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Highlight;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(11, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "안전플러그 동작";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_RCS_InterLockOut_4_Data);
            this.groupBox1.Controls.Add(this.lbl_RCS_InterLockOut_3_Data);
            this.groupBox1.Controls.Add(this.lbl_RCS_InterLockOut_2_Data);
            this.groupBox1.Controls.Add(this.lbl_RCS_InterLockOut_1_Data);
            this.groupBox1.Controls.Add(this.lbl_RCS_InterLockOut_4_Index);
            this.groupBox1.Controls.Add(this.lbl_RCS_InterLockOut_3_Index);
            this.groupBox1.Controls.Add(this.lbl_RCS_InterLockOut_2_Index);
            this.groupBox1.Controls.Add(this.lbl_RCS_InterLockOut_1_Index);
            this.groupBox1.Controls.Add(this.lbl_RCS_InterLockIn_4_Data);
            this.groupBox1.Controls.Add(this.lbl_RCS_InterLockIn_3_Data);
            this.groupBox1.Controls.Add(this.lbl_RCS_InterLockIn_2_Data);
            this.groupBox1.Controls.Add(this.lbl_RCS_InterLockIn_1_Data);
            this.groupBox1.Controls.Add(this.lbl_RCS_InterLockIn_4_Index);
            this.groupBox1.Controls.Add(this.lbl_RCS_InterLockIn_3_Index);
            this.groupBox1.Controls.Add(this.lbl_RCS_InterLockIn_2_Index);
            this.groupBox1.Controls.Add(this.lbl_RCS_InterLockIn_1_Index);
            this.groupBox1.Controls.Add(this.lbl_RCSSt_0);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(25, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 131);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "지상반 정보";
            // 
            // lbl_RCS_InterLockOut_4_Data
            // 
            this.lbl_RCS_InterLockOut_4_Data.BackColor = System.Drawing.Color.White;
            this.lbl_RCS_InterLockOut_4_Data.Location = new System.Drawing.Point(294, 96);
            this.lbl_RCS_InterLockOut_4_Data.Name = "lbl_RCS_InterLockOut_4_Data";
            this.lbl_RCS_InterLockOut_4_Data.Size = new System.Drawing.Size(54, 16);
            this.lbl_RCS_InterLockOut_4_Data.TabIndex = 28;
            this.lbl_RCS_InterLockOut_4_Data.Text = "0x00";
            this.lbl_RCS_InterLockOut_4_Data.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RCS_InterLockOut_3_Data
            // 
            this.lbl_RCS_InterLockOut_3_Data.BackColor = System.Drawing.Color.White;
            this.lbl_RCS_InterLockOut_3_Data.Location = new System.Drawing.Point(237, 96);
            this.lbl_RCS_InterLockOut_3_Data.Name = "lbl_RCS_InterLockOut_3_Data";
            this.lbl_RCS_InterLockOut_3_Data.Size = new System.Drawing.Size(54, 16);
            this.lbl_RCS_InterLockOut_3_Data.TabIndex = 27;
            this.lbl_RCS_InterLockOut_3_Data.Text = "0x00";
            this.lbl_RCS_InterLockOut_3_Data.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RCS_InterLockOut_2_Data
            // 
            this.lbl_RCS_InterLockOut_2_Data.BackColor = System.Drawing.Color.White;
            this.lbl_RCS_InterLockOut_2_Data.Location = new System.Drawing.Point(180, 96);
            this.lbl_RCS_InterLockOut_2_Data.Name = "lbl_RCS_InterLockOut_2_Data";
            this.lbl_RCS_InterLockOut_2_Data.Size = new System.Drawing.Size(54, 16);
            this.lbl_RCS_InterLockOut_2_Data.TabIndex = 26;
            this.lbl_RCS_InterLockOut_2_Data.Text = "0x00";
            this.lbl_RCS_InterLockOut_2_Data.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RCS_InterLockOut_1_Data
            // 
            this.lbl_RCS_InterLockOut_1_Data.BackColor = System.Drawing.Color.White;
            this.lbl_RCS_InterLockOut_1_Data.Location = new System.Drawing.Point(123, 96);
            this.lbl_RCS_InterLockOut_1_Data.Name = "lbl_RCS_InterLockOut_1_Data";
            this.lbl_RCS_InterLockOut_1_Data.Size = new System.Drawing.Size(54, 16);
            this.lbl_RCS_InterLockOut_1_Data.TabIndex = 25;
            this.lbl_RCS_InterLockOut_1_Data.Text = "0x00";
            this.lbl_RCS_InterLockOut_1_Data.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RCS_InterLockOut_4_Index
            // 
            this.lbl_RCS_InterLockOut_4_Index.BackColor = System.Drawing.Color.White;
            this.lbl_RCS_InterLockOut_4_Index.Location = new System.Drawing.Point(294, 78);
            this.lbl_RCS_InterLockOut_4_Index.Name = "lbl_RCS_InterLockOut_4_Index";
            this.lbl_RCS_InterLockOut_4_Index.Size = new System.Drawing.Size(54, 16);
            this.lbl_RCS_InterLockOut_4_Index.TabIndex = 24;
            this.lbl_RCS_InterLockOut_4_Index.Text = "0";
            this.lbl_RCS_InterLockOut_4_Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RCS_InterLockOut_3_Index
            // 
            this.lbl_RCS_InterLockOut_3_Index.BackColor = System.Drawing.Color.White;
            this.lbl_RCS_InterLockOut_3_Index.Location = new System.Drawing.Point(237, 78);
            this.lbl_RCS_InterLockOut_3_Index.Name = "lbl_RCS_InterLockOut_3_Index";
            this.lbl_RCS_InterLockOut_3_Index.Size = new System.Drawing.Size(54, 16);
            this.lbl_RCS_InterLockOut_3_Index.TabIndex = 23;
            this.lbl_RCS_InterLockOut_3_Index.Text = "0";
            this.lbl_RCS_InterLockOut_3_Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RCS_InterLockOut_2_Index
            // 
            this.lbl_RCS_InterLockOut_2_Index.BackColor = System.Drawing.Color.White;
            this.lbl_RCS_InterLockOut_2_Index.Location = new System.Drawing.Point(180, 78);
            this.lbl_RCS_InterLockOut_2_Index.Name = "lbl_RCS_InterLockOut_2_Index";
            this.lbl_RCS_InterLockOut_2_Index.Size = new System.Drawing.Size(54, 16);
            this.lbl_RCS_InterLockOut_2_Index.TabIndex = 22;
            this.lbl_RCS_InterLockOut_2_Index.Text = "0";
            this.lbl_RCS_InterLockOut_2_Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RCS_InterLockOut_1_Index
            // 
            this.lbl_RCS_InterLockOut_1_Index.BackColor = System.Drawing.Color.White;
            this.lbl_RCS_InterLockOut_1_Index.Location = new System.Drawing.Point(123, 78);
            this.lbl_RCS_InterLockOut_1_Index.Name = "lbl_RCS_InterLockOut_1_Index";
            this.lbl_RCS_InterLockOut_1_Index.Size = new System.Drawing.Size(54, 16);
            this.lbl_RCS_InterLockOut_1_Index.TabIndex = 21;
            this.lbl_RCS_InterLockOut_1_Index.Text = "0";
            this.lbl_RCS_InterLockOut_1_Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RCS_InterLockIn_4_Data
            // 
            this.lbl_RCS_InterLockIn_4_Data.BackColor = System.Drawing.Color.White;
            this.lbl_RCS_InterLockIn_4_Data.Location = new System.Drawing.Point(294, 60);
            this.lbl_RCS_InterLockIn_4_Data.Name = "lbl_RCS_InterLockIn_4_Data";
            this.lbl_RCS_InterLockIn_4_Data.Size = new System.Drawing.Size(54, 16);
            this.lbl_RCS_InterLockIn_4_Data.TabIndex = 20;
            this.lbl_RCS_InterLockIn_4_Data.Text = "0x00";
            this.lbl_RCS_InterLockIn_4_Data.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RCS_InterLockIn_3_Data
            // 
            this.lbl_RCS_InterLockIn_3_Data.BackColor = System.Drawing.Color.White;
            this.lbl_RCS_InterLockIn_3_Data.Location = new System.Drawing.Point(237, 60);
            this.lbl_RCS_InterLockIn_3_Data.Name = "lbl_RCS_InterLockIn_3_Data";
            this.lbl_RCS_InterLockIn_3_Data.Size = new System.Drawing.Size(54, 16);
            this.lbl_RCS_InterLockIn_3_Data.TabIndex = 19;
            this.lbl_RCS_InterLockIn_3_Data.Text = "0x00";
            this.lbl_RCS_InterLockIn_3_Data.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RCS_InterLockIn_2_Data
            // 
            this.lbl_RCS_InterLockIn_2_Data.BackColor = System.Drawing.Color.White;
            this.lbl_RCS_InterLockIn_2_Data.Location = new System.Drawing.Point(180, 60);
            this.lbl_RCS_InterLockIn_2_Data.Name = "lbl_RCS_InterLockIn_2_Data";
            this.lbl_RCS_InterLockIn_2_Data.Size = new System.Drawing.Size(54, 16);
            this.lbl_RCS_InterLockIn_2_Data.TabIndex = 18;
            this.lbl_RCS_InterLockIn_2_Data.Text = "0x00";
            this.lbl_RCS_InterLockIn_2_Data.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RCS_InterLockIn_1_Data
            // 
            this.lbl_RCS_InterLockIn_1_Data.BackColor = System.Drawing.Color.White;
            this.lbl_RCS_InterLockIn_1_Data.Location = new System.Drawing.Point(123, 60);
            this.lbl_RCS_InterLockIn_1_Data.Name = "lbl_RCS_InterLockIn_1_Data";
            this.lbl_RCS_InterLockIn_1_Data.Size = new System.Drawing.Size(54, 16);
            this.lbl_RCS_InterLockIn_1_Data.TabIndex = 17;
            this.lbl_RCS_InterLockIn_1_Data.Text = "0x00";
            this.lbl_RCS_InterLockIn_1_Data.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RCS_InterLockIn_4_Index
            // 
            this.lbl_RCS_InterLockIn_4_Index.BackColor = System.Drawing.Color.White;
            this.lbl_RCS_InterLockIn_4_Index.Location = new System.Drawing.Point(294, 42);
            this.lbl_RCS_InterLockIn_4_Index.Name = "lbl_RCS_InterLockIn_4_Index";
            this.lbl_RCS_InterLockIn_4_Index.Size = new System.Drawing.Size(54, 16);
            this.lbl_RCS_InterLockIn_4_Index.TabIndex = 16;
            this.lbl_RCS_InterLockIn_4_Index.Text = "0";
            this.lbl_RCS_InterLockIn_4_Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RCS_InterLockIn_3_Index
            // 
            this.lbl_RCS_InterLockIn_3_Index.BackColor = System.Drawing.Color.White;
            this.lbl_RCS_InterLockIn_3_Index.Location = new System.Drawing.Point(237, 42);
            this.lbl_RCS_InterLockIn_3_Index.Name = "lbl_RCS_InterLockIn_3_Index";
            this.lbl_RCS_InterLockIn_3_Index.Size = new System.Drawing.Size(54, 16);
            this.lbl_RCS_InterLockIn_3_Index.TabIndex = 15;
            this.lbl_RCS_InterLockIn_3_Index.Text = "0";
            this.lbl_RCS_InterLockIn_3_Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RCS_InterLockIn_2_Index
            // 
            this.lbl_RCS_InterLockIn_2_Index.BackColor = System.Drawing.Color.White;
            this.lbl_RCS_InterLockIn_2_Index.Location = new System.Drawing.Point(180, 42);
            this.lbl_RCS_InterLockIn_2_Index.Name = "lbl_RCS_InterLockIn_2_Index";
            this.lbl_RCS_InterLockIn_2_Index.Size = new System.Drawing.Size(54, 16);
            this.lbl_RCS_InterLockIn_2_Index.TabIndex = 14;
            this.lbl_RCS_InterLockIn_2_Index.Text = "0";
            this.lbl_RCS_InterLockIn_2_Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RCS_InterLockIn_1_Index
            // 
            this.lbl_RCS_InterLockIn_1_Index.BackColor = System.Drawing.Color.White;
            this.lbl_RCS_InterLockIn_1_Index.Location = new System.Drawing.Point(123, 42);
            this.lbl_RCS_InterLockIn_1_Index.Name = "lbl_RCS_InterLockIn_1_Index";
            this.lbl_RCS_InterLockIn_1_Index.Size = new System.Drawing.Size(54, 16);
            this.lbl_RCS_InterLockIn_1_Index.TabIndex = 12;
            this.lbl_RCS_InterLockIn_1_Index.Text = "0";
            this.lbl_RCS_InterLockIn_1_Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RCSSt_0
            // 
            this.lbl_RCSSt_0.BackColor = System.Drawing.Color.Silver;
            this.lbl_RCSSt_0.Location = new System.Drawing.Point(123, 24);
            this.lbl_RCSSt_0.Name = "lbl_RCSSt_0";
            this.lbl_RCSSt_0.Size = new System.Drawing.Size(68, 16);
            this.lbl_RCSSt_0.TabIndex = 9;
            this.lbl_RCSSt_0.Text = "OFF";
            this.lbl_RCSSt_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.Highlight;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(11, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 34);
            this.label7.TabIndex = 7;
            this.label7.Text = "인터록 (장치->CV)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Highlight;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(11, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 34);
            this.label6.TabIndex = 6;
            this.label6.Text = "인터록 (CV->장치)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DevMode_Auto
            // 
            this.lbl_DevMode_Auto.BackColor = System.Drawing.Color.Lime;
            this.lbl_DevMode_Auto.Location = new System.Drawing.Point(11, 82);
            this.lbl_DevMode_Auto.Name = "lbl_DevMode_Auto";
            this.lbl_DevMode_Auto.Size = new System.Drawing.Size(89, 18);
            this.lbl_DevMode_Auto.TabIndex = 7;
            this.lbl_DevMode_Auto.Text = "자동모드";
            this.lbl_DevMode_Auto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DevMode_Manual
            // 
            this.lbl_DevMode_Manual.BackColor = System.Drawing.Color.Silver;
            this.lbl_DevMode_Manual.Location = new System.Drawing.Point(11, 103);
            this.lbl_DevMode_Manual.Name = "lbl_DevMode_Manual";
            this.lbl_DevMode_Manual.Size = new System.Drawing.Size(89, 18);
            this.lbl_DevMode_Manual.TabIndex = 8;
            this.lbl_DevMode_Manual.Text = "수동모드";
            this.lbl_DevMode_Manual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DevMode_Force
            // 
            this.lbl_DevMode_Force.BackColor = System.Drawing.Color.Silver;
            this.lbl_DevMode_Force.Location = new System.Drawing.Point(103, 82);
            this.lbl_DevMode_Force.Name = "lbl_DevMode_Force";
            this.lbl_DevMode_Force.Size = new System.Drawing.Size(89, 18);
            this.lbl_DevMode_Force.TabIndex = 9;
            this.lbl_DevMode_Force.Text = "강제모드";
            this.lbl_DevMode_Force.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DevMode_Setup
            // 
            this.lbl_DevMode_Setup.BackColor = System.Drawing.Color.Silver;
            this.lbl_DevMode_Setup.Location = new System.Drawing.Point(103, 103);
            this.lbl_DevMode_Setup.Name = "lbl_DevMode_Setup";
            this.lbl_DevMode_Setup.Size = new System.Drawing.Size(89, 18);
            this.lbl_DevMode_Setup.TabIndex = 10;
            this.lbl_DevMode_Setup.Text = "셋업모드";
            this.lbl_DevMode_Setup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.SystemColors.Highlight;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(8, 57);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(464, 16);
            this.label12.TabIndex = 15;
            this.label12.Text = "장비 이상 / 경고";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.SystemColors.Highlight;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(11, 77);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(113, 18);
            this.label13.TabIndex = 14;
            this.label13.Text = "인버터 접속";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.SystemColors.Highlight;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(253, 39);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(113, 18);
            this.label14.TabIndex = 13;
            this.label14.Text = "비상정지";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.SystemColors.Highlight;
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(11, 57);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(113, 18);
            this.label16.TabIndex = 11;
            this.label16.Text = "시작 상태";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.SystemColors.Highlight;
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(8, 23);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(464, 16);
            this.label19.TabIndex = 18;
            this.label19.Text = "장비 동작 코드";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbl_Dev_AlarmCodeType);
            this.groupBox3.Controls.Add(this.label99);
            this.groupBox3.Controls.Add(this.lbl_Dev_FanFault);
            this.groupBox3.Controls.Add(this.label95);
            this.groupBox3.Controls.Add(this.label32);
            this.groupBox3.Controls.Add(this.lblSystemTimeUTC);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.lblVersion);
            this.groupBox3.Controls.Add(this.lbl_Dev_Emergency);
            this.groupBox3.Controls.Add(this.lbl_Dev_InvertorConn);
            this.groupBox3.Controls.Add(this.lbl_Dev_Start);
            this.groupBox3.Controls.Add(this.lbl_DevEmergencySwitch);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Location = new System.Drawing.Point(9, 11);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(493, 137);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "장비 모드 및 상태";
            // 
            // lbl_Dev_AlarmCodeType
            // 
            this.lbl_Dev_AlarmCodeType.BackColor = System.Drawing.Color.White;
            this.lbl_Dev_AlarmCodeType.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbl_Dev_AlarmCodeType.Location = new System.Drawing.Point(126, 97);
            this.lbl_Dev_AlarmCodeType.Name = "lbl_Dev_AlarmCodeType";
            this.lbl_Dev_AlarmCodeType.Size = new System.Drawing.Size(120, 18);
            this.lbl_Dev_AlarmCodeType.TabIndex = 40;
            this.lbl_Dev_AlarmCodeType.Text = "-";
            this.lbl_Dev_AlarmCodeType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label99
            // 
            this.label99.BackColor = System.Drawing.SystemColors.Highlight;
            this.label99.ForeColor = System.Drawing.Color.White;
            this.label99.Location = new System.Drawing.Point(11, 97);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(113, 18);
            this.label99.TabIndex = 39;
            this.label99.Text = "알람코드 타입";
            this.label99.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Dev_FanFault
            // 
            this.lbl_Dev_FanFault.BackColor = System.Drawing.Color.Lime;
            this.lbl_Dev_FanFault.Location = new System.Drawing.Point(368, 59);
            this.lbl_Dev_FanFault.Name = "lbl_Dev_FanFault";
            this.lbl_Dev_FanFault.Size = new System.Drawing.Size(120, 18);
            this.lbl_Dev_FanFault.TabIndex = 38;
            this.lbl_Dev_FanFault.Text = "정상";
            this.lbl_Dev_FanFault.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label95
            // 
            this.label95.BackColor = System.Drawing.SystemColors.Highlight;
            this.label95.ForeColor = System.Drawing.Color.White;
            this.label95.Location = new System.Drawing.Point(253, 59);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(113, 18);
            this.label95.TabIndex = 37;
            this.label95.Text = "FAN";
            this.label95.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.SystemColors.Highlight;
            this.label32.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label32.ForeColor = System.Drawing.Color.White;
            this.label32.Location = new System.Drawing.Point(11, 39);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(78, 16);
            this.label32.TabIndex = 36;
            this.label32.Text = "장비시간";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSystemTimeUTC
            // 
            this.lblSystemTimeUTC.BackColor = System.Drawing.Color.White;
            this.lblSystemTimeUTC.Location = new System.Drawing.Point(92, 38);
            this.lblSystemTimeUTC.Name = "lblSystemTimeUTC";
            this.lblSystemTimeUTC.Size = new System.Drawing.Size(155, 16);
            this.lblSystemTimeUTC.TabIndex = 35;
            this.lblSystemTimeUTC.Text = "-";
            this.lblSystemTimeUTC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.Highlight;
            this.label8.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(11, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 16);
            this.label8.TabIndex = 34;
            this.label8.Text = "Version";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(92, 20);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(155, 16);
            this.lblVersion.TabIndex = 32;
            this.lblVersion.Text = "-";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Dev_Emergency
            // 
            this.lbl_Dev_Emergency.BackColor = System.Drawing.Color.Lime;
            this.lbl_Dev_Emergency.Location = new System.Drawing.Point(368, 39);
            this.lbl_Dev_Emergency.Name = "lbl_Dev_Emergency";
            this.lbl_Dev_Emergency.Size = new System.Drawing.Size(120, 18);
            this.lbl_Dev_Emergency.TabIndex = 31;
            this.lbl_Dev_Emergency.Text = "정상";
            this.lbl_Dev_Emergency.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Dev_InvertorConn
            // 
            this.lbl_Dev_InvertorConn.BackColor = System.Drawing.Color.Silver;
            this.lbl_Dev_InvertorConn.Location = new System.Drawing.Point(126, 77);
            this.lbl_Dev_InvertorConn.Name = "lbl_Dev_InvertorConn";
            this.lbl_Dev_InvertorConn.Size = new System.Drawing.Size(120, 18);
            this.lbl_Dev_InvertorConn.TabIndex = 29;
            this.lbl_Dev_InvertorConn.Text = "미접속";
            this.lbl_Dev_InvertorConn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Dev_Start
            // 
            this.lbl_Dev_Start.BackColor = System.Drawing.Color.Silver;
            this.lbl_Dev_Start.Location = new System.Drawing.Point(126, 57);
            this.lbl_Dev_Start.Name = "lbl_Dev_Start";
            this.lbl_Dev_Start.Size = new System.Drawing.Size(120, 18);
            this.lbl_Dev_Start.TabIndex = 28;
            this.lbl_Dev_Start.Text = "OFF";
            this.lbl_Dev_Start.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DevEmergencySwitch
            // 
            this.lbl_DevEmergencySwitch.BackColor = System.Drawing.Color.Silver;
            this.lbl_DevEmergencySwitch.Location = new System.Drawing.Point(368, 19);
            this.lbl_DevEmergencySwitch.Name = "lbl_DevEmergencySwitch";
            this.lbl_DevEmergencySwitch.Size = new System.Drawing.Size(120, 18);
            this.lbl_DevEmergencySwitch.TabIndex = 26;
            this.lbl_DevEmergencySwitch.Text = "OFF";
            this.lbl_DevEmergencySwitch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.SystemColors.Highlight;
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(253, 19);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(113, 18);
            this.label18.TabIndex = 22;
            this.label18.Text = "비상정지 스위치";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.SystemColors.Highlight;
            this.label55.ForeColor = System.Drawing.Color.White;
            this.label55.Location = new System.Drawing.Point(11, 23);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(181, 18);
            this.label55.TabIndex = 39;
            this.label55.Text = "장비 모드 스위치";
            this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DevmodeSwitch_1
            // 
            this.lbl_DevmodeSwitch_1.BackColor = System.Drawing.Color.Silver;
            this.lbl_DevmodeSwitch_1.Location = new System.Drawing.Point(103, 42);
            this.lbl_DevmodeSwitch_1.Name = "lbl_DevmodeSwitch_1";
            this.lbl_DevmodeSwitch_1.Size = new System.Drawing.Size(89, 18);
            this.lbl_DevmodeSwitch_1.TabIndex = 38;
            this.lbl_DevmodeSwitch_1.Text = "수동";
            this.lbl_DevmodeSwitch_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DevmodeSwitch_0
            // 
            this.lbl_DevmodeSwitch_0.BackColor = System.Drawing.Color.Lime;
            this.lbl_DevmodeSwitch_0.Location = new System.Drawing.Point(11, 42);
            this.lbl_DevmodeSwitch_0.Name = "lbl_DevmodeSwitch_0";
            this.lbl_DevmodeSwitch_0.Size = new System.Drawing.Size(89, 18);
            this.lbl_DevmodeSwitch_0.TabIndex = 37;
            this.lbl_DevmodeSwitch_0.Text = "자동";
            this.lbl_DevmodeSwitch_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label105
            // 
            this.label105.BackColor = System.Drawing.SystemColors.Highlight;
            this.label105.ForeColor = System.Drawing.Color.White;
            this.label105.Location = new System.Drawing.Point(11, 63);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(181, 18);
            this.label105.TabIndex = 24;
            this.label105.Text = "장비 S/W 모드";
            this.label105.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox9);
            this.panel1.Controls.Add(this.groupBox8);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1576, 156);
            this.panel1.TabIndex = 53;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label55);
            this.groupBox9.Controls.Add(this.lbl_DevmodeSwitch_1);
            this.groupBox9.Controls.Add(this.lbl_DevMode_Auto);
            this.groupBox9.Controls.Add(this.lbl_DevmodeSwitch_0);
            this.groupBox9.Controls.Add(this.lbl_DevMode_Manual);
            this.groupBox9.Controls.Add(this.lbl_DevMode_Force);
            this.groupBox9.Controls.Add(this.lbl_DevMode_Setup);
            this.groupBox9.Controls.Add(this.label105);
            this.groupBox9.Location = new System.Drawing.Point(508, 11);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(202, 137);
            this.groupBox9.TabIndex = 55;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "장비 모드";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lblHOGINum);
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Controls.Add(this.lblGroupNum);
            this.groupBox8.Controls.Add(this.label24);
            this.groupBox8.Controls.Add(this.lblProjectNo);
            this.groupBox8.Controls.Add(this.label42);
            this.groupBox8.Controls.Add(this.label27);
            this.groupBox8.Controls.Add(this.lbl_RTV_RailType);
            this.groupBox8.Location = new System.Drawing.Point(1202, 11);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(283, 137);
            this.groupBox8.TabIndex = 54;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "장비 정보";
            // 
            // lblHOGINum
            // 
            this.lblHOGINum.BackColor = System.Drawing.Color.White;
            this.lblHOGINum.Location = new System.Drawing.Point(146, 81);
            this.lblHOGINum.Name = "lblHOGINum";
            this.lblHOGINum.Size = new System.Drawing.Size(126, 16);
            this.lblHOGINum.TabIndex = 41;
            this.lblHOGINum.Text = "-";
            this.lblHOGINum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.Highlight;
            this.label9.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(11, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 16);
            this.label9.TabIndex = 40;
            this.label9.Text = "호기번호";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGroupNum
            // 
            this.lblGroupNum.BackColor = System.Drawing.Color.White;
            this.lblGroupNum.Location = new System.Drawing.Point(146, 62);
            this.lblGroupNum.Name = "lblGroupNum";
            this.lblGroupNum.Size = new System.Drawing.Size(126, 16);
            this.lblGroupNum.TabIndex = 39;
            this.lblGroupNum.Text = "-";
            this.lblGroupNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.SystemColors.Highlight;
            this.label24.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label24.ForeColor = System.Drawing.Color.White;
            this.label24.Location = new System.Drawing.Point(11, 62);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(131, 16);
            this.label24.TabIndex = 38;
            this.label24.Text = "그룹번호";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProjectNo
            // 
            this.lblProjectNo.BackColor = System.Drawing.Color.White;
            this.lblProjectNo.Location = new System.Drawing.Point(146, 43);
            this.lblProjectNo.Name = "lblProjectNo";
            this.lblProjectNo.Size = new System.Drawing.Size(126, 16);
            this.lblProjectNo.TabIndex = 37;
            this.lblProjectNo.Text = "-";
            this.lblProjectNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.SystemColors.Highlight;
            this.label42.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label42.ForeColor = System.Drawing.Color.White;
            this.label42.Location = new System.Drawing.Point(11, 43);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(131, 16);
            this.label42.TabIndex = 36;
            this.label42.Text = "Project No";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.SystemColors.Highlight;
            this.label27.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label27.ForeColor = System.Drawing.Color.White;
            this.label27.Location = new System.Drawing.Point(11, 24);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(131, 16);
            this.label27.TabIndex = 34;
            this.label27.Text = "레일타입";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RTV_RailType
            // 
            this.lbl_RTV_RailType.BackColor = System.Drawing.Color.White;
            this.lbl_RTV_RailType.Location = new System.Drawing.Point(146, 23);
            this.lbl_RTV_RailType.Name = "lbl_RTV_RailType";
            this.lbl_RTV_RailType.Size = new System.Drawing.Size(126, 16);
            this.lbl_RTV_RailType.TabIndex = 32;
            this.lbl_RTV_RailType.Text = "-";
            this.lbl_RTV_RailType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lbl_Dev_ActionCode);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.lbl_Dev_Error);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Location = new System.Drawing.Point(716, 11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(481, 137);
            this.groupBox4.TabIndex = 53;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "장비 동작 및 이상";
            // 
            // lbl_Dev_ActionCode
            // 
            this.lbl_Dev_ActionCode.BackColor = System.Drawing.Color.White;
            this.lbl_Dev_ActionCode.Location = new System.Drawing.Point(8, 40);
            this.lbl_Dev_ActionCode.Name = "lbl_Dev_ActionCode";
            this.lbl_Dev_ActionCode.Size = new System.Drawing.Size(464, 16);
            this.lbl_Dev_ActionCode.TabIndex = 34;
            this.lbl_Dev_ActionCode.Text = "-";
            this.lbl_Dev_ActionCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Dev_Error
            // 
            this.lbl_Dev_Error.BackColor = System.Drawing.Color.White;
            this.lbl_Dev_Error.Location = new System.Drawing.Point(8, 73);
            this.lbl_Dev_Error.Name = "lbl_Dev_Error";
            this.lbl_Dev_Error.Size = new System.Drawing.Size(464, 47);
            this.lbl_Dev_Error.TabIndex = 33;
            this.lbl_Dev_Error.Text = "-";
            this.lbl_Dev_Error.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 156);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1576, 739);
            this.panel2.TabIndex = 56;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(418, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(703, 739);
            this.panel3.TabIndex = 58;
            // 
            // panel5
            // 
            this.panel5.AutoScroll = true;
            this.panel5.Controls.Add(this.lbl_OSt_38);
            this.panel5.Controls.Add(this.lbl_O_Title_38);
            this.panel5.Controls.Add(this.lbl_IOSt_38);
            this.panel5.Controls.Add(this.lbl_IO_Title_38);
            this.panel5.Controls.Add(this.label98);
            this.panel5.Controls.Add(this.lbl_OSt_37);
            this.panel5.Controls.Add(this.lbl_O_Title_37);
            this.panel5.Controls.Add(this.lbl_OSt_36);
            this.panel5.Controls.Add(this.lbl_OSt_35);
            this.panel5.Controls.Add(this.lbl_OSt_34);
            this.panel5.Controls.Add(this.lbl_OSt_33);
            this.panel5.Controls.Add(this.lbl_O_Title_36);
            this.panel5.Controls.Add(this.lbl_O_Title_35);
            this.panel5.Controls.Add(this.lbl_O_Title_34);
            this.panel5.Controls.Add(this.lbl_O_Title_33);
            this.panel5.Controls.Add(this.lbl_O_Title_32);
            this.panel5.Controls.Add(this.lbl_OSt_32);
            this.panel5.Controls.Add(this.lbl_O_Title_28);
            this.panel5.Controls.Add(this.lbl_O_Title_29);
            this.panel5.Controls.Add(this.lbl_O_Title_30);
            this.panel5.Controls.Add(this.lbl_O_Title_31);
            this.panel5.Controls.Add(this.lbl_OSt_28);
            this.panel5.Controls.Add(this.lbl_OSt_29);
            this.panel5.Controls.Add(this.lbl_OSt_30);
            this.panel5.Controls.Add(this.lbl_OSt_31);
            this.panel5.Controls.Add(this.lbl_OSt_16);
            this.panel5.Controls.Add(this.lbl_OSt_15);
            this.panel5.Controls.Add(this.lbl_OSt_14);
            this.panel5.Controls.Add(this.lbl_OSt_13);
            this.panel5.Controls.Add(this.lbl_OSt_12);
            this.panel5.Controls.Add(this.lbl_OSt_11);
            this.panel5.Controls.Add(this.lbl_OSt_10);
            this.panel5.Controls.Add(this.lbl_OSt_9);
            this.panel5.Controls.Add(this.lbl_OSt_8);
            this.panel5.Controls.Add(this.lbl_OSt_7);
            this.panel5.Controls.Add(this.lbl_OSt_6);
            this.panel5.Controls.Add(this.lbl_OSt_5);
            this.panel5.Controls.Add(this.lbl_OSt_4);
            this.panel5.Controls.Add(this.lbl_OSt_3);
            this.panel5.Controls.Add(this.lbl_OSt_2);
            this.panel5.Controls.Add(this.lbl_OSt_1);
            this.panel5.Controls.Add(this.lbl_O_Title_16);
            this.panel5.Controls.Add(this.lbl_O_Title_15);
            this.panel5.Controls.Add(this.lbl_O_Title_14);
            this.panel5.Controls.Add(this.lbl_O_Title_13);
            this.panel5.Controls.Add(this.lbl_O_Title_17);
            this.panel5.Controls.Add(this.lbl_O_Title_12);
            this.panel5.Controls.Add(this.lbl_O_Title_18);
            this.panel5.Controls.Add(this.lbl_O_Title_11);
            this.panel5.Controls.Add(this.lbl_O_Title_19);
            this.panel5.Controls.Add(this.lbl_O_Title_10);
            this.panel5.Controls.Add(this.lbl_O_Title_20);
            this.panel5.Controls.Add(this.lbl_O_Title_9);
            this.panel5.Controls.Add(this.lbl_O_Title_21);
            this.panel5.Controls.Add(this.lbl_O_Title_8);
            this.panel5.Controls.Add(this.lbl_O_Title_22);
            this.panel5.Controls.Add(this.lbl_O_Title_7);
            this.panel5.Controls.Add(this.lbl_O_Title_23);
            this.panel5.Controls.Add(this.lbl_O_Title_6);
            this.panel5.Controls.Add(this.lbl_O_Title_24);
            this.panel5.Controls.Add(this.lbl_O_Title_5);
            this.panel5.Controls.Add(this.lbl_O_Title_25);
            this.panel5.Controls.Add(this.lbl_O_Title_4);
            this.panel5.Controls.Add(this.lbl_O_Title_26);
            this.panel5.Controls.Add(this.lbl_O_Title_3);
            this.panel5.Controls.Add(this.lbl_O_Title_27);
            this.panel5.Controls.Add(this.lbl_O_Title_2);
            this.panel5.Controls.Add(this.lbl_O_Title_1);
            this.panel5.Controls.Add(this.lbl_OSt_17);
            this.panel5.Controls.Add(this.lbl_OSt_18);
            this.panel5.Controls.Add(this.lbl_OSt_19);
            this.panel5.Controls.Add(this.lbl_OSt_20);
            this.panel5.Controls.Add(this.lbl_OSt_21);
            this.panel5.Controls.Add(this.lbl_OSt_22);
            this.panel5.Controls.Add(this.lbl_OSt_23);
            this.panel5.Controls.Add(this.lbl_OSt_24);
            this.panel5.Controls.Add(this.lbl_OSt_25);
            this.panel5.Controls.Add(this.lbl_OSt_26);
            this.panel5.Controls.Add(this.lbl_OSt_27);
            this.panel5.Controls.Add(this.lbl_IOSt_37);
            this.panel5.Controls.Add(this.lbl_IO_Title_37);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.label23);
            this.panel5.Controls.Add(this.lbl_IOSt_36);
            this.panel5.Controls.Add(this.lbl_IOSt_35);
            this.panel5.Controls.Add(this.lbl_IOSt_34);
            this.panel5.Controls.Add(this.lbl_IOSt_33);
            this.panel5.Controls.Add(this.lbl_IO_Title_36);
            this.panel5.Controls.Add(this.lbl_IO_Title_35);
            this.panel5.Controls.Add(this.lbl_IO_Title_34);
            this.panel5.Controls.Add(this.lbl_IO_Title_33);
            this.panel5.Controls.Add(this.label94);
            this.panel5.Controls.Add(this.label96);
            this.panel5.Controls.Add(this.label97);
            this.panel5.Controls.Add(this.label30);
            this.panel5.Controls.Add(this.lbl_IO_Title_32);
            this.panel5.Controls.Add(this.lbl_IOSt_32);
            this.panel5.Controls.Add(this.label230);
            this.panel5.Controls.Add(this.label229);
            this.panel5.Controls.Add(this.label228);
            this.panel5.Controls.Add(this.label227);
            this.panel5.Controls.Add(this.lbl_IO_Title_28);
            this.panel5.Controls.Add(this.lbl_IO_Title_29);
            this.panel5.Controls.Add(this.lbl_IO_Title_30);
            this.panel5.Controls.Add(this.lbl_IO_Title_31);
            this.panel5.Controls.Add(this.lbl_IOSt_28);
            this.panel5.Controls.Add(this.lbl_IOSt_29);
            this.panel5.Controls.Add(this.lbl_IOSt_30);
            this.panel5.Controls.Add(this.lbl_IOSt_31);
            this.panel5.Controls.Add(this.lbl_IOSt_16);
            this.panel5.Controls.Add(this.lbl_IOSt_15);
            this.panel5.Controls.Add(this.lbl_IOSt_14);
            this.panel5.Controls.Add(this.lbl_IOSt_13);
            this.panel5.Controls.Add(this.label289);
            this.panel5.Controls.Add(this.lbl_IOSt_12);
            this.panel5.Controls.Add(this.label241);
            this.panel5.Controls.Add(this.lbl_IOSt_11);
            this.panel5.Controls.Add(this.label240);
            this.panel5.Controls.Add(this.lbl_IOSt_10);
            this.panel5.Controls.Add(this.label239);
            this.panel5.Controls.Add(this.lbl_IOSt_9);
            this.panel5.Controls.Add(this.label238);
            this.panel5.Controls.Add(this.lbl_IOSt_8);
            this.panel5.Controls.Add(this.label237);
            this.panel5.Controls.Add(this.lbl_IOSt_7);
            this.panel5.Controls.Add(this.label236);
            this.panel5.Controls.Add(this.lbl_IOSt_6);
            this.panel5.Controls.Add(this.label235);
            this.panel5.Controls.Add(this.lbl_IOSt_5);
            this.panel5.Controls.Add(this.label234);
            this.panel5.Controls.Add(this.lbl_IOSt_4);
            this.panel5.Controls.Add(this.label233);
            this.panel5.Controls.Add(this.lbl_IOSt_3);
            this.panel5.Controls.Add(this.label232);
            this.panel5.Controls.Add(this.lbl_IOSt_2);
            this.panel5.Controls.Add(this.label231);
            this.panel5.Controls.Add(this.lbl_IOSt_1);
            this.panel5.Controls.Add(this.lbl_IO_Title_16);
            this.panel5.Controls.Add(this.lbl_IO_Title_15);
            this.panel5.Controls.Add(this.lbl_IO_Title_14);
            this.panel5.Controls.Add(this.lbl_IO_Title_13);
            this.panel5.Controls.Add(this.lbl_IO_Title_17);
            this.panel5.Controls.Add(this.lbl_IO_Title_12);
            this.panel5.Controls.Add(this.lbl_IO_Title_18);
            this.panel5.Controls.Add(this.lbl_IO_Title_11);
            this.panel5.Controls.Add(this.lbl_IO_Title_19);
            this.panel5.Controls.Add(this.lbl_IO_Title_10);
            this.panel5.Controls.Add(this.lbl_IO_Title_20);
            this.panel5.Controls.Add(this.lbl_IO_Title_9);
            this.panel5.Controls.Add(this.lbl_IO_Title_21);
            this.panel5.Controls.Add(this.lbl_IO_Title_8);
            this.panel5.Controls.Add(this.lbl_IO_Title_22);
            this.panel5.Controls.Add(this.lbl_IO_Title_7);
            this.panel5.Controls.Add(this.lbl_IO_Title_23);
            this.panel5.Controls.Add(this.lbl_IO_Title_6);
            this.panel5.Controls.Add(this.lbl_IO_Title_24);
            this.panel5.Controls.Add(this.lbl_IO_Title_5);
            this.panel5.Controls.Add(this.lbl_IO_Title_25);
            this.panel5.Controls.Add(this.lbl_IO_Title_4);
            this.panel5.Controls.Add(this.lbl_IO_Title_26);
            this.panel5.Controls.Add(this.lbl_IO_Title_3);
            this.panel5.Controls.Add(this.lbl_IO_Title_27);
            this.panel5.Controls.Add(this.lbl_IO_Title_2);
            this.panel5.Controls.Add(this.lbl_IO_Title_1);
            this.panel5.Controls.Add(this.label274);
            this.panel5.Controls.Add(this.label275);
            this.panel5.Controls.Add(this.label276);
            this.panel5.Controls.Add(this.lbl_IOSt_17);
            this.panel5.Controls.Add(this.label277);
            this.panel5.Controls.Add(this.lbl_IOSt_18);
            this.panel5.Controls.Add(this.label278);
            this.panel5.Controls.Add(this.lbl_IOSt_19);
            this.panel5.Controls.Add(this.label279);
            this.panel5.Controls.Add(this.lbl_IOSt_20);
            this.panel5.Controls.Add(this.label280);
            this.panel5.Controls.Add(this.lbl_IOSt_21);
            this.panel5.Controls.Add(this.label281);
            this.panel5.Controls.Add(this.lbl_IOSt_22);
            this.panel5.Controls.Add(this.label282);
            this.panel5.Controls.Add(this.lbl_IOSt_23);
            this.panel5.Controls.Add(this.label283);
            this.panel5.Controls.Add(this.lbl_IOSt_24);
            this.panel5.Controls.Add(this.label284);
            this.panel5.Controls.Add(this.lbl_IOSt_25);
            this.panel5.Controls.Add(this.label285);
            this.panel5.Controls.Add(this.lbl_IOSt_26);
            this.panel5.Controls.Add(this.label286);
            this.panel5.Controls.Add(this.lbl_IOSt_27);
            this.panel5.Controls.Add(this.label287);
            this.panel5.Controls.Add(this.label288);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel5.Location = new System.Drawing.Point(0, 45);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(703, 694);
            this.panel5.TabIndex = 609;
            // 
            // lbl_OSt_38
            // 
            this.lbl_OSt_38.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_38.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_38.Location = new System.Drawing.Point(637, 671);
            this.lbl_OSt_38.Name = "lbl_OSt_38";
            this.lbl_OSt_38.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_38.TabIndex = 853;
            this.lbl_OSt_38.Text = "OFF";
            this.lbl_OSt_38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_38
            // 
            this.lbl_O_Title_38.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_38.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_38.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_38.Location = new System.Drawing.Point(374, 671);
            this.lbl_O_Title_38.Name = "lbl_O_Title_38";
            this.lbl_O_Title_38.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_38.TabIndex = 852;
            this.lbl_O_Title_38.Text = "예비";
            this.lbl_O_Title_38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_38
            // 
            this.lbl_IOSt_38.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_38.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_38.Location = new System.Drawing.Point(326, 671);
            this.lbl_IOSt_38.Name = "lbl_IOSt_38";
            this.lbl_IOSt_38.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_38.TabIndex = 851;
            this.lbl_IOSt_38.Text = "OFF";
            this.lbl_IOSt_38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_38
            // 
            this.lbl_IO_Title_38.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_38.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_38.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_38.Location = new System.Drawing.Point(63, 671);
            this.lbl_IO_Title_38.Name = "lbl_IO_Title_38";
            this.lbl_IO_Title_38.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_38.TabIndex = 850;
            this.lbl_IO_Title_38.Text = "예비";
            this.lbl_IO_Title_38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label98
            // 
            this.label98.BackColor = System.Drawing.SystemColors.Highlight;
            this.label98.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label98.ForeColor = System.Drawing.Color.White;
            this.label98.Location = new System.Drawing.Point(15, 671);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(48, 16);
            this.label98.TabIndex = 849;
            this.label98.Text = "No 38";
            this.label98.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_37
            // 
            this.lbl_OSt_37.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_37.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_37.Location = new System.Drawing.Point(637, 653);
            this.lbl_OSt_37.Name = "lbl_OSt_37";
            this.lbl_OSt_37.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_37.TabIndex = 848;
            this.lbl_OSt_37.Text = "OFF";
            this.lbl_OSt_37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_37
            // 
            this.lbl_O_Title_37.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_37.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_37.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_37.Location = new System.Drawing.Point(374, 653);
            this.lbl_O_Title_37.Name = "lbl_O_Title_37";
            this.lbl_O_Title_37.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_37.TabIndex = 847;
            this.lbl_O_Title_37.Text = "예비";
            this.lbl_O_Title_37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_36
            // 
            this.lbl_OSt_36.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_36.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_36.Location = new System.Drawing.Point(637, 635);
            this.lbl_OSt_36.Name = "lbl_OSt_36";
            this.lbl_OSt_36.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_36.TabIndex = 846;
            this.lbl_OSt_36.Text = "OFF";
            this.lbl_OSt_36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_35
            // 
            this.lbl_OSt_35.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_35.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_35.Location = new System.Drawing.Point(637, 617);
            this.lbl_OSt_35.Name = "lbl_OSt_35";
            this.lbl_OSt_35.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_35.TabIndex = 845;
            this.lbl_OSt_35.Text = "OFF";
            this.lbl_OSt_35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_34
            // 
            this.lbl_OSt_34.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_34.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_34.Location = new System.Drawing.Point(637, 599);
            this.lbl_OSt_34.Name = "lbl_OSt_34";
            this.lbl_OSt_34.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_34.TabIndex = 844;
            this.lbl_OSt_34.Text = "OFF";
            this.lbl_OSt_34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_33
            // 
            this.lbl_OSt_33.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_33.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_33.Location = new System.Drawing.Point(637, 581);
            this.lbl_OSt_33.Name = "lbl_OSt_33";
            this.lbl_OSt_33.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_33.TabIndex = 843;
            this.lbl_OSt_33.Text = "OFF";
            this.lbl_OSt_33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_36
            // 
            this.lbl_O_Title_36.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_36.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_36.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_36.Location = new System.Drawing.Point(374, 635);
            this.lbl_O_Title_36.Name = "lbl_O_Title_36";
            this.lbl_O_Title_36.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_36.TabIndex = 842;
            this.lbl_O_Title_36.Text = "예비";
            this.lbl_O_Title_36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_35
            // 
            this.lbl_O_Title_35.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_35.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_35.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_35.Location = new System.Drawing.Point(374, 617);
            this.lbl_O_Title_35.Name = "lbl_O_Title_35";
            this.lbl_O_Title_35.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_35.TabIndex = 841;
            this.lbl_O_Title_35.Text = "예비";
            this.lbl_O_Title_35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_34
            // 
            this.lbl_O_Title_34.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_34.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_34.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_34.Location = new System.Drawing.Point(374, 599);
            this.lbl_O_Title_34.Name = "lbl_O_Title_34";
            this.lbl_O_Title_34.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_34.TabIndex = 840;
            this.lbl_O_Title_34.Text = "예비";
            this.lbl_O_Title_34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_33
            // 
            this.lbl_O_Title_33.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_33.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_33.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_33.Location = new System.Drawing.Point(374, 581);
            this.lbl_O_Title_33.Name = "lbl_O_Title_33";
            this.lbl_O_Title_33.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_33.TabIndex = 839;
            this.lbl_O_Title_33.Text = "DEVICE_FLT";
            this.lbl_O_Title_33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_32
            // 
            this.lbl_O_Title_32.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_32.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_32.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_32.Location = new System.Drawing.Point(374, 563);
            this.lbl_O_Title_32.Name = "lbl_O_Title_32";
            this.lbl_O_Title_32.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_32.TabIndex = 837;
            this.lbl_O_Title_32.Text = "예비";
            this.lbl_O_Title_32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_32
            // 
            this.lbl_OSt_32.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_32.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_32.Location = new System.Drawing.Point(637, 563);
            this.lbl_OSt_32.Name = "lbl_OSt_32";
            this.lbl_OSt_32.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_32.TabIndex = 838;
            this.lbl_OSt_32.Text = "OFF";
            this.lbl_OSt_32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_28
            // 
            this.lbl_O_Title_28.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_28.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_28.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_28.Location = new System.Drawing.Point(374, 491);
            this.lbl_O_Title_28.Name = "lbl_O_Title_28";
            this.lbl_O_Title_28.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_28.TabIndex = 829;
            this.lbl_O_Title_28.Text = "예비";
            this.lbl_O_Title_28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_29
            // 
            this.lbl_O_Title_29.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_29.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_29.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_29.Location = new System.Drawing.Point(374, 509);
            this.lbl_O_Title_29.Name = "lbl_O_Title_29";
            this.lbl_O_Title_29.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_29.TabIndex = 830;
            this.lbl_O_Title_29.Text = "예비";
            this.lbl_O_Title_29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_30
            // 
            this.lbl_O_Title_30.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_30.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_30.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_30.Location = new System.Drawing.Point(374, 527);
            this.lbl_O_Title_30.Name = "lbl_O_Title_30";
            this.lbl_O_Title_30.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_30.TabIndex = 831;
            this.lbl_O_Title_30.Text = "예비";
            this.lbl_O_Title_30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_31
            // 
            this.lbl_O_Title_31.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_31.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_31.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_31.Location = new System.Drawing.Point(374, 545);
            this.lbl_O_Title_31.Name = "lbl_O_Title_31";
            this.lbl_O_Title_31.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_31.TabIndex = 832;
            this.lbl_O_Title_31.Text = "예비";
            this.lbl_O_Title_31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_28
            // 
            this.lbl_OSt_28.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_28.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_28.Location = new System.Drawing.Point(637, 491);
            this.lbl_OSt_28.Name = "lbl_OSt_28";
            this.lbl_OSt_28.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_28.TabIndex = 833;
            this.lbl_OSt_28.Text = "OFF";
            this.lbl_OSt_28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_29
            // 
            this.lbl_OSt_29.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_29.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_29.Location = new System.Drawing.Point(637, 509);
            this.lbl_OSt_29.Name = "lbl_OSt_29";
            this.lbl_OSt_29.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_29.TabIndex = 834;
            this.lbl_OSt_29.Text = "OFF";
            this.lbl_OSt_29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_30
            // 
            this.lbl_OSt_30.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_30.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_30.Location = new System.Drawing.Point(637, 527);
            this.lbl_OSt_30.Name = "lbl_OSt_30";
            this.lbl_OSt_30.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_30.TabIndex = 835;
            this.lbl_OSt_30.Text = "OFF";
            this.lbl_OSt_30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_31
            // 
            this.lbl_OSt_31.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_31.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_31.Location = new System.Drawing.Point(637, 545);
            this.lbl_OSt_31.Name = "lbl_OSt_31";
            this.lbl_OSt_31.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_31.TabIndex = 836;
            this.lbl_OSt_31.Text = "OFF";
            this.lbl_OSt_31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_16
            // 
            this.lbl_OSt_16.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_16.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_16.Location = new System.Drawing.Point(637, 275);
            this.lbl_OSt_16.Name = "lbl_OSt_16";
            this.lbl_OSt_16.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_16.TabIndex = 828;
            this.lbl_OSt_16.Text = "OFF";
            this.lbl_OSt_16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_15
            // 
            this.lbl_OSt_15.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_15.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_15.Location = new System.Drawing.Point(637, 257);
            this.lbl_OSt_15.Name = "lbl_OSt_15";
            this.lbl_OSt_15.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_15.TabIndex = 827;
            this.lbl_OSt_15.Text = "OFF";
            this.lbl_OSt_15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_14
            // 
            this.lbl_OSt_14.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_14.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_14.Location = new System.Drawing.Point(637, 239);
            this.lbl_OSt_14.Name = "lbl_OSt_14";
            this.lbl_OSt_14.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_14.TabIndex = 826;
            this.lbl_OSt_14.Text = "OFF";
            this.lbl_OSt_14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_13
            // 
            this.lbl_OSt_13.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_13.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_13.Location = new System.Drawing.Point(637, 221);
            this.lbl_OSt_13.Name = "lbl_OSt_13";
            this.lbl_OSt_13.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_13.TabIndex = 825;
            this.lbl_OSt_13.Text = "OFF";
            this.lbl_OSt_13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_12
            // 
            this.lbl_OSt_12.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_12.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_12.Location = new System.Drawing.Point(637, 203);
            this.lbl_OSt_12.Name = "lbl_OSt_12";
            this.lbl_OSt_12.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_12.TabIndex = 824;
            this.lbl_OSt_12.Text = "OFF";
            this.lbl_OSt_12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_11
            // 
            this.lbl_OSt_11.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_11.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_11.Location = new System.Drawing.Point(637, 185);
            this.lbl_OSt_11.Name = "lbl_OSt_11";
            this.lbl_OSt_11.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_11.TabIndex = 823;
            this.lbl_OSt_11.Text = "OFF";
            this.lbl_OSt_11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_10
            // 
            this.lbl_OSt_10.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_10.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_10.Location = new System.Drawing.Point(637, 167);
            this.lbl_OSt_10.Name = "lbl_OSt_10";
            this.lbl_OSt_10.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_10.TabIndex = 822;
            this.lbl_OSt_10.Text = "OFF";
            this.lbl_OSt_10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_9
            // 
            this.lbl_OSt_9.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_9.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_9.Location = new System.Drawing.Point(637, 149);
            this.lbl_OSt_9.Name = "lbl_OSt_9";
            this.lbl_OSt_9.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_9.TabIndex = 821;
            this.lbl_OSt_9.Text = "OFF";
            this.lbl_OSt_9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_8
            // 
            this.lbl_OSt_8.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_8.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_8.Location = new System.Drawing.Point(637, 131);
            this.lbl_OSt_8.Name = "lbl_OSt_8";
            this.lbl_OSt_8.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_8.TabIndex = 820;
            this.lbl_OSt_8.Text = "OFF";
            this.lbl_OSt_8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_7
            // 
            this.lbl_OSt_7.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_7.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_7.Location = new System.Drawing.Point(637, 113);
            this.lbl_OSt_7.Name = "lbl_OSt_7";
            this.lbl_OSt_7.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_7.TabIndex = 819;
            this.lbl_OSt_7.Text = "OFF";
            this.lbl_OSt_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_6
            // 
            this.lbl_OSt_6.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_6.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_6.Location = new System.Drawing.Point(637, 95);
            this.lbl_OSt_6.Name = "lbl_OSt_6";
            this.lbl_OSt_6.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_6.TabIndex = 818;
            this.lbl_OSt_6.Text = "OFF";
            this.lbl_OSt_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_5
            // 
            this.lbl_OSt_5.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_5.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_5.Location = new System.Drawing.Point(637, 77);
            this.lbl_OSt_5.Name = "lbl_OSt_5";
            this.lbl_OSt_5.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_5.TabIndex = 817;
            this.lbl_OSt_5.Text = "OFF";
            this.lbl_OSt_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_4
            // 
            this.lbl_OSt_4.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_4.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_4.Location = new System.Drawing.Point(637, 59);
            this.lbl_OSt_4.Name = "lbl_OSt_4";
            this.lbl_OSt_4.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_4.TabIndex = 816;
            this.lbl_OSt_4.Text = "OFF";
            this.lbl_OSt_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_3
            // 
            this.lbl_OSt_3.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_3.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_3.Location = new System.Drawing.Point(637, 41);
            this.lbl_OSt_3.Name = "lbl_OSt_3";
            this.lbl_OSt_3.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_3.TabIndex = 815;
            this.lbl_OSt_3.Text = "OFF";
            this.lbl_OSt_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_2
            // 
            this.lbl_OSt_2.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_2.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_2.Location = new System.Drawing.Point(637, 23);
            this.lbl_OSt_2.Name = "lbl_OSt_2";
            this.lbl_OSt_2.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_2.TabIndex = 814;
            this.lbl_OSt_2.Text = "OFF";
            this.lbl_OSt_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_1
            // 
            this.lbl_OSt_1.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_1.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_1.Location = new System.Drawing.Point(637, 5);
            this.lbl_OSt_1.Name = "lbl_OSt_1";
            this.lbl_OSt_1.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_1.TabIndex = 813;
            this.lbl_OSt_1.Text = "OFF";
            this.lbl_OSt_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_16
            // 
            this.lbl_O_Title_16.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_16.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_16.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_16.Location = new System.Drawing.Point(374, 275);
            this.lbl_O_Title_16.Name = "lbl_O_Title_16";
            this.lbl_O_Title_16.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_16.TabIndex = 812;
            this.lbl_O_Title_16.Text = "예비";
            this.lbl_O_Title_16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_15
            // 
            this.lbl_O_Title_15.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_15.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_15.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_15.Location = new System.Drawing.Point(374, 257);
            this.lbl_O_Title_15.Name = "lbl_O_Title_15";
            this.lbl_O_Title_15.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_15.TabIndex = 811;
            this.lbl_O_Title_15.Text = "예비";
            this.lbl_O_Title_15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_14
            // 
            this.lbl_O_Title_14.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_14.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_14.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_14.Location = new System.Drawing.Point(374, 239);
            this.lbl_O_Title_14.Name = "lbl_O_Title_14";
            this.lbl_O_Title_14.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_14.TabIndex = 810;
            this.lbl_O_Title_14.Text = "예비";
            this.lbl_O_Title_14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_13
            // 
            this.lbl_O_Title_13.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_13.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_13.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_13.Location = new System.Drawing.Point(374, 221);
            this.lbl_O_Title_13.Name = "lbl_O_Title_13";
            this.lbl_O_Title_13.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_13.TabIndex = 809;
            this.lbl_O_Title_13.Text = "예비";
            this.lbl_O_Title_13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_17
            // 
            this.lbl_O_Title_17.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_17.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_17.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_17.Location = new System.Drawing.Point(374, 293);
            this.lbl_O_Title_17.Name = "lbl_O_Title_17";
            this.lbl_O_Title_17.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_17.TabIndex = 775;
            this.lbl_O_Title_17.Text = "예비";
            this.lbl_O_Title_17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_12
            // 
            this.lbl_O_Title_12.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_12.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_12.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_12.Location = new System.Drawing.Point(374, 203);
            this.lbl_O_Title_12.Name = "lbl_O_Title_12";
            this.lbl_O_Title_12.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_12.TabIndex = 808;
            this.lbl_O_Title_12.Text = "예비";
            this.lbl_O_Title_12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_18
            // 
            this.lbl_O_Title_18.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_18.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_18.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_18.Location = new System.Drawing.Point(374, 311);
            this.lbl_O_Title_18.Name = "lbl_O_Title_18";
            this.lbl_O_Title_18.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_18.TabIndex = 776;
            this.lbl_O_Title_18.Text = "예비";
            this.lbl_O_Title_18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_11
            // 
            this.lbl_O_Title_11.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_11.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_11.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_11.Location = new System.Drawing.Point(374, 185);
            this.lbl_O_Title_11.Name = "lbl_O_Title_11";
            this.lbl_O_Title_11.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_11.TabIndex = 807;
            this.lbl_O_Title_11.Text = "예비";
            this.lbl_O_Title_11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_19
            // 
            this.lbl_O_Title_19.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_19.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_19.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_19.Location = new System.Drawing.Point(374, 329);
            this.lbl_O_Title_19.Name = "lbl_O_Title_19";
            this.lbl_O_Title_19.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_19.TabIndex = 777;
            this.lbl_O_Title_19.Text = "예비";
            this.lbl_O_Title_19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_10
            // 
            this.lbl_O_Title_10.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_10.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_10.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_10.Location = new System.Drawing.Point(374, 167);
            this.lbl_O_Title_10.Name = "lbl_O_Title_10";
            this.lbl_O_Title_10.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_10.TabIndex = 806;
            this.lbl_O_Title_10.Text = "예비";
            this.lbl_O_Title_10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_20
            // 
            this.lbl_O_Title_20.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_20.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_20.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_20.Location = new System.Drawing.Point(374, 347);
            this.lbl_O_Title_20.Name = "lbl_O_Title_20";
            this.lbl_O_Title_20.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_20.TabIndex = 778;
            this.lbl_O_Title_20.Text = "예비";
            this.lbl_O_Title_20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_9
            // 
            this.lbl_O_Title_9.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_9.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_9.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_9.Location = new System.Drawing.Point(374, 149);
            this.lbl_O_Title_9.Name = "lbl_O_Title_9";
            this.lbl_O_Title_9.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_9.TabIndex = 805;
            this.lbl_O_Title_9.Text = "예비";
            this.lbl_O_Title_9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_21
            // 
            this.lbl_O_Title_21.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_21.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_21.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_21.Location = new System.Drawing.Point(374, 365);
            this.lbl_O_Title_21.Name = "lbl_O_Title_21";
            this.lbl_O_Title_21.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_21.TabIndex = 779;
            this.lbl_O_Title_21.Text = "예비";
            this.lbl_O_Title_21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_8
            // 
            this.lbl_O_Title_8.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_8.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_8.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_8.Location = new System.Drawing.Point(374, 131);
            this.lbl_O_Title_8.Name = "lbl_O_Title_8";
            this.lbl_O_Title_8.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_8.TabIndex = 804;
            this.lbl_O_Title_8.Text = "예비";
            this.lbl_O_Title_8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_22
            // 
            this.lbl_O_Title_22.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_22.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_22.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_22.Location = new System.Drawing.Point(374, 383);
            this.lbl_O_Title_22.Name = "lbl_O_Title_22";
            this.lbl_O_Title_22.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_22.TabIndex = 780;
            this.lbl_O_Title_22.Text = "예비";
            this.lbl_O_Title_22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_7
            // 
            this.lbl_O_Title_7.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_7.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_7.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_7.Location = new System.Drawing.Point(374, 113);
            this.lbl_O_Title_7.Name = "lbl_O_Title_7";
            this.lbl_O_Title_7.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_7.TabIndex = 803;
            this.lbl_O_Title_7.Text = "예비";
            this.lbl_O_Title_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_23
            // 
            this.lbl_O_Title_23.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_23.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_23.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_23.Location = new System.Drawing.Point(374, 401);
            this.lbl_O_Title_23.Name = "lbl_O_Title_23";
            this.lbl_O_Title_23.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_23.TabIndex = 781;
            this.lbl_O_Title_23.Text = "예비";
            this.lbl_O_Title_23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_6
            // 
            this.lbl_O_Title_6.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_6.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_6.Location = new System.Drawing.Point(374, 95);
            this.lbl_O_Title_6.Name = "lbl_O_Title_6";
            this.lbl_O_Title_6.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_6.TabIndex = 802;
            this.lbl_O_Title_6.Text = "예비";
            this.lbl_O_Title_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_24
            // 
            this.lbl_O_Title_24.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_24.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_24.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_24.Location = new System.Drawing.Point(374, 419);
            this.lbl_O_Title_24.Name = "lbl_O_Title_24";
            this.lbl_O_Title_24.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_24.TabIndex = 782;
            this.lbl_O_Title_24.Text = "예비";
            this.lbl_O_Title_24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_5
            // 
            this.lbl_O_Title_5.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_5.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_5.Location = new System.Drawing.Point(374, 77);
            this.lbl_O_Title_5.Name = "lbl_O_Title_5";
            this.lbl_O_Title_5.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_5.TabIndex = 801;
            this.lbl_O_Title_5.Text = "예비";
            this.lbl_O_Title_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_25
            // 
            this.lbl_O_Title_25.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_25.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_25.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_25.Location = new System.Drawing.Point(374, 437);
            this.lbl_O_Title_25.Name = "lbl_O_Title_25";
            this.lbl_O_Title_25.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_25.TabIndex = 783;
            this.lbl_O_Title_25.Text = "예비";
            this.lbl_O_Title_25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_4
            // 
            this.lbl_O_Title_4.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_4.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_4.Location = new System.Drawing.Point(374, 59);
            this.lbl_O_Title_4.Name = "lbl_O_Title_4";
            this.lbl_O_Title_4.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_4.TabIndex = 800;
            this.lbl_O_Title_4.Text = "예비";
            this.lbl_O_Title_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_26
            // 
            this.lbl_O_Title_26.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_26.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_26.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_26.Location = new System.Drawing.Point(374, 455);
            this.lbl_O_Title_26.Name = "lbl_O_Title_26";
            this.lbl_O_Title_26.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_26.TabIndex = 784;
            this.lbl_O_Title_26.Text = "예비";
            this.lbl_O_Title_26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_3
            // 
            this.lbl_O_Title_3.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_3.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_3.Location = new System.Drawing.Point(374, 41);
            this.lbl_O_Title_3.Name = "lbl_O_Title_3";
            this.lbl_O_Title_3.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_3.TabIndex = 799;
            this.lbl_O_Title_3.Text = "예비";
            this.lbl_O_Title_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_27
            // 
            this.lbl_O_Title_27.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_27.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_27.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_27.Location = new System.Drawing.Point(374, 473);
            this.lbl_O_Title_27.Name = "lbl_O_Title_27";
            this.lbl_O_Title_27.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_27.TabIndex = 785;
            this.lbl_O_Title_27.Text = "예비";
            this.lbl_O_Title_27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_2
            // 
            this.lbl_O_Title_2.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_2.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_2.Location = new System.Drawing.Point(374, 23);
            this.lbl_O_Title_2.Name = "lbl_O_Title_2";
            this.lbl_O_Title_2.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_2.TabIndex = 798;
            this.lbl_O_Title_2.Text = "예비";
            this.lbl_O_Title_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_O_Title_1
            // 
            this.lbl_O_Title_1.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_O_Title_1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_O_Title_1.ForeColor = System.Drawing.Color.White;
            this.lbl_O_Title_1.Location = new System.Drawing.Point(374, 5);
            this.lbl_O_Title_1.Name = "lbl_O_Title_1";
            this.lbl_O_Title_1.Size = new System.Drawing.Size(260, 16);
            this.lbl_O_Title_1.TabIndex = 797;
            this.lbl_O_Title_1.Text = "DEVICE_FLT";
            this.lbl_O_Title_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_17
            // 
            this.lbl_OSt_17.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_17.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_17.Location = new System.Drawing.Point(637, 293);
            this.lbl_OSt_17.Name = "lbl_OSt_17";
            this.lbl_OSt_17.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_17.TabIndex = 786;
            this.lbl_OSt_17.Text = "OFF";
            this.lbl_OSt_17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_18
            // 
            this.lbl_OSt_18.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_18.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_18.Location = new System.Drawing.Point(637, 311);
            this.lbl_OSt_18.Name = "lbl_OSt_18";
            this.lbl_OSt_18.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_18.TabIndex = 787;
            this.lbl_OSt_18.Text = "OFF";
            this.lbl_OSt_18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_19
            // 
            this.lbl_OSt_19.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_19.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_19.Location = new System.Drawing.Point(637, 329);
            this.lbl_OSt_19.Name = "lbl_OSt_19";
            this.lbl_OSt_19.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_19.TabIndex = 788;
            this.lbl_OSt_19.Text = "OFF";
            this.lbl_OSt_19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_20
            // 
            this.lbl_OSt_20.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_20.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_20.Location = new System.Drawing.Point(637, 347);
            this.lbl_OSt_20.Name = "lbl_OSt_20";
            this.lbl_OSt_20.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_20.TabIndex = 789;
            this.lbl_OSt_20.Text = "OFF";
            this.lbl_OSt_20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_21
            // 
            this.lbl_OSt_21.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_21.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_21.Location = new System.Drawing.Point(637, 365);
            this.lbl_OSt_21.Name = "lbl_OSt_21";
            this.lbl_OSt_21.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_21.TabIndex = 790;
            this.lbl_OSt_21.Text = "OFF";
            this.lbl_OSt_21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_22
            // 
            this.lbl_OSt_22.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_22.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_22.Location = new System.Drawing.Point(637, 383);
            this.lbl_OSt_22.Name = "lbl_OSt_22";
            this.lbl_OSt_22.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_22.TabIndex = 791;
            this.lbl_OSt_22.Text = "OFF";
            this.lbl_OSt_22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_23
            // 
            this.lbl_OSt_23.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_23.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_23.Location = new System.Drawing.Point(637, 401);
            this.lbl_OSt_23.Name = "lbl_OSt_23";
            this.lbl_OSt_23.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_23.TabIndex = 792;
            this.lbl_OSt_23.Text = "OFF";
            this.lbl_OSt_23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_24
            // 
            this.lbl_OSt_24.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_24.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_24.Location = new System.Drawing.Point(637, 419);
            this.lbl_OSt_24.Name = "lbl_OSt_24";
            this.lbl_OSt_24.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_24.TabIndex = 793;
            this.lbl_OSt_24.Text = "OFF";
            this.lbl_OSt_24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_25
            // 
            this.lbl_OSt_25.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_25.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_25.Location = new System.Drawing.Point(637, 437);
            this.lbl_OSt_25.Name = "lbl_OSt_25";
            this.lbl_OSt_25.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_25.TabIndex = 794;
            this.lbl_OSt_25.Text = "OFF";
            this.lbl_OSt_25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_26
            // 
            this.lbl_OSt_26.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_26.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_26.Location = new System.Drawing.Point(637, 455);
            this.lbl_OSt_26.Name = "lbl_OSt_26";
            this.lbl_OSt_26.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_26.TabIndex = 795;
            this.lbl_OSt_26.Text = "OFF";
            this.lbl_OSt_26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OSt_27
            // 
            this.lbl_OSt_27.BackColor = System.Drawing.Color.Silver;
            this.lbl_OSt_27.ForeColor = System.Drawing.Color.Black;
            this.lbl_OSt_27.Location = new System.Drawing.Point(637, 473);
            this.lbl_OSt_27.Name = "lbl_OSt_27";
            this.lbl_OSt_27.Size = new System.Drawing.Size(42, 16);
            this.lbl_OSt_27.TabIndex = 796;
            this.lbl_OSt_27.Text = "OFF";
            this.lbl_OSt_27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_37
            // 
            this.lbl_IOSt_37.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_37.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_37.Location = new System.Drawing.Point(326, 653);
            this.lbl_IOSt_37.Name = "lbl_IOSt_37";
            this.lbl_IOSt_37.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_37.TabIndex = 774;
            this.lbl_IOSt_37.Text = "OFF";
            this.lbl_IOSt_37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_37
            // 
            this.lbl_IO_Title_37.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_37.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_37.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_37.Location = new System.Drawing.Point(63, 653);
            this.lbl_IO_Title_37.Name = "lbl_IO_Title_37";
            this.lbl_IO_Title_37.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_37.TabIndex = 773;
            this.lbl_IO_Title_37.Text = "예비";
            this.lbl_IO_Title_37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.SystemColors.Highlight;
            this.label11.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(15, 653);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 16);
            this.label11.TabIndex = 772;
            this.label11.Text = "No 37";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.SystemColors.Highlight;
            this.label23.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(15, 581);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(48, 16);
            this.label23.TabIndex = 736;
            this.label23.Text = "No 33";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_36
            // 
            this.lbl_IOSt_36.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_36.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_36.Location = new System.Drawing.Point(326, 635);
            this.lbl_IOSt_36.Name = "lbl_IOSt_36";
            this.lbl_IOSt_36.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_36.TabIndex = 771;
            this.lbl_IOSt_36.Text = "OFF";
            this.lbl_IOSt_36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_35
            // 
            this.lbl_IOSt_35.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_35.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_35.Location = new System.Drawing.Point(326, 617);
            this.lbl_IOSt_35.Name = "lbl_IOSt_35";
            this.lbl_IOSt_35.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_35.TabIndex = 770;
            this.lbl_IOSt_35.Text = "OFF";
            this.lbl_IOSt_35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_34
            // 
            this.lbl_IOSt_34.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_34.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_34.Location = new System.Drawing.Point(326, 599);
            this.lbl_IOSt_34.Name = "lbl_IOSt_34";
            this.lbl_IOSt_34.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_34.TabIndex = 769;
            this.lbl_IOSt_34.Text = "OFF";
            this.lbl_IOSt_34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_33
            // 
            this.lbl_IOSt_33.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_33.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_33.Location = new System.Drawing.Point(326, 581);
            this.lbl_IOSt_33.Name = "lbl_IOSt_33";
            this.lbl_IOSt_33.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_33.TabIndex = 768;
            this.lbl_IOSt_33.Text = "OFF";
            this.lbl_IOSt_33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_36
            // 
            this.lbl_IO_Title_36.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_36.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_36.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_36.Location = new System.Drawing.Point(63, 635);
            this.lbl_IO_Title_36.Name = "lbl_IO_Title_36";
            this.lbl_IO_Title_36.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_36.TabIndex = 755;
            this.lbl_IO_Title_36.Text = "예비";
            this.lbl_IO_Title_36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_35
            // 
            this.lbl_IO_Title_35.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_35.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_35.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_35.Location = new System.Drawing.Point(63, 617);
            this.lbl_IO_Title_35.Name = "lbl_IO_Title_35";
            this.lbl_IO_Title_35.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_35.TabIndex = 754;
            this.lbl_IO_Title_35.Text = "예비";
            this.lbl_IO_Title_35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_34
            // 
            this.lbl_IO_Title_34.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_34.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_34.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_34.Location = new System.Drawing.Point(63, 599);
            this.lbl_IO_Title_34.Name = "lbl_IO_Title_34";
            this.lbl_IO_Title_34.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_34.TabIndex = 753;
            this.lbl_IO_Title_34.Text = "예비";
            this.lbl_IO_Title_34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_33
            // 
            this.lbl_IO_Title_33.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_33.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_33.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_33.Location = new System.Drawing.Point(63, 581);
            this.lbl_IO_Title_33.Name = "lbl_IO_Title_33";
            this.lbl_IO_Title_33.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_33.TabIndex = 752;
            this.lbl_IO_Title_33.Text = "DEVICE_FLT";
            this.lbl_IO_Title_33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label94
            // 
            this.label94.BackColor = System.Drawing.SystemColors.Highlight;
            this.label94.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label94.ForeColor = System.Drawing.Color.White;
            this.label94.Location = new System.Drawing.Point(15, 635);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(48, 16);
            this.label94.TabIndex = 739;
            this.label94.Text = "No 36";
            this.label94.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label96
            // 
            this.label96.BackColor = System.Drawing.SystemColors.Highlight;
            this.label96.ForeColor = System.Drawing.Color.White;
            this.label96.Location = new System.Drawing.Point(15, 617);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(48, 16);
            this.label96.TabIndex = 738;
            this.label96.Text = "No 35";
            this.label96.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label97
            // 
            this.label97.BackColor = System.Drawing.SystemColors.Highlight;
            this.label97.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label97.ForeColor = System.Drawing.Color.White;
            this.label97.Location = new System.Drawing.Point(15, 599);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(48, 16);
            this.label97.TabIndex = 737;
            this.label97.Text = "No 34";
            this.label97.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.SystemColors.Highlight;
            this.label30.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label30.ForeColor = System.Drawing.Color.White;
            this.label30.Location = new System.Drawing.Point(15, 563);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(48, 16);
            this.label30.TabIndex = 700;
            this.label30.Text = "No 32";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_32
            // 
            this.lbl_IO_Title_32.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_32.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_32.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_32.Location = new System.Drawing.Point(63, 563);
            this.lbl_IO_Title_32.Name = "lbl_IO_Title_32";
            this.lbl_IO_Title_32.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_32.TabIndex = 701;
            this.lbl_IO_Title_32.Text = "예비";
            this.lbl_IO_Title_32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_32
            // 
            this.lbl_IOSt_32.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_32.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_32.Location = new System.Drawing.Point(326, 563);
            this.lbl_IOSt_32.Name = "lbl_IOSt_32";
            this.lbl_IOSt_32.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_32.TabIndex = 702;
            this.lbl_IOSt_32.Text = "OFF";
            this.lbl_IOSt_32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label230
            // 
            this.label230.BackColor = System.Drawing.SystemColors.Highlight;
            this.label230.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label230.ForeColor = System.Drawing.Color.White;
            this.label230.Location = new System.Drawing.Point(15, 491);
            this.label230.Name = "label230";
            this.label230.Size = new System.Drawing.Size(48, 16);
            this.label230.TabIndex = 688;
            this.label230.Text = "No 28";
            this.label230.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label229
            // 
            this.label229.BackColor = System.Drawing.SystemColors.Highlight;
            this.label229.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label229.ForeColor = System.Drawing.Color.White;
            this.label229.Location = new System.Drawing.Point(15, 509);
            this.label229.Name = "label229";
            this.label229.Size = new System.Drawing.Size(48, 16);
            this.label229.TabIndex = 689;
            this.label229.Text = "No 29";
            this.label229.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label228
            // 
            this.label228.BackColor = System.Drawing.SystemColors.Highlight;
            this.label228.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label228.ForeColor = System.Drawing.Color.White;
            this.label228.Location = new System.Drawing.Point(15, 527);
            this.label228.Name = "label228";
            this.label228.Size = new System.Drawing.Size(48, 16);
            this.label228.TabIndex = 690;
            this.label228.Text = "No 30";
            this.label228.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label227
            // 
            this.label227.BackColor = System.Drawing.SystemColors.Highlight;
            this.label227.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label227.ForeColor = System.Drawing.Color.White;
            this.label227.Location = new System.Drawing.Point(15, 545);
            this.label227.Name = "label227";
            this.label227.Size = new System.Drawing.Size(48, 16);
            this.label227.TabIndex = 691;
            this.label227.Text = "No 31";
            this.label227.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_28
            // 
            this.lbl_IO_Title_28.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_28.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_28.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_28.Location = new System.Drawing.Point(63, 491);
            this.lbl_IO_Title_28.Name = "lbl_IO_Title_28";
            this.lbl_IO_Title_28.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_28.TabIndex = 692;
            this.lbl_IO_Title_28.Text = "예비";
            this.lbl_IO_Title_28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_29
            // 
            this.lbl_IO_Title_29.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_29.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_29.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_29.Location = new System.Drawing.Point(63, 509);
            this.lbl_IO_Title_29.Name = "lbl_IO_Title_29";
            this.lbl_IO_Title_29.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_29.TabIndex = 693;
            this.lbl_IO_Title_29.Text = "예비";
            this.lbl_IO_Title_29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_30
            // 
            this.lbl_IO_Title_30.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_30.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_30.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_30.Location = new System.Drawing.Point(63, 527);
            this.lbl_IO_Title_30.Name = "lbl_IO_Title_30";
            this.lbl_IO_Title_30.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_30.TabIndex = 694;
            this.lbl_IO_Title_30.Text = "예비";
            this.lbl_IO_Title_30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_31
            // 
            this.lbl_IO_Title_31.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_31.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_31.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_31.Location = new System.Drawing.Point(63, 545);
            this.lbl_IO_Title_31.Name = "lbl_IO_Title_31";
            this.lbl_IO_Title_31.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_31.TabIndex = 695;
            this.lbl_IO_Title_31.Text = "예비";
            this.lbl_IO_Title_31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_28
            // 
            this.lbl_IOSt_28.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_28.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_28.Location = new System.Drawing.Point(326, 491);
            this.lbl_IOSt_28.Name = "lbl_IOSt_28";
            this.lbl_IOSt_28.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_28.TabIndex = 696;
            this.lbl_IOSt_28.Text = "OFF";
            this.lbl_IOSt_28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_29
            // 
            this.lbl_IOSt_29.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_29.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_29.Location = new System.Drawing.Point(326, 509);
            this.lbl_IOSt_29.Name = "lbl_IOSt_29";
            this.lbl_IOSt_29.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_29.TabIndex = 697;
            this.lbl_IOSt_29.Text = "OFF";
            this.lbl_IOSt_29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_30
            // 
            this.lbl_IOSt_30.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_30.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_30.Location = new System.Drawing.Point(326, 527);
            this.lbl_IOSt_30.Name = "lbl_IOSt_30";
            this.lbl_IOSt_30.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_30.TabIndex = 698;
            this.lbl_IOSt_30.Text = "OFF";
            this.lbl_IOSt_30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_31
            // 
            this.lbl_IOSt_31.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_31.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_31.Location = new System.Drawing.Point(326, 545);
            this.lbl_IOSt_31.Name = "lbl_IOSt_31";
            this.lbl_IOSt_31.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_31.TabIndex = 699;
            this.lbl_IOSt_31.Text = "OFF";
            this.lbl_IOSt_31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_16
            // 
            this.lbl_IOSt_16.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_16.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_16.Location = new System.Drawing.Point(326, 275);
            this.lbl_IOSt_16.Name = "lbl_IOSt_16";
            this.lbl_IOSt_16.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_16.TabIndex = 687;
            this.lbl_IOSt_16.Text = "OFF";
            this.lbl_IOSt_16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_15
            // 
            this.lbl_IOSt_15.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_15.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_15.Location = new System.Drawing.Point(326, 257);
            this.lbl_IOSt_15.Name = "lbl_IOSt_15";
            this.lbl_IOSt_15.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_15.TabIndex = 686;
            this.lbl_IOSt_15.Text = "OFF";
            this.lbl_IOSt_15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_14
            // 
            this.lbl_IOSt_14.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_14.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_14.Location = new System.Drawing.Point(326, 239);
            this.lbl_IOSt_14.Name = "lbl_IOSt_14";
            this.lbl_IOSt_14.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_14.TabIndex = 685;
            this.lbl_IOSt_14.Text = "OFF";
            this.lbl_IOSt_14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_13
            // 
            this.lbl_IOSt_13.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_13.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_13.Location = new System.Drawing.Point(326, 221);
            this.lbl_IOSt_13.Name = "lbl_IOSt_13";
            this.lbl_IOSt_13.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_13.TabIndex = 684;
            this.lbl_IOSt_13.Text = "OFF";
            this.lbl_IOSt_13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label289
            // 
            this.label289.BackColor = System.Drawing.SystemColors.Highlight;
            this.label289.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label289.ForeColor = System.Drawing.Color.White;
            this.label289.Location = new System.Drawing.Point(15, 5);
            this.label289.Name = "label289";
            this.label289.Size = new System.Drawing.Size(48, 16);
            this.label289.TabIndex = 640;
            this.label289.Text = "No 1";
            this.label289.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_12
            // 
            this.lbl_IOSt_12.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_12.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_12.Location = new System.Drawing.Point(326, 203);
            this.lbl_IOSt_12.Name = "lbl_IOSt_12";
            this.lbl_IOSt_12.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_12.TabIndex = 683;
            this.lbl_IOSt_12.Text = "OFF";
            this.lbl_IOSt_12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label241
            // 
            this.label241.BackColor = System.Drawing.SystemColors.Highlight;
            this.label241.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label241.ForeColor = System.Drawing.Color.White;
            this.label241.Location = new System.Drawing.Point(15, 293);
            this.label241.Name = "label241";
            this.label241.Size = new System.Drawing.Size(48, 16);
            this.label241.TabIndex = 607;
            this.label241.Text = "No 17";
            this.label241.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_11
            // 
            this.lbl_IOSt_11.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_11.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_11.Location = new System.Drawing.Point(326, 185);
            this.lbl_IOSt_11.Name = "lbl_IOSt_11";
            this.lbl_IOSt_11.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_11.TabIndex = 682;
            this.lbl_IOSt_11.Text = "OFF";
            this.lbl_IOSt_11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label240
            // 
            this.label240.BackColor = System.Drawing.SystemColors.Highlight;
            this.label240.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label240.ForeColor = System.Drawing.Color.White;
            this.label240.Location = new System.Drawing.Point(15, 311);
            this.label240.Name = "label240";
            this.label240.Size = new System.Drawing.Size(48, 16);
            this.label240.TabIndex = 608;
            this.label240.Text = "No 18";
            this.label240.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_10
            // 
            this.lbl_IOSt_10.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_10.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_10.Location = new System.Drawing.Point(326, 167);
            this.lbl_IOSt_10.Name = "lbl_IOSt_10";
            this.lbl_IOSt_10.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_10.TabIndex = 681;
            this.lbl_IOSt_10.Text = "OFF";
            this.lbl_IOSt_10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label239
            // 
            this.label239.BackColor = System.Drawing.SystemColors.Highlight;
            this.label239.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label239.ForeColor = System.Drawing.Color.White;
            this.label239.Location = new System.Drawing.Point(15, 329);
            this.label239.Name = "label239";
            this.label239.Size = new System.Drawing.Size(48, 16);
            this.label239.TabIndex = 609;
            this.label239.Text = "No 19";
            this.label239.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_9
            // 
            this.lbl_IOSt_9.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_9.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_9.Location = new System.Drawing.Point(326, 149);
            this.lbl_IOSt_9.Name = "lbl_IOSt_9";
            this.lbl_IOSt_9.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_9.TabIndex = 680;
            this.lbl_IOSt_9.Text = "OFF";
            this.lbl_IOSt_9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label238
            // 
            this.label238.BackColor = System.Drawing.SystemColors.Highlight;
            this.label238.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label238.ForeColor = System.Drawing.Color.White;
            this.label238.Location = new System.Drawing.Point(15, 347);
            this.label238.Name = "label238";
            this.label238.Size = new System.Drawing.Size(48, 16);
            this.label238.TabIndex = 610;
            this.label238.Text = "No 20";
            this.label238.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_8
            // 
            this.lbl_IOSt_8.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_8.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_8.Location = new System.Drawing.Point(326, 131);
            this.lbl_IOSt_8.Name = "lbl_IOSt_8";
            this.lbl_IOSt_8.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_8.TabIndex = 679;
            this.lbl_IOSt_8.Text = "OFF";
            this.lbl_IOSt_8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label237
            // 
            this.label237.BackColor = System.Drawing.SystemColors.Highlight;
            this.label237.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label237.ForeColor = System.Drawing.Color.White;
            this.label237.Location = new System.Drawing.Point(15, 365);
            this.label237.Name = "label237";
            this.label237.Size = new System.Drawing.Size(48, 16);
            this.label237.TabIndex = 611;
            this.label237.Text = "No 21";
            this.label237.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_7
            // 
            this.lbl_IOSt_7.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_7.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_7.Location = new System.Drawing.Point(326, 113);
            this.lbl_IOSt_7.Name = "lbl_IOSt_7";
            this.lbl_IOSt_7.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_7.TabIndex = 678;
            this.lbl_IOSt_7.Text = "OFF";
            this.lbl_IOSt_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label236
            // 
            this.label236.BackColor = System.Drawing.SystemColors.Highlight;
            this.label236.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label236.ForeColor = System.Drawing.Color.White;
            this.label236.Location = new System.Drawing.Point(15, 383);
            this.label236.Name = "label236";
            this.label236.Size = new System.Drawing.Size(48, 16);
            this.label236.TabIndex = 612;
            this.label236.Text = "No 22";
            this.label236.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_6
            // 
            this.lbl_IOSt_6.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_6.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_6.Location = new System.Drawing.Point(326, 95);
            this.lbl_IOSt_6.Name = "lbl_IOSt_6";
            this.lbl_IOSt_6.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_6.TabIndex = 677;
            this.lbl_IOSt_6.Text = "OFF";
            this.lbl_IOSt_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label235
            // 
            this.label235.BackColor = System.Drawing.SystemColors.Highlight;
            this.label235.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label235.ForeColor = System.Drawing.Color.White;
            this.label235.Location = new System.Drawing.Point(15, 401);
            this.label235.Name = "label235";
            this.label235.Size = new System.Drawing.Size(48, 16);
            this.label235.TabIndex = 613;
            this.label235.Text = "No 23";
            this.label235.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_5
            // 
            this.lbl_IOSt_5.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_5.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_5.Location = new System.Drawing.Point(326, 77);
            this.lbl_IOSt_5.Name = "lbl_IOSt_5";
            this.lbl_IOSt_5.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_5.TabIndex = 676;
            this.lbl_IOSt_5.Text = "OFF";
            this.lbl_IOSt_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label234
            // 
            this.label234.BackColor = System.Drawing.SystemColors.Highlight;
            this.label234.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label234.ForeColor = System.Drawing.Color.White;
            this.label234.Location = new System.Drawing.Point(15, 419);
            this.label234.Name = "label234";
            this.label234.Size = new System.Drawing.Size(48, 16);
            this.label234.TabIndex = 614;
            this.label234.Text = "No 24";
            this.label234.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_4
            // 
            this.lbl_IOSt_4.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_4.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_4.Location = new System.Drawing.Point(326, 59);
            this.lbl_IOSt_4.Name = "lbl_IOSt_4";
            this.lbl_IOSt_4.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_4.TabIndex = 675;
            this.lbl_IOSt_4.Text = "OFF";
            this.lbl_IOSt_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label233
            // 
            this.label233.BackColor = System.Drawing.SystemColors.Highlight;
            this.label233.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label233.ForeColor = System.Drawing.Color.White;
            this.label233.Location = new System.Drawing.Point(15, 437);
            this.label233.Name = "label233";
            this.label233.Size = new System.Drawing.Size(48, 16);
            this.label233.TabIndex = 615;
            this.label233.Text = "No 25";
            this.label233.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_3
            // 
            this.lbl_IOSt_3.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_3.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_3.Location = new System.Drawing.Point(326, 41);
            this.lbl_IOSt_3.Name = "lbl_IOSt_3";
            this.lbl_IOSt_3.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_3.TabIndex = 674;
            this.lbl_IOSt_3.Text = "OFF";
            this.lbl_IOSt_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label232
            // 
            this.label232.BackColor = System.Drawing.SystemColors.Highlight;
            this.label232.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label232.ForeColor = System.Drawing.Color.White;
            this.label232.Location = new System.Drawing.Point(15, 455);
            this.label232.Name = "label232";
            this.label232.Size = new System.Drawing.Size(48, 16);
            this.label232.TabIndex = 616;
            this.label232.Text = "No 26";
            this.label232.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_2
            // 
            this.lbl_IOSt_2.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_2.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_2.Location = new System.Drawing.Point(326, 23);
            this.lbl_IOSt_2.Name = "lbl_IOSt_2";
            this.lbl_IOSt_2.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_2.TabIndex = 673;
            this.lbl_IOSt_2.Text = "OFF";
            this.lbl_IOSt_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label231
            // 
            this.label231.BackColor = System.Drawing.SystemColors.Highlight;
            this.label231.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label231.ForeColor = System.Drawing.Color.White;
            this.label231.Location = new System.Drawing.Point(15, 473);
            this.label231.Name = "label231";
            this.label231.Size = new System.Drawing.Size(48, 16);
            this.label231.TabIndex = 617;
            this.label231.Text = "No 27";
            this.label231.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_1
            // 
            this.lbl_IOSt_1.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_1.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_1.Location = new System.Drawing.Point(326, 5);
            this.lbl_IOSt_1.Name = "lbl_IOSt_1";
            this.lbl_IOSt_1.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_1.TabIndex = 672;
            this.lbl_IOSt_1.Text = "OFF";
            this.lbl_IOSt_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_16
            // 
            this.lbl_IO_Title_16.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_16.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_16.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_16.Location = new System.Drawing.Point(63, 275);
            this.lbl_IO_Title_16.Name = "lbl_IO_Title_16";
            this.lbl_IO_Title_16.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_16.TabIndex = 671;
            this.lbl_IO_Title_16.Text = "예비";
            this.lbl_IO_Title_16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_15
            // 
            this.lbl_IO_Title_15.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_15.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_15.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_15.Location = new System.Drawing.Point(63, 257);
            this.lbl_IO_Title_15.Name = "lbl_IO_Title_15";
            this.lbl_IO_Title_15.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_15.TabIndex = 670;
            this.lbl_IO_Title_15.Text = "예비";
            this.lbl_IO_Title_15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_14
            // 
            this.lbl_IO_Title_14.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_14.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_14.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_14.Location = new System.Drawing.Point(63, 239);
            this.lbl_IO_Title_14.Name = "lbl_IO_Title_14";
            this.lbl_IO_Title_14.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_14.TabIndex = 669;
            this.lbl_IO_Title_14.Text = "예비";
            this.lbl_IO_Title_14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_IO_Title_14.Click += new System.EventHandler(this.lbl_IO_Title_14_Click);
            // 
            // lbl_IO_Title_13
            // 
            this.lbl_IO_Title_13.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_13.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_13.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_13.Location = new System.Drawing.Point(63, 221);
            this.lbl_IO_Title_13.Name = "lbl_IO_Title_13";
            this.lbl_IO_Title_13.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_13.TabIndex = 668;
            this.lbl_IO_Title_13.Text = "예비";
            this.lbl_IO_Title_13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_17
            // 
            this.lbl_IO_Title_17.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_17.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_17.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_17.Location = new System.Drawing.Point(63, 293);
            this.lbl_IO_Title_17.Name = "lbl_IO_Title_17";
            this.lbl_IO_Title_17.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_17.TabIndex = 618;
            this.lbl_IO_Title_17.Text = "예비";
            this.lbl_IO_Title_17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_12
            // 
            this.lbl_IO_Title_12.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_12.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_12.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_12.Location = new System.Drawing.Point(63, 203);
            this.lbl_IO_Title_12.Name = "lbl_IO_Title_12";
            this.lbl_IO_Title_12.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_12.TabIndex = 667;
            this.lbl_IO_Title_12.Text = "예비";
            this.lbl_IO_Title_12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_18
            // 
            this.lbl_IO_Title_18.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_18.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_18.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_18.Location = new System.Drawing.Point(63, 311);
            this.lbl_IO_Title_18.Name = "lbl_IO_Title_18";
            this.lbl_IO_Title_18.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_18.TabIndex = 619;
            this.lbl_IO_Title_18.Text = "예비";
            this.lbl_IO_Title_18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_11
            // 
            this.lbl_IO_Title_11.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_11.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_11.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_11.Location = new System.Drawing.Point(63, 185);
            this.lbl_IO_Title_11.Name = "lbl_IO_Title_11";
            this.lbl_IO_Title_11.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_11.TabIndex = 666;
            this.lbl_IO_Title_11.Text = "예비";
            this.lbl_IO_Title_11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_19
            // 
            this.lbl_IO_Title_19.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_19.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_19.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_19.Location = new System.Drawing.Point(63, 329);
            this.lbl_IO_Title_19.Name = "lbl_IO_Title_19";
            this.lbl_IO_Title_19.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_19.TabIndex = 620;
            this.lbl_IO_Title_19.Text = "예비";
            this.lbl_IO_Title_19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_10
            // 
            this.lbl_IO_Title_10.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_10.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_10.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_10.Location = new System.Drawing.Point(63, 167);
            this.lbl_IO_Title_10.Name = "lbl_IO_Title_10";
            this.lbl_IO_Title_10.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_10.TabIndex = 665;
            this.lbl_IO_Title_10.Text = "예비";
            this.lbl_IO_Title_10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_20
            // 
            this.lbl_IO_Title_20.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_20.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_20.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_20.Location = new System.Drawing.Point(63, 347);
            this.lbl_IO_Title_20.Name = "lbl_IO_Title_20";
            this.lbl_IO_Title_20.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_20.TabIndex = 621;
            this.lbl_IO_Title_20.Text = "예비";
            this.lbl_IO_Title_20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_9
            // 
            this.lbl_IO_Title_9.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_9.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_9.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_9.Location = new System.Drawing.Point(63, 149);
            this.lbl_IO_Title_9.Name = "lbl_IO_Title_9";
            this.lbl_IO_Title_9.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_9.TabIndex = 664;
            this.lbl_IO_Title_9.Text = "예비";
            this.lbl_IO_Title_9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_21
            // 
            this.lbl_IO_Title_21.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_21.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_21.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_21.Location = new System.Drawing.Point(63, 365);
            this.lbl_IO_Title_21.Name = "lbl_IO_Title_21";
            this.lbl_IO_Title_21.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_21.TabIndex = 622;
            this.lbl_IO_Title_21.Text = "예비";
            this.lbl_IO_Title_21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_8
            // 
            this.lbl_IO_Title_8.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_8.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_8.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_8.Location = new System.Drawing.Point(63, 131);
            this.lbl_IO_Title_8.Name = "lbl_IO_Title_8";
            this.lbl_IO_Title_8.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_8.TabIndex = 663;
            this.lbl_IO_Title_8.Text = "예비";
            this.lbl_IO_Title_8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_22
            // 
            this.lbl_IO_Title_22.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_22.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_22.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_22.Location = new System.Drawing.Point(63, 383);
            this.lbl_IO_Title_22.Name = "lbl_IO_Title_22";
            this.lbl_IO_Title_22.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_22.TabIndex = 623;
            this.lbl_IO_Title_22.Text = "예비";
            this.lbl_IO_Title_22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_7
            // 
            this.lbl_IO_Title_7.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_7.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_7.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_7.Location = new System.Drawing.Point(63, 113);
            this.lbl_IO_Title_7.Name = "lbl_IO_Title_7";
            this.lbl_IO_Title_7.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_7.TabIndex = 662;
            this.lbl_IO_Title_7.Text = "예비";
            this.lbl_IO_Title_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_23
            // 
            this.lbl_IO_Title_23.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_23.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_23.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_23.Location = new System.Drawing.Point(63, 401);
            this.lbl_IO_Title_23.Name = "lbl_IO_Title_23";
            this.lbl_IO_Title_23.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_23.TabIndex = 624;
            this.lbl_IO_Title_23.Text = "예비";
            this.lbl_IO_Title_23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_6
            // 
            this.lbl_IO_Title_6.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_6.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_6.Location = new System.Drawing.Point(63, 95);
            this.lbl_IO_Title_6.Name = "lbl_IO_Title_6";
            this.lbl_IO_Title_6.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_6.TabIndex = 661;
            this.lbl_IO_Title_6.Text = "예비";
            this.lbl_IO_Title_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_24
            // 
            this.lbl_IO_Title_24.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_24.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_24.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_24.Location = new System.Drawing.Point(63, 419);
            this.lbl_IO_Title_24.Name = "lbl_IO_Title_24";
            this.lbl_IO_Title_24.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_24.TabIndex = 625;
            this.lbl_IO_Title_24.Text = "예비";
            this.lbl_IO_Title_24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_5
            // 
            this.lbl_IO_Title_5.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_5.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_5.Location = new System.Drawing.Point(63, 77);
            this.lbl_IO_Title_5.Name = "lbl_IO_Title_5";
            this.lbl_IO_Title_5.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_5.TabIndex = 660;
            this.lbl_IO_Title_5.Text = "예비";
            this.lbl_IO_Title_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_25
            // 
            this.lbl_IO_Title_25.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_25.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_25.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_25.Location = new System.Drawing.Point(63, 437);
            this.lbl_IO_Title_25.Name = "lbl_IO_Title_25";
            this.lbl_IO_Title_25.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_25.TabIndex = 626;
            this.lbl_IO_Title_25.Text = "예비";
            this.lbl_IO_Title_25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_4
            // 
            this.lbl_IO_Title_4.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_4.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_4.Location = new System.Drawing.Point(63, 59);
            this.lbl_IO_Title_4.Name = "lbl_IO_Title_4";
            this.lbl_IO_Title_4.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_4.TabIndex = 659;
            this.lbl_IO_Title_4.Text = "예비";
            this.lbl_IO_Title_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_26
            // 
            this.lbl_IO_Title_26.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_26.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_26.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_26.Location = new System.Drawing.Point(63, 455);
            this.lbl_IO_Title_26.Name = "lbl_IO_Title_26";
            this.lbl_IO_Title_26.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_26.TabIndex = 627;
            this.lbl_IO_Title_26.Text = "예비";
            this.lbl_IO_Title_26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_3
            // 
            this.lbl_IO_Title_3.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_3.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_3.Location = new System.Drawing.Point(63, 41);
            this.lbl_IO_Title_3.Name = "lbl_IO_Title_3";
            this.lbl_IO_Title_3.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_3.TabIndex = 658;
            this.lbl_IO_Title_3.Text = "예비";
            this.lbl_IO_Title_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_27
            // 
            this.lbl_IO_Title_27.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_27.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_27.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_27.Location = new System.Drawing.Point(63, 473);
            this.lbl_IO_Title_27.Name = "lbl_IO_Title_27";
            this.lbl_IO_Title_27.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_27.TabIndex = 628;
            this.lbl_IO_Title_27.Text = "예비";
            this.lbl_IO_Title_27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_2
            // 
            this.lbl_IO_Title_2.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_2.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_2.Location = new System.Drawing.Point(63, 23);
            this.lbl_IO_Title_2.Name = "lbl_IO_Title_2";
            this.lbl_IO_Title_2.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_2.TabIndex = 657;
            this.lbl_IO_Title_2.Text = "예비";
            this.lbl_IO_Title_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IO_Title_1
            // 
            this.lbl_IO_Title_1.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_IO_Title_1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_IO_Title_1.ForeColor = System.Drawing.Color.White;
            this.lbl_IO_Title_1.Location = new System.Drawing.Point(63, 5);
            this.lbl_IO_Title_1.Name = "lbl_IO_Title_1";
            this.lbl_IO_Title_1.Size = new System.Drawing.Size(260, 16);
            this.lbl_IO_Title_1.TabIndex = 656;
            this.lbl_IO_Title_1.Text = "DEVICE_FLT";
            this.lbl_IO_Title_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_IO_Title_1.Click += new System.EventHandler(this.lbl_IO_Title_1_Click);
            // 
            // label274
            // 
            this.label274.BackColor = System.Drawing.SystemColors.Highlight;
            this.label274.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label274.ForeColor = System.Drawing.Color.White;
            this.label274.Location = new System.Drawing.Point(15, 275);
            this.label274.Name = "label274";
            this.label274.Size = new System.Drawing.Size(48, 16);
            this.label274.TabIndex = 655;
            this.label274.Text = "No 16";
            this.label274.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label275
            // 
            this.label275.BackColor = System.Drawing.SystemColors.Highlight;
            this.label275.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label275.ForeColor = System.Drawing.Color.White;
            this.label275.Location = new System.Drawing.Point(15, 257);
            this.label275.Name = "label275";
            this.label275.Size = new System.Drawing.Size(48, 16);
            this.label275.TabIndex = 654;
            this.label275.Text = "No 15";
            this.label275.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label276
            // 
            this.label276.BackColor = System.Drawing.SystemColors.Highlight;
            this.label276.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label276.ForeColor = System.Drawing.Color.White;
            this.label276.Location = new System.Drawing.Point(15, 239);
            this.label276.Name = "label276";
            this.label276.Size = new System.Drawing.Size(48, 16);
            this.label276.TabIndex = 653;
            this.label276.Text = "No 14";
            this.label276.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_17
            // 
            this.lbl_IOSt_17.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_17.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_17.Location = new System.Drawing.Point(326, 293);
            this.lbl_IOSt_17.Name = "lbl_IOSt_17";
            this.lbl_IOSt_17.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_17.TabIndex = 629;
            this.lbl_IOSt_17.Text = "OFF";
            this.lbl_IOSt_17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label277
            // 
            this.label277.BackColor = System.Drawing.SystemColors.Highlight;
            this.label277.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label277.ForeColor = System.Drawing.Color.White;
            this.label277.Location = new System.Drawing.Point(15, 221);
            this.label277.Name = "label277";
            this.label277.Size = new System.Drawing.Size(48, 16);
            this.label277.TabIndex = 652;
            this.label277.Text = "No 13";
            this.label277.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_18
            // 
            this.lbl_IOSt_18.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_18.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_18.Location = new System.Drawing.Point(326, 311);
            this.lbl_IOSt_18.Name = "lbl_IOSt_18";
            this.lbl_IOSt_18.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_18.TabIndex = 630;
            this.lbl_IOSt_18.Text = "OFF";
            this.lbl_IOSt_18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label278
            // 
            this.label278.BackColor = System.Drawing.SystemColors.Highlight;
            this.label278.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label278.ForeColor = System.Drawing.Color.White;
            this.label278.Location = new System.Drawing.Point(15, 203);
            this.label278.Name = "label278";
            this.label278.Size = new System.Drawing.Size(48, 16);
            this.label278.TabIndex = 651;
            this.label278.Text = "No 12";
            this.label278.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_19
            // 
            this.lbl_IOSt_19.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_19.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_19.Location = new System.Drawing.Point(326, 329);
            this.lbl_IOSt_19.Name = "lbl_IOSt_19";
            this.lbl_IOSt_19.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_19.TabIndex = 631;
            this.lbl_IOSt_19.Text = "OFF";
            this.lbl_IOSt_19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label279
            // 
            this.label279.BackColor = System.Drawing.SystemColors.Highlight;
            this.label279.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label279.ForeColor = System.Drawing.Color.White;
            this.label279.Location = new System.Drawing.Point(15, 185);
            this.label279.Name = "label279";
            this.label279.Size = new System.Drawing.Size(48, 16);
            this.label279.TabIndex = 650;
            this.label279.Text = "No 11";
            this.label279.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_20
            // 
            this.lbl_IOSt_20.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_20.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_20.Location = new System.Drawing.Point(326, 347);
            this.lbl_IOSt_20.Name = "lbl_IOSt_20";
            this.lbl_IOSt_20.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_20.TabIndex = 632;
            this.lbl_IOSt_20.Text = "OFF";
            this.lbl_IOSt_20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label280
            // 
            this.label280.BackColor = System.Drawing.SystemColors.Highlight;
            this.label280.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label280.ForeColor = System.Drawing.Color.White;
            this.label280.Location = new System.Drawing.Point(15, 167);
            this.label280.Name = "label280";
            this.label280.Size = new System.Drawing.Size(48, 16);
            this.label280.TabIndex = 649;
            this.label280.Text = "No 10";
            this.label280.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_21
            // 
            this.lbl_IOSt_21.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_21.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_21.Location = new System.Drawing.Point(326, 365);
            this.lbl_IOSt_21.Name = "lbl_IOSt_21";
            this.lbl_IOSt_21.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_21.TabIndex = 633;
            this.lbl_IOSt_21.Text = "OFF";
            this.lbl_IOSt_21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label281
            // 
            this.label281.BackColor = System.Drawing.SystemColors.Highlight;
            this.label281.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label281.ForeColor = System.Drawing.Color.White;
            this.label281.Location = new System.Drawing.Point(15, 149);
            this.label281.Name = "label281";
            this.label281.Size = new System.Drawing.Size(48, 16);
            this.label281.TabIndex = 648;
            this.label281.Text = "No 9";
            this.label281.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_22
            // 
            this.lbl_IOSt_22.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_22.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_22.Location = new System.Drawing.Point(326, 383);
            this.lbl_IOSt_22.Name = "lbl_IOSt_22";
            this.lbl_IOSt_22.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_22.TabIndex = 634;
            this.lbl_IOSt_22.Text = "OFF";
            this.lbl_IOSt_22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label282
            // 
            this.label282.BackColor = System.Drawing.SystemColors.Highlight;
            this.label282.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label282.ForeColor = System.Drawing.Color.White;
            this.label282.Location = new System.Drawing.Point(15, 131);
            this.label282.Name = "label282";
            this.label282.Size = new System.Drawing.Size(48, 16);
            this.label282.TabIndex = 647;
            this.label282.Text = "No 8";
            this.label282.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_23
            // 
            this.lbl_IOSt_23.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_23.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_23.Location = new System.Drawing.Point(326, 401);
            this.lbl_IOSt_23.Name = "lbl_IOSt_23";
            this.lbl_IOSt_23.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_23.TabIndex = 635;
            this.lbl_IOSt_23.Text = "OFF";
            this.lbl_IOSt_23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label283
            // 
            this.label283.BackColor = System.Drawing.SystemColors.Highlight;
            this.label283.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label283.ForeColor = System.Drawing.Color.White;
            this.label283.Location = new System.Drawing.Point(15, 113);
            this.label283.Name = "label283";
            this.label283.Size = new System.Drawing.Size(48, 16);
            this.label283.TabIndex = 646;
            this.label283.Text = "No 7";
            this.label283.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_24
            // 
            this.lbl_IOSt_24.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_24.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_24.Location = new System.Drawing.Point(326, 419);
            this.lbl_IOSt_24.Name = "lbl_IOSt_24";
            this.lbl_IOSt_24.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_24.TabIndex = 636;
            this.lbl_IOSt_24.Text = "OFF";
            this.lbl_IOSt_24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label284
            // 
            this.label284.BackColor = System.Drawing.SystemColors.Highlight;
            this.label284.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label284.ForeColor = System.Drawing.Color.White;
            this.label284.Location = new System.Drawing.Point(15, 95);
            this.label284.Name = "label284";
            this.label284.Size = new System.Drawing.Size(48, 16);
            this.label284.TabIndex = 645;
            this.label284.Text = "No 6";
            this.label284.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_25
            // 
            this.lbl_IOSt_25.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_25.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_25.Location = new System.Drawing.Point(326, 437);
            this.lbl_IOSt_25.Name = "lbl_IOSt_25";
            this.lbl_IOSt_25.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_25.TabIndex = 637;
            this.lbl_IOSt_25.Text = "OFF";
            this.lbl_IOSt_25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label285
            // 
            this.label285.BackColor = System.Drawing.SystemColors.Highlight;
            this.label285.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label285.ForeColor = System.Drawing.Color.White;
            this.label285.Location = new System.Drawing.Point(15, 77);
            this.label285.Name = "label285";
            this.label285.Size = new System.Drawing.Size(48, 16);
            this.label285.TabIndex = 644;
            this.label285.Text = "No 5";
            this.label285.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_26
            // 
            this.lbl_IOSt_26.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_26.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_26.Location = new System.Drawing.Point(326, 455);
            this.lbl_IOSt_26.Name = "lbl_IOSt_26";
            this.lbl_IOSt_26.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_26.TabIndex = 638;
            this.lbl_IOSt_26.Text = "OFF";
            this.lbl_IOSt_26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label286
            // 
            this.label286.BackColor = System.Drawing.SystemColors.Highlight;
            this.label286.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label286.ForeColor = System.Drawing.Color.White;
            this.label286.Location = new System.Drawing.Point(15, 59);
            this.label286.Name = "label286";
            this.label286.Size = new System.Drawing.Size(48, 16);
            this.label286.TabIndex = 643;
            this.label286.Text = "No 4";
            this.label286.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IOSt_27
            // 
            this.lbl_IOSt_27.BackColor = System.Drawing.Color.Silver;
            this.lbl_IOSt_27.ForeColor = System.Drawing.Color.Black;
            this.lbl_IOSt_27.Location = new System.Drawing.Point(326, 473);
            this.lbl_IOSt_27.Name = "lbl_IOSt_27";
            this.lbl_IOSt_27.Size = new System.Drawing.Size(42, 16);
            this.lbl_IOSt_27.TabIndex = 639;
            this.lbl_IOSt_27.Text = "OFF";
            this.lbl_IOSt_27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label287
            // 
            this.label287.BackColor = System.Drawing.SystemColors.Highlight;
            this.label287.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label287.ForeColor = System.Drawing.Color.White;
            this.label287.Location = new System.Drawing.Point(15, 41);
            this.label287.Name = "label287";
            this.label287.Size = new System.Drawing.Size(48, 16);
            this.label287.TabIndex = 642;
            this.label287.Text = "No 3";
            this.label287.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label288
            // 
            this.label288.BackColor = System.Drawing.SystemColors.Highlight;
            this.label288.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label288.ForeColor = System.Drawing.Color.White;
            this.label288.Location = new System.Drawing.Point(15, 23);
            this.label288.Name = "label288";
            this.label288.Size = new System.Drawing.Size(48, 16);
            this.label288.TabIndex = 641;
            this.label288.Text = "No 2";
            this.label288.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.gb_Outputgroup);
            this.panel4.Controls.Add(this.gb_Inputgroup);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(703, 45);
            this.panel4.TabIndex = 607;
            // 
            // gb_Outputgroup
            // 
            this.gb_Outputgroup.Controls.Add(this.rb_DIO_DigitalOut_3);
            this.gb_Outputgroup.Controls.Add(this.rb_DIO_DigitalOut_2);
            this.gb_Outputgroup.Controls.Add(this.rb_DIO_DigitalOut_1);
            this.gb_Outputgroup.Location = new System.Drawing.Point(374, 4);
            this.gb_Outputgroup.Name = "gb_Outputgroup";
            this.gb_Outputgroup.Size = new System.Drawing.Size(326, 35);
            this.gb_Outputgroup.TabIndex = 318;
            this.gb_Outputgroup.TabStop = false;
            this.gb_Outputgroup.Text = "Output";
            // 
            // rb_DIO_DigitalOut_3
            // 
            this.rb_DIO_DigitalOut_3.AutoSize = true;
            this.rb_DIO_DigitalOut_3.Location = new System.Drawing.Point(215, 14);
            this.rb_DIO_DigitalOut_3.Name = "rb_DIO_DigitalOut_3";
            this.rb_DIO_DigitalOut_3.Size = new System.Drawing.Size(90, 16);
            this.rb_DIO_DigitalOut_3.TabIndex = 319;
            this.rb_DIO_DigitalOut_3.Tag = "4";
            this.rb_DIO_DigitalOut_3.Text = "Digital Out 3";
            this.rb_DIO_DigitalOut_3.UseVisualStyleBackColor = true;
            this.rb_DIO_DigitalOut_3.CheckedChanged += new System.EventHandler(this.rb_DIO_DigitalOut_3_CheckedChanged);
            this.rb_DIO_DigitalOut_3.Click += new System.EventHandler(this.rb_DIO_DigitalIn_1_Click);
            // 
            // rb_DIO_DigitalOut_2
            // 
            this.rb_DIO_DigitalOut_2.AutoSize = true;
            this.rb_DIO_DigitalOut_2.Location = new System.Drawing.Point(117, 15);
            this.rb_DIO_DigitalOut_2.Name = "rb_DIO_DigitalOut_2";
            this.rb_DIO_DigitalOut_2.Size = new System.Drawing.Size(90, 16);
            this.rb_DIO_DigitalOut_2.TabIndex = 23;
            this.rb_DIO_DigitalOut_2.Tag = "4";
            this.rb_DIO_DigitalOut_2.Text = "Digital Out 2";
            this.rb_DIO_DigitalOut_2.UseVisualStyleBackColor = true;
            this.rb_DIO_DigitalOut_2.Click += new System.EventHandler(this.rb_DIO_DigitalIn_1_Click);
            // 
            // rb_DIO_DigitalOut_1
            // 
            this.rb_DIO_DigitalOut_1.AutoSize = true;
            this.rb_DIO_DigitalOut_1.Checked = true;
            this.rb_DIO_DigitalOut_1.Location = new System.Drawing.Point(17, 14);
            this.rb_DIO_DigitalOut_1.Name = "rb_DIO_DigitalOut_1";
            this.rb_DIO_DigitalOut_1.Size = new System.Drawing.Size(90, 16);
            this.rb_DIO_DigitalOut_1.TabIndex = 9;
            this.rb_DIO_DigitalOut_1.TabStop = true;
            this.rb_DIO_DigitalOut_1.Tag = "3";
            this.rb_DIO_DigitalOut_1.Text = "Digital Out 1";
            this.rb_DIO_DigitalOut_1.UseVisualStyleBackColor = true;
            this.rb_DIO_DigitalOut_1.Click += new System.EventHandler(this.rb_DIO_DigitalIn_1_Click);
            // 
            // gb_Inputgroup
            // 
            this.gb_Inputgroup.Controls.Add(this.rb_DIO_DigitalIn_3);
            this.gb_Inputgroup.Controls.Add(this.rb_DIO_DigitalIn_2);
            this.gb_Inputgroup.Controls.Add(this.rb_DIO_DigitalIn_1);
            this.gb_Inputgroup.Location = new System.Drawing.Point(13, 4);
            this.gb_Inputgroup.Name = "gb_Inputgroup";
            this.gb_Inputgroup.Size = new System.Drawing.Size(310, 35);
            this.gb_Inputgroup.TabIndex = 317;
            this.gb_Inputgroup.TabStop = false;
            this.gb_Inputgroup.Text = "Input";
            // 
            // rb_DIO_DigitalIn_3
            // 
            this.rb_DIO_DigitalIn_3.AutoSize = true;
            this.rb_DIO_DigitalIn_3.Location = new System.Drawing.Point(220, 15);
            this.rb_DIO_DigitalIn_3.Name = "rb_DIO_DigitalIn_3";
            this.rb_DIO_DigitalIn_3.Size = new System.Drawing.Size(81, 16);
            this.rb_DIO_DigitalIn_3.TabIndex = 22;
            this.rb_DIO_DigitalIn_3.Tag = "3";
            this.rb_DIO_DigitalIn_3.Text = "Digital In 3";
            this.rb_DIO_DigitalIn_3.UseVisualStyleBackColor = true;
            this.rb_DIO_DigitalIn_3.Click += new System.EventHandler(this.rb_DIO_DigitalIn_1_Click);
            // 
            // rb_DIO_DigitalIn_2
            // 
            this.rb_DIO_DigitalIn_2.AutoSize = true;
            this.rb_DIO_DigitalIn_2.Location = new System.Drawing.Point(110, 14);
            this.rb_DIO_DigitalIn_2.Name = "rb_DIO_DigitalIn_2";
            this.rb_DIO_DigitalIn_2.Size = new System.Drawing.Size(81, 16);
            this.rb_DIO_DigitalIn_2.TabIndex = 21;
            this.rb_DIO_DigitalIn_2.Tag = "2";
            this.rb_DIO_DigitalIn_2.Text = "Digital In 2";
            this.rb_DIO_DigitalIn_2.UseVisualStyleBackColor = true;
            this.rb_DIO_DigitalIn_2.Click += new System.EventHandler(this.rb_DIO_DigitalIn_1_Click);
            // 
            // rb_DIO_DigitalIn_1
            // 
            this.rb_DIO_DigitalIn_1.AutoSize = true;
            this.rb_DIO_DigitalIn_1.Checked = true;
            this.rb_DIO_DigitalIn_1.Location = new System.Drawing.Point(8, 14);
            this.rb_DIO_DigitalIn_1.Name = "rb_DIO_DigitalIn_1";
            this.rb_DIO_DigitalIn_1.Size = new System.Drawing.Size(81, 16);
            this.rb_DIO_DigitalIn_1.TabIndex = 0;
            this.rb_DIO_DigitalIn_1.TabStop = true;
            this.rb_DIO_DigitalIn_1.Tag = "1";
            this.rb_DIO_DigitalIn_1.Text = "Digital In 1";
            this.rb_DIO_DigitalIn_1.UseVisualStyleBackColor = true;
            this.rb_DIO_DigitalIn_1.CheckedChanged += new System.EventHandler(this.rb_DIO_DigitalIn_1_CheckedChanged);
            this.rb_DIO_DigitalIn_1.Click += new System.EventHandler(this.rb_DIO_DigitalIn_1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(327, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(41, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(418, 739);
            this.tabControl1.TabIndex = 57;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.lbl_DriveRearAreaInfo_RegionSt_0);
            this.tabPage1.Controls.Add(this.lbl_DriveRearAreaInfo_RegionSt_1);
            this.tabPage1.Controls.Add(this.lbl_DriveRearAreaInfo_RegionSt_2);
            this.tabPage1.Controls.Add(this.lbl_DriveSt2_2);
            this.tabPage1.Controls.Add(this.label82);
            this.tabPage1.Controls.Add(this.btnBarCodeErrCountInit);
            this.tabPage1.Controls.Add(this.lbl_Drive_CurrentStation_Feed2);
            this.tabPage1.Controls.Add(this.lbl_Drive_CurrentStation_Feed1);
            this.tabPage1.Controls.Add(this.label64);
            this.tabPage1.Controls.Add(this.lbl_DriveBarcodeErrCount);
            this.tabPage1.Controls.Add(this.label44);
            this.tabPage1.Controls.Add(this.lbl_DriveFrontAreaInfo_RegionSt_0);
            this.tabPage1.Controls.Add(this.lbl_DriveFrontAreaInfo_RegionSt_1);
            this.tabPage1.Controls.Add(this.lbl_DriveFrontAreaInfo_RegionSt_2);
            this.tabPage1.Controls.Add(this.label59);
            this.tabPage1.Controls.Add(this.lbl_Feed2_DestSpeed);
            this.tabPage1.Controls.Add(this.lbl_Feed1_DestSpeed);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.lbl_Drive_DestSpeed);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.lbl_Feed2_Speed);
            this.tabPage1.Controls.Add(this.lbl_Feed1_Speed);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label34);
            this.tabPage1.Controls.Add(this.label31);
            this.tabPage1.Controls.Add(this.lbl_Drive_CurrentPos_Feed2);
            this.tabPage1.Controls.Add(this.lbl_Drive_CurrentPos_Feed1);
            this.tabPage1.Controls.Add(this.label45);
            this.tabPage1.Controls.Add(this.lbl_Feed2St2_1);
            this.tabPage1.Controls.Add(this.lbl_Feed1St2_1);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.lbl_Feed2St1_5);
            this.tabPage1.Controls.Add(this.lbl_Feed1St1_5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.lbl_Feed2St1_6);
            this.tabPage1.Controls.Add(this.lbl_Feed1St1_6);
            this.tabPage1.Controls.Add(this.label40);
            this.tabPage1.Controls.Add(this.lbl_Feed2St1_4);
            this.tabPage1.Controls.Add(this.lbl_Feed1St1_4);
            this.tabPage1.Controls.Add(this.label38);
            this.tabPage1.Controls.Add(this.label57);
            this.tabPage1.Controls.Add(this.label58);
            this.tabPage1.Controls.Add(this.lbl_Feed2St2_0);
            this.tabPage1.Controls.Add(this.lbl_Feed2St1_0);
            this.tabPage1.Controls.Add(this.lbl_Feed2St1_1);
            this.tabPage1.Controls.Add(this.lbl_Feed2St1_2);
            this.tabPage1.Controls.Add(this.lbl_Feed2St1_3);
            this.tabPage1.Controls.Add(this.lbl_Feed2_Dest);
            this.tabPage1.Controls.Add(this.lbl_Feed2_Pos);
            this.tabPage1.Controls.Add(this.lbl_Feed1St2_0);
            this.tabPage1.Controls.Add(this.lbl_Feed1St1_0);
            this.tabPage1.Controls.Add(this.lbl_Feed1St1_1);
            this.tabPage1.Controls.Add(this.lbl_Feed1St1_2);
            this.tabPage1.Controls.Add(this.lbl_Feed1St1_3);
            this.tabPage1.Controls.Add(this.lbl_Feed1_Dest);
            this.tabPage1.Controls.Add(this.lbl_Feed1_Pos);
            this.tabPage1.Controls.Add(this.label170);
            this.tabPage1.Controls.Add(this.label171);
            this.tabPage1.Controls.Add(this.label172);
            this.tabPage1.Controls.Add(this.label173);
            this.tabPage1.Controls.Add(this.label174);
            this.tabPage1.Controls.Add(this.label177);
            this.tabPage1.Controls.Add(this.label178);
            this.tabPage1.Controls.Add(this.lbl_DriveSt2_1);
            this.tabPage1.Controls.Add(this.label63);
            this.tabPage1.Controls.Add(this.lbl_Dev_Maintance);
            this.tabPage1.Controls.Add(this.lbl_Dev_Home);
            this.tabPage1.Controls.Add(this.label36);
            this.tabPage1.Controls.Add(this.label50);
            this.tabPage1.Controls.Add(this.lbl_Drive_Destination);
            this.tabPage1.Controls.Add(this.lbl_Drive_Speed);
            this.tabPage1.Controls.Add(this.lbl_Drive_Position);
            this.tabPage1.Controls.Add(this.label53);
            this.tabPage1.Controls.Add(this.label131);
            this.tabPage1.Controls.Add(this.label132);
            this.tabPage1.Controls.Add(this.lbl_DriveSt2_0);
            this.tabPage1.Controls.Add(this.lbl_DriveSt1_0);
            this.tabPage1.Controls.Add(this.lbl_DriveSt1_1);
            this.tabPage1.Controls.Add(this.lbl_DriveSt1_2);
            this.tabPage1.Controls.Add(this.lbl_DriveSt1_3);
            this.tabPage1.Controls.Add(this.lbl_DriveSt1_4);
            this.tabPage1.Controls.Add(this.lbl_CanWork_StationIndex);
            this.tabPage1.Controls.Add(this.lbl_DriveSt1_6);
            this.tabPage1.Controls.Add(this.lbl_DriveSt1_5);
            this.tabPage1.Controls.Add(this.label46);
            this.tabPage1.Controls.Add(this.label48);
            this.tabPage1.Controls.Add(this.label49);
            this.tabPage1.Controls.Add(this.label117);
            this.tabPage1.Controls.Add(this.label118);
            this.tabPage1.Controls.Add(this.label47);
            this.tabPage1.Controls.Add(this.label113);
            this.tabPage1.Controls.Add(this.label43);
            this.tabPage1.Controls.Add(this.label22);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage1.Size = new System.Drawing.Size(410, 713);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "주행 / 피딩";
            // 
            // lbl_DriveRearAreaInfo_RegionSt_0
            // 
            this.lbl_DriveRearAreaInfo_RegionSt_0.BackColor = System.Drawing.Color.White;
            this.lbl_DriveRearAreaInfo_RegionSt_0.Location = new System.Drawing.Point(254, 265);
            this.lbl_DriveRearAreaInfo_RegionSt_0.Name = "lbl_DriveRearAreaInfo_RegionSt_0";
            this.lbl_DriveRearAreaInfo_RegionSt_0.Size = new System.Drawing.Size(94, 16);
            this.lbl_DriveRearAreaInfo_RegionSt_0.TabIndex = 780;
            this.lbl_DriveRearAreaInfo_RegionSt_0.Text = "후방 Region 1";
            this.lbl_DriveRearAreaInfo_RegionSt_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DriveRearAreaInfo_RegionSt_1
            // 
            this.lbl_DriveRearAreaInfo_RegionSt_1.BackColor = System.Drawing.Color.White;
            this.lbl_DriveRearAreaInfo_RegionSt_1.Location = new System.Drawing.Point(254, 283);
            this.lbl_DriveRearAreaInfo_RegionSt_1.Name = "lbl_DriveRearAreaInfo_RegionSt_1";
            this.lbl_DriveRearAreaInfo_RegionSt_1.Size = new System.Drawing.Size(94, 16);
            this.lbl_DriveRearAreaInfo_RegionSt_1.TabIndex = 779;
            this.lbl_DriveRearAreaInfo_RegionSt_1.Text = "후방 Region 2";
            this.lbl_DriveRearAreaInfo_RegionSt_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DriveRearAreaInfo_RegionSt_2
            // 
            this.lbl_DriveRearAreaInfo_RegionSt_2.BackColor = System.Drawing.Color.White;
            this.lbl_DriveRearAreaInfo_RegionSt_2.Location = new System.Drawing.Point(254, 301);
            this.lbl_DriveRearAreaInfo_RegionSt_2.Name = "lbl_DriveRearAreaInfo_RegionSt_2";
            this.lbl_DriveRearAreaInfo_RegionSt_2.Size = new System.Drawing.Size(94, 16);
            this.lbl_DriveRearAreaInfo_RegionSt_2.TabIndex = 778;
            this.lbl_DriveRearAreaInfo_RegionSt_2.Text = "후방 Region 3";
            this.lbl_DriveRearAreaInfo_RegionSt_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DriveSt2_2
            // 
            this.lbl_DriveSt2_2.BackColor = System.Drawing.Color.White;
            this.lbl_DriveSt2_2.Location = new System.Drawing.Point(158, 193);
            this.lbl_DriveSt2_2.Name = "lbl_DriveSt2_2";
            this.lbl_DriveSt2_2.Size = new System.Drawing.Size(125, 16);
            this.lbl_DriveSt2_2.TabIndex = 777;
            this.lbl_DriveSt2_2.Text = "미접속";
            this.lbl_DriveSt2_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label82
            // 
            this.label82.BackColor = System.Drawing.SystemColors.Highlight;
            this.label82.ForeColor = System.Drawing.Color.White;
            this.label82.Location = new System.Drawing.Point(20, 193);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(135, 16);
            this.label82.TabIndex = 776;
            this.label82.Text = "원점확인";
            this.label82.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBarCodeErrCountInit
            // 
            this.btnBarCodeErrCountInit.Location = new System.Drawing.Point(288, 386);
            this.btnBarCodeErrCountInit.Name = "btnBarCodeErrCountInit";
            this.btnBarCodeErrCountInit.Size = new System.Drawing.Size(115, 23);
            this.btnBarCodeErrCountInit.TabIndex = 775;
            this.btnBarCodeErrCountInit.Text = "에러횟수 초기화";
            this.btnBarCodeErrCountInit.UseVisualStyleBackColor = true;
            this.btnBarCodeErrCountInit.Click += new System.EventHandler(this.btnBarCodeErrCountInit_Click);
            // 
            // lbl_Drive_CurrentStation_Feed2
            // 
            this.lbl_Drive_CurrentStation_Feed2.BackColor = System.Drawing.Color.White;
            this.lbl_Drive_CurrentStation_Feed2.Location = new System.Drawing.Point(263, 438);
            this.lbl_Drive_CurrentStation_Feed2.Name = "lbl_Drive_CurrentStation_Feed2";
            this.lbl_Drive_CurrentStation_Feed2.Size = new System.Drawing.Size(125, 16);
            this.lbl_Drive_CurrentStation_Feed2.TabIndex = 249;
            this.lbl_Drive_CurrentStation_Feed2.Text = "X";
            this.lbl_Drive_CurrentStation_Feed2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Drive_CurrentStation_Feed1
            // 
            this.lbl_Drive_CurrentStation_Feed1.BackColor = System.Drawing.Color.White;
            this.lbl_Drive_CurrentStation_Feed1.Location = new System.Drawing.Point(134, 438);
            this.lbl_Drive_CurrentStation_Feed1.Name = "lbl_Drive_CurrentStation_Feed1";
            this.lbl_Drive_CurrentStation_Feed1.Size = new System.Drawing.Size(125, 16);
            this.lbl_Drive_CurrentStation_Feed1.TabIndex = 248;
            this.lbl_Drive_CurrentStation_Feed1.Text = "X";
            this.lbl_Drive_CurrentStation_Feed1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label64
            // 
            this.label64.BackColor = System.Drawing.SystemColors.Highlight;
            this.label64.ForeColor = System.Drawing.Color.White;
            this.label64.Location = new System.Drawing.Point(20, 438);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(110, 16);
            this.label64.TabIndex = 247;
            this.label64.Text = "Station 정위치";
            this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DriveBarcodeErrCount
            // 
            this.lbl_DriveBarcodeErrCount.BackColor = System.Drawing.Color.White;
            this.lbl_DriveBarcodeErrCount.Location = new System.Drawing.Point(158, 391);
            this.lbl_DriveBarcodeErrCount.Name = "lbl_DriveBarcodeErrCount";
            this.lbl_DriveBarcodeErrCount.Size = new System.Drawing.Size(125, 16);
            this.lbl_DriveBarcodeErrCount.TabIndex = 246;
            this.lbl_DriveBarcodeErrCount.Text = "0";
            this.lbl_DriveBarcodeErrCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.SystemColors.Highlight;
            this.label44.ForeColor = System.Drawing.Color.White;
            this.label44.Location = new System.Drawing.Point(20, 391);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(135, 16);
            this.label44.TabIndex = 245;
            this.label44.Text = "바코드 에러횟수";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DriveFrontAreaInfo_RegionSt_0
            // 
            this.lbl_DriveFrontAreaInfo_RegionSt_0.BackColor = System.Drawing.Color.White;
            this.lbl_DriveFrontAreaInfo_RegionSt_0.Location = new System.Drawing.Point(158, 265);
            this.lbl_DriveFrontAreaInfo_RegionSt_0.Name = "lbl_DriveFrontAreaInfo_RegionSt_0";
            this.lbl_DriveFrontAreaInfo_RegionSt_0.Size = new System.Drawing.Size(94, 16);
            this.lbl_DriveFrontAreaInfo_RegionSt_0.TabIndex = 244;
            this.lbl_DriveFrontAreaInfo_RegionSt_0.Text = "전방 Region 1";
            this.lbl_DriveFrontAreaInfo_RegionSt_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DriveFrontAreaInfo_RegionSt_1
            // 
            this.lbl_DriveFrontAreaInfo_RegionSt_1.BackColor = System.Drawing.Color.White;
            this.lbl_DriveFrontAreaInfo_RegionSt_1.Location = new System.Drawing.Point(158, 283);
            this.lbl_DriveFrontAreaInfo_RegionSt_1.Name = "lbl_DriveFrontAreaInfo_RegionSt_1";
            this.lbl_DriveFrontAreaInfo_RegionSt_1.Size = new System.Drawing.Size(94, 16);
            this.lbl_DriveFrontAreaInfo_RegionSt_1.TabIndex = 243;
            this.lbl_DriveFrontAreaInfo_RegionSt_1.Text = "전방 Region 2";
            this.lbl_DriveFrontAreaInfo_RegionSt_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DriveFrontAreaInfo_RegionSt_2
            // 
            this.lbl_DriveFrontAreaInfo_RegionSt_2.BackColor = System.Drawing.Color.White;
            this.lbl_DriveFrontAreaInfo_RegionSt_2.Location = new System.Drawing.Point(158, 301);
            this.lbl_DriveFrontAreaInfo_RegionSt_2.Name = "lbl_DriveFrontAreaInfo_RegionSt_2";
            this.lbl_DriveFrontAreaInfo_RegionSt_2.Size = new System.Drawing.Size(94, 16);
            this.lbl_DriveFrontAreaInfo_RegionSt_2.TabIndex = 242;
            this.lbl_DriveFrontAreaInfo_RegionSt_2.Text = "전방 Region 3";
            this.lbl_DriveFrontAreaInfo_RegionSt_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.SystemColors.Highlight;
            this.label59.ForeColor = System.Drawing.Color.White;
            this.label59.Location = new System.Drawing.Point(20, 265);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(135, 52);
            this.label59.TabIndex = 241;
            this.label59.Text = "감지영역 감지상태";
            this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed2_DestSpeed
            // 
            this.lbl_Feed2_DestSpeed.BackColor = System.Drawing.Color.White;
            this.lbl_Feed2_DestSpeed.Location = new System.Drawing.Point(263, 690);
            this.lbl_Feed2_DestSpeed.Name = "lbl_Feed2_DestSpeed";
            this.lbl_Feed2_DestSpeed.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed2_DestSpeed.TabIndex = 233;
            this.lbl_Feed2_DestSpeed.Text = "0";
            this.lbl_Feed2_DestSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed1_DestSpeed
            // 
            this.lbl_Feed1_DestSpeed.BackColor = System.Drawing.Color.White;
            this.lbl_Feed1_DestSpeed.Location = new System.Drawing.Point(134, 690);
            this.lbl_Feed1_DestSpeed.Name = "lbl_Feed1_DestSpeed";
            this.lbl_Feed1_DestSpeed.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed1_DestSpeed.TabIndex = 232;
            this.lbl_Feed1_DestSpeed.Text = "0";
            this.lbl_Feed1_DestSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.Highlight;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(20, 690);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 16);
            this.label10.TabIndex = 231;
            this.label10.Text = "목표속도 (m/min)";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Drive_DestSpeed
            // 
            this.lbl_Drive_DestSpeed.BackColor = System.Drawing.Color.White;
            this.lbl_Drive_DestSpeed.Location = new System.Drawing.Point(158, 373);
            this.lbl_Drive_DestSpeed.Name = "lbl_Drive_DestSpeed";
            this.lbl_Drive_DestSpeed.Size = new System.Drawing.Size(125, 16);
            this.lbl_Drive_DestSpeed.TabIndex = 230;
            this.lbl_Drive_DestSpeed.Text = "0";
            this.lbl_Drive_DestSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Highlight;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(20, 373);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 16);
            this.label5.TabIndex = 229;
            this.label5.Text = "목표속도 (m/min)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed2_Speed
            // 
            this.lbl_Feed2_Speed.BackColor = System.Drawing.Color.White;
            this.lbl_Feed2_Speed.Location = new System.Drawing.Point(263, 672);
            this.lbl_Feed2_Speed.Name = "lbl_Feed2_Speed";
            this.lbl_Feed2_Speed.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed2_Speed.TabIndex = 228;
            this.lbl_Feed2_Speed.Text = "0";
            this.lbl_Feed2_Speed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed1_Speed
            // 
            this.lbl_Feed1_Speed.BackColor = System.Drawing.Color.White;
            this.lbl_Feed1_Speed.Location = new System.Drawing.Point(134, 672);
            this.lbl_Feed1_Speed.Name = "lbl_Feed1_Speed";
            this.lbl_Feed1_Speed.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed1_Speed.TabIndex = 227;
            this.lbl_Feed1_Speed.Text = "0";
            this.lbl_Feed1_Speed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Highlight;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(20, 672);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 226;
            this.label2.Text = "현재속도 (m/min)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.SystemColors.Highlight;
            this.label34.ForeColor = System.Drawing.Color.White;
            this.label34.Location = new System.Drawing.Point(20, 418);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(110, 18);
            this.label34.TabIndex = 225;
            this.label34.Text = "Feeding";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.SystemColors.Highlight;
            this.label31.ForeColor = System.Drawing.Color.White;
            this.label31.Location = new System.Drawing.Point(20, 7);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(263, 22);
            this.label31.TabIndex = 224;
            this.label31.Text = "주행";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Drive_CurrentPos_Feed2
            // 
            this.lbl_Drive_CurrentPos_Feed2.BackColor = System.Drawing.Color.White;
            this.lbl_Drive_CurrentPos_Feed2.Location = new System.Drawing.Point(263, 456);
            this.lbl_Drive_CurrentPos_Feed2.Name = "lbl_Drive_CurrentPos_Feed2";
            this.lbl_Drive_CurrentPos_Feed2.Size = new System.Drawing.Size(125, 16);
            this.lbl_Drive_CurrentPos_Feed2.TabIndex = 223;
            this.lbl_Drive_CurrentPos_Feed2.Text = "X";
            this.lbl_Drive_CurrentPos_Feed2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Drive_CurrentPos_Feed1
            // 
            this.lbl_Drive_CurrentPos_Feed1.BackColor = System.Drawing.Color.White;
            this.lbl_Drive_CurrentPos_Feed1.Location = new System.Drawing.Point(134, 456);
            this.lbl_Drive_CurrentPos_Feed1.Name = "lbl_Drive_CurrentPos_Feed1";
            this.lbl_Drive_CurrentPos_Feed1.Size = new System.Drawing.Size(125, 16);
            this.lbl_Drive_CurrentPos_Feed1.TabIndex = 222;
            this.lbl_Drive_CurrentPos_Feed1.Text = "X";
            this.lbl_Drive_CurrentPos_Feed1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.SystemColors.Highlight;
            this.label45.ForeColor = System.Drawing.Color.White;
            this.label45.Location = new System.Drawing.Point(20, 456);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(110, 16);
            this.label45.TabIndex = 221;
            this.label45.Text = "Position 정위치";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed2St2_1
            // 
            this.lbl_Feed2St2_1.BackColor = System.Drawing.Color.White;
            this.lbl_Feed2St2_1.Location = new System.Drawing.Point(263, 636);
            this.lbl_Feed2St2_1.Name = "lbl_Feed2St2_1";
            this.lbl_Feed2St2_1.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed2St2_1.TabIndex = 220;
            this.lbl_Feed2St2_1.Text = "미접속";
            this.lbl_Feed2St2_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed1St2_1
            // 
            this.lbl_Feed1St2_1.BackColor = System.Drawing.Color.White;
            this.lbl_Feed1St2_1.Location = new System.Drawing.Point(134, 636);
            this.lbl_Feed1St2_1.Name = "lbl_Feed1St2_1";
            this.lbl_Feed1St2_1.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed1St2_1.TabIndex = 219;
            this.lbl_Feed1St2_1.Text = "미접속";
            this.lbl_Feed1St2_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.SystemColors.Highlight;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(20, 636);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(110, 16);
            this.label17.TabIndex = 218;
            this.label17.Text = "인버터 알람";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed2St1_5
            // 
            this.lbl_Feed2St1_5.BackColor = System.Drawing.Color.White;
            this.lbl_Feed2St1_5.Location = new System.Drawing.Point(263, 546);
            this.lbl_Feed2St1_5.Name = "lbl_Feed2St1_5";
            this.lbl_Feed2St1_5.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed2St1_5.TabIndex = 217;
            this.lbl_Feed2St1_5.Text = "미감지";
            this.lbl_Feed2St1_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed1St1_5
            // 
            this.lbl_Feed1St1_5.BackColor = System.Drawing.Color.White;
            this.lbl_Feed1St1_5.Location = new System.Drawing.Point(134, 546);
            this.lbl_Feed1St1_5.Name = "lbl_Feed1St1_5";
            this.lbl_Feed1St1_5.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed1St1_5.TabIndex = 216;
            this.lbl_Feed1St1_5.Text = "미감지";
            this.lbl_Feed1St1_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Highlight;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(20, 546);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 16);
            this.label4.TabIndex = 215;
            this.label4.Text = "이탈화물 감지(우)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed2St1_6
            // 
            this.lbl_Feed2St1_6.BackColor = System.Drawing.Color.White;
            this.lbl_Feed2St1_6.Location = new System.Drawing.Point(263, 528);
            this.lbl_Feed2St1_6.Name = "lbl_Feed2St1_6";
            this.lbl_Feed2St1_6.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed2St1_6.TabIndex = 214;
            this.lbl_Feed2St1_6.Text = "미감지";
            this.lbl_Feed2St1_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed1St1_6
            // 
            this.lbl_Feed1St1_6.BackColor = System.Drawing.Color.White;
            this.lbl_Feed1St1_6.Location = new System.Drawing.Point(134, 528);
            this.lbl_Feed1St1_6.Name = "lbl_Feed1St1_6";
            this.lbl_Feed1St1_6.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed1St1_6.TabIndex = 213;
            this.lbl_Feed1St1_6.Text = "미감지";
            this.lbl_Feed1St1_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.SystemColors.Highlight;
            this.label40.ForeColor = System.Drawing.Color.White;
            this.label40.Location = new System.Drawing.Point(20, 528);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(110, 16);
            this.label40.TabIndex = 212;
            this.label40.Text = "이탈화물 감지(좌)";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed2St1_4
            // 
            this.lbl_Feed2St1_4.BackColor = System.Drawing.Color.White;
            this.lbl_Feed2St1_4.Location = new System.Drawing.Point(263, 510);
            this.lbl_Feed2St1_4.Name = "lbl_Feed2St1_4";
            this.lbl_Feed2St1_4.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed2St1_4.TabIndex = 211;
            this.lbl_Feed2St1_4.Text = "미감지";
            this.lbl_Feed2St1_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed1St1_4
            // 
            this.lbl_Feed1St1_4.BackColor = System.Drawing.Color.White;
            this.lbl_Feed1St1_4.Location = new System.Drawing.Point(134, 510);
            this.lbl_Feed1St1_4.Name = "lbl_Feed1St1_4";
            this.lbl_Feed1St1_4.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed1St1_4.TabIndex = 210;
            this.lbl_Feed1St1_4.Text = "미감지";
            this.lbl_Feed1St1_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.SystemColors.Highlight;
            this.label38.ForeColor = System.Drawing.Color.White;
            this.label38.Location = new System.Drawing.Point(20, 510);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(110, 16);
            this.label38.TabIndex = 209;
            this.label38.Text = "적재화물 감지";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.SystemColors.Highlight;
            this.label57.ForeColor = System.Drawing.Color.White;
            this.label57.Location = new System.Drawing.Point(263, 418);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(125, 18);
            this.label57.TabIndex = 208;
            this.label57.Text = "Feeding 2";
            this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label58
            // 
            this.label58.BackColor = System.Drawing.SystemColors.Highlight;
            this.label58.ForeColor = System.Drawing.Color.White;
            this.label58.Location = new System.Drawing.Point(134, 418);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(125, 18);
            this.label58.TabIndex = 207;
            this.label58.Text = "Feeding 1";
            this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed2St2_0
            // 
            this.lbl_Feed2St2_0.BackColor = System.Drawing.Color.White;
            this.lbl_Feed2St2_0.Location = new System.Drawing.Point(263, 654);
            this.lbl_Feed2St2_0.Name = "lbl_Feed2St2_0";
            this.lbl_Feed2St2_0.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed2St2_0.TabIndex = 206;
            this.lbl_Feed2St2_0.Text = "미접속";
            this.lbl_Feed2St2_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed2St1_0
            // 
            this.lbl_Feed2St1_0.BackColor = System.Drawing.Color.White;
            this.lbl_Feed2St1_0.Location = new System.Drawing.Point(263, 618);
            this.lbl_Feed2St1_0.Name = "lbl_Feed2St1_0";
            this.lbl_Feed2St1_0.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed2St1_0.TabIndex = 205;
            this.lbl_Feed2St1_0.Text = "정지";
            this.lbl_Feed2St1_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed2St1_1
            // 
            this.lbl_Feed2St1_1.BackColor = System.Drawing.Color.White;
            this.lbl_Feed2St1_1.Location = new System.Drawing.Point(263, 600);
            this.lbl_Feed2St1_1.Name = "lbl_Feed2St1_1";
            this.lbl_Feed2St1_1.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed2St1_1.TabIndex = 204;
            this.lbl_Feed2St1_1.Text = "정지/동속";
            this.lbl_Feed2St1_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed2St1_2
            // 
            this.lbl_Feed2St1_2.BackColor = System.Drawing.Color.White;
            this.lbl_Feed2St1_2.Location = new System.Drawing.Point(263, 582);
            this.lbl_Feed2St1_2.Name = "lbl_Feed2St1_2";
            this.lbl_Feed2St1_2.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed2St1_2.TabIndex = 203;
            this.lbl_Feed2St1_2.Text = "정지/동속";
            this.lbl_Feed2St1_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed2St1_3
            // 
            this.lbl_Feed2St1_3.BackColor = System.Drawing.Color.White;
            this.lbl_Feed2St1_3.Location = new System.Drawing.Point(263, 564);
            this.lbl_Feed2St1_3.Name = "lbl_Feed2St1_3";
            this.lbl_Feed2St1_3.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed2St1_3.TabIndex = 202;
            this.lbl_Feed2St1_3.Text = "정방향";
            this.lbl_Feed2St1_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed2_Dest
            // 
            this.lbl_Feed2_Dest.BackColor = System.Drawing.Color.White;
            this.lbl_Feed2_Dest.Location = new System.Drawing.Point(263, 492);
            this.lbl_Feed2_Dest.Name = "lbl_Feed2_Dest";
            this.lbl_Feed2_Dest.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed2_Dest.TabIndex = 201;
            this.lbl_Feed2_Dest.Text = "X";
            this.lbl_Feed2_Dest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed2_Pos
            // 
            this.lbl_Feed2_Pos.BackColor = System.Drawing.Color.White;
            this.lbl_Feed2_Pos.Location = new System.Drawing.Point(263, 474);
            this.lbl_Feed2_Pos.Name = "lbl_Feed2_Pos";
            this.lbl_Feed2_Pos.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed2_Pos.TabIndex = 200;
            this.lbl_Feed2_Pos.Text = "X";
            this.lbl_Feed2_Pos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed1St2_0
            // 
            this.lbl_Feed1St2_0.BackColor = System.Drawing.Color.White;
            this.lbl_Feed1St2_0.Location = new System.Drawing.Point(134, 654);
            this.lbl_Feed1St2_0.Name = "lbl_Feed1St2_0";
            this.lbl_Feed1St2_0.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed1St2_0.TabIndex = 199;
            this.lbl_Feed1St2_0.Text = "미접속";
            this.lbl_Feed1St2_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed1St1_0
            // 
            this.lbl_Feed1St1_0.BackColor = System.Drawing.Color.White;
            this.lbl_Feed1St1_0.Location = new System.Drawing.Point(134, 618);
            this.lbl_Feed1St1_0.Name = "lbl_Feed1St1_0";
            this.lbl_Feed1St1_0.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed1St1_0.TabIndex = 198;
            this.lbl_Feed1St1_0.Text = "정지";
            this.lbl_Feed1St1_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed1St1_1
            // 
            this.lbl_Feed1St1_1.BackColor = System.Drawing.Color.White;
            this.lbl_Feed1St1_1.Location = new System.Drawing.Point(134, 600);
            this.lbl_Feed1St1_1.Name = "lbl_Feed1St1_1";
            this.lbl_Feed1St1_1.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed1St1_1.TabIndex = 197;
            this.lbl_Feed1St1_1.Text = "정지/동속";
            this.lbl_Feed1St1_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed1St1_2
            // 
            this.lbl_Feed1St1_2.BackColor = System.Drawing.Color.White;
            this.lbl_Feed1St1_2.Location = new System.Drawing.Point(134, 582);
            this.lbl_Feed1St1_2.Name = "lbl_Feed1St1_2";
            this.lbl_Feed1St1_2.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed1St1_2.TabIndex = 196;
            this.lbl_Feed1St1_2.Text = "정지/동속";
            this.lbl_Feed1St1_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed1St1_3
            // 
            this.lbl_Feed1St1_3.BackColor = System.Drawing.Color.White;
            this.lbl_Feed1St1_3.Location = new System.Drawing.Point(134, 564);
            this.lbl_Feed1St1_3.Name = "lbl_Feed1St1_3";
            this.lbl_Feed1St1_3.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed1St1_3.TabIndex = 195;
            this.lbl_Feed1St1_3.Text = "정방향";
            this.lbl_Feed1St1_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed1_Dest
            // 
            this.lbl_Feed1_Dest.BackColor = System.Drawing.Color.White;
            this.lbl_Feed1_Dest.Location = new System.Drawing.Point(134, 492);
            this.lbl_Feed1_Dest.Name = "lbl_Feed1_Dest";
            this.lbl_Feed1_Dest.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed1_Dest.TabIndex = 194;
            this.lbl_Feed1_Dest.Text = "X";
            this.lbl_Feed1_Dest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Feed1_Pos
            // 
            this.lbl_Feed1_Pos.BackColor = System.Drawing.Color.White;
            this.lbl_Feed1_Pos.Location = new System.Drawing.Point(134, 474);
            this.lbl_Feed1_Pos.Name = "lbl_Feed1_Pos";
            this.lbl_Feed1_Pos.Size = new System.Drawing.Size(125, 16);
            this.lbl_Feed1_Pos.TabIndex = 193;
            this.lbl_Feed1_Pos.Text = "X";
            this.lbl_Feed1_Pos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label170
            // 
            this.label170.BackColor = System.Drawing.SystemColors.Highlight;
            this.label170.ForeColor = System.Drawing.Color.White;
            this.label170.Location = new System.Drawing.Point(20, 654);
            this.label170.Name = "label170";
            this.label170.Size = new System.Drawing.Size(110, 16);
            this.label170.TabIndex = 192;
            this.label170.Text = "인버터 접속";
            this.label170.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label171
            // 
            this.label171.BackColor = System.Drawing.SystemColors.Highlight;
            this.label171.ForeColor = System.Drawing.Color.White;
            this.label171.Location = new System.Drawing.Point(20, 618);
            this.label171.Name = "label171";
            this.label171.Size = new System.Drawing.Size(110, 16);
            this.label171.TabIndex = 191;
            this.label171.Text = "동작상태";
            this.label171.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label172
            // 
            this.label172.BackColor = System.Drawing.SystemColors.Highlight;
            this.label172.ForeColor = System.Drawing.Color.White;
            this.label172.Location = new System.Drawing.Point(20, 600);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(110, 16);
            this.label172.TabIndex = 190;
            this.label172.Text = "가속상태";
            this.label172.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label173
            // 
            this.label173.BackColor = System.Drawing.SystemColors.Highlight;
            this.label173.ForeColor = System.Drawing.Color.White;
            this.label173.Location = new System.Drawing.Point(20, 582);
            this.label173.Name = "label173";
            this.label173.Size = new System.Drawing.Size(110, 16);
            this.label173.TabIndex = 189;
            this.label173.Text = "감속상태";
            this.label173.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label174
            // 
            this.label174.BackColor = System.Drawing.SystemColors.Highlight;
            this.label174.ForeColor = System.Drawing.Color.White;
            this.label174.Location = new System.Drawing.Point(20, 564);
            this.label174.Name = "label174";
            this.label174.Size = new System.Drawing.Size(110, 16);
            this.label174.TabIndex = 188;
            this.label174.Text = "이동방향";
            this.label174.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label177
            // 
            this.label177.BackColor = System.Drawing.SystemColors.Highlight;
            this.label177.ForeColor = System.Drawing.Color.White;
            this.label177.Location = new System.Drawing.Point(20, 492);
            this.label177.Name = "label177";
            this.label177.Size = new System.Drawing.Size(110, 16);
            this.label177.TabIndex = 187;
            this.label177.Text = "목적 주행위치";
            this.label177.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label178
            // 
            this.label178.BackColor = System.Drawing.SystemColors.Highlight;
            this.label178.ForeColor = System.Drawing.Color.White;
            this.label178.Location = new System.Drawing.Point(20, 474);
            this.label178.Name = "label178";
            this.label178.Size = new System.Drawing.Size(110, 16);
            this.label178.TabIndex = 186;
            this.label178.Text = "현재 주행위치";
            this.label178.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DriveSt2_1
            // 
            this.lbl_DriveSt2_1.BackColor = System.Drawing.Color.White;
            this.lbl_DriveSt2_1.Location = new System.Drawing.Point(158, 157);
            this.lbl_DriveSt2_1.Name = "lbl_DriveSt2_1";
            this.lbl_DriveSt2_1.Size = new System.Drawing.Size(125, 16);
            this.lbl_DriveSt2_1.TabIndex = 167;
            this.lbl_DriveSt2_1.Text = "미접속";
            this.lbl_DriveSt2_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label63
            // 
            this.label63.BackColor = System.Drawing.SystemColors.Highlight;
            this.label63.ForeColor = System.Drawing.Color.White;
            this.label63.Location = new System.Drawing.Point(20, 157);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(135, 16);
            this.label63.TabIndex = 166;
            this.label63.Text = "인버터 알람";
            this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Dev_Maintance
            // 
            this.lbl_Dev_Maintance.BackColor = System.Drawing.Color.White;
            this.lbl_Dev_Maintance.Location = new System.Drawing.Point(158, 49);
            this.lbl_Dev_Maintance.Name = "lbl_Dev_Maintance";
            this.lbl_Dev_Maintance.Size = new System.Drawing.Size(125, 16);
            this.lbl_Dev_Maintance.TabIndex = 152;
            this.lbl_Dev_Maintance.Text = "X";
            this.lbl_Dev_Maintance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Dev_Home
            // 
            this.lbl_Dev_Home.BackColor = System.Drawing.Color.White;
            this.lbl_Dev_Home.Location = new System.Drawing.Point(158, 31);
            this.lbl_Dev_Home.Name = "lbl_Dev_Home";
            this.lbl_Dev_Home.Size = new System.Drawing.Size(125, 16);
            this.lbl_Dev_Home.TabIndex = 151;
            this.lbl_Dev_Home.Text = "X";
            this.lbl_Dev_Home.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.SystemColors.Highlight;
            this.label36.ForeColor = System.Drawing.Color.White;
            this.label36.Location = new System.Drawing.Point(20, 49);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(135, 16);
            this.label36.TabIndex = 150;
            this.label36.Text = "보수 정위치";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.SystemColors.Highlight;
            this.label50.ForeColor = System.Drawing.Color.White;
            this.label50.Location = new System.Drawing.Point(20, 31);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(135, 16);
            this.label50.TabIndex = 149;
            this.label50.Text = "홈 정위치";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Drive_Destination
            // 
            this.lbl_Drive_Destination.BackColor = System.Drawing.Color.White;
            this.lbl_Drive_Destination.Location = new System.Drawing.Point(158, 355);
            this.lbl_Drive_Destination.Name = "lbl_Drive_Destination";
            this.lbl_Drive_Destination.Size = new System.Drawing.Size(125, 16);
            this.lbl_Drive_Destination.TabIndex = 125;
            this.lbl_Drive_Destination.Text = "0";
            this.lbl_Drive_Destination.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Drive_Speed
            // 
            this.lbl_Drive_Speed.BackColor = System.Drawing.Color.White;
            this.lbl_Drive_Speed.Location = new System.Drawing.Point(158, 337);
            this.lbl_Drive_Speed.Name = "lbl_Drive_Speed";
            this.lbl_Drive_Speed.Size = new System.Drawing.Size(125, 16);
            this.lbl_Drive_Speed.TabIndex = 124;
            this.lbl_Drive_Speed.Text = "0";
            this.lbl_Drive_Speed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Drive_Position
            // 
            this.lbl_Drive_Position.BackColor = System.Drawing.Color.White;
            this.lbl_Drive_Position.Location = new System.Drawing.Point(158, 319);
            this.lbl_Drive_Position.Name = "lbl_Drive_Position";
            this.lbl_Drive_Position.Size = new System.Drawing.Size(125, 16);
            this.lbl_Drive_Position.TabIndex = 123;
            this.lbl_Drive_Position.Text = "0";
            this.lbl_Drive_Position.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.SystemColors.Highlight;
            this.label53.ForeColor = System.Drawing.Color.White;
            this.label53.Location = new System.Drawing.Point(20, 355);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(135, 16);
            this.label53.TabIndex = 122;
            this.label53.Text = "목표위치값 (mm)";
            this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label131
            // 
            this.label131.BackColor = System.Drawing.SystemColors.Highlight;
            this.label131.ForeColor = System.Drawing.Color.White;
            this.label131.Location = new System.Drawing.Point(20, 337);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(135, 16);
            this.label131.TabIndex = 121;
            this.label131.Text = "현재속도 (m/min)";
            this.label131.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label132
            // 
            this.label132.BackColor = System.Drawing.SystemColors.Highlight;
            this.label132.ForeColor = System.Drawing.Color.White;
            this.label132.Location = new System.Drawing.Point(20, 319);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(135, 16);
            this.label132.TabIndex = 120;
            this.label132.Text = "현재위치값 (mm)";
            this.label132.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DriveSt2_0
            // 
            this.lbl_DriveSt2_0.BackColor = System.Drawing.Color.White;
            this.lbl_DriveSt2_0.Location = new System.Drawing.Point(158, 175);
            this.lbl_DriveSt2_0.Name = "lbl_DriveSt2_0";
            this.lbl_DriveSt2_0.Size = new System.Drawing.Size(125, 16);
            this.lbl_DriveSt2_0.TabIndex = 116;
            this.lbl_DriveSt2_0.Text = "미접속";
            this.lbl_DriveSt2_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DriveSt1_0
            // 
            this.lbl_DriveSt1_0.BackColor = System.Drawing.Color.White;
            this.lbl_DriveSt1_0.Location = new System.Drawing.Point(158, 139);
            this.lbl_DriveSt1_0.Name = "lbl_DriveSt1_0";
            this.lbl_DriveSt1_0.Size = new System.Drawing.Size(125, 16);
            this.lbl_DriveSt1_0.TabIndex = 115;
            this.lbl_DriveSt1_0.Text = "정지";
            this.lbl_DriveSt1_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DriveSt1_1
            // 
            this.lbl_DriveSt1_1.BackColor = System.Drawing.Color.White;
            this.lbl_DriveSt1_1.Location = new System.Drawing.Point(158, 121);
            this.lbl_DriveSt1_1.Name = "lbl_DriveSt1_1";
            this.lbl_DriveSt1_1.Size = new System.Drawing.Size(125, 16);
            this.lbl_DriveSt1_1.TabIndex = 114;
            this.lbl_DriveSt1_1.Text = "정지/동속";
            this.lbl_DriveSt1_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DriveSt1_2
            // 
            this.lbl_DriveSt1_2.BackColor = System.Drawing.Color.White;
            this.lbl_DriveSt1_2.Location = new System.Drawing.Point(158, 103);
            this.lbl_DriveSt1_2.Name = "lbl_DriveSt1_2";
            this.lbl_DriveSt1_2.Size = new System.Drawing.Size(125, 16);
            this.lbl_DriveSt1_2.TabIndex = 113;
            this.lbl_DriveSt1_2.Text = "정지/동속";
            this.lbl_DriveSt1_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DriveSt1_3
            // 
            this.lbl_DriveSt1_3.BackColor = System.Drawing.Color.White;
            this.lbl_DriveSt1_3.Location = new System.Drawing.Point(158, 85);
            this.lbl_DriveSt1_3.Name = "lbl_DriveSt1_3";
            this.lbl_DriveSt1_3.Size = new System.Drawing.Size(125, 16);
            this.lbl_DriveSt1_3.TabIndex = 112;
            this.lbl_DriveSt1_3.Text = "정방향";
            this.lbl_DriveSt1_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DriveSt1_4
            // 
            this.lbl_DriveSt1_4.BackColor = System.Drawing.Color.White;
            this.lbl_DriveSt1_4.Location = new System.Drawing.Point(158, 67);
            this.lbl_DriveSt1_4.Name = "lbl_DriveSt1_4";
            this.lbl_DriveSt1_4.Size = new System.Drawing.Size(125, 16);
            this.lbl_DriveSt1_4.TabIndex = 111;
            this.lbl_DriveSt1_4.Text = "정위치 아님";
            this.lbl_DriveSt1_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_CanWork_StationIndex
            // 
            this.lbl_CanWork_StationIndex.BackColor = System.Drawing.Color.White;
            this.lbl_CanWork_StationIndex.Location = new System.Drawing.Point(158, 211);
            this.lbl_CanWork_StationIndex.Name = "lbl_CanWork_StationIndex";
            this.lbl_CanWork_StationIndex.Size = new System.Drawing.Size(125, 16);
            this.lbl_CanWork_StationIndex.TabIndex = 110;
            this.lbl_CanWork_StationIndex.Text = "0";
            this.lbl_CanWork_StationIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DriveSt1_6
            // 
            this.lbl_DriveSt1_6.BackColor = System.Drawing.Color.White;
            this.lbl_DriveSt1_6.Location = new System.Drawing.Point(158, 247);
            this.lbl_DriveSt1_6.Name = "lbl_DriveSt1_6";
            this.lbl_DriveSt1_6.Size = new System.Drawing.Size(125, 16);
            this.lbl_DriveSt1_6.TabIndex = 109;
            this.lbl_DriveSt1_6.Text = "0";
            this.lbl_DriveSt1_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DriveSt1_5
            // 
            this.lbl_DriveSt1_5.BackColor = System.Drawing.Color.White;
            this.lbl_DriveSt1_5.Location = new System.Drawing.Point(158, 229);
            this.lbl_DriveSt1_5.Name = "lbl_DriveSt1_5";
            this.lbl_DriveSt1_5.Size = new System.Drawing.Size(125, 16);
            this.lbl_DriveSt1_5.TabIndex = 108;
            this.lbl_DriveSt1_5.Text = "0";
            this.lbl_DriveSt1_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.SystemColors.Highlight;
            this.label46.ForeColor = System.Drawing.Color.White;
            this.label46.Location = new System.Drawing.Point(20, 175);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(135, 16);
            this.label46.TabIndex = 104;
            this.label46.Text = "인버터 접속";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.SystemColors.Highlight;
            this.label48.ForeColor = System.Drawing.Color.White;
            this.label48.Location = new System.Drawing.Point(20, 139);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(135, 16);
            this.label48.TabIndex = 103;
            this.label48.Text = "동작상태";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.SystemColors.Highlight;
            this.label49.ForeColor = System.Drawing.Color.White;
            this.label49.Location = new System.Drawing.Point(20, 121);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(135, 16);
            this.label49.TabIndex = 102;
            this.label49.Text = "가속상태";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label117
            // 
            this.label117.BackColor = System.Drawing.SystemColors.Highlight;
            this.label117.ForeColor = System.Drawing.Color.White;
            this.label117.Location = new System.Drawing.Point(20, 103);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(135, 16);
            this.label117.TabIndex = 101;
            this.label117.Text = "감속상태";
            this.label117.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label118
            // 
            this.label118.BackColor = System.Drawing.SystemColors.Highlight;
            this.label118.ForeColor = System.Drawing.Color.White;
            this.label118.Location = new System.Drawing.Point(20, 85);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(135, 16);
            this.label118.TabIndex = 100;
            this.label118.Text = "이동방향";
            this.label118.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.SystemColors.Highlight;
            this.label47.ForeColor = System.Drawing.Color.White;
            this.label47.Location = new System.Drawing.Point(20, 67);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(135, 16);
            this.label47.TabIndex = 99;
            this.label47.Text = "주행 정위치";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label113
            // 
            this.label113.BackColor = System.Drawing.SystemColors.Highlight;
            this.label113.ForeColor = System.Drawing.Color.White;
            this.label113.Location = new System.Drawing.Point(20, 211);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(135, 16);
            this.label113.TabIndex = 98;
            this.label113.Text = "작업 가능 스테이션";
            this.label113.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.SystemColors.Highlight;
            this.label43.ForeColor = System.Drawing.Color.White;
            this.label43.Location = new System.Drawing.Point(20, 247);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(135, 16);
            this.label43.TabIndex = 97;
            this.label43.Text = "후진감속 센서";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.SystemColors.Highlight;
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(20, 229);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(135, 16);
            this.label22.TabIndex = 96;
            this.label22.Text = "전진감속 센서";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label22.Click += new System.EventHandler(this.label22_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage2.Size = new System.Drawing.Size(410, 713);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "리모컨";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lbl_Key_1_0);
            this.groupBox5.Controls.Add(this.lbl_Key_1_1);
            this.groupBox5.Controls.Add(this.lbl_Key_1_2);
            this.groupBox5.Controls.Add(this.lbl_Key_1_3);
            this.groupBox5.Controls.Add(this.lbl_Key_1_4);
            this.groupBox5.Controls.Add(this.lbl_Key_2_0);
            this.groupBox5.Controls.Add(this.lbl_Key_2_1);
            this.groupBox5.Controls.Add(this.lbl_Key_2_2);
            this.groupBox5.Controls.Add(this.lbl_Key_2_3);
            this.groupBox5.Controls.Add(this.lbl_Key_2_4);
            this.groupBox5.Controls.Add(this.lbl_Key_2_5);
            this.groupBox5.Controls.Add(this.lbl_Key_1_5);
            this.groupBox5.Controls.Add(this.lbl_Key_1_6);
            this.groupBox5.Location = new System.Drawing.Point(19, 38);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(385, 384);
            this.groupBox5.TabIndex = 164;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "리모콘 Key 입력";
            // 
            // lbl_Key_1_0
            // 
            this.lbl_Key_1_0.BackColor = System.Drawing.Color.Silver;
            this.lbl_Key_1_0.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Key_1_0.Location = new System.Drawing.Point(29, 43);
            this.lbl_Key_1_0.Name = "lbl_Key_1_0";
            this.lbl_Key_1_0.Size = new System.Drawing.Size(113, 24);
            this.lbl_Key_1_0.TabIndex = 197;
            this.lbl_Key_1_0.Text = "AUTO/MAN";
            this.lbl_Key_1_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Key_1_1
            // 
            this.lbl_Key_1_1.BackColor = System.Drawing.Color.Silver;
            this.lbl_Key_1_1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Key_1_1.Location = new System.Drawing.Point(241, 43);
            this.lbl_Key_1_1.Name = "lbl_Key_1_1";
            this.lbl_Key_1_1.Size = new System.Drawing.Size(113, 24);
            this.lbl_Key_1_1.TabIndex = 196;
            this.lbl_Key_1_1.Text = "S.STOP";
            this.lbl_Key_1_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Key_1_2
            // 
            this.lbl_Key_1_2.BackColor = System.Drawing.Color.Silver;
            this.lbl_Key_1_2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Key_1_2.Location = new System.Drawing.Point(135, 116);
            this.lbl_Key_1_2.Name = "lbl_Key_1_2";
            this.lbl_Key_1_2.Size = new System.Drawing.Size(113, 24);
            this.lbl_Key_1_2.TabIndex = 195;
            this.lbl_Key_1_2.Text = "FWD";
            this.lbl_Key_1_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Key_1_3
            // 
            this.lbl_Key_1_3.BackColor = System.Drawing.Color.Silver;
            this.lbl_Key_1_3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Key_1_3.Location = new System.Drawing.Point(135, 193);
            this.lbl_Key_1_3.Name = "lbl_Key_1_3";
            this.lbl_Key_1_3.Size = new System.Drawing.Size(113, 24);
            this.lbl_Key_1_3.TabIndex = 194;
            this.lbl_Key_1_3.Text = "REV";
            this.lbl_Key_1_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Key_1_4
            // 
            this.lbl_Key_1_4.BackColor = System.Drawing.Color.Silver;
            this.lbl_Key_1_4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Key_1_4.Location = new System.Drawing.Point(11, 153);
            this.lbl_Key_1_4.Name = "lbl_Key_1_4";
            this.lbl_Key_1_4.Size = new System.Drawing.Size(113, 24);
            this.lbl_Key_1_4.TabIndex = 193;
            this.lbl_Key_1_4.Text = "CATCH/L";
            this.lbl_Key_1_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Key_2_0
            // 
            this.lbl_Key_2_0.BackColor = System.Drawing.Color.Silver;
            this.lbl_Key_2_0.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Key_2_0.Location = new System.Drawing.Point(11, 265);
            this.lbl_Key_2_0.Name = "lbl_Key_2_0";
            this.lbl_Key_2_0.Size = new System.Drawing.Size(113, 24);
            this.lbl_Key_2_0.TabIndex = 192;
            this.lbl_Key_2_0.Text = "M1/MODE";
            this.lbl_Key_2_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Key_2_1
            // 
            this.lbl_Key_2_1.BackColor = System.Drawing.Color.Silver;
            this.lbl_Key_2_1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Key_2_1.Location = new System.Drawing.Point(11, 323);
            this.lbl_Key_2_1.Name = "lbl_Key_2_1";
            this.lbl_Key_2_1.Size = new System.Drawing.Size(113, 24);
            this.lbl_Key_2_1.TabIndex = 191;
            this.lbl_Key_2_1.Text = "M2/SET";
            this.lbl_Key_2_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Key_2_2
            // 
            this.lbl_Key_2_2.BackColor = System.Drawing.Color.Silver;
            this.lbl_Key_2_2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Key_2_2.Location = new System.Drawing.Point(135, 265);
            this.lbl_Key_2_2.Name = "lbl_Key_2_2";
            this.lbl_Key_2_2.Size = new System.Drawing.Size(113, 24);
            this.lbl_Key_2_2.TabIndex = 190;
            this.lbl_Key_2_2.Text = "UP/+";
            this.lbl_Key_2_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Key_2_3
            // 
            this.lbl_Key_2_3.BackColor = System.Drawing.Color.Silver;
            this.lbl_Key_2_3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Key_2_3.Location = new System.Drawing.Point(135, 323);
            this.lbl_Key_2_3.Name = "lbl_Key_2_3";
            this.lbl_Key_2_3.Size = new System.Drawing.Size(113, 24);
            this.lbl_Key_2_3.TabIndex = 189;
            this.lbl_Key_2_3.Text = "DOWN/-";
            this.lbl_Key_2_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Key_2_4
            // 
            this.lbl_Key_2_4.BackColor = System.Drawing.Color.Silver;
            this.lbl_Key_2_4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Key_2_4.Location = new System.Drawing.Point(259, 265);
            this.lbl_Key_2_4.Name = "lbl_Key_2_4";
            this.lbl_Key_2_4.Size = new System.Drawing.Size(113, 24);
            this.lbl_Key_2_4.TabIndex = 188;
            this.lbl_Key_2_4.Text = "START";
            this.lbl_Key_2_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Key_2_5
            // 
            this.lbl_Key_2_5.BackColor = System.Drawing.Color.Silver;
            this.lbl_Key_2_5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Key_2_5.Location = new System.Drawing.Point(259, 323);
            this.lbl_Key_2_5.Name = "lbl_Key_2_5";
            this.lbl_Key_2_5.Size = new System.Drawing.Size(113, 24);
            this.lbl_Key_2_5.TabIndex = 187;
            this.lbl_Key_2_5.Text = "RESET";
            this.lbl_Key_2_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Key_1_5
            // 
            this.lbl_Key_1_5.BackColor = System.Drawing.Color.Silver;
            this.lbl_Key_1_5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Key_1_5.Location = new System.Drawing.Point(259, 154);
            this.lbl_Key_1_5.Name = "lbl_Key_1_5";
            this.lbl_Key_1_5.Size = new System.Drawing.Size(113, 24);
            this.lbl_Key_1_5.TabIndex = 184;
            this.lbl_Key_1_5.Text = "CATCH/R";
            this.lbl_Key_1_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Key_1_6
            // 
            this.lbl_Key_1_6.BackColor = System.Drawing.Color.Silver;
            this.lbl_Key_1_6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Key_1_6.Location = new System.Drawing.Point(135, 153);
            this.lbl_Key_1_6.Name = "lbl_Key_1_6";
            this.lbl_Key_1_6.Size = new System.Drawing.Size(113, 24);
            this.lbl_Key_1_6.TabIndex = 183;
            this.lbl_Key_1_6.Text = "ENTER";
            this.lbl_Key_1_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage5.Controls.Add(this.groupBox7);
            this.tabPage5.Controls.Add(this.groupBox1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage5.Size = new System.Drawing.Size(410, 713);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "WCS / 지상반";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label77);
            this.groupBox7.Controls.Add(this.label76);
            this.groupBox7.Controls.Add(this.lbl_WCS14_CTRL2_Dest);
            this.groupBox7.Controls.Add(this.lbl_WCS14_CTRL1_0);
            this.groupBox7.Controls.Add(this.label61);
            this.groupBox7.Controls.Add(this.label62);
            this.groupBox7.Controls.Add(this.lbl_WCS14_CTRL1_2);
            this.groupBox7.Controls.Add(this.lbl_WCS14_CTRL1_3);
            this.groupBox7.Controls.Add(this.label66);
            this.groupBox7.Controls.Add(this.label67);
            this.groupBox7.Controls.Add(this.lbl_WCS14_CTRL1_4);
            this.groupBox7.Controls.Add(this.lbl_WCS14_CTRL1_5);
            this.groupBox7.Controls.Add(this.label70);
            this.groupBox7.Controls.Add(this.label71);
            this.groupBox7.Controls.Add(this.lbl_WCS14_CTRL1_6);
            this.groupBox7.Controls.Add(this.lbl_WCS14_CTRL1_7);
            this.groupBox7.Controls.Add(this.label74);
            this.groupBox7.Controls.Add(this.label75);
            this.groupBox7.Controls.Add(this.lbl_WCS14_ST2_Position);
            this.groupBox7.Controls.Add(this.label56);
            this.groupBox7.Controls.Add(this.lbl_WCS14_ST2_7);
            this.groupBox7.Controls.Add(this.lbl_WCS14_ST1_Speed);
            this.groupBox7.Controls.Add(this.label39);
            this.groupBox7.Controls.Add(this.label41);
            this.groupBox7.Controls.Add(this.lbl_WCS14_ST1_2);
            this.groupBox7.Controls.Add(this.lbl_WCS14_ST1_3);
            this.groupBox7.Controls.Add(this.label52);
            this.groupBox7.Controls.Add(this.label54);
            this.groupBox7.Controls.Add(this.lbl_WCS14_ST1_4);
            this.groupBox7.Controls.Add(this.lbl_WCS14_ST1_5);
            this.groupBox7.Controls.Add(this.label29);
            this.groupBox7.Controls.Add(this.label33);
            this.groupBox7.Controls.Add(this.lbl_WCS14_ST1_6);
            this.groupBox7.Controls.Add(this.lbl_WCS14_ST1_7);
            this.groupBox7.Controls.Add(this.label25);
            this.groupBox7.Controls.Add(this.label26);
            this.groupBox7.Controls.Add(this.lbl_WCS14_ControlRx_Count);
            this.groupBox7.Controls.Add(this.lbl_WCS14_StatusRx_Count);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.label20);
            this.groupBox7.Location = new System.Drawing.Point(25, 160);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(359, 424);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "WCS (14bytes Protocol)";
            // 
            // label77
            // 
            this.label77.BackColor = System.Drawing.SystemColors.Highlight;
            this.label77.ForeColor = System.Drawing.Color.White;
            this.label77.Location = new System.Drawing.Point(11, 264);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(53, 142);
            this.label77.TabIndex = 192;
            this.label77.Text = "제어";
            this.label77.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label76
            // 
            this.label76.BackColor = System.Drawing.SystemColors.Highlight;
            this.label76.ForeColor = System.Drawing.Color.White;
            this.label76.Location = new System.Drawing.Point(11, 84);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(53, 160);
            this.label76.TabIndex = 191;
            this.label76.Text = "상태";
            this.label76.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_CTRL2_Dest
            // 
            this.lbl_WCS14_CTRL2_Dest.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_CTRL2_Dest.Location = new System.Drawing.Point(204, 390);
            this.lbl_WCS14_CTRL2_Dest.Name = "lbl_WCS14_CTRL2_Dest";
            this.lbl_WCS14_CTRL2_Dest.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_CTRL2_Dest.TabIndex = 190;
            this.lbl_WCS14_CTRL2_Dest.Text = "0";
            this.lbl_WCS14_CTRL2_Dest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_CTRL1_0
            // 
            this.lbl_WCS14_CTRL1_0.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_CTRL1_0.Location = new System.Drawing.Point(204, 372);
            this.lbl_WCS14_CTRL1_0.Name = "lbl_WCS14_CTRL1_0";
            this.lbl_WCS14_CTRL1_0.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_CTRL1_0.TabIndex = 189;
            this.lbl_WCS14_CTRL1_0.Text = "0";
            this.lbl_WCS14_CTRL1_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.SystemColors.Highlight;
            this.label61.ForeColor = System.Drawing.Color.White;
            this.label61.Location = new System.Drawing.Point(66, 390);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(135, 16);
            this.label61.TabIndex = 188;
            this.label61.Text = "목적지";
            this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.SystemColors.Highlight;
            this.label62.ForeColor = System.Drawing.Color.White;
            this.label62.Location = new System.Drawing.Point(66, 372);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(135, 16);
            this.label62.TabIndex = 187;
            this.label62.Text = "PASS C/V";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_CTRL1_2
            // 
            this.lbl_WCS14_CTRL1_2.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_CTRL1_2.Location = new System.Drawing.Point(204, 354);
            this.lbl_WCS14_CTRL1_2.Name = "lbl_WCS14_CTRL1_2";
            this.lbl_WCS14_CTRL1_2.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_CTRL1_2.TabIndex = 186;
            this.lbl_WCS14_CTRL1_2.Text = "0";
            this.lbl_WCS14_CTRL1_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_CTRL1_3
            // 
            this.lbl_WCS14_CTRL1_3.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_CTRL1_3.Location = new System.Drawing.Point(204, 336);
            this.lbl_WCS14_CTRL1_3.Name = "lbl_WCS14_CTRL1_3";
            this.lbl_WCS14_CTRL1_3.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_CTRL1_3.TabIndex = 185;
            this.lbl_WCS14_CTRL1_3.Text = "0";
            this.lbl_WCS14_CTRL1_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label66
            // 
            this.label66.BackColor = System.Drawing.SystemColors.Highlight;
            this.label66.ForeColor = System.Drawing.Color.White;
            this.label66.Location = new System.Drawing.Point(66, 354);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(135, 16);
            this.label66.TabIndex = 184;
            this.label66.Text = "FD1";
            this.label66.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label67
            // 
            this.label67.BackColor = System.Drawing.SystemColors.Highlight;
            this.label67.ForeColor = System.Drawing.Color.White;
            this.label67.Location = new System.Drawing.Point(66, 336);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(135, 16);
            this.label67.TabIndex = 183;
            this.label67.Text = "FD2";
            this.label67.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_CTRL1_4
            // 
            this.lbl_WCS14_CTRL1_4.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_CTRL1_4.Location = new System.Drawing.Point(204, 318);
            this.lbl_WCS14_CTRL1_4.Name = "lbl_WCS14_CTRL1_4";
            this.lbl_WCS14_CTRL1_4.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_CTRL1_4.TabIndex = 182;
            this.lbl_WCS14_CTRL1_4.Text = "0";
            this.lbl_WCS14_CTRL1_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_CTRL1_5
            // 
            this.lbl_WCS14_CTRL1_5.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_CTRL1_5.Location = new System.Drawing.Point(204, 300);
            this.lbl_WCS14_CTRL1_5.Name = "lbl_WCS14_CTRL1_5";
            this.lbl_WCS14_CTRL1_5.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_CTRL1_5.TabIndex = 181;
            this.lbl_WCS14_CTRL1_5.Text = "0";
            this.lbl_WCS14_CTRL1_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label70
            // 
            this.label70.BackColor = System.Drawing.SystemColors.Highlight;
            this.label70.ForeColor = System.Drawing.Color.White;
            this.label70.Location = new System.Drawing.Point(66, 318);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(135, 16);
            this.label70.TabIndex = 180;
            this.label70.Text = "GO HOME";
            this.label70.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label71
            // 
            this.label71.BackColor = System.Drawing.SystemColors.Highlight;
            this.label71.ForeColor = System.Drawing.Color.White;
            this.label71.Location = new System.Drawing.Point(66, 300);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(135, 16);
            this.label71.TabIndex = 179;
            this.label71.Text = "자동전환";
            this.label71.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_CTRL1_6
            // 
            this.lbl_WCS14_CTRL1_6.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_CTRL1_6.Location = new System.Drawing.Point(204, 282);
            this.lbl_WCS14_CTRL1_6.Name = "lbl_WCS14_CTRL1_6";
            this.lbl_WCS14_CTRL1_6.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_CTRL1_6.TabIndex = 178;
            this.lbl_WCS14_CTRL1_6.Text = "0";
            this.lbl_WCS14_CTRL1_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_CTRL1_7
            // 
            this.lbl_WCS14_CTRL1_7.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_CTRL1_7.Location = new System.Drawing.Point(204, 264);
            this.lbl_WCS14_CTRL1_7.Name = "lbl_WCS14_CTRL1_7";
            this.lbl_WCS14_CTRL1_7.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_CTRL1_7.TabIndex = 177;
            this.lbl_WCS14_CTRL1_7.Text = "0";
            this.lbl_WCS14_CTRL1_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label74
            // 
            this.label74.BackColor = System.Drawing.SystemColors.Highlight;
            this.label74.ForeColor = System.Drawing.Color.White;
            this.label74.Location = new System.Drawing.Point(66, 282);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(135, 16);
            this.label74.TabIndex = 176;
            this.label74.Text = "Reset";
            this.label74.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label75
            // 
            this.label75.BackColor = System.Drawing.SystemColors.Highlight;
            this.label75.ForeColor = System.Drawing.Color.White;
            this.label75.Location = new System.Drawing.Point(66, 264);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(135, 16);
            this.label75.TabIndex = 175;
            this.label75.Text = "Start/Stop";
            this.label75.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_ST2_Position
            // 
            this.lbl_WCS14_ST2_Position.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_ST2_Position.Location = new System.Drawing.Point(204, 228);
            this.lbl_WCS14_ST2_Position.Name = "lbl_WCS14_ST2_Position";
            this.lbl_WCS14_ST2_Position.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_ST2_Position.TabIndex = 174;
            this.lbl_WCS14_ST2_Position.Text = "0";
            this.lbl_WCS14_ST2_Position.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.SystemColors.Highlight;
            this.label56.ForeColor = System.Drawing.Color.White;
            this.label56.Location = new System.Drawing.Point(66, 228);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(135, 16);
            this.label56.TabIndex = 173;
            this.label56.Text = "현재위치";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_ST2_7
            // 
            this.lbl_WCS14_ST2_7.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_ST2_7.Location = new System.Drawing.Point(204, 210);
            this.lbl_WCS14_ST2_7.Name = "lbl_WCS14_ST2_7";
            this.lbl_WCS14_ST2_7.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_ST2_7.TabIndex = 172;
            this.lbl_WCS14_ST2_7.Text = "0";
            this.lbl_WCS14_ST2_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_ST1_Speed
            // 
            this.lbl_WCS14_ST1_Speed.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_ST1_Speed.Location = new System.Drawing.Point(204, 192);
            this.lbl_WCS14_ST1_Speed.Name = "lbl_WCS14_ST1_Speed";
            this.lbl_WCS14_ST1_Speed.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_ST1_Speed.TabIndex = 171;
            this.lbl_WCS14_ST1_Speed.Text = "0";
            this.lbl_WCS14_ST1_Speed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.SystemColors.Highlight;
            this.label39.ForeColor = System.Drawing.Color.White;
            this.label39.Location = new System.Drawing.Point(66, 210);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(135, 16);
            this.label39.TabIndex = 170;
            this.label39.Text = "Error 유무";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.SystemColors.Highlight;
            this.label41.ForeColor = System.Drawing.Color.White;
            this.label41.Location = new System.Drawing.Point(66, 192);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(135, 16);
            this.label41.TabIndex = 169;
            this.label41.Text = "주행속도";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_ST1_2
            // 
            this.lbl_WCS14_ST1_2.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_ST1_2.Location = new System.Drawing.Point(204, 174);
            this.lbl_WCS14_ST1_2.Name = "lbl_WCS14_ST1_2";
            this.lbl_WCS14_ST1_2.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_ST1_2.TabIndex = 168;
            this.lbl_WCS14_ST1_2.Text = "0";
            this.lbl_WCS14_ST1_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_ST1_3
            // 
            this.lbl_WCS14_ST1_3.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_ST1_3.Location = new System.Drawing.Point(204, 156);
            this.lbl_WCS14_ST1_3.Name = "lbl_WCS14_ST1_3";
            this.lbl_WCS14_ST1_3.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_ST1_3.TabIndex = 167;
            this.lbl_WCS14_ST1_3.Text = "0";
            this.lbl_WCS14_ST1_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.SystemColors.Highlight;
            this.label52.ForeColor = System.Drawing.Color.White;
            this.label52.Location = new System.Drawing.Point(66, 174);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(135, 16);
            this.label52.TabIndex = 166;
            this.label52.Text = "FD#1 화물유무";
            this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.SystemColors.Highlight;
            this.label54.ForeColor = System.Drawing.Color.White;
            this.label54.Location = new System.Drawing.Point(66, 156);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(135, 16);
            this.label54.TabIndex = 165;
            this.label54.Text = "FD#2 화물유무";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_ST1_4
            // 
            this.lbl_WCS14_ST1_4.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_ST1_4.Location = new System.Drawing.Point(204, 138);
            this.lbl_WCS14_ST1_4.Name = "lbl_WCS14_ST1_4";
            this.lbl_WCS14_ST1_4.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_ST1_4.TabIndex = 164;
            this.lbl_WCS14_ST1_4.Text = "0";
            this.lbl_WCS14_ST1_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_ST1_5
            // 
            this.lbl_WCS14_ST1_5.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_ST1_5.Location = new System.Drawing.Point(204, 120);
            this.lbl_WCS14_ST1_5.Name = "lbl_WCS14_ST1_5";
            this.lbl_WCS14_ST1_5.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_ST1_5.TabIndex = 163;
            this.lbl_WCS14_ST1_5.Text = "0";
            this.lbl_WCS14_ST1_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.SystemColors.Highlight;
            this.label29.ForeColor = System.Drawing.Color.White;
            this.label29.Location = new System.Drawing.Point(66, 138);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(135, 16);
            this.label29.TabIndex = 162;
            this.label29.Text = "F/D Over Time";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.SystemColors.Highlight;
            this.label33.ForeColor = System.Drawing.Color.White;
            this.label33.Location = new System.Drawing.Point(66, 120);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(135, 16);
            this.label33.TabIndex = 161;
            this.label33.Text = "자동상태";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_ST1_6
            // 
            this.lbl_WCS14_ST1_6.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_ST1_6.Location = new System.Drawing.Point(204, 102);
            this.lbl_WCS14_ST1_6.Name = "lbl_WCS14_ST1_6";
            this.lbl_WCS14_ST1_6.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_ST1_6.TabIndex = 160;
            this.lbl_WCS14_ST1_6.Text = "0";
            this.lbl_WCS14_ST1_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_ST1_7
            // 
            this.lbl_WCS14_ST1_7.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_ST1_7.Location = new System.Drawing.Point(204, 84);
            this.lbl_WCS14_ST1_7.Name = "lbl_WCS14_ST1_7";
            this.lbl_WCS14_ST1_7.Size = new System.Drawing.Size(70, 16);
            this.lbl_WCS14_ST1_7.TabIndex = 159;
            this.lbl_WCS14_ST1_7.Text = "0";
            this.lbl_WCS14_ST1_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.SystemColors.Highlight;
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(66, 102);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(135, 16);
            this.label25.TabIndex = 158;
            this.label25.Text = "화물유무";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.SystemColors.Highlight;
            this.label26.ForeColor = System.Drawing.Color.White;
            this.label26.Location = new System.Drawing.Point(66, 84);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(135, 16);
            this.label26.TabIndex = 157;
            this.label26.Text = "명령대기";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_ControlRx_Count
            // 
            this.lbl_WCS14_ControlRx_Count.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_ControlRx_Count.Location = new System.Drawing.Point(149, 48);
            this.lbl_WCS14_ControlRx_Count.Name = "lbl_WCS14_ControlRx_Count";
            this.lbl_WCS14_ControlRx_Count.Size = new System.Drawing.Size(125, 16);
            this.lbl_WCS14_ControlRx_Count.TabIndex = 156;
            this.lbl_WCS14_ControlRx_Count.Text = "0";
            this.lbl_WCS14_ControlRx_Count.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS14_StatusRx_Count
            // 
            this.lbl_WCS14_StatusRx_Count.BackColor = System.Drawing.Color.White;
            this.lbl_WCS14_StatusRx_Count.Location = new System.Drawing.Point(149, 30);
            this.lbl_WCS14_StatusRx_Count.Name = "lbl_WCS14_StatusRx_Count";
            this.lbl_WCS14_StatusRx_Count.Size = new System.Drawing.Size(125, 16);
            this.lbl_WCS14_StatusRx_Count.TabIndex = 155;
            this.lbl_WCS14_StatusRx_Count.Text = "0";
            this.lbl_WCS14_StatusRx_Count.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.SystemColors.Highlight;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(11, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(135, 16);
            this.label15.TabIndex = 154;
            this.label15.Text = "제어 수신 카운트";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.SystemColors.Highlight;
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(11, 30);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(135, 16);
            this.label20.TabIndex = 153;
            this.label20.Text = "상태 수신 카운트";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_RTVSt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1576, 895);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_RTVSt";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "RTV 장치 상태";
            this.Load += new System.EventHandler(this.Form_RTVSt_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.gb_Outputgroup.ResumeLayout(false);
            this.gb_Outputgroup.PerformLayout();
            this.gb_Inputgroup.ResumeLayout(false);
            this.gb_Inputgroup.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_DevMode_Auto;
        private System.Windows.Forms.Label lbl_DevMode_Manual;
        private System.Windows.Forms.Label lbl_DevMode_Force;
        private System.Windows.Forms.Label lbl_DevMode_Setup;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_RCS_InterLockIn_1_Index;
        private System.Windows.Forms.Label lbl_RCSSt_0;
        private System.Windows.Forms.Label lbl_Dev_ActionCode;
        private System.Windows.Forms.Label lbl_Dev_Error;
        private System.Windows.Forms.Label lbl_Dev_Emergency;
        private System.Windows.Forms.Label lbl_Dev_InvertorConn;
        private System.Windows.Forms.Label lbl_Dev_Start;
        private System.Windows.Forms.Label lbl_DevEmergencySwitch;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl_Drive_Destination;
        private System.Windows.Forms.Label lbl_Drive_Speed;
        private System.Windows.Forms.Label lbl_Drive_Position;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.Label lbl_DriveSt2_0;
        private System.Windows.Forms.Label lbl_DriveSt1_0;
        private System.Windows.Forms.Label lbl_DriveSt1_1;
        private System.Windows.Forms.Label lbl_DriveSt1_2;
        private System.Windows.Forms.Label lbl_DriveSt1_3;
        private System.Windows.Forms.Label lbl_DriveSt1_4;
        private System.Windows.Forms.Label lbl_CanWork_StationIndex;
        private System.Windows.Forms.Label lbl_DriveSt1_6;
        private System.Windows.Forms.Label lbl_DriveSt1_5;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.Label label118;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox gb_Inputgroup;
        private System.Windows.Forms.RadioButton rb_DIO_DigitalIn_2;
        private System.Windows.Forms.RadioButton rb_DIO_DigitalOut_1;
        private System.Windows.Forms.RadioButton rb_DIO_DigitalIn_1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lbl_IOSt_36;
        private System.Windows.Forms.Label lbl_IOSt_35;
        private System.Windows.Forms.Label lbl_IOSt_34;
        private System.Windows.Forms.Label lbl_IOSt_33;
        private System.Windows.Forms.Label lbl_IO_Title_36;
        private System.Windows.Forms.Label lbl_IO_Title_35;
        private System.Windows.Forms.Label lbl_IO_Title_34;
        private System.Windows.Forms.Label lbl_IO_Title_33;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label lbl_IO_Title_32;
        private System.Windows.Forms.Label lbl_IOSt_32;
        private System.Windows.Forms.Label label230;
        private System.Windows.Forms.Label label229;
        private System.Windows.Forms.Label label228;
        private System.Windows.Forms.Label label227;
        private System.Windows.Forms.Label lbl_IO_Title_28;
        private System.Windows.Forms.Label lbl_IO_Title_29;
        private System.Windows.Forms.Label lbl_IO_Title_30;
        private System.Windows.Forms.Label lbl_IO_Title_31;
        private System.Windows.Forms.Label lbl_IOSt_28;
        private System.Windows.Forms.Label lbl_IOSt_29;
        private System.Windows.Forms.Label lbl_IOSt_30;
        private System.Windows.Forms.Label lbl_IOSt_31;
        private System.Windows.Forms.Label lbl_IOSt_16;
        private System.Windows.Forms.Label lbl_IOSt_15;
        private System.Windows.Forms.Label lbl_IOSt_14;
        private System.Windows.Forms.Label lbl_IOSt_13;
        private System.Windows.Forms.Label label289;
        private System.Windows.Forms.Label lbl_IOSt_12;
        private System.Windows.Forms.Label label241;
        private System.Windows.Forms.Label lbl_IOSt_11;
        private System.Windows.Forms.Label label240;
        private System.Windows.Forms.Label lbl_IOSt_10;
        private System.Windows.Forms.Label label239;
        private System.Windows.Forms.Label lbl_IOSt_9;
        private System.Windows.Forms.Label label238;
        private System.Windows.Forms.Label lbl_IOSt_8;
        private System.Windows.Forms.Label label237;
        private System.Windows.Forms.Label lbl_IOSt_7;
        private System.Windows.Forms.Label label236;
        private System.Windows.Forms.Label lbl_IOSt_6;
        private System.Windows.Forms.Label label235;
        private System.Windows.Forms.Label lbl_IOSt_5;
        private System.Windows.Forms.Label label234;
        private System.Windows.Forms.Label lbl_IOSt_4;
        private System.Windows.Forms.Label label233;
        private System.Windows.Forms.Label lbl_IOSt_3;
        private System.Windows.Forms.Label label232;
        private System.Windows.Forms.Label lbl_IOSt_2;
        private System.Windows.Forms.Label label231;
        private System.Windows.Forms.Label lbl_IOSt_1;
        private System.Windows.Forms.Label lbl_IO_Title_16;
        private System.Windows.Forms.Label lbl_IO_Title_15;
        private System.Windows.Forms.Label lbl_IO_Title_14;
        private System.Windows.Forms.Label lbl_IO_Title_13;
        private System.Windows.Forms.Label lbl_IO_Title_17;
        private System.Windows.Forms.Label lbl_IO_Title_12;
        private System.Windows.Forms.Label lbl_IO_Title_18;
        private System.Windows.Forms.Label lbl_IO_Title_11;
        private System.Windows.Forms.Label lbl_IO_Title_19;
        private System.Windows.Forms.Label lbl_IO_Title_10;
        private System.Windows.Forms.Label lbl_IO_Title_20;
        private System.Windows.Forms.Label lbl_IO_Title_9;
        private System.Windows.Forms.Label lbl_IO_Title_21;
        private System.Windows.Forms.Label lbl_IO_Title_8;
        private System.Windows.Forms.Label lbl_IO_Title_22;
        private System.Windows.Forms.Label lbl_IO_Title_7;
        private System.Windows.Forms.Label lbl_IO_Title_23;
        private System.Windows.Forms.Label lbl_IO_Title_6;
        private System.Windows.Forms.Label lbl_IO_Title_24;
        private System.Windows.Forms.Label lbl_IO_Title_5;
        private System.Windows.Forms.Label lbl_IO_Title_25;
        private System.Windows.Forms.Label lbl_IO_Title_4;
        private System.Windows.Forms.Label lbl_IO_Title_26;
        private System.Windows.Forms.Label lbl_IO_Title_3;
        private System.Windows.Forms.Label lbl_IO_Title_27;
        private System.Windows.Forms.Label lbl_IO_Title_2;
        private System.Windows.Forms.Label lbl_IO_Title_1;
        private System.Windows.Forms.Label label274;
        private System.Windows.Forms.Label label275;
        private System.Windows.Forms.Label label276;
        private System.Windows.Forms.Label lbl_IOSt_17;
        private System.Windows.Forms.Label label277;
        private System.Windows.Forms.Label lbl_IOSt_18;
        private System.Windows.Forms.Label label278;
        private System.Windows.Forms.Label lbl_IOSt_19;
        private System.Windows.Forms.Label label279;
        private System.Windows.Forms.Label lbl_IOSt_20;
        private System.Windows.Forms.Label label280;
        private System.Windows.Forms.Label lbl_IOSt_21;
        private System.Windows.Forms.Label label281;
        private System.Windows.Forms.Label lbl_IOSt_22;
        private System.Windows.Forms.Label label282;
        private System.Windows.Forms.Label lbl_IOSt_23;
        private System.Windows.Forms.Label label283;
        private System.Windows.Forms.Label lbl_IOSt_24;
        private System.Windows.Forms.Label label284;
        private System.Windows.Forms.Label lbl_IOSt_25;
        private System.Windows.Forms.Label label285;
        private System.Windows.Forms.Label lbl_IOSt_26;
        private System.Windows.Forms.Label label286;
        private System.Windows.Forms.Label lbl_IOSt_27;
        private System.Windows.Forms.Label label287;
        private System.Windows.Forms.Label label288;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label lblSystemTimeUTC;
        private System.Windows.Forms.Label lbl_Dev_Maintance;
        private System.Windows.Forms.Label lbl_Dev_Home;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label lbl_RCS_InterLockOut_4_Data;
        private System.Windows.Forms.Label lbl_RCS_InterLockOut_3_Data;
        private System.Windows.Forms.Label lbl_RCS_InterLockOut_2_Data;
        private System.Windows.Forms.Label lbl_RCS_InterLockOut_1_Data;
        private System.Windows.Forms.Label lbl_RCS_InterLockOut_4_Index;
        private System.Windows.Forms.Label lbl_RCS_InterLockOut_3_Index;
        private System.Windows.Forms.Label lbl_RCS_InterLockOut_2_Index;
        private System.Windows.Forms.Label lbl_RCS_InterLockOut_1_Index;
        private System.Windows.Forms.Label lbl_RCS_InterLockIn_4_Data;
        private System.Windows.Forms.Label lbl_RCS_InterLockIn_3_Data;
        private System.Windows.Forms.Label lbl_RCS_InterLockIn_2_Data;
        private System.Windows.Forms.Label lbl_RCS_InterLockIn_1_Data;
        private System.Windows.Forms.Label lbl_RCS_InterLockIn_4_Index;
        private System.Windows.Forms.Label lbl_RCS_InterLockIn_3_Index;
        private System.Windows.Forms.Label lbl_RCS_InterLockIn_2_Index;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lbl_Drive_CurrentPos_Feed2;
        private System.Windows.Forms.Label lbl_Drive_CurrentPos_Feed1;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label lbl_Feed2St2_1;
        private System.Windows.Forms.Label lbl_Feed1St2_1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbl_Feed2St1_5;
        private System.Windows.Forms.Label lbl_Feed1St1_5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_Feed2St1_6;
        private System.Windows.Forms.Label lbl_Feed1St1_6;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label lbl_Feed2St1_4;
        private System.Windows.Forms.Label lbl_Feed1St1_4;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label lbl_Feed2St2_0;
        private System.Windows.Forms.Label lbl_Feed2St1_0;
        private System.Windows.Forms.Label lbl_Feed2St1_1;
        private System.Windows.Forms.Label lbl_Feed2St1_2;
        private System.Windows.Forms.Label lbl_Feed2St1_3;
        private System.Windows.Forms.Label lbl_Feed2_Dest;
        private System.Windows.Forms.Label lbl_Feed2_Pos;
        private System.Windows.Forms.Label lbl_Feed1St2_0;
        private System.Windows.Forms.Label lbl_Feed1St1_0;
        private System.Windows.Forms.Label lbl_Feed1St1_1;
        private System.Windows.Forms.Label lbl_Feed1St1_2;
        private System.Windows.Forms.Label lbl_Feed1St1_3;
        private System.Windows.Forms.Label lbl_Feed1_Dest;
        private System.Windows.Forms.Label lbl_Feed1_Pos;
        private System.Windows.Forms.Label label170;
        private System.Windows.Forms.Label label171;
        private System.Windows.Forms.Label label172;
        private System.Windows.Forms.Label label173;
        private System.Windows.Forms.Label label174;
        private System.Windows.Forms.Label label177;
        private System.Windows.Forms.Label label178;
        private System.Windows.Forms.Label lbl_DriveSt2_1;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label lbl_Feed1_Speed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_Feed2_Speed;
        private System.Windows.Forms.RadioButton rb_DIO_DigitalOut_2;
        private System.Windows.Forms.Label lbl_Drive_DestSpeed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_Feed2_DestSpeed;
        private System.Windows.Forms.Label lbl_Feed1_DestSpeed;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_IOSt_37;
        private System.Windows.Forms.Label lbl_IO_Title_37;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lbl_Key_1_0;
        private System.Windows.Forms.Label lbl_Key_1_1;
        private System.Windows.Forms.Label lbl_Key_1_2;
        private System.Windows.Forms.Label lbl_Key_1_3;
        private System.Windows.Forms.Label lbl_Key_1_4;
        private System.Windows.Forms.Label lbl_Key_2_0;
        private System.Windows.Forms.Label lbl_Key_2_1;
        private System.Windows.Forms.Label lbl_Key_2_2;
        private System.Windows.Forms.Label lbl_Key_2_3;
        private System.Windows.Forms.Label lbl_Key_2_4;
        private System.Windows.Forms.Label lbl_Key_2_5;
        private System.Windows.Forms.Label lbl_Key_1_5;
        private System.Windows.Forms.Label lbl_Key_1_6;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label lbl_WCS14_CTRL2_Dest;
        private System.Windows.Forms.Label lbl_WCS14_CTRL1_0;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label lbl_WCS14_CTRL1_2;
        private System.Windows.Forms.Label lbl_WCS14_CTRL1_3;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label lbl_WCS14_CTRL1_4;
        private System.Windows.Forms.Label lbl_WCS14_CTRL1_5;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label lbl_WCS14_CTRL1_6;
        private System.Windows.Forms.Label lbl_WCS14_CTRL1_7;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label lbl_WCS14_ST2_Position;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label lbl_WCS14_ST2_7;
        private System.Windows.Forms.Label lbl_WCS14_ST1_Speed;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label lbl_WCS14_ST1_2;
        private System.Windows.Forms.Label lbl_WCS14_ST1_3;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label lbl_WCS14_ST1_4;
        private System.Windows.Forms.Label lbl_WCS14_ST1_5;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label lbl_WCS14_ST1_6;
        private System.Windows.Forms.Label lbl_WCS14_ST1_7;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lbl_WCS14_ControlRx_Count;
        private System.Windows.Forms.Label lbl_WCS14_StatusRx_Count;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lbl_RTV_RailType;
        private System.Windows.Forms.Label lblHOGINum;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblGroupNum;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblProjectNo;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label lbl_DriveFrontAreaInfo_RegionSt_0;
        private System.Windows.Forms.Label lbl_DriveFrontAreaInfo_RegionSt_1;
        private System.Windows.Forms.Label lbl_DriveFrontAreaInfo_RegionSt_2;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label lbl_DriveBarcodeErrCount;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label lbl_Drive_CurrentStation_Feed2;
        private System.Windows.Forms.Label lbl_Drive_CurrentStation_Feed1;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Button btnBarCodeErrCountInit;
        private System.Windows.Forms.Label lbl_OSt_37;
        private System.Windows.Forms.Label lbl_O_Title_37;
        private System.Windows.Forms.Label lbl_OSt_36;
        private System.Windows.Forms.Label lbl_OSt_35;
        private System.Windows.Forms.Label lbl_OSt_34;
        private System.Windows.Forms.Label lbl_OSt_33;
        private System.Windows.Forms.Label lbl_O_Title_36;
        private System.Windows.Forms.Label lbl_O_Title_35;
        private System.Windows.Forms.Label lbl_O_Title_34;
        private System.Windows.Forms.Label lbl_O_Title_33;
        private System.Windows.Forms.Label lbl_O_Title_32;
        private System.Windows.Forms.Label lbl_OSt_32;
        private System.Windows.Forms.Label lbl_O_Title_28;
        private System.Windows.Forms.Label lbl_O_Title_29;
        private System.Windows.Forms.Label lbl_O_Title_30;
        private System.Windows.Forms.Label lbl_O_Title_31;
        private System.Windows.Forms.Label lbl_OSt_28;
        private System.Windows.Forms.Label lbl_OSt_29;
        private System.Windows.Forms.Label lbl_OSt_30;
        private System.Windows.Forms.Label lbl_OSt_31;
        private System.Windows.Forms.Label lbl_OSt_16;
        private System.Windows.Forms.Label lbl_OSt_15;
        private System.Windows.Forms.Label lbl_OSt_14;
        private System.Windows.Forms.Label lbl_OSt_13;
        private System.Windows.Forms.Label lbl_OSt_12;
        private System.Windows.Forms.Label lbl_OSt_11;
        private System.Windows.Forms.Label lbl_OSt_10;
        private System.Windows.Forms.Label lbl_OSt_9;
        private System.Windows.Forms.Label lbl_OSt_8;
        private System.Windows.Forms.Label lbl_OSt_7;
        private System.Windows.Forms.Label lbl_OSt_6;
        private System.Windows.Forms.Label lbl_OSt_5;
        private System.Windows.Forms.Label lbl_OSt_4;
        private System.Windows.Forms.Label lbl_OSt_3;
        private System.Windows.Forms.Label lbl_OSt_2;
        private System.Windows.Forms.Label lbl_OSt_1;
        private System.Windows.Forms.Label lbl_O_Title_16;
        private System.Windows.Forms.Label lbl_O_Title_15;
        private System.Windows.Forms.Label lbl_O_Title_14;
        private System.Windows.Forms.Label lbl_O_Title_13;
        private System.Windows.Forms.Label lbl_O_Title_17;
        private System.Windows.Forms.Label lbl_O_Title_12;
        private System.Windows.Forms.Label lbl_O_Title_18;
        private System.Windows.Forms.Label lbl_O_Title_11;
        private System.Windows.Forms.Label lbl_O_Title_19;
        private System.Windows.Forms.Label lbl_O_Title_10;
        private System.Windows.Forms.Label lbl_O_Title_20;
        private System.Windows.Forms.Label lbl_O_Title_9;
        private System.Windows.Forms.Label lbl_O_Title_21;
        private System.Windows.Forms.Label lbl_O_Title_8;
        private System.Windows.Forms.Label lbl_O_Title_22;
        private System.Windows.Forms.Label lbl_O_Title_7;
        private System.Windows.Forms.Label lbl_O_Title_23;
        private System.Windows.Forms.Label lbl_O_Title_6;
        private System.Windows.Forms.Label lbl_O_Title_24;
        private System.Windows.Forms.Label lbl_O_Title_5;
        private System.Windows.Forms.Label lbl_O_Title_25;
        private System.Windows.Forms.Label lbl_O_Title_4;
        private System.Windows.Forms.Label lbl_O_Title_26;
        private System.Windows.Forms.Label lbl_O_Title_3;
        private System.Windows.Forms.Label lbl_O_Title_27;
        private System.Windows.Forms.Label lbl_O_Title_2;
        private System.Windows.Forms.Label lbl_O_Title_1;
        private System.Windows.Forms.Label lbl_OSt_17;
        private System.Windows.Forms.Label lbl_OSt_18;
        private System.Windows.Forms.Label lbl_OSt_19;
        private System.Windows.Forms.Label lbl_OSt_20;
        private System.Windows.Forms.Label lbl_OSt_21;
        private System.Windows.Forms.Label lbl_OSt_22;
        private System.Windows.Forms.Label lbl_OSt_23;
        private System.Windows.Forms.Label lbl_OSt_24;
        private System.Windows.Forms.Label lbl_OSt_25;
        private System.Windows.Forms.Label lbl_OSt_26;
        private System.Windows.Forms.Label lbl_OSt_27;
        private System.Windows.Forms.GroupBox gb_Outputgroup;
        private System.Windows.Forms.Label lbl_OSt_38;
        private System.Windows.Forms.Label lbl_O_Title_38;
        private System.Windows.Forms.Label lbl_IOSt_38;
        private System.Windows.Forms.Label lbl_IO_Title_38;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.RadioButton rb_DIO_DigitalIn_3;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label lbl_DevmodeSwitch_1;
        private System.Windows.Forms.Label lbl_DevmodeSwitch_0;
        private System.Windows.Forms.Label lbl_DriveSt2_2;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label lbl_Dev_FanFault;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label lbl_Dev_AlarmCodeType;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.RadioButton rb_DIO_DigitalOut_3;
        private System.Windows.Forms.Label lbl_DriveRearAreaInfo_RegionSt_0;
        private System.Windows.Forms.Label lbl_DriveRearAreaInfo_RegionSt_1;
        private System.Windows.Forms.Label lbl_DriveRearAreaInfo_RegionSt_2;
    }
}
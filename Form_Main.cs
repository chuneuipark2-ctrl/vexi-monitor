using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;   //DllImport
using System.Threading;
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_Main : Form
    {
        #region variable
        private string WCIP;
        private string CONFIG_FILE;
        public bool IsAdmin = false;
        public bool IsPGInit = false;
        private DateTime AdminLoginView;

        public static DEVLogManager devLogManager;
        public VEXI_DEFS.TSRM_ToTalFile SRM_ToTalFile = new VEXI_DEFS.TSRM_ToTalFile();
        public VEXI_DEFS.TRTV_ToTalFile RTV_ToTalFile = new VEXI_DEFS.TRTV_ToTalFile();
        public VEXI_DEFS.TEMS_ToTalFile EMS_ToTalFile = new VEXI_DEFS.TEMS_ToTalFile();
        public Global_Class GlobalObj = new Global_Class();
        public TCOMMDataManager COMMDataManager = new TCOMMDataManager(Application.StartupPath);
        public List<Thread> ThreadList = new List<Thread>();
        public List<int> GLoopCnt = new List<int>();
        public List<int> GLoopCnt_backup = new List<int>();

        
        Form_ComDataDP form_ComDataDP;

        Form_BasicSt form_DevBasicSt;
        Form_DevLog form_DevLog;
        Form_DevEventLog form_DevEventLog;
        Form_DevDebugLog form_DevDebugLog;
        Form_OpInfo form_OpInfo;
        Form_RackInitial form_RackInitial;
        Form_TimeInfo form_TimeInfo;
        Form_Graph form_Graph;
        Form_Developer form_DeveloperTest;
        Form_DO_TestCtrl form_DO_TestCtrl;
        Form_IOStructureSet form_IOStructureSet;
        Form_FWDownload form_Download;

        Form_SRMSt form_SRMSt;
        Form_SRM_CTL form_SRM_CTL;
        Form_SRMConfig form_SRMConfig;
        Form_SRMParam_CTRL form_srmParam_Ctrl;
        Form_SRMDriveParam_Speed form_srmParam_Drive;
        Form_SRMLiftParam_Speed form_srmParam_Lift;
        Form_SRMForkParam_Speed form_srmParam_Fork;
        Form_SRMRack form_SRMRack;
        Form_SRMSpecialRack form_srmSpecialRack;
        Form_SRMInhibitionRack form_srmInhibitionRack;
        Form_SRMInvertorParam form_SRMInvertorParam;
        Form_SRMTotal form_SRMTotal;
        Form_SRMInvertorSt form_SRMInvertorSt;

        Form_RTVSt form_RTVSt;
        Form_RTV_CTL form_RTV_CTL;
        Form_RTVConfig form_RTVConfig;
        Form_RTVParam_CTRL form_rtvParam_Ctrl;
        Form_RTVDriveParam_Speed form_rtvParam_Drive;
        Form_RTVFeedParam_Speed form_rtvParam_Feed;
        Form_RTVRack form_RTVRack;
        Form_RTVTotal form_RTVTotal;
        Form_EMSRTVInvertorSt form_EMSRTVInvertorSt;
        Form_MovexWCSMemoryMap form_MovexWCSMemoryMap;


        Form_EMSSt form_EMSSt;
        Form_EMS_CTL form_EMS_CTL;
        Form_EMSConfig form_EMSConfig;
        Form_EMSParam_CTRL form_emsParam_Ctrl;
        Form_EMSDriveParam_Speed form_emsParam_Drive;
        Form_EMSLiftParam_Speed form_emsParam_Lift;
        Form_EMSTotal form_EMSTotal;
        Form_EMSRack form_EMSRack;

        private byte DebugValue_1;
        #endregion

        public Form_Main()
        {
            InitializeComponent();

            //GlobalObj 객체에 기본 폴더 셋팅
            GlobalObj.RootDIR = Application.StartupPath;
        }

        #region 컴포넌트 이벤트
        private void Form_Main_Load(object sender, EventArgs e)
        {
            //setup.ini 파일 경로

            CONFIG_FILE = Application.StartupPath + "\\CONFIG\\Setup.ini";

            if (!Global_Class.UTIL_Dir_exists(Application.StartupPath + "\\CONFIG\\"))
            {
                Global_Class.UTIL_Dir_create(Application.StartupPath + "\\CONFIG\\");
            }


            //바로가기 실행시 AW 파라미터로 실행하면 ISAdmin 모드 True.
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 2)
            {
                //GlobalObj.MsgBox_Confirm_OK("args.Length == 2");
                //GlobalObj.MsgBox_Confirm_OK(args[1].ToUpper());
                IsAdmin = (args[1].ToUpper() == "AW");
            }

            //MenuItem_Graph.Visible = false; //GR
            //MenuItem_RunTime.Visible = false; //RT
            //menu_EventLog.Visible = false;  //EL
            //menu_DebugLog.Visible = false; //DL
            //menu_ViewCommData.Visible = false; //CD
            //menu_RackInitial.Visible = IsAdmin;

            // 통신객체 구성
            COMMDataManager.ADD_DataBuffer(ConstClass.COMM_UDP);
            COMMDataManager.ADD_DataBuffer(ConstClass.COMM_SERIAL);
            COMMDataManager.ADD_DataBuffer(ConstClass.COMM_TEST);
            //COMMDataManager.OnCommDataReceived += OnMainCommDataReceived;
            COMMDataManager.OnDebugging += OnMainDebugging;
            COMMDataManager.OnPacketReceived += OnMainPacketReceived;
            COMMDataManager.OnPacketSended += OnMainPacketSended;
            // 송신 폴링(uCommClass, 약 200ms)에서 호출 — 조그 중 CMD2_80 유지. Do_JogCtrl 주석 참고.
            COMMDataManager.OnCheckJogCtrl += Do_JogCtrl;


            //연결장치 컴포넌트 초기화
            cbDevType.SelectedIndex = 0;
            cbDevID.SelectedIndex = 0;

            //시리얼포트리스트 구성 갱신
            Refresh_Comports();

            //Setup.ini 에서 환경설정 내용 불러오기
            LoadSystemConfig();

            // INI에 IP·장치가 있으면 시작 시 UDP 오픈(조그/폴링이 CommSt!=0을 요구). 실패해도 무시.
            TryConnectUdpFromCurrentSettings(false);

            //UDP 통신 Thread 1개 생성
            Create_Thread(1);

            DPTimer.Enabled = true;
            Loggingtimer.Enabled = true;
            IsPGInit = true;
        }

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DPTimer.Enabled = false;
            Loggingtimer.Enabled = false;
            SaveSystemConfig();
            Thread_Abort();
            Thread.Sleep(1000);
        }
        
        private void DPTimer_Tick(object sender, EventArgs e)
        {
            //통신 Open / Close 상태 갱신 : "통신 연결", "끊기" 버튼 이벤트에서만 호출하면 USBToSerial 연결시 USB를 제거하였을 때 계속 포트 open 상태로 표시되는 문제가 있다
            Display_CommSt();
            //장치 통신상태표출 갱신을 위해 호출
            Display_RealDevInfo(false);
        }

        private void cbDevType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckSelectedDevice();
            SaveSystemConfig();
        }

        private void btnCommClose_Click(object sender, EventArgs e)
        {
            COMMDataManager.StopComm();

            Display_CommSt();
            Display_RealDevInfo(false);
            Display_DevErrSt();
            Display_DevFW();
            Display_DevForceMode();
            Display_DevSetupMode();
        }

        private void cbDevID_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckSelectedDevice();
            SaveSystemConfig();
        }

        private void UpdateWifiDeviceIP()
        {
            if (!pnUDPConnectType.Visible)
            {
                return;
            }

            int baseAddress;
            if (rb_2Connect.Checked)
            {
                baseAddress = 100;
            }
            else if (rb_5Connect.Checked)
            {
                baseAddress = 150;
            }
            else
            {
                return;
            }

            int hostId = 1;
            if ((cbDevID.SelectedIndex >= 0) && (cbDevID.SelectedIndex < (cbDevID.Items.Count - 1)))
            {
                hostId = COMMDataManager.SelectDestDevID;
            }

            // Main 폼에는 WiFi 접속용 IP 입력 컨트롤이 edDevIP로 존재하지 않을 수 있어
            // 여기서는 실제 접속 로직이 참조하는 값(WCIP)만 갱신한다.
            WCIP = "192.168.100." + (baseAddress + hostId).ToString();
        }

        private void btn_RefreshComPort_Click(object sender, EventArgs e)
        {
            //시리얼포트리스트 갱신
            Refresh_Comports();
        }

        private void menu_InvertorParameter_Click(object sender, EventArgs e)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    form_SRMInvertorParam = ShowActiveSingleForm(form_SRMInvertorParam, typeof(Form_SRMInvertorParam)) as Form_SRMInvertorParam;

                    form_SRMInvertorParam.form_Main = this;
                    break;
                case ConstClass.TYPE_RTV:
                    break;
                case ConstClass.TYPE_EMS:
                    break;
                default:
                    break;
            }
        }


        private void menu_Developer_Click(object sender, EventArgs e)
        {
            if ((COMMDataManager.RX_DestDevType != ConstClass.TYPE_SRM) &&
                (COMMDataManager.RX_DestDevType != ConstClass.TYPE_RTV) &&
                (COMMDataManager.RX_DestDevType != ConstClass.TYPE_EMS)) return;

            //장치 테스트 상태/제어, Fun 제어창 열기 (MDI)
            //이 창은 개발자가 장치 프로그램을 시험하거나 디버깅하기 위한 용도의 창임
            //추후에는 ISAdmin 속성에 의해 메뉴 Visible 변경할 것
            if (this.ActiveMdiChild != null)
            {
                if (this.ActiveMdiChild != form_DeveloperTest)
                {
                    this.ActiveMdiChild.Close();

                }

            }
            form_DeveloperTest = ShowActiveChildForm(form_DeveloperTest, typeof(Form_Developer)) as Form_Developer;
            form_DeveloperTest.form_Main = this;
        }

        private void menu_AllWIndowsClose_Click(object sender, EventArgs e)
        {
            AllClose_Windows();
        }



        private void menu_TotalSetLoad_Click(object sender, EventArgs e)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    //통신 데이터 표출창 열기 (Single)
                    form_SRMTotal = ShowActiveSingleForm(form_SRMTotal, typeof(Form_SRMTotal)) as Form_SRMTotal;

                    form_SRMTotal.form_Main = this;
                    break;
                case ConstClass.TYPE_RTV:
                    form_RTVTotal = ShowActiveSingleForm(form_RTVTotal, typeof(Form_RTVTotal)) as Form_RTVTotal;

                    form_RTVTotal.form_Main = this;
                    break;
                case ConstClass.TYPE_EMS:
                    form_EMSTotal = ShowActiveSingleForm(form_EMSTotal, typeof(Form_EMSTotal)) as Form_EMSTotal;

                    form_EMSTotal.form_Main = this;
                    break;
                default:
                    break;
            }
        }

        private void menu_CtrlParameter_Click(object sender, EventArgs e)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_srmParam_Ctrl)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }

                    }
                    form_srmParam_Ctrl = ShowActiveChildForm(form_srmParam_Ctrl, typeof(Form_SRMParam_CTRL)) as Form_SRMParam_CTRL;
                    form_srmParam_Ctrl.form_Main = this;
                    break;
                case ConstClass.TYPE_RTV:

                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_rtvParam_Ctrl)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }
                    }
                    form_rtvParam_Ctrl = ShowActiveChildForm(form_rtvParam_Ctrl, typeof(Form_RTVParam_CTRL)) as Form_RTVParam_CTRL;
                    form_rtvParam_Ctrl.form_Main = this;
                    break;
                case ConstClass.TYPE_EMS:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_emsParam_Ctrl)
                        {
                            this.ActiveMdiChild.Close();

                        }
                    }
                    form_emsParam_Ctrl = ShowActiveChildForm(form_emsParam_Ctrl, typeof(Form_EMSParam_CTRL)) as Form_EMSParam_CTRL;
                    form_emsParam_Ctrl.form_Main = this;
                    break;
                default:
                    break;
            }
        }

        private void menu_DriveParameter_Click(object sender, EventArgs e)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    //장치 기본상태 창 열기 (MDI)
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_srmParam_Drive)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }

                    }
                    form_srmParam_Drive = ShowActiveChildForm(form_srmParam_Drive, typeof(Form_SRMDriveParam_Speed)) as Form_SRMDriveParam_Speed;
                    form_srmParam_Drive.form_Main = this;
                    break;
                case ConstClass.TYPE_RTV:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_rtvParam_Drive)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }

                    }
                    form_rtvParam_Drive = ShowActiveChildForm(form_rtvParam_Drive, typeof(Form_RTVDriveParam_Speed)) as Form_RTVDriveParam_Speed;
                    form_rtvParam_Drive.form_Main = this;
                    break;
                case ConstClass.TYPE_EMS:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_emsParam_Drive)
                        {
                            this.ActiveMdiChild.Close();

                        }

                    }
                    form_emsParam_Drive = ShowActiveChildForm(form_emsParam_Drive, typeof(Form_EMSDriveParam_Speed)) as Form_EMSDriveParam_Speed;
                    form_emsParam_Drive.form_Main = this;
                    break;
                default:
                    break;
            }
        }

        private void menu_LiftParameter_Click(object sender, EventArgs e)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    //장치 기본상태 창 열기 (MDI)
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_srmParam_Lift)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }

                    }
                    form_srmParam_Lift = ShowActiveChildForm(form_srmParam_Lift, typeof(Form_SRMLiftParam_Speed)) as Form_SRMLiftParam_Speed;
                    form_srmParam_Lift.form_Main = this;
                    break;
                case ConstClass.TYPE_RTV:
                    break;
                case ConstClass.TYPE_EMS:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_emsParam_Lift)
                        {
                            this.ActiveMdiChild.Close();

                        }

                    }
                    form_emsParam_Lift = ShowActiveChildForm(form_emsParam_Lift, typeof(Form_EMSLiftParam_Speed)) as Form_EMSLiftParam_Speed;
                    form_emsParam_Lift.form_Main = this;
                    break;
                default:
                    break;
            }
        }

        private void menu_ForParameter_Click(object sender, EventArgs e)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_srmParam_Fork)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }

                    }
                    form_srmParam_Fork = ShowActiveChildForm(form_srmParam_Fork, typeof(Form_SRMForkParam_Speed)) as Form_SRMForkParam_Speed;
                    form_srmParam_Fork.form_Main = this;
                    break;
                case ConstClass.TYPE_RTV:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_rtvParam_Feed)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }

                    }
                    form_rtvParam_Feed = ShowActiveChildForm(form_rtvParam_Feed, typeof(Form_RTVFeedParam_Speed)) as Form_RTVFeedParam_Speed;
                    form_rtvParam_Feed.form_Main = this;
                    break;
                case ConstClass.TYPE_EMS:
                    break;
                default:
                    break;
            }
        }

        private void menu_RackBase_Click(object sender, EventArgs e)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_SRMRack)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }

                    }
                    form_SRMRack = ShowActiveChildForm(form_SRMRack, typeof(Form_SRMRack)) as Form_SRMRack;
                    form_SRMRack.form_Main = this;
                    break;
                case ConstClass.TYPE_RTV:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_RTVRack)
                        {
                            this.ActiveMdiChild.Close();
                         
                        }

                    }
                    form_RTVRack = ShowActiveChildForm(form_RTVRack, typeof(Form_RTVRack)) as Form_RTVRack;
                    form_RTVRack.form_Main = this;
                    break;
                case ConstClass.TYPE_EMS:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_EMSRack)
                        {
                            this.ActiveMdiChild.Close();

                        }

                    }
                    form_EMSRack = ShowActiveChildForm(form_EMSRack, typeof(Form_EMSRack)) as Form_EMSRack;
                    form_EMSRack.form_Main = this;
                    break;
                default:
                    break;
            }
        }

        private void menu_SpecialRackSet_Click(object sender, EventArgs e)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    //장치 기본상태 창 열기 (MDI)
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_srmSpecialRack)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }

                    }
                    form_srmSpecialRack = ShowActiveChildForm(form_srmSpecialRack, typeof(Form_SRMSpecialRack)) as Form_SRMSpecialRack;
                    form_srmSpecialRack.form_Main = this;
                    break;
                case ConstClass.TYPE_RTV:
                    break;
                case ConstClass.TYPE_EMS:
                    break;
                default:
                    break;
            }
        }

        private void menu_RackInitial_Click(object sender, EventArgs e)
        {
            frameLogin frmLogging = new frameLogin(); frmLogging.ShowDialog();

            if (frmLogging.DialogResult == DialogResult.OK)
            {
                form_RackInitial = ShowActiveSingleForm(form_RackInitial, typeof(Form_RackInitial)) as Form_RackInitial;
                form_RackInitial.form_Main = this;
            }
        }

        private void menu_NoRackSet_Click(object sender, EventArgs e)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    //장치 기본상태 창 열기 (MDI)
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_srmInhibitionRack)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }

                    }
                    form_srmInhibitionRack = ShowActiveChildForm(form_srmInhibitionRack, typeof(Form_SRMInhibitionRack)) as Form_SRMInhibitionRack;
                    form_srmInhibitionRack.form_Main = this;
                    break;
                case ConstClass.TYPE_RTV:
                    break;
                case ConstClass.TYPE_EMS:
                    break;
                default:
                    break;
            }
        }

        private void menu_ViewCommData_Click(object sender, EventArgs e)
        {
            //통신 데이터 표출창 열기 (Single)
            form_ComDataDP = ShowActiveSingleForm(form_ComDataDP, typeof(Form_ComDataDP)) as Form_ComDataDP;

            form_ComDataDP.form_Main = this;
        }

        private void btn_DevMode_AutoOn_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            if (GlobalObj.MsgBox_Confirm_OKCancel(this, "장치의 운영모드를 변경하시겠습니까?"))
            {
                Do_Ctrl_DevMode(ConstClass.CMD2_58, Convert.ToByte(bt.Tag.ToString()));
            }
        }

        private void MenuItem_DevLog_Click(object sender, EventArgs e)
        {
            if ((COMMDataManager.RX_DestDevType != ConstClass.TYPE_SRM) &&
                (COMMDataManager.RX_DestDevType != ConstClass.TYPE_RTV) &&
                (COMMDataManager.RX_DestDevType != ConstClass.TYPE_EMS)) return;

            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                case ConstClass.TYPE_RTV:
                case ConstClass.TYPE_EMS:

                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_DevLog)
                        {
                            this.ActiveMdiChild.Close();

                        }

                    }
                    form_DevLog = ShowActiveChildForm(form_DevLog, typeof(Form_DevLog)) as Form_DevLog;
                    form_DevLog.form_Main = this;
                    break;
            }
        }


        private void MenuItem_Graph_Click(object sender, EventArgs e)
        {
            if ((COMMDataManager.RX_DestDevType != ConstClass.TYPE_SRM) &&
                (COMMDataManager.RX_DestDevType != ConstClass.TYPE_RTV) &&
                (COMMDataManager.RX_DestDevType != ConstClass.TYPE_EMS)) return;

            if (this.ActiveMdiChild != null)
            {
                if (this.ActiveMdiChild != form_Graph)
                {
                    this.ActiveMdiChild.Close();
                    
                }
                
            }
            form_Graph = ShowActiveChildForm(form_Graph, typeof(Form_Graph)) as Form_Graph;
            form_Graph.form_Main = this;
        }

        private void MenuItem_OpInfo_Click(object sender, EventArgs e)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    form_OpInfo = ShowActiveSingleForm(form_OpInfo, typeof(Form_OpInfo)) as Form_OpInfo;
                    form_OpInfo.form_Main = this;
                    break;
                case ConstClass.TYPE_RTV:
                    break;
                case ConstClass.TYPE_EMS:
                    break;
                default:
                    break;
            }
        }

        private void MenuItem_RunTime_Click(object sender, EventArgs e)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                case ConstClass.TYPE_RTV:
                case ConstClass.TYPE_EMS:
                    form_TimeInfo = ShowActiveSingleForm(form_TimeInfo, typeof(Form_TimeInfo)) as Form_TimeInfo;
                    form_TimeInfo.form_Main = this;
                    break;
                default:
                    break;
            }
        }

        private void MenuItem_basicSt_Click(object sender, EventArgs e)
        {
            if ((COMMDataManager.RX_DestDevType != ConstClass.TYPE_SRM) &&
                (COMMDataManager.RX_DestDevType != ConstClass.TYPE_RTV) &&
                (COMMDataManager.RX_DestDevType != ConstClass.TYPE_EMS)) return;

            //장치 기본상태 창 열기(MDI)
            if (this.ActiveMdiChild != null)
            {
                if (this.ActiveMdiChild != form_DevBasicSt)
                {
                    this.ActiveMdiChild.Close();
                    
                }

            }
            form_DevBasicSt = ShowActiveChildForm(form_DevBasicSt, typeof(Form_BasicSt)) as Form_BasicSt;
            form_DevBasicSt.form_Main = this;
        }

        private void MenuItem_InvertorInfo_Click(object sender, EventArgs e)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_SRMInvertorSt)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }

                    }
                    form_SRMInvertorSt = ShowActiveChildForm(form_SRMInvertorSt, typeof(Form_SRMInvertorSt)) as Form_SRMInvertorSt;
                    form_SRMInvertorSt.form_Main = this;
                    break;
                case ConstClass.TYPE_RTV:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_EMSRTVInvertorSt)
                        {
                            this.ActiveMdiChild.Close();

                        }

                    }
                    form_EMSRTVInvertorSt = ShowActiveChildForm(form_EMSRTVInvertorSt, typeof(Form_EMSRTVInvertorSt)) as Form_EMSRTVInvertorSt;
                    form_EMSRTVInvertorSt.form_Main = this;
                    break;
                case ConstClass.TYPE_EMS:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_EMSRTVInvertorSt)
                        {
                            this.ActiveMdiChild.Close();

                        }

                    }
                    form_EMSRTVInvertorSt = ShowActiveChildForm(form_EMSRTVInvertorSt, typeof(Form_EMSRTVInvertorSt)) as Form_EMSRTVInvertorSt;
                    form_EMSRTVInvertorSt.form_Main = this;
                    break;
                default:
                    break;
            }
        }

        private void menu_MCU_IO_Config_Click(object sender, EventArgs e)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_IOStructureSet)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }

                    }
                    form_IOStructureSet = ShowActiveChildForm(form_IOStructureSet, typeof(Form_IOStructureSet)) as Form_IOStructureSet;
                    form_IOStructureSet.form_Main = this;
                    break;
                case ConstClass.TYPE_RTV:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_IOStructureSet)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }

                    }
                    form_IOStructureSet = ShowActiveChildForm(form_IOStructureSet, typeof(Form_IOStructureSet)) as Form_IOStructureSet;
                    form_IOStructureSet.form_Main = this;
                    break;
                case ConstClass.TYPE_EMS:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_IOStructureSet)
                        {
                            this.ActiveMdiChild.Close();

                        }

                    }
                    form_IOStructureSet = ShowActiveChildForm(form_IOStructureSet, typeof(Form_IOStructureSet)) as Form_IOStructureSet;
                    form_IOStructureSet.form_Main = this;
                    break;
                default:
                    break;
            }
        }

        private void Menu_Download_Click(object sender, EventArgs e)
        {
            if ((COMMDataManager.RX_DestDevType != ConstClass.TYPE_SRM) &&
                (COMMDataManager.RX_DestDevType != ConstClass.TYPE_RTV) &&
                (COMMDataManager.RX_DestDevType != ConstClass.TYPE_EMS)) return;

            form_Download = ShowActiveSingleForm(form_Download, typeof(Form_FWDownload)) as Form_FWDownload;
            form_Download.form_Main = this;
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    form_SRM_CTL = ShowActiveSingleForm(form_SRM_CTL, typeof(Form_SRM_CTL)) as Form_SRM_CTL;
                    form_SRM_CTL.form_Main = this;
                    break;
                case ConstClass.TYPE_RTV:
                    form_RTV_CTL = ShowActiveSingleForm(form_RTV_CTL, typeof(Form_RTV_CTL)) as Form_RTV_CTL;
                    form_RTV_CTL.form_Main = this;
                    
                    break;
                case ConstClass.TYPE_EMS:
                    form_EMS_CTL = ShowActiveSingleForm(form_EMS_CTL, typeof(Form_EMS_CTL)) as Form_EMS_CTL;
                    form_EMS_CTL.form_Main = this;

                    break;
                default:
                    break;
            }
        }


        private void menu_MyTest_Click(object sender, EventArgs e)
        {
            
        }

        private void menu_DevConfig_Click(object sender, EventArgs e)
        {
            //수신된 장치 타입에 따라 해당하는 장치의 상태창을 열어준다
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_SRMConfig)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }

                    }
                    form_SRMConfig = ShowActiveChildForm(form_SRMConfig, typeof(Form_SRMConfig)) as Form_SRMConfig;
                    form_SRMConfig.form_Main = this;
                    break;
                case ConstClass.TYPE_RTV:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_RTVConfig)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }

                    }
                    form_RTVConfig = ShowActiveChildForm(form_RTVConfig, typeof(Form_RTVConfig)) as Form_RTVConfig;
                    form_RTVConfig.form_Main = this;
                    break;
                case ConstClass.TYPE_EMS:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_EMSConfig)
                        {
                            this.ActiveMdiChild.Close();

                        }

                    }
                    form_EMSConfig = ShowActiveChildForm(form_EMSConfig, typeof(Form_EMSConfig)) as Form_EMSConfig;
                    form_EMSConfig.form_Main = this;
                    break;
                default:
                    break;
            }
        }


        private void AllClose_Windows()
        {
            while (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
                
            }

            if (form_SRM_CTL != null)
            {
                if (!form_SRM_CTL.IsDisposed)
                {
                    //form_SRM_CTL.Dispose();
                    form_SRM_CTL.Close();
                }
            }

            if (form_SRMTotal != null)
            {
                if (!form_SRMTotal.IsDisposed)
                {
                    //form_SRMTotal.Dispose();
                    form_SRMTotal.Close();
                }
            }

            if (form_SRMInvertorParam != null)
            {
                if (!form_SRMInvertorParam.IsDisposed)
                {
                    //form_SRMInvertorParam.Dispose();
                    form_SRMInvertorParam.Close();
                }
            }

            if (form_OpInfo != null)
            {
                if (!form_OpInfo.IsDisposed)
                {
                    //form_OpInfo.Dispose();
                    form_OpInfo.Close();
                }
            }

            if (form_RackInitial != null)
            {
                if (!form_RackInitial.IsDisposed)
                {
                    //form_RackInitial.Dispose();
                    form_RackInitial.Close();
                }
            }

            

            if (form_TimeInfo != null)
            {
                if (!form_TimeInfo.IsDisposed)
                {
                    //form_TimeInfo.Dispose();
                    form_TimeInfo.Close();
                }
            }

            if (form_Download != null)
            {
                if (!form_Download.IsDisposed)
                {
                    //form_Download.Dispose();
                    form_Download.Close();
                }
            }

            if (form_RTV_CTL != null)
            {
                if (!form_RTV_CTL.IsDisposed)
                {
                    form_RTV_CTL.Close();
                }
            }

            if (form_RTVTotal != null)
            {
                if (!form_RTVTotal.IsDisposed)
                {
                    form_RTVTotal.Close();
                }
            }

            if (form_EMS_CTL != null)
            {
                if (!form_EMS_CTL.IsDisposed)
                {
                    form_EMS_CTL.Close();
                }
            }

            if (form_EMSTotal != null)
            {
                if (!form_EMSTotal.IsDisposed)
                {
                    form_EMSTotal.Close();
                }
            }

        }

        private void menu_DevSt_Click(object sender, EventArgs e)
        {
            //수신된 장치 타입에 따라 해당하는 장치의 상태창을 열어준다
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_SRMSt)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }

                    }
                    form_SRMSt = ShowActiveChildForm(form_SRMSt, typeof(Form_SRMSt)) as Form_SRMSt;
                    form_SRMSt.form_Main = this;
                    break;
                case ConstClass.TYPE_RTV:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_RTVSt)
                        {
                            this.ActiveMdiChild.Close();
                            
                        }

                    }
                    form_RTVSt = ShowActiveChildForm(form_RTVSt, typeof(Form_RTVSt)) as Form_RTVSt;
                    form_RTVSt.form_Main = this;
                    break;
                case ConstClass.TYPE_EMS:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_EMSSt)
                        {
                            this.ActiveMdiChild.Close();

                        }

                    }
                    form_EMSSt = ShowActiveChildForm(form_EMSSt, typeof(Form_EMSSt)) as Form_EMSSt;
                    form_EMSSt.form_Main = this;
                    break;
                default:
                    break;
            }
        }

        /// <summary>INI·UI의 WCIP로 UDP 오픈. 이미 열려 있으면 true. showErrors false면 검증 실패 시 메시지 없이 false.</summary>
        private bool TryConnectUdpFromCurrentSettings(bool showErrors)
        {
            if (COMMDataManager.CommSt != 0)
            {
                return true;
            }

            IPAddress devIp;
            const string caption = "Warnning";
            const MessageBoxButtons buttons = MessageBoxButtons.OK;

            if (pnUDPConnectType.Visible)
            {
                if (!rb_2Connect.Checked && !rb_5Connect.Checked)
                {
                    rb_2Connect.Checked = true;
                }
                UpdateWifiDeviceIP();
            }

            string targetIp = WCIP != null ? WCIP.Trim() : "";
            if (targetIp == "")
            {
                if (showErrors)
                {
                    MessageBox.Show(this, "IP를 설정하여야 합니다", caption, buttons, MessageBoxIcon.Warning);
                }
                return false;
            }

            if (!Global_Class.UTIL_IsValid_IP(targetIp, out devIp))
            {
                if (showErrors)
                {
                    MessageBox.Show(this, "IP를 설정하여야 합니다", caption, buttons, MessageBoxIcon.Warning);
                }
                return false;
            }

            if (pnUDPConnectType.Visible)
            {
                COMMDataManager.RemotePort = ConstClass.WIFIConnect_PORT;
            }
            else
            {
                COMMDataManager.RemotePort = ConstClass.MCUConnect_PORT;
            }

            COMMDataManager.SetCommMode(ConstClass.COMM_UDP, "", targetIp, 8000);
            Display_CommSt();
            return true;
        }

        private void btnCommOpen_Click(object sender, EventArgs e)
        {
            const string caption = "Warnning";
            const MessageBoxButtons buttons = MessageBoxButtons.OK;

            if (pnUDPConnectType.Visible)
            {
                if (!rb_2Connect.Checked && !rb_5Connect.Checked)
                {
                    MessageBox.Show(this, "2.4G 또는 5G 연결을 선택하여야 합니다", caption, buttons, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (!TryConnectUdpFromCurrentSettings(true))
            {
                return;
            }

            CheckSelectedDevice();
            SaveSystemConfig();
        }

        private void Display_CommSt()
        {
            switch (COMMDataManager.CommSt)
            {
                case 0: lblCommStatus.Text = "COMM OFF"; lblCommStatus.BackColor = System.Drawing.Color.Gray; break;
                case 1: lblCommStatus.Text = "COMM Serial (" + COMMDataManager.SerialPort + ")"; lblCommStatus.BackColor = System.Drawing.Color.Yellow; break;
                case 2: if (COMMDataManager.TxErrSt)
                        {
                            lblCommStatus.Text = "COMM UDP"; lblCommStatus.BackColor = System.Drawing.Color.Gray; break;
                        }
                        else
                        {
                            lblCommStatus.Text = "COMM UDP"; lblCommStatus.BackColor = System.Drawing.Color.Yellow; break;
                        };
            }

            if (COMMDataManager.CommSt == 0)
            {
                pnCommSetting.BackColor = System.Drawing.Color.White;
                pnCommSetting.Enabled = true;

                pnQuickCtrl.Visible = false;
            }
            else
            {
                pnCommSetting.BackColor = System.Drawing.Color.Silver;
                pnCommSetting.Enabled = false;

                pnQuickCtrl.Visible = true;
            }
        }

        private void btn_StopEmergency_Click(object sender, EventArgs e)
        {
            Do_Ctrl_Cmd_withNoData(ConstClass.CMD2_55);
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            Do_Ctrl_Cmd_withNoData(ConstClass.CMD2_54);
        }

        #endregion

        #region 쓰레드와 감시 타이머
        private void WatchDog_Tick(object sender, EventArgs e)
        {
            //쓰레드 감시 타이머
            WatchDog.Enabled = false;
            try
            {
                // Thread Status check
                for (int my_i = 0; my_i < ThreadList.Count; my_i++)
                {
                    //*****   자체 run compair  back data  ***********************
                    if ((GLoopCnt[my_i] == GLoopCnt_backup[my_i]) ||
                        (ThreadList[my_i].IsAlive == false))
                    {
                        Thread_Abort(my_i);

                        Thread.Sleep(200);

                        Thread tempthread = new Thread(new ParameterizedThreadStart(Comm_Thread_Do));
                        ThreadList[my_i] = tempthread;
                        ThreadList[my_i].Start(my_i);
                    }
                    else
                    {
                        GLoopCnt_backup[my_i] = GLoopCnt[my_i];
                    }
                }

            }
            finally
            {
                WatchDog.Enabled = true;
            }
        }

        private void Comm_Thread_Do(Object ThreadId)
        {
            //쓰레드 함수 => 30ms마다 UDP_Read를 수행함
            
            int i_no = (int)ThreadId;

            try
            {
                while (true)
                {
                    Thread.Sleep(20);
                    GLoopCnt[i_no]++;
                    COMMDataManager.UDP_Read();

                };
            }
            //catch (Exception er)
            catch
            {

            }
        }

        private void Create_Thread(int ThreadCnt)
        {
            //UDP 수신처리를 위한 쓰레드 생성
            try
            {
                // 통신 Thread 생성
                for (int int_i = 0; int_i < ThreadCnt; int_i++)
                {
                    // 1. 쓰레드 상태 문의 추가
                    ThreadList.Add(new Thread(new ParameterizedThreadStart(Comm_Thread_Do)));

                    // 4. 쓰레드 실행
                    for (int my_i = 0; my_i < ThreadList.Count; my_i++)
                    {
                        ThreadList[int_i].Start(int_i);

                        GLoopCnt.Add(0);
                        GLoopCnt_backup.Add(0);

                        Thread.Sleep(100);
                    }
                }
            }
            finally
            {
                WatchDog.Enabled = true;
            }
        }

        public void Thread_Abort()
        {
            //쓰레드 중지
            try
            {
                for (int my_i = 0; my_i < ThreadList.Count; my_i++)
                {
                    ThreadList[my_i].Abort();
                }
            }
            //catch (Exception er)
            catch
            {
            }
        }

        public void Thread_Abort(int ThreadId)
        {
            //쓰레드 중지
            try
            {
                DebugValue_1++;
                ThreadList[ThreadId].Abort();
            }
            catch (Exception exp)
            {
            }
        }

        #endregion

        #region 이벤트
        public void OnMainPacketSended(byte TmpCommType, byte[] Paketbytes)
        {
            //송신한 데이터를 통신 데이터 표출화면에 표시
            if (form_ComDataDP != null)
            {
                if (!form_ComDataDP.IsDisposed)
                {
                    form_ComDataDP.Display_TxData(Paketbytes);
                }
            }
        }
        public void OnMainDebugging(string CaptionStr, String DebugStr)
        {
            if (form_ComDataDP != null)
            {
                if (!form_ComDataDP.IsDisposed)
                {
                    form_ComDataDP.Display_Debug(CaptionStr, DebugStr);
                }
            }
        }
        public void OnMainPacketReceived(byte TmpCommType, bool CRCOK, TPacketClass PaketObj, byte RevCRC1, byte RevCRC2, byte RevETX)
        {

            COMMDataManager.RX_DestDevType = PaketObj.SrcDevType;
            COMMDataManager.RX_DestDevID = PaketObj.SrcID;

            //연결장치 정보 표출을 위해 호출
            Display_RealDevInfo(true);

            if (form_ComDataDP != null)
            {
                if (!form_ComDataDP.IsDisposed)
                {
                    form_ComDataDP.Display_RxData(CRCOK, PaketObj, RevCRC1, RevCRC2, RevETX);
                }
            }

            if ((PaketObj.CMD1 != ConstClass.CMD1_80) &&
                (PaketObj.CMD1 != ConstClass.CMD1_81) &&
                (PaketObj.CMD1 != ConstClass.CMD1_C0) &&
                (PaketObj.CMD1 != ConstClass.CMD1_C1)) return;

            //수신한 데이터 처리 및  통신 데이터 표출화면에 표시
            //PaketObj.CMD1 은 동일 COMD2 가 있을 때 CMD2 안에서 분기해서 처리 하자.
            switch (PaketObj.CMD2)
            {
                case ConstClass.CMD2_10:
                    if (COMMDataManager.Set_dev_REC_BasicSt(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        //분기가 필요한지는 3가지 장치에 대한 화면 구성이 모두 끝났을 때 결정해서 코드 정리 해야함
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_DevBasicSt != null)
                                {
                                    if (!form_DevBasicSt.IsDisposed)
                                    {
                                        form_DevBasicSt.Display_DevBasicSt();
                                    }
                                };
                                break;
                            case ConstClass.TYPE_RTV:
                                if (form_DevBasicSt != null)
                                {
                                    if (!form_DevBasicSt.IsDisposed)
                                    {
                                        form_DevBasicSt.Display_DevBasicSt();
                                    }
                                };
                                break;
                            case ConstClass.TYPE_EMS:
                                if (form_DevBasicSt != null)
                                {
                                    if (!form_DevBasicSt.IsDisposed)
                                    {
                                        form_DevBasicSt.Display_DevBasicSt();
                                    }
                                };
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConstClass.CMD2_12:
                    if (COMMDataManager.Set_dev_REC_TestSt(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            //분기가 필요한지는 3가지 장치에 대한 화면 구성이 모두 끝났을 때 결정해서 코드 정리 해야함
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_DeveloperTest != null)
                                {
                                    if (!form_DeveloperTest.IsDisposed)
                                    {
                                        form_DeveloperTest.Display_DevTestSt();
                                    }
                                };
                                break;
                            case ConstClass.TYPE_RTV:
                                if (form_DeveloperTest != null)
                                {
                                    if (!form_DeveloperTest.IsDisposed)
                                    {
                                        form_DeveloperTest.Display_DevTestSt();
                                    }
                                };
                                break;
                            case ConstClass.TYPE_EMS:
                                if (form_DeveloperTest != null)
                                {
                                    if (!form_DeveloperTest.IsDisposed)
                                    {
                                        form_DeveloperTest.Display_DevTestSt();
                                    }
                                };
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConstClass.CMD2_3D:
                    if (COMMDataManager.Set_dev_REC_WCSData(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            //분기가 필요한지는 3가지 장치에 대한 화면 구성이 모두 끝났을 때 결정해서 코드 정리 해야함
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                break;
                            case ConstClass.TYPE_RTV:
                                if (form_MovexWCSMemoryMap != null)
                                {
                                    if (!form_MovexWCSMemoryMap.IsDisposed)
                                    {
                                        form_MovexWCSMemoryMap.SET_WCSData(PaketObj.GetDataBytes());
                                    }
                                };
                                break;
                            case ConstClass.TYPE_EMS:
                                if (form_MovexWCSMemoryMap != null)
                                {
                                    if (!form_MovexWCSMemoryMap.IsDisposed)
                                    {
                                        form_MovexWCSMemoryMap.SET_WCSData(PaketObj.GetDataBytes());
                                    }
                                };
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConstClass.CMD2_15:
                    if (COMMDataManager.Check_DEV_REC_Graph(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            //분기가 필요한지는 3가지 장치에 대한 화면 구성이 모두 끝났을 때 결정해서 코드 정리 해야함
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_Graph != null)
                                {
                                    if (!form_Graph.IsDisposed)
                                    {
                                        form_Graph.Rxprocess_Graph(PaketObj.GetDataBytes());
                                    }
                                };
                                break;
                            case ConstClass.TYPE_RTV:
                                if (form_Graph != null)
                                {
                                    if (!form_Graph.IsDisposed)
                                    {
                                        form_Graph.Rxprocess_Graph(PaketObj.GetDataBytes());
                                    }
                                };
                                break;
                            case ConstClass.TYPE_EMS:
                                if (form_Graph != null)
                                {
                                    if (!form_Graph.IsDisposed)
                                    {
                                        form_Graph.Rxprocess_Graph(PaketObj.GetDataBytes());
                                    }
                                };
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConstClass.CMD2_20:
                case ConstClass.CMD2_21:
                case ConstClass.CMD2_22:
                    //다운로드는 SRM, RTV, EMS 에서 동일한 프로토콜로 구현해야 하는 기능임. 분기 필요없음
                    if (form_Download != null)
                    {
                        if (!form_Download.IsDisposed)
                        {
                            form_Download.RxDownloadres(PaketObj.CMD2, PaketObj.SEQ, PaketObj.GetDataBytes());
                        }
                    }
                    break;

                case ConstClass.CMD2_23:
                    if (COMMDataManager.Check_Dev_REC_DevIOConfig(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Process_IO_CFG_Load(PaketObj.GetDataBytes());
                                            
                                            break;
                                        }
                                    }
                                }

                                if (form_IOStructureSet != null)
                                {
                                    if (!form_IOStructureSet.IsDisposed)
                                    {
                                        form_IOStructureSet.Display_IOConfig(PaketObj.GetDataBytes());
                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:
                                
                                if (form_RTVTotal != null)
                                {
                                    if (!form_RTVTotal.IsDisposed)
                                    {
                                        if (form_RTVTotal.Want_Data)
                                        {
                                            form_RTVTotal.Want_Data = false;
                                            form_RTVTotal.Process_IO_CFG_Load(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }

                                if (form_IOStructureSet != null)
                                {
                                    if (!form_IOStructureSet.IsDisposed)
                                    {
                                        form_IOStructureSet.Display_IOConfig(PaketObj.GetDataBytes());
                                    }
                                }
                                break;
                            case ConstClass.TYPE_EMS:
                               
                                if (form_EMSTotal != null)
                                {
                                    if (!form_EMSTotal.IsDisposed)
                                    {
                                        if (form_EMSTotal.Want_Data)
                                        {
                                            form_EMSTotal.Want_Data = false;
                                            form_EMSTotal.Process_IO_CFG_Load(PaketObj.GetDataBytes());
                                            
                                            break;
                                        }
                                    }
                                }

                                if (form_IOStructureSet != null)
                                {
                                    if (!form_IOStructureSet.IsDisposed)
                                    {
                                        form_IOStructureSet.Display_IOConfig(PaketObj.GetDataBytes());
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;

                case ConstClass.CMD2_24:
                    //if (COMMDataManager.Check_DEV_CtrlRes_2Byte(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    if (PaketObj.GetDataSize() > 0)
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Response_IO_CFG(PaketObj.GetDataBytes());
                                            
                                            break;
                                        }
                                    }
                                }


                                break;
                            case ConstClass.TYPE_RTV:

                                if (form_RTVTotal != null)
                                {
                                    if (!form_RTVTotal.IsDisposed)
                                    {
                                        if (form_RTVTotal.Want_Data)
                                        {
                                            form_RTVTotal.Want_Data = false;
                                            form_RTVTotal.Response_IO_CFG(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_EMS:
                                if (form_EMSTotal != null)
                                {
                                    if (!form_EMSTotal.IsDisposed)
                                    {
                                        if (form_EMSTotal.Want_Data)
                                        {
                                            form_EMSTotal.Want_Data = false;
                                            form_EMSTotal.Response_IO_CFG(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConstClass.CMD2_25:
                    if (COMMDataManager.Check_DEV_DevConfig(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면

                                
                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Process_SRM_CFG_Load(PaketObj.GetDataBytes());
                                            
                                            break;
                                        }
                                    }
                                }

                                if (form_SRMConfig != null)
                                {
                                    if (!form_SRMConfig.IsDisposed)
                                    {
                                        form_SRMConfig.Display_SRMConfig(PaketObj.GetDataBytes());
                                    }
                                }

                                break;
                            case ConstClass.TYPE_RTV:

                                if (form_RTVTotal != null)
                                {
                                    if (!form_RTVTotal.IsDisposed)
                                    {
                                        if (form_RTVTotal.Want_Data)
                                        {
                                            form_RTVTotal.Want_Data = false;
                                            form_RTVTotal.Process_MCU_CFG_Load(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }

                                if (form_RTVConfig != null)
                                {
                                    if (!form_RTVConfig.IsDisposed)
                                    {
                                        form_RTVConfig.Display_RTVConfig(PaketObj.GetDataBytes());
                                    }
                                }
                                break;
                            case ConstClass.TYPE_EMS:
                                if (form_EMSTotal != null)
                                {
                                    if (!form_EMSTotal.IsDisposed)
                                    {
                                        if (form_EMSTotal.Want_Data)
                                        {
                                            form_EMSTotal.Want_Data = false;
                                            form_EMSTotal.Process_MCU_CFG_Load(PaketObj.GetDataBytes());
                                            break;
                                        }
                                    }
                                }

                                if (form_EMSConfig != null)
                                {
                                    if (!form_EMSConfig.IsDisposed)
                                    {
                                        form_EMSConfig.Display_EMSConfig(PaketObj.GetDataBytes());
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;

                case ConstClass.CMD2_26:
                    //if (COMMDataManager.Check_DEV_CtrlRes_2Byte(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    if (PaketObj.GetDataSize() > 0)
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Response_SRM_CFGCtrl(PaketObj.GetDataBytes());
                                            
                                            break;
                                        }
                                    }
                                }


                                break;
                            case ConstClass.TYPE_RTV:

                                if (form_RTVTotal != null)
                                {
                                    if (!form_RTVTotal.IsDisposed)
                                    {
                                        if (form_RTVTotal.Want_Data)
                                        {
                                            form_RTVTotal.Want_Data = false;
                                            form_RTVTotal.Response_MCU_CFGCtrl(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_EMS:
                                if (form_EMSTotal != null)
                                {
                                    if (!form_EMSTotal.IsDisposed)
                                    {
                                        if (form_EMSTotal.Want_Data)
                                        {
                                            form_EMSTotal.Want_Data = false;
                                            form_EMSTotal.Response_MCU_CFGCtrl(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;

                case ConstClass.CMD2_30:
                    if (COMMDataManager.Set_DEV_REC_DevSt(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                Display_DevErrSt();
                                Display_DevFW();
                                Display_DevForceMode();
                                Display_DevSetupMode();
                                if (form_SRMSt != null)
                                {
                                    if (!form_SRMSt.IsDisposed)
                                    {
                                        form_SRMSt.Display_DevSt();
                                    }
                                }

                                if (form_DO_TestCtrl != null)
                                {
                                    if (!form_DO_TestCtrl.IsDisposed)
                                    {
                                        form_DO_TestCtrl.Display_DevSt();
                                    }
                                }
                                
                                if (form_SRM_CTL != null)
                                {
                                    if (!form_SRM_CTL.IsDisposed)
                                    {
                                        form_SRM_CTL.Display_DevSt();
                                    }
                                }

                                break;
                            case ConstClass.TYPE_RTV:
                                Display_DevErrSt();
                                Display_DevFW();
                                Display_DevForceMode();
                                Display_DevSetupMode();
                                if (form_RTVSt != null)
                                {
                                    if (!form_RTVSt.IsDisposed)
                                    {
                                        form_RTVSt.Display_DevSt();
                                    }
                                }

                                if (form_DO_TestCtrl != null)
                                {
                                    if (!form_DO_TestCtrl.IsDisposed)
                                    {
                                        form_DO_TestCtrl.Display_DevSt();
                                    }
                                }

                                if (form_RTV_CTL != null)
                                {
                                    if (!form_RTV_CTL.IsDisposed)
                                    {
                                        form_RTV_CTL.Display_DevSt();
                                    }
                                }

                                if (form_RTVRack != null)
                                {
                                    if (!form_RTVRack.IsDisposed)
                                    {
                                        form_RTVRack.Display_RTVStatus();
                                    }
                                }

                                break;
                            case ConstClass.TYPE_EMS:
                                Display_DevErrSt();
                                Display_DevFW();
                                Display_DevForceMode();
                                Display_DevSetupMode();
                                if (form_EMSSt != null)
                                {
                                    if (!form_EMSSt.IsDisposed)
                                    {
                                        form_EMSSt.Display_DevSt();
                                    }
                                }

                                if (form_DO_TestCtrl != null)
                                {
                                    if (!form_DO_TestCtrl.IsDisposed)
                                    {
                                        form_DO_TestCtrl.Display_DevSt();
                                    }
                                }

                                if (form_EMS_CTL != null)
                                {
                                    if (!form_EMS_CTL.IsDisposed)
                                    {
                                        form_EMS_CTL.Display_DevSt();
                                    }
                                }

                                if (form_EMSRack != null)
                                {
                                    if (!form_EMSRack.IsDisposed)
                                    {
                                        form_EMSRack.Display_EMSStatus();
                                    }
                                }

                                break;
                            default:
                                break;
                        }
                    }
                    break;
                    
                case ConstClass.CMD2_31:
                    if (COMMDataManager.Check_Dev_REC_OpInfo(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면

                                if (form_OpInfo != null)
                                {
                                    if (!form_OpInfo.IsDisposed)
                                    {
                                        form_OpInfo.Display_OpInfo(PaketObj.GetDataBytes());
                                    }
                                }

                                if (form_TimeInfo != null)
                                {
                                    if (!form_TimeInfo.IsDisposed)
                                    {
                                        form_TimeInfo.Display_TimeInfo( PaketObj.GetDataBytes());
                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:
                                if (form_TimeInfo != null)
                                {
                                    if (!form_TimeInfo.IsDisposed)
                                    {
                                        form_TimeInfo.Display_TimeInfo( PaketObj.GetDataBytes());
                                    }
                                }
                                break;
                            case ConstClass.TYPE_EMS:
                                if (form_TimeInfo != null)
                                {
                                    if (!form_TimeInfo.IsDisposed)
                                    {
                                        form_TimeInfo.Display_TimeInfo(PaketObj.GetDataBytes());
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConstClass.CMD2_32:
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면

                                if (COMMDataManager.Check_SRM_REC_InvertorInfo(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                                {
                                    if (form_SRMInvertorSt != null)
                                    {
                                        if (!form_SRMInvertorSt.IsDisposed)
                                        {
                                            form_SRMInvertorSt.Display_InvertorSt(PaketObj.GetDataBytes());
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:
                            case ConstClass.TYPE_EMS:
                            if (COMMDataManager.Check_EMSRTV_REC_InvertorInfo(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                                {
                                    if (form_EMSRTVInvertorSt != null)
                                    {
                                        if (!form_EMSRTVInvertorSt.IsDisposed)
                                        {
                                        form_EMSRTVInvertorSt.SET_InvertorSt(PaketObj.GetDataBytes());
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    break;

                case ConstClass.CMD2_34:
                    
                    if (COMMDataManager.Check_DEV_REC_LOG(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        //알람로그 화면은 같은 화면이다. 장치타입에 따른 알람코드 정의만 분류해서 처리한다.
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM:
                            case ConstClass.TYPE_RTV:
                            case ConstClass.TYPE_EMS:
                                if (form_DevLog != null)
                                {
                                    if (!form_DevLog.IsDisposed)
                                    {
                                        form_DevLog.Process_DevLog(PaketObj.GetDataBytes());
                                    }
                                }
                                

                                if (form_DevEventLog != null)
                                {
                                    if (!form_DevEventLog.IsDisposed)
                                    {
                                        form_DevEventLog.Process_DevLog(PaketObj.GetDataBytes());
                                    }
                                }
                                break;
                        }
                    }
                    break;

                case ConstClass.CMD2_41:

                    switch (PaketObj.SrcDevType)
                    {
                        case ConstClass.TYPE_SRM:
                            if (COMMDataManager.Check_SRM_JobCtrlRes(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {
                                if (form_SRM_CTL != null)
                                {
                                    if (!form_SRM_CTL.IsDisposed)
                                    {
                                        form_SRM_CTL.Display_JobCtrlRes(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                        case ConstClass.TYPE_RTV:
                            if (COMMDataManager.Check_RTV_JobCtrlRes(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {
                                if (form_RTV_CTL != null)
                                {
                                    if (!form_RTV_CTL.IsDisposed)
                                    {
                                        //form_RTV_CTL.Display_JobCtrlRes(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                        case ConstClass.TYPE_EMS:
                            if (COMMDataManager.Check_EMS_JobCtrlRes(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {
                                if (form_EMS_CTL != null)
                                {
                                    if (!form_EMS_CTL.IsDisposed)
                                    {
                                        form_EMS_CTL.Display_JobCtrlRes(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                    }
                    break;

                case ConstClass.CMD2_36:

                    if (COMMDataManager.Check_DEV_REC_DEBUG(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        //알람로그 화면은 같은 화면이다. 장치타입에 따른 알람코드 정의만 분류해서 처리한다.
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM:
                            case ConstClass.TYPE_RTV:
                            case ConstClass.TYPE_EMS:
                                if (form_DevDebugLog != null)
                                {
                                    if (!form_DevDebugLog.IsDisposed)
                                    {
                                        form_DevDebugLog.Process_DevLog(PaketObj.GetDataBytes());
                                    }
                                }
                                break;
                        }
                    }
                    break;

                case ConstClass.CMD2_61:
                    if (COMMDataManager.Check_DEV_CtrlRes_2Byte(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_SRM_CTL != null)
                                {
                                    if (!form_SRM_CTL.IsDisposed)
                                    {
                                        form_SRM_CTL.Process_0x61CtrlRes(PaketObj.GetDataBytes());
                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:

                                break;
                            case ConstClass.TYPE_EMS:

                                break;
                            default:
                                break;
                        }
                    }
                    break;

                case ConstClass.CMD2_90:
                    if (COMMDataManager.Check_SRM_InvertorParam(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_SRMInvertorParam != null)
                                {
                                    if (!form_SRMInvertorParam.IsDisposed)
                                    {
                                        form_SRMInvertorParam.Display_InvertorParamSt(PaketObj.GetDataBytes());
                                    }
                                }


                                break;
                            case ConstClass.TYPE_RTV:
                                break;
                            case ConstClass.TYPE_EMS:
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConstClass.CMD2_91:
                    if (COMMDataManager.Check_SRM_InvertorParamCtrlRes(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_SRMInvertorParam != null)
                                {
                                    if (!form_SRMInvertorParam.IsDisposed)
                                    {
                                        form_SRMInvertorParam.Display_InvertorParamCtrl(PaketObj.GetDataBytes());
                                    }
                                }


                                break;
                            case ConstClass.TYPE_RTV:
                                break;
                            case ConstClass.TYPE_EMS:
                                break;
                            default:
                                break;
                        }
                    }
                    break;

                case ConstClass.CMD2_94:
                    switch (PaketObj.SrcDevType)
                    {
                        case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                            if (COMMDataManager.Check_SRM_REC_RackPosition(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {
                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Process_CellPosition_Load(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }

                                if (form_SRMRack != null)
                                {
                                    if (!form_SRMRack.IsDisposed)
                                    {
                                        form_SRMRack.Display_SRMRackPosition(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                        case ConstClass.TYPE_RTV:
                            if (COMMDataManager.Check_RTV_REC_Position(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {
                                if (form_RTVTotal != null)
                                {
                                    if (!form_RTVTotal.IsDisposed)
                                    {
                                        if (form_RTVTotal.Want_Data)
                                        {
                                            form_RTVTotal.Want_Data = false;
                                            form_RTVTotal.Process_RTVPosition_Load(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }

                                if (form_RTVRack != null)
                                {
                                    if (!form_RTVRack.IsDisposed)
                                    {
                                        form_RTVRack.Display_RTVPosition(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                        case ConstClass.TYPE_EMS:
                            if (COMMDataManager.Check_EMS_REC_Position(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {
                                //EMS 수정
                                if (form_EMSTotal != null)
                                {
                                    if (!form_EMSTotal.IsDisposed)
                                    {
                                        if (form_EMSTotal.Want_Data)
                                        {
                                            form_EMSTotal.Want_Data = false;
                                            form_EMSTotal.Process_EMSPosition_Load(PaketObj.GetDataBytes());
                                            break;
                                        }
                                    }
                                }

                                if (form_EMSRack != null)
                                {
                                    if (!form_EMSRack.IsDisposed)
                                    {
                                        form_EMSRack.Display_EMSPosition(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;

                case ConstClass.CMD2_95:
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (COMMDataManager.Check_SRM_REC_RackPositionCtrl(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                                {
                                    if (form_SRMTotal != null)
                                    {
                                        if (!form_SRMTotal.IsDisposed)
                                        {
                                            if (form_SRMTotal.Want_Data)
                                            {
                                                form_SRMTotal.Want_Data = false;
                                                form_SRMTotal.Response_Position_CFG(PaketObj.GetDataBytes());

                                                break;
                                            }
                                        }
                                    }

                                    if (form_SRMRack != null)
                                    {
                                        if (!form_SRMRack.IsDisposed)
                                        {
                                            form_SRMRack.Process_SRMRackPositionCtrlRes(PaketObj.GetDataBytes());
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:
                                if (COMMDataManager.Check_RTV_REC_PositionCtrl(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                                {
                                    if (form_RTVTotal != null)
                                    {
                                        if (!form_RTVTotal.IsDisposed)
                                        {
                                            if (form_RTVTotal.Want_Data)
                                            {
                                                form_RTVTotal.Want_Data = false;
                                                form_RTVTotal.Response_Position_CFG(PaketObj.GetDataBytes());

                                                break;
                                            }
                                        }
                                    }

                                    if (form_RTVRack != null)
                                    {
                                        if (!form_RTVRack.IsDisposed)
                                        {
                                            form_RTVRack.Process_RTVPositionCtrlRes(PaketObj.GetDataBytes());
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_EMS:
                            if (COMMDataManager.Check_EMS_REC_PositionCtrl(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {
                                //EMS 수정
                                if (form_EMSTotal != null)
                                {
                                    if (!form_EMSTotal.IsDisposed)
                                    {
                                        if (form_EMSTotal.Want_Data)
                                        {
                                            form_EMSTotal.Want_Data = false;
                                            form_EMSTotal.Response_Position_CFG(PaketObj.GetDataBytes());
                                            break;
                                        }
                                    }
                                }

                                if (form_EMSRack != null)
                                {
                                    if (!form_EMSRack.IsDisposed)
                                    {
                                        form_EMSRack.Process_EMSPositionCtrlRes(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                            default:
                                break;
                        }
                    break;
                case ConstClass.CMD2_96:
                    if (COMMDataManager.Check_SRM_REC_RackOffset(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Process_CellOffset_Load(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }

                                if (form_SRMRack != null)
                                {
                                    if (!form_SRMRack.IsDisposed)
                                    {
                                        form_SRMRack.Display_SRMRackOffset(PaketObj.GetDataBytes());
                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:
                                break;
                            case ConstClass.TYPE_EMS:
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConstClass.CMD2_97:
                    if (COMMDataManager.Check_SRM_REC_RackOffsetCtrl(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Response_OFFSET_CFG(PaketObj.GetDataBytes());
                                            
                                            break;
                                        }
                                    } 
                                }

                                if (form_SRMRack != null)
                                {
                                    if (!form_SRMRack.IsDisposed)
                                    {
                                        form_SRMRack.Process_SRMRackOffsetCtrlRes(PaketObj.GetDataBytes());
                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:
                                break;
                            case ConstClass.TYPE_EMS:
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConstClass.CMD2_98:
                    switch (PaketObj.SrcDevType)
                    {
                        case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                            if (COMMDataManager.Check_SRM_REC_StationParam(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {
                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Process_Station_CFG_Load(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }
                                if (form_SRMRack != null)
                                {
                                    if (!form_SRMRack.IsDisposed)
                                    {
                                        form_SRMRack.Display_SRMStationParam(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                        case ConstClass.TYPE_RTV:
                            if (COMMDataManager.Check_RTV_REC_StationParam(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {
                                if (form_RTVTotal != null)
                                {
                                    if (!form_RTVTotal.IsDisposed)
                                    {
                                        if (form_RTVTotal.Want_Data)
                                        {
                                            form_RTVTotal.Want_Data = false;
                                            form_RTVTotal.Process_Station_CFG_Load(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }
                                if (form_RTVRack != null)
                                {
                                    if (!form_RTVRack.IsDisposed)
                                    {
                                        form_RTVRack.Display_RTVStationParam(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                        case ConstClass.TYPE_EMS:
                            if (COMMDataManager.Check_EMS_REC_StationParam(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {
                                //EMS 수정
                                if (form_EMSTotal != null)
                                {
                                    if (!form_EMSTotal.IsDisposed)
                                    {
                                        if (form_EMSTotal.Want_Data)
                                        {
                                            form_EMSTotal.Want_Data = false;
                                            form_EMSTotal.Process_Station_CFG_Load(PaketObj.GetDataBytes());
                                            break;
                                        }
                                    }
                                }

                                if (form_EMSRack != null)
                                {
                                    if (!form_EMSRack.IsDisposed)
                                    {
                                        form_EMSRack.Display_EMSStationParam(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case ConstClass.CMD2_99:
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (COMMDataManager.Check_DEV_CtrlRes_3Byte(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                                {
                                    if (form_SRMTotal != null)
                                    {
                                        if (!form_SRMTotal.IsDisposed)
                                        {
                                            if (form_SRMTotal.Want_Data)
                                            {
                                                form_SRMTotal.Want_Data = false;
                                                form_SRMTotal.Response_Sation_CFG(PaketObj.GetDataBytes());

                                                break;
                                            }
                                        }
                                    }

                                    if (form_SRMRack != null)
                                    {
                                        if (!form_SRMRack.IsDisposed)
                                        {
                                            form_SRMRack.Process_SRMStationParamCtrlRes(PaketObj.GetDataBytes());
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:
                                if (COMMDataManager.Check_RTV_REC_StationCtrl(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                                {
                                    if (form_RTVTotal != null)
                                    {
                                        if (!form_RTVTotal.IsDisposed)
                                        {
                                            if (form_RTVTotal.Want_Data)
                                            {
                                                form_RTVTotal.Want_Data = false;
                                                form_RTVTotal.Response_Station_CFG(PaketObj.GetDataBytes());

                                                break;
                                            }
                                        }
                                    }

                                    if (form_RTVRack != null)
                                    {
                                        if (!form_RTVRack.IsDisposed)
                                        {
                                            form_RTVRack.Process_RTVStationParamCtrlRes(PaketObj.GetDataBytes());
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_EMS:
                                if (COMMDataManager.Check_EMS_REC_StationCtrl(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                                {
                                    //EMS 수정    
                                    if (form_EMSTotal != null)
                                    {
                                        if (!form_EMSTotal.IsDisposed)
                                        {
                                            if (form_EMSTotal.Want_Data)
                                            {
                                                form_EMSTotal.Want_Data = false;
                                                form_EMSTotal.Response_Station_CFG(PaketObj.GetDataBytes());
                                                break;
                                            }
                                        }
                                    }

                                    if (form_EMSRack != null)
                                    {
                                        if (!form_EMSRack.IsDisposed)
                                        {
                                        form_EMSRack.Process_EMSStationParamCtrlRes(PaketObj.GetDataBytes());
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    break;
                case ConstClass.CMD2_9A:
                        switch (PaketObj.SrcDevType)
                        {
                            
                            case ConstClass.TYPE_RTV:
                                if (COMMDataManager.Check_RTV_REC_AreaSpeed(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                                {
                                    if (form_RTVTotal != null)
                                    {
                                        if (!form_RTVTotal.IsDisposed)
                                        {
                                            if (form_RTVTotal.Want_Data)
                                            {
                                                form_RTVTotal.Want_Data = false;
                                                form_RTVTotal.Process_RTV_SpeedArea_Load(PaketObj.GetDataBytes());

                                                break;
                                            }
                                        }
                                    }

                                    if (form_RTVRack != null)
                                    {
                                        if (!form_RTVRack.IsDisposed)
                                        {
                                            form_RTVRack.Display_RTVSpeedArea(PaketObj.GetDataBytes());
                                        }
                                    }
                                }
                                break;
                        case ConstClass.TYPE_EMS:
                            if (COMMDataManager.Check_EMS_REC_AreaSpeed(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {
                                //EMS 수정
                                if (form_EMSTotal != null)
                                {
                                    if (!form_EMSTotal.IsDisposed)
                                    {
                                        if (form_EMSTotal.Want_Data)
                                        {
                                            form_EMSTotal.Want_Data = false;
                                            form_EMSTotal.Process_EMS_SpeedArea_Load(PaketObj.GetDataBytes());
                                            break;
                                        }
                                    }
                                }

                                if (form_EMSRack != null)
                                {
                                    if (!form_EMSRack.IsDisposed)
                                    {
                                        form_EMSRack.Display_EMSSpeedArea(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;

                        default:
                                break;
                        }
                    break;
                case ConstClass.CMD2_9B:
                    switch (PaketObj.SrcDevType)
                    {
                        case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                            break;
                        case ConstClass.TYPE_RTV:
                            if (COMMDataManager.Check_RTV_REC_AreaSpeedCtrl(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {
                                if (form_RTVTotal != null)
                                {
                                    if (!form_RTVTotal.IsDisposed)
                                    {
                                        if (form_RTVTotal.Want_Data)
                                        {
                                            form_RTVTotal.Want_Data = false;
                                            form_RTVTotal.Response_SpeedArea_CFG(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }

                                if (form_RTVRack != null)
                                {
                                    if (!form_RTVRack.IsDisposed)
                                    {
                                        form_RTVRack.Process_RTVSpeedAreaCtrlRes(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                        case ConstClass.TYPE_EMS:
                            if (COMMDataManager.Check_EMS_REC_AreaSpeedCtrl(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {
                                //EMS 수정
                                if (form_EMSTotal != null)
                                {
                                    if (!form_EMSTotal.IsDisposed)
                                    {
                                        if (form_EMSTotal.Want_Data)
                                        {
                                            form_EMSTotal.Want_Data = false;
                                            form_EMSTotal.Response_SpeedArea_CFG(PaketObj.GetDataBytes());
                                            break;
                                        }
                                    }
                                }

                                if (form_EMSRack != null)
                                {
                                    if (!form_EMSRack.IsDisposed)
                                    {
                                        form_EMSRack.Process_EMSSpeedAreaCtrlRes(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case ConstClass.CMD2_9C:
                    if (COMMDataManager.Check_SRM_NoUseRackSt(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면                        
                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Process_NoUseRack_CFG_Load(PaketObj.GetDataBytes());
                                            
                                            break;
                                        }
                                    }

                                }
                                if (form_srmInhibitionRack != null)
                                {
                                    if (!form_srmInhibitionRack.IsDisposed)
                                    {
                                        form_srmInhibitionRack.Display_NoUseRack(PaketObj.GetDataBytes());
                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:
                                break;
                            case ConstClass.TYPE_EMS:
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConstClass.CMD2_9D:
                    //if (COMMDataManager.Check_DEV_CtrlRes_2Byte(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    if (PaketObj.GetDataSize() > 0)
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Response_NoUseRack_CFG(PaketObj.GetDataBytes());
                                            
                                            break;
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:
                                break;
                            case ConstClass.TYPE_EMS:
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConstClass.CMD2_9E:
                    if (COMMDataManager.Check_SRM_SpecialRackSt(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면                        

                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Process_SpecialRack_CFG_Load(PaketObj.GetDataBytes());
                                            
                                            break;
                                        }

                                    }

                                }

                                if (form_srmSpecialRack != null)
                                {
                                    if (!form_srmSpecialRack.IsDisposed)
                                    {
                                        form_srmSpecialRack.Display_SpecialRack(PaketObj.GetDataBytes());
                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:
                                break;
                            case ConstClass.TYPE_EMS:
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConstClass.CMD2_9F:
                    //if (COMMDataManager.Check_DEV_CtrlRes_2Byte(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    if (PaketObj.GetDataSize() > 0)
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Response_SpecialUseRack_CFG(PaketObj.GetDataBytes());
                                            
                                            break;
                                        }
                                    }
                                }


                                break;
                            case ConstClass.TYPE_RTV:
                                break;
                            case ConstClass.TYPE_EMS:
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConstClass.CMD2_A1:
                    switch (PaketObj.SrcDevType)
                    {
                        case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                            if (COMMDataManager.Check_SRM_CtrlParamRes(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {
                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Process_CTRL_PARAM_CFG_Load(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }

                                if (form_srmParam_Ctrl != null)
                                {
                                    if (!form_srmParam_Ctrl.IsDisposed)
                                    {
                                        form_srmParam_Ctrl.Display_SRM_CtrlParam(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                        case ConstClass.TYPE_RTV:
                            if (COMMDataManager.Check_RTV_CtrlParamRes(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {

                                if (form_RTVTotal != null)
                                {
                                    if (!form_RTVTotal.IsDisposed)
                                    {
                                        if (form_RTVTotal.Want_Data)
                                        {
                                            form_RTVTotal.Want_Data = false;
                                            form_RTVTotal.Process_CTRL_PARAM_CFG_Load(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }

                                if (form_rtvParam_Ctrl != null)
                                {
                                    if (!form_rtvParam_Ctrl.IsDisposed)
                                    {
                                        form_rtvParam_Ctrl.Display_RTV_CtrlParam(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                        case ConstClass.TYPE_EMS:
                            if (COMMDataManager.Check_EMS_CtrlParamRes(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {
                                //EMS 수정
                                if (form_EMSTotal != null)
                                {
                                    if (!form_EMSTotal.IsDisposed)
                                    {
                                        if (form_EMSTotal.Want_Data)
                                        {
                                            form_EMSTotal.Want_Data = false;
                                            form_EMSTotal.Process_CTRL_PARAM_CFG_Load(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }

                                if (form_emsParam_Ctrl != null)
                                {
                                    if (!form_emsParam_Ctrl.IsDisposed)
                                    {
                                        form_emsParam_Ctrl.Display_EMS_CtrlParam(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case ConstClass.CMD2_A2:
                    //if (COMMDataManager.Check_DEV_CtrlRes_2Byte(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    if (PaketObj.GetDataSize() > 0)
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Response_CTRL_PARAM_CFG(PaketObj.GetDataBytes());
                                            
                                            break;
                                        }

                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:
                                if(form_RTVTotal != null)
                                {
                                    if (!form_RTVTotal.IsDisposed)
                                    {
                                        if (form_RTVTotal.Want_Data)
                                        {
                                            form_RTVTotal.Want_Data = false;
                                            form_RTVTotal.Response_CTRL_PARAM_CFG(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_EMS:
                                if (form_EMSTotal != null)
                                {
                                    if (!form_EMSTotal.IsDisposed)
                                    {
                                        if (form_EMSTotal.Want_Data)
                                        {
                                            form_EMSTotal.Want_Data = false;
                                            form_EMSTotal.Response_CTRL_PARAM_CFG(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConstClass.CMD2_A3:
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (COMMDataManager.Check_SRM_DriveParamRes(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                                {
                                    if (form_SRMTotal != null)
                                    {
                                        if (!form_SRMTotal.IsDisposed)
                                        {
                                            if (form_SRMTotal.Want_Data)
                                            {
                                                form_SRMTotal.Want_Data = false;
                                                form_SRMTotal.Process_DRIVE_PARAM_CFG_Load(PaketObj.GetDataBytes());

                                                break;
                                            }
                                        }
                                    }

                                    if (form_srmParam_Drive != null)
                                    {
                                        if (!form_srmParam_Drive.IsDisposed)
                                        {
                                            form_srmParam_Drive.Display_SRM_Param(PaketObj.GetDataBytes());
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:
                                if (COMMDataManager.Check_RTV_DriveParamRes(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                                {

                                    if (form_RTVTotal != null)
                                    {
                                        if (!form_RTVTotal.IsDisposed)
                                        {
                                            if (form_RTVTotal.Want_Data)
                                            {
                                                form_RTVTotal.Want_Data = false;
                                                form_RTVTotal.Process_DRIVE_PARAM_CFG_Load(PaketObj.GetDataBytes());

                                                break;
                                            }
                                        }
                                    }

                                    if (form_rtvParam_Drive != null)
                                    {
                                        if (!form_rtvParam_Drive.IsDisposed)
                                        {
                                            form_rtvParam_Drive.Display_RTV_Param(PaketObj.GetDataBytes());
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_EMS:
                                if (COMMDataManager.Check_EMS_DriveParamRes(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                                {
                                    //EMS 수정
                                    if (form_EMSTotal != null)
                                    {
                                        if (!form_EMSTotal.IsDisposed)
                                        {
                                            if (form_EMSTotal.Want_Data)
                                            {
                                                form_EMSTotal.Want_Data = false;
                                                form_EMSTotal.Process_DRIVE_PARAM_CFG_Load(PaketObj.GetDataBytes());
                                                break;
                                            }
                                        }
                                    }

                                    if (form_emsParam_Drive != null)
                                    {
                                        if (!form_emsParam_Drive.IsDisposed)
                                        {
                                        form_emsParam_Drive.Display_EMS_Param(PaketObj.GetDataBytes());
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    break;
                case ConstClass.CMD2_A4:
                    //if (COMMDataManager.Check_DEV_CtrlRes_2Byte(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    if (PaketObj.GetDataSize() > 0)
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Response_DRIVE_PARAM_CFG(PaketObj.GetDataBytes());
                                            
                                            break;
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:
                                if (form_RTVTotal != null)
                                {
                                    if (!form_RTVTotal.IsDisposed)
                                    {
                                        if (form_RTVTotal.Want_Data)
                                        {
                                            form_RTVTotal.Want_Data = false;
                                            form_RTVTotal.Response_DRIVE_PARAM_CFG(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_EMS:
                                if (form_EMSTotal != null)
                                {
                                    if (!form_EMSTotal.IsDisposed)
                                    {
                                        if (form_EMSTotal.Want_Data)
                                        {
                                            form_EMSTotal.Want_Data = false;
                                            form_EMSTotal.Response_DRIVE_PARAM_CFG(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;

                case ConstClass.CMD2_A5:
                    switch (PaketObj.SrcDevType)
                    {
                        case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                            if (COMMDataManager.Check_SRM_LiftParamRes(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {

                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Process_LIFT_PARAM_CFG_Load(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }
                                if (form_srmParam_Lift != null)
                                {
                                    if (!form_srmParam_Lift.IsDisposed)
                                    {
                                        form_srmParam_Lift.Display_SRM_Param(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                        case ConstClass.TYPE_RTV:
                            break;
                        case ConstClass.TYPE_EMS:
                            if (COMMDataManager.Check_EMS_LiftParamRes(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                            {

                                //EMS 수정
                                if (form_EMSTotal != null)
                                {
                                    if (!form_EMSTotal.IsDisposed)
                                    {
                                        if (form_EMSTotal.Want_Data)
                                        {
                                            form_EMSTotal.Want_Data = false;
                                            form_EMSTotal.Process_LIFT_PARAM_CFG_Load(PaketObj.GetDataBytes());
                                            break;
                                        }
                                    }
                                }

                                if (form_emsParam_Lift != null)
                                {
                                    if (!form_emsParam_Lift.IsDisposed)
                                    {
                                        form_emsParam_Lift.Display_EMS_Param(PaketObj.GetDataBytes());
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case ConstClass.CMD2_A6:
                    //if (COMMDataManager.Check_DEV_CtrlRes_2Byte(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    if (PaketObj.GetDataSize() > 0)
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Response_LIFT_PARAM_CFG(PaketObj.GetDataBytes());
                                            
                                            break;
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:
                                break;
                            case ConstClass.TYPE_EMS:
                                if (form_EMSTotal != null)
                                {
                                    if (!form_EMSTotal.IsDisposed)
                                    {
                                        if (form_EMSTotal.Want_Data)
                                        {
                                            form_EMSTotal.Want_Data = false;
                                            form_EMSTotal.Response_LIFT_PARAM_CFG(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConstClass.CMD2_A7:
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (COMMDataManager.Check_SRM_ForkParamRes(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                                {
                                    if (form_SRMTotal != null)
                                    {
                                        if (!form_SRMTotal.IsDisposed)
                                        {
                                            if (form_SRMTotal.Want_Data)
                                            {
                                                form_SRMTotal.Want_Data = false;
                                                form_SRMTotal.Process_FORK_PARAM_CFG_Load(PaketObj.GetDataBytes());

                                                break;
                                            }
                                        }
                                    }
                                    if (form_srmParam_Fork != null)
                                    {
                                        if (!form_srmParam_Fork.IsDisposed)
                                        {
                                            form_srmParam_Fork.Display_SRM_Param(PaketObj.GetDataBytes());
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:
                                if (COMMDataManager.Check_RTV_FeedParamRes(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                                {

                                    if (form_RTVTotal != null)
                                    {
                                        if (!form_RTVTotal.IsDisposed)
                                        {
                                            if (form_RTVTotal.Want_Data)
                                            {
                                                form_RTVTotal.Want_Data = false;
                                                form_RTVTotal.Process_FEED_PARAM_CFG_Load(PaketObj.GetDataBytes());

                                                break;
                                            }
                                        }
                                    }
                                    if (form_rtvParam_Feed != null)
                                    {
                                        if (!form_rtvParam_Feed.IsDisposed)
                                        {
                                            form_rtvParam_Feed.Display_RTV_Param(PaketObj.GetDataBytes());
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_EMS:
                                break;
                            default:
                                break;
                        }
                    break;
                case ConstClass.CMD2_A8:
                    //if (COMMDataManager.Check_DEV_CtrlRes_2Byte(PaketObj.SrcDevType, PaketObj.SrcID, PaketObj.GetDataBytes()))
                    if (PaketObj.GetDataSize() > 0)
                    {
                        switch (PaketObj.SrcDevType)
                        {
                            case ConstClass.TYPE_SRM: //장치타입이 SRM이면
                                if (form_SRMTotal != null)
                                {
                                    if (!form_SRMTotal.IsDisposed)
                                    {
                                        if (form_SRMTotal.Want_Data)
                                        {
                                            form_SRMTotal.Want_Data = false;
                                            form_SRMTotal.Response_FORK_PARAM_CFG(PaketObj.GetDataBytes());
                                            
                                            break;
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_RTV:
                                if (form_RTVTotal != null)
                                {
                                    if (!form_RTVTotal.IsDisposed)
                                    {
                                        if (form_RTVTotal.Want_Data)
                                        {
                                            form_RTVTotal.Want_Data = false;
                                            form_RTVTotal.Response_FEED_PARAM_CFG(PaketObj.GetDataBytes());

                                            break;
                                        }
                                    }
                                }
                                break;
                            case ConstClass.TYPE_EMS:
                                break;
                            default:
                                break;
                        }
                    }
                    break;
            }
        }
        #endregion

        #region 기능함수

        private void CheckSelectedDevice()
        {
            byte OLD_SelectDestDevType = COMMDataManager.SelectDestDevType;
            switch (cbDevType.SelectedIndex)
            {
                case 0:
                    COMMDataManager.SelectDestDevType = ConstClass.TYPE_SRM;
                    break;
                case 1:
                    COMMDataManager.SelectDestDevType = ConstClass.TYPE_RTV;
                    break;
                case 2:
                    COMMDataManager.SelectDestDevType = ConstClass.TYPE_EMS;
                    break;
                case 3:
                    COMMDataManager.SelectDestDevType = 0xFF;
                    break;
                default:
                    COMMDataManager.SelectDestDevType = ConstClass.TYPE_SRM;
                    break;
            }

            pnUDPConnectType.Visible = ((COMMDataManager.SelectDestDevType == ConstClass.TYPE_RTV) || (COMMDataManager.SelectDestDevType == ConstClass.TYPE_EMS));
            if (!pnUDPConnectType.Visible)
            {
                //rb_WCConnect.Checked = true;
            }

            if (cbDevID.SelectedIndex >= 0)
            {

                if (cbDevID.SelectedIndex == (cbDevID.Items.Count - 1))
                {
                    COMMDataManager.SelectDestDevID = 0xFF;
                }
                else
                {
                    COMMDataManager.SelectDestDevID = Convert.ToByte(cbDevID.SelectedIndex + 1);
                }
            }
            else
            {
                COMMDataManager.SelectDestDevID = 1;
            }

            UpdateWifiDeviceIP();

            switch (COMMDataManager.SelectDestDevType)
            {
                case ConstClass.TYPE_SRM: lbl_SelectDev.Text = "선택장치 : SRM " + COMMDataManager.SelectDestDevID.ToString(); break;
                case ConstClass.TYPE_RTV: lbl_SelectDev.Text = "선택장치 : RTV " + COMMDataManager.SelectDestDevID.ToString(); break;
                case ConstClass.TYPE_EMS: lbl_SelectDev.Text = "선택장치 : EMS " + COMMDataManager.SelectDestDevID.ToString(); break;
                default: lbl_SelectDev.Text = "선택장치 : 0x" + string.Format("{0:X2} {1:X2}", COMMDataManager.SelectDestDevType, COMMDataManager.SelectDestDevID); break;
            }

            if (OLD_SelectDestDevType != COMMDataManager.SelectDestDevType)
            {
                AllClose_Windows();
            }

            //장치 타입에 따른 메뉴 활성화
            switch (COMMDataManager.SelectDestDevType_WithOutANY)
            {
                case ConstClass.TYPE_SRM:
                    MenuItem_basicSt.Enabled = true;
                    menu_DevSt.Enabled = true;
                    //MenuItem_DevLog.Enabled = true;
                    //menu_Debug.Enabled = true;
                    //MenuItem_OpInfo.Enabled = true;
                    //MenuItem_OpInfo.Visible = true;
                    //MenuItem_InvertorInfo.Enabled = true;
                    //MenuItem_Graph.Enabled = true;
                    ToolStripMenuItem.Enabled = true;
                    /*
                    menu_DevBaseConfig.Enabled = true;
                    menu_DEV_IO_Config.Enabled = true;
                    menu_CtrlParameter.Enabled = true;
                    menu_DriveParameter.Enabled = true;
                    menu_LiftParameter.Enabled = true;
                    menu_LiftParameter.Visible = true;
                    menu_ForParameter.Enabled = true;
                    menu_ForParameter.Visible = true;
                    menu_ForParameter.Text = "1.6. 포크드라이브 설정";
                    menu_RackBase.Text = "1.7. 위치 설정";
                    
                    menu_TotalSetLoad.Enabled = true;
                    menu_InvertorParameter.Enabled = true;
                    menu_InvertorParameter.Visible = true;
                    Menu_Download.Enabled = true;

                    menu_RackBase.Enabled = true;
                    menu_NoRackSet.Enabled = true;
                    menu_NoRackSet.Visible = true;
                    menu_SpecialRackSet.Enabled = true;
                    menu_SpecialRackSet.Visible = true
                    */
                    ;
                    toolStripMenuItem2.Enabled = true;
                    break;
                case ConstClass.TYPE_RTV:
                    MenuItem_basicSt.Enabled = true;
                    menu_DevSt.Enabled = true;
                   // MenuItem_DevLog.Enabled = true;
                   // menu_Debug.Enabled = true;
                   // MenuItem_OpInfo.Enabled = false; //미해당
                   // MenuItem_OpInfo.Visible = false; //미해당
                   // MenuItem_InvertorInfo.Enabled = true; 
                    //MenuItem_Graph.Enabled = true;
                    ToolStripMenuItem.Enabled = true;
                    /*
                    menu_DevBaseConfig.Enabled = true;
                    menu_DEV_IO_Config.Enabled = true;
                    menu_CtrlParameter.Enabled = true;
                    menu_DriveParameter.Enabled = true;
                    menu_LiftParameter.Enabled = false;//미해당
                    menu_LiftParameter.Visible = false;//미해당
                    menu_ForParameter.Enabled = true;
                    menu_ForParameter.Visible = true;
                    menu_ForParameter.Text = "1.6. 피딩드라이브 설정";
                    menu_RackBase.Text = "1.7. 위치 설정";
                    

                    menu_TotalSetLoad.Enabled = true; 
                    menu_InvertorParameter.Enabled = false; //미해당
                    menu_InvertorParameter.Visible = false; //미해당
                    Menu_Download.Enabled = true; 

                    menu_RackBase.Enabled = true;
                    menu_NoRackSet.Enabled = false; //미해당
                    menu_NoRackSet.Visible = false; //미해당
                    menu_SpecialRackSet.Enabled = false; //미해당
                    menu_SpecialRackSet.Visible = false; //미해당
                    */
                    toolStripMenuItem2.Enabled = true;
                    break;
                case ConstClass.TYPE_EMS:
                    MenuItem_basicSt.Enabled = true;
                    menu_DevSt.Enabled = true;
                   // MenuItem_DevLog.Enabled = true;
                   // menu_Debug.Enabled = true;
                   // MenuItem_OpInfo.Enabled = false; //미해당
                   // MenuItem_OpInfo.Visible = false; //미해당
                   // MenuItem_InvertorInfo.Enabled = true;
                    //MenuItem_Graph.Enabled = true;
                    ToolStripMenuItem.Enabled = true;
                    /*
                    menu_DevBaseConfig.Enabled = true;
                    menu_DEV_IO_Config.Enabled = true;
                    menu_CtrlParameter.Enabled = true;
                    menu_DriveParameter.Enabled = true;
                    menu_LiftParameter.Enabled = true;
                    menu_LiftParameter.Visible = true;
                    menu_ForParameter.Enabled = false; //미해당
                    menu_ForParameter.Visible = false; //미해당
                    menu_RackBase.Text = "1.7. 위치 설정";

                    menu_TotalSetLoad.Enabled = true;
                    menu_InvertorParameter.Enabled = false; //미해당
                    menu_InvertorParameter.Visible = false; //미해당
                    Menu_Download.Enabled = true;

                    menu_RackBase.Enabled = true;
                    menu_NoRackSet.Enabled = false; //미해당
                    menu_NoRackSet.Visible = false; //미해당
                    menu_SpecialRackSet.Enabled = false; //미해당
                    menu_SpecialRackSet.Visible = false; //미해당
                    */
                    toolStripMenuItem2.Enabled = true;
                    break;
            }
        }


        public void Do_Ctrl_Cmd_withbytes(byte TmpCMD1, byte TmpCMD2, byte[] CtrlData)
        {
            COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, TmpCMD1, TmpCMD2, CtrlData);
        }
        public void Do_Ctrl_Cmd_withOnebyte(byte TmpCMD1, byte TmpCMD2, byte CtrlData)
        {
            byte ctrlValue = 0;

            switch (TmpCMD2)
            {
                case ConstClass.CMD2_44:
                    if (CtrlData == 0) ctrlValue |= 0x01;
                    else if (CtrlData == 1) ctrlValue |= 0x02;
                    else if (CtrlData == 2) ctrlValue |= 0x04;
                    else if (CtrlData == 3) ctrlValue |= 0x08;
                    break;
                default:
                    ctrlValue = CtrlData;
                    break;
            }

            COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, TmpCMD1, TmpCMD2, ctrlValue);
        }
        public void Do_Ctrl_Cmd_withNoData(byte TmpCMD2)
        {
            COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, TmpCMD2);
        }

        public void Do_Ctrl_DevMode(byte TmpCMD2, byte CtrlData)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    switch (TmpCMD2)
                    {
                        case ConstClass.CMD2_58:
                            //현재의 장치의 모드값을 제어 구조체에 반영하고 나서 제어값을 만들어야 한다.
                            byte[] ctrlValue = { 0, 0 };

                            if (Global_Class.BitStatus(COMMDataManager.DevRec.srm_REC_SRMSt.DevMode, 0)) ctrlValue[0] = 2;
                            else if (Global_Class.BitStatus(COMMDataManager.DevRec.srm_REC_SRMSt.DevMode, 1)) ctrlValue[0] = 0;
                            else if (Global_Class.BitStatus(COMMDataManager.DevRec.srm_REC_SRMSt.DevMode, 3)) ctrlValue[0] = 1;
                            if (Global_Class.BitStatus(COMMDataManager.DevRec.srm_REC_SRMSt.DevMode, 2)) ctrlValue[1] |= 0x01;


                            switch (CtrlData)
                            {
                                case 0: //수동모드
                                    ctrlValue[0] = 0;
                                    break;
                                case 1: //셋업모드
                                    ctrlValue[0] = 1;
                                    break;
                                case 2: //자동모드
                                    ctrlValue[0] = 2;
                                    break;
                                case 10: //강제모드 OFF
                                    ctrlValue[1] &= 0x00;
                                    break;
                                case 11: //강제모드 ON
                                    ctrlValue[1] |= 0x01;
                                    break;
                            }
                            COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, TmpCMD2, ctrlValue);
                            break;
                    }
                    break;
                case ConstClass.TYPE_RTV:
                    switch (TmpCMD2)
                    {
                        case ConstClass.CMD2_58:
                            //현재의 장치의 모드값을 제어 구조체에 반영하고 나서 제어값을 만들어야 한다.
                            byte[] ctrlValue = { 0, 0 };

                            if (Global_Class.BitStatus(COMMDataManager.DevRec.rtv_REC_RTVSt.DevMode, 0)) ctrlValue[0] = 2;
                            else if (Global_Class.BitStatus(COMMDataManager.DevRec.rtv_REC_RTVSt.DevMode, 1)) ctrlValue[0] = 0;
                            else if (Global_Class.BitStatus(COMMDataManager.DevRec.rtv_REC_RTVSt.DevMode, 3)) ctrlValue[0] = 1;
                            if (Global_Class.BitStatus(COMMDataManager.DevRec.rtv_REC_RTVSt.DevMode, 2)) ctrlValue[1] |= 0x01;


                            switch (CtrlData)
                            {
                                case 0: //수동모드
                                    ctrlValue[0] = 0;
                                    break;
                                case 1: //셋업모드
                                    ctrlValue[0] = 1;
                                    break;
                                case 2: //자동모드
                                    ctrlValue[0] = 2;
                                    break;
                                case 10: //강제모드 OFF
                                    ctrlValue[1] &= 0x00;
                                    break;
                                case 11: //강제모드 ON
                                    ctrlValue[1] |= 0x01;
                                    break;
                            }
                            COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, TmpCMD2, ctrlValue);
                            break;
                    }
                    break;
                case ConstClass.TYPE_EMS:
                    switch (TmpCMD2)
                    {
                        case ConstClass.CMD2_58:
                            //현재의 장치의 모드값을 제어 구조체에 반영하고 나서 제어값을 만들어야 한다.
                            byte[] ctrlValue = { 0, 0 };

                            if (Global_Class.BitStatus(COMMDataManager.DevRec.ems_REC_EMSSt.DevMode, 0)) ctrlValue[0] = 2;
                            else if (Global_Class.BitStatus(COMMDataManager.DevRec.ems_REC_EMSSt.DevMode, 1)) ctrlValue[0] = 0;
                            else if (Global_Class.BitStatus(COMMDataManager.DevRec.rtv_REC_RTVSt.DevMode, 3)) ctrlValue[0] = 1;
                            if (Global_Class.BitStatus(COMMDataManager.DevRec.ems_REC_EMSSt.DevMode, 2)) ctrlValue[1] |= 0x01;


                            switch (CtrlData)
                            {
                                case 0: //수동모드
                                    ctrlValue[0] = 0;
                                    break;
                                case 1: //셋업모드
                                    ctrlValue[0] = 1;
                                    break;
                                case 2: //자동모드
                                    ctrlValue[0] = 2;
                                    break;
                                case 10: //강제모드 OFF
                                    ctrlValue[1] &= 0x00;
                                    break;
                                case 11: //강제모드 ON
                                    ctrlValue[1] |= 0x01;
                                    break;
                            }
                            COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, TmpCMD2, ctrlValue);
                            break;
                    }
                    break;
                default:
                    break;
            }
            
        }

        /*!
         * 수동 조그 → 송신(프로토콜 문서 "0x0080 수동명령"과 대응).
         * - UI(Form_RTV_CTL 등)가 Manual_DEV_CtrlRec에 CtrlTypeValue·LowSpeedRef를 넣고 호출.
         * - 문서의 0080은 보통 CMD1=0x00, CMD2=0x80 두 바이트로 표기한 것이며, 본문은 TDEV_ManualCtrl(dev_REC_ManualCtrl) 직렬화.
         * - MouseDown: CtrlTypeValue=버튼 Tag(11·12·…). MouseUp: CtrlTypeValue=0, CtrlTypeValue_before=방금 Tag → 정지용 프레임.
         * - CtrlTypeValue==0 분기에서 IStwice이면 송신 큐 비운 뒤 CMD2_80을 두 번 연속 넣음(정지/전환 시퀀스).
         * - 누르고 있는 동안은 uCommClass에서 약 200ms마다 OnCheckJogCtrl→본 함수로 동일 조그 재전송.
         */
        public unsafe void Do_JogCtrl()
        {
            bool IStwice = false;

            if (COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue == 0xFF)
            {
               // label1.BackColor = label4.BackColor;

            } else
            {
      //          label1.BackColor = System.Drawing.Color.Yellow;
            }


            if (COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue == 0xFF) return;

            if (COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue == 0 &&
                COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue_before == 0)
            {
                return;
            }

            // 송신 큐에 데이터가 있어도 동일 조그 코드 재전송 허용(누르고 있는 동안 200ms 폴링·MCU 유지용).
            // 예전 UserDataCount==vOld 차단은 통신 정상인데도 조그가 막히는 경우가 있어 제거함.

            COMMDataManager.RefreshTxRepeatCtrlCheckTime();

            COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue_OLD = COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue;


            fixed (VEXI_DEFS.TDEV_ManualCtrl* DevCtrl = &COMMDataManager.DevRec.dev_REC_ManualCtrl)
            {

                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_ManualCtrl)));

                byte CtrlTypeValue = COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue;
                byte CtrlTypeValue_before = COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue_before;


                DevCtrl->LowSpeed_Ref = COMMDataManager.DevRec.Manual_DEV_CtrlRec.LowSpeedRef;


                switch (CtrlTypeValue)
                {
                    case 0:
                        switch (CtrlTypeValue_before)
                        {
                            case 11: DevCtrl->CtrlFlag[0] = 0x01; break;
                            case 12: DevCtrl->CtrlFlag[0] = 0x01; break;
                            case 13: DevCtrl->CtrlFlag[0] = 0x01; break;
                            case 14: DevCtrl->CtrlFlag[0] = 0x01; break;
                            case 21: DevCtrl->CtrlFlag[0] = 0x02; break;
                            case 22: DevCtrl->CtrlFlag[0] = 0x02; break;
                            case 23: DevCtrl->CtrlFlag[0] = 0x02; break;
                            case 24: DevCtrl->CtrlFlag[0] = 0x02; break;
                            case 31: DevCtrl->CtrlFlag[0] = 0x04; break;
                            case 32: DevCtrl->CtrlFlag[0] = 0x04; break;
                            case 33: DevCtrl->CtrlFlag[0] = 0x04; break;
                            case 34: DevCtrl->CtrlFlag[0] = 0x04; break;
                            case 35: DevCtrl->CtrlFlag[0] = 0x04; break;
                            case 41: DevCtrl->CtrlFlag[0] = 0x08; break;
                            case 42: DevCtrl->CtrlFlag[0] = 0x08; break;
                            case 43: DevCtrl->CtrlFlag[0] = 0x08; break;
                            case 44: DevCtrl->CtrlFlag[0] = 0x08; break;
                            case 45: DevCtrl->CtrlFlag[0] = 0x08; break;
                            case 72: DevCtrl->CtrlFlag[0] = 0x0C; break;
                            case 73: DevCtrl->CtrlFlag[0] = 0x0C; break;
                            case 74: DevCtrl->CtrlFlag[0] = 0x0C; break;
                            case 75: DevCtrl->CtrlFlag[0] = 0x0C; break;
                            default: DevCtrl->CtrlFlag[0] = 0x0F; break;
                        }
                        IStwice = true;
                        break;
                    case 11:
                        DevCtrl->CtrlFlag[0] = 0x01;
                        DevCtrl->Drive = 1;
                        break;
                    case 12:
                        DevCtrl->CtrlFlag[0] = 0x01;
                        DevCtrl->Drive = 2;
                        break;
                    case 13:
                        DevCtrl->CtrlFlag[0] = 0x01;
                        DevCtrl->Drive = 11;
                        break;
                    case 14:
                        DevCtrl->CtrlFlag[0] = 0x01;
                        DevCtrl->Drive = 12;
                        break;
                    case 21:
                        DevCtrl->CtrlFlag[0] = 0x02;
                        DevCtrl->Updown = 1;
                        break;
                    case 22:
                        DevCtrl->CtrlFlag[0] = 0x02;
                        DevCtrl->Updown = 2;
                        break;
                    case 23:
                        DevCtrl->CtrlFlag[0] = 0x02;
                        DevCtrl->Updown = 11;
                        break;
                    case 24:
                        DevCtrl->CtrlFlag[0] = 0x02;
                        DevCtrl->Updown = 12;
                        break;
                    case 31:
                        DevCtrl->CtrlFlag[0] = 0x04;
                        DevCtrl->Fork1 = 1;
                        break;
                    case 32:
                        DevCtrl->CtrlFlag[0] = 0x04;
                        DevCtrl->Fork1 = 2;
                        break;
                    case 33:
                        DevCtrl->CtrlFlag[0] = 0x04;
                        DevCtrl->Fork1 = 3;
                        break;
                    case 34:
                        DevCtrl->CtrlFlag[0] = 0x04;
                        if (COMMDataManager.RX_DestDevType == ConstClass.TYPE_EMS)
                        {
                            DevCtrl->Fork1 = 4;
                        }
                        else
                        {
                            DevCtrl->Fork1 = 12;
                        }
                        break;
                    case 35:
                        DevCtrl->CtrlFlag[0] = 0x04;
                        DevCtrl->Fork1 = 13;
                        break;
                    case 41:
                        DevCtrl->CtrlFlag[0] = 0x08;
                        DevCtrl->Fork2 = 1;
                        break;
                    case 42:
                        DevCtrl->CtrlFlag[0] = 0x08;
                        DevCtrl->Fork2 = 2;
                        break;
                    case 43:
                        DevCtrl->CtrlFlag[0] = 0x08;
                        DevCtrl->Fork2 = 3;
                        break;
                    case 44:
                        DevCtrl->CtrlFlag[0] = 0x08;
                        DevCtrl->Fork2 = 12;
                        break;
                    case 45:
                        DevCtrl->CtrlFlag[0] = 0x0C;
                        DevCtrl->Fork2 = 13;
                        break;
                    case 72:
                        DevCtrl->CtrlFlag[0] = 0x0C;
                        DevCtrl->Fork1 = 2;
                        DevCtrl->Fork2 = 2;
                        break;
                    case 73:
                        DevCtrl->CtrlFlag[0] = 0x0C;
                        DevCtrl->Fork1 = 3;
                        DevCtrl->Fork2 = 3;
                        break;
                    case 74:
                        DevCtrl->CtrlFlag[0] = 0x0C;
                        DevCtrl->Fork1 = 12;
                        DevCtrl->Fork2 = 12;
                        break;
                    case 75:
                        DevCtrl->CtrlFlag[0] = 0x0C;
                        DevCtrl->Fork1 = 13;
                        DevCtrl->Fork2 = 13;
                        break;
                    default:
                        return;
                }
            }

            // ISCOMM_ResponsGood 차단 제거: UDP는 열렸는데 폴링 타이밍만 어긋나 조그가 막히는 현장 대응.
            // CommSt==0 이면 ADD_TxUserData는 큐에 넣지 않음 — 시작 시 자동 UDP 연결(TryConnectUdpFromCurrentSettings) 권장.

            // TYPE_02·CMD1_00·CMD2_0x80 + 바디 길이는 ADD_TxUserData 내부에서 Marshal.SizeOf(TDEV_ManualCtrl) 기준으로 헤더에 설정됨.
            if (IStwice)
            {
                COMMDataManager.ADD_TxUserDataBeforeClear(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_80, COMMDataManager.DevRec.dev_REC_ManualCtrl);
                COMMDataManager.ADD_TxUserData           (ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_80, COMMDataManager.DevRec.dev_REC_ManualCtrl);
            } else
            {
                COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_80, COMMDataManager.DevRec.dev_REC_ManualCtrl);
            }
            
            if (COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue == 0)
            {
                COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue = 0xFF;
                COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue_OLD = 0xFF;
            } else
            {
                COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue_before = COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue;
            }
        }

        private void Display_DevErrSt()
        {
            if ((COMMDataManager.CommSt == 0) || (!COMMDataManager.ISCOMM_ResponsGood))
            {
                lbl_DevError.Text = "정상 상태";
                lbl_DevError.BackColor = System.Drawing.Color.Gray;
                lbl_DevError.ForeColor = System.Drawing.Color.Black;
                lbl_DevErrorCode.Visible = false;
                return;
            }

            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    if (Global_Class.BitStatus(COMMDataManager.DevRec.srm_REC_SRMSt.DevSt_1, 3))
                    {
                        lbl_DevError.Text = "장애 상태";
                        lbl_DevError.BackColor = System.Drawing.Color.Red;
                        lbl_DevError.ForeColor = System.Drawing.Color.White;
                        lbl_DevErrorCode.Visible = true;
                        lbl_DevErrorCode.BackColor = System.Drawing.Color.Red;
                        lbl_DevErrorCode.ForeColor = System.Drawing.Color.White;
                    }
                    else if (Global_Class.BitStatus(COMMDataManager.DevRec.srm_REC_SRMSt.DevSt_1, 2))
                    {
                        lbl_DevError.Text = "경고 상태";
                        lbl_DevError.BackColor = System.Drawing.Color.Orange;
                        lbl_DevError.ForeColor = System.Drawing.Color.Black;
                        lbl_DevErrorCode.Visible = true;
                        lbl_DevErrorCode.BackColor = System.Drawing.Color.Orange;
                        lbl_DevErrorCode.ForeColor = System.Drawing.Color.Black;
                    } else
                    {
                        lbl_DevError.Text = "정상 상태";
                        lbl_DevError.BackColor = System.Drawing.Color.Lime;
                        lbl_DevError.ForeColor = System.Drawing.Color.Black;
                        lbl_DevErrorCode.Visible = false;
                    }
                    lbl_DevErrorCode.Text = String.Format("{0}-{1}-{2}", COMMDataManager.DevRec.srm_REC_SRMSt.ErrorCode.MainCode, COMMDataManager.DevRec.srm_REC_SRMSt.ErrorCode.SubCode, COMMDataManager.DevRec.srm_REC_SRMSt.ErrorCode.PosCode);
                    break;
                case ConstClass.TYPE_RTV:
                    if (Global_Class.BitStatus(COMMDataManager.DevRec.rtv_REC_RTVSt.DevSt_1, 3))
                    {
                        lbl_DevError.Text = "장애 상태";
                        lbl_DevError.BackColor = System.Drawing.Color.Red;
                        lbl_DevError.ForeColor = System.Drawing.Color.White;
                        lbl_DevErrorCode.Visible = true;
                        lbl_DevErrorCode.BackColor = System.Drawing.Color.Red;
                        lbl_DevErrorCode.ForeColor = System.Drawing.Color.White;
                    }
                    else if (Global_Class.BitStatus(COMMDataManager.DevRec.rtv_REC_RTVSt.DevSt_1, 2))
                    {
                        lbl_DevError.Text = "경고 상태";
                        lbl_DevError.BackColor = System.Drawing.Color.Orange;
                        lbl_DevError.ForeColor = System.Drawing.Color.Black;
                        lbl_DevErrorCode.Visible = true;
                        lbl_DevErrorCode.BackColor = System.Drawing.Color.Orange;
                        lbl_DevErrorCode.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        lbl_DevError.Text = "정상 상태";
                        lbl_DevError.BackColor = System.Drawing.Color.Lime;
                        lbl_DevError.ForeColor = System.Drawing.Color.Black;
                        lbl_DevErrorCode.Visible = false;
                    }
                    lbl_DevErrorCode.Text = String.Format("{0}-{1}-{2}", COMMDataManager.DevRec.rtv_REC_RTVSt.ErrorCode.MainCode, COMMDataManager.DevRec.rtv_REC_RTVSt.ErrorCode.SubCode, COMMDataManager.DevRec.rtv_REC_RTVSt.ErrorCode.PosCode);
                    break;
                case ConstClass.TYPE_EMS:
                    if (Global_Class.BitStatus(COMMDataManager.DevRec.ems_REC_EMSSt.DevSt_1, 3))
                    {
                        lbl_DevError.Text = "장애 상태";
                        lbl_DevError.BackColor = System.Drawing.Color.Red;
                        lbl_DevError.ForeColor = System.Drawing.Color.White;
                        lbl_DevErrorCode.Visible = true;
                        lbl_DevErrorCode.BackColor = System.Drawing.Color.Red;
                        lbl_DevErrorCode.ForeColor = System.Drawing.Color.White;
                    }
                    else if (Global_Class.BitStatus(COMMDataManager.DevRec.ems_REC_EMSSt.DevSt_1, 2))
                    {
                        lbl_DevError.Text = "경고 상태";
                        lbl_DevError.BackColor = System.Drawing.Color.Orange;
                        lbl_DevError.ForeColor = System.Drawing.Color.Black;
                        lbl_DevErrorCode.Visible = true;
                        lbl_DevErrorCode.BackColor = System.Drawing.Color.Orange;
                        lbl_DevErrorCode.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        lbl_DevError.Text = "정상 상태";
                        lbl_DevError.BackColor = System.Drawing.Color.Lime;
                        lbl_DevError.ForeColor = System.Drawing.Color.Black;
                        lbl_DevErrorCode.Visible = false;
                    }
                    lbl_DevErrorCode.Text = String.Format("{0}-{1}-{2}", COMMDataManager.DevRec.ems_REC_EMSSt.ErrorCode.MainCode, COMMDataManager.DevRec.ems_REC_EMSSt.ErrorCode.SubCode, COMMDataManager.DevRec.ems_REC_EMSSt.ErrorCode.PosCode);
                    break;
                default:
                    lbl_DevError.Text = "정상 상태";
                    lbl_DevError.BackColor = System.Drawing.Color.Lime;
                    lbl_DevError.ForeColor = System.Drawing.Color.Black;
                    lbl_DevErrorCode.Visible = false;
                    break;
            }
        }
        private void Display_DevForceMode()
        {
            if ((COMMDataManager.CommSt == 0) || (!COMMDataManager.ISCOMM_ResponsGood))
            {
                if (lbl_DevForceMode.Visible)
                {
                    lbl_DevForceMode.Visible = false;
                }
                return;
            }

            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:

                    if (Global_Class.BitStatus(COMMDataManager.DevRec.srm_REC_SRMSt.DevMode, 2))
                    {
                        if (!lbl_DevForceMode.Visible)
                        {
                            lbl_DevForceMode.BackColor = System.Drawing.Color.Red;
                            lbl_DevForceMode.ForeColor = System.Drawing.Color.Yellow;
                            lbl_DevForceMode.Visible = true;
                        }
                        else
                        {
                            if (lbl_DevForceMode.BackColor == System.Drawing.Color.Red)
                            {
                                lbl_DevForceMode.BackColor = System.Drawing.Color.Yellow;
                                lbl_DevForceMode.ForeColor = System.Drawing.Color.Black;
                            }
                            else
                            {
                                lbl_DevForceMode.BackColor = System.Drawing.Color.Red;
                                lbl_DevForceMode.ForeColor = System.Drawing.Color.Yellow;
                            }
                        }
                    } else
                    {
                        if (lbl_DevForceMode.Visible)
                        {
                            lbl_DevForceMode.Visible = false;
                        }
                    }
                    break;
                case ConstClass.TYPE_RTV:
                    if (Global_Class.BitStatus(COMMDataManager.DevRec.rtv_REC_RTVSt.DevMode, 2))
                    {
                        if (!lbl_DevForceMode.Visible)
                        {
                            lbl_DevForceMode.BackColor = System.Drawing.Color.Red;
                            lbl_DevForceMode.ForeColor = System.Drawing.Color.Yellow;
                            lbl_DevForceMode.Visible = true;
                        }
                        else
                        {
                            if (lbl_DevForceMode.BackColor == System.Drawing.Color.Red)
                            {
                                lbl_DevForceMode.BackColor = System.Drawing.Color.Yellow;
                                lbl_DevForceMode.ForeColor = System.Drawing.Color.Black;
                            }
                            else
                            {
                                lbl_DevForceMode.BackColor = System.Drawing.Color.Red;
                                lbl_DevForceMode.ForeColor = System.Drawing.Color.Yellow;
                            }
                        }
                    }
                    else
                    {
                        if (lbl_DevForceMode.Visible)
                        {
                            lbl_DevForceMode.Visible = false;
                        }
                    }
                    break;
                case ConstClass.TYPE_EMS:
                    if (Global_Class.BitStatus(COMMDataManager.DevRec.ems_REC_EMSSt.DevMode, 2))
                    {
                        if (!lbl_DevForceMode.Visible)
                        {
                            lbl_DevForceMode.BackColor = System.Drawing.Color.Red;
                            lbl_DevForceMode.ForeColor = System.Drawing.Color.Yellow;
                            lbl_DevForceMode.Visible = true;
                        }
                        else
                        {
                            if (lbl_DevForceMode.BackColor == System.Drawing.Color.Red)
                            {
                                lbl_DevForceMode.BackColor = System.Drawing.Color.Yellow;
                                lbl_DevForceMode.ForeColor = System.Drawing.Color.Black;
                            }
                            else
                            {
                                lbl_DevForceMode.BackColor = System.Drawing.Color.Red;
                                lbl_DevForceMode.ForeColor = System.Drawing.Color.Yellow;
                            }
                        }
                    }
                    else
                    {
                        if (lbl_DevForceMode.Visible)
                        {
                            lbl_DevForceMode.Visible = false;
                        }
                    }
                    break;
                default:
                    lbl_DevForceMode.Visible = false;
                    break;
            }
        }

        private unsafe void Display_DevFW()
        {
            if ((COMMDataManager.CommSt == 0) || (!COMMDataManager.ISCOMM_ResponsGood))
            {
                lbl_ResponseDevFW.Text = "---";
                return;
            }

            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &COMMDataManager.DevRec.srm_REC_SRMSt)
                    {
                        lbl_ResponseDevFW.Text = Global_Class.UTIL_ByteToFVerstr(DevSt->FWversion);
                    }
                    break;
                case ConstClass.TYPE_RTV:
                    fixed (VEXI_DEFS.TRTV_StatusRes* DevSt = &COMMDataManager.DevRec.rtv_REC_RTVSt)
                    {
                        lbl_ResponseDevFW.Text = Global_Class.UTIL_ByteToFVerstr(DevSt->FWversion);
                    }
                    break;
                case ConstClass.TYPE_EMS:
                    fixed (VEXI_DEFS.TEMS_StatusRes* DevSt = &COMMDataManager.DevRec.ems_REC_EMSSt)
                    {
                        lbl_ResponseDevFW.Text = Global_Class.UTIL_ByteToFVerstr(DevSt->FWversion);
                    }
                    break;
                default:
                    lbl_ResponseDevFW.Text = "---";
                    break;
            }
        }

        private void Display_DevSetupMode()
        {
            if ((COMMDataManager.CommSt == 0) || (!COMMDataManager.ISCOMM_ResponsGood))
            {
                if (lbl_SetUpMode.Visible)
                {
                    lbl_SetUpMode.Visible = false;
                }
                return;
            }

            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:

                    if (Global_Class.BitStatus(COMMDataManager.DevRec.srm_REC_SRMSt.DevMode, 3))
                    {
                        if (!lbl_SetUpMode.Visible)
                        {
                            lbl_SetUpMode.BackColor = System.Drawing.Color.Yellow;
                            lbl_SetUpMode.ForeColor = System.Drawing.Color.Black;
                            lbl_SetUpMode.Visible = true;
                        }
                    }
                    else
                    {
                        if (lbl_SetUpMode.Visible)
                        {
                            lbl_SetUpMode.Visible = false;
                        }
                    }
                    break;
                case ConstClass.TYPE_RTV:
                    if (Global_Class.BitStatus(COMMDataManager.DevRec.rtv_REC_RTVSt.DevMode, 3))
                    {
                        if (!lbl_SetUpMode.Visible)
                        {
                            lbl_SetUpMode.BackColor = System.Drawing.Color.Yellow;
                            lbl_SetUpMode.ForeColor = System.Drawing.Color.Black;
                            lbl_SetUpMode.Visible = true;
                        }
                    }
                    else
                    {
                        if (lbl_SetUpMode.Visible)
                        {
                            lbl_SetUpMode.Visible = false;
                        }
                    }
                    break;
                case ConstClass.TYPE_EMS:
                    if (Global_Class.BitStatus(COMMDataManager.DevRec.ems_REC_EMSSt.DevMode, 3))
                    {
                        if (!lbl_SetUpMode.Visible)
                        {
                            lbl_SetUpMode.BackColor = System.Drawing.Color.Yellow;
                            lbl_SetUpMode.ForeColor = System.Drawing.Color.Black;
                            lbl_SetUpMode.Visible = true;
                        }
                    }
                    else
                    {
                        if (lbl_SetUpMode.Visible)
                        {
                            lbl_SetUpMode.Visible = false;
                        }
                    }
                    break;
                default:
                    lbl_SetUpMode.Visible = false;
                    break;
            }
        }

        private void Display_RealDevInfo(bool TmpIsReceive)
        {
            // 연결되어 있는 실제 장치 타입, ID 표출 및 통신 상태 표출
            if (COMMDataManager.CommSt == 0)
            {
                lbl_ResponseDevType.Text = "---";
                lbl_ResponseDevID.Text = "---";
                lbl_ReceviceGood.BackColor = System.Drawing.Color.Gray;
                lbl_ReceviceGood.Text = "통신 불능";
                return;
            }


            if (TmpIsReceive)
            {
                switch (COMMDataManager.RX_DestDevType)
                {
                    case ConstClass.TYPE_SRM: lbl_ResponseDevType.Text = "SRM"; break;
                    case ConstClass.TYPE_RTV: lbl_ResponseDevType.Text = "RTV"; break;
                    case ConstClass.TYPE_EMS: lbl_ResponseDevType.Text = "EMS"; break;
                    default: lbl_ResponseDevType.Text = string.Format("{0:X2}", COMMDataManager.RX_DestDevType); break;
                }

                lbl_ResponseDevID.Text = COMMDataManager.RX_DestDevID.ToString();
            }

            if (COMMDataManager.CommSt != 0)
            {

                if (COMMDataManager.ISCOMM_ResponsGood)
                {
                    lbl_ReceviceGood.BackColor = System.Drawing.Color.Lime;
                    lbl_ReceviceGood.Text = "통신 정상";
                }
                else
                {
                    if (lbl_ReceviceGood.BackColor == System.Drawing.Color.Lime)
                    {
                        lbl_ReceviceGood.BackColor = System.Drawing.Color.Gray;
                    } else if (lbl_ReceviceGood.BackColor == System.Drawing.Color.Gray)
                    {
                        lbl_ReceviceGood.BackColor = System.Drawing.Color.Red;
                    } else if (lbl_ReceviceGood.BackColor == System.Drawing.Color.Red)
                    {
                        lbl_ReceviceGood.BackColor = System.Drawing.Color.Gray;
                    }
                    lbl_ReceviceGood.Text = "통신 불능";
                    lbl_DevError.Text = "정상 상태";
                    lbl_DevError.BackColor = System.Drawing.Color.Gray;
                    lbl_DevError.ForeColor = System.Drawing.Color.Black;
                    lbl_DevErrorCode.Visible = false;
                    lbl_DevForceMode.Visible = false;
                    lbl_SetUpMode.Visible = false;
                }
            }

            if (form_SRM_CTL != null)
            {
                if (!form_SRM_CTL.IsDisposed)
                {
                    form_SRM_CTL.Display_DevCommSt();
                }
            }

            if (form_RTV_CTL != null)
            {
                if (!form_RTV_CTL.IsDisposed)
                {
                    form_RTV_CTL.Display_DevCommSt();
                }
            }

            if (form_EMS_CTL != null)
            {
                if (!form_EMS_CTL.IsDisposed)
                {
                    form_EMS_CTL.Display_DevCommSt();
                }
            }

        }

        private void SaveSystemConfig()
        {
            if (!IsPGInit) return;
            
            //통신 설정, 선택 장치 설정을 환경설정 파일에 저장
            if (COMMDataManager.CommSt != 0)
            {
                IniControl.WriteIni(CONFIG_FILE, "COMM", "ComMode", COMMDataManager.COMM_Mode);

                if (COMMDataManager.COMM_Mode == ConstClass.COMM_SERIAL)
                {
                    IniControl.WriteIni(CONFIG_FILE, "COMM", "Serial_Port", COMMDataManager.SerialPort);
                }
                if (COMMDataManager.COMM_Mode == ConstClass.COMM_UDP)
                {
                 //   if (rb_WCConnect.Checked)
                  //  {
                   //     IniControl.WriteIni(CONFIG_FILE, "COMM", "UDP_IP", COMMDataManager.UDPIP);
                   // }
                }
            }
            IniControl.WriteIni(CONFIG_FILE, "COMM", "SelectDevType", COMMDataManager.SelectDestDevType);
            IniControl.WriteIni(CONFIG_FILE, "COMM", "SelectDevID", COMMDataManager.SelectDestDevID);
        }

        private void LoadSystemConfig()
        {
            //통신 설정, 선택 장치 설정을 환경설정 파일에서 읽어드리기
            int TmpComMode = IniControl.ReadInteger(CONFIG_FILE, "COMM", "ComMode", ConstClass.COMM_SERIAL);
            string TmpSerialPort = IniControl.ReadString(CONFIG_FILE, "COMM", "Serial_Port", "");
            string TmpUDPIP = IniControl.ReadString(CONFIG_FILE, "COMM", "UDP_IP", COMMDataManager.UDPIP);
            int TmpSelectDevType = IniControl.ReadInteger(CONFIG_FILE, "COMM", "SelectDevType", ConstClass.TYPE_SRM);
            int TmpSelectDevID = IniControl.ReadInteger(CONFIG_FILE, "COMM", "SelectDevID", 1);

            switch (TmpComMode)
            {
             //   case ConstClass.COMM_SERIAL: rbCommSerial.Checked = true; break;
             //   case ConstClass.COMM_UDP: rbCommUDP.Checked = true; break;
            }

            if (TmpSerialPort != "")
            {
            //    int TmpIndex = Port_Combox.Items.IndexOf(TmpSerialPort);
            //    Port_Combox.SelectedIndex = TmpIndex;
            }

            WCIP = TmpUDPIP;
            //rb_WCConnect.Checked = true;
           // edDevIP.Text = TmpUDPIP;

            switch (TmpSelectDevType)
            {
                case ConstClass.TYPE_SRM: 
                    cbDevType.SelectedIndex = 0; 
                    break;
                case ConstClass.TYPE_RTV:
                    cbDevType.SelectedIndex = 1;
                    break;
                case ConstClass.TYPE_EMS:
                    cbDevType.SelectedIndex = 2;
                    break;
                default:
                    cbDevType.SelectedIndex = 3;
                    break;
            }

            if (TmpSelectDevID > 0)
            {
                if (TmpSelectDevID > cbDevID.Items.Count)
                {
                    cbDevID.SelectedIndex = cbDevID.Items.Count - 1;
                } else
                {
                    cbDevID.SelectedIndex = TmpSelectDevID - 1;
                }
            } else
            {
                cbDevID.SelectedIndex = 0;
            }
            
            CheckSelectedDevice();
        }

        public void Debug_Insert(string msg)
        {
            //Debug 처리
            //Debugging 할 부분이 있으면 "Debug_Insert" 함수를 호출한다
            GlobalObj.DEBUG_Insert(msg);
        }

        private Form ShowActiveChildForm(Form form, Type type)
        {
            //MDI 창 활성화 함수
            if (form == null)
            {
                form = (Form)Activator.CreateInstance(type);
                int TmpHeight = form.Height;
                form.MdiParent = this;
                //form.Dock = DockStyle.Fill;
                form.Dock = DockStyle.Left;
                form.Show();
                int tmpTop = form.Top;
                
                form.Dock = DockStyle.None;
                form.Top = tmpTop;
                form.Height = TmpHeight + 30;
            }
            else
            {
                if (form.IsDisposed)
                {
                    form = (Form)Activator.CreateInstance(type);
                    int TmpHeight = form.Height;
                    form.MdiParent = this;
                    //form.Dock = DockStyle.Fill;
                    form.Dock = DockStyle.Left;
                    form.Show();
                    int tmpTop = form.Top;

                    form.Dock = DockStyle.None;
                    form.Top = tmpTop;
                    form.Height = TmpHeight + 30;

                }
                else
                {
                    form.Activate();
                }
            }
            return form;
        }

        private Form ShowActiveSingleForm(Form form, Type type)
        {
            //Single 창 활성화 함수
            if (form == null)
            {
                form = (Form)Activator.CreateInstance(type);
                form.Owner = this;
                form.Show();
            }
            else
            {
                if (form.IsDisposed)
                {
                    form = (Form)Activator.CreateInstance(type);
                    form.Owner = this;
                    form.Show();
                }
                else
                {
                    form.Show();
                }
            }
            return form;
        }

        private Form ShowActiveModalForm(Form form, Type type)
        {
            //Single 창 활성화 함수
            if (form == null)
            {
                form = (Form)Activator.CreateInstance(type);
                form.Owner = this;
                form.ShowDialog();
            }
            else
            {
                if (form.IsDisposed)
                {
                    form = (Form)Activator.CreateInstance(type);
                    form.Owner = this;
                    form.ShowDialog();
                }
                else
                {
                    form.ShowDialog();
                }
            }
            return form;
        }

        public void Refresh_Comports()
        {
            //PC의 시리얼포트 리스트를 콤보박스에 구성
            byte[] data = Enumerable.Repeat<byte>(1, 20).ToArray<byte>();

            string[] PortNames = SerialPort.GetPortNames();  // 포트 검색.

          //  int backupindex = Port_Combox.SelectedIndex;
          //  Port_Combox.Items.Clear();
            foreach (string portnumber in PortNames)
            {
            //    Port_Combox.Items.Add(portnumber);          // 검색한 포트를 콤보박스에 입력. 
            }

           // Port_Combox.SelectedIndex = backupindex;
        }

        #endregion

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                case ConstClass.TYPE_RTV:
                case ConstClass.TYPE_EMS:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_DO_TestCtrl)
                        {
                            this.ActiveMdiChild.Close();
                        }
                    }
                    form_DO_TestCtrl = ShowActiveChildForm(form_DO_TestCtrl, typeof(Form_DO_TestCtrl)) as Form_DO_TestCtrl;
                    form_DO_TestCtrl.form_Main = this;
                    break;
                default:
                    break;
            }
        }

        private void Loggingtimer_Tick(object sender, EventArgs e)
        {
            //Logging
            if (COMMDataManager.LoggingMode > 0)
            {
                COMMDataManager.Process_DEVSt_LoggingIntervalCheck(COMMDataManager.SelectDestDevType_WithOutANY); 
            }
           
        }

        private void lbl_DevError_DoubleClick(object sender, EventArgs e)
        {
            menu_DevSt_Click(sender, e);
        }

        private void rb_LanConnect_CheckedChanged(object sender, EventArgs e)
        {
            if (pnUDPConnectType.Visible)
            {
               // if ((rb_WCConnect.Checked))
               // {
             //       edDevIP.Text = WCIP;
                //}
            } 
            else
            {
              //   edDevIP.Text = WCIP;
            }
        }

        private void rb_2Connect_CheckedChanged(object sender, EventArgs e)
        {
            if (pnUDPConnectType.Visible)
            {
                if (rb_2Connect.Checked)
                {
                    UpdateWifiDeviceIP();
                }
            }
        }

        private void rb_5Connect_CheckedChanged(object sender, EventArgs e)
        {
            if (pnUDPConnectType.Visible)
            {
                if (rb_5Connect.Checked)
                {
                    UpdateWifiDeviceIP();
                }
            }
        }

        private void btn_ErrRest_Click(object sender, EventArgs e)
        {
            GlobalObj.MsgBox_Info("장치의 이상을 리셋합니다", "I");

            Do_Ctrl_Cmd_withNoData(ConstClass.CMD2_52);
        }

        private void menu_EventLog_Click(object sender, EventArgs e)
        {
            if ((COMMDataManager.RX_DestDevType != ConstClass.TYPE_SRM) &&
                (COMMDataManager.RX_DestDevType != ConstClass.TYPE_RTV) &&
                (COMMDataManager.RX_DestDevType != ConstClass.TYPE_EMS)) return;

            if (this.ActiveMdiChild != null)
            {
                if (this.ActiveMdiChild != form_DevEventLog)
                {
                    this.ActiveMdiChild.Close();

                }

            }
            form_DevEventLog = ShowActiveChildForm(form_DevEventLog, typeof(Form_DevEventLog)) as Form_DevEventLog;
            form_DevEventLog.form_Main = this;
        }

        private void menu_DebugLog_Click(object sender, EventArgs e)
        {
            if ((COMMDataManager.RX_DestDevType != ConstClass.TYPE_SRM) &&
                (COMMDataManager.RX_DestDevType != ConstClass.TYPE_RTV) &&
                (COMMDataManager.RX_DestDevType != ConstClass.TYPE_EMS)) return;

            if (this.ActiveMdiChild != null)
            {
                if (this.ActiveMdiChild != form_DevDebugLog)
                {
                    this.ActiveMdiChild.Close();

                }

            }
            form_DevDebugLog = ShowActiveChildForm(form_DevDebugLog, typeof(Form_DevDebugLog)) as Form_DevDebugLog;
            form_DevDebugLog.form_Main = this;
        }

        private void lbl_ResponseDevType_DoubleClick(object sender, EventArgs e)
        {
            return;
            
            TPacketClass TmpPaketObj = new TPacketClass();
             int test = Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_BasicStRes));
            

            TmpPaketObj.SetHeader(ConstClass.TYPE_SRM, 1, ConstClass.TYPE_SRM, 1, 0, ConstClass.CMD1_80, ConstClass.CMD2_10, (ushort)(test + 1));
            byte[] a = new byte[test + 1];
            a[2] = 0x30;
            a[3] = 0x23;
            a[4] = 0x24;
            a[5] = 0x11;
            a[6] = 0x03;
            
            TmpPaketObj.SetBody(a);

            OnMainPacketReceived(0, true, TmpPaketObj, 0, 0, 0);
            test = Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_StatusRes));

            TmpPaketObj.SetHeader(ConstClass.TYPE_SRM, 1, ConstClass.TYPE_SRM, 1, 0, ConstClass.CMD1_80, ConstClass.CMD2_30, (ushort)(test + 1));

            byte[] b = new byte[test + 1];
            b[3] = 0x30;
            b[4] = 0x23;
            b[5] = 0x24;
            b[6] = 0x11;
            b[7] = 0x03;
            b[40] = 0x10;
            b[41] = 0x10;
            TmpPaketObj.SetBody(b);
            OnMainPacketReceived(0, true, TmpPaketObj, 0, 0, 0);
            
        }

        private void label5_MouseDown(object sender, MouseEventArgs e)
        {
            AdminLoginView = DateTime.Now;
        }

        private void label5_MouseUp(object sender, MouseEventArgs e)
        {
            TimeSpan dateDiff = DateTime.Now - AdminLoginView;

            if (dateDiff.Seconds >= 2)
            {
                //edAdmin.Visible = true;
                //edAdmin.Text = "";
            }
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            AdminLoginView = DateTime.Now;
        }

        private void edAdmin_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void edAdmin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                /*
                if (edAdmin.Text == "GR")
                {
                    MenuItem_Graph.Visible = true;
                    MenuItem_RunTime.Visible = false;
                    menu_EventLog.Visible = false;
                    menu_DebugLog.Visible = false;
                    menu_ViewCommData.Visible = false;
                }

                if (edAdmin.Text == "RT")
                { 
                    MenuItem_Graph.Visible = false;
                    MenuItem_RunTime.Visible = true;
                    menu_EventLog.Visible = false;
                    menu_DebugLog.Visible = false;
                    menu_ViewCommData.Visible = false;
                }

                if (edAdmin.Text == "EL")
                {
                    MenuItem_Graph.Visible = false;
                    MenuItem_RunTime.Visible = false;
                    menu_EventLog.Visible = true;
                    menu_DebugLog.Visible = false;
                    menu_ViewCommData.Visible = false;
                }

                if (edAdmin.Text == "DL")
                {
                    MenuItem_Graph.Visible = false;
                    MenuItem_RunTime.Visible = false;
                    menu_EventLog.Visible = false;
                    menu_DebugLog.Visible = true;
                    menu_ViewCommData.Visible = false;
                }

                if (edAdmin.Text == "CD")
                {
                    MenuItem_Graph.Visible = false;
                    MenuItem_RunTime.Visible = false;
                    menu_EventLog.Visible = false;
                    menu_DebugLog.Visible = false;
                    menu_ViewCommData.Visible = true;
                }

                if (edAdmin.Text == "ON")
                {
                    MenuItem_Graph.Visible = true;
                    MenuItem_RunTime.Visible = true;
                    menu_EventLog.Visible = true;
                    menu_DebugLog.Visible = true;
                    menu_ViewCommData.Visible = true;
                }

                if (edAdmin.Text == "OF")
                {
                    MenuItem_Graph.Visible = false;
                    MenuItem_RunTime.Visible = false;
                    menu_EventLog.Visible = false;
                    menu_DebugLog.Visible = false;
                    menu_ViewCommData.Visible = false;
                }
                */
                //edAdmin.Visible = false;
            }
        }

        private void movexWCSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    break;
                case ConstClass.TYPE_RTV:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_MovexWCSMemoryMap)
                        {
                            this.ActiveMdiChild.Close();

                        }

                    }
                    form_MovexWCSMemoryMap = ShowActiveChildForm(form_MovexWCSMemoryMap, typeof(Form_MovexWCSMemoryMap)) as Form_MovexWCSMemoryMap;
                    form_MovexWCSMemoryMap.form_Main = this;
                    break;
                case ConstClass.TYPE_EMS:
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != form_MovexWCSMemoryMap)
                        {
                            this.ActiveMdiChild.Close();

                        }

                    }
                    form_MovexWCSMemoryMap = ShowActiveChildForm(form_MovexWCSMemoryMap, typeof(Form_MovexWCSMemoryMap)) as Form_MovexWCSMemoryMap;
                    form_MovexWCSMemoryMap.form_Main = this;
                    break;
                default:
                    break;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            

        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void edAdmin_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_SelectDev_Click(object sender, EventArgs e)
        {

        }

        private void lbl_ResponseDevType_Click(object sender, EventArgs e)
        {

        }
    }
}

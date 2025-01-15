using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_TimeInfo : Form
    {

        public Form_Main form_Main;
        private static VEXI_DEFS.TDEV_REC_OpInfoRes dev_REC_OpInfo;
        private static VEXI_DEFS.TDEV_REC_OPInfoReq dev_REQ_OpInfo;



        public Form_TimeInfo()
        {
            InitializeComponent();

        }
        #region 컴포넌트 이벤트

        private void Form_TimeInfo_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }

            Display_Init();
        }
        private void btn_Req_Click(object sender, EventArgs e)
        {
            dev_REQ_OpInfo.ReqType = 0;
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_31, dev_REQ_OpInfo);
        }

        #endregion

        #region 기능함수
        public void Display_Init()
        {
            switch (form_Main.COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_RTV:
                    {
                        lbl_UpDownTitle.Visible = false;
                        lbl_UpDownStart.Visible = false;
                        lbl_UpDownFrom.Visible = false;
                        lbl_UpDownTo.Visible = false;

                        lbl_TimeTitle1.Text = "명령 수신 (ms)";
                        lbl_TimeTitle2.Text = "예비";
                        lbl_TimeTitle3.Text = "예비";
                        lbl_TimeTitle4.Text = "예비";
                        lbl_TimeTitle5.Text = "예비";
                        lbl_TimeTitle6.Text = "예비";
                        lbl_TimeTitle7.Text = "예비";
                        lbl_TimeTitle8.Text = "예비";
                        lbl_TimeTitle9.Text = "예비";
                        lbl_TimeTitle10.Text = "예비";
                        lbl_TimeTitle11.Text = "To 출발 시작 (ms)";
                        lbl_TimeTitle12.Text = "To 도착 완료 (ms)";
                        lbl_TimeTitle13.Text = "피딩 시작 (ms)";
                        lbl_TimeTitle14.Text = "피딩 완료 (ms)";
                        lbl_TimeTitle15.Text = "예비";
                        lbl_TimeTitle16.Text = "예비";
                        lbl_TimeTitle17.Text = "예비";
                        lbl_TimeTitle18.Text = "예비";
                        lbl_TimeTitle19.Text = "예비";
                        lbl_TimeTitle20.Text = "작업 완료 (ms)";
                        lbl_TimeTitle21.Text = "예비";
                        lbl_TimeTitle22.Text = "예비";
                        lbl_TimeTitle23.Text = "예비";
                        lbl_TimeTitle24.Text = "예비";

                        lbl_DriveStart.Text = dev_REC_OpInfo.Timeinfo.StartPosition_Drive.ToString();
                        lbl_DriveFrom.Text = "-";
                        lbl_DriveTo.Text = dev_REC_OpInfo.Timeinfo.ToPosition_Drive.ToString();

                        lbl_Time1.Text = dev_REC_OpInfo.Timeinfo.Time_1.ToString();
                        lbl_Time2.Text = "-";
                        lbl_Time3.Text = "-";
                        lbl_Time4.Text = "-";
                        lbl_Time5.Text = "-";
                        lbl_Time6.Text = "-";
                        lbl_Time7.Text = "-";
                        lbl_Time8.Text = "-";
                        lbl_Time9.Text = "-";
                        lbl_Time10.Text = "-";
                        lbl_Time11.Text = dev_REC_OpInfo.Timeinfo.Time_11.ToString();
                        lbl_Time12.Text = dev_REC_OpInfo.Timeinfo.Time_12.ToString();
                        lbl_Time13.Text = dev_REC_OpInfo.Timeinfo.Time_13.ToString();
                        lbl_Time14.Text = dev_REC_OpInfo.Timeinfo.Time_14.ToString();
                        lbl_Time15.Text = "-";
                        lbl_Time16.Text = "-";
                        lbl_Time17.Text = "-";
                        lbl_Time18.Text = "-";
                        lbl_Time19.Text = "-";
                        lbl_Time20.Text = dev_REC_OpInfo.Timeinfo.Time_20.ToString();
                        lbl_Time21.Text = "-";
                        lbl_Time22.Text = "-";
                        lbl_Time23.Text = "-";
                        lbl_Time24.Text = "-";
                    }
                    break;
                case ConstClass.TYPE_EMS:
                    {
                        lbl_UpDownTitle.Visible = false;
                        lbl_UpDownStart.Visible = false;
                        lbl_UpDownFrom.Visible = false;
                        lbl_UpDownTo.Visible = false;
                    }
                    break;
                default:
                    {
                        lbl_UpDownTitle.Visible = true;
                        lbl_UpDownStart.Visible = true;
                        lbl_UpDownFrom.Visible = true;
                        lbl_UpDownTo.Visible = true;

                        lbl_TimeTitle1.Text = "명령 수신 (ms)";
                        lbl_TimeTitle2.Text = "From 출발 시작 (ms)";
                        lbl_TimeTitle3.Text = "From 도착 완료 (ms)";
                        lbl_TimeTitle4.Text = "포크 진출 시작 (ms)";
                        lbl_TimeTitle5.Text = "포크 진출 완료 (ms)";
                        lbl_TimeTitle6.Text = "포크 상승 시작 (ms)";
                        lbl_TimeTitle7.Text = "포크 상승 완료 (ms)";
                        lbl_TimeTitle8.Text = "포크 중심 시작 (ms)";
                        lbl_TimeTitle9.Text = "포크 중심 완료 (ms)";
                        lbl_TimeTitle10.Text = "화물 적재 완료 (ms)";
                        lbl_TimeTitle11.Text = "To 출발 시작 (ms)";
                        lbl_TimeTitle12.Text = "To 도착 완료 (ms)";
                        lbl_TimeTitle13.Text = "포크 진출 시작 (ms)";
                        lbl_TimeTitle14.Text = "포크 진출 완료 (ms)";
                        lbl_TimeTitle15.Text = "포크 하강 시작 (ms)";
                        lbl_TimeTitle16.Text = "포크 하강 완료 (ms)";
                        lbl_TimeTitle17.Text = "포크 중심 시작 (ms)";
                        lbl_TimeTitle18.Text = "포크 중심 완료 (ms)";
                        lbl_TimeTitle19.Text = "화물 이재 완료 (ms)";
                        lbl_TimeTitle20.Text = "작업 완료 (ms)";
                        lbl_TimeTitle21.Text = "예비";
                        lbl_TimeTitle22.Text = "예비";
                        lbl_TimeTitle23.Text = "예비";
                        lbl_TimeTitle24.Text = "예비";

                        lbl_DriveStart.Text = dev_REC_OpInfo.Timeinfo.StartPosition_Drive.ToString();
                        lbl_DriveFrom.Text = dev_REC_OpInfo.Timeinfo.FromPosition_Drive.ToString();
                        lbl_DriveTo.Text = dev_REC_OpInfo.Timeinfo.ToPosition_Drive.ToString();
                        lbl_UpDownStart.Text = dev_REC_OpInfo.Timeinfo.StartPosition_UpDown.ToString();
                        lbl_UpDownFrom.Text = dev_REC_OpInfo.Timeinfo.FromPosition_UpDown.ToString();
                        lbl_UpDownTo.Text = dev_REC_OpInfo.Timeinfo.ToPosition_UpDown.ToString();

                        lbl_Time1.Text = dev_REC_OpInfo.Timeinfo.Time_1.ToString();
                        lbl_Time2.Text = dev_REC_OpInfo.Timeinfo.Time_2.ToString();
                        lbl_Time3.Text = dev_REC_OpInfo.Timeinfo.Time_3.ToString();
                        lbl_Time4.Text = dev_REC_OpInfo.Timeinfo.Time_4.ToString();
                        lbl_Time5.Text = dev_REC_OpInfo.Timeinfo.Time_5.ToString();
                        lbl_Time6.Text = dev_REC_OpInfo.Timeinfo.Time_6.ToString();
                        lbl_Time7.Text = dev_REC_OpInfo.Timeinfo.Time_7.ToString();
                        lbl_Time8.Text = dev_REC_OpInfo.Timeinfo.Time_8.ToString();
                        lbl_Time9.Text = dev_REC_OpInfo.Timeinfo.Time_9.ToString();
                        lbl_Time10.Text = dev_REC_OpInfo.Timeinfo.Time_10.ToString();
                        lbl_Time11.Text = dev_REC_OpInfo.Timeinfo.Time_11.ToString();
                        lbl_Time12.Text = dev_REC_OpInfo.Timeinfo.Time_12.ToString();
                        lbl_Time13.Text = dev_REC_OpInfo.Timeinfo.Time_13.ToString();
                        lbl_Time14.Text = dev_REC_OpInfo.Timeinfo.Time_14.ToString();
                        lbl_Time15.Text = dev_REC_OpInfo.Timeinfo.Time_15.ToString();
                        lbl_Time16.Text = dev_REC_OpInfo.Timeinfo.Time_16.ToString();
                        lbl_Time17.Text = dev_REC_OpInfo.Timeinfo.Time_17.ToString();
                        lbl_Time18.Text = dev_REC_OpInfo.Timeinfo.Time_18.ToString();
                        lbl_Time19.Text = dev_REC_OpInfo.Timeinfo.Time_19.ToString();
                        lbl_Time20.Text = dev_REC_OpInfo.Timeinfo.Time_20.ToString();
                        lbl_Time21.Text = dev_REC_OpInfo.Timeinfo.Time_21.ToString();
                        lbl_Time22.Text = dev_REC_OpInfo.Timeinfo.Time_22.ToString();
                        lbl_Time23.Text = dev_REC_OpInfo.Timeinfo.Time_23.ToString();
                        lbl_Time24.Text = dev_REC_OpInfo.Timeinfo.Time_24.ToString();

                    }
                    break;
            }

        }
        //데이터 화면 표출 함수
        public void Display_TimeInfo(byte[] datas)
        {
            dev_REC_OpInfo = (VEXI_DEFS.TDEV_REC_OpInfoRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TDEV_REC_OpInfoRes));
            Display_Init();
        }



        #endregion

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label49_Click(object sender, EventArgs e)
        {

        }
    }
}

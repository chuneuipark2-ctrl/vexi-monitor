using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_OpInfo : Form
    {

        public Form_Main form_Main;
        private static VEXI_DEFS.TDEV_REC_OpInfoRes dev_REC_OpInfo;
        private static VEXI_DEFS.TDEV_REC_OPInfoReq dev_REQ_OpInfo;
        


        public Form_OpInfo()
        {
            InitializeComponent();

        }
        #region 컴포넌트 이벤트

        private void Form_OpInfo_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }
        }
        private void btn_Req_Click(object sender, EventArgs e)
        {
            dev_REQ_OpInfo.ReqType = 0;
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_31, dev_REQ_OpInfo);
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "운행정보값을 초기화하시겠습니까?"))
            {
                dev_REQ_OpInfo.ReqType = 1;
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_31, dev_REQ_OpInfo);
            }
        }
        #endregion

        #region 기능함수
        //데이터 화면 표출 함수
        public void Display_OpInfo(byte[] datas)
        {
            dev_REC_OpInfo = (VEXI_DEFS.TDEV_REC_OpInfoRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TDEV_REC_OpInfoRes));

            lbl_DriveTime_Hour.Text = string.Format("{0}", dev_REC_OpInfo.DriveTime_Hour);
            lbl_LiftTime_Hour.Text = string.Format("{0}", dev_REC_OpInfo.LiftTime_Hour);
            lbl_Fork1Time_Hour.Text = string.Format("{0}", dev_REC_OpInfo.Fork1Time_Hour);
            lbl_Fork2Time_Hour.Text = string.Format("{0}", dev_REC_OpInfo.Fork2Time_Hour);

            lbl_DriveDistance_KM.Text = string.Format("{0}", dev_REC_OpInfo.DriveDistance_KM);
            lbl_LiftDistance_KM.Text = string.Format("{0}", dev_REC_OpInfo.LiftDistance_KM);
            lbl_Fork1Distance_KM.Text = string.Format("{0}", dev_REC_OpInfo.Fork1Distance_KM);
            lbl_Fork2Distance_KM.Text = string.Format("{0}", dev_REC_OpInfo.Fork2Distance_KM);

            lbl_DriveCount.Text = string.Format("{0}", dev_REC_OpInfo.DriveCount);
            lbl_LiftCount.Text = string.Format("{0}", dev_REC_OpInfo.LiftCount);
            lbl_Fork1Count.Text = string.Format("{0}", dev_REC_OpInfo.Fork1Count);
            lbl_Fork2Count.Text = string.Format("{0}", dev_REC_OpInfo.Fork2Count);
        }

        #endregion

       
    }
}

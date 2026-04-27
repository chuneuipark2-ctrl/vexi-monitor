using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_RackInitial : Form
    {

        public Form_Main form_Main;
       


        public Form_RackInitial()
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
            switch (form_Main.COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    lbl_SRMWarning.Visible = true;
                    lbl_RTVEMSWarning.Visible = false;
                    btn_AreaConfigClear.Visible = false;
                    break;
                case ConstClass.TYPE_RTV:
                    lbl_SRMWarning.Visible = false;
                    lbl_RTVEMSWarning.Visible = true;
                    btn_AreaConfigClear.Visible = true;
                    break;
                case ConstClass.TYPE_EMS:
                    lbl_SRMWarning.Visible = false;
                    lbl_RTVEMSWarning.Visible = true;
                    btn_AreaConfigClear.Visible = true;
                    break;
            }

        }
        private void btn_Req_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            switch (form_Main.COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    if ((form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode & 0x08) == 0)
                    {
                        form_Main.GlobalObj.MsgBox_Confirm_OK("셋업모드가 아닙니다");
                        return;
                    }
                    break;
                case ConstClass.TYPE_RTV:
                    if ((form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.DevMode & 0x08) == 0)
                    {
                        form_Main.GlobalObj.MsgBox_Confirm_OK("셋업모드가 아닙니다");
                        return;
                    }
                    break;
                case ConstClass.TYPE_EMS:
                    if ((form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.DevMode & 0x08) == 0)
                    {
                        form_Main.GlobalObj.MsgBox_Confirm_OK("셋업모드가 아닙니다");
                        return;
                    }
                    break;
            }


            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "위치 및 랙 설정을 모두 초기화하시겠습니까?"))
            {

                byte[] Data = { 0, 0 };

                switch (form_Main.COMMDataManager.RX_DestDevType)
                {
                    case ConstClass.TYPE_SRM:
                        Data[0] = 0x01;
                        Data[1] = 0xFC;
                        break;
                    case ConstClass.TYPE_RTV:
                        Data[0] = 0x02;
                        Data[1] = 0xD2;
                        break;
                    case ConstClass.TYPE_EMS:
                        Data[0] = 0x03;
                        Data[1] = 0xD2;
                        break;
                }

                form_Main.Do_Ctrl_Cmd_withbytes(ConstClass.CMD1_00, ConstClass.CMD2_A0, Data);
            }
        }

        private void btn_AreaConfigClear_Click(object sender, EventArgs e)
        {

            if ((form_Main.COMMDataManager.RX_DestDevType == ConstClass.TYPE_RTV) || (form_Main.COMMDataManager.RX_DestDevType == ConstClass.TYPE_EMS))
            {
                switch (form_Main.COMMDataManager.RX_DestDevType)
                {
                    case ConstClass.TYPE_RTV:
                        if ((form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.DevMode & 0x08) == 0)
                        {
                            form_Main.GlobalObj.MsgBox_Confirm_OK("셋업모드가 아닙니다");
                            return;
                        }
                        break;
                    case ConstClass.TYPE_EMS:
                        if ((form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.DevMode & 0x08) == 0)
                        {
                            form_Main.GlobalObj.MsgBox_Confirm_OK("셋업모드가 아닙니다");
                            return;
                        }
                        break;
                }

                if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "구간 설정을 초기화하시겠습니까?"))
                {

                    byte[] Data = { 0, 0 };

                    switch (form_Main.COMMDataManager.RX_DestDevType)
                    {
                        case ConstClass.TYPE_RTV:
                            Data[0] = 0x02;
                            Data[1] = 0x02;
                            break;
                        case ConstClass.TYPE_EMS:
                            Data[0] = 0x03;
                            Data[1] = 0x02;
                            break;
                    }

                    form_Main.Do_Ctrl_Cmd_withbytes(ConstClass.CMD1_00, ConstClass.CMD2_A0, Data);
                }

            }
        }
        #endregion

        #region 기능함수

        #endregion


    }
}

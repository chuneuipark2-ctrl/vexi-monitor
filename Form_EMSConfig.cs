using System;
using System.Net;
using System.Windows.Forms;
using System.Runtime.InteropServices;   //DllImport

namespace VEXI
{
    public partial class Form_EMSConfig : Form
    {

        public Form_Main form_Main;
        private VEXI_DEFS.TEMS_DevConfigRes ems_REC_DEVConfigStatus;
        private static VEXI_DEFS.TEMS_DevConfigCtrl ems_REC_DEVConfigCtrl;

        public Form_EMSConfig()
        {
            InitializeComponent();
        }

        private void Form_EMSConfig_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }
            Display_EMSConfig();

            btn_Set.Enabled = false;
        }

        public unsafe void Display_EMSConfig(byte[] Data)
        {
            ems_REC_DEVConfigStatus = (VEXI_DEFS.TEMS_DevConfigRes)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TEMS_DevConfigRes));

            Display_EMSConfig();
        }

        private unsafe void Display_EMSConfig()
        { 
            fixed (VEXI_DEFS.TEMS_DevConfigRes* DevSt = &ems_REC_DEVConfigStatus)
            {
                switch (DevSt->EMSsType)
                {
                    case 1: lbl_EMSsType.Text = rb_EMSsType_1.Text; rb_EMSsType_1.Checked = true; break;
                    case 2: lbl_EMSsType.Text = rb_EMSsType_2.Text; rb_EMSsType_2.Checked = true; break;
                    default: lbl_EMSsType.Text = ""; rb_EMSsType_1.Checked = true; break;
                }

                switch (DevSt->InvetorKind)
                {
                    case 1: lbl_InvertorKind.Text = rb_InvertorKind_1.Text; rb_InvertorKind_1.Checked = true; break;
                    default: lbl_InvertorKind.Text = ""; rb_InvertorKind_1.Checked = true; break;
                }

                switch (DevSt->MovePositionSensor)
                {
                    case 1: lbl_MovePositionSensor.Text = rb_MovePositionSensor_1.Text; rb_MovePositionSensor_1.Checked = true; break;
                    case 2: lbl_MovePositionSensor.Text = rb_MovePositionSensor_2.Text; rb_MovePositionSensor_2.Checked = true; break;
                    default: lbl_MovePositionSensor.Text = ""; rb_MovePositionSensor_1.Checked = true; break;
                }

                switch (DevSt->CageType)
                {
                    case 1: lbl_EMSCageType.Text = rb_EMSCageType_1.Text; rb_EMSCageType_1.Checked = true; break;
                    case 2: lbl_EMSCageType.Text = rb_EMSCageType_2.Text; rb_EMSCageType_2.Checked = true; break;
                    default: lbl_EMSCageType.Text = ""; rb_EMSCageType_2.Checked = true; break;
                }

                switch (DevSt->LampType)
                {
                    case 0: lbl_LampType.Text = rb_LampType_0.Text; rb_LampType_0.Checked = true; break;
                    case 1: lbl_LampType.Text = rb_LampType_1.Text; rb_LampType_1.Checked = true; break;
                    default: lbl_LampType.Text = ""; rb_LampType_0.Checked = true; break;
                }

                switch (DevSt->LiftType)
                {
                    case 0: lbl_EMSLiftType.Text = rb_EMSLiftType_0.Text; rb_EMSLiftType_0.Checked = true; break;
                    case 1: lbl_EMSLiftType.Text = rb_EMSLiftType_1.Text; rb_EMSLiftType_1.Checked = true; break;
                    default: lbl_EMSLiftType.Text = ""; rb_EMSLiftType_1.Checked = true; break;
                }


                if ((DevSt->DSPInstallInfoRec.Install_Type < 1) || 
                    (DevSt->DSPInstallInfoRec.Install_Type > 2) || 
                    (DevSt->DSPInstallInfoRec.Install_Count < 1) || 
                    (DevSt->DSPInstallInfoRec.Install_Count > 2))
                {
                    lbl_DSP_Comm.Text = "";
                    rg_DSP_Comm_1.Checked = false;
                    rg_DSP_Comm_2.Checked = false;

                    lbl_DSP_Count.Text = cb_DSP_Count.Items[0].ToString();
                    cb_DSP_Count.SelectedIndex = 0;

                    lbl_DSP1_IP.Text = "";
                    ed_DSP1_IP.Text = "";
                    lbl_DSP2_IP.Text = "";
                    ed_DSP2_IP.Text = "";
                }
                else
                {
                    switch (DevSt->DSPInstallInfoRec.Install_Type)
                    {
                        case 1: lbl_DSP_Comm.Text = rg_DSP_Comm_1.Text; rg_DSP_Comm_1.Checked = true; break;
                        case 2: lbl_DSP_Comm.Text = rg_DSP_Comm_2.Text; rg_DSP_Comm_2.Checked = true; break;
                    }

                    switch (DevSt->DSPInstallInfoRec.Install_Count)
                    {
                        case 1: lbl_DSP_Count.Text = cb_DSP_Count.Items[1].ToString(); cb_DSP_Count.SelectedIndex = 1; break;
                        case 2: lbl_DSP_Count.Text = cb_DSP_Count.Items[2].ToString(); cb_DSP_Count.SelectedIndex = 2; break;
                    }

                    lbl_DSP1_IP.Text = string.Format("{0}.{1}.{2}.{3}",
                                                        DevSt->DSPInstallInfoRec.DSP1_IP[0],
                                                        DevSt->DSPInstallInfoRec.DSP1_IP[1],
                                                        DevSt->DSPInstallInfoRec.DSP1_IP[2],
                                                        DevSt->DSPInstallInfoRec.DSP1_IP[3]);
                    ed_DSP1_IP.Text = lbl_DSP1_IP.Text;

                    lbl_DSP2_IP.Text = string.Format("{0}.{1}.{2}.{3}",
                                                        DevSt->DSPInstallInfoRec.DSP2_IP[0],
                                                        DevSt->DSPInstallInfoRec.DSP2_IP[1],
                                                        DevSt->DSPInstallInfoRec.DSP2_IP[2],
                                                        DevSt->DSPInstallInfoRec.DSP2_IP[3]);
                    ed_DSP2_IP.Text = lbl_DSP2_IP.Text;
                }


                switch (DevSt->ModeSwitchUse)
                {
                    case 0: lbl_ModeSwitchUse.Text = "미사용"; rb_ModeSwitchUse_0.Checked = true; break;
                    case 1: lbl_ModeSwitchUse.Text = "사용"; rb_ModeSwitchUse_1.Checked = true; break;
                    default: lbl_ModeSwitchUse.Text = "미사용"; rb_ModeSwitchUse_0.Checked = true; break;
                }
            }
            
            btn_Set.Enabled = ((form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.DevMode & 0x08) != 0);
        }

        private unsafe void Do_Ctrl(bool IsFileSave)
        {
            IPAddress ReturnIP;

            ems_REC_DEVConfigCtrl.CtrlFlag[0] = 0x07;
            ems_REC_DEVConfigCtrl.CtrlFlag[1] = 0x00;
            ems_REC_DEVConfigCtrl.CtrlFlag[2] = 0x00;


            if (rb_EMSsType_2.Checked) ems_REC_DEVConfigCtrl.Data.EMSsType = 2;
            else ems_REC_DEVConfigCtrl.Data.EMSsType = 1;

            ems_REC_DEVConfigCtrl.Data.InvetorKind = 1;

            if (rb_MovePositionSensor_2.Checked) ems_REC_DEVConfigCtrl.Data.MovePositionSensor = 2;
            else ems_REC_DEVConfigCtrl.Data.MovePositionSensor = 1;

            if (rb_EMSCageType_2.Checked) ems_REC_DEVConfigCtrl.Data.CageType = 2;
            else ems_REC_DEVConfigCtrl.Data.CageType = 1;

            if (rb_LampType_1.Checked) ems_REC_DEVConfigCtrl.Data.LampType = 1;
            else ems_REC_DEVConfigCtrl.Data.LampType = 0;

            if (rb_EMSLiftType_1.Checked) ems_REC_DEVConfigCtrl.Data.LiftType = 1;
            else ems_REC_DEVConfigCtrl.Data.LiftType = 0;

            ems_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Type = 0;
            ems_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Count = 0;
            ems_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP1_IP[0] = 0x00;
            ems_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP1_IP[1] = 0x00;
            ems_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP1_IP[2] = 0x00;
            ems_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP1_IP[3] = 0x00;
            ems_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP2_IP[0] = 0x00;
            ems_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP2_IP[1] = 0x00;
            ems_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP2_IP[2] = 0x00;
            ems_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP2_IP[3] = 0x00;
            if (cb_DSP_Count.SelectedIndex > 0)
            {
                if (rg_DSP_Comm_1.Checked) ems_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Type = 1;
                else if (rg_DSP_Comm_2.Checked) ems_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Type = 2;
                else ems_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Type = 2;

                ems_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Count = (byte) cb_DSP_Count.SelectedIndex;

                fixed (VEXI_DEFS.TDSPInstallInfoRec* DSPCtrl = &ems_REC_DEVConfigCtrl.Data.DSPInstallInfoRec)
                {
                    if (Global_Class.UTIL_IsValid_IP(ed_DSP1_IP.Text, out ReturnIP))
                    {
                        Global_Class.UTIL_BytesToBytePtr(ReturnIP.GetAddressBytes(), (byte*)DSPCtrl->DSP1_IP);
                    }
                    if (Global_Class.UTIL_IsValid_IP(ed_DSP2_IP.Text, out ReturnIP))
                    {
                        Global_Class.UTIL_BytesToBytePtr(ReturnIP.GetAddressBytes(), (byte*)DSPCtrl->DSP2_IP);
                    }
                }
            }

            if (rb_ModeSwitchUse_1.Checked) ems_REC_DEVConfigCtrl.Data.ModeSwitchUse = 1;
            else ems_REC_DEVConfigCtrl.Data.ModeSwitchUse = 0;
            if (!IsFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_26, ems_REC_DEVConfigCtrl);
            }
        }

        private void btn_Set_Click(object sender, EventArgs e)
        {
            
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "설정값을 장치에 적용하시겠습니까?"))
            {
                Do_Ctrl(false);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_25, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_DevConfigReq)));
        }

        private void btn_LoadTotalFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.EMScfg|*.EMSCFG";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.EMS_ToTalFile.FileName = openFileDialog1.FileName;
                if (form_Main.EMS_ToTalFile.Read_MCU_CFG(ref ems_REC_DEVConfigCtrl))
                {
                    ems_REC_DEVConfigStatus = ems_REC_DEVConfigCtrl.Data;

                    Display_EMSConfig();
                }
                else
                {
                    MessageBox.Show("해당 파일안에 [" + this.Text + "] 데이터가 없습니다");
                }
            }
        }

        private void btn_SaveTotalFile_Click(object sender, EventArgs e)
        {
            Do_Ctrl(true);

            saveFileDialog1.Filter = "*.EMScfg|*.EMSCFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.EMS_ToTalFile.FileName = saveFileDialog1.FileName;
                form_Main.EMS_ToTalFile.Write_MCU_CFG(ems_REC_DEVConfigCtrl);
            }
        }

    }
}

using System;
using System.Net;
using System.Windows.Forms;
using System.Runtime.InteropServices;   //DllImport

namespace VEXI
{
    public partial class Form_RTVConfig : Form
    {

        public Form_Main form_Main;
        private VEXI_DEFS.TRTV_DevConfigRes rtv_REC_DEVConfigStatus;
        private static VEXI_DEFS.TRTV_DevConfigCtrl rtv_REC_DEVConfigCtrl;

        public Form_RTVConfig()
        {
            InitializeComponent();
        }

        private void Form_RTVConfig_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }
            Display_RTVConfig();

            btn_Set.Enabled = false;
        }

        public unsafe void Display_RTVConfig(byte[] Data)
        {
            rtv_REC_DEVConfigStatus = (VEXI_DEFS.TRTV_DevConfigRes)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TRTV_DevConfigRes));

            Display_RTVConfig();
        }

        private unsafe void Display_RTVConfig()
        { 
            fixed (VEXI_DEFS.TRTV_DevConfigRes* DevSt = &rtv_REC_DEVConfigStatus)
            {
                switch (DevSt->RTVsType)
                {
                    case 1: lbl_RTVsType.Text = rb_RTVsType_1.Text; rb_RTVsType_1.Checked = true; break;
                    case 2: lbl_RTVsType.Text = rb_RTVsType_2.Text; rb_RTVsType_2.Checked = true; break;
                    case 3: lbl_RTVsType.Text = rb_RTVsType_3.Text; rb_RTVsType_3.Checked = true; break;
                    default: lbl_RTVsType.Text = ""; rb_RTVsType_1.Checked = true; break;
                }

                switch (DevSt->InvetorKind)
                {
                    case 1: lbl_InvertorKind.Text = rb_InvertorKind_1.Text; rb_InvertorKind_1.Checked = true; break;
                    case 2: lbl_InvertorKind.Text = rb_InvertorKind_2.Text; rb_InvertorKind_2.Checked = true; break;
                    case 3: lbl_InvertorKind.Text = rb_InvertorKind_3.Text; rb_InvertorKind_3.Checked = true; break;
                    default: lbl_InvertorKind.Text = ""; rb_InvertorKind_1.Checked = true; break;
                }

                switch (DevSt->MovePositionSensor)
                {
                    case 1: lbl_MovePositionSensor.Text = rb_MovePositionSensor_1.Text; rb_MovePositionSensor_1.Checked = true; break;
                    case 2: lbl_MovePositionSensor.Text = rb_MovePositionSensor_2.Text; rb_MovePositionSensor_2.Checked = true; break;
                    case 3: lbl_MovePositionSensor.Text = rb_MovePositionSensor_3.Text; rb_MovePositionSensor_3.Checked = true; break;
                    default: lbl_MovePositionSensor.Text = ""; rb_MovePositionSensor_1.Checked = true; break;
                }

                UInt32 TmpValue = (UInt32)(DevSt->InvertorType.WheelCount * 100 + DevSt->InvertorType.FeedType * 10 + DevSt->InvertorType.InvertorCount);

                switch (TmpValue)
                {
                    case 111: lbl_InvertorType.Text = rb_InvertorType_111.Text; rb_InvertorType_111.Checked = true; break;
                    case 112: lbl_InvertorType.Text = rb_InvertorType_112.Text; rb_InvertorType_112.Checked = true; break;
                    case 113: lbl_InvertorType.Text = rb_InvertorType_113.Text; rb_InvertorType_113.Checked = true; break;
                    case 122: lbl_InvertorType.Text = rb_InvertorType_122.Text; rb_InvertorType_122.Checked = true; break;
                    case 123: lbl_InvertorType.Text = rb_InvertorType_123.Text; rb_InvertorType_123.Checked = true; break;
                    case 131: lbl_InvertorType.Text = rb_InvertorType_131.Text; rb_InvertorType_131.Checked = true; break;
                    case 132: lbl_InvertorType.Text = rb_InvertorType_132.Text; rb_InvertorType_132.Checked = true; break;
                    case 133: lbl_InvertorType.Text = rb_InvertorType_133.Text; rb_InvertorType_133.Checked = true; break;
                    case 212: lbl_InvertorType.Text = rb_InvertorType_212.Text; rb_InvertorType_212.Checked = true; break;
                    case 213: lbl_InvertorType.Text = rb_InvertorType_213.Text; rb_InvertorType_213.Checked = true; break;
                    case 222: lbl_InvertorType.Text = rb_InvertorType_222.Text; rb_InvertorType_222.Checked = true; break;
                    case 224: lbl_InvertorType.Text = rb_InvertorType_224.Text; rb_InvertorType_224.Checked = true; break;
                    case 232: lbl_InvertorType.Text = rb_InvertorType_232.Text; rb_InvertorType_232.Checked = true; break;
                    case 233: lbl_InvertorType.Text = rb_InvertorType_233.Text; rb_InvertorType_233.Checked = true; break;
                    case 234: lbl_InvertorType.Text = rb_InvertorType_234.Text; rb_InvertorType_234.Checked = true; break;
                    default: lbl_InvertorType.Text = ""; rb_InvertorType_112.Checked = true; break;
                }


                switch (DevSt->Front_LampType)
                {
                    case 0: lbl_FrontLampType.Text = "LED BAR"; rb_FrontLampType_0.Checked = true; break;
                    case 1: lbl_FrontLampType.Text = "Tower LAMP"; rb_FrontLampType_1.Checked = true; break;
                    case 2: lbl_FrontLampType.Text = "LED BAR + Tower Lamp"; rb_FrontLampType_2.Checked = true; break;
                    default: lbl_FrontLampType.Text = "LED BAR"; rb_FrontLampType_0.Checked = true; break;
                }

                switch (DevSt->Rear_LampType)
                {
                    case 0: lbl_RearLampType.Text = "LED BAR"; rb_RearLampType_0.Checked = true; break;
                    case 1: lbl_RearLampType.Text = "Tower LAMP"; rb_RearLampType_1.Checked = true; break;
                    case 2: lbl_RearLampType.Text = "LED BAR + Tower Lamp"; rb_RearLampType_2.Checked = true; break;
                    default: lbl_RearLampType.Text = "LED BAR"; rb_RearLampType_0.Checked = true; break;
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
            
            btn_Set.Enabled = ((form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.DevMode & 0x08) != 0);
        }

        private unsafe void Do_Ctrl(bool IsFileSave)
        {
            IPAddress ReturnIP;

            rtv_REC_DEVConfigCtrl.CtrlFlag[0] = 0x07;
            rtv_REC_DEVConfigCtrl.CtrlFlag[1] = 0x00;
            rtv_REC_DEVConfigCtrl.CtrlFlag[2] = 0x00;


            if (rb_RTVsType_2.Checked) rtv_REC_DEVConfigCtrl.Data.RTVsType = 2;
            else if (rb_RTVsType_3.Checked) rtv_REC_DEVConfigCtrl.Data.RTVsType = 3;
            else rtv_REC_DEVConfigCtrl.Data.RTVsType = 1;

            if (rb_InvertorKind_2.Checked) rtv_REC_DEVConfigCtrl.Data.InvetorKind = 2;
            else if (rb_InvertorKind_3.Checked) rtv_REC_DEVConfigCtrl.Data.InvetorKind = 3;
            else rtv_REC_DEVConfigCtrl.Data.InvetorKind = 1;

            
            if (rb_MovePositionSensor_3.Checked) rtv_REC_DEVConfigCtrl.Data.MovePositionSensor = 3;
            else if (rb_MovePositionSensor_2.Checked) rtv_REC_DEVConfigCtrl.Data.MovePositionSensor = 2;
            else rtv_REC_DEVConfigCtrl.Data.MovePositionSensor = 1;



            if (rb_InvertorType_111.Checked)
            {
                rtv_REC_DEVConfigCtrl.Data.InvertorType.WheelCount = 1;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.FeedType = 1;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.InvertorCount = 1;
            }
            else if (rb_InvertorType_112.Checked)
            {
                rtv_REC_DEVConfigCtrl.Data.InvertorType.WheelCount = 1;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.FeedType = 1;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.InvertorCount = 2;
            }
            else if (rb_InvertorType_113.Checked)
            {
                rtv_REC_DEVConfigCtrl.Data.InvertorType.WheelCount = 1;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.FeedType = 1;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.InvertorCount = 3;
            }
            else if (rb_InvertorType_122.Checked)
            {
                rtv_REC_DEVConfigCtrl.Data.InvertorType.WheelCount = 1;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.FeedType = 2;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.InvertorCount = 2;
            }
            else if (rb_InvertorType_123.Checked)
            {
                rtv_REC_DEVConfigCtrl.Data.InvertorType.WheelCount = 1;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.FeedType = 2;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.InvertorCount = 3;
            }
            else if (rb_InvertorType_131.Checked)
            {
                rtv_REC_DEVConfigCtrl.Data.InvertorType.WheelCount = 1;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.FeedType = 3;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.InvertorCount = 1;
            }
            else if (rb_InvertorType_132.Checked)
            {
                rtv_REC_DEVConfigCtrl.Data.InvertorType.WheelCount = 1;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.FeedType = 3;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.InvertorCount = 2;
            }
            else if (rb_InvertorType_133.Checked)
            {
                rtv_REC_DEVConfigCtrl.Data.InvertorType.WheelCount = 1;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.FeedType = 3;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.InvertorCount = 3;
            }
            else if (rb_InvertorType_212.Checked)
            {
                rtv_REC_DEVConfigCtrl.Data.InvertorType.WheelCount = 2;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.FeedType = 1;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.InvertorCount = 2;
            }
            else if (rb_InvertorType_213.Checked)
            {
                rtv_REC_DEVConfigCtrl.Data.InvertorType.WheelCount = 2;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.FeedType = 1;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.InvertorCount = 3;
            }
            else if (rb_InvertorType_222.Checked)
            {
                rtv_REC_DEVConfigCtrl.Data.InvertorType.WheelCount = 2;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.FeedType = 2;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.InvertorCount = 2;
            }
            else if (rb_InvertorType_224.Checked)
            {
                rtv_REC_DEVConfigCtrl.Data.InvertorType.WheelCount = 2;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.FeedType = 2;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.InvertorCount = 4;
            }
            else if (rb_InvertorType_232.Checked)
            {
                rtv_REC_DEVConfigCtrl.Data.InvertorType.WheelCount = 2;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.FeedType = 3;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.InvertorCount = 2;
            }
            else if (rb_InvertorType_233.Checked)
            {
                rtv_REC_DEVConfigCtrl.Data.InvertorType.WheelCount = 2;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.FeedType = 3;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.InvertorCount = 3;
            }
            else if (rb_InvertorType_234.Checked)
            {
                rtv_REC_DEVConfigCtrl.Data.InvertorType.WheelCount = 2;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.FeedType = 3;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.InvertorCount = 4;
            }
            else
            {
                rtv_REC_DEVConfigCtrl.Data.InvertorType.WheelCount = 1;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.FeedType = 1;
                rtv_REC_DEVConfigCtrl.Data.InvertorType.InvertorCount = 2;
            }

            if (rb_FrontLampType_1.Checked) rtv_REC_DEVConfigCtrl.Data.Front_LampType = 1;
            else if (rb_FrontLampType_2.Checked) rtv_REC_DEVConfigCtrl.Data.Front_LampType = 2;
            else rtv_REC_DEVConfigCtrl.Data.Front_LampType = 0;

            if (rb_RearLampType_1.Checked) rtv_REC_DEVConfigCtrl.Data.Rear_LampType = 1;
            else if (rb_RearLampType_2.Checked) rtv_REC_DEVConfigCtrl.Data.Rear_LampType = 2;
            else rtv_REC_DEVConfigCtrl.Data.Rear_LampType = 0;

            rtv_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Type = 0;
            rtv_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Count = 0;
            rtv_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP1_IP[0] = 0x00;
            rtv_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP1_IP[1] = 0x00;
            rtv_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP1_IP[2] = 0x00;
            rtv_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP1_IP[3] = 0x00;
            rtv_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP2_IP[0] = 0x00;
            rtv_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP2_IP[1] = 0x00;
            rtv_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP2_IP[2] = 0x00;
            rtv_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP2_IP[3] = 0x00;
            if (cb_DSP_Count.SelectedIndex > 0)
            {
                if (rg_DSP_Comm_1.Checked) rtv_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Type = 1;
                else if (rg_DSP_Comm_2.Checked) rtv_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Type = 2;
                else rtv_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Type = 2;

                rtv_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Count = (byte) cb_DSP_Count.SelectedIndex;

                fixed (VEXI_DEFS.TDSPInstallInfoRec* DSPCtrl = &rtv_REC_DEVConfigCtrl.Data.DSPInstallInfoRec)
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
            if (rb_ModeSwitchUse_1.Checked) rtv_REC_DEVConfigCtrl.Data.ModeSwitchUse = 1;
            else rtv_REC_DEVConfigCtrl.Data.ModeSwitchUse = 0;

            if (!IsFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_26, rtv_REC_DEVConfigCtrl);
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
            openFileDialog1.Filter = "*.RTVcfg|*.RTVCFG";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.RTV_ToTalFile.FileName = openFileDialog1.FileName;
                if (form_Main.RTV_ToTalFile.Read_MCU_CFG(ref rtv_REC_DEVConfigCtrl))
                {
                    rtv_REC_DEVConfigStatus = rtv_REC_DEVConfigCtrl.Data;

                    Display_RTVConfig();
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

            saveFileDialog1.Filter = "*.RTVcfg|*.RTVCFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.RTV_ToTalFile.FileName = saveFileDialog1.FileName;
                form_Main.RTV_ToTalFile.Write_MCU_CFG(rtv_REC_DEVConfigCtrl);
            }
        }

    }
}

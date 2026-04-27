using System;
using System.Net;
using System.Windows.Forms;
using System.Runtime.InteropServices;   //DllImport

namespace VEXI
{
    public partial class Form_SRMConfig : Form
    {

        public Form_Main form_Main;
        private VEXI_DEFS.TSRM_DevConfigRes srm_REC_DEVConfigStatus;
        private static VEXI_DEFS.TSRM_DevConfigCtrl srm_REC_DEVConfigCtrl;

        public Form_SRMConfig()
        {
            InitializeComponent();
        }

        #region 컴포넌트 이벤트
        private void Form_SRMConfig_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }
            Display_SRMConfig();

            btn_Set.Enabled = false;
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

        //통합파일에서 불러오는 것으로 변경
        //개별파일에서 불러오는 소스는 남겨놓음
        private void btn_FileLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.SRM_cfg|*.SRM_CFG";
            //openFileDialog1.Filter = "*.ini|*.INI";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //나중에 추가된 항목은 빠졌을 수 있다. 
                //이 루틴을 쓰려면 프로토콜 확인해서 빠지거나 추가된 부분 수정
                srm_REC_DEVConfigStatus.ForkCount = (byte)IniControl.ReadInteger(openFileDialog1.FileName, "SRM CONFIG", "ForkCount", 0);
                srm_REC_DEVConfigStatus.MoveDriveCount = (byte)IniControl.ReadInteger(openFileDialog1.FileName, "SRM CONFIG", "MoveDriveCount", 0);
                srm_REC_DEVConfigStatus.MoveDriveType = (byte)IniControl.ReadInteger(openFileDialog1.FileName, "SRM CONFIG", "MoveDriveType", 0);
                srm_REC_DEVConfigStatus.ForkDriveType = (byte)IniControl.ReadInteger(openFileDialog1.FileName, "SRM CONFIG", "ForkDriveType", 0);
                srm_REC_DEVConfigStatus.InvertorType = (byte)IniControl.ReadInteger(openFileDialog1.FileName, "SRM CONFIG", "InvertorType", 0);
                srm_REC_DEVConfigStatus.ForkInvertor = (byte)IniControl.ReadInteger(openFileDialog1.FileName, "SRM CONFIG", "ForkInvertor", 0);
                srm_REC_DEVConfigStatus.ForkSensor = (byte)IniControl.ReadInteger(openFileDialog1.FileName, "SRM CONFIG", "ForkSensor", 0);
                srm_REC_DEVConfigStatus.MovePositionSensor = (byte)IniControl.ReadInteger(openFileDialog1.FileName, "SRM CONFIG", "MovePositionSensor", 0);
                srm_REC_DEVConfigStatus.UpdownPositionSensor = (byte)IniControl.ReadInteger(openFileDialog1.FileName, "SRM CONFIG", "UpdownPositionSensor", 0);

                Display_SRMConfig();
            }

        }
        private void btn_LoadTotalFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.SRMcfg|*.SRMCFG";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
                if (form_Main.SRM_ToTalFile.Read_MCU_CFG(ref srm_REC_DEVConfigCtrl))
                {
                    srm_REC_DEVConfigStatus = srm_REC_DEVConfigCtrl.Data;

                    Display_SRMConfig();
                }
                else
                {
                    MessageBox.Show("해당 파일안에 [" + this.Text + "] 데이터가 없습니다");
                }
            }
        }
        //통합파일에 저장하는 것으로 변경
        //개별파일에 저장하는 소스는 남겨놓음
        private void btn_Tofile_Click(object sender, EventArgs e)
        {
            Do_Ctrl(true);

            saveFileDialog1.Filter = "*.SRM_cfg|*.SRM_CFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                //나중에 추가된 항목은 빠졌을 수 있다. 
                //이 루틴을 쓰려면 프로토콜 확인해서 빠지거나 추가된 부분 수정
                IniControl.WriteIni(saveFileDialog1.FileName, "SRM CONFIG", "ForkCount", srm_REC_DEVConfigCtrl.Data.ForkCount);
                IniControl.WriteIni(saveFileDialog1.FileName, "SRM CONFIG", "MoveDriveCount", srm_REC_DEVConfigCtrl.Data.MoveDriveCount);
                IniControl.WriteIni(saveFileDialog1.FileName, "SRM CONFIG", "MoveDriveType", srm_REC_DEVConfigCtrl.Data.MoveDriveType);
                IniControl.WriteIni(saveFileDialog1.FileName, "SRM CONFIG", "ForkDriveType", srm_REC_DEVConfigCtrl.Data.ForkDriveType);
                IniControl.WriteIni(saveFileDialog1.FileName, "SRM CONFIG", "InvertorType", srm_REC_DEVConfigCtrl.Data.InvertorType);
                IniControl.WriteIni(saveFileDialog1.FileName, "SRM CONFIG", "ForkInvertor", srm_REC_DEVConfigCtrl.Data.ForkInvertor);
                IniControl.WriteIni(saveFileDialog1.FileName, "SRM CONFIG", "ForkSensor", srm_REC_DEVConfigCtrl.Data.ForkSensor);
                IniControl.WriteIni(saveFileDialog1.FileName, "SRM CONFIG", "MovePositionSensor", srm_REC_DEVConfigCtrl.Data.MovePositionSensor);
                IniControl.WriteIni(saveFileDialog1.FileName, "SRM CONFIG", "UpdownPositionSensor", srm_REC_DEVConfigCtrl.Data.UpdownPositionSensor);
            }
        }
        private void btn_SaveTotalFile_Click(object sender, EventArgs e)
        {
            Do_Ctrl(true);

            saveFileDialog1.Filter = "*.SRMcfg|*.SRMCFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
                form_Main.SRM_ToTalFile.Write_MCU_CFG(srm_REC_DEVConfigCtrl);
            }
        }

        #endregion

        #region 기능함수
        public unsafe void Display_SRMConfig(byte[] Data)
        {
            srm_REC_DEVConfigStatus = (VEXI_DEFS.TSRM_DevConfigRes)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TSRM_DevConfigRes));

            Display_SRMConfig();
        }

        private unsafe void Display_SRMConfig()
        {
            fixed (VEXI_DEFS.TSRM_DevConfigRes* DevSt = &srm_REC_DEVConfigStatus)
            {
                switch (DevSt->ForkCount)
                {
                    case 1: lbl_ForkCount.Text = "싱글"; rb_ForkCount_1.Checked = true; break;
                    case 2: lbl_ForkCount.Text = "트윈"; rb_ForkCount_2.Checked = true; break;
                    default: lbl_ForkCount.Text = ""; rb_ForkCount_1.Checked = true; break;
                }

                switch (DevSt->MoveDriveCount)
                {
                    case 1: lbl_MoveDriveCount.Text = "싱글"; rb_MoveDriveCount_1.Checked = true; break;
                    case 2: lbl_MoveDriveCount.Text = "트윈"; rb_MoveDriveCount_2.Checked = true; break;
                    default: lbl_MoveDriveCount.Text = ""; rb_MoveDriveCount_1.Checked = true; break;
                }

                switch (DevSt->MoveDriveType)
                {
                    case 1: lbl_MoveDriveType.Text = "휠구동"; rb_MoveDriveType_1.Checked = true; break;
                    case 2: lbl_MoveDriveType.Text = "타이밍벨트"; rb_MoveDriveType_2.Checked = true; break;
                    default: lbl_MoveDriveType.Text = ""; rb_MoveDriveType_1.Checked = true; break;
                }

                switch (DevSt->ForkDriveType)
                {
                    case 1: lbl_ForkDriveType.Text = "싱글딥"; rb_ForkDriveType_1.Checked = true; break;
                    case 2: lbl_ForkDriveType.Text = "더블딥 2POS"; rb_ForkDriveType_2.Checked = true; break;
                    case 3: lbl_ForkDriveType.Text = "더블딥 3POS"; rb_ForkDriveType_3.Checked = true; break;
                    case 4: lbl_ForkDriveType.Text = "더블딥 2POS 베리언트"; rb_ForkDriveType_4.Checked = true; break;
                    case 5: lbl_ForkDriveType.Text = "더블딥 3POS 베리언트"; rb_ForkDriveType_5.Checked = true; break;
                    default: lbl_ForkDriveType.Text = ""; rb_ForkDriveType_1.Checked = true; break;
                }

                switch (DevSt->InvertorType)
                {
                    case 1: lbl_InvertorType.Text = "SEW"; rb_InvertorType_1.Checked = true; break;
                    case 2: lbl_InvertorType.Text = "SIEMENS"; rb_InvertorType_2.Checked = true; break;
                    case 3: lbl_InvertorType.Text = "SEW Ver2"; rb_InvertorType_3.Checked = true; break;
                    case 4: lbl_InvertorType.Text = "SEW Ver2 (Anti-Sway)"; rb_InvertorType_4.Checked = true; break;
                    case 5: lbl_InvertorType.Text = "SEW Ver2 (SEW Ver2(Sync-Travel)"; rb_InvertorType_5.Checked = true; break;
                    default: lbl_InvertorType.Text = ""; rb_InvertorType_1.Checked = true; break;
                }

                switch (DevSt->ForkInvertor)
                {
                    case 1: lbl_ForkInvertor.Text = "주행/승강 인버터 사용"; rb_ForkInvertor_1.Checked = true; break;
                    case 2: lbl_ForkInvertor.Text = "포크 인버터 사용"; rb_ForkInvertor_2.Checked = true; break;
                    default: lbl_ForkInvertor.Text = ""; rb_ForkInvertor_1.Checked = true; break;
                }

                if ((DevSt->ForkSensor & 0x01) != 0) lbl_ForkSensor_0.Text = "적용"; else lbl_ForkSensor_0.Text = "미적용";
                if ((DevSt->ForkSensor & 0x02) != 0) lbl_ForkSensor_1.Text = "적용"; else lbl_ForkSensor_1.Text = "미적용";
                if ((DevSt->ForkSensor & 0x04) != 0) lbl_ForkSensor_2.Text = "적용"; else lbl_ForkSensor_2.Text = "미적용";
                cb_ForkSensor_0.Checked = ((DevSt->ForkSensor & 0x01) != 0);
                cb_ForkSensor_1.Checked = ((DevSt->ForkSensor & 0x02) != 0);
                cb_ForkSensor_2.Checked = ((DevSt->ForkSensor & 0x04) != 0);

                switch (DevSt->MovePositionSensor)
                {
                    case 1: lbl_MovePositionSensor.Text = "레이저"; rb_MovePositionSensor_1.Checked = true; break;
                    case 2: lbl_MovePositionSensor.Text = "코드레일"; rb_MovePositionSensor_2.Checked = true; break;
                    case 3: lbl_MovePositionSensor.Text = "바코드"; rb_MovePositionSensor_3.Checked = true; break;
                    default: lbl_MovePositionSensor.Text = ""; rb_MovePositionSensor_1.Checked = true; break;
                }

                switch (DevSt->UpdownPositionSensor)
                {
                    case 1: lbl_UpdownPositionSensor.Text = "레이저"; rb_UpdownPositionSensor_1.Checked = true; break;
                    case 2: lbl_UpdownPositionSensor.Text = "코드레일"; rb_UpdownPositionSensor_2.Checked = true; break;
                    case 3: lbl_UpdownPositionSensor.Text = "바코드"; rb_UpdownPositionSensor_3.Checked = true; break;
                    default: lbl_UpdownPositionSensor.Text = ""; rb_UpdownPositionSensor_1.Checked = true; break;
                }

                switch (DevSt->LampType)
                {
                    case 0: lbl_LampType.Text = "LED BAR"; rb_LampType_0.Checked = true; break;
                    case 1: lbl_LampType.Text = "Tower LAMP"; rb_LampType_1.Checked = true; break;
                    default: lbl_LampType.Text = "LED BAR"; rb_LampType_0.Checked = true; break;
                }

                switch (DevSt->ForkEncoderType)
                {
                    case 0: lbl_ForkEncoderType.Text = "Absolute"; rb_ForkEncoderType_0.Checked = true; break;
                    case 1: lbl_ForkEncoderType.Text = "Incremental"; rb_ForkEncoderType_1.Checked = true; break;
                    default: lbl_ForkEncoderType.Text = "Absolute"; rb_ForkEncoderType_0.Checked = true; break;
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
            btn_Set.Enabled = ((form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode & 0x08) != 0);
        }

        private unsafe void Do_Ctrl(bool IsFileSave)
        {
            IPAddress ReturnIP;

            srm_REC_DEVConfigCtrl.CtrlFlag[0] = 0x07;
            srm_REC_DEVConfigCtrl.CtrlFlag[1] = 0x00;
            srm_REC_DEVConfigCtrl.CtrlFlag[2] = 0x00;


            if (rb_ForkCount_2.Checked) srm_REC_DEVConfigCtrl.Data.ForkCount = 2;
            else srm_REC_DEVConfigCtrl.Data.ForkCount = 1;
            if (rb_MoveDriveCount_2.Checked) srm_REC_DEVConfigCtrl.Data.MoveDriveCount = 2;
            else srm_REC_DEVConfigCtrl.Data.MoveDriveCount = 1;
            if (rb_MoveDriveType_2.Checked) srm_REC_DEVConfigCtrl.Data.MoveDriveType = 2;
            else srm_REC_DEVConfigCtrl.Data.MoveDriveType = 1;
            if (rb_ForkDriveType_5.Checked) srm_REC_DEVConfigCtrl.Data.ForkDriveType = 5;
            else if (rb_ForkDriveType_4.Checked) srm_REC_DEVConfigCtrl.Data.ForkDriveType = 4;
            else if (rb_ForkDriveType_3.Checked) srm_REC_DEVConfigCtrl.Data.ForkDriveType = 3;
            else if (rb_ForkDriveType_2.Checked) srm_REC_DEVConfigCtrl.Data.ForkDriveType = 2;
            else srm_REC_DEVConfigCtrl.Data.ForkDriveType = 1;
            if (rb_InvertorType_5.Checked) srm_REC_DEVConfigCtrl.Data.InvertorType = 5;
            else if (rb_InvertorType_4.Checked) srm_REC_DEVConfigCtrl.Data.InvertorType = 4;
            else if (rb_InvertorType_3.Checked) srm_REC_DEVConfigCtrl.Data.InvertorType = 3;
            else if (rb_InvertorType_2.Checked) srm_REC_DEVConfigCtrl.Data.InvertorType = 2;
            else srm_REC_DEVConfigCtrl.Data.InvertorType = 1;
            if (rb_ForkInvertor_2.Checked) srm_REC_DEVConfigCtrl.Data.ForkInvertor = 2;
            else srm_REC_DEVConfigCtrl.Data.ForkInvertor = 1;
            srm_REC_DEVConfigCtrl.Data.ForkSensor = 0x00;
            if (cb_ForkSensor_0.Checked) srm_REC_DEVConfigCtrl.Data.ForkSensor |= 0x01;
            if (cb_ForkSensor_1.Checked) srm_REC_DEVConfigCtrl.Data.ForkSensor |= 0x02;
            if (cb_ForkSensor_2.Checked) srm_REC_DEVConfigCtrl.Data.ForkSensor |= 0x04;
            if (rb_MovePositionSensor_3.Checked) srm_REC_DEVConfigCtrl.Data.MovePositionSensor = 3;
            else if (rb_MovePositionSensor_2.Checked) srm_REC_DEVConfigCtrl.Data.MovePositionSensor = 2;
            else srm_REC_DEVConfigCtrl.Data.MovePositionSensor = 1;
            if (rb_UpdownPositionSensor_3.Checked) srm_REC_DEVConfigCtrl.Data.UpdownPositionSensor = 3;
            else if (rb_UpdownPositionSensor_2.Checked) srm_REC_DEVConfigCtrl.Data.UpdownPositionSensor = 2;
            else srm_REC_DEVConfigCtrl.Data.UpdownPositionSensor = 1;


            if (rb_LampType_1.Checked) srm_REC_DEVConfigCtrl.Data.LampType = 1;
            else srm_REC_DEVConfigCtrl.Data.LampType = 0;

            if (rb_ForkEncoderType_1.Checked) srm_REC_DEVConfigCtrl.Data.ForkEncoderType = 1;
            else srm_REC_DEVConfigCtrl.Data.ForkEncoderType = 0;

            srm_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Type = 0;
            srm_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Count = 0;
            srm_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP1_IP[0] = 0x00;
            srm_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP1_IP[1] = 0x00;
            srm_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP1_IP[2] = 0x00;
            srm_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP1_IP[3] = 0x00;
            srm_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP2_IP[0] = 0x00;
            srm_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP2_IP[1] = 0x00;
            srm_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP2_IP[2] = 0x00;
            srm_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.DSP2_IP[3] = 0x00;
            if (cb_DSP_Count.SelectedIndex > 0)
            {
                if (rg_DSP_Comm_1.Checked) srm_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Type = 1;
                else if (rg_DSP_Comm_2.Checked) srm_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Type = 2;
                else srm_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Type = 2;

                srm_REC_DEVConfigCtrl.Data.DSPInstallInfoRec.Install_Count = (byte)cb_DSP_Count.SelectedIndex;

                fixed (VEXI_DEFS.TDSPInstallInfoRec* DSPCtrl = &srm_REC_DEVConfigCtrl.Data.DSPInstallInfoRec)
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

            if (rb_ModeSwitchUse_1.Checked) srm_REC_DEVConfigCtrl.Data.ModeSwitchUse = 1;
            else srm_REC_DEVConfigCtrl.Data.ModeSwitchUse = 0;

            if (!IsFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_26, srm_REC_DEVConfigCtrl);
            }
        }

        #endregion

    }
}

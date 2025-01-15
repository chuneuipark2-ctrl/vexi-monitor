using System;
using System.IO;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_SRMForkParam_Speed : Form 
    {
        
        public Form_Main form_Main;
        public static VEXI_DEFS.TSRM_ForkParamRes srm_ForkParam_RES;
        public static VEXI_DEFS.TSRM_ForkParamCTRL srm_ForkParam_CTRL;
        

        public Form_SRMForkParam_Speed()
        {
            InitializeComponent();
        }

        #region 컴포넌트 이벤트
        private void Form_Param_Speed_Load(object sender, EventArgs e)
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

            btn_Param_Set.Enabled = false;

            toolTip1.SetToolTip(ed_Fork_ManualOp_TokeAlarm, "설정 범위 : 0.0 ~ 200.0%");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_1, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_2, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_3, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_4, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_5, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_6, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_7, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_8, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_9, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_10, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_11, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_12, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_13, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_14, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_15, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_16, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_17, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_18, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_19, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_20, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_21, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_22, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_23, "설정값이 0 일 경우, 사용안함");
            toolTip1.SetToolTip(ed_Fork_AutoForkDecel_pos_24, "설정값이 0 일 경우, 사용안함");
        }

        private unsafe void btn_Drive_OpSpeed_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "운영속도로 초기화 하시겠습니까? "))
            {
                srm_ForkParam_CTRL.CtrlFlag[0] = 0x02;
                srm_ForkParam_CTRL.CtrlFlag[1] = 0x00;

                srm_ForkParam_CTRL.CtrlFlag[2] = 0x00;
                srm_ForkParam_CTRL.CtrlFlag[3] = 0x00;

                srm_ForkParam_CTRL.CtrlFlag[4] = 0x00;

                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A8, srm_ForkParam_CTRL);

            }
        }

        private unsafe void btn_Fork_TestSpeed_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "시운전속도로 초기화 하시겠습니까? "))
            {
                srm_ForkParam_CTRL.CtrlFlag[0] = 0x01;
                srm_ForkParam_CTRL.CtrlFlag[1] = 0x00;

                srm_ForkParam_CTRL.CtrlFlag[2] = 0x00;
                srm_ForkParam_CTRL.CtrlFlag[3] = 0x00;

                srm_ForkParam_CTRL.CtrlFlag[4] = 0x00;

                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A8, srm_ForkParam_CTRL);

            }

        }
        private void ed_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox ed = sender as TextBox;

            if (ed.Tag.ToString() == "10") // 마이너스 ON, 소수점 OFF
            {
                if (
                (char.IsControl(e.KeyChar)) ||
                (char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == '-') ||
                (e.KeyChar == Convert.ToChar(Keys.Enter)) ||
                (e.KeyChar == Convert.ToChar(Keys.Back)) ||
                (e.KeyChar == Convert.ToChar(Keys.Tab)))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if (ed.Tag.ToString() == "11") // 마이너스 ON, 소수점 ON
            {
                if (
                (char.IsControl(e.KeyChar)) ||
                (char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == '-') ||
                (e.KeyChar == '.') ||
                (e.KeyChar == Convert.ToChar(Keys.Enter)) ||
                (e.KeyChar == Convert.ToChar(Keys.Back)) ||
                (e.KeyChar == Convert.ToChar(Keys.Tab)))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if (ed.Tag.ToString() == "01") // 마이너스 OFF, 소수점 ON
            {
                if (
                (char.IsControl(e.KeyChar)) ||
                (char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == '.') ||
                (e.KeyChar == Convert.ToChar(Keys.Enter)) ||
                (e.KeyChar == Convert.ToChar(Keys.Back)) ||
                (e.KeyChar == Convert.ToChar(Keys.Tab)))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }

            }
            else if (ed.Tag.ToString() == "00") // 마이너스 OFF, 소수점 OFF
            {
                if (
                (char.IsControl(e.KeyChar)) ||
                (char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == Convert.ToChar(Keys.Enter)) ||
                (e.KeyChar == Convert.ToChar(Keys.Back)) ||
                (e.KeyChar == Convert.ToChar(Keys.Tab)))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = false;
            }

        }

        private void btn_Param_Load_Click(object sender, EventArgs e)
        {
            Display_Init();
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A7, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_PARAMReq)));
        }

        private void btn_Param_Set_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "포크 설정을 장치에 다운로드 하시겠습니까 ? "))
            {
                Do_Ctrl(false);
            }
        }

        //통합파일로 저장하는 것으로 변경
        //개별파일로 저장하는 소스는 남겨놓음
        private void btn_Param_FileWrite_Click(object sender, EventArgs e)
        {
            Do_Ctrl(true);

            saveFileDialog1.Filter = "*.Fork_Param|*.FORK_PARAM";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (BinaryWriter br = new BinaryWriter(File.Open(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write)))
                {
                    try
                    {
                        br.Seek(0, SeekOrigin.Begin);
                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_ForkParamCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(srm_ForkParam_CTRL, Savebytes);
                        br.Write(Savebytes);
                    }
                    finally
                    {
                        br.Close();
                    }

                }
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
                form_Main.SRM_ToTalFile.Write_SRM_ForkParamCTRL(srm_ForkParam_CTRL);
            }
        }

        //통합파일에서 읽어오는 것으로 변경
        //개별파일에서 읽어오는 소스는 남겨놓음
        private void btn_Param_FileRead_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.Fork_Param|*.FORK_PARAM";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] Savebytes;

                using (BinaryReader br = new BinaryReader(File.Open(openFileDialog1.FileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        ushort Len = (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_ForkParamCTRL));
                        if (br.BaseStream.Length == Len)
                        {
                            Savebytes = br.ReadBytes(Len);
                            srm_ForkParam_CTRL = (VEXI_DEFS.TSRM_ForkParamCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_ForkParamCTRL));
                            srm_ForkParam_RES = (VEXI_DEFS.TSRM_ForkParamRes)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_ForkParamRes), 35, Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_ForkParamRes)));

                            Display_SRM_Param();

                        }
                        else
                        {
                            MessageBox.Show("현재 프로토콜과 맞지 않는 파일입니다");
                        }
                    }
                    finally
                    {
                        br.Close();
                    }
                }
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
                if (form_Main.SRM_ToTalFile.Read_SRM_ForkParamCTRL(ref srm_ForkParam_CTRL))
                {
                    srm_ForkParam_RES = srm_ForkParam_CTRL.ParamItemsRec;

                    Display_SRM_Param();
                }
                else
                {
                    MessageBox.Show("해당 파일안에 [" + this.Text + "] 데이터가 없습니다");
                }
            }
        }

        #endregion

        #region 기능함수
        private unsafe void Do_Ctrl(bool isFileSave)
        {
            fixed (VEXI_DEFS.TSRM_ForkParamCTRL* TmpPtr = &srm_ForkParam_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)TmpPtr, Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_ForkParamCTRL)));
            }

            srm_ForkParam_CTRL.CtrlFlag[0] = 0x00;
            srm_ForkParam_CTRL.CtrlFlag[1] = 0xFF;
            srm_ForkParam_CTRL.CtrlFlag[2] = 0x07;
            srm_ForkParam_CTRL.CtrlFlag[3] = 0xCF;
            srm_ForkParam_CTRL.CtrlFlag[4] = 0x07;

            srm_ForkParam_CTRL.ParamItemsRec.Speed_Auto_High.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoHighSpeed_Speed.Text, 10);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Auto_High.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoHighSpeed_Accel.Text, 1);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Auto_High.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoHighSpeed_Decel.Text, 1);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Auto_High.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoHighSpeed_Jerk.Text, 1);

            srm_ForkParam_CTRL.ParamItemsRec.Speed_Auto_Middle.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoMiddleSpeed_Speed.Text, 10);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Auto_Middle.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoMiddleSpeed_Accel.Text, 1);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Auto_Middle.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoMiddleSpeed_Decel.Text, 1);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Auto_Middle.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoMiddleSpeed_jerk.Text, 1);

            srm_ForkParam_CTRL.ParamItemsRec.Speed_Auto_Low.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoLowSpeed_Speed.Text, 10);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Auto_Low.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoLowSpeed_Accel.Text, 1);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Auto_Low.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoLowSpeed_Decel.Text, 1);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Auto_Low.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoLowSpeed_jerk.Text, 1);

            srm_ForkParam_CTRL.ParamItemsRec.Speed_Manual_Middle.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_ManualMiddleSpeed_Speed.Text, 10);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Manual_Middle.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_ManualMiddleSpeed_Accel.Text, 1);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Manual_Middle.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_ManualMiddleSpeed_Decel.Text, 1);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Manual_Middle.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_ManualMiddleSpeed_Jerk.Text, 1);

            srm_ForkParam_CTRL.ParamItemsRec.Speed_Manual_Low.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_ManualLowSpeed_Speed.Text, 10);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Manual_Low.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_ManualLowSpeed_Accel.Text, 1);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Manual_Low.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_ManualLowSpeed_Decel.Text, 1);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Manual_Low.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_ManualLowSpeed_Jerk.Text, 1);

            srm_ForkParam_CTRL.ParamItemsRec.Speed_Force.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_ForceMode_Speed.Text, 10);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Force.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_ForceMode_Accel.Text, 1);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Force.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_ForceMode_Decel.Text, 1);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Force.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_ForceMode_Jerk.Text, 1);

            srm_ForkParam_CTRL.ParamItemsRec.Speed_Creep.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_Creep_Speed.Text, 10);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Creep.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_Creep_Decel.Text, 1);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Creep.jerk  = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_Creep_Jerk.Text, 1);

            srm_ForkParam_CTRL.ParamItemsRec.Speed_RefSet.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_RefMode_Speed.Text, 10);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_RefSet.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_RefMode_Accel.Text, 1);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_RefSet.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_RefMode_Decel.Text, 1);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_RefSet.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_RefMode_Jerk.Text, 1);

            srm_ForkParam_CTRL.ParamItemsRec.Speed_Emergency.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_Emergency_Decel.Text, 1);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Emergency.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_Emergency_Jerk.Text, 1);

            srm_ForkParam_CTRL.ParamItemsRec.Speed_Auto_Decel1.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoDecel1_Speed.Text, 10);
            srm_ForkParam_CTRL.ParamItemsRec.Speed_Auto_Decel2.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoDecel2_Speed.Text, 10);


            srm_ForkParam_CTRL.ParamItemsRec.CurrentPos_Offset = (byte)Global_Class.UTIL_StrToIntDef(ed_Fork_CurrentPos_Offset.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.CurrentPos_histerisis = (byte)Global_Class.UTIL_StrToIntDef(ed_Fork_CurrentPos_histerisis.Text, 0);

            srm_ForkParam_CTRL.ParamItemsRec.ManualOp_TokeAlarm = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Fork_ManualOp_TokeAlarm.Text, 0) * 10);
            srm_ForkParam_CTRL.ParamItemsRec.ManualOp_Leftmm = Global_Class.UTIL_StrToIntDef(ed_Fork_ManualOp_Leftmm.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.ManualOp_Rightmm = Global_Class.UTIL_StrToUInt32Def(ed_Fork_ManualOp_Rightmm.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.ManualOp_Creepmm = (byte)Global_Class.UTIL_StrToIntDef(ed_Fork_ManualOp_Creepmm.Text, 0);
            if (cb_Fork_MotorDirection.SelectedIndex >= 0) srm_ForkParam_CTRL.ParamItemsRec.MotorDirection = (byte)cb_Fork_MotorDirection.SelectedIndex;

            if (cb_Fork_Encoder_Direct.SelectedIndex >= 0) srm_ForkParam_CTRL.ParamItemsRec.Encoder_Direct = (byte)cb_Fork_Encoder_Direct.SelectedIndex;
            if (cb_Fork_Encoder_InputPulse.SelectedIndex >= 0) srm_ForkParam_CTRL.ParamItemsRec.Encoder_InputPulse = (byte)cb_Fork_Encoder_InputPulse.SelectedIndex;
            srm_ForkParam_CTRL.ParamItemsRec.Encoder_Preset = Global_Class.UTIL_StrToUInt32Def(ed_Fork_Encoder_Preset.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.Encoder_Pulsebee = (UInt32)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Fork_Encoder_Pulsebee.Text, 0) * 1000000);

            srm_ForkParam_CTRL.ParamItemsRec.RefPositionOffset_FCL = (byte)Global_Class.UTIL_StrToIntDef(ed_Fork_RefPositionFCLOffset.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.RefPositionOffset_FCR = (byte)Global_Class.UTIL_StrToIntDef(ed_Fork_RefPositionFCROffset.Text, 0);
            if (cb_Fork_RefType.SelectedIndex >= 0) srm_ForkParam_CTRL.ParamItemsRec.RefPositionType = (byte)cb_Fork_RefType.SelectedIndex;

            if (cb_Fork_AutoForkDecel_Set.SelectedIndex >= 0) srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_Set = (byte)cb_Fork_AutoForkDecel_Set.SelectedIndex;
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[0] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_1.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[1] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_2.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[2] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_3.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[3] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_4.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[4] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_5.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[5] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_6.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[6] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_7.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[7] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_8.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[8] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_9.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[9]  = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_10.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[10] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_11.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[11] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_12.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[12] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_13.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[13] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_14.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[14] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_15.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[15] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_16.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[16] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_17.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[17] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_18.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[18] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_19.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[19] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_20.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[20] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_21.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[21] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_22.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[22] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_23.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.AutoForkDecel_pos[23] = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_AutoForkDecel_pos_24.Text, 0);

            srm_ForkParam_CTRL.ParamItemsRec.TwinFork_Gap = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fork_TwinFork_Gap.Text, 0);


            srm_ForkParam_CTRL.ParamItemsRec.FHL = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_POS_FHL.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.FML = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_POS_FML.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.FEL = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_POS_FEL.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.FHR = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_POS_FHR.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.FMR = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_POS_FMR.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.FER = (Int16)Global_Class.UTIL_StrToIntDef(ed_Fork_POS_FER.Text, 0);

            srm_ForkParam_CTRL.ParamItemsRec.Invertor_Ref = Global_Class.UTIL_StrToIntDef(ed_Fork_Invertor_Ref.Text, 0);
            srm_ForkParam_CTRL.ParamItemsRec.Invertor_Gain = (UInt32)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Fork_Invertor_Gain.Text, 0) * 1000);
            srm_ForkParam_CTRL.ParamItemsRec.Invertor_DifferLimit = (byte)Global_Class.UTIL_StrToIntDef(ed_Fork_Invertor_DifferLimit.Text, 0);
            if (cb_Fork_Invertor_Use.SelectedIndex >= 0) srm_ForkParam_CTRL.ParamItemsRec.Invertor_ParamUse = (byte)cb_Fork_Invertor_Use.SelectedIndex;

            if (cb_Fork_FER_Current.SelectedIndex == 1) srm_ForkParam_CTRL.ParamItemsRec.ForkCurrenPosition_Define |= (byte)0x80;
            if (cb_Fork_FMR_Current.SelectedIndex == 1) srm_ForkParam_CTRL.ParamItemsRec.ForkCurrenPosition_Define |= (byte)0x40;
            if (cb_Fork_FHR_Current.SelectedIndex == 1) srm_ForkParam_CTRL.ParamItemsRec.ForkCurrenPosition_Define |= (byte)0x20;
            if (cb_Fork_FEL_Current.SelectedIndex == 1) srm_ForkParam_CTRL.ParamItemsRec.ForkCurrenPosition_Define |= (byte)0x10;
            if (cb_Fork_FML_Current.SelectedIndex == 1) srm_ForkParam_CTRL.ParamItemsRec.ForkCurrenPosition_Define |= (byte)0x08;
            if (cb_Fork_FHL_Current.SelectedIndex == 1) srm_ForkParam_CTRL.ParamItemsRec.ForkCurrenPosition_Define |= (byte)0x04;
            if (cb_Fork_FC_Current.SelectedIndex == 2) srm_ForkParam_CTRL.ParamItemsRec.ForkCurrenPosition_Define |= (byte)0x02;
            if (cb_Fork_FC_Current.SelectedIndex == 1)  srm_ForkParam_CTRL.ParamItemsRec.ForkCurrenPosition_Define |= (byte)0x01;
            


            if (!isFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A8, srm_ForkParam_CTRL);
            }

        }

        public void Display_Init()
        {
            ed_Fork_AutoHighSpeed_Speed.Text = "0";
            ed_Fork_AutoHighSpeed_Accel.Text = "0";
            ed_Fork_AutoHighSpeed_Decel.Text = "0";
            ed_Fork_AutoHighSpeed_Jerk.Text = "0";

            ed_Fork_AutoMiddleSpeed_Speed.Text = "0";
            ed_Fork_AutoMiddleSpeed_Accel.Text = "0";
            ed_Fork_AutoMiddleSpeed_Decel.Text = "0";
            ed_Fork_AutoMiddleSpeed_jerk.Text = "0";

            ed_Fork_AutoLowSpeed_Speed.Text = "0";
            ed_Fork_AutoLowSpeed_Accel.Text = "0";
            ed_Fork_AutoLowSpeed_Decel.Text = "0";
            ed_Fork_AutoLowSpeed_jerk.Text = "0";

            ed_Fork_ManualMiddleSpeed_Speed.Text = "0";
            ed_Fork_ManualMiddleSpeed_Accel.Text = "0";
            ed_Fork_ManualMiddleSpeed_Decel.Text = "0";
            ed_Fork_ManualMiddleSpeed_Jerk.Text = "0";

            ed_Fork_ManualLowSpeed_Speed.Text = "0";
            ed_Fork_ManualLowSpeed_Accel.Text = "0";
            ed_Fork_ManualLowSpeed_Decel.Text = "0";
            ed_Fork_ManualLowSpeed_Jerk.Text = "0";

            ed_Fork_ForceMode_Speed.Text = "0";
            ed_Fork_ForceMode_Accel.Text = "0";
            ed_Fork_ForceMode_Decel.Text = "0";
            ed_Fork_ForceMode_Jerk.Text = "0";

            ed_Fork_Creep_Speed.Text = "0";
            ed_Fork_Creep_Decel.Text = "0";
            ed_Fork_Creep_Jerk.Text = "0";

            ed_Fork_RefMode_Speed.Text = "0";
            ed_Fork_RefMode_Accel.Text = "0";
            ed_Fork_RefMode_Decel.Text = "0";
            ed_Fork_RefMode_Jerk.Text = "0";

            ed_Fork_Emergency_Decel.Text = "0";
            ed_Fork_Emergency_Jerk.Text = "0";

            ed_Fork_AutoDecel1_Speed.Text = "0";
            ed_Fork_AutoDecel2_Speed.Text = "0";

            ed_Fork_CurrentPos_Offset.Text = "0";
            ed_Fork_CurrentPos_histerisis.Text = "0";

            ed_Fork_ManualOp_TokeAlarm.Text = "0.0";
            ed_Fork_ManualOp_Leftmm.Text = "0";
            ed_Fork_ManualOp_Rightmm.Text = "0";
            ed_Fork_ManualOp_Creepmm.Text = "0";
            cb_Fork_MotorDirection.SelectedIndex = -1;


            cb_Fork_Encoder_Direct.SelectedIndex = -1;
            cb_Fork_Encoder_InputPulse.SelectedIndex =  -1;
            ed_Fork_Encoder_Preset.Text = "0";
            ed_Fork_Encoder_Pulsebee.Text = "0.000000";

            ed_Fork_RefPositionFCLOffset.Text = "0";
            ed_Fork_RefPositionFCROffset.Text = "0";
            cb_Fork_RefType.SelectedIndex = -1;

            cb_Fork_AutoForkDecel_Set.SelectedIndex = -1;
            ed_Fork_AutoForkDecel_pos_1.Text = "0";
            ed_Fork_AutoForkDecel_pos_2.Text = "0";
            ed_Fork_AutoForkDecel_pos_3.Text = "0";
            ed_Fork_AutoForkDecel_pos_4.Text = "0";
            ed_Fork_AutoForkDecel_pos_5.Text = "0";
            ed_Fork_AutoForkDecel_pos_6.Text = "0";
            ed_Fork_AutoForkDecel_pos_7.Text = "0";
            ed_Fork_AutoForkDecel_pos_8.Text = "0";
            ed_Fork_AutoForkDecel_pos_9.Text = "0";
            ed_Fork_AutoForkDecel_pos_10.Text = "0";
            ed_Fork_AutoForkDecel_pos_11.Text = "0";
            ed_Fork_AutoForkDecel_pos_12.Text = "0";
            ed_Fork_AutoForkDecel_pos_13.Text = "0";
            ed_Fork_AutoForkDecel_pos_14.Text = "0";
            ed_Fork_AutoForkDecel_pos_15.Text = "0";
            ed_Fork_AutoForkDecel_pos_16.Text = "0";
            ed_Fork_AutoForkDecel_pos_17.Text = "0";
            ed_Fork_AutoForkDecel_pos_18.Text = "0";
            ed_Fork_AutoForkDecel_pos_19.Text = "0";
            ed_Fork_AutoForkDecel_pos_20.Text = "0";
            ed_Fork_AutoForkDecel_pos_21.Text = "0";
            ed_Fork_AutoForkDecel_pos_22.Text = "0";
            ed_Fork_AutoForkDecel_pos_23.Text = "0";
            ed_Fork_AutoForkDecel_pos_24.Text = "0";

            ed_Fork_TwinFork_Gap.Text = "0";

            ed_Fork_POS_FHL.Text = "0";
            ed_Fork_POS_FML.Text = "0";
            ed_Fork_POS_FEL.Text = "0";
            ed_Fork_POS_FHR.Text = "0";
            ed_Fork_POS_FMR.Text = "0";
            ed_Fork_POS_FER.Text = "0";

            ed_Fork_Invertor_Ref.Text = "0";
            ed_Fork_Invertor_Gain.Text = "0";
            ed_Fork_Invertor_DifferLimit.Text = "0";
            cb_Fork_Invertor_Use.SelectedIndex = -1;

            cb_Fork_FER_Current.SelectedIndex = -1;
            cb_Fork_FMR_Current.SelectedIndex = -1;
            cb_Fork_FHR_Current.SelectedIndex = -1;
            cb_Fork_FEL_Current.SelectedIndex = -1;
            cb_Fork_FML_Current.SelectedIndex = -1;
            cb_Fork_FHL_Current.SelectedIndex = -1;
            cb_Fork_FC_Current.SelectedIndex = -1;
        }

        public void Display_SRM_Param(byte[] data)
        {
            srm_ForkParam_RES = (VEXI_DEFS.TSRM_ForkParamRes)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TSRM_ForkParamRes));
            srm_ForkParam_CTRL.ParamItemsRec = (VEXI_DEFS.TSRM_ForkParamRes)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TSRM_ForkParamRes));
            Display_SRM_Param();
        }

        public unsafe void Display_SRM_Param()
        {
            ed_Fork_AutoHighSpeed_Speed.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Auto_High.Speed);
            ed_Fork_AutoHighSpeed_Accel.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Auto_High.Accel);
            ed_Fork_AutoHighSpeed_Decel.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Auto_High.Decel);
            ed_Fork_AutoHighSpeed_Jerk.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Auto_High.jerk);

            ed_Fork_AutoMiddleSpeed_Speed.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Auto_Middle.Speed);
            ed_Fork_AutoMiddleSpeed_Accel.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Auto_Middle.Accel);
            ed_Fork_AutoMiddleSpeed_Decel.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Auto_Middle.Decel);
            ed_Fork_AutoMiddleSpeed_jerk.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Auto_Middle.jerk);

            ed_Fork_AutoLowSpeed_Speed.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Auto_Low.Speed);
            ed_Fork_AutoLowSpeed_Accel.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Auto_Low.Accel);
            ed_Fork_AutoLowSpeed_Decel.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Auto_Low.Decel);
            ed_Fork_AutoLowSpeed_jerk.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Auto_Low.jerk);

            ed_Fork_ManualMiddleSpeed_Speed.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Manual_Middle.Speed);
            ed_Fork_ManualMiddleSpeed_Accel.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Manual_Middle.Accel);
            ed_Fork_ManualMiddleSpeed_Decel.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Manual_Middle.Decel);
            ed_Fork_ManualMiddleSpeed_Jerk.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Manual_Middle.jerk);

            ed_Fork_ManualLowSpeed_Speed.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Manual_Low.Speed);
            ed_Fork_ManualLowSpeed_Accel.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Manual_Low.Accel);
            ed_Fork_ManualLowSpeed_Decel.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Manual_Low.Decel);
            ed_Fork_ManualLowSpeed_Jerk.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Manual_Low.jerk);

            ed_Fork_ForceMode_Speed.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Force.Speed);
            ed_Fork_ForceMode_Accel.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Force.Accel);
            ed_Fork_ForceMode_Decel.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Force.Decel);
            ed_Fork_ForceMode_Jerk.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Force.jerk);

            ed_Fork_Creep_Speed.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Creep.Speed);
            ed_Fork_Creep_Decel.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Creep.Decel);
            ed_Fork_Creep_Jerk.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Creep.jerk);

            ed_Fork_RefMode_Speed.Text = string.Format("{0}", srm_ForkParam_RES.Speed_RefSet.Speed);
            ed_Fork_RefMode_Accel.Text = string.Format("{0}", srm_ForkParam_RES.Speed_RefSet.Accel);
            ed_Fork_RefMode_Decel.Text = string.Format("{0}", srm_ForkParam_RES.Speed_RefSet.Decel);
            ed_Fork_RefMode_Jerk.Text = string.Format("{0}", srm_ForkParam_RES.Speed_RefSet.jerk);

            ed_Fork_Emergency_Decel.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Emergency.Decel);
            ed_Fork_Emergency_Jerk.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Emergency.jerk);

            ed_Fork_AutoDecel1_Speed.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Auto_Decel1.Speed);
            ed_Fork_AutoDecel2_Speed.Text = string.Format("{0}", srm_ForkParam_RES.Speed_Auto_Decel2.Speed);


            ed_Fork_CurrentPos_Offset.Text = string.Format("{0}", srm_ForkParam_RES.CurrentPos_Offset);
            ed_Fork_CurrentPos_histerisis.Text = string.Format("{0}", srm_ForkParam_RES.CurrentPos_histerisis);

            ed_Fork_ManualOp_TokeAlarm.Text = string.Format("{0:0.0}", (double)srm_ForkParam_RES.ManualOp_TokeAlarm / 10);
            ed_Fork_ManualOp_Leftmm.Text = string.Format("{0}", srm_ForkParam_RES.ManualOp_Leftmm);
            ed_Fork_ManualOp_Rightmm.Text = string.Format("{0}", srm_ForkParam_RES.ManualOp_Rightmm);
            ed_Fork_ManualOp_Creepmm.Text = string.Format("{0}", srm_ForkParam_RES.ManualOp_Creepmm);
            cb_Fork_MotorDirection.SelectedIndex = Math.Min(srm_ForkParam_RES.MotorDirection, cb_Fork_MotorDirection.Items.Count - 1);

            cb_Fork_Encoder_Direct.SelectedIndex = Math.Min(srm_ForkParam_RES.Encoder_Direct, cb_Fork_Encoder_Direct.Items.Count - 1);
            cb_Fork_Encoder_InputPulse.SelectedIndex = Math.Min(srm_ForkParam_RES.Encoder_InputPulse, cb_Fork_Encoder_InputPulse.Items.Count - 1);
            ed_Fork_Encoder_Preset.Text = string.Format("{0}", srm_ForkParam_RES.Encoder_Preset);
            ed_Fork_Encoder_Pulsebee.Text = string.Format("{0:0.000000}", (double)srm_ForkParam_RES.Encoder_Pulsebee / 1000000);

            ed_Fork_RefPositionFCLOffset.Text = string.Format("{0}", srm_ForkParam_RES.RefPositionOffset_FCL);
            ed_Fork_RefPositionFCROffset.Text = string.Format("{0}", srm_ForkParam_RES.RefPositionOffset_FCR);
            cb_Fork_RefType.SelectedIndex = Math.Min(srm_ForkParam_RES.RefPositionType, cb_Fork_RefType.Items.Count - 1);

            cb_Fork_AutoForkDecel_Set.SelectedIndex = Math.Min(srm_ForkParam_RES.AutoForkDecel_Set, cb_Fork_AutoForkDecel_Set.Items.Count - 1);
            ed_Fork_AutoForkDecel_pos_1.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[0]);
            ed_Fork_AutoForkDecel_pos_2.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[1]);
            ed_Fork_AutoForkDecel_pos_3.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[2]);
            ed_Fork_AutoForkDecel_pos_4.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[3]);
            ed_Fork_AutoForkDecel_pos_5.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[4]);
            ed_Fork_AutoForkDecel_pos_6.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[5]);
            ed_Fork_AutoForkDecel_pos_7.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[6]);
            ed_Fork_AutoForkDecel_pos_8.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[7]);
            ed_Fork_AutoForkDecel_pos_9.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[8]);
            ed_Fork_AutoForkDecel_pos_10.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[9]);
            ed_Fork_AutoForkDecel_pos_11.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[10]);
            ed_Fork_AutoForkDecel_pos_12.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[11]);
            ed_Fork_AutoForkDecel_pos_13.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[12]);
            ed_Fork_AutoForkDecel_pos_14.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[13]);
            ed_Fork_AutoForkDecel_pos_15.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[14]);
            ed_Fork_AutoForkDecel_pos_16.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[15]);
            ed_Fork_AutoForkDecel_pos_17.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[16]);
            ed_Fork_AutoForkDecel_pos_18.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[17]);
            ed_Fork_AutoForkDecel_pos_19.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[18]);
            ed_Fork_AutoForkDecel_pos_20.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[19]);
            ed_Fork_AutoForkDecel_pos_21.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[20]);
            ed_Fork_AutoForkDecel_pos_22.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[21]);
            ed_Fork_AutoForkDecel_pos_23.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[22]);
            ed_Fork_AutoForkDecel_pos_24.Text = string.Format("{0}", srm_ForkParam_RES.AutoForkDecel_pos[23]);

            ed_Fork_TwinFork_Gap.Text = string.Format("{0}", srm_ForkParam_RES.TwinFork_Gap);

            ed_Fork_POS_FHL.Text = string.Format("{0}", srm_ForkParam_RES.FHL);
            ed_Fork_POS_FML.Text = string.Format("{0}", srm_ForkParam_RES.FML);
            ed_Fork_POS_FEL.Text = string.Format("{0}", srm_ForkParam_RES.FEL);
            ed_Fork_POS_FHR.Text = string.Format("{0}", srm_ForkParam_RES.FHR);
            ed_Fork_POS_FMR.Text = string.Format("{0}", srm_ForkParam_RES.FMR);
            ed_Fork_POS_FER.Text = string.Format("{0}", srm_ForkParam_RES.FER);

            ed_Fork_Invertor_Ref.Text = string.Format("{0}", srm_ForkParam_RES.Invertor_Ref);
            ed_Fork_Invertor_Gain.Text = string.Format("{0:0.000}", (double)srm_ForkParam_RES.Invertor_Gain / 1000);
            ed_Fork_Invertor_DifferLimit.Text = string.Format("{0}", srm_ForkParam_RES.Invertor_DifferLimit);
            cb_Fork_Invertor_Use.SelectedIndex = Math.Min(srm_ForkParam_RES.Invertor_ParamUse, cb_Fork_Invertor_Use.Items.Count - 1);


            if ((srm_ForkParam_CTRL.ParamItemsRec.ForkCurrenPosition_Define & 0x80) != 0) cb_Fork_FER_Current.SelectedIndex = 1; else cb_Fork_FER_Current.SelectedIndex = 0;
            if ((srm_ForkParam_CTRL.ParamItemsRec.ForkCurrenPosition_Define & 0x40) != 0) cb_Fork_FMR_Current.SelectedIndex = 1; else cb_Fork_FMR_Current.SelectedIndex = 0;
            if ((srm_ForkParam_CTRL.ParamItemsRec.ForkCurrenPosition_Define & 0x20) != 0) cb_Fork_FHR_Current.SelectedIndex = 1; else cb_Fork_FHR_Current.SelectedIndex = 0;
            if ((srm_ForkParam_CTRL.ParamItemsRec.ForkCurrenPosition_Define & 0x10) != 0) cb_Fork_FEL_Current.SelectedIndex = 1; else cb_Fork_FEL_Current.SelectedIndex = 0;
            if ((srm_ForkParam_CTRL.ParamItemsRec.ForkCurrenPosition_Define & 0x08) != 0) cb_Fork_FML_Current.SelectedIndex = 1; else cb_Fork_FML_Current.SelectedIndex = 0;
            if ((srm_ForkParam_CTRL.ParamItemsRec.ForkCurrenPosition_Define & 0x04) != 0) cb_Fork_FHL_Current.SelectedIndex = 1; else cb_Fork_FHL_Current.SelectedIndex = 0;
            cb_Fork_FC_Current.SelectedIndex = (srm_ForkParam_CTRL.ParamItemsRec.ForkCurrenPosition_Define & 0x03);

            btn_Param_Set.Enabled = ((form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode & 0x08) != 0);
        }

        #endregion


    }
}

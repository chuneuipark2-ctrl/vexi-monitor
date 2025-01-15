using System;
using System.IO;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_SRMLiftParam_Speed : Form
    {

        public Form_Main form_Main;
        public static VEXI_DEFS.TSRM_LiftParamRes srm__LiftParam_RES;
        public static VEXI_DEFS.TSRM_LiftParamCTRL srm_LiftParam_Ctrl;


        public Form_SRMLiftParam_Speed()
        {
            InitializeComponent();
        }

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

            toolTip1.SetToolTip(ed_Lift_Home_Position, "설정값 0 일 경우, 랙 또는 스테이션 최소값 적용");
            toolTip1.SetToolTip(ed_Lift_Maintance_Position, "설정값 0 일 경우, 홈위치값 적용.");
            toolTip1.SetToolTip(ed_Lift_ManualOp_TokeAlarm, "설정 범위 : 0.0 ~ 200.0%");

        }

        #region 컴포넌트 이벤트
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
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A5, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_PARAMReq)));
        }

        private void btn_Param_Set_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "승강 설정을 장치에 다운로드 하시겠습니까 ? "))
            {
                Do_Ctrl(false);
            }
        }

        //통합파일로 저장하는 것으로 변경
        //개별파일로 저장하는 소스는 남겨놓음
        private void btn_Param_FileWrite_Click(object sender, EventArgs e)
        {
            Do_Ctrl(true);

            saveFileDialog1.Filter = "*.Lift_Param|*.LIFT_PARAM";

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
                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_LiftParamCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(srm_LiftParam_Ctrl, Savebytes);
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
                form_Main.SRM_ToTalFile.Write_SRM_LIFT_PARAM(srm_LiftParam_Ctrl);
            }
        }

        //통합파일에서 읽어오는 것으로 변경
        //개별파일에서 읽어오는 소스는 남겨놓음
        private void btn_Param_FileRead_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.Lift_Param|*.LIFT_PARAM";

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
                        ushort Len = (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_LiftParamCTRL));
                        if (br.BaseStream.Length == Len)
                        {
                            Savebytes = br.ReadBytes(Len);
                            srm_LiftParam_Ctrl = (VEXI_DEFS.TSRM_LiftParamCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_LiftParamCTRL));
                            srm__LiftParam_RES = (VEXI_DEFS.TSRM_LiftParamRes)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_LiftParamRes), 35, Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_LiftParamRes)));

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
                if (form_Main.SRM_ToTalFile.Read_SRM_LIFT_PARAM(ref srm_LiftParam_Ctrl))
                {
                    srm__LiftParam_RES = srm_LiftParam_Ctrl.ParamItemsRec;

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

            fixed (VEXI_DEFS.TSRM_LiftParamCTRL* TmpPtr = &srm_LiftParam_Ctrl)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)TmpPtr, Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_LiftParamCTRL)));
            }

            srm_LiftParam_Ctrl.CtrlFlag[0] = 0x00;
            srm_LiftParam_Ctrl.CtrlFlag[1] = 0xFF;
            srm_LiftParam_Ctrl.CtrlFlag[2] = 0x1F;
            srm_LiftParam_Ctrl.CtrlFlag[3] = 0xF7;
            srm_LiftParam_Ctrl.CtrlFlag[4] = 0xFF;

            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_High.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoHighSpeed_Speed.Text, 10);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_High.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoHighSpeed_Accel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_High.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoHighSpeed_Decel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_High.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoHighSpeed_Jerk.Text, 1);

            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Middle.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoMiddleSpeed_Speed.Text, 10);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Middle.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoMiddleSpeed_Accel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Middle.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoMiddleSpeed_Decel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Middle.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoMiddleSpeed_jerk.Text, 1);

            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Low.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoLowSpeed_Speed.Text, 10);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Low.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoLowSpeed_Accel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Low.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoLowSpeed_Decel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Low.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoLowSpeed_jerk.Text, 1);

            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Middle.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualMiddleSpeed_Speed.Text, 10);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Middle.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualMiddleSpeed_Accel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Middle.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualMiddleSpeed_Decel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Middle.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualMiddleSpeed_Jerk.Text, 1);

            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Low.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualLowSpeed_Speed.Text, 10);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Low.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualLowSpeed_Accel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Low.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualLowSpeed_Decel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Low.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualLowSpeed_Jerk.Text, 1);

            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Force.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ForceMode_Speed.Text, 10);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Force.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ForceMode_Accel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Force.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ForceMode_Decel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Force.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ForceMode_Jerk.Text, 1);

            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Creep.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_Creep_Speed.Text, 10);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Creep.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_Creep_Decel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Creep.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_Creep_Jerk.Text, 1);
                                   
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_RefSet.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_RefMode_Speed.Text, 10);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_RefSet.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_RefMode_Accel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_RefSet.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_RefMode_Decel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_RefSet.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_RefMode_Jerk.Text, 1);

            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Emergency.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_Emergency_Decel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Emergency.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_Emergency_Jerk.Text, 1);
                                   
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Decel1.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoDecel1_Speed.Text, 10);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Decel2.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoDecel2_Speed.Text, 10);
                                   
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_AutoForkOut.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoForkOut_Speed.Text, 10);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_AutoForkOut.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoForkOut_Accel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_AutoForkOut.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoForkOut_Decel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_AutoForkOut.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoForkOut_Jerk.Text, 1);
                                   
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_ManualForkOut.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualForkOut_Speed.Text, 10);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_ManualForkOut.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualForkOut_Accel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_ManualForkOut.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualForkOut_Decel.Text, 1);
            srm_LiftParam_Ctrl.ParamItemsRec.Speed_ManualForkOut.jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualForkOut_Jerk.Text, 1);
                                   

            srm_LiftParam_Ctrl.ParamItemsRec.CurrentPos_Offset = (byte)Global_Class.UTIL_StrToIntDef(ed_Lift_CurrentPos_Offset.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.CurrentPos_histerisis = (byte)Global_Class.UTIL_StrToIntDef(ed_Lift_CurrentPos_histerisis.Text, 0);

            srm_LiftParam_Ctrl.ParamItemsRec.ManualOp_TokeAlarm = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Lift_ManualOp_TokeAlarm.Text, 0) * 10);
            srm_LiftParam_Ctrl.ParamItemsRec.ManualOp_Startmm = Global_Class.UTIL_StrToIntDef(ed_Lift_ManualOp_Startmm.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.ManualOp_Endmm = Global_Class.UTIL_StrToIntDef(ed_Lift_ManualOp_Endmm.Text, 0);

            srm_LiftParam_Ctrl.ParamItemsRec.breakOpenContinueTime = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Lift_breakOpenContinueTime.Text, 0) * 100);


            if (cb_Lift_Home_SpeedType.SelectedIndex >= 0) srm_LiftParam_Ctrl.ParamItemsRec.Home_SpeedType = (byte)cb_Lift_Home_SpeedType.SelectedIndex;
            srm_LiftParam_Ctrl.ParamItemsRec.Home_Position = Global_Class.UTIL_StrToIntDef(ed_Lift_Home_Position.Text, 0);

            if (cb_Lift_Maintance_SpeedType.SelectedIndex >= 0) srm_LiftParam_Ctrl.ParamItemsRec.Maintance_SpeedType = (byte)cb_Lift_Maintance_SpeedType.SelectedIndex;
            srm_LiftParam_Ctrl.ParamItemsRec.Maintance_Position = Global_Class.UTIL_StrToIntDef(ed_Lift_Maintance_Position.Text, 0);


            if (cb_Lift_DecelSensorOpSet_1.SelectedIndex >= 0) srm_LiftParam_Ctrl.ParamItemsRec.DecelSensorOpSet_1 = (byte)cb_Lift_DecelSensorOpSet_1.SelectedIndex;
            if (cb_Lift_DecelSensorOpSet_2.SelectedIndex >= 0) srm_LiftParam_Ctrl.ParamItemsRec.DecelSensorOpSet_2 = (byte)cb_Lift_DecelSensorOpSet_2.SelectedIndex;

            if (cb_Lift_SoftLimit_DetectSet.SelectedIndex >= 0) srm_LiftParam_Ctrl.ParamItemsRec.SoftLimit_DetectSet = (byte)cb_Lift_SoftLimit_DetectSet.SelectedIndex;
            srm_LiftParam_Ctrl.ParamItemsRec.SoftLimit_HomePos = Global_Class.UTIL_StrToIntDef(ed_Lift_SoftLimit_HomePos.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.SoftLimit_EndPos = Global_Class.UTIL_StrToIntDef(ed_Lift_SoftLimit_EndPos.Text, 0);

            if (cb_Lift_SoftDecel_DetectSet.SelectedIndex >= 0) srm_LiftParam_Ctrl.ParamItemsRec.SoftDecel_DetectSet = (byte)cb_Lift_SoftDecel_DetectSet.SelectedIndex;
            srm_LiftParam_Ctrl.ParamItemsRec.SoftDecel_Offset = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_SoftDecel_Offset.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.SoftDecel_StopDistance_1 = Global_Class.UTIL_StrToIntDef(ed_Lift_SoftDecel_StopDistance_1.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.SoftDecel_StopDistance_2 = Global_Class.UTIL_StrToIntDef(ed_Lift_SoftDecel_StopDistance_2.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.SoftDecel_StopDistance_3 = Global_Class.UTIL_StrToIntDef(ed_Lift_SoftDecel_StopDistance_3.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.SoftDecel_StopDistance_4 = Global_Class.UTIL_StrToIntDef(ed_Lift_SoftDecel_StopDistance_4.Text, 0);

            if (cb_Lift_RefSetDog_DetectSet.SelectedIndex >= 0) srm_LiftParam_Ctrl.ParamItemsRec.RefSetDog_DetectSet = (byte)cb_Lift_RefSetDog_DetectSet.SelectedIndex;
            srm_LiftParam_Ctrl.ParamItemsRec.RefSetDog_Offset = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_RefSetDog_Offset.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.RefSetDog_Home = Global_Class.UTIL_StrToIntDef(ed_Lift_RefSetDog_Home_1.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.RefSetDog_End = Global_Class.UTIL_StrToIntDef(ed_Lift_RefSetDog_End_1.Text, 0);

            if (cb_Lift_DecelDog_DetectSet.SelectedIndex >= 0) srm_LiftParam_Ctrl.ParamItemsRec.DecelDog_DetectSet = (byte)cb_Lift_DecelDog_DetectSet.SelectedIndex;
            srm_LiftParam_Ctrl.ParamItemsRec.DecelDog_Offset = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_DecelDog_Offset.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.DecelDog_Front1_Pos1 = Global_Class.UTIL_StrToIntDef(ed_Lift_DecelDog_Front1_Pos1.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.DecelDog_Front1_Pos2 = Global_Class.UTIL_StrToIntDef(ed_Lift_DecelDog_Front1_Pos2.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.DecelDog_Front2_Pos = Global_Class.UTIL_StrToIntDef(ed_Lift_DecelDog_Front2_Pos1.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.DecelDog_Rear1_Pos1 = Global_Class.UTIL_StrToIntDef(ed_Lift_DecelDog_Rear1_Pos1.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.DecelDog_Rear1_Pos2 = Global_Class.UTIL_StrToIntDef(ed_Lift_DecelDog_Rear1_Pos2.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.DecelDog_Rear2_Pos = Global_Class.UTIL_StrToIntDef(ed_Lift_DecelDog_Rear2_Pos1.Text, 0);

            if (cb_Lift_jerk_ForkOut_Set.SelectedIndex >= 0) srm_LiftParam_Ctrl.ParamItemsRec.jerk_ForkOut_Set = (byte)cb_Lift_jerk_ForkOut_Set.SelectedIndex;
            srm_LiftParam_Ctrl.ParamItemsRec.jerk_ForkOut_DefTime = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_jerk_ForkOut_DefTime.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.jerk_ForkOut_SetTime = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_jerk_ForkOut_SetTime.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.jerk_ForkOut_GoodTime = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_jerk_ForkOut_GoodTime.Text, 0);

            srm_LiftParam_Ctrl.ParamItemsRec.Invertor_Ref = Global_Class.UTIL_StrToIntDef(ed_Lift_Invertor_Ref.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.Invertor_Gain = (UInt32)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Lift_Invertor_Gain.Text, 0) * 1000);
            srm_LiftParam_Ctrl.ParamItemsRec.Invertor_DifferLimit = (byte)Global_Class.UTIL_StrToIntDef(ed_Lift_Invertor_DifferLimit.Text, 0);
            if (cb_Lift_Invertor_Use.SelectedIndex >= 0) srm_LiftParam_Ctrl.ParamItemsRec.Invertor_ParamUse = (byte)cb_Lift_Invertor_Use.SelectedIndex;


            srm_LiftParam_Ctrl.ParamItemsRec.LevelUp_Offset = (byte)Global_Class.UTIL_StrToIntDef(ed_Lift_LevelUp_Offset.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.LevelDn_Offset = (sbyte)Global_Class.UTIL_StrToIntDef(ed_Lift_LevelDn_Offset.Text, 0);
            srm_LiftParam_Ctrl.ParamItemsRec.DoubleGap = (byte)Global_Class.UTIL_StrToIntDef(ed_Lift_DoubleGap.Text, 0);

            if (!isFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A6, srm_LiftParam_Ctrl);
            }

        }

        public void Display_Init()
        {
            ed_Lift_AutoHighSpeed_Speed.Text = "0";
            ed_Lift_AutoHighSpeed_Accel.Text = "0";
            ed_Lift_AutoHighSpeed_Decel.Text = "0";
            ed_Lift_AutoHighSpeed_Jerk.Text  = "0";

            ed_Lift_AutoMiddleSpeed_Speed.Text = "0";
            ed_Lift_AutoMiddleSpeed_Accel.Text = "0";
            ed_Lift_AutoMiddleSpeed_Decel.Text = "0";
            ed_Lift_AutoMiddleSpeed_jerk.Text  = "0";

            ed_Lift_AutoLowSpeed_Speed.Text = "0";
            ed_Lift_AutoLowSpeed_Accel.Text = "0";
            ed_Lift_AutoLowSpeed_Decel.Text = "0";
            ed_Lift_AutoLowSpeed_jerk.Text  = "0";

            ed_Lift_ManualMiddleSpeed_Speed.Text = "0";
            ed_Lift_ManualMiddleSpeed_Accel.Text = "0";
            ed_Lift_ManualMiddleSpeed_Decel.Text = "0";
            ed_Lift_ManualMiddleSpeed_Jerk.Text  = "0";

            ed_Lift_ManualLowSpeed_Speed.Text = "0";
            ed_Lift_ManualLowSpeed_Accel.Text = "0";
            ed_Lift_ManualLowSpeed_Decel.Text = "0";
            ed_Lift_ManualLowSpeed_Jerk.Text  = "0";

            ed_Lift_ForceMode_Speed.Text = "0";
            ed_Lift_ForceMode_Accel.Text = "0";
            ed_Lift_ForceMode_Decel.Text = "0";
            ed_Lift_ForceMode_Jerk.Text  = "0";

            ed_Lift_Creep_Speed.Text = "0";
            ed_Lift_Creep_Decel.Text = "0";
            ed_Lift_Creep_Jerk.Text  = "0";

            ed_Lift_RefMode_Speed.Text = "0";
            ed_Lift_RefMode_Accel.Text = "0";
            ed_Lift_RefMode_Decel.Text = "0";
            ed_Lift_RefMode_Jerk.Text  = "0";

            ed_Lift_Emergency_Decel.Text = "0";
            ed_Lift_Emergency_Jerk.Text  = "0";

            ed_Lift_AutoDecel1_Speed.Text = "0";
            ed_Lift_AutoDecel2_Speed.Text = "0";

            ed_Lift_AutoForkOut_Speed.Text = "0";
            ed_Lift_AutoForkOut_Accel.Text = "0";
            ed_Lift_AutoForkOut_Decel.Text = "0";
            ed_Lift_AutoForkOut_Jerk.Text  = "0";

            ed_Lift_ManualForkOut_Speed.Text = "0";
            ed_Lift_ManualForkOut_Accel.Text = "0";
            ed_Lift_ManualForkOut_Decel.Text = "0";
            ed_Lift_ManualForkOut_Jerk.Text  = "0";


            ed_Lift_CurrentPos_Offset.Text = "0";
            ed_Lift_CurrentPos_histerisis.Text = "0";

            ed_Lift_ManualOp_TokeAlarm.Text = "0.0";
            ed_Lift_ManualOp_Startmm.Text = "0";
            ed_Lift_ManualOp_Endmm.Text = "0";

            ed_Lift_breakOpenContinueTime.Text = "0.00";

            

            cb_Lift_Home_SpeedType.SelectedIndex = -1;
             ed_Lift_Home_Position.Text = "0";

            cb_Lift_Maintance_SpeedType.SelectedIndex = -1;
            ed_Lift_Maintance_Position.Text = "0";

            cb_Lift_DecelSensorOpSet_1.SelectedIndex = -1;
            cb_Lift_DecelSensorOpSet_2.SelectedIndex = -1;

            cb_Lift_SoftLimit_DetectSet.SelectedIndex = -1;
            ed_Lift_SoftLimit_HomePos.Text = "0";
            ed_Lift_SoftLimit_EndPos.Text = "0";

            cb_Lift_SoftDecel_DetectSet.SelectedIndex = -1;
            ed_Lift_SoftDecel_Offset.Text = "0";
            ed_Lift_SoftDecel_StopDistance_1.Text = "0";
            ed_Lift_SoftDecel_StopDistance_2.Text = "0";
            ed_Lift_SoftDecel_StopDistance_3.Text = "0";
            ed_Lift_SoftDecel_StopDistance_4.Text = "0";

            cb_Lift_RefSetDog_DetectSet.SelectedIndex = -1;
            ed_Lift_RefSetDog_Offset.Text = "0";
            ed_Lift_RefSetDog_Home_1.Text = "0";
            ed_Lift_RefSetDog_End_1.Text = "0";

            cb_Lift_DecelDog_DetectSet.SelectedIndex = -1;
            ed_Lift_DecelDog_Offset.Text = "0";
            ed_Lift_DecelDog_Front1_Pos1.Text = "0";
            ed_Lift_DecelDog_Front1_Pos2.Text = "0";
            ed_Lift_DecelDog_Front2_Pos1.Text = "0";
            ed_Lift_DecelDog_Rear1_Pos1.Text = "0";
            ed_Lift_DecelDog_Rear1_Pos2.Text = "0";
            ed_Lift_DecelDog_Rear2_Pos1.Text = "0";

            cb_Lift_jerk_ForkOut_Set.SelectedIndex = -1;
            ed_Lift_jerk_ForkOut_DefTime.Text = "0";
            ed_Lift_jerk_ForkOut_SetTime.Text = "0";
            ed_Lift_jerk_ForkOut_GoodTime.Text = "0";

            ed_Lift_Invertor_Ref.Text = "0";
            ed_Lift_Invertor_Gain.Text = "0";
            ed_Lift_Invertor_DifferLimit.Text = "0";
            cb_Lift_Invertor_Use.SelectedIndex = -1;


            ed_Lift_LevelUp_Offset.Text = "0";
            ed_Lift_LevelDn_Offset.Text = "0";
            ed_Lift_DoubleGap.Text = "0";
        }
        public void Display_SRM_Param(byte[] data)
        {
            srm__LiftParam_RES = (VEXI_DEFS.TSRM_LiftParamRes)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TSRM_LiftParamRes));
            srm_LiftParam_Ctrl.ParamItemsRec = (VEXI_DEFS.TSRM_LiftParamRes)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TSRM_LiftParamRes));
            Display_SRM_Param();
        }

        public void Display_SRM_Param()
        {

            ed_Lift_AutoHighSpeed_Speed.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Auto_High.Speed);
            ed_Lift_AutoHighSpeed_Accel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Auto_High.Accel);
            ed_Lift_AutoHighSpeed_Decel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Auto_High.Decel);
            ed_Lift_AutoHighSpeed_Jerk.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Auto_High.jerk);

            ed_Lift_AutoMiddleSpeed_Speed.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Auto_Middle.Speed);
            ed_Lift_AutoMiddleSpeed_Accel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Auto_Middle.Accel);
            ed_Lift_AutoMiddleSpeed_Decel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Auto_Middle.Decel);
            ed_Lift_AutoMiddleSpeed_jerk.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Auto_Middle.jerk);

            ed_Lift_AutoLowSpeed_Speed.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Auto_Low.Speed);
            ed_Lift_AutoLowSpeed_Accel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Auto_Low.Accel);
            ed_Lift_AutoLowSpeed_Decel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Auto_Low.Decel);
            ed_Lift_AutoLowSpeed_jerk.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Auto_Low.jerk);

            ed_Lift_ManualMiddleSpeed_Speed.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Manual_Middle.Speed);
            ed_Lift_ManualMiddleSpeed_Accel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Manual_Middle.Accel);
            ed_Lift_ManualMiddleSpeed_Decel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Manual_Middle.Decel);
            ed_Lift_ManualMiddleSpeed_Jerk.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Manual_Middle.jerk);

            ed_Lift_ManualLowSpeed_Speed.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Manual_Low.Speed);
            ed_Lift_ManualLowSpeed_Accel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Manual_Low.Accel);
            ed_Lift_ManualLowSpeed_Decel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Manual_Low.Decel);
            ed_Lift_ManualLowSpeed_Jerk.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Manual_Low.jerk);

            ed_Lift_ForceMode_Speed.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Force.Speed);
            ed_Lift_ForceMode_Accel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Force.Accel);
            ed_Lift_ForceMode_Decel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Force.Decel);
            ed_Lift_ForceMode_Jerk.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Force.jerk);

            ed_Lift_Creep_Speed.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Creep.Speed);
            ed_Lift_Creep_Decel.Text= string.Format("{0}", srm__LiftParam_RES.Speed_Creep.Decel);
            ed_Lift_Creep_Jerk.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Creep.jerk);

            ed_Lift_RefMode_Speed.Text = string.Format("{0}", srm__LiftParam_RES.Speed_RefSet.Speed);
            ed_Lift_RefMode_Accel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_RefSet.Accel);
            ed_Lift_RefMode_Decel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_RefSet.Decel);
            ed_Lift_RefMode_Jerk.Text = string.Format("{0}", srm__LiftParam_RES.Speed_RefSet.jerk);

            ed_Lift_Emergency_Decel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Emergency.Decel);
            ed_Lift_Emergency_Jerk.Text  = string.Format("{0}", srm__LiftParam_RES.Speed_Emergency.jerk);

            ed_Lift_AutoDecel1_Speed.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Auto_Decel1.Speed);
            ed_Lift_AutoDecel2_Speed.Text = string.Format("{0}", srm__LiftParam_RES.Speed_Auto_Decel2.Speed);

            ed_Lift_AutoForkOut_Speed.Text = string.Format("{0}", srm__LiftParam_RES.Speed_AutoForkOut.Speed);
            ed_Lift_AutoForkOut_Accel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_AutoForkOut.Accel);
            ed_Lift_AutoForkOut_Decel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_AutoForkOut.Decel);
            ed_Lift_AutoForkOut_Jerk.Text = string.Format("{0}", srm__LiftParam_RES.Speed_AutoForkOut.jerk);

            ed_Lift_ManualForkOut_Speed.Text = string.Format("{0}", srm__LiftParam_RES.Speed_ManualForkOut.Speed);
            ed_Lift_ManualForkOut_Accel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_ManualForkOut.Accel);
            ed_Lift_ManualForkOut_Decel.Text = string.Format("{0}", srm__LiftParam_RES.Speed_ManualForkOut.Decel);
            ed_Lift_ManualForkOut_Jerk.Text = string.Format("{0}", srm__LiftParam_RES.Speed_ManualForkOut.jerk);


            ed_Lift_CurrentPos_Offset.Text = string.Format("{0}", srm__LiftParam_RES.CurrentPos_Offset);
            ed_Lift_CurrentPos_histerisis.Text = string.Format("{0}", srm__LiftParam_RES.CurrentPos_histerisis);
            
            ed_Lift_ManualOp_TokeAlarm.Text = string.Format("{0:0.0}", (double)srm__LiftParam_RES.ManualOp_TokeAlarm / 10);
            ed_Lift_ManualOp_Startmm.Text = string.Format("{0}", srm__LiftParam_RES.ManualOp_Startmm);
            ed_Lift_ManualOp_Endmm.Text = string.Format("{0}", srm__LiftParam_RES.ManualOp_Endmm);

            ed_Lift_breakOpenContinueTime.Text = string.Format("{0:0.00}", (double)srm__LiftParam_RES.breakOpenContinueTime / 100);


            cb_Lift_Home_SpeedType.SelectedIndex = Math.Min(srm__LiftParam_RES.Home_SpeedType, cb_Lift_Home_SpeedType.Items.Count - 1);

            ed_Lift_Home_Position.Text = string.Format("{0}", srm__LiftParam_RES.Home_Position);

            cb_Lift_Maintance_SpeedType.SelectedIndex = Math.Min(srm__LiftParam_RES.Maintance_SpeedType, cb_Lift_Maintance_SpeedType.Items.Count - 1);
            ed_Lift_Maintance_Position.Text = string.Format("{0}", srm__LiftParam_RES.Maintance_Position);

            cb_Lift_DecelSensorOpSet_1.SelectedIndex = Math.Min(srm__LiftParam_RES.DecelSensorOpSet_1, cb_Lift_DecelSensorOpSet_1.Items.Count - 1);
            cb_Lift_DecelSensorOpSet_2.SelectedIndex = Math.Min(srm__LiftParam_RES.DecelSensorOpSet_2,cb_Lift_DecelSensorOpSet_2.Items.Count - 1);

            cb_Lift_SoftLimit_DetectSet.SelectedIndex = Math.Min(srm__LiftParam_RES.SoftLimit_DetectSet, cb_Lift_SoftLimit_DetectSet.Items.Count - 1);
            ed_Lift_SoftLimit_HomePos.Text = string.Format("{0}", srm__LiftParam_RES.SoftLimit_HomePos);
            ed_Lift_SoftLimit_EndPos.Text = string.Format("{0}", srm__LiftParam_RES.SoftLimit_EndPos);

            cb_Lift_SoftDecel_DetectSet.SelectedIndex = Math.Min(srm__LiftParam_RES.SoftDecel_DetectSet, cb_Lift_SoftDecel_DetectSet.Items.Count - 1);
            ed_Lift_SoftDecel_Offset.Text = string.Format("{0}", srm__LiftParam_RES.SoftDecel_Offset);
            ed_Lift_SoftDecel_StopDistance_1.Text = string.Format("{0}", srm__LiftParam_RES.SoftDecel_StopDistance_1);
            ed_Lift_SoftDecel_StopDistance_2.Text = string.Format("{0}", srm__LiftParam_RES.SoftDecel_StopDistance_2);
            ed_Lift_SoftDecel_StopDistance_3.Text = string.Format("{0}", srm__LiftParam_RES.SoftDecel_StopDistance_3);
            ed_Lift_SoftDecel_StopDistance_4.Text = string.Format("{0}", srm__LiftParam_RES.SoftDecel_StopDistance_4);

            cb_Lift_RefSetDog_DetectSet.SelectedIndex = Math.Min(srm__LiftParam_RES.RefSetDog_DetectSet, cb_Lift_RefSetDog_DetectSet.Items.Count - 1);
            ed_Lift_RefSetDog_Offset.Text = string.Format("{0}", srm__LiftParam_RES.RefSetDog_Offset);
            ed_Lift_RefSetDog_Home_1.Text = string.Format("{0}", srm__LiftParam_RES.RefSetDog_Home);
            ed_Lift_RefSetDog_End_1.Text = string.Format("{0}", srm__LiftParam_RES.RefSetDog_End);

            cb_Lift_DecelDog_DetectSet.SelectedIndex = Math.Min(srm__LiftParam_RES.DecelDog_DetectSet, cb_Lift_DecelDog_DetectSet.Items.Count - 1);
            ed_Lift_DecelDog_Offset.Text = string.Format("{0}", srm__LiftParam_RES.DecelDog_Offset);
            ed_Lift_DecelDog_Front1_Pos1.Text = string.Format("{0}", srm__LiftParam_RES.DecelDog_Front1_Pos1);
            ed_Lift_DecelDog_Front1_Pos2.Text = string.Format("{0}", srm__LiftParam_RES.DecelDog_Front1_Pos2);
            ed_Lift_DecelDog_Front2_Pos1.Text = string.Format("{0}", srm__LiftParam_RES.DecelDog_Front2_Pos);
            ed_Lift_DecelDog_Rear1_Pos1.Text = string.Format("{0}", srm__LiftParam_RES.DecelDog_Rear1_Pos1);
            ed_Lift_DecelDog_Rear1_Pos2.Text = string.Format("{0}", srm__LiftParam_RES.DecelDog_Rear1_Pos2);
            ed_Lift_DecelDog_Rear2_Pos1.Text = string.Format("{0}", srm__LiftParam_RES.DecelDog_Rear2_Pos);

            cb_Lift_jerk_ForkOut_Set.SelectedIndex = Math.Min(srm__LiftParam_RES.jerk_ForkOut_Set, cb_Lift_jerk_ForkOut_Set.Items.Count - 1);
            ed_Lift_jerk_ForkOut_DefTime.Text = string.Format("{0}", srm__LiftParam_RES.jerk_ForkOut_DefTime);
            ed_Lift_jerk_ForkOut_SetTime.Text = string.Format("{0}", srm__LiftParam_RES.jerk_ForkOut_SetTime);
            ed_Lift_jerk_ForkOut_GoodTime.Text = string.Format("{0}", srm__LiftParam_RES.jerk_ForkOut_GoodTime);

            ed_Lift_Invertor_Ref.Text = string.Format("{0}", srm__LiftParam_RES.Invertor_Ref);
            ed_Lift_Invertor_Gain.Text = string.Format("{0:0.000}", (double)srm__LiftParam_RES.Invertor_Gain / 1000);
            ed_Lift_Invertor_DifferLimit.Text = string.Format("{0}", srm__LiftParam_RES.Invertor_DifferLimit);
            cb_Lift_Invertor_Use.SelectedIndex = Math.Min(srm__LiftParam_RES.Invertor_ParamUse, cb_Lift_Invertor_Use.Items.Count - 1);


            ed_Lift_LevelUp_Offset.Text = string.Format("{0}", srm__LiftParam_RES.LevelUp_Offset);
            ed_Lift_LevelDn_Offset.Text = string.Format("{0}", srm__LiftParam_RES.LevelDn_Offset);
            ed_Lift_DoubleGap.Text = string.Format("{0}", srm__LiftParam_RES.DoubleGap);

            btn_Param_Set.Enabled = ((form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode & 0x08) != 0);
        }

        #endregion

        private unsafe void btn_Lift_OpSpeed_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "운영속도로 초기화 하시겠습니까? "))
            {
                srm_LiftParam_Ctrl.CtrlFlag[0] = 0x02;
                srm_LiftParam_Ctrl.CtrlFlag[1] = 0x00;

                srm_LiftParam_Ctrl.CtrlFlag[2] = 0x00;
                srm_LiftParam_Ctrl.CtrlFlag[3] = 0x00;

                srm_LiftParam_Ctrl.CtrlFlag[4] = 0x00;

                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A6, srm_LiftParam_Ctrl);

            }
        }

        private unsafe void btn_Lift_TestSpeed_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "시운전속도로 초기화 하시겠습니까? "))
            {
                srm_LiftParam_Ctrl.CtrlFlag[0] = 0x01;
                srm_LiftParam_Ctrl.CtrlFlag[1] = 0x00;

                srm_LiftParam_Ctrl.CtrlFlag[2] = 0x00;
                srm_LiftParam_Ctrl.CtrlFlag[3] = 0x00;

                srm_LiftParam_Ctrl.CtrlFlag[4] = 0x00;

                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A6, srm_LiftParam_Ctrl);

            }

        }

    }
}

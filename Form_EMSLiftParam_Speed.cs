using System;
using System.IO;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_EMSLiftParam_Speed : Form
    {

        public Form_Main form_Main;
        public static VEXI_DEFS.TEMS_LiftParamRes ems_LiftParam_RES;
        public static VEXI_DEFS.TEMS_LiftParamCTRL ems_LiftParam_Ctrl;


        public Form_EMSLiftParam_Speed()
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

            toolTip1.SetToolTip(ed_StartPos, "셋업, 강제, 수동모드 운전범위 소프트웨어 리미트");
            toolTip1.SetToolTip(ed_EndPos, "셋업, 강제, 수동모드 운전범위 소프트웨어 리미트");
            toolTip1.SetToolTip(ed_CurrenctPostion_RetryRange, "설정값 이내의 오버런이 발생하면 재구동 실행");
            toolTip1.SetToolTip(ed_ManualOp_TokeAlarm, "설정 범위 : 0.0 ~ 200.0%");

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
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A5, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_REC_PARAMReq)));
        }

        private void btn_Param_Set_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "승강 설정을 장치에 다운로드 하시겠습니까 ? "))
            {
                Do_Ctrl(false);
            }
        }

        private void btn_Param_FileWrite_Click(object sender, EventArgs e)
        {
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
                form_Main.EMS_ToTalFile.Write_EMS_LIFT_PARAM(ems_LiftParam_Ctrl);
            }
        }

        private void btn_Param_FileRead_Click(object sender, EventArgs e)
        {
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
                if (form_Main.EMS_ToTalFile.Read_EMS_LIFT_PARAM(ref ems_LiftParam_Ctrl))
                {
                    ems_LiftParam_RES = ems_LiftParam_Ctrl.ParamItemsRec;

                    Display_EMS_Param();
                }
                else
                {
                    MessageBox.Show("해당 파일안에 [" + this.Text + "] 데이터가 없습니다");
                }
            }
        }



        #endregion

        #region 기능함수

        public void Display_Init()
        {
            ed_Lift_AutoHighSpeed_Speed.Text = "0.0";
            ed_Lift_AutoHighSpeed_Accel.Text = "0";
            ed_Lift_AutoHighSpeed_Decel.Text = "0";
            ed_Lift_AutoHighSpeed_AccJerk.Text = "0";
            ed_Lift_AutoHighSpeed_DecJerk.Text = "0";

            ed_Lift_AutoMiddleSpeed_Speed.Text = "0.0";
            ed_Lift_AutoMiddleSpeed_Accel.Text = "0";
            ed_Lift_AutoMiddleSpeed_Decel.Text = "0";
            ed_Lift_AutoMiddleSpeed_Accjerk.Text = "0";
            ed_Lift_AutoMiddleSpeed_Decjerk.Text = "0";

            ed_Lift_AutoLowSpeed_Speed.Text = "0.0";
            ed_Lift_AutoLowSpeed_Accel.Text = "0";
            ed_Lift_AutoLowSpeed_Decel.Text = "0";
            ed_Lift_AutoLowSpeed_Accjerk.Text = "0";
            ed_Lift_AutoLowSpeed_Decjerk.Text = "0";

            ed_Lift_ManualMiddleSpeed_Speed.Text = "0.0";
            ed_Lift_ManualMiddleSpeed_Accel.Text = "0";
            ed_Lift_ManualMiddleSpeed_Decel.Text = "0";
            ed_Lift_ManualMiddleSpeed_AccJerk.Text = "0";
            ed_Lift_ManualMiddleSpeed_DecJerk.Text = "0";

            ed_Lift_ManualLowSpeed_Speed.Text = "0.0";
            ed_Lift_ManualLowSpeed_Accel.Text = "0";
            ed_Lift_ManualLowSpeed_Decel.Text = "0";
            ed_Lift_ManualLowSpeed_AccJerk.Text = "0";
            ed_Lift_ManualLowSpeed_DecJerk.Text = "0";

            ed_Lift_ForceMode_Speed.Text = "0.0";
            ed_Lift_ForceMode_Accel.Text = "0";
            ed_Lift_ForceMode_Decel.Text = "0";
            ed_Lift_ForceMode_AccJerk.Text = "0";
            ed_Lift_ForceMode_DecJerk.Text = "0";


            ed_Lift_Emergency_Decel.Text = "0";
            ed_Lift_Emergency_DecJerk.Text = "0";


            ed_MAX_RPM.Text = "0";
            ed_CALC_MPM.Text = "0";
            ed_CALC_RPM.Text = "0";
            cb_MotorDirection.SelectedIndex = -1;
            ed_ManualOp_TokeAlarm.Text = "0.0";
            ed_StartPos.Text = "0";
            ed_EndPos.Text = "0";
            ed_breakOpenContinueTime.Text = "0.00";

            ed_Invertor_Reference.Text = "0";
            ed_Invertor_PositionGain.Text = "0.000";
            ed_Invetor_Positiontolerance.Text = "0";
            cb_Invetor_Param_Use.SelectedIndex = -1;

            ed_CurrenctPostion_Offset.Text = "0";
            ed_CurrenctPostion_His.Text = "0";
            ed_CurrenctPostion_RetryCount.Text = "0";
            ed_CurrenctPostion_RetryRange.Text = "0";
        }

        public void Display_EMS_Param(byte[] data)
        {
            ems_LiftParam_RES = (VEXI_DEFS.TEMS_LiftParamRes)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TEMS_LiftParamRes));
            ems_LiftParam_Ctrl.ParamItemsRec = (VEXI_DEFS.TEMS_LiftParamRes)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TEMS_LiftParamRes));
            Display_EMS_Param();
        }

        public void Display_EMS_Param()
        {
            
            ed_Lift_AutoHighSpeed_Speed.Text = string.Format("{0:0.0}", (double)ems_LiftParam_RES.Speed_Auto_High.Speed / 10);
            ed_Lift_AutoHighSpeed_Accel.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Auto_High.Accel);
            ed_Lift_AutoHighSpeed_Decel.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Auto_High.Decel);
            ed_Lift_AutoHighSpeed_AccJerk.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Auto_High.A_jerk);
            ed_Lift_AutoHighSpeed_DecJerk.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Auto_High.D_jerk);

            ed_Lift_AutoMiddleSpeed_Speed.Text = string.Format("{0:0.0}", (double)ems_LiftParam_RES.Speed_Auto_Middle.Speed / 10);
            ed_Lift_AutoMiddleSpeed_Accel.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Auto_Middle.Accel);
            ed_Lift_AutoMiddleSpeed_Decel.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Auto_Middle.Decel);
            ed_Lift_AutoMiddleSpeed_Accjerk.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Auto_Middle.A_jerk);
            ed_Lift_AutoMiddleSpeed_Decjerk.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Auto_Middle.D_jerk);

            ed_Lift_AutoLowSpeed_Speed.Text = string.Format("{0:0.0}", (double)ems_LiftParam_RES.Speed_Auto_Low.Speed / 10);
            ed_Lift_AutoLowSpeed_Accel.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Auto_Low.Accel);
            ed_Lift_AutoLowSpeed_Decel.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Auto_Low.Decel);
            ed_Lift_AutoLowSpeed_Accjerk.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Auto_Low.A_jerk);
            ed_Lift_AutoLowSpeed_Decjerk.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Auto_Low.D_jerk);

            ed_Lift_ManualMiddleSpeed_Speed.Text = string.Format("{0:0.0}", (double)ems_LiftParam_RES.Speed_Manual_Middle.Speed / 10);
            ed_Lift_ManualMiddleSpeed_Accel.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Manual_Middle.Accel);
            ed_Lift_ManualMiddleSpeed_Decel.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Manual_Middle.Decel);
            ed_Lift_ManualMiddleSpeed_AccJerk.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Manual_Middle.A_jerk);
            ed_Lift_ManualMiddleSpeed_DecJerk.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Manual_Middle.D_jerk);

            ed_Lift_ManualLowSpeed_Speed.Text = string.Format("{0:0.0}", (double)ems_LiftParam_RES.Speed_Manual_Low.Speed / 10);
            ed_Lift_ManualLowSpeed_Accel.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Manual_Low.Accel);
            ed_Lift_ManualLowSpeed_Decel.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Manual_Low.Decel);
            ed_Lift_ManualLowSpeed_AccJerk.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Manual_Low.A_jerk);
            ed_Lift_ManualLowSpeed_DecJerk.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Manual_Low.D_jerk);

            ed_Lift_ForceMode_Speed.Text = string.Format("{0:0.0}", (double)ems_LiftParam_RES.Speed_Force.Speed / 10);
            ed_Lift_ForceMode_Accel.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Force.Accel);
            ed_Lift_ForceMode_Decel.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Force.Decel);
            ed_Lift_ForceMode_AccJerk.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Force.A_jerk);
            ed_Lift_ForceMode_DecJerk.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Force.D_jerk);

            ed_Lift_Emergency_Decel.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Emergency.Decel);
            ed_Lift_Emergency_DecJerk.Text = string.Format("{0}", ems_LiftParam_RES.Speed_Emergency.D_jerk);


            ed_MAX_RPM.Text = string.Format("{0}", ems_LiftParam_RES.MAX_RPM);
            ed_CALC_MPM.Text = string.Format("{0}", ems_LiftParam_RES.CALC_MPM);
            ed_CALC_RPM.Text = string.Format("{0}", ems_LiftParam_RES.CALC_RPM);
            cb_MotorDirection.SelectedIndex = Math.Min(ems_LiftParam_RES.MotorDirection, cb_MotorDirection.Items.Count - 1);
            ed_ManualOp_TokeAlarm.Text = string.Format("{0:0.0}", (double)ems_LiftParam_RES.ManualOp_TokeAlarm / 10);
            ed_StartPos.Text = string.Format("{0}", ems_LiftParam_RES.ManualOp_Startmm);
            ed_EndPos.Text = string.Format("{0}", ems_LiftParam_RES.ManualOp_Endmm);
            ed_breakOpenContinueTime.Text = string.Format("{0:0.00}", (double)ems_LiftParam_RES.breakOpenContinueTime / 100);

            ed_Invertor_Reference.Text = string.Format("{0}", ems_LiftParam_RES.Invertor_Reference);
            ed_Invertor_PositionGain.Text = string.Format("{0:0.000}", (double)ems_LiftParam_RES.Invertor_PositionGain / 1000);
            ed_Invetor_Positiontolerance.Text = string.Format("{0}", ems_LiftParam_RES.Invetor_Positiontolerance);
            cb_Invetor_Param_Use.SelectedIndex = Math.Min(ems_LiftParam_RES.Invetor_Param_Use, cb_Invetor_Param_Use.Items.Count - 1);

            ed_CurrenctPostion_Offset.Text = string.Format("{0}", ems_LiftParam_RES.CurrenctPostion_Offset);
            ed_CurrenctPostion_His.Text = string.Format("{0}", ems_LiftParam_RES.CurrenctPostion_His);
            ed_CurrenctPostion_RetryCount.Text = string.Format("{0}", ems_LiftParam_RES.CurrenctPostion_RetryCount);
            ed_CurrenctPostion_RetryRange.Text = string.Format("{0}", ems_LiftParam_RES.CurrenctPostion_RetryRange);


            btn_Param_Set.Enabled = ((form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.DevMode & 0x08) != 0);
        }

        private unsafe void Do_Ctrl(bool isFileSave)
        {

            fixed (VEXI_DEFS.TEMS_LiftParamCTRL* TmpPtr = &ems_LiftParam_Ctrl)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)TmpPtr, Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_LiftParamCTRL)));
            }

            ems_LiftParam_Ctrl.CtrlFlag[0] = 0x00;
            ems_LiftParam_Ctrl.CtrlFlag[1] = 0x7F;
            ems_LiftParam_Ctrl.CtrlFlag[2] = 0x1F;
            ems_LiftParam_Ctrl.CtrlFlag[3] = 0x07;

            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_High.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoHighSpeed_Speed.Text, 10);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_High.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoHighSpeed_Accel.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_High.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoHighSpeed_Decel.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_High.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoHighSpeed_AccJerk.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_High.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoHighSpeed_DecJerk.Text, 1);

            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Middle.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoMiddleSpeed_Speed.Text, 10);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Middle.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoMiddleSpeed_Accel.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Middle.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoMiddleSpeed_Decel.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Middle.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoMiddleSpeed_Accjerk.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Middle.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoMiddleSpeed_Decjerk.Text, 1);

            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Low.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoLowSpeed_Speed.Text, 10);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Low.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoLowSpeed_Accel.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Low.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoLowSpeed_Decel.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Low.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoLowSpeed_Accjerk.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Auto_Low.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_AutoLowSpeed_Decjerk.Text, 1);

            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Middle.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualMiddleSpeed_Speed.Text, 10);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Middle.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualMiddleSpeed_Accel.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Middle.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualMiddleSpeed_Decel.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Middle.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualMiddleSpeed_AccJerk.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Middle.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualMiddleSpeed_DecJerk.Text, 1);

            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Low.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualLowSpeed_Speed.Text, 10);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Low.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualLowSpeed_Accel.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Low.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualLowSpeed_Decel.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Low.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualLowSpeed_AccJerk.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Manual_Low.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ManualLowSpeed_DecJerk.Text, 1);

            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Force.Speed = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ForceMode_Speed.Text, 10);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Force.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ForceMode_Accel.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Force.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ForceMode_Decel.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Force.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ForceMode_AccJerk.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Force.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_ForceMode_DecJerk.Text, 1);

            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Emergency.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_Emergency_Decel.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Speed_Emergency.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Lift_Emergency_DecJerk.Text, 1);


            ems_LiftParam_Ctrl.ParamItemsRec.MAX_RPM = (UInt16)Global_Class.UTIL_StrToIntDef(ed_MAX_RPM.Text, 0);
            ems_LiftParam_Ctrl.ParamItemsRec.CALC_MPM = (UInt16)Global_Class.UTIL_StrToIntDef(ed_CALC_MPM.Text, 0);
            ems_LiftParam_Ctrl.ParamItemsRec.CALC_RPM = (UInt16)Global_Class.UTIL_StrToIntDef(ed_CALC_RPM.Text, 0);
            if (cb_MotorDirection.SelectedIndex >= 0) ems_LiftParam_Ctrl.ParamItemsRec.MotorDirection = (byte)cb_MotorDirection.SelectedIndex;
            ems_LiftParam_Ctrl.ParamItemsRec.ManualOp_TokeAlarm = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_ManualOp_TokeAlarm.Text, 0) * 10);
            ems_LiftParam_Ctrl.ParamItemsRec.ManualOp_Startmm = (UInt16)Global_Class.UTIL_StrToIntDef(ed_StartPos.Text, 0);
            ems_LiftParam_Ctrl.ParamItemsRec.ManualOp_Endmm = (UInt16)Global_Class.UTIL_StrToIntDef(ed_EndPos.Text, 0);
            ems_LiftParam_Ctrl.ParamItemsRec.breakOpenContinueTime = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_breakOpenContinueTime.Text, 0) * 100);

            ems_LiftParam_Ctrl.ParamItemsRec.Invertor_Reference = (Int32)Global_Class.UTIL_StrToIntDef(ed_Invertor_Reference.Text, 1);
            ems_LiftParam_Ctrl.ParamItemsRec.Invertor_PositionGain = (UInt32)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Invertor_PositionGain.Text, 0) * 1000);
            ems_LiftParam_Ctrl.ParamItemsRec.Invetor_Positiontolerance = (byte)Global_Class.UTIL_StrToIntDef(ed_Invetor_Positiontolerance.Text, 1);
            if (cb_Invetor_Param_Use.SelectedIndex >= 0) ems_LiftParam_Ctrl.ParamItemsRec.Invetor_Param_Use = (byte)cb_Invetor_Param_Use.SelectedIndex;

            ems_LiftParam_Ctrl.ParamItemsRec.CurrenctPostion_Offset     = (byte)Global_Class.UTIL_StrToIntDef(ed_CurrenctPostion_Offset.Text, 0);
            ems_LiftParam_Ctrl.ParamItemsRec.CurrenctPostion_His        = (byte)Global_Class.UTIL_StrToIntDef(ed_CurrenctPostion_His.Text, 0);
            ems_LiftParam_Ctrl.ParamItemsRec.CurrenctPostion_RetryCount = (byte)Global_Class.UTIL_StrToIntDef(ed_CurrenctPostion_RetryCount.Text, 0);
            ems_LiftParam_Ctrl.ParamItemsRec.CurrenctPostion_RetryRange = (byte)Global_Class.UTIL_StrToIntDef(ed_CurrenctPostion_RetryRange.Text, 0);

            if (!isFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A6, ems_LiftParam_Ctrl);
            }

        }


        #endregion

        private unsafe void btn_Lift_OpSpeed_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "운영속도로 초기화 하시겠습니까? "))
            {
                ems_LiftParam_Ctrl.CtrlFlag[0] = 0x02;
                ems_LiftParam_Ctrl.CtrlFlag[1] = 0x00;

                ems_LiftParam_Ctrl.CtrlFlag[2] = 0x00;
                ems_LiftParam_Ctrl.CtrlFlag[3] = 0x00;

                ems_LiftParam_Ctrl.CtrlFlag[4] = 0x00;

                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A6, ems_LiftParam_Ctrl);

            }
        }

        private unsafe void btn_Lift_TestSpeed_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "시운전속도로 초기화 하시겠습니까? "))
            {
                ems_LiftParam_Ctrl.CtrlFlag[0] = 0x01;
                ems_LiftParam_Ctrl.CtrlFlag[1] = 0x00;

                ems_LiftParam_Ctrl.CtrlFlag[2] = 0x00;
                ems_LiftParam_Ctrl.CtrlFlag[3] = 0x00;

                ems_LiftParam_Ctrl.CtrlFlag[4] = 0x00;

                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A6, ems_LiftParam_Ctrl);

            }

        }

        private void groupBox17_Enter(object sender, EventArgs e)
        {

        }
    }
}

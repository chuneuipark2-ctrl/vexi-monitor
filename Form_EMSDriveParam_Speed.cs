using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;   //DllImport

namespace VEXI
{
    public partial class Form_EMSDriveParam_Speed : Form
    {

        public Form_Main form_Main;
        public static VEXI_DEFS.TEMS_DriveParamRes ems_DriveParam_RES;
        public static VEXI_DEFS.TEMS_DriveParamCTRL ems_DriveParam_CTRL;


        public Form_EMSDriveParam_Speed()
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

            //Tip을 넣고 싶은 경우 아래와 같이 넣어준다
            toolTip1.SetToolTip(ed_Drive_ManualOp_TokeAlarm, "설정 범위 : 0.0 ~ 200.0%");
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

        private void btn_SpeedParam_Load_Click(object sender, EventArgs e)
        {
            Display_Init();
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A3, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_REC_PARAMReq)));
        }

        private void btn_SpeedParam_Set_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "주행 설정을 장치에 다운로드 하시겠습니까 ? "))
            {
                Do_Ctrl(false);
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
                form_Main.EMS_ToTalFile.Write_EMS_DRIVE_PARAM(ems_DriveParam_CTRL);
            }
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
                if (form_Main.EMS_ToTalFile.Read_EMS_DRIVE_PARAM(ref ems_DriveParam_CTRL))
                {
                    ems_DriveParam_RES = ems_DriveParam_CTRL.ParamItemsRec;

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
            ed_Drive_AutoHighSpeed_Speed.Text = "0.0";
            ed_Drive_AutoHighSpeed_Accel.Text = "0";
            ed_Drive_AutoHighSpeed_Decel.Text = "0";
            ed_Drive_AutoHighSpeed_AJerk.Text = "0";
            ed_Drive_AutoHighSpeed_DJerk.Text = "0";

            ed_Drive_AutoMiddleSpeed_Speed.Text = "0.0";
            ed_Drive_AutoMiddleSpeed_Accel.Text = "0";
            ed_Drive_AutoMiddleSpeed_Decel.Text = "0";
            ed_Drive_AutoMiddleSpeed_Ajerk.Text = "0";
            ed_Drive_AutoMiddleSpeed_Djerk.Text = "0";

            ed_Drive_AutoLowSpeed_Speed.Text = "0.0";
            ed_Drive_AutoLowSpeed_Accel.Text = "0";
            ed_Drive_AutoLowSpeed_Decel.Text = "0";
            ed_Drive_AutoLowSpeed_Ajerk.Text = "0";
            ed_Drive_AutoLowSpeed_Djerk.Text = "0";

            ed_Drive_ManualMiddleSpeed_Speed.Text = "0.0";
            ed_Drive_ManualMiddleSpeed_Accel.Text = "0";
            ed_Drive_ManualMiddleSpeed_Decel.Text = "0";
            ed_Drive_ManualMiddleSpeed_AJerk.Text = "0";
            ed_Drive_ManualMiddleSpeed_DJerk.Text = "0";

            ed_Drive_ManualLowSpeed_Speed.Text = "0.0";
            ed_Drive_ManualLowSpeed_Accel.Text = "0";
            ed_Drive_ManualLowSpeed_Decel.Text = "0";
            ed_Drive_ManualLowSpeed_AJerk.Text = "0";
            ed_Drive_ManualLowSpeed_DJerk.Text = "0";

            ed_Drive_ForceMode_Speed.Text = "0.0";
            ed_Drive_ForceMode_Accel.Text = "0";
            ed_Drive_ForceMode_Decel.Text = "0";
            ed_Drive_ForceMode_AJerk.Text = "0";
            ed_Drive_ForceMode_DJerk.Text = "0";

            ed_Drive_Creep_Speed.Text = "0.0";

            ed_Drive_RefMode_Speed.Text = "0.0";
            ed_Drive_RefMode_Accel.Text = "0";
            ed_Drive_RefMode_Decel.Text = "0";
            ed_Drive_RefMode_AJerk.Text = "0";
            ed_Drive_RefMode_DJerk.Text = "0";

            ed_Drive_Emergency_Decel.Text = "0";
            ed_Drive_Emergency_DJerk.Text = "0";

            ed_Drive_AutoDecel1_Speed.Text = "0.0";
            ed_Drive_AutoDecel2_Speed.Text = "0.0";

            ed_Drive_Collision_Decel.Text = "0";
            ed_Drive_Collision_DJerk.Text = "0";

            ed_Drive_MAX_RPM.Text = "0";
            ed_Drive_CALC_MPM.Text = "0";
            ed_Drive_CALC_RPM.Text = "0";
            cb_Drive_MotorDirection.SelectedIndex = -1;
            ed_Drive_ManualOp_TokeAlarm.Text = "0.0";
            ed_Drive_breakOpenContinueTime.Text = "0.00";

            cb_Invetor_Param_Use.SelectedIndex = -1;
            ed_Invertor_Reference.Text = "0";
            ed_Invertor_PositionGain.Text = "0.000";
            ed_Invetor_Positiontolerance.Text = "0";

            ed_InPosition_Offset.Text = "0";
            ed_InPosition_Hist.Text = "0";
            ed_RetryInPosition_Count.Text = "0";
            ed_RetryInPosition_Range.Text = "0";
            ed_CripRange.Text = "0";
            ed_CurrentDecel_OffsetTime.Text = "0.0";
            ed_CurrentDecel_OffsetMaxDistance.Text = "0";
            ed_CurrentLowSpeedDistance.Text = "0";
        }

        public void Display_EMS_Param(byte[] data)
        {
            ems_DriveParam_RES = (VEXI_DEFS.TEMS_DriveParamRes)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TEMS_DriveParamRes));
            ems_DriveParam_CTRL.ParamItemsRec = (VEXI_DEFS.TEMS_DriveParamRes)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TEMS_DriveParamRes));
            Display_EMS_Param();
        }

        public void Display_EMS_Param()
        {

            ed_Drive_AutoHighSpeed_Speed.Text = string.Format("{0:0.0}", (double)ems_DriveParam_RES.Speed_Auto_High.Speed / 10);
            ed_Drive_AutoHighSpeed_Accel.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Auto_High.Accel);
            ed_Drive_AutoHighSpeed_Decel.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Auto_High.Decel);
            ed_Drive_AutoHighSpeed_AJerk.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Auto_High.A_jerk);
            ed_Drive_AutoHighSpeed_DJerk.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Auto_High.D_jerk);

            ed_Drive_AutoMiddleSpeed_Speed.Text = string.Format("{0:0.0}", (double)ems_DriveParam_RES.Speed_Auto_Middle.Speed / 10);
            ed_Drive_AutoMiddleSpeed_Accel.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Auto_Middle.Accel);
            ed_Drive_AutoMiddleSpeed_Decel.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Auto_Middle.Decel);
            ed_Drive_AutoMiddleSpeed_Ajerk.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Auto_Middle.A_jerk);
            ed_Drive_AutoMiddleSpeed_Djerk.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Auto_Middle.D_jerk);

            ed_Drive_AutoLowSpeed_Speed.Text = string.Format("{0:0.0}", (double)ems_DriveParam_RES.Speed_Auto_Low.Speed / 10);
            ed_Drive_AutoLowSpeed_Accel.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Auto_Low.Accel);
            ed_Drive_AutoLowSpeed_Decel.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Auto_Low.Decel);
            ed_Drive_AutoLowSpeed_Ajerk.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Auto_Low.A_jerk);
            ed_Drive_AutoLowSpeed_Djerk.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Auto_Low.D_jerk);

            ed_Drive_ManualMiddleSpeed_Speed.Text = string.Format("{0:0.0}", (double)ems_DriveParam_RES.Speed_Manual_Middle.Speed / 10);
            ed_Drive_ManualMiddleSpeed_Accel.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Manual_Middle.Accel);
            ed_Drive_ManualMiddleSpeed_Decel.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Manual_Middle.Decel);
            ed_Drive_ManualMiddleSpeed_AJerk.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Manual_Middle.A_jerk);
            ed_Drive_ManualMiddleSpeed_DJerk.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Manual_Middle.D_jerk);

            ed_Drive_ManualLowSpeed_Speed.Text = string.Format("{0:0.0}", (double)ems_DriveParam_RES.Speed_Manual_Low.Speed / 10);
            ed_Drive_ManualLowSpeed_Accel.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Manual_Low.Accel);
            ed_Drive_ManualLowSpeed_Decel.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Manual_Low.Decel);
            ed_Drive_ManualLowSpeed_AJerk.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Manual_Low.A_jerk);
            ed_Drive_ManualLowSpeed_DJerk.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Manual_Low.D_jerk);

            ed_Drive_ForceMode_Speed.Text = string.Format("{0:0.0}", (double)ems_DriveParam_RES.Speed_Force.Speed / 10);
            ed_Drive_ForceMode_Accel.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Force.Accel);
            ed_Drive_ForceMode_Decel.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Force.Decel);
            ed_Drive_ForceMode_AJerk.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Force.A_jerk);
            ed_Drive_ForceMode_DJerk.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Force.D_jerk);

            ed_Drive_Creep_Speed.Text = string.Format("{0:0.0}", (double)ems_DriveParam_RES.Speed_Creep.Speed / 10);

            ed_Drive_RefMode_Speed.Text = string.Format("{0:0.0}", (double)ems_DriveParam_RES.Speed_RefSet.Speed / 10);
            ed_Drive_RefMode_Accel.Text = string.Format("{0}", ems_DriveParam_RES.Speed_RefSet.Accel);
            ed_Drive_RefMode_Decel.Text = string.Format("{0}", ems_DriveParam_RES.Speed_RefSet.Decel);
            ed_Drive_RefMode_AJerk.Text = string.Format("{0}", ems_DriveParam_RES.Speed_RefSet.A_jerk);
            ed_Drive_RefMode_DJerk.Text = string.Format("{0}", ems_DriveParam_RES.Speed_RefSet.D_jerk);

            ed_Drive_Emergency_Decel.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Emergency.Decel);
            ed_Drive_Emergency_DJerk.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Emergency.D_jerk);

            ed_Drive_AutoDecel1_Speed.Text = string.Format("{0:0.0}", (double)ems_DriveParam_RES.Speed_Auto_Decel1.Speed / 10);
            ed_Drive_AutoDecel2_Speed.Text = string.Format("{0:0.0}", (double)ems_DriveParam_RES.Speed_Auto_Decel2.Speed / 10);

            ed_Drive_Collision_Decel.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Collision.Decel);
            ed_Drive_Collision_DJerk.Text = string.Format("{0}", ems_DriveParam_RES.Speed_Collision.D_jerk);

            ed_Drive_MAX_RPM.Text = string.Format("{0}", ems_DriveParam_RES.MAX_RPM);
            ed_Drive_CALC_MPM.Text = string.Format("{0}", ems_DriveParam_RES.CALC_MPM);
            ed_Drive_CALC_RPM.Text = string.Format("{0}", ems_DriveParam_RES.CALC_RPM);
            cb_Drive_MotorDirection.SelectedIndex = Math.Min(ems_DriveParam_RES.MotorDirection, cb_Drive_MotorDirection.Items.Count - 1);
            ed_Drive_ManualOp_TokeAlarm.Text = string.Format("{0:0.0}", (double)ems_DriveParam_RES.ManualOp_TokeAlarm / 10);
            ed_Drive_breakOpenContinueTime.Text = string.Format("{0:0.00}", (double)ems_DriveParam_RES.breakOpenContinueTime / 100);

            
            ed_Invertor_Reference.Text = string.Format("{0}", ems_DriveParam_RES.Invertor_Reference);
            ed_Invertor_PositionGain.Text = string.Format("{0:0.000}", (double)ems_DriveParam_RES.Invertor_PositionGain / 1000);
            ed_Invetor_Positiontolerance.Text = string.Format("{0}", ems_DriveParam_RES.Invetor_Positiontolerance);
            cb_Invetor_Param_Use.SelectedIndex = Math.Min(ems_DriveParam_RES.Invetor_Param_Use, cb_Invetor_Param_Use.Items.Count - 1);

            ed_InPosition_Offset.Text = string.Format("{0}", ems_DriveParam_RES.InPosition_Offset);
            ed_InPosition_Hist.Text = string.Format("{0}", ems_DriveParam_RES.InPosition_Hist);
            ed_RetryInPosition_Count.Text = string.Format("{0}", ems_DriveParam_RES.RetryInPosition_Count);
            ed_RetryInPosition_Range.Text = string.Format("{0}", ems_DriveParam_RES.RetryInPosition_Range);
            ed_CripRange.Text = string.Format("{0}", ems_DriveParam_RES.CripRange);
            ed_CurrentDecel_OffsetTime.Text = string.Format("{0:0.0}", (double)ems_DriveParam_RES.CurrentDecel_OffsetTime / 10);
            ed_CurrentDecel_OffsetMaxDistance.Text = string.Format("{0}", ems_DriveParam_RES.CurrentDecel_OffsetMaxDistance);
            ed_CurrentLowSpeedDistance.Text = string.Format("{0}", ems_DriveParam_RES.CurrentLowSpeedDistance);


            btn_Param_Set.Enabled = ((form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.DevMode & 0x08) != 0);
            //btn_Param_Set.Enabled = true;
        }



        private unsafe void Do_Ctrl(bool isFileSave)
        {
            fixed (VEXI_DEFS.TEMS_DriveParamCTRL* TmpPtr = &ems_DriveParam_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)TmpPtr, Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_DriveParamCTRL)));
            }


            //제어플래그
            //전체 항목 제어 이기 때문에 전체 설정 플래그 1개만 세워도 되고 모든 제어 Flag를 세워도 된다. 
            ems_DriveParam_CTRL.CtrlFlag[0] = 0x00;
            ems_DriveParam_CTRL.CtrlFlag[1] = 0xFF;
            ems_DriveParam_CTRL.CtrlFlag[2] = 0x0F;
            ems_DriveParam_CTRL.CtrlFlag[3] = 0x07;

            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_High.Speed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Drive_AutoHighSpeed_Speed.Text, 10) * 10);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_High.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_AutoHighSpeed_Accel.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_High.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_AutoHighSpeed_Decel.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_High.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_AutoHighSpeed_AJerk.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_High.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_AutoHighSpeed_DJerk.Text, 1);

            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Middle.Speed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Drive_AutoMiddleSpeed_Speed.Text, 10) * 10);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Middle.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_AutoMiddleSpeed_Accel.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Middle.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_AutoMiddleSpeed_Decel.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Middle.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_AutoMiddleSpeed_Ajerk.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Middle.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_AutoMiddleSpeed_Djerk.Text, 1);

            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Low.Speed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Drive_AutoLowSpeed_Speed.Text, 10) * 10);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Low.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_AutoLowSpeed_Accel.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Low.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_AutoLowSpeed_Decel.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Low.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_AutoLowSpeed_Ajerk.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Low.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_AutoLowSpeed_Djerk.Text, 1);

            ems_DriveParam_CTRL.ParamItemsRec.Speed_Manual_Middle.Speed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Drive_ManualMiddleSpeed_Speed.Text, 10) * 10);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Manual_Middle.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_ManualMiddleSpeed_Accel.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Manual_Middle.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_ManualMiddleSpeed_Decel.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Manual_Middle.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_ManualMiddleSpeed_AJerk.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Manual_Middle.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_ManualMiddleSpeed_DJerk.Text, 1);


            ems_DriveParam_CTRL.ParamItemsRec.Speed_Manual_Low.Speed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Drive_ManualLowSpeed_Speed.Text, 10) * 10);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Manual_Low.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_ManualLowSpeed_Accel.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Manual_Low.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_ManualLowSpeed_Decel.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Manual_Low.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_ManualLowSpeed_AJerk.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Manual_Low.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_ManualLowSpeed_DJerk.Text, 1);

            ems_DriveParam_CTRL.ParamItemsRec.Speed_Force.Speed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Drive_ForceMode_Speed.Text, 10) * 10);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Force.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_ForceMode_Accel.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Force.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_ForceMode_Decel.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Force.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_ForceMode_AJerk.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Force.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_ForceMode_DJerk.Text, 1);

            ems_DriveParam_CTRL.ParamItemsRec.Speed_Creep.Speed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Drive_Creep_Speed.Text, 10) * 10);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Creep.Accel = 0;
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Creep.Decel = 0;
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Creep.A_jerk = 0;
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Creep.D_jerk = 0;


            ems_DriveParam_CTRL.ParamItemsRec.Speed_RefSet.Speed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Drive_RefMode_Speed.Text, 10) * 10);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_RefSet.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_RefMode_Accel.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_RefSet.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_RefMode_Decel.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_RefSet.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_RefMode_AJerk.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_RefSet.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_RefMode_DJerk.Text, 1);

            ems_DriveParam_CTRL.ParamItemsRec.Speed_Emergency.Speed = 0;
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Emergency.Accel = 0;
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Emergency.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_Emergency_Decel.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Emergency.D_jerk = 0;
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Emergency.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_Emergency_DJerk.Text, 1);

            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Decel1.Speed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Drive_AutoDecel1_Speed.Text, 10) * 10);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Decel1.Accel = 0;
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Decel1.Decel = 0;
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Decel1.A_jerk = 0;
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Decel1.D_jerk = 0;

            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Decel2.Speed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Drive_AutoDecel2_Speed.Text, 10) * 10);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Decel2.Accel = 0;
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Decel2.Decel = 0;
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Decel2.A_jerk = 0;
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Auto_Decel2.D_jerk = 0;

            ems_DriveParam_CTRL.ParamItemsRec.Speed_Collision.Speed = 0;
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Collision.Accel = 0;
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Collision.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_Collision_Decel.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Collision.D_jerk = 0;
            ems_DriveParam_CTRL.ParamItemsRec.Speed_Collision.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_Collision_DJerk.Text, 1);


            ems_DriveParam_CTRL.ParamItemsRec.MAX_RPM = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_MAX_RPM.Text, 0);
            ems_DriveParam_CTRL.ParamItemsRec.CALC_MPM = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_CALC_MPM.Text, 0);
            ems_DriveParam_CTRL.ParamItemsRec.CALC_RPM = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Drive_CALC_RPM.Text, 0);
            if (cb_Drive_MotorDirection.SelectedIndex >= 0) ems_DriveParam_CTRL.ParamItemsRec.MotorDirection = (byte)cb_Drive_MotorDirection.SelectedIndex;

            ems_DriveParam_CTRL.ParamItemsRec.ManualOp_TokeAlarm = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Drive_ManualOp_TokeAlarm.Text, 0) * 10);
            ems_DriveParam_CTRL.ParamItemsRec.breakOpenContinueTime = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Drive_breakOpenContinueTime.Text, 0) * 100);

            ems_DriveParam_CTRL.ParamItemsRec.Invertor_Reference = (Int32)Global_Class.UTIL_StrToIntDef(ed_Invertor_Reference.Text, 1);
            ems_DriveParam_CTRL.ParamItemsRec.Invertor_PositionGain = (UInt32)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Invertor_PositionGain.Text, 0) * 1000);
            ems_DriveParam_CTRL.ParamItemsRec.Invetor_Positiontolerance = (byte)Global_Class.UTIL_StrToIntDef(ed_Invetor_Positiontolerance.Text, 1);
            if (cb_Invetor_Param_Use.SelectedIndex >= 0) ems_DriveParam_CTRL.ParamItemsRec.Invetor_Param_Use = (byte)cb_Invetor_Param_Use.SelectedIndex;

            ems_DriveParam_CTRL.ParamItemsRec.InPosition_Offset = (byte)Global_Class.UTIL_StrToIntDef(ed_InPosition_Offset.Text, 0);
            ems_DriveParam_CTRL.ParamItemsRec.InPosition_Hist = (byte)Global_Class.UTIL_StrToIntDef(ed_InPosition_Hist.Text, 0);
            ems_DriveParam_CTRL.ParamItemsRec.RetryInPosition_Count = (byte)Global_Class.UTIL_StrToIntDef(ed_RetryInPosition_Count.Text, 0);
            ems_DriveParam_CTRL.ParamItemsRec.RetryInPosition_Range = (byte)Global_Class.UTIL_StrToIntDef(ed_RetryInPosition_Range.Text, 0);
            ems_DriveParam_CTRL.ParamItemsRec.CripRange = (UInt16)Global_Class.UTIL_StrToIntDef(ed_CripRange.Text, 0);
            ems_DriveParam_CTRL.ParamItemsRec.CurrentDecel_OffsetTime = (byte)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_CurrentDecel_OffsetTime.Text, 0) * 10);
            ems_DriveParam_CTRL.ParamItemsRec.CurrentDecel_OffsetMaxDistance = (UInt16)Global_Class.UTIL_StrToIntDef(ed_CurrentDecel_OffsetMaxDistance.Text, 0);
            ems_DriveParam_CTRL.ParamItemsRec.CurrentLowSpeedDistance = (UInt16)Global_Class.UTIL_StrToIntDef(ed_CurrentLowSpeedDistance.Text, 0);


            if (!isFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A4, ems_DriveParam_CTRL);
            }
        }

        #endregion

        private unsafe void btn_Drive_OpSpeed_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "운영속도로 초기화 하시겠습니까? "))
            {
                ems_DriveParam_CTRL.CtrlFlag[0] = 0x02;
                ems_DriveParam_CTRL.CtrlFlag[1] = 0x00;

                ems_DriveParam_CTRL.CtrlFlag[2] = 0x00;
                ems_DriveParam_CTRL.CtrlFlag[3] = 0x00;

                ems_DriveParam_CTRL.CtrlFlag[4] = 0x00;

                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A4, ems_DriveParam_CTRL);

            }
        }

        private unsafe void btn_Drive_TestSpeed_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "시운전속도로 초기화 하시겠습니까? "))
            {
                ems_DriveParam_CTRL.CtrlFlag[0] = 0x01;
                ems_DriveParam_CTRL.CtrlFlag[1] = 0x00;

                ems_DriveParam_CTRL.CtrlFlag[2] = 0x00;
                ems_DriveParam_CTRL.CtrlFlag[3] = 0x00;

                ems_DriveParam_CTRL.CtrlFlag[4] = 0x00;

                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A4, ems_DriveParam_CTRL);

            }
        }

    }
}

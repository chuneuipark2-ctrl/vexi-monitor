using System;
using System.IO;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_RTVFeedParam_Speed : Form 
    {
        
        public Form_Main form_Main;
        public static VEXI_DEFS.TRTV_FeedParamRes rtv_FeedParam_RES;
        public static VEXI_DEFS.TRTV_FeedParamCTRL rtv_FeedParam_CTRL;
        
        public Form_RTVFeedParam_Speed()
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
            toolTip1.SetToolTip(ed_Feed_ManualOp_TokeAlarm, "설정 범위 : 0.0 ~ 200.0%");
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
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A7, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_REC_PARAMReq)));
        }

        private void btn_Param_Set_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "피딩 설정을 장치에 다운로드 하시겠습니까 ? "))
            {
                Do_Ctrl(false);
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
                form_Main.RTV_ToTalFile.Write_RTV_FeedParamCTRL(rtv_FeedParam_CTRL);
            }
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
                if (form_Main.RTV_ToTalFile.Read_RTV_FeedParamCTRL(ref rtv_FeedParam_CTRL))
                {
                    rtv_FeedParam_RES = rtv_FeedParam_CTRL.ParamItemsRec;

                    Display_RTV_Param();
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
            fixed (VEXI_DEFS.TRTV_FeedParamCTRL* TmpPtr = &rtv_FeedParam_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)TmpPtr, Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_FeedParamCTRL)));
            }

            //제어플래그
            //전체 항목 제어 이기 때문에 전체 설정 플래그 1개만 세워도 되고 모든 제어 Flag를 세워도 된다. 
            rtv_FeedParam_CTRL.CtrlFlag[0] = 0x00;
            rtv_FeedParam_CTRL.CtrlFlag[1] = 0xFF;
            rtv_FeedParam_CTRL.CtrlFlag[2] = 0x07;
            rtv_FeedParam_CTRL.CtrlFlag[3] = 0x03;

            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Auto_High.Speed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Feed_AutoHighSpeed_Speed.Text, 0) * 10);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Auto_High.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_AutoHighSpeed_Accel.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Auto_High.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_AutoHighSpeed_Decel.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Auto_High.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_AutoHighSpeed_AJerk.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Auto_High.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_AutoHighSpeed_DJerk.Text, 1);

            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Auto_Middle.Speed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Feed_AutoMiddleSpeed_Speed.Text, 0) * 10);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Auto_Middle.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_AutoMiddleSpeed_Accel.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Auto_Middle.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_AutoMiddleSpeed_Decel.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Auto_Middle.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_AutoMiddleSpeed_Ajerk.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Auto_Middle.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_AutoMiddleSpeed_Djerk.Text, 1);

            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Auto_Low.Speed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Feed_AutoLowSpeed_Speed.Text, 0) * 10);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Auto_Low.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_AutoLowSpeed_Accel.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Auto_Low.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_AutoLowSpeed_Decel.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Auto_Low.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_AutoLowSpeed_Ajerk.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Auto_Low.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_AutoLowSpeed_Djerk.Text, 1);

            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Manual_Middle.Speed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Feed_ManualMiddleSpeed_Speed.Text, 0) * 10);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Manual_Middle.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_ManualMiddleSpeed_Accel.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Manual_Middle.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_ManualMiddleSpeed_Decel.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Manual_Middle.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_ManualMiddleSpeed_AJerk.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Manual_Middle.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_ManualMiddleSpeed_DJerk.Text, 1);

            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Manual_Low.Speed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Feed_ManualLowSpeed_Speed.Text, 0) * 10);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Manual_Low.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_ManualLowSpeed_Accel.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Manual_Low.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_ManualLowSpeed_Decel.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Manual_Low.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_ManualLowSpeed_AJerk.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Manual_Low.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_ManualLowSpeed_DJerk.Text, 1);

            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Force.Speed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Feed_ForceMode_Speed.Text, 0) * 10);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Force.Accel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_ForceMode_Accel.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Force.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_ForceMode_Decel.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Force.A_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_ForceMode_AJerk.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Force.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_ForceMode_DJerk.Text, 1);


            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Emergency.Decel = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_Emergency_Decel.Text, 1);
            rtv_FeedParam_CTRL.ParamItemsRec.Speed_Emergency.D_jerk = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_Emergency_DJerk.Text, 1);

            rtv_FeedParam_CTRL.ParamItemsRec.MAX_RPM = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_MAX_RPM.Text, 0);
            rtv_FeedParam_CTRL.ParamItemsRec.CALC_MPM = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_CALC_MPM.Text, 0);
            rtv_FeedParam_CTRL.ParamItemsRec.CALC_RPM = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Feed_CALC_RPM.Text, 0);
            if (cb_Feed1_MotorDirection.SelectedIndex >= 0) rtv_FeedParam_CTRL.ParamItemsRec.Feed1_MotorDirection = (byte)cb_Feed1_MotorDirection.SelectedIndex;
            if (cb_Feed2_MotorDirection.SelectedIndex >= 0) rtv_FeedParam_CTRL.ParamItemsRec.Feed2_MotorDirection = (byte)cb_Feed2_MotorDirection.SelectedIndex;

            rtv_FeedParam_CTRL.ParamItemsRec.ManualOp_TokeAlarm = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Feed_ManualOp_TokeAlarm.Text, 0) * 10);
            rtv_FeedParam_CTRL.ParamItemsRec.breakOpenContinueTime = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Feed_breakOpenContinueTime.Text, 0) * 100);
            rtv_FeedParam_CTRL.ParamItemsRec.InvertorGain = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Feed_InvertorGain.Text, 0) * 100);


            if (!isFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A8, rtv_FeedParam_CTRL);
            }
        }

        public void Display_Init()
        {
            ed_Feed_AutoHighSpeed_Speed.Text = "0.0";
            ed_Feed_AutoHighSpeed_Accel.Text = "0";
            ed_Feed_AutoHighSpeed_Decel.Text = "0";
            ed_Feed_AutoHighSpeed_AJerk.Text = "0";
            ed_Feed_AutoHighSpeed_DJerk.Text = "0";

            ed_Feed_AutoMiddleSpeed_Speed.Text = "0.0";
            ed_Feed_AutoMiddleSpeed_Accel.Text = "0";
            ed_Feed_AutoMiddleSpeed_Decel.Text = "0";
            ed_Feed_AutoMiddleSpeed_Ajerk.Text = "0";
            ed_Feed_AutoMiddleSpeed_Djerk.Text = "0";

            ed_Feed_AutoLowSpeed_Speed.Text = "0.0";
            ed_Feed_AutoLowSpeed_Accel.Text = "0";
            ed_Feed_AutoLowSpeed_Decel.Text = "0";
            ed_Feed_AutoLowSpeed_Ajerk.Text = "0";
            ed_Feed_AutoLowSpeed_Djerk.Text = "0";

            ed_Feed_ManualMiddleSpeed_Speed.Text = "0.0";
            ed_Feed_ManualMiddleSpeed_Accel.Text = "0";
            ed_Feed_ManualMiddleSpeed_Decel.Text = "0";
            ed_Feed_ManualMiddleSpeed_AJerk.Text = "0";
            ed_Feed_ManualMiddleSpeed_DJerk.Text = "0";

            ed_Feed_ManualLowSpeed_Speed.Text = "0.0";
            ed_Feed_ManualLowSpeed_Accel.Text = "0";
            ed_Feed_ManualLowSpeed_Decel.Text = "0";
            ed_Feed_ManualLowSpeed_AJerk.Text = "0";
            ed_Feed_ManualLowSpeed_DJerk.Text = "0";

            ed_Feed_ForceMode_Speed.Text = "0.0";
            ed_Feed_ForceMode_Accel.Text = "0";
            ed_Feed_ForceMode_Decel.Text = "0";
            ed_Feed_ForceMode_AJerk.Text = "0";
            ed_Feed_ForceMode_DJerk.Text = "0";

            ed_Feed_Emergency_Decel.Text = "0";
            ed_Feed_Emergency_DJerk.Text = "0";

            ed_Feed_MAX_RPM.Text = "0";
            ed_Feed_CALC_MPM.Text = "0";
            ed_Feed_CALC_RPM.Text = "0";
            cb_Feed1_MotorDirection.SelectedIndex = -1;
            cb_Feed2_MotorDirection.SelectedIndex = -1;

            ed_Feed_ManualOp_TokeAlarm.Text = "0";
            ed_Feed_breakOpenContinueTime.Text = "0";
            ed_Feed_InvertorGain.Text = "0";
        }

        public void Display_RTV_Param(byte[] data)
        {
            rtv_FeedParam_RES = (VEXI_DEFS.TRTV_FeedParamRes)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TRTV_FeedParamRes));
            rtv_FeedParam_CTRL.ParamItemsRec = (VEXI_DEFS.TRTV_FeedParamRes)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TRTV_FeedParamRes));
            Display_RTV_Param();
        }

        public unsafe void Display_RTV_Param()
        {
            
            ed_Feed_AutoHighSpeed_Speed.Text = string.Format("{0:0.0}", (double)rtv_FeedParam_RES.Speed_Auto_High.Speed / 10);
            ed_Feed_AutoHighSpeed_Accel.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Auto_High.Accel);
            ed_Feed_AutoHighSpeed_Decel.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Auto_High.Decel);
            ed_Feed_AutoHighSpeed_AJerk.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Auto_High.A_jerk);
            ed_Feed_AutoHighSpeed_DJerk.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Auto_High.D_jerk);

            ed_Feed_AutoMiddleSpeed_Speed.Text = string.Format("{0:0.0}", (double)rtv_FeedParam_RES.Speed_Auto_Middle.Speed / 10);
            ed_Feed_AutoMiddleSpeed_Accel.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Auto_Middle.Accel);
            ed_Feed_AutoMiddleSpeed_Decel.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Auto_Middle.Decel);
            ed_Feed_AutoMiddleSpeed_Ajerk.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Auto_Middle.A_jerk);
            ed_Feed_AutoMiddleSpeed_Djerk.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Auto_Middle.D_jerk);

            ed_Feed_AutoLowSpeed_Speed.Text = string.Format("{0:0.0}", (double)rtv_FeedParam_RES.Speed_Auto_Low.Speed / 10);
            ed_Feed_AutoLowSpeed_Accel.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Auto_Low.Accel);
            ed_Feed_AutoLowSpeed_Decel.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Auto_Low.Decel);
            ed_Feed_AutoLowSpeed_Ajerk.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Auto_Low.A_jerk);
            ed_Feed_AutoLowSpeed_Djerk.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Auto_Low.D_jerk);

            ed_Feed_ManualMiddleSpeed_Speed.Text = string.Format("{0:0.0}", (double)rtv_FeedParam_RES.Speed_Manual_Middle.Speed / 10);
            ed_Feed_ManualMiddleSpeed_Accel.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Manual_Middle.Accel);
            ed_Feed_ManualMiddleSpeed_Decel.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Manual_Middle.Decel);
            ed_Feed_ManualMiddleSpeed_AJerk.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Manual_Middle.A_jerk);
            ed_Feed_ManualMiddleSpeed_DJerk.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Manual_Middle.D_jerk);

            ed_Feed_ManualLowSpeed_Speed.Text = string.Format("{0:0.0}", (double)rtv_FeedParam_RES.Speed_Manual_Low.Speed / 10);
            ed_Feed_ManualLowSpeed_Accel.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Manual_Low.Accel);
            ed_Feed_ManualLowSpeed_Decel.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Manual_Low.Decel);
            ed_Feed_ManualLowSpeed_AJerk.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Manual_Low.A_jerk);

            ed_Feed_ForceMode_Speed.Text = string.Format("{0:0.0}", (double)rtv_FeedParam_RES.Speed_Force.Speed / 10);
            ed_Feed_ForceMode_Accel.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Force.Accel);
            ed_Feed_ForceMode_Decel.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Force.Decel);
            ed_Feed_ForceMode_AJerk.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Force.A_jerk);
            ed_Feed_ForceMode_DJerk.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Force.D_jerk);

            ed_Feed_Emergency_Decel.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Emergency.Decel);
            ed_Feed_Emergency_DJerk.Text = string.Format("{0}", rtv_FeedParam_RES.Speed_Emergency.D_jerk);

            cb_Feed1_MotorDirection.SelectedIndex = Math.Min(rtv_FeedParam_RES.Feed1_MotorDirection, cb_Feed1_MotorDirection.Items.Count - 1);
            cb_Feed2_MotorDirection.SelectedIndex = Math.Min(rtv_FeedParam_RES.Feed2_MotorDirection, cb_Feed2_MotorDirection.Items.Count - 1);
            ed_Feed_MAX_RPM.Text = string.Format("{0}", rtv_FeedParam_RES.MAX_RPM);
            ed_Feed_CALC_MPM.Text = string.Format("{0}", rtv_FeedParam_RES.CALC_MPM);
            ed_Feed_CALC_RPM.Text = string.Format("{0}", rtv_FeedParam_RES.CALC_RPM);

            ed_Feed_ManualOp_TokeAlarm.Text = string.Format("{0:0.0}", (double)rtv_FeedParam_RES.ManualOp_TokeAlarm / 10);
            ed_Feed_breakOpenContinueTime.Text = string.Format("{0:0.00}", (double)rtv_FeedParam_RES.breakOpenContinueTime / 100);
            ed_Feed_InvertorGain.Text = string.Format("{0:0.00}", (double)rtv_FeedParam_RES.InvertorGain / 100);
            

            btn_Param_Set.Enabled = ((form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.DevMode & 0x08) != 0);
        }

        #endregion

        private unsafe void btn_Drive_OpSpeed_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "운영속도로 초기화 하시겠습니까? "))
            {
                rtv_FeedParam_CTRL.CtrlFlag[0] = 0x02;
                rtv_FeedParam_CTRL.CtrlFlag[1] = 0x00;

                rtv_FeedParam_CTRL.CtrlFlag[2] = 0x00;
                rtv_FeedParam_CTRL.CtrlFlag[3] = 0x00;

                rtv_FeedParam_CTRL.CtrlFlag[4] = 0x00;

                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A8, rtv_FeedParam_CTRL);

            }
        }

        private unsafe void btn_Fork_TestSpeed_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "시운전속도로 초기화 하시겠습니까? "))
            {
                rtv_FeedParam_CTRL.CtrlFlag[0] = 0x01;
                rtv_FeedParam_CTRL.CtrlFlag[1] = 0x00;

                rtv_FeedParam_CTRL.CtrlFlag[2] = 0x00;
                rtv_FeedParam_CTRL.CtrlFlag[3] = 0x00;

                rtv_FeedParam_CTRL.CtrlFlag[4] = 0x00;

                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A8, rtv_FeedParam_CTRL);

            }

        }

    }
}

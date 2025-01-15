using System;
using System.IO;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_RTVParam_CTRL : Form
    {

        public Form_Main form_Main;
        public static VEXI_DEFS.TRTV_CTRLParamRes rtv_CtrlParam_RES;
        public static VEXI_DEFS.TRTV_CTRLParamCTRL rtv_CtrlParam_CTRL;


        public Form_RTVParam_CTRL()
        {
            InitializeComponent();
        }

        #region 컴포넌트 이벤트
        private void Form_Param_CTRL_Load(object sender, EventArgs e)
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

            btn_CtrlParam_Set.Enabled = false;


            //Tip을 넣고 싶은 경우 아래와 같이 넣어준다
            toolTip1.SetToolTip(cb_AlarmUse_LimitOver, "순환구조(루프형)일때는 무효항목");
            toolTip1.SetToolTip(cb_DriveRef_Type, "순환구조(루프형)일때는 무효항목");
            toolTip1.SetToolTip(ed_DriveRef_Offset, "순환구조(루프형)일때는 무효항목");
            toolTip1.SetToolTip(ed_ChangeMC_DelayTime, "모터 절체 타입인 경우에만 유효");
            toolTip1.SetToolTip(ed_InvertorOn_DelayTime, "모터 절체 타입인 경우에만 유효");
            toolTip1.SetToolTip(ed_AutoOP_TimeOut_Move, "0 으로 제어 시 자동해제 안함");
            toolTip1.SetToolTip(ed_Buzzer_Error_Time, "0 으로 제어 시 자동 OFF 하지 않음");
            toolTip1.SetToolTip(ed_Buzzer_Warnning_Time, "0 으로 제어 시 자동 OFF 하지 않음");
            toolTip1.SetToolTip(ed_Buzzer_AutoMode_RepeatCount, "0 으로 제어시 자동모드 변경시 부저울림 없음");
        }


        private void ed_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox ed = sender as TextBox;

            //TAG 값을 설정하여 마이너스, 소수점 입력 가능/불가능을 처리

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
            } else if (ed.Tag.ToString() == "11") // 마이너스 ON, 소수점 ON
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
            } else if (ed.Tag.ToString() == "01") // 마이너스 OFF, 소수점 ON
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
            } else
            {
                e.Handled = false;
            }
                
        }
        private void btn_CtrlParam_Load_Click(object sender, EventArgs e)
        {
            Display_Init();
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A1, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_REC_PARAMReq)));
        }

        private void btn_CtrlParam_Set_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "제어 설정을 장치에 다운로드 하시겠습니까 ? "))
            {
                Do_Ctrl(false);
            }
        }

        private void btn_SaveTotalFile_Click(object sender, EventArgs e)
        {
            //설정 파일 저장 (통합설정 파일)
            //제어로직을 통해 제어값을 만들어내서 제어구조체를 바이너리 형식으로 저장한다.
            Do_Ctrl(true);

            saveFileDialog1.Filter = "*.RTVcfg|*.RTVCFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.RTV_ToTalFile.FileName = saveFileDialog1.FileName;
                form_Main.RTV_ToTalFile.Write_RTV_CTRL_PARAM(rtv_CtrlParam_CTRL);
            }
        }

        private void btn_LoadTotalFile_Click(object sender, EventArgs e)
        {
            //설정 파일에서 값 로딩 (통합설정 파일)
            //통합파일에서 해당영역을 제어구조체로 가져온 후에 상태구조체에 넘겨준다.
            //상태구조체와 제어구조체가 동일한 경우 상태구조체 변수로 로딩하여도 된다
            openFileDialog1.Filter = "*.RTVcfg|*.RTVCFG";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.RTV_ToTalFile.FileName = openFileDialog1.FileName;
                if (form_Main.RTV_ToTalFile.Read_RTV_CTRL_PARAM(ref rtv_CtrlParam_CTRL))
                {
                    rtv_CtrlParam_RES = rtv_CtrlParam_CTRL.ParamItemsRec;

                    //화면에 값을 표시해준다.
                    Display_RTV_CtrlParam();
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
            fixed (VEXI_DEFS.TRTV_CTRLParamCTRL* TmpPtr = &rtv_CtrlParam_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)TmpPtr, Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_CTRLParamCTRL)));
            }

            //제어플래그
            //한 화면에 모든 항목을 구성하였기 때문에 모든 항목에 제어플래그를 세워준다
            //전체제어 플래그 1bit 만 세워서 처리하는 것도 방법임
            //(화면이 나눠지는 경우에는 해당 화면에 있는 항목들에 대해서만 제어가 되도록 해당하는 항목들에 대한 제어플래그만 세워주거나
            //상태구조체의 값을 제어구조체에 담은 후에 전체 항목 플래그를 세우고 현재 화면에 해당하는 항목들에 대해서는 상태구조체 값이 아닌 화면값으로 값을 갱신해주는 방법도 있다.)
            rtv_CtrlParam_CTRL.CtrlFlag[0] = 0x00;
            rtv_CtrlParam_CTRL.CtrlFlag[1] = 0x1F;
            rtv_CtrlParam_CTRL.CtrlFlag[2] = 0x0F;
            rtv_CtrlParam_CTRL.CtrlFlag[3] = 0x07;

            //제어값
            if (cb_SafetyPlug_ProcessType.SelectedIndex >= 0) rtv_CtrlParam_CTRL.ParamItemsRec.SafetyPlug_ProcessType = (byte) cb_SafetyPlug_ProcessType.SelectedIndex ;
            if (cb_Collision_ProcessType.SelectedIndex >= 0) rtv_CtrlParam_CTRL.ParamItemsRec.Collision_ProcessType = (byte)cb_Collision_ProcessType.SelectedIndex;
            if (cb_AlarmUse_OpticModem.SelectedIndex >= 0) rtv_CtrlParam_CTRL.ParamItemsRec.AlarmUse_OpticModem = (byte)cb_AlarmUse_OpticModem.SelectedIndex;
            if (cb_AlarmUse_LimitOver.SelectedIndex >= 0) rtv_CtrlParam_CTRL.ParamItemsRec.AlarmUse_LimitOver = (byte)cb_AlarmUse_LimitOver.SelectedIndex;
            if (cb_DriveRef_Type.SelectedIndex >= 0) rtv_CtrlParam_CTRL.ParamItemsRec.DriveRef_Type = (byte)cb_DriveRef_Type.SelectedIndex;
            rtv_CtrlParam_CTRL.ParamItemsRec.DriveRef_Offset = (UInt16)Global_Class.UTIL_StrToIntDef(ed_DriveRef_Offset.Text, 0);
            if (cb_DeSpeedArea_Type.SelectedIndex >= 0) rtv_CtrlParam_CTRL.ParamItemsRec.DeSpeedArea_Type = (byte)cb_DeSpeedArea_Type.SelectedIndex;
            rtv_CtrlParam_CTRL.ParamItemsRec.DeSpeedArea_Offset = (UInt16)Global_Class.UTIL_StrToIntDef(ed_DeSpeedArea_Offset.Text, 0);
            if (cb_AlarmUse_OverItemOnFeeding.SelectedIndex >= 0) rtv_CtrlParam_CTRL.ParamItemsRec.AlarmUse_OverItemOnFeeding = (byte)cb_AlarmUse_OverItemOnFeeding.SelectedIndex;
            if (cb_AlarmUse_InterlcokTimeOut.SelectedIndex >= 0) rtv_CtrlParam_CTRL.ParamItemsRec.AlarmUse_InterlcokTimeOut = (byte)cb_AlarmUse_InterlcokTimeOut.SelectedIndex;
            rtv_CtrlParam_CTRL.ParamItemsRec.InPosition_Offset = (byte)Global_Class.UTIL_StrToIntDef(ed_InPosition_Offset.Text, 0);
            rtv_CtrlParam_CTRL.ParamItemsRec.InPosition_Hist = (byte)Global_Class.UTIL_StrToIntDef(ed_InPosition_Hist.Text, 0);
            rtv_CtrlParam_CTRL.ParamItemsRec.RetryInPosition_Count = (byte)Global_Class.UTIL_StrToIntDef(ed_RetryInPosition_Count.Text, 0);
            rtv_CtrlParam_CTRL.ParamItemsRec.RetryInPosition_Range = (byte)Global_Class.UTIL_StrToIntDef(ed_RetryInPosition_Range.Text, 0);
            rtv_CtrlParam_CTRL.ParamItemsRec.CripRange = (UInt16)Global_Class.UTIL_StrToIntDef(ed_CripRange.Text, 0);
            rtv_CtrlParam_CTRL.ParamItemsRec.CurrentDecel_OffsetTime = (byte)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_CurrentDecel_OffsetTime.Text, 0) * 10);
            rtv_CtrlParam_CTRL.ParamItemsRec.CurrentDecel_OffsetMaxDistance = (UInt16)Global_Class.UTIL_StrToIntDef(ed_CurrentDecel_OffsetMaxDistance.Text, 0);
            rtv_CtrlParam_CTRL.ParamItemsRec.CurrentLowSpeedDistance = (UInt16)Global_Class.UTIL_StrToIntDef(ed_CurrentLowSpeedDistance.Text, 0);

            rtv_CtrlParam_CTRL.ParamItemsRec.ChangeMC_DelayTime = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_ChangeMC_DelayTime.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.InvertorOn_DelayTime = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_InvertorOn_DelayTime.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_beforMove = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_beforMove.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_afterMove = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_afterMove.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_beforFeed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_beforFeed.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_afterFeed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_afterFeed.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_Done = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_Done.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_beforMove = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_beforMove.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_afterMove = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_afterMove.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_beforFeed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_beforFeed.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_afterFeed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_afterFeed.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_Done = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_Done.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.OP_TimeOut_ManualCtrl = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_OP_TimeOut_ManualCtrl.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_Move = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_Move.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_Interlock = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_Interlock.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_LoadFeed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_LoadFeed.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_UnLoadFeed = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_UnLoadFeed.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_ItemCheckLoad = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_ItemCheckLoad.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_ItemCheckUnLoad = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_ItemCheckUnLoad.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_Crip = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_Crip.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.AutoInit_ForceMode = (byte)Global_Class.UTIL_StrToIntDef(ed_AutoInit_ForceMode.Text, 0);
            rtv_CtrlParam_CTRL.ParamItemsRec.WifiControllerTime = (UInt16)Global_Class.UTIL_StrToIntDef(ed_WifiControllerTime.Text, 0);
            rtv_CtrlParam_CTRL.ParamItemsRec.Buzzer_Error_Time = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Buzzer_Error_Time.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.Buzzer_Warnning_Time = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Buzzer_Warnning_Time.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.Buzzer_AutoModeOn_Time = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Buzzer_AutoModeOn_Time.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.Buzzer_AutoModeOff_Time = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Buzzer_AutoModeOff_Time.Text, 0) * 100);
            rtv_CtrlParam_CTRL.ParamItemsRec.Buzzer_AutoMode_RepeatCount = (byte)Global_Class.UTIL_StrToIntDef(ed_Buzzer_AutoMode_RepeatCount.Text, 0);

            //isFileSave 를 True로 해서 호출하는 경우는 제어를 통신으로 내보낼 목적은 없고 제어구조체를 구성해서 파일에 저장할 목적이 있는 것이므로
            //통신으로 데이터를 내보내는 부분은 수행되지 않도록  (!isFileSave) 조건을 걸어준다
            if (!isFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A2, rtv_CtrlParam_CTRL);
            }
        }

        public void Display_Init()
        {
            cb_SafetyPlug_ProcessType.SelectedIndex = -1;
            cb_Collision_ProcessType.SelectedIndex = -1;

            cb_AlarmUse_OpticModem.SelectedIndex = -1;
            cb_AlarmUse_LimitOver.SelectedIndex = -1;
            cb_DriveRef_Type.SelectedIndex = -1;
            ed_DriveRef_Offset.Text = "0";
            cb_DeSpeedArea_Type.SelectedIndex = -1;
            ed_DeSpeedArea_Offset.Text = "0";
            cb_AlarmUse_OverItemOnFeeding.SelectedIndex = -1;
            cb_AlarmUse_InterlcokTimeOut.SelectedIndex = -1;

            ed_InPosition_Offset.Text = "0";
            ed_InPosition_Hist.Text = "0";
            ed_RetryInPosition_Count.Text = "0";
            ed_RetryInPosition_Range.Text = "0";
            ed_CripRange.Text = "0";
            ed_CurrentDecel_OffsetTime.Text = "0";
            ed_CurrentDecel_OffsetMaxDistance.Text = "0";
            ed_CurrentLowSpeedDistance.Text = "0";

            ed_ChangeMC_DelayTime.Text = "0";
            ed_InvertorOn_DelayTime.Text = "0";

            ed_Loading_DelayTime_beforMove.Text = "0";
            ed_Loading_DelayTime_afterMove.Text = "0";
            ed_Loading_DelayTime_beforFeed.Text = "0";
            ed_Loading_DelayTime_afterFeed.Text = "0";
            ed_Loading_DelayTime_Done.Text = "0";

            ed_UnLoading_DelayTime_beforMove.Text = "0";
            ed_UnLoading_DelayTime_afterMove.Text = "0";
            ed_UnLoading_DelayTime_beforFeed.Text = "0";
            ed_UnLoading_DelayTime_afterFeed.Text = "0";
            ed_UnLoading_DelayTime_Done.Text = "0";

            ed_OP_TimeOut_ManualCtrl.Text = "0";

            ed_AutoOP_TimeOut_Move.Text = "0";
            ed_AutoOP_TimeOut_Interlock.Text = "0";
            ed_AutoOP_TimeOut_LoadFeed.Text = "0";
            ed_AutoOP_TimeOut_UnLoadFeed.Text = "0";
            ed_AutoOP_TimeOut_ItemCheckLoad.Text = "0";
            ed_AutoOP_TimeOut_ItemCheckUnLoad.Text = "0";
            ed_AutoOP_TimeOut_Crip.Text = "0";

            ed_AutoInit_ForceMode.Text = "0";
            ed_WifiControllerTime.Text = "0";

            ed_Buzzer_Error_Time.Text = "0";
            ed_Buzzer_Warnning_Time.Text = "0";
            ed_Buzzer_AutoModeOn_Time.Text = "0";
            ed_Buzzer_AutoModeOff_Time.Text = "0";
            ed_Buzzer_AutoMode_RepeatCount.Text = "0";
        }

        public void Display_RTV_CtrlParam(byte[] data)
        {
            rtv_CtrlParam_RES = (VEXI_DEFS.TRTV_CTRLParamRes)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TRTV_CTRLParamRes));
            rtv_CtrlParam_CTRL.ParamItemsRec = (VEXI_DEFS.TRTV_CTRLParamRes)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TRTV_CTRLParamRes));

            Display_RTV_CtrlParam();
        }

        public void Display_RTV_CtrlParam()
        {
            cb_SafetyPlug_ProcessType.SelectedIndex = Math.Min(rtv_CtrlParam_RES.SafetyPlug_ProcessType, cb_SafetyPlug_ProcessType.Items.Count - 1);
            cb_Collision_ProcessType.SelectedIndex = Math.Min(rtv_CtrlParam_RES.Collision_ProcessType, cb_Collision_ProcessType.Items.Count - 1);
            cb_AlarmUse_OpticModem.SelectedIndex = Math.Min(rtv_CtrlParam_RES.AlarmUse_OpticModem, cb_AlarmUse_OpticModem.Items.Count - 1);
            cb_AlarmUse_LimitOver.SelectedIndex = Math.Min(rtv_CtrlParam_RES.AlarmUse_LimitOver, cb_AlarmUse_LimitOver.Items.Count - 1);
            cb_DriveRef_Type.SelectedIndex = Math.Min(rtv_CtrlParam_RES.DriveRef_Type, cb_DriveRef_Type.Items.Count - 1);
            ed_DriveRef_Offset.Text = string.Format("{0}", rtv_CtrlParam_RES.DriveRef_Offset);
            cb_DeSpeedArea_Type.SelectedIndex = Math.Min(rtv_CtrlParam_RES.DeSpeedArea_Type, cb_DeSpeedArea_Type.Items.Count - 1);
            ed_DeSpeedArea_Offset.Text = string.Format("{0}", rtv_CtrlParam_RES.DeSpeedArea_Offset);
            cb_AlarmUse_OverItemOnFeeding.SelectedIndex = Math.Min(rtv_CtrlParam_RES.AlarmUse_OverItemOnFeeding, cb_AlarmUse_OverItemOnFeeding.Items.Count - 1);
            cb_AlarmUse_InterlcokTimeOut.SelectedIndex = Math.Min(rtv_CtrlParam_RES.AlarmUse_InterlcokTimeOut, cb_AlarmUse_InterlcokTimeOut.Items.Count - 1);
            ed_InPosition_Offset.Text = string.Format("{0}", rtv_CtrlParam_RES.InPosition_Offset);
            ed_InPosition_Hist.Text = string.Format("{0}", rtv_CtrlParam_RES.InPosition_Hist);
            ed_RetryInPosition_Count.Text = string.Format("{0}", rtv_CtrlParam_RES.RetryInPosition_Count);
            ed_RetryInPosition_Range.Text = string.Format("{0}", rtv_CtrlParam_RES.RetryInPosition_Range);
            ed_CripRange.Text = string.Format("{0}", rtv_CtrlParam_RES.CripRange);
            ed_CurrentDecel_OffsetTime.Text = string.Format("{0:0.0}", (double)rtv_CtrlParam_RES.CurrentDecel_OffsetTime / 10);
            ed_CurrentDecel_OffsetMaxDistance.Text = string.Format("{0}", rtv_CtrlParam_RES.CurrentDecel_OffsetMaxDistance);
            ed_CurrentLowSpeedDistance.Text = string.Format("{0}", rtv_CtrlParam_RES.CurrentLowSpeedDistance);

            ed_ChangeMC_DelayTime.Text   = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.ChangeMC_DelayTime / 100);
            ed_InvertorOn_DelayTime.Text = string.Format("{0:0.00}", (double) rtv_CtrlParam_RES.InvertorOn_DelayTime / 100);
            ed_Loading_DelayTime_beforMove.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.Loading_DelayTime_beforMove / 100);
            ed_Loading_DelayTime_afterMove.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.Loading_DelayTime_afterMove / 100);
            ed_Loading_DelayTime_beforFeed.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.Loading_DelayTime_beforFeed / 100);
            ed_Loading_DelayTime_afterFeed.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.Loading_DelayTime_afterFeed / 100);
            ed_Loading_DelayTime_Done.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.Loading_DelayTime_Done / 100);
            ed_UnLoading_DelayTime_beforMove.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.UnLoading_DelayTime_beforMove / 100);
            ed_UnLoading_DelayTime_afterMove.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.UnLoading_DelayTime_afterMove / 100);
            ed_UnLoading_DelayTime_beforFeed.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.UnLoading_DelayTime_beforFeed / 100);
            ed_UnLoading_DelayTime_afterFeed.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.UnLoading_DelayTime_afterFeed / 100);
            ed_UnLoading_DelayTime_Done.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.UnLoading_DelayTime_Done / 100);
            ed_OP_TimeOut_ManualCtrl.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.OP_TimeOut_ManualCtrl / 100);
            ed_AutoOP_TimeOut_Move.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.AutoOP_TimeOut_Move / 100);
            ed_AutoOP_TimeOut_Interlock.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.AutoOP_TimeOut_Interlock / 100);
            ed_AutoOP_TimeOut_LoadFeed.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.AutoOP_TimeOut_LoadFeed / 100);
            ed_AutoOP_TimeOut_UnLoadFeed.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.AutoOP_TimeOut_UnLoadFeed / 100);
            ed_AutoOP_TimeOut_ItemCheckLoad.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.AutoOP_TimeOut_ItemCheckLoad / 100);
            ed_AutoOP_TimeOut_ItemCheckUnLoad.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.AutoOP_TimeOut_ItemCheckUnLoad / 100);
            ed_AutoOP_TimeOut_Crip.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.AutoOP_TimeOut_Crip / 100);
            ed_AutoInit_ForceMode.Text = string.Format("{0}", rtv_CtrlParam_RES.AutoInit_ForceMode);
            ed_WifiControllerTime.Text = string.Format("{0}", rtv_CtrlParam_RES.WifiControllerTime);
            ed_Buzzer_Error_Time.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.Buzzer_Error_Time / 100);
            ed_Buzzer_Warnning_Time.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.Buzzer_Warnning_Time / 100);
            ed_Buzzer_AutoModeOn_Time.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.Buzzer_AutoModeOn_Time / 100);
            ed_Buzzer_AutoModeOff_Time.Text = string.Format("{0:0.00}", (double)rtv_CtrlParam_RES.Buzzer_AutoModeOff_Time / 100);
            ed_Buzzer_AutoMode_RepeatCount.Text = string.Format("{0}", rtv_CtrlParam_RES.Buzzer_AutoMode_RepeatCount);

            //장치에 설정은 셋업모드인 경우에만 가능하게 하려고 조건을 건다.
            //해당 제한이 없어진다면 여기서 Enabled를 true로 해주면 된다.
            btn_CtrlParam_Set.Enabled = ((form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.DevMode & 0x08) != 0);
            //btn_CtrlParam_Set.Enabled = true;
        }
        #endregion

    }
}

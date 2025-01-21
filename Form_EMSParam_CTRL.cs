using System;
using System.IO;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_EMSParam_CTRL : Form
    {

        public Form_Main form_Main;
        public static VEXI_DEFS.TEMS_CTRLParamRes ems_CtrlParam_RES;
        public static VEXI_DEFS.TEMS_CTRLParamCTRL ems_CtrlParam_CTRL;


        public Form_EMSParam_CTRL()
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
            toolTip1.SetToolTip(cb_AlarmUse_DriveLimitSetErr, "순환구조(루프형)일때는 무효항목");
            toolTip1.SetToolTip(ed_AutoHome_WaitTime, "자동 홈 복귀 사용 설정 시 언로딩 완료 후, 명령 대기시간까지 작업명령이 없는 경우 자동 홈 복귀 수행");
            toolTip1.SetToolTip(cb_Chucking_LiftBreakSet, "자동모드 Chuck 동작중 승강브레이크 사용 설정");
            toolTip1.SetToolTip(ed_AutoLiftHome_RefRunCount, "자동모드 반송동작 완료 횟수가 설정된 값 이상일 경우, 원점 복귀 실행.");
            toolTip1.SetToolTip(cb_AlarmUse_InterlockErr, "자동모드 스테이션 로딩, 언로딩시, 인터락이 OFF 되면 알람 발생.");
            toolTip1.SetToolTip(cb_AlarmUse_InterlcokTimeOut, "자동모드시 적용");
            toolTip1.SetToolTip(ed_Time_Release_ForceMode, "설정된 시간 동안 수동조작이 없을 경우 자동으로 해제");
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
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A1, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_REC_PARAMReq)));
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

            saveFileDialog1.Filter = "*.EMScfg|*.EMSCFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.EMS_ToTalFile.FileName = saveFileDialog1.FileName;
                form_Main.EMS_ToTalFile.Write_EMS_CTRL_PARAM(ems_CtrlParam_CTRL);
            }
        }

        private void btn_LoadTotalFile_Click(object sender, EventArgs e)
        {
            //설정 파일에서 값 로딩 (통합설정 파일)
            //통합파일에서 해당영역을 제어구조체로 가져온 후에 상태구조체에 넘겨준다.
            //상태구조체와 제어구조체가 동일한 경우 상태구조체 변수로 로딩하여도 된다
            openFileDialog1.Filter = "*.EMScfg|*.EMSCFG";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.EMS_ToTalFile.FileName = openFileDialog1.FileName;
                if (form_Main.EMS_ToTalFile.Read_EMS_CTRL_PARAM(ref ems_CtrlParam_CTRL))
                {
                    ems_CtrlParam_RES = ems_CtrlParam_CTRL.ParamItemsRec;

                    //화면에 값을 표시해준다.
                    Display_EMS_CtrlParam();
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
            cb_SafetyPlug_ProcessType.SelectedIndex = -1;
            cb_Collision_ProcessType.SelectedIndex = -1;

            cb_AutoHome_Use.SelectedIndex = -1;
            ed_AutoHome_Position.Text = "0";
            ed_AutoHome_WaitTime.Text = "0.0";

            cb_Chucking_LiftBreakSet.SelectedIndex = -1;
            ed_AutoLiftHome_RefRunCount.Text = "0";

            cb_AlarmUse_DriveLimitSetErr.SelectedIndex = -1;
            cb_AlarmUse_InterlockErr.SelectedIndex = -1;
            cb_AlarmUse_InterlcokTimeOut.SelectedIndex = -1;

            ed_ChangeTime_InterlockToMotor.Text = "0.00";
            ed_ChangeTime_MotorToInterlock.Text = "0.00";

            ed_Loading_DelayTime_beforMove.Text = "0.00";
            ed_Loading_DelayTime_afterMove.Text = "0.00";
            ed_Loading_DelayTime_beforLiftDown.Text = "0.00";
            ed_Loading_DelayTime_afterLiftDown.Text = "0.00";
            ed_Loading_DelayTime_beforCatch.Text = "0.00";
            ed_Loading_DelayTime_afterCatch.Text = "0.00";
            ed_Loading_DelayTime_beforLiftUp.Text = "0.00";
            ed_Loading_DelayTime_afterLiftUp.Text = "0.00";
            ed_Loading_DelayTime_WorkDone.Text = "0.00";

            ed_UnLoading_DelayTime_beforMove.Text = "0.00";
            ed_UnLoading_DelayTime_afterMove.Text = "0.00";
            ed_UnLoading_DelayTime_beforLiftDown.Text = "0.00";
            ed_UnLoading_DelayTime_afterLiftDown.Text = "0.00";
            ed_UnLoading_DelayTime_beforUnCatch.Text = "0.00";
            ed_UnLoading_DelayTime_afterUnCatch.Text = "0.00";
            ed_UnLoading_DelayTime_beforLiftUp.Text = "0.00";
            ed_UnLoading_DelayTime_afterLiftUp.Text = "0.00";
            ed_UnLoading_DelayTime_WorkDone.Text = "0.00";

            ed_TimeOut_Set_LiftRef.Text = "0.00";
            ed_TimeOut_Set_ChuckRef.Text = "0.00";

            ed_TimeOut_ManualCmdRx.Text = "0.00";

            ed_AutoOP_TimeOut_Move.Text = "0.00";
            ed_AutoOP_TimeOut_LiftDown.Text = "0.00";
            ed_AutoOP_TimeOut_LiftUp.Text = "0.00";
            ed_AutoOP_TimeOut_LoadInterlock.Text = "0.00";
            ed_AutoOP_TimeOut_UnLoadInterlock.Text = "0.00";
            ed_AutoOP_TimeOut_Load.Text = "0.00";
            ed_AutoOP_TimeOut_UnLoad.Text = "0.00";
            ed_AutoOP_TimeOut_LoadingItemDectect.Text = "0.00";
            ed_AutoOP_TimeOut_UnLoadingItemDectect.Text = "0.00";
            ed_AutoOP_TimeOut_MoveCrip.Text = "0.00";
            ed_AutoOP_TimeOut_LiftCrip.Text = "0.00";

            ed_Time_Release_ForceMode.Text = "0";
            ed_Time_Polling_GMC.Text = "0";

            ed_Buzzer_Error_Time.Text = "0.00";
            ed_Buzzer_Warnning_Time.Text = "0.00";
            ed_Buzzer_AutoModeOn_Time.Text = "0.00";
            ed_Buzzer_AutoModeOff_Time.Text = "0.00";
            ed_Buzzer_AutoMode_RepeatCount.Text = "0";
        }

        public void Display_EMS_CtrlParam(byte[] data)
        {
            ems_CtrlParam_RES = (VEXI_DEFS.TEMS_CTRLParamRes)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TEMS_CTRLParamRes));
            ems_CtrlParam_CTRL.ParamItemsRec = (VEXI_DEFS.TEMS_CTRLParamRes)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TEMS_CTRLParamRes));

            Display_EMS_CtrlParam();
        }

        public void Display_EMS_CtrlParam()
        {
            cb_SafetyPlug_ProcessType.SelectedIndex = Math.Min(ems_CtrlParam_RES.SafetyPlug_ProcessType, cb_SafetyPlug_ProcessType.Items.Count - 1);
            cb_Collision_ProcessType.SelectedIndex = Math.Min(ems_CtrlParam_RES.Collision_ProcessType, cb_Collision_ProcessType.Items.Count - 1);

            cb_AutoHome_Use.SelectedIndex = Math.Min(ems_CtrlParam_RES.AutoHome_Use, cb_AutoHome_Use.Items.Count - 1);
            ed_AutoHome_Position.Text = string.Format("{0}", ems_CtrlParam_RES.AutoHome_Position);
            ed_AutoHome_WaitTime.Text = string.Format("{0:0.0}", (double)ems_CtrlParam_RES.AutoHome_WaitTime / 10);

            cb_Chucking_LiftBreakSet.SelectedIndex = Math.Min(ems_CtrlParam_RES.Chucking_LiftBreakSet, cb_Chucking_LiftBreakSet.Items.Count - 1);
            ed_AutoLiftHome_RefRunCount.Text = string.Format("{0}", ems_CtrlParam_RES.AutoLiftHome_RefRunCount);

            cb_AlarmUse_DriveLimitSetErr.SelectedIndex = Math.Min(ems_CtrlParam_RES.AlarmUse_DriveLimitSetErr, cb_AlarmUse_DriveLimitSetErr.Items.Count - 1);
            cb_AlarmUse_InterlockErr.SelectedIndex = Math.Min(ems_CtrlParam_RES.AlarmUse_InterlockErr, cb_AlarmUse_InterlockErr.Items.Count - 1);
            cb_AlarmUse_InterlcokTimeOut.SelectedIndex = Math.Min(ems_CtrlParam_RES.AlarmUse_InterlcokTimeOut, cb_AlarmUse_InterlcokTimeOut.Items.Count - 1);

            ed_ChangeTime_InterlockToMotor.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.ChangeTime_InterlockToMotor / 100);
            ed_ChangeTime_MotorToInterlock.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.ChangeTime_MotorToInterlock / 100);

            ed_Loading_DelayTime_beforMove.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.Loading_DelayTime_beforMove / 100);
            ed_Loading_DelayTime_afterMove.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.Loading_DelayTime_afterMove / 100);
            ed_Loading_DelayTime_beforLiftDown.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.Loading_DelayTime_beforLiftDown / 100);
            ed_Loading_DelayTime_afterLiftDown.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.Loading_DelayTime_afterLiftDown / 100);
            ed_Loading_DelayTime_beforCatch.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.Loading_DelayTime_beforCatch / 100);
            ed_Loading_DelayTime_afterCatch.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.Loading_DelayTime_afterCatch / 100);
            ed_Loading_DelayTime_beforLiftUp.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.Loading_DelayTime_beforLiftUp / 100);
            ed_Loading_DelayTime_afterLiftUp.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.Loading_DelayTime_afterLiftUp / 100);
            ed_Loading_DelayTime_WorkDone.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.Loading_DelayTime_WorkDone / 100);

            ed_UnLoading_DelayTime_beforMove.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.UnLoading_DelayTime_beforMove / 100);
            ed_UnLoading_DelayTime_afterMove.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.UnLoading_DelayTime_afterMove / 100);
            ed_UnLoading_DelayTime_beforLiftDown.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.UnLoading_DelayTime_beforLiftDown / 100);
            ed_UnLoading_DelayTime_afterLiftDown.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.UnLoading_DelayTime_afterLiftDown / 100);
            ed_UnLoading_DelayTime_beforUnCatch.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.UnLoading_DelayTime_beforUnCatch / 100);
            ed_UnLoading_DelayTime_afterUnCatch.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.UnLoading_DelayTime_afterUnCatch / 100);
            ed_UnLoading_DelayTime_beforLiftUp.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.UnLoading_DelayTime_beforLiftUp / 100);
            ed_UnLoading_DelayTime_afterLiftUp.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.UnLoading_DelayTime_afterLiftUp / 100);
            ed_UnLoading_DelayTime_WorkDone.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.UnLoading_DelayTime_WorkDone / 100); ;

            ed_TimeOut_Set_LiftRef.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.TimeOut_Set_LiftRef / 100);
            ed_TimeOut_Set_ChuckRef.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.TimeOut_Set_ChuckRef / 100);

            ed_TimeOut_ManualCmdRx.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.TimeOut_ManualCmdRx / 100);

            ed_AutoOP_TimeOut_Move.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.AutoOP_TimeOut_Move / 100);
            ed_AutoOP_TimeOut_LiftDown.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.AutoOP_TimeOut_LiftDown / 100);
            ed_AutoOP_TimeOut_LiftUp.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.AutoOP_TimeOut_LiftUp / 100);
            ed_AutoOP_TimeOut_LoadInterlock.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.AutoOP_TimeOut_LoadInterlock / 100);
            ed_AutoOP_TimeOut_UnLoadInterlock.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.AutoOP_TimeOut_UnLoadInterlock / 100);
            ed_AutoOP_TimeOut_Load.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.AutoOP_TimeOut_Load / 100);
            ed_AutoOP_TimeOut_UnLoad.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.AutoOP_TimeOut_UnLoad / 100);
            ed_AutoOP_TimeOut_LoadingItemDectect.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.AutoOP_TimeOut_LoadingItemDectect / 100);
            ed_AutoOP_TimeOut_UnLoadingItemDectect.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.AutoOP_TimeOut_UnLoadingItemDectect / 100);
            ed_AutoOP_TimeOut_MoveCrip.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.AutoOP_TimeOut_MoveCrip / 100);
            ed_AutoOP_TimeOut_LiftCrip.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.AutoOP_TimeOut_LiftCrip / 100);

            ed_Time_Release_ForceMode.Text = string.Format("{0}", ems_CtrlParam_RES.Time_Release_ForceMode);
            ed_Time_Polling_GMC.Text = string.Format("{0}", ems_CtrlParam_RES.Time_Polling_GMC);

            ed_Buzzer_Error_Time.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.Buzzer_Error_Time / 100);
            ed_Buzzer_Warnning_Time.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.Buzzer_Warnning_Time / 100);
            ed_Buzzer_AutoModeOn_Time.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.Buzzer_AutoModeOn_Time / 100);
            ed_Buzzer_AutoModeOff_Time.Text = string.Format("{0:0.00}", (double)ems_CtrlParam_RES.Buzzer_AutoModeOff_Time / 100);
            ed_Buzzer_AutoMode_RepeatCount.Text = string.Format("{0}", ems_CtrlParam_RES.Buzzer_AutoMode_RepeatCount);

            //장치에 설정은 셋업모드인 경우에만 가능하게 하려고 조건을 건다.
            //해당 제한이 없어진다면 여기서 Enabled를 true로 해주면 된다.
            btn_CtrlParam_Set.Enabled = ((form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.DevMode & 0x08) != 0);
            //btn_CtrlParam_Set.Enabled = true;
        }

        private unsafe void Do_Ctrl(bool isFileSave)
        {
            fixed (VEXI_DEFS.TEMS_CTRLParamCTRL* TmpPtr = &ems_CtrlParam_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)TmpPtr, Marshal.SizeOf(typeof(VEXI_DEFS.TEMS_CTRLParamCTRL)));
            }

            //제어플래그
            //한 화면에 모든 항목을 구성하였기 때문에 모든 항목에 제어플래그를 세워준다
            //전체제어 플래그 1bit 만 세워서 처리하는 것도 방법임
            //(화면이 나눠지는 경우에는 해당 화면에 있는 항목들에 대해서만 제어가 되도록 해당하는 항목들에 대한 제어플래그만 세워주거나
            //상태구조체의 값을 제어구조체에 담은 후에 전체 항목 플래그를 세우고 현재 화면에 해당하는 항목들에 대해서는 상태구조체 값이 아닌 화면값으로 값을 갱신해주는 방법도 있다.)
            ems_CtrlParam_CTRL.CtrlFlag[0] = 0x00;
            ems_CtrlParam_CTRL.CtrlFlag[1] = 0x3F;
            ems_CtrlParam_CTRL.CtrlFlag[2] = 0x0F;
            ems_CtrlParam_CTRL.CtrlFlag[3] = 0x07;

            //제어값
            if (cb_SafetyPlug_ProcessType.SelectedIndex >= 0) ems_CtrlParam_CTRL.ParamItemsRec.SafetyPlug_ProcessType = (byte)cb_SafetyPlug_ProcessType.SelectedIndex;
            if (cb_Collision_ProcessType.SelectedIndex >= 0) ems_CtrlParam_CTRL.ParamItemsRec.Collision_ProcessType = (byte)cb_Collision_ProcessType.SelectedIndex;

            if (cb_AutoHome_Use.SelectedIndex >= 0) ems_CtrlParam_CTRL.ParamItemsRec.AutoHome_Use = (byte)cb_AutoHome_Use.SelectedIndex;
            ems_CtrlParam_CTRL.ParamItemsRec.AutoHome_Position = (UInt16)Global_Class.UTIL_StrToIntDef(ed_AutoHome_Position.Text, 0);
            ems_CtrlParam_CTRL.ParamItemsRec.AutoHome_WaitTime = (byte)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoHome_WaitTime.Text, 0) * 10);

            if (cb_Chucking_LiftBreakSet.SelectedIndex >= 0) ems_CtrlParam_CTRL.ParamItemsRec.Chucking_LiftBreakSet = (byte)cb_Chucking_LiftBreakSet.SelectedIndex;
            ems_CtrlParam_CTRL.ParamItemsRec.AutoLiftHome_RefRunCount = (byte)Global_Class.UTIL_StrToIntDef(ed_AutoLiftHome_RefRunCount.Text, 0);

            if (cb_AlarmUse_DriveLimitSetErr.SelectedIndex >= 0) ems_CtrlParam_CTRL.ParamItemsRec.AlarmUse_DriveLimitSetErr = (byte)cb_AlarmUse_DriveLimitSetErr.SelectedIndex;
            if (cb_AlarmUse_InterlockErr.SelectedIndex >= 0) ems_CtrlParam_CTRL.ParamItemsRec.AlarmUse_InterlockErr = (byte)cb_AlarmUse_InterlockErr.SelectedIndex;
            if (cb_AlarmUse_InterlcokTimeOut.SelectedIndex >= 0) ems_CtrlParam_CTRL.ParamItemsRec.AlarmUse_InterlcokTimeOut = (byte)cb_AlarmUse_InterlcokTimeOut.SelectedIndex;

            ems_CtrlParam_CTRL.ParamItemsRec.ChangeTime_InterlockToMotor = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_ChangeTime_InterlockToMotor.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.ChangeTime_MotorToInterlock = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_ChangeTime_MotorToInterlock.Text, 0) * 100);

            ems_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_beforMove = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_beforMove.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_afterMove = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_afterMove.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_beforLiftDown = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_beforLiftDown.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_afterLiftDown = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_afterLiftDown.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_beforCatch = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_beforCatch.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_afterCatch = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_afterCatch.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_beforLiftUp = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_beforLiftUp.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_afterLiftUp = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_afterLiftUp.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_WorkDone = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_WorkDone.Text, 0) * 100);

            ems_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_beforMove = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_beforMove.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_afterMove = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_afterMove.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_beforLiftDown = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_beforLiftDown.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_afterLiftDown = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_afterLiftDown.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_beforUnCatch = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_beforUnCatch.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_afterUnCatch = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_afterUnCatch.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_beforLiftUp = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_beforLiftUp.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_afterLiftUp = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_afterLiftUp.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_WorkDone = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_WorkDone.Text, 0) * 100);

            ems_CtrlParam_CTRL.ParamItemsRec.TimeOut_Set_LiftRef = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_TimeOut_Set_LiftRef.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.TimeOut_Set_ChuckRef = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_TimeOut_Set_ChuckRef.Text, 0) * 100);

            ems_CtrlParam_CTRL.ParamItemsRec.TimeOut_ManualCmdRx = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_TimeOut_ManualCmdRx.Text, 0) * 100);


            ems_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_Move = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_Move.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_LiftDown = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_LiftDown.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_LiftUp = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_LiftUp.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_LoadInterlock = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_LoadInterlock.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_UnLoadInterlock = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_UnLoadInterlock.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_Load = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_Load.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_UnLoad = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_UnLoad.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_LoadingItemDectect = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_LoadingItemDectect.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_UnLoadingItemDectect = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_UnLoadingItemDectect.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_MoveCrip = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_MoveCrip.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_LiftCrip = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_LiftCrip.Text, 0) * 100);


            ems_CtrlParam_CTRL.ParamItemsRec.Time_Release_ForceMode = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Time_Release_ForceMode.Text, 0);
            ems_CtrlParam_CTRL.ParamItemsRec.Time_Polling_GMC = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Time_Polling_GMC.Text, 0);

            ems_CtrlParam_CTRL.ParamItemsRec.Buzzer_Error_Time = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Buzzer_Error_Time.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.Buzzer_Warnning_Time = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Buzzer_Warnning_Time.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.Buzzer_AutoModeOn_Time = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Buzzer_AutoModeOn_Time.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.Buzzer_AutoModeOff_Time = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Buzzer_AutoModeOff_Time.Text, 0) * 100);
            ems_CtrlParam_CTRL.ParamItemsRec.Buzzer_AutoMode_RepeatCount = (byte)Global_Class.UTIL_StrToIntDef(ed_Buzzer_AutoMode_RepeatCount.Text, 0);

            //isFileSave 를 True로 해서 호출하는 경우는 제어를 통신으로 내보낼 목적은 없고 제어구조체를 구성해서 파일에 저장할 목적이 있는 것이므로
            //통신으로 데이터를 내보내는 부분은 수행되지 않도록  (!isFileSave) 조건을 걸어준다
            if (!isFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A2, ems_CtrlParam_CTRL);
            }
        }


        #endregion

    }
}

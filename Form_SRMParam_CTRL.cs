using System;
using System.IO;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_SRMParam_CTRL : Form
    {

        public Form_Main form_Main;
        public static VEXI_DEFS.TSRM_CTRLParamRes srm_CtrlParam_RES;
        public static VEXI_DEFS.TSRM_CTRLParamCTRL srm_CtrlParam_CTRL;

        private static TextBox[] LvDelay_DelayTime_TextBox;
        private static TextBox[] LvDelay_StartLv_TextBox;
        private static TextBox[] LvDelay_EndLv_TextBox;

        public Form_SRMParam_CTRL()
        {
            InitializeComponent();

            LvDelay_DelayTime_TextBox = new TextBox[] { ed_LvDelay_1_Time, ed_LvDelay_2_Time, ed_LvDelay_3_Time, ed_LvDelay_4_Time, ed_LvDelay_5_Time, ed_LvDelay_6_Time, ed_LvDelay_7_Time, ed_LvDelay_8_Time, ed_LvDelay_9_Time, ed_LvDelay_10_Time};
            LvDelay_StartLv_TextBox = new TextBox[] { ed_LvDelay_1_StartLv, ed_LvDelay_2_StartLv, ed_LvDelay_3_StartLv, ed_LvDelay_4_StartLv, ed_LvDelay_5_StartLv, ed_LvDelay_6_StartLv, ed_LvDelay_7_StartLv, ed_LvDelay_8_StartLv, ed_LvDelay_9_StartLv, ed_LvDelay_10_StartLv };
            LvDelay_EndLv_TextBox = new TextBox[] { ed_LvDelay_1_EndLv, ed_LvDelay_2_EndLv, ed_LvDelay_3_EndLv, ed_LvDelay_4_EndLv, ed_LvDelay_5_EndLv, ed_LvDelay_6_EndLv, ed_LvDelay_7_EndLv, ed_LvDelay_8_EndLv, ed_LvDelay_9_EndLv, ed_LvDelay_10_EndLv };
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
            toolTip1.SetToolTip(ed_Forking_ReturnRef_OperCount, "미사용시 0 으로 제어");
            toolTip1.SetToolTip(ed_Setup_TimeOut_DriveRef, "타임아웃 미적용시 0 으로 제어");
            toolTip1.SetToolTip(ed_Setup_TimeOut_LiftRef, "타임아웃 미적용시 0 으로 제어");
            toolTip1.SetToolTip(ed_Setup_TimeOut_ForkRef, "타임아웃 미적용시 0 으로 제어");
            toolTip1.SetToolTip(ed_OP_TimeOut_ManualCtrl, "타임아웃 미적용시 0 으로 제어");
            toolTip1.SetToolTip(ed_OP_TimeOut_GoHome, "타임아웃 미적용시 0 으로 제어");
            toolTip1.SetToolTip(ed_AutoOP_TimeOut_Move, "타임아웃 미적용시 0 으로 제어");
            toolTip1.SetToolTip(ed_AutoOP_TimeOut_ForkOut, "타임아웃 미적용시 0 으로 제어");
            toolTip1.SetToolTip(ed_AutoOP_TimeOut_ForkUpDown, "타임아웃 미적용시 0 으로 제어");
            toolTip1.SetToolTip(ed_AutoOP_TimeOut_ForkIn, "타임아웃 미적용시 0 으로 제어");
            toolTip1.SetToolTip(ed_AutoOP_TimeOut_InterLock, "타임아웃 미적용시 0 으로 제어");
            toolTip1.SetToolTip(ed_AutoOP_TimeOut_ItemLoadUnLoad, "타임아웃 미적용시 0 으로 제어");
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
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A1, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_PARAMReq)));
        }

        private void btn_CtrlParam_Set_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "제어 설정을 장치에 다운로드 하시겠습니까 ? "))
            {
                Do_Ctrl(false);
            }
        }

        //통합파일에 저장하는 것으로 변경
        //개별파일에 저장하는 소스는 남겨놓음
        private void btn_Param_FileWrite_Click(object sender, EventArgs e)
        {
            Do_Ctrl(true);

            saveFileDialog1.Filter = "*.Ctrl_Param|*.CTRL_PARAM";

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
                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CTRLParamCTRL))];
                        Global_Class.UTIL_StructObjectToByteArray(srm_CtrlParam_CTRL, Savebytes);
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
            //설정 파일 저장 (통합설정 파일)
            //제어로직을 통해 제어값을 만들어내서 제어구조체를 바이너리 형식으로 저장한다.
            Do_Ctrl(true);

            saveFileDialog1.Filter = "*.SRMcfg|*.SRMCFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
                form_Main.SRM_ToTalFile.Write_SRM_CTRL_PARAM(srm_CtrlParam_CTRL);
            }
        }


        //통합파일에서 불러오는 것으로 변경
        //개별파일에서 불러오는 소스는 남겨놓음
        private void btn_Param_FileRead_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.Ctrl_Param|*.CTRL_PARAM";

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
                        ushort Len = (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CTRLParamCTRL));
                        if (br.BaseStream.Length == Len)
                        {
                            Savebytes = br.ReadBytes(Len);
                            srm_CtrlParam_CTRL = (VEXI_DEFS.TSRM_CTRLParamCTRL)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_CTRLParamCTRL));
                            srm_CtrlParam_RES = (VEXI_DEFS.TSRM_CTRLParamRes)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_CTRLParamRes), 34, Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CTRLParamRes)));

                            //화면에 값을 표시해준다.
                            Display_SRM_CtrlParam();

                        } else
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
            //설정 파일에서 값 로딩 (통합설정 파일)
            //통합파일에서 해당영역을 제어구조체로 가져온 후에 상태구조체에 넘겨준다.
            //상태구조체와 제어구조체가 동일한 경우 상태구조체 변수로 로딩하여도 된다
            openFileDialog1.Filter = "*.SRMcfg|*.SRMCFG";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
                if (form_Main.SRM_ToTalFile.Read_SRM_CTRL_PARAM(ref srm_CtrlParam_CTRL))
                {
                    srm_CtrlParam_RES = srm_CtrlParam_CTRL.ParamItemsRec;

                    Display_SRM_CtrlParam();
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
            //제어 구조체 Clear
            fixed (VEXI_DEFS.TSRM_CTRLParamCTRL* TmpPtr = &srm_CtrlParam_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)TmpPtr, Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_CTRLParamCTRL)));
            }

            //제어플래그
            //한 화면에 모든 항목을 구성하였기 때문에 모든 항목에 제어플래그를 세워준다
            //전체제어 플래그 1bit 만 세워서 처리하는 것도 방법임
            //(화면이 나눠지는 경우에는 해당 화면에 있는 항목들에 대해서만 제어가 되도록 해당하는 항목들에 대한 제어플래그만 세워주거나
            //상태구조체의 값을 제어구조체에 담은 후에 전체 항목 플래그를 세우고 현재 화면에 해당하는 항목들에 대해서는 상태구조체 값이 아닌 화면값으로 값을 갱신해주는 방법도 있다.)
            srm_CtrlParam_CTRL.CtrlFlag[0] = 0x00;
            srm_CtrlParam_CTRL.CtrlFlag[1] = 0xFF;
            srm_CtrlParam_CTRL.CtrlFlag[2] = 0x3F;
            srm_CtrlParam_CTRL.CtrlFlag[3] = 0x7F;
            srm_CtrlParam_CTRL.CtrlFlag[4] = 0x1F;

            //제어값
            if (cb_SafetyPlug_ProcessType.SelectedIndex >= 0) srm_CtrlParam_CTRL.ParamItemsRec.SafetyPlug_ProcessType = (byte) cb_SafetyPlug_ProcessType.SelectedIndex ;
            if (cb_USE_GisangDoorSense.SelectedIndex >= 0) srm_CtrlParam_CTRL.ParamItemsRec.USE_GisangDoorSense =  (byte) cb_USE_GisangDoorSense.SelectedIndex        ;
            if (cb_AlarmUse_GisangDoorSense.SelectedIndex >= 0) srm_CtrlParam_CTRL.ParamItemsRec.AlarmUse_GisangDoorSense = (byte)cb_AlarmUse_GisangDoorSense.SelectedIndex   ;
            if (cb_AlarmUse_OpticModem.SelectedIndex >= 0) srm_CtrlParam_CTRL.ParamItemsRec.AlarmUse_OpticModem = (byte)cb_AlarmUse_OpticModem.SelectedIndex        ;
            if (cb_AutoModeChangeCheck.SelectedIndex >= 0) srm_CtrlParam_CTRL.ParamItemsRec.AutoModeChangeCheck = (byte)cb_AutoModeChangeCheck.SelectedIndex        ;
            if (cb_DriveLift_Sequence.SelectedIndex >= 0) srm_CtrlParam_CTRL.ParamItemsRec.DriveLift_Sequence = (byte)cb_DriveLift_Sequence.SelectedIndex         ;
            if (cb_NoItemToHome.SelectedIndex >= 0) srm_CtrlParam_CTRL.ParamItemsRec.NoItemToHome = (byte)cb_NoItemToHome.SelectedIndex               ;
            if (cb_AlarmUse_ital_forking.SelectedIndex >= 0) srm_CtrlParam_CTRL.ParamItemsRec.AlarmUse_ital_forking = (byte)cb_AlarmUse_ital_forking.SelectedIndex      ;

            //if (cb_AlarmUse_StartOff.SelectedIndex >= 0) srm_CtrlParam_CTRL.ParamItemsRec.AlarmUse_StartOff = (byte)cb_AlarmUse_StartOff.SelectedIndex;

            if (cb_interlockAlarm_Auto.SelectedIndex >= 0) srm_CtrlParam_CTRL.ParamItemsRec.interlockAlarm_Auto = (byte)cb_interlockAlarm_Auto.SelectedIndex;
            if (cb_interlockAlarm_Manual.SelectedIndex >= 0) srm_CtrlParam_CTRL.ParamItemsRec.interlockAlarm_Manual = (byte)cb_interlockAlarm_Manual.SelectedIndex ;
            
            if (cb_Forking_lift_BrakeOn_UseFlag.SelectedIndex >= 0) srm_CtrlParam_CTRL.ParamItemsRec.Forking_lift_BrakeOn_UseFlag = (byte)cb_Forking_lift_BrakeOn_UseFlag.SelectedIndex;
            srm_CtrlParam_CTRL.ParamItemsRec.Forking_ReturnRef_OperCount = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Forking_ReturnRef_OperCount.Text, 0);
            srm_CtrlParam_CTRL.ParamItemsRec.RetryInPositon_DriveCount = (byte)Global_Class.UTIL_StrToIntDef(ed_RetryInPositon_DriveCount.Text, 0);
            srm_CtrlParam_CTRL.ParamItemsRec.RetryInPositon_DriveOffset = (UInt16)Global_Class.UTIL_StrToIntDef(ed_RetryInPositon_DriveOffset.Text, 0);
            srm_CtrlParam_CTRL.ParamItemsRec.RetryInPositon_LiftCount = (byte)Global_Class.UTIL_StrToIntDef(ed_RetryInPositon_LiftCount.Text, 0);
            srm_CtrlParam_CTRL.ParamItemsRec.RetryInPositon_LiftOffset = (UInt16)Global_Class.UTIL_StrToIntDef(ed_RetryInPositon_LiftOffset.Text, 0);
            srm_CtrlParam_CTRL.ParamItemsRec.RetryInPositon_ForkCount = (byte)Global_Class.UTIL_StrToIntDef(ed_RetryInPositon_ForkCount.Text, 0);
            srm_CtrlParam_CTRL.ParamItemsRec.RetryInPositon_ForkOffset = (UInt16)Global_Class.UTIL_StrToIntDef(ed_RetryInPositon_ForkOffset.Text, 0);
            srm_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_beforMove = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_beforMove.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_afterMove = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_afterMove.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_beforForkOut = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_beforForkOut.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_afterForkOut = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_afterForkOut.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_beforForkUpDown = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_beforForkUpDown.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_afterForkUpDown = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_afterForkUpDown.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_beforForkIn = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_beforForkIn.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.Loading_DelayTime_afterForkIn = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Loading_DelayTime_afterForkIn.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_beforMove = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_beforMove.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_afterMove = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_afterMove.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_beforForkOut = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_beforForkOut.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_afterForkOut = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_afterForkOut.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_beforForkUpDown = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_beforForkUpDown.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_afterForkUpDown = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_afterForkUpDown.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_beforForkIn = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_beforForkIn.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.UnLoading_DelayTime_afterForkIn = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_UnLoading_DelayTime_afterForkIn.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.Fork_Ref_DelayTime = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Fork_Ref_DelayTime.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.Setup_TimeOut_DriveRef = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Setup_TimeOut_DriveRef.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.Setup_TimeOut_LiftRef = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Setup_TimeOut_LiftRef.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.Setup_TimeOut_ForkRef = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Setup_TimeOut_ForkRef.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.OP_TimeOut_ManualCtrl = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_OP_TimeOut_ManualCtrl.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.OP_TimeOut_GoHome = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_OP_TimeOut_GoHome.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_Move = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_Move.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_ForkOut = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_ForkOut.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_ForkUpDown = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_ForkUpDown.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_ForkIn = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_ForkIn.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_InterLock = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_InterLock.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.AutoOP_TimeOut_ItemLoadUnLoad = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_AutoOP_TimeOut_ItemLoadUnLoad.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.AutoInit_ForceMode = (UInt16)Global_Class.UTIL_StrToIntDef(ed_AutoInit_ForceMode.Text, 0);
            srm_CtrlParam_CTRL.ParamItemsRec.Fan_DoTime = (UInt16)Global_Class.UTIL_StrToIntDef(ed_Fan_DoTime.Text, 0);
            if (cb_Fan_IsTempSensor.SelectedIndex >=0) srm_CtrlParam_CTRL.ParamItemsRec.Fan_IsTempSensor = (byte) cb_Fan_IsTempSensor.SelectedIndex;
            srm_CtrlParam_CTRL.ParamItemsRec.Buzzer_Error_Time = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Buzzer_Error_Time.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.Buzzer_Warnning_Time = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Buzzer_Warnning_Time.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.Buzzer_AutoModeOn_Time = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Buzzer_AutoModeOn_Time.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.Buzzer_AutoModeOff_Time = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(ed_Buzzer_AutoModeOff_Time.Text, 0) * 100);
            srm_CtrlParam_CTRL.ParamItemsRec.Buzzer_AutoMode_RepeatCount = (byte)Global_Class.UTIL_StrToIntDef(ed_Buzzer_AutoMode_RepeatCount.Text, 0);
            //if (cb_AlarmUse_PosSensor_AutoDrive.SelectedIndex >= 0) srm_CtrlParam_CTRL.ParamItemsRec.AlarmUse_PosSensor_AutoDrive = (byte)cb_AlarmUse_PosSensor_AutoDrive.SelectedIndex;

            byte TmpCount = 0;
            fixed (VEXI_DEFS.TSRMLevelDelayTimeRec* Ptr_1 = &srm_CtrlParam_CTRL.ParamItemsRec.LevelDelay_1_St)
            {
                for (byte i = 0; i < 10; i++)
                {
                    (Ptr_1 + i)->DelayTime = (byte)Math.Round(Global_Class.UTIL_StrToFloatDef(LvDelay_DelayTime_TextBox[i].Text, 0) * 10);
                    (Ptr_1 + i)->Start = (byte)Global_Class.UTIL_StrToIntDef(LvDelay_StartLv_TextBox[i].Text, 0);
                    (Ptr_1 + i)->End = (byte)Global_Class.UTIL_StrToIntDef(LvDelay_EndLv_TextBox[i].Text, 0);

                    if (((Ptr_1 + i)->Start != 0) && ((Ptr_1 + i)->End != 0))
                    {
                        TmpCount++;
                    } else
                    {
                        (Ptr_1 + i)->DelayTime = 0;
                        (Ptr_1 + i)->Start = 0;
                        (Ptr_1 + i)->End = 0;
                    }
                }
            }
            srm_CtrlParam_CTRL.ParamItemsRec.DelayTimeCount = TmpCount;


            //isFileSave 를 True로 해서 호출하는 경우는 제어를 통신으로 내보낼 목적은 없고 제어구조체를 구성해서 파일에 저장할 목적이 있는 것이므로
            //통신으로 데이터를 내보내는 부분은 수행되지 않도록  (!isFileSave) 조건을 걸어준다
            if (!isFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_A2, srm_CtrlParam_CTRL);
            }
        }

        public void Display_Init()
        {
            cb_SafetyPlug_ProcessType.SelectedIndex = -1;
            cb_USE_GisangDoorSense.SelectedIndex = -1;
            cb_AlarmUse_GisangDoorSense.SelectedIndex = -1;
            cb_AlarmUse_OpticModem.SelectedIndex = -1;
            cb_AutoModeChangeCheck.SelectedIndex = -1;
            cb_DriveLift_Sequence.SelectedIndex = -1;
            cb_NoItemToHome.SelectedIndex = -1;
            cb_AlarmUse_ital_forking.SelectedIndex = -1;

            //cb_AlarmUse_StartOff.SelectedIndex = -1;

            cb_interlockAlarm_Manual.SelectedIndex = -1;
            cb_interlockAlarm_Auto.SelectedIndex = -1;
            cb_Forking_lift_BrakeOn_UseFlag.SelectedIndex = -1;
            ed_Forking_ReturnRef_OperCount.Text = "0";
            


            ed_RetryInPositon_DriveCount.Text = "0";
            ed_RetryInPositon_DriveOffset.Text = "0";
            ed_RetryInPositon_LiftCount.Text = "0";
            ed_RetryInPositon_LiftOffset.Text = "0";
            ed_RetryInPositon_ForkCount.Text = "0";
            ed_RetryInPositon_ForkOffset.Text = "0";

            ed_Loading_DelayTime_beforMove.Text = "0.00";
            ed_Loading_DelayTime_afterMove.Text = "0.00";
            ed_Loading_DelayTime_beforForkOut.Text = "0.00";
            ed_Loading_DelayTime_afterForkOut.Text = "0.00";
            ed_Loading_DelayTime_beforForkUpDown.Text = "0.00";
            ed_Loading_DelayTime_afterForkUpDown.Text = "0.00";
            ed_Loading_DelayTime_beforForkIn.Text = "0.00";
            ed_Loading_DelayTime_afterForkIn.Text = "0.00";

            ed_UnLoading_DelayTime_beforMove.Text = "0.00";
            ed_UnLoading_DelayTime_afterMove.Text = "0.00";
            ed_UnLoading_DelayTime_beforForkOut.Text = "0.00";
            ed_UnLoading_DelayTime_afterForkOut.Text = "0.00";
            ed_UnLoading_DelayTime_beforForkUpDown.Text = "0.00";
            ed_UnLoading_DelayTime_afterForkUpDown.Text = "0.00";
            ed_UnLoading_DelayTime_beforForkIn.Text = "0.00";
            ed_UnLoading_DelayTime_afterForkIn.Text = "0.00";

            ed_Fork_Ref_DelayTime.Text = "0.00";

            ed_Setup_TimeOut_DriveRef.Text = "0.00";
            ed_Setup_TimeOut_LiftRef.Text = "0.00";
            ed_Setup_TimeOut_ForkRef.Text = "0.00";

            ed_OP_TimeOut_ManualCtrl.Text = "0.00";
            ed_OP_TimeOut_GoHome.Text = "0.00";

            ed_AutoOP_TimeOut_Move.Text = "0.00";
            ed_AutoOP_TimeOut_ForkOut.Text = "0.00";
            ed_AutoOP_TimeOut_ForkUpDown.Text = "0.00";
            ed_AutoOP_TimeOut_ForkIn.Text = "0.00";
            ed_AutoOP_TimeOut_InterLock.Text = "0.00";
            ed_AutoOP_TimeOut_ItemLoadUnLoad.Text = "0.00";

            ed_AutoInit_ForceMode.Text = "0";

            ed_Fan_DoTime.Text = "0";
            cb_Fan_IsTempSensor.SelectedIndex = -1;

            ed_Buzzer_Error_Time.Text = "0.00";
            ed_Buzzer_Warnning_Time.Text = "0.00";
            ed_Buzzer_AutoModeOn_Time.Text = "0.00"; ;
            ed_Buzzer_AutoModeOff_Time.Text = "0.00";
            ed_Buzzer_AutoMode_RepeatCount.Text = "0";

            //cb_AlarmUse_PosSensor_AutoDrive.SelectedIndex = -1;

            for (byte i = 0; i < 10; i++)
            {
                LvDelay_DelayTime_TextBox[i].Text = "0.0";
                LvDelay_StartLv_TextBox[i].Text = "0";
                LvDelay_EndLv_TextBox[i].Text = "0";
            }

        }
        public void Display_SRM_CtrlParam(byte[] data)
        {
            srm_CtrlParam_RES = (VEXI_DEFS.TSRM_CTRLParamRes)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TSRM_CTRLParamRes));
            srm_CtrlParam_CTRL.ParamItemsRec = (VEXI_DEFS.TSRM_CTRLParamRes)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TSRM_CTRLParamRes));

            Display_SRM_CtrlParam();
        }

        public unsafe void Display_SRM_CtrlParam()
        {

            cb_SafetyPlug_ProcessType.SelectedIndex = Math.Min(srm_CtrlParam_RES.SafetyPlug_ProcessType, cb_SafetyPlug_ProcessType.Items.Count - 1);
            cb_USE_GisangDoorSense.SelectedIndex = Math.Min(srm_CtrlParam_RES.USE_GisangDoorSense, cb_USE_GisangDoorSense.Items.Count - 1);
            cb_AlarmUse_GisangDoorSense.SelectedIndex = Math.Min(srm_CtrlParam_RES.AlarmUse_GisangDoorSense, cb_AlarmUse_GisangDoorSense.Items.Count - 1);
            cb_AlarmUse_OpticModem.SelectedIndex = Math.Min(srm_CtrlParam_RES.AlarmUse_OpticModem, cb_AlarmUse_OpticModem.Items.Count - 1);
            cb_AutoModeChangeCheck.SelectedIndex = Math.Min(srm_CtrlParam_RES.AutoModeChangeCheck, cb_AutoModeChangeCheck.Items.Count - 1);
            cb_DriveLift_Sequence.SelectedIndex = Math.Min(srm_CtrlParam_RES.DriveLift_Sequence, cb_DriveLift_Sequence.Items.Count - 1);
            cb_NoItemToHome.SelectedIndex = Math.Min(srm_CtrlParam_RES.NoItemToHome, cb_NoItemToHome.Items.Count - 1);
            cb_AlarmUse_ital_forking.SelectedIndex = Math.Min(srm_CtrlParam_RES.AlarmUse_ital_forking, cb_AlarmUse_ital_forking.Items.Count - 1);

            //cb_AlarmUse_StartOff.SelectedIndex = Math.Min(srm_CtrlParam_RES.AlarmUse_StartOff, cb_AlarmUse_StartOff.Items.Count - 1);

            cb_interlockAlarm_Auto.SelectedIndex = Math.Min(srm_CtrlParam_RES.interlockAlarm_Auto, cb_interlockAlarm_Auto.Items.Count - 1);
            cb_interlockAlarm_Manual.SelectedIndex = Math.Min(srm_CtrlParam_RES.interlockAlarm_Manual, cb_interlockAlarm_Manual.Items.Count - 1);
            cb_Forking_lift_BrakeOn_UseFlag.SelectedIndex = Math.Min(srm_CtrlParam_RES.Forking_lift_BrakeOn_UseFlag, cb_Forking_lift_BrakeOn_UseFlag.Items.Count - 1);
            ed_Forking_ReturnRef_OperCount.Text = string.Format("{0}", srm_CtrlParam_RES.Forking_ReturnRef_OperCount);
            ed_RetryInPositon_DriveCount.Text = string.Format("{0}", srm_CtrlParam_RES.RetryInPositon_DriveCount);
            ed_RetryInPositon_DriveOffset.Text = string.Format("{0}", srm_CtrlParam_RES.RetryInPositon_DriveOffset);
            ed_RetryInPositon_LiftCount.Text = string.Format("{0}", srm_CtrlParam_RES.RetryInPositon_LiftCount);
            ed_RetryInPositon_LiftOffset.Text = string.Format("{0}", srm_CtrlParam_RES.RetryInPositon_LiftOffset);
            ed_RetryInPositon_ForkCount.Text = string.Format("{0}", srm_CtrlParam_RES.RetryInPositon_ForkCount);
            ed_RetryInPositon_ForkOffset.Text = string.Format("{0}", srm_CtrlParam_RES.RetryInPositon_ForkOffset);
            ed_Loading_DelayTime_beforMove.Text = string.Format("{0:0.00}", (double) srm_CtrlParam_RES.Loading_DelayTime_beforMove / 100);
            ed_Loading_DelayTime_afterMove.Text = string.Format("{0:0.00}", (double) srm_CtrlParam_RES.Loading_DelayTime_afterMove / 100);
            ed_Loading_DelayTime_beforForkOut.Text = string.Format("{0:0.00}", (double) srm_CtrlParam_RES.Loading_DelayTime_beforForkOut / 100);
            ed_Loading_DelayTime_afterForkOut.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.Loading_DelayTime_afterForkOut / 100);
            ed_Loading_DelayTime_beforForkUpDown.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.Loading_DelayTime_beforForkUpDown / 100);
            ed_Loading_DelayTime_afterForkUpDown.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.Loading_DelayTime_afterForkUpDown / 100);
            ed_Loading_DelayTime_beforForkIn.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.Loading_DelayTime_beforForkIn / 100);
            ed_Loading_DelayTime_afterForkIn.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.Loading_DelayTime_afterForkIn / 100);
            ed_UnLoading_DelayTime_beforMove.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.UnLoading_DelayTime_beforMove / 100);
            ed_UnLoading_DelayTime_afterMove.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.UnLoading_DelayTime_afterMove / 100);
            ed_UnLoading_DelayTime_beforForkOut.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.UnLoading_DelayTime_beforForkOut / 100);
            ed_UnLoading_DelayTime_afterForkOut.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.UnLoading_DelayTime_afterForkOut / 100);
            ed_UnLoading_DelayTime_beforForkUpDown.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.UnLoading_DelayTime_beforForkUpDown / 100);
            ed_UnLoading_DelayTime_afterForkUpDown.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.UnLoading_DelayTime_afterForkUpDown / 100);
            ed_UnLoading_DelayTime_beforForkIn.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.UnLoading_DelayTime_beforForkIn / 100);
            ed_UnLoading_DelayTime_afterForkIn.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.UnLoading_DelayTime_afterForkIn / 100);
            ed_Fork_Ref_DelayTime.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.Fork_Ref_DelayTime / 100);
            ed_Setup_TimeOut_DriveRef.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.Setup_TimeOut_DriveRef / 100);
            ed_Setup_TimeOut_LiftRef.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.Setup_TimeOut_LiftRef / 100);
            ed_Setup_TimeOut_ForkRef.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.Setup_TimeOut_ForkRef / 100);
            ed_OP_TimeOut_ManualCtrl.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.OP_TimeOut_ManualCtrl / 100);
            ed_OP_TimeOut_GoHome.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.OP_TimeOut_GoHome / 100);
            ed_AutoOP_TimeOut_Move.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.AutoOP_TimeOut_Move / 100);
            ed_AutoOP_TimeOut_ForkOut.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.AutoOP_TimeOut_ForkOut / 100);
            ed_AutoOP_TimeOut_ForkUpDown.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.AutoOP_TimeOut_ForkUpDown / 100);
            ed_AutoOP_TimeOut_ForkIn.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.AutoOP_TimeOut_ForkIn / 100);
            ed_AutoOP_TimeOut_InterLock.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.AutoOP_TimeOut_InterLock / 100);
            ed_AutoOP_TimeOut_ItemLoadUnLoad.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.AutoOP_TimeOut_ItemLoadUnLoad / 100);
            ed_AutoInit_ForceMode.Text = string.Format("{0}", srm_CtrlParam_RES.AutoInit_ForceMode);
            ed_Fan_DoTime.Text = string.Format("{0}", srm_CtrlParam_RES.Fan_DoTime);
            cb_Fan_IsTempSensor.SelectedIndex = Math.Min(srm_CtrlParam_RES.Fan_IsTempSensor, cb_Fan_IsTempSensor.Items.Count - 1);
            ed_Buzzer_Error_Time.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.Buzzer_Error_Time / 100);
            ed_Buzzer_Warnning_Time.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.Buzzer_Warnning_Time / 100);
            ed_Buzzer_AutoModeOn_Time.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.Buzzer_AutoModeOn_Time / 100);
            ed_Buzzer_AutoModeOff_Time.Text = string.Format("{0:0.00}", (double)srm_CtrlParam_RES.Buzzer_AutoModeOff_Time / 100);
            ed_Buzzer_AutoMode_RepeatCount.Text = string.Format("{0}", srm_CtrlParam_RES.Buzzer_AutoMode_RepeatCount);
            //cb_AlarmUse_PosSensor_AutoDrive.SelectedIndex = Math.Min(srm_CtrlParam_RES.AlarmUse_PosSensor_AutoDrive, cb_AlarmUse_PosSensor_AutoDrive.Items.Count - 1);

            fixed (VEXI_DEFS.TSRMLevelDelayTimeRec* Ptr_1 = &srm_CtrlParam_RES.LevelDelay_1_St)
            {
                for (byte i = 0; i < 10; i++)
                {
                    if (i < srm_CtrlParam_RES.DelayTimeCount)
                    {
                        LvDelay_DelayTime_TextBox[i].Text = string.Format("{0:0.0}", (double)(Ptr_1 + i)->DelayTime / 10);
                        LvDelay_StartLv_TextBox[i].Text = string.Format("{0}", (Ptr_1 + i)->Start);
                        LvDelay_EndLv_TextBox[i].Text = string.Format("{0}", (Ptr_1 + i)->End);
                    } else
                    {
                        LvDelay_DelayTime_TextBox[i].Text = "0.0";
                        LvDelay_StartLv_TextBox[i].Text = "0";
                        LvDelay_EndLv_TextBox[i].Text = "0";
                    }
                }
            }

            //장치에 설정은 셋업모드인 경우에만 가능하게 하려고 조건을 건다.
            //해당 제한이 없어진다면 여기서 Enabled를 true로 해주면 된다.
            btn_CtrlParam_Set.Enabled = ((form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode & 0x08) != 0);
            //btn_CtrlParam_Set.Enabled = true;
        }
        #endregion
    }
}

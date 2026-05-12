using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_EMS_CTL : Form
    {
        public Form_Main form_Main;

        private static VEXI_DEFS.TEMS_REC_JobCTRLRES ems_REC_Job_CTRLRes;
        private static VEXI_DEFS.TDEV_ManualCtrl ems_REC_ManualCtrl;

        private bool _emsManualJogPressArmed;
        private byte _emsManualJogArmedTag;



        public Form_EMS_CTL()
        {
            InitializeComponent();
        }


        #region 컴포넌트 이벤트
        private void Btn_Chucking_Home_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            if (!TryValidateEmsManualMotionPreconditions(out string failMsg))
            {
                form_Main.GlobalObj.MsgBox_Info(failMsg, "W");
                return;
            }

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "Chucking 이동 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                Do_Chucking_Position_Ctrl(Convert.ToByte(bt.Tag.ToString()));
            }
        }

        private void Form_EMS_CTL_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }

            this.Text = "장비 운전 조작(" + tabControl1.SelectedTab.Text + ")";
            Display_DevSt();
        }

        private void Form_EMS_CTL_Deactivate(object sender, EventArgs e)
        {
            _emsManualJogPressArmed = false;

            if ((form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue != 0) &&
                (form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue != 0xFF))
            {
                form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue = 0;
                form_Main.Do_JogCtrl();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = "장비 운전 조작(" + tabControl1.SelectedTab.Text + ")";
        }

        private void btn_UP_LowSpeed_MouseUp(object sender, MouseEventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            byte jogTag = Convert.ToByte(bt.Tag.ToString());

            if (!_emsManualJogPressArmed || _emsManualJogArmedTag != jogTag)
                return;

            _emsManualJogPressArmed = false;

            form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue_before = jogTag;
            form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue = 0;

            form_Main.Do_JogCtrl();
        }


        private void btn_UP_LowSpeed_MouseDown(object sender, MouseEventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            byte jogTag = Convert.ToByte(bt.Tag.ToString());

            if (!TryValidateEmsManualMotionPreconditions(out string failMsg, false, jogTag))
            {
                _emsManualJogPressArmed = false;
                form_Main.GlobalObj.MsgBox_Info(failMsg, "W");
                return;
            }

            _emsManualJogPressArmed = true;
            _emsManualJogArmedTag = jogTag;

            form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue = jogTag;
            form_Main.Do_JogCtrl();
        }

        private void btn_SetRef_Drive_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            if (!TryValidateEmsManualMotionPreconditions(out string failMsg, true))
            {
                form_Main.GlobalObj.MsgBox_Info(failMsg, "W");
                return;
            }

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "원점을 설정하시겠습니까?"))
            {
                form_Main.Do_Ctrl_Cmd_withOnebyte(ConstClass.CMD1_00, ConstClass.CMD2_44, Convert.ToByte(bt.Tag.ToString()));
            }
        }

        private void btn_Dev_StartOn_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "장치의 시작모드 상태를 변경하시겠습니까?"))
            {
                form_Main.Do_Ctrl_Cmd_withOnebyte(ConstClass.CMD1_00, ConstClass.CMD2_50, Convert.ToByte(bt.Tag.ToString()));
            }
        }

        private void btn_Dev_AlarmReset_Click(object sender, EventArgs e)
        {
            form_Main.GlobalObj.MsgBox_Info("장치의 이상을 리셋합니다", "I");

            form_Main.Do_Ctrl_Cmd_withNoData(ConstClass.CMD2_52);

        }

        private void btn_Dev_Home_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "홈위치로 이동 시키시겠습니까?"))
            {
                form_Main.Do_Ctrl_Cmd_withNoData(ConstClass.CMD2_51);
            }
        }

        private void btn_Dev_Maintance_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "보수위치로 이동 시키시겠습니까?"))
            {
                form_Main.Do_Ctrl_Cmd_withNoData(ConstClass.CMD2_59);
            }
        }

        private void btn_DevMode_AutoOn_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "장치의 운영모드를 변경하시겠습니까?"))
            {
                form_Main.Do_Ctrl_DevMode(ConstClass.CMD2_58, Convert.ToByte(bt.Tag.ToString()));
            }
        }

        #endregion

        #region 기능함수

        /// <summary>EMS 수동 조그·Chucking·원점설정 등 공통 전제. Display_EMS_St의 주행/승강 St_2 bit2 = 원점확인과 동일.</summary>
        /// <param name="skipAxisOriginCheck">true이면 주행·승강 원점확인 비트를 검사하지 않음(원점 설정 CMD2_44 등).</param>
        /// <param name="jogTagForOriginCheck">조그 Tag: 11~14 주행만, 21~24 승강만, 그 외(Catch/Chuck 등)는 주행+승강 모두, null은 Chucking 위치이동 등 양축 필요로 간주.</param>
        private unsafe bool TryValidateEmsManualMotionPreconditions(out string failureDetail, bool skipAxisOriginCheck = false, byte? jogTagForOriginCheck = null)
        {
            var sb = new StringBuilder();

            if (form_Main.COMMDataManager.CommSt == 0)
                sb.AppendLine("· 통신이 두절된 상태입니다.");
            if (!form_Main.COMMDataManager.DevRec.Flag_In_DevStatus)
                sb.AppendLine("· 장비 상태 정보가 수신되지 않았습니다.");

            fixed (VEXI_DEFS.TEMS_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.ems_REC_EMSSt)
            {
                if (!Global_Class.BitStatus(DevSt->DevMode, 1))
                    sb.AppendLine("· 수동 모드가 아닙니다.");
                if (Global_Class.BitStatus(DevSt->DevSt_1, 0))
                    sb.AppendLine("· WCS ON 상태입니다. (상위 명령 수신 모드에서는 수동 조작을 사용할 수 없습니다.)");
                if (Global_Class.BitStatus(DevSt->DevSt_2, 7))
                    sb.AppendLine("· 비상정지 스위치가 ON입니다.");
                if (Global_Class.BitStatus(DevSt->DevSt_1, 1))
                    sb.AppendLine("· 비상정지 상태입니다.");
                if (Global_Class.BitStatus(DevSt->DevSt_1, 2))
                    sb.AppendLine("· 장비 경고 알람이 있습니다.");
                if (Global_Class.BitStatus(DevSt->DevSt_1, 3))
                    sb.AppendLine("· 장비 에러(알람)가 발생했습니다.");

                if (!skipAxisOriginCheck)
                {
                    bool needDriveOrigin = true;
                    bool needLiftOrigin = true;
                    if (jogTagForOriginCheck.HasValue)
                    {
                        byte t = jogTagForOriginCheck.Value;
                        if (t == 11 || t == 12 || t == 13 || t == 14)
                        {
                            needDriveOrigin = true;
                            needLiftOrigin = false;
                        }
                        else if (t == 21 || t == 22 || t == 23 || t == 24)
                        {
                            needDriveOrigin = false;
                            needLiftOrigin = true;
                        }
                    }

                    if (needDriveOrigin && !Global_Class.BitStatus(DevSt->Drive_DisPosition.St_2, 2))
                        sb.AppendLine("· 주행 원점이 확인되지 않았습니다.");
                    if (needLiftOrigin && !Global_Class.BitStatus(DevSt->Lift_DisPosition.St_2, 2))
                        sb.AppendLine("· 승강 원점이 확인되지 않았습니다.");
                }
            }

            if (sb.Length == 0)
            {
                failureDetail = null;
                return true;
            }

            failureDetail = "수동 명령을 실행할 수 없습니다. 미충족 항목:\r\n\r\n" + sb.ToString().TrimEnd();
            return false;
        }

        private unsafe void Do_Chucking_Position_Ctrl(byte CtrlValue)
        {
            
            fixed (VEXI_DEFS.TDEV_ManualCtrl* DevCtrl = &ems_REC_ManualCtrl)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_ManualCtrl)));

                DevCtrl->CtrlFlag[0] = 0x10;

                DevCtrl->PositionMove = CtrlValue;

            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_80, ems_REC_ManualCtrl);
        }

        public void Display_DevCommSt()
        {
            if (form_Main.COMMDataManager.CommSt != 0)
            {

                if (form_Main.COMMDataManager.ISCOMM_ResponsGood)
                {
                    lbl_ReceviceGood.BackColor = System.Drawing.Color.Lime;
                    lbl_ReceviceGood.Text = "통신 정상";
                }
                else
                {
                    if (lbl_ReceviceGood.BackColor == System.Drawing.Color.Lime)
                    {
                        lbl_ReceviceGood.BackColor = System.Drawing.Color.Gray;
                    }
                    else if (lbl_ReceviceGood.BackColor == System.Drawing.Color.Gray)
                    {
                        lbl_ReceviceGood.BackColor = System.Drawing.Color.Red;
                    }
                    else if (lbl_ReceviceGood.BackColor == System.Drawing.Color.Red)
                    {
                        lbl_ReceviceGood.BackColor = System.Drawing.Color.Gray;
                    }
                    lbl_ReceviceGood.Text = "통신 불능";
                }
            } else
            {
                lbl_ReceviceGood.BackColor = System.Drawing.Color.Gray;
                lbl_ReceviceGood.Text = "통신 불능";
            }

        }

        public unsafe void Display_DevSt()
        {
            if ((form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) && (form_Main.COMMDataManager.CommSt != 0))
            {
                Display_EMS_BasicSt();
                Display_EMS_JobSt();
                Display_EMS_St();
            }
            else
            {
                Display_St_Init();
            }
        }

        public void Display_JobCtrlRes(byte[] Data)
        {
            ems_REC_Job_CTRLRes = (VEXI_DEFS.TEMS_REC_JobCTRLRES)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TEMS_REC_JobCTRLRES));
        }
        


        private void Display_St_Init()
        {
            //Display_EMS_BasicSt 내 갱신 컴포넌트들
            lblVersion.Text = "";
            lblSystemTimeUTC.Text = "";
            lbl_DevMode_Auto.Text = "";
            lbl_DevMode_Manual.Text = "";
            lbl_DevMode_Force.Text = "";
            lbl_DevMode_Setup.Text = "";
            lbl_DevEmergencySwitch.Text = "";
            lbl_Dev_Emergency.Text = "";
            lbl_Dev_CanWork.Text = "";
            lbl_Dev_Start.Text = "";
            lbl_Dev_InvertorConn.Text = "";
            lbl_Dev_Error.Text = "";

            lbl_Drive_Pos.Text = "";
            lbl_Drive_Dest.Text = "";
            lbl_DriveSt1_4.Text = "";
            lbl_DriveSt1_0.Text = "";
            //lbl_DriveSt2_2.Text = "";
            lbl_DriveSt2_1.Text = "";
            lbl_DriveSt2_0.Text = "";
            lbl_DriveSt2_3.Text = "";
            lbl_invetorst2_DriveToke  .Text = "";

            lbl_Drive_Position.Text = "";
            lbl_Drive_Position_Copy.Text = "";
            lbl_Drive_Speed.Text = "";
            lbl_Drive_Destination.Text = "";
            lbl_Drive_DestSpeed.Text = "";
            lbl_LiftSt1_4.Text = "";
            lbl_LiftSt1_0.Text = "";
            lbl_LiftSt2_2.Text = "";
            lbl_LiftSt2_1.Text = "";
            lbl_LiftSt2_0.Text = "";
            lbl_LiftSt2_3.Text = "";
            lbl_invetorst2_LiftToke.Text = "";

            lbl_Lift_Position.Text = "";
            lbl_Lift_Position_Copy.Text = "";
            lbl_Lift_Speed.Text = "";
            lbl_Lift_Destination.Text = "";
            lbl_Lift_DestSpeed.Text = "";
            
            lbl_CageSt1_4.Text = "";
            lbl_CageSt1_2.Text = "";
            lbl_CageSt1_3.Text = "";
            lbl_CageSt1_0.Text = "";
            lbl_CageSt2_7.Text = "";
            lbl_CageSt2_6.Text = "";
            lbl_CageSt2_0.Text = "";
            lbl_CageSt2_1.Text = "";
            lbl_CageSt2_3.Text = "";
            lbl_CageSt2_4.Text = "";
            lbl_CageSt2_5.Text = "";

            lbl_CageAction.Text = "";
            lbl_CageMoveTargetNo.Text = "";

            lblVersion.BackColor = Color.White;
            lblSystemTimeUTC.BackColor = Color.White;
            lbl_DevMode_Auto.BackColor = Color.White;
            lbl_DevMode_Manual.BackColor = Color.White;
            lbl_DevMode_Force.BackColor = Color.White;
            lbl_DevMode_Setup.BackColor = Color.White;
            lbl_DevEmergencySwitch.BackColor = Color.White;
            lbl_Dev_Emergency.BackColor = Color.White;
            lbl_Dev_CanWork.BackColor = Color.White;
            lbl_Dev_Start.BackColor = Color.White;
            lbl_Dev_InvertorConn.BackColor = Color.White;
            lbl_Dev_Error.BackColor = Color.White;

            lbl_Drive_Pos.BackColor = Color.White;
            lbl_Drive_Dest.BackColor = Color.White;
            lbl_DriveSt1_4.BackColor = Color.White;
            lbl_DriveSt1_0.BackColor = Color.White;
            //lbl_DriveSt2_2.BackColor = Color.White;
            lbl_DriveSt2_1.BackColor = Color.White;
            lbl_DriveSt2_0.BackColor = Color.White;
            lbl_DriveSt2_3.BackColor = Color.White;
            lbl_invetorst2_DriveToke.BackColor = Color.White;

            lbl_Drive_Position.BackColor = Color.White;
            lbl_Drive_Speed.BackColor = Color.White;
            lbl_Drive_Destination.BackColor = Color.White;
            lbl_Drive_DestSpeed.BackColor = Color.White;
            lbl_LiftSt1_4.BackColor = Color.White;
            lbl_LiftSt1_0.BackColor = Color.White;
            lbl_LiftSt2_2.BackColor = Color.White;
            lbl_LiftSt2_1.BackColor = Color.White;
            lbl_LiftSt2_0.BackColor = Color.White;
            lbl_LiftSt2_3.BackColor = Color.White;
            lbl_invetorst2_LiftToke.BackColor = Color.White;

            lbl_Lift_Position.BackColor = Color.White;
            lbl_Lift_Speed.BackColor = Color.White;
            lbl_Lift_Destination.BackColor = Color.White;
            lbl_Lift_DestSpeed.BackColor = Color.White;

            lbl_CageSt1_4.BackColor = Color.White;
            lbl_CageSt1_2.BackColor = Color.White;
            lbl_CageSt1_3.BackColor = Color.White;
            lbl_CageSt1_0.BackColor = Color.White;
            lbl_CageSt2_7.BackColor = Color.White;
            lbl_CageSt2_6.BackColor = Color.White;
            lbl_CageSt2_0.BackColor = Color.White;
            lbl_CageSt2_1.BackColor = Color.White;
            lbl_CageSt2_3.BackColor = Color.White;
            lbl_CageSt2_4.BackColor = Color.White;
            lbl_CageSt2_5.BackColor = Color.White;

            lbl_CageAction.BackColor = Color.White;
            lbl_CageMoveTargetNo.BackColor = Color.White;
        }

        private unsafe void Display_EMS_BasicSt()
        {
            fixed (VEXI_DEFS.TEMS_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.ems_REC_EMSSt)
            {
                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
                    //코딩
                    lblVersion.Text = Global_Class.UTIL_ByteToFVerstr(DevSt->FWversion);
                    DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(DevSt->SystemUTCTime);
                    lblSystemTimeUTC.Text = String.Format("{0}", PCtime);

                    if (Global_Class.BitStatus(DevSt->DevMode, 0)) lbl_DevMode_Auto.BackColor = Color.Lime;
                    else lbl_DevMode_Auto.BackColor = Color.Silver;

                    if (Global_Class.BitStatus(DevSt->DevMode, 1)) lbl_DevMode_Manual.BackColor = Color.Lime;
                    else lbl_DevMode_Manual.BackColor = Color.Silver;

                    if (Global_Class.BitStatus(DevSt->DevMode, 2)) lbl_DevMode_Force.BackColor = Color.Lime;
                    else lbl_DevMode_Force.BackColor = Color.Silver;

                    if (Global_Class.BitStatus(DevSt->DevMode, 3)) lbl_DevMode_Setup.BackColor = Color.Lime;
                    else lbl_DevMode_Setup.BackColor = Color.Silver;

                    if (Global_Class.BitStatus(DevSt->DevSt_2, 7))
                    {
                        lbl_DevEmergencySwitch.Text = "ON";
                        lbl_DevEmergencySwitch.BackColor = Color.Red;
                        lbl_DevEmergencySwitch.ForeColor = Color.White;
                    }
                    else
                    {
                        lbl_DevEmergencySwitch.Text = "OFF";
                        lbl_DevEmergencySwitch.BackColor = Color.Silver;
                        lbl_DevEmergencySwitch.ForeColor = Color.Black;
                    }

                    if (Global_Class.BitStatus(DevSt->DevSt_1, 1))
                    {
                        lbl_Dev_Emergency.Text = "ON";
                        lbl_Dev_Emergency.BackColor = Color.Red;
                        lbl_Dev_Emergency.ForeColor = Color.White;
                    }
                    else
                    {
                        lbl_Dev_Emergency.Text = "OFF";
                        lbl_Dev_Emergency.BackColor = Color.Silver;
                        lbl_Dev_Emergency.ForeColor = Color.Black;
                    }

                    if (Global_Class.BitStatus(DevSt->DevSt_1, 5))
                    {
                        lbl_Dev_CanWork.Text = "ON";
                        lbl_Dev_CanWork.BackColor = Color.Lime;
                        lbl_Dev_CanWork.ForeColor = Color.Black;
                    }
                    else
                    {
                        lbl_Dev_CanWork.Text = "OFF";
                        lbl_Dev_CanWork.BackColor = Color.White;
                        lbl_Dev_CanWork.ForeColor = Color.Black;
                    }


                    if (Global_Class.BitStatus(DevSt->DevSt_1, 0))
                    {
                        lbl_Dev_Start.Text = "ON";
                        lbl_Dev_Start.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_Dev_Start.Text = "OFF";
                        lbl_Dev_Start.BackColor = Color.Silver;
                    }

                    if (Global_Class.BitStatus(DevSt->DevSt_1, 4))
                    {
                        lbl_Dev_InvertorConn.Text = "접속";
                        lbl_Dev_InvertorConn.BackColor = Color.Lime;
                        lbl_Dev_InvertorConn.ForeColor = Color.Black;
                    }
                    else
                    {
                        lbl_Dev_InvertorConn.Text = "미접속";
                        lbl_Dev_InvertorConn.BackColor = Color.Red;
                        lbl_Dev_InvertorConn.ForeColor = Color.White;
                    }


                    if (Global_Class.BitStatus(DevSt->DevSt_1, 3))
                    {
                        lbl_Dev_Error.Text = String.Format("{0}-{1}-{2}", DevSt->ErrorCode.MainCode, DevSt->ErrorCode.SubCode, DevSt->ErrorCode.PosCode) +
                                             "  " + Global_Class.UTIL_EMSAlarmName(DevSt->ErrorCode.MainCode, DevSt->ErrorCode.SubCode, DevSt->ErrorCode.PosCode);
                        lbl_Dev_Error.BackColor = Color.Red;
                        lbl_Dev_Error.ForeColor = Color.White;
                    }
                    else
                    {
                        if (Global_Class.BitStatus(DevSt->DevSt_1, 2))
                        {
                            //lbl_Dev_Error.Text = "경고";
                            lbl_Dev_Error.Text = String.Format("{0}-{1}-{2}", DevSt->ErrorCode.MainCode, DevSt->ErrorCode.SubCode, DevSt->ErrorCode.PosCode) +
                                             "  " + Global_Class.UTIL_EMSWarnningName(DevSt->ErrorCode.MainCode, DevSt->ErrorCode.SubCode, DevSt->ErrorCode.PosCode);
                            lbl_Dev_Error.BackColor = Color.Orange;
                            lbl_Dev_Error.ForeColor = Color.Black;
                        }
                        else
                        {

                            lbl_Dev_Error.Text = "정상";
                            lbl_Dev_Error.BackColor = Color.Lime;
                            lbl_Dev_Error.ForeColor = Color.Black;
                        }
                    }


                }
            }

        }

        private void Display_EMS_JobSt()
        {
        }

        private unsafe void Display_EMS_St()
        {

            fixed (VEXI_DEFS.TEMS_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.ems_REC_EMSSt)
            {
                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {

                    lbl_Drive_Pos.Text = String.Format("S{0}-P{1}",
                                                       DevSt->Drive_Position.PointRec.Station,
                                                       DevSt->Drive_Position.PointRec.Position);
                    lbl_Drive_Dest.Text = String.Format("S{0}-P{1}",
                                                       DevSt->Drive_Dest.Station,
                                                       DevSt->Drive_Dest.Position);


                    if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_1, 4))
                    {
                        lbl_DriveSt1_4.Text = "정위치";
                        lbl_DriveSt1_4.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_DriveSt1_4.Text = "아님";
                        lbl_DriveSt1_4.BackColor = Color.Silver;
                    }

                    if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_1, 0))
                    {
                        lbl_DriveSt1_0.Text = "동작중";
                    }
                    else
                    {
                        lbl_DriveSt1_0.Text = "정지";
                    }

                    if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_2, 0))
                    {
                        lbl_DriveSt2_0.Text = "접속";
                        lbl_DriveSt2_0.BackColor = Color.Lime;
                        lbl_DriveSt2_0.ForeColor = Color.Black;
                    }
                    else
                    {
                        lbl_DriveSt2_0.Text = "미접속";
                        lbl_DriveSt2_0.BackColor = Color.Red;
                        lbl_DriveSt2_0.ForeColor = Color.White;
                    }
                    if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_2, 0))
                    {
                        if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_2, 1))
                        {
                            lbl_DriveSt2_1.Text = "장애";
                            lbl_DriveSt2_1.BackColor = Color.Red;
                            lbl_DriveSt2_1.ForeColor = Color.White;
                        }
                        else
                        {
                            lbl_DriveSt2_1.Text = "정상";
                            lbl_DriveSt2_1.BackColor = Color.Lime;
                            lbl_DriveSt2_1.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        lbl_DriveSt2_1.Text = "미접속";
                        lbl_DriveSt2_1.BackColor = Color.Red;
                        lbl_DriveSt2_1.ForeColor = Color.White;
                    }

                    if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_2, 3))
                    {
                        lbl_DriveSt2_3.Text = "ON";
                        lbl_DriveSt2_3.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_DriveSt2_3.Text = "OFF";
                        lbl_DriveSt2_3.BackColor = Color.Silver;
                    }

                    lbl_invetorst2_DriveToke.Text = string.Format("{0:0.0}", (double)DevSt->invetorst2_DriveToke / 10);


                    //if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_2, 2))
                    //{
                    //    lbl_DriveSt2_2.Text = "확인완료";
                    //    lbl_DriveSt2_2.BackColor = Color.Lime;
                    //    lbl_DriveSt2_2.ForeColor = Color.Black;
                    //}
                    //else
                    //{
                    //    lbl_DriveSt2_2.Text = "미확인";
                    //    lbl_DriveSt2_2.BackColor = Color.Red;
                    //    lbl_DriveSt2_2.ForeColor = Color.White;
                    //}

                    lbl_Drive_Position.Text = String.Format("{0}", DevSt->Drive_DisPosition.Now_Position);
                    lbl_Drive_Position_Copy.Text = String.Format("{0}", DevSt->Drive_DisPosition.Now_Position);
                    lbl_Drive_Speed.Text = string.Format("{0:0.0}", (double)DevSt->Drive_DisPosition.Now_Speed / 10);
                    lbl_Drive_Destination.Text = String.Format("{0}", DevSt->Drive_DisPosition.Dest_Position);
                    lbl_Drive_DestSpeed.Text = string.Format("{0:0.0}", (double)DevSt->Drive_DisPosition.Dest_Speed / 10);


                    if (Global_Class.BitStatus(DevSt->Lift_DisPosition.St_1, 4))
                    {
                        lbl_LiftSt1_4.Text = "정위치";
                        lbl_LiftSt1_4.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_LiftSt1_4.Text = "아님";
                        lbl_LiftSt1_4.BackColor = Color.Silver;
                    }


                    if (Global_Class.BitStatus(DevSt->Lift_DisPosition.St_1, 0))
                    {
                        lbl_LiftSt1_0.Text = "동작중";
                    }
                    else
                    {
                        lbl_LiftSt1_0.Text = "정지";
                    }

                    if (Global_Class.BitStatus(DevSt->Lift_DisPosition.St_2, 0))
                    {
                        lbl_LiftSt2_0.Text = "접속";
                        lbl_LiftSt2_0.BackColor = Color.Lime;
                        lbl_LiftSt2_0.ForeColor = Color.Black;
                    }
                    else
                    {
                        lbl_LiftSt2_0.Text = "미접속";
                        lbl_LiftSt2_0.BackColor = Color.Red;
                        lbl_LiftSt2_0.ForeColor = Color.White;
                    }
                    if (Global_Class.BitStatus(DevSt->Lift_DisPosition.St_2, 0))
                    {
                        if (Global_Class.BitStatus(DevSt->Lift_DisPosition.St_2, 1))
                        {
                            lbl_LiftSt2_1.Text = "장애";
                            lbl_LiftSt2_1.BackColor = Color.Red;
                            lbl_LiftSt2_1.ForeColor = Color.White;
                        }
                        else
                        {
                            lbl_LiftSt2_1.Text = "정상";
                            lbl_LiftSt2_1.BackColor = Color.Lime;
                            lbl_LiftSt2_1.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        lbl_LiftSt2_1.Text = "미접속";
                        lbl_LiftSt2_1.BackColor = Color.Red;
                        lbl_LiftSt2_1.ForeColor = Color.White;
                    }

                    if (Global_Class.BitStatus(DevSt->Lift_DisPosition.St_2, 3))
                    {
                        lbl_LiftSt2_3.Text = "ON";
                        lbl_LiftSt2_3.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_LiftSt2_3.Text = "OFF";
                        lbl_LiftSt2_3.BackColor = Color.Silver;
                    }

                    lbl_invetorst2_LiftToke.Text = string.Format("{0:0.0}", (double)DevSt->invetorst2_LiftToke / 10);


                    if (Global_Class.BitStatus(DevSt->Lift_DisPosition.St_2, 2))
                    {
                        lbl_LiftSt2_2.Text = "확인완료";
                        lbl_LiftSt2_2.BackColor = Color.Lime;
                        lbl_LiftSt2_2.ForeColor = Color.Black;
                    }
                    else
                    {
                        lbl_LiftSt2_2.Text = "미확인";
                        lbl_LiftSt2_2.BackColor = Color.Red;
                        lbl_LiftSt2_2.ForeColor = Color.White;
                    }

                    lbl_Lift_Position.Text = String.Format("{0}", DevSt->Lift_DisPosition.Now_Position);
                    lbl_Lift_Position_Copy.Text = String.Format("{0}", DevSt->Lift_DisPosition.Now_Position);
                    lbl_Lift_Speed.Text = string.Format("{0:0.0}", (double)DevSt->Lift_DisPosition.Now_Speed / 10);
                    lbl_Lift_Destination.Text = String.Format("{0}", DevSt->Lift_DisPosition.Dest_Position);
                    lbl_Lift_DestSpeed.Text = string.Format("{0:0.0}", (double)DevSt->Lift_DisPosition.Dest_Speed / 10);

                    if (Global_Class.BitStatus(DevSt->Cage_St.St_1, 4))
                    {
                        lbl_CageSt1_4.Text = "ON";
                        lbl_CageSt1_4.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_CageSt1_4.Text = "OFF";
                        lbl_CageSt1_4.BackColor = Color.Silver;
                    }

                    if (Global_Class.BitStatus(DevSt->Cage_St.St_1, 3))
                    {
                        lbl_CageSt1_3.Text = "ON";
                        lbl_CageSt1_3.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_CageSt1_3.Text = "OFF";
                        lbl_CageSt1_3.BackColor = Color.Silver;
                    }
                    if (Global_Class.BitStatus(DevSt->Cage_St.St_1, 2))
                    {
                        lbl_CageSt1_2.Text = "감지";
                        lbl_CageSt1_2.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_CageSt1_2.Text = "미감지";
                        lbl_CageSt1_2.BackColor = Color.Silver;
                    }

                    if (Global_Class.BitStatus(DevSt->Cage_St.St_1, 0))
                    {
                        lbl_CageSt1_0.Text = "동작중";
                    }
                    else
                    {
                        lbl_CageSt1_0.Text = "정지";
                    }

                    if (Global_Class.BitStatus(DevSt->Cage_St.St_2, 0))
                    {
                        lbl_CageSt2_0.Text = "ON";
                        lbl_CageSt2_0.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_CageSt2_0.Text = "OFF";
                        lbl_CageSt2_0.BackColor = Color.Silver;
                    }
                    if (Global_Class.BitStatus(DevSt->Cage_St.St_2, 1))
                    {
                        lbl_CageSt2_1.Text = "ON";
                        lbl_CageSt2_1.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_CageSt2_1.Text = "OFF";
                        lbl_CageSt2_1.BackColor = Color.Silver;
                    }

                    if (Global_Class.BitStatus(DevSt->Cage_St.St_2, 3))
                    {
                        lbl_CageSt2_3.Text = "ON";
                        lbl_CageSt2_3.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_CageSt2_3.Text = "OFF";
                        lbl_CageSt2_3.BackColor = Color.Silver;
                    }
                    if (Global_Class.BitStatus(DevSt->Cage_St.St_2, 4))
                    {
                        lbl_CageSt2_4.Text = "ON";
                        lbl_CageSt2_4.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_CageSt2_4.Text = "OFF";
                        lbl_CageSt2_4.BackColor = Color.Silver;
                    }
                    if (Global_Class.BitStatus(DevSt->Cage_St.St_2, 5))
                    {
                        lbl_CageSt2_5.Text = "ON";
                        lbl_CageSt2_5.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_CageSt2_5.Text = "OFF";
                        lbl_CageSt2_5.BackColor = Color.Silver;
                    }
                    if (Global_Class.BitStatus(DevSt->Cage_St.St_2, 6))
                    {
                        lbl_CageSt2_6.Text = "ON";
                        lbl_CageSt2_6.BackColor = Color.Red;
                        lbl_CageSt2_6.ForeColor = Color.White;
                    }
                    else
                    {
                        lbl_CageSt2_6.Text = "OFF";
                        lbl_CageSt2_6.BackColor = Color.Lime;
                        lbl_CageSt2_6.ForeColor = Color.Black;
                    }
                    if (Global_Class.BitStatus(DevSt->Cage_St.St_2, 7))
                    {
                        lbl_CageSt2_7.Text = "확인완료";
                        lbl_CageSt2_7.BackColor = Color.Lime;
                        lbl_CageSt2_7.ForeColor = Color.Black;
                    }
                    else
                    {
                        lbl_CageSt2_7.Text = "미확인";
                        lbl_CageSt2_7.BackColor = Color.Red;
                        lbl_CageSt2_7.ForeColor = Color.White;
                    }

                    switch (DevSt->Cage_St.ActionCode)
                    {

                        case 0: lbl_CageAction.Text = "정지"; break;
                        case 1:
                            lbl_CageAction.Text = "Jog + Low"; break;
                        case 2:
                            lbl_CageAction.Text = "Jog + High"; break;
                        case 3:
                            lbl_CageAction.Text = "Jog - Low"; break;
                        case 4:
                            lbl_CageAction.Text = "Jog - High"; break;
                        case 5:
                            lbl_CageAction.Text = "Preset"; break;
                        case 6:
                            lbl_CageAction.Text = "Move Home"; break;
                        case 7:
                            lbl_CageAction.Text = "Move Position1"; break;
                        case 8:
                            lbl_CageAction.Text = "Move Position2"; break;
                        case 9:
                            lbl_CageAction.Text = "Move Position3"; break;
                        case 10:
                            lbl_CageAction.Text = "Move Position4"; break;
                        case 11:
                            lbl_CageAction.Text = "Move Position5"; break;
                        case 12:
                            lbl_CageAction.Text = "Move Position6"; break;
                        case 13:
                            lbl_CageAction.Text = "Move Position7"; break;
                        case 14:
                            lbl_CageAction.Text = "Move Position8"; break;
                        case 15:
                            lbl_CageAction.Text = "Move Position9"; break;
                        default:
                            lbl_CageAction.Text = DevSt->Cage_St.ActionCode.ToString(); break;

                    }

                    lbl_CageMoveTargetNo.Text = String.Format("{0}", DevSt->Cage_St.MoveTargetNo);
                }
            }
        }
        #endregion
    }
}

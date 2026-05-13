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
        private void Btn_Chucking_Home_Click(object sender, EventArgs e) //홈처킹 버튼 클릭 하면
        {
            Button bt = sender as Button;
            if (bt == null) return;

            /*
            if (!TryValidateEmsManualMotionPreconditions(out string failMsg))//주행과 승강원점은 필요없음. 홈처킹은 홈처킹이지
            {
                form_Main.GlobalObj.MsgBox_Info(failMsg, "W");
                return;
            }
            */

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

            /*
            if (!TryValidateEmsManualMotionPreconditions(out string failMsg, true))
            {
                form_Main.GlobalObj.MsgBox_Info(failMsg, "W");
                return;
            }
            */

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
        /// <param name="jogTagForOriginCheck">조그 Tag: 11~14 주행만, 21~24 승강만, 31~34 Chuck Catch/Uncatch(저·중속)은 주행·승강 원점 미검사, 그 외는 주행+승강 모두, null은 Chucking 위치이동 등 양축 필요로 간주.</param>
        /// 




        //-----------------------------------------------------------------------------------------------------------//

        // 메뉴얼 모드 동작코드 //
        // 수동모드 동작조건
        /*
         1. 비상정지 버튼이 눌려있지 않을 것
         2. 장치에 알람이 떠있지 않을 것
         3. 통신상태가 정상일 것
         4. 시작 OFF 상태일것 = WCS OFF 상태일것
      
         
         */



        private unsafe bool TryValidateEmsManualMotionPreconditions(out string failureDetail, bool skipAxisOriginCheck = false, byte? jogTagForOriginCheck = null)

        // unsafe: 이 함수 내부에서 포인터를 사용한다는 의미 C# 관리형 메모리에서 벗어나, 장비데이터 구조체의 주소에 직접접근해서 빠르게 상태를 읽어옴
        // bool 형태로 반환(true 수동명령 장비로 전송해도 좋음), (false 수동명령 인가불가)
        // out string failureDetail: 수동명령 불가 시, 실패 사유 문자열이 담기는 변수. 수동명령 가능 시 null이 담김.(out 키워드 덕분에 외부에서 이문자열ㅇ르 받아 사용자 팝업메세지 띄울수 있음)
        // skipAxisOriginCheck: 선택적 매개변수이며, 기본값은 false(원점 검사 수행),  
      



        //해당함수 조건,
        //failureDetail은 EMS 수동 조작(조그, Chucking 위치 이동, 원점 설정 등) 공통의 전제조건임.
        //실패 시 false 반환과 함께 failureDetail에 실패 사유 문자열이 담김. 성공 시 true 반환과 함께 failureDetail은 null.

        //skipaxisorigincheck는
        //주행/승강 원점 확인 비트 검사 여부. 원점 설정 명령 등에서 사용. 조그의 경우, 특정 축만 조그하는 경우
        //(jogTagForOriginCheck로 구분) 해당 축의 원점 확인 비트만 검사하도록 함. (예: 주행 조그는 승강 원점 확인 비트는 검사하지 않음)


        {
            var sb = new StringBuilder(); //에러 메세지를 모으기 위한 스트링빌더

            if (form_Main.COMMDataManager.CommSt == 0 || !form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //통신이 끊어진 경우 또는 장비로부터 정보가 수신되지 않은 경우
                sb.AppendLine("· 통신이 두절되었거나 상태정보가 수신되지 않았습니다.");
            

            fixed (VEXI_DEFS.TEMS_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.ems_REC_EMSSt)
                //fixed 명령으로 포인터를 쓸 떄 메모리가 도망가지 못하게 고정*(메모리 점유)
                //폼메인,통신데이터메니저,디바이스레코드에 ems_rec_emsst라는 주소값을 DevST 포인터 변수에 담는다.
                //EMS 상태블록(ems_REC_EMSSt -> Tems_StatusRes) 안의 비트를 여기서는 DevSt 포인터로 TEMS_StatusRes 필드만 읽는다.



            {
                if (!Global_Class.BitStatus(DevSt->DevMode, 1)) // DevSt는 주소값이다. 주소값이 가르키는 메모리 주소로 이동해서 DevMode 값을 가져온다
                                                                // DevMode의 bit1이 0이면 수동모드가 아님 
                                                                // if문 전체가 1이면 수동명령 불가니까 안에가 0이되면 명령불가. 
                    sb.AppendLine("· 수동 모드가 아닙니다.");
                if (Global_Class.BitStatus(DevSt->DevSt_1, 0)) //DevST1은 프로토콜 엑셀표에 정의 되있으며 0번은 시작상태 on/off르 말한다. 0이면 off 1이면 on
                    sb.AppendLine("· WCS ON 상태입니다. (상위 명령 수신 모드에서는 수동 조작을 사용할 수 없습니다.)");
                if (Global_Class.BitStatus(DevSt->DevSt_2, 7)|| Global_Class.BitStatus(DevSt->DevSt_1, 1)) // 0이면 안눌림 1이면 눌림
                    sb.AppendLine("· 비상정지 스위치가 ON입니다.");
                if (!(Global_Class.BitStatus(DevSt->DevMode, 1) && Global_Class.BitStatus(DevSt->DevMode, 2)))
                {
                    if (Global_Class.BitStatus(DevSt->DevSt_1, 2) || Global_Class.BitStatus(DevSt->DevSt_1, 3))
                        sb.AppendLine("· 장비 경고 알람이 있습니다.");
                }
             

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
                        else if (t == 31 || t == 32 || t == 33 || t == 34)
                        {
                            needDriveOrigin = false;
                            needLiftOrigin = false;
                        }
                    }

                    if (needDriveOrigin && !Global_Class.BitStatus(DevSt->Drive_DisPosition.St_2, 2))
                        sb.AppendLine("· 주행 원점이 확인되지 않았습니다.");
                    if (needLiftOrigin && !Global_Class.BitStatus(DevSt->Lift_DisPosition.St_2, 2))
                        sb.AppendLine("· 승강 원점이 확인되지 않았습니다.");
                }
            }

            if (sb.Length == 0)//에러메세지가 없으면 failureDetail에 null을 반환해서 에러가 없는 상태를 만들고 함수에서는 true를 반환
            {
                failureDetail = null;
                return true;
            }

            failureDetail = "수동 명령을 실행할 수 없습니다. 미충족 항목:\r\n\r\n" + sb.ToString().TrimEnd();
            return false;
        }

        //-----------------------------------------------------------------------------------------------------------//


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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void btn_Catch_Click(object sender, EventArgs e)
        {

        }

        private void btn_UnCatch_Click(object sender, EventArgs e)
        {
      
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btn_Backward_MiddleSpeed_Click(object sender, EventArgs e)
        {

        }

        private void btn_Forward_LowSpeed_Click(object sender, EventArgs e)
        {

        }

        private void btn_Backward_LowSpeed_Click(object sender, EventArgs e)
        {

        }

        private void btn_UP_LowSpeed_Click(object sender, EventArgs e)
        {

        }

        private void btn_DOWN_LowSpeed_Click(object sender, EventArgs e)
        {

        }
    }
}

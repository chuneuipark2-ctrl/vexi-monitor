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

        private static VEXI_DEFS.TEMS_REC_TaskJobCTRL ems_REC_TaskJob_CTRL;
        private static VEXI_DEFS.EMS_REC_JobCTRL ems_REC_Job_CTRL;
        private static VEXI_DEFS.TEMS_REC_JobCTRLRES ems_REC_Job_CTRLRes;
        private static VEXI_DEFS.TDEV_ManualCtrl ems_REC_ManualCtrl;



        public Form_EMS_CTL()
        {
            InitializeComponent();
        }


        #region 컴포넌트 이벤트
        private void Btn_Chucking_Home_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

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
            toolTip1.SetToolTip(lblLastWorkNum_woZero, "더블클릭 시 작업번호로 복사됨");
            Display_DevSt();
        }

        private void Form_EMS_CTL_Deactivate(object sender, EventArgs e)
        {
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

            form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue_before = Convert.ToByte(bt.Tag.ToString());
            form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue = 0;
            
            form_Main.Do_JogCtrl();
        }


        private void btn_UP_LowSpeed_MouseDown(object sender, MouseEventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue = Convert.ToByte(bt.Tag.ToString());
            form_Main.Do_JogCtrl();
        }

        private void btn_SetRef_Drive_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;
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

        private void btn_Move_Station1_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "Station 이동 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                lbl_JobCtrlRes.Visible = false;
                Do_Semi_MoveStationCMD_Ctrl();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "Position 이동 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                lbl_JobCtrlRes.Visible = false;
                Do_Semi_MovePositionCMD_Ctrl();
            }
        }

        private void btn_Load_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "Loading 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                lbl_JobCtrlRes.Visible = false;
                Do_Semi_LoadingCMD_Ctrl();
            }

        }

        private void btn_UnLoad_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "Unloading 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                lbl_JobCtrlRes.Visible = false;
                Do_Semi_UnloadingCMD_Ctrl();
            }

        }

        private void btn_SToS_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "스테이션간 반송 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                lbl_JobCtrlRes.Visible = false;
                Do_Semi_StoSCMD_Ctrl();
            }
        }

        private void btn_ChangeS_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "목적지 스테이션 변경 반송 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                lbl_JobCtrlRes.Visible = false;
                Do_Semi_ChangeSCMD_Ctrl();
            }
        }


        private void btn_DelWork_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "작업을 삭제하시겠습니까"))
            {
                Do_Ctrl_DelWork(ConstClass.CMD2_53, 0x01);
            }
        }



        #endregion

        #region 기능함수
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


        private unsafe void Do_Semi_ChangeSCMD_Ctrl()
        {
            fixed (VEXI_DEFS.EMS_REC_JobCTRL* DevCtrl = &ems_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.EMS_REC_JobCTRL)));

                DevCtrl->OptionFlag = 0x00;
                if (cbJobOption_0.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x01);
                if (cbJobOption_1.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x02);
                if (cbJobOption_2.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x04);

                DevCtrl->CMD = ConstClass.SEMI_ChangeS;

                //DevCtrl->Work_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                DevCtrl->Work_Num = (UInt32)Global_Class.UTIL_StrToUInt32Def(edChangeS_WorkNum.Text, form_Main.COMMDataManager.Random_WorkNum_AndInc);
                edChangeS_WorkNum.Text = DevCtrl->Work_Num.ToString();
                DevCtrl->Work_From.Station = (byte)numed_ChangeS_FromS.Value;
                DevCtrl->Work_To.Station = (byte)numed_ChangeS_ToS.Value;
                DevCtrl->Chucking_Width = (byte)(cbItem_chuckingW.SelectedIndex + 1);
                DevCtrl->Loading_height = (UInt16)Global_Class.UTIL_StrToUInt32Def(edItem_LoadingH.Text, 0);
                DevCtrl->UnLoading_height = (UInt16)Global_Class.UTIL_StrToUInt32Def(edItem_UnLoadingH.Text, 0);

            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, ems_REC_Job_CTRL);
        }

        private unsafe void Do_Semi_StoSCMD_Ctrl()
        {
            fixed (VEXI_DEFS.EMS_REC_JobCTRL* DevCtrl = &ems_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.EMS_REC_JobCTRL)));
                DevCtrl->OptionFlag = 0x00;
                if (cbJobOption_0.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x01);
                if (cbJobOption_1.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x02);
                if (cbJobOption_2.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x04);

                DevCtrl->CMD = ConstClass.SEMI_StoS;

                DevCtrl->Work_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                DevCtrl->Work_From.Station = (byte)numed_StoS_FromS.Value;
                DevCtrl->Work_To.Station = (byte)numed_StoS_ToS.Value;
                DevCtrl->Chucking_Width = (byte)(cbItem_chuckingW.SelectedIndex + 1);
                DevCtrl->Loading_height = (UInt16)Global_Class.UTIL_StrToUInt32Def(edItem_LoadingH.Text, 0);
                DevCtrl->UnLoading_height = (UInt16)Global_Class.UTIL_StrToUInt32Def(edItem_UnLoadingH.Text, 0);
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, ems_REC_Job_CTRL);
        }


        private unsafe void Do_Semi_UnloadingCMD_Ctrl()
        {
            fixed (VEXI_DEFS.EMS_REC_JobCTRL* DevCtrl = &ems_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.EMS_REC_JobCTRL)));
                DevCtrl->OptionFlag = 0x00;
                if (cbJobOption_0.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x01);
                if (cbJobOption_1.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x02);
                if (cbJobOption_2.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x04);

                DevCtrl->CMD = ConstClass.SEMI_TaskUnLoading;

                DevCtrl->Work_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                DevCtrl->Work_To.Station = (byte)numed_UnLoadS.Value;
                DevCtrl->Chucking_Width = (byte)(cbItem_chuckingW.SelectedIndex + 1);
                DevCtrl->Loading_height = (UInt16)Global_Class.UTIL_StrToUInt32Def(edItem_LoadingH.Text, 0);
                DevCtrl->UnLoading_height = (UInt16)Global_Class.UTIL_StrToUInt32Def(edItem_UnLoadingH.Text, 0);
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, ems_REC_Job_CTRL);
        }


        private unsafe void Do_Semi_LoadingCMD_Ctrl()
        {
            fixed (VEXI_DEFS.EMS_REC_JobCTRL* DevCtrl = &ems_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.EMS_REC_JobCTRL)));
                DevCtrl->OptionFlag = 0x00;
                if (cbJobOption_0.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x01);
                if (cbJobOption_1.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x02);
                if (cbJobOption_2.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x04);

                DevCtrl->CMD = ConstClass.SEMI_TaskLoading;

                DevCtrl->Work_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                DevCtrl->Work_To.Station = (byte)numed_LoadS.Value;
                DevCtrl->Chucking_Width = (byte)(cbItem_chuckingW.SelectedIndex + 1);
                DevCtrl->Loading_height = (UInt16)Global_Class.UTIL_StrToUInt32Def(edItem_LoadingH.Text, 0);
                DevCtrl->UnLoading_height = (UInt16)Global_Class.UTIL_StrToUInt32Def(edItem_UnLoadingH.Text, 0);
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, ems_REC_Job_CTRL);
        }

        private unsafe void Do_Semi_MoveStationCMD_Ctrl()

        {
            fixed (VEXI_DEFS.EMS_REC_JobCTRL* DevCtrl = &ems_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.EMS_REC_JobCTRL)));

                DevCtrl->OptionFlag = 0x00;
                if (cbJobOption_0.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x01);
                if (cbJobOption_1.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x02);
                if (cbJobOption_2.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x04);

                DevCtrl->CMD = ConstClass.SEMI_MOVE;

                DevCtrl->Work_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                DevCtrl->Work_To.Station = (byte)numed_Station_Move_S.Value; ;
                DevCtrl->Chucking_Width = (byte)(cbItem_chuckingW.SelectedIndex + 1);
                DevCtrl->Loading_height = (UInt16)Global_Class.UTIL_StrToUInt32Def(edItem_LoadingH.Text, 0);
                DevCtrl->UnLoading_height = (UInt16)Global_Class.UTIL_StrToUInt32Def(edItem_UnLoadingH.Text, 0);
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, ems_REC_Job_CTRL);            
        }

        private unsafe void Do_Semi_MovePositionCMD_Ctrl()

        {
            fixed (VEXI_DEFS.EMS_REC_JobCTRL* DevCtrl = &ems_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.EMS_REC_JobCTRL)));

                DevCtrl->OptionFlag = 0x00;
                if (cbJobOption_0.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x01);
                if (cbJobOption_1.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x02);
                if (cbJobOption_2.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x04);

                DevCtrl->CMD = ConstClass.SEMI_MOVE;

                DevCtrl->Work_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                DevCtrl->Work_To.Position = (byte)numed_Position_Move_S.Value; ;
                DevCtrl->Chucking_Width = (byte)(cbItem_chuckingW.SelectedIndex + 1);
                DevCtrl->Loading_height = (UInt16)Global_Class.UTIL_StrToUInt32Def(edItem_LoadingH.Text, 0);
                DevCtrl->UnLoading_height = (UInt16)Global_Class.UTIL_StrToUInt32Def(edItem_UnLoadingH.Text, 0);
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, ems_REC_Job_CTRL);
        }

        private void Do_Ctrl_DelWork(byte TmpCMD2, byte CtrlData)
        {
            form_Main.Do_Ctrl_Cmd_withOnebyte(ConstClass.CMD1_00, ConstClass.CMD2_53, CtrlData);
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

            lbl_JobCtrlRes.Visible = (ems_REC_Job_CTRLRes.ResultRes != 0);

            switch (ems_REC_Job_CTRLRes.Work_ResultRes)
            {
                case 31: lbl_JobCtrlRes.Text = string.Format("{0}", (ems_REC_Job_CTRLRes.Work_ResultRes)) + " 작업코드 이상"; break;
                case 33: lbl_JobCtrlRes.Text = string.Format("{0}", (ems_REC_Job_CTRLRes.Work_ResultRes)) + " 작업수행중"; break;
                case 34: lbl_JobCtrlRes.Text = string.Format("{0}", (ems_REC_Job_CTRLRes.Work_ResultRes)) + " 장애 상태"; break;
                case 35: lbl_JobCtrlRes.Text = string.Format("{0}", (ems_REC_Job_CTRLRes.Work_ResultRes)) + " 시작 OFF"; break;
                default : lbl_JobCtrlRes.Text = string.Format("{0}", (ems_REC_Job_CTRLRes.Work_ResultRes)) + " Unknown Nack"; break;
            }
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


            lbl_Work_Job.Text = "";
            lbl_Work_Cmd.Text = "";
            lbl_Work_From.Text = "";
            lbl_Work_To.Text = "";
            lbl_Work_jobSt.Text = "";
            lbl_Work_jobStep.Text = "";
            lblItem_chuckingW.Text = "";
            lblItem_LoadingH.Text = "";
            lblItem_UnLoadingH.Text = "";
            lbl_Move_Job.Text = "";
            lbl_Move_Cmd.Text = "";
            lbl_Move_To.Text = "";
            lbl_Move_jobSt.Text = "";
            lbl_Move_jobStep.Text = "";

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

            lblLastWorkNum_woZero.Text = "";

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


            lbl_Work_Job.BackColor = Color.White;
            lbl_Work_Cmd.BackColor = Color.White;
            lbl_Work_From.BackColor = Color.White;
            lbl_Work_To.BackColor = Color.White;
            lbl_Work_jobSt.BackColor = Color.White;
            lbl_Work_jobStep.BackColor = Color.White;
            lblItem_chuckingW.BackColor = Color.White;
            lblItem_LoadingH.BackColor = Color.White;
            lblItem_UnLoadingH.BackColor = Color.White;
            lbl_Move_Job.BackColor = Color.White;
            lbl_Move_Cmd.BackColor = Color.White;
            lbl_Move_To.BackColor = Color.White;
            lbl_Move_jobSt.BackColor = Color.White;
            lbl_Move_jobStep.BackColor = Color.White;

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

        private unsafe void Display_EMS_JobSt()
        {
            fixed (VEXI_DEFS.TEMS_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.ems_REC_EMSSt)
            {
                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
                    if (DevSt->Work_Job.Item_Do_Status == 4)
                    {
                        lbl_Work_Job.Text = string.Format("{0} (완료)", DevSt->Work_Job.Item_JobNumber);
                    }
                    else
                    {
                        lbl_Work_Job.Text = string.Format("{0}", DevSt->Work_Job.Item_JobNumber);
                    }

                    if (DevSt->Work_Job.Item_JobNumber != 0)
                    {
                        lblLastWorkNum_woZero.Text = string.Format("{0}", DevSt->Work_Job.Item_JobNumber);
                    }


                    lbl_Work_Cmd.Text = Global_Class.UTIL_GetJobTextAsValue(DevSt->Work_Job.Item_CMD_Code);
                    lbl_Work_From.Text = string.Format("S{0}-P{1}", DevSt->Work_Job.Item_From.Station
                                                          , DevSt->Work_Job.Item_From.Position);
                    lbl_Work_To.Text = string.Format("S{0}-P{1}", DevSt->Work_Job.Item_To.Station
                                                      , DevSt->Work_Job.Item_To.Position);
                    switch (DevSt->Work_Job.Item_Do_Status)
                    {
                        case 0: lbl_Work_jobSt.Text = "지령없음"; break;
                        case 2: lbl_Work_jobSt.Text = "수행중"; break;
                        case 3: lbl_Work_jobSt.Text = "실패"; break;
                        case 4: lbl_Work_jobSt.Text = "완료"; break;
                        default: lbl_Work_jobSt.Text = string.Format("0x{0:X2}", DevSt->Work_Job.Item_Do_Status); break;
                    }

                    lbl_Work_jobStep.Text = Global_Class.UTIL_GetEMSJobStepTextAsValue(DevSt->Work_Job.Item_Do_Step);
                    lblItem_chuckingW.Text = string.Format("{0}", DevSt->Work_Job.Chucking_Width);
                    lblItem_LoadingH.Text = string.Format("{0}", DevSt->Work_Job.Loading_height);
                    lblItem_UnLoadingH.Text = string.Format("{0}", DevSt->Work_Job.UnLoading_height);


                    if (DevSt->Move_Job.Item_Do_Status == 4)
                    {
                        lbl_Move_Job.Text = string.Format("{0} (완료)", DevSt->Move_Job.Item_JobNumber);
                    }
                    else
                    {
                        lbl_Move_Job.Text = string.Format("{0}", DevSt->Move_Job.Item_JobNumber);
                    }

                    lbl_Move_Cmd.Text = Global_Class.UTIL_GetJobTextAsValue(DevSt->Move_Job.Item_CMD_Code);

                    lbl_Move_To.Text = string.Format("S{0}-P{1}", DevSt->Move_Job.Item_To.Station
                                                      , DevSt->Move_Job.Item_To.Position);
                    switch (DevSt->Move_Job.Item_Do_Status)
                    {
                        case 0: lbl_Move_jobSt.Text = "지령없음"; break;
                        case 2: lbl_Move_jobSt.Text = "수행중"; break;
                        case 3: lbl_Move_jobSt.Text = "실패"; break;
                        case 4: lbl_Move_jobSt.Text = "완료"; break;
                        default: lbl_Move_jobSt.Text = string.Format("0x{0:X2}", DevSt->Move_Job.Item_Do_Status); break;
                    }

                    lbl_Move_jobStep.Text = Global_Class.UTIL_GetEMSJobStepTextAsValue(DevSt->Move_Job.Item_Do_Step);

                }
            }
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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnDemoTest_Start_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "데모운전을 시작하시겠습니까?"))
            {
                byte[] Data = { 0x00, 0x00 };
                if (rbDemoTest_1.Checked)
                {
                    Data[0] = 0x01;
                }
                else if (rbDemoTest_2.Checked)
                {
                    Data[0] = 0x02;
                }
                else
                {
                    Data[0] = 0x03;
                }
                form_Main.Do_Ctrl_Cmd_withbytes(ConstClass.CMD1_01, ConstClass.CMD2_60, Data);
            }
        }

        private void btnDemoTest_Stop_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "데모운전을 중지하시겠습니까?"))
            {
                byte[] Data = { 0x00, 0x00 };
                form_Main.Do_Ctrl_Cmd_withbytes(ConstClass.CMD1_01, ConstClass.CMD2_60, Data);
            }
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void lblLastWorkNum_woZero_DoubleClick(object sender, EventArgs e)
        {
            edChangeS_WorkNum.Text = lblLastWorkNum_woZero.Text;
        }

        private void tab_Change_Station_Click(object sender, EventArgs e)
        {

        }

        private void btn_DoneWork_L1_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "강제 완료 처리하시겠습니까?"))
            {
                form_Main.Do_Ctrl_Cmd_withOnebyte(ConstClass.CMD1_00, ConstClass.CMD2_63, Convert.ToByte(bt.Tag.ToString()));
            }
        }
    }
}

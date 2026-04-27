using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_EMSSt : Form
    {
        public Form_Main form_Main;
        private Label[] lbl_DI_Title;
        private Label[] lbl_DI_ST;
        private Label[] lbl_DO_Title;
        private Label[] lbl_DO_ST;

        public Form_EMSSt()
        {
            InitializeComponent();

            lbl_DI_Title = new Label[] {lbl_IO_Title_1, 
                lbl_IO_Title_2,
                lbl_IO_Title_3,
                lbl_IO_Title_4,
                lbl_IO_Title_5,
                lbl_IO_Title_6,
                lbl_IO_Title_7,
                lbl_IO_Title_8,
                lbl_IO_Title_9,
                lbl_IO_Title_10,
                lbl_IO_Title_11,
                lbl_IO_Title_12,
                lbl_IO_Title_13,
                lbl_IO_Title_14,
                lbl_IO_Title_15,
                lbl_IO_Title_16,
                lbl_IO_Title_17,
                lbl_IO_Title_18,
                lbl_IO_Title_19,
                lbl_IO_Title_20,
                lbl_IO_Title_21,
                lbl_IO_Title_22,
                lbl_IO_Title_23,
                lbl_IO_Title_24,
                lbl_IO_Title_25,
                lbl_IO_Title_26,
                lbl_IO_Title_27,
                lbl_IO_Title_28,
                lbl_IO_Title_29,
                lbl_IO_Title_30,
                lbl_IO_Title_31,
                lbl_IO_Title_32,
                lbl_IO_Title_33,
                lbl_IO_Title_34,
                lbl_IO_Title_35,
                lbl_IO_Title_36,
                lbl_IO_Title_37,
                lbl_IO_Title_38
            };

            lbl_DI_ST = new Label[] {lbl_IOSt_1,
                lbl_IOSt_2,
                lbl_IOSt_3,
                lbl_IOSt_4,
                lbl_IOSt_5,
                lbl_IOSt_6,
                lbl_IOSt_7,
                lbl_IOSt_8,
                lbl_IOSt_9,
                lbl_IOSt_10,
                lbl_IOSt_11,
                lbl_IOSt_12,
                lbl_IOSt_13,
                lbl_IOSt_14,
                lbl_IOSt_15,
                lbl_IOSt_16,
                lbl_IOSt_17,
                lbl_IOSt_18,
                lbl_IOSt_19,
                lbl_IOSt_20,
                lbl_IOSt_21,
                lbl_IOSt_22,
                lbl_IOSt_23,
                lbl_IOSt_24,
                lbl_IOSt_25,
                lbl_IOSt_26,
                lbl_IOSt_27,
                lbl_IOSt_28,
                lbl_IOSt_29,
                lbl_IOSt_30,
                lbl_IOSt_31,
                lbl_IOSt_32,
                lbl_IOSt_33,
                lbl_IOSt_34,
                lbl_IOSt_35,
                lbl_IOSt_36,
                lbl_IOSt_37,
                lbl_IOSt_38
            };


            lbl_DO_Title = new Label[] {lbl_O_Title_1,
                lbl_O_Title_2,
                lbl_O_Title_3,
                lbl_O_Title_4,
                lbl_O_Title_5,
                lbl_O_Title_6,
                lbl_O_Title_7,
                lbl_O_Title_8,
                lbl_O_Title_9,
                lbl_O_Title_10,
                lbl_O_Title_11,
                lbl_O_Title_12,
                lbl_O_Title_13,
                lbl_O_Title_14,
                lbl_O_Title_15,
                lbl_O_Title_16,
                lbl_O_Title_17,
                lbl_O_Title_18,
                lbl_O_Title_19,
                lbl_O_Title_20,
                lbl_O_Title_21,
                lbl_O_Title_22,
                lbl_O_Title_23,
                lbl_O_Title_24,
                lbl_O_Title_25,
                lbl_O_Title_26,
                lbl_O_Title_27,
                lbl_O_Title_28,
                lbl_O_Title_29,
                lbl_O_Title_30,
                lbl_O_Title_31,
                lbl_O_Title_32,
                lbl_O_Title_33,
                lbl_O_Title_34,
                lbl_O_Title_35,
                lbl_O_Title_36,
                lbl_O_Title_37,
                lbl_O_Title_38
            };

            lbl_DO_ST = new Label[] {lbl_OSt_1,
                lbl_OSt_2,
                lbl_OSt_3,
                lbl_OSt_4,
                lbl_OSt_5,
                lbl_OSt_6,
                lbl_OSt_7,
                lbl_OSt_8,
                lbl_OSt_9,
                lbl_OSt_10,
                lbl_OSt_11,
                lbl_OSt_12,
                lbl_OSt_13,
                lbl_OSt_14,
                lbl_OSt_15,
                lbl_OSt_16,
                lbl_OSt_17,
                lbl_OSt_18,
                lbl_OSt_19,
                lbl_OSt_20,
                lbl_OSt_21,
                lbl_OSt_22,
                lbl_OSt_23,
                lbl_OSt_24,
                lbl_OSt_25,
                lbl_OSt_26,
                lbl_OSt_27,
                lbl_OSt_28,
                lbl_OSt_29,
                lbl_OSt_30,
                lbl_OSt_31,
                lbl_OSt_32,
                lbl_OSt_33,
                lbl_OSt_34,
                lbl_OSt_35,
                lbl_OSt_36,
                lbl_OSt_37,
                lbl_OSt_38
            };
        }

        #region 컴포넌트 이벤트
        private void Form_EMSSt_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }
            Display_DevSt();
        }

        private void btnBarCodeErrCountInit_Click(object sender, EventArgs e)
        {
            form_Main.Do_Ctrl_Cmd_withOnebyte(ConstClass.CMD1_00, ConstClass.CMD2_62, 1);
        }

        private void rb_DIO_DigitalIn_1_Click(object sender, EventArgs e)
        {
            //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus)
            {
                Display_Sub_DIO();
            }
        }

        #endregion

        #region 기능함수

        private void Display_Init()
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
            lbl_Dev_Start.Text = "";
            lbl_Dev_InvertorConn.Text = "";
            lbl_Dev_CanWork.Text = "";
            lbl_Dev_ActionCode.Text = "";
            lbl_Dev_Error.Text = "";
            lbl_EMSS_RailType.Text = "";
            lblHOGINum.Text = "";
            lblGroupNum.Text = "";
            lblProjectNo.Text = "";


            lbl_Work_Job.Text = "";
            lbl_Work_Cmd.Text = "";
            lbl_Work_From.Text = "";
            lbl_Work_To.Text = "";
            lbl_Work_jobSt.Text = "";
            lbl_Work_jobStep.Text = "";
            lblItem_chuckingW.Text = "";
            lblItem_LoadingH.Text = "";
            lblItem_UnLoadingH.Text = "";
            lblCmdOption_1.Text = "";
            lblCmdOption_0.Text = "";
            lbl_Move_Job.Text = "";
            lbl_Move_Cmd.Text = "";
            lbl_Move_To.Text = "";
            lbl_Move_jobSt.Text = "";
            lbl_Move_jobStep.Text = "";
            lbl_TaskJobSt.Text = "";
            lbl_TaskJobNumber.Text = "";

            lbl_Dev_Home.Text = "";
            lbl_Dev_LiftHome.Text = "";
            lbl_Dev_Maintance.Text = "";
            lbl_CanWork_StationIndex.Text = "";
            lbl_Drive_Pos.Text = "";
            lbl_Drive_Dest.Text = "";
            lbl_Drive_CurrentPos.Text = "";
            lbl_Drive_CurrentStation.Text = "";
            lbl_DriveSt1_4.Text = "";
            lbl_DriveSt1_3.Text = "";
            lbl_DriveSt1_2.Text = "";
            lbl_DriveSt1_1.Text = "";
            lbl_DriveSt1_0.Text = "";
            lbl_DriveSt2_3.Text = "";
            lbl_invetorst2_DriveToke.Text = "";
            //lbl_DriveSt2_2.Text = "";
            lbl_DriveSt2_1.Text = "";
            lbl_DriveSt2_0.Text = "";
            lbl_DriveAlarmCode_Invertor.Text = "";
            lbl_Drive_Position.Text = "";
            lbl_Drive_Speed.Text = "";
            lbl_Drive_Destination.Text = "";
            lbl_Drive_DestSpeed.Text = "";
            lbl_DriveBarcodeErrNumber.Text = "";
            lbl_DriveBarcodeErrCount.Text = "";
            lbl_LiftSt1_4.Text = "";
            lbl_LiftSt1_3.Text = "";
            lbl_LiftSt1_2.Text = "";
            lbl_LiftSt1_1.Text = "";
            lbl_LiftSt1_0.Text = "";
            lbl_LiftSt2_3.Text = "";
            lbl_invetorst2_LiftToke.Text = "";
            lbl_LiftSt2_2.Text = "";
            lbl_LiftSt2_1.Text = "";
            lbl_LiftSt2_0.Text = "";
            lbl_LiftAlarmCode_Invertor.Text = "";
            lbl_Lift_Position.Text = "";
            lbl_Lift_Speed.Text = "";
            lbl_Lift_Destination.Text = "";
            lbl_Lift_DestSpeed.Text = "";
            
            lbl_CageSt1_4.Text = "";
            lbl_CageSt1_2.Text = "";
            lbl_CageSt1_1.Text = "";
            lbl_CageSt1_3.Text = "";
            lbl_CageSt1_0.Text = "";
            lbl_CageSt2_7.Text = "";
            lbl_CageSt2_6.Text = "";
            lbl_CageSt2_0.Text = "";
            lbl_CageSt2_1.Text = "";
            lbl_CageSt2_3.Text = "";
            lbl_CageSt2_4.Text = "";
            lbl_CageSt2_5.Text = "";
            lbl_CageSt3_0.Text = "";
            lbl_CageSt3_1.Text = "";
            lbl_CageSt3_2.Text = "";

            lbl_CageAction.Text = "";
            lbl_CageMoveTargetNo.Text = "";

            lbl_DriveAreaInfo_AreaNo.Text = "";
            lbl_DriveAreaInfo_AreaType.Text = "";
            lbl_DriveAreaInfo_StartMM.Text = "";
            lbl_DriveAreaInfo_EndMM.Text = "";
            lbl_DriveAreaInfo_MaxSpeed.Text = "";
            lbl_DriveAreaInfo_PrevArea.Text = "";
            lbl_DriveAreaInfo_NextArea.Text = "";
            lbl_DriveAreaInfo_SensorIndex.Text = "";
            lbl_DriveAreaInfo_Region_0.Text = "";
            lbl_DriveAreaInfo_Region_1.Text = "";
            lbl_DriveAreaInfo_Region_2.Text = "";
            lbl_Coll_Stop.Text = "";
            lbl_Coll_Start.Text = "";
            lbl_Now_Station.Text = "";
            lbl_Now_Station_UnloadingCan.Text = "";
            lbl_Now_Station_LoadingCan.Text = "";
            lbl_Front_Collision_EMSID.Text = "";
            lbl_Front_Collision_RxTime.Text = "";
            lbl_Front_Collision_GapDistance.Text = "";
            lbl_Front_Collision_StopDistance.Text = "";
            lbl_Front_Collision_StartDistance.Text = "";
            lbl_Front_Collision_MyAreaType.Text = "";
            lbl_Front_Collision_AreaType.Text = "";
            lbl_Front_Collision_DriveSt_2.Text = "";
            lbl_Front_Collision_DriveSt_1.Text = "";
            lbl_Front_Collision_Speed.Text = "";
            lbl_Front_Collision_NowPosition.Text = "";
            lbl_Front_Collision_DestPosition.Text = "";
            lbl_Rear_Collision_EMSID.Text = "";
            lbl_Rear_Collision_RxTime.Text = "";
            lbl_Rear_Collision_GapDistance.Text = "";
            lbl_Rear_Collision_StopDistance.Text = "";
            lbl_Rear_Collision_StartDistance.Text = "";
            lbl_Rear_Collision_MyAreaType.Text = "";
            lbl_Rear_Collision_AreaType.Text = "";
            lbl_Rear_Collision_DriveSt_2.Text = "";
            lbl_Rear_Collision_DriveSt_1.Text = "";
            lbl_Rear_Collision_Speed.Text = "";
            lbl_Rear_Collision_NowPosition.Text = "";
            lbl_Rear_Collision_DestPosition.Text = "";

            lblInterlockIn0.Text = "";
            lblInterlockIn1.Text = "";
            lblInterlockIn2.Text = "";
            lblInterlockIn3.Text = "";
            lblInterlockIn4.Text = "";
            lblInterlockIn5.Text = "";
            lblInterlockIn6.Text = "";
            lblInterlockIn7.Text = "";

            lblInterlockOut0.Text = "";
            lblInterlockOut1.Text = "";
            lblInterlockOut2.Text = "";
            lblInterlockOut3.Text = "";
            lblInterlockOut4.Text = "";
            lblInterlockOut5.Text = "";
            lblInterlockOut6.Text = "";
            lblInterlockOut7.Text = "";


            lbl_FrontLidar_Area.Text = "";
            lbl_FrontLidar_St7.Text = "";
            lbl_FrontLidar_St6.Text = "";
            lbl_FrontLidar_St2.Text = "";
            lbl_FrontLidar_St1.Text = "";
            lbl_FrontLidar_St0.Text = "";

            lbl_RearLidar_Area.Text = "";
            lbl_RearLidar_St7.Text = "";
            lbl_RearLidar_St6.Text = "";
            lbl_RearLidar_St2.Text = "";
            lbl_RearLidar_St1.Text = "";
            lbl_RearLidar_St0.Text = "";

            lblVersion.BackColor = Color.White;
            lblSystemTimeUTC.BackColor = Color.White;
            lbl_DevMode_Auto.BackColor = Color.White;
            lbl_DevMode_Manual.BackColor = Color.White;
            lbl_DevMode_Force.BackColor = Color.White;
            lbl_DevMode_Setup.BackColor = Color.White;
            lbl_DevEmergencySwitch.BackColor = Color.White;
            lbl_Dev_Emergency.BackColor = Color.White;
            lbl_Dev_Start.BackColor = Color.White;
            lbl_Dev_InvertorConn.BackColor = Color.White;
            lbl_Dev_CanWork.BackColor = Color.White;
            lbl_Dev_ActionCode.BackColor = Color.White;
            lbl_Dev_Error.BackColor = Color.White;
            lbl_EMSS_RailType.BackColor = Color.White;
            lblHOGINum.BackColor = Color.White;
            lblGroupNum.BackColor = Color.White;
            lblProjectNo.BackColor = Color.White;


            lbl_Work_Job.BackColor = Color.White;
            lbl_Work_Cmd.BackColor = Color.White;
            lbl_Work_From.BackColor = Color.White;
            lbl_Work_To.BackColor = Color.White;
            lbl_Work_jobSt.BackColor = Color.White;
            lbl_Work_jobStep.BackColor = Color.White;
            lblCmdOption_1.BackColor = Color.White;
            lblCmdOption_0.BackColor = Color.White;
            lblItem_chuckingW.BackColor = Color.White;
            lblItem_LoadingH.BackColor = Color.White;
            lblItem_UnLoadingH.BackColor = Color.White;
            lbl_Move_Job.BackColor = Color.White;
            lbl_Move_Cmd.BackColor = Color.White;
            lbl_Move_To.BackColor = Color.White;
            lbl_Move_jobSt.BackColor = Color.White;
            lbl_Move_jobStep.BackColor = Color.White;
            lbl_TaskJobSt.BackColor = Color.White;
            lbl_TaskJobNumber.BackColor = Color.White;

            lbl_Dev_Home.BackColor = Color.White;
            lbl_Dev_LiftHome.BackColor = Color.White;
            lbl_Dev_Maintance.BackColor = Color.White;
            lbl_CanWork_StationIndex.BackColor = Color.White;
            lbl_Drive_Pos.BackColor = Color.White;
            lbl_Drive_Dest.BackColor = Color.White;
            lbl_Drive_CurrentPos.BackColor = Color.White;
            lbl_Drive_CurrentStation.BackColor = Color.White;
            lbl_DriveSt1_4.BackColor = Color.White;
            lbl_DriveSt1_3.BackColor = Color.White;
            lbl_DriveSt1_2.BackColor = Color.White;
            lbl_DriveSt1_1.BackColor = Color.White;
            lbl_DriveSt1_0.BackColor = Color.White;
            lbl_DriveSt2_3.BackColor = Color.White;
            lbl_invetorst2_DriveToke.BackColor = Color.White;
            //lbl_DriveSt2_2.BackColor = Color.White;
            lbl_DriveSt2_1.BackColor = Color.White;
            lbl_DriveSt2_0.BackColor = Color.White;
            lbl_DriveAlarmCode_Invertor.BackColor = Color.White;
            lbl_Drive_Position.BackColor = Color.White;
            lbl_Drive_Speed.BackColor = Color.White;
            lbl_Drive_Destination.BackColor = Color.White;
            lbl_Drive_DestSpeed.BackColor = Color.White;
            lbl_DriveBarcodeErrNumber.BackColor = Color.White;
            lbl_DriveBarcodeErrCount.BackColor = Color.White;
            lbl_LiftSt1_4.BackColor = Color.White;
            lbl_LiftSt1_3.BackColor = Color.White;
            lbl_LiftSt1_2.BackColor = Color.White;
            lbl_LiftSt1_1.BackColor = Color.White;
            lbl_LiftSt1_0.BackColor = Color.White;
            lbl_LiftSt2_3.BackColor = Color.White;
            lbl_invetorst2_LiftToke.BackColor = Color.White;
            lbl_LiftSt2_2.BackColor = Color.White;
            lbl_LiftSt2_1.BackColor = Color.White;
            lbl_LiftSt2_0.BackColor = Color.White;
            lbl_LiftAlarmCode_Invertor.BackColor = Color.White;
            lbl_Lift_Position.BackColor = Color.White;
            lbl_Lift_Speed.BackColor = Color.White;
            lbl_Lift_Destination.BackColor = Color.White;
            lbl_Lift_DestSpeed.BackColor = Color.White;
            
            lbl_CageSt1_4.BackColor = Color.White;
            lbl_CageSt1_2.BackColor = Color.White;
            lbl_CageSt1_1.BackColor = Color.White;
            lbl_CageSt1_3.BackColor = Color.White;
            lbl_CageSt1_0.BackColor = Color.White;
            lbl_CageSt2_7.BackColor = Color.White;
            lbl_CageSt2_6.BackColor = Color.White;
            lbl_CageSt2_0.BackColor = Color.White;
            lbl_CageSt2_1.BackColor = Color.White;
            lbl_CageSt2_3.BackColor = Color.White;
            lbl_CageSt2_4.BackColor = Color.White;
            lbl_CageSt2_5.BackColor = Color.White;
            lbl_CageSt3_0.BackColor = Color.White;
            lbl_CageSt3_1.BackColor = Color.White;
            lbl_CageSt3_2.BackColor = Color.White;

            lbl_CageAction.BackColor = Color.White;
            lbl_CageMoveTargetNo.BackColor = Color.White;

            lbl_DriveAreaInfo_AreaNo.BackColor = Color.White;
            lbl_DriveAreaInfo_AreaType.BackColor = Color.White;
            lbl_DriveAreaInfo_StartMM.BackColor = Color.White;
            lbl_DriveAreaInfo_EndMM.BackColor = Color.White;
            lbl_DriveAreaInfo_MaxSpeed.BackColor = Color.White;
            lbl_DriveAreaInfo_PrevArea.BackColor = Color.White;
            lbl_DriveAreaInfo_NextArea.BackColor = Color.White;
            lbl_DriveAreaInfo_SensorIndex.BackColor = Color.White;
            lbl_DriveAreaInfo_Region_0.BackColor = Color.White;
            lbl_DriveAreaInfo_Region_1.BackColor = Color.White;
            lbl_DriveAreaInfo_Region_2.BackColor = Color.White;
            lbl_Coll_Stop.BackColor = Color.White;
            lbl_Coll_Start.BackColor = Color.White;
            lbl_Now_Station.BackColor = Color.White;
            lbl_Now_Station_UnloadingCan.BackColor = Color.White;
            lbl_Now_Station_LoadingCan.BackColor = Color.White;
            lbl_Front_Collision_EMSID.BackColor = Color.White;
            lbl_Front_Collision_RxTime.BackColor = Color.White;
            lbl_Front_Collision_GapDistance.BackColor = Color.White;
            lbl_Front_Collision_StopDistance.BackColor = Color.White;
            lbl_Front_Collision_StartDistance.BackColor = Color.White;
            lbl_Front_Collision_MyAreaType.BackColor = Color.White;
            lbl_Front_Collision_AreaType.BackColor = Color.White;
            lbl_Front_Collision_DriveSt_2.BackColor = Color.White;
            lbl_Front_Collision_DriveSt_1.BackColor = Color.White;
            lbl_Front_Collision_Speed.BackColor = Color.White;
            lbl_Front_Collision_NowPosition.BackColor = Color.White;
            lbl_Front_Collision_DestPosition.BackColor = Color.White;
            lbl_Rear_Collision_EMSID.BackColor = Color.White;
            lbl_Rear_Collision_RxTime.BackColor = Color.White;
            lbl_Rear_Collision_GapDistance.BackColor = Color.White;
            lbl_Rear_Collision_StopDistance.BackColor = Color.White;
            lbl_Rear_Collision_StartDistance.BackColor = Color.White;
            lbl_Rear_Collision_MyAreaType.BackColor = Color.White;
            lbl_Rear_Collision_AreaType.BackColor = Color.White;
            lbl_Rear_Collision_DriveSt_2.BackColor = Color.White;
            lbl_Rear_Collision_DriveSt_1.BackColor = Color.White;
            lbl_Rear_Collision_Speed.BackColor = Color.White;
            lbl_Rear_Collision_NowPosition.BackColor = Color.White;
            lbl_Rear_Collision_DestPosition.BackColor = Color.White;

            lblInterlockIn0.BackColor = Color.White;
            lblInterlockIn1.BackColor = Color.White;
            lblInterlockIn2.BackColor = Color.White;
            lblInterlockIn3.BackColor = Color.White;
            lblInterlockIn4.BackColor = Color.White;
            lblInterlockIn5.BackColor = Color.White;
            lblInterlockIn6.BackColor = Color.White;
            lblInterlockIn7.BackColor = Color.White;

            lblInterlockOut0.BackColor = Color.White;
            lblInterlockOut1.BackColor = Color.White;
            lblInterlockOut2.BackColor = Color.White;
            lblInterlockOut3.BackColor = Color.White;
            lblInterlockOut4.BackColor = Color.White;
            lblInterlockOut5.BackColor = Color.White;
            lblInterlockOut6.BackColor = Color.White;
            lblInterlockOut7.BackColor = Color.White;

            lbl_FrontLidar_Area.BackColor = Color.White;
            lbl_FrontLidar_St7.BackColor = Color.White;
            lbl_FrontLidar_St6.BackColor = Color.White;
            lbl_FrontLidar_St2.BackColor = Color.White;
            lbl_FrontLidar_St1.BackColor = Color.White;
            lbl_FrontLidar_St0.BackColor = Color.White;

            lbl_RearLidar_Area.BackColor = Color.White;
            lbl_RearLidar_St7.BackColor = Color.White;
            lbl_RearLidar_St6.BackColor = Color.White;
            lbl_RearLidar_St2.BackColor = Color.White;
            lbl_RearLidar_St1.BackColor = Color.White;
            lbl_RearLidar_St0.BackColor = Color.White;

            lbl_Key_1_0.BackColor = Color.Silver;
            lbl_Key_1_1.BackColor = Color.Silver;
            lbl_Key_1_2.BackColor = Color.Silver;
            lbl_Key_1_3.BackColor = Color.Silver;
            lbl_Key_1_4.BackColor = Color.Silver;
            lbl_Key_1_5.BackColor = Color.Silver;
            lbl_Key_1_6.BackColor = Color.Silver;
            lbl_Key_2_0.BackColor = Color.Silver;
            lbl_Key_2_1.BackColor = Color.Silver;
            lbl_Key_2_2.BackColor = Color.Silver;
            lbl_Key_2_3.BackColor = Color.Silver;
            lbl_Key_2_4.BackColor = Color.Silver;
            lbl_Key_2_5.BackColor = Color.Silver;



            //Display_Sub_DIO
            for (byte i = 1; i <= 38; i++)
            {

                lbl_DI_Title[i - 1].BackColor = Color.Gray;
                lbl_DI_Title[i - 1].Text = "";

                lbl_DI_ST[i - 1].BackColor = Color.Gray;
                lbl_DI_ST[i - 1].Text = "";

                lbl_DO_Title[i - 1].BackColor = Color.Gray;
                lbl_DO_Title[i - 1].Text = "";

                lbl_DO_ST[i - 1].BackColor = Color.Gray;
                lbl_DO_ST[i - 1].Text = "";
            }

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


                    lbl_Dev_ActionCode.Text = Global_Class.UTIL_EMSActionStText(DevSt->ActionCode);

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


                    var convertedArray = new byte[6];
                    System.Runtime.InteropServices.Marshal.Copy((IntPtr)DevSt->ProjectID, convertedArray, 0, 6);
                    //lblProjectNo.Text = System.Text.Encoding.Default.GetString(convertedArray);
                    lblProjectNo.Text = System.Text.Encoding.ASCII.GetString(convertedArray);
                    lblGroupNum.Text = string.Format("{0}", DevSt->GroupID);
                    lblHOGINum.Text = string.Format("{0}", DevSt->HogiID);

                    switch (DevSt->RailType)
                    {
                        case 0: lbl_EMSS_RailType.Text = "직선형"; break;
                        case 1: lbl_EMSS_RailType.Text = "루프형"; break;
                        case 2: lbl_EMSS_RailType.Text = "곡선형"; break;
                        default: lbl_EMSS_RailType.Text = "직선형"; break;
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

                    if (DevSt->Work_Job.Item_Do_Status == 4)
                    {
                        lbl_Work_Job.Text = string.Format("{0} (완료)", DevSt->Work_Job.Item_JobNumber);
                    }
                    else
                    {
                        lbl_Work_Job.Text = string.Format("{0}", DevSt->Work_Job.Item_JobNumber);
                    }


                    lbl_Work_Cmd.Text = Global_Class.UTIL_GetJobTextAsValue(DevSt->Work_Job.Item_CMD_Code);

                    lblItem_chuckingW.Text = string.Format("{0}", DevSt->Work_Job.Chucking_Width);
                    lblItem_LoadingH.Text = string.Format("{0}", DevSt->Work_Job.Loading_height);
                    lblItem_UnLoadingH.Text = string.Format("{0}", DevSt->Work_Job.UnLoading_height);

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
                        case 5: lbl_Work_jobSt.Text = "중지"; break;
                        default: lbl_Work_jobSt.Text = string.Format("0x{0:X2}", DevSt->Work_Job.Item_Do_Status); break;
                    }

                    lbl_Work_jobStep.Text = Global_Class.UTIL_GetEMSJobStepTextAsValue(DevSt->Work_Job.Item_Do_Step);

                    if (Global_Class.BitStatus(DevSt->Work_Job.CmdOption, 1))
                    {
                        lblCmdOption_1.Text = "무시";
                    }
                    else
                    {
                        lblCmdOption_1.Text = "연동";
                    }
                    if (Global_Class.BitStatus(DevSt->Work_Job.CmdOption, 0))
                    {
                        lblCmdOption_0.Text = "허용";
                    }
                    else
                    {
                        lblCmdOption_0.Text = "불가";
                    }


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
                        case 5: lbl_Move_jobSt.Text = "중지"; break;
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

                    if (Global_Class.BitStatus(DevSt->DevSt_2, 0))
                    {
                        lbl_Dev_Home.Text = "정위치";
                        lbl_Dev_Home.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_Dev_Home.Text = "아님";
                        lbl_Dev_Home.BackColor = Color.Silver;
                    }


                    if (Global_Class.BitStatus(DevSt->DevSt_2, 1))
                    {
                        lbl_Dev_Maintance.Text = "정위치";
                        lbl_Dev_Maintance.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_Dev_Maintance.Text = "아님";
                        lbl_Dev_Maintance.BackColor = Color.Silver;
                    }

                    if (Global_Class.BitStatus(DevSt->DevSt_2, 2))
                    {
                        lbl_Dev_LiftHome.Text = "정위치";
                        lbl_Dev_LiftHome.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_Dev_LiftHome.Text = "아님";
                        lbl_Dev_LiftHome.BackColor = Color.Silver;
                    }

                    if ((DevSt->CanWorkStation.Station == 0xFF) && (DevSt->CanWorkStation.Position == 0xFF))
                    {
                        lbl_CanWork_StationIndex.Text = "-";
                    }
                    else
                    {
                        lbl_CanWork_StationIndex.Text = string.Format("S{0}-P{1}", DevSt->CanWorkStation.Station
                                                                                 , DevSt->CanWorkStation.Position);
                    }

                    lbl_Drive_Pos.Text = String.Format("S{0}-P{1}",
                                                       DevSt->Drive_Position.PointRec.Station,
                                                       DevSt->Drive_Position.PointRec.Position);
                    lbl_Drive_Dest.Text = String.Format("S{0}-P{1}",
                                                       DevSt->Drive_Dest.Station,
                                                       DevSt->Drive_Dest.Position);

                    if (Global_Class.BitStatus(DevSt->Drive_Position.CurrentPosition, 0))
                    {
                        lbl_Drive_CurrentPos.Text = "정위치";
                        lbl_Drive_CurrentPos.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_Drive_CurrentPos.Text = "아님";
                        lbl_Drive_CurrentPos.BackColor = Color.Silver;
                    }

                    if (Global_Class.BitStatus(DevSt->Drive_Position.CurrentPosition, 1))
                    {
                        lbl_Drive_CurrentStation.Text = "정위치";
                        lbl_Drive_CurrentStation.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_Drive_CurrentStation.Text = "아님";
                        lbl_Drive_CurrentStation.BackColor = Color.Silver;
                    }


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
                        if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_1, 3))
                        {
                            lbl_DriveSt1_3.Text = "후진";
                        }
                        else
                        {
                            lbl_DriveSt1_3.Text = "전진";
                        }

                        if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_1, 2))
                        {
                            lbl_DriveSt1_2.Text = "감속";
                        }
                        else
                        {
                            lbl_DriveSt1_2.Text = "아님";
                        }
                        if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_1, 1))
                        {
                            lbl_DriveSt1_1.Text = "가속";
                        }
                        else
                        {
                            lbl_DriveSt1_1.Text = "아님";
                        }
                    }
                    else
                    {
                        lbl_DriveSt1_3.Text = "정지";
                        lbl_DriveSt1_2.Text = "아님";
                        lbl_DriveSt1_1.Text = "아님";
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

                    lbl_DriveAlarmCode_Invertor.Text = String.Format("{0}-{1}", DevSt->Drive_DisPosition.AlarmCode1, DevSt->Drive_DisPosition.AlarmCode2);

                    lbl_Drive_Position.Text = String.Format("{0}", DevSt->Drive_DisPosition.Now_Position);
                    lbl_Drive_Speed.Text = string.Format("{0:0.0}", (double)DevSt->Drive_DisPosition.Now_Speed / 10);
                    lbl_Drive_Destination.Text = String.Format("{0}", DevSt->Drive_DisPosition.Dest_Position);
                    lbl_Drive_DestSpeed.Text = string.Format("{0:0.0}", (double)DevSt->Drive_DisPosition.Dest_Speed / 10);

                    lbl_DriveBarcodeErrNumber.Text = string.Format("{0}", DevSt->PositionSensorErrorNum);
                    lbl_DriveBarcodeErrCount.Text = string.Format("{0}", DevSt->PositionSensorErrorCount);


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
                        if (Global_Class.BitStatus(DevSt->Lift_DisPosition.St_1, 3))
                        {
                            lbl_LiftSt1_3.Text = "후진";
                        }
                        else
                        {
                            lbl_LiftSt1_3.Text = "전진";
                        }

                        if (Global_Class.BitStatus(DevSt->Lift_DisPosition.St_1, 2))
                        {
                            lbl_LiftSt1_2.Text = "감속";
                        }
                        else
                        {
                            lbl_LiftSt1_2.Text = "아님";
                        }
                        if (Global_Class.BitStatus(DevSt->Lift_DisPosition.St_1, 1))
                        {
                            lbl_LiftSt1_1.Text = "가속";
                        }
                        else
                        {
                            lbl_LiftSt1_1.Text = "아님";
                        }
                    }
                    else
                    {
                        lbl_LiftSt1_3.Text = "정지";
                        lbl_LiftSt1_2.Text = "아님";
                        lbl_LiftSt1_1.Text = "아님";
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

                    lbl_LiftAlarmCode_Invertor.Text = String.Format("{0}-{1}", DevSt->Lift_DisPosition.AlarmCode1, DevSt->Lift_DisPosition.AlarmCode2);
                    

                    lbl_Lift_Position.Text = String.Format("{0}", DevSt->Lift_DisPosition.Now_Position);
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

                    if (Global_Class.BitStatus(DevSt->Cage_St.St_1, 1))
                    {
                        lbl_CageSt1_1.Text = "감지";
                        lbl_CageSt1_1.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_CageSt1_1.Text = "미감지";
                        lbl_CageSt1_1.BackColor = Color.Silver;
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

                    if (Global_Class.BitStatus(DevSt->Cage_St.St_3, 0))
                    {
                        lbl_CageSt3_0.Text = "ON";
                        lbl_CageSt3_0.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_CageSt3_0.Text = "OFF";
                        lbl_CageSt3_0.BackColor = Color.Silver;
                    }

                    if (Global_Class.BitStatus(DevSt->Cage_St.St_3, 1))
                    {
                        lbl_CageSt3_1.Text = "ON";
                        lbl_CageSt3_1.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_CageSt3_1.Text = "OFF";
                        lbl_CageSt3_1.BackColor = Color.Silver;
                    }

                    if (Global_Class.BitStatus(DevSt->Cage_St.St_3, 2))
                    {
                        lbl_CageSt3_2.Text = "ON";
                        lbl_CageSt3_2.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_CageSt3_2.Text = "OFF";
                        lbl_CageSt3_2.BackColor = Color.Silver;
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


        private unsafe void Display_Sub_TaskList()
        {
            lbl_TaskJobSt.Text = "";
            lbl_TaskJobNumber.Text = "";

        }
        private unsafe void Display_Sub_DIO()
        {
            byte SelectDIindex = 0;
            byte SelectDOindex = 0;
            byte Loop;
            byte ByteIndex, BitIndex;

            fixed (VEXI_DEFS.TEMS_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.ems_REC_EMSSt)
            {
                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
                    //I/O
                    if (rb_DIO_DigitalIn_1.Checked) SelectDIindex = 1;
                    if (rb_DIO_DigitalIn_2.Checked) SelectDIindex = 2;
                    if (rb_DIO_DigitalOut_1.Checked) SelectDOindex = 4;
                    if (rb_DIO_DigitalOut_2.Checked) SelectDOindex = 5;


                    switch (SelectDIindex)
                    {
                        case 1:
                            for (Loop = 1; Loop <= 38; Loop++)
                            {
                                if (Loop > ConstClass.EMS_DI_Names.GetLength(0))
                                {
                                    lbl_DI_Title[Loop - 1].BackColor = Color.Gray;
                                    lbl_DI_Title[Loop - 1].Text = "";

                                    lbl_DI_ST[Loop - 1].BackColor = Color.Gray;
                                    lbl_DI_ST[Loop - 1].Text = "";
                                }
                                else
                                {
                                    lbl_DI_Title[Loop - 1].BackColor = System.Drawing.SystemColors.Highlight;
                                    lbl_DI_Title[Loop - 1].Text = ConstClass.EMS_DI_Names[Loop - 1, 0];
                                }
                            }
                            break;
                        case 2:
                            for (Loop = 39; Loop <= 76; Loop++)
                            {
                                if (Loop > ConstClass.EMS_DI_Names.GetLength(0))
                                {
                                    lbl_DI_Title[Loop - 39].BackColor = Color.Gray;
                                    lbl_DI_Title[Loop - 39].Text = "";

                                    lbl_DI_ST[Loop - 39].BackColor = Color.Gray;
                                    lbl_DI_ST[Loop - 39].Text = "";
                                }
                                else
                                {
                                    lbl_DI_Title[Loop - 39].BackColor = System.Drawing.SystemColors.Highlight;
                                    lbl_DI_Title[Loop - 39].Text = ConstClass.EMS_DI_Names[Loop - 1, 0];
                                }
                            }
                            break;
                            //case 3:
                            //    for (Loop = 77; Loop <= 114; Loop++)
                            //    {
                            //        if (Loop > ConstClass.EMS_DI_Names.GetLength(0))
                            //        {
                            //            lbl_DI_Title[Loop - 77].BackColor = Color.Gray;
                            //            lbl_DI_Title[Loop - 77].Text = "";

                            //            lbl_DI_ST[Loop - 77].BackColor = Color.Gray;
                            //            lbl_DI_ST[Loop - 77].Text = "";
                            //        }
                            //        else
                            //        {
                            //            lbl_DI_Title[Loop - 77].BackColor = System.Drawing.SystemColors.Highlight;
                            //            lbl_DI_Title[Loop - 77].Text = ConstClass.EMS_DI_Names[Loop - 1, 0];
                            //        }
                            //    }
                            //    break;
                    }
                    switch (SelectDOindex)
                    {

                        case 4:
                            for (Loop = 1; Loop <= 38; Loop++)
                            {
                                if (Loop > ConstClass.EMS_DO_Names.GetLength(0))
                                {
                                    lbl_DO_Title[Loop - 1].BackColor = Color.Gray;
                                    lbl_DO_Title[Loop - 1].Text = "";

                                    lbl_DO_ST[Loop - 1].BackColor = Color.Gray;
                                    lbl_DO_ST[Loop - 1].Text = "";
                                } else
                                {
                                    lbl_DO_Title[Loop - 1].BackColor = Color.DarkOliveGreen;
                                    lbl_DO_Title[Loop - 1].Text = ConstClass.EMS_DO_Names[Loop - 1, 0];
                                }
                            }
                            break;
                        case 5:
                            for (Loop = 39; Loop <= 76; Loop++)
                            {
                                if (Loop > ConstClass.EMS_DO_Names.GetLength(0))
                                {
                                    lbl_DO_Title[Loop - 39].BackColor = Color.Gray;
                                    lbl_DO_Title[Loop - 39].Text = "";

                                    lbl_DO_ST[Loop - 39].BackColor = Color.Gray;
                                    lbl_DO_ST[Loop - 39].Text = "";
                                }
                                else
                                {
                                    lbl_DO_Title[Loop - 39].BackColor = Color.DarkOliveGreen;
                                    lbl_DO_Title[Loop - 39].Text = ConstClass.EMS_DO_Names[Loop - 1, 0];
                                }
                            }
                            break;
                    }


                    switch (SelectDIindex)
                    {
                        case 1:
                            for (Loop = 1; Loop <= 38; Loop++)
                            {
                                ByteIndex = (byte)((Loop - 1) / 8);
                                BitIndex = (byte)((Loop - 1) % 8);

                                if (Loop <= ConstClass.EMS_DI_Names.GetLength(0))
                                {
                                    if (Global_Class.BitStatus(DevSt->IO_Digital_IN[ByteIndex], BitIndex))
                                    {
                                        lbl_DI_ST[Loop - 1].BackColor = Color.Yellow;
                                        lbl_DI_ST[Loop - 1].Text = "ON";
                                    }
                                    else
                                    {
                                        lbl_DI_ST[Loop - 1].BackColor = Color.Silver;
                                        lbl_DI_ST[Loop - 1].Text = "OFF";
                                    }
                                }

                            }
                            break;
                        case 2:
                            for (Loop = 39; Loop <= 76; Loop++)
                            {
                                ByteIndex = (byte)((Loop - 1) / 8);
                                BitIndex = (byte)((Loop - 1) % 8);

                                if (Loop <= ConstClass.EMS_DI_Names.GetLength(0))
                                {

                                    if (Global_Class.BitStatus(DevSt->IO_Digital_IN[ByteIndex], BitIndex))
                                    {
                                        lbl_DI_ST[Loop - 39].BackColor = Color.Yellow;
                                        lbl_DI_ST[Loop - 39].Text = "ON";
                                    }
                                    else
                                    {
                                        lbl_DI_ST[Loop - 39].BackColor = Color.Silver;
                                        lbl_DI_ST[Loop - 39].Text = "OFF";
                                    }
                                }
                            }
                            break;
                            //case 3:
                            //    for (Loop = 77; Loop <= 114; Loop++)
                            //    {
                            //        ByteIndex = (byte)((Loop - 1) / 8);
                            //        BitIndex = (byte)((Loop - 1) % 8);

                            //        if (Loop <= ConstClass.EMS_DI_Names.GetLength(0))
                            //        {

                            //            if (Global_Class.BitStatus(DevSt->IO_Digital_IN[ByteIndex], BitIndex))
                            //            {
                            //                lbl_DI_ST[Loop - 77].BackColor = Color.Yellow;
                            //                lbl_DI_ST[Loop - 77].Text = "ON";
                            //            }
                            //            else
                            //            {
                            //                lbl_DI_ST[Loop - 77].BackColor = Color.Silver;
                            //                lbl_DI_ST[Loop - 77].Text = "OFF";
                            //            }
                            //        }
                            //    }
                            //    break;
                    }
                    switch (SelectDOindex)
                    {
                        case 4:
                            for (Loop = 1; Loop <= 38; Loop++)
                            {
                                ByteIndex = (byte)((Loop - 1) / 8);
                                BitIndex = (byte)((Loop - 1) % 8);

                                if (Loop <= ConstClass.EMS_DO_Names.GetLength(0))
                                {
                                    if (Global_Class.BitStatus(DevSt->IO_Digital_OUT[ByteIndex], BitIndex))
                                    {
                                        lbl_DO_ST[Loop - 1].BackColor = Color.Yellow;
                                        lbl_DO_ST[Loop - 1].Text = "ON";
                                    }
                                    else
                                    {
                                        lbl_DO_ST[Loop - 1].BackColor = Color.Silver;
                                        lbl_DO_ST[Loop - 1].Text = "OFF";
                                    }
                                }
                            }
                            break;
                        case 5:
                            for (Loop = 39; Loop <= 76; Loop++)
                            {
                                ByteIndex = (byte)((Loop - 1) / 8);
                                BitIndex = (byte)((Loop - 1) % 8);


                                if (Loop <= ConstClass.EMS_DO_Names.GetLength(0))
                                {
                                    if (Global_Class.BitStatus(DevSt->IO_Digital_OUT[ByteIndex], BitIndex))
                                    {
                                        lbl_DO_ST[Loop - 39].BackColor = Color.Yellow;
                                        lbl_DO_ST[Loop - 39].Text = "ON";
                                    }
                                    else
                                    {
                                        lbl_DO_ST[Loop - 39].BackColor = Color.Silver;
                                        lbl_DO_ST[Loop - 39].Text = "OFF";
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }


        private unsafe void Display_UpperSystem()
        {
            if (cbStation_Interlock.SelectedIndex < 0) return;


            fixed (VEXI_DEFS.TEMS_REC_Interlock* InterlockPtr = &form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.InterlockData)
            {
                byte TmpStationIndex = (byte)cbStation_Interlock.SelectedIndex;

                //label2.Text = TmpStationIndex.ToString();

                if (Global_Class.BitStatus((InterlockPtr + TmpStationIndex)->CVToEMS, 0))
                {
                    lblInterlockIn0.Text = "ON";
                    lblInterlockIn0.BackColor = Color.Yellow;
                    lblInterlockIn0.ForeColor = Color.Black;
                }
                else
                {
                    lblInterlockIn0.Text = "OFF";
                    lblInterlockIn0.BackColor = Color.Silver;
                    lblInterlockIn0.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus((InterlockPtr + TmpStationIndex)->CVToEMS, 1))
                {
                    lblInterlockIn1.Text = "ON";
                    lblInterlockIn1.BackColor = Color.Yellow;
                    lblInterlockIn1.ForeColor = Color.Black;
                }
                else
                {
                    lblInterlockIn1.Text = "OFF";
                    lblInterlockIn1.BackColor = Color.Silver;
                    lblInterlockIn1.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus((InterlockPtr + TmpStationIndex)->CVToEMS, 2))
                {
                    lblInterlockIn2.Text = "ON";
                    lblInterlockIn2.BackColor = Color.Yellow;
                    lblInterlockIn2.ForeColor = Color.Black;
                }
                else
                {
                    lblInterlockIn2.Text = "OFF";
                    lblInterlockIn2.BackColor = Color.Silver;
                    lblInterlockIn2.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus((InterlockPtr + TmpStationIndex)->CVToEMS, 3))
                {
                    lblInterlockIn3.Text = "ON";
                    lblInterlockIn3.BackColor = Color.Yellow;
                    lblInterlockIn3.ForeColor = Color.Black;
                }
                else
                {
                    lblInterlockIn3.Text = "OFF";
                    lblInterlockIn3.BackColor = Color.Silver;
                    lblInterlockIn3.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus((InterlockPtr + TmpStationIndex)->CVToEMS, 4))
                {
                    lblInterlockIn4.Text = "ON";
                    lblInterlockIn4.BackColor = Color.Yellow;
                    lblInterlockIn4.ForeColor = Color.Black;
                }
                else
                {
                    lblInterlockIn4.Text = "OFF";
                    lblInterlockIn4.BackColor = Color.Silver;
                    lblInterlockIn4.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus((InterlockPtr + TmpStationIndex)->CVToEMS, 5))
                {
                    lblInterlockIn5.Text = "ON";
                    lblInterlockIn5.BackColor = Color.Yellow;
                    lblInterlockIn5.ForeColor = Color.Black;
                }
                else
                {
                    lblInterlockIn5.Text = "OFF";
                    lblInterlockIn5.BackColor = Color.Silver;
                    lblInterlockIn5.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus((InterlockPtr + TmpStationIndex)->CVToEMS, 6))
                {
                    lblInterlockIn6.Text = "ON";
                    lblInterlockIn6.BackColor = Color.Yellow;
                    lblInterlockIn6.ForeColor = Color.Black;
                }
                else
                {
                    lblInterlockIn6.Text = "OFF";
                    lblInterlockIn6.BackColor = Color.Silver;
                    lblInterlockIn6.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus((InterlockPtr + TmpStationIndex)->CVToEMS, 7))
                {
                    lblInterlockIn7.Text = "ON";
                    lblInterlockIn7.BackColor = Color.Yellow;
                    lblInterlockIn7.ForeColor = Color.Black;
                }
                else
                {
                    lblInterlockIn7.Text = "OFF";
                    lblInterlockIn7.BackColor = Color.Silver;
                    lblInterlockIn7.ForeColor = Color.Black;
                }


                if (Global_Class.BitStatus((InterlockPtr + TmpStationIndex)->EMSToCV, 0))
                {
                    lblInterlockOut0.Text = "ON";
                    lblInterlockOut0.BackColor = Color.Yellow;
                    lblInterlockOut0.ForeColor = Color.Black;
                }
                else
                {
                    lblInterlockOut0.Text = "OFF";
                    lblInterlockOut0.BackColor = Color.Silver;
                    lblInterlockOut0.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus((InterlockPtr + TmpStationIndex)->EMSToCV, 1))
                {
                    lblInterlockOut1.Text = "ON";
                    lblInterlockOut1.BackColor = Color.Yellow;
                    lblInterlockOut1.ForeColor = Color.Black;
                }
                else
                {
                    lblInterlockOut1.Text = "OFF";
                    lblInterlockOut1.BackColor = Color.Silver;
                    lblInterlockOut1.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus((InterlockPtr + TmpStationIndex)->EMSToCV, 2))
                {
                    lblInterlockOut2.Text = "ON";
                    lblInterlockOut2.BackColor = Color.Yellow;
                    lblInterlockOut2.ForeColor = Color.Black;
                }
                else
                {
                    lblInterlockOut2.Text = "OFF";
                    lblInterlockOut2.BackColor = Color.Silver;
                    lblInterlockOut2.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus((InterlockPtr + TmpStationIndex)->EMSToCV, 3))
                {
                    lblInterlockOut3.Text = "ON";
                    lblInterlockOut3.BackColor = Color.Yellow;
                    lblInterlockOut3.ForeColor = Color.Black;
                }
                else
                {
                    lblInterlockOut3.Text = "OFF";
                    lblInterlockOut3.BackColor = Color.Silver;
                    lblInterlockOut3.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus((InterlockPtr + TmpStationIndex)->EMSToCV, 4))
                {
                    lblInterlockOut4.Text = "ON";
                    lblInterlockOut4.BackColor = Color.Yellow;
                    lblInterlockOut4.ForeColor = Color.Black;
                }
                else
                {
                    lblInterlockOut4.Text = "OFF";
                    lblInterlockOut4.BackColor = Color.Silver;
                    lblInterlockOut4.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus((InterlockPtr + TmpStationIndex)->EMSToCV, 5))
                {
                    lblInterlockOut5.Text = "ON";
                    lblInterlockOut5.BackColor = Color.Yellow;
                    lblInterlockOut5.ForeColor = Color.Black;
                }
                else
                {
                    lblInterlockOut5.Text = "OFF";
                    lblInterlockOut5.BackColor = Color.Silver;
                    lblInterlockOut5.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus((InterlockPtr + TmpStationIndex)->EMSToCV, 6))
                {
                    lblInterlockOut6.Text = "ON";
                    lblInterlockOut6.BackColor = Color.Yellow;
                    lblInterlockOut6.ForeColor = Color.Black;
                }
                else
                {
                    lblInterlockOut6.Text = "OFF";
                    lblInterlockOut6.BackColor = Color.Silver;
                    lblInterlockOut6.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus((InterlockPtr + TmpStationIndex)->EMSToCV, 7))
                {
                    lblInterlockOut7.Text = "ON";
                    lblInterlockOut7.BackColor = Color.Yellow;
                    lblInterlockOut7.ForeColor = Color.Black;
                }
                else
                {
                    lblInterlockOut7.Text = "OFF";
                    lblInterlockOut7.BackColor = Color.Silver;
                    lblInterlockOut7.ForeColor = Color.Black;
                }
            }
        }

        private unsafe void Display_Lidar()
        {
            fixed (VEXI_DEFS.TEMS_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.ems_REC_EMSSt)
            {
                lbl_FrontLidar_Area.Text = String.Format("{0}", DevSt->front_LidarArea);

                if (Global_Class.BitStatus(DevSt->front_LidarSt, 7))
                {
                    lbl_FrontLidar_St7.Text = "사용";
                    lbl_FrontLidar_St7.BackColor = Color.White;
                    lbl_FrontLidar_St7.ForeColor = Color.Black;
                }
                else
                {
                    lbl_FrontLidar_St7.Text = "미사용";
                    lbl_FrontLidar_St7.BackColor = Color.White;
                    lbl_FrontLidar_St7.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus(DevSt->front_LidarSt, 6))
                {
                    lbl_FrontLidar_St6.Text = "이상";
                    lbl_FrontLidar_St6.BackColor = Color.Red;
                    lbl_FrontLidar_St6.ForeColor = Color.White;
                }
                else
                {
                    lbl_FrontLidar_St6.Text = "정상";
                    lbl_FrontLidar_St6.BackColor = Color.Lime;
                    lbl_FrontLidar_St6.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus(DevSt->front_LidarSt, 2))
                {
                    lbl_FrontLidar_St2.Text = "감지";
                    lbl_FrontLidar_St2.BackColor = Color.Yellow;
                    lbl_FrontLidar_St2.ForeColor = Color.Black;
                }
                else
                {
                    lbl_FrontLidar_St2.Text = "정상";
                    lbl_FrontLidar_St2.BackColor = Color.Silver;
                    lbl_FrontLidar_St2.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus(DevSt->front_LidarSt, 1))
                {
                    lbl_FrontLidar_St1.Text = "감지";
                    lbl_FrontLidar_St1.BackColor = Color.Yellow;
                    lbl_FrontLidar_St1.ForeColor = Color.Black;
                }
                else
                {
                    lbl_FrontLidar_St1.Text = "정상";
                    lbl_FrontLidar_St1.BackColor = Color.Silver;
                    lbl_FrontLidar_St1.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus(DevSt->front_LidarSt, 0))
                {
                    lbl_FrontLidar_St0.Text = "감지";
                    lbl_FrontLidar_St0.BackColor = Color.Yellow;
                    lbl_FrontLidar_St0.ForeColor = Color.Black;
                }
                else
                {
                    lbl_FrontLidar_St0.Text = "정상";
                    lbl_FrontLidar_St0.BackColor = Color.Silver;
                    lbl_FrontLidar_St0.ForeColor = Color.Black;
                }



                lbl_RearLidar_Area.Text = String.Format("{0}", DevSt->Rear_LidarArea);

                if (Global_Class.BitStatus(DevSt->Rear_LidarSt, 7))
                {
                    lbl_RearLidar_St7.Text = "사용";
                    lbl_RearLidar_St7.BackColor = Color.White;
                    lbl_RearLidar_St7.ForeColor = Color.Black;
                }
                else
                {
                    lbl_RearLidar_St7.Text = "미사용";
                    lbl_RearLidar_St7.BackColor = Color.White;
                    lbl_RearLidar_St7.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus(DevSt->Rear_LidarSt, 6))
                {
                    lbl_RearLidar_St6.Text = "이상";
                    lbl_RearLidar_St6.BackColor = Color.Red;
                    lbl_RearLidar_St6.ForeColor = Color.White;
                }
                else
                {
                    lbl_RearLidar_St6.Text = "정상";
                    lbl_RearLidar_St6.BackColor = Color.Lime;
                    lbl_RearLidar_St6.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus(DevSt->Rear_LidarSt, 2))
                {
                    lbl_RearLidar_St2.Text = "감지";
                    lbl_RearLidar_St2.BackColor = Color.Yellow;
                    lbl_RearLidar_St2.ForeColor = Color.Black;
                }
                else
                {
                    lbl_RearLidar_St2.Text = "정상";
                    lbl_RearLidar_St2.BackColor = Color.Silver;
                    lbl_RearLidar_St2.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus(DevSt->Rear_LidarSt, 1))
                {
                    lbl_RearLidar_St1.Text = "감지";
                    lbl_RearLidar_St1.BackColor = Color.Yellow;
                    lbl_RearLidar_St1.ForeColor = Color.Black;
                }
                else
                {
                    lbl_RearLidar_St1.Text = "정상";
                    lbl_RearLidar_St1.BackColor = Color.Silver;
                    lbl_RearLidar_St1.ForeColor = Color.Black;
                }

                if (Global_Class.BitStatus(DevSt->Rear_LidarSt, 0))
                {
                    lbl_RearLidar_St0.Text = "감지";
                    lbl_RearLidar_St0.BackColor = Color.Yellow;
                    lbl_RearLidar_St0.ForeColor = Color.Black;
                }
                else
                {
                    lbl_RearLidar_St0.Text = "정상";
                    lbl_RearLidar_St0.BackColor = Color.Silver;
                    lbl_RearLidar_St0.ForeColor = Color.Black;
                }
            }
        }


        private unsafe void Display_Ref_St()
        {
            fixed (VEXI_DEFS.TEMS_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.ems_REC_EMSSt)
            {

                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
                    //Display_Collision
                    lbl_Front_Collision_EMSID.Text = String.Format("{0}", DevSt->BeforeCar_Collision.EMS_ID);
                    lbl_Front_Collision_RxTime.Text = String.Format("{0}", DevSt->BeforeCar_Collision.RxTime);
                    lbl_Front_Collision_GapDistance.Text = String.Format("{0}", DevSt->BeforeCar_Collision.GapOtherCar);
                    lbl_Front_Collision_StopDistance.Text = String.Format("{0}", DevSt->BeforeCar_Collision.StopDistance);
                    lbl_Front_Collision_StartDistance.Text = String.Format("{0}", DevSt->BeforeCar_Collision.StartDistance);
                    switch (DevSt->BeforeCar_Collision.PostionType)
                    {
                        case 0 :
                            lbl_Front_Collision_AreaType.Text = "직선";
                            lbl_Front_Collision_MyAreaType.Text = "직선"; 
                            break;
                        case 1:
                            lbl_Front_Collision_AreaType.Text = "직선";
                            lbl_Front_Collision_MyAreaType.Text = "곡선"; 
                            break;
                        case 2:
                            lbl_Front_Collision_AreaType.Text = "곡선";
                            lbl_Front_Collision_MyAreaType.Text = "직선"; 
                            break;
                        case 3:
                            lbl_Front_Collision_AreaType.Text = "곡선";
                            lbl_Front_Collision_MyAreaType.Text = "곡선"; 
                            break;
                    }
                    if (Global_Class.BitStatus(DevSt->BeforeCar_Collision.DriveSt, 2))
                    {
                        lbl_Front_Collision_DriveSt_2.Text = "이상";
                        lbl_Front_Collision_DriveSt_2.BackColor = Color.Red;
                        lbl_Front_Collision_DriveSt_2.ForeColor = Color.White;

                    } else
                    {
                        lbl_Front_Collision_DriveSt_2.Text = "정상";
                        lbl_Front_Collision_DriveSt_2.BackColor = Color.Lime;
                        lbl_Front_Collision_DriveSt_2.ForeColor = Color.Black;
                    }
                    switch (DevSt->BeforeCar_Collision.DriveSt & 0x03)
                    {
                        case 0:
                            lbl_Front_Collision_DriveSt_1.Text = "정지";
                            break;
                        case 1:
                            lbl_Front_Collision_DriveSt_1.Text = "전진";
                            break;
                        case 2:
                            lbl_Front_Collision_DriveSt_1.Text = "후진";
                            break;
                    }
                    lbl_Front_Collision_Speed.Text = String.Format("{0}", DevSt->BeforeCar_Collision.Speed);
                    lbl_Front_Collision_NowPosition.Text = String.Format("{0}", DevSt->BeforeCar_Collision.NowPosition);
                    lbl_Front_Collision_DestPosition.Text = String.Format("{0}", DevSt->BeforeCar_Collision.DestPosition);

                    lbl_Rear_Collision_EMSID.Text = String.Format("{0}", DevSt->AfterCar_Collision.EMS_ID);
                    lbl_Rear_Collision_RxTime.Text = String.Format("{0}", DevSt->AfterCar_Collision.RxTime);
                    lbl_Rear_Collision_GapDistance.Text = String.Format("{0}", DevSt->AfterCar_Collision.GapOtherCar);
                    lbl_Rear_Collision_StopDistance.Text = String.Format("{0}", DevSt->AfterCar_Collision.StopDistance);
                    lbl_Rear_Collision_StartDistance.Text = String.Format("{0}", DevSt->AfterCar_Collision.StartDistance);
                    switch (DevSt->AfterCar_Collision.PostionType)
                    {
                        case 0:
                            lbl_Rear_Collision_AreaType.Text = "직선";
                            lbl_Rear_Collision_MyAreaType.Text = "직선";
                            break;
                        case 1:
                            lbl_Rear_Collision_AreaType.Text = "직선";
                            lbl_Rear_Collision_MyAreaType.Text = "곡선";
                            break;
                        case 2:
                            lbl_Rear_Collision_AreaType.Text = "곡선";
                            lbl_Rear_Collision_MyAreaType.Text = "직선";
                            break;
                        case 3:
                            lbl_Rear_Collision_AreaType.Text = "곡선";
                            lbl_Rear_Collision_MyAreaType.Text = "곡선";
                            break;
                    }
                }
                if (Global_Class.BitStatus(DevSt->AfterCar_Collision.DriveSt, 2))
                {
                    lbl_Rear_Collision_DriveSt_2.Text = "이상";
                    lbl_Rear_Collision_DriveSt_2.BackColor = Color.Red;
                    lbl_Rear_Collision_DriveSt_2.ForeColor = Color.White;

                }
                else
                {
                    lbl_Rear_Collision_DriveSt_2.Text = "정상";
                    lbl_Rear_Collision_DriveSt_2.BackColor = Color.Lime;
                    lbl_Rear_Collision_DriveSt_2.ForeColor = Color.Black;
                }
                switch (DevSt->AfterCar_Collision.DriveSt & 0x03)
                {
                    case 0:
                        lbl_Rear_Collision_DriveSt_1.Text = "정지";
                        break;
                    case 1:
                        lbl_Rear_Collision_DriveSt_1.Text = "전진";
                        break;
                    case 2:
                        lbl_Rear_Collision_DriveSt_1.Text = "후진";
                        break;
                }
                lbl_Rear_Collision_Speed.Text = String.Format("{0}", DevSt->AfterCar_Collision.Speed);
                lbl_Rear_Collision_NowPosition.Text = String.Format("{0}", DevSt->AfterCar_Collision.NowPosition);
                lbl_Rear_Collision_DestPosition.Text = String.Format("{0}", DevSt->AfterCar_Collision.DestPosition);


                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
                    lbl_DriveAreaInfo_AreaNo.Text = String.Format("{0}", DevSt->DriveAreaInfo.AreaNo);
                    switch (DevSt->DriveAreaInfo.Area_Type & 0x03)
                    {
                        case 0: lbl_DriveAreaInfo_AreaType.Text = "직선"; break;
                        case 1: lbl_DriveAreaInfo_AreaType.Text = "직선 HOME"; break;
                        case 2: lbl_DriveAreaInfo_AreaType.Text = "직선 END"; break;
                        case 3: lbl_DriveAreaInfo_AreaType.Text = "곡선"; break;
                    }


                    lbl_DriveAreaInfo_StartMM.Text = string.Format("{0}", DevSt->DriveAreaInfo.Start_MM);
                    lbl_DriveAreaInfo_EndMM.Text = string.Format("{0}", DevSt->DriveAreaInfo.End_MM);
                    lbl_DriveAreaInfo_MaxSpeed.Text = string.Format("{0:0.0}", (double)DevSt->DriveAreaInfo.MaxSpeed / 10);
                    lbl_DriveAreaInfo_PrevArea.Text = string.Format("{0}", DevSt->DriveAreaInfo.PrevAreaIndex);
                    lbl_DriveAreaInfo_NextArea.Text = string.Format("{0}", DevSt->DriveAreaInfo.NextAreaIndex);
                    if (DevSt->DriveAreaInfo.Sensorindex == 0)
                    {
                        lbl_DriveAreaInfo_SensorIndex.Text = "OFF";
                    }
                    else
                    {
                        lbl_DriveAreaInfo_SensorIndex.Text = string.Format("AREA {0}", DevSt->DriveAreaInfo.Sensorindex);
                    }
                    if ((DevSt->DriveAreaInfo.Region & 0x01) == 0x00)
                    {
                        lbl_DriveAreaInfo_Region_0.BackColor = Color.Silver;
                    }
                    else
                    {
                        lbl_DriveAreaInfo_Region_0.BackColor = Color.Yellow;
                    }
                    if ((DevSt->DriveAreaInfo.Region & 0x02) == 0x00)
                    {
                        lbl_DriveAreaInfo_Region_1.BackColor = Color.Silver;
                    }
                    else
                    {
                        lbl_DriveAreaInfo_Region_1.BackColor = Color.Yellow;
                    }
                    if ((DevSt->DriveAreaInfo.Region & 0x04) == 0x00)
                    {
                        lbl_DriveAreaInfo_Region_2.BackColor = Color.Silver;
                    }
                    else
                    {
                        lbl_DriveAreaInfo_Region_2.BackColor = Color.Yellow;
                    }
                    lbl_Coll_Stop.Text = string.Format("{0}", DevSt->DriveAreaInfo.Coll_Stop);
                    lbl_Coll_Start.Text = string.Format("{0}", DevSt->DriveAreaInfo.Coll_Start);



                    lbl_Now_Station.Text = String.Format("S{0}", DevSt->Drive_Position.PointRec.Station);
                    if (Global_Class.BitStatus(DevSt->Drive_Position.StationInfo, 0))
                    {
                        lbl_Now_Station_LoadingCan.Text = "가능";
                    }
                    else
                    {
                        lbl_Now_Station_LoadingCan.Text = "불가";
                    }

                    if (Global_Class.BitStatus(DevSt->Drive_Position.StationInfo, 1))
                    {
                        lbl_Now_Station_UnloadingCan.Text = "가능";
                    }
                    else
                    {
                        lbl_Now_Station_UnloadingCan.Text = "불가";
                    }

                    if (Global_Class.BitStatus(DevSt->Drive_Position.StationInfo, 2))
                    {
                        lbl_Now_Station_LoadingCan.Text = "가능";
                    }
                    else
                    {
                        lbl_Now_Station_LoadingCan.Text = "불가";
                    }
                }
            }

        }


        private unsafe void Display_RemocongKey_St()
        {
            fixed (VEXI_DEFS.TEMS_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.ems_REC_EMSSt)
            {
                if ((DevSt->KEYIN_St_1 & 0x40) != 0)
                {
                    lbl_Key_1_6.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_Key_1_6.BackColor = Color.Silver;
                }
                if ((DevSt->KEYIN_St_1 & 0x20) != 0)
                {
                    lbl_Key_1_5.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_Key_1_5.BackColor = Color.Silver;
                }
                if ((DevSt->KEYIN_St_1 & 0x10) != 0)
                {
                    lbl_Key_1_4.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_Key_1_4.BackColor = Color.Silver;
                }
                if ((DevSt->KEYIN_St_1 & 0x08) != 0)
                {
                    lbl_Key_1_3.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_Key_1_3.BackColor = Color.Silver;
                }
                if ((DevSt->KEYIN_St_1 & 0x04) != 0)
                {
                    lbl_Key_1_2.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_Key_1_2.BackColor = Color.Silver;
                }
                if ((DevSt->KEYIN_St_1 & 0x02) != 0)
                {
                    lbl_Key_1_1.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_Key_1_1.BackColor = Color.Silver;
                }
                if ((DevSt->KEYIN_St_1 & 0x01) != 0)
                {
                    lbl_Key_1_0.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_Key_1_0.BackColor = Color.Silver;
                }



                if ((DevSt->KEYIN_St_2 & 0x20) != 0)
                {
                    lbl_Key_2_5.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_Key_2_5.BackColor = Color.Silver;
                }
                if ((DevSt->KEYIN_St_2 & 0x10) != 0)
                {
                    lbl_Key_2_4.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_Key_2_4.BackColor = Color.Silver;
                }
                if ((DevSt->KEYIN_St_2 & 0x08) != 0)
                {
                    lbl_Key_2_3.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_Key_2_3.BackColor = Color.Silver;
                }
                if ((DevSt->KEYIN_St_2 & 0x04) != 0)
                {
                    lbl_Key_2_2.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_Key_2_2.BackColor = Color.Silver;
                }
                if ((DevSt->KEYIN_St_2 & 0x02) != 0)
                {
                    lbl_Key_2_1.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_Key_2_1.BackColor = Color.Silver;
                }
                if ((DevSt->KEYIN_St_2 & 0x01) != 0)
                {
                    lbl_Key_2_0.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_Key_2_0.BackColor = Color.Silver;
                }



            }
        }

        public unsafe void Display_DevSt()
        {
            //if (true)
            if ((form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) && (form_Main.COMMDataManager.CommSt != 0))
            {
                Display_EMS_BasicSt();
                Display_EMS_JobSt();
                Display_EMS_St();
                Display_RemocongKey_St();
                Display_Ref_St();
                Display_UpperSystem();
                Display_Lidar();
                Display_Sub_DIO();
              
            } else
            {
                Display_Init();
                Display_Sub_DIO();
            }
        }
        #endregion




        private unsafe void button1_Click(object sender, EventArgs e)
        {
            form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.IO_Digital_IN[0] = 0x01;
            form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.IO_Digital_IN[1] = 0x02;
            form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.IO_Digital_IN[2] = 0x04;
            form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.IO_Digital_IN[3] = 0x01;
            form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.IO_Digital_IN[4] = 0x10;
            form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.IO_Digital_IN[5] = 0x20;
            form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.IO_Digital_IN[6] = 0x40;
            form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.IO_Digital_IN[7] = 0x80;

            form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.IO_Digital_OUT[0] = 0x01;
            form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.IO_Digital_OUT[1] = 0x02;
            form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.IO_Digital_OUT[2] = 0x04;
            form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.IO_Digital_OUT[3] = 0x08;
            form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.IO_Digital_OUT[4] = 0x10;
        }

        private void rb_DIO_DigitalIn_1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void cbStation_Interlock_Click(object sender, EventArgs e)
        {
         
        }

        private void cbStation_Interlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            Display_UpperSystem();
        }
    }

}

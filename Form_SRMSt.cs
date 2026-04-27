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
    public partial class Form_SRMSt : Form
    {
        public Form_Main form_Main;
        private Label[] lbl_DI_Title;
        private Label[] lbl_DI_ST;
        private Label[] lbl_DO_Title;
        private Label[] lbl_DO_ST;

        public Form_SRMSt()
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
                lbl_IO_Title_38,
                lbl_IO_Title_39,
                lbl_IO_Title_40,
                lbl_IO_Title_41,
                lbl_IO_Title_42,
                lbl_IO_Title_43,
                lbl_IO_Title_44
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
                lbl_IOSt_38,
                lbl_IOSt_39,
                lbl_IOSt_40,
                lbl_IOSt_41,
                lbl_IOSt_42,
                lbl_IOSt_43,
                lbl_IOSt_44
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
                lbl_O_Title_38,
                lbl_O_Title_39,
                lbl_O_Title_40,
                lbl_O_Title_41,
                lbl_O_Title_42,
                lbl_O_Title_43,
                lbl_O_Title_44
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
                lbl_OSt_38,
                lbl_OSt_39,
                lbl_OSt_40,
                lbl_OSt_41,
                lbl_OSt_42,
                lbl_OSt_43,
                lbl_OSt_44
            };
        }
        #region 컴포넌트 이벤트
        private void Form_SRMSt_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }
            Make_SiView();
            Display_DevSt();
        }
        #endregion

        #region 기능함수
        private void Make_SiView()
        {
            lv_SI.Items.Clear();

            ListViewItem item;

            item = lv_SI.Items.Add("Travel Setting Speed(m / min)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Lift Setting Speed(m / min)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Fork1 Setting Speed(m / min)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Fork2 Setting Speed(m / min)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Travel Setting Acceleration(mm / sec ^ 2)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Lift Setting Acceleration(mm / sec ^ 2)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Fork1 Setting Acceleration(mm / sec ^ 2)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Fork2 Setting Acceleration(mm / sec ^ 2)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Travel Setting Deceleration(mm / sec ^ 2)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Lift Setting Deceleration(mm / sec ^ 2)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Fork1 Setting Deceleration(mm / sec ^ 2)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Fork2 Setting Deceleration(mm / sec ^ 2)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Travel Setting Jeck(mm / sec ^ 3)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Lift Setting Jeck(mm / sec ^ 3)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Fork1 Setting Jeck(mm / sec ^ 3)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Fork2 Setting Jeck(mm / sec ^ 3)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Load Travel, Lift Moving Before Delay(ms)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Load Travel, Lift Moving After Delay(ms)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Load Fork Extend Moving Before Delay(ms)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Load Fork Extend Moving After Delay(ms)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Load Forking Lift Moving Before Delay(ms)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Load Forking Lift Moving After Delay(ms)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Load Fork Fold Moving Before Delay(ms)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Load Fork Fold Moving After Delay(ms)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Unload Travel, Lift Moving Before Delay(ms)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Unload Travel, Lift Moving After Delay(ms)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Unload Fork Extend Moving Before Delay(ms)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Unload Fork Extend Moving After Delay(ms)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Unload Forking Lift Moving Before Delay(ms)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Unload Forking Lift Moving After Delay(ms)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Unload Fork Fold Moving Before Delay(ms)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Unload Fork Fold Moving After Delay(ms)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Travel Moter Torque(%)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Lift Moter Torque(%)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Fork1 Moter Torque(%)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Fork2 Moter Torque(%)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("PLC Operation Time(Sec)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Travel Operation Time(Sec)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Lift Operation Time(Sec)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Fork1 Operation Time(Sec)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Fork2 Operation Time(Sec)"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Travel Brake Open Count"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Lift Brake Open Count"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Fork1 Brake Open Count"); item.SubItems.Add("");
            item = lv_SI.Items.Add("Fork2 Brake Open Count"); item.SubItems.Add("");

        }

        private unsafe void Display_Sub_ForkJob()
        {
            try
            {
                fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt)
                {
                    //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                    //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                    {
                        //반송 or Task : 수행하고 있는(실패상태 포함) 혹은 마지막 수행완료된 작업
                        if (DevSt->FF1_Job.Item_Do_Status == 4)
                        {
                            lbl_Fork1_Job.Text = string.Format("{0} / {1} (완료)", DevSt->FF1_Job.Item_JobNumber, DevSt->FF1_Job.ItemType);
                        }
                        else
                        {
                            lbl_Fork1_Job.Text = string.Format("{0} / {1}", DevSt->FF1_Job.Item_JobNumber, DevSt->FF1_Job.ItemType);
                        }

                        lbl_Fork1_Cmd.Text = Global_Class.UTIL_GetJobTextAsValue(DevSt->FF1_Job.Item_CMD_Code);

                            lbl_Fork1_From.Text = string.Format("S{0}-R{1}-B{2}-L{3}", DevSt->FF1_Job.Item_From.Station
                                                              , DevSt->FF1_Job.Item_From.Row
                                                              , DevSt->FF1_Job.Item_From.BayID
                                                              , DevSt->FF1_Job.Item_From.LevelID);
                        lbl_Fork1_To.Text = string.Format("S{0}-R{1}-B{2}-L{3}", DevSt->FF1_Job.Item_To.Station
                                                          , DevSt->FF1_Job.Item_To.Row
                                                          , DevSt->FF1_Job.Item_To.BayID
                                                          , DevSt->FF1_Job.Item_To.LevelID);
                        switch (DevSt->FF1_Job.Item_Do_Status)
                        {
                            case 0: lbl_Fork1_jobSt.Text = "지령없음"; break;
                            case 2: lbl_Fork1_jobSt.Text = "수행중"; break;
                            case 3: lbl_Fork1_jobSt.Text = "실패"; break;
                            case 4: lbl_Fork1_jobSt.Text = "완료"; break;
                            default: lbl_Fork1_jobSt.Text = string.Format("0x{0:X2}", DevSt->FF1_Job.Item_Do_Status); break;
                        }

                        lbl_Fork1_jobStep.Text = Global_Class.UTIL_GetSRMJobStepTextAsValue(DevSt->FF1_Job.Item_Do_Step);

                        //이동 : 수행하고 있는(실패상태 포함) 혹은 마지막 수행완료된 작업
                        if (DevSt->FF1_Job.Move_Do_Status == 4)
                        {
                            lbl_Fork1_MoveJob.Text = string.Format("{0} (완료)", DevSt->FF1_Job.Move_JobNumber);
                        }
                        else
                        {
                            lbl_Fork1_MoveJob.Text = string.Format("{0}", DevSt->FF1_Job.Move_JobNumber);
                        }
                        lbl_Fork1_MoveJob_To.Text = string.Format("S{0}-R{1}-B{2}-L{3}", DevSt->FF1_Job.Move_To.Station
                                                          , DevSt->FF1_Job.Move_To.Row
                                                          , DevSt->FF1_Job.Move_To.BayID
                                                          , DevSt->FF1_Job.Move_To.LevelID);
                        switch (DevSt->FF1_Job.Move_Do_Status)
                        {
                            case 0: lbl_Fork1_MoveJob_St.Text = "지령없음"; break;
                            case 2: lbl_Fork1_MoveJob_St.Text = "수행중"; break;
                            case 3: lbl_Fork1_MoveJob_St.Text = "실패"; break;
                            case 4: lbl_Fork1_MoveJob_St.Text = "완료"; break;
                            default: lbl_Fork1_MoveJob_St.Text = string.Format("0x{0:X2}", DevSt->FF1_Job.Move_Do_Status); break;
                        }

                        lbl_Fork1_MoveJob_Step.Text = Global_Class.UTIL_GetSRMJobStepTextAsValue(DevSt->FF1_Job.Move_Do_Step);

                        //반송 or Task : 수행하고 있는(실패상태 포함) 혹은 마지막 수행완료된 작업
                        if (DevSt->FF2_Job.Item_Do_Status == 4)
                        {
                            lbl_Fork2_Job.Text = string.Format("{0} / {1} (완료)", DevSt->FF2_Job.Item_JobNumber, DevSt->FF2_Job.ItemType);
                        }
                        else
                        {
                            lbl_Fork2_Job.Text = string.Format("{0} / {1}", DevSt->FF2_Job.Item_JobNumber, DevSt->FF2_Job.ItemType);
                        }

                        lbl_Fork2_Cmd.Text = Global_Class.UTIL_GetJobTextAsValue(DevSt->FF2_Job.Item_CMD_Code);

                        lbl_Fork2_From.Text = string.Format("S{0}-R{1}-B{2}-L{3}", DevSt->FF2_Job.Item_From.Station
                                                              , DevSt->FF2_Job.Item_From.Row
                                                              , DevSt->FF2_Job.Item_From.BayID
                                                              , DevSt->FF2_Job.Item_From.LevelID);
                        lbl_Fork2_To.Text = string.Format("S{0}-R{1}-B{2}-L{3}", DevSt->FF2_Job.Item_To.Station
                                                          , DevSt->FF2_Job.Item_To.Row
                                                          , DevSt->FF2_Job.Item_To.BayID
                                                          , DevSt->FF2_Job.Item_To.LevelID);
                        switch (DevSt->FF2_Job.Item_Do_Status)
                        {
                            case 0: lbl_Fork2_jobSt.Text = "지령없음"; break;
                            case 2: lbl_Fork2_jobSt.Text = "수행중"; break;
                            case 3: lbl_Fork2_jobSt.Text = "실패"; break;
                            case 4: lbl_Fork2_jobSt.Text = "완료"; break;
                            default: lbl_Fork2_jobSt.Text = string.Format("{0:X2}", DevSt->FF2_Job.Item_Do_Status); break;
                        }

                        lbl_Fork2_jobStep.Text = Global_Class.UTIL_GetSRMJobStepTextAsValue(DevSt->FF2_Job.Item_Do_Step);
                        //이동 : 수행하고 있는(실패상태 포함) 혹은 마지막 수행완료된 작업
                        if (DevSt->FF2_Job.Move_Do_Status == 4)
                        {
                            lbl_Fork2_MoveJob.Text = string.Format("{0} (완료)", DevSt->FF2_Job.Move_JobNumber);
                        }
                        else
                        {
                            lbl_Fork2_MoveJob.Text = string.Format("{0}", DevSt->FF2_Job.Move_JobNumber);
                        }
                        lbl_Fork2_MoveJob_To.Text = string.Format("S{0}-R{1}-B{2}-L{3}", DevSt->FF2_Job.Move_To.Station
                                                          , DevSt->FF2_Job.Move_To.Row
                                                          , DevSt->FF2_Job.Move_To.BayID
                                                          , DevSt->FF2_Job.Move_To.LevelID);
                        switch (DevSt->FF2_Job.Move_Do_Status)
                        {
                            case 0: lbl_Fork2_MoveJob_St.Text = "지령없음"; break;
                            case 2: lbl_Fork2_MoveJob_St.Text = "수행중"; break;
                            case 3: lbl_Fork2_MoveJob_St.Text = "실패"; break;
                            case 4: lbl_Fork2_MoveJob_St.Text = "완료"; break;
                            default: lbl_Fork2_MoveJob_St.Text = string.Format("{0:X2}", DevSt->FF2_Job.Move_Do_Status); break;
                        }

                        lbl_Fork2_MoveJob_Step.Text = Global_Class.UTIL_GetSRMJobStepTextAsValue(DevSt->FF2_Job.Move_Do_Step);
                    }
                }
            }
            catch
            {

            }
        }

        private unsafe void Display_InvErr()
        {
            try
            {
                fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt)
                {
                    //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                    //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                    {
                        
                        lbl_Dev_InvErr_DriveCode.Text = string.Format("{0}-{1}", DevSt->InvArr_M_Drive, DevSt->InvArr_S_Drive);
                        lbl_Dev_InvErr_LiftCode.Text = string.Format("{0}-{1}", DevSt->InvArr_M_Lift, DevSt->InvArr_S_Lift);
                        lbl_Dev_InvErr_Fork1Code.Text = string.Format("{0}-{1}", DevSt->InvArr_M_Fork1, DevSt->InvArr_S_Fork1);
                        lbl_Dev_InvErr_Fork2Code.Text = string.Format("{0}-{1}", DevSt->InvArr_M_Fork2, DevSt->InvArr_S_Fork2);

                        if (DevSt->InvArr_M_Drive > 0)
                        {
                            lbl_Dev_InvErr_DriveCode.ForeColor = Color.Red;
                        } else
                        {
                            lbl_Dev_InvErr_DriveCode.ForeColor = Color.Black;
                        }

                        if (DevSt->InvArr_M_Lift > 0)
                        {
                            lbl_Dev_InvErr_LiftCode.ForeColor = Color.Red;
                        }
                        else
                        {
                            lbl_Dev_InvErr_LiftCode.ForeColor = Color.Black;
                        }

                        if (DevSt->InvArr_M_Fork1 > 0)
                        {
                            lbl_Dev_InvErr_Fork1Code.ForeColor = Color.Red;
                        }
                        else
                        {
                            lbl_Dev_InvErr_Fork1Code.ForeColor = Color.Black;
                        }

                        if (DevSt->InvArr_M_Fork2 > 0)
                        {
                            lbl_Dev_InvErr_Fork2Code.ForeColor = Color.Red;
                        }
                        else
                        {
                            lbl_Dev_InvErr_Fork2Code.ForeColor = Color.Black;
                        }

                    }
                }
            }
            catch
            {

            }
        }
        private unsafe void Display_Sub_TaskList()
        {
            try
            {
                if (lv_TaskJob.Items.Count == 0)
                {
                    for (byte i = 0; i < 20; i++)
                    {
                        ListViewItem item = lv_TaskJob.Items.Add(string.Format("{0}", i + 1));
                        item.SubItems.Add("");
                        item.SubItems.Add("");
                        item.SubItems.Add("");
                        item.SubItems.Add("");
                        item.SubItems.Add("");
                        //item.SubItems.Add("");
                    }
                }
                fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt)
                {
                    //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                    //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                    {
                        //Task List
                        lbl_TaskJobNumber.Text = string.Format("{0}", DevSt->Task_JobNumber);
                        switch (DevSt->Task_JobStatus)
                        {
                            case 0: lbl_TaskJobSt.Text = ""; break;
                            case 1: lbl_TaskJobSt.Text = "(진행중)"; break;
                        }

                        fixed (VEXI_DEFS.TSRM_TaskJobItem* TaskJobPtr = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.TaskJobItem_1)
                        {
                            VEXI_DEFS.TSRM_TaskJobItem* Ptr = TaskJobPtr;
                            for (byte i = 0; i < 20; i++)
                            {

                                switch (Ptr->Cmd)
                                {
                                    case 0x00: lv_TaskJob.Items[i].SubItems[1].Text = "None"; break;
                                    case 0x01: lv_TaskJob.Items[i].SubItems[1].Text = "Move"; break;
                                    case 0x02: lv_TaskJob.Items[i].SubItems[1].Text = "Loading"; break;
                                    case 0x03: lv_TaskJob.Items[i].SubItems[1].Text = "Unloading"; break;
                                    default: lv_TaskJob.Items[i].SubItems[1].Text = string.Format("{0}", Ptr->Cmd); break;
                                }
                                switch (Ptr->WorkStatus)
                                {
                                    case 0: lv_TaskJob.Items[i].SubItems[2].Text = "None"; break;
                                    case 1: lv_TaskJob.Items[i].SubItems[2].Text = "대기"; break;
                                    case 2: lv_TaskJob.Items[i].SubItems[2].Text = "수행중"; break;
                                    case 3: lv_TaskJob.Items[i].SubItems[2].Text = "실패"; break;
                                    case 4: lv_TaskJob.Items[i].SubItems[2].Text = "완료"; break;
                                    default: lv_TaskJob.Items[i].SubItems[2].Text = string.Format("{0}", Ptr->WorkStatus); break;
                                }

                                //lv_TaskJob.Items[i].SubItems[3].Text = string.Format("{0}", Ptr->LoadFactor);
                                switch (Ptr->Fork)
                                {
                                    case 1: lv_TaskJob.Items[i].SubItems[3].Text = "Fork1"; break;
                                    case 2: lv_TaskJob.Items[i].SubItems[3].Text = "Fork2"; break;
                                    default: lv_TaskJob.Items[i].SubItems[3].Text = ""; break;
                                }
                                lv_TaskJob.Items[i].SubItems[4].Text = string.Format("S{0}-R{1}-B{2}-L{3}", Ptr->To.Station
                                                          , Ptr->To.Row
                                                          , Ptr->To.BayID
                                                          , Ptr->To.LevelID);
                                lv_TaskJob.Items[i].SubItems[5].Text = string.Format("{0}", Ptr->itemType);
                                Ptr = Ptr + 1;
                            }
                        }
                    }
                }
            } catch
            {

            }
        }

        private unsafe void Display_SI()
        {
            fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt)
            {
                lv_SI.Items[0].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_1);
                lv_SI.Items[1].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_2);
                lv_SI.Items[2].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_3);
                lv_SI.Items[3].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_4);
                lv_SI.Items[4].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_5);
                lv_SI.Items[5].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_6);
                lv_SI.Items[6].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_7);
                lv_SI.Items[7].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_8);
                lv_SI.Items[8].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_9);
                lv_SI.Items[9].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_10);
                lv_SI.Items[10].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_11);
                lv_SI.Items[11].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_12);
                lv_SI.Items[12].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_13);
                lv_SI.Items[13].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_14);
                lv_SI.Items[14].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_15);
                lv_SI.Items[15].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_16);

                lv_SI.Items[16].SubItems[1].Text = String.Format("0x{0:X4}", DevSt->SI_17);
                lv_SI.Items[17].SubItems[1].Text = String.Format("0x{0:X4}", DevSt->SI_18);
                lv_SI.Items[18].SubItems[1].Text = String.Format("0x{0:X4}", DevSt->SI_19);
                lv_SI.Items[19].SubItems[1].Text = String.Format("0x{0:X4}", DevSt->SI_20);
                lv_SI.Items[20].SubItems[1].Text = String.Format("0x{0:X4}", DevSt->SI_21);
                lv_SI.Items[21].SubItems[1].Text = String.Format("0x{0:X4}", DevSt->SI_22);
                lv_SI.Items[22].SubItems[1].Text = String.Format("0x{0:X4}", DevSt->SI_23);
                lv_SI.Items[23].SubItems[1].Text = String.Format("0x{0:X4}", DevSt->SI_24);
                lv_SI.Items[24].SubItems[1].Text = String.Format("0x{0:X4}", DevSt->SI_25);
                lv_SI.Items[25].SubItems[1].Text = String.Format("0x{0:X4}", DevSt->SI_26);
                lv_SI.Items[26].SubItems[1].Text = String.Format("0x{0:X4}", DevSt->SI_27);
                lv_SI.Items[27].SubItems[1].Text = String.Format("0x{0:X4}", DevSt->SI_28);
                lv_SI.Items[28].SubItems[1].Text = String.Format("0x{0:X4}", DevSt->SI_29);
                lv_SI.Items[29].SubItems[1].Text = String.Format("0x{0:X4}", DevSt->SI_30);
                lv_SI.Items[30].SubItems[1].Text = String.Format("0x{0:X4}", DevSt->SI_31);
                lv_SI.Items[31].SubItems[1].Text = String.Format("0x{0:X4}", DevSt->SI_32);

                lv_SI.Items[32].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_33);
                lv_SI.Items[33].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_34);
                lv_SI.Items[34].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_35);
                lv_SI.Items[35].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_36);
                lv_SI.Items[36].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_37);
                lv_SI.Items[37].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_38);
                lv_SI.Items[38].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_39);
                lv_SI.Items[39].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_40);
                lv_SI.Items[40].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_41);
                lv_SI.Items[41].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_42);
                lv_SI.Items[42].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_43);
                lv_SI.Items[43].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_44);
                lv_SI.Items[44].SubItems[1].Text = String.Format("0x{0:X8}", DevSt->SI_45);
            }
        }

        private unsafe void Display_Sub_DIO()
        {
            byte SelectDIindex = 0;
            byte SelectDOindex = 0;
            byte Loop;
            byte ByteIndex, BitIndex;

            try
            {
                fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt)
                {
                    //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                    //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                    {
                        //I/O
                        if (rb_DIO_DigitalIn_1.Checked) SelectDIindex = 1;
                        if (rb_DIO_DigitalIn_2.Checked) SelectDIindex = 2;
                        if (rb_DIO_DigitalIn_3.Checked) SelectDIindex = 3;
                        if (rb_DIO_DigitalIn_4.Checked) SelectDIindex = 4;
                        if (rb_DIO_DigitalOut_1.Checked) SelectDOindex = 4;
                        if (rb_DIO_DigitalOut_2.Checked) SelectDOindex = 5;
                        
                        switch (SelectDIindex)
                        {
                            case 1:
                                for (Loop = 1; Loop <= 44; Loop++)
                                {

                                    if (Loop > ConstClass.SRM_DI_Names_1.GetLength(0))
                                    {
                                        lbl_DI_Title[Loop - 1].BackColor = Color.Gray;
                                        lbl_DI_Title[Loop - 1].Text = "";

                                        lbl_DI_ST[Loop - 1].BackColor = Color.Gray;
                                        lbl_DI_ST[Loop - 1].Text = "";
                                    }
                                    else
                                    {
                                        lbl_DI_Title[Loop - 1].BackColor = System.Drawing.SystemColors.Highlight;
                                        lbl_DI_Title[Loop - 1].Text = ConstClass.SRM_DI_Names_1[Loop - 1, 0];
                                    }
                                }
                                break;
                            case 2:
                                for (Loop = 45; Loop <= 88; Loop++)
                                {
                                    if (Loop > ConstClass.SRM_DI_Names_1.GetLength(0))
                                    {
                                        lbl_DI_Title[Loop - 45].BackColor = Color.Gray;
                                        lbl_DI_Title[Loop - 45].Text = "";

                                        lbl_DI_ST[Loop - 45].BackColor = Color.Gray;
                                        lbl_DI_ST[Loop - 45].Text = "";
                                    }
                                    else
                                    {
                                        lbl_DI_Title[Loop - 45].BackColor = System.Drawing.SystemColors.Highlight;
                                        lbl_DI_Title[Loop - 45].Text = ConstClass.SRM_DI_Names_1[Loop - 1, 0];
                                    }
                                }

                                break;
                            case 3:
                                for (Loop = 89; Loop <= 132; Loop++)
                                {
                                    if (Loop > ConstClass.SRM_DI_Names_1.GetLength(0))
                                    {
                                        if (Loop <= (ConstClass.SRM_DI_Names_1.GetLength(0) + ConstClass.SRM_DI_Names_2.GetLength(0)))
                                        {
                                            lbl_DI_Title[Loop - 89].BackColor = System.Drawing.SystemColors.Highlight;
                                            lbl_DI_Title[Loop - 89].Text = ConstClass.SRM_DI_Names_2[Loop - ConstClass.SRM_DI_Names_1.GetLength(0) - 1, 0];

                                        }
                                        else
                                        {
                                            lbl_DI_Title[Loop - 89].BackColor = Color.Gray;
                                            lbl_DI_Title[Loop - 89].Text = "";

                                            lbl_DI_ST[Loop - 89].BackColor = Color.Gray;
                                            lbl_DI_ST[Loop - 89].Text = "";

                                        }
                                    }
                                    else
                                    {
                                        lbl_DI_Title[Loop - 89].BackColor = System.Drawing.SystemColors.Highlight;
                                        lbl_DI_Title[Loop - 89].Text = ConstClass.SRM_DI_Names_1[Loop - 1, 0];
                                    }
                                }

                                break;
                            case 4:
                                for (Loop = 133; Loop <= 176; Loop++)
                                {
                                    if (Loop > ConstClass.SRM_DI_Names_1.GetLength(0))
                                    {
                                        if (Loop <= (ConstClass.SRM_DI_Names_1.GetLength(0) + ConstClass.SRM_DI_Names_2.GetLength(0)))
                                        {
                                            lbl_DI_Title[Loop - 133].BackColor = System.Drawing.SystemColors.Highlight;
                                            lbl_DI_Title[Loop - 133].Text = ConstClass.SRM_DI_Names_2[Loop - ConstClass.SRM_DI_Names_1.GetLength(0) - 1, 0];

                                        }
                                        else
                                        {
                                            lbl_DI_Title[Loop - 133].BackColor = Color.Gray;
                                            lbl_DI_Title[Loop - 133].Text = "";

                                            lbl_DI_ST[Loop - 133].BackColor = Color.Gray;
                                            lbl_DI_ST[Loop - 133].Text = "";

                                        }
                                    }
                                    else
                                    {
                                        lbl_DI_Title[Loop - 133].BackColor = System.Drawing.SystemColors.Highlight;
                                        lbl_DI_Title[Loop - 133].Text = ConstClass.SRM_DI_Names_1[Loop - 1, 0];
                                    }
                                }

                                break;
                        }
                        switch (SelectDOindex)
                        {
                            case 4:
                                for (Loop = 1; Loop <= 44; Loop++)
                                {
                                    if (Loop > ConstClass.SRM_DO_Names_1.GetLength(0))
                                    {
                                        if (Loop <= (ConstClass.SRM_DO_Names_1.GetLength(0) + ConstClass.SRM_DO_Names_2.GetLength(0)))
                                        {
                                            lbl_DO_Title[Loop - 1].BackColor = Color.DarkOliveGreen;
                                            lbl_DO_Title[Loop - 1].Text = ConstClass.SRM_DO_Names_2[Loop - ConstClass.SRM_DO_Names_1.GetLength(0) - 1, 0];
                                        }
                                        else
                                        {
                                            lbl_DO_Title[Loop - 1].BackColor = Color.Gray;
                                            lbl_DO_Title[Loop - 1].Text = "";

                                            lbl_DO_ST[Loop - 1].BackColor = Color.Gray;
                                            lbl_DO_ST[Loop - 1].Text = "";
                                        }
                                    }
                                    else
                                    {
                                        lbl_DO_Title[Loop - 1].BackColor = Color.DarkOliveGreen;
                                        lbl_DO_Title[Loop - 1].Text = ConstClass.SRM_DO_Names_1[Loop - 1, 0];
                                    }
                                }
                                break;

                            case 5:
                                for (Loop = 45; Loop <= 88; Loop++)
                                {
                                    if (Loop > ConstClass.SRM_DO_Names_1.GetLength(0))
                                    {
                                        if (Loop <= (ConstClass.SRM_DO_Names_1.GetLength(0) + ConstClass.SRM_DO_Names_2.GetLength(0)))
                                        {
                                            lbl_DO_Title[Loop - 45].BackColor = Color.DarkOliveGreen;
                                            lbl_DO_Title[Loop - 45].Text = ConstClass.SRM_DO_Names_2[Loop - ConstClass.SRM_DO_Names_1.GetLength(0) - 1, 0];
                                        }
                                        else
                                        {
                                            lbl_DO_Title[Loop - 45].BackColor = Color.Gray;
                                            lbl_DO_Title[Loop - 45].Text = "";

                                            lbl_DO_ST[Loop - 45].BackColor = Color.Gray;
                                            lbl_DO_ST[Loop - 45].Text = "";
                                        }
                                    }
                                    else
                                    {
                                        lbl_DO_Title[Loop - 45].BackColor = Color.DarkOliveGreen;
                                        lbl_DO_Title[Loop - 45].Text = ConstClass.SRM_DO_Names_1[Loop - 1, 0];
                                    }
                                }
                                break;


                        }


                        switch (SelectDIindex)
                        {
                            case 1:
                                for (Loop = 1; Loop <= 44; Loop++)
                                {
                                    ByteIndex = (byte)((Loop - 1) / 8);
                                    BitIndex = (byte)((Loop - 1) % 8);

                                    if (Loop <= ConstClass.SRM_DI_Names_1.GetLength(0))
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

                                        lbl_DI_Title[Loop - 1].ForeColor = Color.White;
                                    }

                                }
                                break;
                            case 2:
                                for (Loop = 45; Loop <= 88; Loop++)
                                {
                                    ByteIndex = (byte)((Loop - 1) / 8);
                                    BitIndex = (byte)((Loop - 1) % 8);

                                    if (Loop <= ConstClass.SRM_DI_Names_1.GetLength(0))
                                    {
                                        if (Global_Class.BitStatus(DevSt->IO_Digital_IN[ByteIndex], BitIndex))
                                        {
                                            lbl_DI_ST[Loop - 45].BackColor = Color.Yellow;
                                            lbl_DI_ST[Loop - 45].Text = "ON";
                                        }
                                        else
                                        {
                                            lbl_DI_ST[Loop - 45].BackColor = Color.Silver;
                                            lbl_DI_ST[Loop - 45].Text = "OFF";
                                        }

                                        lbl_DI_Title[Loop - 45].ForeColor = Color.White;
                                    }
                                }
                                break;
                            case 3:
                                for (Loop = 89; Loop <= 132; Loop++)
                                {
                                    ByteIndex = (byte)((Loop - 1) / 8);
                                    BitIndex = (byte)((Loop - 1) % 8);

                                    if (Loop <= (ConstClass.SRM_DI_Names_1.GetLength(0) + ConstClass.SRM_DI_Names_2.GetLength(0)))
                                    {
                                        if (Global_Class.BitStatus(DevSt->IO_Digital_IN[ByteIndex], BitIndex))
                                        {
                                            lbl_DI_ST[Loop - 89].BackColor = Color.Yellow;
                                            lbl_DI_ST[Loop - 89].Text = "ON";
                                        }
                                        else
                                        {
                                            lbl_DI_ST[Loop - 89].BackColor = Color.Silver;
                                            lbl_DI_ST[Loop - 89].Text = "OFF";
                                        }

                                        lbl_DI_Title[Loop - 89].ForeColor = Color.White;
                                    }
                                }
                                break;
                            case 4:
                                for (Loop = 133; Loop <= 176; Loop++)
                                {
                                    ByteIndex = (byte)((Loop - 1) / 8);
                                    BitIndex = (byte)((Loop - 1) % 8);

                                    if (Loop <= (ConstClass.SRM_DI_Names_1.GetLength(0) + ConstClass.SRM_DI_Names_2.GetLength(0)))
                                    {
                                        if (Global_Class.BitStatus(DevSt->IO_Digital_IN[ByteIndex], BitIndex))
                                        {
                                            lbl_DI_ST[Loop - 133].BackColor = Color.Yellow;
                                            lbl_DI_ST[Loop - 133].Text = "ON";
                                        }
                                        else
                                        {
                                            lbl_DI_ST[Loop - 133].BackColor = Color.Silver;
                                            lbl_DI_ST[Loop - 133].Text = "OFF";
                                        }

                                        lbl_DI_Title[Loop - 133].ForeColor = Color.White;
                                    }
                                }
                                break;
                        }
                        switch (SelectDOindex)
                        {
                            case 4:
                                for (Loop = 1; Loop <= 44; Loop++)
                                {
                                    ByteIndex = (byte)((Loop - 1) / 8);
                                    BitIndex = (byte)((Loop - 1) % 8);

                                    if (Loop <= (ConstClass.SRM_DO_Names_1.GetLength(0) + ConstClass.SRM_DO_Names_2.GetLength(0)))
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

                                        if (ByteIndex <= 4)
                                        {
                                            if (Global_Class.BitStatus(DevSt->IO_Digital_OUTMode_1[ByteIndex], BitIndex))
                                            {
                                                lbl_DO_Title[Loop - 1].ForeColor = Color.Red;
                                            }
                                            else
                                            {
                                                lbl_DO_Title[Loop - 1].ForeColor = Color.White;
                                            }
                                        } else
                                        {
                                            if (Global_Class.BitStatus(DevSt->IO_Digital_OUTMode_2[ByteIndex-5], BitIndex))
                                            {
                                                lbl_DO_Title[Loop - 1].ForeColor = Color.Red;
                                            }
                                            else
                                            {
                                                lbl_DO_Title[Loop - 1].ForeColor = Color.White;
                                            }
                                        }
                                    }
                                }
                                break;

                            case 5:
                                for (Loop = 45; Loop <= 88; Loop++)
                                {
                                    ByteIndex = (byte)((Loop - 1) / 8);
                                    BitIndex = (byte)((Loop - 1) % 8);

                                    if (Loop <= (ConstClass.SRM_DO_Names_1.GetLength(0) + ConstClass.SRM_DO_Names_2.GetLength(0)))
                                    {
                                        if (Global_Class.BitStatus(DevSt->IO_Digital_OUT[ByteIndex], BitIndex))
                                        {
                                            lbl_DO_ST[Loop - 45].BackColor = Color.Yellow;
                                            lbl_DO_ST[Loop - 45].Text = "ON";
                                        }
                                        else
                                        {
                                            lbl_DO_ST[Loop - 45].BackColor = Color.Silver;
                                            lbl_DO_ST[Loop - 45].Text = "OFF";
                                        }

                                        if (ByteIndex <= 4)
                                        {
                                            if (Global_Class.BitStatus(DevSt->IO_Digital_OUTMode_1[ByteIndex], BitIndex))
                                            {
                                                lbl_DO_Title[Loop - 45].ForeColor = Color.Red;
                                            }
                                            else
                                            {
                                                lbl_DO_Title[Loop - 45].ForeColor = Color.White;
                                            }
                                        }
                                        else
                                        {
                                            if (Global_Class.BitStatus(DevSt->IO_Digital_OUTMode_2[ByteIndex - 5], BitIndex))
                                            {
                                                lbl_DO_Title[Loop - 45].ForeColor = Color.Red;
                                            }
                                            else
                                            {
                                                lbl_DO_Title[Loop - 45].ForeColor = Color.White;
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void Display_Init()
        {
            //Display_SRM_BasicSt 내 갱신 컴포넌트들
            lbl_DevDetailType.Text = "";
            lblVersion.Text = "";
            lblSystemTimeUTC.Text = "";
            lbl_JisangMode.Text = "";
            lbl_JisangSt_0.Text = "";
            lbl_JisangSt_0.BackColor = Color.White;
            lbl_JisangSt_3.BackColor = Color.White;
            lbl_JisangSt_4.BackColor = Color.White;
            lbl_JisangSt_2.Text = "";
            lbl_JisangSt_2.BackColor = Color.White;
            lbl_JisangSt_1.Text = "";
            lbl_JisangSt_1.BackColor = Color.White;
            lbl_Jisang_InterLockIn.Text = "";
            lbl_Jisang_InterLockOut.Text = "";
            lbl_DevMode_Auto.BackColor = Color.White;
            lbl_DevMode_Manual.BackColor = Color.White;

            lbl_DevMode_Force.BackColor = Color.White;
            lbl_DevMode_Setup.BackColor = Color.White;
            lbl_Dev_Start.Text = "";
            lbl_Dev_Start.BackColor = Color.White;
            lbl_Dev_Emergency.Text = "";
            lbl_Dev_Emergency.BackColor = Color.White;
            lbl_Dev_InvertorConn.Text = "";
            lbl_Dev_InvertorConn.BackColor = Color.White;
            lbl_DevReady.Text = "";
            lbl_DevReady.BackColor = Color.White;
            lbl_Dev_Error.Text = "";
            lbl_Dev_Error.BackColor = Color.White;
            lbl_DevEmergencySwitch.Text = "";
            lbl_DevEmergencySwitch.BackColor = Color.White;
            lbl_DevModeSwitch.Text = "";
            lbl_DevModeSwitch.BackColor = Color.White;
            lbl_Dev_ActionCode.Text = "";


            //Display_Fork_Position 내 갱신 컴포넌트들
            lbl_Fork1_Pos.Text = "";
            lbl_Fork1_Dest.Text = "";
            lbl_Fork1_ForkPos.Text = "";
            lbl_Fork1_CurrentPos_Center.BackColor = Color.White;
            lbl_Fork1_CurrentPos_L1.BackColor = Color.White;
            lbl_Fork1_CurrentPos_L2.BackColor = Color.White;
            lbl_Fork1_CurrentPos_L3.BackColor = Color.White;
            lbl_Fork1_CurrentPos_R1.BackColor = Color.White;
            lbl_Fork1_CurrentPos_R2.BackColor = Color.White;
            lbl_Fork1_CurrentPos_R3.BackColor = Color.White;
            lbl_Drive_CurrentPos_L_Fork1.BackColor = Color.White;
            lbl_Drive_CurrentPos_R_Fork1.BackColor = Color.White;
            lbl_UpDown_Fork1_CurrentPos_L_1.BackColor = Color.White;
            lbl_UpDown_Fork1_CurrentPos_L_2.BackColor = Color.White;
            lbl_UpDown_Fork1_CurrentPos_R_1.BackColor = Color.White;
            lbl_UpDown_Fork1_CurrentPos_R_2.BackColor = Color.White;
            lbl_Fork2_Pos.Text = "";
            lbl_Fork2_Dest.Text = "";
            lbl_Fork2_ForkPos.Text = "";
            lbl_Fork2_CurrentPos_Center.BackColor = Color.White;
            lbl_Fork2_CurrentPos_L1.BackColor = Color.White;
            lbl_Fork2_CurrentPos_L2.BackColor = Color.White;
            lbl_Fork2_CurrentPos_L3.BackColor = Color.White;
            lbl_Fork2_CurrentPos_R1.BackColor = Color.White;
            lbl_Fork2_CurrentPos_R2.BackColor = Color.White;
            lbl_Fork2_CurrentPos_R3.BackColor = Color.White;
            lbl_Drive_CurrentPos_L_Fork2.BackColor = Color.White;
            lbl_Drive_CurrentPos_R_Fork2.BackColor = Color.White;
            lbl_UpDown_Fork2_CurrentPos_L_1.BackColor = Color.White;
            lbl_UpDown_Fork2_CurrentPos_L_2.BackColor = Color.White;
            lbl_UpDown_Fork2_CurrentPos_R_1.BackColor = Color.White;
            lbl_UpDown_Fork2_CurrentPos_R_2.BackColor = Color.White;

            //Display_D_UD_Fork_St 내 갱신 컴포넌트들
            lbl_Dev_Home.Text = "";
            lbl_Dev_Home.BackColor = Color.White;
            lbl_Dev_Maintance.Text = "";
            lbl_Dev_Maintance.BackColor = Color.White;
            lbl_Drive_Decel_1.Text = "";
            lbl_Drive_Decel_2.Text = "";
            lbl_DriveSt1_0.Text = "";
            lbl_DriveSt1_1.Text = "";
            lbl_DriveSt1_2.Text = "";
            lbl_DriveSt1_3.Text = "";
            lbl_DriveSt1_4.Text = "";
            lbl_DriveSt1_4.BackColor = Color.White;
            lbl_DriveSt1_5.Text = "";
            lbl_DriveSt1_5.BackColor = Color.White;

            lbl_DriveSt2_0.Text = "";
            lbl_DriveSt2_0.BackColor = Color.White;
            lbl_DriveSt2_1.Text = "";
            lbl_DriveSt2_1.BackColor = Color.White;
            lbl_DriveSt2_2.Text = "";
            lbl_DriveSt2_2.BackColor = Color.White;
            lbl_DriveSt2_3.Text = "";
            lbl_DriveSt2_3.BackColor = Color.White;
            lbl_DriveSt2_4.Text = "";
            lbl_DriveSt2_4.BackColor = Color.White;
            lbl_Drive_Position.Text = "";
            lbl_Drive_Speed.Text = "";
            lbl_Drive_Destination.Text = "";
            lbl_UpDown_Decel_1.Text = "";
            lbl_UpDown_Decel_2.Text = "";
            lbl_Drive_DestSpeed.Text = "";

            lbl_UpDownSt1_0.Text = "";
            lbl_UpDownSt1_1.Text = "";
            lbl_UpDownSt1_2.Text = "";
            lbl_UpDownSt1_3.Text = "";
            lbl_UpDownSt1_4.Text = "";
            lbl_UpDownSt1_4.BackColor = Color.White;
            lbl_UpDownSt1_5.Text = "";
            lbl_UpDownSt1_5.BackColor = Color.White;

            lbl_UpDownSt2_0.Text = "";
            lbl_UpDownSt2_0.BackColor = Color.White;
            lbl_UpDownSt2_1.Text = "";
            lbl_UpDownSt2_1.BackColor = Color.White;
            lbl_UpDownSt2_2.Text = "";
            lbl_UpDownSt2_2.BackColor = Color.White;
            lbl_UpDownSt2_3.Text = "";
            lbl_UpDownSt2_3.BackColor = Color.White;
            lbl_UpDownSt2_4.Text = "";
            lbl_UpDownSt2_4.BackColor = Color.White;

            lbl_UpDown_Position.Text = "";
            lbl_UpDown_Speed.Text = "";
            lbl_UpDown_Destination.Text = "";
            lbl_UpDown_DestSpeed.Text = "";

            lbl_Fork1St1_0.Text = "";
            lbl_Fork1St1_1.Text = "";
            lbl_Fork1St1_2.Text = "";
            lbl_Fork1St1_3.Text = "";
            lbl_Fork1St1_4.Text = "";
            lbl_Fork1St1_4.BackColor = Color.White;
            lbl_Fork1St1_5.Text = "";
            lbl_Fork1St1_5.BackColor = Color.White;
            lbl_Fork1St1_6.Text = "";
            lbl_Fork1St1_6.BackColor = Color.White;
            lbl_Fork1St1_7.Text = "";
            lbl_Fork1St1_7.BackColor = Color.White;
            lbl_Fork1St2_0.Text = "";
            lbl_Fork1St2_0.BackColor = Color.White;
            lbl_Fork1St2_1.Text = "";
            lbl_Fork1St2_1.BackColor = Color.White;
            lbl_Fork1St2_2.Text = "";
            lbl_Fork1St2_2.BackColor = Color.White;
            lbl_Fork1St2_3.Text = "";
            lbl_Fork1St2_3.BackColor = Color.White;
            lbl_Fork1St2_4.Text = "";
            lbl_Fork1St2_4.BackColor = Color.White;
            lbl_Fork1St2_6.Text = "";
            lbl_Fork1St2_6.BackColor = Color.White;
            lbl_Fork1St2_7.Text = "";
            lbl_Fork1St2_7.BackColor = Color.White;
            lbl_Fork1_Position.Text = "";
            lbl_Fork1_Speed.Text = "";
            lbl_Fork1_Destination.Text = "";
            lbl_Fork1_DestSpeed.Text = "";
            lbl_Fork1_HaveItemType.Text = "";
            lbl_Fork1_Existitem.Text = "";
            lbl_Fork1_Existitem.BackColor = Color.White;

            lbl_Fork2St1_0.Text = "";
            lbl_Fork2St1_1.Text = "";
            lbl_Fork2St1_2.Text = "";
            lbl_Fork2St1_3.Text = "";
            lbl_Fork2St1_4.Text = "";
            lbl_Fork2St1_4.BackColor = Color.White;
            lbl_Fork2St1_5.Text = "";
            lbl_Fork2St1_5.BackColor = Color.White;
            lbl_Fork2St1_6.Text = "";
            lbl_Fork2St1_6.BackColor = Color.White;
            lbl_Fork2St1_7.Text = "";
            lbl_Fork2St1_7.BackColor = Color.White;
            lbl_Fork2St2_0.Text = "";
            lbl_Fork2St2_0.BackColor = Color.White;
            lbl_Fork2St2_1.Text = "";
            lbl_Fork2St2_1.BackColor = Color.White;
            lbl_Fork2St2_2.Text = "";
            lbl_Fork2St2_2.BackColor = Color.White;
            lbl_Fork2St2_3.Text = "";
            lbl_Fork2St2_3.BackColor = Color.White;
            lbl_Fork2St2_4.Text = "";
            lbl_Fork2St2_4.BackColor = Color.White;
            lbl_Fork2St2_6.Text = "";
            lbl_Fork2St2_6.BackColor = Color.White;
            lbl_Fork2St2_7.Text = "";
            lbl_Fork2St2_7.BackColor = Color.White;
            lbl_Fork2_Position.Text = "";
            lbl_Fork2_Speed.Text = "";
            lbl_Fork2_Destination.Text = "";
            lbl_Fork2_DestSpeed.Text = "";
            lbl_Fork2_HaveItemType.Text = "";
            lbl_Fork2_Existitem.Text = "";
            lbl_Fork2_Existitem.BackColor = Color.White;

            //Display_Sub_TaskList
            lbl_TaskJobNumber.Text = "";
            lbl_TaskJobSt.Text = "";
            lv_TaskJob.Items.Clear();

            //Display_Sub_ForkJob
            lbl_Fork1_Job.Text = "";
            
            lbl_Fork1_Cmd.Text = "";
            lbl_Fork1_From.Text = "";
            lbl_Fork1_To.Text = "";
            lbl_Fork1_jobSt.Text = "";
            lbl_Fork1_jobStep.Text = "";
            lbl_Fork1_MoveJob.Text = "";
            lbl_Fork1_MoveJob_To.Text = "";
            lbl_Fork1_MoveJob_St.Text = "";
            lbl_Fork1_MoveJob_Step.Text = "";
            lbl_Fork2_Job.Text = "";
            
            lbl_Fork2_Cmd.Text = "";
            lbl_Fork2_From.Text = "";
            lbl_Fork2_To.Text = "";
            lbl_Fork2_jobSt.Text = "";
            lbl_Fork2_jobStep.Text = "";
            lbl_Fork2_MoveJob.Text = "";
            lbl_Fork2_MoveJob_To.Text = "";
            lbl_Fork2_MoveJob_St.Text = "";
            lbl_Fork2_MoveJob_Step.Text = "";

            //Display_Sub_DIO
            for (byte i = 1; i <= 44; i++)
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

            for (byte i = 0; i < lv_SI.Items.Count; i++)
            {
                lv_SI.Items[i].SubItems[1].Text = "";
            }


            //Display_InvErr
            lbl_Dev_InvErr_DriveCode.Text = "";
            lbl_Dev_InvErr_DriveCode.BackColor = Color.White;
            lbl_Dev_InvErr_LiftCode.Text = "";
            lbl_Dev_InvErr_LiftCode.BackColor = Color.White;
            lbl_Dev_InvErr_Fork1Code.Text = "";
            lbl_Dev_InvErr_Fork1Code.BackColor = Color.White;
            lbl_Dev_InvErr_Fork2Code.Text = "";
            lbl_Dev_InvErr_Fork2Code.BackColor = Color.White;

        }

        private unsafe void Display_SRM_BasicSt()
        {
            try
            {
                fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt)
                {
                    //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                    //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                    {
                        //코딩
                        lbl_DevDetailType.Text = String.Format("0x{0:X4}", DevSt->DevDetailtype);
                        lblVersion.Text = Global_Class.UTIL_ByteToFVerstr(DevSt->FWversion);
                        DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(DevSt->SystemUTCTime);
                        lblSystemTimeUTC.Text = String.Format("{0}", PCtime);

                        switch (DevSt->ControllerMode)
                        {
                            case ConstClass.TControlMode.CtrlMode_Auto:
                                lbl_JisangMode.Text = "자동"; break;
                            case ConstClass.TControlMode.CtrlMode_Semi:
                                lbl_JisangMode.Text = "반자동"; break;
                            case ConstClass.TControlMode.CtrlMode_Manual:
                                lbl_JisangMode.Text = "수동"; break;
                        }
                        if (Global_Class.BitStatus(DevSt->ControllerSt, 0))
                        {
                            lbl_JisangSt_0.Text = "ON";
                            lbl_JisangSt_0.BackColor = Color.Red;
                            lbl_JisangSt_0.ForeColor = Color.White;
                        }
                        else
                        {
                            lbl_JisangSt_0.Text = "OFF";
                            lbl_JisangSt_0.BackColor = Color.Silver;
                            lbl_JisangSt_0.ForeColor = Color.Black;
                        }
                        if (Global_Class.BitStatus(DevSt->ControllerSt, 1))
                        {
                            lbl_JisangSt_1.Text = "ON";
                            lbl_JisangSt_1.BackColor = Color.Red;
                            lbl_JisangSt_1.ForeColor = Color.White;
                        }
                        else
                        {
                            lbl_JisangSt_1.Text = "OFF";
                            lbl_JisangSt_1.BackColor = Color.Silver;
                            lbl_JisangSt_1.ForeColor = Color.Black;
                        }
                        if (Global_Class.BitStatus(DevSt->ControllerSt, 2))
                        {
                            lbl_JisangSt_2.Text = "ON";
                            lbl_JisangSt_2.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_JisangSt_2.Text = "OFF";
                            lbl_JisangSt_2.BackColor = Color.Silver;
                        }
                        if (Global_Class.BitStatus(DevSt->ControllerSt, 3))
                        {
                            lbl_JisangSt_3.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_JisangSt_3.BackColor = Color.Silver;
                        }
                        if (Global_Class.BitStatus(DevSt->ControllerSt, 4))
                        {
                            lbl_JisangSt_4.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_JisangSt_4.BackColor = Color.Silver;
                        }


                        lbl_Jisang_InterLockIn.Text = string.Format("0x{0:X2} 0x{1:X2} 0x{2:X2} 0x{3:X2} x{4:X2} 0x{5:X2} 0x{6:X2} 0x{7:X2} "
                                                                                                          , DevSt->CV_Interlock[0]
                                                                                                          , DevSt->CV_Interlock[1]
                                                                                                          , DevSt->CV_Interlock[2]
                                                                                                          , DevSt->CV_Interlock[3]
                                                                                                          , DevSt->CV_Interlock[4]
                                                                                                          , DevSt->CV_Interlock[5]
                                                                                                          , DevSt->CV_Interlock[6]
                                                                                                          , DevSt->CV_Interlock[7]);
                        lbl_Jisang_InterLockOut.Text = string.Format("0x{0:X2} 0x{1:X2} 0x{2:X2} 0x{3:X2} x{4:X2} 0x{5:X2} 0x{6:X2} 0x{7:X2} "
                                                                                                           , DevSt->Dev_Interlock[0]
                                                                                                           , DevSt->Dev_Interlock[1]
                                                                                                           , DevSt->Dev_Interlock[2]
                                                                                                           , DevSt->Dev_Interlock[3]
                                                                                                           , DevSt->Dev_Interlock[4]
                                                                                                           , DevSt->Dev_Interlock[5]
                                                                                                           , DevSt->Dev_Interlock[6]
                                                                                                           , DevSt->Dev_Interlock[7]);
                        if (Global_Class.BitStatus(DevSt->DevMode, 0)) lbl_DevMode_Auto.BackColor = Color.Lime;
                        else lbl_DevMode_Auto.BackColor = Color.Silver;

                        if (Global_Class.BitStatus(DevSt->DevMode, 1)) lbl_DevMode_Manual.BackColor = Color.Lime;
                        else lbl_DevMode_Manual.BackColor = Color.Silver;

                        if (Global_Class.BitStatus(DevSt->DevMode, 2)) lbl_DevMode_Force.BackColor = Color.Lime;
                        else lbl_DevMode_Force.BackColor = Color.Silver;

                        if (Global_Class.BitStatus(DevSt->DevMode, 3)) lbl_DevMode_Setup.BackColor = Color.Lime;
                        else lbl_DevMode_Setup.BackColor = Color.Silver;


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

                        if (Global_Class.BitStatus(DevSt->DevSt_1, 5))
                        {
                            lbl_DevReady.Text = "ON";
                            lbl_DevReady.BackColor = Color.Lime;
                            lbl_DevReady.ForeColor = Color.Black;
                        }
                        else
                        {
                            lbl_DevReady.Text = "OFF";
                            lbl_DevReady.BackColor = Color.Silver;
                            lbl_DevReady.ForeColor = Color.Black;
                        }
                        

                        if (Global_Class.BitStatus(DevSt->DevSt_1, 3))
                        {
                            lbl_Dev_Error.Text = String.Format("{0}-{1}-{2}", DevSt->ErrorCode.MainCode, DevSt->ErrorCode.SubCode, DevSt->ErrorCode.PosCode) +
                                                 "  " + Global_Class.UTIL_SRMAlarmName(DevSt->ErrorCode.MainCode, DevSt->ErrorCode.SubCode, DevSt->ErrorCode.PosCode);
                            lbl_Dev_Error.BackColor = Color.Red;
                            lbl_Dev_Error.ForeColor = Color.White;
                        }
                        else
                        {
                            if (Global_Class.BitStatus(DevSt->DevSt_1, 2))
                            {
                                //lbl_Dev_Error.Text = "경고";
                                lbl_Dev_Error.Text = String.Format("{0}-{1}-{2}", DevSt->ErrorCode.MainCode, DevSt->ErrorCode.SubCode, DevSt->ErrorCode.PosCode) +
                                                 "  " + Global_Class.UTIL_SRMWarnningName(DevSt->ErrorCode.MainCode, DevSt->ErrorCode.SubCode, DevSt->ErrorCode.PosCode);
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

                        if (Global_Class.BitStatus(DevSt->DevSt_2, 6))
                        {
                            lbl_DevModeSwitch.Text = "수동";
                            lbl_DevModeSwitch.BackColor = Color.Red;
                            lbl_DevModeSwitch.ForeColor = Color.White;
                        }
                        else
                        {
                            lbl_DevModeSwitch.Text = "자동";
                            lbl_DevModeSwitch.BackColor = Color.Lime;
                            lbl_DevModeSwitch.ForeColor = Color.Black;
                        }

                        lbl_Dev_ActionCode.Text = Global_Class.UTIL_SRMActionStText(DevSt->ActionCode);
                    }
                }
            } catch
            {

            }

        }

        private unsafe void Display_Fork_Position()
        {
            try
            {
                fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt)
                {
                    //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                    //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                    {
                        lbl_Fork1_Pos.Text = String.Format("S{0}-R{1}-B{2}-L{3}",
                                                           DevSt->Fork1_Position.PositionCellRec.Station,
                                                           DevSt->Fork1_Position.RowID,
                                                           DevSt->Fork1_Position.PositionCellRec.BayID,
                                                           DevSt->Fork1_Position.PositionCellRec.LevelID);
                        lbl_Fork1_Dest.Text = String.Format("S{0}-R{1}-B{2}-L{3}",
                                                           DevSt->Fork1_DestCell.Station,
                                                           DevSt->Fork1_DestCell.Row,
                                                           DevSt->Fork1_DestCell.BayID,
                                                           DevSt->Fork1_DestCell.LevelID);
                        switch (DevSt->Fork1_Position.Fork)
                        {
                            case -3: lbl_Fork1_ForkPos.Text = "좌3"; break;
                            case -2: lbl_Fork1_ForkPos.Text = "좌2"; break;
                            case -1: lbl_Fork1_ForkPos.Text = "좌1"; break;
                            case 0: lbl_Fork1_ForkPos.Text = "중심"; break;
                            case 1: lbl_Fork1_ForkPos.Text = "우1"; break;
                            case 2: lbl_Fork1_ForkPos.Text = "우2"; break;
                            case 3: lbl_Fork1_ForkPos.Text = "우3"; break;
                            default: lbl_Fork1_ForkPos.Text = ""; break;
                        }

                        if (Global_Class.BitStatus(DevSt->Fork1_Position.CurrentPosition_1, 0)) lbl_Drive_CurrentPos_L_Fork1.BackColor = Color.Lime;
                        else lbl_Drive_CurrentPos_L_Fork1.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork1_Position.CurrentPosition_1, 1)) lbl_UpDown_Fork1_CurrentPos_L_1.BackColor = Color.Lime;
                        else lbl_UpDown_Fork1_CurrentPos_L_1.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork1_Position.CurrentPosition_1, 2)) lbl_UpDown_Fork1_CurrentPos_L_2.BackColor = Color.Lime;
                        else lbl_UpDown_Fork1_CurrentPos_L_2.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork1_Position.CurrentPosition_1, 3)) lbl_Drive_CurrentPos_R_Fork1.BackColor = Color.Lime;
                        else lbl_Drive_CurrentPos_R_Fork1.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork1_Position.CurrentPosition_1, 4)) lbl_UpDown_Fork1_CurrentPos_R_1.BackColor = Color.Lime;
                        else lbl_UpDown_Fork1_CurrentPos_R_1.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork1_Position.CurrentPosition_1, 5)) lbl_UpDown_Fork1_CurrentPos_R_2.BackColor = Color.Lime;
                        else lbl_UpDown_Fork1_CurrentPos_R_2.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork1_Position.CurrentPosition_2, 0)) lbl_Fork1_CurrentPos_Center.BackColor = Color.Lime;
                        else lbl_Fork1_CurrentPos_Center.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork1_Position.CurrentPosition_2, 1)) lbl_Fork1_CurrentPos_L1.BackColor = Color.Yellow;
                        else lbl_Fork1_CurrentPos_L1.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork1_Position.CurrentPosition_2, 2)) lbl_Fork1_CurrentPos_L2.BackColor = Color.Yellow;
                        else lbl_Fork1_CurrentPos_L2.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork1_Position.CurrentPosition_2, 3)) lbl_Fork1_CurrentPos_L3.BackColor = Color.Yellow;
                        else lbl_Fork1_CurrentPos_L3.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork1_Position.CurrentPosition_2, 4)) lbl_Fork1_CurrentPos_R1.BackColor = Color.Yellow;
                        else lbl_Fork1_CurrentPos_R1.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork1_Position.CurrentPosition_2, 5)) lbl_Fork1_CurrentPos_R2.BackColor = Color.Yellow;
                        else lbl_Fork1_CurrentPos_R2.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork1_Position.CurrentPosition_2, 6)) lbl_Fork1_CurrentPos_R3.BackColor = Color.Yellow;
                        else lbl_Fork1_CurrentPos_R3.BackColor = Color.Silver;


                        lbl_Fork2_Pos.Text = String.Format("S{0}-R{1}-B{2}-L{3}",
                                                           DevSt->Fork2_Position.PositionCellRec.Station,
                                                           DevSt->Fork2_Position.RowID,
                                                           DevSt->Fork2_Position.PositionCellRec.BayID,
                                                           DevSt->Fork2_Position.PositionCellRec.LevelID);
                        lbl_Fork2_Dest.Text = String.Format("S{0}-R{1}-B{2}-L{3}",
                                                           DevSt->Fork2_DestCell.Station,
                                                           DevSt->Fork2_DestCell.Row,
                                                           DevSt->Fork2_DestCell.BayID,
                                                           DevSt->Fork2_DestCell.LevelID);
                        switch (DevSt->Fork2_Position.Fork)
                        {
                            case -3: lbl_Fork2_ForkPos.Text = "좌3"; break;
                            case -2: lbl_Fork2_ForkPos.Text = "좌2"; break;
                            case -1: lbl_Fork2_ForkPos.Text = "좌1"; break;
                            case 0: lbl_Fork2_ForkPos.Text = "중심"; break;
                            case 1: lbl_Fork2_ForkPos.Text = "우1"; break;
                            case 2: lbl_Fork2_ForkPos.Text = "우2"; break;
                            case 3: lbl_Fork2_ForkPos.Text = "우3"; break;
                            default: lbl_Fork2_ForkPos.Text = ""; break;
                        }

                        if (Global_Class.BitStatus(DevSt->Fork2_Position.CurrentPosition_1, 0)) lbl_Drive_CurrentPos_L_Fork2.BackColor = Color.Lime;
                        else lbl_Drive_CurrentPos_L_Fork2.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork2_Position.CurrentPosition_1, 1)) lbl_UpDown_Fork2_CurrentPos_L_1.BackColor = Color.Lime;
                        else lbl_UpDown_Fork2_CurrentPos_L_1.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork2_Position.CurrentPosition_1, 2)) lbl_UpDown_Fork2_CurrentPos_L_2.BackColor = Color.Lime;
                        else lbl_UpDown_Fork2_CurrentPos_L_2.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork2_Position.CurrentPosition_1, 3)) lbl_Drive_CurrentPos_R_Fork2.BackColor = Color.Lime;
                        else lbl_Drive_CurrentPos_R_Fork2.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork2_Position.CurrentPosition_1, 4)) lbl_UpDown_Fork2_CurrentPos_R_1.BackColor = Color.Lime;
                        else lbl_UpDown_Fork2_CurrentPos_R_1.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork2_Position.CurrentPosition_1, 5)) lbl_UpDown_Fork2_CurrentPos_R_2.BackColor = Color.Lime;
                        else lbl_UpDown_Fork2_CurrentPos_R_2.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork2_Position.CurrentPosition_2, 0)) lbl_Fork2_CurrentPos_Center.BackColor = Color.Lime;
                        else lbl_Fork2_CurrentPos_Center.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork2_Position.CurrentPosition_2, 1)) lbl_Fork2_CurrentPos_L1.BackColor = Color.Yellow;
                        else lbl_Fork2_CurrentPos_L1.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork2_Position.CurrentPosition_2, 2)) lbl_Fork2_CurrentPos_L2.BackColor = Color.Yellow;
                        else lbl_Fork2_CurrentPos_L2.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork2_Position.CurrentPosition_2, 3)) lbl_Fork2_CurrentPos_L3.BackColor = Color.Yellow;
                        else lbl_Fork2_CurrentPos_L3.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork2_Position.CurrentPosition_2, 4)) lbl_Fork2_CurrentPos_R1.BackColor = Color.Yellow;
                        else lbl_Fork2_CurrentPos_R1.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork2_Position.CurrentPosition_2, 5)) lbl_Fork2_CurrentPos_R2.BackColor = Color.Yellow;
                        else lbl_Fork2_CurrentPos_R2.BackColor = Color.Silver;
                        if (Global_Class.BitStatus(DevSt->Fork2_Position.CurrentPosition_2, 6)) lbl_Fork2_CurrentPos_R3.BackColor = Color.Yellow;
                        else lbl_Fork2_CurrentPos_R3.BackColor = Color.Silver;
                    }
                }
            } catch
            {

            }
        }

        private unsafe void Display_D_UD_Fork_St()
        {
            try
            {
                fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt)
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


                        if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_1, 5))
                        {
                            lbl_DriveSt1_5.Text = "홈복귀";
                            lbl_DriveSt1_5.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_DriveSt1_5.Text = "아님";
                            lbl_DriveSt1_5.BackColor = Color.Silver;
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
                                lbl_DriveSt1_3.Text = "역방향";
                            }
                            else
                            {
                                lbl_DriveSt1_3.Text = "정방향";
                            }
                        }
                        else
                        {
                            lbl_DriveSt1_3.Text = "정지";
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
                        if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_1, 0))
                        {
                            lbl_DriveSt1_0.Text = "동작중";
                        }
                        else
                        {
                            lbl_DriveSt1_0.Text = "정지";
                        }

                        if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_2, 4))
                        {
                            lbl_DriveSt2_4.Text = "튜닝중";
                            lbl_DriveSt2_4.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_DriveSt2_4.Text = "아님";
                            lbl_DriveSt2_4.BackColor = Color.Silver;
                        }

                        if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_2, 3))
                        {
                            lbl_DriveSt2_3.Text = "튜닝중";
                            lbl_DriveSt2_3.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_DriveSt2_3.Text = "아님";
                            lbl_DriveSt2_3.BackColor = Color.Silver;
                        }


                        if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_2, 2))
                        {
                            lbl_DriveSt2_2.Text = "확인완료";
                            lbl_DriveSt2_2.BackColor = Color.Lime;
                            lbl_DriveSt2_2.ForeColor = Color.Black;
                        }
                        else
                        {
                            lbl_DriveSt2_2.Text = "미확인";
                            lbl_DriveSt2_2.BackColor = Color.Red;
                            lbl_DriveSt2_2.ForeColor = Color.White;
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


                        lbl_Drive_Decel_1.Text = String.Format("{0}", DevSt->Drive_DisPosition.DecelNo_1);
                        lbl_Drive_Decel_2.Text = String.Format("{0}", DevSt->Drive_DisPosition.DecelNo_2);

                        lbl_Drive_Position.Text = String.Format("{0}", DevSt->Drive_DisPosition.Now_Position);
                        lbl_Drive_Speed.Text = String.Format("{0}", DevSt->Drive_DisPosition.Now_Speed);
                        lbl_Drive_Destination.Text = String.Format("{0}", DevSt->Drive_DisPosition.Dest_Position);
                        lbl_Drive_DestSpeed.Text = String.Format("{0}", DevSt->Drive_DisPosition.Dest_Speed);


                        if (Global_Class.BitStatus(DevSt->Updown_DisPosition.St_1, 5))
                        {
                            lbl_UpDownSt1_5.Text = "홈복귀";
                            lbl_UpDownSt1_5.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_UpDownSt1_5.Text = "아님";
                            lbl_UpDownSt1_5.BackColor = Color.Silver;
                        }
                        if (Global_Class.BitStatus(DevSt->Updown_DisPosition.St_1, 4))
                        {
                            lbl_UpDownSt1_4.Text = "정위치";
                            lbl_UpDownSt1_4.BackColor = Color.Lime;
                        }
                        else
                        {
                            lbl_UpDownSt1_4.Text = "아님";
                            lbl_UpDownSt1_4.BackColor = Color.Silver;
                        }
                        if (Global_Class.BitStatus(DevSt->Updown_DisPosition.St_1, 0))
                        {
                            if (Global_Class.BitStatus(DevSt->Updown_DisPosition.St_1, 3))
                            {
                                lbl_UpDownSt1_3.Text = "역방향";
                            }
                            else
                            {
                                lbl_UpDownSt1_3.Text = "정방향";
                            }
                        }
                        else
                        {
                            lbl_UpDownSt1_3.Text = "정지";
                        }
                        if (Global_Class.BitStatus(DevSt->Updown_DisPosition.St_1, 2))
                        {
                            lbl_UpDownSt1_2.Text = "감속";
                        }
                        else
                        {
                            lbl_UpDownSt1_2.Text = "아님";
                        }
                        if (Global_Class.BitStatus(DevSt->Updown_DisPosition.St_1, 1))
                        {
                            lbl_UpDownSt1_1.Text = "가속";
                        }
                        else
                        {
                            lbl_UpDownSt1_1.Text = "아님";
                        }
                        if (Global_Class.BitStatus(DevSt->Updown_DisPosition.St_1, 0))
                        {
                            lbl_UpDownSt1_0.Text = "동작중";
                        }
                        else
                        {
                            lbl_UpDownSt1_0.Text = "정지";
                        }

                        if (Global_Class.BitStatus(DevSt->Updown_DisPosition.St_2, 4))
                        {
                            lbl_UpDownSt2_4.Text = "튜닝중";
                            lbl_UpDownSt2_4.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_UpDownSt2_4.Text = "아님";
                            lbl_UpDownSt2_4.BackColor = Color.Silver;
                        }

                        if (Global_Class.BitStatus(DevSt->Updown_DisPosition.St_2, 3))
                        {
                            lbl_UpDownSt2_3.Text = "튜닝중";
                            lbl_UpDownSt2_3.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_UpDownSt2_3.Text = "아님";
                            lbl_UpDownSt2_3.BackColor = Color.Silver;
                        }

                        if (Global_Class.BitStatus(DevSt->Updown_DisPosition.St_2, 2))
                        {
                            lbl_UpDownSt2_2.Text = "확인완료";
                            lbl_UpDownSt2_2.BackColor = Color.Lime;
                            lbl_UpDownSt2_2.ForeColor = Color.Black;
                        }
                        else
                        {
                            lbl_UpDownSt2_2.Text = "미확인";
                            lbl_UpDownSt2_2.BackColor = Color.Red;
                            lbl_UpDownSt2_2.ForeColor = Color.White;
                        }

                        if (Global_Class.BitStatus(DevSt->Updown_DisPosition.St_2, 0))
                        {
                            if (Global_Class.BitStatus(DevSt->Updown_DisPosition.St_2, 1))
                            {
                                lbl_UpDownSt2_1.Text = "장애";
                                lbl_UpDownSt2_1.BackColor = Color.Red;
                                lbl_UpDownSt2_1.ForeColor = Color.White;
                            }
                            else
                            {
                                lbl_UpDownSt2_1.Text = "정상";
                                lbl_UpDownSt2_1.BackColor = Color.Lime;
                                lbl_UpDownSt2_1.ForeColor = Color.Black;
                            }
                        }
                        else
                        {
                            lbl_UpDownSt2_1.Text = "미접속";
                            lbl_UpDownSt2_1.BackColor = Color.Red;
                            lbl_UpDownSt2_1.ForeColor = Color.White;
                        }


                        if (Global_Class.BitStatus(DevSt->Updown_DisPosition.St_2, 0))
                        {
                            lbl_UpDownSt2_0.Text = "접속";
                            lbl_UpDownSt2_0.BackColor = Color.Lime;
                            lbl_UpDownSt2_0.ForeColor = Color.Black;
                        }
                        else
                        {
                            lbl_UpDownSt2_0.Text = "미접속";
                            lbl_UpDownSt2_0.BackColor = Color.Red;
                            lbl_UpDownSt2_0.ForeColor = Color.White;
                        }



                        lbl_UpDown_Decel_1.Text = String.Format("{0}", DevSt->Updown_DisPosition.DecelNo_1);
                        lbl_UpDown_Decel_2.Text = String.Format("{0}", DevSt->Updown_DisPosition.DecelNo_2);

                        lbl_UpDown_Position.Text = String.Format("{0}", DevSt->Updown_DisPosition.Now_Position);
                        lbl_UpDown_Speed.Text = String.Format("{0}", DevSt->Updown_DisPosition.Now_Speed);
                        lbl_UpDown_Destination.Text = String.Format("{0}", DevSt->Updown_DisPosition.Dest_Position);
                        lbl_UpDown_DestSpeed.Text = String.Format("{0}", DevSt->Updown_DisPosition.Dest_Speed);


                        if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_1, 7))
                        {
                            lbl_Fork1St1_7.Text = "ON";
                            lbl_Fork1St1_7.BackColor = Color.Lime;
                            
                        }
                        else
                        {
                            lbl_Fork1St1_7.Text = "OFF";
                            lbl_Fork1St1_7.BackColor = Color.Silver;
                            
                        }

                        if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_1, 6))
                        {
                            lbl_Fork1St1_6.Text = "ON";
                            lbl_Fork1St1_6.BackColor = Color.Lime;
                            
                        }
                        else
                        {
                            lbl_Fork1St1_6.Text = "OFF";
                            lbl_Fork1St1_6.BackColor = Color.Silver;
                            
                        }

                        if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_1, 5))
                        {
                            lbl_Fork1St1_5.Text = "있음";
                            lbl_Fork1St1_5.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_Fork1St1_5.Text = "없음";
                            lbl_Fork1St1_5.BackColor = Color.Silver;
                        }

                        if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_1, 4))
                        {
                            lbl_Fork1St1_4.Text = "정위치";
                            lbl_Fork1St1_4.BackColor = Color.Lime;
                        }
                        else
                        {
                            lbl_Fork1St1_4.Text = "아님";
                            lbl_Fork1St1_4.BackColor = Color.Silver;
                        }

                        if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_1, 0))
                        {
                            if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_1, 3))
                            {
                                lbl_Fork1St1_3.Text = "역방향";
                            }
                            else
                            {
                                lbl_Fork1St1_3.Text = "정방향";
                            }
                        }
                        else
                        {
                            lbl_Fork1St1_3.Text = "정지";
                        }
                        if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_1, 2))
                        {
                            lbl_Fork1St1_2.Text = "감속";
                        }
                        else
                        {
                            lbl_Fork1St1_2.Text = "아님";
                        }
                        if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_1, 1))
                        {
                            lbl_Fork1St1_1.Text = "가속";
                        }
                        else
                        {
                            lbl_Fork1St1_1.Text = "아님";
                        }
                        if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_1, 0))
                        {
                            lbl_Fork1St1_0.Text = "동작중";
                        }
                        else
                        {
                            lbl_Fork1St1_0.Text = "정지";
                        }

                        if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_2, 7))
                        {
                            lbl_Fork1St2_7.Text = "동작";
                            lbl_Fork1St2_7.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_Fork1St2_7.Text = "--";
                            lbl_Fork1St2_7.BackColor = Color.Silver;
                        }

                        if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_2, 6))
                        {
                            lbl_Fork1St2_6.Text = "동작";
                            lbl_Fork1St2_6.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_Fork1St2_6.Text = "--";
                            lbl_Fork1St2_6.BackColor = Color.Silver;
                        }



                        if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_2, 4))
                        {
                            lbl_Fork1St2_4.Text = "튜닝중";
                            lbl_Fork1St2_4.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_Fork1St2_4.Text = "아님";
                            lbl_Fork1St2_4.BackColor = Color.Silver;
                        }

                        if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_2, 3))
                        {
                            lbl_Fork1St2_3.Text = "튜닝중";
                            lbl_Fork1St2_3.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_Fork1St2_3.Text = "아님";
                            lbl_Fork1St2_3.BackColor = Color.Silver;
                        }

                        if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_2, 2))
                        {
                            lbl_Fork1St2_2.Text = "확인완료";
                            lbl_Fork1St2_2.BackColor = Color.Lime;
                            lbl_Fork1St2_2.ForeColor = Color.Black;
                        }
                        else
                        {
                            lbl_Fork1St2_2.Text = "미확인";
                            lbl_Fork1St2_2.BackColor = Color.Red;
                            lbl_Fork1St2_2.ForeColor = Color.White;
                        }


                        if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_2, 0))
                        {
                            if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_2, 1))
                            {
                                lbl_Fork1St2_1.Text = "장애";
                                lbl_Fork1St2_1.BackColor = Color.Red;
                                lbl_Fork1St2_1.ForeColor = Color.White;
                            }
                            else
                            {
                                lbl_Fork1St2_1.Text = "정상";
                                lbl_Fork1St2_1.BackColor = Color.Lime;
                                lbl_Fork1St2_1.ForeColor = Color.Black;
                            }
                        }
                        else
                        {
                            lbl_Fork1St2_1.Text = "미접속";
                            lbl_Fork1St2_1.BackColor = Color.Red;
                            lbl_Fork1St2_1.ForeColor = Color.White;
                        }

                        if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_2, 0))
                        {
                            lbl_Fork1St2_0.Text = "접속";
                            lbl_Fork1St2_0.BackColor = Color.Lime;
                            lbl_Fork1St2_0.ForeColor = Color.Black;
                        }
                        else
                        {
                            lbl_Fork1St2_0.Text = "미접속";
                            lbl_Fork1St2_0.BackColor = Color.Red;
                            lbl_Fork1St2_0.ForeColor = Color.White;
                        }

                        lbl_Fork1_HaveItemType.Text = String.Format("{0}", DevSt->Fork1_DisPosition.HaveItemType);

                        if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.ItemExist, 0))
                        {
                            lbl_Fork1_Existitem.Text = "감지";
                            lbl_Fork1_Existitem.BackColor = Color.Yellow;

                        }
                        else
                        {
                            lbl_Fork1_Existitem.Text = "미감지";
                            lbl_Fork1_Existitem.BackColor = Color.Silver;

                        }

                        lbl_Fork1_Position.Text = String.Format("{0}", DevSt->Fork1_DisPosition.Now_Position);
                        lbl_Fork1_Speed.Text = String.Format("{0}", DevSt->Fork1_DisPosition.Now_Speed);
                        lbl_Fork1_Destination.Text = String.Format("{0}", DevSt->Fork1_DisPosition.Dest_Position);
                        lbl_Fork1_DestSpeed.Text = String.Format("{0}", DevSt->Fork1_DisPosition.Dest_Speed);

                        //
                        if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_1, 7))
                        {
                            lbl_Fork2St1_7.Text = "ON";
                            lbl_Fork2St1_7.BackColor = Color.Lime;
                            
                        }
                        else
                        {
                            lbl_Fork2St1_7.Text = "OFF";
                            lbl_Fork2St1_7.BackColor = Color.Silver;
                            
                        }

                        if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_1, 6))
                        {
                            lbl_Fork2St1_6.Text = "ON";
                            lbl_Fork2St1_6.BackColor = Color.Lime;
                            
                        }
                        else
                        {
                            lbl_Fork2St1_6.Text = "OFF";
                            lbl_Fork2St1_6.BackColor = Color.Silver;
                            
                        }

                        if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_1, 5))
                        {
                            lbl_Fork2St1_5.Text = "있음";
                            lbl_Fork2St1_5.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_Fork2St1_5.Text = "없음";
                            lbl_Fork2St1_5.BackColor = Color.Silver;
                        }



                        if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_1, 4))
                        {
                            lbl_Fork2St1_4.Text = "정위치";
                            lbl_Fork2St1_4.BackColor = Color.Lime;
                        }
                        else
                        {
                            lbl_Fork2St1_4.Text = "아님";
                            lbl_Fork2St1_4.BackColor = Color.Silver;
                        }



                        if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_1, 0))
                        {
                            if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_1, 3))
                            {
                                lbl_Fork2St1_3.Text = "역방향";
                            }
                            else
                            {
                                lbl_Fork2St1_3.Text = "정방향";
                            }
                        }
                        else
                        {
                            lbl_Fork2St1_3.Text = "정지";
                        }
                        if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_1, 2))
                        {
                            lbl_Fork2St1_2.Text = "감속";
                        }
                        else
                        {
                            lbl_Fork2St1_2.Text = "아님";
                        }
                        if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_1, 1))
                        {
                            lbl_Fork2St1_1.Text = "가속";
                        }
                        else
                        {
                            lbl_Fork2St1_1.Text = "아님";
                        }
                        if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_1, 0))
                        {
                            lbl_Fork2St1_0.Text = "동작중";
                        }
                        else
                        {
                            lbl_Fork2St1_0.Text = "정지";
                        }

                        if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_2, 7))
                        {
                            lbl_Fork2St2_7.Text = "동작";
                            lbl_Fork2St2_7.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_Fork2St2_7.Text = "--";
                            lbl_Fork2St2_7.BackColor = Color.Silver;
                        }

                        if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_2, 6))
                        {
                            lbl_Fork2St2_6.Text = "동작";
                            lbl_Fork2St2_6.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_Fork2St2_6.Text = "--";
                            lbl_Fork2St2_6.BackColor = Color.Silver;
                        }

                        if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_2, 4))
                        {
                            lbl_Fork2St2_4.Text = "튜닝중";
                            lbl_Fork2St2_4.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_Fork2St2_4.Text = "아님";
                            lbl_Fork2St2_4.BackColor = Color.Silver;
                        }

                        if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_2, 3))
                        {
                            lbl_Fork2St2_3.Text = "튜닝중";
                            lbl_Fork2St2_3.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lbl_Fork2St2_3.Text = "아님";
                            lbl_Fork2St2_3.BackColor = Color.Silver;
                        }

                        if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_2, 2))
                        {
                            lbl_Fork2St2_2.Text = "확인완료";
                            lbl_Fork2St2_2.BackColor = Color.Lime;
                            lbl_Fork2St2_2.ForeColor = Color.Black;
                        }
                        else
                        {
                            lbl_Fork2St2_2.Text = "미확인";
                            lbl_Fork2St2_2.BackColor = Color.Red;
                            lbl_Fork2St2_2.ForeColor = Color.White;
                        }


                        if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_2, 0))
                        {
                            if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_2, 1))
                            {
                                lbl_Fork2St2_1.Text = "장애";
                                lbl_Fork2St2_1.BackColor = Color.Red;
                                lbl_Fork2St2_1.ForeColor = Color.White;
                            }
                            else
                            {
                                lbl_Fork2St2_1.Text = "정상";
                                lbl_Fork2St2_1.BackColor = Color.Lime;
                                lbl_Fork2St2_1.ForeColor = Color.Black;
                            }
                        }
                        else
                        {
                            lbl_Fork2St2_1.Text = "미접속";
                            lbl_Fork2St2_1.BackColor = Color.Red;
                            lbl_Fork2St2_1.ForeColor = Color.White;
                        }

                        if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_2, 0))
                        {
                            lbl_Fork2St2_0.Text = "접속";
                            lbl_Fork2St2_0.BackColor = Color.Lime;
                            lbl_Fork2St2_0.ForeColor = Color.Black;
                        }
                        else
                        {
                            lbl_Fork2St2_0.Text = "미접속";
                            lbl_Fork2St2_0.BackColor = Color.Red;
                            lbl_Fork2St2_0.ForeColor = Color.White;
                        }

                        lbl_Fork2_HaveItemType.Text = String.Format("{0}", DevSt->Fork2_DisPosition.HaveItemType);

                        if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.ItemExist, 0))
                        {
                            lbl_Fork2_Existitem.Text = "감지";
                            lbl_Fork2_Existitem.BackColor = Color.Yellow;

                        }
                        else
                        {
                            lbl_Fork2_Existitem.Text = "미감지";
                            lbl_Fork2_Existitem.BackColor = Color.Silver;

                        }

                        lbl_Fork2_Position.Text = String.Format("{0}", DevSt->Fork2_DisPosition.Now_Position);
                        lbl_Fork2_Speed.Text = String.Format("{0}", DevSt->Fork2_DisPosition.Now_Speed);
                        lbl_Fork2_Destination.Text = String.Format("{0}", DevSt->Fork2_DisPosition.Dest_Position);
                        lbl_Fork2_DestSpeed.Text = String.Format("{0}", DevSt->Fork2_DisPosition.Dest_Speed);
                    }
                }
            } catch
            {

            }

        }

        public unsafe void Display_DevSt()
        {
            if ((form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) && (form_Main.COMMDataManager.CommSt != 0))
            {
                Display_SRM_BasicSt();
                Display_Fork_Position();
                Display_D_UD_Fork_St();
                Display_Sub_ForkJob();
                Display_Sub_TaskList();
                Display_Sub_DIO();
                Display_InvErr();
                Display_SI();
            } else
            {
                Display_Init();
                Display_Sub_DIO();
            }
        }
        #endregion

        private unsafe void button1_Click(object sender, EventArgs e)
        {
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_IN[0] = 0x01;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_IN[1] = 0x02;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_IN[2] = 0x04;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_IN[3] = 0x01;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_IN[4] = 0x10;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_IN[5] = 0x20;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_IN[6] = 0x40;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_IN[7] = 0x80;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_IN[8] = 0x01;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_IN[9] = 0x02;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_IN[10] = 0x04;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_IN[11] = 0x08;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_IN[12] = 0x10;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_IN[13] = 0x20;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_IN[14] = 0x40;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_IN[15] = 0x80;

            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_OUT[0] = 0x01;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_OUT[1] = 0x02;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_OUT[2] = 0x04;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_OUT[3] = 0x08;
            form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.IO_Digital_OUT[4] = 0x10;
        }

        private void rb_DIO_DigitalIn_1_Click(object sender, EventArgs e)
        {
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus)
                {
                    Display_Sub_DIO();
                }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void rb_DIO_DigitalIn_1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label338_Click(object sender, EventArgs e)
        {

        }
    }

}

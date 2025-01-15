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
    public partial class Form_RTVSt : Form
    {
        public Form_Main form_Main;
        private Label[] lbl_DI_Title;
        private Label[] lbl_DI_ST;
        private Label[] lbl_DO_Title;
        private Label[] lbl_DO_ST;

        public Form_RTVSt()
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
        private void Form_RTVSt_Load(object sender, EventArgs e)
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

        private unsafe void Display_Sub_FeedJob()
        {
            fixed (VEXI_DEFS.TRTV_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt)
            {
                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
                    //반송 or Task : 수행하고 있는(실패상태 포함) 혹은 마지막 수행완료된 작업
                    if (DevSt->FF1_Job.Item_Do_Status == 4)
                    {
                        lbl_Feed1_Job.Text = string.Format("{0} (완료)", DevSt->FF1_Job.Item_JobNumber);
                    }
                    else
                    {
                        lbl_Feed1_Job.Text = string.Format("{0}", DevSt->FF1_Job.Item_JobNumber);
                    }
                    if (DevSt->FF1_Job.taskIndex == 0)
                    {
                        lbl_Feed1_TaskIndex.Text = "";
                    }
                    else
                    {
                        lbl_Feed1_TaskIndex.Text = string.Format("{0}", DevSt->FF1_Job.taskIndex);
                    }

                    lbl_Feed1_Cmd.Text = Global_Class.UTIL_GetJobTextAsValue(DevSt->FF1_Job.Item_CMD_Code);

                    if (DevSt->FF1_Job.taskIndex == 0)
                    {
                        lbl_Feed1_From.Text = string.Format("S{0}-P{1}", DevSt->FF1_Job.Item_From.Station
                                                          , DevSt->FF1_Job.Item_From.Position);
                    }
                    else
                    {
                        lbl_Feed1_From.Text = "";
                    }
                    lbl_Feed1_To.Text = string.Format("S{0}-P{1}", DevSt->FF1_Job.Item_To.Station
                                                      , DevSt->FF1_Job.Item_To.Position);
                    switch (DevSt->FF1_Job.Item_Do_Status)
                    {
                        case 0: lbl_Feed1_jobSt.Text = "지령없음"; break;
                        case 2: lbl_Feed1_jobSt.Text = "수행중"; break;
                        case 3: lbl_Feed1_jobSt.Text = "실패"; break;
                        case 4: lbl_Feed1_jobSt.Text = "완료"; break;
                        default: lbl_Feed1_jobSt.Text = string.Format("0x{0:X2}", DevSt->FF1_Job.Item_Do_Status); break;
                    }

                    if (DevSt->FF1_Job.taskIndex == 0)
                    {
                        lbl_Feed1_jobStep.Text = Global_Class.UTIL_GetJobStepTextAsValue(DevSt->FF1_Job.Item_Do_Step);
                    }
                    else
                    {
                        lbl_Feed1_jobStep.Text = Global_Class.UTIL_GetTaskStepTextAsValue(DevSt->FF1_Job.Item_Do_Step);
                    }

                    //반송 or Task : 수행하고 있는(실패상태 포함) 혹은 마지막 수행완료된 작업
                    if (DevSt->FF2_Job.Item_Do_Status == 4)
                    {
                        lbl_Feed2_Job.Text = string.Format("{0} (완료)", DevSt->FF2_Job.Item_JobNumber);
                    }
                    else
                    {
                        lbl_Feed2_Job.Text = string.Format("{0}", DevSt->FF2_Job.Item_JobNumber);
                    }
                    if (DevSt->FF2_Job.taskIndex == 0)
                    {
                        lbl_Feed2_TaskIndex.Text = "";
                    }
                    else
                    {
                        lbl_Feed2_TaskIndex.Text = string.Format("{0}", DevSt->FF2_Job.taskIndex);
                    }
                    
                    lbl_Feed2_Cmd.Text = Global_Class.UTIL_GetJobTextAsValue(DevSt->FF2_Job.Item_CMD_Code);

                    if (DevSt->FF2_Job.taskIndex == 0)
                    {
                        lbl_Feed2_From.Text = string.Format("S{0}-P{1}", DevSt->FF2_Job.Item_From.Station
                                                          , DevSt->FF2_Job.Item_From.Position);
                    }
                    else
                    {
                        lbl_Feed2_From.Text = "";
                    }
                    lbl_Feed2_To.Text = string.Format("S{0}-P{1}", DevSt->FF2_Job.Item_To.Station
                                                      , DevSt->FF2_Job.Item_To.Position);
                    switch (DevSt->FF2_Job.Item_Do_Status)
                    {
                        case 0: lbl_Feed2_jobSt.Text = "지령없음"; break;
                        case 2: lbl_Feed2_jobSt.Text = "수행중"; break;
                        case 3: lbl_Feed2_jobSt.Text = "실패"; break;
                        case 4: lbl_Feed2_jobSt.Text = "완료"; break;
                        default: lbl_Feed2_jobSt.Text = string.Format("0x{0:X2}", DevSt->FF2_Job.Item_Do_Status); break;
                    }

                    if (DevSt->FF2_Job.taskIndex == 0)
                    {
                        lbl_Feed2_jobStep.Text = Global_Class.UTIL_GetJobStepTextAsValue(DevSt->FF2_Job.Item_Do_Step);
                    }
                    else
                    {
                        lbl_Feed2_jobStep.Text = Global_Class.UTIL_GetTaskStepTextAsValue(DevSt->FF2_Job.Item_Do_Step);
                    }
                    
                }
            }
        }
        private unsafe void Display_Sub_TaskList()
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
                }
            }
            fixed (VEXI_DEFS.TRTV_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt)
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

                    fixed (VEXI_DEFS.TRTV_TaskJobItem* TaskJobPtr = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.TaskJobItem_1)
                    {
                        for (byte i = 0; i < 20; i++)
                        {

                            switch ((TaskJobPtr+i)->Cmd)
                            {
                                case 0x00: lv_TaskJob.Items[i].SubItems[1].Text = "None"; break;
                                case 0x01: lv_TaskJob.Items[i].SubItems[1].Text = "Move"; break;
                                case 0x02: lv_TaskJob.Items[i].SubItems[1].Text = "Loading"; break;
                                case 0x03: lv_TaskJob.Items[i].SubItems[1].Text = "Unloading"; break;
                                default: lv_TaskJob.Items[i].SubItems[1].Text = string.Format("{0}", (TaskJobPtr + i)->Cmd); break;
                            }
                            switch ((TaskJobPtr + i)->WorkStatus)
                            {
                                case 0: lv_TaskJob.Items[i].SubItems[2].Text = "None"; break;
                                case 1: lv_TaskJob.Items[i].SubItems[2].Text = "대기"; break;
                                case 2: lv_TaskJob.Items[i].SubItems[2].Text = "수행중"; break;
                                case 3: lv_TaskJob.Items[i].SubItems[2].Text = "실패"; break;
                                case 4: lv_TaskJob.Items[i].SubItems[2].Text = "완료"; break;
                                default: lv_TaskJob.Items[i].SubItems[2].Text = string.Format("{0}", (TaskJobPtr + i)->WorkStatus); break;
                            }

                            switch ((TaskJobPtr + i)->Feed)
                            {
                                case 1: lv_TaskJob.Items[i].SubItems[3].Text = "Feeding 1"; break;
                                case 2: lv_TaskJob.Items[i].SubItems[3].Text = "Feeding 2"; break;
                                default: lv_TaskJob.Items[i].SubItems[3].Text = ""; break;
                            }
                            lv_TaskJob.Items[i].SubItems[4].Text = string.Format("S{0}-P{1}", (TaskJobPtr + i)->To.Station, (TaskJobPtr + i)->To.Position);
                        }
                    }
                }
            }
        }
        private unsafe void Display_Sub_DIO()
        {
            byte SelectDIindex = 0;
            byte SelectDOindex = 0;
            byte Loop;
            byte ByteIndex, BitIndex;

            fixed (VEXI_DEFS.TRTV_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt)
            {
                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
                    //I/O
                    if (rb_DIO_DigitalIn_1.Checked) SelectDIindex = 1;
                    if (rb_DIO_DigitalIn_2.Checked) SelectDIindex = 2;
                    if (rb_DIO_DigitalIn_3.Checked) SelectDIindex = 3;
                    if (rb_DIO_DigitalOut_1.Checked) SelectDOindex = 4;
                    if (rb_DIO_DigitalOut_2.Checked) SelectDOindex = 5;


                    switch (SelectDIindex)
                    {
                        case 1:
                            for (Loop = 1; Loop <= 38; Loop++)
                            {
                                if (Loop > ConstClass.RTV_DI_Names.GetLength(0))
                                {
                                    lbl_DI_Title[Loop - 1].BackColor = Color.Gray;
                                    lbl_DI_Title[Loop - 1].Text = "";

                                    lbl_DI_ST[Loop - 1].BackColor = Color.Gray;
                                    lbl_DI_ST[Loop - 1].Text = "";
                                }
                                else
                                {
                                    lbl_DI_Title[Loop - 1].BackColor = System.Drawing.SystemColors.Highlight;
                                    lbl_DI_Title[Loop - 1].Text = ConstClass.RTV_DI_Names[Loop - 1, 0];
                                }
                            }
                            break;
                        case 2:
                            for (Loop = 39; Loop <= 76; Loop++)
                            {
                                if (Loop > ConstClass.RTV_DI_Names.GetLength(0))
                                {
                                    lbl_DI_Title[Loop - 39].BackColor = Color.Gray;
                                    lbl_DI_Title[Loop - 39].Text = "";

                                    lbl_DI_ST[Loop - 39].BackColor = Color.Gray;
                                    lbl_DI_ST[Loop - 39].Text = "";
                                }
                                else
                                {
                                    lbl_DI_Title[Loop - 39].BackColor = System.Drawing.SystemColors.Highlight;
                                    lbl_DI_Title[Loop - 39].Text = ConstClass.RTV_DI_Names[Loop - 1, 0];
                                }
                            }
                            break;
                        case 3:
                            for (Loop = 77; Loop <= 114; Loop++)
                            {
                                if (Loop > ConstClass.RTV_DI_Names.GetLength(0))
                                {
                                    lbl_DI_Title[Loop - 77].BackColor = Color.Gray;
                                    lbl_DI_Title[Loop - 77].Text = "";

                                    lbl_DI_ST[Loop - 77].BackColor = Color.Gray;
                                    lbl_DI_ST[Loop - 77].Text = "";
                                }
                                else
                                {
                                    lbl_DI_Title[Loop - 77].BackColor = System.Drawing.SystemColors.Highlight;
                                    lbl_DI_Title[Loop - 77].Text = ConstClass.RTV_DI_Names[Loop - 1, 0];
                                }
                            }
                            break;
                    }
                    switch (SelectDOindex)
                    {

                        case 4:
                            for (Loop = 1; Loop <= 38; Loop++)
                            {
                                if (Loop > ConstClass.RTV_DO_Names_1.GetLength(0))
                                {
                                    lbl_DO_Title[Loop - 1].BackColor = Color.Gray;
                                    lbl_DO_Title[Loop - 1].Text = "";

                                    lbl_DO_ST[Loop - 1].BackColor = Color.Gray;
                                    lbl_DO_ST[Loop - 1].Text = "";
                                } else
                                {
                                    lbl_DO_Title[Loop - 1].BackColor = Color.DarkOliveGreen;
                                    lbl_DO_Title[Loop - 1].Text = ConstClass.RTV_DO_Names_1[Loop - 1, 0];
                                }
                            }
                            break;
                        case 5:
                            for (Loop = 39; Loop <= 76; Loop++)
                            {
                                if (Loop > ConstClass.RTV_DO_Names_1.GetLength(0))
                                {
                                    if (Loop <= (ConstClass.RTV_DO_Names_1.GetLength(0) + ConstClass.RTV_DO_Names_2.GetLength(0)))
                                    {
                                        lbl_DO_Title[Loop - 39].BackColor = Color.DarkOliveGreen;
                                        lbl_DO_Title[Loop - 39].Text = ConstClass.RTV_DO_Names_2[Loop - ConstClass.RTV_DO_Names_1.GetLength(0) - 1, 0];
                                    }
                                    else
                                    {
                                        lbl_DO_Title[Loop - 39].BackColor = Color.Gray;
                                        lbl_DO_Title[Loop - 39].Text = "";

                                        lbl_DO_ST[Loop - 39].BackColor = Color.Gray;
                                        lbl_DO_ST[Loop - 39].Text = "";
                                    }
                                }
                                else
                                {
                                    lbl_DO_Title[Loop - 39].BackColor = Color.DarkOliveGreen;
                                    lbl_DO_Title[Loop - 39].Text = ConstClass.RTV_DO_Names_1[Loop - 1, 0];
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

                                if (Loop <= ConstClass.RTV_DI_Names.GetLength(0))
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

                                if (Loop <= ConstClass.RTV_DI_Names.GetLength(0))
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
                        case 3:
                            for (Loop = 77; Loop <= 114; Loop++)
                            {
                                ByteIndex = (byte)((Loop - 1) / 8);
                                BitIndex = (byte)((Loop - 1) % 8);

                                if (Loop <= ConstClass.RTV_DI_Names.GetLength(0))
                                {

                                    if (Global_Class.BitStatus(DevSt->IO_Digital_IN[ByteIndex], BitIndex))
                                    {
                                        lbl_DI_ST[Loop - 77].BackColor = Color.Yellow;
                                        lbl_DI_ST[Loop - 77].Text = "ON";
                                    }
                                    else
                                    {
                                        lbl_DI_ST[Loop - 77].BackColor = Color.Silver;
                                        lbl_DI_ST[Loop - 77].Text = "OFF";
                                    }
                                }
                            }
                            break;
                    }
                    switch (SelectDOindex)
                    {
                        case 4:
                            for (Loop = 1; Loop <= 38; Loop++)
                            {
                                ByteIndex = (byte)((Loop - 1) / 8);
                                BitIndex = (byte)((Loop - 1) % 8);

                                if (Loop <= ConstClass.RTV_DO_Names_1.GetLength(0))
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


                                if (Loop <= (ConstClass.RTV_DO_Names_1.GetLength(0) + ConstClass.RTV_DO_Names_2.GetLength(0)))
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

        private void Display_Init()
        {
            //Display_RTV_BasicSt 내 갱신 컴포넌트들
            lblVersion.Text = "";
            lblSystemTimeUTC.Text = "";
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
            lbl_Dev_Error.Text = "";
            lbl_Dev_Error.BackColor = Color.White;
            lbl_DevEmergencySwitch.Text = "";
            lbl_DevEmergencySwitch.BackColor = Color.White;
            lbl_Dev_ActionCode.Text = "";

            lbl_RTV_RailType.Text = "";
            lblProjectNo.Text = "";
            lblGroupNum.Text = "";
            lblHOGINum.Text = "";


            //Display_Drive_St 내 갱신 컴포넌트들
            lbl_Dev_Home.Text = "";
            lbl_Dev_Home.BackColor = Color.White;
            lbl_Dev_Maintance.Text = "";
            lbl_Dev_Maintance.BackColor = Color.White;
            lbl_DriveSt1_4.Text = "";
            lbl_DriveSt1_4.BackColor = Color.White;
            lbl_DriveSt1_5.Text = "";
            lbl_DriveSt1_5.BackColor = Color.White;
            lbl_DriveSt1_6.Text = "";
            lbl_DriveSt1_6.BackColor = Color.White;
            lbl_DriveSt1_3.Text = "";
            lbl_DriveSt1_2.Text = "";
            lbl_DriveSt1_1.Text = "";
            lbl_DriveSt1_0.Text = "";
            lbl_DriveSt2_1.Text = "";
            lbl_DriveSt2_1.BackColor = Color.White;
            lbl_DriveSt2_0.Text = "";
            lbl_DriveSt2_0.BackColor = Color.White;
            lbl_CanWork_StationIndex.Text = "";
            lbl_DriveSt1_5.Text = "";
            lbl_DriveSt1_6.Text = "";
            lbl_Drive_Position.Text = "";
            lbl_Drive_Speed.Text = "";
            lbl_Drive_Destination.Text = "";
            lbl_Drive_DestSpeed.Text = "";
            lbl_DriveAreaInfo_RegionSt_0.BackColor = Color.White;
            lbl_DriveAreaInfo_RegionSt_1.BackColor = Color.White;
            lbl_DriveAreaInfo_RegionSt_2.BackColor = Color.White;
            lbl_DriveBarcodeErrCount.Text = "";
            

            //Display_Feed_St 내 갱신 컴포넌트들
            lbl_Drive_CurrentPos_Feed1.Text = "";
            lbl_Drive_CurrentPos_Feed1.BackColor = Color.White;
            lbl_Drive_CurrentStation_Feed1.Text = "";
            lbl_Drive_CurrentStation_Feed1.BackColor = Color.White;
            lbl_Feed1_Pos.Text = "";
            lbl_Feed1_Dest.Text = "";
            lbl_Feed1St1_4.Text = "";
            lbl_Feed1St1_4.BackColor = Color.White;
            lbl_Feed1St1_6.Text = "";
            lbl_Feed1St1_6.BackColor = Color.White;
            lbl_Feed1St1_5.Text = "";
            lbl_Feed1St1_5.BackColor = Color.White;
            lbl_Feed1St1_3.Text = "";
            lbl_Feed1St1_2.Text = "";
            lbl_Feed1St1_1.Text = "";
            lbl_Feed1St1_0.Text = "";
            lbl_Feed1St2_1.Text = "";
            lbl_Feed1St2_1.BackColor = Color.White;
            lbl_Feed1St2_0.Text = "";
            lbl_Feed1St2_0.BackColor = Color.White;
            lbl_Feed1_Speed.Text = "";
            lbl_Feed1_DestSpeed.Text = "";

            lbl_Drive_CurrentPos_Feed2.Text = "";
            lbl_Drive_CurrentPos_Feed2.BackColor = Color.White;
            lbl_Drive_CurrentStation_Feed2.Text = "";
            lbl_Drive_CurrentStation_Feed2.BackColor = Color.White;
            lbl_Feed2_Pos.Text = "";
            lbl_Feed2_Dest.Text = "";
            lbl_Feed2St1_4.Text = "";
            lbl_Feed2St1_4.BackColor = Color.White;
            lbl_Feed2St1_6.Text = "";
            lbl_Feed2St1_6.BackColor = Color.White;
            lbl_Feed2St1_5.Text = "";
            lbl_Feed2St1_5.BackColor = Color.White;
            lbl_Feed2St1_3.Text = "";
            lbl_Feed2St1_2.Text = "";
            lbl_Feed2St1_1.Text = "";
            lbl_Feed2St1_0.Text = "";
            lbl_Feed2St2_1.Text = "";
            lbl_Feed2St2_1.BackColor = Color.White;
            lbl_Feed2St2_0.Text = "";
            lbl_Feed2St2_0.BackColor = Color.White;
            lbl_Feed2_Speed.Text = "";
            lbl_Feed2_DestSpeed.Text = "";

            //Display_Sub_TaskList
            lbl_TaskJobNumber.Text = "";
            lbl_TaskJobSt.Text = "";
            lv_TaskJob.Items.Clear();

            //Display_Sub_FeedJob
            lbl_Feed1_Job.Text = "";
            lbl_Feed1_TaskIndex.Text = "";
            lbl_Feed1_Cmd.Text = "";
            lbl_Feed1_From.Text = "";
            lbl_Feed1_To.Text = "";
            lbl_Feed1_jobSt.Text = "";
            lbl_Feed1_jobStep.Text = "";

            lbl_Feed2_Job.Text = "";
            lbl_Feed2_TaskIndex.Text = "";
            lbl_Feed2_Cmd.Text = "";
            lbl_Feed2_From.Text = "";
            lbl_Feed2_To.Text = "";
            lbl_Feed2_jobSt.Text = "";
            lbl_Feed2_jobStep.Text = "";

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

            //Display_RemocongKey_St
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


            //Display_UpperSystem
            lbl_RCSSt_0.Text = "";
            lbl_RCSSt_0.BackColor = Color.White;
            lbl_RCS_InterLockIn_1_Index.Text = "";
            lbl_RCS_InterLockIn_1_Data.Text = "";
            lbl_RCS_InterLockIn_2_Index.Text = "";
            lbl_RCS_InterLockIn_2_Data.Text = "";
            lbl_RCS_InterLockIn_3_Index.Text = "";
            lbl_RCS_InterLockIn_3_Data.Text = "";
            lbl_RCS_InterLockIn_4_Index.Text = "";
            lbl_RCS_InterLockIn_4_Data.Text = "";
            lbl_RCS_InterLockOut_1_Index.Text = "";
            lbl_RCS_InterLockOut_1_Data.Text = "";
            lbl_RCS_InterLockOut_2_Index.Text = "";
            lbl_RCS_InterLockOut_2_Data.Text = "";
            lbl_RCS_InterLockOut_3_Index.Text = "";
            lbl_RCS_InterLockOut_3_Data.Text = "";
            lbl_RCS_InterLockOut_4_Index.Text = "";
            lbl_RCS_InterLockOut_4_Data.Text = "";

            lbl_WCS14_StatusRx_Count.Text = "";
            lbl_WCS14_ControlRx_Count.Text = "";
            lbl_WCS14_ST1_7.Text = "";
            lbl_WCS14_ST1_7.BackColor = Color.White;
            lbl_WCS14_ST1_6.Text = "";
            lbl_WCS14_ST1_6.BackColor = Color.White;
            lbl_WCS14_ST1_5.Text = "";
            lbl_WCS14_ST1_5.BackColor = Color.White;
            lbl_WCS14_ST1_4.Text = "";
            lbl_WCS14_ST1_4.BackColor = Color.White;
            lbl_WCS14_ST1_3.Text = "";
            lbl_WCS14_ST1_3.BackColor = Color.White;
            lbl_WCS14_ST1_2.Text = "";
            lbl_WCS14_ST1_2.BackColor = Color.White;
            lbl_WCS14_ST1_Speed.Text = "";
            lbl_WCS14_ST2_7.Text = "";
            lbl_WCS14_ST2_7.BackColor = Color.White;
            lbl_WCS14_ST2_Position.Text = "";
            lbl_WCS14_CTRL1_7.Text = "";
            lbl_WCS14_CTRL1_7.BackColor = Color.White;
            lbl_WCS14_CTRL1_6.Text = "";
            lbl_WCS14_CTRL1_6.BackColor = Color.White;
            lbl_WCS14_CTRL1_5.Text = "";
            lbl_WCS14_CTRL1_5.BackColor = Color.White;
            lbl_WCS14_CTRL1_4.Text = "";
            lbl_WCS14_CTRL1_4.BackColor = Color.White;
            lbl_WCS14_CTRL1_3.Text = "";
            lbl_WCS14_CTRL1_3.BackColor = Color.White;
            lbl_WCS14_CTRL1_2.Text = "";
            lbl_WCS14_CTRL1_2.BackColor = Color.White;
            lbl_WCS14_CTRL1_0.Text = "";
            lbl_WCS14_CTRL1_0.BackColor = Color.White;
            lbl_WCS14_CTRL2_Dest.Text = "";


            //Display_AreaSpeedInfo
            lbl_DriveAreaInfo_AreaNo.Text = "";
            lbl_DriveAreaInfo_AreaType.Text = "";
            lbl_DriveAreaInfo_StartMM.Text = "";
            lbl_DriveAreaInfo_EndMM.Text = "";
            lbl_DriveAreaInfo_MaxSpeed.Text = "";
            lbl_DriveAreaInfo_PrevArea.Text = "";
            lbl_DriveAreaInfo_NextArea.Text = "";
            lbl_DriveAreaInfo_SensorIndex.Text = "";
            lbl_DriveAreaInfo_Region_0.BackColor = Color.White;
            lbl_DriveAreaInfo_Region_1.BackColor = Color.White;
            lbl_DriveAreaInfo_Region_2.BackColor = Color.White;
            lbl_DriveAreaInfo_AreaType_2.BackColor = Color.White;
            lbl_DriveAreaInfo_AreaType_3.BackColor = Color.White;
            lbl_DriveAreaInfo_AreaType_4.BackColor = Color.White;
            lbl_DriveAreaInfo_AreaType_5.BackColor = Color.White;

            lbl_Feed1_Station.Text = "";
            lbl_Feed1_Station_Direction.Text = "";
            lbl_Feed1_Station_InputCan.Text = "";
            lbl_Feed1_Station_OutputCan.Text = "";

            lbl_Feed2_Station.Text = "";
            lbl_Feed2_Station_Direction.Text = "";
            lbl_Feed2_Station_InputCan.Text = "";
            lbl_Feed2_Station_OutputCan.Text = "";

            //Display_Collision
            lbl_Front_Collision_RTVID.Text = "";
            lbl_Front_Collision_RxTime.Text = "";
            c.Text = "";
            lbl_Front_Collision_StopDistance.Text = "";
            lbl_Front_Collision_StartDistance.Text = "";
            lbl_Front_Collision_AreaType.Text = "";
            lbl_Front_Collision_MyAreaType.Text = "";

            lbl_Rear_Collision_RTVID.Text = "";
            lbl_Rear_Collision_RxTime.Text = "";
            lbl_Rear_Collision_GapDistance.Text = "";
            lbl_Rear_Collision_StopDistance.Text = "";
            lbl_Rear_Collision_StartDistance.Text = "";
            lbl_Rear_Collision_AreaType.Text = "";
            lbl_Rear_Collision_MyAreaType.Text = "";
        }

        private unsafe void Display_UpperSystem()
        {
            fixed (VEXI_DEFS.TRTV_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt)
            {
                //지상반
                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
                    if (Global_Class.BitStatus(DevSt->ControllerSt, 0))
                    {
                        lbl_RCSSt_0.Text = "ON";
                        lbl_RCSSt_0.BackColor = Color.Red;
                        lbl_RCSSt_0.ForeColor = Color.White;
                    }
                    else
                    {
                        lbl_RCSSt_0.Text = "OFF";
                        lbl_RCSSt_0.BackColor = Color.Silver;
                        lbl_RCSSt_0.ForeColor = Color.Black;
                    }

                    fixed (VEXI_DEFS.TRTV_REC_Interlock* InterlockPtr = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.CV_Interlock)
                    {

                        lbl_RCS_InterLockIn_1_Index.Text = String.Format("{0}", (InterlockPtr + 0)->StationIndex);
                        lbl_RCS_InterLockIn_2_Index.Text = String.Format("{0}", (InterlockPtr + 1)->StationIndex);
                        lbl_RCS_InterLockIn_3_Index.Text = String.Format("{0}", (InterlockPtr + 2)->StationIndex);
                        lbl_RCS_InterLockIn_4_Index.Text = String.Format("{0}", (InterlockPtr + 3)->StationIndex);

                        lbl_RCS_InterLockIn_1_Data.Text = String.Format("0x{0:X2}", (InterlockPtr + 0)->Data);
                        lbl_RCS_InterLockIn_2_Data.Text = String.Format("0x{0:X2}", (InterlockPtr + 1)->Data);
                        lbl_RCS_InterLockIn_3_Data.Text = String.Format("0x{0:X2}", (InterlockPtr + 2)->Data);
                        lbl_RCS_InterLockIn_4_Data.Text = String.Format("0x{0:X2}", (InterlockPtr + 3)->Data);
                    }

                    fixed (VEXI_DEFS.TRTV_REC_Interlock* InterlockPtr = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.Dev_Interlock)
                    {

                        lbl_RCS_InterLockOut_1_Index.Text = String.Format("{0}", (InterlockPtr + 0)->StationIndex);
                        lbl_RCS_InterLockOut_2_Index.Text = String.Format("{0}", (InterlockPtr + 1)->StationIndex);
                        lbl_RCS_InterLockOut_3_Index.Text = String.Format("{0}", (InterlockPtr + 2)->StationIndex);
                        lbl_RCS_InterLockOut_4_Index.Text = String.Format("{0}", (InterlockPtr + 3)->StationIndex);

                        lbl_RCS_InterLockOut_1_Data.Text = String.Format("0x{0:X2}", (InterlockPtr + 0)->Data);
                        lbl_RCS_InterLockOut_2_Data.Text = String.Format("0x{0:X2}", (InterlockPtr + 1)->Data);
                        lbl_RCS_InterLockOut_3_Data.Text = String.Format("0x{0:X2}", (InterlockPtr + 2)->Data);
                        lbl_RCS_InterLockOut_4_Data.Text = String.Format("0x{0:X2}", (InterlockPtr + 3)->Data);
                    }
                }

                //WCS (14bytes) 데이터
                lbl_WCS14_StatusRx_Count.Text = String.Format("{0}", DevSt->RTVWCS14byte.StatusRx_Count);
                lbl_WCS14_ControlRx_Count.Text = String.Format("{0}", DevSt->RTVWCS14byte.ControlRx_Count);
                if (Global_Class.BitStatus(DevSt->RTVWCS14byte.ST_1, 7))
                {
                    lbl_WCS14_ST1_7.Text = "ON";
                    lbl_WCS14_ST1_7.BackColor = Color.Yellow;
                } else
                {
                    lbl_WCS14_ST1_7.Text = "OFF";
                    lbl_WCS14_ST1_7.BackColor = Color.Silver;
                }
                if (Global_Class.BitStatus(DevSt->RTVWCS14byte.ST_1, 6))
                {
                    lbl_WCS14_ST1_6.Text = "ON";
                    lbl_WCS14_ST1_6.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_WCS14_ST1_6.Text = "OFF";
                    lbl_WCS14_ST1_6.BackColor = Color.Silver;
                }
                if (Global_Class.BitStatus(DevSt->RTVWCS14byte.ST_1, 5))
                {
                    lbl_WCS14_ST1_5.Text = "ON";
                    lbl_WCS14_ST1_5.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_WCS14_ST1_5.Text = "OFF";
                    lbl_WCS14_ST1_5.BackColor = Color.Silver;
                }
                if (Global_Class.BitStatus(DevSt->RTVWCS14byte.ST_1, 4))
                {
                    lbl_WCS14_ST1_4.Text = "ON";
                    lbl_WCS14_ST1_4.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_WCS14_ST1_4.Text = "OFF";
                    lbl_WCS14_ST1_4.BackColor = Color.Silver;
                }
                if (Global_Class.BitStatus(DevSt->RTVWCS14byte.ST_1, 3))
                {
                    lbl_WCS14_ST1_3.Text = "ON";
                    lbl_WCS14_ST1_3.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_WCS14_ST1_3.Text = "OFF";
                    lbl_WCS14_ST1_3.BackColor = Color.Silver;
                }
                if (Global_Class.BitStatus(DevSt->RTVWCS14byte.ST_1, 2))
                {
                    lbl_WCS14_ST1_2.Text = "ON";
                    lbl_WCS14_ST1_2.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_WCS14_ST1_2.Text = "OFF";
                    lbl_WCS14_ST1_2.BackColor = Color.Silver;
                }
                lbl_WCS14_ST1_Speed.Text = String.Format("{0}", DevSt->RTVWCS14byte.ST_1 & 0x03);
                if (Global_Class.BitStatus(DevSt->RTVWCS14byte.ST_2, 7))
                {
                    lbl_WCS14_ST2_7.Text = "ON";
                    lbl_WCS14_ST2_7.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_WCS14_ST2_7.Text = "OFF";
                    lbl_WCS14_ST2_7.BackColor = Color.Silver;
                }
                lbl_WCS14_ST2_Position.Text = String.Format("{0}", DevSt->RTVWCS14byte.ST_2 & 0x7F);

                
                if (Global_Class.BitStatus(DevSt->RTVWCS14byte.CTRL_1, 7))
                {
                    lbl_WCS14_CTRL1_7.Text = "ON";
                    lbl_WCS14_CTRL1_7.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_WCS14_CTRL1_7.Text = "OFF";
                    lbl_WCS14_CTRL1_7.BackColor = Color.Silver;
                }
                if (Global_Class.BitStatus(DevSt->RTVWCS14byte.CTRL_1, 6))
                {
                    lbl_WCS14_CTRL1_6.Text = "ON";
                    lbl_WCS14_CTRL1_6.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_WCS14_CTRL1_6.Text = "OFF";
                    lbl_WCS14_CTRL1_6.BackColor = Color.Silver;
                }
                if (Global_Class.BitStatus(DevSt->RTVWCS14byte.CTRL_1, 5))
                {
                    lbl_WCS14_CTRL1_5.Text = "ON";
                    lbl_WCS14_CTRL1_5.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_WCS14_CTRL1_5.Text = "OFF";
                    lbl_WCS14_CTRL1_5.BackColor = Color.Silver;
                }
                if (Global_Class.BitStatus(DevSt->RTVWCS14byte.CTRL_1, 4))
                {
                    lbl_WCS14_CTRL1_4.Text = "ON";
                    lbl_WCS14_CTRL1_4.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_WCS14_CTRL1_4.Text = "OFF";
                    lbl_WCS14_CTRL1_4.BackColor = Color.Silver;
                }
                if (Global_Class.BitStatus(DevSt->RTVWCS14byte.CTRL_1, 3))
                {
                    lbl_WCS14_CTRL1_3.Text = "ON";
                    lbl_WCS14_CTRL1_3.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_WCS14_CTRL1_3.Text = "OFF";
                    lbl_WCS14_CTRL1_3.BackColor = Color.Silver;
                }
                if (Global_Class.BitStatus(DevSt->RTVWCS14byte.CTRL_1, 2))
                {
                    lbl_WCS14_CTRL1_2.Text = "ON";
                    lbl_WCS14_CTRL1_2.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_WCS14_CTRL1_2.Text = "OFF";
                    lbl_WCS14_CTRL1_2.BackColor = Color.Silver;
                }
                if (Global_Class.BitStatus(DevSt->RTVWCS14byte.CTRL_1, 0))
                {
                    lbl_WCS14_CTRL1_0.Text = "ON";
                    lbl_WCS14_CTRL1_0.BackColor = Color.Yellow;
                }
                else
                {
                    lbl_WCS14_CTRL1_0.Text = "OFF";
                    lbl_WCS14_CTRL1_0.BackColor = Color.Silver;
                }
                lbl_WCS14_CTRL2_Dest.Text = String.Format("{0}", DevSt->RTVWCS14byte.CTRL_2 & 0x7F);
            }
        }

        private unsafe void Display_Collision()
        {
            fixed (VEXI_DEFS.TRTV_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt)
            {

                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
                    //Display_Collision
                    lbl_Front_Collision_RTVID.Text = String.Format("{0}", DevSt->BeforeCar_Collision.RTV_ID);
                    lbl_Front_Collision_RxTime.Text = String.Format("{0}", DevSt->BeforeCar_Collision.RxTime);
                    c.Text = String.Format("{0}", DevSt->BeforeCar_Collision.GapOtherCar);
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


                    lbl_Rear_Collision_RTVID.Text = String.Format("{0}", DevSt->AfterCar_Collision.RTV_ID);
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
            }
        }

        private unsafe void Display_AreaSpeedInfo()
        {
            fixed (VEXI_DEFS.TRTV_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt)
            {

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

                    if ((DevSt->DriveAreaInfo.Area_Type & 0x04) == 0x00)
                    {
                        lbl_DriveAreaInfo_AreaType_2.BackColor = Color.Silver;
                    }
                    else
                    {
                        lbl_DriveAreaInfo_AreaType_2.BackColor = Color.Yellow;
                    }

                    if ((DevSt->DriveAreaInfo.Area_Type & 0x08) == 0x00)
                    {
                        lbl_DriveAreaInfo_AreaType_3.BackColor = Color.Silver;
                    }
                    else
                    {
                        lbl_DriveAreaInfo_AreaType_3.BackColor = Color.Yellow;
                    }

                    if ((DevSt->DriveAreaInfo.Area_Type & 0x10) == 0x00)
                    {
                        lbl_DriveAreaInfo_AreaType_4.BackColor = Color.Silver;
                    }
                    else
                    {
                        lbl_DriveAreaInfo_AreaType_4.BackColor = Color.Yellow;
                    }

                    if ((DevSt->DriveAreaInfo.Area_Type & 0x20) == 0x00)
                    {
                        lbl_DriveAreaInfo_AreaType_5.BackColor = Color.Silver;
                    }
                    else
                    {
                        lbl_DriveAreaInfo_AreaType_5.BackColor = Color.Yellow;
                    }

                    lbl_DriveAreaInfo_StartMM.Text  = string.Format("{0}", DevSt->DriveAreaInfo.Start_MM);
                    lbl_DriveAreaInfo_EndMM.Text    = string.Format("{0}", DevSt->DriveAreaInfo.End_MM);
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

                    lbl_Feed1_Station.Text = String.Format("S{0}", DevSt->Feed1_Position.PointRec.Station);
                    if (Global_Class.BitStatus(DevSt->Feed1_Position.StationInfo, 0))
                    {
                        lbl_Feed1_Station_Direction.Text = "우측";                        
                    } else
                    {
                        lbl_Feed1_Station_Direction.Text = "좌측";
                    }

                    if (Global_Class.BitStatus(DevSt->Feed1_Position.StationInfo, 1))
                    {
                        lbl_Feed1_Station_InputCan.Text = "가능";
                    }
                    else
                    {
                        lbl_Feed1_Station_InputCan.Text = "불가";
                    }

                    if (Global_Class.BitStatus(DevSt->Feed1_Position.StationInfo, 2))
                    {
                        lbl_Feed1_Station_OutputCan.Text = "가능";
                    }
                    else
                    {
                        lbl_Feed1_Station_OutputCan.Text = "불가";
                    }

                    lbl_Feed2_Station.Text = String.Format("S{0}", DevSt->Feed2_Position.PointRec.Station);
                    if (Global_Class.BitStatus(DevSt->Feed2_Position.StationInfo, 0))
                    {
                        lbl_Feed2_Station_Direction.Text = "우측";
                    }
                    else
                    {
                        lbl_Feed2_Station_Direction.Text = "좌측";
                    }

                    if (Global_Class.BitStatus(DevSt->Feed2_Position.StationInfo, 1))
                    {
                        lbl_Feed2_Station_InputCan.Text = "가능";
                    }
                    else
                    {
                        lbl_Feed2_Station_InputCan.Text = "불가";
                    }

                    if (Global_Class.BitStatus(DevSt->Feed2_Position.StationInfo, 2))
                    {
                        lbl_Feed2_Station_OutputCan.Text = "가능";
                    }
                    else
                    {
                        lbl_Feed2_Station_OutputCan.Text = "불가";
                    }

                }
            }
           
        }

        private unsafe void Display_RTV_BasicSt()
        {
            fixed (VEXI_DEFS.TRTV_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt)
            {
                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
                    //코딩
                    lblVersion.Text = Global_Class.UTIL_ByteToFVerstr(DevSt->FWversion);
                    DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(DevSt->SystemUTCTime);
                    lblSystemTimeUTC.Text = String.Format("{0}", PCtime);

                    var convertedArray = new byte[6];
                    System.Runtime.InteropServices.Marshal.Copy((IntPtr)DevSt->ProjectID, convertedArray, 0, 6);
                    //lblProjectNo.Text = System.Text.Encoding.Default.GetString(convertedArray);
                    lblProjectNo.Text = System.Text.Encoding.ASCII.GetString(convertedArray);
                    lblGroupNum.Text = string.Format("{0}", DevSt->GroupID);
                    lblHOGINum.Text = string.Format("{0}", DevSt->HogiID);

                    switch (DevSt->RailType)
                    {
                        case 0: lbl_RTV_RailType.Text = "직선형"; break;
                        case 1: lbl_RTV_RailType.Text = "루프형"; break;
                        case 2: lbl_RTV_RailType.Text = "곡선형"; break;
                        default: lbl_RTV_RailType.Text = "직선형"; break;
                    }

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

                    if (Global_Class.BitStatus(DevSt->DevSt_1, 3))
                    {
                        lbl_Dev_Error.Text = String.Format("{0}-{1}-{2}", DevSt->ErrorCode.MainCode, DevSt->ErrorCode.SubCode, DevSt->ErrorCode.PosCode) +
                                             "  " + Global_Class.UTIL_RTVAlarmName(DevSt->ErrorCode.MainCode, DevSt->ErrorCode.SubCode, DevSt->ErrorCode.PosCode);
                        lbl_Dev_Error.BackColor = Color.Red;
                        lbl_Dev_Error.ForeColor = Color.White;
                    }
                    else
                    {
                        if (Global_Class.BitStatus(DevSt->DevSt_1, 2))
                        {
                            //lbl_Dev_Error.Text = "경고";
                            lbl_Dev_Error.Text = String.Format("{0}-{1}-{2}", DevSt->ErrorCode.MainCode, DevSt->ErrorCode.SubCode, DevSt->ErrorCode.PosCode) +
                                             "  " + Global_Class.UTIL_RTVWarnningName(DevSt->ErrorCode.MainCode, DevSt->ErrorCode.SubCode, DevSt->ErrorCode.PosCode);
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

                    lbl_Dev_ActionCode.Text = Global_Class.UTIL_RTVActionStText(DevSt->ActionCode);
                }
            }

        }

        private unsafe void Display_Feed_St()
        {
            fixed (VEXI_DEFS.TRTV_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt)
            {
                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
                    lbl_Feed1_Pos.Text = String.Format("S{0}-P{1}", 
                                                       DevSt->Feed1_Position.PointRec.Station,
                                                       DevSt->Feed1_Position.PointRec.Position);
                    lbl_Feed1_Dest.Text = String.Format("S{0}-P{1}", 
                                                       DevSt->Feed1_Dest.Station, 
                                                       DevSt->Feed1_Dest.Position);

                    if (Global_Class.BitStatus(DevSt->Feed1_Position.CurrentPosition, 0))
                    {
                        lbl_Drive_CurrentPos_Feed1.Text = "정위치";
                        lbl_Drive_CurrentPos_Feed1.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_Drive_CurrentPos_Feed1.Text = "아님";
                        lbl_Drive_CurrentPos_Feed1.BackColor = Color.Silver;
                    }

                    if (Global_Class.BitStatus(DevSt->Feed1_Position.CurrentPosition, 1))
                    {
                        lbl_Drive_CurrentStation_Feed1.Text = "정위치";
                        lbl_Drive_CurrentStation_Feed1.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_Drive_CurrentStation_Feed1.Text = "아님";
                        lbl_Drive_CurrentStation_Feed1.BackColor = Color.Silver;
                    }

                    if (Global_Class.BitStatus(DevSt->Feed1_DisPosition.St_1, 4))
                    {
                        lbl_Feed1St1_4.Text = "감지";
                        lbl_Feed1St1_4.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_Feed1St1_4.Text = "미감지";
                        lbl_Feed1St1_4.BackColor = Color.Silver;
                    }
                    if (Global_Class.BitStatus(DevSt->Feed1_DisPosition.St_1, 6))
                    {
                        lbl_Feed1St1_6.Text = "감지";
                        lbl_Feed1St1_6.BackColor = Color.Red;
                        lbl_Feed1St1_6.ForeColor = Color.White;
                    }
                    else
                    {
                        lbl_Feed1St1_6.Text = "미감지";
                        lbl_Feed1St1_6.BackColor = Color.Silver;
                        lbl_Feed1St1_6.ForeColor = Color.Black;
                    }
                    if (Global_Class.BitStatus(DevSt->Feed1_DisPosition.St_1, 5))
                    {
                        lbl_Feed1St1_5.Text = "감지";
                        lbl_Feed1St1_5.BackColor = Color.Red;
                        lbl_Feed1St1_5.ForeColor = Color.White;
                    }
                    else
                    {
                        lbl_Feed1St1_5.Text = "미감지";
                        lbl_Feed1St1_5.BackColor = Color.Silver;
                        lbl_Feed1St1_5.ForeColor = Color.Black;
                    }
                    if (Global_Class.BitStatus(DevSt->Feed1_DisPosition.St_1, 0))
                    {
                        if (Global_Class.BitStatus(DevSt->Feed1_DisPosition.St_1, 3))
                        {
                            lbl_Feed1St1_3.Text = "우";
                        }
                        else
                        {
                            lbl_Feed1St1_3.Text = "좌";
                        }

                        if (Global_Class.BitStatus(DevSt->Feed1_DisPosition.St_1, 2))
                        {
                            lbl_Feed1St1_2.Text = "감속";
                        }
                        else
                        {
                            lbl_Feed1St1_2.Text = "아님";
                        }

                        if (Global_Class.BitStatus(DevSt->Feed1_DisPosition.St_1, 1))
                        {
                            lbl_Feed1St1_1.Text = "감속";
                        }
                        else
                        {
                            lbl_Feed1St1_1.Text = "아님";
                        }
                    }
                    else
                    {
                        lbl_Feed1St1_3.Text = "정지";
                        lbl_Feed1St1_2.Text = "아님";
                        lbl_Feed1St1_1.Text = "아님";
                    }

                    if (Global_Class.BitStatus(DevSt->Feed1_DisPosition.St_2, 0))
                    {
                        lbl_Feed1St2_0.Text = "접속";
                        lbl_Feed1St2_0.BackColor = Color.Lime;
                        lbl_Feed1St2_0.ForeColor = Color.Black;
                    }
                    else
                    {
                        lbl_Feed1St2_0.Text = "미접속";
                        lbl_Feed1St2_0.BackColor = Color.Red;
                        lbl_Feed1St2_0.ForeColor = Color.White;
                    }

                    if (Global_Class.BitStatus(DevSt->Feed1_DisPosition.St_2, 0))
                    {
                        if (Global_Class.BitStatus(DevSt->Feed1_DisPosition.St_2, 1))
                        {
                            lbl_Feed1St2_1.Text = "장애";
                            lbl_Feed1St2_1.BackColor = Color.Red;
                            lbl_Feed1St2_1.ForeColor = Color.White;
                        }
                        else
                        {
                            lbl_Feed1St2_1.Text = "정상";
                            lbl_Feed1St2_1.BackColor = Color.Lime;
                            lbl_Feed1St2_1.ForeColor = Color.Black;
                        }
                    } else
                    {
                        lbl_Feed1St2_1.Text = "미접속";
                        lbl_Feed1St2_1.BackColor = Color.Red;
                        lbl_Feed1St2_1.ForeColor = Color.White;
                    }
                    lbl_Feed1_Speed.Text = string.Format("{0:0.0}", (double)DevSt->Feed1_DisPosition.Now_Speed / 10);
                    lbl_Feed1_DestSpeed.Text = string.Format("{0:0.0}", (double)DevSt->Feed1_DisPosition.Dest_Speed / 10);


                    lbl_Feed2_Pos.Text = String.Format("S{0}-P{1}",
                                                       DevSt->Feed2_Position.PointRec.Station,
                                                       DevSt->Feed2_Position.PointRec.Position);
                    lbl_Feed2_Dest.Text = String.Format("S{0}-P{1}",
                                                       DevSt->Feed2_Dest.Station,
                                                       DevSt->Feed2_Dest.Position);

                    if (Global_Class.BitStatus(DevSt->Feed2_Position.CurrentPosition, 0))
                    {
                        lbl_Drive_CurrentPos_Feed2.Text = "정위치";
                        lbl_Drive_CurrentPos_Feed2.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_Drive_CurrentPos_Feed2.Text = "아님";
                        lbl_Drive_CurrentPos_Feed2.BackColor = Color.Silver;
                    }

                    if (Global_Class.BitStatus(DevSt->Feed2_Position.CurrentPosition, 1))
                    {
                        lbl_Drive_CurrentStation_Feed2.Text = "정위치";
                        lbl_Drive_CurrentStation_Feed2.BackColor = Color.Lime;
                    }
                    else
                    {
                        lbl_Drive_CurrentStation_Feed2.Text = "아님";
                        lbl_Drive_CurrentStation_Feed2.BackColor = Color.Silver;
                    }


                    if (Global_Class.BitStatus(DevSt->Feed2_DisPosition.St_1, 4))
                    {
                        lbl_Feed2St1_4.Text = "감지";
                        lbl_Feed2St1_4.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_Feed2St1_4.Text = "미감지";
                        lbl_Feed2St1_4.BackColor = Color.Silver;
                    }
                    if (Global_Class.BitStatus(DevSt->Feed2_DisPosition.St_1, 6))
                    {
                        lbl_Feed2St1_6.Text = "감지";
                        lbl_Feed2St1_6.BackColor = Color.Red;
                        lbl_Feed2St1_6.ForeColor = Color.White;
                    }
                    else
                    {
                        lbl_Feed2St1_6.Text = "미감지";
                        lbl_Feed2St1_6.BackColor = Color.Silver;
                        lbl_Feed2St1_6.ForeColor = Color.Black;
                    }
                    if (Global_Class.BitStatus(DevSt->Feed2_DisPosition.St_1, 5))
                    {
                        lbl_Feed2St1_5.Text = "감지";
                        lbl_Feed2St1_5.BackColor = Color.Red;
                        lbl_Feed2St1_5.ForeColor = Color.White;
                    }
                    else
                    {
                        lbl_Feed2St1_5.Text = "미감지";
                        lbl_Feed2St1_5.BackColor = Color.Silver;
                        lbl_Feed2St1_5.ForeColor = Color.Black;
                    }
                    if (Global_Class.BitStatus(DevSt->Feed2_DisPosition.St_1, 0))
                    {
                        if (Global_Class.BitStatus(DevSt->Feed2_DisPosition.St_1, 3))
                        {
                            lbl_Feed2St1_3.Text = "우";
                        }
                        else
                        {
                            lbl_Feed2St1_3.Text = "좌";
                        }

                        if (Global_Class.BitStatus(DevSt->Feed2_DisPosition.St_1, 2))
                        {
                            lbl_Feed2St1_2.Text = "감속";
                        }
                        else
                        {
                            lbl_Feed2St1_2.Text = "아님";
                        }

                        if (Global_Class.BitStatus(DevSt->Feed2_DisPosition.St_1, 1))
                        {
                            lbl_Feed2St1_1.Text = "감속";
                        }
                        else
                        {
                            lbl_Feed2St1_1.Text = "아님";
                        }
                    }
                    else
                    {
                        lbl_Feed2St1_3.Text = "정지";
                        lbl_Feed2St1_2.Text = "아님";
                        lbl_Feed2St1_1.Text = "아님";
                    }

                    if (Global_Class.BitStatus(DevSt->Feed2_DisPosition.St_2, 0))
                    {
                        lbl_Feed2St2_0.Text = "접속";
                        lbl_Feed2St2_0.BackColor = Color.Lime;
                        lbl_Feed2St2_0.ForeColor = Color.Black;
                    }
                    else
                    {
                        lbl_Feed2St2_0.Text = "미접속";
                        lbl_Feed2St2_0.BackColor = Color.Red;
                        lbl_Feed2St2_0.ForeColor = Color.White;
                    }

                    if (Global_Class.BitStatus(DevSt->Feed2_DisPosition.St_2, 0))
                    {
                        if (Global_Class.BitStatus(DevSt->Feed2_DisPosition.St_2, 1))
                        {
                            lbl_Feed2St2_1.Text = "장애";
                            lbl_Feed2St2_1.BackColor = Color.Red;
                            lbl_Feed2St2_1.ForeColor = Color.White;
                        }
                        else
                        {
                            lbl_Feed2St2_1.Text = "정상";
                            lbl_Feed2St2_1.BackColor = Color.Lime;
                            lbl_Feed2St2_1.ForeColor = Color.Black;
                        }
                    } else
                    {
                        lbl_Feed2St2_1.Text = "미접속";
                        lbl_Feed2St2_1.BackColor = Color.Red;
                        lbl_Feed2St2_1.ForeColor = Color.White;
                    }
                    lbl_Feed2_Speed.Text = string.Format("{0:0.0}", (double)DevSt->Feed2_DisPosition.Now_Speed / 10);
                    lbl_Feed2_DestSpeed.Text = string.Format("{0:0.0}", (double)DevSt->Feed2_DisPosition.Dest_Speed / 10);

                }
            }
        }

        private unsafe void Display_Drive_St()
        {
            fixed (VEXI_DEFS.TRTV_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt)
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

                    if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_1, 5))
                    {
                        lbl_DriveSt1_5.Text = "ON";
                        lbl_DriveSt1_5.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_DriveSt1_5.Text = "OFF";
                        lbl_DriveSt1_5.BackColor = Color.Silver;
                    }

                    if (Global_Class.BitStatus(DevSt->Drive_DisPosition.St_1, 6))
                    {
                        lbl_DriveSt1_6.Text = "ON";
                        lbl_DriveSt1_6.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_DriveSt1_6.Text = "OFF";
                        lbl_DriveSt1_6.BackColor = Color.Silver;
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
                    } else
                    {
                        lbl_DriveSt2_1.Text = "미접속";
                        lbl_DriveSt2_1.BackColor = Color.Red;
                        lbl_DriveSt2_1.ForeColor = Color.White;
                    }

                    if ((DevSt->CanWorkStation.Station == 0xFF) && (DevSt->CanWorkStation.Position == 0xFF))
                    {
                        lbl_CanWork_StationIndex.Text = "-";
                    } else
                    {
                        lbl_CanWork_StationIndex.Text = string.Format("S{0}-P{1}", DevSt->CanWorkStation.Station
                                                                                 , DevSt->CanWorkStation.Position);
                    }
                    lbl_Drive_Position.Text = String.Format("{0}", DevSt->Drive_DisPosition.Now_Position);
                    lbl_Drive_Speed.Text = string.Format("{0:0.0}", (double)DevSt->Drive_DisPosition.Now_Speed / 10);
                    lbl_Drive_Destination.Text = String.Format("{0}", DevSt->Drive_DisPosition.Dest_Position);
                    lbl_Drive_DestSpeed.Text = string.Format("{0:0.0}", (double)DevSt->Drive_DisPosition.Dest_Speed / 10);

                    if ((DevSt->DriveAreaInfo.RegionSt & 0x01) == 0x00)
                    {
                        lbl_DriveAreaInfo_RegionSt_0.BackColor = Color.Silver;
                    }
                    else
                    {
                        lbl_DriveAreaInfo_RegionSt_0.BackColor = Color.Yellow;
                    }
                    if ((DevSt->DriveAreaInfo.RegionSt & 0x02) == 0x00)
                    {
                        lbl_DriveAreaInfo_RegionSt_1.BackColor = Color.Silver;
                    }
                    else
                    {
                        lbl_DriveAreaInfo_RegionSt_1.BackColor = Color.Yellow;
                    }
                    if ((DevSt->DriveAreaInfo.RegionSt & 0x04) == 0x00)
                    {
                        lbl_DriveAreaInfo_RegionSt_2.BackColor = Color.Silver;
                    }
                    else
                    {
                        lbl_DriveAreaInfo_RegionSt_2.BackColor = Color.Yellow;
                    }

                    lbl_DriveBarcodeErrCount.Text = string.Format("{0}", DevSt->BarcodeErrCount);

                }
            }
        }

        private unsafe void Display_RemocongKey_St()
        {
            fixed (VEXI_DEFS.TRTV_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt)
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
                Display_RTV_BasicSt();
                Display_Feed_St();
                Display_Drive_St();
                Display_Sub_FeedJob();
                Display_Sub_TaskList();
                Display_Sub_DIO();
                Display_RemocongKey_St();
                Display_AreaSpeedInfo();
                Display_Collision();
                Display_UpperSystem();
            } else
            {
                Display_Init();
                Display_Sub_DIO();
            }
        }
        #endregion




        private unsafe void button1_Click(object sender, EventArgs e)
        {
            form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.IO_Digital_IN[0] = 0x01;
            form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.IO_Digital_IN[1] = 0x02;
            form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.IO_Digital_IN[2] = 0x04;
            form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.IO_Digital_IN[3] = 0x01;
            form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.IO_Digital_IN[4] = 0x10;
            form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.IO_Digital_IN[5] = 0x20;
            form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.IO_Digital_IN[6] = 0x40;
            form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.IO_Digital_IN[7] = 0x80;

            form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.IO_Digital_OUT[0] = 0x01;
            form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.IO_Digital_OUT[1] = 0x02;
            form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.IO_Digital_OUT[2] = 0x04;
            form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.IO_Digital_OUT[3] = 0x08;
            form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.IO_Digital_OUT[4] = 0x10;
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Front_Collision_RTVID_Click(object sender, EventArgs e)
        {

        }

        private void rb_DIO_DigitalIn_1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

}

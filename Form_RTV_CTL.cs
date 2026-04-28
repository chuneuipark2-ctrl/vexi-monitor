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
    public partial class Form_RTV_CTL : Form
    {
        public Form_Main form_Main;
        private static ComboBox[] EditComboxBox;
        private ListViewItem lv_Item;

        private static VEXI_DEFS.TRTV_REC_TaskJobCTRL rtv_REC_TaskJob_CTRL;
        private static VEXI_DEFS.RTV_REC_JobCTRL rtv_REC_Job_CTRL;
        private static VEXI_DEFS.TRTV_REC_JobCTRLRES rtv_REC_Job_CTRLRes;


        public Form_RTV_CTL()
        {
            InitializeComponent();
            EditComboxBox = new ComboBox[] { null,
            /*                                 Lv_cb_Column1,
                                             Lv_cb_Column3,
                                             Lv_cb_Column4,
                                             Lv_cb_Column5
            */
                };
        }


        #region 컴포넌트 이벤트

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Hide_AllEdit();
            this.Text = "장비 운전 조작(" + tabControl1.SelectedTab.Text + ")";
        }

        private void btn_LoadDevTaskList_Click(object sender, EventArgs e)
        {
            //Hide_AllEdit();
            if ((form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) && (form_Main.COMMDataManager.CommSt != 0))
            {
              //  Display_Sub_TaskList_Ctrl(false);
            }
            else
            {
               // Display_Ctrl_Init();
            }
        }

        private void btn_TaskInit_Click(object sender, EventArgs e)
        {
            //Hide_AllEdit();
            //Display_Ctrl_Init();

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
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "홈위치로 이동 시키시겠습니까?"))
            {
                form_Main.Do_Ctrl_Cmd_withNoData(ConstClass.CMD2_51);
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

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "이동 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                //lbl_JobCtrlRes1.Visible = false;
                //lbl_JobCtrlRes2.Visible = false;
                //Do_Semi_MoveCMD_Ctrl(bt.Tag.ToString());
            }
        }

        private void btn_Input_Feed1_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "적재 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                //lbl_JobCtrlRes1.Visible = false;
                //lbl_JobCtrlRes2.Visible = false;

                //Do_Semi_LoadCMD_Ctrl(bt.Tag.ToString());
            }
        }

        private void btn_Output_Feed1_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "이재 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                //lbl_JobCtrlRes1.Visible = false;
                //lbl_JobCtrlRes2.Visible = false;

                //Do_Semi_UnLoadCMD_Ctrl(bt.Tag.ToString());
            }
        }

        private void btn_SToS_Feed1_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "스테이션간 반송 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                //lbl_JobCtrlRes1.Visible = false;
                //lbl_JobCtrlRes2.Visible = false;

                //Do_Semi_StoSCMD_Ctrl(bt.Tag.ToString());
            }
        }

        private void btn_ChangeS_Feed1_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "목적지 스테이션 변경 반송 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                //lbl_JobCtrlRes1.Visible = false;
                //lbl_JobCtrlRes2.Visible = false;

             //   Do_Semi_ChangeSCMD_Ctrl(bt.Tag.ToString());
            }
        }


        private void btn_TaskSet_Click(object sender, EventArgs e)
        {
            //lbl_JobCtrlRes1.Visible = false;
            //lbl_JobCtrlRes2.Visible = false;

            //Hide_AllEdit();
            /*
            if (lv_TaskJob_Ctrl.Items.Count == 20)
            {
                if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "Task 작업리스트를 전송하시겠습니까? (안전에 주의하세요)"))
                {
                    Do_TaskList_Ctrl();
                }
            }
            else
            {
                form_Main.GlobalObj.MsgBox_Info("전송할 Task 작업리스트가 작성되지 않았습니다.", "I");
            }
            */

        }

        private void btn_Taskbtn_TaskCancelEdit_Click(object sender, EventArgs e)
        {
            //Hide_AllEdit();
            if ((form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) && (form_Main.COMMDataManager.CommSt != 0))
            {
              //  Display_Sub_TaskList_Ctrl(true);
            }
            else
            {
                //Display_Ctrl_SelectedInit();
            }
        }
        private void lv_TaskJob_Ctrl_DoubleClick(object sender, EventArgs e)
        {
            bool once = false;
            /*
            if (lv_TaskJob_Ctrl.SelectedItems.Count == 1)
            {
                for (byte i = 0; i <= (lv_TaskJob_Ctrl.Columns.Count - 1); i++)
                {
                    if (EditComboxBox[i] != null)
                    {
                        if (Convert.ToByte(EditComboxBox[i].Tag.ToString()) != 1)
                        {
                            EditComboxBox[i].Visible = true;
                            EditComboxBox[i].BringToFront();
                            if (!once)
                            {
                                EditComboxBox[i].Focus();
                                once = true;
                            }
                        }
                    }
                }
            }
            */

        }

        private void lv_TaskJob_Ctrl_Enter(object sender, EventArgs e)
        {
            /*
            for (byte i = 0; i <= (lv_TaskJob_Ctrl.Columns.Count - 1); i++)
            {
                if (EditComboxBox[i] != null)
                {
                    EditComboxBox[i].Visible = false;
                }
            }
            */
        }

        /*
        private void lv_TaskJob_Ctrl_MouseUp(object sender, MouseEventArgs e)
        {
            lv_Item = this.lv_TaskJob_Ctrl.GetItemAt(e.X, e.Y);

            if (lv_Item != null)
            {
                Rectangle ClickedItem;

                for (byte i = 0; i <= (lv_TaskJob_Ctrl.Columns.Count - 1); i++)
                {
                    if (EditComboxBox[i] != null)
                    {
                        ClickedItem = lv_Item.SubItems[i].Bounds;

                        //화면에 안보이는 경우
                        if ((ClickedItem.Left + this.lv_TaskJob_Ctrl.Columns[i].Width) < 0)
                        {
                            EditComboxBox[i].Tag = 1;
                            return;
                        }
                        else if (ClickedItem.Left < 0) // 해당 컬럼의 Left가 화면을 벗어난 경우
                        {
                            EditComboxBox[i].Tag = 0;
                            if ((ClickedItem.Left + this.lv_TaskJob_Ctrl.Columns[i].Width) > this.lv_TaskJob_Ctrl.Width) // 해당 컬럼의 Right가 화면을 벗어난 경우
                            {
                                ClickedItem.Width = this.lv_TaskJob_Ctrl.Width;
                                ClickedItem.X = 0;
                            }
                            else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                            {
                                ClickedItem.Width = this.lv_TaskJob_Ctrl.Columns[i].Width + ClickedItem.Left;
                                ClickedItem.X = 2;
                            }
                        }
                        else // 해당 컬럼의 Left가 화면을 벗어나지 않은 경우
                        {
                            EditComboxBox[i].Tag = 0;
                            if ((ClickedItem.Left + this.lv_TaskJob_Ctrl.Columns[i].Width) > this.lv_TaskJob_Ctrl.Width)  // 해당 컬럼의 Left가 화면을 벗어난 경우
                            {
                                ClickedItem.Width = this.lv_TaskJob_Ctrl.Width - ClickedItem.Left;
                            }
                            else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                            {
                                ClickedItem.Width = this.lv_TaskJob_Ctrl.Columns[i].Width;
                            }
                        }

                        // Adjust the top to account for the location of the ListView.
                        ClickedItem.Y += this.lv_TaskJob_Ctrl.Top;
                        ClickedItem.X += this.lv_TaskJob_Ctrl.Left;

                        // Assign calculated bounds to the ComboBox.
                        EditComboxBox[i].Bounds = ClickedItem;

                        // Set default text for ComboBox to match the item that is clicked.
                        EditComboxBox[i].Text = lv_Item.SubItems[i].Text;
                    }
                }
            }
            else
            {
                for (byte i = 0; i <= (lv_TaskJob_Ctrl.Columns.Count - 1); i++)
                {
                    if (EditComboxBox[i] != null)
                    {
                        EditComboxBox[i].Visible = false;
                    }

                }
            }

        }

        private void lv_TaskJob_Ctrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (byte i = 0; i <= (lv_TaskJob_Ctrl.Columns.Count - 1); i++)
            {
                if (EditComboxBox[i] != null)
                {
                    EditComboxBox[i].Visible = false;
                }
            }
        }

        private void Lv_cb_Column1_VisibleChanged(object sender, EventArgs e)
        {
            if (lv_Item != null)
            {
                ComboBox TmpComboBox = (ComboBox)sender;

                for (byte i = 0; i <= (lv_TaskJob_Ctrl.Columns.Count - 1); i++)
                {
                    if (EditComboxBox[i] != null)
                    {
                        if (TmpComboBox == EditComboxBox[i])
                        {
                            if (!TmpComboBox.Visible)
                            {
                                lv_Item.SubItems[i].Text = EditComboxBox[i].Text;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 기능함수


        private void Hide_AllEdit()
        {
            for (byte i = 0; i <= (lv_TaskJob_Ctrl.Columns.Count - 1); i++)
            {
                if (EditComboxBox[i] != null)
                {
                    EditComboxBox[i].Visible = false;
                }
            }
        }

        public void Display_JobCtrlRes(byte[] Data)
        {
            rtv_REC_Job_CTRLRes = (VEXI_DEFS.TRTV_REC_JobCTRLRES)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TRTV_REC_JobCTRLRES));

            lbl_JobCtrlRes1.Visible = (rtv_REC_Job_CTRLRes.ResultRes != 0) && (rtv_REC_Job_CTRLRes.Work1_ResultRes != 0);
            lbl_JobCtrlRes2.Visible = (rtv_REC_Job_CTRLRes.ResultRes != 0) && (rtv_REC_Job_CTRLRes.Work2_ResultRes != 0);

            switch (rtv_REC_Job_CTRLRes.Work1_ResultRes)
            {
                case 0 : lbl_JobCtrlRes1.Text = string.Format("{0}", (rtv_REC_Job_CTRLRes.Work1_ResultRes)) + " 피딩 1 : 이상없음"; break;
                case 31: lbl_JobCtrlRes1.Text = string.Format("{0}", (rtv_REC_Job_CTRLRes.Work1_ResultRes)) + " 피딩 1 : 작업코드 이상"; break;
                case 33: lbl_JobCtrlRes1.Text = string.Format("{0}", (rtv_REC_Job_CTRLRes.Work1_ResultRes)) + " 피딩 1 : 작업수행중"; break;
                case 34: lbl_JobCtrlRes1.Text = string.Format("{0}", (rtv_REC_Job_CTRLRes.Work1_ResultRes)) + " 피딩 1 : 장애 상태"; break;
                case 35: lbl_JobCtrlRes1.Text = string.Format("{0}", (rtv_REC_Job_CTRLRes.Work1_ResultRes)) + " 피딩 1 : 시작 OFF"; break;
                default: lbl_JobCtrlRes1.Text = string.Format("{0}", (rtv_REC_Job_CTRLRes.Work1_ResultRes)) + " 피딩 1 : Unknown Nack"; break;
            }

            switch (rtv_REC_Job_CTRLRes.Work2_ResultRes)
            {
                case 0: lbl_JobCtrlRes2.Text = string.Format("{0}", (rtv_REC_Job_CTRLRes.Work2_ResultRes)) + " 피딩 2 : 이상없음"; break;
                case 31: lbl_JobCtrlRes2.Text = string.Format("{0}", (rtv_REC_Job_CTRLRes.Work2_ResultRes)) + " 피딩 2 : 작업코드 이상"; break;
                case 33: lbl_JobCtrlRes2.Text = string.Format("{0}", (rtv_REC_Job_CTRLRes.Work2_ResultRes)) + " 피딩 2 : 작업수행중"; break;
                case 34: lbl_JobCtrlRes2.Text = string.Format("{0}", (rtv_REC_Job_CTRLRes.Work2_ResultRes)) + " 피딩 2 : 장애 상태"; break;
                case 35: lbl_JobCtrlRes2.Text = string.Format("{0}", (rtv_REC_Job_CTRLRes.Work2_ResultRes)) + " 피딩 2 : 시작 OFF"; break;
                default: lbl_JobCtrlRes2.Text = string.Format("{0}", (rtv_REC_Job_CTRLRes.Work2_ResultRes)) + " 피딩 2 : Unknown Nack"; break;
            }
        }

        private void Display_Ctrl_Init()
        {
            lv_TaskJob_Ctrl.Items.Clear();


            for (byte i = 0; i < 20; i++)
            {
                ListViewItem item = lv_TaskJob_Ctrl.Items.Add(string.Format("{0}", i + 1));
                item.SubItems.Add("지령없음");
                item.SubItems.Add("Feeding1");
                item.SubItems.Add("0");
                item.SubItems.Add("0");

            }
        }

        private unsafe void Display_Sub_TaskList_Ctrl(bool onlySelected)
        {
            if (lv_TaskJob_Ctrl.Items.Count == 0)
            {
                Display_Ctrl_Init();
            }

            fixed (VEXI_DEFS.TRTV_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt)
            {
                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
                    //Task List
                    fixed (VEXI_DEFS.TRTV_TaskJobItem* TaskJobPtr = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.TaskJobItem_1)
                    {
                        VEXI_DEFS.TRTV_TaskJobItem* Ptr = TaskJobPtr;
                        for (byte i = 0; i < 20; i++)
                        {

                            if ((!onlySelected) || ((onlySelected) && lv_TaskJob_Ctrl.Items[i].Selected))
                            {

                                switch (Ptr->Cmd)
                                {
                                    case ConstClass.SEMI_NONE: lv_TaskJob_Ctrl.Items[i].SubItems[1].Text = "지령없음"; break;
                                    case ConstClass.SEMI_MOVE: lv_TaskJob_Ctrl.Items[i].SubItems[1].Text = "Move"; break;
                                    case ConstClass.SEMI_TaskLoading: lv_TaskJob_Ctrl.Items[i].SubItems[1].Text = "Loading"; break;
                                    case ConstClass.SEMI_TaskUnLoading: lv_TaskJob_Ctrl.Items[i].SubItems[1].Text = "Unloading"; break;
                                    default: lv_TaskJob_Ctrl.Items[i].SubItems[1].Text = "지령없음"; break;
                                }

                                switch (Ptr->Feed)
                                {
                                    case 1: lv_TaskJob_Ctrl.Items[i].SubItems[2].Text = "Feeding1"; break;
                                    case 2: lv_TaskJob_Ctrl.Items[i].SubItems[2].Text = "Feeding2"; break;
                                    default: lv_TaskJob_Ctrl.Items[i].SubItems[2].Text = "Feeding1"; break;
                                }
                                lv_TaskJob_Ctrl.Items[i].SubItems[3].Text = string.Format("{0}", Ptr->To.Station);
                                lv_TaskJob_Ctrl.Items[i].SubItems[4].Text = string.Format("{0}", Ptr->To.Position);
                            }
                            Ptr = Ptr + 1;
                        }
                    }
                }
            }

        }

        private unsafe void Display_Sub_TaskList_St()
        {
            if (lv_TaskJob_St.Items.Count == 0)
            {
                for (byte i = 0; i < 20; i++)
                {
                    ListViewItem item = lv_TaskJob_St.Items.Add(string.Format("{0}", i + 1));
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
                        VEXI_DEFS.TRTV_TaskJobItem* Ptr = TaskJobPtr;
                        for (byte i = 0; i < 20; i++)
                        {

                            switch (Ptr->Cmd)
                            {
                                case ConstClass.SEMI_NONE: lv_TaskJob_St.Items[i].SubItems[1].Text = "지령없음"; break;
                                case ConstClass.SEMI_MOVE: lv_TaskJob_St.Items[i].SubItems[1].Text = "Move"; break;
                                case ConstClass.SEMI_TaskLoading: lv_TaskJob_St.Items[i].SubItems[1].Text = "Loading"; break;
                                case ConstClass.SEMI_TaskUnLoading: lv_TaskJob_St.Items[i].SubItems[1].Text = "Unloading"; break;
                                default: lv_TaskJob_St.Items[i].SubItems[1].Text = string.Format("{0}", Ptr->Cmd); break;
                            }
                            switch (Ptr->WorkStatus)
                            {
                                case 0: lv_TaskJob_St.Items[i].SubItems[2].Text = "None"; break;
                                case 1: lv_TaskJob_St.Items[i].SubItems[2].Text = "대기"; break;
                                case 2: lv_TaskJob_St.Items[i].SubItems[2].Text = "수행중"; break;
                                case 3: lv_TaskJob_St.Items[i].SubItems[2].Text = "실패"; break;
                                case 4: lv_TaskJob_St.Items[i].SubItems[2].Text = "완료"; break;
                                default: lv_TaskJob_St.Items[i].SubItems[2].Text = string.Format("{0}", Ptr->WorkStatus); break;
                            }


                            switch (Ptr->Feed)
                            {
                                case 1: lv_TaskJob_St.Items[i].SubItems[3].Text = "Feeding1"; break;
                                case 2: lv_TaskJob_St.Items[i].SubItems[3].Text = "Feeding2"; break;
                                default: lv_TaskJob_St.Items[i].SubItems[3].Text = ""; break;
                            }
                            lv_TaskJob_St.Items[i].SubItems[4].Text = string.Format("S{0}-P{1}", Ptr->To.Station
                                                      , Ptr->To.Position);
                            
                            Ptr = Ptr + 1;
                        }
                    }
                }
            }

        }


        private unsafe void Do_TaskList_Ctrl()
        {
            fixed (VEXI_DEFS.TRTV_REC_TaskJobCTRL* DevCtrl = &rtv_REC_TaskJob_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.TRTV_REC_TaskJobCTRL)));

                DevCtrl->taskWorkNum = form_Main.COMMDataManager.Random_WorkNum_AndInc;

                fixed (VEXI_DEFS.TRTV_TaskJobItem* TaskJobPtr = &rtv_REC_TaskJob_CTRL.TaskJobItem_1)
                {
                    for (byte i = 0; i < 20; i++)
                    {
                        if (lv_TaskJob_Ctrl.Items[i].SubItems[1].Text == "지령없음")
                        {
                            (TaskJobPtr + i)->Cmd = ConstClass.SEMI_NONE;
                            (TaskJobPtr + i)->Feed = 1;
                            (TaskJobPtr + i)->To.Station = 0;
                            (TaskJobPtr + i)->To.Position = 0;
                        }
                        else
                        {
                            if (lv_TaskJob_Ctrl.Items[i].SubItems[1].Text == "Move") (TaskJobPtr + i)->Cmd = ConstClass.SEMI_MOVE;
                            else if (lv_TaskJob_Ctrl.Items[i].SubItems[1].Text == "Loading") (TaskJobPtr + i)->Cmd = ConstClass.SEMI_TaskLoading;
                            else if (lv_TaskJob_Ctrl.Items[i].SubItems[1].Text == "Unloading") (TaskJobPtr + i)->Cmd = ConstClass.SEMI_TaskUnLoading;

                            if (lv_TaskJob_Ctrl.Items[i].SubItems[2].Text == "Feeding1") (TaskJobPtr + i)->Feed = 1;
                            else if (lv_TaskJob_Ctrl.Items[i].SubItems[2].Text == "Feeding2") (TaskJobPtr + i)->Feed = 2;

                            (TaskJobPtr + i)->To.Station = (byte)Global_Class.UTIL_StrToIntDef(lv_TaskJob_Ctrl.Items[i].SubItems[3].Text, 0);
                            (TaskJobPtr + i)->To.Position = (byte)Global_Class.UTIL_StrToIntDef(lv_TaskJob_Ctrl.Items[i].SubItems[4].Text, 0);
                        }
                    }
                }
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_40, rtv_REC_TaskJob_CTRL);
        }

        private unsafe void Do_Semi_ChangeSCMD_Ctrl(string CtrlType)
        {
            fixed (VEXI_DEFS.RTV_REC_JobCTRL* DevCtrl = &rtv_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.RTV_REC_JobCTRL)));

                switch (CtrlType)
                {
                    case ("F1"):
                        DevCtrl->CMD = ConstClass.SEMI_ChangeS;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_To.Station = (byte)numed_Feed1_Change.Value;
                        break;
                    case ("F2"):
                        DevCtrl->CMD = ConstClass.SEMI_ChangeS;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_To.Station = (byte)numed_Feed2_Change.Value;
                        break;
                    case ("ALL"):
                        DevCtrl->CMD = ConstClass.SEMI_ChangeS;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_To.Station = (byte)numed_Feed1_Change.Value;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_To.Station = (byte)numed_Feed2_Change.Value;
                        break;
                }
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, rtv_REC_Job_CTRL);
        }

        private unsafe void Do_Semi_StoSCMD_Ctrl(string CtrlType)
        {
            fixed (VEXI_DEFS.RTV_REC_JobCTRL* DevCtrl = &rtv_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.RTV_REC_JobCTRL)));

                switch (CtrlType)
                {
                    case ("F1"):
                        DevCtrl->CMD = ConstClass.SEMI_StoS;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_From.Station = (byte)numed_Feed1_StoS_From.Value;
                        DevCtrl->Work1_To.Station = (byte)numed_Feed1_StoS_To.Value;
                        break;
                    case ("F2"):
                        DevCtrl->CMD = ConstClass.SEMI_StoS;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_From.Station = (byte)numed_Feed2_StoS_From.Value;
                        DevCtrl->Work2_To.Station = (byte)numed_Feed2_StoS_To.Value;
                        break;
                    case ("ALL"):
                        DevCtrl->CMD = ConstClass.SEMI_StoS;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_From.Station = (byte)numed_Feed1_StoS_From.Value;
                        DevCtrl->Work1_To.Station = (byte)numed_Feed1_StoS_To.Value;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_From.Station = (byte)numed_Feed2_StoS_From.Value;
                        DevCtrl->Work2_To.Station = (byte)numed_Feed2_StoS_To.Value;
                        break;
                }
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, rtv_REC_Job_CTRL);
        }


        private unsafe void Do_Semi_LoadCMD_Ctrl(string CtrlType)
        {
            fixed (VEXI_DEFS.RTV_REC_JobCTRL* DevCtrl = &rtv_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.RTV_REC_JobCTRL)));

                switch (CtrlType)
                {
                    case ("F1"):
                        DevCtrl->CMD = ConstClass.SEMI_Loading;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_To.Station = (byte)numed_Feed1_Load.Value;
                        break;
                    case ("F2"):
                        DevCtrl->CMD = ConstClass.SEMI_Loading;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_To.Station = (byte)numed_Feed2_Load.Value;
                        break;
                    case ("ALL"):
                        DevCtrl->CMD = ConstClass.SEMI_Loading;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_To.Station = (byte)numed_Feed1_Load.Value;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_To.Station = (byte)numed_Feed2_Load.Value;
                        break;
                }
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, rtv_REC_Job_CTRL);
        }

        private unsafe void Do_Semi_UnLoadCMD_Ctrl(string CtrlType)
        {
            fixed (VEXI_DEFS.RTV_REC_JobCTRL* DevCtrl = &rtv_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.RTV_REC_JobCTRL)));

                switch (CtrlType)
                {
                    case ("F1"):
                        DevCtrl->CMD = ConstClass.SEMI_UnLoading;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_To.Station = (byte)numed_Feed1_Unload.Value;
                        break;
                    case ("F2"):
                        DevCtrl->CMD = ConstClass.SEMI_UnLoading;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_To.Station = (byte)numed_Feed2_Unload.Value;
                        break;
                    case ("ALL"):
                        DevCtrl->CMD = ConstClass.SEMI_UnLoading;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_To.Station = (byte)numed_Feed1_Unload.Value;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_To.Station = (byte)numed_Feed2_Unload.Value;
                        break;
                }
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, rtv_REC_Job_CTRL);
        }

        private unsafe void Do_Semi_MoveCMD_Ctrl(string CtrlType)
        {
            fixed (VEXI_DEFS.RTV_REC_JobCTRL* DevCtrl = &rtv_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.RTV_REC_JobCTRL)));

                switch (CtrlType)
                {
                    case "F1S":
                        DevCtrl->CMD = ConstClass.SEMI_MOVE;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_To.Station = (byte)numed_F1_Station.Value;
                        break;
                    case "F1P":
                        DevCtrl->CMD = ConstClass.SEMI_MOVE;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_To.Position = (byte)numed_F1_Position.Value;
                        break;

                    case "F2S":
                        DevCtrl->CMD = ConstClass.SEMI_MOVE;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_To.Station = (byte)numed_F2_Station.Value;
                        break;

                    case "F2P":
                        DevCtrl->CMD = ConstClass.SEMI_MOVE;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_To.Position = (byte)numed_F2_Position.Value;
                        break;
                }
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, rtv_REC_Job_CTRL);
        }

        */

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
            }
            else
            {
                lbl_ReceviceGood.BackColor = System.Drawing.Color.Gray;
                lbl_ReceviceGood.Text = "통신 불능";
            }


        }

        public unsafe void Display_DevSt()
        {
            if ((form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) && (form_Main.COMMDataManager.CommSt != 0))
            {
                Display_RTV_BasicSt();
                Display_Feed_St();
                //Display_Sub_FeedJob();
              //Display_Sub_TaskList_St();
            }
            else
            {
                Display_St_Init();
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
                        if (DevSt->AlarmCodeType == 1)
                        {
                            lbl_Dev_Error.Text = String.Format("{0}-{1}-{2}", DevSt->ErrorCode.MainCode, DevSt->ErrorCode.SubCode, DevSt->ErrorCode.PosCode) +
                                             "  " + Global_Class.UTIL_RTVAlarmName_MemoryMap(DevSt->ErrorCode.MainCode, DevSt->ErrorCode.SubCode, DevSt->ErrorCode.PosCode);
                        } else
                        {
                            lbl_Dev_Error.Text = String.Format("{0}-{1}-{2}", DevSt->ErrorCode.MainCode, DevSt->ErrorCode.SubCode, DevSt->ErrorCode.PosCode) +
                                             "  " + Global_Class.UTIL_RTVAlarmName_14Bytes(DevSt->ErrorCode.MainCode, DevSt->ErrorCode.SubCode, DevSt->ErrorCode.PosCode);
                        }
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

                    if (Global_Class.BitStatus(DevSt->DevSt_2, 6))
                    {
                        lbl_DevmodeSwitch.Text = "수동";
                        lbl_DevmodeSwitch.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl_DevmodeSwitch.Text = "자동";
                        lbl_DevmodeSwitch.BackColor = Color.Lime;
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

                    lbl_Drive_Position.Text = String.Format("{0}", DevSt->Drive_DisPosition.Now_Position);

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

                }
            }
        }

        /*

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
                        lbl_Feed1_jobStep.Text = Global_Class.UTIL_GetRTVJobStepTextAsValue(DevSt->FF1_Job.Item_Do_Step);
                    }
                    else
                    {
                        lbl_Feed1_jobStep.Text = Global_Class.UTIL_GetRTVTaskStepTextAsValue(DevSt->FF1_Job.Item_Do_Step);
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
                        lbl_Feed2_jobStep.Text = Global_Class.UTIL_GetRTVJobStepTextAsValue(DevSt->FF2_Job.Item_Do_Step);
                    }
                    else
                    {
                        lbl_Feed2_jobStep.Text = Global_Class.UTIL_GetRTVTaskStepTextAsValue(DevSt->FF2_Job.Item_Do_Step);
                    }

                }
            }
        }

        private void Display_Ctrl_SelectedInit()
        {
            if (lv_TaskJob_Ctrl.SelectedItems.Count > 0)
            {
                for (byte i = 0; i < lv_TaskJob_Ctrl.SelectedItems.Count; i++)
                {
                    lv_TaskJob_Ctrl.SelectedItems[i].SubItems[1].Text = "지령없음";
                    lv_TaskJob_Ctrl.SelectedItems[i].SubItems[3].Text = "Feeding1";
                    lv_TaskJob_Ctrl.SelectedItems[i].SubItems[4].Text = "0";
                    lv_TaskJob_Ctrl.SelectedItems[i].SubItems[5].Text = "0";

                }
            }

        }

        */

        private void Display_St_Init()
        {
            //Display_RTV_BasicSt 내 갱신 컴포넌트들
            lblVersion.Text = "";


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

            lbl_DevmodeSwitch.Text = "";
            lbl_DevmodeSwitch.BackColor = Color.White;

            lbl_DriveSt2_2.Text = "";
            lbl_DriveSt2_2.BackColor = Color.White;
            lbl_Drive_Position.Text = "";

            //Display_Feed_St 내 갱신 컴포넌트들
            lbl_Drive_CurrentPos_Feed1.Text = "";
            lbl_Drive_CurrentPos_Feed1.BackColor = Color.White;
            lbl_Feed1_Pos.Text = "";
            lbl_Feed1_Dest.Text = "";

            lbl_Drive_CurrentPos_Feed2.Text = "";
            lbl_Drive_CurrentPos_Feed2.BackColor = Color.White;
            lbl_Feed2_Pos.Text = "";
            lbl_Feed2_Dest.Text = "";
            //Display_Sub_TaskList
            //lbl_TaskJobNumber.Text = "";
            //lbl_TaskJobSt.Text = "";
            //lv_TaskJob_St.Items.Clear();

            //Display_Sub_FeedJob
            //lbl_Feed1_Job.Text = "";
            //lbl_Feed1_TaskIndex.Text = "";
            //lbl_Feed1_Cmd.Text = "";
            //lbl_Feed1_From.Text = "";
            //lbl_Feed1_To.Text = "";
            //lbl_Feed1_jobSt.Text = "";
            //lbl_Feed1_jobStep.Text = "";

            //lbl_Feed2_Job.Text = "";
            //lbl_Feed2_TaskIndex.Text = "";
            //lbl_Feed2_Cmd.Text = "";
            //lbl_Feed2_From.Text = "";
            //lbl_Feed2_To.Text = "";
            //lbl_Feed2_jobSt.Text = "";
            //lbl_Feed2_jobStep.Text = "";
        }

        #endregion

        private void btn_DelWork_Feed1_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null) return;

            switch (bt.Tag.ToString())
            {
                case "1":
                    if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "Feeding1 작업을 삭제하시겠습니까"))
                    {
                        Do_Ctrl_DelWork(ConstClass.CMD2_53, 0x01);
                    }
                    break;
                case "2":
                    if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "Feeding2 작업을 삭제하시겠습니까"))
                    {
                        Do_Ctrl_DelWork(ConstClass.CMD2_53, 0x02);
                    }
                    break;
                case "3":
                    if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "Task 작업을 모두 삭제하시겠습니까"))
                    {
                        Do_Ctrl_DelWork(ConstClass.CMD2_53, 0x04);
                    }
                    break;
            }
        }

        private void btn_Dev_Maintance_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "보수위치로 이동 시키시겠습니까?"))
            {
                form_Main.Do_Ctrl_Cmd_withNoData(ConstClass.CMD2_59);
            }
        }

        private void Form_RTV_CTL_Deactivate(object sender, EventArgs e)
        {
            if ((form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue != 0) &&
                (form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue != 0xFF))
            {
                form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue = 0;
                form_Main.Do_JogCtrl();
            }
        }

        private void Form_RTV_CTL_Load(object sender, EventArgs e)
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
    }
}

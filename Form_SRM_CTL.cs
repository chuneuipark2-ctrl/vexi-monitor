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
    public partial class Form_SRM_CTL : Form
    {
        public Form_Main form_Main;
        private static ComboBox[] EditComboxBox;
        private ListViewItem lv_Item;

        private static VEXI_DEFS.TSRM_REC_TaskJobCTRL srm_REC_TaskJob_CTRL;
        private static VEXI_DEFS.SRM_REC_JobCTRL srm_REC_Job_CTRL;
        private static VEXI_DEFS.TSRM_REC_JobCTRLRES srm_REC_Job_CTRLRes;
        private static VEXI_DEFS.TDEV_CtrlRes_2Byte dev_Response_2byte;
        private static VEXI_DEFS.TDEV_REC_SensorScanCtrl dev_REC_0x0161;


        public Form_SRM_CTL()
        {
            InitializeComponent();
            EditComboxBox = new ComboBox[] { null,
                                             Lv_cb_Column1,
                                             Lv_cb_Column3,
                                             Lv_cb_Column4,
                                             Lv_cb_Column5,
                                             Lv_cb_Column6,
                                             Lv_cb_Column7,
                                             Lv_cb_Column8};
        }


        #region 컴포넌트 이벤트
        private void Form_SRM_CTL_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }

            cbItem_ItemType.SelectedIndex = 0;

            if (rb_ForkRef_1.Checked) form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.ForkRef = 1;
            else if (rb_ForkRef_2.Checked) form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.ForkRef = 2;
            else if (rb_ForkRef_3.Checked) form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.ForkRef = 3;
            else form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.ForkRef = 1;

            if (rb_LowSpeed_Fork1_L.Checked) form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.LowSpeedRef = 1;
            else if (rb_LowSpeed_Fork1_R.Checked) form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.LowSpeedRef = 2;
            else if (rb_LowSpeed_Fork2_L.Checked) form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.LowSpeedRef = 3;
            else if (rb_LowSpeed_Fork2_R.Checked) form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.LowSpeedRef = 4;
            else form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.LowSpeedRef = 1;

            Display_DevSt();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hide_AllEdit();
            this.Text = "장비 운전 조작(" + tabControl1.SelectedTab.Text + ")";
        }

        private void btn_LoadDevTaskList_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();
            if ((form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) && (form_Main.COMMDataManager.CommSt != 0))
            {
                Display_Sub_TaskList_Ctrl(false);
            }
            else
            {
                Display_Ctrl_Init();
            }
        }

        private void btn_TaskInit_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();
            Display_Ctrl_Init();

        }

        private void btn_UP_LowSpeed_MouseUp(object sender, MouseEventArgs e)
        {
            Button bt = sender as Button;

            form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue_before = Convert.ToByte(bt.Tag.ToString());
            form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue = 0;
            
            form_Main.Do_ManualCtrl();
        }

        private void rb_LowSpeed_Fork1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_ForkRef_1.Checked) form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.ForkRef = 1;
            else if (rb_ForkRef_2.Checked) form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.ForkRef = 2;
            else if (rb_ForkRef_3.Checked) form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.ForkRef = 3;
            else form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.ForkRef = 1;

            if (rb_LowSpeed_Fork1_L.Checked) form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.LowSpeedRef = 1;
            else if (rb_LowSpeed_Fork1_R.Checked) form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.LowSpeedRef = 2;
            else if (rb_LowSpeed_Fork2_L.Checked) form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.LowSpeedRef = 3;
            else if (rb_LowSpeed_Fork2_R.Checked) form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.LowSpeedRef = 4;
            else form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.LowSpeedRef = 1;

        }

        private void btn_UP_LowSpeed_MouseDown(object sender, MouseEventArgs e)
        {
            Button bt = sender as Button;

            form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue = Convert.ToByte(bt.Tag.ToString());
            form_Main.Do_ManualCtrl();
        }

        private void btn_SetRef_Drive_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "원점을 설정하시겠습니까?"))
            {
                Button bt = sender as Button;

                form_Main.Do_Ctrl_Cmd_withOnebyte(ConstClass.CMD1_00, ConstClass.CMD2_44, Convert.ToByte(bt.Tag.ToString()));
            }
        }

        private void btn_Dev_StartOn_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "장치의 시작모드 상태를 변경하시겠습니까?"))
            {
                Button bt = sender as Button;

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
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "장치의 운영모드를 변경하시겠습니까?"))
            {
                Button bt = sender as Button;

                form_Main.Do_Ctrl_DevMode(ConstClass.CMD2_58, Convert.ToByte(bt.Tag.ToString()));
            }
        }

        private void btn_Move_Station1_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "이동 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                Button bt = sender as Button;

                Do_Semi_MoveCMD_Ctrl(bt.Tag.ToString());
            }
        }

        private void btn_Input_Fork1_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "입고 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                Button bt = sender as Button;

                Do_Semi_InputCMD_Ctrl(bt.Tag.ToString());
            }
        }

        private void btn_Output_Fork1_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "출고 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                Button bt = sender as Button;

                Do_Semi_OutputCMD_Ctrl(bt.Tag.ToString());
            }
        }


        private void btn_RToR_Fork1_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "랙간 반송 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                Button bt = sender as Button;

                Do_Semi_RtoRCMD_Ctrl(bt.Tag.ToString());
            }
        }

        private void btn_SToS_Fork1_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "스테이션간 반송 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                Button bt = sender as Button;

                Do_Semi_StoSCMD_Ctrl(bt.Tag.ToString());
            }
        }

        private void btn_ChangeR_Fork1_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "목적지 랙 변경 반송 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                Button bt = sender as Button;

                Do_Semi_ChangeRCMD_Ctrl(bt.Tag.ToString());
            }
        }

        private void btn_ChangeS_Fork1_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "목적지 스테이션 변경 반송 명령을 전송하시겠습니까? (안전에 주의하세요)"))
            {
                Button bt = sender as Button;

                Do_Semi_ChangeSCMD_Ctrl(bt.Tag.ToString());
            }
        }


        private void btn_TaskSet_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();
            if (lv_TaskJob_Ctrl.Items.Count == 20)
            {
                if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "Task 작업리스트를 전송하시겠습니까? (안전에 주의하세요)"))
                {
                    Do_TaskList_Ctrl();
                }
            } else
            {
                form_Main.GlobalObj.MsgBox_Info("전송할 Task 작업리스트가 작성되지 않았습니다.", "I");
            }
        }

        private void btn_Taskbtn_TaskCancelEdit_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();
            if ((form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) && (form_Main.COMMDataManager.CommSt != 0))
            {
                Display_Sub_TaskList_Ctrl(true);
            }
            else
            {
                Display_Ctrl_SelectedInit();
            }
        }
        private void lv_TaskJob_Ctrl_DoubleClick(object sender, EventArgs e)
        {
            bool once = false;
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
        }

        private void lv_TaskJob_Ctrl_Enter(object sender, EventArgs e)
        {
            for (byte i = 0; i <= (lv_TaskJob_Ctrl.Columns.Count - 1); i++)
            {
                if (EditComboxBox[i] != null)
                {
                    EditComboxBox[i].Visible = false;
                }
            }
        }

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

        private void btn_DelWork_Fork1_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            switch (bt.Tag.ToString())
            {
                case "1":
                    if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "Fork1 작업을 삭제하시겠습니까"))
                    {
                        Do_Ctrl_DelWork(ConstClass.CMD2_53, 0x01);
                    }
                    break;
                case "2":
                    if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "Fork2 작업을 삭제하시겠습니까"))
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

        private void btnDemoTest_Start_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "데모운전을 시작하시겠습니까?"))
            {
                byte[] Data = { 0x01, 0x00 };
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

        private unsafe void button2_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;

            fixed (VEXI_DEFS.TDEV_REC_SensorScanCtrl* DevCtrl = &dev_REC_0x0161)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_SensorScanCtrl)));

                if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, bt.Text + " 하시겠습니까?"))
                {
                    switch (bt.Tag.ToString())
                    {
                        case "11": DevCtrl->StartStop = 1; DevCtrl->Mode = 1; break;
                        case "21": DevCtrl->StartStop = 1; DevCtrl->Mode = 2; break;
                        case "31": DevCtrl->StartStop = 1; DevCtrl->Mode = 3; break;
                        case "41": DevCtrl->StartStop = 1; DevCtrl->Mode = 4; break;
                        case "10": DevCtrl->StartStop = 0; DevCtrl->Mode = 1; break;
                        case "20": DevCtrl->StartStop = 0; DevCtrl->Mode = 2; break;
                        case "30": DevCtrl->StartStop = 0; DevCtrl->Mode = 3; break;
                        case "40": DevCtrl->StartStop = 0; DevCtrl->Mode = 4; break;
                    }
                }
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_61, dev_REC_0x0161);
        }

        private void Form_SRM_CTL_Deactivate(object sender, EventArgs e)
        {
            if ((form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue != 0) &&
                (form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue != 0xFF))
            {
                form_Main.COMMDataManager.DevRec.Manual_DEV_CtrlRec.CtrlTypeValue = 0;
                form_Main.Do_ManualCtrl();
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
        public unsafe void Process_0x61CtrlRes(byte[] datas)
        {
            dev_Response_2byte = (VEXI_DEFS.TDEV_CtrlRes_2Byte)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TDEV_CtrlRes_2Byte));

            if (dev_Response_2byte.CtrlResult == 1 )
            {
                form_Main.GlobalObj.MsgBox_Info("제어 실패 : 센서 스캔 제어", "W");
            }
        }

        private unsafe void Do_TaskList_Ctrl()
        {
            fixed (VEXI_DEFS.TSRM_REC_TaskJobCTRL* DevCtrl = &srm_REC_TaskJob_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_TaskJobCTRL)));

                DevCtrl->OptionFlag = 0x00;
                if (cbTaskOption_0.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x01);
                if (cbTaskOption_1.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x02);

                DevCtrl->taskWorkNum = form_Main.COMMDataManager.Random_WorkNum_AndInc;

                fixed (VEXI_DEFS.TSRM_TaskJobItem* TaskJobPtr = &srm_REC_TaskJob_CTRL.TaskJobItem_1)
                {
                    for (byte i= 0; i < 20; i++)
                    {
                        if (lv_TaskJob_Ctrl.Items[i].SubItems[1].Text == "지령없음")
                        {
                            (TaskJobPtr + i)->Cmd = ConstClass.SEMI_NONE;
                            (TaskJobPtr + i)->Fork = 1;
                            (TaskJobPtr + i)->To.Station = 0;
                            (TaskJobPtr + i)->To.Row = 0;
                            (TaskJobPtr + i)->To.BayID = 0;
                            (TaskJobPtr + i)->To.LevelID = 0;
                            (TaskJobPtr + i)->itemType = 1;
                        }
                        else
                        {
                            if (lv_TaskJob_Ctrl.Items[i].SubItems[1].Text == "Move") (TaskJobPtr + i)->Cmd = ConstClass.SEMI_MOVE;
                            else if (lv_TaskJob_Ctrl.Items[i].SubItems[1].Text == "Loading") (TaskJobPtr + i)->Cmd = ConstClass.SEMI_Loading;
                            else if (lv_TaskJob_Ctrl.Items[i].SubItems[1].Text == "Unloading") (TaskJobPtr + i)->Cmd = ConstClass.SEMI_UnLoading;

//                            (TaskJobPtr + i)->LoadFactor = (byte)Global_Class.UTIL_StrToIntDef(lv_TaskJob_Ctrl.Items[i].SubItems[2].Text, 100);

                            if (lv_TaskJob_Ctrl.Items[i].SubItems[2].Text == "Fork1") (TaskJobPtr + i)->Fork = 1;
                            else if (lv_TaskJob_Ctrl.Items[i].SubItems[2].Text == "Fork2") (TaskJobPtr + i)->Fork = 2;

                            (TaskJobPtr + i)->To.Station = (byte)Global_Class.UTIL_StrToIntDef(lv_TaskJob_Ctrl.Items[i].SubItems[3].Text, 0);
                            (TaskJobPtr + i)->To.Row = (byte)Global_Class.UTIL_StrToIntDef(lv_TaskJob_Ctrl.Items[i].SubItems[4].Text, 0);
                            (TaskJobPtr + i)->To.BayID = (UInt16)Global_Class.UTIL_StrToIntDef(lv_TaskJob_Ctrl.Items[i].SubItems[5].Text, 0);
                            (TaskJobPtr + i)->To.LevelID = (byte)Global_Class.UTIL_StrToIntDef(lv_TaskJob_Ctrl.Items[i].SubItems[6].Text, 0);

                            (TaskJobPtr + i)->itemType = (byte)Global_Class.UTIL_StrToIntDef(lv_TaskJob_Ctrl.Items[i].SubItems[7].Text, 1);
                        }

                    }
                }
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_40, srm_REC_TaskJob_CTRL);
        }

        private unsafe void Do_Semi_ChangeSCMD_Ctrl(string CtrlType)
        {
            fixed (VEXI_DEFS.SRM_REC_JobCTRL* DevCtrl = &srm_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.SRM_REC_JobCTRL)));

                DevCtrl->OptionFlag = 0x00;
                if (cbJobOption_0.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x01);
                if (cbJobOption_1.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x02);

                switch (CtrlType)
                {
                    case ("F1"):
                        DevCtrl->CMD = ConstClass.SEMI_ChangeS;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_To.Station = (byte)numed_Fork1_ChangeS_S.Value;
                        DevCtrl->Work1_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                    case ("F2"):
                        DevCtrl->CMD = ConstClass.SEMI_ChangeS;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_To.Station = (byte)numed_Fork2_ChangeS_S.Value;
                        DevCtrl->Work2_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                    case ("ALL"):
                        DevCtrl->CMD = ConstClass.SEMI_ChangeS;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_To.Station = (byte)numed_Fork1_ChangeS_S.Value;
                        DevCtrl->Work1_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_To.Station = (byte)numed_Fork2_ChangeS_S.Value;
                        DevCtrl->Work2_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                }
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, srm_REC_Job_CTRL);
        }

        private unsafe void Do_Semi_ChangeRCMD_Ctrl(string CtrlType)
        {
            fixed (VEXI_DEFS.SRM_REC_JobCTRL* DevCtrl = &srm_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.SRM_REC_JobCTRL)));
                DevCtrl->OptionFlag = 0x00;
                if (cbJobOption_0.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x01);
                if (cbJobOption_1.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x02);

                switch (CtrlType)
                {
                    case ("F1"):
                        DevCtrl->CMD = ConstClass.SEMI_ChangeR;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_To.Row = (byte)numed_Fork1_ChangeR_R.Value;
                        DevCtrl->Work1_To.BayID = (UInt16)numed_Fork1_ChangeR_B.Value;
                        DevCtrl->Work1_To.LevelID = (byte)numed_Fork1_ChangeR_L.Value;
                        DevCtrl->Work1_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                    case ("F2"):
                        DevCtrl->CMD = ConstClass.SEMI_ChangeR;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_To.Row = (byte)numed_Fork2_ChangeR_R.Value;
                        DevCtrl->Work2_To.BayID = (UInt16)numed_Fork2_ChangeR_B.Value;
                        DevCtrl->Work2_To.LevelID = (byte)numed_Fork2_ChangeR_L.Value;
                        DevCtrl->Work2_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                    case ("ALL"):
                        DevCtrl->CMD = ConstClass.SEMI_ChangeR;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_To.Row = (byte)numed_Fork1_ChangeR_R.Value;
                        DevCtrl->Work1_To.BayID = (UInt16)numed_Fork1_ChangeR_B.Value;
                        DevCtrl->Work1_To.LevelID = (byte)numed_Fork1_ChangeR_L.Value;
                        DevCtrl->Work1_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_To.Row = (byte)numed_Fork2_ChangeR_R.Value;
                        DevCtrl->Work2_To.BayID = (UInt16)numed_Fork2_ChangeR_B.Value;
                        DevCtrl->Work2_To.LevelID = (byte)numed_Fork2_ChangeR_L.Value;
                        DevCtrl->Work2_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                }
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, srm_REC_Job_CTRL);
        }
        private unsafe void Do_Semi_StoSCMD_Ctrl(string CtrlType)
        {
            fixed (VEXI_DEFS.SRM_REC_JobCTRL* DevCtrl = &srm_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.SRM_REC_JobCTRL)));
                DevCtrl->OptionFlag = 0x00;
                if (cbJobOption_0.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x01);
                if (cbJobOption_1.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x02);

                switch (CtrlType)
                {
                    case ("F1"):
                        DevCtrl->CMD = ConstClass.SEMI_StoS;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_From.Station = (byte)numed_Fork1_StoS_FromS.Value;
                        DevCtrl->Work1_To.Station = (byte)numed_Fork1_StoS_ToS.Value;
                        DevCtrl->Work1_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                    case ("F2"):
                        DevCtrl->CMD = ConstClass.SEMI_StoS;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_From.Station = (byte)numed_Fork2_StoS_FromS.Value;
                        DevCtrl->Work2_To.Station = (byte)numed_Fork2_StoS_ToS.Value;
                        DevCtrl->Work2_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                    case ("ALL"):
                        DevCtrl->CMD = ConstClass.SEMI_StoS;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_From.Station = (byte)numed_Fork1_StoS_FromS.Value;
                        DevCtrl->Work1_To.Station = (byte)numed_Fork1_StoS_ToS.Value;
                        DevCtrl->Work1_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_From.Station = (byte)numed_Fork2_StoS_FromS.Value;
                        DevCtrl->Work2_To.Station = (byte)numed_Fork2_StoS_ToS.Value;
                        DevCtrl->Work2_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                }
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, srm_REC_Job_CTRL);
        }

        private unsafe void Do_Semi_RtoRCMD_Ctrl(string CtrlType)
        {
            fixed (VEXI_DEFS.SRM_REC_JobCTRL* DevCtrl = &srm_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.SRM_REC_JobCTRL)));
                DevCtrl->OptionFlag = 0x00;
                if (cbJobOption_0.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x01);
                if (cbJobOption_1.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x02);

                switch (CtrlType)
                {
                    case ("F1"):
                        DevCtrl->CMD = ConstClass.SEMI_RtoR;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_From.Row = (byte)numed_Fork1_RtoR_FromR.Value;
                        DevCtrl->Work1_From.BayID = (UInt16)numed_Fork1_RtoR_FromB.Value;
                        DevCtrl->Work1_From.LevelID = (byte)numed_Fork1_RtoR_FromL.Value;
                        DevCtrl->Work1_To.Row = (byte)numed_Fork1_RtoR_ToR.Value;
                        DevCtrl->Work1_To.BayID = (UInt16)numed_Fork1_RtoR_ToB.Value;
                        DevCtrl->Work1_To.LevelID = (byte)numed_Fork1_RtoR_ToL.Value;
                        DevCtrl->Work1_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                    case ("F2"):
                        DevCtrl->CMD = ConstClass.SEMI_RtoR;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_From.Row = (byte)numed_Fork2_RtoR_FromR.Value;
                        DevCtrl->Work2_From.BayID = (UInt16)numed_Fork2_RtoR_FromB.Value;
                        DevCtrl->Work2_From.LevelID = (byte)numed_Fork2_RtoR_FromL.Value;
                        DevCtrl->Work2_To.Row = (byte)numed_Fork2_RtoR_ToR.Value;
                        DevCtrl->Work2_To.BayID = (UInt16)numed_Fork2_RtoR_ToB.Value;
                        DevCtrl->Work2_To.LevelID = (byte)numed_Fork2_RtoR_ToL.Value;
                        DevCtrl->Work2_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                    case ("ALL"):
                        DevCtrl->CMD = ConstClass.SEMI_RtoR;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_From.Row = (byte)numed_Fork1_RtoR_FromR.Value;
                        DevCtrl->Work1_From.BayID = (UInt16)numed_Fork1_RtoR_FromB.Value;
                        DevCtrl->Work1_From.LevelID = (byte)numed_Fork1_RtoR_FromL.Value;
                        DevCtrl->Work1_To.Row = (byte)numed_Fork1_RtoR_ToR.Value;
                        DevCtrl->Work1_To.BayID = (UInt16)numed_Fork1_RtoR_ToB.Value;
                        DevCtrl->Work1_To.LevelID = (byte)numed_Fork1_RtoR_ToL.Value;
                        DevCtrl->Work1_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_From.Row = (byte)numed_Fork2_RtoR_FromR.Value;
                        DevCtrl->Work2_From.BayID = (UInt16)numed_Fork2_RtoR_FromB.Value;
                        DevCtrl->Work2_From.LevelID = (byte)numed_Fork2_RtoR_FromL.Value;
                        DevCtrl->Work2_To.Row = (byte)numed_Fork2_RtoR_ToR.Value;
                        DevCtrl->Work2_To.BayID = (UInt16)numed_Fork2_RtoR_ToB.Value;
                        DevCtrl->Work2_To.LevelID = (byte)numed_Fork2_RtoR_ToL.Value; 
                        DevCtrl->Work2_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                }
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, srm_REC_Job_CTRL);
        }

        private unsafe void Do_Semi_OutputCMD_Ctrl(string CtrlType)
        {
            fixed (VEXI_DEFS.SRM_REC_JobCTRL* DevCtrl = &srm_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.SRM_REC_JobCTRL)));
                DevCtrl->OptionFlag = 0x00;
                if (cbJobOption_0.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x01);
                if (cbJobOption_1.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x02);

                switch (CtrlType)
                {
                    case ("F1"):
                        DevCtrl->CMD = ConstClass.SEMI_OUTPUT;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_From.Row = (byte)numed_Fork1_Output_R.Value;
                        DevCtrl->Work1_From.BayID = (UInt16)numed_Fork1_Output_B.Value;
                        DevCtrl->Work1_From.LevelID = (byte)numed_Fork1_Output_L.Value;
                        DevCtrl->Work1_To.Station = (byte)numed_Fork1_Output_S.Value;
                        DevCtrl->Work1_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                    case ("F2"):
                        DevCtrl->CMD = ConstClass.SEMI_OUTPUT;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_From.Row = (byte)numed_Fork2_Output_R.Value;
                        DevCtrl->Work2_From.BayID = (UInt16)numed_Fork2_Output_B.Value;
                        DevCtrl->Work2_From.LevelID = (byte)numed_Fork2_Output_L.Value;
                        DevCtrl->Work2_To.Station = (byte)numed_Fork2_Output_S.Value;
                        DevCtrl->Work2_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                    case ("ALL"):
                        DevCtrl->CMD = ConstClass.SEMI_OUTPUT;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_From.Row = (byte)numed_Fork1_Output_R.Value;
                        DevCtrl->Work1_From.BayID = (UInt16)numed_Fork1_Output_B.Value;
                        DevCtrl->Work1_From.LevelID = (byte)numed_Fork1_Output_L.Value;
                        DevCtrl->Work1_To.Station = (byte)numed_Fork1_Output_S.Value;
                        DevCtrl->Work1_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_From.Row = (byte)numed_Fork2_Output_R.Value;
                        DevCtrl->Work2_From.BayID = (UInt16)numed_Fork2_Output_B.Value;
                        DevCtrl->Work2_From.LevelID = (byte)numed_Fork2_Output_L.Value;
                        DevCtrl->Work2_To.Station = (byte)numed_Fork2_Output_S.Value;
                        DevCtrl->Work2_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                }
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, srm_REC_Job_CTRL);
        }

        private void Do_Ctrl_DelWork(byte TmpCMD2, byte CtrlData)
        {
            form_Main.Do_Ctrl_Cmd_withOnebyte(ConstClass.CMD1_00, ConstClass.CMD2_53, CtrlData);
        }

        private unsafe void Do_Semi_InputCMD_Ctrl(string CtrlType)
        {
            fixed (VEXI_DEFS.SRM_REC_JobCTRL* DevCtrl = &srm_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.SRM_REC_JobCTRL)));
                DevCtrl->OptionFlag = 0x00;
                if (cbJobOption_0.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x01);
                if (cbJobOption_1.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x02);

                switch (CtrlType)
                {
                    case ("F1"):
                        DevCtrl->CMD = ConstClass.SEMI_INPUT;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_From.Station = (byte)numed_Fork1_Input_S.Value;
                        DevCtrl->Work1_To.Row = (byte)numed_Fork1_Input_R.Value;
                        DevCtrl->Work1_To.BayID = (UInt16)numed_Fork1_Input_B.Value;
                        DevCtrl->Work1_To.LevelID = (byte)numed_Fork1_Input_L.Value;
                        DevCtrl->Work1_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                    case ("F2"):
                        DevCtrl->CMD = ConstClass.SEMI_INPUT;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_From.Station = (byte)numed_Fork2_Input_S.Value;
                        DevCtrl->Work2_To.Row = (byte)numed_Fork2_Input_R.Value;
                        DevCtrl->Work2_To.BayID = (UInt16)numed_Fork2_Input_B.Value;
                        DevCtrl->Work2_To.LevelID = (byte)numed_Fork2_Input_L.Value;
                        DevCtrl->Work2_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                    case ("ALL"):
                        DevCtrl->CMD = ConstClass.SEMI_INPUT;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_From.Station = (byte)numed_Fork1_Input_S.Value;
                        DevCtrl->Work1_To.Row = (byte)numed_Fork1_Input_R.Value;
                        DevCtrl->Work1_To.BayID = (UInt16)numed_Fork1_Input_B.Value;
                        DevCtrl->Work1_To.LevelID = (byte)numed_Fork1_Input_L.Value;
                        DevCtrl->Work1_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_From.Station = (byte)numed_Fork2_Input_S.Value;
                        DevCtrl->Work2_To.Row = (byte)numed_Fork2_Input_R.Value;
                        DevCtrl->Work2_To.BayID = (UInt16)numed_Fork2_Input_B.Value;
                        DevCtrl->Work2_To.LevelID = (byte)numed_Fork2_Input_L.Value;
                        DevCtrl->Work2_ItermType = (byte)(cbItem_ItemType.SelectedIndex + 1);
                        break;
                }
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, srm_REC_Job_CTRL);
        }

        private unsafe void Do_Semi_MoveCMD_Ctrl(string CtrlType)

        {
            fixed (VEXI_DEFS.SRM_REC_JobCTRL* DevCtrl = &srm_REC_Job_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.SRM_REC_JobCTRL)));
                DevCtrl->OptionFlag = 0x00;
                if (cbJobOption_0.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x01);
                if (cbJobOption_1.Checked) DevCtrl->OptionFlag = (byte)(DevCtrl->OptionFlag | 0x02);

                switch (CtrlType)
                {
                    case "S":
                        DevCtrl->CMD = ConstClass.SEMI_MOVE;

                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_To.Station = (byte)numed_Station_Move_S.Value; ;
                        break;
                    case "F1":
                        DevCtrl->CMD = ConstClass.SEMI_MOVE;
                        
                        DevCtrl->Work1_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work1_To.Row = (byte)numed_Fork1_Move_R.Value;
                        DevCtrl->Work1_To.BayID = (UInt16)numed_Fork1_Move_B.Value;
                        DevCtrl->Work1_To.LevelID = (byte)numed_Fork1_Move_L.Value;
                        break;

                    case "F2":
                        DevCtrl->CMD = ConstClass.SEMI_MOVE;

                        DevCtrl->Work2_Num = form_Main.COMMDataManager.Random_WorkNum_AndInc;
                        DevCtrl->Work2_To.Row = (byte)numed_Fork2_Move_R.Value;
                        DevCtrl->Work2_To.BayID = (UInt16)numed_Fork2_Move_B.Value;
                        DevCtrl->Work2_To.LevelID = (byte)numed_Fork2_Move_L.Value;
                        break;
                }
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_41, srm_REC_Job_CTRL);            
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
                Display_SRM_BasicSt();
                Display_DriveUpDown_Position();
                Display_Fork_Position();
                Display_D_UD_Fork_St();
                Display_Sub_ForkJob();
                Display_Sub_TaskList_St();
            }
            else
            {
                Display_St_Init();
            }
        }

        public void Display_JobCtrlRes(byte[] Data)
        {
            srm_REC_Job_CTRLRes = (VEXI_DEFS.TSRM_REC_JobCTRLRES)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TSRM_REC_JobCTRLRES));

            lbl_JobCtrlRes.Visible = (srm_REC_Job_CTRLRes.ResultRes != 0);

            switch (srm_REC_Job_CTRLRes.Work1_ResultRes)
            {
                case 1: lbl_JobCtrlRes.Text = string.Format("{0}", (srm_REC_Job_CTRLRes.Work1_ResultRes)) + " 장애 상태"; break;
                case 2: lbl_JobCtrlRes.Text = string.Format("{0}", (srm_REC_Job_CTRLRes.Work1_ResultRes)) + " 포크가 중심상태가 아님"; break;
                case 3: lbl_JobCtrlRes.Text = string.Format("{0}", (srm_REC_Job_CTRLRes.Work1_ResultRes)) + " 주행 위치 거리값(mm)가 설정 범위 아님"; break;
                case 4: lbl_JobCtrlRes.Text = string.Format("{0}", (srm_REC_Job_CTRLRes.Work1_ResultRes)) + " 주행 위치 거리값(mm)가 설정 범위 아님"; break;
                case 5: lbl_JobCtrlRes.Text = string.Format("{0}", (srm_REC_Job_CTRLRes.Work1_ResultRes)) + " 자동모드가 아닌 상태"; break;
                case 6: lbl_JobCtrlRes.Text = string.Format("{0}", (srm_REC_Job_CTRLRes.Work1_ResultRes)) + " 시작 OFF 상태"; break;
                case 10: lbl_JobCtrlRes.Text = string.Format("{0}", (srm_REC_Job_CTRLRes.Work1_ResultRes)) + " 명령 위치가 설정 위치가 아님"; break;
                case 11: lbl_JobCtrlRes.Text = string.Format("{0}", (srm_REC_Job_CTRLRes.Work1_ResultRes)) + " 입고, 출고, 랙간반송, 스테이션 반송 명령 수신 시, 화물감지 상태"; break;
                case 12: lbl_JobCtrlRes.Text = string.Format("{0}", (srm_REC_Job_CTRLRes.Work1_ResultRes)) + " 이전 입고, 출고, 랙간반송, 스테이션 반송 작업 중지 상태 (Start Off 전달 시)"; break;
                case 13: lbl_JobCtrlRes.Text = string.Format("{0}", (srm_REC_Job_CTRLRes.Work1_ResultRes)) + " 이전 입고, 출고, 랙간반송, 스테이션 반송 작업 실패 상태 (이상 발생 시)"; break;
                case 14: lbl_JobCtrlRes.Text = string.Format("{0}", (srm_REC_Job_CTRLRes.Work1_ResultRes)) + " 이전 명령 작업 수행중 (수행해야 할 Task Commnad 작업 남아있는 경우도 포함)"; break;
                case 15: lbl_JobCtrlRes.Text = string.Format("{0}", (srm_REC_Job_CTRLRes.Work1_ResultRes)) + " 작업 번호 '0' 으로 수신"; break;
                case 16: lbl_JobCtrlRes.Text = string.Format("{0}", (srm_REC_Job_CTRLRes.Work1_ResultRes)) + " 랙, 스테이션 목적지 변경 작업번호 이상"; break;
                case 17: lbl_JobCtrlRes.Text = string.Format("{0}", (srm_REC_Job_CTRLRes.Work1_ResultRes)) + " 동일한 작업번호 완료 상태인데, 랙, 스테이션 목적지 변경 명령 수신"; break;
                default : lbl_JobCtrlRes.Text = string.Format("{0}", (srm_REC_Job_CTRLRes.Work1_ResultRes)) + " Unknown Nack"; break;
            }
        }
        

        private void Display_Ctrl_Init()
        {
            lv_TaskJob_Ctrl.Items.Clear();


            for (byte i = 0; i < 20; i++)
            {
                ListViewItem item = lv_TaskJob_Ctrl.Items.Add(string.Format("{0}", i + 1));
                item.SubItems.Add("지령없음");
                item.SubItems.Add("Fork1");
                item.SubItems.Add("0");
                item.SubItems.Add("0");
                item.SubItems.Add("0");
                item.SubItems.Add("0");
                item.SubItems.Add("1");
            }
        }

        private void Display_Ctrl_SelectedInit()
        {
            if (lv_TaskJob_Ctrl.SelectedItems.Count > 0)
            {
                for (byte i=0; i < lv_TaskJob_Ctrl.SelectedItems.Count; i++)
                {
                    lv_TaskJob_Ctrl.SelectedItems[i].SubItems[1].Text = "지령없음";
                    lv_TaskJob_Ctrl.SelectedItems[i].SubItems[3].Text = "Fork1";
                    lv_TaskJob_Ctrl.SelectedItems[i].SubItems[4].Text = "0";
                    lv_TaskJob_Ctrl.SelectedItems[i].SubItems[5].Text = "0";
                    lv_TaskJob_Ctrl.SelectedItems[i].SubItems[6].Text = "0";
                    lv_TaskJob_Ctrl.SelectedItems[i].SubItems[7].Text = "0";
                    lv_TaskJob_Ctrl.SelectedItems[i].SubItems[8].Text = "1";

                }
            }
            
        }

        private void Display_St_Init()
        {
            //Display_SRM_BasicSt 내 갱신 컴포넌트들

            lbl_ReceviceGood.BackColor = System.Drawing.Color.Gray;
            lbl_DevDetailType.Text = "";
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
            lbl_DevReady.Text = "";
            lbl_DevReady.BackColor = Color.White;
            lbl_Dev_Error.Text = "";
            lbl_Dev_Error.BackColor = Color.White;
            lbl_DevEmergencySwitch.Text = "";
            lbl_DevEmergencySwitch.BackColor = Color.White;
            lbl_DevModeSwitch.Text = "";
            lbl_DevModeSwitch.BackColor = Color.White;

            //Display_DriveUpDown_Position 내 갱신 컴포넌트들
            lbl_Fork1_Pos.Text = "";
            lbl_Fork1_Dest.Text = "";
            lbl_Fork1_CurrentPos_Center.BackColor = Color.White;
            lbl_Drive_CurrentPos_L_Fork1.BackColor = Color.White;
            lbl_Drive_CurrentPos_R_Fork1.BackColor = Color.White;
            lbl_UpDown_Fork1_CurrentPos_L_1.BackColor = Color.White;
            lbl_UpDown_Fork1_CurrentPos_L_2.BackColor = Color.White;
            lbl_UpDown_Fork1_CurrentPos_R_1.BackColor = Color.White;
            lbl_UpDown_Fork1_CurrentPos_R_2.BackColor = Color.White;
            lbl_Fork2_Pos.Text = "";
            lbl_Fork2_Dest.Text = "";
            lbl_Fork2_CurrentPos_Center.BackColor = Color.White;
            lbl_Drive_CurrentPos_L_Fork2.BackColor = Color.White;
            lbl_Drive_CurrentPos_R_Fork2.BackColor = Color.White;
            lbl_UpDown_Fork2_CurrentPos_L_1.BackColor = Color.White;
            lbl_UpDown_Fork2_CurrentPos_L_2.BackColor = Color.White;
            lbl_UpDown_Fork2_CurrentPos_R_1.BackColor = Color.White;
            lbl_UpDown_Fork2_CurrentPos_R_2.BackColor = Color.White;

            //Display_Fork_Position 내 갱신 컴포넌트들
            lbl_Fork1St1_0.Text = "";
            lbl_Fork1St1_4.Text = "";
            lbl_Fork1St1_4.BackColor = Color.White;
            lbl_Fork1_CurrentPos_Center.BackColor = Color.White;
            lbl_Fork1_CurrentPos_L1.BackColor = Color.White;
            lbl_Fork1_CurrentPos_L2.BackColor = Color.White;
            lbl_Fork1_CurrentPos_L3.BackColor = Color.White;
            lbl_Fork1_CurrentPos_R1.BackColor = Color.White;
            lbl_Fork1_CurrentPos_R2.BackColor = Color.White;
            lbl_Fork1_CurrentPos_R3.BackColor = Color.White;
            lbl_Fork1St1_6.Text = "";
            lbl_Fork1St1_6.BackColor = Color.White;
            lbl_Fork1St1_7.Text = "";
            lbl_Fork1St1_7.BackColor = Color.White;
            lbl_Fork2St1_0.Text = "";
            lbl_Fork2St1_4.Text = "";
            lbl_Fork2St1_4.BackColor = Color.White;
            lbl_Fork2_CurrentPos_Center.BackColor = Color.White;
            lbl_Fork2_CurrentPos_L1.BackColor = Color.White;
            lbl_Fork2_CurrentPos_L2.BackColor = Color.White;
            lbl_Fork2_CurrentPos_L3.BackColor = Color.White;
            lbl_Fork2_CurrentPos_R1.BackColor = Color.White;
            lbl_Fork2_CurrentPos_R2.BackColor = Color.White;
            lbl_Fork2_CurrentPos_R3.BackColor = Color.White;
            lbl_Fork2St1_6.Text = "";
            lbl_Fork2St1_6.BackColor = Color.White;
            lbl_Fork2St1_7.Text = "";
            lbl_Fork2St1_7.BackColor = Color.White;

            //Display_D_UD_Fork_St 내 갱신 컴포넌트들
            lbl_DriveSt2_2.Text = "";
            lbl_DriveSt2_2.BackColor = Color.White;
            lbl_Drive_Position.Text = "";
            lbl_UpDownSt2_2.Text = "";
            lbl_UpDownSt2_2.BackColor = Color.White;
            lbl_UpDown_Position.Text = "";
            lbl_Fork1St2_2.Text = "";
            lbl_Fork1St2_2.BackColor = Color.White;
            lbl_Fork1_Position.Text = "";
            lbl_Fork2St2_2.Text = "";
            lbl_Fork2St2_2.BackColor = Color.White;
            lbl_Fork2_Position.Text = "";


            //Display_Sub_TaskList
            lbl_TaskJobNumber.Text = "";
            lbl_TaskJobSt.Text = "";
            lv_TaskJob_St.Items.Clear();

            //Display_Sub_ForkJob
            lbl_Fork1_Job.Text = "";
            lbl_Fork1_TaskIndex.Text = "";
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
            lbl_Fork2_TaskIndex.Text = "";
            lbl_Fork2_Cmd.Text = "";
            lbl_Fork2_From.Text = "";
            lbl_Fork2_To.Text = "";
            lbl_Fork2_jobSt.Text = "";
            lbl_Fork2_jobStep.Text = "";
            lbl_Fork2_MoveJob.Text = "";
            lbl_Fork2_MoveJob_To.Text = "";
            lbl_Fork2_MoveJob_St.Text = "";
            lbl_Fork2_MoveJob_Step.Text = "";
        }

        private unsafe void Display_Fork_Position()
        {
            fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt)
            {
                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
                    if (Global_Class.BitStatus(DevSt->Fork1_DisPosition.St_1, 0))
                    {
                        lbl_Fork1St1_0.Text = "동작중";
                    }
                    else
                    {
                        lbl_Fork1St1_0.Text = "정지";
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

                    if (Global_Class.BitStatus(DevSt->Fork2_DisPosition.St_1, 0))
                    {
                        lbl_Fork2St1_0.Text = "동작중";
                    }
                    else
                    {
                        lbl_Fork2St1_0.Text = "정지";
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

                }
            }
        }

        private unsafe void Display_DriveUpDown_Position()
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
                }
            }
        }

        private unsafe void Display_D_UD_Fork_St()
        {
            fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt)
            {
                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
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


                    lbl_UpDown_Position.Text = String.Format("{0}", DevSt->Updown_DisPosition.Now_Position);


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
                    lbl_Fork1_Position.Text = String.Format("{0}", DevSt->Fork1_DisPosition.Now_Position);
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
                    lbl_Fork2_Position.Text = String.Format("{0}", DevSt->Fork2_DisPosition.Now_Position);
                }
            }
        }

        private unsafe void Display_SRM_BasicSt()
        {
            fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt)
            {
                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
                    //코딩
                    //현재 디자인 중임. 디자인 완료 후 컴포넌트 Name 제대로 부여한 후 코딩 진행 예정
                    lbl_DevDetailType.Text = String.Format("0x{0:X2}", DevSt->DevDetailtype);
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
                }
            }

        }

        private unsafe void Display_Sub_ForkJob()
        {
            fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt)
            {
                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
                    //반송 or Task : 수행하고 있는(실패상태 포함) 혹은 마지막 수행완료된 작업
                    if (DevSt->FF1_Job.Item_Do_Status == 4)
                    {
                        lbl_Fork1_Job.Text = string.Format("{0} (완료)", DevSt->FF1_Job.Item_JobNumber);
                    }
                    else
                    {
                        lbl_Fork1_Job.Text = string.Format("{0}", DevSt->FF1_Job.Item_JobNumber);
                    }
                    if (DevSt->FF1_Job.taskIndex == 0)
                    {
                        lbl_Fork1_TaskIndex.Text = "";
                    }
                    else
                    {
                        lbl_Fork1_TaskIndex.Text = string.Format("{0}", DevSt->FF1_Job.taskIndex);
                    }

                    lbl_Fork1_Cmd.Text = Global_Class.UTIL_GetJobTextAsValue(DevSt->FF1_Job.Item_CMD_Code);

                    if (DevSt->FF1_Job.taskIndex == 0)
                    {
                        lbl_Fork1_From.Text = string.Format("S{0}-R{1}-B{2}-L{3}", DevSt->FF1_Job.Item_From.Station
                                                          , DevSt->FF1_Job.Item_From.Row
                                                          , DevSt->FF1_Job.Item_From.BayID
                                                          , DevSt->FF1_Job.Item_From.LevelID);
                    }
                    else
                    {
                        lbl_Fork1_From.Text = "";
                    }
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
                        default: lbl_Fork1_jobSt.Text = string.Format("{0:X2}", DevSt->FF1_Job.Item_Do_Status); break;
                    }

                    if (DevSt->FF1_Job.taskIndex == 0)
                    {
                        lbl_Fork1_jobStep.Text = Global_Class.UTIL_GetJobStepTextAsValue(DevSt->FF1_Job.Item_Do_Step);
                    }
                    else
                    {
                        lbl_Fork1_jobStep.Text = Global_Class.UTIL_GetTaskStepTextAsValue(DevSt->FF1_Job.Item_Do_Step);
                    }
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
                        default: lbl_Fork1_MoveJob_St.Text = string.Format("{0:X2}", DevSt->FF1_Job.Move_Do_Status); break;
                    }

                    lbl_Fork1_MoveJob_Step.Text = Global_Class.UTIL_GetJobStepTextAsValue(DevSt->FF1_Job.Move_Do_Step);


                    //반송 or Task : 수행하고 있는(실패상태 포함) 혹은 마지막 수행완료된 작업
                    if (DevSt->FF2_Job.Item_Do_Status == 4)
                    {
                        lbl_Fork2_Job.Text = string.Format("{0} (완료)", DevSt->FF2_Job.Item_JobNumber);
                    }
                    else
                    {
                        lbl_Fork2_Job.Text = string.Format("{0}", DevSt->FF2_Job.Item_JobNumber);
                    }
                    if (DevSt->FF2_Job.taskIndex == 0)
                    {
                        lbl_Fork2_TaskIndex.Text = "";
                    }
                    else
                    {
                        lbl_Fork2_TaskIndex.Text = string.Format("{0}", DevSt->FF2_Job.taskIndex);
                    }

                    lbl_Fork2_Cmd.Text = Global_Class.UTIL_GetJobTextAsValue(DevSt->FF2_Job.Item_CMD_Code);

                    if (DevSt->FF2_Job.taskIndex == 0)
                    {
                        lbl_Fork2_From.Text = string.Format("S{0}-R{1}-B{2}-L{3}", DevSt->FF2_Job.Item_From.Station
                                                          , DevSt->FF2_Job.Item_From.Row
                                                          , DevSt->FF2_Job.Item_From.BayID
                                                          , DevSt->FF2_Job.Item_From.LevelID);
                    }
                    else
                    {
                        lbl_Fork2_From.Text = "";
                    }
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

                    if (DevSt->FF2_Job.taskIndex == 0)
                    {
                        lbl_Fork2_jobStep.Text = Global_Class.UTIL_GetJobStepTextAsValue(DevSt->FF2_Job.Item_Do_Step);
                    }
                    else
                    {
                        lbl_Fork2_jobStep.Text = Global_Class.UTIL_GetTaskStepTextAsValue(DevSt->FF2_Job.Item_Do_Step);
                    }
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

                    lbl_Fork2_MoveJob_Step.Text = Global_Class.UTIL_GetJobStepTextAsValue(DevSt->FF2_Job.Move_Do_Step);
                }
            }

        }


        private unsafe void Display_Sub_TaskList_Ctrl(bool onlySelected)
        {
            if (lv_TaskJob_Ctrl.Items.Count == 0)
            {
                Display_Ctrl_Init();
            }

            fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt)
            {
                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                {
                    //Task List
                    fixed (VEXI_DEFS.TSRM_TaskJobItem* TaskJobPtr = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.TaskJobItem_1)
                    {
                        VEXI_DEFS.TSRM_TaskJobItem* Ptr = TaskJobPtr;
                        for (byte i = 0; i < 20; i++)
                        {

                            if ((!onlySelected) || ((onlySelected) && lv_TaskJob_Ctrl.Items[i].Selected))
                            {

                                switch (Ptr->Cmd)
                                {
                                    case ConstClass.SEMI_NONE      : lv_TaskJob_Ctrl.Items[i].SubItems[1].Text = "지령없음"; break;
                                    case ConstClass.SEMI_MOVE      : lv_TaskJob_Ctrl.Items[i].SubItems[1].Text = "Move"; break;
                                    case ConstClass.SEMI_TaskLoading   : lv_TaskJob_Ctrl.Items[i].SubItems[1].Text = "Loading"; break;
                                    case ConstClass.SEMI_TaskUnLoading : lv_TaskJob_Ctrl.Items[i].SubItems[1].Text = "Unloading"; break;
                                    default: lv_TaskJob_Ctrl.Items[i].SubItems[1].Text = "지령없음"; break;
                                }

                                switch (Ptr->Fork)
                                {
                                    case 1: lv_TaskJob_Ctrl.Items[i].SubItems[2].Text = "Fork1"; break;
                                    case 2: lv_TaskJob_Ctrl.Items[i].SubItems[2].Text = "Fork2"; break;
                                    default: lv_TaskJob_Ctrl.Items[i].SubItems[2].Text = "Fork1"; break;
                                }
                                lv_TaskJob_Ctrl.Items[i].SubItems[3].Text = string.Format("{0}", Ptr->To.Station);
                                lv_TaskJob_Ctrl.Items[i].SubItems[4].Text = string.Format("{0}", Ptr->To.Row);
                                lv_TaskJob_Ctrl.Items[i].SubItems[5].Text = string.Format("{0}", Ptr->To.BayID);
                                lv_TaskJob_Ctrl.Items[i].SubItems[6].Text = string.Format("{0}", Ptr->To.LevelID);
                                if ((Ptr->itemType > 0) && (Ptr->itemType <= 8))
                                {
                                    lv_TaskJob_Ctrl.Items[i].SubItems[7].Text = string.Format("{0}", Ptr->itemType);
                                }
                                else lv_TaskJob_Ctrl.Items[i].SubItems[7].Text = "0";
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
                                case ConstClass.SEMI_NONE     : lv_TaskJob_St.Items[i].SubItems[1].Text = "지령없음"; break;
                                case ConstClass.SEMI_MOVE     : lv_TaskJob_St.Items[i].SubItems[1].Text = "Move"; break;
                                case ConstClass.SEMI_TaskLoading  : lv_TaskJob_St.Items[i].SubItems[1].Text = "Loading"; break;
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


                            switch (Ptr->Fork)
                            {
                                case 1: lv_TaskJob_St.Items[i].SubItems[3].Text = "Fork1"; break;
                                case 2: lv_TaskJob_St.Items[i].SubItems[3].Text = "Fork2"; break;
                                default: lv_TaskJob_St.Items[i].SubItems[3].Text = ""; break;
                            }
                            lv_TaskJob_St.Items[i].SubItems[4].Text = string.Format("S{0}-R{1}-B{2}-L{3}", Ptr->To.Station
                                                      , Ptr->To.Row
                                                      , Ptr->To.BayID
                                                      , Ptr->To.LevelID);
                            lv_TaskJob_St.Items[i].SubItems[5].Text = string.Format("{0}", Ptr->itemType);
                            Ptr = Ptr + 1;
                        }
                    }
                }
            }

        }

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



        #endregion


    }
}

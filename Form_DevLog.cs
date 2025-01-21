using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace VEXI
{
    public partial class Form_DevLog : Form
    {

        public Form_Main form_Main;
        //private static DEVLogManager devLogManager;
        private static VEXI_DEFS.TDEV_REC_LogReq dev_Rec_LogReq;

        private DateTime RxDateTime;

        public Form_DevLog()
        {
            InitializeComponent();


        }
        #region 컴포넌트 이벤트
        private void Form_DevLog_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }

            if (Form_Main.devLogManager == null)
            {
                Form_Main.devLogManager = new DEVLogManager(Application.StartupPath);
            }
            {
                Form_Main.devLogManager.ClearLogList();
            }

        }


        private void Form_DevLog_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form_Main.devLogManager.ClearLogList();

        }

        ~Form_DevLog()
        {
            
        }

        private void btn_Delete_AlarmLog_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "장치의 저장된 알람로그를 모두 삭제하시겠습니까?"))
            {
                form_Main.Do_Ctrl_Cmd_withOnebyte(ConstClass.CMD1_00, ConstClass.CMD2_35, 0);
            }
        }

        private void btn_Log_Req_Click(object sender, EventArgs e)
        {
            Form_Main.devLogManager.ClearLogList();
            lv_DevLog.Items.Clear();
            lblProgress.Text = "0/0";


            Disable_Btn();
            Request_Log(0);
            NoAnswerTimer.Enabled = true;
        }

        private void btn_Log_TextFileSave_Click(object sender, EventArgs e)
        {
            if (lv_DevLog.Items.Count == 0) return;

            saveFileDialog1.Filter = "*.txt|*.txt";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, Encoding.UTF8);
                StringBuilder sb = new System.Text.StringBuilder();
                sb.Clear();
                for (int j = 0; j < lv_DevLog.Columns.Count; j++)
                {
                    sb.Append("\"" + lv_DevLog.Columns[j].Text + "\"");
                    if (j != (lv_DevLog.Columns.Count - 1))
                    {
                        sb.Append(",");
                    }
                }
                sw.WriteLine(sb);

                for (int i =0; i<lv_DevLog.Items.Count; i++ )
                {
                    sb.Clear();
                    for (int j = 0; j < lv_DevLog.Columns.Count; j++)
                    {
                        sb.Append("\"" + lv_DevLog.Items[i].SubItems[j].Text + "\"");
                        //sb.Append(lv_DevLog.Items[i].SubItems[j].Text);
                        if (j != (lv_DevLog.Columns.Count - 1))
                        {
                            sb.Append(",");
                        }
                    }

                    sw.WriteLine(sb);
                }
                sw.Close();
            }
        }

        private void NoAnswerTimer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Compare(RxDateTime, DateTime.Now) > 0)
            {
                RxDateTime = DateTime.Now;
            }

            TimeSpan ts = DateTime.Now - RxDateTime;

            if (ts.TotalSeconds >= 2)
            {
                Enable_Btn();
                NoAnswerTimer.Enabled = false;
            }
        }


        private void btn_Log_FileSave_Click(object sender, EventArgs e)
        {
            if (lv_DevLog.Items.Count == 0) return;
            
            saveFileDialog1.Filter = "*.LOGDATA|*.LogData";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Form_Main.devLogManager.SaveLogToFile(saveFileDialog1.FileName);
            }
        }

        private void btn_Log_FileLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.LOGDATA|*.LogData";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Form_Main.devLogManager.ClearLogList();
                lv_DevLog.Items.Clear();
                lblProgress.Text = "0/0";

                Form_Main.devLogManager.LoadLogFromFile(openFileDialog1.FileName);
                lblProgress.Text = string.Format("{0}/{1}", Form_Main.devLogManager.LogItemCount, Form_Main.devLogManager.TotalCount);
                DisplayLog_INSERT(Form_Main.devLogManager.TotalCount);
            }
        }
        #endregion

        #region 기능함수

        private void MakeListView(byte tmpDatatype)
        {
            byte ColumnCount = (byte)lv_DevLog.Columns.Count;

            for (byte i=4; i < ColumnCount; i++)
            {
                lv_DevLog.Columns.RemoveAt(4);
            }

            switch (tmpDatatype)
            {
                case 0:
                    lv_DevLog.Columns.Add("위치", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업1", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업1_단계", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업2", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업2_단계", 150, HorizontalAlignment.Center);
                    break;
                case 10:
                    lv_DevLog.Columns.Add("위치", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업1", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업1_단계", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업2", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업2_단계", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업코드", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("현재위치(mm)", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("현재속도", 150, HorizontalAlignment.Center);
                    break;
                case 20:
                    lv_DevLog.Columns.Add("위치", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업1", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업1_단계", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업2", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업2_단계", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업코드", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("현재위치(mm)", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("현재속도", 150, HorizontalAlignment.Center);
                    break;
                case 1:
                case 11:
                case 21:
                    lv_DevLog.Columns.Add("위치", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업1", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업1_단계", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업2", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업2_단계", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("비고", 400, HorizontalAlignment.Center);
                    break;
                case 2:
                    lv_DevLog.Columns.Add("위치", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업1", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업1_단계", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("작업1 번호", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("위치", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("속도", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Main Code", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Sub Code", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("FB Code", 150, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("비고", 400, HorizontalAlignment.Center);
                    break;
            }
        }

        public unsafe void DisplayLog_ADD(UInt16 From, int Count)
        {
            if (lv_DevLog.Items.Count == 0)
            {
                MakeListView(Form_Main.devLogManager.DataType);
            }

            ListViewItem tmpListViewitem;
            VEXI_DEFS.TLogUnionRec logUnion = new VEXI_DEFS.TLogUnionRec();

            switch (Form_Main.devLogManager.DataType)
            {
                case 0:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}-{3}", logUnion.LogSRM00Rec.ItemCell.Station, logUnion.LogSRM00Rec.ItemCell.Row, logUnion.LogSRM00Rec.ItemCell.Bay, logUnion.LogSRM00Rec.ItemCell.Level));


                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogSRM00Rec.Work1_Type));
                            if (logUnion.LogSRM00Rec.Work1_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogSRM00Rec.Work1_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogSRM00Rec.Work1_Step));
                            }
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogSRM00Rec.Work2_Type));
                            if (logUnion.LogSRM00Rec.Work2_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogSRM00Rec.Work2_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogSRM00Rec.Work2_Step));
                            }
                        }
                    }
                    break;
                case 1:
                    for (int i = From; i < From + Count; i++)
                    {
                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}-{3}", logUnion.LogSRM01Rec.Log.ItemCell.Station, logUnion.LogSRM01Rec.Log.ItemCell.Row, logUnion.LogSRM01Rec.Log.ItemCell.Bay, logUnion.LogSRM01Rec.Log.ItemCell.Level));

                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogSRM01Rec.Log.Work1_Type));
                            if (logUnion.LogSRM01Rec.Log.Work1_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogSRM01Rec.Log.Work1_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogSRM01Rec.Log.Work1_Step));
                            }
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogSRM01Rec.Log.Work2_Type));
                            if (logUnion.LogSRM01Rec.Log.Work2_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogSRM01Rec.Log.Work2_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogSRM01Rec.Log.Work2_Step));
                            }

                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_BytePtrToHexStr(logUnion.LogSRM01Rec.DIO, 29, ConstClass.TWithSpaceFlag.WithSpace, 5));
                        }
                    }
                    break;

                case 2:
                    for (int i = From; i < From + Count; i++)
                    {
                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}-{3}", logUnion.LogSRM02Rec.ItemCell.Station, logUnion.LogSRM02Rec.ItemCell.Row, logUnion.LogSRM02Rec.ItemCell.Bay, logUnion.LogSRM02Rec.ItemCell.Level));

                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogSRM02Rec.Work1_Type));
                            if (logUnion.LogSRM02Rec.Work1_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogSRM02Rec.Work1_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogSRM02Rec.Work1_Step));
                            }
                            
                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogSRM02Rec.Work1_JobNumber));

                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogSRM02Rec.Position));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogSRM02Rec.Speed));
                            tmpListViewitem.SubItems.Add(String.Format("0x{0:X8}", logUnion.LogSRM02Rec.MainCode));
                            tmpListViewitem.SubItems.Add(String.Format("0x{0:X8}", logUnion.LogSRM02Rec.SubCode));
                            tmpListViewitem.SubItems.Add(String.Format("0x{0:X8}", logUnion.LogSRM02Rec.FBCode));

                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_BytePtrToHexStr(logUnion.LogSRM02Rec.DIO, 29, ConstClass.TWithSpaceFlag.WithSpace, 5));
                        }
                    }
                    break;

                case 10:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}", logUnion.LogRTVRec.Log.Station, logUnion.LogRTVRec.Log.Position));


                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogRTVRec.Log.Work1_Type));
                            if (logUnion.LogRTVRec.Log.Work1_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogRTVRec.Log.Work1_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogRTVRec.Log.Work1_Step));
                            }
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogRTVRec.Log.Work2_Type));
                            if (logUnion.LogRTVRec.Log.Work2_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogRTVRec.Log.Work2_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogRTVRec.Log.Work2_Step));
                            }

                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRTVRec.Log.WorkCode));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRTVRec.PositionMM));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRTVRec.Speed));
                        }
                    }
                    break;

                case 11:
                    for (int i = From; i < From + Count; i++)
                    {
                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}", logUnion.LogRTVIORec.Log.Station, logUnion.LogRTVIORec.Log.Position));

                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogRTVIORec.Log.Work1_Type));
                            if (logUnion.LogRTVIORec.Log.Work1_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogRTVIORec.Log.Work1_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogRTVIORec.Log.Work1_Step));
                            }
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogRTVIORec.Log.Work2_Type));
                            if (logUnion.LogRTVIORec.Log.Work2_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogRTVIORec.Log.Work2_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogRTVIORec.Log.Work2_Step));
                            }

                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_BytePtrToHexStr(logUnion.LogRTVIORec.DIO, 22, ConstClass.TWithSpaceFlag.WithSpace, 5));
                        }
                    }
                    break;

                case 20:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}", logUnion.LogRTVRec.Log.Station, logUnion.LogRTVRec.Log.Position));


                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogRTVRec.Log.Work1_Type));
                            if (logUnion.LogRTVRec.Log.Work1_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogRTVRec.Log.Work1_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogRTVRec.Log.Work1_Step));
                            }
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogRTVRec.Log.Work2_Type));
                            if (logUnion.LogRTVRec.Log.Work2_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogRTVRec.Log.Work2_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogRTVRec.Log.Work2_Step));
                            }

                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRTVRec.Log.WorkCode));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRTVRec.PositionMM));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRTVRec.Speed));
                        }
                    }
                    break;

                case 21:
                    for (int i = From; i < From + Count; i++)
                    {
                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}", logUnion.LogRTVIORec.Log.Station, logUnion.LogRTVIORec.Log.Position));

                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogRTVIORec.Log.Work1_Type));
                            if (logUnion.LogRTVIORec.Log.Work1_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogRTVIORec.Log.Work1_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogRTVIORec.Log.Work1_Step));
                            }
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogRTVIORec.Log.Work2_Type));
                            if (logUnion.LogRTVIORec.Log.Work2_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogRTVIORec.Log.Work2_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogRTVIORec.Log.Work2_Step));
                            }

                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_BytePtrToHexStr(logUnion.LogRTVIORec.DIO, 22, ConstClass.TWithSpaceFlag.WithSpace, 5));
                        }
                    }
                    break;

            }
        }


        public unsafe void DisplayLog_INSERT(int Count)
        {
            if (lv_DevLog.Items.Count == 0)
            {
                MakeListView(Form_Main.devLogManager.DataType);
            }

            int FromIndex = lv_DevLog.Items.Count;

            switch (Form_Main.devLogManager.DataType)
            {
                case 0:
                case 1:
                case 2:
                    if (form_Main.COMMDataManager.RX_DestDevType != ConstClass.TYPE_SRM) lbl_TypeError.Visible = true; break;
                case 10:
                case 11:
                    if (form_Main.COMMDataManager.RX_DestDevType != ConstClass.TYPE_RTV) lbl_TypeError.Visible = true; break;
                case 20:
                case 21:
                    if (form_Main.COMMDataManager.RX_DestDevType != ConstClass.TYPE_EMS) lbl_TypeError.Visible = true; break;
                default:
                    lbl_TypeError.Visible = false; break;
            }


            ListViewItem tmpListViewitem;
            VEXI_DEFS.TLogUnionRec logUnion = new VEXI_DEFS.TLogUnionRec();

            switch (Form_Main.devLogManager.DataType)
            {
                case 0:
                    for (int i = 0; i <  Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i + FromIndex, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Insert(0,string.Format("{0}", 1 + lv_DevLog.Items.Count));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}-{3}", logUnion.LogSRM00Rec.ItemCell.Station, logUnion.LogSRM00Rec.ItemCell.Row, logUnion.LogSRM00Rec.ItemCell.Bay, logUnion.LogSRM00Rec.ItemCell.Level));


                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogSRM00Rec.Work1_Type));
                            if (logUnion.LogSRM00Rec.Work1_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogSRM00Rec.Work1_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogSRM00Rec.Work1_Step));
                            }
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogSRM00Rec.Work2_Type));
                            if (logUnion.LogSRM00Rec.Work2_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogSRM00Rec.Work2_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogSRM00Rec.Work2_Step));
                            }
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < Count; i++)
                    {
                        if (Form_Main.devLogManager.LogItem(i + FromIndex, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Insert(0, string.Format("{0}", 1 + lv_DevLog.Items.Count));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}-{3}", logUnion.LogSRM01Rec.Log.ItemCell.Station, logUnion.LogSRM01Rec.Log.ItemCell.Row, logUnion.LogSRM01Rec.Log.ItemCell.Bay, logUnion.LogSRM01Rec.Log.ItemCell.Level));

                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogSRM01Rec.Log.Work1_Type));
                            if (logUnion.LogSRM01Rec.Log.Work1_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogSRM01Rec.Log.Work1_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogSRM01Rec.Log.Work1_Step));
                            }
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogSRM01Rec.Log.Work2_Type));
                            if (logUnion.LogSRM01Rec.Log.Work2_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogSRM01Rec.Log.Work2_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogSRM01Rec.Log.Work2_Step));
                            }

                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_BytePtrToHexStr(logUnion.LogSRM01Rec.DIO, 29, ConstClass.TWithSpaceFlag.WithSpace, 5));
                        }
                    }
                    break;

                case 2:
                    for (int i = 0; i < Count; i++)
                    {
                        if (Form_Main.devLogManager.LogItem(i + FromIndex, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Insert(0, string.Format("{0}", 1 + lv_DevLog.Items.Count));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}-{3}", logUnion.LogSRM02Rec.ItemCell.Station, logUnion.LogSRM02Rec.ItemCell.Row, logUnion.LogSRM02Rec.ItemCell.Bay, logUnion.LogSRM02Rec.ItemCell.Level));

                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogSRM02Rec.Work1_Type));
                            if (logUnion.LogSRM02Rec.Work1_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogSRM02Rec.Work1_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogSRM02Rec.Work1_Step));
                            }
                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogSRM02Rec.Work1_JobNumber));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogSRM02Rec.Position));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogSRM02Rec.Speed));
                            tmpListViewitem.SubItems.Add(String.Format("0x{0:X8}", logUnion.LogSRM02Rec.MainCode));
                            tmpListViewitem.SubItems.Add(String.Format("0x{0:X8}", logUnion.LogSRM02Rec.SubCode));
                            tmpListViewitem.SubItems.Add(String.Format("0x{0:X8}", logUnion.LogSRM02Rec.FBCode));


                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_BytePtrToHexStr(logUnion.LogSRM02Rec.DIO, 29, ConstClass.TWithSpaceFlag.WithSpace, 5));
                        }
                    }
                    break;

                case 10:
                    for (int i = 0; i < Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i + FromIndex, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Insert(0, string.Format("{0}", 1 + lv_DevLog.Items.Count));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}", logUnion.LogRTVRec.Log.Station, logUnion.LogRTVRec.Log.Position));


                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogRTVRec.Log.Work1_Type));
                            if (logUnion.LogRTVRec.Log.Work1_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogRTVRec.Log.Work1_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogRTVRec.Log.Work1_Step));
                            }
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogRTVRec.Log.Work2_Type));
                            if (logUnion.LogRTVRec.Log.Work2_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogRTVRec.Log.Work2_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogRTVRec.Log.Work2_Step));
                            }

                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRTVRec.Log.WorkCode));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRTVRec.PositionMM));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRTVRec.Speed));
                        }
                    }
                    break;

                case 11:
                    for (int i = 0; i < Count; i++)
                    {
                        if (Form_Main.devLogManager.LogItem(i + FromIndex, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Insert(0, string.Format("{0}", 1 + lv_DevLog.Items.Count));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}", logUnion.LogRTVIORec.Log.Station, logUnion.LogRTVIORec.Log.Position));

                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogRTVIORec.Log.Work1_Type));
                            if (logUnion.LogRTVIORec.Log.Work1_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogRTVIORec.Log.Work1_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogRTVIORec.Log.Work1_Step));
                            }
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogRTVIORec.Log.Work2_Type));
                            if (logUnion.LogRTVIORec.Log.Work2_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogRTVIORec.Log.Work2_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogRTVIORec.Log.Work2_Step));
                            }

                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_BytePtrToHexStr(logUnion.LogRTVIORec.DIO, 22, ConstClass.TWithSpaceFlag.WithSpace, 5));
                        }
                    }
                    break;

                case 20:
                    for (int i = 0; i < Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i + FromIndex, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Insert(0, string.Format("{0}", 1 + lv_DevLog.Items.Count));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}", logUnion.LogRTVRec.Log.Station, logUnion.LogRTVRec.Log.Position));


                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogRTVRec.Log.Work1_Type));
                            if (logUnion.LogRTVRec.Log.Work1_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogRTVRec.Log.Work1_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogRTVRec.Log.Work1_Step));
                            }
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogRTVRec.Log.Work2_Type));
                            if (logUnion.LogRTVRec.Log.Work2_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogRTVRec.Log.Work2_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogRTVRec.Log.Work2_Step));
                            }

                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRTVRec.Log.WorkCode));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRTVRec.PositionMM));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRTVRec.Speed));
                        }
                    }
                    break;

                case 21:
                    for (int i = 0; i < Count; i++)
                    {
                        if (Form_Main.devLogManager.LogItem(i + FromIndex, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Insert(0, string.Format("{0}", 1 + lv_DevLog.Items.Count));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}", logUnion.LogRTVIORec.Log.Station, logUnion.LogRTVIORec.Log.Position));

                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogRTVIORec.Log.Work1_Type));
                            if (logUnion.LogRTVIORec.Log.Work1_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogRTVIORec.Log.Work1_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogRTVIORec.Log.Work1_Step));
                            }
                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobTextAsValue(logUnion.LogRTVIORec.Log.Work2_Type));
                            if (logUnion.LogRTVIORec.Log.Work2_Step >= 0x80)
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetTaskStepTextAsValue(logUnion.LogRTVIORec.Log.Work2_Step));
                            }
                            else
                            {
                                tmpListViewitem.SubItems.Add(Global_Class.UTIL_GetJobStepTextAsValue(logUnion.LogRTVIORec.Log.Work2_Step));
                            }

                            tmpListViewitem.SubItems.Add(Global_Class.UTIL_BytePtrToHexStr(logUnion.LogRTVIORec.DIO, 22, ConstClass.TWithSpaceFlag.WithSpace, 5));
                        }
                    }
                    break;

            }
        }

        //데이터 처리 함수
        public unsafe void Process_DevLog(byte[] datas)
        {
            VEXI_DEFS.TDEV_REC_LogHeader dev_REC_LogHeader;
            byte HeaderLen;
            int index = 0;

            HeaderLen = (byte)Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_LogHeader));
            dev_REC_LogHeader = (VEXI_DEFS.TDEV_REC_LogHeader)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TDEV_REC_LogHeader), index, HeaderLen);

            //30부터는 이벤트 로그 타입으로 이벤트 로그 타입은 Form_DevEventLog 에서 처리 한다
            if (dev_REC_LogHeader.DataType >= 30)
            {
                return;
            }

            byte count = 0;
            UInt16 PrevStartIndex;
            VEXI_DEFS.TLogUnionRec logUnion;

            RxDateTime = DateTime.Now;
            index = index + HeaderLen;

            PrevStartIndex = (UInt16)lv_DevLog.Items.Count;

            Form_Main.devLogManager.TotalCount = dev_REC_LogHeader.TotalLogCount;
            Form_Main.devLogManager.DataType = dev_REC_LogHeader.DataType;
            Form_Main.devLogManager.SignedFlag = dev_REC_LogHeader.signedFlag;

            while (true)
            {
                if (dev_REC_LogHeader.TotalLogCount == 0) break;
                if (dev_REC_LogHeader.LogCount == 0) break;

                logUnion = new VEXI_DEFS.TLogUnionRec();

                Global_Class.UTIL_ByteArrayToBytePtr(datas, (byte*)&logUnion.LogItemHeader.LogTime, index, 0, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGItemHeader)));
                index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGItemHeader)); 

                switch (dev_REC_LogHeader.DataType)
                {
                    case 0: 
                        logUnion.LogSRM00Rec = (VEXI_DEFS.TLOGType_SRM_00)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TLOGType_SRM_00), index, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_SRM_00)));
                        index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_SRM_00));
                        break;
                    case 1:
                        logUnion.LogSRM01Rec = (VEXI_DEFS.TLOGType_SRM_01)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TLOGType_SRM_01), index, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_SRM_01)));
                        index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_SRM_01));
                        break;
                    case 2:
                        logUnion.LogSRM02Rec = (VEXI_DEFS.TLOGType_SRM_02)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TLOGType_SRM_02), index, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_SRM_02)));
                        index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_SRM_02));
                        break;
                    case 10:
                        logUnion.LogRTVRec = (VEXI_DEFS.TLOGType_RTV)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TLOGType_RTV), index, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_RTV)));
                        index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_RTV));
                        break;
                    case 11:
                        logUnion.LogRTVIORec = (VEXI_DEFS.TLOGType_RTV_IO)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TLOGType_RTV_IO), index, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_RTV_IO)));
                        index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_RTV_IO));
                        break;
                    case 20:
                        logUnion.LogRTVRec = (VEXI_DEFS.TLOGType_RTV)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TLOGType_RTV), index, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_RTV)));
                        index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_RTV));
                        break;
                    case 21:
                        logUnion.LogRTVIORec = (VEXI_DEFS.TLOGType_RTV_IO)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TLOGType_RTV_IO), index, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_RTV_IO)));
                        index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_RTV_IO));
                        break;
                }
                count++;

                Form_Main.devLogManager.ADDLog(logUnion);


                if (count == dev_REC_LogHeader.LogCount) break;
            }

            lblProgress.Text = string.Format("{0}/{1}", Form_Main.devLogManager.LogItemCount, Form_Main.devLogManager.TotalCount);
            DisplayLog_INSERT(dev_REC_LogHeader.LogCount);
            //DisplayLog_ADD((ushort)lv_DevLog.Items.Count, dev_REC_LogHeader.LogCount);


            if ((Form_Main.devLogManager.LogItemCount >= Form_Main.devLogManager.TotalCount) || (dev_REC_LogHeader.LogCount == 0))
            {
                NoAnswerTimer.Enabled = false;
                Enable_Btn();

            } else
            {
                Request_Log((UInt16)Form_Main.devLogManager.LogItemCount);
            }
        }

        private void Disable_Btn()
        {
            btn_Log_Req.Enabled = false;
            btn_Log_FileSave.Enabled = false;
            btn_Log_FileLoad.Enabled = false;
            btn_Log_TextFileSave.Enabled = false;
        }

        private void Enable_Btn()
        {
            btn_Log_Req.Enabled = true;
            btn_Log_FileSave.Enabled = true;
            btn_Log_FileLoad.Enabled = true;
            btn_Log_TextFileSave.Enabled = true;
        }

        private void Request_Log(UInt16 TmpLogindex)
        {
            dev_Rec_LogReq.LogKind = 0;
            dev_Rec_LogReq.ReqKind = 0;

            dev_Rec_LogReq.LogIndex = TmpLogindex;

            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_34, dev_Rec_LogReq);
            RxDateTime = DateTime.Now;
        }


        #endregion


    }
}

using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace VEXI
{
    public partial class Form_DevEventLog : Form
    {

        public Form_Main form_Main;
        private static DEVLogManager devLogManager;
        private static VEXI_DEFS.TDEV_REC_LogReq dev_Rec_LogReq;
        private static bool ISStop = false;

        private DateTime RxDateTime;

        public Form_DevEventLog()
        {
            InitializeComponent();

            if (devLogManager == null)
            {
                devLogManager = new DEVLogManager(Application.StartupPath);
            }
            {
                devLogManager.ClearLogList();
            }

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
        }


        private void Form_DevLog_FormClosed(object sender, FormClosedEventArgs e)
        {
            devLogManager.ClearLogList();

        }

        ~Form_DevEventLog()
        {
            
        }

        private void btn_Delete_AlarmLog_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "장치의 저장된 이벤트로그를 모두 삭제하시겠습니까?"))
            {
                form_Main.Do_Ctrl_Cmd_withOnebyte(ConstClass.CMD1_00, ConstClass.CMD2_35, 1);
            }
        }

        private void btn_Log_Req_Click(object sender, EventArgs e)
        {
            ISStop = false;

            devLogManager.ClearLogList();
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
                devLogManager.SaveLogToFile(saveFileDialog1.FileName);
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
                devLogManager.ClearLogList();
                lv_DevLog.Items.Clear();
                lblProgress.Text = "0/0";

                devLogManager.LoadLogFromFile(openFileDialog1.FileName);
                lblProgress.Text = string.Format("{0}/{1}", devLogManager.LogItemCount, devLogManager.TotalCount);
                DisplayLog_ADD(0,devLogManager.TotalCount);
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
                case 30:
                    lv_DevLog.Columns.Add("Data0", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data3", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data4", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data5", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data6", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data7", 55, HorizontalAlignment.Center);
                    break;
                case 31:
                    lv_DevLog.Columns.Add("Data0", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data3", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data4", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data5", 100, HorizontalAlignment.Center);
                    break;
                case 32:
                    lv_DevLog.Columns.Add("Data0", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data3", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data4", 100, HorizontalAlignment.Center);
                    break;
                case 33:
                    lv_DevLog.Columns.Add("Data0", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data3", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data4", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data5", 80, HorizontalAlignment.Center);
                    break;
                case 34:
                    lv_DevLog.Columns.Add("Data0", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data1", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data2", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data3", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data4", 80, HorizontalAlignment.Center);
                    break;
                case 35:
                    lv_DevLog.Columns.Add("Data0", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data1", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data2", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data3", 100, HorizontalAlignment.Center);
                    break;
                case 36:
                    lv_DevLog.Columns.Add("Data0", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data1", 100, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data2", 100, HorizontalAlignment.Center);
                    break;
                case 37:
                    lv_DevLog.Columns.Add("Data0", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data3", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data4", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data5", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data6", 80, HorizontalAlignment.Center);
                    break;
            }
        }

        public unsafe void DisplayLog_ADD(UInt16 From, int Count)
        {
            if (lv_DevLog.Items.Count == 0)
            {
                MakeListView(devLogManager.DataType);
            }

            ListViewItem tmpListViewitem;
            VEXI_DEFS.TLogUnionRec logUnion = new VEXI_DEFS.TLogUnionRec();

            switch (devLogManager.DataType)
            {
                case 30:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));

                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM: tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_RTV: tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_EMS: tmpListViewitem.SubItems.Add(Global_Class.UTIL_EMSAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                default:                  tmpListViewitem.SubItems.Add(""); break;
                            }

                            if ((devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_0));

                            if ((devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_1));

                            if ((devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_2));

                            if ((devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_3));

                            if ((devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_4));

                            if ((devLogManager.SignedFlag & 0x20) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_5));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_5));

                            if ((devLogManager.SignedFlag & 0x40) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_6));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_6));

                            if ((devLogManager.SignedFlag & 0x80) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_7));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_7));

                        }
                    }
                    break;
                case 31:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));

                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM: tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_RTV: tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_EMS: tmpListViewitem.SubItems.Add(Global_Class.UTIL_EMSAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                default: tmpListViewitem.SubItems.Add(""); break;
                            }

                            if ((devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_31.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_0));

                            if ((devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_31.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_1));

                            if ((devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_31.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_2));

                            if ((devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_31.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_3));

                            if ((devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_31.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_4));

                            if ((devLogManager.SignedFlag & 0x20) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_31.Logvalue_5));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_5));
                        }
                    }
                    break;
                case 32:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM: tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_RTV: tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_EMS: tmpListViewitem.SubItems.Add(Global_Class.UTIL_EMSAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                default: tmpListViewitem.SubItems.Add(""); break;
                            }

                            if ((devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_32.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_32.Logvalue_0));

                            if ((devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_32.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_32.Logvalue_1));

                            if ((devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_32.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_32.Logvalue_2));

                            if ((devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_32.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_32.Logvalue_3));

                            if ((devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_32.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_32.Logvalue_4));
                        }
                    }
                    break;
                case 33:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM: tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_RTV: tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_EMS: tmpListViewitem.SubItems.Add(Global_Class.UTIL_EMSAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                default: tmpListViewitem.SubItems.Add(""); break;
                            }

                            if ((devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_33.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_0));

                            if ((devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_33.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_1));

                            if ((devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_33.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_2));

                            if ((devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_33.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_3));

                            if ((devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_33.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_4));

                            if ((devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_33.Logvalue_5));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_5));
                        }
                    }
                    break;
                case 34:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM: tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_RTV: tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_EMS: tmpListViewitem.SubItems.Add(Global_Class.UTIL_EMSAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                default: tmpListViewitem.SubItems.Add(""); break;
                            }

                            if ((devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_34.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_34.Logvalue_0));

                            if ((devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_34.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_34.Logvalue_1));

                            if ((devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_34.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_34.Logvalue_2));

                            if ((devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_34.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_34.Logvalue_3));

                            if ((devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_34.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_34.Logvalue_4));
                        }
                    }
                    break;
                case 35:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM: tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_RTV: tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_EMS: tmpListViewitem.SubItems.Add(Global_Class.UTIL_EMSAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                default: tmpListViewitem.SubItems.Add(""); break;
                            }

                            if ((devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_35.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_35.Logvalue_0));

                            if ((devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_35.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_35.Logvalue_1));

                            if ((devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_35.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_35.Logvalue_2));

                            if ((devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_35.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_35.Logvalue_3));
                        }
                    }
                    break;
                case 36:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM: tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_RTV: tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_EMS: tmpListViewitem.SubItems.Add(Global_Class.UTIL_EMSAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                default: tmpListViewitem.SubItems.Add(""); break;
                            }

                            if ((devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_36.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_36.Logvalue_0));

                            if ((devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_36.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_36.Logvalue_1));

                            if ((devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_36.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_36.Logvalue_2));
                        }
                    }
                    break;
                case 37:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM: tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_RTV: tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_EMS: tmpListViewitem.SubItems.Add(Global_Class.UTIL_EMSAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                default: tmpListViewitem.SubItems.Add(""); break;
                            }

                            if ((devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_37.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_0));

                            if ((devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_37.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_1));

                            if ((devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_37.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_2));

                            if ((devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_37.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_3));

                            if ((devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_37.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_4));

                            if ((devLogManager.SignedFlag & 0x20) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_37.Logvalue_5));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_5));

                            if ((devLogManager.SignedFlag & 0x40) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_37.Logvalue_6));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_6));
                        }
                    }
                    break;
            }
        }


        public unsafe void DisplayLog_INSERT(int Count)
        {
            if (lv_DevLog.Items.Count == 0)
            {
                MakeListView(devLogManager.DataType);
            }

            int FromIndex = lv_DevLog.Items.Count;

            ListViewItem tmpListViewitem;
            VEXI_DEFS.TLogUnionRec logUnion = new VEXI_DEFS.TLogUnionRec();

            switch (devLogManager.DataType)
            {
                case 30:
                    for (int i = 0; i < Count; i++)
                    {

                        if (devLogManager.LogItem(i + FromIndex, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Insert(0, string.Format("{0}", 1 + lv_DevLog.Items.Count));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM: tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_RTV: tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_EMS: tmpListViewitem.SubItems.Add(Global_Class.UTIL_EMSAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                default: tmpListViewitem.SubItems.Add(""); break;
                            }

                            if ((devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_0));

                            if ((devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_1));

                            if ((devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_2));

                            if ((devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_3));

                            if ((devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_4));

                            if ((devLogManager.SignedFlag & 0x20) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_5));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_5));

                            if ((devLogManager.SignedFlag & 0x40) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_6));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_6));

                            if ((devLogManager.SignedFlag & 0x80) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_7));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_7));

                        }
                    }
                    break;
                case 31:
                    for (int i = 0; i < Count; i++)
                    {

                        if (devLogManager.LogItem(i + FromIndex, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Insert(0, string.Format("{0}", 1 + lv_DevLog.Items.Count));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM: tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_RTV: tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_EMS: tmpListViewitem.SubItems.Add(Global_Class.UTIL_EMSAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                default: tmpListViewitem.SubItems.Add(""); break;
                            }

                            if ((devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_31.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_0));

                            if ((devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_31.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_1));

                            if ((devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_31.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_2));

                            if ((devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_31.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_3));

                            if ((devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_31.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_4));

                            if ((devLogManager.SignedFlag & 0x20) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_31.Logvalue_5));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_5));
                        }
                    }
                    break;
                case 32:
                    for (int i = 0; i < Count; i++)
                    {

                        if (devLogManager.LogItem(i + FromIndex, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Insert(0, string.Format("{0}", 1 + lv_DevLog.Items.Count));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM: tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_RTV: tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_EMS: tmpListViewitem.SubItems.Add(Global_Class.UTIL_EMSAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                default: tmpListViewitem.SubItems.Add(""); break;
                            }

                            if ((devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_32.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_32.Logvalue_0));

                            if ((devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_32.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_32.Logvalue_1));

                            if ((devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_32.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_32.Logvalue_2));

                            if ((devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_32.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_32.Logvalue_3));

                            if ((devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_32.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_32.Logvalue_4));
                        }
                    }
                    break;
                case 33:
                    for (int i = 0; i < Count; i++)
                    {

                        if (devLogManager.LogItem(i + FromIndex, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Insert(0, string.Format("{0}", 1 + lv_DevLog.Items.Count));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM: tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_RTV: tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_EMS: tmpListViewitem.SubItems.Add(Global_Class.UTIL_EMSAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                default: tmpListViewitem.SubItems.Add(""); break;
                            }

                            if ((devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_33.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_0));

                            if ((devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_33.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_1));

                            if ((devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_33.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_2));

                            if ((devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_33.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_3));

                            if ((devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_33.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_4));

                            if ((devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_33.Logvalue_5));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_5));
                        }
                    }
                    break;
                case 34:
                    for (int i = 0; i < Count; i++)
                    {

                        if (devLogManager.LogItem(i + FromIndex, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Insert(0, string.Format("{0}", 1 + lv_DevLog.Items.Count));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM: tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_RTV: tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_EMS: tmpListViewitem.SubItems.Add(Global_Class.UTIL_EMSAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                default: tmpListViewitem.SubItems.Add(""); break;
                            }

                            if ((devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_34.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_34.Logvalue_0));

                            if ((devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_34.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_34.Logvalue_1));

                            if ((devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_34.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_34.Logvalue_2));

                            if ((devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_34.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_34.Logvalue_3));

                            if ((devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_34.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_34.Logvalue_4));
                        }
                    }
                    break;
                case 35:
                    for (int i = 0; i < Count; i++)
                    {

                        if (devLogManager.LogItem(i + FromIndex, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Insert(0, string.Format("{0}", 1 + lv_DevLog.Items.Count));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM: tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_RTV: tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_EMS: tmpListViewitem.SubItems.Add(Global_Class.UTIL_EMSAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                default: tmpListViewitem.SubItems.Add(""); break;
                            }

                            if ((devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_35.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_35.Logvalue_0));

                            if ((devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_35.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_35.Logvalue_1));

                            if ((devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_35.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_35.Logvalue_2));

                            if ((devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_35.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_35.Logvalue_3));
                        }
                    }
                    break;
                case 36:
                    for (int i = 0; i < Count; i++)
                    {

                        if (devLogManager.LogItem(i + FromIndex, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Insert(0, string.Format("{0}", 1 + lv_DevLog.Items.Count));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM: tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_RTV: tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_EMS: tmpListViewitem.SubItems.Add(Global_Class.UTIL_EMSAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                default: tmpListViewitem.SubItems.Add(""); break;
                            }

                            if ((devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_36.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_36.Logvalue_0));

                            if ((devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_36.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_36.Logvalue_1));

                            if ((devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_36.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_36.Logvalue_2));
                        }
                    }
                    break;
                case 37:
                    for (int i = 0; i < Count; i++)
                    {

                        if (devLogManager.LogItem(i + FromIndex, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Insert(0, string.Format("{0}", 1 + lv_DevLog.Items.Count));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}-{1}-{2}", logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3));
                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM: tmpListViewitem.SubItems.Add(Global_Class.UTIL_SRMAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_RTV: tmpListViewitem.SubItems.Add(Global_Class.UTIL_RTVAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                case ConstClass.TYPE_EMS: tmpListViewitem.SubItems.Add(Global_Class.UTIL_EMSAlarmName(logUnion.LogItemHeader.Code_1, logUnion.LogItemHeader.Code_2, logUnion.LogItemHeader.Code_3)); break;
                                default: tmpListViewitem.SubItems.Add(""); break;
                            }

                            if ((devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_37.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_0));

                            if ((devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_37.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_1));

                            if ((devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_37.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_2));

                            if ((devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_37.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_3));

                            if ((devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_37.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_4));

                            if ((devLogManager.SignedFlag & 0x20) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_37.Logvalue_5));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_5));

                            if ((devLogManager.SignedFlag & 0x40) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_37.Logvalue_6));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_6));
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

            //30 미만은 알람 로그 타입으로 알람 로그 타입은 Form_DevLog 에서 처리 한다
            if (dev_REC_LogHeader.DataType < 30)
            {
                return;
            }

            byte count = 0;
            UInt16 PrevStartIndex;
            VEXI_DEFS.TLogUnionRec logUnion;

            RxDateTime = DateTime.Now;
            index = index + HeaderLen;

            PrevStartIndex = (UInt16)lv_DevLog.Items.Count;

            devLogManager.TotalCount = dev_REC_LogHeader.TotalLogCount;
            devLogManager.DataType = dev_REC_LogHeader.DataType;
            devLogManager.SignedFlag = dev_REC_LogHeader.signedFlag;

            while (true)
            {
                if (dev_REC_LogHeader.TotalLogCount == 0) break;

                logUnion = new VEXI_DEFS.TLogUnionRec();

                Global_Class.UTIL_ByteArrayToBytePtr(datas, (byte*)&logUnion.LogItemHeader.LogTime, index, 0, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGItemHeader)));
                index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGItemHeader)); 

                switch (dev_REC_LogHeader.DataType)
                {
                    case 30:
                        logUnion.LogRec_30 = (VEXI_DEFS.TLOGType_30)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TLOGType_30), index, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_30)));
                        index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_30));
                        break;
                    case 31:
                        logUnion.LogRec_31 = (VEXI_DEFS.TLOGType_31)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TLOGType_31), index, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_31)));
                        index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_31));
                        break;
                    case 32:
                        logUnion.LogRec_32 = (VEXI_DEFS.TLOGType_32)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TLOGType_32), index, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_32)));
                        index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_32));
                        break;
                    case 33:
                        logUnion.LogRec_33 = (VEXI_DEFS.TLOGType_33)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TLOGType_33), index, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_33)));
                        index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_33));
                        break;
                    case 34:
                        logUnion.LogRec_34 = (VEXI_DEFS.TLOGType_34)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TLOGType_34), index, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_34)));
                        index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_34));
                        break;
                    case 35:
                        logUnion.LogRec_35 = (VEXI_DEFS.TLOGType_35)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TLOGType_35), index, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_35)));
                        index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_35));
                        break;
                    case 36:
                        logUnion.LogRec_36 = (VEXI_DEFS.TLOGType_36)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TLOGType_36), index, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_36)));
                        index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_36));
                        break;
                    case 37:
                        logUnion.LogRec_37 = (VEXI_DEFS.TLOGType_37)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TLOGType_37), index, Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_37)));
                        index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TLOGType_37));
                        break;
                }
                count++;

                devLogManager.ADDLog(logUnion);


                if (count == dev_REC_LogHeader.LogCount) break;
            }

            lblProgress.Text = string.Format("{0}/{1}", devLogManager.LogItemCount, devLogManager.TotalCount);
            //DisplayLog_INSERT(dev_REC_LogHeader.LogCount);
            DisplayLog_ADD((ushort)lv_DevLog.Items.Count, dev_REC_LogHeader.LogCount);

            if ((devLogManager.LogItemCount == devLogManager.TotalCount) || (ISStop))
            {
                NoAnswerTimer.Enabled = false;
                Enable_Btn();

            } else
            {
                Request_Log((UInt16)devLogManager.LogItemCount);
            }
        }

        private void Disable_Btn()
        {
            btn_EventLog_Stop.Enabled = true;
            btn_EventLog_Req.Enabled = false;
            btn_Log_FileSave.Enabled = false;
            btn_Log_FileLoad.Enabled = false;
            btn_Log_TextFileSave.Enabled = false;
        }

        private void Enable_Btn()
        {
            btn_EventLog_Stop.Enabled = false;
            btn_EventLog_Req.Enabled = true;
            btn_Log_FileSave.Enabled = true;
            btn_Log_FileLoad.Enabled = true;
            btn_Log_TextFileSave.Enabled = true;
        }

        private void Request_Log(UInt16 TmpLogindex)
        {
            if (TmpLogindex == 0)
            {
                dev_Rec_LogReq.LogKind = 1;

                if (rbAll.Checked)
                {
                    dev_Rec_LogReq.ReqKind = 0;
                }
                else
                {
                    dev_Rec_LogReq.ReqKind = 1;


                    if (dateTimePicker_From.Value.Second >= 1)
                    {
                        dateTimePicker_From.Value = dateTimePicker_From.Value.AddSeconds(-1 * (dateTimePicker_From.Value.Second - 1));
                    }

                    dateTimePicker_To.Value = dateTimePicker_To.Value.AddSeconds(60 - dateTimePicker_To.Value.Second - 1);

                    dev_Rec_LogReq.Req_StartTime = Global_Class.UTIL_GetUnixTimeStampFromLocalTime(dateTimePicker_From.Value);
                    dev_Rec_LogReq.Req_EndTime = Global_Class.UTIL_GetUnixTimeStampFromLocalTime(dateTimePicker_To.Value);
                }
            }

            dev_Rec_LogReq.LogIndex = TmpLogindex;

            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_34, dev_Rec_LogReq);
            RxDateTime = DateTime.Now;
        }



        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_EventLog_Stop_Click(object sender, EventArgs e)
        {
            ISStop = true;

            Enable_Btn();
            NoAnswerTimer.Enabled = false;
        }
    }
}

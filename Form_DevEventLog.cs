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
        //private static DEVLogManager devLogManager;
        private static VEXI_DEFS.TDEV_REC_LogReq dev_Rec_LogReq;
        private static bool ISStop = false;
        private byte MakeList_DataType = 0;
        private Int32 PrevRxIndex = -1;
        private DateTime RxDateTime;

        public Form_DevEventLog()
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

            PrevRxIndex = -1;

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
            
            saveFileDialog1.Filter = "*.EVENTDATA|*.EventData";

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
            openFileDialog1.Filter = "*.EVENTDATA|*.EventData";

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
                DisplayLog_ADD(0, Form_Main.devLogManager.TotalCount);

                Enable_Btn();
            }
        }
        #endregion

        #region 기능함수

        private void MakeListView(byte tmpDatatype)
        {
            MakeList_DataType = tmpDatatype;


            byte ColumnCount = (byte)lv_DevLog.Columns.Count;

            for (byte i=2; i < ColumnCount; i++)
            {
                lv_DevLog.Columns.RemoveAt(2);
            }

            switch (MakeList_DataType)
            {
                case 30:
                    lv_DevLog.Columns.Add("Csae1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Case2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Case3", 55, HorizontalAlignment.Center);
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
                    lv_DevLog.Columns.Add("Csae1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Case2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Case3", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data0", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data3", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data4", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data5", 100, HorizontalAlignment.Center);
                    break;
                case 32:
                    lv_DevLog.Columns.Add("Csae1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Case2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Case3", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data0", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data3", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data4", 100, HorizontalAlignment.Center);
                    break;
                case 33:
                    lv_DevLog.Columns.Add("Csae1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Case2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Case3", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data0", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data3", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data4", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data5", 80, HorizontalAlignment.Center);
                    break;
                case 34:
                    lv_DevLog.Columns.Add("Csae1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Case2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Case3", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data0", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data1", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data2", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data3", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data4", 80, HorizontalAlignment.Center);
                    break;
                case 35:
                    lv_DevLog.Columns.Add("Csae1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Case2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Case3", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data0", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data1", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data2", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data3", 100, HorizontalAlignment.Center);
                    break;
                case 36:
                    lv_DevLog.Columns.Add("Csae1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Case2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Case3", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data0", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data1", 100, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data2", 100, HorizontalAlignment.Center);
                    break;
                case 37:
                    lv_DevLog.Columns.Add("Csae1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Case2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Case3", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data0", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data1", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data2", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data3", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data4", 55, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data5", 80, HorizontalAlignment.Center);
                    lv_DevLog.Columns.Add("Data6", 80, HorizontalAlignment.Center);
                    break;
            }

            Load_Title(MakeList_DataType);
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
                case 30:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_1));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_2));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_3));

                            if ((Form_Main.devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_0));

                            if ((Form_Main.devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_1));

                            if ((Form_Main.devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_2));

                            if ((Form_Main.devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_3));

                            if ((Form_Main.devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_4));

                            if ((Form_Main.devLogManager.SignedFlag & 0x20) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_5));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_5));

                            if ((Form_Main.devLogManager.SignedFlag & 0x40) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_6));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_6));

                            if ((Form_Main.devLogManager.SignedFlag & 0x80) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_30.Logvalue_7));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_30.Logvalue_7));

                        }
                    }
                    break;
                case 31:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_1));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_2));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_3));

                            if ((Form_Main.devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_31.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_0));

                            if ((Form_Main.devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_31.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_1));

                            if ((Form_Main.devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_31.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_2));

                            if ((Form_Main.devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_31.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_3));

                            if ((Form_Main.devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_31.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_4));

                            if ((Form_Main.devLogManager.SignedFlag & 0x20) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_31.Logvalue_5));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_31.Logvalue_5));
                        }
                    }
                    break;
                case 32:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_1));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_2));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_3));

                            if ((Form_Main.devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_32.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_32.Logvalue_0));

                            if ((Form_Main.devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_32.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_32.Logvalue_1));

                            if ((Form_Main.devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_32.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_32.Logvalue_2));

                            if ((Form_Main.devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_32.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_32.Logvalue_3));

                            if ((Form_Main.devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_32.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_32.Logvalue_4));
                        }
                    }
                    break;
                case 33:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_1));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_2));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_3));

                            if ((Form_Main.devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_33.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_0));

                            if ((Form_Main.devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_33.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_1));

                            if ((Form_Main.devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_33.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_2));

                            if ((Form_Main.devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_33.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_3));

                            if ((Form_Main.devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_33.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_4));

                            if ((Form_Main.devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_33.Logvalue_5));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_33.Logvalue_5));
                        }
                    }
                    break;
                case 34:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_1));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_2));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_3));

                            if ((Form_Main.devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_34.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_34.Logvalue_0));

                            if ((Form_Main.devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_34.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_34.Logvalue_1));

                            if ((Form_Main.devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_34.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_34.Logvalue_2));

                            if ((Form_Main.devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_34.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_34.Logvalue_3));

                            if ((Form_Main.devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_34.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_34.Logvalue_4));
                        }
                    }
                    break;
                case 35:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_1));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_2));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_3));

                            if ((Form_Main.devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_35.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_35.Logvalue_0));

                            if ((Form_Main.devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_35.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_35.Logvalue_1));

                            if ((Form_Main.devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_35.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_35.Logvalue_2));

                            if ((Form_Main.devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_35.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_35.Logvalue_3));
                        }
                    }
                    break;
                case 36:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_1));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_2));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_3));

                            if ((Form_Main.devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_36.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_36.Logvalue_0));

                            if ((Form_Main.devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_36.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_36.Logvalue_1));

                            if ((Form_Main.devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (Int32)logUnion.LogRec_36.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_36.Logvalue_2));
                        }
                    }
                    break;
                case 37:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.LogItemHeader.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_1));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_2));
                            tmpListViewitem.SubItems.Add(String.Format("{0}", (byte)logUnion.LogItemHeader.Code_3));

                            if ((Form_Main.devLogManager.SignedFlag & 0x01) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_37.Logvalue_0));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_0));

                            if ((Form_Main.devLogManager.SignedFlag & 0x02) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_37.Logvalue_1));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_1));

                            if ((Form_Main.devLogManager.SignedFlag & 0x04) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_37.Logvalue_2));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_2));

                            if ((Form_Main.devLogManager.SignedFlag & 0x08) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (sbyte)logUnion.LogRec_37.Logvalue_3));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_3));

                            if ((Form_Main.devLogManager.SignedFlag & 0x10) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_37.Logvalue_4));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_4));

                            if ((Form_Main.devLogManager.SignedFlag & 0x20) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_37.Logvalue_5));
                            else tmpListViewitem.SubItems.Add(String.Format("{0}", logUnion.LogRec_37.Logvalue_5));

                            if ((Form_Main.devLogManager.SignedFlag & 0x40) != 0) tmpListViewitem.SubItems.Add(String.Format("{0}", (short)logUnion.LogRec_37.Logvalue_6));
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
            bool IsSame = false;
            UInt16 PrevStartIndex;
            VEXI_DEFS.TLogUnionRec logUnion;

            RxDateTime = DateTime.Now;
            index = index + HeaderLen;

            PrevStartIndex = (UInt16)lv_DevLog.Items.Count;

            Form_Main.devLogManager.TotalCount = dev_REC_LogHeader.TotalLogCount;
            Form_Main.devLogManager.DataType = dev_REC_LogHeader.DataType;
            Form_Main.devLogManager.SignedFlag = dev_REC_LogHeader.signedFlag;

            if (PrevRxIndex >= 0)
            {
                if (dev_REC_LogHeader.LogIndex <= PrevRxIndex)
                {
                    IsSame = true;
                }
                else
                {
                    PrevRxIndex = dev_REC_LogHeader.LogIndex;
                }
            }
            else
            {
                PrevRxIndex = dev_REC_LogHeader.LogIndex;
            }

            if (!IsSame)
            {
                while (true)
                {
                    if (dev_REC_LogHeader.TotalLogCount == 0) break;
                    if (dev_REC_LogHeader.LogCount == 0) break;

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

                    Form_Main.devLogManager.ADDLog(logUnion);


                    if (count == dev_REC_LogHeader.LogCount) break;
                }
            }

            lblProgress.Text = string.Format("{0}/{1}", Form_Main.devLogManager.LogItemCount, Form_Main.devLogManager.TotalCount);
            //DisplayLog_INSERT(dev_REC_LogHeader.LogCount);
            DisplayLog_ADD((ushort)lv_DevLog.Items.Count, dev_REC_LogHeader.LogCount);

            if ((Form_Main.devLogManager.LogItemCount >= Form_Main.devLogManager.TotalCount) || (ISStop) || (dev_REC_LogHeader.LogCount == 0))
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

        private void Load_Title(byte tmpDatatype)
        {
            if (tmpDatatype == 0) return;

            string Title_FILE = Application.StartupPath + "\\CONFIG\\TITLE_EVENT.INI";

            bool TmpIsExist = Global_Class.UTIL_File_exists(Title_FILE);


            switch (tmpDatatype)
            {
                case 30:
                    
                    lv_DevLog.Columns[5].Text = IniControl.ReadString(Title_FILE, "Event_30_TITLE", "Data0", "Data0");
                    lv_DevLog.Columns[6].Text = IniControl.ReadString(Title_FILE, "Event_30_TITLE", "Data1", "Data1");
                    lv_DevLog.Columns[7].Text = IniControl.ReadString(Title_FILE, "Event_30_TITLE", "Data2", "Data2");
                    lv_DevLog.Columns[8].Text = IniControl.ReadString(Title_FILE, "Event_30_TITLE", "Data3", "Data3");
                    lv_DevLog.Columns[9].Text = IniControl.ReadString(Title_FILE, "Event_30_TITLE", "Data4", "Data4");
                    lv_DevLog.Columns[10].Text = IniControl.ReadString(Title_FILE, "Event_30_TITLE", "Data5", "Data5");
                    lv_DevLog.Columns[11].Text = IniControl.ReadString(Title_FILE, "Event_30_TITLE", "Data6", "Data6");
                    lv_DevLog.Columns[12].Text = IniControl.ReadString(Title_FILE, "Event_30_TITLE", "Data7", "Data7");

                    break;
                case 31:
                    lv_DevLog.Columns[5].Text = IniControl.ReadString(Title_FILE, "Event_31_TITLE", "Data0", "Data0");
                    lv_DevLog.Columns[6].Text = IniControl.ReadString(Title_FILE, "Event_31_TITLE", "Data1", "Data1");
                    lv_DevLog.Columns[7].Text = IniControl.ReadString(Title_FILE, "Event_31_TITLE", "Data2", "Data2");
                    lv_DevLog.Columns[8].Text = IniControl.ReadString(Title_FILE, "Event_31_TITLE", "Data3", "Data3");
                    lv_DevLog.Columns[9].Text = IniControl.ReadString(Title_FILE, "Event_31_TITLE", "Data4", "Data4");
                    lv_DevLog.Columns[10].Text = IniControl.ReadString(Title_FILE, "Event_31_TITLE", "Data5", "Data5");
                    break;
                case 32:
                    lv_DevLog.Columns[5].Text = IniControl.ReadString(Title_FILE, "Event_32_TITLE", "Data0", "Data0");
                    lv_DevLog.Columns[6].Text = IniControl.ReadString(Title_FILE, "Event_32_TITLE", "Data1", "Data1");
                    lv_DevLog.Columns[7].Text = IniControl.ReadString(Title_FILE, "Event_32_TITLE", "Data2", "Data2");
                    lv_DevLog.Columns[8].Text = IniControl.ReadString(Title_FILE, "Event_32_TITLE", "Data3", "Data3");
                    lv_DevLog.Columns[9].Text = IniControl.ReadString(Title_FILE, "Event_32_TITLE", "Data4", "Data4");
                    break;
                case 33:
                    lv_DevLog.Columns[5].Text = IniControl.ReadString(Title_FILE, "Event_33_TITLE", "Data0", "Data0");
                    lv_DevLog.Columns[6].Text = IniControl.ReadString(Title_FILE, "Event_33_TITLE", "Data1", "Data1");
                    lv_DevLog.Columns[7].Text = IniControl.ReadString(Title_FILE, "Event_33_TITLE", "Data2", "Data2");
                    lv_DevLog.Columns[8].Text = IniControl.ReadString(Title_FILE, "Event_33_TITLE", "Data3", "Data3");
                    lv_DevLog.Columns[9].Text = IniControl.ReadString(Title_FILE, "Event_33_TITLE", "Data4", "Data4");
                    lv_DevLog.Columns[10].Text = IniControl.ReadString(Title_FILE, "Event_33_TITLE", "Data5", "Data5");
                    break;
                case 34:
                    lv_DevLog.Columns[5].Text = IniControl.ReadString(Title_FILE, "Event_34_TITLE", "Data0", "Data0");
                    lv_DevLog.Columns[6].Text = IniControl.ReadString(Title_FILE, "Event_34_TITLE", "Data1", "Data1");
                    lv_DevLog.Columns[7].Text = IniControl.ReadString(Title_FILE, "Event_34_TITLE", "Data2", "Data2");
                    lv_DevLog.Columns[8].Text = IniControl.ReadString(Title_FILE, "Event_34_TITLE", "Data3", "Data3");
                    lv_DevLog.Columns[9].Text = IniControl.ReadString(Title_FILE, "Event_34_TITLE", "Data4", "Data4");
                    break;
                case 35:
                    lv_DevLog.Columns[5].Text = IniControl.ReadString(Title_FILE, "Event_35_TITLE", "Data0", "Data0");
                    lv_DevLog.Columns[6].Text = IniControl.ReadString(Title_FILE, "Event_35_TITLE", "Data1", "Data1");
                    lv_DevLog.Columns[7].Text = IniControl.ReadString(Title_FILE, "Event_35_TITLE", "Data2", "Data2");
                    lv_DevLog.Columns[8].Text = IniControl.ReadString(Title_FILE, "Event_35_TITLE", "Data3", "Data3");
                    break;
                case 36:
                    lv_DevLog.Columns[5].Text = IniControl.ReadString(Title_FILE, "Event_36_TITLE", "Data0", "Data0");
                    lv_DevLog.Columns[6].Text = IniControl.ReadString(Title_FILE, "Event_36_TITLE", "Data1", "Data1");
                    lv_DevLog.Columns[7].Text = IniControl.ReadString(Title_FILE, "Event_36_TITLE", "Data2", "Data2");
                    break;
                case 37:
                    lv_DevLog.Columns[5].Text = IniControl.ReadString(Title_FILE, "Event_37_TITLE", "Data0", "Data0");
                    lv_DevLog.Columns[6].Text = IniControl.ReadString(Title_FILE, "Event_37_TITLE", "Data1", "Data1");
                    lv_DevLog.Columns[7].Text = IniControl.ReadString(Title_FILE, "Event_37_TITLE", "Data2", "Data2");
                    lv_DevLog.Columns[8].Text = IniControl.ReadString(Title_FILE, "Event_37_TITLE", "Data3", "Data3");
                    lv_DevLog.Columns[9].Text = IniControl.ReadString(Title_FILE, "Event_37_TITLE", "Data4", "Data4");
                    lv_DevLog.Columns[10].Text = IniControl.ReadString(Title_FILE, "Event_37_TITLE", "Data5", "Data5");
                    lv_DevLog.Columns[11].Text = IniControl.ReadString(Title_FILE, "Event_37_TITLE", "Data6", "Data6");

                    break;
            }

            if (!TmpIsExist)
            {
                //StreamWriter wr = new StreamWriter(Title_FILE, false, Encoding.Unicode);
                StreamWriter wr = new StreamWriter(Title_FILE, false, Encoding.UTF8);

                wr.Close();

                IniControl.WriteIni(Title_FILE, "Event_30_TITLE", "Data0", "Data0");
                IniControl.WriteIni(Title_FILE, "Event_30_TITLE", "Data1", "Data1");
                IniControl.WriteIni(Title_FILE, "Event_30_TITLE", "Data2", "Data2");
                IniControl.WriteIni(Title_FILE, "Event_30_TITLE", "Data3", "Data3");
                IniControl.WriteIni(Title_FILE, "Event_30_TITLE", "Data4", "Data4");
                IniControl.WriteIni(Title_FILE, "Event_30_TITLE", "Data5", "Data5");
                IniControl.WriteIni(Title_FILE, "Event_30_TITLE", "Data6", "Data6");
                IniControl.WriteIni(Title_FILE, "Event_30_TITLE", "Data7", "Data7");
                IniControl.WriteIni(Title_FILE, "Event_31_TITLE", "Data0", "Data0");
                IniControl.WriteIni(Title_FILE, "Event_31_TITLE", "Data1", "Data1");
                IniControl.WriteIni(Title_FILE, "Event_31_TITLE", "Data2", "Data2");
                IniControl.WriteIni(Title_FILE, "Event_31_TITLE", "Data3", "Data3");
                IniControl.WriteIni(Title_FILE, "Event_31_TITLE", "Data4", "Data4");
                IniControl.WriteIni(Title_FILE, "Event_31_TITLE", "Data5", "Data5");
                IniControl.WriteIni(Title_FILE, "Event_32_TITLE", "Data0", "Data0");
                IniControl.WriteIni(Title_FILE, "Event_32_TITLE", "Data1", "Data1");
                IniControl.WriteIni(Title_FILE, "Event_32_TITLE", "Data2", "Data2");
                IniControl.WriteIni(Title_FILE, "Event_32_TITLE", "Data3", "Data3");
                IniControl.WriteIni(Title_FILE, "Event_32_TITLE", "Data4", "Data4");
                IniControl.WriteIni(Title_FILE, "Event_33_TITLE", "Data0", "Data0");
                IniControl.WriteIni(Title_FILE, "Event_33_TITLE", "Data1", "Data1");
                IniControl.WriteIni(Title_FILE, "Event_33_TITLE", "Data2", "Data2");
                IniControl.WriteIni(Title_FILE, "Event_33_TITLE", "Data3", "Data3");
                IniControl.WriteIni(Title_FILE, "Event_33_TITLE", "Data4", "Data4");
                IniControl.WriteIni(Title_FILE, "Event_33_TITLE", "Data5", "Data5");
                IniControl.WriteIni(Title_FILE, "Event_34_TITLE", "Data0", "Data0");
                IniControl.WriteIni(Title_FILE, "Event_34_TITLE", "Data1", "Data1");
                IniControl.WriteIni(Title_FILE, "Event_34_TITLE", "Data2", "Data2");
                IniControl.WriteIni(Title_FILE, "Event_34_TITLE", "Data3", "Data3");
                IniControl.WriteIni(Title_FILE, "Event_34_TITLE", "Data4", "Data4");
                IniControl.WriteIni(Title_FILE, "Event_35_TITLE", "Data0", "Data0");
                IniControl.WriteIni(Title_FILE, "Event_35_TITLE", "Data1", "Data1");
                IniControl.WriteIni(Title_FILE, "Event_35_TITLE", "Data2", "Data2");
                IniControl.WriteIni(Title_FILE, "Event_35_TITLE", "Data3", "Data3");
                IniControl.WriteIni(Title_FILE, "Event_36_TITLE", "Data0", "Data0");
                IniControl.WriteIni(Title_FILE, "Event_36_TITLE", "Data1", "Data1");
                IniControl.WriteIni(Title_FILE, "Event_36_TITLE", "Data2", "Data2");
                IniControl.WriteIni(Title_FILE, "Event_37_TITLE", "Data0", "Data0");
                IniControl.WriteIni(Title_FILE, "Event_37_TITLE", "Data1", "Data1");
                IniControl.WriteIni(Title_FILE, "Event_37_TITLE", "Data2", "Data2");
                IniControl.WriteIni(Title_FILE, "Event_37_TITLE", "Data3", "Data3");
                IniControl.WriteIni(Title_FILE, "Event_37_TITLE", "Data4", "Data4");
                IniControl.WriteIni(Title_FILE, "Event_37_TITLE", "Data5", "Data5");
                IniControl.WriteIni(Title_FILE, "Event_37_TITLE", "Data6", "Data6");
            }
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

        private void Btn_LoadTitle_Click(object sender, EventArgs e)
        {
            Load_Title(MakeList_DataType);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace VEXI
{
    public partial class Form_DevDebugLog : Form
    {

        public Form_Main form_Main;
        //private static DEVLogManager devLogManager;
        
        private static VEXI_DEFS.TDEV_REC_DebugReq dev_Rec_LogReq;
        private static bool ISStop = false;
        private byte MakeList_DataType = 0;
        private Int32 PrevRxIndex = -1;
        private DateTime RxDateTime;

        private StringBuilder Reserved_Byte = new StringBuilder();

        public Form_DevDebugLog()
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

            MakeList_DataType = 0;
        }


        private void Form_DevLog_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form_Main.devLogManager.ClearLogList();

        }

        ~Form_DevDebugLog()
        {
            
        }

        private void btn_Delete_AlarmLog_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "장치의 저장된 개발자로그를 모두 삭제하시겠습니까?"))
            {
                form_Main.Do_Ctrl_Cmd_withOnebyte(ConstClass.CMD1_01, ConstClass.CMD2_37, 0);
            }
        }

        private void btn_Log_Req_Click(object sender, EventArgs e)
        {
            ISStop = false;
            PrevRxIndex = -1;

            listBox1.Items.Clear();

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
                StringBuilder sb = new StringBuilder();
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
            
            saveFileDialog1.Filter = "*.DEBUGDATA|*.DebugData";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Form_Main.devLogManager.SaveDebuugToFile(saveFileDialog1.FileName);
            }
        }

        private void btn_Log_FileLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.DEBUGDATA|*.DebugData";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Form_Main.devLogManager.ClearLogList();
                lv_DevLog.Items.Clear();
                lblProgress.Text = "0/0";

                Form_Main.devLogManager.LoadDebugFromFile(openFileDialog1.FileName);
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
                case 1:
                    for (int i=1; i<73; i++)
                    {
                        lv_DevLog.Columns.Add("Debug_" + i.ToString(), 60, HorizontalAlignment.Center);
                    }
                    break;
                case 2:
                    for (int i = 1; i < 17; i++)
                    {
                        if (i == 15)  lv_DevLog.Columns.Add("Debug_" + i.ToString(), 300, HorizontalAlignment.Center);
                        else if (i == 16) lv_DevLog.Columns.Add("Debug_" + i.ToString(), 160, HorizontalAlignment.Center);
                        else lv_DevLog.Columns.Add("Debug_" + i.ToString(), 80, HorizontalAlignment.Center);
                    }
                    break;
                case 3:
                    for (int i = 1; i < 73; i++)
                    {
                        lv_DevLog.Columns.Add("Debug_" + i.ToString(), 60, HorizontalAlignment.Center);
                    }
                    break;
                case 4:
                    for (int i = 1; i < 73; i++)
                    {
                        lv_DevLog.Columns.Add("Debug_" + i.ToString(), 60, HorizontalAlignment.Center);
                    }
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
                case 1:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.DebugItem_1.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_1.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_2.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_3.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_4.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_5.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_6.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_7.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_8.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_9.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_10.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_11.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_12.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_13.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_14.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_15.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_16.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_17.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_18.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_19.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_20.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_21.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_22.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_23.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_24.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_25.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_26.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_27.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_28.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_29.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_30.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_31.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_32.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_33.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_34.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_35.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_36.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_37.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_38.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_39.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_40.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_41.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_42.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_43.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_44.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_45.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_46.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_47.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_48.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_49.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_50.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_51.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_52.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_53.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_54.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_55.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_56.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_57.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_58.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_59.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_60.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_61.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_62.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_63.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_64.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_65.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_66.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_67.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_68.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_69.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_70.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_71.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_72.ToString());
                        }
                    }
                    break;
                case 2:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.DebugItem_2.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));

                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_1.ToString());

                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_2.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_3.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_4.ToString());

                            //tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_5.ToString());
                            if (logUnion.DebugItem_2.Debug_5 == -1)
                            {
                                tmpListViewitem.SubItems.Add("No Data");
                            } else
                            {
                                tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_5.ToString());
                            }

                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_6.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_7.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_8.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_9.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_10.ToString());

                            //tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_11.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_11.ToString());

                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_12.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_13.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_14.ToString());

                            //tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_15.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_2.Debug_15.ToString());

                            Reserved_Byte.Clear();
                            for (int k = 0; k < 8; k++)
                            {
                                Reserved_Byte.Append(string.Format("{0:X2} ", logUnion.DebugItem_2.Debug_16[k]));
                            }

                            tmpListViewitem.SubItems.Add(Reserved_Byte.ToString());
                        }
                    }
                    break;
                case 3:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.DebugItem_1.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_1.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_2.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_3.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_4.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_5.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_6.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_7.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_8.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_9.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_10.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_11.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_12.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_13.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_14.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_15.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_16.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_17.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_18.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_19.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_20.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_21.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_22.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_23.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_24.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_25.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_26.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_27.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_28.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_29.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_30.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_31.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_32.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_33.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_34.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_35.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_36.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_37.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_38.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_39.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_40.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_41.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_42.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_43.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_44.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_45.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_46.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_47.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_48.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_49.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_50.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_51.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_52.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_53.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_54.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_55.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_56.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_57.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_58.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_59.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_60.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_61.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_62.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_63.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_64.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_65.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_66.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_67.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_68.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_69.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_70.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_71.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_72.ToString());

                        }
                    }
                    break;
                case 4:
                    for (int i = From; i < From + Count; i++)
                    {

                        if (Form_Main.devLogManager.LogItem(i, ref logUnion))
                        {
                            tmpListViewitem = lv_DevLog.Items.Add(string.Format("{0}", i + 1));
                            DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(logUnion.DebugItem_1.LogTime);
                            tmpListViewitem.SubItems.Add(String.Format("{0}", PCtime));
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_1.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_2.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_3.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_4.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_5.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_6.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_7.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_8.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_9.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_10.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_11.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_12.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_13.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_14.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_15.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_16.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_17.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_18.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_19.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_20.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_21.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_22.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_23.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_24.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_25.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_26.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_27.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_28.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_29.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_30.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_31.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_32.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_33.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_34.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_35.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_36.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_37.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_38.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_39.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_40.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_41.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_42.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_43.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_44.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_45.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_46.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_47.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_48.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_49.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_50.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_51.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_52.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_53.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_54.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_55.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_56.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_57.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_58.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_59.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_60.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_61.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_62.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_63.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_64.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_65.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_66.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_67.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_68.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_69.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_70.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_71.ToString());
                            tmpListViewitem.SubItems.Add(logUnion.DebugItem_1.Debug_72.ToString());

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

            if (dev_REC_LogHeader.DataType > 4)
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

                    switch (dev_REC_LogHeader.DataType)
                    {
                        case 1:
                            logUnion.DebugItem_1 = (VEXI_DEFS.TDebugLogItem_1)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TDebugLogItem_1), index, Marshal.SizeOf(typeof(VEXI_DEFS.TDebugLogItem_1)));
                            index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TDebugLogItem_1));
                            break;
                        case 2:
                            logUnion.DebugItem_2 = (VEXI_DEFS.TDebugLogItem_2)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TDebugLogItem_2), index, Marshal.SizeOf(typeof(VEXI_DEFS.TDebugLogItem_2)));
                            index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TDebugLogItem_2));
                            break;
                        case 3:
                            logUnion.DebugItem_1 = (VEXI_DEFS.TDebugLogItem_1)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TDebugLogItem_1), index, Marshal.SizeOf(typeof(VEXI_DEFS.TDebugLogItem_1)));
                            index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TDebugLogItem_1));
                            break;
                        case 4:
                            logUnion.DebugItem_1 = (VEXI_DEFS.TDebugLogItem_1)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TDebugLogItem_1), index, Marshal.SizeOf(typeof(VEXI_DEFS.TDebugLogItem_1)));
                            index = index + Marshal.SizeOf(typeof(VEXI_DEFS.TDebugLogItem_1));
                            break;
                    }
                    count++;

                    Form_Main.devLogManager.ADDLog(logUnion);


                    if (count == dev_REC_LogHeader.LogCount) break;
                }
            }

            lblProgress.Text = string.Format("{0}/{1}", Form_Main.devLogManager.LogItemCount, Form_Main.devLogManager.TotalCount);
            //DisplayLog_INSERT(dev_REC_LogHeader.LogCount);
            //DisplayLog_ADD((ushort)lv_DevLog.Items.Count, dev_REC_LogHeader.LogCount);

            listBox1.Items.Add("RX " + DateTime.Now.ToString() + " Index : " + dev_REC_LogHeader.LogIndex.ToString());
            if ((Form_Main.devLogManager.LogItemCount >= Form_Main.devLogManager.TotalCount) || (ISStop) || (dev_REC_LogHeader.LogCount == 0))
            {
                DisplayLog_ADD(0, Form_Main.devLogManager.TotalCount);
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
                dev_Rec_LogReq.Reserved = (byte)Global_Class.UTIL_StrToIntDef(edLogkind.Text, 0); ;

                if (rbAll.Checked)
                {
                    dev_Rec_LogReq.ReqKind = 0;
                }
                else
                {
                    dev_Rec_LogReq.ReqKind = 2;
                    dev_Rec_LogReq.WantLogCount = (UInt16)Global_Class.UTIL_StrToIntDef(edWantCount.Text, 100);
                }
            }

            dev_Rec_LogReq.LogIndex = TmpLogindex;

            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_36, dev_Rec_LogReq);
            RxDateTime = DateTime.Now;

            listBox1.Items.Add("TX " + DateTime.Now.ToString() + " Index : " +  dev_Rec_LogReq.LogIndex.ToString());
            
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


        private void Load_Title(byte tmpDatatype)
        {
            if (tmpDatatype == 0) return;
            
            string Title_FILE = Application.StartupPath + "\\CONFIG\\TITLE_DEBUG.INI";

            bool TmpIsExist = Global_Class.UTIL_File_exists(Title_FILE);


            switch (tmpDatatype)
            {
                case 1:
                    for (int i = 1; i < 73; i++)
                    {
                        lv_DevLog.Columns[i+1].Text = IniControl.ReadString(Title_FILE, "RTV_DEBUG_TITLE", "Debug_" + i.ToString(), "Debug_" + i.ToString());
                    }
                    break;
                case 2:
                    for (int i = 1; i < 17; i++)
                    {
                        lv_DevLog.Columns[i + 1].Text = IniControl.ReadString(Title_FILE, "RTV_DEBUG_TITLE_2", "Debug_" + i.ToString(), "Debug_" + i.ToString());
                    }
                    break;
                case 3:
                    for (int i = 1; i < 73; i++)
                    {
                        lv_DevLog.Columns[i + 1].Text = IniControl.ReadString(Title_FILE, "SRM_DEBUG_TITLE", "Debug_" + i.ToString(), "Debug_" + i.ToString());
                    }
                    break;
                case 4:
                    for (int i = 1; i < 73; i++)
                    {
                        lv_DevLog.Columns[i + 1].Text = IniControl.ReadString(Title_FILE, "EMS_DEBUG_TITLE", "Debug_" + i.ToString(), "Debug_" + i.ToString());
                    }
                    break;
            }

            if (!TmpIsExist)
            {
                //StreamWriter wr = new StreamWriter(Title_FILE, false, Encoding.Unicode);
                StreamWriter wr = new StreamWriter(Title_FILE, false, Encoding.UTF8);

                wr.Close();

                for (int i = 1; i < 73; i++)
                {
                    IniControl.WriteIni(Title_FILE, "RTV_DEBUG_TITLE", "Debug_" + i.ToString(), "Debug_" + i.ToString());
                }

                for (int i = 1; i < 17; i++)
                {
                    IniControl.WriteIni(Title_FILE, "RTV_DEBUG_TITLE_2", "Debug_" + i.ToString(), "Debug_" + i.ToString());
                }


                for (int i = 1; i < 73; i++)
                {
                    IniControl.WriteIni(Title_FILE, "SRM_DEBUG_TITLE", "Debug_" + i.ToString(), "Debug_" + i.ToString());
                }

                for (int i = 1; i < 73; i++)
                {
                    IniControl.WriteIni(Title_FILE, "EMS_DEBUG_TITLE", "Debug_" + i.ToString(), "Debug_" + i.ToString());
                }

            }
        }

        private void Btn_LoadTitle_Click(object sender, EventArgs e)
        {
            Load_Title(MakeList_DataType);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }




            private unsafe void button1_Click_2(object sender, EventArgs e)
        {
            VEXI_DEFS.TTESTRecord myRecord;
            myRecord.a = 0;// double.NegativeInfinity;
            myRecord.b = 0;//double.PositiveInfinity;
            myRecord.c = 0;//double.NaN;


            byte[] data = new byte[24];

            data[0] = 0x00;
            data[1] = 0x00;
            data[2] = 0x00;
            data[3] = 0x00;
            data[4] = 0x00;
            data[5] = 0x00;
            data[6] = 0xF0;
            data[7] = 0xFF;

            data[8] = 0x00;
            data[9] = 0x00;
            data[10] = 0x00;
            data[11] = 0x00;
            data[12] = 0x00;
            data[13] = 0x00;
            data[14] = 0xF0;
            data[15] = 0x7F;

            data[16] = 0x00;
            data[17] = 0x00;
            data[18] = 0x00;
            data[19] = 0x00;
            data[20] = 0x00;
            data[21] = 0x00;
            data[22] = 0xF8;
            data[23] = 0xFF;

            myRecord = (VEXI_DEFS.TTESTRecord)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TTESTRecord));
            this.Text = myRecord.a.ToString() + " , " +
myRecord.b.ToString() + " , " +
myRecord.c.ToString() + " , ";

            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, 0xFF, myRecord);

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MakeListView(2);
        }

        private void label55_DoubleClick(object sender, EventArgs e)
        {
            listBox1.Visible = !(listBox1.Visible);
        }
    }
}


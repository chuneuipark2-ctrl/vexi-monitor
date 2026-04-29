using System;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VEXI
{
    public partial class Form_ComDataDP : Form
    {
        const UInt16 MAX_DP_COUNT = 3000;
        public Form_Main form_Main;
        byte[] RXPacketBuffer = { };
        byte[] TXPacketBuffer = { };
        static StringBuilder rx_packet = new StringBuilder();
        static StringBuilder tx_packet = new StringBuilder();

        public Form_ComDataDP()
        {
            InitializeComponent();
        }

        private void Form_ComDataDP_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }

            if (cbLoggingInterval.SelectedIndex < 0) cbLoggingInterval.SelectedIndex = 1;
        }

        public void Display_RxData(bool CRCOK, TPacketClass PaketObj, byte RevCRC1, byte RevCRC2, byte RevETX)
        {
            if (cbStopRefresh.Checked) return;

            ushort Len = PaketObj.GetTotalSize();
            Array.Resize(ref RXPacketBuffer, Len);
            RXPacketBuffer = PaketObj.GetTotalBytes();

            Display_RxData(CRCOK, RXPacketBuffer, RevCRC1, RevCRC2, RevETX);
        }

        public void Display_RxData(bool CRCOK, byte[] PaketBytes, byte RevCRC1, byte RevCRC2, byte RevETX)
        {
            if (cbStopRefresh.Checked) return;
            if (!listBox1.IsHandleCreated) return;
            if (cbNoPollingData.Checked)
            {
                if ((PaketBytes[11] == ConstClass.CMD1_80) || (PaketBytes[11] == ConstClass.CMD1_81))
                {
                    if ((PaketBytes[14] == ConstClass.CMD2_30) ||
                        (PaketBytes[14] == ConstClass.CMD2_32) ||
                        (PaketBytes[14] == ConstClass.CMD2_3D) ||
                        (PaketBytes[14] == ConstClass.CMD2_10) ||
                        (PaketBytes[14] == ConstClass.CMD2_12)) return;
                }

            }

            bool InOnce = false;
            ushort Len = (ushort)PaketBytes.Length;

            rx_packet.Clear();

            for (int i = 0; i < Len; i++)
            {
                rx_packet.Append(string.Format("{0:X2} ", PaketBytes[i]));


                if (((i != 0) && (i % 32 == 31)) || (i == (Len - 1)))
                {
                    //디버그 모드에서는 컨트롤이 자신이 만든 스레드가 아닌 다른 스레드에 의해서 호출되면 에러가 나기때문에invoke
                    if (listBox1.InvokeRequired)
                    {
                        if (!listBox1.IsHandleCreated) return;
                        if (CRCOK)
                        {
                            if (!InOnce)
                            {
                                listBox1.Invoke(new MethodInvoker(delegate { listBox1.Items.Add("");
                                                                             listBox1.Items.Add(String.Format("{0:MM/dd HH:mm:ss.fff} {1,-19}", DateTime.Now, "[RX]"));
                                                                             listBox1.Items.Add(rx_packet.ToString());}));
                                InOnce = true;
                            }
                            else
                            {
                                listBox1.Invoke(new MethodInvoker(delegate { listBox1.Items.Add(rx_packet.ToString()); }));
                            }
                        }
                        else
                        {
                            if (!InOnce)
                            {
                                listBox1.Invoke(new MethodInvoker(delegate {
                                    listBox1.Items.Add("");
                                    listBox1.Items.Add(String.Format("{0:MM/dd HH:mm:ss.fff} {1,-19}", DateTime.Now, "[RX CRC ERR]"));
                                    listBox1.Items.Add(rx_packet.ToString());
                                }));
                                InOnce = true;
                            }
                            else
                            {
                                listBox1.Invoke(new MethodInvoker(delegate { listBox1.Items.Add(rx_packet.ToString()); }));
                            }
                        }

                    }
                    else
                    {
                        if (listBox1.Items.Count > MAX_DP_COUNT)
                        {
                            listBox1.Items.Clear();
                        }

                        if (CRCOK)
                        {
                            if (!InOnce)
                            {
                                listBox1.Items.Add("");
                                listBox1.Items.Add(String.Format("{0:MM/dd HH:mm:ss.fff} {1,-19}", DateTime.Now, "[RX]"));
                                listBox1.Items.Add(rx_packet.ToString());
                                InOnce = true;
                            }
                            else
                            {
                                listBox1.Items.Add(rx_packet.ToString());
                            }
                        }
                        else
                        {
                            if (!InOnce)
                            {
                                listBox1.Items.Add("");
                                listBox1.Items.Add(String.Format("{0:MM/dd HH:mm:ss.fff} {1,-19} {2:X2} {3:X2} {4:X2} ", DateTime.Now, "[RX CRC ERR]", RevCRC1, RevCRC2, RevETX));
                                listBox1.Items.Add(rx_packet.ToString());
                                InOnce = true;
                            }
                            else
                            {
                                listBox1.Items.Add(rx_packet.ToString());
                            }
                        }
                    }
                    rx_packet.Clear();
                }
            }
            if (listBox1.Items.Count > 0) listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        public void Display_TxData(TPacketClass PaketObj)
        {
            if (cbStopRefresh.Checked) return;

            ushort Len = PaketObj.GetTotalSize();
            Array.Resize(ref TXPacketBuffer, Len);
            TXPacketBuffer = PaketObj.GetTotalBytes();

            Display_TxData(TXPacketBuffer);
        }

        public void Display_TxData(byte[] PaketBytes)
        {
            if (cbStopRefresh.Checked) return;
            if (!listBox1.IsHandleCreated) return;
            if (cbNoPollingData.Checked)
            {
                if ((PaketBytes[11] == ConstClass.CMD1_00) || (PaketBytes[11] == ConstClass.CMD1_01))
                {
                    if ((PaketBytes[14] == ConstClass.CMD2_30) ||
                        (PaketBytes[14] == ConstClass.CMD2_32) ||
                        (PaketBytes[14] == ConstClass.CMD2_3D) ||
                        (PaketBytes[14] == ConstClass.CMD2_10) ||
                        (PaketBytes[14] == ConstClass.CMD2_12)) return;
                }

            }
            bool InOnce = false;
            ushort Len = (ushort)PaketBytes.Length;

            tx_packet.Clear();

            for (int i = 0; i < Len; i++)
            {
                tx_packet.Append(string.Format("{0:X2} ", PaketBytes[i]));

                if (((i != 0) && (i % 32 == 31)) || (i == (Len - 1)))
                {
                    //디버그 모드에서는 컨트롤이 자신이 만든 스레드가 아닌 다른 스레드에 의해서 호출되면 에러가 나서 아래처럼 해결
                    if (listBox1.InvokeRequired)
                    {
                        if (!listBox1.IsHandleCreated) return;
                        if (!InOnce)
                        {
                            
                            listBox1.Invoke(new MethodInvoker(delegate {
                                listBox1.Items.Add(""); 
                                listBox1.Items.Add(String.Format("{0:MM/dd HH:mm:ss.fff} {1,-19}", DateTime.Now, "[TX]"));
                                listBox1.Items.Add(tx_packet.ToString());}));
                            InOnce = true;
                        }
                        else
                        {
                            listBox1.Invoke(new MethodInvoker(delegate { listBox1.Items.Add(tx_packet.ToString()); }));
                        }
                    }
                    else
                    {
                        if (listBox1.Items.Count > MAX_DP_COUNT)
                        {
                            listBox1.Items.Clear();
                        }

                        if (!InOnce)
                        {
                            listBox1.Items.Add("");
                            listBox1.Items.Add(String.Format("{0:MM/dd HH:mm:ss.fff} {1,-19}", DateTime.Now, "[TX]"));
                            listBox1.Items.Add(tx_packet.ToString());
                            InOnce = true;
                        }
                        else
                        {
                            listBox1.Items.Add(tx_packet.ToString());
                        }
                    }
                    tx_packet.Clear();
                }

            }
            if (listBox1.Items.Count > 0) listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        public void Display_Debug(string catpionStr, string DebugStr)
        {
            if (!listBox1.IsHandleCreated) return;
            if (listBox1.InvokeRequired)
            {
                listBox1.Invoke(new MethodInvoker(delegate
                {
                    listBox1.Items.Add("");
                    listBox1.Items.Add(String.Format("{0:MM/dd HH:mm:ss.fff} {1} {2}", DateTime.Now, catpionStr, DebugStr));
                    if (listBox1.Items.Count > 0) listBox1.SelectedIndex = listBox1.Items.Count - 1;
                }));
            } else
            {
                listBox1.Items.Add("");
                listBox1.Items.Add(String.Format("{0:MM/dd HH:mm:ss.fff} {1} {2}", DateTime.Now, catpionStr, DebugStr));
                if (listBox1.Items.Count > 0) listBox1.SelectedIndex = listBox1.Items.Count - 1;

            }
        }
        private void SaveListBoxToFile()
        {
            if (listBox1.Items.Count == 0) return;

            int listCount = listBox1.Items.Count;
            string delimeter = Environment.NewLine;
            using (TextWriter textExport = new StreamWriter(Application.StartupPath + "\\CommData" + string.Format("{0:yyyyMMdd_HHmmss}.txt", DateTime.Now)))
            {
                for (int i = 0; i < listCount; i++)
                {
                    try
                    {
                        textExport.Write(listBox1.Items[i].ToString() + delimeter);
                    }
                    catch
                    {
                        textExport.Write(delimeter);
                    }
                }
                textExport.Flush();
                textExport.Close();
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            SaveListBoxToFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            form_Main.COMMDataManager.ISPolingStop = checkBox1.Checked;
        }

        private void rbLogging_Start_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLogging_Stop.Checked)
            {
                if ((form_Main.COMMDataManager.LoggingMode == 1) || (form_Main.COMMDataManager.LoggingMode == 2))
                {
                    form_Main.COMMDataManager.LoggingMode = 0;
                    form_Main.COMMDataManager.SaveCommDataLogging(true, form_Main.COMMDataManager.SelectDestDevType);
                } else if (form_Main.COMMDataManager.LoggingMode == 3)
                {
                    form_Main.COMMDataManager.LoggingMode = 0;
                    form_Main.COMMDataManager.SaveSpeedDataLogging(true, form_Main.COMMDataManager.SelectDestDevType);
                }
                else
                {
                    form_Main.COMMDataManager.LoggingMode = 0;
                }
                
                gbComDataLoggingOption.Enabled = true;
            } else if (rbComDataLogging_Start.Checked)
            {
                if (form_Main.COMMDataManager.LoggingMode == 3)
                {
                    if (cbLogging_DIOChange.Checked) form_Main.COMMDataManager.LoggingMode = 2;
                    else form_Main.COMMDataManager.LoggingMode = 1;
                    form_Main.COMMDataManager.SaveSpeedDataLogging(true, form_Main.COMMDataManager.SelectDestDevType);
                } else
                {
                    if (cbLogging_DIOChange.Checked) form_Main.COMMDataManager.LoggingMode = 2;
                    else form_Main.COMMDataManager.LoggingMode = 1;
                }

                switch (cbLoggingInterval.SelectedIndex)
                {
                    case 0:
                        form_Main.COMMDataManager.Logginginterval = 500;
                        break;
                    case 1:
                        form_Main.COMMDataManager.Logginginterval = 1000;
                        break;
                    case 2:
                        form_Main.COMMDataManager.Logginginterval = 2000;
                        break;
                    case 3:
                        form_Main.COMMDataManager.Logginginterval = 5000;
                        break;
                    default:
                        form_Main.COMMDataManager.Logginginterval = 500;
                        break;
                }
                gbComDataLoggingOption.Enabled = true;
            } else if (rbSpeedLogging_Start.Checked)
            {
                if ((form_Main.COMMDataManager.LoggingMode == 1) || (form_Main.COMMDataManager.LoggingMode == 2))
                {
                    form_Main.COMMDataManager.LoggingMode = 3;
                    form_Main.COMMDataManager.SaveCommDataLogging(true, form_Main.COMMDataManager.SelectDestDevType);
                } else
                {
                    form_Main.COMMDataManager.LoggingMode = 3;
                }
                    
                form_Main.COMMDataManager.Logginginterval = 1000;
                gbComDataLoggingOption.Enabled = false;
            }
        }

        private void cbLoggingInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form_Main.COMMDataManager.LoggingMode > 0)
            {
                if (rbComDataLogging_Start.Checked)
                {
                    switch (cbLoggingInterval.SelectedIndex)
                    {
                        case 0:
                            form_Main.COMMDataManager.Logginginterval = 500;
                            break;
                        case 1:
                            form_Main.COMMDataManager.Logginginterval = 1000;
                            break;
                        case 2:
                            form_Main.COMMDataManager.Logginginterval = 2000;
                            break;
                        case 3:
                            form_Main.COMMDataManager.Logginginterval = 5000;
                            break;
                        default:
                            form_Main.COMMDataManager.Logginginterval = 500;
                            break;
                    }
                }
            }
        }

        private void cbLogging_DIOChange_Click(object sender, EventArgs e)
        {
            if (form_Main.COMMDataManager.LoggingMode > 0)
            {
                if (rbComDataLogging_Start.Checked)
                {
                    if (cbLogging_DIOChange.Checked) form_Main.COMMDataManager.LoggingMode = 2;
                    else form_Main.COMMDataManager.LoggingMode = 1;
                }
            }
        }
    }
}

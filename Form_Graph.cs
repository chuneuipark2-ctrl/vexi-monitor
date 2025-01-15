using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace VEXI
{
    public partial class Form_Graph : Form
    {
        public Form_Main form_Main;
        private byte LastReqType = 0;
        private static VEXI_DEFS.TDEV_REC_Graph dev_REC_Graph;
        private static VEXI_DEFS.TDEV_REC_GraphReq dev_REQ_Graph;
        private static Label[] lbl_GraphValue;
        private static CheckBox[] cb_Graph;
        private sbyte LastReqIndex = -1;
        public DateTime LastTxTime;
        public byte RetryCount;

        public Form_Graph()
        {
            InitializeComponent();

            lbl_GraphValue = new Label[]{lbl_Graph1, lbl_Graph2, lbl_Graph3, lbl_Graph4,
                                         lbl_Graph5, lbl_Graph6, lbl_Graph7, lbl_Graph8,
                                         lbl_Graph9, lbl_Graph10, lbl_Graph11, lbl_Graph12 };
            cb_Graph = new CheckBox[] {cb_Graph1, cb_Graph2, cb_Graph3, cb_Graph4,
                                       cb_Graph5,cb_Graph6,cb_Graph7,cb_Graph8,
                                       cb_Graph9,cb_Graph10,cb_Graph11,cb_Graph12};
        }

        private void Form_Graph_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }

            cbGraphSave_interval.SelectedIndex = 1;

            Load_Title();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            var chart = sender as Chart;

            if (chart != null)

            {
                HitTestResult hit = chart.HitTest(e.X, e.Y);

                if (hit.ChartArea != null)
                {
                    try
                    {
                        double xValue = hit.ChartArea.AxisX.PixelPositionToValue(e.Location.X);

                        int xIntValue = (int)(xValue / 1);

                        if (xIntValue >= 0)
                        {
                            lbl_GraphX.Text = xIntValue.ToString();
                            for (byte i = 0; i < 12; i++)
                            {
                                if (cb_Graph[i].Checked)
                                {
                                    if (xIntValue < chart.Series[i].Points.Count)
                                    {

                                        double yIntValue = chart.Series[i].Points[xIntValue].YValues[0];

                                        lbl_GraphValue[i].Text = yIntValue.ToString();
                                    }
                                    else
                                    {
                                        lbl_GraphValue[i].Text = "---";
                                    }
                                }
                                else
                                {
                                    lbl_GraphValue[i].Text = "---";
                                }
                            }

                        }
                    } catch
                    {
                        Text = e.Location.X.ToString();
                    }
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (byte i=0; i < 12; i++)
            {
                chart1.Series[i].Points.Clear();
            }
            
            RetryCount = 0;
            LastReqIndex = -1;
            LastReqIndex = GetNextReqIndex();


            if (LastReqIndex != -1)
            {
                Chagne_Enable(false);
                ReqGraph((byte)LastReqIndex);
                timer1.Enabled = true;
            } else
            {
                Chagne_Enable(true);
                timer1.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan ts;
            if (!btnLoad_Graph.Enabled)
            {
                if (DateTime.Compare(LastTxTime, DateTime.Now) > 0)
                {
                    LastTxTime = DateTime.Now;
                }
                ts = DateTime.Now - LastTxTime;

                if (ts.TotalMilliseconds >= 2000)
                {
                    RetryCount++;
                    if (RetryCount > 3)
                    {
                        LastReqIndex = GetNextReqIndex();
                        if (LastReqIndex != -1)
                        {
                            RetryCount = 0;
                            ReqGraph((byte)LastReqIndex);
                        }
                        else
                        {
                            Chagne_Enable(true);
                            timer1.Enabled = false;
                        }
                    } else
                    {
                        ReqGraph((byte)LastReqIndex);
                    }
                }
            }
        }

        private void cb_Graph1_CheckedChanged(object sender, EventArgs e)
        {
            for (byte i =0; i<12; i++)
            {
                if (cb_Graph[i].Checked)
                {
                    chart1.Series[i].Enabled = true;
                }
                else
                {
                    chart1.Series[i].Enabled = false;
                }
            }
            chart1.ResetAutoValues();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            for (byte i = 0; i < 12; i++)
            {
                cb_Graph[i].Checked = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (byte i = 0; i < 12; i++)
            {
                cb_Graph[i].Checked = false;
            }
        }

        private void Btn_Y_change_Click(object sender, EventArgs e)
        {
            for (byte i = 0; i < 12; i++)
            {
               // chart1.Series[i].
            }
        }

        private void Chagne_Enable(bool TmpIsEnable)
        {
            btn_AllChecked.Enabled = TmpIsEnable;
            btn_AllUnChecked.Enabled = TmpIsEnable;
            btnLoad_Graph.Enabled = TmpIsEnable;
            for (byte i = 0; i < 12; i++)
            {
                cb_Graph[i].Enabled = TmpIsEnable;
            }

            btnLoad_GraphSaveSetting.Enabled = TmpIsEnable;
            btnSet_GraphSaveSetting.Enabled = TmpIsEnable;
        }
        private void ReqGraph(byte index)
        {
            LastTxTime = DateTime.Now;
            LastReqType = 0;
            dev_REQ_Graph.ReqCommand_Type = LastReqType;
            dev_REQ_Graph.Graph_Type = index;
            dev_REQ_Graph.Graph_Reserved = 0x00;

            dev_REQ_Graph.Save_Type = 0;
            dev_REQ_Graph.Save_interval = 0;
            dev_REQ_Graph.Save_Position = 0;

            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_15, dev_REQ_Graph);
        }

        private void SetGraphSaveSetting()
        {
            LastReqType = 1;
            dev_REQ_Graph.ReqCommand_Type = LastReqType;

            dev_REQ_Graph.Graph_Type = 0x00;
            dev_REQ_Graph.Graph_Reserved = 0x00;

            if (rbGraphSaveType_0.Checked) dev_REQ_Graph.Save_Type = 0;
            else if (rbGraphSaveType_1.Checked) dev_REQ_Graph.Save_Type = 1;
            else if (rbGraphSaveType_2.Checked) dev_REQ_Graph.Save_Type = 2;

            if (cbGraphSave_interval.SelectedIndex >= 0)
            {
                dev_REQ_Graph.Save_interval = (byte)(cbGraphSave_interval.SelectedIndex + 1);
            } else
            {
                    dev_REQ_Graph.Save_interval = 2;
            }
            dev_REQ_Graph.Save_Position = Global_Class.UTIL_StrToUInt32Def(edGraphSave_Position.Text, 0);

            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_15, dev_REQ_Graph);
        }

        private void ReqGraphSaveSetting()
        {
            LastReqType = 2;
            dev_REQ_Graph.ReqCommand_Type = LastReqType;

            dev_REQ_Graph.Graph_Type = 0x00;
            dev_REQ_Graph.Graph_Reserved = 0x00;

            dev_REQ_Graph.Save_Type = 0;
            dev_REQ_Graph.Save_interval = 0;
            dev_REQ_Graph.Save_Position = 0;

            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_15, dev_REQ_Graph);
        }

        private sbyte GetNextReqIndex()
        {
            sbyte Tmpindex = LastReqIndex;
            if (Tmpindex == 23) return (-1);

            if (Tmpindex < 0)
            {
                for (byte i = 0; i < 12; i++)
                {
                    if (cb_Graph[i].Checked)
                    {
                        return ((sbyte)(i * 2));
                    }
                }
            }
            else
            {

                if (Tmpindex % 2 == 0)
                {
                    return ((sbyte)(Tmpindex + 1));
                }
                else
                {
                    for (byte i = 0; i < 12; i++)
                    {
                        if (cb_Graph[i].Checked)
                        {
                            if ((Tmpindex / 2) < i) return ((sbyte)(i * 2));
                        }
                    }
                }
            }

            
            return (-1);
        }

        public unsafe void Rxprocess_Graph(byte[] datas)
        {
            
            dev_REC_Graph = (VEXI_DEFS.TDEV_REC_Graph)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TDEV_REC_Graph));

            rbGraphSaveType_0.Checked = (dev_REC_Graph.Save_Type == 0);
            rbGraphSaveType_1.Checked = (dev_REC_Graph.Save_Type == 1);
            rbGraphSaveType_2.Checked = (dev_REC_Graph.Save_Type == 2);
            cbGraphSave_interval.SelectedIndex = dev_REC_Graph.Save_interval -1;
            edGraphSave_Position.Text = string.Format("{0}", dev_REC_Graph.Save_Position);


            if (LastReqType == 0)
            {
                fixed (VEXI_DEFS.TDEV_REC_Graph* Ptr_1 = &dev_REC_Graph)
                {
                    byte type = Ptr_1->Graph_Type;
                    byte graphtype = (byte)(type / 2);
                    if (graphtype > 12) return;

                    

                    if (type % 2 == 0)
                    {
                        chart1.Series[graphtype].Points.Clear();
                        for (int i = 0; i < 350; i++)
                        {
                            //Ptr_1->Data[i] = type * 5 + 5;
                            chart1.Series[graphtype].Points.AddY(Ptr_1->Data[i]);
                            //chart1.Series[graphtype].Points.AddY(type * 10 + 5);
                        }
                    } else
                    {
                        for (int i = 350; i < 700; i++)
                        {
                            //Ptr_1->Data[i - 350] = type * 5 + 8;
                            chart1.Series[graphtype].Points.AddY(Ptr_1->Data[i-350]);
                            //chart1.Series[graphtype].Points.AddY(type * 10 + 8);
                        }
                    }
                }

                
                LastReqIndex = GetNextReqIndex();
                if (LastReqIndex != -1)
                {
                    if (!btnLoad_Graph.Enabled)
                    {
                        RetryCount = 0;
                        ReqGraph((byte)LastReqIndex);
                    }
                    else
                    {
                        Chagne_Enable(true);

                        timer1.Enabled = false;
                    }
                }
                else
                {
                    Chagne_Enable(true);

                    timer1.Enabled = false;
                }
            }
        }

        private void Load_Title()
        {
            string Title_FILE = Application.StartupPath + "\\CONFIG\\TITLE_GRAPH.INI";

            bool TmpIsExist = Global_Class.UTIL_File_exists(Title_FILE);

            for (int i = 1; i <= 12; i++)
            {
                chart1.Series[i - 1].LegendText = IniControl.ReadString(Title_FILE, "Graph_TITLE", i.ToString(), "Item_" + i.ToString());
            }
            
            for (int i = 1; i <= 12; i++)
            {
                cb_Graph[i - 1].Text = IniControl.ReadString(Title_FILE, "Graph_TITLE", i.ToString(), "Item_" + i.ToString());
            }

            if (!TmpIsExist)
            {
                //StreamWriter wr = new StreamWriter(Title_FILE, false, Encoding.Unicode);
                StreamWriter wr = new StreamWriter(Title_FILE, false, Encoding.UTF8);

                wr.Close();


                for (int i = 1; i <= 12; i++)
                {
                    IniControl.WriteIni(Title_FILE, "Graph_TITLE", i.ToString(), "Item_" + i.ToString());
                }
            }
        }
        private void btnLoadStop_Graph_Click(object sender, EventArgs e)
        {
            Chagne_Enable(true);
            timer1.Enabled = false;
        }

        private void btnSaveCSV_Click(object sender, EventArgs e)
        {
            

            saveFileDialog1.Filter = "*.csv|*.csv";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, Encoding.UTF8);
                StringBuilder sb = new System.Text.StringBuilder();
                sb.Clear();
                sb.Append("No");
                for (byte i = 0; i < 12; i++)
                {
                    if (cb_Graph[i].Checked)
                    {
                        sb.Append(",");
                        sb.Append(cb_Graph[i].Text);
                    }
                }
                
                sw.WriteLine(sb);

                
                for (UInt16 i = 0; i < 700; i++)
                {
                    sb.Clear();
                    sb.Append((i+1).ToString());
                    for (byte j = 0; j < 12; j++)
                    {
                        if (cb_Graph[j].Checked)
                        {
                            sb.Append(",");
                            if (chart1.Series[j].Points.Count > i)
                            {
                                sb.Append(chart1.Series[j].Points[i].YValues[0]);
                            } else
                            {
                                sb.Append("0");
                            }
                        }
                    }
                    sw.WriteLine(sb);
                }
                sw.Close();
            }
        }

        private void rbGraphSaveType_0_CheckedChanged(object sender, EventArgs e)
        {
            edGraphSave_Position.Enabled = (rbGraphSaveType_1.Checked);
        }

        private void btnSet_GraphSaveSetting_Click(object sender, EventArgs e)
        {
            SetGraphSaveSetting();
        }

        private void btnLoad_GraphSaveSetting_Click(object sender, EventArgs e)
        {
            ReqGraphSaveSetting();
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Btn_LoadTitle_Click(object sender, EventArgs e)
        {
            Load_Title();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.graph|*.graph";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                chart1.Serializer.Load(openFileDialog1.FileName);
            }

            for (int i=0; i < chart1.Series.Count; i++)
            {
                cb_Graph[i].Text = chart1.Series[i].LegendText;
                if (chart1.Series[i].Points.Count>0)
                {
                    cb_Graph[i].Checked = true;
                } else
                {
                    cb_Graph[i].Checked = false;
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "*.graph|*.graph";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                chart1.Serializer.Save(saveFileDialog1.FileName);
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].Visible = false;
            chart1.ChartAreas[1].Position.Y = chart1.ChartAreas[0].Position.Y;
            chart1.ChartAreas[1].Position.Height = chart1.ChartAreas[1].Position.Height * 2;
        }
    }
}

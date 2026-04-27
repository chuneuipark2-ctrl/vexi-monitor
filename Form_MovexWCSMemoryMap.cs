using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_MovexWCSMemoryMap : Form
    {

        public Form_Main form_Main;
        private static VEXI_DEFS.TMOVEX_WCS_DataRec dev_REC_MOVEX_WCS;
         

        public Form_MovexWCSMemoryMap()
        {
            InitializeComponent();

        }
        #region 컴포넌트 이벤트
        private void Form_InvertorSt_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }
            Display_Init();
        }

        private void Form_InvertorSt_Activated(object sender, EventArgs e)
        {
            form_Main.COMMDataManager.ISWcsMapPolling = true;
        }

        private void Form_InvertorSt_Deactivate(object sender, EventArgs e)
        {
            form_Main.COMMDataManager.ISWcsMapPolling = false;
        }
        #endregion

        #region 기능함수
        public unsafe void Display_Init()
        {
            ListViewItem listviewitem;

            lv_WCSData.Items.Clear();
            lv_DevData.Items.Clear();
            lv_DevDataTotal.Items.Clear();
            lv_WCSDataTotal.Items.Clear();

            for (int Loop = 0; Loop < 100; Loop ++)
            {
                listviewitem = lv_WCSData.Items.Add(string.Format("{0}", 7000 + Loop));
                listviewitem.SubItems.Add("");
            }

            for (int Loop = 0; Loop < 300; Loop++)
            {
                listviewitem = lv_DevData.Items.Add(string.Format("{0}", 7500 + Loop));
                listviewitem.SubItems.Add("");
            }


            //for (int Loop = 0; Loop < 100; Loop++)
            //{

            //    dev_REC_MOVEX_WCS.WCSData[Loop] = (UInt16)(Loop % 3);
            //}

            //for (int Loop = 0; Loop < 300; Loop++)
            //{
            //    dev_REC_MOVEX_WCS.DEVData[Loop] = (UInt16)(Loop % 3);
            //}

            rbEMS1.Visible = (form_Main.COMMDataManager.RX_DestDevType == ConstClass.TYPE_EMS);
            rbEMS2.Visible = (form_Main.COMMDataManager.RX_DestDevType == ConstClass.TYPE_EMS);
            MakeAllDisplay_Dev();
            MakeAllDisplay_WCS();
            Display_WCSData();
        }

        //상태데이터 화면 표출 함수
        public unsafe void SET_WCSData(byte[] datas)
        {
            dev_REC_MOVEX_WCS = (VEXI_DEFS.TMOVEX_WCS_DataRec)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TMOVEX_WCS_DataRec));

            Display_WCSData();
        }


        private unsafe void Display_WCSData()
        {
            DateTime time = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(dev_REC_MOVEX_WCS.RX_WCSDataTime);
            lbl_WCS_RxTime.Text = String.Format("{0}", time);

            time = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(dev_REC_MOVEX_WCS.TX_DEVDataTime_1);
            lbl_WCS_TxTime1.Text = String.Format("{0}", time);

            time = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(dev_REC_MOVEX_WCS.TX_DEVDataTime_2);
            lbl_WCS_TxTime2.Text = String.Format("{0}", time);

            for (int Loop = 0; Loop < 100; Loop++)
            {
                lv_WCSData.Items[Loop].SubItems[1].Text = string.Format("0x{0:X4}", dev_REC_MOVEX_WCS.WCSData[Loop]);
            }

            for (int Loop = 0; Loop < 300; Loop++)
            {
                lv_DevData.Items[Loop].SubItems[1].Text = string.Format("0x{0:X4}", dev_REC_MOVEX_WCS.DEVData[Loop]);
            }


            DisplayValue_WCS();
            DisplayValue_DEV();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private unsafe void lv_WCSData_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        private string GetType(string TmpAddr)
        {
            for (int Loop = 0; Loop < ConstClass.MOVEX_WCS_ADDR_DEF.GetLength(0); Loop++)
            {
                if (ConstClass.MOVEX_WCS_ADDR_DEF[Loop, 0] == TmpAddr)
                {
                    switch (form_Main.COMMDataManager.RX_DestDevType)
                    {
                        case ConstClass.TYPE_RTV:
                            return (ConstClass.MOVEX_WCS_ADDR_DEF[Loop, 1]); 
                        case ConstClass.TYPE_EMS:
                            {
                                if   (rbEMS1.Checked) return (ConstClass.MOVEX_WCS_ADDR_DEF[Loop, 2]);
                                else                  return (ConstClass.MOVEX_WCS_ADDR_DEF[Loop, 3]);
                            }
                        default: 
                            return ("");
                            
                    }
                }
            }
            return ("");
        }

        private string GetEMSItem(string TmpAddr)
        {
            if (rbEMS1.Checked)
            {
                return GetEMS1Item(TmpAddr);
            } else
            {
                return GetEMS2Item(TmpAddr);
            }
        }

        private string GetEMS1Item(string TmpAddr)
        {
            for (int Loop = 0; Loop < ConstClass.MOVEX_WCS_EMS1Detail_DEF.GetLength(0); Loop++)
            {
                if (ConstClass.MOVEX_WCS_EMS1Detail_DEF[Loop, 0] == TmpAddr)
                {
                    return (ConstClass.MOVEX_WCS_EMS1Detail_DEF[Loop, 1]);
                }
            }
            return ("SPARE");
        }

        private string GetEMS2Item(string TmpAddr)
        {
            for (int Loop = 0; Loop < ConstClass.MOVEX_WCS_EMS2Detail_DEF.GetLength(0); Loop++)
            {
                if (ConstClass.MOVEX_WCS_EMS2Detail_DEF[Loop, 0] == TmpAddr)
                {
                    return (ConstClass.MOVEX_WCS_EMS2Detail_DEF[Loop, 1]);
                }
            }
            return ("SPARE");
        }

        private string GetRTVItem(string TmpAddr)
        {
            for (int Loop = 0; Loop < ConstClass.MOVEX_WCS_RTVDetail_DEF.GetLength(0); Loop++)
            {
                if (ConstClass.MOVEX_WCS_RTVDetail_DEF[Loop, 0] == TmpAddr)
                {
                    return (ConstClass.MOVEX_WCS_RTVDetail_DEF[Loop, 1]);
                }
            }
            return ("SPARE");
        }

        private unsafe void MakeAllDisplay_WCS()
        {
            string Addr = "";
            uint AddrInt = 0;
            string Addrtype = "";
            string SubAddr = "";
            ListViewItem listviewitem;
            int Selectedindex = -1;
            UInt16 AddrValue = 0;


            lv_WCSDataTotal.Items.Clear();

            for (int i = 0; i < lv_WCSData.Items.Count; i++)
            {
                Selectedindex = i;

                Addr = lv_WCSData.Items[Selectedindex].Text;
                AddrInt = (uint)Convert.ToDecimal(Addr);
                Addrtype = GetType(Addr);

                AddrValue = dev_REC_MOVEX_WCS.DEVData[Selectedindex];

                if (Addrtype == "B")
                {
                    switch (form_Main.COMMDataManager.RX_DestDevType)
                    {
                        case ConstClass.TYPE_RTV:
                            for (int Loop = 0; Loop < 16; Loop++)
                            {
                                SubAddr = Addr + "." + string.Format("{0}", Loop);

                                if (GetRTVItem(SubAddr) != "SPARE")
                                {
                                    listviewitem = lv_WCSDataTotal.Items.Add(Addr);
                                    listviewitem.SubItems.Add(string.Format("{0}", Loop));
                                    listviewitem.SubItems.Add(GetRTVItem(SubAddr));
                                    listviewitem.SubItems.Add("");

                                }
                            }
                            break;
                        case ConstClass.TYPE_EMS:
                            for (int Loop = 0; Loop < 16; Loop++)
                            {
                                if (rbEMS1.Checked)
                                {
                                    if ((AddrInt >= 7040) && (AddrInt <= 7089))
                                    {
                                        SubAddr = "70XX." + string.Format("{0}", Loop);
                                    }
                                    else
                                    {
                                        SubAddr = Addr + "." + string.Format("{0}", Loop);
                                    }
                                } else
                                {
                                    SubAddr = Addr + "." + string.Format("{0}", Loop);
                                }
                                if (GetEMSItem(SubAddr) != "SPARE")
                                {
                                    listviewitem = lv_WCSDataTotal.Items.Add(Addr);
                                    listviewitem.SubItems.Add(string.Format("{0}", Loop));

                                    if (rbEMS1.Checked)
                                    {
                                        if ((AddrInt >= 7040) && (AddrInt <= 7089))
                                        {
                                            listviewitem.SubItems.Add(string.Format("ST {0} ", AddrInt - 7040 + 1) + GetEMSItem(SubAddr));
                                        }
                                        else
                                        {
                                            listviewitem.SubItems.Add(GetEMSItem(SubAddr));
                                        }
                                    } else
                                    {
                                        listviewitem.SubItems.Add(GetEMSItem(SubAddr));
                                    }
                                    listviewitem.SubItems.Add("");
                                }
                            }
                            break;
                    }
                }
                else if (Addrtype == "W")
                {
                    switch (form_Main.COMMDataManager.RX_DestDevType)
                    {
                        case ConstClass.TYPE_RTV:
                            if (GetRTVItem(Addr) != "SPARE")
                            {
                                listviewitem = lv_WCSDataTotal.Items.Add(Addr);
                                listviewitem.SubItems.Add("W");
                                listviewitem.SubItems.Add(GetRTVItem(Addr));
                                listviewitem.SubItems.Add("");
                            }
                            break;

                        case ConstClass.TYPE_EMS:
                            if (GetEMSItem(Addr) != "SPARE")
                            {
                                listviewitem = lv_WCSDataTotal.Items.Add(Addr);
                                listviewitem.SubItems.Add("W");
                                listviewitem.SubItems.Add(GetEMSItem(Addr));
                                listviewitem.SubItems.Add("");
                            }
                            break;

                    }
                }
                else if (Addrtype == "WL")
                {

                    switch (form_Main.COMMDataManager.RX_DestDevType)
                    {
                        case ConstClass.TYPE_RTV:
                            if (GetRTVItem(Addr) != "SPARE")
                            {
                                listviewitem = lv_WCSDataTotal.Items.Add(Addr);
                                listviewitem.SubItems.Add("WL");
                                listviewitem.SubItems.Add(GetRTVItem(Addr));
                                listviewitem.SubItems.Add("");
                            }
                            break;

                        case ConstClass.TYPE_EMS:
                            if (GetEMSItem(Addr) != "SPARE")
                            {
                                listviewitem = lv_WCSDataTotal.Items.Add(Addr);
                                listviewitem.SubItems.Add("WL");
                                listviewitem.SubItems.Add(GetEMSItem(Addr));
                                listviewitem.SubItems.Add("");
                            }
                            break;

                    }
                }
                else if (Addrtype == "WH")
                {
                    switch (form_Main.COMMDataManager.RX_DestDevType)
                    {
                        case ConstClass.TYPE_RTV:
                            if (GetRTVItem(Addr) != "SPARE")
                            {
                                listviewitem = lv_WCSDataTotal.Items.Add(Addr);
                                listviewitem.SubItems.Add("WH");
                                listviewitem.SubItems.Add(GetRTVItem(Addr));
                                listviewitem.SubItems.Add("");
                            }
                            break;

                        case ConstClass.TYPE_EMS:
                            if (GetEMSItem(Addr) != "SPARE")
                            {
                                listviewitem = lv_WCSDataTotal.Items.Add(Addr);
                                listviewitem.SubItems.Add("WH");
                                listviewitem.SubItems.Add(GetEMSItem(Addr));
                                listviewitem.SubItems.Add("");
                            }
                            break;
                    }
                }
            }

        }

        private unsafe void MakeAllDisplay_Dev()
        {
            string Addr = "";
            uint AddrInt = 0;
            string Addrtype = "";
            string SubAddr = "";
            ListViewItem listviewitem;
            int Selectedindex = -1;

            lv_DevDataTotal.Items.Clear();

            for (int i = 0; i < lv_DevData.Items.Count; i++)
            {
                Selectedindex = i;

                Addr = lv_DevData.Items[Selectedindex].Text;
                AddrInt = (uint)Convert.ToDecimal(Addr);
                Addrtype = GetType(Addr);

                if (Addrtype == "B")
                {
                    switch (form_Main.COMMDataManager.RX_DestDevType)
                    {
                        case ConstClass.TYPE_RTV:
                            for (int Loop = 0; Loop < 16; Loop++)
                            {
                                SubAddr = Addr + "." + string.Format("{0}", Loop);

                                if (GetRTVItem(SubAddr) != "SPARE")
                                {
                                    //listviewitem = lv_DevDataTotal.Items.Add(SubAddr);
                                    //listviewitem.SubItems.Add(GetRTVItem(SubAddr));

                                    listviewitem = lv_DevDataTotal.Items.Add(Addr);
                                    listviewitem.SubItems.Add(string.Format("{0}", Loop));
                                    listviewitem.SubItems.Add(GetRTVItem(SubAddr));
                                    listviewitem.SubItems.Add("");

                                }
                            }
                            break;
                        case ConstClass.TYPE_EMS:
                            for (int Loop = 0; Loop < 16; Loop++)
                            {
                                if (rbEMS1.Checked)
                                {

                                    if ((AddrInt >= 7640) && (AddrInt <= 7689))
                                    {
                                        SubAddr = "76XX." + string.Format("{0}", Loop);
                                    }
                                    else
                                    {
                                        SubAddr = Addr + "." + string.Format("{0}", Loop);
                                    }
                                } else
                                {
                                    SubAddr = Addr + "." + string.Format("{0}", Loop);
                                }
                                if (GetEMSItem(SubAddr) != "SPARE")
                                {
                                    listviewitem = lv_DevDataTotal.Items.Add(Addr);
                                    listviewitem.SubItems.Add(string.Format("{0}", Loop));
                                    if (rbEMS1.Checked)
                                    {
                                        if ((AddrInt >= 7640) && (AddrInt <= 7689))
                                        {
                                            listviewitem.SubItems.Add(string.Format("ST {0} ", AddrInt - 7640 + 1) + GetEMSItem(SubAddr));
                                        }
                                        else
                                        {
                                            listviewitem.SubItems.Add(GetEMSItem(SubAddr));
                                        }
                                    } else
                                    {
                                        listviewitem.SubItems.Add(GetEMSItem(SubAddr));
                                    }

                                    listviewitem.SubItems.Add("");
                                }
                            }
                            break;
                    }
                }
                else if (Addrtype == "W")
                {
                    switch (form_Main.COMMDataManager.RX_DestDevType)
                    {
                        case ConstClass.TYPE_RTV:
                            if (GetRTVItem(Addr) != "SPARE")
                            {
                                //listviewitem = lv_DevDataTotal.Items.Add(Addr);
                                //listviewitem.SubItems.Add(GetRTVItem(Addr));

                                listviewitem = lv_DevDataTotal.Items.Add(Addr);
                                listviewitem.SubItems.Add("W");
                                listviewitem.SubItems.Add(GetRTVItem(Addr));
                                listviewitem.SubItems.Add("");
                            }
                            break;

                        case ConstClass.TYPE_EMS:
                            if (GetEMSItem(Addr) != "SPARE")
                            {
                                listviewitem = lv_DevDataTotal.Items.Add(Addr);
                                listviewitem.SubItems.Add("W");
                                listviewitem.SubItems.Add(GetEMSItem(Addr));
                                listviewitem.SubItems.Add("");
                            }
                            break;

                    }
                }
                else if (Addrtype == "WL")
                {
                    switch (form_Main.COMMDataManager.RX_DestDevType)
                    {
                        case ConstClass.TYPE_RTV:
                            if (GetRTVItem(Addr) != "SPARE")
                            {
                                listviewitem = lv_DevDataTotal.Items.Add(Addr);
                                listviewitem.SubItems.Add("WL");
                                listviewitem.SubItems.Add(GetRTVItem(Addr));
                                listviewitem.SubItems.Add("");
                            }
                            break;

                        case ConstClass.TYPE_EMS:
                            if (GetEMSItem(Addr) != "SPARE")
                            {
                                listviewitem = lv_DevDataTotal.Items.Add(Addr);
                                listviewitem.SubItems.Add("WL");
                                listviewitem.SubItems.Add(GetEMSItem(Addr));
                                listviewitem.SubItems.Add("");
                            }
                            break;

                    }
                }
                else if (Addrtype == "WH")
                {
                    switch (form_Main.COMMDataManager.RX_DestDevType)
                    {
                        case ConstClass.TYPE_RTV:
                            if (GetRTVItem(Addr) != "SPARE")
                            {
                                listviewitem = lv_DevDataTotal.Items.Add(Addr);
                                listviewitem.SubItems.Add("WH");
                                listviewitem.SubItems.Add(GetRTVItem(Addr));
                                listviewitem.SubItems.Add("");
                            }
                            break;

                        case ConstClass.TYPE_EMS:
                            if (GetEMSItem(Addr) != "SPARE")
                            {
                                listviewitem = lv_DevDataTotal.Items.Add(Addr);
                                listviewitem.SubItems.Add("WH");
                                listviewitem.SubItems.Add(GetEMSItem(Addr));
                                listviewitem.SubItems.Add("");
                            }
                            break;
                    }
                }
            }
        }


        private unsafe void DisplayValue_WCS()
        {
            string Addr = "";
            uint AddrInt = 0;
            string SubAddr = "";
            UInt32 AddrValue = 0;
            UInt16 L_Value = 0;
            UInt16 H_Value = 0;


            for (int i = 0; i < lv_WCSDataTotal.Items.Count; i++)
            {

                Addr = lv_WCSDataTotal.Items[i].Text;
                AddrInt = (uint)Convert.ToDecimal(Addr);
                SubAddr = lv_WCSDataTotal.Items[i].SubItems[1].Text;

                if (SubAddr == "W")
                {
                    AddrValue = dev_REC_MOVEX_WCS.WCSData[AddrInt - 7000];
                } else if (SubAddr == "WL")
                {
                    L_Value = dev_REC_MOVEX_WCS.WCSData[AddrInt - 7000];
                    H_Value = dev_REC_MOVEX_WCS.WCSData[AddrInt - 7000 + 1];
                    AddrValue = (UInt32)(L_Value | (H_Value << 16));
                } else if (SubAddr == "WH")
                {
                    L_Value = dev_REC_MOVEX_WCS.WCSData[AddrInt - 7000 - 1];
                    H_Value = dev_REC_MOVEX_WCS.WCSData[AddrInt - 7000];
                    AddrValue = (UInt32)(L_Value | (H_Value << 16));

                } else
                {
                    if  ((dev_REC_MOVEX_WCS.WCSData[AddrInt - 7000] & (0x00000001 << (byte)(Convert.ToDecimal(SubAddr)))) > 0)
                    {
                        AddrValue = 1;
                    } else
                    {
                        AddrValue = 0;
                    }

                }
                lv_WCSDataTotal.Items[i].SubItems[3].Text = Convert.ToString(AddrValue);
            }

        }

        private unsafe void DisplayValue_DEV()
        {
            string Addr = "";
            uint AddrInt = 0;
            string SubAddr = "";
            UInt32 AddrValue = 0;
            UInt16 L_Value = 0;
            UInt16 H_Value = 0;


            for (int i = 0; i < lv_DevDataTotal.Items.Count; i++)
            {

                Addr = lv_DevDataTotal.Items[i].Text;
                AddrInt = (uint)Convert.ToDecimal(Addr);
                SubAddr = lv_DevDataTotal.Items[i].SubItems[1].Text;

                if (SubAddr == "W")
                {
                    AddrValue = dev_REC_MOVEX_WCS.DEVData[AddrInt - 7500];
                }
                else if (SubAddr == "WL")
                {
                    L_Value = dev_REC_MOVEX_WCS.DEVData[AddrInt - 7500];
                    H_Value = dev_REC_MOVEX_WCS.DEVData[AddrInt - 7500 + 1];
                    AddrValue = (UInt32)(L_Value | (H_Value << 16));
                }
                else if (SubAddr == "WH")
                {
                    L_Value = dev_REC_MOVEX_WCS.DEVData[AddrInt - 7500 - 1];
                    H_Value = dev_REC_MOVEX_WCS.DEVData[AddrInt - 7500];
                    AddrValue = (UInt32)(L_Value | (H_Value << 16));

                }
                else
                {
                    if ((dev_REC_MOVEX_WCS.DEVData[AddrInt - 7500] & (0x00000001 << (byte)(Convert.ToDecimal(SubAddr)))) > 0)
                    {
                        AddrValue = 1;
                    }
                    else
                    {
                        AddrValue = 0;
                    }

                }
                lv_DevDataTotal.Items[i].SubItems[3].Text = Convert.ToString(AddrValue);
            }

        }

        private unsafe void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void rbEMS1_CheckedChanged(object sender, EventArgs e)
        {
            Display_Init();
        }

        private unsafe void button1_Click_2(object sender, EventArgs e)
        {
            
            for (int Loop = 0; Loop < 100; Loop++)
            {
                dev_REC_MOVEX_WCS.WCSData[Loop] = 0xffff;
            }

            for (int Loop = 0; Loop < 300; Loop++)
            {
                dev_REC_MOVEX_WCS.DEVData[Loop] = 0xffff;
            }
            Display_WCSData();
        }
    }
}

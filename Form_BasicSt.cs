using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_BasicSt : Form
    {

        public Form_Main form_Main;

        private static Label[,] lbl_Link;
        private static VEXI_DEFS.TDEV_REC_BasicCtrl dev_REC_BasicCtrl;
        public bool IsFirst = true;

        public Form_BasicSt()
        {
            InitializeComponent();

            lbl_Link = new Label[,] {{ lbl_Link0_0, lbl_Link0_1,  null, lbl_Link0_3, lbl_Link0_4, lbl_Link0_5, lbl_Link0_6, lbl_Link0_7},
                                     { lbl_Link1_0, lbl_Link1_1, lbl_Link1_2, lbl_Link1_3, lbl_Link1_4, lbl_Link1_5, lbl_Link1_6, lbl_Link1_7},
                                     { lbl_Link2_0, lbl_Link2_1, lbl_Link2_2, lbl_Link2_3, lbl_Link2_4, lbl_Link2_5, lbl_Link2_6, lbl_Link2_7},
                                     { lbl_Link3_0, lbl_Link3_1, lbl_Link3_2, null, null, null, null, null}};


        }
        #region 컴포넌트 이벤트
        private void Form_BasicSt_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            } else
            { 
                form_Main = (Form_Main)this.Owner;
            }
            Display_DevBasicSt();
        }

        private void BtnDevicereset_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;

            Do_Ctrl(Convert.ToByte(bt.Tag.ToString()));

            //임시
            //호기번호 변경시 Device Reset 명령도 내린다
            //장치에서 자동으로 Device Reset을 수행하는 걸로 바뀌거나 호기번호 변경 처리가 제대로 되면 이 루틴은 필요없다.
            //현재는 Device Reset 전에는 변경한 호기번호로 Request 해야 응답하는 응답하는 호기번호는 변경전 호기번호이다
            //if (bt.Tag.ToString() == "25")
            //{
            //    if (lblHOGINum.Text != edHOGINum.Text)
            //    {
            //        Do_DeviceReset();
            //    }
            //}
        }

        private void edHOGINum_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox ed = sender as TextBox;

            if (ed.Tag.ToString() == "00") // 마이너스 OFF, 소수점 OFF
            {
                if (
                (char.IsControl(e.KeyChar)) ||
                (char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == Convert.ToChar(Keys.Enter)) ||
                (e.KeyChar == Convert.ToChar(Keys.Back)) ||
                (e.KeyChar == Convert.ToChar(Keys.Tab)))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if (ed.Tag.ToString() == "01") // 마이너스 OFF, 소수점 ON
            {
                if (
                (char.IsControl(e.KeyChar)) ||
                (char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == '.') ||
                (e.KeyChar == Convert.ToChar(Keys.Enter)) ||
                (e.KeyChar == Convert.ToChar(Keys.Back)) ||
                (e.KeyChar == Convert.ToChar(Keys.Tab)))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = false;
            }
        }
        #endregion

        #region 기능함수
        //제어 함수
        private unsafe void Do_DeviceReset()
        {
            dev_REC_BasicCtrl.CtrlFlag[0] = 0x01;
            dev_REC_BasicCtrl.CtrlFlag[1] = 0x00;
            dev_REC_BasicCtrl.CtrlFlag[2] = 0x00;
            dev_REC_BasicCtrl.CtrlFlag[3] = 0x00;
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_11, dev_REC_BasicCtrl);
            form_Main.COMMDataManager.ADD_TxUserData_UserSeletDev(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_11, dev_REC_BasicCtrl);
        }

        private unsafe void Do_Ctrl(byte TmpFlag)
        {
            IPAddress ReturnIP;
            PhysicalAddress ReturnMAC;

            fixed (VEXI_DEFS.TDEV_REC_BasicCtrl* DevCtrl = &dev_REC_BasicCtrl)
            {

                //Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl->CtrlFlag, Marshal.SizeOf(typeof(VEXI_DEFS.DEV_REC_0x0111)));
                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_BasicCtrl)));

                switch (TmpFlag)
                {
                    case 10: //Device Reset
                        DevCtrl->CtrlFlag[0] = 0x01;
                        break;
                    case 20:
                        DevCtrl->CtrlFlag[1] = 0x01;
                        break;
                    case 21:
                        if (pnMacUse_1.Visible)
                        {
                            DevCtrl->CtrlFlag[1] = 0x06;
                        }
                        else
                        {
                            DevCtrl->CtrlFlag[1] = 0x02;
                        }
                        break;
                    case 23:
                        if (pnMacUse_2.Visible)
                        {
                            DevCtrl->CtrlFlag[1] = 0x18;
                        }
                        else
                        {
                            DevCtrl->CtrlFlag[1] = 0x08;
                        }
                        break;
                    case 25:
                        DevCtrl->CtrlFlag[1] = 0x20;
                        break;
                }


                if (Global_Class.UTIL_IsValid_IP(edDevIP_1.Text, out ReturnIP))
                {
                    Global_Class.UTIL_BytesToBytePtr(ReturnIP.GetAddressBytes(), (byte*)DevCtrl->Network_1.DevIP);
                }
                else
                {
                    if (TmpFlag == 21) 
                    {
                        form_Main.GlobalObj.MsgBox_Confirm_OK("IP 형식이 맞지 않습니다");
                        return;
                    }
                }

                if (Global_Class.UTIL_IsValid_IP(edDevSubnet_1.Text, out ReturnIP))
                {
                    Global_Class.UTIL_BytesToBytePtr(ReturnIP.GetAddressBytes(), (byte*)DevCtrl->Network_1.DevSubnet);
                }
                else
                {
                    if (TmpFlag == 21)
                    {
                        form_Main.GlobalObj.MsgBox_Confirm_OK("SubNet 형식이 맞지 않습니다");
                        return;
                    }
                }

                if (Global_Class.UTIL_IsValid_IP(edDevGateway_1.Text, out ReturnIP))
                {
                    Global_Class.UTIL_BytesToBytePtr(ReturnIP.GetAddressBytes(), (byte*)DevCtrl->Network_1.DevGateway);
                }
                else
                {
                    if (TmpFlag == 21)
                    {
                        form_Main.GlobalObj.MsgBox_Confirm_OK("Gateway 형식이 맞지 않습니다");
                        return;
                    }
                }

                edDevMAC__1.Text = edDevMAC__1.Text.Replace(':', '-');
                edDevMAC__1.Text = edDevMAC__1.Text.ToUpper();
                if (Global_Class.UTIL_IsValid_MAC(edDevMAC__1.Text, out ReturnMAC))
                {
                    Global_Class.UTIL_BytesToBytePtr(PhysicalAddress.Parse(edDevMAC__1.Text).GetAddressBytes(), (byte*)DevCtrl->Network_1.DevMacAddr);
                }
                else
                {
                    if ((TmpFlag == 21) && (pnMacUse_1.Visible))
                    {
                        form_Main.GlobalObj.MsgBox_Confirm_OK("MAC 형식이 맞지 않습니다");

                        return;
                    }
                }



                if (Global_Class.UTIL_IsValid_IP(edDevIP_2.Text, out ReturnIP))
                {
                    Global_Class.UTIL_BytesToBytePtr(ReturnIP.GetAddressBytes(), (byte*)DevCtrl->Network_2.DevIP);
                }
                else
                {
                    if (TmpFlag == 23)
                    {
                        form_Main.GlobalObj.MsgBox_Confirm_OK("IP 형식이 맞지 않습니다");
                        return;
                    }
                }

                if (Global_Class.UTIL_IsValid_IP(edDevSubnet_2.Text, out ReturnIP))
                {
                    Global_Class.UTIL_BytesToBytePtr(ReturnIP.GetAddressBytes(), (byte*)DevCtrl->Network_2.DevSubnet);
                }
                else
                {
                    if (TmpFlag == 23)
                    {
                        form_Main.GlobalObj.MsgBox_Confirm_OK("SubNet 형식이 맞지 않습니다");
                        return;
                    }
                }

                if (Global_Class.UTIL_IsValid_IP(edDevGateway_2.Text, out ReturnIP))
                {
                    Global_Class.UTIL_BytesToBytePtr(ReturnIP.GetAddressBytes(), (byte*)DevCtrl->Network_2.DevGateway);
                }
                else
                {
                    if (TmpFlag == 23)
                    {
                        form_Main.GlobalObj.MsgBox_Confirm_OK("Gateway 형식이 맞지 않습니다");
                        return;
                    }
                }

                edDevMAC__2.Text = edDevMAC__2.Text.Replace(':', '-');
                edDevMAC__2.Text = edDevMAC__2.Text.ToUpper();
                if (Global_Class.UTIL_IsValid_MAC(edDevMAC__2.Text, out ReturnMAC))
                {
                    Global_Class.UTIL_BytesToBytePtr(PhysicalAddress.Parse(edDevMAC__2.Text).GetAddressBytes(), (byte*)DevCtrl->Network_2.DevMacAddr);
                }
                else
                {
                    if ((TmpFlag == 23) && (pnMacUse_2.Visible))
                    {
                        form_Main.GlobalObj.MsgBox_Confirm_OK("MAC 형식이 맞지 않습니다");

                        return;
                    }
                }



                DevCtrl->SystemUTCTime = Global_Class.UTIL_GetUnixTimeStampFromLocalTime(DateTime.Now);

                //Global_Class.UTIL_BytesToBytePtr(System.Text.Encoding.Default.GetBytes(edProjectNo.Text), (byte*)DevCtrl->ProjectID, 6);
                //ASCII 코드 처리 (영문1자리 + 숫자5자리 조합)
                Global_Class.UTIL_BytesToBytePtr(System.Text.Encoding.ASCII.GetBytes(edProjectNo.Text), (byte*)DevCtrl->ProjectID, 6);
                DevCtrl->GroupID = (byte)Global_Class.UTIL_StrToIntDef(edGroupNum.Text, 0);
                DevCtrl->HogiID = (UInt16)Global_Class.UTIL_StrToIntDef(edHOGINum.Text, 0);
            }

            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_11, dev_REC_BasicCtrl);
        }

        //상태데이터 화면 표출 함수
        public unsafe void Display_DevBasicSt()
        {
            
            //MAC Address는 시리얼통신 연결 상태, 어드민 실행 2가지 조건이 만족하는 경우에만 제어가능하도록 한다
            if (form_Main.COMMDataManager.COMM_Mode == ConstClass.COMM_SERIAL)
            {
                pnMacUse_1.Visible = (form_Main.IsAdmin);
                pnMacUse_2.Visible = (form_Main.IsAdmin);
            }

            fixed (VEXI_DEFS.TDEV_REC_BasicStRes* DevSt = &form_Main.COMMDataManager.DevRec.dev_REC_BasicSt)
            {
                //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                if (form_Main.COMMDataManager.DevRec.Flag_In_8110)
                //if (true) //테스트 시
                {

                    lblPVersion.Text = Global_Class.UTIL_ByteToPVerstr(DevSt->PGVersion);
                    lblFWVersion.Text = Global_Class.UTIL_ByteToFVerstr(DevSt->FWversion);
                    DateTime PCtime = Global_Class.UTIL_GetLocalTimeFromUnixTimeStamp(DevSt->SystemUTCTime);
                    lblSystemTimeUTC.Text = String.Format("{0}", PCtime);

                    lblDevIP_1.Text = string.Format("{0}.{1}.{2}.{3}",
                                                        DevSt->Network_1.DevIP[0],
                                                        DevSt->Network_1.DevIP[1],
                                                        DevSt->Network_1.DevIP[2],
                                                        DevSt->Network_1.DevIP[3]);
                    lblDevSubnet_1.Text = string.Format("{0}.{1}.{2}.{3}",
                                                        DevSt->Network_1.DevSubnet[0],
                                                        DevSt->Network_1.DevSubnet[1],
                                                        DevSt->Network_1.DevSubnet[2],
                                                        DevSt->Network_1.DevSubnet[3]);
                    lblDevGateway_1.Text = string.Format("{0}.{1}.{2}.{3}",
                                                        DevSt->Network_1.DevGateway[0],
                                                        DevSt->Network_1.DevGateway[1],
                                                        DevSt->Network_1.DevGateway[2],
                                                        DevSt->Network_1.DevGateway[3]);

                    lblDevMAC_1.Text = string.Format("{0:X2}-{1:X2}-{2:X2}-{3:X2}-{4:X2}-{5:X2}",
                                                    DevSt->Network_1.DevMacAddr[0],
                                                    DevSt->Network_1.DevMacAddr[1],
                                                    DevSt->Network_1.DevMacAddr[2],
                                                    DevSt->Network_1.DevMacAddr[3],
                                                    DevSt->Network_1.DevMacAddr[4],
                                                    DevSt->Network_1.DevMacAddr[5]);
                    lblDevMAC__1.Text = lblDevMAC_1.Text;


                    //if (edDevIP_1.Text == "") edDevIP_1.Text = lblDevIP_1.Text;
                    //if (edDevSubnet_1.Text == "") edDevSubnet_1.Text = lblDevSubnet_1.Text;
                    //if (edDevGateway_1.Text == "") edDevGateway_1.Text = lblDevGateway_1.Text;
                    //if (edDevMAC__1.Text == "") edDevMAC__1.Text = lblDevMAC__1.Text;

                    if (IsFirst) edDevIP_1.Text = lblDevIP_1.Text;
                    if (IsFirst) edDevSubnet_1.Text = lblDevSubnet_1.Text;
                    if (IsFirst) edDevGateway_1.Text = lblDevGateway_1.Text;
                    if (IsFirst) edDevMAC__1.Text = lblDevMAC__1.Text;

                    lblDevIP_2.Text = string.Format("{0}.{1}.{2}.{3}",
                                                        DevSt->Network_2.DevIP[0],
                                                        DevSt->Network_2.DevIP[1],
                                                        DevSt->Network_2.DevIP[2],
                                                        DevSt->Network_2.DevIP[3]);
                    lblDevSubnet_2.Text = string.Format("{0}.{1}.{2}.{3}",
                                                        DevSt->Network_2.DevSubnet[0],
                                                        DevSt->Network_2.DevSubnet[1],
                                                        DevSt->Network_2.DevSubnet[2],
                                                        DevSt->Network_2.DevSubnet[3]);
                    lblDevGateway_2.Text = string.Format("{0}.{1}.{2}.{3}",
                                                        DevSt->Network_2.DevGateway[0],
                                                        DevSt->Network_2.DevGateway[1],
                                                        DevSt->Network_2.DevGateway[2],
                                                        DevSt->Network_2.DevGateway[3]);

                    lblDevMAC_2.Text = string.Format("{0:X2}-{1:X2}-{2:X2}-{3:X2}-{4:X2}-{5:X2}",
                                                    DevSt->Network_2.DevMacAddr[0],
                                                    DevSt->Network_2.DevMacAddr[1],
                                                    DevSt->Network_2.DevMacAddr[2],
                                                    DevSt->Network_2.DevMacAddr[3],
                                                    DevSt->Network_2.DevMacAddr[4],
                                                    DevSt->Network_2.DevMacAddr[5]);
                    lblDevMAC__2.Text = lblDevMAC_2.Text;


                    //if (edDevIP_2.Text == "") edDevIP_2.Text = lblDevIP_2.Text;
                    //if (edDevSubnet_2.Text == "") edDevSubnet_2.Text = lblDevSubnet_2.Text;
                    //if (edDevGateway_2.Text == "") edDevGateway_2.Text = lblDevGateway_2.Text;
                    //if (edDevMAC__2.Text == "") edDevMAC__2.Text = lblDevMAC__2.Text;

                    if (IsFirst) edDevIP_2.Text = lblDevIP_2.Text;
                    if (IsFirst) edDevSubnet_2.Text = lblDevSubnet_2.Text;
                    if (IsFirst) edDevGateway_2.Text = lblDevGateway_2.Text;
                    if (IsFirst) edDevMAC__2.Text = lblDevMAC__2.Text;

                    var convertedArray = new byte[6];
                    System.Runtime.InteropServices.Marshal.Copy((IntPtr)DevSt->ProjectID, convertedArray, 0, 6);
                    //lblProjectNo.Text = System.Text.Encoding.Default.GetString(convertedArray);
                    lblProjectNo.Text = System.Text.Encoding.ASCII.GetString(convertedArray);
                    lblGroupNum.Text = string.Format("{0}", DevSt->GroupID);
                    lblHOGINum.Text = string.Format("{0}", DevSt->HogiID);
                    //if (edProjectNo.Text == "") edProjectNo.Text = lblProjectNo.Text;
                    //if (edGroupNum.Text == "") edGroupNum.Text = lblGroupNum.Text;
                    //if (edHOGINum.Text == "") edHOGINum.Text = lblHOGINum.Text;
                    if (IsFirst) edProjectNo.Text = lblProjectNo.Text;
                    if (IsFirst) edGroupNum.Text = lblGroupNum.Text;
                    if (IsFirst) edHOGINum.Text = lblHOGINum.Text;


                    lbl_ModeSW.Text = string.Format("{0}", (DevSt->ModeSwitch & 0x0F));
                    lbl_IDSW.Text   = string.Format("{0:X2}", (DevSt->IDSwitch));

                    for (byte i = 0; i < 4; i++)
                    {
                        for (byte j = 0; j < 8; j++)
                        {
                            if (lbl_Link[i, j] != null)
                            {
                                if (Global_Class.BitStatus(DevSt->LinkSt[i], j))
                                {
                                    lbl_Link[i, j].BackColor = System.Drawing.Color.Yellow;
                                } 
                                else
                                {
                                    lbl_Link[i, j].BackColor = System.Drawing.Color.Gray;
                                }
                            }
                        }
                    }

                    IsFirst = false;

                }
                else
                {
                    lblPVersion.Text = "";
                    lblFWVersion.Text = "";
                    lblSystemTimeUTC.Text = "";

                    lblDevIP_1.Text = "";
                    lblDevSubnet_1.Text = "";
                    lblDevGateway_1.Text = "";
                    lblDevMAC_1.Text = "";
                    lblDevMAC__1.Text = "";
                    edDevIP_1.Text = "";
                    edDevSubnet_1.Text = "";
                    edDevGateway_1.Text = "";
                    edDevMAC__1.Text = "";

                    lblDevIP_2.Text = "";
                    lblDevSubnet_2.Text = "";
                    lblDevGateway_2.Text = "";
                    lblDevMAC_2.Text = "";
                    lblDevMAC__2.Text = "";
                    edDevIP_2.Text = "";
                    edDevSubnet_2.Text = "";
                    edDevGateway_2.Text = "";
                    edDevMAC__2.Text = "";

                    lblProjectNo.Text = "";
                    lblGroupNum.Text = "";
                    lblHOGINum.Text = "";
                    edProjectNo.Text = "";
                    edGroupNum.Text = "";
                    edHOGINum.Text = "";

                    lbl_ModeSW.Text = "";
                    lbl_IDSW.Text = "";

                    for (byte i=0; i < 4; i++)
                    {
                        for (byte j=0; j < 8; j++)
                        {
                            if (lbl_Link[i,j] != null)
                            {
                                lbl_Link[i, j].BackColor = System.Drawing.Color.Gray;
                            }
                        }
                    }
                }
            }
        }

        #endregion

       
    }
}

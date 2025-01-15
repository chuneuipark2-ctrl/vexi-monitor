using System;
using System.Net;
using System.Windows.Forms;
using System.Runtime.InteropServices;   //DllImport

namespace VEXI
{
    public partial class Form_SRMInvertorParam : Form
    {

        public Form_Main form_Main;
        private VEXI_DEFS.TSRM_InvertorParamReq srm_InvertorParamReq;
        private static VEXI_DEFS.TSRM_InvertorParamResCtrl srm_InvertorParamResCtrl;
        private static VEXI_DEFS.TSRM_InvertorParamCtrlRes srm_InvertorParamCtrlRes;

        public Form_SRMInvertorParam()
        {
            InitializeComponent();
        }

        private void Form_SRMConfig_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }

            cbParameter.SelectedIndex = 0;
        }

        public unsafe void Display_InvertorParamSt(byte[] Data)
        {
            srm_InvertorParamResCtrl = (VEXI_DEFS.TSRM_InvertorParamResCtrl)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TSRM_InvertorParamResCtrl));
            
            
            switch (srm_InvertorParamResCtrl.Addr_Main)
            {
                case 8404:
                    lblSt.Text = string.Format("{0:0.000}", (double)srm_InvertorParamResCtrl.U_Value / 1000);
                    break;
                case 8510:
                    switch (srm_InvertorParamResCtrl.DevType)
                    {
                        case 1:
                            lblSt.Text = string.Format("{0:0.000000}", (double)srm_InvertorParamResCtrl.U_Value / 1000000);
                            break;
                        case 2:
                            lblSt.Text = string.Format("{0:0.00000}", (double)srm_InvertorParamResCtrl.U_Value / 100000);
                            break;
                        case 3:
                            lblSt.Text = string.Format("{0:0.000000}", (double)srm_InvertorParamResCtrl.U_Value / 1000000);
                            break;
                    }
                    break;
                case 8550:
                    lblSt.Text = string.Format("{0:0}", srm_InvertorParamResCtrl.U_Value);
                    break;
                case 8357:
                    lblSt.Text = string.Format("{0:0.0}", (double)srm_InvertorParamResCtrl.U_Value / 10);
                    break;
                case 8334:
                    lblSt.Text = string.Format("{0:0}", srm_InvertorParamResCtrl.U_Value);
                    break;
                case 8335:
                    lblSt.Text = string.Format("{0:0}", srm_InvertorParamResCtrl.U_Value);
                    break;
                default: lblSt.Text = "---"; break;

            }

            if (edCtrl.Text == "---") edCtrl.Text = lblSt.Text;
        }


        public unsafe void Display_InvertorParamCtrl(byte[] Data)
        {
            srm_InvertorParamCtrlRes = (VEXI_DEFS.TSRM_InvertorParamCtrlRes)Global_Class.UTIL_BytesToStructure(Data, typeof(VEXI_DEFS.TSRM_InvertorParamCtrlRes));


            switch (srm_InvertorParamCtrlRes.CtrlResult)
            {
                case 0:
                    MessageBox.Show("제어성공");
                    break;
                default:
                    MessageBox.Show("제어성공");
                    break;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            lblSt.Text = "---";

            srm_InvertorParamReq.DevType = 1;
            switch (cbParameter.SelectedIndex)
            {
                 case 0: srm_InvertorParamReq.Invertor_Index = 1; srm_InvertorParamReq.Addr_Main = 8404; srm_InvertorParamReq.Addr_Sub =  4; break;//[주행] Speed P Gain(단위: 1 / s / 정밀도 : 0.001 1 / s)
                 case 1: srm_InvertorParamReq.Invertor_Index = 1; srm_InvertorParamReq.Addr_Main = 8404; srm_InvertorParamReq.Addr_Sub =  5; break;//[주행] Speed I Gain(단위: ms / 정밀도 : 0.001 ms)
                 case 2: srm_InvertorParamReq.Invertor_Index = 1; srm_InvertorParamReq.Addr_Main = 8510; srm_InvertorParamReq.Addr_Sub =  4; break;//[주행] Lag err(단위 : M / 정밀도 : 0.001 M)
                 case 3: srm_InvertorParamReq.Invertor_Index = 1; srm_InvertorParamReq.Addr_Main = 8550; srm_InvertorParamReq.Addr_Sub =  2; break;//[주행] Speed monitoring(단위 : ms / 정밀도 : 1ms)
                 case 4: srm_InvertorParamReq.Invertor_Index = 1; srm_InvertorParamReq.Addr_Main = 8357; srm_InvertorParamReq.Addr_Sub = 15; break;//[주행] Motor Torque(단위 : % / 정밀도 : 0.1 %)
                 case 5: srm_InvertorParamReq.Invertor_Index = 1; srm_InvertorParamReq.Addr_Main = 8334; srm_InvertorParamReq.Addr_Sub = 11; break;//[주행] DI01(정수값)
                 case 6: srm_InvertorParamReq.Invertor_Index = 1; srm_InvertorParamReq.Addr_Main = 8334; srm_InvertorParamReq.Addr_Sub = 12; break;//[주행] DI02(정수값)
                 case 7: srm_InvertorParamReq.Invertor_Index = 1; srm_InvertorParamReq.Addr_Main = 8334; srm_InvertorParamReq.Addr_Sub = 13; break;//[주행] DI03(정수값)
                 case 8: srm_InvertorParamReq.Invertor_Index = 1; srm_InvertorParamReq.Addr_Main = 8335; srm_InvertorParamReq.Addr_Sub = 10; break;//[주행] DO01(정수값)
                 case 9: srm_InvertorParamReq.Invertor_Index = 1; srm_InvertorParamReq.Addr_Main = 8335; srm_InvertorParamReq.Addr_Sub = 11; break;//[주행] DO02(정수값)
                 case 10: srm_InvertorParamReq.Invertor_Index = 1; srm_InvertorParamReq.Addr_Main = 8335; srm_InvertorParamReq.Addr_Sub = 12; break;//[주행] DO03(정수값)
                 case 11: srm_InvertorParamReq.Invertor_Index = 2; srm_InvertorParamReq.Addr_Main = 8404; srm_InvertorParamReq.Addr_Sub = 4; break;//[승강] Speed P Gain(단위: 1 / s / 정밀도 : 0.001 1 / s)
                 case 12: srm_InvertorParamReq.Invertor_Index = 2; srm_InvertorParamReq.Addr_Main = 8404; srm_InvertorParamReq.Addr_Sub =  5; break;//[승강] Speed I Gain(단위: ms / 정밀도 : 0.001 ms)
                 case 13: srm_InvertorParamReq.Invertor_Index = 2; srm_InvertorParamReq.Addr_Main = 8510; srm_InvertorParamReq.Addr_Sub =  4; break;//[승강] Lag err(단위 : M / 정밀도 : 0.001 M)
                 case 14: srm_InvertorParamReq.Invertor_Index = 2; srm_InvertorParamReq.Addr_Main = 8550; srm_InvertorParamReq.Addr_Sub =  2; break;//[승강] Speed monitoring(단위 : ms / 정밀도 : 1ms)
                 case 15: srm_InvertorParamReq.Invertor_Index = 2; srm_InvertorParamReq.Addr_Main = 8357; srm_InvertorParamReq.Addr_Sub = 15; break;//[승강] Motor Torque(단위 : % / 정밀도 : 0.1 %)
                 case 16: srm_InvertorParamReq.Invertor_Index = 2; srm_InvertorParamReq.Addr_Main = 8334; srm_InvertorParamReq.Addr_Sub = 11; break;//[승강] DI01(정수값)
                 case 17: srm_InvertorParamReq.Invertor_Index = 2; srm_InvertorParamReq.Addr_Main = 8334; srm_InvertorParamReq.Addr_Sub = 12; break;//[승강] DI02(정수값)
                 case 18: srm_InvertorParamReq.Invertor_Index = 2; srm_InvertorParamReq.Addr_Main = 8334; srm_InvertorParamReq.Addr_Sub = 13; break;//[승강] DI03(정수값)
                 case 19: srm_InvertorParamReq.Invertor_Index = 2; srm_InvertorParamReq.Addr_Main = 8335; srm_InvertorParamReq.Addr_Sub = 10; break;//[승강] DO01(정수값)
                 case 20: srm_InvertorParamReq.Invertor_Index = 2; srm_InvertorParamReq.Addr_Main = 8335; srm_InvertorParamReq.Addr_Sub = 11; break;//[승강] DO02(정수값)
                 case 21: srm_InvertorParamReq.Invertor_Index = 2; srm_InvertorParamReq.Addr_Main = 8335; srm_InvertorParamReq.Addr_Sub = 12; break;//[승강] DO03(정수값)
                 case 22: srm_InvertorParamReq.Invertor_Index = 3; srm_InvertorParamReq.Addr_Main = 8404; srm_InvertorParamReq.Addr_Sub = 4; break;//[포크] Speed P Gain(단위: 1 / s / 정밀도 : 0.001 1 / s)
                 case 23: srm_InvertorParamReq.Invertor_Index = 3; srm_InvertorParamReq.Addr_Main = 8404; srm_InvertorParamReq.Addr_Sub =  5; break;//[포크] Speed I Gain(단위: ms / 정밀도 : 0.001 ms)
                 case 24: srm_InvertorParamReq.Invertor_Index = 3; srm_InvertorParamReq.Addr_Main = 8510; srm_InvertorParamReq.Addr_Sub =  4; break;//[포크] Lag err(단위 : M / 정밀도 : 0.001 M)
                 case 25: srm_InvertorParamReq.Invertor_Index = 3; srm_InvertorParamReq.Addr_Main = 8550; srm_InvertorParamReq.Addr_Sub =  2; break;//[포크] Speed monitoring(단위 : ms / 정밀도 : 1ms)
                 case 26: srm_InvertorParamReq.Invertor_Index = 3; srm_InvertorParamReq.Addr_Main = 8357; srm_InvertorParamReq.Addr_Sub = 15; break;//[포크] Motor Torque(단위 : % / 정밀도 : 0.1 %)
                 case 27: srm_InvertorParamReq.Invertor_Index = 3; srm_InvertorParamReq.Addr_Main = 8334; srm_InvertorParamReq.Addr_Sub = 11; break;//[포크] DI01(정수값)
                 case 28: srm_InvertorParamReq.Invertor_Index = 3; srm_InvertorParamReq.Addr_Main = 8334; srm_InvertorParamReq.Addr_Sub = 12; break;//[포크] DI02(정수값)
                 case 29: srm_InvertorParamReq.Invertor_Index = 3; srm_InvertorParamReq.Addr_Main = 8334; srm_InvertorParamReq.Addr_Sub = 13; break;//[포크] DI03(정수값)
                 case 30: srm_InvertorParamReq.Invertor_Index = 3; srm_InvertorParamReq.Addr_Main = 8335; srm_InvertorParamReq.Addr_Sub = 10; break;//[포크] DO01(정수값)
                 case 31: srm_InvertorParamReq.Invertor_Index = 3; srm_InvertorParamReq.Addr_Main = 8335; srm_InvertorParamReq.Addr_Sub = 11; break;//[포크] DO02(정수값)
                 case 32: srm_InvertorParamReq.Invertor_Index = 3; srm_InvertorParamReq.Addr_Main = 8335; srm_InvertorParamReq.Addr_Sub = 12; break;//[포크] DO03(정수값)
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_90, srm_InvertorParamReq);

            

        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            srm_InvertorParamResCtrl.DevType = 1;
            switch (cbParameter.SelectedIndex)
            {
                case 0: srm_InvertorParamResCtrl.Invertor_Index = 1; srm_InvertorParamResCtrl.Addr_Main = 8404; srm_InvertorParamResCtrl.Addr_Sub = 4; break;//[주행] Speed P Gain(단위: 1 / s / 정밀도 : 0.001 1 / s)
                case 1: srm_InvertorParamResCtrl.Invertor_Index = 1; srm_InvertorParamResCtrl.Addr_Main = 8404; srm_InvertorParamResCtrl.Addr_Sub = 5; break;//[주행] Speed I Gain(단위: ms / 정밀도 : 0.001 ms)
                case 2: srm_InvertorParamResCtrl.Invertor_Index = 1; srm_InvertorParamResCtrl.Addr_Main = 8510; srm_InvertorParamResCtrl.Addr_Sub = 4; break;//[주행] Lag err(단위 : M / 정밀도 : 0.001 M)
                case 3: srm_InvertorParamResCtrl.Invertor_Index = 1; srm_InvertorParamResCtrl.Addr_Main = 8550; srm_InvertorParamResCtrl.Addr_Sub = 2; break;//[주행] Speed monitoring(단위 : ms / 정밀도 : 1ms)
                case 4: srm_InvertorParamResCtrl.Invertor_Index = 1; srm_InvertorParamResCtrl.Addr_Main = 8357; srm_InvertorParamResCtrl.Addr_Sub = 15; break;//[주행] Motor Torque(단위 : % / 정밀도 : 0.1 %)
                case 5: srm_InvertorParamResCtrl.Invertor_Index = 1; srm_InvertorParamResCtrl.Addr_Main = 8334; srm_InvertorParamResCtrl.Addr_Sub = 11; break;//[주행] DI01(정수값)
                case 6: srm_InvertorParamResCtrl.Invertor_Index = 1; srm_InvertorParamResCtrl.Addr_Main = 8334; srm_InvertorParamResCtrl.Addr_Sub = 12; break;//[주행] DI02(정수값)
                case 7: srm_InvertorParamResCtrl.Invertor_Index = 1; srm_InvertorParamResCtrl.Addr_Main = 8334; srm_InvertorParamResCtrl.Addr_Sub = 13; break;//[주행] DI03(정수값)
                case 8: srm_InvertorParamResCtrl.Invertor_Index = 1; srm_InvertorParamResCtrl.Addr_Main = 8335; srm_InvertorParamResCtrl.Addr_Sub = 10; break;//[주행] DO01(정수값)
                case 9: srm_InvertorParamResCtrl.Invertor_Index = 1; srm_InvertorParamResCtrl.Addr_Main = 8335; srm_InvertorParamResCtrl.Addr_Sub = 11; break;//[주행] DO02(정수값)
                case 10: srm_InvertorParamResCtrl.Invertor_Index = 1; srm_InvertorParamResCtrl.Addr_Main = 8335; srm_InvertorParamResCtrl.Addr_Sub = 12; break;//[주행] DO03(정수값)
                case 11: srm_InvertorParamResCtrl.Invertor_Index = 2; srm_InvertorParamResCtrl.Addr_Main = 8404; srm_InvertorParamResCtrl.Addr_Sub = 4; break;//[승강] Speed P Gain(단위: 1 / s / 정밀도 : 0.001 1 / s)
                case 12: srm_InvertorParamResCtrl.Invertor_Index = 2; srm_InvertorParamResCtrl.Addr_Main = 8404; srm_InvertorParamResCtrl.Addr_Sub = 5; break;//[승강] Speed I Gain(단위: ms / 정밀도 : 0.001 ms)
                case 13: srm_InvertorParamResCtrl.Invertor_Index = 2; srm_InvertorParamResCtrl.Addr_Main = 8510; srm_InvertorParamResCtrl.Addr_Sub = 4; break;//[승강] Lag err(단위 : M / 정밀도 : 0.001 M)
                case 14: srm_InvertorParamResCtrl.Invertor_Index = 2; srm_InvertorParamResCtrl.Addr_Main = 8550; srm_InvertorParamResCtrl.Addr_Sub = 2; break;//[승강] Speed monitoring(단위 : ms / 정밀도 : 1ms)
                case 15: srm_InvertorParamResCtrl.Invertor_Index = 2; srm_InvertorParamResCtrl.Addr_Main = 8357; srm_InvertorParamResCtrl.Addr_Sub = 15; break;//[승강] Motor Torque(단위 : % / 정밀도 : 0.1 %)
                case 16: srm_InvertorParamResCtrl.Invertor_Index = 2; srm_InvertorParamResCtrl.Addr_Main = 8334; srm_InvertorParamResCtrl.Addr_Sub = 11; break;//[승강] DI01(정수값)
                case 17: srm_InvertorParamResCtrl.Invertor_Index = 2; srm_InvertorParamResCtrl.Addr_Main = 8334; srm_InvertorParamResCtrl.Addr_Sub = 12; break;//[승강] DI02(정수값)
                case 18: srm_InvertorParamResCtrl.Invertor_Index = 2; srm_InvertorParamResCtrl.Addr_Main = 8334; srm_InvertorParamResCtrl.Addr_Sub = 13; break;//[승강] DI03(정수값)
                case 19: srm_InvertorParamResCtrl.Invertor_Index = 2; srm_InvertorParamResCtrl.Addr_Main = 8335; srm_InvertorParamResCtrl.Addr_Sub = 10; break;//[승강] DO01(정수값)
                case 20: srm_InvertorParamResCtrl.Invertor_Index = 2; srm_InvertorParamResCtrl.Addr_Main = 8335; srm_InvertorParamResCtrl.Addr_Sub = 11; break;//[승강] DO02(정수값)
                case 21: srm_InvertorParamResCtrl.Invertor_Index = 2; srm_InvertorParamResCtrl.Addr_Main = 8335; srm_InvertorParamResCtrl.Addr_Sub = 12; break;//[승강] DO03(정수값)
                case 22: srm_InvertorParamResCtrl.Invertor_Index = 3; srm_InvertorParamResCtrl.Addr_Main = 8404; srm_InvertorParamResCtrl.Addr_Sub = 4; break;//[포크] Speed P Gain(단위: 1 / s / 정밀도 : 0.001 1 / s)
                case 23: srm_InvertorParamResCtrl.Invertor_Index = 3; srm_InvertorParamResCtrl.Addr_Main = 8404; srm_InvertorParamResCtrl.Addr_Sub = 5; break;//[포크] Speed I Gain(단위: ms / 정밀도 : 0.001 ms)
                case 24: srm_InvertorParamResCtrl.Invertor_Index = 3; srm_InvertorParamResCtrl.Addr_Main = 8510; srm_InvertorParamResCtrl.Addr_Sub = 4; break;//[포크] Lag err(단위 : M / 정밀도 : 0.001 M)
                case 25: srm_InvertorParamResCtrl.Invertor_Index = 3; srm_InvertorParamResCtrl.Addr_Main = 8550; srm_InvertorParamResCtrl.Addr_Sub = 2; break;//[포크] Speed monitoring(단위 : ms / 정밀도 : 1ms)
                case 26: srm_InvertorParamResCtrl.Invertor_Index = 3; srm_InvertorParamResCtrl.Addr_Main = 8357; srm_InvertorParamResCtrl.Addr_Sub = 15; break;//[포크] Motor Torque(단위 : % / 정밀도 : 0.1 %)
                case 27: srm_InvertorParamResCtrl.Invertor_Index = 3; srm_InvertorParamResCtrl.Addr_Main = 8334; srm_InvertorParamResCtrl.Addr_Sub = 11; break;//[포크] DI01(정수값)
                case 28: srm_InvertorParamResCtrl.Invertor_Index = 3; srm_InvertorParamResCtrl.Addr_Main = 8334; srm_InvertorParamResCtrl.Addr_Sub = 12; break;//[포크] DI02(정수값)
                case 29: srm_InvertorParamResCtrl.Invertor_Index = 3; srm_InvertorParamResCtrl.Addr_Main = 8334; srm_InvertorParamResCtrl.Addr_Sub = 13; break;//[포크] DI03(정수값)
                case 30: srm_InvertorParamResCtrl.Invertor_Index = 3; srm_InvertorParamResCtrl.Addr_Main = 8335; srm_InvertorParamResCtrl.Addr_Sub = 10; break;//[포크] DO01(정수값)
                case 31: srm_InvertorParamResCtrl.Invertor_Index = 3; srm_InvertorParamResCtrl.Addr_Main = 8335; srm_InvertorParamResCtrl.Addr_Sub = 11; break;//[포크] DO02(정수값)
                case 32: srm_InvertorParamResCtrl.Invertor_Index = 3; srm_InvertorParamResCtrl.Addr_Main = 8335; srm_InvertorParamResCtrl.Addr_Sub = 12; break;//[포크] DO03(정수값)
            }

            switch (srm_InvertorParamResCtrl.Addr_Main)
            {
                case 8404:
                    srm_InvertorParamResCtrl.U_Value = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(edCtrl.Text, 0) * 1000);
                    break;
                case 8510:
                    switch (srm_InvertorParamResCtrl.DevType)
                    {
                        case 1: srm_InvertorParamResCtrl.U_Value = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(edCtrl.Text, 0) * 1000000); break;
                        case 2: srm_InvertorParamResCtrl.U_Value = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(edCtrl.Text, 0) * 100000); break;
                        case 3: srm_InvertorParamResCtrl.U_Value = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(edCtrl.Text, 0) * 1000000); break;
                    }
                    
                    break;
                case 8550:
                    srm_InvertorParamResCtrl.U_Value = (UInt16)Global_Class.UTIL_StrToIntDef(edCtrl.Text, 0);
                    break;
                case 8357:
                    srm_InvertorParamResCtrl.U_Value = (UInt16)Math.Round(Global_Class.UTIL_StrToFloatDef(edCtrl.Text, 0) * 10);
                    break;
                case 8334:
                    srm_InvertorParamResCtrl.U_Value = (UInt16)Global_Class.UTIL_StrToIntDef(edCtrl.Text, 0);
                    break;
                case 8335:
                    srm_InvertorParamResCtrl.U_Value = (UInt16)Global_Class.UTIL_StrToIntDef(edCtrl.Text, 0);
                    break;
                default: srm_InvertorParamResCtrl.U_Value = 0; break;

            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_91, srm_InvertorParamResCtrl);
        }

        private void lblSt_DoubleClick(object sender, EventArgs e)
        {
            edCtrl.Text = lblSt.Text;
                 
        }

        private void cbParameter_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSt.Text = "---";
            edCtrl.Text = "---";
        }
    }
}

using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_RTVInvertorSt : Form
    {

        public Form_Main form_Main;
        private static VEXI_DEFS.TRTV_REC_InvertorRes dev_REC_Invertor;


        private static Label[] lbl_INV_St_11;
        private static Label[] lbl_INV_St_10;
        private static Label[] lbl_INV_St_9;
        private static Label[] lbl_INV_St_7;
        private static Label[] lbl_INV_St_6;
        private static Label[] lbl_INV_St_5;
        private static Label[] lbl_INV_St_4;
        private static Label[] lbl_INV_St_3;
        private static Label[] lbl_INV_St_2;
        private static Label[] lbl_INV_St_1;
        private static Label[] lbl_INV_St_0;
        private static Label[] lbl_INV_St_ActualSpeed;
        private static Label[] lbl_INV_St_AccDeltaTime;
        private static Label[] lbl_INV_St_DecDeltaTime;
        private static Label[] lbl_INV_St_OperationMode;

        private static Label[] lbl_INV_Ctrl_8;
        private static Label[] lbl_INV_Ctrl_7;
        private static Label[] lbl_INV_Ctrl_6;
        private static Label[] lbl_INV_Ctrl_5;
        private static Label[] lbl_INV_Ctrl_4;
        private static Label[] lbl_INV_Ctrl_3;
        private static Label[] lbl_INV_Ctrl_2;
        private static Label[] lbl_INV_Ctrl_1;
        private static Label[] lbl_INV_Ctrl_0;
        private static Label[] lbl_INV_Ctrl_DemandVelocity;
        private static Label[] lbl_INV_Ctrl_ActualVelocity;
        private static Label[] lbl_INV_Ctrl_ActualTorgue;
        private static Label[] lbl_INV_Ctrl_Setpointdeceleration;
        private static Label[] lbl_INV_Ctrl_ErrorCode;
        private static Label[] lbl_INV_Ctrl_OperationMode;
        
        public Form_RTVInvertorSt()
        {
            InitializeComponent();

            lbl_INV_St_11 = new Label[] { lbl_INV1_St_11, lbl_INV2_St_11, lbl_INV3_St_11, lbl_INV4_St_11, lbl_INV5_St_11, lbl_INV6_St_11 };
            lbl_INV_St_10 = new Label[] { lbl_INV1_St_10, lbl_INV2_St_10, lbl_INV3_St_10, lbl_INV4_St_10, lbl_INV5_St_10, lbl_INV6_St_10 };
            lbl_INV_St_9 = new Label[] { lbl_INV1_St_9, lbl_INV2_St_9, lbl_INV3_St_9, lbl_INV4_St_9, lbl_INV5_St_9, lbl_INV6_St_9 };
            lbl_INV_St_7 = new Label[] { lbl_INV1_St_7, lbl_INV2_St_7, lbl_INV3_St_7, lbl_INV4_St_7, lbl_INV5_St_7, lbl_INV6_St_7 };
            lbl_INV_St_6 = new Label[] { lbl_INV1_St_6, lbl_INV2_St_6, lbl_INV3_St_6, lbl_INV4_St_6, lbl_INV5_St_6, lbl_INV6_St_6 };
            lbl_INV_St_5 = new Label[] { lbl_INV1_St_5, lbl_INV2_St_5, lbl_INV3_St_5, lbl_INV4_St_5, lbl_INV5_St_5, lbl_INV6_St_5 };
            lbl_INV_St_4 = new Label[] { lbl_INV1_St_4, lbl_INV2_St_4, lbl_INV3_St_4, lbl_INV4_St_4, lbl_INV5_St_4, lbl_INV6_St_4 };
            lbl_INV_St_3 = new Label[] { lbl_INV1_St_3, lbl_INV2_St_3, lbl_INV3_St_3, lbl_INV4_St_3, lbl_INV5_St_3, lbl_INV6_St_3 };
            lbl_INV_St_2 = new Label[] { lbl_INV1_St_2, lbl_INV2_St_2, lbl_INV3_St_2, lbl_INV4_St_2, lbl_INV5_St_2, lbl_INV6_St_2 };
            lbl_INV_St_1 = new Label[] { lbl_INV1_St_1, lbl_INV2_St_1, lbl_INV3_St_1, lbl_INV4_St_1, lbl_INV5_St_1, lbl_INV6_St_1 };
            lbl_INV_St_0 = new Label[] { lbl_INV1_St_0, lbl_INV2_St_0, lbl_INV3_St_0, lbl_INV4_St_0, lbl_INV5_St_0, lbl_INV6_St_0 };
            lbl_INV_St_ActualSpeed = new Label[] { lbl_INV1_St_ActualSpeed, lbl_INV2_St_ActualSpeed, lbl_INV3_St_ActualSpeed, lbl_INV4_St_ActualSpeed, lbl_INV5_St_ActualSpeed, lbl_INV6_St_ActualSpeed };
            lbl_INV_St_AccDeltaTime = new Label[] { lbl_INV1_St_AccDeltaTime, lbl_INV2_St_AccDeltaTime, lbl_INV3_St_AccDeltaTime, lbl_INV4_St_AccDeltaTime, lbl_INV5_St_AccDeltaTime, lbl_INV6_St_AccDeltaTime };
            lbl_INV_St_DecDeltaTime = new Label[] { lbl_INV1_St_DecDeltaTime, lbl_INV2_St_DecDeltaTime, lbl_INV3_St_DecDeltaTime, lbl_INV4_St_DecDeltaTime, lbl_INV5_St_DecDeltaTime, lbl_INV6_St_DecDeltaTime };
            lbl_INV_St_OperationMode = new Label[] { lbl_INV1_St_OperationMode, lbl_INV2_St_OperationMode, lbl_INV3_St_OperationMode, lbl_INV4_St_OperationMode, lbl_INV5_St_OperationMode, lbl_INV6_St_OperationMode };

            lbl_INV_Ctrl_8 = new Label[] { lbl_INV1_Ctrl_8, lbl_INV2_Ctrl_8, lbl_INV3_Ctrl_8, lbl_INV4_Ctrl_8, lbl_INV5_Ctrl_8, lbl_INV6_Ctrl_8 };
            lbl_INV_Ctrl_7 = new Label[] { lbl_INV1_Ctrl_7, lbl_INV2_Ctrl_7, lbl_INV3_Ctrl_7, lbl_INV4_Ctrl_7, lbl_INV5_Ctrl_7, lbl_INV6_Ctrl_7 };
            lbl_INV_Ctrl_6 = new Label[] { lbl_INV1_Ctrl_6, lbl_INV2_Ctrl_6, lbl_INV3_Ctrl_6, lbl_INV4_Ctrl_6, lbl_INV5_Ctrl_6, lbl_INV6_Ctrl_6 };
            lbl_INV_Ctrl_5 = new Label[] { lbl_INV1_Ctrl_5,lbl_INV2_Ctrl_5, lbl_INV3_Ctrl_5, lbl_INV4_Ctrl_5, lbl_INV5_Ctrl_5, lbl_INV6_Ctrl_5 };
            lbl_INV_Ctrl_4 = new Label[] { lbl_INV1_Ctrl_4, lbl_INV2_Ctrl_4, lbl_INV3_Ctrl_4, lbl_INV4_Ctrl_4, lbl_INV5_Ctrl_4, lbl_INV6_Ctrl_4 };
            lbl_INV_Ctrl_3 = new Label[] { lbl_INV1_Ctrl_3, lbl_INV2_Ctrl_3, lbl_INV3_Ctrl_3, lbl_INV4_Ctrl_3, lbl_INV5_Ctrl_3, lbl_INV6_Ctrl_3};
            lbl_INV_Ctrl_2 = new Label[] { lbl_INV1_Ctrl_2, lbl_INV2_Ctrl_2, lbl_INV3_Ctrl_2, lbl_INV4_Ctrl_2, lbl_INV5_Ctrl_2, lbl_INV6_Ctrl_2};
            lbl_INV_Ctrl_1 = new Label[] { lbl_INV1_Ctrl_1, lbl_INV2_Ctrl_1, lbl_INV3_Ctrl_1, lbl_INV4_Ctrl_1, lbl_INV5_Ctrl_1, lbl_INV6_Ctrl_1 };
            lbl_INV_Ctrl_0 = new Label[] { lbl_INV1_Ctrl_0, lbl_INV2_Ctrl_0, lbl_INV3_Ctrl_0, lbl_INV4_Ctrl_0, lbl_INV5_Ctrl_0, lbl_INV6_Ctrl_0 };
            lbl_INV_Ctrl_DemandVelocity = new Label[] { lbl_INV1_Ctrl_DemandVelocity, lbl_INV2_Ctrl_DemandVelocity, lbl_INV3_Ctrl_DemandVelocity, lbl_INV4_Ctrl_DemandVelocity, lbl_INV5_Ctrl_DemandVelocity, lbl_INV6_Ctrl_DemandVelocity };
            lbl_INV_Ctrl_ActualVelocity = new Label[] { lbl_INV1_Ctrl_ActualVelocity , lbl_INV2_Ctrl_ActualVelocity , lbl_INV3_Ctrl_ActualVelocity , lbl_INV4_Ctrl_ActualVelocity , lbl_INV5_Ctrl_ActualVelocity , lbl_INV6_Ctrl_ActualVelocity };
            lbl_INV_Ctrl_ActualTorgue = new Label[] { lbl_INV1_Ctrl_ActualTorgue , lbl_INV2_Ctrl_ActualTorgue , lbl_INV3_Ctrl_ActualTorgue , lbl_INV4_Ctrl_ActualTorgue , lbl_INV5_Ctrl_ActualTorgue , lbl_INV6_Ctrl_ActualTorgue };
            lbl_INV_Ctrl_Setpointdeceleration = new Label[] { lbl_INV1_Ctrl_Setpointdeceleration , lbl_INV2_Ctrl_Setpointdeceleration , lbl_INV3_Ctrl_Setpointdeceleration , lbl_INV4_Ctrl_Setpointdeceleration , lbl_INV5_Ctrl_Setpointdeceleration , lbl_INV6_Ctrl_Setpointdeceleration };
            lbl_INV_Ctrl_ErrorCode = new Label[] { lbl_INV1_Ctrl_ErrorCode , lbl_INV2_Ctrl_ErrorCode , lbl_INV3_Ctrl_ErrorCode , lbl_INV4_Ctrl_ErrorCode , lbl_INV5_Ctrl_ErrorCode , lbl_INV6_Ctrl_ErrorCode };
            lbl_INV_Ctrl_OperationMode = new Label[] { lbl_INV1_Ctrl_OperationMode , lbl_INV2_Ctrl_OperationMode , lbl_INV3_Ctrl_OperationMode , lbl_INV4_Ctrl_OperationMode , lbl_INV5_Ctrl_OperationMode , lbl_INV6_Ctrl_OperationMode };

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
            form_Main.COMMDataManager.ISInvertorPolling = true;
        }

        private void Form_InvertorSt_Deactivate(object sender, EventArgs e)
        {
            form_Main.COMMDataManager.ISInvertorPolling = false;
        }
        #endregion

        #region 기능함수
        public void Display_Init()
        {
            lblInvertorType.Text = "";

            for (byte i = 0; i < 6; i++)
            {
                lbl_INV_St_11[i].Text = "";
                lbl_INV_St_10[i].Text = "";
                lbl_INV_St_9[i].Text = "";
                lbl_INV_St_7[i].Text = "";
                lbl_INV_St_6[i].Text = "";
                lbl_INV_St_5[i].Text = "";
                lbl_INV_St_4[i].Text = "";
                lbl_INV_St_3[i].Text = "";
                lbl_INV_St_2[i].Text = "";
                lbl_INV_St_1[i].Text = "";
                lbl_INV_St_0[i].Text = "";
                lbl_INV_St_ActualSpeed[i].Text = "";
                lbl_INV_St_AccDeltaTime[i].Text = "";
                lbl_INV_St_DecDeltaTime[i].Text = "";
                lbl_INV_St_OperationMode[i].Text = "";

                lbl_INV_Ctrl_8[i].Text = "";
                lbl_INV_Ctrl_7[i].Text = "";
                lbl_INV_Ctrl_6[i].Text = "";
                lbl_INV_Ctrl_5[i].Text = "";
                lbl_INV_Ctrl_4[i].Text = "";
                lbl_INV_Ctrl_3[i].Text = "";
                lbl_INV_Ctrl_2[i].Text = "";
                lbl_INV_Ctrl_1[i].Text = "";
                lbl_INV_Ctrl_0[i].Text = "";
                lbl_INV_Ctrl_DemandVelocity[i].Text = "";
                lbl_INV_Ctrl_ActualVelocity[i].Text = "";
                lbl_INV_Ctrl_ActualTorgue[i].Text = "";
                lbl_INV_Ctrl_Setpointdeceleration[i].Text = "";
                lbl_INV_Ctrl_ErrorCode[i].Text = "";
                lbl_INV_Ctrl_OperationMode[i].Text = "";
            }
        }

        //상태데이터 화면 표출 함수
        public unsafe void Display_InvertorSt(byte[] datas)
        {
            dev_REC_Invertor = (VEXI_DEFS.TRTV_REC_InvertorRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TRTV_REC_InvertorRes));

            switch (dev_REC_Invertor.InvertorType)
            {
                case 1: lblInvertorType.Text = "ABB"; break;
                case 2: lblInvertorType.Text = "SEW"; break;
                default: lblInvertorType.Text = ""; break;
            }

            fixed (VEXI_DEFS.TRTV_InvertorStRec* Ptr_1 = &dev_REC_Invertor.Invertor_1_St)
            {
                for (byte i = 0; i < 6; i++)
                {
                    if (Global_Class.BitStatus((Ptr_1 + i)->St_bit, 11)) lbl_INV_St_11[i].Text = "1"; else lbl_INV_St_11[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->St_bit, 10)) lbl_INV_St_10[i].Text = "1"; else lbl_INV_St_10[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->St_bit, 9)) lbl_INV_St_9[i].Text = "1"; else lbl_INV_St_9[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->St_bit, 7)) lbl_INV_St_7[i].Text = "1"; else lbl_INV_St_7[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->St_bit, 6)) lbl_INV_St_6[i].Text = "1"; else lbl_INV_St_6[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->St_bit, 5)) lbl_INV_St_5[i].Text = "1"; else lbl_INV_St_5[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->St_bit, 4)) lbl_INV_St_4[i].Text = "1"; else lbl_INV_St_4[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->St_bit, 3)) lbl_INV_St_3[i].Text = "1"; else lbl_INV_St_3[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->St_bit, 2)) lbl_INV_St_2[i].Text = "1"; else lbl_INV_St_2[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->St_bit, 1)) lbl_INV_St_1[i].Text = "1"; else lbl_INV_St_1[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->St_bit, 0)) lbl_INV_St_0[i].Text = "1"; else lbl_INV_St_0[i].Text = "0";

                    lbl_INV_St_ActualSpeed[i].Text = string.Format("{0}", (Ptr_1 + i)->St_Speed);
                    lbl_INV_St_AccDeltaTime[i].Text = string.Format("{0}", (Ptr_1 + i)->St_AccDeltaTime);
                    lbl_INV_St_DecDeltaTime[i].Text = string.Format("{0}", (Ptr_1 + i)->St_DecDeltaTime);
                    lbl_INV_St_OperationMode[i].Text = string.Format("{0}", (Ptr_1 + i)->St_OperationMode);
                }
            }

            fixed (VEXI_DEFS.TRTV_InvertorCtrlRec* Ptr_1 = &dev_REC_Invertor.Invertor_1_Ctrl)
            {
                for (byte i = 0; i < 6; i++)
                {
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl_bit, 8)) lbl_INV_Ctrl_8[i].Text = "1"; else lbl_INV_Ctrl_8[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl_bit, 7)) lbl_INV_Ctrl_7[i].Text = "1"; else lbl_INV_Ctrl_7[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl_bit, 6)) lbl_INV_Ctrl_6[i].Text = "1"; else lbl_INV_Ctrl_6[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl_bit, 5)) lbl_INV_Ctrl_5[i].Text = "1"; else lbl_INV_Ctrl_5[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl_bit, 4)) lbl_INV_Ctrl_4[i].Text = "1"; else lbl_INV_Ctrl_4[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl_bit, 3)) lbl_INV_Ctrl_3[i].Text = "1"; else lbl_INV_Ctrl_3[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl_bit, 2)) lbl_INV_Ctrl_2[i].Text = "1"; else lbl_INV_Ctrl_2[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl_bit, 1)) lbl_INV_Ctrl_1[i].Text = "1"; else lbl_INV_Ctrl_1[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl_bit, 0)) lbl_INV_Ctrl_0[i].Text = "1"; else lbl_INV_Ctrl_1[i].Text = "0";

                    lbl_INV_Ctrl_DemandVelocity[i].Text = string.Format("{0}", (Ptr_1 + i)->Ctrl_DemandVelocity);
                    lbl_INV_Ctrl_ActualVelocity[i].Text = string.Format("{0}", (Ptr_1 + i)->Ctrl_ActualVelocity);
                    lbl_INV_Ctrl_ActualTorgue[i].Text = string.Format("{0}", (Ptr_1 + i)->Ctrl_ActualTorgue);
                    lbl_INV_Ctrl_Setpointdeceleration[i].Text = string.Format("{0}", (Ptr_1 + i)->Ctrl_Setpoint_deceleration);
                    lbl_INV_Ctrl_ErrorCode[i].Text = string.Format("{0}", (Ptr_1 + i)->Ctrl_ErrorCode);
                    lbl_INV_Ctrl_OperationMode[i].Text = string.Format("{0}", (Ptr_1 + i)->Ctrl_OperationMode);
                }
            }
        }

        #endregion
    }
}

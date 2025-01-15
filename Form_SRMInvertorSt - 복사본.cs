using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_SRMInvertorSt : Form
    {

        public Form_Main form_Main;
        private static VEXI_DEFS.TSRM_REC_InvertorRes dev_REC_Invertor;


        private static Label[] lbl_INV_St_7;
        private static Label[] lbl_INV_St_5;
        private static Label[] lbl_INV_St_4;
        private static Label[] lbl_INV_St_3;
        private static Label[] lbl_INV_St_2;
        private static Label[] lbl_INV_St_1;
        private static Label[] lbl_INV_St_0;
        private static Label[] lbl_INV_St_Fault;
        private static Label[] lbl_INV_St_Position;
        private static Label[] lbl_INV_St_Speed;
        private static Label[] lbl_INV_St_torque;
        private static Label[] lbl_INV_St_MainErrCode;
        private static Label[] lbl_INV_St_SubErrCode;
        private static Label[] lbl_INV_St_BlockErrCode;
        private static Label[] lbl_INV_St_Output_Current;
        private static Label[] lbl_INV_St_DCLink_Volt;
        private static Label[] lbl_INV_St_radiation_Temp;
        private static Label[] lbl_INV_St_Motor_Temp;
        private static Label[] lbl_INV_St_Motor_Encoder;
        private static Label[] lbl_INV_St_Exteranl_Encoder;
        private static Label[] lbl_INV_St_Sum_energy_consumed;
        private static Label[] lbl_INV_St_Sum_regenerative_energy;
        private static Label[] lbl_INV_St_Actual_Reference_offset;
        private static Label[] lbl_INV_St_Actual_Position_gain;
        private static Label[] lbl_INV_St_Actual_Position_window;


        private static Label[] lbl_INV_Ctrl_15;
        private static Label[] lbl_INV_Ctrl_12;
        private static Label[] lbl_INV_Ctrl_11;
        private static Label[] lbl_INV_Ctrl_10;
        private static Label[] lbl_INV_Ctrl_9;
        private static Label[] lbl_INV_Ctrl_8;
        private static Label[] lbl_INV_Ctrl_7;
        private static Label[] lbl_INV_Ctrl_6;
        private static Label[] lbl_INV_Ctrl_5;
        private static Label[] lbl_INV_Ctrl_4;
        private static Label[] lbl_INV_Ctrl_3;
        private static Label[] lbl_INV_Ctrl_2;
        private static Label[] lbl_INV_Ctrl_1;
        private static Label[] lbl_INV_Ctrl_Position;
        private static Label[] lbl_INV_Ctrl_Speed;
        private static Label[] lbl_INV_Ctrl_Accel;
        private static Label[] lbl_INV_Ctrl_Decel;
        private static Label[] lbl_INV_Ctrl_JerkTime;
        private static Label[] lbl_INV_Ctrl_Reference_offset;
        private static Label[] lbl_INV_Ctrl_Position_gain;
        private static Label[] lbl_INV_Ctrl_Position_window;

        public Form_SRMInvertorSt()
        {
            InitializeComponent();

            lbl_INV_St_7 = new Label[] { lbl_INV1_St_7, lbl_INV2_St_7, lbl_INV3_St_7, lbl_INV4_St_7, lbl_INV5_St_7, lbl_INV6_St_7 };
            lbl_INV_St_5 = new Label[] { lbl_INV1_St_5, lbl_INV2_St_5, lbl_INV3_St_5, lbl_INV4_St_5, lbl_INV5_St_5, lbl_INV6_St_5 };
            lbl_INV_St_4 = new Label[] { lbl_INV1_St_4, lbl_INV2_St_4, lbl_INV3_St_4, lbl_INV4_St_4, lbl_INV5_St_4, lbl_INV6_St_4 };
            lbl_INV_St_3 = new Label[] { lbl_INV1_St_3, lbl_INV2_St_3, lbl_INV3_St_3, lbl_INV4_St_3, lbl_INV5_St_3, lbl_INV6_St_3 };
            lbl_INV_St_2 = new Label[] { lbl_INV1_St_2, lbl_INV2_St_2, lbl_INV3_St_2, lbl_INV4_St_2, lbl_INV5_St_2, lbl_INV6_St_2 };
            lbl_INV_St_1 = new Label[] { lbl_INV1_St_1, lbl_INV2_St_1, lbl_INV3_St_1, lbl_INV4_St_1, lbl_INV5_St_1, lbl_INV6_St_1 };
            lbl_INV_St_0 = new Label[] { lbl_INV1_St_0, lbl_INV2_St_0, lbl_INV3_St_0, lbl_INV4_St_0, lbl_INV5_St_0, lbl_INV6_St_0 };

            lbl_INV_St_Fault = new Label[] { lbl_INV1_St_Fault, lbl_INV2_St_Fault, lbl_INV3_St_Fault, lbl_INV4_St_Fault, lbl_INV5_St_Fault, lbl_INV6_St_Fault };
            lbl_INV_St_Position = new Label[] { lbl_INV1_St_Position , lbl_INV2_St_Position , lbl_INV3_St_Position , lbl_INV4_St_Position , lbl_INV5_St_Position , lbl_INV6_St_Position };
            lbl_INV_St_Speed = new Label[] { lbl_INV1_St_Speed , lbl_INV2_St_Speed , lbl_INV3_St_Speed , lbl_INV4_St_Speed , lbl_INV5_St_Speed , lbl_INV6_St_Speed };
            lbl_INV_St_torque = new Label[] { lbl_INV1_St_torque , lbl_INV2_St_torque , lbl_INV3_St_torque , lbl_INV4_St_torque , lbl_INV5_St_torque , lbl_INV6_St_torque };
            lbl_INV_St_MainErrCode = new Label[] { lbl_INV1_St_MainErrCode , lbl_INV2_St_MainErrCode , lbl_INV3_St_MainErrCode , lbl_INV4_St_MainErrCode , lbl_INV5_St_MainErrCode , lbl_INV6_St_MainErrCode };
            lbl_INV_St_SubErrCode = new Label[] { lbl_INV1_St_SubErrCode, lbl_INV2_St_SubErrCode , lbl_INV3_St_SubErrCode , lbl_INV4_St_SubErrCode , lbl_INV5_St_SubErrCode , lbl_INV6_St_SubErrCode };
            lbl_INV_St_BlockErrCode = new Label[] { lbl_INV1_St_BlockErrCode , lbl_INV2_St_BlockErrCode , lbl_INV3_St_BlockErrCode , lbl_INV4_St_BlockErrCode , lbl_INV5_St_BlockErrCode , lbl_INV6_St_BlockErrCode };
            lbl_INV_St_Output_Current = new Label[] { lbl_INV1_St_Output_Current , lbl_INV2_St_Output_Current , lbl_INV3_St_Output_Current , lbl_INV4_St_Output_Current , lbl_INV5_St_Output_Current , lbl_INV6_St_Output_Current };
            lbl_INV_St_DCLink_Volt = new Label[] { lbl_INV1_St_DCLink_Volt , lbl_INV2_St_DCLink_Volt , lbl_INV3_St_DCLink_Volt , lbl_INV4_St_DCLink_Volt , lbl_INV5_St_DCLink_Volt , lbl_INV6_St_DCLink_Volt };
            lbl_INV_St_radiation_Temp = new Label[] { lbl_INV1_St_radiation_Temp , lbl_INV2_St_radiation_Temp , lbl_INV3_St_radiation_Temp , lbl_INV4_St_radiation_Temp , lbl_INV5_St_radiation_Temp , lbl_INV6_St_radiation_Temp };
            lbl_INV_St_Motor_Temp = new Label[] { lbl_INV1_St_Motor_Temp , lbl_INV2_St_Motor_Temp , lbl_INV3_St_Motor_Temp , lbl_INV4_St_Motor_Temp , lbl_INV5_St_Motor_Temp , lbl_INV6_St_Motor_Temp };
            lbl_INV_St_Motor_Encoder = new Label[] { lbl_INV1_St_Motor_Encoder , lbl_INV2_St_Motor_Encoder , lbl_INV3_St_Motor_Encoder , lbl_INV4_St_Motor_Encoder , lbl_INV5_St_Motor_Encoder , lbl_INV6_St_Motor_Encoder };
            lbl_INV_St_Exteranl_Encoder = new Label[] { lbl_INV1_St_Exteranl_Encoder , lbl_INV2_St_Exteranl_Encoder , lbl_INV3_St_Exteranl_Encoder , lbl_INV4_St_Exteranl_Encoder , lbl_INV5_St_Exteranl_Encoder , lbl_INV6_St_Exteranl_Encoder };
            lbl_INV_St_Sum_energy_consumed = new Label[] { lbl_INV1_St_Sum_energy_consumed , lbl_INV2_St_Sum_energy_consumed , lbl_INV3_St_Sum_energy_consumed , lbl_INV4_St_Sum_energy_consumed , lbl_INV5_St_Sum_energy_consumed , lbl_INV6_St_Sum_energy_consumed };
            lbl_INV_St_Sum_regenerative_energy = new Label[] { lbl_INV1_St_Sum_regenerative_energy , lbl_INV2_St_Sum_regenerative_energy , lbl_INV3_St_Sum_regenerative_energy , lbl_INV4_St_Sum_regenerative_energy , lbl_INV5_St_Sum_regenerative_energy , lbl_INV6_St_Sum_regenerative_energy };
            lbl_INV_St_Actual_Reference_offset = new Label[] { lbl_INV1_St_Actual_Reference_offset, lbl_INV2_St_Actual_Reference_offset, lbl_INV3_St_Actual_Reference_offset, lbl_INV4_St_Actual_Reference_offset, lbl_INV5_St_Actual_Reference_offset, lbl_INV6_St_Actual_Reference_offset };
            lbl_INV_St_Actual_Position_gain = new Label[] { lbl_INV1_St_Actual_Position_gain, lbl_INV2_St_Actual_Position_gain, lbl_INV3_St_Actual_Position_gain, lbl_INV4_St_Actual_Position_gain, lbl_INV5_St_Actual_Position_gain, lbl_INV6_St_Actual_Position_gain };
            lbl_INV_St_Actual_Position_window = new Label[] { lbl_INV1_St_Actual_Position_window, lbl_INV2_St_Actual_Position_window, lbl_INV3_St_Actual_Position_window, lbl_INV4_St_Actual_Position_window, lbl_INV5_St_Actual_Position_window, lbl_INV6_St_Actual_Position_window};

            lbl_INV_Ctrl_15 = new Label[] { lbl_INV1_Ctrl_15, lbl_INV2_Ctrl_15, lbl_INV3_Ctrl_15, lbl_INV4_Ctrl_15, lbl_INV5_Ctrl_15, lbl_INV6_Ctrl_15 };
            lbl_INV_Ctrl_12 = new Label[] { lbl_INV1_Ctrl_12, lbl_INV2_Ctrl_12, lbl_INV3_Ctrl_12, lbl_INV4_Ctrl_12, lbl_INV5_Ctrl_12, lbl_INV6_Ctrl_12 };
            lbl_INV_Ctrl_11 = new Label[] { lbl_INV1_Ctrl_11, lbl_INV2_Ctrl_11, lbl_INV3_Ctrl_11, lbl_INV4_Ctrl_11, lbl_INV5_Ctrl_11, lbl_INV6_Ctrl_11 };
            lbl_INV_Ctrl_10 = new Label[] { lbl_INV1_Ctrl_10,lbl_INV2_Ctrl_10, lbl_INV3_Ctrl_10, lbl_INV4_Ctrl_10, lbl_INV5_Ctrl_10, lbl_INV6_Ctrl_10 };
            lbl_INV_Ctrl_9 = new Label[] { lbl_INV1_Ctrl_9, lbl_INV2_Ctrl_9, lbl_INV3_Ctrl_9, lbl_INV4_Ctrl_9, lbl_INV5_Ctrl_9, lbl_INV6_Ctrl_9 };
            lbl_INV_Ctrl_8 = new Label[] { lbl_INV1_Ctrl_8, lbl_INV2_Ctrl_8, lbl_INV3_Ctrl_8, lbl_INV4_Ctrl_8, lbl_INV5_Ctrl_8, lbl_INV6_Ctrl_8};
            lbl_INV_Ctrl_7 = new Label[] { lbl_INV1_Ctrl_7, lbl_INV2_Ctrl_7, lbl_INV3_Ctrl_7, lbl_INV4_Ctrl_7, lbl_INV5_Ctrl_7, lbl_INV6_Ctrl_7};
            lbl_INV_Ctrl_6 = new Label[] { lbl_INV1_Ctrl_6, lbl_INV2_Ctrl_6, lbl_INV3_Ctrl_6, lbl_INV4_Ctrl_6, lbl_INV5_Ctrl_6, lbl_INV6_Ctrl_6};
            lbl_INV_Ctrl_5 = new Label[] { lbl_INV1_Ctrl_5, lbl_INV2_Ctrl_5, lbl_INV3_Ctrl_5, lbl_INV4_Ctrl_5, lbl_INV5_Ctrl_5, lbl_INV6_Ctrl_5 };
            lbl_INV_Ctrl_4 = new Label[] { lbl_INV1_Ctrl_4, lbl_INV2_Ctrl_4, lbl_INV3_Ctrl_4, lbl_INV4_Ctrl_4, lbl_INV5_Ctrl_4, lbl_INV6_Ctrl_4 };
            lbl_INV_Ctrl_3 = new Label[] { lbl_INV1_Ctrl_3, lbl_INV2_Ctrl_3, lbl_INV3_Ctrl_3, lbl_INV4_Ctrl_3, lbl_INV5_Ctrl_3, lbl_INV6_Ctrl_3 };
            lbl_INV_Ctrl_2 = new Label[] { lbl_INV1_Ctrl_2, lbl_INV2_Ctrl_2, lbl_INV3_Ctrl_2, lbl_INV4_Ctrl_2, lbl_INV5_Ctrl_2, lbl_INV6_Ctrl_2};
            lbl_INV_Ctrl_1 = new Label[] { lbl_INV1_Ctrl_1, lbl_INV2_Ctrl_1, lbl_INV3_Ctrl_1, lbl_INV4_Ctrl_1, lbl_INV5_Ctrl_1, lbl_INV6_Ctrl_1};
            lbl_INV_Ctrl_Position = new Label[] { lbl_INV1_Ctrl_Position , lbl_INV2_Ctrl_Position , lbl_INV3_Ctrl_Position , lbl_INV4_Ctrl_Position , lbl_INV5_Ctrl_Position , lbl_INV6_Ctrl_Position };
            lbl_INV_Ctrl_Speed = new Label[] { lbl_INV1_Ctrl_Speed , lbl_INV2_Ctrl_Speed , lbl_INV3_Ctrl_Speed , lbl_INV4_Ctrl_Speed , lbl_INV5_Ctrl_Speed , lbl_INV6_Ctrl_Speed };
            lbl_INV_Ctrl_Accel = new Label[] { lbl_INV1_Ctrl_Accel , lbl_INV2_Ctrl_Accel , lbl_INV3_Ctrl_Accel , lbl_INV4_Ctrl_Accel , lbl_INV5_Ctrl_Accel , lbl_INV6_Ctrl_Accel };
            lbl_INV_Ctrl_Decel = new Label[] { lbl_INV1_Ctrl_Decel , lbl_INV2_Ctrl_Decel , lbl_INV3_Ctrl_Decel , lbl_INV4_Ctrl_Decel , lbl_INV5_Ctrl_Decel , lbl_INV6_Ctrl_Decel };
            lbl_INV_Ctrl_JerkTime = new Label[] { lbl_INV1_Ctrl_JerkTime , lbl_INV2_Ctrl_JerkTime , lbl_INV3_Ctrl_JerkTime , lbl_INV4_Ctrl_JerkTime , lbl_INV5_Ctrl_JerkTime , lbl_INV6_Ctrl_JerkTime };
            lbl_INV_Ctrl_Reference_offset = new Label[] { lbl_INV1_Ctrl_Reference_offset, lbl_INV2_Ctrl_Reference_offset, lbl_INV3_Ctrl_Reference_offset, lbl_INV4_Ctrl_Reference_offset, lbl_INV5_Ctrl_Reference_offset, lbl_INV6_Ctrl_Reference_offset };
            lbl_INV_Ctrl_Position_gain = new Label[] { lbl_INV1_Ctrl_Position_gain, lbl_INV2_Ctrl_Position_gain, lbl_INV3_Ctrl_Position_gain, lbl_INV4_Ctrl_Position_gain, lbl_INV5_Ctrl_Position_gain, lbl_INV6_Ctrl_Position_gain };
            lbl_INV_Ctrl_Position_window = new Label[] { lbl_INV1_Ctrl_Position_window, lbl_INV2_Ctrl_Position_window, lbl_INV3_Ctrl_Position_window, lbl_INV4_Ctrl_Position_window, lbl_INV5_Ctrl_Position_window, lbl_INV6_Ctrl_Position_window };

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
            for (byte i = 0; i < 6; i++)
            {
                lbl_INV_St_7[i].Text = "";
                lbl_INV_St_5[i].Text = "";
                lbl_INV_St_4[i].Text = "";
                lbl_INV_St_3[i].Text = "";
                lbl_INV_St_2[i].Text = "";
                lbl_INV_St_1[i].Text = "";
                lbl_INV_St_0[i].Text = "";
                lbl_INV_St_Fault[i].Text = "";
                lbl_INV_St_Position[i].Text = "";
                lbl_INV_St_Speed[i].Text = "";
                lbl_INV_St_torque[i].Text = "";
                lbl_INV_St_MainErrCode[i].Text = "";
                lbl_INV_St_SubErrCode[i].Text = "";
                lbl_INV_St_BlockErrCode[i].Text = "";
                lbl_INV_St_Output_Current[i].Text = "";
                lbl_INV_St_DCLink_Volt[i].Text = "";
                lbl_INV_St_radiation_Temp[i].Text = "";
                lbl_INV_St_Motor_Temp[i].Text = "";
                lbl_INV_St_Motor_Encoder[i].Text = "";
                lbl_INV_St_Exteranl_Encoder[i].Text = "";
                lbl_INV_St_Sum_energy_consumed[i].Text = "";
                lbl_INV_St_Sum_regenerative_energy[i].Text = "";
                lbl_INV_St_Actual_Reference_offset[i].Text = "";
                lbl_INV_St_Actual_Position_gain[i].Text = "";
                lbl_INV_St_Actual_Position_window[i].Text = "";

                lbl_INV_Ctrl_15[i].Text = "";
                lbl_INV_Ctrl_12[i].Text = "";
                lbl_INV_Ctrl_11[i].Text = "";
                lbl_INV_Ctrl_10[i].Text = "";
                lbl_INV_Ctrl_9[i].Text = "";
                lbl_INV_Ctrl_8[i].Text = "";
                lbl_INV_Ctrl_7[i].Text = "";
                lbl_INV_Ctrl_6[i].Text = "";
                lbl_INV_Ctrl_5[i].Text = "";
                lbl_INV_Ctrl_4[i].Text = "";
                lbl_INV_Ctrl_3[i].Text = "";
                lbl_INV_Ctrl_2[i].Text = "";
                lbl_INV_Ctrl_1[i].Text = "";
                lbl_INV_Ctrl_Position[i].Text = "";
                lbl_INV_Ctrl_Speed[i].Text = "";
                lbl_INV_Ctrl_Accel[i].Text = "";
                lbl_INV_Ctrl_Decel[i].Text = "";
                lbl_INV_Ctrl_JerkTime[i].Text = "";
                lbl_INV_Ctrl_Reference_offset[i].Text = "";
                lbl_INV_Ctrl_Position_gain[i].Text = "";
                lbl_INV_Ctrl_Position_window[i].Text = "";
            }
        }

        //상태데이터 화면 표출 함수
        public unsafe void Display_InvertorSt(byte[] datas)
        {
            dev_REC_Invertor = (VEXI_DEFS.TSRM_REC_InvertorRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TSRM_REC_InvertorRes));

            fixed (VEXI_DEFS.TSRM_InvertorStRec* Ptr_1 = &dev_REC_Invertor.Invertor_1_St)
            {
                for (byte i = 0; i < 6; i++)
                {
                    if (Global_Class.BitStatus((Ptr_1 + i)->St, 6)) lbl_INV_St_7[i].Text = "1"; else lbl_INV_St_7[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->St, 5)) lbl_INV_St_5[i].Text = "1"; else lbl_INV_St_5[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->St, 4)) lbl_INV_St_4[i].Text = "1"; else lbl_INV_St_4[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->St, 3)) lbl_INV_St_3[i].Text = "1"; else lbl_INV_St_3[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->St, 2)) lbl_INV_St_2[i].Text = "1"; else lbl_INV_St_2[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->St, 1)) lbl_INV_St_1[i].Text = "1"; else lbl_INV_St_1[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->St, 0)) lbl_INV_St_0[i].Text = "1"; else lbl_INV_St_0[i].Text = "0";
                    lbl_INV_St_Fault[i].Text = string.Format("{0}", (Ptr_1 + i)->St_Fault);
                    lbl_INV_St_Position[i].Text = string.Format("{0}", (Ptr_1 + i)->St_Position);
                    lbl_INV_St_Speed[i].Text = string.Format("{0}", (Ptr_1 + i)->St_Speed);
                    lbl_INV_St_torque[i].Text = string.Format("{0}", (Ptr_1 + i)->St_torque);
                    lbl_INV_St_MainErrCode[i].Text = string.Format("0x{0:X8}", (Ptr_1 + i)->St_MainErrCode);
                    lbl_INV_St_SubErrCode[i].Text = string.Format("0x{0:X8}", (Ptr_1 + i)->St_SubErrCode);
                    lbl_INV_St_BlockErrCode[i].Text = string.Format("0x{0:X8}", (Ptr_1 + i)->St_BlockErrCode);
                    lbl_INV_St_Output_Current[i].Text = string.Format("{0}", (Ptr_1 + i)->St_Output_Current);
                    //lbl_INV_St_DCLink_Volt[i].Text = string.Format("{0}", (Ptr_1 + i)->St_DCLink_Volt);
                    lbl_INV_St_DCLink_Volt[i].Text = string.Format("{0:0.000}", (double)(Ptr_1 + i)->St_DCLink_Volt / 1000);
                    lbl_INV_St_radiation_Temp[i].Text = string.Format("{0}", (Ptr_1 + i)->St_radiation_Temp);
                    lbl_INV_St_Motor_Temp[i].Text = string.Format("{0}", (Ptr_1 + i)->St_Motor_Temp);
                    lbl_INV_St_Motor_Encoder[i].Text = string.Format("{0}", (Ptr_1 + i)->St_Motor_Encoder);
                    lbl_INV_St_Exteranl_Encoder[i].Text = string.Format("{0}", (Ptr_1 + i)->St_Exteranl_Encoder);
                    lbl_INV_St_Sum_energy_consumed[i].Text = string.Format("{0}", (Ptr_1 + i)->St_Sum_energy_consumed);
                    lbl_INV_St_Sum_regenerative_energy[i].Text = string.Format("{0}", (Ptr_1 + i)->St_Sum_regenerative_energy);
                    lbl_INV_St_Actual_Reference_offset[i].Text = string.Format("{0}", (Ptr_1 + i)->St_Actual_Reference_offset);
                    lbl_INV_St_Actual_Position_gain[i].Text = string.Format("{0}", (Ptr_1 + i)->St_Actual_Position_gain);
                    lbl_INV_St_Actual_Position_window[i].Text = string.Format("{0}", (Ptr_1 + i)->St_Actual_Position_window);

                }
            }

            fixed (VEXI_DEFS.TSRM_InvertorCtrlRec* Ptr_1 = &dev_REC_Invertor.Invertor_1_Ctrl)
            {
                for (byte i = 0; i < 6; i++)
                {
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl, 15)) lbl_INV_Ctrl_15[i].Text = "1"; else lbl_INV_Ctrl_15[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl, 12)) lbl_INV_Ctrl_12[i].Text = "1"; else lbl_INV_Ctrl_12[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl, 11)) lbl_INV_Ctrl_11[i].Text = "1"; else lbl_INV_Ctrl_11[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl, 10)) lbl_INV_Ctrl_10[i].Text = "1"; else lbl_INV_Ctrl_10[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl, 9)) lbl_INV_Ctrl_9[i].Text = "1"; else lbl_INV_Ctrl_9[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl, 8)) lbl_INV_Ctrl_8[i].Text = "1"; else lbl_INV_Ctrl_8[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl, 7)) lbl_INV_Ctrl_7[i].Text = "1"; else lbl_INV_Ctrl_7[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl, 6)) lbl_INV_Ctrl_6[i].Text = "1"; else lbl_INV_Ctrl_6[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl, 5)) lbl_INV_Ctrl_5[i].Text = "1"; else lbl_INV_Ctrl_5[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl, 4)) lbl_INV_Ctrl_4[i].Text = "1"; else lbl_INV_Ctrl_4[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl, 3)) lbl_INV_Ctrl_3[i].Text = "1"; else lbl_INV_Ctrl_3[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl, 2)) lbl_INV_Ctrl_2[i].Text = "1"; else lbl_INV_Ctrl_2[i].Text = "0";
                    if (Global_Class.BitStatus((Ptr_1 + i)->Ctrl, 1)) lbl_INV_Ctrl_1[i].Text = "1"; else lbl_INV_Ctrl_1[i].Text = "0";
                    lbl_INV_Ctrl_Position[i].Text = string.Format("{0}", (Ptr_1 + i)->Ctrl_Position);
                    lbl_INV_Ctrl_Speed[i].Text = string.Format("{0}", (Ptr_1 + i)->Ctrl_Speed);
                    lbl_INV_Ctrl_Accel[i].Text = string.Format("{0}", (Ptr_1 + i)->Ctrl_Accel);
                    lbl_INV_Ctrl_Decel[i].Text = string.Format("{0}", (Ptr_1 + i)->Ctrl_Decel);
                    lbl_INV_Ctrl_JerkTime[i].Text = string.Format("{0}", (Ptr_1 + i)->Ctrl_JerkTime);
                    lbl_INV_Ctrl_Reference_offset[i].Text = string.Format("{0}", (Ptr_1 + i)->Ctrl_Reference_offset);
                    lbl_INV_Ctrl_Position_gain[i].Text = string.Format("{0}", (Ptr_1 + i)->Ctrl_Position_gain);
                    lbl_INV_Ctrl_Position_window[i].Text = string.Format("{0}", (Ptr_1 + i)->Ctrl_Position_window);
                }
            }
        }

        #endregion
    }
}

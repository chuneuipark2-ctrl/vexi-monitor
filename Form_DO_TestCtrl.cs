using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_DO_TestCtrl : Form
    {
        public Form_Main form_Main;
        private Label[] lbl_DIO_Title;
        private Label[] lbl_DIO_ST;
        private Label[] lbl_DIO_MDOE;
        private Button[] btn_OutMODE;
        private Button[] btn_OutSt;
        private VEXI_DEFS.TDEV_REC_DO_TEST dev_REC_DO_Test;

        public Form_DO_TestCtrl()
        {
            InitializeComponent();

            btn_OutSt = new Button[] {
                Btn_DOCtrl_1,
                Btn_DOCtrl_2,
                Btn_DOCtrl_3,
                Btn_DOCtrl_4,
                Btn_DOCtrl_5,
                Btn_DOCtrl_6,
                Btn_DOCtrl_7,
                Btn_DOCtrl_8,
                Btn_DOCtrl_9,
                Btn_DOCtrl_10,
                Btn_DOCtrl_11,
                Btn_DOCtrl_12,
                Btn_DOCtrl_13,
                Btn_DOCtrl_14,
                Btn_DOCtrl_15,
                Btn_DOCtrl_16,
                Btn_DOCtrl_17,
                Btn_DOCtrl_18,
                Btn_DOCtrl_19,
                Btn_DOCtrl_20,
                Btn_DOCtrl_21,
                Btn_DOCtrl_22,
                Btn_DOCtrl_23,
                Btn_DOCtrl_24,
                Btn_DOCtrl_25,
                Btn_DOCtrl_26,
                Btn_DOCtrl_27,
                Btn_DOCtrl_28,
                Btn_DOCtrl_29,
                Btn_DOCtrl_30,
                Btn_DOCtrl_31,
                Btn_DOCtrl_32,
                Btn_DOCtrl_33,
                Btn_DOCtrl_34,
                Btn_DOCtrl_35,
                Btn_DOCtrl_36,
                Btn_DOCtrl_37,
                Btn_DOCtrl_38,
                Btn_DOCtrl_39,
                Btn_DOCtrl_40,
                Btn_DOCtrl_41,
                Btn_DOCtrl_42,
                Btn_DOCtrl_43,
                Btn_DOCtrl_44,
                Btn_DOCtrl_45,
                Btn_DOCtrl_46,
                Btn_DOCtrl_47,
                Btn_DOCtrl_48,
                Btn_DOCtrl_49,
                Btn_DOCtrl_50,
                Btn_DOCtrl_51,
                Btn_DOCtrl_52,
                Btn_DOCtrl_53,
                Btn_DOCtrl_54,
                Btn_DOCtrl_55,
                Btn_DOCtrl_56,
                Btn_DOCtrl_57,
                Btn_DOCtrl_58,
                Btn_DOCtrl_59,
                Btn_DOCtrl_60,
                Btn_DOCtrl_61,
                Btn_DOCtrl_62,
                Btn_DOCtrl_63,
                Btn_DOCtrl_64,
                Btn_DOCtrl_65,
                Btn_DOCtrl_66,
                Btn_DOCtrl_67,
                Btn_DOCtrl_68,
                Btn_DOCtrl_69,
                Btn_DOCtrl_70,
                Btn_DOCtrl_71,
                Btn_DOCtrl_72,
                Btn_DOCtrl_73,
                Btn_DOCtrl_74,
                Btn_DOCtrl_75,
                Btn_DOCtrl_76,
                Btn_DOCtrl_77,
                Btn_DOCtrl_78,
                Btn_DOCtrl_79,
                Btn_DOCtrl_80
            };

            btn_OutMODE = new Button[] {Btn_ModeCtrl_1,
                Btn_ModeCtrl_2,
                Btn_ModeCtrl_3,
                Btn_ModeCtrl_4,
                Btn_ModeCtrl_5,
                Btn_ModeCtrl_6,
                Btn_ModeCtrl_7,
                Btn_ModeCtrl_8,
                Btn_ModeCtrl_9,
                Btn_ModeCtrl_10,
                Btn_ModeCtrl_11,
                Btn_ModeCtrl_12,
                Btn_ModeCtrl_13,
                Btn_ModeCtrl_14,
                Btn_ModeCtrl_15,
                Btn_ModeCtrl_16,
                Btn_ModeCtrl_17,
                Btn_ModeCtrl_18,
                Btn_ModeCtrl_19,
                Btn_ModeCtrl_20,
                Btn_ModeCtrl_21,
                Btn_ModeCtrl_22,
                Btn_ModeCtrl_23,
                Btn_ModeCtrl_24,
                Btn_ModeCtrl_25,
                Btn_ModeCtrl_26,
                Btn_ModeCtrl_27,
                Btn_ModeCtrl_28,
                Btn_ModeCtrl_29,
                Btn_ModeCtrl_30,
                Btn_ModeCtrl_31,
                Btn_ModeCtrl_32,
                Btn_ModeCtrl_33,
                Btn_ModeCtrl_34,
                Btn_ModeCtrl_35,
                Btn_ModeCtrl_36,
                Btn_ModeCtrl_37,
                Btn_ModeCtrl_38,
                Btn_ModeCtrl_39,
                Btn_ModeCtrl_40,
                Btn_ModeCtrl_41,
                Btn_ModeCtrl_42,
                Btn_ModeCtrl_43,
                Btn_ModeCtrl_44,
                Btn_ModeCtrl_45,
                Btn_ModeCtrl_46,
                Btn_ModeCtrl_47,
                Btn_ModeCtrl_48,
                Btn_ModeCtrl_49,
                Btn_ModeCtrl_50,
                Btn_ModeCtrl_51,
                Btn_ModeCtrl_52,
                Btn_ModeCtrl_53,
                Btn_ModeCtrl_54,
                Btn_ModeCtrl_55,
                Btn_ModeCtrl_56,
                Btn_ModeCtrl_57,
                Btn_ModeCtrl_58,
                Btn_ModeCtrl_59,
                Btn_ModeCtrl_60,
                Btn_ModeCtrl_61,
                Btn_ModeCtrl_62,
                Btn_ModeCtrl_63,
                Btn_ModeCtrl_64,
                Btn_ModeCtrl_65,
                Btn_ModeCtrl_66,
                Btn_ModeCtrl_67,
                Btn_ModeCtrl_68,
                Btn_ModeCtrl_69,
                Btn_ModeCtrl_70,
                Btn_ModeCtrl_71,
                Btn_ModeCtrl_72,
                Btn_ModeCtrl_73,
                Btn_ModeCtrl_74,
                Btn_ModeCtrl_75,
                Btn_ModeCtrl_76,
                Btn_ModeCtrl_77,
                Btn_ModeCtrl_78,
                Btn_ModeCtrl_79,
                Btn_ModeCtrl_80
            };

            lbl_DIO_Title = new Label[] {lbl_IO_Title_1, 
                lbl_IO_Title_2,
                lbl_IO_Title_3,
                lbl_IO_Title_4,
                lbl_IO_Title_5,
                lbl_IO_Title_6,
                lbl_IO_Title_7,
                lbl_IO_Title_8,
                lbl_IO_Title_9,
                lbl_IO_Title_10,
                lbl_IO_Title_11,
                lbl_IO_Title_12,
                lbl_IO_Title_13,
                lbl_IO_Title_14,
                lbl_IO_Title_15,
                lbl_IO_Title_16,
                lbl_IO_Title_17,
                lbl_IO_Title_18,
                lbl_IO_Title_19,
                lbl_IO_Title_20,
                lbl_IO_Title_21,
                lbl_IO_Title_22,
                lbl_IO_Title_23,
                lbl_IO_Title_24,
                lbl_IO_Title_25,
                lbl_IO_Title_26,
                lbl_IO_Title_27,
                lbl_IO_Title_28,
                lbl_IO_Title_29,
                lbl_IO_Title_30,
                lbl_IO_Title_31,
                lbl_IO_Title_32,
                lbl_IO_Title_33,
                lbl_IO_Title_34,
                lbl_IO_Title_35,
                lbl_IO_Title_36,
                lbl_IO_Title_37,
                lbl_IO_Title_38,
                lbl_IO_Title_39,
                lbl_IO_Title_40,
                lbl_IO_Title_41,
                lbl_IO_Title_42,
                lbl_IO_Title_43,
                lbl_IO_Title_44,
                lbl_IO_Title_45,
                lbl_IO_Title_46,
                lbl_IO_Title_47,
                lbl_IO_Title_48,
                lbl_IO_Title_49,
                lbl_IO_Title_50,
                lbl_IO_Title_51,
                lbl_IO_Title_52,
                lbl_IO_Title_53,
                lbl_IO_Title_54,
                lbl_IO_Title_55,
                lbl_IO_Title_56,
                lbl_IO_Title_57,
                lbl_IO_Title_58,
                lbl_IO_Title_59,
                lbl_IO_Title_60,
                lbl_IO_Title_61,
                lbl_IO_Title_62,
                lbl_IO_Title_63,
                lbl_IO_Title_64,
                lbl_IO_Title_65,
                lbl_IO_Title_66,
                lbl_IO_Title_67,
                lbl_IO_Title_68,
                lbl_IO_Title_69,
                lbl_IO_Title_70,
                lbl_IO_Title_71,
                lbl_IO_Title_72,
                lbl_IO_Title_73,
                lbl_IO_Title_74,
                lbl_IO_Title_75,
                lbl_IO_Title_76,
                lbl_IO_Title_77,
                lbl_IO_Title_78,
                lbl_IO_Title_79,
                lbl_IO_Title_80
            };

            lbl_DIO_ST = new Label[] {lbl_IOSt_1,
                lbl_IOSt_2,
                lbl_IOSt_3,
                lbl_IOSt_4,
                lbl_IOSt_5,
                lbl_IOSt_6,
                lbl_IOSt_7,
                lbl_IOSt_8,
                lbl_IOSt_9,
                lbl_IOSt_10,
                lbl_IOSt_11,
                lbl_IOSt_12,
                lbl_IOSt_13,
                lbl_IOSt_14,
                lbl_IOSt_15,
                lbl_IOSt_16,
                lbl_IOSt_17,
                lbl_IOSt_18,
                lbl_IOSt_19,
                lbl_IOSt_20,
                lbl_IOSt_21,
                lbl_IOSt_22,
                lbl_IOSt_23,
                lbl_IOSt_24,
                lbl_IOSt_25,
                lbl_IOSt_26,
                lbl_IOSt_27,
                lbl_IOSt_28,
                lbl_IOSt_29,
                lbl_IOSt_30,
                lbl_IOSt_31,
                lbl_IOSt_32,
                lbl_IOSt_33,
                lbl_IOSt_34,
                lbl_IOSt_35,
                lbl_IOSt_36,
                lbl_IOSt_37,
                lbl_IOSt_38,
                lbl_IOSt_39,
                lbl_IOSt_40,
                lbl_IOSt_41,
                lbl_IOSt_42,
                lbl_IOSt_43,
                lbl_IOSt_44,
                lbl_IOSt_45,
                lbl_IOSt_46,
                lbl_IOSt_47,
                lbl_IOSt_48,
                lbl_IOSt_49,
                lbl_IOSt_50,
                lbl_IOSt_51,
                lbl_IOSt_52,
                lbl_IOSt_53,
                lbl_IOSt_54,
                lbl_IOSt_55,
                lbl_IOSt_56,
                lbl_IOSt_57,
                lbl_IOSt_58,
                lbl_IOSt_59,
                lbl_IOSt_60,
                lbl_IOSt_61,
                lbl_IOSt_62,
                lbl_IOSt_63,
                lbl_IOSt_64,
                lbl_IOSt_65,
                lbl_IOSt_66,
                lbl_IOSt_67,
                lbl_IOSt_68,
                lbl_IOSt_69,
                lbl_IOSt_70,
                lbl_IOSt_71,
                lbl_IOSt_72,
                lbl_IOSt_73,
                lbl_IOSt_74,
                lbl_IOSt_75,
                lbl_IOSt_76,
                lbl_IOSt_77,
                lbl_IOSt_78,
                lbl_IOSt_79,
                lbl_IOSt_80
            };


            lbl_DIO_MDOE = new Label[] {lbl_IOMode_1,
                lbl_IOMode_2,
                lbl_IOMode_3,
                lbl_IOMode_4,
                lbl_IOMode_5,
                lbl_IOMode_6,
                lbl_IOMode_7,
                lbl_IOMode_8,
                lbl_IOMode_9,
                lbl_IOMode_10,
                lbl_IOMode_11,
                lbl_IOMode_12,
                lbl_IOMode_13,
                lbl_IOMode_14,
                lbl_IOMode_15,
                lbl_IOMode_16,
                lbl_IOMode_17,
                lbl_IOMode_18,
                lbl_IOMode_19,
                lbl_IOMode_20,
                lbl_IOMode_21,
                lbl_IOMode_22,
                lbl_IOMode_23,
                lbl_IOMode_24,
                lbl_IOMode_25,
                lbl_IOMode_26,
                lbl_IOMode_27,
                lbl_IOMode_28,
                lbl_IOMode_29,
                lbl_IOMode_30,
                lbl_IOMode_31,
                lbl_IOMode_32,
                lbl_IOMode_33,
                lbl_IOMode_34,
                lbl_IOMode_35,
                lbl_IOMode_36,
                lbl_IOMode_37,
                lbl_IOMode_38,
                lbl_IOMode_39,
                lbl_IOMode_40,
                lbl_IOMode_41,
                lbl_IOMode_42,
                lbl_IOMode_43,
                lbl_IOMode_44,
                lbl_IOMode_45,
                lbl_IOMode_46,
                lbl_IOMode_47,
                lbl_IOMode_48,
                lbl_IOMode_49,
                lbl_IOMode_50,
                lbl_IOMode_51,
                lbl_IOMode_52,
                lbl_IOMode_53,
                lbl_IOMode_54,
                lbl_IOMode_55,
                lbl_IOMode_56,
                lbl_IOMode_57,
                lbl_IOMode_58,
                lbl_IOMode_59,
                lbl_IOMode_60,
                lbl_IOMode_61,
                lbl_IOMode_62,
                lbl_IOMode_63,
                lbl_IOMode_64,
                lbl_IOMode_65,
                lbl_IOMode_66,
                lbl_IOMode_67,
                lbl_IOMode_68,
                lbl_IOMode_69,
                lbl_IOMode_70,
                lbl_IOMode_71,
                lbl_IOMode_72,
                lbl_IOMode_73,
                lbl_IOMode_74,
                lbl_IOMode_75,
                lbl_IOMode_76,
                lbl_IOMode_77,
                lbl_IOMode_78,
                lbl_IOMode_79,
                lbl_IOMode_80
            };
        }

        #region 컴포넌트 이벤트
        private void Form_SRMSt_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }
            Display_DevSt();
        }
        #endregion

        #region 기능함수

        private unsafe void Display_Sub_DIO()
        {
            byte Loop;
            byte ByteIndex, BitIndex;

            switch (form_Main.COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt)
                    {
                        //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                        //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                        {
                            //I/O
                            for (Loop = 1; Loop <= 80; Loop++)
                            {
                                if (Loop > ConstClass.SRM_DO_Names.GetLength(0))
                                {
                                    lbl_DIO_Title[Loop - 1].BackColor = Color.Gray;
                                    lbl_DIO_Title[Loop - 1].Text = "";

                                    lbl_DIO_ST[Loop - 1].BackColor = Color.Gray;
                                    lbl_DIO_ST[Loop - 1].Text = "";

                                    lbl_DIO_MDOE[Loop - 1].BackColor = Color.Gray;
                                    lbl_DIO_MDOE[Loop - 1].Text = "";

                                    btn_OutMODE[Loop - 1].Enabled = false;
                                    btn_OutSt[Loop - 1].Enabled = false;
                                }
                                else
                                {
                                    lbl_DIO_Title[Loop - 1].BackColor = System.Drawing.SystemColors.Highlight;
                                    lbl_DIO_Title[Loop - 1].Text = ConstClass.SRM_DO_Names[Loop - 1, 0];
                                    btn_OutMODE[Loop - 1].Enabled = true;
                                    btn_OutSt[Loop - 1].Enabled = true;
                                }
                            }
                            for (Loop = 1; Loop <= 80; Loop++)
                            {
                                ByteIndex = (byte)((Loop - 1) / 8);
                                BitIndex = (byte)((Loop - 1) % 8);

                                if (Loop <= ConstClass.SRM_DO_Names.GetLength(0))
                                {
                                    if (Global_Class.BitStatus(DevSt->IO_Digital_OUT[ByteIndex], BitIndex))
                                    {
                                        lbl_DIO_ST[Loop - 1].BackColor = Color.Yellow;
                                        lbl_DIO_ST[Loop - 1].Text = "ON";
                                    }
                                    else
                                    {
                                        lbl_DIO_ST[Loop - 1].BackColor = Color.Silver;
                                        lbl_DIO_ST[Loop - 1].Text = "OFF";
                                    }

                                    if (Global_Class.BitStatus(DevSt->IO_Digital_OUTMode[ByteIndex], BitIndex))
                                    {
                                        lbl_DIO_MDOE[Loop - 1].BackColor = Color.Tomato;
                                        lbl_DIO_MDOE[Loop - 1].Text = "수동";
                                    }
                                    else
                                    {
                                        lbl_DIO_MDOE[Loop - 1].BackColor = Color.White;
                                        lbl_DIO_MDOE[Loop - 1].Text = "자동";
                                    }
                                }
                            }
                        }


                    }
                    break;
                case ConstClass.TYPE_RTV:
                    fixed (VEXI_DEFS.TRTV_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt)
                    {
                        //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                        //if (form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) //호출하는데서 체크하는 걸로 수정함
                        {
                            //I/O
                            for (Loop = 1; Loop <= 80; Loop++)
                            {
                                if (Loop > (ConstClass.RTV_DO_Names_1.GetLength(0) + ConstClass.RTV_DO_Names_2.GetLength(0)))
                                {
                                    lbl_DIO_Title[Loop - 1].BackColor = Color.Gray;
                                    lbl_DIO_Title[Loop - 1].Text = "";

                                    lbl_DIO_ST[Loop - 1].BackColor = Color.Gray;
                                    lbl_DIO_ST[Loop - 1].Text = "";

                                    lbl_DIO_MDOE[Loop - 1].BackColor = Color.Gray;
                                    lbl_DIO_MDOE[Loop - 1].Text = "";

                                    btn_OutMODE[Loop - 1].Enabled = false;
                                    btn_OutSt[Loop - 1].Enabled = false;
                                }
                                else
                                {
                                    if (Loop <= (ConstClass.RTV_DO_Names_1.GetLength(0)))
                                    {
                                        lbl_DIO_Title[Loop - 1].BackColor = System.Drawing.SystemColors.Highlight;
                                        lbl_DIO_Title[Loop - 1].Text = ConstClass.RTV_DO_Names_1[Loop - 1, 0];
                                        btn_OutMODE[Loop - 1].Enabled = true;
                                        btn_OutSt[Loop - 1].Enabled = true;
                                    } else
                                    {
                                        lbl_DIO_Title[Loop - 1].BackColor = System.Drawing.SystemColors.Highlight;
                                        lbl_DIO_Title[Loop - 1].Text = ConstClass.RTV_DO_Names_2[Loop - ConstClass.RTV_DO_Names_1.GetLength(0) - 1, 0];
                                        btn_OutMODE[Loop - 1].Enabled = true;
                                        btn_OutSt[Loop - 1].Enabled = true;
                                    }
                                }
                            }
                            for (Loop = 1; Loop <= 80; Loop++)
                            {
                                ByteIndex = (byte)((Loop - 1) / 8);
                                BitIndex = (byte)((Loop - 1) % 8);

                                if (Loop <= (ConstClass.RTV_DO_Names_1.GetLength(0) + ConstClass.RTV_DO_Names_2.GetLength(0)))
                                {
                                    if (Global_Class.BitStatus(DevSt->IO_Digital_OUT[ByteIndex], BitIndex))
                                    {
                                        lbl_DIO_ST[Loop - 1].BackColor = Color.Yellow;
                                        lbl_DIO_ST[Loop - 1].Text = "ON";
                                    }
                                    else
                                    {
                                        lbl_DIO_ST[Loop - 1].BackColor = Color.Silver;
                                        lbl_DIO_ST[Loop - 1].Text = "OFF";
                                    }

                                    if (ByteIndex <= 7)
                                    {
                                        if (Global_Class.BitStatus(DevSt->IO_Digital_OUTMode_1[ByteIndex], BitIndex))
                                        {
                                            lbl_DIO_MDOE[Loop - 1].BackColor = Color.Tomato;
                                            lbl_DIO_MDOE[Loop - 1].Text = "수동";
                                        }
                                        else
                                        {
                                            lbl_DIO_MDOE[Loop - 1].BackColor = Color.White;
                                            lbl_DIO_MDOE[Loop - 1].Text = "자동";
                                        }
                                    } else
                                    {
                                        if (Global_Class.BitStatus(DevSt->IO_Digital_OUTMode_2[ByteIndex-8], BitIndex))
                                        {
                                            lbl_DIO_MDOE[Loop - 1].BackColor = Color.Tomato;
                                            lbl_DIO_MDOE[Loop - 1].Text = "수동";
                                        }
                                        else
                                        {
                                            lbl_DIO_MDOE[Loop - 1].BackColor = Color.White;
                                            lbl_DIO_MDOE[Loop - 1].Text = "자동";
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case ConstClass.TYPE_EMS: break;
            }
        }

        private void Display_Init()
        {
            //Display_Sub_DIO
            for (byte i = 1; i <= 80; i++)
            {

                lbl_DIO_Title[i - 1].BackColor = Color.Gray;
                lbl_DIO_Title[i - 1].Text = "";

                lbl_DIO_ST[i - 1].BackColor = Color.Gray;
                lbl_DIO_ST[i - 1].Text = "";

                lbl_DIO_MDOE[i - 1].BackColor = Color.Gray;
                lbl_DIO_MDOE[i - 1].Text = "";

                btn_OutMODE[i - 1].Enabled = false;
                btn_OutSt[i - 1].Enabled = false;
            }
        }

        public unsafe void Display_DevSt()
        {
            if ((form_Main.COMMDataManager.DevRec.Flag_In_DevStatus) && (form_Main.COMMDataManager.CommSt != 0))
            {
                Display_Sub_DIO();
            } else
            {
                Display_Init();
                Display_Sub_DIO();
            }
        }
        #endregion

        private void Btn_ModeCtrl_1_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;

            Do_Ctrl_Mode(Convert.ToByte(bt.Tag.ToString()));
        }

        private unsafe void Do_Ctrl_Mode(byte TmpSignalID)
        {
            byte ByteIndex, BitIndex;

            dev_REC_DO_Test.CtrlFlag = 0x01;
            dev_REC_DO_Test.OutCtrl_ID = 0;
            dev_REC_DO_Test.OutCtrl_Value = 0;


            dev_REC_DO_Test.OutMode_ID = TmpSignalID;

            switch (form_Main.COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt)
                    {
                        ByteIndex = (byte)((TmpSignalID - 1) / 8);
                        BitIndex = (byte)((TmpSignalID - 1) % 8);

                        if (TmpSignalID <= ConstClass.SRM_DO_Names.GetLength(0))
                        {
                            if (Global_Class.BitStatus(DevSt->IO_Digital_OUTMode[ByteIndex], BitIndex))
                            {
                                dev_REC_DO_Test.OutMode_Value = 0;
                            }
                            else
                            {
                                dev_REC_DO_Test.OutMode_Value = 1; 
                            }
                        }
                    }
                    break;
                case ConstClass.TYPE_RTV:
                    fixed (VEXI_DEFS.TRTV_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt)
                    {
                        ByteIndex = (byte)((TmpSignalID - 1) / 8);
                        BitIndex = (byte)((TmpSignalID - 1) % 8);

                        if (TmpSignalID <= (ConstClass.RTV_DO_Names_1.GetLength(0) + ConstClass.RTV_DO_Names_2.GetLength(0)))
                        {
                            if (ByteIndex <= 7)
                            {
                                if (Global_Class.BitStatus(DevSt->IO_Digital_OUTMode_1[ByteIndex], BitIndex))
                                {
                                    dev_REC_DO_Test.OutMode_Value = 0;
                                }
                                else
                                {
                                    dev_REC_DO_Test.OutMode_Value = 1;
                                }
                            } else
                            {
                                if (Global_Class.BitStatus(DevSt->IO_Digital_OUTMode_2[ByteIndex-8], BitIndex))
                                {
                                    dev_REC_DO_Test.OutMode_Value = 0;
                                }
                                else
                                {
                                    dev_REC_DO_Test.OutMode_Value = 1;
                                }
                            }

                        }
                    }
                    break;
                case ConstClass.TYPE_EMS:
                    break;
            }
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_16, dev_REC_DO_Test);
        }

        private unsafe void Do_Ctrl_AllAutoMode()
        {
            dev_REC_DO_Test.CtrlFlag = 0x01;
            dev_REC_DO_Test.OutCtrl_ID = 0;
            dev_REC_DO_Test.OutCtrl_Value = 0;
            dev_REC_DO_Test.OutMode_ID = 0xFF;
            dev_REC_DO_Test.OutMode_Value = 0;
            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_16, dev_REC_DO_Test);
        }

        private unsafe void Do_Ctrl_St(byte TmpSignalID)
        {
            byte ByteIndex, BitIndex;

            dev_REC_DO_Test.CtrlFlag = 0x02;
            dev_REC_DO_Test.OutMode_ID = 0;
            dev_REC_DO_Test.OutMode_Value = 0;

            dev_REC_DO_Test.OutCtrl_ID = TmpSignalID;

            switch (form_Main.COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    fixed (VEXI_DEFS.TSRM_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.srm_REC_SRMSt)
                    {
                        ByteIndex = (byte)((TmpSignalID - 1) / 8);
                        BitIndex = (byte)((TmpSignalID - 1) % 8);

                        if (TmpSignalID <= ConstClass.SRM_DO_Names.GetLength(0))
                        {
                            if (Global_Class.BitStatus(DevSt->IO_Digital_OUT[ByteIndex], BitIndex))
                            {
                                dev_REC_DO_Test.OutCtrl_Value = 0;
                            }
                            else
                            {
                                dev_REC_DO_Test.OutCtrl_Value = 1;
                            }
                        }
                    }
                    break;
                case ConstClass.TYPE_RTV:
                    fixed (VEXI_DEFS.TRTV_StatusRes* DevSt = &form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt)
                    {
                        ByteIndex = (byte)((TmpSignalID - 1) / 8);
                        BitIndex = (byte)((TmpSignalID - 1) % 8);

                        if (TmpSignalID <= (ConstClass.RTV_DO_Names_1.GetLength(0) + ConstClass.RTV_DO_Names_2.GetLength(0)))
                        {
                            if (Global_Class.BitStatus(DevSt->IO_Digital_OUT[ByteIndex], BitIndex))
                            {
                                dev_REC_DO_Test.OutCtrl_Value = 0;
                            }
                            else
                            {
                                dev_REC_DO_Test.OutCtrl_Value = 1;
                            }
                        }
                    }
                    break;
                case ConstClass.TYPE_EMS:
                    break;
            }
             form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_16, dev_REC_DO_Test);
        }

        private void Btn_DOCtrl_1_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;

            Do_Ctrl_St(Convert.ToByte(bt.Tag.ToString()));

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Do_Ctrl_AllAutoMode();
        }
    }
}

using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_EMSRTVInvertorSt : Form
    {

        public Form_Main form_Main;
        private static VEXI_DEFS.TEMSRTV_REC_InvertorRes dev_REC_Invertor;

        private byte INVType = 255;

        
        public Form_EMSRTVInvertorSt()
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

            if (INVType != dev_REC_Invertor.InvertorType)
            {
                ListViewItem listviewItem;

                INVType = dev_REC_Invertor.InvertorType;

                lv_InvertorSt.Items.Clear();
                lv_InvertorCtrl.Items.Clear();

                switch (INVType)
                {
                    case 2:
                        for (ushort i = 0; i < ConstClass.RTV_SEW_InvertorStNames.GetLength(0); i++)
                        {
                            listviewItem = lv_InvertorSt.Items.Add(ConstClass.RTV_SEW_InvertorStNames[i]);
                            listviewItem.SubItems.Add("-");
                        }
                        for (ushort i = 0; i < ConstClass.RTV_SEW_InvertorCtrlNames.GetLength(0); i++)
                        {
                            listviewItem = lv_InvertorCtrl.Items.Add(ConstClass.RTV_SEW_InvertorCtrlNames[i]);
                            listviewItem.SubItems.Add("-");
                        }
                        break;
                    default:
                        for (ushort i = 0; i < ConstClass.RTV_ABB_InvertorStNames.GetLength(0); i++)
                        {
                            listviewItem = lv_InvertorSt.Items.Add(ConstClass.RTV_ABB_InvertorStNames[i]);
                            listviewItem.SubItems.Add("-");
                        }
                        for (ushort i = 0; i < ConstClass.RTV_ABB_InvertorCtrlNames.GetLength(0); i++)
                        {
                            listviewItem = lv_InvertorCtrl.Items.Add(ConstClass.RTV_ABB_InvertorCtrlNames[i]);
                            listviewItem.SubItems.Add("-");
                        }
                        break;

                }
            }
        }

        //상태데이터 화면 표출 함수
        public unsafe void SET_InvertorSt(byte[] datas)
        {
            dev_REC_Invertor = (VEXI_DEFS.TEMSRTV_REC_InvertorRes)Global_Class.UTIL_BytesToStructure(datas, typeof(VEXI_DEFS.TEMSRTV_REC_InvertorRes));

            Display_InvertorSt();
        }


        private unsafe void Display_InvertorSt()
        {
            if (INVType != dev_REC_Invertor.InvertorType)
            {
                Display_Init();
            }


            switch (INVType)
            {
                case 1: lblInvertorType.Text = "ABB"; break;
                case 2: lblInvertorType.Text = "SEW"; break;
                default: lblInvertorType.Text = ""; break;
            }

            byte INV_INDEX = 0;
            if (radioButton1.Checked) INV_INDEX = 0;
            else if (radioButton2.Checked) INV_INDEX = 1;
            else if (radioButton3.Checked) INV_INDEX = 2;
            else if (radioButton4.Checked) INV_INDEX = 3;
            else if (radioButton5.Checked) INV_INDEX = 4;
            else if (radioButton6.Checked) INV_INDEX = 5;

            switch (INVType)
                {
                    case 2:
                    fixed (VEXI_DEFS.TEMSRTV_SEWInvertorStRec* Ptr_1 = &dev_REC_Invertor.SEW_INV.Invertor_1_St)
                    {

                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_1, 15)) lv_InvertorSt.Items[0].SubItems[1].Text = "1"; else lv_InvertorSt.Items[0].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_1, 14)) lv_InvertorSt.Items[1].SubItems[1].Text = "1"; else lv_InvertorSt.Items[1].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_1, 12)) lv_InvertorSt.Items[2].SubItems[1].Text = "1"; else lv_InvertorSt.Items[2].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_1, 11)) lv_InvertorSt.Items[3].SubItems[1].Text = "1"; else lv_InvertorSt.Items[3].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_1, 10)) lv_InvertorSt.Items[4].SubItems[1].Text = "1"; else lv_InvertorSt.Items[4].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_1, 9)) lv_InvertorSt.Items[5].SubItems[1].Text = "1"; else lv_InvertorSt.Items[5].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_1, 8)) lv_InvertorSt.Items[6].SubItems[1].Text = "1"; else lv_InvertorSt.Items[6].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_1, 7)) lv_InvertorSt.Items[7].SubItems[1].Text = "1"; else lv_InvertorSt.Items[7].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_1, 6)) lv_InvertorSt.Items[8].SubItems[1].Text = "1"; else lv_InvertorSt.Items[8].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_1, 5)) lv_InvertorSt.Items[9].SubItems[1].Text = "1"; else lv_InvertorSt.Items[9].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_1, 4)) lv_InvertorSt.Items[10].SubItems[1].Text = "1"; else lv_InvertorSt.Items[10].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_1, 3)) lv_InvertorSt.Items[11].SubItems[1].Text = "1"; else lv_InvertorSt.Items[11].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_1, 2)) lv_InvertorSt.Items[12].SubItems[1].Text = "1"; else lv_InvertorSt.Items[12].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_1, 1)) lv_InvertorSt.Items[13].SubItems[1].Text = "1"; else lv_InvertorSt.Items[13].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_1, 0)) lv_InvertorSt.Items[14].SubItems[1].Text = "1"; else lv_InvertorSt.Items[14].SubItems[1].Text = "0";

                        lv_InvertorSt.Items[15].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_1);
                        lv_InvertorSt.Items[16].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_2);
                        lv_InvertorSt.Items[17].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_3);

                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_2, 2)) lv_InvertorSt.Items[18].SubItems[1].Text = "1"; else lv_InvertorSt.Items[18].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_2, 1)) lv_InvertorSt.Items[19].SubItems[1].Text = "1"; else lv_InvertorSt.Items[19].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit_2, 0)) lv_InvertorSt.Items[20].SubItems[1].Text = "1"; else lv_InvertorSt.Items[20].SubItems[1].Text = "0";

                        lv_InvertorSt.Items[21].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_4);
                        lv_InvertorSt.Items[22].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_5);
                    }

                    fixed (VEXI_DEFS.TEMSRTV_SEWInvertorCtrlRec* Ptr_1 = &dev_REC_Invertor.SEW_INV.Invertor_1_Ctrl)
                    {
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit_1, 15)) lv_InvertorCtrl.Items[0].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[0].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit_1, 14)) lv_InvertorCtrl.Items[1].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[1].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit_1, 13)) lv_InvertorCtrl.Items[2].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[2].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit_1, 12)) lv_InvertorCtrl.Items[3].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[3].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit_1, 10)) lv_InvertorCtrl.Items[4].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[4].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit_1, 8)) lv_InvertorCtrl.Items[5].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[5].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit_1, 7)) lv_InvertorCtrl.Items[6].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[6].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit_1, 6)) lv_InvertorCtrl.Items[7].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[7].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit_1, 5)) lv_InvertorCtrl.Items[8].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[8].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit_1, 4)) lv_InvertorCtrl.Items[9].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[9].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit_1, 3)) lv_InvertorCtrl.Items[10].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[10].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit_1, 1)) lv_InvertorCtrl.Items[11].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[11].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit_1, 0)) lv_InvertorCtrl.Items[12].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[12].SubItems[1].Text = "0";

                        lv_InvertorCtrl.Items[13].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_1);
                        lv_InvertorCtrl.Items[14].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_2);
                        lv_InvertorCtrl.Items[15].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_3);

                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit_2, 0)) lv_InvertorCtrl.Items[16].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[16].SubItems[1].Text = "0";

                        lv_InvertorCtrl.Items[17].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_4);
                        lv_InvertorCtrl.Items[18].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_5);
                        lv_InvertorCtrl.Items[19].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_6);
                    }
                    break;
                    default:
                        fixed (VEXI_DEFS.TRTV_ABBInvertorStRec* Ptr_1 = &dev_REC_Invertor.ABB_INV.Invertor_1_St)
                        {

                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit, 11)) lv_InvertorSt.Items[0].SubItems[1].Text = "1"; else lv_InvertorSt.Items[0].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit, 10)) lv_InvertorSt.Items[1].SubItems[1].Text = "1"; else lv_InvertorSt.Items[1].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit, 9)) lv_InvertorSt.Items[2].SubItems[1].Text = "1"; else lv_InvertorSt.Items[2].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit, 7)) lv_InvertorSt.Items[3].SubItems[1].Text = "1"; else lv_InvertorSt.Items[3].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit, 6)) lv_InvertorSt.Items[4].SubItems[1].Text = "1"; else lv_InvertorSt.Items[4].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit, 5)) lv_InvertorSt.Items[5].SubItems[1].Text = "1"; else lv_InvertorSt.Items[5].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit, 4)) lv_InvertorSt.Items[6].SubItems[1].Text = "1"; else lv_InvertorSt.Items[6].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit, 3)) lv_InvertorSt.Items[7].SubItems[1].Text = "1"; else lv_InvertorSt.Items[7].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit, 2)) lv_InvertorSt.Items[8].SubItems[1].Text = "1"; else lv_InvertorSt.Items[8].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit, 1)) lv_InvertorSt.Items[9].SubItems[1].Text = "1"; else lv_InvertorSt.Items[9].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->St_bit, 0)) lv_InvertorSt.Items[10].SubItems[1].Text = "1"; else lv_InvertorSt.Items[10].SubItems[1].Text = "0";

                        lv_InvertorSt.Items[11].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_1);
                        lv_InvertorSt.Items[12].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_2);
                        lv_InvertorSt.Items[13].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_3);
                        lv_InvertorSt.Items[14].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_4);
                        }

                        fixed (VEXI_DEFS.TRTV_ABBInvertorCtrlRec* Ptr_1 = &dev_REC_Invertor.ABB_INV.Invertor_1_Ctrl)
                        {
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit, 8)) lv_InvertorCtrl.Items[0].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[0].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit, 7)) lv_InvertorCtrl.Items[1].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[1].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit, 6)) lv_InvertorCtrl.Items[2].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[2].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit, 5)) lv_InvertorCtrl.Items[3].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[3].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit, 4)) lv_InvertorCtrl.Items[4].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[4].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit, 3)) lv_InvertorCtrl.Items[5].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[5].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit, 2)) lv_InvertorCtrl.Items[6].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[6].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit, 1)) lv_InvertorCtrl.Items[7].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[7].SubItems[1].Text = "0";
                        if (Global_Class.BitStatus((Ptr_1 + INV_INDEX)->Ctrl_bit, 0)) lv_InvertorCtrl.Items[8].SubItems[1].Text = "1"; else lv_InvertorCtrl.Items[8].SubItems[1].Text = "0";

                        lv_InvertorCtrl.Items[9].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_1);
                        lv_InvertorCtrl.Items[10].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_2);
                        lv_InvertorCtrl.Items[11].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_3);
                        lv_InvertorCtrl.Items[12].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_4);
                        lv_InvertorCtrl.Items[13].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_5);
                        lv_InvertorCtrl.Items[14].SubItems[1].Text = string.Format("{0}", (Ptr_1 + INV_INDEX)->Value_6);
                        }
                        break;
                }


            
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] a = new byte[481];
            a[0] = 2;
            SET_InvertorSt(a);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] a = new byte[481];
            a[0] = 1;
            SET_InvertorSt(a);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Display_InvertorSt();
        }
    }
}

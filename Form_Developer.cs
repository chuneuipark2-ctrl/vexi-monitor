using System;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;
using System.Text;
using System.IO;

namespace VEXI
{
    public partial class Form_Developer : Form
    {

        public Form_Main form_Main;

        private static VEXI_DEFS.TDEV_REC_TestCtrl dev_REC_TestCtrl;
        private static VEXI_DEFS.TDEV_REC_FunctionCtrl dev_REC_FuncCtrl;
        
        public bool IsFirst = true;

        private Label[] lbl_ST_Title;
        private Label[] lbl_CTRL_Title;
        private Label[] lbl_FUNC_Title;

        public Form_Developer()
        {
            InitializeComponent();

            lbl_ST_Title = new Label[] {lblTitle_St1,
                lblTitle_St2,
                lblTitle_St3,
                lblTitle_St4,
                lblTitle_St5,
                lblTitle_St6,
                lblTitle_St7,
                lblTitle_St8,
                lblTitle_St9,
                lblTitle_St10,
                lblTitle_St11,
                lblTitle_St12,
                lblTitle_St13,
                lblTitle_St14,
                lblTitle_St15,
                lblTitle_St16,
                lblTitle_St17,
                lblTitle_St18,
                lblTitle_St19,
                lblTitle_St20,
                lblTitle_St21,
                lblTitle_St22,
                lblTitle_St23,
                lblTitle_St24,
                lblTitle_St25,
                lblTitle_St26,
                lblTitle_St27,
                lblTitle_St28,
                lblTitle_St29,
                lblTitle_St30,
                lblTitle_St31,
                lblTitle_St32,
                lblTitle_St33,
                lblTitle_St34,
                lblTitle_St35,
                lblTitle_St36,
                lblTitle_St37,
                lblTitle_St38,
                lblTitle_St39,
                lblTitle_St40,
                lblTitle_St41,
                lblTitle_St42,
                lblTitle_St43,
                lblTitle_St44,
                lblTitle_St45,
                lblTitle_St46,
                lblTitle_St47,
                lblTitle_St48,
                lblTitle_St49,
                lblTitle_St50,
                lblTitle_St51,
                lblTitle_St52,
                lblTitle_St53,
                lblTitle_St54,
                lblTitle_St55,
                lblTitle_St56,
                lblTitle_St57,
                lblTitle_St58,
                lblTitle_St59,
                lblTitle_St60,
                lblTitle_St61,
                lblTitle_St62,
                lblTitle_St63,
                lblTitle_St64 };
            lbl_CTRL_Title = new Label[] {lblTitle_Ctrl1,
                lblTitle_Ctrl2,
                lblTitle_Ctrl3,
                lblTitle_Ctrl4,
                lblTitle_Ctrl5,
                lblTitle_Ctrl6,
                lblTitle_Ctrl7,
                lblTitle_Ctrl8,
                lblTitle_Ctrl9,
                lblTitle_Ctrl10,
                lblTitle_Ctrl11,
                lblTitle_Ctrl12,
                lblTitle_Ctrl13,
                lblTitle_Ctrl14,
                lblTitle_Ctrl15,
                lblTitle_Ctrl16 };
            lbl_FUNC_Title = new Label[] {lblTitle_Func1,
                lblTitle_Func2,
                lblTitle_Func3,
                lblTitle_Func4,
                lblTitle_Func5,
                lblTitle_Func6,
                lblTitle_Func7,
                lblTitle_Func8,
                lblTitle_Func9,
                lblTitle_Func10 };
        }

        #region 컴포넌트 이벤트
        private void Form_Developer_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }
            Display_DevTestSt();
            Load_Title();
        }

        //본 화면이 활성화 되어 있는 상태라면 Test 상태요청을 주기적으로 하도록 설정
        private void Form_Developer_Activated(object sender, EventArgs e)
        {
            form_Main.COMMDataManager.ISTestPolling = true;
        }

        private void Form_Developer_Deactivate(object sender, EventArgs e)
        {
            form_Main.COMMDataManager.ISTestPolling = false;
        }

        private void Btn_LoadTitle_Click(object sender, EventArgs e)
        {
            Load_Title();
        }
        private void ed_TestCtrl_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //입력할 수 있는 키 한정
            if (
                (char.IsControl(e.KeyChar)) ||
                (char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == Convert.ToChar(Keys.Enter)) ||
                (e.KeyChar == Convert.ToChar(Keys.Back)) ||
                (e.KeyChar == Convert.ToChar(Keys.Tab)) ||
                (e.KeyChar == '-')
                )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            if (!e.Handled)
            {
                //엔터 입력시 제어
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    TextBox ed = sender as TextBox;

                    Do_TestCtrl(Convert.ToByte(ed.Tag.ToString()));

                }
            }
        }

        private void ed_FunCtrl_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //입력할 수 있는 키 한정
            if (
                (char.IsControl(e.KeyChar)) ||
                (char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == Convert.ToChar(Keys.Enter)) ||
                (e.KeyChar == Convert.ToChar(Keys.Back)) ||
                (e.KeyChar == Convert.ToChar(Keys.Tab)) ||
                (e.KeyChar == '-')
                )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            if (!e.Handled)
            {
                //엔터 입력시 제어
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    TextBox ed = sender as TextBox;

                    Do_FuncCtrl(Convert.ToByte(ed.Tag.ToString()));
                }
            }
        }

        private void btn_FunCtrl_1_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;

            Do_FuncCtrl(Convert.ToByte(bt.Tag.ToString()));
        }

        #endregion

        #region 기능함수
        private void Load_Title()
        {
            string Title_FILE = Application.StartupPath + "\\CONFIG\\TITLE_TEST.INI";

            bool TmpIsExist = Global_Class.UTIL_File_exists(Title_FILE);

            for (int i = 1; i <= 64; i++)
            {
                lbl_ST_Title[i-1].Text = i.ToString() + " : " + IniControl.ReadString(Title_FILE, "TEST_STATUS_TITLE", i.ToString() , " ");
            }
            for (int i = 1; i <= 16; i++)
            {
                lbl_CTRL_Title[i-1].Text = i.ToString() + " : " + IniControl.ReadString(Title_FILE, "TEST_CTRL_TITLE", i.ToString(), " ");
            }
            for (int i = 1; i <= 10; i++)
            {
                lbl_FUNC_Title[i-1].Text = i.ToString() + " : " + IniControl.ReadString(Title_FILE, "TEST_Func_TITLE", i.ToString(), " ");
            }

            if (!TmpIsExist)
            {
                //StreamWriter wr = new StreamWriter(Title_FILE, false, Encoding.Unicode);
                StreamWriter wr = new StreamWriter(Title_FILE, false, Encoding.UTF8);

                wr.Close();


                for (int i = 1; i <= 64; i++)
                {
                    IniControl.WriteIni(Title_FILE, "TEST_STATUS_TITLE", i.ToString(), "");
                }
                for (int i = 1; i <= 16; i++)
                {
                    IniControl.WriteIni(Title_FILE, "TEST_CTRL_TITLE", i.ToString(), "");
                }
                for (int i = 1; i <= 10; i++)
                {
                    IniControl.WriteIni(Title_FILE, "TEST_Func_TITLE", i.ToString(), "");
                }
            }
        }
        private unsafe void Do_FuncCtrl(byte TmpFlag)
        {
            fixed (VEXI_DEFS.TDEV_REC_FunctionCtrl* DevCtrl = &dev_REC_FuncCtrl)
            {
                DevCtrl->CtrlIndex = TmpFlag;

                switch (TmpFlag)
                {
                    case 1: DevCtrl->CtrlValue = Global_Class.UTIL_StrToIntDef(ed_FunCtrl_1.Text, 0); break;
                    case 2: DevCtrl->CtrlValue = Global_Class.UTIL_StrToIntDef(ed_FunCtrl_2.Text, 0); break;
                    case 3: DevCtrl->CtrlValue = Global_Class.UTIL_StrToIntDef(ed_FunCtrl_3.Text, 0); break;
                    case 4: DevCtrl->CtrlValue = Global_Class.UTIL_StrToIntDef(ed_FunCtrl_4.Text, 0); break;
                    case 5: DevCtrl->CtrlValue = Global_Class.UTIL_StrToIntDef(ed_FunCtrl_5.Text, 0); break;
                    case 6: DevCtrl->CtrlValue = Global_Class.UTIL_StrToIntDef(ed_FunCtrl_6.Text, 0); break;
                    case 7: DevCtrl->CtrlValue = Global_Class.UTIL_StrToIntDef(ed_FunCtrl_7.Text, 0); break;
                    case 8: DevCtrl->CtrlValue = Global_Class.UTIL_StrToIntDef(ed_FunCtrl_8.Text, 0); break;
                    case 9: DevCtrl->CtrlValue = Global_Class.UTIL_StrToIntDef(ed_FunCtrl_9.Text, 0); break;
                    case 10: DevCtrl->CtrlValue = Global_Class.UTIL_StrToIntDef(ed_FunCtrl_10.Text, 0); break;
                }
            }

            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_14, dev_REC_FuncCtrl);
        }

        private unsafe void Do_TestCtrl(byte TmpFlag)
        {
            fixed (VEXI_DEFS.TDEV_REC_TestCtrl* DevCtrl = &dev_REC_TestCtrl)
            {

                Global_Class.UTIL_Byteptr_clear((byte*)DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_TestCtrl)));

                switch (TmpFlag)
                {
                    case 1: DevCtrl->CtrlFlag[0] = 0x01; break;
                    case 2: DevCtrl->CtrlFlag[0] = 0x02; break;
                    case 3: DevCtrl->CtrlFlag[0] = 0x04; break;
                    case 4: DevCtrl->CtrlFlag[0] = 0x08; break;
                    case 5: DevCtrl->CtrlFlag[0] = 0x10; break;
                    case 6: DevCtrl->CtrlFlag[0] = 0x20; break;
                    case 7: DevCtrl->CtrlFlag[0] = 0x40; break;
                    case 8: DevCtrl->CtrlFlag[0] = 0x80; break;
                    case 9: DevCtrl->CtrlFlag[1]  = 0x01; break;
                    case 10: DevCtrl->CtrlFlag[1] = 0x02; break;
                    case 11: DevCtrl->CtrlFlag[1] = 0x04; break;
                    case 12: DevCtrl->CtrlFlag[1] = 0x08; break;
                    case 13: DevCtrl->CtrlFlag[1] = 0x10; break;
                    case 14: DevCtrl->CtrlFlag[1] = 0x20; break;
                    case 15: DevCtrl->CtrlFlag[1] = 0x40; break;
                    case 16: DevCtrl->CtrlFlag[1] = 0x80; break;
                }

                DevCtrl->Ctrl[0] = Global_Class.UTIL_StrToIntDef(ed_TestCtrl_1.Text, 0);
                DevCtrl->Ctrl[1] = Global_Class.UTIL_StrToIntDef(ed_TestCtrl_2.Text, 0);
                DevCtrl->Ctrl[2] = Global_Class.UTIL_StrToIntDef(ed_TestCtrl_3.Text, 0);
                DevCtrl->Ctrl[3] = Global_Class.UTIL_StrToIntDef(ed_TestCtrl_4.Text, 0);
                DevCtrl->Ctrl[4] = Global_Class.UTIL_StrToIntDef(ed_TestCtrl_5.Text, 0);
                DevCtrl->Ctrl[5] = Global_Class.UTIL_StrToIntDef(ed_TestCtrl_6.Text, 0);
                DevCtrl->Ctrl[6] = Global_Class.UTIL_StrToIntDef(ed_TestCtrl_7.Text, 0);
                DevCtrl->Ctrl[7] = Global_Class.UTIL_StrToIntDef(ed_TestCtrl_8.Text, 0);
                DevCtrl->Ctrl[8] = Global_Class.UTIL_StrToIntDef(ed_TestCtrl_9.Text, 0);
                DevCtrl->Ctrl[9] = Global_Class.UTIL_StrToIntDef(ed_TestCtrl_10.Text, 0);
                DevCtrl->Ctrl[10] = Global_Class.UTIL_StrToIntDef(ed_TestCtrl_11.Text, 0);
                DevCtrl->Ctrl[11] = Global_Class.UTIL_StrToIntDef(ed_TestCtrl_12.Text, 0);
                DevCtrl->Ctrl[12] = Global_Class.UTIL_StrToIntDef(ed_TestCtrl_13.Text, 0);
                DevCtrl->Ctrl[13] = Global_Class.UTIL_StrToIntDef(ed_TestCtrl_14.Text, 0);
                DevCtrl->Ctrl[14] = Global_Class.UTIL_StrToIntDef(ed_TestCtrl_15.Text, 0);
                DevCtrl->Ctrl[15] = Global_Class.UTIL_StrToIntDef(ed_TestCtrl_16.Text, 0);
            }

            form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_13, dev_REC_TestCtrl);

        }

        //TEST 상태응답 데이터 화면 표출
        public unsafe void Display_DevTestSt()
        {
            {
                fixed (VEXI_DEFS.TDEV_REC_TestStRes* DevSt = &form_Main.COMMDataManager.DevRec.dev_REC_TestSt)
                {
                    //Flag_In_XXXX 는 해당 데이터가 수신된 적이 있는지에 대한 변수임
                    if (form_Main.COMMDataManager.DevRec.Flag_In_8112)
                    //if (true) //테스트시
                    {
                        lbl_TestSt_1.Text = string.Format("{0} (0x{0:X8})", DevSt->St[0]);
                        lbl_TestSt_2.Text = string.Format("{0} (0x{0:X8})", DevSt->St[1]);
                        lbl_TestSt_3.Text = string.Format("{0} (0x{0:X8})", DevSt->St[2]);
                        lbl_TestSt_4.Text = string.Format("{0} (0x{0:X8})", DevSt->St[3]);
                        lbl_TestSt_5.Text = string.Format("{0} (0x{0:X8})", DevSt->St[4]);
                        lbl_TestSt_6.Text = string.Format("{0} (0x{0:X8})", DevSt->St[5]);
                        lbl_TestSt_7.Text = string.Format("{0} (0x{0:X8})", DevSt->St[6]);
                        lbl_TestSt_8.Text = string.Format("{0} (0x{0:X8})", DevSt->St[7]);
                        lbl_TestSt_9.Text = string.Format("{0} (0x{0:X8})", DevSt->St[8]);
                        lbl_TestSt_10.Text = string.Format("{0} (0x{0:X8})", DevSt->St[9]);
                        lbl_TestSt_11.Text = string.Format("{0} (0x{0:X8})", DevSt->St[10]);
                        lbl_TestSt_12.Text = string.Format("{0} (0x{0:X8})", DevSt->St[11]);
                        lbl_TestSt_13.Text = string.Format("{0} (0x{0:X8})", DevSt->St[12]);
                        lbl_TestSt_14.Text = string.Format("{0} (0x{0:X8})", DevSt->St[13]);
                        lbl_TestSt_15.Text = string.Format("{0} (0x{0:X8})", DevSt->St[14]);
                        lbl_TestSt_16.Text = string.Format("{0} (0x{0:X8})", DevSt->St[15]);
                        lbl_TestSt_17.Text = string.Format("{0} (0x{0:X8})", DevSt->St[16]);
                        lbl_TestSt_18.Text = string.Format("{0} (0x{0:X8})", DevSt->St[17]);
                        lbl_TestSt_19.Text = string.Format("{0} (0x{0:X8})", DevSt->St[18]);
                        lbl_TestSt_20.Text = string.Format("{0} (0x{0:X8})", DevSt->St[19]);
                        lbl_TestSt_21.Text = string.Format("{0} (0x{0:X8})", DevSt->St[20]);
                        lbl_TestSt_22.Text = string.Format("{0} (0x{0:X8})", DevSt->St[21]);
                        lbl_TestSt_23.Text = string.Format("{0} (0x{0:X8})", DevSt->St[22]);
                        lbl_TestSt_24.Text = string.Format("{0} (0x{0:X8})", DevSt->St[23]);
                        lbl_TestSt_25.Text = string.Format("{0} (0x{0:X8})", DevSt->St[24]);
                        lbl_TestSt_26.Text = string.Format("{0} (0x{0:X8})", DevSt->St[25]);
                        lbl_TestSt_27.Text = string.Format("{0} (0x{0:X8})", DevSt->St[26]);
                        lbl_TestSt_28.Text = string.Format("{0} (0x{0:X8})", DevSt->St[27]);
                        lbl_TestSt_29.Text = string.Format("{0} (0x{0:X8})", DevSt->St[28]);
                        lbl_TestSt_30.Text = string.Format("{0} (0x{0:X8})", DevSt->St[29]);
                        lbl_TestSt_31.Text = string.Format("{0} (0x{0:X8})", DevSt->St[30]);
                        lbl_TestSt_32.Text = string.Format("{0} (0x{0:X8})", DevSt->St[31]);
                        lbl_TestSt_33.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[0]);
                        lbl_TestSt_34.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[1]);
                        lbl_TestSt_35.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[2]);
                        lbl_TestSt_36.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[3]);
                        lbl_TestSt_37.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[4]);
                        lbl_TestSt_38.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[5]);
                        lbl_TestSt_39.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[6]);
                        lbl_TestSt_40.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[7]);
                        lbl_TestSt_41.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[8]);
                        lbl_TestSt_42.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[9]);
                        lbl_TestSt_43.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[10]);
                        lbl_TestSt_44.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[11]);
                        lbl_TestSt_45.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[12]);
                        lbl_TestSt_46.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[13]);
                        lbl_TestSt_47.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[14]);
                        lbl_TestSt_48.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[15]);
                        lbl_TestSt_49.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[16]);
                        lbl_TestSt_50.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[17]);
                        lbl_TestSt_51.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[18]);
                        lbl_TestSt_52.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[19]);
                        lbl_TestSt_53.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[20]);
                        lbl_TestSt_54.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[21]);
                        lbl_TestSt_55.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[22]);
                        lbl_TestSt_56.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[23]);
                        lbl_TestSt_57.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[24]);
                        lbl_TestSt_58.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[25]);
                        lbl_TestSt_59.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[26]);
                        lbl_TestSt_60.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[27]);
                        lbl_TestSt_61.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[28]);
                        lbl_TestSt_62.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[29]);
                        lbl_TestSt_63.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[30]);
                        lbl_TestSt_64.Text = string.Format("{0} (0x{0:X8})", DevSt->St_2[31]);


                        lbl_TestCtrl_1.Text = string.Format("{0}", DevSt->Ctrl[0]);
                        lbl_TestCtrl_2.Text = string.Format("{0}", DevSt->Ctrl[1]);
                        lbl_TestCtrl_3.Text = string.Format("{0}", DevSt->Ctrl[2]);
                        lbl_TestCtrl_4.Text = string.Format("{0}", DevSt->Ctrl[3]);
                        lbl_TestCtrl_5.Text = string.Format("{0}", DevSt->Ctrl[4]);
                        lbl_TestCtrl_6.Text = string.Format("{0}", DevSt->Ctrl[5]);
                        lbl_TestCtrl_7.Text = string.Format("{0}", DevSt->Ctrl[6]);
                        lbl_TestCtrl_8.Text = string.Format("{0}", DevSt->Ctrl[7]);
                        lbl_TestCtrl_9.Text = string.Format("{0}", DevSt->Ctrl[8]);
                        lbl_TestCtrl_10.Text =string.Format("{0}", DevSt->Ctrl[9]);
                        lbl_TestCtrl_11.Text =string.Format("{0}", DevSt->Ctrl[10]);
                        lbl_TestCtrl_12.Text =string.Format("{0}", DevSt->Ctrl[11]);
                        lbl_TestCtrl_13.Text =string.Format("{0}", DevSt->Ctrl[12]);
                        lbl_TestCtrl_14.Text =string.Format("{0}", DevSt->Ctrl[13]);
                        lbl_TestCtrl_15.Text =string.Format("{0}", DevSt->Ctrl[14]);
                        lbl_TestCtrl_16.Text = string.Format("{0}", DevSt->Ctrl[15]);


                        if (IsFirst) ed_TestCtrl_1.Text = lbl_TestCtrl_1.Text;
                        if (IsFirst) ed_TestCtrl_2.Text = lbl_TestCtrl_2.Text;
                        if (IsFirst) ed_TestCtrl_3.Text = lbl_TestCtrl_3.Text;
                        if (IsFirst) ed_TestCtrl_4.Text = lbl_TestCtrl_4.Text;
                        if (IsFirst) ed_TestCtrl_5.Text = lbl_TestCtrl_5.Text;
                        if (IsFirst) ed_TestCtrl_6.Text = lbl_TestCtrl_6.Text;
                        if (IsFirst) ed_TestCtrl_7.Text = lbl_TestCtrl_7.Text;
                        if (IsFirst) ed_TestCtrl_8.Text = lbl_TestCtrl_8.Text;
                        if (IsFirst) ed_TestCtrl_9.Text = lbl_TestCtrl_9.Text;
                        if (IsFirst) ed_TestCtrl_10.Text = lbl_TestCtrl_10.Text;
                        if (IsFirst) ed_TestCtrl_11.Text = lbl_TestCtrl_11.Text;
                        if (IsFirst) ed_TestCtrl_12.Text = lbl_TestCtrl_12.Text;
                        if (IsFirst) ed_TestCtrl_13.Text = lbl_TestCtrl_13.Text;
                        if (IsFirst) ed_TestCtrl_14.Text = lbl_TestCtrl_14.Text;
                        if (IsFirst) ed_TestCtrl_15.Text = lbl_TestCtrl_15.Text;
                        if (IsFirst) ed_TestCtrl_16.Text = lbl_TestCtrl_16.Text;

                        IsFirst = false;
                    }
                    else
                    {
                        lbl_TestSt_1.Text = "";
                        lbl_TestSt_2.Text = "";
                        lbl_TestSt_3.Text = "";
                        lbl_TestSt_4.Text = "";
                        lbl_TestSt_5.Text = "";
                        lbl_TestSt_6.Text = "";
                        lbl_TestSt_7.Text = "";
                        lbl_TestSt_8.Text = "";
                        lbl_TestSt_9.Text = "";
                        lbl_TestSt_10.Text = "";
                        lbl_TestSt_11.Text = "";
                        lbl_TestSt_12.Text = "";
                        lbl_TestSt_13.Text = "";
                        lbl_TestSt_14.Text = "";
                        lbl_TestSt_15.Text = "";
                        lbl_TestSt_16.Text = "";
                        lbl_TestSt_17.Text = "";
                        lbl_TestSt_18.Text = "";
                        lbl_TestSt_19.Text = "";
                        lbl_TestSt_20.Text = "";
                        lbl_TestSt_21.Text = "";
                        lbl_TestSt_22.Text = "";
                        lbl_TestSt_23.Text = "";
                        lbl_TestSt_24.Text = "";
                        lbl_TestSt_25.Text = "";
                        lbl_TestSt_26.Text = "";
                        lbl_TestSt_27.Text = "";
                        lbl_TestSt_28.Text = "";
                        lbl_TestSt_29.Text = "";
                        lbl_TestSt_30.Text = "";
                        lbl_TestSt_31.Text = "";
                        lbl_TestSt_32.Text = "";
                        lbl_TestSt_33.Text = "";
                        lbl_TestSt_34.Text = "";
                        lbl_TestSt_35.Text = "";
                        lbl_TestSt_36.Text = "";
                        lbl_TestSt_37.Text = "";
                        lbl_TestSt_38.Text = "";
                        lbl_TestSt_39.Text = "";
                        lbl_TestSt_40.Text = "";
                        lbl_TestSt_41.Text = "";
                        lbl_TestSt_42.Text = "";
                        lbl_TestSt_43.Text = "";
                        lbl_TestSt_44.Text = "";
                        lbl_TestSt_45.Text = "";
                        lbl_TestSt_46.Text = "";
                        lbl_TestSt_47.Text = "";
                        lbl_TestSt_48.Text = "";
                        lbl_TestSt_49.Text = "";
                        lbl_TestSt_50.Text = "";
                        lbl_TestSt_51.Text = "";
                        lbl_TestSt_52.Text = "";
                        lbl_TestSt_53.Text = "";
                        lbl_TestSt_54.Text = "";
                        lbl_TestSt_55.Text = "";
                        lbl_TestSt_56.Text = "";
                        lbl_TestSt_57.Text = "";
                        lbl_TestSt_58.Text = "";
                        lbl_TestSt_59.Text = "";
                        lbl_TestSt_60.Text = "";
                        lbl_TestSt_61.Text = "";
                        lbl_TestSt_62.Text = "";
                        lbl_TestSt_63.Text = "";
                        lbl_TestSt_64.Text = "";



                        lbl_TestCtrl_1.Text = "";
                        lbl_TestCtrl_2.Text = "";
                        lbl_TestCtrl_3.Text = "";
                        lbl_TestCtrl_4.Text = "";
                        lbl_TestCtrl_5.Text = "";
                        lbl_TestCtrl_6.Text = "";
                        lbl_TestCtrl_7.Text = "";
                        lbl_TestCtrl_8.Text = "";
                        lbl_TestCtrl_9.Text = "";
                        lbl_TestCtrl_10.Text = "";
                        lbl_TestCtrl_11.Text = "";
                        lbl_TestCtrl_12.Text = "";
                        lbl_TestCtrl_13.Text = "";
                        lbl_TestCtrl_14.Text = "";
                        lbl_TestCtrl_15.Text = "";
                        lbl_TestCtrl_16.Text = "";


                        ed_TestCtrl_1.Text = "";
                        ed_TestCtrl_2.Text = "";
                        ed_TestCtrl_3.Text = "";
                        ed_TestCtrl_4.Text = "";
                        ed_TestCtrl_5.Text = "";
                        ed_TestCtrl_6.Text = "";
                        ed_TestCtrl_7.Text = "";
                        ed_TestCtrl_8.Text = "";
                        ed_TestCtrl_9.Text = "";
                        ed_TestCtrl_10.Text = "";
                        ed_TestCtrl_11.Text = "";
                        ed_TestCtrl_12.Text = "";
                        ed_TestCtrl_13.Text = "";
                        ed_TestCtrl_14.Text = "";
                        ed_TestCtrl_15.Text = "";
                        ed_TestCtrl_16.Text = "";


                        ed_FunCtrl_1.Text = "";
                        ed_FunCtrl_2.Text = "";
                        ed_FunCtrl_3.Text = "";
                        ed_FunCtrl_4.Text = "";
                        ed_FunCtrl_5.Text = "";
                        ed_FunCtrl_6.Text = "";
                        ed_FunCtrl_7.Text = "";
                        ed_FunCtrl_8.Text = "";
                        ed_FunCtrl_9.Text = "";
                        ed_FunCtrl_10.Text = "";
                    }
                }
            }

        }


        #endregion

        private void label19_Click(object sender, EventArgs e)
        {

        }
    }
}

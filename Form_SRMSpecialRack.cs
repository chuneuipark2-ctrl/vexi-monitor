using System;
using System.IO;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_SRMSpecialRack : Form
    {

        public Form_Main form_Main;
        public static VEXI_DEFS.TSRM_SpecialRack srm_SpecialRackRes;
        public static VEXI_DEFS.TSRM_SpecialRack srm_SpecialRackCTRL;

        public Form_SRMSpecialRack()
        {
            InitializeComponent();
        }

        #region 컴포넌트 이벤트
        private void Form_SpecialRack_Load(object sender, EventArgs e)
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

            btn_SpecialRack_Set.Enabled = false;

        }

        private void lv_SpecialRack_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i;
            if (lv_SpecialRack.SelectedItems.Count > 0)
            {
                i = lv_SpecialRack.SelectedItems[0].Index;
            }
            else
            {
                i = -1;
            }

            if (i >= 0)
            {
                Display_SelectIndex((byte)i);
            }

        }

        //통합파일에서 불러오는 것으로 변경
        //개별파일에서 불러오는 소스는 남겨놓음
        private void btn_SpecialRack_FileRead_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.SRack|*.SRACK";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] Savebytes;

                using (BinaryReader br = new BinaryReader(File.Open(openFileDialog1.FileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        ushort Len = (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_SpecialRack));
                        if (br.BaseStream.Length == Len)
                        {
                            Savebytes = br.ReadBytes(Len);
                            srm_SpecialRackCTRL = (VEXI_DEFS.TSRM_SpecialRack)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_SpecialRack));
                            srm_SpecialRackRes = (VEXI_DEFS.TSRM_SpecialRack)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_SpecialRack));

                            Validate_srm_SpecialRack_CTRL();
                            Display_SpecialRack();
                            if ((form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode & 0x08) == 0)
                            {
                                btn_SpecialRack_Set.Enabled = false;
                            }
                            else
                            {
                                btn_SpecialRack_Set.Enabled = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("현재 프로토콜과 맞지 않는 파일입니다");
                        }
                    }
                    finally
                    {
                        br.Close();
                    }
                }
            }
        }

        private void btn_LoadTotalFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.SRMcfg|*.SRMCFG";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
                if (form_Main.SRM_ToTalFile.Read_SRM_SpecialRack(ref srm_SpecialRackCTRL))
                {
                    srm_SpecialRackRes = srm_SpecialRackCTRL;
                    Validate_srm_SpecialRack_CTRL();
                    Display_SpecialRack();
                    if ((form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode & 0x08) == 0)
                    {
                        btn_SpecialRack_Set.Enabled = false;
                    }
                    else
                    {
                        btn_SpecialRack_Set.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("해당 파일안에 [" + this.Text + "] 데이터가 없습니다");
                }
            }
        }

        //통합파일에 저장하는 것으로 변경
        //개별파일에 저장하는 소스는 남겨놓음
        private void btn_SpecialRack_FileWrite_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "*.SRack|*.SRACK";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (BinaryWriter br = new BinaryWriter(File.Open(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write)))
                {
                    try
                    {
                        br.Seek(0, SeekOrigin.Begin);
                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_SpecialRack))];
                        Global_Class.UTIL_StructObjectToByteArray(srm_SpecialRackCTRL, Savebytes);
                        br.Write(Savebytes);
                    }
                    finally
                    {
                        br.Close();
                    }

                }
            }
        }

        private void btn_SaveTotalFile_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "*.SRMcfg|*.SRMCFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
                form_Main.SRM_ToTalFile.Write_SRM_SpecialRack(srm_SpecialRackCTRL);
            }
        }

        private void btnAllClear_Click(object sender, EventArgs e)
        {
            srm_SpecialRackCTRL.SetCount = 0;
            Validate_srm_SpecialRack_CTRL();
            Display_SpecialRack();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            byte TmpIndex = srm_SpecialRackCTRL.SetCount;

            if (lv_SpecialRack.Items.Count == 1)
            {
                lv_SpecialRack.Items[0].Selected = true;
            }

            if (srm_SpecialRackCTRL.SetCount < 100)
            {
                ADD_SpecialRack(1);
            }
        }

        private void btnAdd_2_Click(object sender, EventArgs e)
        {
            byte TmpIndex = srm_SpecialRackCTRL.SetCount;

            if (lv_SpecialRack.Items.Count == 1)
            {
                lv_SpecialRack.Items[0].Selected = true;
            }

            if (srm_SpecialRackCTRL.SetCount < 100)
            {
                ADD_SpecialRack(2);
            }
        }

        private void btnUIpdate_Click(object sender, EventArgs e)
        {
            if (lv_SpecialRack.SelectedItems.Count == 0)
            {

            }
            else
            {
                Update_SpecialRack();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lv_SpecialRack.SelectedItems.Count == 0)
            {

            }
            else
            {
                Delete_SpecialRack();
            }
        }


        private void btn_SpecialRack_Set_Click(object sender, EventArgs e)
        {
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "스폐셜랙 설정을 장치에 다운로드 하시겠습니까 ? "))
            {
                Do_Ctrl(false);
            }
        }

        private void btn_SpecialRack_Load_Click(object sender, EventArgs e)
        {
            srm_SpecialRackCTRL.SetCount = 0;
            Validate_srm_SpecialRack_CTRL();
            Display_SpecialRack();

            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_9E, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_SpecialRackReq)));
        }

        private void ckItemNone_Click(object sender, EventArgs e)
        {
            if (ckItemNone.Checked)
            {
                ckItem_0.Checked = false;
                ckItem_1.Checked = false;
                ckItem_2.Checked = false;
                ckItem_3.Checked = false;
                ckItem_4.Checked = false;
                ckItem_5.Checked = false;
                ckItem_6.Checked = false;
                ckItem_7.Checked = false;
            }
        }

        private void ckItem_0_Click(object sender, EventArgs e)
        {
            if ((ckItem_0.Checked) ||
                    (ckItem_1.Checked) ||
                    (ckItem_2.Checked) ||
                    (ckItem_3.Checked) ||
                    (ckItem_4.Checked) ||
                    (ckItem_5.Checked) ||
                    (ckItem_6.Checked) ||
                    (ckItem_7.Checked))
            {
                ckItemNone.Checked = false;
            }
            else
            {
                ckItemNone.Checked = true;
            }
        }

  
        private void button1_Click(object sender, EventArgs e)
        {
            if (lv_SpecialRack.SelectedItems.Count == 0)
            {

            }
            else
            {
                ChangeIndex_UP((byte)lv_SpecialRack.SelectedItems[0].Index);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lv_SpecialRack.SelectedItems.Count == 0)
            {

            }
            else
            {
                ChangeIndex_DOWN((byte)lv_SpecialRack.SelectedItems[0].Index);
            }
        }

        

        #endregion

        #region 기능함수
        public void Display_SpecialRack(byte[] data)
        {
            srm_SpecialRackRes = (VEXI_DEFS.TSRM_SpecialRack)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TSRM_SpecialRack));
            srm_SpecialRackCTRL = (VEXI_DEFS.TSRM_SpecialRack)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TSRM_SpecialRack));
            Validate_srm_SpecialRack_CTRL();
            Display_SpecialRack();
            if ((form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode & 0x08) == 0)
            {
                btn_SpecialRack_Set.Enabled = false;
            }
            else
            {
                btn_SpecialRack_Set.Enabled = true;
            }
        }

        private unsafe void Display_Init()
        {
            fixed (VEXI_DEFS.TSRM_SpecialRack* Data = &srm_SpecialRackRes)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)Data, Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_SpecialRack)));
            }
            fixed (VEXI_DEFS.TSRM_SpecialRack* Data = &srm_SpecialRackCTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)Data, Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_SpecialRack)));
            }

            lv_SpecialRack.Items.Clear();
        }
        private unsafe void Display_SpecialRack()
        {
            {
                //제어구조체로 Display 하기
                fixed (VEXI_DEFS.TSRM_SpecialRackItem* Data = &srm_SpecialRackCTRL.SpecialRack1)
                {
                    ListViewItem listviewitem;

                    if (srm_SpecialRackCTRL.SetCount < lv_SpecialRack.Items.Count)
                    {
                        byte Gap = (byte)(lv_SpecialRack.Items.Count - srm_SpecialRackCTRL.SetCount);
                        for (byte i = 0; i < Gap; i++)
                        {
                            lv_SpecialRack.Items.RemoveAt(0);
                        }
                    } else if (srm_SpecialRackCTRL.SetCount > lv_SpecialRack.Items.Count)
                    {
                        byte Gap = (byte)(srm_SpecialRackCTRL.SetCount - lv_SpecialRack.Items.Count);
                        for (byte i = 0; i < Gap; i++)
                        {
                            listviewitem = lv_SpecialRack.Items.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                        }
                    }

                    if (srm_SpecialRackCTRL.SetCount > 0)
                    {
                        for (byte i = 0; i < srm_SpecialRackCTRL.SetCount; i++)
                        {
                            listviewitem = lv_SpecialRack.Items[i];
                            listviewitem.SubItems[0].Text = string.Format("{0}", i + 1);


                            if (((Data + i)->BayID >= 0) && ((Data + i)->BayID <= 256))
                            {
                                listviewitem.SubItems[1].Text = cbBay.Items[(Data + i)->BayID].ToString();
                            }
                            else
                            {
                                listviewitem.SubItems[1].Text = cbBay.Items[0].ToString();
                            }

                            if (((Data + i)->Level_ID >= 0) && ((Data + i)->Level_ID <= 128))
                            {
                                listviewitem.SubItems[2].Text = cbLevel.Items[(Data + i)->Level_ID].ToString();
                            }
                            else
                            {
                                listviewitem.SubItems[2].Text = cbLevel.Items[0].ToString();
                            }


                            if (Global_Class.BitStatus((Data + i)->Row, 0))
                            {
                                listviewitem.SubItems[3].Text = "적용";
                            }
                            else
                            {
                                listviewitem.SubItems[3].Text = "";
                            }

                            if (Global_Class.BitStatus((Data + i)->Row, 1))
                            {
                                listviewitem.SubItems[4].Text = "적용";
                            }
                            else
                            {
                                listviewitem.SubItems[4].Text = "";
                            }

                            if (Global_Class.BitStatus((Data + i)->Row, 2))
                            {
                                listviewitem.SubItems[5].Text = "적용";
                            }
                            else
                            {
                                listviewitem.SubItems[6].Text = "";
                            }

                            if (Global_Class.BitStatus((Data + i)->Row, 3))
                            {
                                listviewitem.SubItems[6].Text = "적용";
                            }
                            else
                            {
                                listviewitem.SubItems[6].Text = "";
                            }


                            if ((Data + i)->ItemType == 0)
                            {
                                listviewitem.SubItems[7].Text = "X";
                                listviewitem.SubItems[8].Text = "X";
                                listviewitem.SubItems[9].Text = "X";
                                listviewitem.SubItems[10].Text = "X";
                                listviewitem.SubItems[11].Text = "X";
                                listviewitem.SubItems[12].Text = "X";
                                listviewitem.SubItems[13].Text = "X";
                                listviewitem.SubItems[14].Text = "X";
                            } else
                            {
                                if (Global_Class.BitStatus((Data + i)->ItemType, 0)) listviewitem.SubItems[7].Text = "O";
                                else listviewitem.SubItems[7].Text = "X";
                                if (Global_Class.BitStatus((Data + i)->ItemType, 1)) listviewitem.SubItems[8].Text = "O";
                                else listviewitem.SubItems[8].Text = "X";
                                if (Global_Class.BitStatus((Data + i)->ItemType, 2)) listviewitem.SubItems[9].Text = "O";
                                else listviewitem.SubItems[9].Text = "X";
                                if (Global_Class.BitStatus((Data + i)->ItemType, 3)) listviewitem.SubItems[10].Text = "O";
                                else listviewitem.SubItems[10].Text = "X";
                                if (Global_Class.BitStatus((Data + i)->ItemType, 4)) listviewitem.SubItems[11].Text = "O";
                                else listviewitem.SubItems[11].Text = "X";
                                if (Global_Class.BitStatus((Data + i)->ItemType, 5)) listviewitem.SubItems[12].Text = "O";
                                else listviewitem.SubItems[12].Text = "X";
                                if (Global_Class.BitStatus((Data + i)->ItemType, 6)) listviewitem.SubItems[13].Text = "O";
                                else listviewitem.SubItems[13].Text = "X";
                                if (Global_Class.BitStatus((Data + i)->ItemType, 7)) listviewitem.SubItems[14].Text = "O";
                                else listviewitem.SubItems[14].Text = "X";
                            }
                        }
                    }
                }
            }

        }

        private unsafe void Display_SelectIndex(byte TmpIndex)
        {
            fixed (VEXI_DEFS.TSRM_SpecialRackItem* Data = &srm_SpecialRackCTRL.SpecialRack1)
            {
                if (srm_SpecialRackCTRL.SetCount > TmpIndex)
                {
                    lbl_SelectedIndex.Text = string.Format("{0}", (TmpIndex + 1));
                    
                    if (((Data + TmpIndex)->BayID > 0) && ((Data + TmpIndex)->BayID <= 256))
                    {
                        cbBay.SelectedIndex = (Data + TmpIndex)->BayID;
                    }
                    else
                    {
                        cbBay.SelectedIndex = 0;
                    }

                    if (((Data + TmpIndex)->Level_ID > 0) && ((Data + TmpIndex)->Level_ID <= 128))
                    {
                        cbLevel.SelectedIndex = (Data + TmpIndex)->Level_ID;
                    }
                    else
                    {
                        cbLevel.SelectedIndex = 0;
                    }



                    if (Global_Class.BitStatus((Data + TmpIndex)->Row, 0))
                    {
                        ckRow_1.Checked = true;
                    }
                    else
                    {
                        ckRow_1.Checked = false;
                    }

                    if (Global_Class.BitStatus((Data + TmpIndex)->Row, 1))
                    {
                        ckRow_2.Checked = true;
                    }
                    else
                    {
                        ckRow_2.Checked = false;
                    }

                    if (Global_Class.BitStatus((Data + TmpIndex)->Row, 2))
                    {
                        ckRow_3.Checked = true;
                    }
                    else
                    {
                        ckRow_3.Checked = false;
                    }

                    if (Global_Class.BitStatus((Data + TmpIndex)->Row, 3))
                    {
                        ckRow_4.Checked = true;
                    }
                    else
                    {
                        ckRow_4.Checked = false;
                    }


                    if ((ckRow_1.Checked) &&
                        (ckRow_2.Checked) &&
                        (ckRow_3.Checked) &&
                        (ckRow_4.Checked))
                    {
                        ckRowAll.Checked = true;
                    }
                    else
                    {
                        ckRowAll.Checked = false;
                    }

                    if ((Data + TmpIndex)->ItemType == 0)
                    {
                        ckItemNone.Checked = true;
                        ckItem_0.Checked = false;
                        ckItem_1.Checked = false;
                        ckItem_2.Checked = false;
                        ckItem_3.Checked = false;
                        ckItem_4.Checked = false;
                        ckItem_5.Checked = false;
                        ckItem_6.Checked = false;
                        ckItem_7.Checked = false;
                    }
                    else
                    {
                        ckItemNone.Checked = false;
                        ckItem_0.Checked = (Global_Class.BitStatus((Data + TmpIndex)->ItemType, 0));
                        ckItem_1.Checked = (Global_Class.BitStatus((Data + TmpIndex)->ItemType, 1));
                        ckItem_2.Checked = (Global_Class.BitStatus((Data + TmpIndex)->ItemType, 2));
                        ckItem_3.Checked = (Global_Class.BitStatus((Data + TmpIndex)->ItemType, 3));
                        ckItem_4.Checked = (Global_Class.BitStatus((Data + TmpIndex)->ItemType, 4));
                        ckItem_5.Checked = (Global_Class.BitStatus((Data + TmpIndex)->ItemType, 5));
                        ckItem_6.Checked = (Global_Class.BitStatus((Data + TmpIndex)->ItemType, 6));
                        ckItem_7.Checked = (Global_Class.BitStatus((Data + TmpIndex)->ItemType, 7));
                    }
                }
            }
        }
        private unsafe void ADD_SpecialRack(byte ADDType)
        {
            byte TmpIndex ;

            if (ADDType == 1)
            {
                if ((lv_SpecialRack.SelectedItems.Count == 0) || ((lv_SpecialRack.SelectedItems.Count > 0) && (lv_SpecialRack.SelectedItems[0].Index == 0)))
                {
                    // 맨앞에 추가
                    TmpIndex = 0;

                    if (srm_SpecialRackCTRL.SetCount > 0)
                    {
                        fixed (VEXI_DEFS.TSRM_SpecialRackItem* Data = &srm_SpecialRackCTRL.SpecialRack1)
                        {
                            for (byte i = srm_SpecialRackCTRL.SetCount; i > 0; i--)
                            {
                                (Data + i)->BayID = (Data + i - 1)->BayID;
                                (Data + i)->Level_ID = (Data + i - 1)->Level_ID;
                                (Data + i)->Row = (Data + i - 1)->Row;
                                (Data + i)->ItemType = (Data + i - 1)->ItemType;
                            }
                        }
                    }

                } else
                {
                    TmpIndex = (byte)lv_SpecialRack.SelectedItems[0].Index;

                    fixed (VEXI_DEFS.TSRM_SpecialRackItem* Data = &srm_SpecialRackCTRL.SpecialRack1)
                    {
                        for (byte i = srm_SpecialRackCTRL.SetCount; i > TmpIndex; i--)
                        {
                            (Data + i)->BayID = (Data + i - 1)->BayID;
                            (Data + i)->Level_ID = (Data + i - 1)->Level_ID;
                            (Data + i)->Row = (Data + i - 1)->Row;
                            (Data + i)->ItemType = (Data + i - 1)->ItemType;
                        }
                    }
                }
            } else
            {
                if ((lv_SpecialRack.SelectedItems.Count == 0) || ((lv_SpecialRack.SelectedItems.Count > 0) && (lv_SpecialRack.SelectedItems[0].Index == lv_SpecialRack.Items.Count - 1)))
                {
                    //맨뒤 추가
                   TmpIndex = srm_SpecialRackCTRL.SetCount;
                } else
                {
                    TmpIndex = (byte)(lv_SpecialRack.SelectedItems[0].Index + 1);

                    fixed (VEXI_DEFS.TSRM_SpecialRackItem* Data = &srm_SpecialRackCTRL.SpecialRack1)
                    {
                        for (byte i = srm_SpecialRackCTRL.SetCount; i > TmpIndex; i--)
                        {
                            (Data + i)->BayID = (Data + i - 1)->BayID;
                            (Data + i)->Level_ID = (Data + i - 1)->Level_ID;
                            (Data + i)->Row = (Data + i - 1)->Row;
                            (Data + i)->ItemType = (Data + i - 1)->ItemType;
                        }
                    }
                }
            }
            
            fixed (VEXI_DEFS.TSRM_SpecialRackItem* Data = &srm_SpecialRackCTRL.SpecialRack1)
            {
                if (cbBay.SelectedIndex <= 0)
                {
                    (Data + TmpIndex)->BayID = 0;
                }
                else
                {
                    (Data + TmpIndex)->BayID = (byte)(cbBay.SelectedIndex);
                };

                if (cbLevel.SelectedIndex <= 0)
                {
                    (Data + TmpIndex)->Level_ID = 0;
                }
                else
                {
                    (Data + TmpIndex)->Level_ID = (byte)(cbLevel.SelectedIndex);
                };

                (Data + TmpIndex)->Row = 0x00;
                if ((ckRow_1.Checked) || (ckRow_2.Checked) || (ckRow_3.Checked) || (ckRow_4.Checked))
                {
                    if (ckRow_1.Checked) (Data + TmpIndex)->Row = (byte)((Data + TmpIndex)->Row | 0x01);
                    if (ckRow_2.Checked) (Data + TmpIndex)->Row = (byte)((Data + TmpIndex)->Row | 0x02);
                    if (ckRow_3.Checked) (Data + TmpIndex)->Row = (byte)((Data + TmpIndex)->Row | 0x04);
                    if (ckRow_4.Checked) (Data + TmpIndex)->Row = (byte)((Data + TmpIndex)->Row | 0x08);
                }
                else
                {
                    (Data + TmpIndex)->Row = 0x0F;
                }
                (Data + TmpIndex)->ItemType = 0;
                if (ckItemNone.Checked)
                {
                } else
                {
                    if (ckItem_0.Checked) (Data + TmpIndex)->ItemType = (byte)((Data + TmpIndex)->ItemType | 0x01);
                    if (ckItem_1.Checked) (Data + TmpIndex)->ItemType = (byte)((Data + TmpIndex)->ItemType | 0x02);
                    if (ckItem_2.Checked) (Data + TmpIndex)->ItemType = (byte)((Data + TmpIndex)->ItemType | 0x04);
                    if (ckItem_3.Checked) (Data + TmpIndex)->ItemType = (byte)((Data + TmpIndex)->ItemType | 0x08);
                    if (ckItem_4.Checked) (Data + TmpIndex)->ItemType = (byte)((Data + TmpIndex)->ItemType | 0x10);
                    if (ckItem_5.Checked) (Data + TmpIndex)->ItemType = (byte)((Data + TmpIndex)->ItemType | 0x20);
                    if (ckItem_6.Checked) (Data + TmpIndex)->ItemType = (byte)((Data + TmpIndex)->ItemType | 0x40);
                    if (ckItem_7.Checked) (Data + TmpIndex)->ItemType = (byte)((Data + TmpIndex)->ItemType | 0x80);
                }
            }
            srm_SpecialRackCTRL.SetCount++;
            Validate_srm_SpecialRack_CTRL();
            Display_SpecialRack();

            lv_SpecialRack.Items[TmpIndex].Selected = false;
            lv_SpecialRack.Items[TmpIndex].Selected = true;
        }

        private unsafe void Update_SpecialRack()
        {
            byte TmpIndex = (byte)lv_SpecialRack.SelectedItems[0].Index;
            fixed (VEXI_DEFS.TSRM_SpecialRackItem* Data = &srm_SpecialRackCTRL.SpecialRack1)
            {
                if (cbBay.SelectedIndex <= 0)
                {
                    (Data + TmpIndex)->BayID = 0;
                }
                else
                {
                    (Data + TmpIndex)->BayID = (byte)(cbBay.SelectedIndex);
                };

                if (cbLevel.SelectedIndex <= 0)
                {
                    (Data + TmpIndex)->Level_ID = 0;
                }
                else
                {
                    (Data + TmpIndex)->Level_ID = (byte)(cbLevel.SelectedIndex);
                };

                (Data + TmpIndex)->Row = 0x00;
                if ((ckRow_1.Checked) || (ckRow_2.Checked) || (ckRow_3.Checked) || (ckRow_4.Checked))
                {
                    if (ckRow_1.Checked) (Data + TmpIndex)->Row = (byte)((Data + TmpIndex)->Row | 0x01);
                    if (ckRow_2.Checked) (Data + TmpIndex)->Row = (byte)((Data + TmpIndex)->Row | 0x02);
                    if (ckRow_3.Checked) (Data + TmpIndex)->Row = (byte)((Data + TmpIndex)->Row | 0x04);
                    if (ckRow_4.Checked) (Data + TmpIndex)->Row = (byte)((Data + TmpIndex)->Row | 0x08);
                }
                else
                {
                    (Data + TmpIndex)->Row = 0x0F;
                }

                (Data + TmpIndex)->ItemType = 0;
                if (ckItemNone.Checked)
                {
                }
                else
                {
                    if (ckItem_0.Checked) (Data + TmpIndex)->ItemType = (byte)((Data + TmpIndex)->ItemType | 0x01);
                    if (ckItem_1.Checked) (Data + TmpIndex)->ItemType = (byte)((Data + TmpIndex)->ItemType | 0x02);
                    if (ckItem_2.Checked) (Data + TmpIndex)->ItemType = (byte)((Data + TmpIndex)->ItemType | 0x04);
                    if (ckItem_3.Checked) (Data + TmpIndex)->ItemType = (byte)((Data + TmpIndex)->ItemType | 0x08);
                    if (ckItem_4.Checked) (Data + TmpIndex)->ItemType = (byte)((Data + TmpIndex)->ItemType | 0x10);
                    if (ckItem_5.Checked) (Data + TmpIndex)->ItemType = (byte)((Data + TmpIndex)->ItemType | 0x20);
                    if (ckItem_6.Checked) (Data + TmpIndex)->ItemType = (byte)((Data + TmpIndex)->ItemType | 0x40);
                    if (ckItem_7.Checked) (Data + TmpIndex)->ItemType = (byte)((Data + TmpIndex)->ItemType | 0x80);
                }
            }
            Display_SpecialRack();
            lv_SpecialRack.Items[TmpIndex].Selected = false;
            lv_SpecialRack.Items[TmpIndex].Selected = true;
        }

        private unsafe void Delete_SpecialRack()
        {
            byte TmpIndex = (byte)lv_SpecialRack.SelectedItems[0].Index;

            if ((TmpIndex + 1) == srm_SpecialRackCTRL.SetCount)
            {

            } else
            {
                fixed (VEXI_DEFS.TSRM_SpecialRackItem* Data = &srm_SpecialRackCTRL.SpecialRack1)
                {
                    for (byte i = TmpIndex; i < (srm_SpecialRackCTRL.SetCount - 1); i++)
                    {
                        (Data + i)->BayID = (Data + i + 1)->BayID;
                        (Data + i)->Level_ID = (Data + i + 1)->Level_ID;
                        (Data + i)->Row = (Data + i + 1)->Row;
                        (Data + i)->ItemType = (Data + i + 1)->ItemType;
                    }
                }
            }

            srm_SpecialRackCTRL.SetCount--;
            Validate_srm_SpecialRack_CTRL();
            Display_SpecialRack();
            if (TmpIndex < srm_SpecialRackCTRL.SetCount)
            {
                lv_SpecialRack.Items[TmpIndex].Selected = false;
                lv_SpecialRack.Items[TmpIndex].Selected = true;
                
            } else
            {
                if (srm_SpecialRackCTRL.SetCount > 0)
                {
                    lv_SpecialRack.Items[srm_SpecialRackCTRL.SetCount - 1].Selected = false;
                    lv_SpecialRack.Items[srm_SpecialRackCTRL.SetCount - 1].Selected = true;
                }
            }
        }

        private unsafe void Validate_srm_SpecialRack_CTRL()
        {
            if (srm_SpecialRackCTRL.SetCount == 0)
            {
                fixed (VEXI_DEFS.TSRM_SpecialRack* Data = &srm_SpecialRackCTRL)
                {
                    Global_Class.UTIL_Byteptr_clear((byte*)Data, Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_SpecialRack)));
                }
            } else if (srm_SpecialRackCTRL.SetCount < 100)
            {
                fixed (VEXI_DEFS.TSRM_SpecialRackItem* Data = &srm_SpecialRackCTRL.SpecialRack1)
                {
                    for (byte i = srm_SpecialRackCTRL.SetCount; i < 100; i++)
                    {
                        (Data + i)->BayID = 0;
                        (Data + i)->Level_ID = 0;
                        (Data + i)->Row = 0;
                        (Data + i)->ItemType = 0;
                    }
                }

            }
        }

        private unsafe void Do_Ctrl(bool isFileSave)
        {


            if (!isFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_9F, srm_SpecialRackCTRL);
            }
        }
        private unsafe void ChangeIndex_UP(byte TmpTagetIndex)
        {
            VEXI_DEFS.TSRM_SpecialRackItem TmpSRM_SpecialRack;

            if (TmpTagetIndex > 0)
            {
                fixed (VEXI_DEFS.TSRM_SpecialRackItem* Data = &srm_SpecialRackCTRL.SpecialRack1)
                {

                    TmpSRM_SpecialRack.BayID = (Data + TmpTagetIndex)->BayID;
                    TmpSRM_SpecialRack.Level_ID = (Data + TmpTagetIndex)->Level_ID;
                    TmpSRM_SpecialRack.Row = (Data + TmpTagetIndex)->Row;
                    TmpSRM_SpecialRack.ItemType = (Data + TmpTagetIndex)->ItemType;


                    (Data + TmpTagetIndex)->BayID = (Data + TmpTagetIndex - 1)->BayID;
                    (Data + TmpTagetIndex)->Level_ID= (Data + TmpTagetIndex - 1)->Level_ID;
                    (Data + TmpTagetIndex)->Row = (Data + TmpTagetIndex - 1)->Row;
                    (Data + TmpTagetIndex)->ItemType= (Data + TmpTagetIndex - 1)->ItemType;

                    (Data + TmpTagetIndex - 1)->BayID = TmpSRM_SpecialRack.BayID;
                    (Data + TmpTagetIndex - 1)->Level_ID = TmpSRM_SpecialRack.Level_ID;
                    (Data + TmpTagetIndex - 1)->Row = TmpSRM_SpecialRack.Row;
                    (Data + TmpTagetIndex - 1)->ItemType = TmpSRM_SpecialRack.ItemType;
                }

                
                Display_SpecialRack();
                lv_SpecialRack.Items[TmpTagetIndex - 1].Selected = true;
                
            }
        }
        private unsafe void ChangeIndex_DOWN(byte TmpTagetIndex)
        {
            VEXI_DEFS.TSRM_SpecialRackItem TmpSRM_SpecialRack;

            if (TmpTagetIndex < (srm_SpecialRackCTRL.SetCount - 1))
            {
                fixed (VEXI_DEFS.TSRM_SpecialRackItem* Data = &srm_SpecialRackCTRL.SpecialRack1)
                {

                    TmpSRM_SpecialRack.BayID = (Data + TmpTagetIndex)->BayID;
                    TmpSRM_SpecialRack.Level_ID = (Data + TmpTagetIndex)->Level_ID;
                    TmpSRM_SpecialRack.Row = (Data + TmpTagetIndex)->Row;
                    TmpSRM_SpecialRack.ItemType = (Data + TmpTagetIndex)->ItemType;


                    (Data + TmpTagetIndex)->BayID = (Data + TmpTagetIndex + 1)->BayID;
                    (Data + TmpTagetIndex)->Level_ID = (Data + TmpTagetIndex + 1)->Level_ID;
                    (Data + TmpTagetIndex)->Row = (Data + TmpTagetIndex + 1)->Row;
                    (Data + TmpTagetIndex)->ItemType = (Data + TmpTagetIndex + 1)->ItemType;

                    (Data + TmpTagetIndex + 1)->BayID = TmpSRM_SpecialRack.BayID;
                    (Data + TmpTagetIndex + 1)->Level_ID = TmpSRM_SpecialRack.Level_ID;
                    (Data + TmpTagetIndex + 1)->Row = TmpSRM_SpecialRack.Row;
                    (Data + TmpTagetIndex + 1)->ItemType = TmpSRM_SpecialRack.ItemType;
                }

                Display_SpecialRack();
                lv_SpecialRack.Items[TmpTagetIndex + 1].Selected = true;
                
            }
        }

        #endregion

        private void ckRowAll_Click(object sender, EventArgs e)
        {
            if (ckRowAll.Checked)
            {
                ckRow_1.Checked = true;
                ckRow_2.Checked = true;
                ckRow_3.Checked = true;
                ckRow_4.Checked = true;
            }
            else
            {
                ckRow_1.Checked = false;
                ckRow_2.Checked = false;
                ckRow_3.Checked = false;
                ckRow_4.Checked = false;
            }
        }

        private void ckRow_1_Click(object sender, EventArgs e)
        {
            if ((ckRow_1.Checked) &&
                (ckRow_2.Checked) &&
                (ckRow_3.Checked) &&
                (ckRow_4.Checked))
            {
                ckRowAll.Checked = true;
            }
            else
            {
                ckRowAll.Checked = false;
            }
        }

        
    }
}

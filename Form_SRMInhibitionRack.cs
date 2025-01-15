using System;
using System.IO;
using System.Runtime.InteropServices;   //DllImport
using System.Windows.Forms;

namespace VEXI
{
    public partial class Form_SRMInhibitionRack : Form
    {

        public Form_Main form_Main;
        public static VEXI_DEFS.TSRM_NoUseRack srm_NoUseRack_Res;
        public static VEXI_DEFS.TSRM_NoUseRack srm_NoUseRack_CTRL;

        public Form_SRMInhibitionRack()
        {
            InitializeComponent();
        }

        #region 컴포넌트 이벤트
        private void Form_NoUseRack_Load(object sender, EventArgs e)
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

            btn_NoUseRack_Set.Enabled = false;
        }

        private void lv_NoUseRack_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i;
            if (lv_NoUseRack.SelectedItems.Count > 0)
            {
                i = lv_NoUseRack.SelectedItems[0].Index;
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
        private void btn_NoUseRack_FileRead_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.NRack|*.NRACK";

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
                        ushort Len = (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_NoUseRack));
                        if (br.BaseStream.Length == Len)
                        {
                            Savebytes = br.ReadBytes(Len);
                            srm_NoUseRack_CTRL = (VEXI_DEFS.TSRM_NoUseRack)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_NoUseRack));
                            srm_NoUseRack_Res = (VEXI_DEFS.TSRM_NoUseRack)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.TSRM_NoUseRack));

                            Validate_srm_NoUseRack_CTRL();
                            Display_NoUseRack();
                            if ((form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode & 0x08) == 0)
                            {
                                btn_NoUseRack_Set.Enabled = false;
                            }
                            else
                            {
                                btn_NoUseRack_Set.Enabled = true;
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
                if (form_Main.SRM_ToTalFile.Read_SRM_NoUseRack(ref srm_NoUseRack_CTRL))
                {
                    srm_NoUseRack_Res = srm_NoUseRack_CTRL;
                    Validate_srm_NoUseRack_CTRL();
                    Display_NoUseRack();
                    if ((form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode & 0x08) == 0)
                    {
                        btn_NoUseRack_Set.Enabled = false;
                    }
                    else
                    {
                        btn_NoUseRack_Set.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("해당 파일안에 [" + this.Text + "] 데이터가 없습니다");
                }
            }
        }

        //통합파일로 저장하는 것으로 변경
        //개별파일로 저장하는 소스는 남겨놓음
        private void btn_NoUse_FileWrite_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "*.NRack|*.NRACK";

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
                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_NoUseRack))];
                        Global_Class.UTIL_StructObjectToByteArray(srm_NoUseRack_CTRL, Savebytes);
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
                form_Main.SRM_ToTalFile.Write_SRM_NoUseRack(srm_NoUseRack_CTRL);
            }
        }


        private void btnAllClear_Click(object sender, EventArgs e)
        {
            srm_NoUseRack_CTRL.SetCount = 0;
            Validate_srm_NoUseRack_CTRL();
            Display_NoUseRack();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            byte TmpIndex = srm_NoUseRack_CTRL.SetCount;

            if (srm_NoUseRack_CTRL.SetCount < 100)
            {
                ADD_NoUseRack();
            }
        }

        private void btnUIpdate_Click(object sender, EventArgs e)
        {
            if (lv_NoUseRack.SelectedItems.Count == 0)
            {

            }
            else
            {
                Update_NoUseRack();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lv_NoUseRack.SelectedItems.Count == 0)
            {

            }
            else
            {
                Delete_NoUseRack();
            }
        }

        private void btn_NoUseRack_Set_Click(object sender, EventArgs e)
        {
           
            if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "금지랙 설정을 장치에 다운로드 하시겠습니까 ? "))
            {
                Do_Ctrl(false);
            }
        }

        private void btn_NoUseRack_Load_Click(object sender, EventArgs e)
        {
            srm_NoUseRack_CTRL.SetCount = 0;
            Validate_srm_NoUseRack_CTRL();
            Display_NoUseRack();

            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_9C, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_REC_NoUseRackReq)));
        }

        #endregion

        #region 기능함수
        public void Display_NoUseRack(byte[] data)
        {
            
            srm_NoUseRack_Res = (VEXI_DEFS.TSRM_NoUseRack)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TSRM_NoUseRack));
            srm_NoUseRack_CTRL = (VEXI_DEFS.TSRM_NoUseRack)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.TSRM_NoUseRack));
            Validate_srm_NoUseRack_CTRL();
            Display_NoUseRack();
            if ((form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode & 0x08) == 0)
            {
                btn_NoUseRack_Set.Enabled = false;
            }
            else
            {
                btn_NoUseRack_Set.Enabled = true;
            }
        }

        private unsafe void Display_Init()
        {
            fixed (VEXI_DEFS.TSRM_NoUseRack* Data = &srm_NoUseRack_Res)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)Data, Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_NoUseRack)));
            }
            fixed (VEXI_DEFS.TSRM_NoUseRack* Data = &srm_NoUseRack_CTRL)
            {
                Global_Class.UTIL_Byteptr_clear((byte*)Data, Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_NoUseRack)));
            }

            lv_NoUseRack.Items.Clear();
        }
        private unsafe void Display_NoUseRack()
        {
            {

                //제어구조체로 Display 하기
                fixed (VEXI_DEFS.TSRM_NoUseRackItem* Data = &srm_NoUseRack_CTRL.NoUseRack1)
                {
                    ListViewItem listviewitem;

                    if (srm_NoUseRack_CTRL.SetCount < lv_NoUseRack.Items.Count)
                    {
                        byte Gap = (byte)(lv_NoUseRack.Items.Count - srm_NoUseRack_CTRL.SetCount);
                        for (byte i = 0; i < Gap; i++)
                        {
                            lv_NoUseRack.Items.RemoveAt(0);
                        }
                    } else if (srm_NoUseRack_CTRL.SetCount > lv_NoUseRack.Items.Count)
                    {
                        byte Gap = (byte)(srm_NoUseRack_CTRL.SetCount - lv_NoUseRack.Items.Count);
                        for (byte i = 0; i < Gap; i++)
                        {
                            listviewitem = lv_NoUseRack.Items.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                        }
                    }

                    if (srm_NoUseRack_CTRL.SetCount > 0)
                    {
                        for (byte i = 0; i < srm_NoUseRack_CTRL.SetCount; i++)
                        {
                            listviewitem = lv_NoUseRack.Items[i];
                            listviewitem.SubItems[0].Text = string.Format("{0}", i + 1);

                            if (((Data + i)->BayID > 0) && ((Data + i)->BayID <= 256))
                            {
                                listviewitem.SubItems[1].Text = cbBay.Items[(Data + i)->BayID].ToString();
                            } else
                            {
                                listviewitem.SubItems[1].Text = cbBay.Items[0].ToString();
                            }
                            

                            if (((Data + i)->Level_ID > 0) && ((Data + i)->Level_ID <= 128))
                            {
                                listviewitem.SubItems[2].Text = cbLevel.Items[(Data + i)->Level_ID].ToString();
                            }
                            else
                            {
                                listviewitem.SubItems[2].Text = cbLevel.Items[0].ToString(); ;
                            }

                            switch ((Data + i)->RowID)
                            {
                                case 0:
                                    listviewitem.SubItems[3].Text = "적용";
                                    listviewitem.SubItems[4].Text = "적용";
                                    listviewitem.SubItems[5].Text = "적용";
                                    listviewitem.SubItems[6].Text = "적용";
                                    break;
                                case 1:
                                    listviewitem.SubItems[3].Text = "적용";
                                    listviewitem.SubItems[4].Text = "";
                                    listviewitem.SubItems[5].Text = "";
                                    listviewitem.SubItems[6].Text = "";
                                    break;
                                case 2:
                                    listviewitem.SubItems[3].Text = "";
                                    listviewitem.SubItems[4].Text = "적용";
                                    listviewitem.SubItems[5].Text = "";
                                    listviewitem.SubItems[6].Text = "";
                                    break;
                                case 3:
                                    listviewitem.SubItems[3].Text = "";
                                    listviewitem.SubItems[4].Text = "";
                                    listviewitem.SubItems[5].Text = "적용";
                                    listviewitem.SubItems[6].Text = "";
                                    break;
                                case 4:
                                    listviewitem.SubItems[3].Text = "";
                                    listviewitem.SubItems[4].Text = "";
                                    listviewitem.SubItems[5].Text = "";
                                    listviewitem.SubItems[6].Text = "적용";
                                    break;
                                default:
                                    listviewitem.SubItems[3].Text = "적용";
                                    listviewitem.SubItems[4].Text = "적용";
                                    listviewitem.SubItems[5].Text = "적용";
                                    listviewitem.SubItems[6].Text = "적용";
                                    break;
                            }
                        }
                    }
                }
            }

        }

        private unsafe void Display_SelectIndex(byte TmpIndex)
        {
            fixed (VEXI_DEFS.TSRM_NoUseRackItem* Data = &srm_NoUseRack_CTRL.NoUseRack1)
            {
                if (srm_NoUseRack_CTRL.SetCount > TmpIndex)
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

                    switch ((Data + TmpIndex)->RowID)
                    {
                        case 0: rbRowAll.Checked = true; break;
                        case 1: rbRow_1.Checked = true; break;
                        case 2: rbRow_2.Checked = true; break;
                        case 3: rbRow_3.Checked = true; break;
                        case 4: rbRow_4.Checked = true; break;
                        default: rbRowAll.Checked = true; break;
                    }
                    
                }
            }
        }
        private unsafe void ADD_NoUseRack()
        {
            byte TmpIndex = srm_NoUseRack_CTRL.SetCount;
            
            fixed (VEXI_DEFS.TSRM_NoUseRackItem* Data = &srm_NoUseRack_CTRL.NoUseRack1)
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


                (Data + TmpIndex)->RowID = 0;
                if (rbRow_1.Checked) (Data + TmpIndex)->RowID = 1;
                else if (rbRow_2.Checked) (Data + TmpIndex)->RowID = 2;
                else if (rbRow_3.Checked) (Data + TmpIndex)->RowID = 3;
                else if (rbRow_4.Checked) (Data + TmpIndex)->RowID = 4;
                    
            }
            srm_NoUseRack_CTRL.SetCount++;
            Validate_srm_NoUseRack_CTRL();
            Display_NoUseRack();

            lv_NoUseRack.Items[TmpIndex].Selected = false;
            lv_NoUseRack.Items[TmpIndex].Selected = true;
        }

        private unsafe void Update_NoUseRack()
        {
            byte TmpIndex = (byte)lv_NoUseRack.SelectedItems[0].Index;
            fixed (VEXI_DEFS.TSRM_NoUseRackItem* Data = &srm_NoUseRack_CTRL.NoUseRack1)
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


                (Data + TmpIndex)->RowID = 0;
                if (rbRow_1.Checked) (Data + TmpIndex)->RowID = 1;
                else if (rbRow_2.Checked) (Data + TmpIndex)->RowID = 2;
                else if (rbRow_3.Checked) (Data + TmpIndex)->RowID = 3;
                else if (rbRow_4.Checked) (Data + TmpIndex)->RowID = 4;
            }
            Display_NoUseRack();
            lv_NoUseRack.Items[TmpIndex].Selected = false;
            lv_NoUseRack.Items[TmpIndex].Selected = true;
        }

        private unsafe void Delete_NoUseRack()
        {
            byte TmpIndex = (byte)lv_NoUseRack.SelectedItems[0].Index;

            if ((TmpIndex + 1) == srm_NoUseRack_CTRL.SetCount)
            {

            } else
            {
                fixed (VEXI_DEFS.TSRM_NoUseRackItem* Data = &srm_NoUseRack_CTRL.NoUseRack1)
                {
                    for (byte i = TmpIndex; i < (srm_NoUseRack_CTRL.SetCount - 1); i++)
                    {
                        (Data + i)->RowID = (Data + i + 1)->RowID;
                        (Data + i)->BayID = (Data + i + 1)->BayID;
                        (Data + i)->Level_ID = (Data + i + 1)->Level_ID;
                    }
                }
            }

            srm_NoUseRack_CTRL.SetCount--;
            Validate_srm_NoUseRack_CTRL();
            Display_NoUseRack();
            if (TmpIndex < srm_NoUseRack_CTRL.SetCount)
            {
                lv_NoUseRack.Items[TmpIndex].Selected = false;
                lv_NoUseRack.Items[TmpIndex].Selected = true;
                
            } else
            {
                if (srm_NoUseRack_CTRL.SetCount > 0)
                {
                    lv_NoUseRack.Items[srm_NoUseRack_CTRL.SetCount - 1].Selected = false;
                    lv_NoUseRack.Items[srm_NoUseRack_CTRL.SetCount - 1].Selected = true;
                }
            }
        }

        private unsafe void Validate_srm_NoUseRack_CTRL()
        {
            if (srm_NoUseRack_CTRL.SetCount == 0)
            {
                fixed (VEXI_DEFS.TSRM_NoUseRack* Data = &srm_NoUseRack_CTRL)
                {
                    Global_Class.UTIL_Byteptr_clear((byte*)Data, Marshal.SizeOf(typeof(VEXI_DEFS.TSRM_NoUseRack)));
                }
            } else if (srm_NoUseRack_CTRL.SetCount < 100)
            {
                fixed (VEXI_DEFS.TSRM_NoUseRackItem* Data = &srm_NoUseRack_CTRL.NoUseRack1)
                {
                    for (byte i = srm_NoUseRack_CTRL.SetCount; i < 100; i++)
                    {
                        (Data + i)->RowID = 0;
                        (Data + i)->BayID = 0;
                        (Data + i)->Level_ID = 0;
                    }
                }

            }
        }

        private unsafe void Do_Ctrl(bool isFileSave)
        {
            if (!isFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_00, ConstClass.CMD2_9D, srm_NoUseRack_CTRL);
            }
        }


        #endregion

        private void ckRowAll_Click(object sender, EventArgs e)
        {
           
        }

        private void ckRow_1_Click(object sender, EventArgs e)
        {
           
        }

    }
}

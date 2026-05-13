using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;   //DllImport
namespace VEXI
{
    public partial class Form_IOStructureSet : Form
    {
        public Form_Main form_Main;
        private ListViewItem lvDI_Item;
        private ListViewItem lvDO_Item;
        private static Label[] lbl_Scan;
        private static Label[] lbl_Config;
        private static ComboBox[] cb_Config;
        private static ComboBox[] DI_EditComboxBox;
        private static ComboBox[] DO_EditComboxBox;
        private static VEXI_DEFS.DevUnion_IOConfig CtrlREC;
        private static VEXI_DEFS.DevUnion_IOConfig dev_REC_IOConfig;
        private bool IsControl = false;
        private int MAX_DI_COUNT_1 = 0;
        private int MAX_DI_COUNT_2 = 0;
        private int MAX_DO_COUNT_1 = 0;
        private int MAX_DO_COUNT_2 = 0;

        private bool IsSRM_New = true;
        private bool IsRXIO = false;

        public Form_IOStructureSet()
        {
            InitializeComponent();

            lbl_Scan = new Label[] { lbl_Scanethercat_1, lbl_Scanethercat_2, lbl_Scanethercat_3, 
                                     lbl_Scanethercat_4, lbl_Scanethercat_5, lbl_Scanethercat_6, lbl_Scanethercat_7,
                                     lbl_Scanethercat_8, lbl_Scanethercat_9, lbl_Scanethercat_10};
            lbl_Config = new Label[] { lbl_ethercat_1, lbl_ethercat_2, lbl_ethercat_3,
                                       lbl_ethercat_4, lbl_ethercat_5, lbl_ethercat_6, lbl_ethercat_7,
                                       lbl_ethercat_8, lbl_ethercat_9, lbl_ethercat_10};
            cb_Config = new ComboBox[] {cb_Ehtercat_1, cb_Ehtercat_2, cb_Ehtercat_3, cb_Ehtercat_4, cb_Ehtercat_5,
                                        cb_Ehtercat_6, cb_Ehtercat_7, cb_Ehtercat_8, cb_Ehtercat_9, cb_Ehtercat_10};

            DI_EditComboxBox = new ComboBox[] { null, LvDI_cb_Item1, LvDI_cb_Item2, LvDI_cb_Item3, LvDI_cb_Item4, LvDI_cb_Item5 };
            DO_EditComboxBox = new ComboBox[] { null, LvDO_cb_Item1, LvDO_cb_Item2, LvDO_cb_Item3 };
        }


        private void Form_IOStructureSet_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild)
            {
                form_Main = (Form_Main)this.MdiParent;
            }
            else
            {
                form_Main = (Form_Main)this.Owner;
            }
            switch (form_Main.COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    MAX_DI_COUNT_1 = ConstClass.SRM_DI_Names_1.GetLength(0);
                    MAX_DI_COUNT_2 = ConstClass.SRM_DI_Names_2.GetLength(0);
                    MAX_DO_COUNT_1 = ConstClass.SRM_DO_Names_1.GetLength(0);
                    MAX_DO_COUNT_2 = ConstClass.SRM_DO_Names_2.GetLength(0);
                    break;
                case ConstClass.TYPE_RTV:
                    MAX_DI_COUNT_1 = ConstClass.RTV_DI_Names.GetLength(0);
                    MAX_DI_COUNT_2 = 0;
                    MAX_DO_COUNT_1 = ConstClass.RTV_DO_Names_1.GetLength(0);
                    MAX_DO_COUNT_2 = ConstClass.RTV_DO_Names_2.GetLength(0);
                    break;
                case ConstClass.TYPE_EMS:
                    MAX_DI_COUNT_1 = ConstClass.EMS_DI_Names.GetLength(0);
                    MAX_DI_COUNT_2 = 0;
                    MAX_DO_COUNT_1 = ConstClass.EMS_DO_Names.GetLength(0);
                    MAX_DO_COUNT_2 = 0;
                    break;
                default:
                    MAX_DI_COUNT_1 = ConstClass.RTV_DI_Names.GetLength(0);
                    MAX_DI_COUNT_2 = 0;
                    MAX_DO_COUNT_1 = ConstClass.RTV_DO_Names_1.GetLength(0);
                    MAX_DO_COUNT_2 = ConstClass.RTV_DO_Names_2.GetLength(0);
                    break;
            }

            Init_DI();
            Init_DO();
            IsControl = false;
        }

        #region DI 컴포넌트이벤트
        private void lv_DI_Enter(object sender, EventArgs e)
        {
            Hide_AllEdit();
            //for (byte i = 0; i <= (lv_DI.Columns.Count - 1); i++)
            //{
            //    if (DI_EditComboxBox[i] != null)
            //    {
            //        DI_EditComboxBox[i].Visible = false;
            //    }
            //}
        }
        private void LvDI_cb_Item1_VisibleChanged(object sender, EventArgs e)
        {
            if (lvDI_Item != null)
            {
                ComboBox TmpComboBox = (ComboBox)sender;

                for (byte i = 0; i <= (lv_DI.Columns.Count - 1); i++)
                {
                    if (DI_EditComboxBox[i] != null)
                    {
                        if (TmpComboBox == DI_EditComboxBox[i])
                        {
                            if (!TmpComboBox.Visible)
                            {
                                lvDI_Item.SubItems[i].Text = DI_EditComboxBox[i].Text;
                            }
                        }
                    }
                }
            }

        }
        private void LvDI_cb_Item1_SelectedValueChanged(object sender, EventArgs e)
        {
            

            int Loop;
            int OLDindex;
            //(DI_EditComboxBox[1] 의 값이 MCU가 되면 DI_EditComboxBox[2] 0~7Pin, 그 외에는 0~31pin
            OLDindex = DI_EditComboxBox[2].SelectedIndex;
            if (DI_EditComboxBox[1].SelectedIndex == 1)
            {
                if (DI_EditComboxBox[2].Items.Count > 9)
                {
                    for (Loop = DI_EditComboxBox[2].Items.Count; Loop > 9; Loop--)
                    {
                        DI_EditComboxBox[2].Items.RemoveAt(Loop - 1);
                    }

                    if (DI_EditComboxBox[2].Items.Count > OLDindex)
                    {
                        DI_EditComboxBox[2].SelectedIndex = OLDindex;
                    }
                    else
                    {
                        DI_EditComboxBox[2].SelectedIndex = 0;
                    }
                }
            }
            else
            {
                if (DI_EditComboxBox[2].Items.Count <= 9)
                {
                    for (Loop = 8; Loop <= 31; Loop++)
                    {
                        DI_EditComboxBox[2].Items.Add(Loop.ToString());
                    }
                    if (DI_EditComboxBox[2].Items.Count > OLDindex)
                    {
                        DI_EditComboxBox[2].SelectedIndex = OLDindex;
                    }
                    else
                    {
                        DI_EditComboxBox[2].SelectedIndex = 0;
                    }
                }
            }

        }

        private void lvDI_IO_MouseUp(object sender, MouseEventArgs e)
        {
            // Get the item on the row that is clicked.
            lvDI_Item = this.lv_DI.GetItemAt(e.X, e.Y);

            // Make sure that an item is clicked.
            if (lvDI_Item != null)
            {
                //Column 2
                Rectangle ClickedItem;


                for (byte i = 0; i <= (lv_DI.Columns.Count - 1); i++)
                {
                    if (DI_EditComboxBox[i] != null)
                    {
                        ClickedItem = lvDI_Item.SubItems[i].Bounds;

                        //화면에 안보이는 경우
                        if ((ClickedItem.Left + this.lv_DI.Columns[i].Width) < 0)
                        {
                            DI_EditComboxBox[i].Tag = 1;
                            return;
                        }
                        else if (ClickedItem.Left < 0) // 해당 컬럼의 Left가 화면을 벗어난 경우
                        {
                            DI_EditComboxBox[i].Tag = 0;
                            if ((ClickedItem.Left + this.lv_DI.Columns[i].Width) > this.lv_DI.Width) // 해당 컬럼의 Right가 화면을 벗어난 경우
                            {
                                ClickedItem.Width = this.lv_DI.Width;
                                ClickedItem.X = 0;
                            }
                            else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                            {
                                ClickedItem.Width = this.lv_DI.Columns[i].Width + ClickedItem.Left;
                                ClickedItem.X = 2;
                            }
                        }
                        else // 해당 컬럼의 Left가 화면을 벗어나지 않은 경우
                        {
                            DI_EditComboxBox[i].Tag = 0;
                            if ((ClickedItem.Left + this.lv_DI.Columns[i].Width) > this.lv_DI.Width)  // 해당 컬럼의 Left가 화면을 벗어난 경우
                            {
                                ClickedItem.Width = this.lv_DI.Width - ClickedItem.Left;
                            }
                            else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                            {
                                ClickedItem.Width = this.lv_DI.Columns[i].Width;
                            }
                        }

                        // Adjust the top to account for the location of the ListView.
                        ClickedItem.Y += this.lv_DI.Top;
                        ClickedItem.X += this.lv_DI.Left;

                        // Assign calculated bounds to the ComboBox.
                        DI_EditComboxBox[i].Bounds = ClickedItem;

                        // Set default text for ComboBox to match the item that is clicked.
                        if (i != 4)
                        {
                            if ((DI_EditComboxBox[i].Items.IndexOf(lvDI_Item.SubItems[i].Text) != -1))
                            {
                                DI_EditComboxBox[i].Text = lvDI_Item.SubItems[i].Text;
                            }
                            else
                            {
                                DI_EditComboxBox[i].SelectedIndex = 0;
                            }
                        } else
                        {
                            DI_EditComboxBox[i].Text = lvDI_Item.SubItems[i].Text;
                        }


                        if (i == 5)
                        {
                            switch (form_Main.COMMDataManager.RX_DestDevType)
                            {
                                case ConstClass.TYPE_SRM:
                                    if (lvDI_Item.Index < MAX_DI_COUNT_1)
                                    {
                                        if (ConstClass.SRM_DI_Names_1[lvDI_Item.Index, 3] == "0") DI_EditComboxBox[i].Tag = 1;
                                    } else
                                    {
                                        if (ConstClass.SRM_DI_Names_2[lvDI_Item.Index- MAX_DI_COUNT_1, 3] == "0") DI_EditComboxBox[i].Tag = 1;
                                    }
                                    break;
                                case ConstClass.TYPE_RTV:
                                    if (ConstClass.RTV_DI_Names[lvDI_Item.Index, 3] == "0") DI_EditComboxBox[i].Tag = 1;
                                    break;
                                case ConstClass.TYPE_EMS:
                                    if (ConstClass.EMS_DI_Names[lvDI_Item.Index, 3] == "0") DI_EditComboxBox[i].Tag = 1;
                                    break;
                                default:
                                    if (ConstClass.RTV_DI_Names[lvDI_Item.Index, 3] == "0") DI_EditComboxBox[i].Tag = 1;
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                for (byte i = 0; i <= (lv_DI.Columns.Count - 1); i++)
                {
                    if (DI_EditComboxBox[i] != null)
                    {
                        DI_EditComboxBox[i].Visible = false;
                    }

                }
            }
        }

        private void lvDI_IO_DoubleClick(object sender, EventArgs e)
        {
            // Display the ComboBox, and make sure that it is on top with focus.
            bool once = false;
            if (lv_DI.SelectedItems.Count == 1)
            {
                for (byte i = 0; i <= (lv_DI.Columns.Count - 1); i++)
                {
                    if (DI_EditComboxBox[i] != null)
                    {
                        if (Convert.ToByte(DI_EditComboxBox[i].Tag.ToString()) != 1)
                        {
                            DI_EditComboxBox[i].Visible = true;
                            DI_EditComboxBox[i].BringToFront();
                            if (!once)
                            {
                                DI_EditComboxBox[i].Focus();
                                once = true;
                            }
                        }
                    }
                }
            }

        }

        private void lvDI_IO_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (byte i = 0; i <= (lv_DI.Columns.Count - 1); i++)
            {
                if (DI_EditComboxBox[i] != null)
                {
                    DI_EditComboxBox[i].Visible = false;
                }
            }
        }


        #endregion
        #region DO 컴포넌트이벤트
        private void LvDO_cb_Item1_VisibleChanged(object sender, EventArgs e)
        {
            if (lvDO_Item != null)
            {
                ComboBox TmpComboBox = (ComboBox)sender;

                for (byte i = 0; i <= (lv_DO.Columns.Count - 1); i++)
                {
                    if (DO_EditComboxBox[i] != null)
                    {
                        if (TmpComboBox == DO_EditComboxBox[i])
                        {
                            if (!TmpComboBox.Visible)
                            {
                                lvDO_Item.SubItems[i].Text = DO_EditComboxBox[i].Text;

                            }
                        }
                    }
                }
            }

        }
        private void LvDO_cb_Item1_SelectedValueChanged(object sender, EventArgs e)
        {
            

            int Loop;
            int OLDindex;
            //(DO_EditComboxBox[1] 의 값이 MCU가 되면 DO_EditComboxBox[2] 0~7Pin, 그 외에는 0~31pin
            OLDindex = DO_EditComboxBox[2].SelectedIndex;
            if (DO_EditComboxBox[1].SelectedIndex == 1)
            {
                if (DO_EditComboxBox[2].Items.Count > 9)
                {
                    for (Loop = DO_EditComboxBox[2].Items.Count; Loop > 9; Loop--)
                    {
                        DO_EditComboxBox[2].Items.RemoveAt(Loop - 1);
                    }

                    if (DO_EditComboxBox[2].Items.Count > OLDindex)
                    {
                        DO_EditComboxBox[2].SelectedIndex = OLDindex;
                    }
                    else
                    {
                        DO_EditComboxBox[2].SelectedIndex = 0;
                    }
                }
            } else
            {
                if (DO_EditComboxBox[2].Items.Count <= 9)
                {
                    for (Loop = 8; Loop <= 31; Loop++)
                    {
                        DO_EditComboxBox[2].Items.Add(Loop.ToString());
                    }
                    if (DO_EditComboxBox[2].Items.Count > OLDindex)
                    {
                        DO_EditComboxBox[2].SelectedIndex = OLDindex;
                    }
                    else
                    {
                        DO_EditComboxBox[2].SelectedIndex = 0;
                    }
                }
            }
        }

        private void lvDO_IO_MouseUp(object sender, MouseEventArgs e)
        {
            // Get the item on the row that is clicked.
            lvDO_Item = this.lv_DO.GetItemAt(e.X, e.Y);

            // Make sure that an item is clicked.
            if (lvDO_Item != null)
            {
                //Column 2
                Rectangle ClickedItem;


                for (byte i = 0; i <= (lv_DO.Columns.Count - 1); i++)
                {
                    if (DO_EditComboxBox[i] != null)
                    {
                        ClickedItem = lvDO_Item.SubItems[i].Bounds;

                        //화면에 안보이는 경우
                        if ((ClickedItem.Left + this.lv_DO.Columns[i].Width) < 0)
                        {
                            DO_EditComboxBox[i].Tag = 1;
                            return;
                        }
                        else if (ClickedItem.Left < 0) // 해당 컬럼의 Left가 화면을 벗어난 경우
                        {
                            DO_EditComboxBox[i].Tag = 0;
                            if ((ClickedItem.Left + this.lv_DO.Columns[i].Width) > this.lv_DO.Width) // 해당 컬럼의 Right가 화면을 벗어난 경우
                            {
                                ClickedItem.Width = this.lv_DO.Width;
                                ClickedItem.X = 0;
                            }
                            else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                            {
                                ClickedItem.Width = this.lv_DO.Columns[i].Width + ClickedItem.Left;
                                ClickedItem.X = 2;
                            }
                        }
                        else // 해당 컬럼의 Left가 화면을 벗어나지 않은 경우
                        {
                            DO_EditComboxBox[i].Tag = 0;
                            if ((ClickedItem.Left + this.lv_DO.Columns[i].Width) > this.lv_DO.Width)  // 해당 컬럼의 Left가 화면을 벗어난 경우
                            {
                                ClickedItem.Width = this.lv_DO.Width - ClickedItem.Left;
                            }
                            else // 해당 컬럼의 Right가 화면을 벗어나지 않은 경우
                            {
                                ClickedItem.Width = this.lv_DO.Columns[i].Width;
                            }
                        }

                        // Adjust the top to account for the location of the ListView.
                        ClickedItem.Y += this.lv_DO.Top;
                        ClickedItem.X += this.lv_DO.Left;

                        // Assign calculated bounds to the ComboBox.
                        DO_EditComboxBox[i].Bounds = ClickedItem;

                        // Set default text for ComboBox to match the item that is clicked.
                        if ((DO_EditComboxBox[i].Items.IndexOf(lvDO_Item.SubItems[i].Text) != -1))
                        {
                            DO_EditComboxBox[i].Text = lvDO_Item.SubItems[i].Text;
                        }
                        else
                        {
                            DO_EditComboxBox[i].SelectedIndex = 0;
                        }

                    }
                }
            } else
            {
                for (byte i = 0; i <= (lv_DO.Columns.Count - 1); i++)
                {
                    if (DO_EditComboxBox[i] != null)
                    {
                        DO_EditComboxBox[i].Visible = false;
                    }

                }
            }
        }

        private void lvDO_IO_DoubleClick(object sender, EventArgs e)
        {
            // Display the ComboBox, and make sure that it is on top with focus.
            bool once = false;
            if (lv_DO.SelectedItems.Count == 1)
            {
                for (byte i = 0; i <= (lv_DO.Columns.Count - 1); i++)
                {
                    if (DO_EditComboxBox[i] != null)
                    {
                        if (Convert.ToByte(DO_EditComboxBox[i].Tag.ToString()) != 1)
                        {
                            DO_EditComboxBox[i].Visible = true;
                            DO_EditComboxBox[i].BringToFront();
                            if (!once)
                            {
                                DO_EditComboxBox[i].Focus();
                                once = true;
                            }
                        }
                    }
                }
            }

        }

        private void lvDO_IO_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (byte i = 0; i <= (lv_DO.Columns.Count - 1); i++)
            {
                if (DO_EditComboxBox[i] != null)
                {
                    DO_EditComboxBox[i].Visible = false;
                }
            }
        }

        private void lv_DO_Enter(object sender, EventArgs e)
        {
            Hide_AllEdit();
            //for (byte i = 0; i <= (lv_DO.Columns.Count - 1); i++)
            //{
            //    if (DO_EditComboxBox[i] != null)
            //    {
            //        DO_EditComboxBox[i].Visible = false;
            //    }
            //}
        }
        #endregion

        private void Hide_AllEdit()
        {
            for (byte i = 0; i <= (lv_DI.Columns.Count - 1); i++)
            {
                if (DI_EditComboxBox[i] != null)
                {
                    DI_EditComboxBox[i].Visible = false;
                }
            }

            for (byte i = 0; i <= (lv_DO.Columns.Count - 1); i++)
            {
                if (DO_EditComboxBox[i] != null)
                {
                    DO_EditComboxBox[i].Visible = false;
                }
            }
        }
        
        private void Init_DI()
        {
            ListViewItem listviewitem;

            

            this.lv_DI.Items.Clear();
            for (int i = 0; i < MAX_DI_COUNT_1; i++)
            {
                switch (form_Main.COMMDataManager.RX_DestDevType)
                {
                    case ConstClass.TYPE_SRM:
                        listviewitem = new ListViewItem(ConstClass.SRM_DI_Names_1[i, 0]);
                        break;
                    case ConstClass.TYPE_RTV:
                        listviewitem = new ListViewItem(ConstClass.RTV_DI_Names[i, 0]);
                        break;
                    case ConstClass.TYPE_EMS:
                        listviewitem = new ListViewItem(ConstClass.EMS_DI_Names[i, 0]);
                        break;
                    default:
                        listviewitem = new ListViewItem(ConstClass.RTV_DI_Names[i, 0]);
                        break;
                }

                listviewitem.SubItems.Add("");
                listviewitem.SubItems.Add("");
                listviewitem.SubItems.Add("");
                listviewitem.SubItems.Add("");
                switch (form_Main.COMMDataManager.RX_DestDevType)
                {
                    case ConstClass.TYPE_SRM:
                        if (ConstClass.SRM_DI_Names_1[i, 3] == "0") listviewitem.SubItems.Add("단독");
                        else listviewitem.SubItems.Add("");
                        break;
                    case ConstClass.TYPE_RTV:
                        if (ConstClass.RTV_DI_Names[i, 3] == "0") listviewitem.SubItems.Add("단독");
                        else listviewitem.SubItems.Add("");
                        break;
                    case ConstClass.TYPE_EMS:
                        if (ConstClass.EMS_DI_Names[i, 3] == "0") listviewitem.SubItems.Add("단독");
                        else listviewitem.SubItems.Add("");
                        break;
                    default:
                        if (ConstClass.RTV_DI_Names[i, 3] == "0") listviewitem.SubItems.Add("단독");
                        else listviewitem.SubItems.Add("");
                        break;
                }
                

                this.lv_DI.Items.Add(listviewitem);

            }

            if (MAX_DI_COUNT_2 > 0)
            {
                switch (form_Main.COMMDataManager.RX_DestDevType)
                {
                    case ConstClass.TYPE_SRM:
                        for (int i = 0; i < MAX_DI_COUNT_2; i++)
                        {
                            listviewitem = new ListViewItem(ConstClass.SRM_DI_Names_2[i, 0]);

                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");

                            if (ConstClass.SRM_DI_Names_2[i, 3] == "0") listviewitem.SubItems.Add("단독");
                            else listviewitem.SubItems.Add("");

                            this.lv_DI.Items.Add(listviewitem);
                        }
                        break;
                }
            }
        }

        private void Init_DO()
        {
            ListViewItem listviewitem;

            this.lv_DO.Items.Clear();
            for (int i = 0; i < MAX_DO_COUNT_1; i++)
            {
                switch (form_Main.COMMDataManager.RX_DestDevType)
                {
                    case ConstClass.TYPE_SRM:
                        listviewitem = new ListViewItem(ConstClass.SRM_DO_Names_1[i, 0]);
                        break;
                    case ConstClass.TYPE_RTV:
                        listviewitem = new ListViewItem(ConstClass.RTV_DO_Names_1[i, 0]);
                        break;
                    case ConstClass.TYPE_EMS:
                        listviewitem = new ListViewItem(ConstClass.EMS_DO_Names[i, 0]);
                        break;
                    default:
                        listviewitem = new ListViewItem(ConstClass.RTV_DO_Names_1[i, 0]);
                        break;
                }
                listviewitem.SubItems.Add("");
                listviewitem.SubItems.Add("");
                listviewitem.SubItems.Add("");

                this.lv_DO.Items.Add(listviewitem);

            }

            if (MAX_DO_COUNT_2 > 0)
            {
                switch (form_Main.COMMDataManager.RX_DestDevType)
                {
                    case ConstClass.TYPE_SRM:
                        for (int i = 0; i < MAX_DO_COUNT_2; i++)
                        {
                            listviewitem = new ListViewItem(ConstClass.SRM_DO_Names_2[i, 0]);
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");

                            this.lv_DO.Items.Add(listviewitem);
                        }
                        break;
                    case ConstClass.TYPE_RTV:
                        for (int i = 0; i < MAX_DO_COUNT_2; i++)
                        {
                            listviewitem = new ListViewItem(ConstClass.RTV_DO_Names_2[i, 0]);
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");
                            listviewitem.SubItems.Add("");

                            this.lv_DO.Items.Add(listviewitem);
                        }
                        break;
                }
            }
        }

        public unsafe bool Compare_Ctrl_St()
        {
            bool IsCompareOK = true;
            if (IsControl)
            {
                switch (form_Main.COMMDataManager.RX_DestDevType)
                {
                    case ConstClass.TYPE_SRM:
                        if (dev_REC_IOConfig.SRMIO.EthercatBoard[0] != CtrlREC.SRMIO.EthercatBoard[0]) lbl_ethercat_1.ForeColor = Color.Red; else lbl_ethercat_1.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.SRMIO.EthercatBoard[1] != CtrlREC.SRMIO.EthercatBoard[1]) lbl_ethercat_2.ForeColor = Color.Red; else lbl_ethercat_2.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.SRMIO.EthercatBoard[2] != CtrlREC.SRMIO.EthercatBoard[2]) lbl_ethercat_3.ForeColor = Color.Red; else lbl_ethercat_3.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.SRMIO.EthercatBoard[3] != CtrlREC.SRMIO.EthercatBoard[3]) lbl_ethercat_4.ForeColor = Color.Red; else lbl_ethercat_4.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.SRMIO.EthercatBoard[4] != CtrlREC.SRMIO.EthercatBoard[4]) lbl_ethercat_5.ForeColor = Color.Red; else lbl_ethercat_5.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.SRMIO.EthercatBoard[5] != CtrlREC.SRMIO.EthercatBoard[5]) lbl_ethercat_6.ForeColor = Color.Red; else lbl_ethercat_6.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.SRMIO.EthercatBoard[6] != CtrlREC.SRMIO.EthercatBoard[6]) lbl_ethercat_7.ForeColor = Color.Red; else lbl_ethercat_7.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.SRMIO.EthercatBoard[7] != CtrlREC.SRMIO.EthercatBoard[7]) lbl_ethercat_8.ForeColor = Color.Red; else lbl_ethercat_8.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.SRMIO.EthercatBoard[8] != CtrlREC.SRMIO.EthercatBoard[8]) lbl_ethercat_9.ForeColor = Color.Red; else lbl_ethercat_9.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.SRMIO.EthercatBoard[9] != CtrlREC.SRMIO.EthercatBoard[9]) lbl_ethercat_10.ForeColor = Color.Red; else lbl_ethercat_10.ForeColor = Color.Black;

                        fixed (VEXI_DEFS.REC_DIConfig* DIConfigStPtr = &dev_REC_IOConfig.SRMIO.DIConfig_1)
                        {
                            fixed (VEXI_DEFS.REC_DIConfig* DIConfigctrlPtr = &CtrlREC.SRMIO.DIConfig_1)
                            {
                                for (byte i = 0; i < MAX_DI_COUNT_1; i++)
                                {
                                    if (((DIConfigStPtr + i)->EthercatID != (DIConfigctrlPtr + i)->EthercatID) ||
                                        ((DIConfigStPtr + i)->Pin != (DIConfigctrlPtr + i)->Pin) ||
                                        ((DIConfigStPtr + i)->Type != (DIConfigctrlPtr + i)->Type) ||
                                        ((DIConfigStPtr + i)->Chattering != (DIConfigctrlPtr + i)->Chattering) ||
                                        ((DIConfigStPtr + i)->Dual != (DIConfigctrlPtr + i)->Dual))
                                    {
                                        lv_DI.Items[i].ForeColor = Color.Red;
                                        IsCompareOK = false;
                                    }
                                    else lv_DI.Items[i].ForeColor = Color.Black;

                                }
                            }
                        }

                        fixed (VEXI_DEFS.REC_DOConfig* DOConfigStPtr = &dev_REC_IOConfig.SRMIO.DOConfig_1)
                        {
                            fixed (VEXI_DEFS.REC_DOConfig* DOConfigctrlPtr = &CtrlREC.SRMIO.DOConfig_1)
                            {
                                for (byte i = 0; i < MAX_DO_COUNT_1; i++)
                                {
                                    if (((DOConfigStPtr + i)->EthercatID != (DOConfigctrlPtr + i)->EthercatID) ||
                                        ((DOConfigStPtr + i)->Pin != (DOConfigctrlPtr + i)->Pin) ||
                                        ((DOConfigStPtr + i)->Type != (DOConfigctrlPtr + i)->Type))
                                    {
                                        lv_DO.Items[i].ForeColor = Color.Red;
                                        IsCompareOK = false;
                                    }
                                    else lv_DO.Items[i].ForeColor = Color.Black;

                                }
                            }
                        }

                        fixed (VEXI_DEFS.REC_DIConfig* DIConfigStPtr = &dev_REC_IOConfig.SRMIO.DIConfig_123)
                        {
                            fixed (VEXI_DEFS.REC_DIConfig* DIConfigctrlPtr = &CtrlREC.SRMIO.DIConfig_123)
                            {
                                for (byte i = 0; i < MAX_DI_COUNT_2; i++)
                                {
                                    if (((DIConfigStPtr + i)->EthercatID != (DIConfigctrlPtr + i)->EthercatID) ||
                                        ((DIConfigStPtr + i)->Pin != (DIConfigctrlPtr + i)->Pin) ||
                                        ((DIConfigStPtr + i)->Type != (DIConfigctrlPtr + i)->Type) ||
                                        ((DIConfigStPtr + i)->Chattering != (DIConfigctrlPtr + i)->Chattering) ||
                                        ((DIConfigStPtr + i)->Dual != (DIConfigctrlPtr + i)->Dual))
                                    {
                                        lv_DI.Items[MAX_DI_COUNT_1 + i].ForeColor = Color.Red;
                                        IsCompareOK = false;
                                    }
                                    else lv_DI.Items[MAX_DI_COUNT_1 + i].ForeColor = Color.Black;

                                }
                            }
                        }

                        fixed (VEXI_DEFS.REC_DOConfig* DOConfigStPtr = &dev_REC_IOConfig.SRMIO.DOConfig_44)
                        {
                            fixed (VEXI_DEFS.REC_DOConfig* DOConfigctrlPtr = &CtrlREC.SRMIO.DOConfig_44)
                            {
                                for (byte i = 0; i < MAX_DO_COUNT_2; i++)
                                {
                                    if (((DOConfigStPtr + i)->EthercatID != (DOConfigctrlPtr + i)->EthercatID) ||
                                        ((DOConfigStPtr + i)->Pin != (DOConfigctrlPtr + i)->Pin) ||
                                        ((DOConfigStPtr + i)->Type != (DOConfigctrlPtr + i)->Type))
                                    {
                                        lv_DO.Items[MAX_DO_COUNT_1 + i].ForeColor = Color.Red;
                                        IsCompareOK = false;
                                    }
                                    else lv_DO.Items[MAX_DO_COUNT_1 + i].ForeColor = Color.Black;

                                }
                            }
                        }

                        
                        break;
                    case ConstClass.TYPE_RTV:
                        if (dev_REC_IOConfig.RTVIO.EthercatBoard[0] != CtrlREC.RTVIO.EthercatBoard[0]) lbl_ethercat_1.ForeColor = Color.Red; else lbl_ethercat_1.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.RTVIO.EthercatBoard[1] != CtrlREC.RTVIO.EthercatBoard[1]) lbl_ethercat_2.ForeColor = Color.Red; else lbl_ethercat_2.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.RTVIO.EthercatBoard[2] != CtrlREC.RTVIO.EthercatBoard[2]) lbl_ethercat_3.ForeColor = Color.Red; else lbl_ethercat_3.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.RTVIO.EthercatBoard[3] != CtrlREC.RTVIO.EthercatBoard[3]) lbl_ethercat_4.ForeColor = Color.Red; else lbl_ethercat_4.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.RTVIO.EthercatBoard[4] != CtrlREC.RTVIO.EthercatBoard[4]) lbl_ethercat_5.ForeColor = Color.Red; else lbl_ethercat_5.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.RTVIO.EthercatBoard[5] != CtrlREC.RTVIO.EthercatBoard[5]) lbl_ethercat_6.ForeColor = Color.Red; else lbl_ethercat_6.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.RTVIO.EthercatBoard[6] != CtrlREC.RTVIO.EthercatBoard[6]) lbl_ethercat_7.ForeColor = Color.Red; else lbl_ethercat_7.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.RTVIO.EthercatBoard[7] != CtrlREC.RTVIO.EthercatBoard[7]) lbl_ethercat_8.ForeColor = Color.Red; else lbl_ethercat_8.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.RTVIO.EthercatBoard[8] != CtrlREC.RTVIO.EthercatBoard[8]) lbl_ethercat_9.ForeColor = Color.Red; else lbl_ethercat_9.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.RTVIO.EthercatBoard[9] != CtrlREC.RTVIO.EthercatBoard[9]) lbl_ethercat_10.ForeColor = Color.Red; else lbl_ethercat_10.ForeColor = Color.Black;

                        fixed (VEXI_DEFS.REC_DIConfig* DIConfigStPtr = &dev_REC_IOConfig.RTVIO.DIConfig_1)
                        {
                            fixed (VEXI_DEFS.REC_DIConfig* DIConfigctrlPtr = &CtrlREC.RTVIO.DIConfig_1)
                            {
                                for (byte i = 0; i < MAX_DI_COUNT_1; i++)
                                {
                                    if (((DIConfigStPtr + i)->EthercatID != (DIConfigctrlPtr + i)->EthercatID) ||
                                        ((DIConfigStPtr + i)->Pin != (DIConfigctrlPtr + i)->Pin) ||
                                        ((DIConfigStPtr + i)->Type != (DIConfigctrlPtr + i)->Type) ||
                                        ((DIConfigStPtr + i)->Chattering != (DIConfigctrlPtr + i)->Chattering) ||
                                        ((DIConfigStPtr + i)->Dual != (DIConfigctrlPtr + i)->Dual))
                                    {
                                        lv_DI.Items[i].ForeColor = Color.Red;
                                        IsCompareOK = false;
                                    }
                                    else lv_DI.Items[i].ForeColor = Color.Black;

                                }
                            }
                        }

                        fixed (VEXI_DEFS.REC_DOConfig* DOConfigStPtr = &dev_REC_IOConfig.RTVIO.DOConfig_1)
                        {
                            fixed (VEXI_DEFS.REC_DOConfig* DOConfigctrlPtr = &CtrlREC.RTVIO.DOConfig_1)
                            {
                                for (byte i = 0; i < MAX_DO_COUNT_1; i++)
                                {
                                    if (((DOConfigStPtr + i)->EthercatID != (DOConfigctrlPtr + i)->EthercatID) ||
                                        ((DOConfigStPtr + i)->Pin != (DOConfigctrlPtr + i)->Pin) ||
                                        ((DOConfigStPtr + i)->Type != (DOConfigctrlPtr + i)->Type))
                                    {
                                        lv_DO.Items[i].ForeColor = Color.Red;
                                        IsCompareOK = false;
                                    }
                                    else lv_DO.Items[i].ForeColor = Color.Black;

                                }
                            }
                        }

                        fixed (VEXI_DEFS.REC_DOConfig* DOConfigStPtr = &dev_REC_IOConfig.RTVIO.DOConfig_64)
                        {
                            fixed (VEXI_DEFS.REC_DOConfig* DOConfigctrlPtr = &CtrlREC.RTVIO.DOConfig_64)
                            {
                                for (byte i = 0; i < MAX_DO_COUNT_2; i++)
                                {
                                    if (((DOConfigStPtr + i)->EthercatID != (DOConfigctrlPtr + i)->EthercatID) ||
                                        ((DOConfigStPtr + i)->Pin != (DOConfigctrlPtr + i)->Pin) ||
                                        ((DOConfigStPtr + i)->Type != (DOConfigctrlPtr + i)->Type))
                                    {
                                        lv_DO.Items[MAX_DO_COUNT_1 + i].ForeColor = Color.Red;
                                        IsCompareOK = false;
                                    }
                                    else lv_DO.Items[MAX_DO_COUNT_1 + i].ForeColor = Color.Black;

                                }
                            }
                        }
                        break;
                    case ConstClass.TYPE_EMS:
                        if (dev_REC_IOConfig.EMSIO.EthercatBoard[0] != CtrlREC.EMSIO.EthercatBoard[0]) lbl_ethercat_1.ForeColor = Color.Red; else lbl_ethercat_1.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.EMSIO.EthercatBoard[1] != CtrlREC.EMSIO.EthercatBoard[1]) lbl_ethercat_2.ForeColor = Color.Red; else lbl_ethercat_2.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.EMSIO.EthercatBoard[2] != CtrlREC.EMSIO.EthercatBoard[2]) lbl_ethercat_3.ForeColor = Color.Red; else lbl_ethercat_3.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.EMSIO.EthercatBoard[3] != CtrlREC.EMSIO.EthercatBoard[3]) lbl_ethercat_4.ForeColor = Color.Red; else lbl_ethercat_4.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.EMSIO.EthercatBoard[4] != CtrlREC.EMSIO.EthercatBoard[4]) lbl_ethercat_5.ForeColor = Color.Red; else lbl_ethercat_5.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.EMSIO.EthercatBoard[5] != CtrlREC.EMSIO.EthercatBoard[5]) lbl_ethercat_6.ForeColor = Color.Red; else lbl_ethercat_6.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.EMSIO.EthercatBoard[6] != CtrlREC.EMSIO.EthercatBoard[6]) lbl_ethercat_7.ForeColor = Color.Red; else lbl_ethercat_7.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.EMSIO.EthercatBoard[7] != CtrlREC.EMSIO.EthercatBoard[7]) lbl_ethercat_8.ForeColor = Color.Red; else lbl_ethercat_8.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.EMSIO.EthercatBoard[8] != CtrlREC.EMSIO.EthercatBoard[8]) lbl_ethercat_9.ForeColor = Color.Red; else lbl_ethercat_9.ForeColor = Color.Black;
                        if (dev_REC_IOConfig.EMSIO.EthercatBoard[9] != CtrlREC.EMSIO.EthercatBoard[9]) lbl_ethercat_10.ForeColor = Color.Red; else lbl_ethercat_10.ForeColor = Color.Black;

                        fixed (VEXI_DEFS.REC_DIConfig* DIConfigStPtr = &dev_REC_IOConfig.EMSIO.DIConfig)
                        {
                            fixed (VEXI_DEFS.REC_DIConfig* DIConfigctrlPtr = &CtrlREC.EMSIO.DIConfig)
                            {
                                for (byte i = 0; i < MAX_DI_COUNT_1; i++)
                                {
                                    if (((DIConfigStPtr + i)->EthercatID != (DIConfigctrlPtr + i)->EthercatID) ||
                                        ((DIConfigStPtr + i)->Pin != (DIConfigctrlPtr + i)->Pin) ||
                                        ((DIConfigStPtr + i)->Type != (DIConfigctrlPtr + i)->Type) ||
                                        ((DIConfigStPtr + i)->Chattering != (DIConfigctrlPtr + i)->Chattering) ||
                                        ((DIConfigStPtr + i)->Dual != (DIConfigctrlPtr + i)->Dual))
                                    {
                                        lv_DI.Items[i].ForeColor = Color.Red;
                                        IsCompareOK = false;
                                    }
                                    else lv_DI.Items[i].ForeColor = Color.Black;

                                }
                            }
                        }

                        fixed (VEXI_DEFS.REC_DOConfig* DOConfigStPtr = &dev_REC_IOConfig.EMSIO.DOConfig)
                        {
                            fixed (VEXI_DEFS.REC_DOConfig* DOConfigctrlPtr = &CtrlREC.EMSIO.DOConfig)
                            {
                                for (byte i = 0; i < MAX_DO_COUNT_1; i++)
                                {
                                    if (((DOConfigStPtr + i)->EthercatID != (DOConfigctrlPtr + i)->EthercatID) ||
                                        ((DOConfigStPtr + i)->Pin != (DOConfigctrlPtr + i)->Pin) ||
                                        ((DOConfigStPtr + i)->Type != (DOConfigctrlPtr + i)->Type))
                                    {
                                        lv_DO.Items[i].ForeColor = Color.Red;
                                        IsCompareOK = false;
                                    }
                                    else lv_DO.Items[i].ForeColor = Color.Black;

                                }
                            }
                        }

                        
                        break;
                    default:
                        break;
                }


                return IsCompareOK;
            } else
            {
                return true;
            }
        }


        public unsafe void Process_SRMSt_OLDIO()
        {
            if (!IsSRM_New)
            {
                fixed (VEXI_DEFS.REC_DIConfig* DICOnfigPtr = &dev_REC_IOConfig.SRMIO.DIConfig_123)
                {
                    //123 -> 0
                    //129 ~ 149 -> 6 ~ 26
                    for (int i = 6; i <= 26; i++)
                    {
                        (DICOnfigPtr + i)->EthercatID = 255;
                        (DICOnfigPtr + i)->Pin = 0;
                        (DICOnfigPtr + i)->Type = 0;
                        (DICOnfigPtr + i)->Chattering = 0;
                        (DICOnfigPtr + i)->Dual = 0;
                    }
                };

                fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &dev_REC_IOConfig.SRMIO.DOConfig_1)
                {
                    //1 -> 0
                    //38 ~ 43 -> 37 ~ 42
                    for (int i = 37; i <= 42; i++)
                    {
                        (DOCOnfigPtr + i)->EthercatID = 255;
                        (DOCOnfigPtr + i)->Pin = 0;
                        (DOCOnfigPtr + i)->Type = 0;
                    }
                };

                fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &dev_REC_IOConfig.SRMIO.DOConfig_44)
                {
                    //44 -> 0
                    //44 ~ 69 -> 0 ~ 25
                    for (int i = 0; i <= 25; i++)
                    {
                        (DOCOnfigPtr + i)->EthercatID = 255;
                        (DOCOnfigPtr + i)->Pin = 0;
                        (DOCOnfigPtr + i)->Type = 0;
                    }
                };
            }
        }

        public unsafe void Process_SRMCtrl_OLDIO()
        {
            if (!IsSRM_New)
            {
                fixed (VEXI_DEFS.REC_DIConfig* DICOnfigPtr = &CtrlREC.SRMIO.DIConfig_123)
                {
                    //123 -> 0
                    //129 ~ 149 -> 6 ~ 26
                    for (int i = 6; i <= 26; i++)
                    {
                        (DICOnfigPtr + i)->EthercatID = 255;
                        (DICOnfigPtr + i)->Pin = 0;
                        (DICOnfigPtr + i)->Type = 0;
                        (DICOnfigPtr + i)->Chattering = 0;
                        (DICOnfigPtr + i)->Dual = 0;
                    }
                };

                fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &CtrlREC.SRMIO.DOConfig_1)
                {
                    //1 -> 0
                    //38 ~ 43 -> 37 ~ 42
                    for (int i = 37; i <= 42; i++)
                    {
                        (DOCOnfigPtr + i)->EthercatID = 255;
                        (DOCOnfigPtr + i)->Pin = 0;
                        (DOCOnfigPtr + i)->Type = 0;
                    }
                };

                fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &CtrlREC.SRMIO.DOConfig_44)
                {
                    //44 -> 0
                    //44 ~ 69 -> 0 ~ 25
                    for (int i = 0; i <= 25; i++)
                    {
                        (DOCOnfigPtr + i)->EthercatID = 255;
                        (DOCOnfigPtr + i)->Pin = 0;
                        (DOCOnfigPtr + i)->Type = 0;
                    }
                };
            }
        }

        public void Display_IOConfig(byte[] data)
        {
            //dev_REC_IOConfig = (VEXI_DEFS.DEV_IOConfig)Global_Class.UTIL_BytesToStructure(data, typeof(VEXI_DEFS.DEV_IOConfig));

            IsRXIO = true;
            dev_REC_IOConfig = (VEXI_DEFS.DevUnion_IOConfig)Global_Class.UTIL_BytesToStructure(data, data.Length, typeof(VEXI_DEFS.DevUnion_IOConfig));

            
            if (form_Main.COMMDataManager.RX_DestDevType == ConstClass.TYPE_SRM)
            {
                if (data.Length <= 844)
                {
                    //IN 135~149, OUT 44~69 추가전  (IN 128, OUT 37)
                    IsSRM_New = false;
                    Process_SRMSt_OLDIO();
                } else
                {
                    IsSRM_New = true;
                }
            }

            Display_IOConfig(true, false);
        }

        private unsafe void Display_IOConfig(bool IsResponse, bool IsLoadCtrl)
        {
            if (IsResponse && IsControl)
            {
                if (Compare_Ctrl_St())
                {
                    //form_Main.GlobalObj.MsgBox_Confirm_OK("제어값과 상태값이 일치합니다");
                    IsControl = false;
                }
                else
                {
                    form_Main.GlobalObj.MsgBox_Info("제어값과 상태값이 일치하지 않습니다", "W");
                }
            }

            switch (form_Main.COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    fixed (VEXI_DEFS.REC_Scan_EthercatSlave* Ptr_1 = &dev_REC_IOConfig.SRMIO.Scan_EthercatSlave1)
                    {
                        for (byte i = 0; i < 10; i++)
                        {
                            switch ((Ptr_1 + i)->BoardType)
                            {
                                case 0:
                                    lbl_Scan[i].Text = "연결없음";
                                    break;
                                case 1:
                                    lbl_Scan[i].Text = String.Format("MX-DIO-10X : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 2:
                                    lbl_Scan[i].Text = String.Format("MX-DIO-20X : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 3:
                                    lbl_Scan[i].Text = String.Format("MX-DIO-30X : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 4:
                                    lbl_Scan[i].Text = String.Format("MX-RLY-10X : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 5:
                                    lbl_Scan[i].Text = String.Format("MX-EXT-10X : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 6:
                                    lbl_Scan[i].Text = String.Format("GX-MD1611 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 7:
                                    lbl_Scan[i].Text = String.Format("GX-ID1618 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 8:
                                    lbl_Scan[i].Text = String.Format("GX-ID3218 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 9:
                                    lbl_Scan[i].Text = String.Format("GX-EC0211 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 10:
                                    lbl_Scan[i].Text = String.Format("GX-MD1612 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 50:
                                    lbl_Scan[i].Text = String.Format("ACS380 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 255:
                                    lbl_Scan[i].Text = String.Format("Not Define : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                default:
                                    lbl_Scan[i].Text = String.Format("{0} : {1}", (Ptr_1 + i)->BoardType, (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                            }
                        }
                    }

                    for (byte i = 0; i < 10; i++)
                    {
                        switch (dev_REC_IOConfig.SRMIO.EthercatBoard[i])
                        {
                            case 0:
                                lbl_Config[i].Text = "연결없음";
                                break;
                            case 1:
                                lbl_Config[i].Text = "MX-DIO-10X";
                                break;
                            case 2:
                                lbl_Config[i].Text = "MX-DIO-20X";
                                break;
                            case 3:
                                lbl_Config[i].Text = "MX-DIO-30X";
                                break;
                            case 4:
                                lbl_Config[i].Text = "MX-RLY-10X";
                                break;
                            case 5:
                                lbl_Config[i].Text = "MX-EXT-10X";
                                break;
                            case 6:
                                lbl_Config[i].Text = "GX-MD1611";
                                break;
                            case 7:
                                lbl_Config[i].Text = "GX-ID1618";
                                break;
                            case 8:
                                lbl_Config[i].Text = "GX-ID3218";
                                break;
                            case 9:
                                lbl_Config[i].Text = "GX-EC0211";
                                break;
                            case 10:
                                lbl_Config[i].Text = "GX-MD1612";
                                break;
                            case 50:
                                lbl_Config[i].Text = "ACS380";
                                break;
                            case 255:
                                lbl_Config[i].Text = "Not Define";
                                break;
                            default:
                                lbl_Config[i].Text = "";
                                cb_Config[i].SelectedIndex = -1;
                                break;
                        }

                        if (dev_REC_IOConfig.SRMIO.EthercatBoard[i] <= 10)
                        {
                            if (IsLoadCtrl)
                            {
                                cb_Config[i].SelectedIndex = dev_REC_IOConfig.SRMIO.EthercatBoard[i];
                            }
                            else
                            {
                                if (IsResponse)
                                {
                                    if (cb_Config[i].SelectedIndex == -1) cb_Config[i].SelectedIndex = dev_REC_IOConfig.SRMIO.EthercatBoard[i];
                                }
                            }
                        }
                        else
                        {
                            switch (dev_REC_IOConfig.SRMIO.EthercatBoard[i])
                            {
                                case 50:
                                    if (IsLoadCtrl)
                                    {
                                        cb_Config[i].SelectedIndex = 11;
                                    }
                                    else
                                    {
                                        if (IsResponse)
                                        {
                                            if (cb_Config[i].SelectedIndex == -1) cb_Config[i].SelectedIndex = 11;
                                        }
                                    }
                                    break;
                                case 255:
                                    if (IsLoadCtrl)
                                    {
                                        cb_Config[i].SelectedIndex = 12;
                                    }
                                    else
                                    {
                                        if (IsResponse)
                                        {
                                            if (cb_Config[i].SelectedIndex == -1) cb_Config[i].SelectedIndex = 12;
                                        }
                                    }
                                    break;
                            }

                        }

                    }


                    fixed (VEXI_DEFS.REC_DIConfig* DICOnfigPtr = &dev_REC_IOConfig.SRMIO.DIConfig_1)
                    {
                        for (byte i = 0; i < MAX_DI_COUNT_1; i++)
                        {
                            if ((DICOnfigPtr + i)->EthercatID == 0) lv_DI.Items[i].SubItems[1].Text = "MCU";
                            else if ((DICOnfigPtr + i)->EthercatID == 255) lv_DI.Items[i].SubItems[1].Text = "신호없음";
                            else lv_DI.Items[i].SubItems[1].Text = string.Format("{0}", (DICOnfigPtr + i)->EthercatID);

                            if ((DICOnfigPtr + i)->EthercatID != 255)
                            {
                                lv_DI.Items[i].SubItems[2].Text = string.Format("{0}", (DICOnfigPtr + i)->Pin);
                            }
                            else
                            {
                                lv_DI.Items[i].SubItems[2].Text = "없음";
                            }

                            if ((DICOnfigPtr + i)->Type != 1) lv_DI.Items[i].SubItems[3].Text = "A";
                            else lv_DI.Items[i].SubItems[3].Text = "B";
                            lv_DI.Items[i].SubItems[4].Text = string.Format("{0}", (DICOnfigPtr + i)->Chattering);

                                    if (ConstClass.SRM_DI_Names_1[i, 3] == "0")
                                    {
                                        lv_DI.Items[i].SubItems[5].Text = "단독";
                                    }
                                    else
                                    {
                                        if ((DICOnfigPtr + i)->Dual == 2) lv_DI.Items[i].SubItems[5].Text = "AND";
                                        else lv_DI.Items[i].SubItems[5].Text = "OR";
                                    }

                            //Ptr = Ptr + 1;
                        }
                    }

                    fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &dev_REC_IOConfig.SRMIO.DOConfig_1)
                    {
                        for (byte i = 0; i < MAX_DO_COUNT_1; i++)
                        {
                            if ((DOCOnfigPtr + i)->EthercatID == 0) lv_DO.Items[i].SubItems[1].Text = "MCU";
                            else if ((DOCOnfigPtr + i)->EthercatID == 255) lv_DO.Items[i].SubItems[1].Text = "신호없음";
                            else lv_DO.Items[i].SubItems[1].Text = string.Format("{0}", (DOCOnfigPtr + i)->EthercatID);

                            if ((DOCOnfigPtr + i)->EthercatID != 255)
                            {
                                lv_DO.Items[i].SubItems[2].Text = string.Format("{0}", (DOCOnfigPtr + i)->Pin);
                            }
                            else
                            {
                                lv_DO.Items[i].SubItems[2].Text = "없음";
                            }
                            if ((DOCOnfigPtr + i)->Type != 1) lv_DO.Items[i].SubItems[3].Text = "A";
                            else lv_DO.Items[i].SubItems[3].Text = "B";

                            //Ptr = Ptr + 1;
                        }

                    }

                    fixed (VEXI_DEFS.REC_DIConfig* DICOnfigPtr = &dev_REC_IOConfig.SRMIO.DIConfig_123)
                    {
                        for (byte i = 0; i < MAX_DI_COUNT_2; i++)
                        {
                            if ((DICOnfigPtr + i)->EthercatID == 0) lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[1].Text = "MCU";
                            else if ((DICOnfigPtr + i)->EthercatID == 255) lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[1].Text = "신호없음";
                            else lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[1].Text = string.Format("{0}", (DICOnfigPtr + i)->EthercatID);

                            if((DICOnfigPtr + i)->EthercatID != 255)
                            {
                                lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[2].Text = string.Format("{0}", (DICOnfigPtr + i)->Pin);
                            }
                            else
                            {
                                lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[2].Text = "없음";
                            }

                            if ((DICOnfigPtr + i)->Type != 1) lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[3].Text = "A";
                            else lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[3].Text = "B";
                            lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[4].Text = string.Format("{0}", (DICOnfigPtr + i)->Chattering);

                            if (ConstClass.SRM_DI_Names_2[i, 3] == "0")
                            {
                                lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[5].Text = "단독";
                            }
                            else
                            {
                                if ((DICOnfigPtr + i)->Dual == 2) lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[5].Text = "AND";
                                else lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[5].Text = "OR";
                            }

                            //Ptr = Ptr + 1;
                        }
                    }

                    fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &dev_REC_IOConfig.SRMIO.DOConfig_44)
                    {
                        for (byte i = 0; i < MAX_DO_COUNT_2; i++)
                        {
                            if ((DOCOnfigPtr + i)->EthercatID == 0) lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[1].Text = "MCU";
                            else if ((DOCOnfigPtr + i)->EthercatID == 255) lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[1].Text = "신호없음";
                            else lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[1].Text = string.Format("{0}", (DOCOnfigPtr + i)->EthercatID);

                            if ((DOCOnfigPtr + i)->EthercatID != 255)
                            {
                                lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[2].Text = string.Format("{0}", (DOCOnfigPtr + i)->Pin);
                            }
                            else
                            {
                                lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[2].Text = "없음";
                            }
                            if ((DOCOnfigPtr + i)->Type != 1) lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[3].Text = "A";
                            else lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[3].Text = "B";

                            //Ptr = Ptr + 1;
                        }

                    }
                    break;
                case ConstClass.TYPE_RTV:
                    fixed (VEXI_DEFS.REC_Scan_EthercatSlave* Ptr_1 = &dev_REC_IOConfig.RTVIO.Scan_EthercatSlave1)
                    {
                        for (byte i = 0; i < 10; i++)
                        {
                            switch ((Ptr_1 + i)->BoardType)
                            {
                                case 0:
                                    lbl_Scan[i].Text = "연결없음";
                                    break;
                                case 1:
                                    lbl_Scan[i].Text = String.Format("MX-DIO-10X : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 2:
                                    lbl_Scan[i].Text = String.Format("MX-DIO-20X : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 3:
                                    lbl_Scan[i].Text = String.Format("MX-DIO-30X : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 4:
                                    lbl_Scan[i].Text = String.Format("MX-RLY-10X : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 5:
                                    lbl_Scan[i].Text = String.Format("MX-EXT-10X : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 6:
                                    lbl_Scan[i].Text = String.Format("GX-MD1611 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 7:
                                    lbl_Scan[i].Text = String.Format("GX-ID1618 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 8:
                                    lbl_Scan[i].Text = String.Format("GX-ID3218 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 9:
                                    lbl_Scan[i].Text = String.Format("GX-EC0211 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 10:
                                    lbl_Scan[i].Text = String.Format("GX-MD1612 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 50:
                                    lbl_Scan[i].Text = String.Format("ACS380 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 255:
                                    lbl_Scan[i].Text = String.Format("Not Define : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                default:
                                    lbl_Scan[i].Text = String.Format("{0} : {1}", (Ptr_1 + i)->BoardType, (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                            }
                        }
                    }

                    for (byte i = 0; i < 10; i++)
                    {
                        switch (dev_REC_IOConfig.RTVIO.EthercatBoard[i])
                        {
                            case 0:
                                lbl_Config[i].Text = "연결없음";
                                break;
                            case 1:
                                lbl_Config[i].Text = "MX-DIO-10X";
                                break;
                            case 2:
                                lbl_Config[i].Text = "MX-DIO-20X";
                                break;
                            case 3:
                                lbl_Config[i].Text = "MX-DIO-30X";
                                break;
                            case 4:
                                lbl_Config[i].Text = "MX-RLY-10X";
                                break;
                            case 5:
                                lbl_Config[i].Text = "MX-EXT-10X";
                                break;
                            case 6:
                                lbl_Config[i].Text = "GX-MD1611";
                                break;
                            case 7:
                                lbl_Config[i].Text = "GX-ID1618";
                                break;
                            case 8:
                                lbl_Config[i].Text = "GX-ID3218";
                                break;
                            case 9:
                                lbl_Config[i].Text = "GX-EC0211";
                                break;
                            case 10:
                                lbl_Config[i].Text = "GX-MD1612";
                                break;
                            case 50:
                                lbl_Config[i].Text = "ACS380";
                                break;
                            case 255:
                                lbl_Config[i].Text = "Not Define";
                                break;
                            default:
                                lbl_Config[i].Text = "";
                                cb_Config[i].SelectedIndex = -1;
                                break;
                        }

                        if (dev_REC_IOConfig.RTVIO.EthercatBoard[i] <= 10)
                        {
                            if (IsLoadCtrl)
                            {
                                cb_Config[i].SelectedIndex = dev_REC_IOConfig.RTVIO.EthercatBoard[i];
                            }
                            else
                            {
                                if (IsResponse)
                                {
                                    if (cb_Config[i].SelectedIndex == -1) cb_Config[i].SelectedIndex = dev_REC_IOConfig.RTVIO.EthercatBoard[i];
                                }
                            }
                        }
                        else
                        {
                            switch (dev_REC_IOConfig.RTVIO.EthercatBoard[i])
                            {
                                case 50:
                                    if (IsLoadCtrl)
                                    {
                                        cb_Config[i].SelectedIndex = 11;
                                    }
                                    else
                                    {
                                        if (IsResponse)
                                        {
                                            if (cb_Config[i].SelectedIndex == -1) cb_Config[i].SelectedIndex = 11;
                                        }
                                    }
                                    break;
                                case 255:
                                    if (IsLoadCtrl)
                                    {
                                        cb_Config[i].SelectedIndex = 12;
                                    }
                                    else
                                    {
                                        if (IsResponse)
                                        {
                                            if (cb_Config[i].SelectedIndex == -1) cb_Config[i].SelectedIndex = 12;
                                        }
                                    }
                                    break;
                            }

                        }

                    }


                    fixed (VEXI_DEFS.REC_DIConfig* DICOnfigPtr = &dev_REC_IOConfig.RTVIO.DIConfig_1)
                    {
                        for (byte i = 0; i < MAX_DI_COUNT_1; i++)
                        {
                            if ((DICOnfigPtr + i)->EthercatID == 0) lv_DI.Items[i].SubItems[1].Text = "MCU";
                            else if ((DICOnfigPtr + i)->EthercatID == 255) lv_DI.Items[i].SubItems[1].Text = "신호없음";
                            else lv_DI.Items[i].SubItems[1].Text = string.Format("{0}", (DICOnfigPtr + i)->EthercatID);

                            if ((DICOnfigPtr + i)->EthercatID != 255)
                            {
                                lv_DI.Items[i].SubItems[2].Text = string.Format("{0}", (DICOnfigPtr + i)->Pin);
                            }
                            else
                            {
                                lv_DI.Items[i].SubItems[2].Text = "없음";
                            }

                            if ((DICOnfigPtr + i)->Type != 1) lv_DI.Items[i].SubItems[3].Text = "A";
                            else lv_DI.Items[i].SubItems[3].Text = "B";
                            lv_DI.Items[i].SubItems[4].Text = string.Format("{0}", (DICOnfigPtr + i)->Chattering);

                            if (ConstClass.RTV_DI_Names[i, 3] == "0")
                            {
                                lv_DI.Items[i].SubItems[5].Text = "단독";
                            }
                            else
                            {
                                if ((DICOnfigPtr + i)->Dual == 2) lv_DI.Items[i].SubItems[5].Text = "AND";
                                else lv_DI.Items[i].SubItems[5].Text = "OR";
                            }

                            //Ptr = Ptr + 1;
                        }
                    }

                    fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &dev_REC_IOConfig.RTVIO.DOConfig_1)
                    {
                        for (byte i = 0; i < MAX_DO_COUNT_1; i++)
                        {
                            if ((DOCOnfigPtr + i)->EthercatID == 0) lv_DO.Items[i].SubItems[1].Text = "MCU";
                            else if ((DOCOnfigPtr + i)->EthercatID == 255) lv_DO.Items[i].SubItems[1].Text = "신호없음";
                            else lv_DO.Items[i].SubItems[1].Text = string.Format("{0}", (DOCOnfigPtr + i)->EthercatID);

                            if ((DOCOnfigPtr + i)->EthercatID != 255)
                            {
                                lv_DO.Items[i].SubItems[2].Text = string.Format("{0}", (DOCOnfigPtr + i)->Pin);
                            }
                            else
                            {
                                lv_DO.Items[i].SubItems[2].Text = "없음";
                            }
                            if ((DOCOnfigPtr + i)->Type != 1) lv_DO.Items[i].SubItems[3].Text = "A";
                            else lv_DO.Items[i].SubItems[3].Text = "B";

                            //Ptr = Ptr + 1;
                        }

                    }

                    fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &dev_REC_IOConfig.RTVIO.DOConfig_64)
                    {
                        for (byte i = 0; i < MAX_DO_COUNT_2; i++)
                        {
                            if ((DOCOnfigPtr + i)->EthercatID == 0) lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[1].Text = "MCU";
                            else if ((DOCOnfigPtr + i)->EthercatID == 255) lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[1].Text = "신호없음";
                            else lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[1].Text = string.Format("{0}", (DOCOnfigPtr + i)->EthercatID);

                            if ((DOCOnfigPtr + i)->EthercatID != 255)
                            {
                                lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[2].Text = string.Format("{0}", (DOCOnfigPtr + i)->Pin);
                            }
                            else
                            {
                                lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[2].Text = "없음";
                            }
                            if ((DOCOnfigPtr + i)->Type != 1) lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[3].Text = "A";
                            else lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[3].Text = "B";

                            //Ptr = Ptr + 1;
                        }

                    }

                    break;
                case ConstClass.TYPE_EMS:
                    fixed (VEXI_DEFS.REC_Scan_EthercatSlave* Ptr_1 = &dev_REC_IOConfig.EMSIO.Scan_EthercatSlave1)
                    {
                        for (byte i = 0; i < 10; i++)
                        {
                            switch ((Ptr_1 + i)->BoardType)
                            {
                                case 0:
                                    lbl_Scan[i].Text = "연결없음";
                                    break;
                                case 1:
                                    lbl_Scan[i].Text = String.Format("MX-DIO-10X : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 2:
                                    lbl_Scan[i].Text = String.Format("MX-DIO-20X : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 3:
                                    lbl_Scan[i].Text = String.Format("MX-DIO-30X : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 4:
                                    lbl_Scan[i].Text = String.Format("MX-RLY-10X : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 5:
                                    lbl_Scan[i].Text = String.Format("MX-EXT-10X : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 6:
                                    lbl_Scan[i].Text = String.Format("GX-MD1611 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 7:
                                    lbl_Scan[i].Text = String.Format("GX-ID1618 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 8:
                                    lbl_Scan[i].Text = String.Format("GX-ID3218 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 9:
                                    lbl_Scan[i].Text = String.Format("GX-EC0211 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 10:
                                    lbl_Scan[i].Text = String.Format("GX-MD1612 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 50:
                                    lbl_Scan[i].Text = String.Format("ACS380 : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                case 255:
                                    lbl_Scan[i].Text = String.Format("Not Define : {0}", (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                                default:
                                    lbl_Scan[i].Text = String.Format("{0} : {1}", (Ptr_1 + i)->BoardType, (Ptr_1 + i)->Slave_IDSwitch);
                                    break;
                            }
                        }
                    }

                    for (byte i = 0; i < 10; i++)
                    {
                        switch (dev_REC_IOConfig.EMSIO.EthercatBoard[i])
                        {
                            case 0:
                                lbl_Config[i].Text = "연결없음";
                                break;
                            case 1:
                                lbl_Config[i].Text = "MX-DIO-10X";
                                break;
                            case 2:
                                lbl_Config[i].Text = "MX-DIO-20X";
                                break;
                            case 3:
                                lbl_Config[i].Text = "MX-DIO-30X";
                                break;
                            case 4:
                                lbl_Config[i].Text = "MX-RLY-10X";
                                break;
                            case 5:
                                lbl_Config[i].Text = "MX-EXT-10X";
                                break;
                            case 6:
                                lbl_Config[i].Text = "GX-MD1611";
                                break;
                            case 7:
                                lbl_Config[i].Text = "GX-ID1618";
                                break;
                            case 8:
                                lbl_Config[i].Text = "GX-ID3218";
                                break;
                            case 9:
                                lbl_Config[i].Text = "GX-EC0211";
                                break;
                            case 10:
                                lbl_Config[i].Text = "GX-MD1612";
                                break;
                            case 50:
                                lbl_Config[i].Text = "ACS380";
                                break;
                            case 255:
                                lbl_Config[i].Text = "Not Define";
                                break;
                            default:
                                lbl_Config[i].Text = "";
                                cb_Config[i].SelectedIndex = -1;
                                break;
                        }

                        if (dev_REC_IOConfig.EMSIO.EthercatBoard[i] <= 10)
                        {
                            if (IsLoadCtrl)
                            {
                                cb_Config[i].SelectedIndex = dev_REC_IOConfig.EMSIO.EthercatBoard[i];
                            }
                            else
                            {
                                if (IsResponse)
                                {
                                    if (cb_Config[i].SelectedIndex == -1) cb_Config[i].SelectedIndex = dev_REC_IOConfig.EMSIO.EthercatBoard[i];
                                }
                            }
                        }
                        else
                        {
                            switch (dev_REC_IOConfig.EMSIO.EthercatBoard[i])
                            {
                                case 50:
                                    if (IsLoadCtrl)
                                    {
                                        cb_Config[i].SelectedIndex = 11;
                                    }
                                    else
                                    {
                                        if (IsResponse)
                                        {
                                            if (cb_Config[i].SelectedIndex == -1) cb_Config[i].SelectedIndex = 11;
                                        }
                                    }
                                    break;
                                case 255:
                                    if (IsLoadCtrl)
                                    {
                                        cb_Config[i].SelectedIndex = 12;
                                    }
                                    else
                                    {
                                        if (IsResponse)
                                        {
                                            if (cb_Config[i].SelectedIndex == -1) cb_Config[i].SelectedIndex = 12;
                                        }
                                    }
                                    break;
                            }

                        }

                    }


                    fixed (VEXI_DEFS.REC_DIConfig* DICOnfigPtr = &dev_REC_IOConfig.EMSIO.DIConfig)
                    {
                        for (byte i = 0; i < MAX_DI_COUNT_1; i++)
                        {
                            if ((DICOnfigPtr + i)->EthercatID == 0) lv_DI.Items[i].SubItems[1].Text = "MCU";
                            else if ((DICOnfigPtr + i)->EthercatID == 255) lv_DI.Items[i].SubItems[1].Text = "신호없음";
                            else lv_DI.Items[i].SubItems[1].Text = string.Format("{0}", (DICOnfigPtr + i)->EthercatID);

                            if ((DICOnfigPtr + i)->EthercatID != 255)
                            {
                                lv_DI.Items[i].SubItems[2].Text = string.Format("{0}", (DICOnfigPtr + i)->Pin);
                            }
                            else
                            {
                                lv_DI.Items[i].SubItems[2].Text = "없음";
                            }

                            if ((DICOnfigPtr + i)->Type != 1) lv_DI.Items[i].SubItems[3].Text = "A";
                            else lv_DI.Items[i].SubItems[3].Text = "B";
                            lv_DI.Items[i].SubItems[4].Text = string.Format("{0}", (DICOnfigPtr + i)->Chattering);

                            if (ConstClass.EMS_DI_Names[i, 3] == "0")
                            {
                                lv_DI.Items[i].SubItems[5].Text = "단독";
                            }
                            else
                            {
                                if ((DICOnfigPtr + i)->Dual == 2) lv_DI.Items[i].SubItems[5].Text = "AND";
                                else lv_DI.Items[i].SubItems[5].Text = "OR";
                            }

                            //Ptr = Ptr + 1;
                        }
                    }

                    fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &dev_REC_IOConfig.EMSIO.DOConfig)
                    {
                        for (byte i = 0; i < MAX_DO_COUNT_1; i++)
                        {
                            if ((DOCOnfigPtr + i)->EthercatID == 0) lv_DO.Items[i].SubItems[1].Text = "MCU";
                            else if ((DOCOnfigPtr + i)->EthercatID == 255) lv_DO.Items[i].SubItems[1].Text = "신호없음";
                            else lv_DO.Items[i].SubItems[1].Text = string.Format("{0}", (DOCOnfigPtr + i)->EthercatID);

                            if ((DOCOnfigPtr + i)->EthercatID != 255)
                            {
                                lv_DO.Items[i].SubItems[2].Text = string.Format("{0}", (DOCOnfigPtr + i)->Pin);
                            }
                            else
                            {
                                lv_DO.Items[i].SubItems[2].Text = "없음";
                            }
                            if ((DOCOnfigPtr + i)->Type != 1) lv_DO.Items[i].SubItems[3].Text = "A";
                            else lv_DO.Items[i].SubItems[3].Text = "B";

                            //Ptr = Ptr + 1;
                        }

                    }

                    break;
                default:
                    break;
            }


            
            switch (form_Main.COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    btnSet.Enabled = ((IsRXIO) &&  (form_Main.COMMDataManager.DevRec.srm_REC_SRMSt.DevMode & 0x08) != 0);
                    break;
                case ConstClass.TYPE_RTV:
                    btnSet.Enabled = ((form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.DevMode & 0x08) != 0);
                    break;
                case ConstClass.TYPE_EMS:
                    btnSet.Enabled = ((form_Main.COMMDataManager.DevRec.ems_REC_EMSSt.DevMode & 0x08) != 0);
                    break;
                default:
                    btnSet.Enabled = ((form_Main.COMMDataManager.DevRec.rtv_REC_RTVSt.DevMode & 0x08) != 0);
                    break;
            }
     
        }

        private bool Is_AllValue()
        {
            //IO 설정은 전체 설정값이 한번에 내려가는 형태이므로
            //값이 지정된 것이 없거나 해서는 안된다.
            bool Result = true;

            NullToNoUse();

            if (cb_Ehtercat_1.SelectedIndex < 0) Result = false;
            if (cb_Ehtercat_2.SelectedIndex < 0) Result = false;
            if (cb_Ehtercat_3.SelectedIndex < 0) Result = false;
            if (cb_Ehtercat_4.SelectedIndex < 0) Result = false;
            if (cb_Ehtercat_5.SelectedIndex < 0) Result = false;
            if (cb_Ehtercat_6.SelectedIndex < 0) Result = false;
            if (cb_Ehtercat_7.SelectedIndex < 0) Result = false;
            if (cb_Ehtercat_8.SelectedIndex < 0) Result = false;
            if (cb_Ehtercat_9.SelectedIndex < 0) Result = false;
            if (cb_Ehtercat_10.SelectedIndex < 0) Result = false;

            for (byte i = 0; i < MAX_DI_COUNT_1; i++)
            {
                if (lv_DI.Items[i].SubItems[1].Text == "") Result = false;
                if (lv_DI.Items[i].SubItems[2].Text == "") Result = false;
                if (lv_DI.Items[i].SubItems[3].Text == "") Result = false;
                if (lv_DI.Items[i].SubItems[4].Text == "") Result = false;
                if (lv_DI.Items[i].SubItems[5].Text == "") Result = false;

                if (lv_DI.Items[i].SubItems[1].Text != "신호없음")
                {
                    if (lv_DI.Items[i].SubItems[2].Text == "없음") Result = false;
                }
            }

            if (MAX_DI_COUNT_2 > 0)
            {
                for (byte i = 0; i < MAX_DI_COUNT_2; i++)
                {
                    if (lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[1].Text == "") Result = false;
                    if (lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[2].Text == "") Result = false;
                    if (lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[3].Text == "") Result = false;
                    if (lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[4].Text == "") Result = false;
                    if (lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[5].Text == "") Result = false;

                    if (lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[1].Text != "신호없음")
                    {
                        if (lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[2].Text == "없음") Result = false;
                    }
                }
            }

            for (byte i = 0; i < MAX_DO_COUNT_1; i++)
            {
                if (lv_DO.Items[i].SubItems[1].Text == "") Result = false;
                if (lv_DO.Items[i].SubItems[2].Text == "") Result = false;
                if (lv_DO.Items[i].SubItems[3].Text == "") Result = false;

                if (lv_DO.Items[i].SubItems[1].Text != "신호없음")
                {
                    if (lv_DO.Items[i].SubItems[2].Text == "없음") Result = false;
                }

            }

            if (MAX_DO_COUNT_2 > 0)
            {
                for (byte i = 0; i < MAX_DO_COUNT_2; i++)
                {
                    if (lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[1].Text == "") Result = false;
                    if (lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[2].Text == "") Result = false;
                    if (lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[3].Text == "") Result = false;

                    if (lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[1].Text != "신호없음")
                    {
                        if (lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[2].Text == "없음") Result = false;
                    }

                }
            }

            return Result;
        }

        private void NullToNoUse()
        {

            int Loop; 
            for (Loop = 0; Loop < lv_DO.Items.Count; Loop++)
            {
                if (lv_DO.Items[Loop].SubItems[1].Text == "")
                {
                    lv_DO.Items[Loop].SubItems[1].Text = "신호없음";
                    lv_DO.Items[Loop].SubItems[2].Text = "없음";
                    lv_DO.Items[Loop].SubItems[3].Text = "A";
                }
            }

            for (Loop = 0; Loop < lv_DI.Items.Count; Loop++)
            {
                if (lv_DI.Items[Loop].SubItems[1].Text == "")
                {
                    lv_DI.Items[Loop].SubItems[1].Text = "신호없음";
                    lv_DI.Items[Loop].SubItems[2].Text = "없음";
                    lv_DI.Items[Loop].SubItems[3].Text = "A";
                    lv_DI.Items[Loop].SubItems[4].Text = "100";

                    if (Loop < MAX_DI_COUNT_1)
                    {
                        if (ConstClass.SRM_DI_Names_1[Loop, 3] == "0")
                        {
                            lv_DI.Items[Loop].SubItems[5].Text = "단독";
                        }
                        else
                        {
                            lv_DI.Items[Loop].SubItems[5].Text = "OR";
                        }
                    } else
                    {
                        if (ConstClass.SRM_DI_Names_2[Loop - MAX_DI_COUNT_1, 3] == "0")
                        {
                            lv_DI.Items[Loop].SubItems[5].Text = "단독";
                        }
                        else
                        {
                            lv_DI.Items[Loop].SubItems[5].Text = "OR";
                        }
                    }

                }
            }
        }


        private bool checkCtrlPin()
        {
            int Loop;
            int Tmpint;

            lv_DO.SelectedItems.Clear();
            for (Loop = 0; Loop < lv_DO.Items.Count; Loop++)
            {
                if (lv_DO.Items[Loop].SubItems[1].Text != "신호없음")
                {
                    if ((lv_DO.Items[Loop].SubItems[2].Text == "없음") || (lv_DO.Items[Loop].SubItems[2].Text == ""))
                    {
                        lv_DO.Items[Loop].Selected = true;
                        lv_DO.Focus();
                        return false;
                    }
                }
                
                if (lv_DO.Items[Loop].SubItems[1].Text == "MCU")
                {
                    Tmpint = (int)Global_Class.UTIL_StrToIntDef(lv_DO.Items[Loop].SubItems[2].Text, -1);
                    if ((Tmpint == -1) || (Tmpint > 7))
                    {
                        lv_DO.Items[Loop].Selected = true;
                        lv_DO.Focus();
                        return false;
                    }
                }
            }


            lv_DI.SelectedItems.Clear();
            for (Loop = 0; Loop < lv_DI.Items.Count; Loop++)
            {
                if (lv_DI.Items[Loop].SubItems[1].Text != "신호없음")
                {
                    if ((lv_DI.Items[Loop].SubItems[2].Text == "없음") || (lv_DI.Items[Loop].SubItems[2].Text == ""))
                    {
                        lv_DI.Items[Loop].Selected = true;
                        lv_DI.Focus();
                        return false;
                    }

                    if (lv_DI.Items[Loop].SubItems[1].Text == "MCU")
                    {
                        Tmpint = (int)Global_Class.UTIL_StrToIntDef(lv_DI.Items[Loop].SubItems[2].Text, -1);
                        if ((Tmpint == -1) || (Tmpint > 7))
                        {
                            lv_DI.Items[Loop].Selected = true;
                            lv_DI.Focus();
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private unsafe void Do_Ctrl(bool isFileSave)
        {
            //
            //fixed (byte* DevCtrl = &CtrlREC.Reserved1)
            //{
            //      Global_Class.UTIL_Byteptr_clear(DevCtrl, Marshal.SizeOf(typeof(VEXI_DEFS.DEV_IOConfig)));
            //}

            switch (form_Main.COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    switch (cb_Ehtercat_1.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.SRMIO.EthercatBoard[0] = (byte)cb_Ehtercat_1.SelectedIndex; break;
                        case 11: CtrlREC.SRMIO.EthercatBoard[0] = 50; break;
                        case 12: CtrlREC.SRMIO.EthercatBoard[0] = 255; break;
                    }

                    switch (cb_Ehtercat_2.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.SRMIO.EthercatBoard[1] = (byte)cb_Ehtercat_2.SelectedIndex; break;
                        case 11: CtrlREC.SRMIO.EthercatBoard[1] = 50; break;
                        case 12: CtrlREC.SRMIO.EthercatBoard[1] = 255; break;
                    }

                    switch (cb_Ehtercat_3.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.SRMIO.EthercatBoard[2] = (byte)cb_Ehtercat_3.SelectedIndex; break;
                        case 11: CtrlREC.SRMIO.EthercatBoard[2] = 50; break;
                        case 12: CtrlREC.SRMIO.EthercatBoard[2] = 255; break;
                    }

                    switch (cb_Ehtercat_4.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.SRMIO.EthercatBoard[3] = (byte)cb_Ehtercat_4.SelectedIndex; break;
                        case 11: CtrlREC.SRMIO.EthercatBoard[3] = 50; break;
                        case 12: CtrlREC.SRMIO.EthercatBoard[3] = 255; break;
                    }

                    switch (cb_Ehtercat_5.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.SRMIO.EthercatBoard[4] = (byte)cb_Ehtercat_5.SelectedIndex; break;
                        case 11: CtrlREC.SRMIO.EthercatBoard[4] = 50; break;
                        case 12: CtrlREC.SRMIO.EthercatBoard[4] = 255; break;
                    }

                    switch (cb_Ehtercat_6.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.SRMIO.EthercatBoard[5] = (byte)cb_Ehtercat_6.SelectedIndex; break;
                        case 11: CtrlREC.SRMIO.EthercatBoard[5] = 50; break;
                        case 12: CtrlREC.SRMIO.EthercatBoard[5] = 255; break;
                    }

                    switch (cb_Ehtercat_7.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.SRMIO.EthercatBoard[6] = (byte)cb_Ehtercat_7.SelectedIndex; break;
                        case 11: CtrlREC.SRMIO.EthercatBoard[6] = 50; break;
                        case 12: CtrlREC.SRMIO.EthercatBoard[6] = 255; break;
                    }

                    switch (cb_Ehtercat_8.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.SRMIO.EthercatBoard[7] = (byte)cb_Ehtercat_8.SelectedIndex; break;
                        case 11: CtrlREC.SRMIO.EthercatBoard[7] = 50; break;
                        case 12: CtrlREC.SRMIO.EthercatBoard[7] = 255; break;
                    }

                    switch (cb_Ehtercat_9.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.SRMIO.EthercatBoard[8] = (byte)cb_Ehtercat_9.SelectedIndex; break;
                        case 11: CtrlREC.SRMIO.EthercatBoard[8] = 50; break;
                        case 12: CtrlREC.SRMIO.EthercatBoard[8] = 255; break;
                    }

                    switch (cb_Ehtercat_10.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.SRMIO.EthercatBoard[9] = (byte)cb_Ehtercat_10.SelectedIndex; break;
                        case 11: CtrlREC.SRMIO.EthercatBoard[9] = 50; break;
                        case 12: CtrlREC.SRMIO.EthercatBoard[9] = 255; break;
                    }


                    fixed (VEXI_DEFS.REC_DIConfig* DICOnfigPtr = &CtrlREC.SRMIO.DIConfig_1)
                    {
                        for (byte i = 0; i < MAX_DI_COUNT_1; i++)
                        {
                            if (lv_DI.Items[i].SubItems[1].Text == "MCU") (DICOnfigPtr + i)->EthercatID = 0;
                            else if (lv_DI.Items[i].SubItems[1].Text == "신호없음") (DICOnfigPtr + i)->EthercatID = 255;
                            else (DICOnfigPtr + i)->EthercatID = (byte)Global_Class.UTIL_StrToIntDef(lv_DI.Items[i].SubItems[1].Text, 1);

                            if ((DICOnfigPtr + i)->EthercatID != 255)
                            {
                                (DICOnfigPtr + i)->Pin = (byte)Global_Class.UTIL_StrToIntDef(lv_DI.Items[i].SubItems[2].Text, 0);
                                if (lv_DI.Items[i].SubItems[3].Text == "B") (DICOnfigPtr + i)->Type = 1;
                                else (DICOnfigPtr + i)->Type = 0;
                                (DICOnfigPtr + i)->Chattering = (byte)Global_Class.UTIL_StrToIntDef(lv_DI.Items[i].SubItems[4].Text, 0);

                                if (ConstClass.SRM_DI_Names_1[i, 3] == "0") (DICOnfigPtr + i)->Dual = 0;
                                else
                                {
                                    if (lv_DI.Items[i].SubItems[5].Text == "OR") (DICOnfigPtr + i)->Dual = 1;
                                    else (DICOnfigPtr + i)->Dual = 2;
                                }
                            }
                            else
                            {
                                (DICOnfigPtr + i)->Pin = 0;
                                (DICOnfigPtr + i)->Type = 0;
                                (DICOnfigPtr + i)->Chattering = 0;
                                if (ConstClass.SRM_DI_Names_1[i, 3] == "0") (DICOnfigPtr + i)->Dual = 0;
                                else (DICOnfigPtr + i)->Dual = 1;
                            }
                        }
                    }

                    fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &CtrlREC.SRMIO.DOConfig_1)
                    {
                        for (byte i = 0; i < MAX_DO_COUNT_1; i++)
                        {
                            if (lv_DO.Items[i].SubItems[1].Text == "MCU") (DOCOnfigPtr + i)->EthercatID = 0;
                            else if (lv_DO.Items[i].SubItems[1].Text == "신호없음") (DOCOnfigPtr + i)->EthercatID = 255;
                            else (DOCOnfigPtr + i)->EthercatID = (byte)Global_Class.UTIL_StrToIntDef(lv_DO.Items[i].SubItems[1].Text, 1);

                            if ((DOCOnfigPtr + i)->EthercatID != 255)
                            {
                                (DOCOnfigPtr + i)->Pin = (byte)Global_Class.UTIL_StrToIntDef(lv_DO.Items[i].SubItems[2].Text, 0);
                                if (lv_DO.Items[i].SubItems[3].Text == "B") (DOCOnfigPtr + i)->Type = 1;
                                else (DOCOnfigPtr + i)->Type = 0;
                            }
                            else
                            {
                                (DOCOnfigPtr + i)->Pin = 0;
                                (DOCOnfigPtr + i)->Type = 0;
                            }
                        }
                    }

                    fixed (VEXI_DEFS.REC_DIConfig* DICOnfigPtr = &CtrlREC.SRMIO.DIConfig_123)
                    {
                        for (byte i = 0; i < MAX_DI_COUNT_2; i++)
                        {
                            if (lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[1].Text == "MCU") (DICOnfigPtr + i)->EthercatID = 0;
                            else if (lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[1].Text == "신호없음") (DICOnfigPtr + i)->EthercatID = 255;
                            else (DICOnfigPtr + i)->EthercatID = (byte)Global_Class.UTIL_StrToIntDef(lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[1].Text, 1);

                            if ((DICOnfigPtr + i)->EthercatID != 255)
                            {
                                (DICOnfigPtr + i)->Pin = (byte)Global_Class.UTIL_StrToIntDef(lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[2].Text, 0);
                                if (lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[3].Text == "B") (DICOnfigPtr + i)->Type = 1;
                                else (DICOnfigPtr + i)->Type = 0;
                                (DICOnfigPtr + i)->Chattering = (byte)Global_Class.UTIL_StrToIntDef(lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[4].Text, 0);

                                if (ConstClass.SRM_DI_Names_2[i, 3] == "0") (DICOnfigPtr + i)->Dual = 0;
                                else
                                {
                                    if (lv_DI.Items[MAX_DI_COUNT_1 + i].SubItems[5].Text == "OR") (DICOnfigPtr + i)->Dual = 1;
                                    else (DICOnfigPtr + i)->Dual = 2;
                                }
                            }
                            else
                            {
                                (DICOnfigPtr + i)->Pin = 0;
                                (DICOnfigPtr + i)->Type = 0;
                                (DICOnfigPtr + i)->Chattering = 0;
                                if (ConstClass.SRM_DI_Names_2[i, 3] == "0") (DICOnfigPtr + i)->Dual = 0;
                                else (DICOnfigPtr + i)->Dual = 1;
                            }
                        }
                    }

                    fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &CtrlREC.SRMIO.DOConfig_44)
                    {
                        for (byte i = 0; i < MAX_DO_COUNT_2; i++)
                        {
                            if (lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[1].Text == "MCU") (DOCOnfigPtr + i)->EthercatID = 0;
                            else if (lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[1].Text == "신호없음") (DOCOnfigPtr + i)->EthercatID = 255;
                            else (DOCOnfigPtr + i)->EthercatID = (byte)Global_Class.UTIL_StrToIntDef(lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[1].Text, 1);

                            if ((DOCOnfigPtr + i)->EthercatID != 255)
                            {
                                (DOCOnfigPtr + i)->Pin = (byte)Global_Class.UTIL_StrToIntDef(lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[2].Text, 0);
                                if (lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[3].Text == "B") (DOCOnfigPtr + i)->Type = 1;
                                else (DOCOnfigPtr + i)->Type = 0;
                            }
                            else
                            {
                                (DOCOnfigPtr + i)->Pin = 0;
                                (DOCOnfigPtr + i)->Type = 0;
                            }
                        }
                    }
                    break;
                case ConstClass.TYPE_RTV:
                    switch (cb_Ehtercat_1.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.RTVIO.EthercatBoard[0] = (byte)cb_Ehtercat_1.SelectedIndex; break;
                        case 11: CtrlREC.RTVIO.EthercatBoard[0] = 50; break;
                        case 12: CtrlREC.RTVIO.EthercatBoard[0] = 255; break;
                    }

                    switch (cb_Ehtercat_2.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.RTVIO.EthercatBoard[1] = (byte)cb_Ehtercat_2.SelectedIndex; break;
                        case 11: CtrlREC.RTVIO.EthercatBoard[1] = 50; break;
                        case 12: CtrlREC.RTVIO.EthercatBoard[1] = 255; break;
                    }

                    switch (cb_Ehtercat_3.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.RTVIO.EthercatBoard[2] = (byte)cb_Ehtercat_3.SelectedIndex; break;
                        case 11: CtrlREC.RTVIO.EthercatBoard[2] = 50; break;
                        case 12: CtrlREC.RTVIO.EthercatBoard[2] = 255; break;
                    }

                    switch (cb_Ehtercat_4.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.RTVIO.EthercatBoard[3] = (byte)cb_Ehtercat_4.SelectedIndex; break;
                        case 11: CtrlREC.RTVIO.EthercatBoard[3] = 50; break;
                        case 12: CtrlREC.RTVIO.EthercatBoard[3] = 255; break;
                    }

                    switch (cb_Ehtercat_5.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.RTVIO.EthercatBoard[4] = (byte)cb_Ehtercat_5.SelectedIndex; break;
                        case 11: CtrlREC.RTVIO.EthercatBoard[4] = 50; break;
                        case 12: CtrlREC.RTVIO.EthercatBoard[4] = 255; break;
                    }

                    switch (cb_Ehtercat_6.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.RTVIO.EthercatBoard[5] = (byte)cb_Ehtercat_6.SelectedIndex; break;
                        case 11: CtrlREC.RTVIO.EthercatBoard[5] = 50; break;
                        case 12: CtrlREC.RTVIO.EthercatBoard[5] = 255; break;
                    }

                    switch (cb_Ehtercat_7.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.RTVIO.EthercatBoard[6] = (byte)cb_Ehtercat_7.SelectedIndex; break;
                        case 11: CtrlREC.RTVIO.EthercatBoard[6] = 50; break;
                        case 12: CtrlREC.RTVIO.EthercatBoard[6] = 255; break;
                    }

                    switch (cb_Ehtercat_8.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.RTVIO.EthercatBoard[7] = (byte)cb_Ehtercat_8.SelectedIndex; break;
                        case 11: CtrlREC.RTVIO.EthercatBoard[7] = 50; break;
                        case 12: CtrlREC.RTVIO.EthercatBoard[7] = 255; break;
                    }

                    switch (cb_Ehtercat_9.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.RTVIO.EthercatBoard[8] = (byte)cb_Ehtercat_9.SelectedIndex; break;
                        case 11: CtrlREC.RTVIO.EthercatBoard[8] = 50; break;
                        case 12: CtrlREC.RTVIO.EthercatBoard[8] = 255; break;
                    }

                    switch (cb_Ehtercat_10.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.RTVIO.EthercatBoard[9] = (byte)cb_Ehtercat_10.SelectedIndex; break;
                        case 11: CtrlREC.RTVIO.EthercatBoard[9] = 50; break;
                        case 12: CtrlREC.RTVIO.EthercatBoard[9] = 255; break;
                    }


                    fixed (VEXI_DEFS.REC_DIConfig* DICOnfigPtr = &CtrlREC.RTVIO.DIConfig_1)
                    {
                        for (byte i = 0; i < MAX_DI_COUNT_1; i++)
                        {
                            if (lv_DI.Items[i].SubItems[1].Text == "MCU") (DICOnfigPtr + i)->EthercatID = 0;
                            else if (lv_DI.Items[i].SubItems[1].Text == "신호없음") (DICOnfigPtr + i)->EthercatID = 255;
                            else (DICOnfigPtr + i)->EthercatID = (byte)Global_Class.UTIL_StrToIntDef(lv_DI.Items[i].SubItems[1].Text, 1);

                            if ((DICOnfigPtr + i)->EthercatID != 255)
                            {
                                (DICOnfigPtr + i)->Pin = (byte)Global_Class.UTIL_StrToIntDef(lv_DI.Items[i].SubItems[2].Text, 0);
                                if (lv_DI.Items[i].SubItems[3].Text == "B") (DICOnfigPtr + i)->Type = 1;
                                else (DICOnfigPtr + i)->Type = 0;
                                (DICOnfigPtr + i)->Chattering = (byte)Global_Class.UTIL_StrToIntDef(lv_DI.Items[i].SubItems[4].Text, 0);

                                if (ConstClass.RTV_DI_Names[i, 3] == "0") (DICOnfigPtr + i)->Dual = 0;
                                else
                                {
                                    if (lv_DI.Items[i].SubItems[5].Text == "OR") (DICOnfigPtr + i)->Dual = 1;
                                    else (DICOnfigPtr + i)->Dual = 2;
                                }
                            }
                            else
                            {
                                (DICOnfigPtr + i)->Pin = 0;
                                (DICOnfigPtr + i)->Type = 0;
                                (DICOnfigPtr + i)->Chattering = 0;
                                if (ConstClass.RTV_DI_Names[i, 3] == "0") (DICOnfigPtr + i)->Dual = 0;
                                else (DICOnfigPtr + i)->Dual = 1;
                            }
                        }
                    }


                    fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &CtrlREC.RTVIO.DOConfig_1)
                    {
                        for (byte i = 0; i < MAX_DO_COUNT_1; i++)
                        {
                            if (lv_DO.Items[i].SubItems[1].Text == "MCU") (DOCOnfigPtr + i)->EthercatID = 0;
                            else if (lv_DO.Items[i].SubItems[1].Text == "신호없음") (DOCOnfigPtr + i)->EthercatID = 255;
                            else (DOCOnfigPtr + i)->EthercatID = (byte)Global_Class.UTIL_StrToIntDef(lv_DO.Items[i].SubItems[1].Text, 1);

                            if ((DOCOnfigPtr + i)->EthercatID != 255)
                            {
                                (DOCOnfigPtr + i)->Pin = (byte)Global_Class.UTIL_StrToIntDef(lv_DO.Items[i].SubItems[2].Text, 0);
                                if (lv_DO.Items[i].SubItems[3].Text == "B") (DOCOnfigPtr + i)->Type = 1;
                                else (DOCOnfigPtr + i)->Type = 0;
                            }
                            else
                            {
                                (DOCOnfigPtr + i)->Pin = 0;
                                (DOCOnfigPtr + i)->Type = 0;
                            }
                        }
                    }

                    fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &CtrlREC.RTVIO.DOConfig_64)
                    {
                        for (byte i = 0; i < MAX_DO_COUNT_2; i++)
                        {
                            if (lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[1].Text == "MCU") (DOCOnfigPtr + i)->EthercatID = 0;
                            else if (lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[1].Text == "신호없음") (DOCOnfigPtr + i)->EthercatID = 255;
                            else (DOCOnfigPtr + i)->EthercatID = (byte)Global_Class.UTIL_StrToIntDef(lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[1].Text, 1);

                            if ((DOCOnfigPtr + i)->EthercatID != 255)
                            {
                                (DOCOnfigPtr + i)->Pin = (byte)Global_Class.UTIL_StrToIntDef(lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[2].Text, 0);
                                if (lv_DO.Items[MAX_DO_COUNT_1 + i].SubItems[3].Text == "B") (DOCOnfigPtr + i)->Type = 1;
                                else (DOCOnfigPtr + i)->Type = 0;
                            }
                            else
                            {
                                (DOCOnfigPtr + i)->Pin = 0;
                                (DOCOnfigPtr + i)->Type = 0;
                            }
                        }
                    }

                    break;
                case ConstClass.TYPE_EMS:
                    switch (cb_Ehtercat_1.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.EMSIO.EthercatBoard[0] = (byte)cb_Ehtercat_1.SelectedIndex; break;
                        case 11: CtrlREC.EMSIO.EthercatBoard[0] = 50; break;
                        case 12: CtrlREC.EMSIO.EthercatBoard[0] = 255; break;
                    }

                    switch (cb_Ehtercat_2.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.EMSIO.EthercatBoard[1] = (byte)cb_Ehtercat_2.SelectedIndex; break;
                        case 11: CtrlREC.EMSIO.EthercatBoard[1] = 50; break;
                        case 12: CtrlREC.EMSIO.EthercatBoard[1] = 255; break;
                    }

                    switch (cb_Ehtercat_3.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.EMSIO.EthercatBoard[2] = (byte)cb_Ehtercat_3.SelectedIndex; break;
                        case 11: CtrlREC.EMSIO.EthercatBoard[2] = 50; break;
                        case 12: CtrlREC.EMSIO.EthercatBoard[2] = 255; break;
                    }

                    switch (cb_Ehtercat_4.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.EMSIO.EthercatBoard[3] = (byte)cb_Ehtercat_4.SelectedIndex; break;
                        case 11: CtrlREC.EMSIO.EthercatBoard[3] = 50; break;
                        case 12: CtrlREC.EMSIO.EthercatBoard[3] = 255; break;
                    }

                    switch (cb_Ehtercat_5.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.EMSIO.EthercatBoard[4] = (byte)cb_Ehtercat_5.SelectedIndex; break;
                        case 11: CtrlREC.EMSIO.EthercatBoard[4] = 50; break;
                        case 12: CtrlREC.EMSIO.EthercatBoard[4] = 255; break;
                    }

                    switch (cb_Ehtercat_6.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.EMSIO.EthercatBoard[5] = (byte)cb_Ehtercat_6.SelectedIndex; break;
                        case 11: CtrlREC.EMSIO.EthercatBoard[5] = 50; break;
                        case 12: CtrlREC.EMSIO.EthercatBoard[5] = 255; break;
                    }

                    switch (cb_Ehtercat_7.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.EMSIO.EthercatBoard[6] = (byte)cb_Ehtercat_7.SelectedIndex; break;
                        case 11: CtrlREC.EMSIO.EthercatBoard[6] = 50; break;
                        case 12: CtrlREC.EMSIO.EthercatBoard[6] = 255; break;
                    }

                    switch (cb_Ehtercat_8.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.EMSIO.EthercatBoard[7] = (byte)cb_Ehtercat_8.SelectedIndex; break;
                        case 11: CtrlREC.EMSIO.EthercatBoard[7] = 50; break;
                        case 12: CtrlREC.EMSIO.EthercatBoard[7] = 255; break;
                    }

                    switch (cb_Ehtercat_9.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.EMSIO.EthercatBoard[8] = (byte)cb_Ehtercat_9.SelectedIndex; break;
                        case 11: CtrlREC.EMSIO.EthercatBoard[8] = 50; break;
                        case 12: CtrlREC.EMSIO.EthercatBoard[8] = 255; break;
                    }

                    switch (cb_Ehtercat_10.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: CtrlREC.EMSIO.EthercatBoard[9] = (byte)cb_Ehtercat_10.SelectedIndex; break;
                        case 11: CtrlREC.EMSIO.EthercatBoard[9] = 50; break;
                        case 12: CtrlREC.EMSIO.EthercatBoard[9] = 255; break;
                    }


                    fixed (VEXI_DEFS.REC_DIConfig* DICOnfigPtr = &CtrlREC.EMSIO.DIConfig)
                    {
                        for (byte i = 0; i < MAX_DI_COUNT_1; i++)
                        {
                            if (lv_DI.Items[i].SubItems[1].Text == "MCU") (DICOnfigPtr + i)->EthercatID = 0;
                            else if (lv_DI.Items[i].SubItems[1].Text == "신호없음") (DICOnfigPtr + i)->EthercatID = 255;
                            else (DICOnfigPtr + i)->EthercatID = (byte)Global_Class.UTIL_StrToIntDef(lv_DI.Items[i].SubItems[1].Text, 1);

                            if ((DICOnfigPtr + i)->EthercatID != 255)
                            {
                                (DICOnfigPtr + i)->Pin = (byte)Global_Class.UTIL_StrToIntDef(lv_DI.Items[i].SubItems[2].Text, 0);
                                if (lv_DI.Items[i].SubItems[3].Text == "B") (DICOnfigPtr + i)->Type = 1;
                                else (DICOnfigPtr + i)->Type = 0;
                                (DICOnfigPtr + i)->Chattering = (byte)Global_Class.UTIL_StrToIntDef(lv_DI.Items[i].SubItems[4].Text, 0);

                                if (ConstClass.EMS_DI_Names[i, 3] == "0") (DICOnfigPtr + i)->Dual = 0;
                                else
                                {
                                    if (lv_DI.Items[i].SubItems[5].Text == "OR") (DICOnfigPtr + i)->Dual = 1;
                                    else (DICOnfigPtr + i)->Dual = 2;
                                }
                            }
                            else
                            {
                                (DICOnfigPtr + i)->Pin = 0;
                                (DICOnfigPtr + i)->Type = 0;
                                (DICOnfigPtr + i)->Chattering = 0;
                                if (ConstClass.EMS_DI_Names[i, 3] == "0") (DICOnfigPtr + i)->Dual = 0;
                                else (DICOnfigPtr + i)->Dual = 1;
                            }
                        }
                    }


                    fixed (VEXI_DEFS.REC_DOConfig* DOCOnfigPtr = &CtrlREC.EMSIO.DOConfig)
                    {
                        for (byte i = 0; i < MAX_DO_COUNT_1; i++)
                        {
                            if (lv_DO.Items[i].SubItems[1].Text == "MCU") (DOCOnfigPtr + i)->EthercatID = 0;
                            else if (lv_DO.Items[i].SubItems[1].Text == "신호없음") (DOCOnfigPtr + i)->EthercatID = 255;
                            else (DOCOnfigPtr + i)->EthercatID = (byte)Global_Class.UTIL_StrToIntDef(lv_DO.Items[i].SubItems[1].Text, 1);

                            if ((DOCOnfigPtr + i)->EthercatID != 255)
                            {
                                (DOCOnfigPtr + i)->Pin = (byte)Global_Class.UTIL_StrToIntDef(lv_DO.Items[i].SubItems[2].Text, 0);
                                if (lv_DO.Items[i].SubItems[3].Text == "B") (DOCOnfigPtr + i)->Type = 1;
                                else (DOCOnfigPtr + i)->Type = 0;
                            }
                            else
                            {
                                (DOCOnfigPtr + i)->Pin = 0;
                                (DOCOnfigPtr + i)->Type = 0;
                            }
                        }
                    }

                    break;
                default:
                    break;
            }

            if (form_Main.COMMDataManager.RX_DestDevType == ConstClass.TYPE_SRM)
            {
                //IN 135~149, OUT 44~69 추가전  (IN 128, OUT 37)
                if (!IsSRM_New)
                {
                    Process_SRMCtrl_OLDIO();
                }
            }


            if (!isFileSave)
            {
                form_Main.COMMDataManager.ADD_TxUserData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_24, CtrlREC);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();
            Init_DI();
            Init_DO();
            form_Main.COMMDataManager.ADD_TxUserZeroData(ConstClass.TYPE_02, 0x00, ConstClass.CMD1_01, ConstClass.CMD2_23, (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.TDEV_REC_IOConfigReq)));

        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();
            if (Is_AllValue())
            {
                if (form_Main.GlobalObj.MsgBox_Confirm_OKCancel(this, "I/O 설정을 장치에 다운로드 하시겠습니까?"))
                {
                    IsControl = true;

                    Do_Ctrl(false);
                }
            } else
            {
                form_Main.GlobalObj.MsgBox_Info("값이 설정되지 않은 항목이 있습니다.", "W");
            }

        }

        private unsafe void button1_Click(object sender, EventArgs e)
        {
            
        }



        private unsafe void btnFileSave_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();


            if (!checkCtrlPin())
            {
                MessageBox.Show("Pin 값이 올바르게 설정되지 않았습니다");
                return ;
            }


            Do_Ctrl(true);

            saveFileDialog1.Filter = "*.IO_cfg|*.IO_CFG";

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }


            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (BinaryWriter  br = new BinaryWriter(File.Open(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write)))
                {
                    try
                    {
                        br.Seek(0, SeekOrigin.Begin);
                        byte[] Savebytes = new byte[Marshal.SizeOf(typeof(VEXI_DEFS.DevUnion_IOConfig))];
                        Global_Class.UTIL_StructObjectToByteArray(CtrlREC, Savebytes);
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
            Hide_AllEdit();
            NullToNoUse();

            if (!checkCtrlPin())
            {
                MessageBox.Show("Pin 값이 올바르게 설정되지 않았습니다");
                return;
            }


            Do_Ctrl(true);

            switch (form_Main.COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    saveFileDialog1.Filter = "*.SRMcfg|*.SRMCFG";
                    break;
                case ConstClass.TYPE_RTV:
                    saveFileDialog1.Filter = "*.RTVcfg|*.RTVCFG";
                    break;
                case ConstClass.TYPE_EMS:
                    saveFileDialog1.Filter = "*.EMScfg|*.EMScfg";
                    break;
                default:
                    saveFileDialog1.Filter = "*.RTVcfg|*.RTVcfg";
                    break;
            }

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
                switch (form_Main.COMMDataManager.RX_DestDevType)
                {
                    case ConstClass.TYPE_SRM:
                        form_Main.SRM_ToTalFile.FileName = saveFileDialog1.FileName;
                        form_Main.SRM_ToTalFile.Write_IO_CFG(CtrlREC.SRMIO);
                        break;
                    case ConstClass.TYPE_RTV:
                        form_Main.RTV_ToTalFile.Write_IO_CFG(CtrlREC.RTVIO);
                        break;
                    case ConstClass.TYPE_EMS:
                        form_Main.EMS_ToTalFile.Write_IO_CFG(CtrlREC.EMSIO);
                        break;
                    default:
                        form_Main.RTV_ToTalFile.Write_IO_CFG(CtrlREC.RTVIO);
                        break;
                }
                
            }
        }

        private void btnFileLoad_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            openFileDialog1.Filter = "*.IO_cfg|*.IO_CFG";

            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (BinaryReader br = new BinaryReader(File.Open(openFileDialog1.FileName, FileMode.Open, FileAccess.Read)))
                {
                    try
                    {
                        ushort Len = (ushort)Marshal.SizeOf(typeof(VEXI_DEFS.DevUnion_IOConfig));
                        byte[] Savebytes;
                        if (br.BaseStream.Length == Len)
                        {
                            Savebytes = br.ReadBytes(Len);
                            dev_REC_IOConfig = (VEXI_DEFS.DevUnion_IOConfig)Global_Class.UTIL_BytesToStructure(Savebytes, typeof(VEXI_DEFS.DevUnion_IOConfig));
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

                if (form_Main.COMMDataManager.RX_DestDevType == ConstClass.TYPE_SRM)
                {
                    if (!IsSRM_New)
                    {
                        Process_SRMSt_OLDIO();
                    }
                }
                Display_IOConfig(false, true);
            }
        }
        private void btn_LoadTotalFile_Click(object sender, EventArgs e)
        {
            Hide_AllEdit();

            switch (form_Main.COMMDataManager.RX_DestDevType)
            {
                case ConstClass.TYPE_SRM:
                    openFileDialog1.Filter = "*.SRMcfg|*.SRMCFG";
                    break;
                case ConstClass.TYPE_RTV:
                    openFileDialog1.Filter = "*.RTVcfg|*.RTVCFG";
                    break;
                case ConstClass.TYPE_EMS:
                    openFileDialog1.Filter = "*.EMScfg|*.EMSCFG";
                    break;
                default:
                    openFileDialog1.Filter = "*.RTVcfg|*.RTVCFG";
                    break;
            }
            if (openFileDialog1.FileName == "")
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                switch (form_Main.COMMDataManager.RX_DestDevType)
                {
                    case ConstClass.TYPE_SRM:
                        form_Main.SRM_ToTalFile.FileName = openFileDialog1.FileName;
                        if (form_Main.SRM_ToTalFile.Read_IO_CFG(ref dev_REC_IOConfig.SRMIO))
                        {
                                if (!IsSRM_New)
                                {
                                    Process_SRMSt_OLDIO();
                                }
                            Display_IOConfig(false, true);
                        }
                        else
                        {
                            MessageBox.Show("해당 파일안에 [" + this.Text + "] 데이터가 없습니다");
                        }
                        break;
                    case ConstClass.TYPE_RTV:
                        form_Main.RTV_ToTalFile.FileName = openFileDialog1.FileName;
                        if (form_Main.RTV_ToTalFile.Read_IO_CFG(ref dev_REC_IOConfig.RTVIO))
                        {
                            Display_IOConfig(false, true);
                        }
                        else
                        {
                            MessageBox.Show("해당 파일안에 [" + this.Text + "] 데이터가 없습니다");
                        }
                        break;
                    case ConstClass.TYPE_EMS:
                        form_Main.EMS_ToTalFile.FileName = openFileDialog1.FileName;
                        if (form_Main.EMS_ToTalFile.Read_IO_CFG(ref dev_REC_IOConfig.EMSIO))
                        {
                            Display_IOConfig(false, true);
                        }
                        else
                        {
                            MessageBox.Show("해당 파일안에 [" + this.Text + "] 데이터가 없습니다");
                        }
                        break;
                    default:
                        form_Main.RTV_ToTalFile.FileName = openFileDialog1.FileName;
                        if (form_Main.RTV_ToTalFile.Read_IO_CFG(ref dev_REC_IOConfig.RTVIO))
                        {
                            Display_IOConfig(false, true);
                        }
                        else
                        {
                            MessageBox.Show("해당 파일안에 [" + this.Text + "] 데이터가 없습니다");
                        }
                        break;
                }
                
            }
            else
            {
                MessageBox.Show("파일을 선택하지 않으셨습니다.");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            btnSet.Enabled = true;
        }
    }
}

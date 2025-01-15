
namespace VEXI
{
    partial class Form_SRMSpecialRack
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lv_SpecialRack = new InheritedListView.MyListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAdd_1 = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUIpdate = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_SelectedIndex = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ckItem_0 = new System.Windows.Forms.CheckBox();
            this.ckItem_1 = new System.Windows.Forms.CheckBox();
            this.ckItem_3 = new System.Windows.Forms.CheckBox();
            this.ckItem_2 = new System.Windows.Forms.CheckBox();
            this.ckItem_5 = new System.Windows.Forms.CheckBox();
            this.ckItem_4 = new System.Windows.Forms.CheckBox();
            this.ckItem_7 = new System.Windows.Forms.CheckBox();
            this.ckItem_6 = new System.Windows.Forms.CheckBox();
            this.ckItemNone = new System.Windows.Forms.CheckBox();
            this.cbBay = new System.Windows.Forms.ComboBox();
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.btnAllClear = new System.Windows.Forms.Button();
            this.btn_SpecialRack_FileRead = new System.Windows.Forms.Button();
            this.btn_SpecialRack_FileWrite = new System.Windows.Forms.Button();
            this.btn_SpecialRack_Set = new System.Windows.Forms.Button();
            this.btn_SpecialRack_Load = new System.Windows.Forms.Button();
            this.btnAdd_2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ckRowAll = new System.Windows.Forms.CheckBox();
            this.ckRow_4 = new System.Windows.Forms.CheckBox();
            this.ckRow_3 = new System.Windows.Forms.CheckBox();
            this.ckRow_2 = new System.Windows.Forms.CheckBox();
            this.ckRow_1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_LoadTotalFile = new System.Windows.Forms.Button();
            this.btn_SaveTotalFile = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_SpecialRack
            // 
            this.lv_SpecialRack.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader6,
            this.columnHeader11,
            this.columnHeader4,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader12,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader13});
            this.lv_SpecialRack.FullRowSelect = true;
            this.lv_SpecialRack.GridLines = true;
            this.lv_SpecialRack.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_SpecialRack.HideSelection = false;
            this.lv_SpecialRack.Location = new System.Drawing.Point(16, 158);
            this.lv_SpecialRack.MultiSelect = false;
            this.lv_SpecialRack.Name = "lv_SpecialRack";
            this.lv_SpecialRack.Size = new System.Drawing.Size(1115, 534);
            this.lv_SpecialRack.TabIndex = 273;
            this.lv_SpecialRack.UseCompatibleStateImageBehavior = false;
            this.lv_SpecialRack.View = System.Windows.Forms.View.Details;
            this.lv_SpecialRack.SelectedIndexChanged += new System.EventHandler(this.lv_SpecialRack_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Index";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "BAY";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 84;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "LEVEL";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader11.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ROW1";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 80;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "ROW2";
            this.columnHeader14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader14.Width = 80;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "ROW3";
            this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader15.Width = 80;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "ROW4";
            this.columnHeader16.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader16.Width = 80;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "1종";
            this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "2종";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "3종";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 59;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "4종";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "5종";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "6종";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "7종";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "8종";
            this.columnHeader13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnAdd_1
            // 
            this.btnAdd_1.Location = new System.Drawing.Point(872, 99);
            this.btnAdd_1.Name = "btnAdd_1";
            this.btnAdd_1.Size = new System.Drawing.Size(80, 23);
            this.btnAdd_1.TabIndex = 274;
            this.btnAdd_1.TabStop = false;
            this.btnAdd_1.Text = "추가(앞)";
            this.btnAdd_1.UseVisualStyleBackColor = true;
            this.btnAdd_1.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(958, 128);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 23);
            this.btnDelete.TabIndex = 275;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "삭제";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUIpdate
            // 
            this.btnUIpdate.Location = new System.Drawing.Point(872, 128);
            this.btnUIpdate.Name = "btnUIpdate";
            this.btnUIpdate.Size = new System.Drawing.Size(80, 23);
            this.btnUIpdate.TabIndex = 276;
            this.btnUIpdate.TabStop = false;
            this.btnUIpdate.Text = "수정";
            this.btnUIpdate.UseVisualStyleBackColor = true;
            this.btnUIpdate.Click += new System.EventHandler(this.btnUIpdate_Click);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.SystemColors.Highlight;
            this.label12.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(14, 87);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 24);
            this.label12.TabIndex = 278;
            this.label12.Text = "Index";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_SelectedIndex
            // 
            this.lbl_SelectedIndex.BackColor = System.Drawing.Color.Snow;
            this.lbl_SelectedIndex.ForeColor = System.Drawing.Color.Black;
            this.lbl_SelectedIndex.Location = new System.Drawing.Point(14, 111);
            this.lbl_SelectedIndex.Name = "lbl_SelectedIndex";
            this.lbl_SelectedIndex.Size = new System.Drawing.Size(82, 41);
            this.lbl_SelectedIndex.TabIndex = 279;
            this.lbl_SelectedIndex.Text = "0";
            this.lbl_SelectedIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Highlight;
            this.label3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(102, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 24);
            this.label3.TabIndex = 282;
            this.label3.Text = "BAY";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Highlight;
            this.label4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(180, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 24);
            this.label4.TabIndex = 283;
            this.label4.Text = "LEVEL";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Highlight;
            this.label5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(460, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(388, 24);
            this.label5.TabIndex = 284;
            this.label5.Text = "허용 화물";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ckItem_0
            // 
            this.ckItem_0.AutoSize = true;
            this.ckItem_0.Location = new System.Drawing.Point(463, 136);
            this.ckItem_0.Name = "ckItem_0";
            this.ckItem_0.Size = new System.Drawing.Size(42, 16);
            this.ckItem_0.TabIndex = 285;
            this.ckItem_0.Text = "1종";
            this.ckItem_0.UseVisualStyleBackColor = true;
            this.ckItem_0.Click += new System.EventHandler(this.ckItem_0_Click);
            // 
            // ckItem_1
            // 
            this.ckItem_1.AutoSize = true;
            this.ckItem_1.Location = new System.Drawing.Point(512, 136);
            this.ckItem_1.Name = "ckItem_1";
            this.ckItem_1.Size = new System.Drawing.Size(42, 16);
            this.ckItem_1.TabIndex = 286;
            this.ckItem_1.Text = "2종";
            this.ckItem_1.UseVisualStyleBackColor = true;
            this.ckItem_1.Click += new System.EventHandler(this.ckItem_0_Click);
            // 
            // ckItem_3
            // 
            this.ckItem_3.AutoSize = true;
            this.ckItem_3.Location = new System.Drawing.Point(610, 136);
            this.ckItem_3.Name = "ckItem_3";
            this.ckItem_3.Size = new System.Drawing.Size(42, 16);
            this.ckItem_3.TabIndex = 288;
            this.ckItem_3.Text = "4종";
            this.ckItem_3.UseVisualStyleBackColor = true;
            this.ckItem_3.Click += new System.EventHandler(this.ckItem_0_Click);
            // 
            // ckItem_2
            // 
            this.ckItem_2.AutoSize = true;
            this.ckItem_2.Location = new System.Drawing.Point(561, 136);
            this.ckItem_2.Name = "ckItem_2";
            this.ckItem_2.Size = new System.Drawing.Size(42, 16);
            this.ckItem_2.TabIndex = 287;
            this.ckItem_2.Text = "3종";
            this.ckItem_2.UseVisualStyleBackColor = true;
            this.ckItem_2.Click += new System.EventHandler(this.ckItem_0_Click);
            // 
            // ckItem_5
            // 
            this.ckItem_5.AutoSize = true;
            this.ckItem_5.Location = new System.Drawing.Point(708, 136);
            this.ckItem_5.Name = "ckItem_5";
            this.ckItem_5.Size = new System.Drawing.Size(42, 16);
            this.ckItem_5.TabIndex = 290;
            this.ckItem_5.Text = "6종";
            this.ckItem_5.UseVisualStyleBackColor = true;
            this.ckItem_5.Click += new System.EventHandler(this.ckItem_0_Click);
            // 
            // ckItem_4
            // 
            this.ckItem_4.AutoSize = true;
            this.ckItem_4.Location = new System.Drawing.Point(659, 136);
            this.ckItem_4.Name = "ckItem_4";
            this.ckItem_4.Size = new System.Drawing.Size(42, 16);
            this.ckItem_4.TabIndex = 289;
            this.ckItem_4.Text = "5종";
            this.ckItem_4.UseVisualStyleBackColor = true;
            this.ckItem_4.Click += new System.EventHandler(this.ckItem_0_Click);
            // 
            // ckItem_7
            // 
            this.ckItem_7.AutoSize = true;
            this.ckItem_7.Location = new System.Drawing.Point(806, 136);
            this.ckItem_7.Name = "ckItem_7";
            this.ckItem_7.Size = new System.Drawing.Size(42, 16);
            this.ckItem_7.TabIndex = 292;
            this.ckItem_7.Text = "8종";
            this.ckItem_7.UseVisualStyleBackColor = true;
            this.ckItem_7.Click += new System.EventHandler(this.ckItem_0_Click);
            // 
            // ckItem_6
            // 
            this.ckItem_6.AutoSize = true;
            this.ckItem_6.Location = new System.Drawing.Point(757, 136);
            this.ckItem_6.Name = "ckItem_6";
            this.ckItem_6.Size = new System.Drawing.Size(42, 16);
            this.ckItem_6.TabIndex = 291;
            this.ckItem_6.Text = "7종";
            this.ckItem_6.UseVisualStyleBackColor = true;
            this.ckItem_6.Click += new System.EventHandler(this.ckItem_0_Click);
            // 
            // ckItemNone
            // 
            this.ckItemNone.AutoSize = true;
            this.ckItemNone.Location = new System.Drawing.Point(463, 116);
            this.ckItemNone.Name = "ckItemNone";
            this.ckItemNone.Size = new System.Drawing.Size(96, 16);
            this.ckItemNone.TabIndex = 293;
            this.ckItemNone.Text = "허용화물없음";
            this.ckItemNone.UseVisualStyleBackColor = true;
            this.ckItemNone.Click += new System.EventHandler(this.ckItemNone_Click);
            // 
            // cbBay
            // 
            this.cbBay.FormattingEnabled = true;
            this.cbBay.Items.AddRange(new object[] {
            "ALL",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "105",
            "106",
            "107",
            "108",
            "109",
            "110",
            "111",
            "112",
            "113",
            "114",
            "115",
            "116",
            "117",
            "118",
            "119",
            "120",
            "121",
            "122",
            "123",
            "124",
            "125",
            "126",
            "127",
            "128",
            "129",
            "130",
            "131",
            "132",
            "133",
            "134",
            "135",
            "136",
            "137",
            "138",
            "139",
            "140",
            "141",
            "142",
            "143",
            "144",
            "145",
            "146",
            "147",
            "148",
            "149",
            "150",
            "151",
            "152",
            "153",
            "154",
            "155",
            "156",
            "157",
            "158",
            "159",
            "160",
            "161",
            "162",
            "163",
            "164",
            "165",
            "166",
            "167",
            "168",
            "169",
            "170",
            "171",
            "172",
            "173",
            "174",
            "175",
            "176",
            "177",
            "178",
            "179",
            "180",
            "181",
            "182",
            "183",
            "184",
            "185",
            "186",
            "187",
            "188",
            "189",
            "190",
            "191",
            "192",
            "193",
            "194",
            "195",
            "196",
            "197",
            "198",
            "199",
            "200",
            "201",
            "202",
            "203",
            "204",
            "205",
            "206",
            "207",
            "208",
            "209",
            "210",
            "211",
            "212",
            "213",
            "214",
            "215",
            "216",
            "217",
            "218",
            "219",
            "220",
            "221",
            "222",
            "223",
            "224",
            "225",
            "226",
            "227",
            "228",
            "229",
            "230",
            "231",
            "232",
            "233",
            "234",
            "235",
            "236",
            "237",
            "238",
            "239",
            "240",
            "241",
            "242",
            "243",
            "244",
            "245",
            "246",
            "247",
            "248",
            "249",
            "250",
            "251",
            "252",
            "253",
            "254",
            "255",
            "256"});
            this.cbBay.Location = new System.Drawing.Point(102, 122);
            this.cbBay.Name = "cbBay";
            this.cbBay.Size = new System.Drawing.Size(72, 20);
            this.cbBay.TabIndex = 296;
            // 
            // cbLevel
            // 
            this.cbLevel.FormattingEnabled = true;
            this.cbLevel.Items.AddRange(new object[] {
            "ALL",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "105",
            "106",
            "107",
            "108",
            "109",
            "110",
            "111",
            "112",
            "113",
            "114",
            "115",
            "116",
            "117",
            "118",
            "119",
            "120",
            "121",
            "122",
            "123",
            "124",
            "125",
            "126",
            "127",
            "128"});
            this.cbLevel.Location = new System.Drawing.Point(180, 122);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(72, 20);
            this.cbLevel.TabIndex = 297;
            // 
            // btnAllClear
            // 
            this.btnAllClear.Location = new System.Drawing.Point(1044, 128);
            this.btnAllClear.Name = "btnAllClear";
            this.btnAllClear.Size = new System.Drawing.Size(71, 23);
            this.btnAllClear.TabIndex = 298;
            this.btnAllClear.TabStop = false;
            this.btnAllClear.Text = "전체 삭제";
            this.btnAllClear.UseVisualStyleBackColor = true;
            this.btnAllClear.Click += new System.EventHandler(this.btnAllClear_Click);
            // 
            // btn_SpecialRack_FileRead
            // 
            this.btn_SpecialRack_FileRead.Location = new System.Drawing.Point(1082, 2);
            this.btn_SpecialRack_FileRead.Name = "btn_SpecialRack_FileRead";
            this.btn_SpecialRack_FileRead.Size = new System.Drawing.Size(56, 42);
            this.btn_SpecialRack_FileRead.TabIndex = 661;
            this.btn_SpecialRack_FileRead.Text = "파일에서 불러오기";
            this.btn_SpecialRack_FileRead.UseVisualStyleBackColor = true;
            this.btn_SpecialRack_FileRead.Visible = false;
            this.btn_SpecialRack_FileRead.Click += new System.EventHandler(this.btn_SpecialRack_FileRead_Click);
            // 
            // btn_SpecialRack_FileWrite
            // 
            this.btn_SpecialRack_FileWrite.Location = new System.Drawing.Point(1082, 50);
            this.btn_SpecialRack_FileWrite.Name = "btn_SpecialRack_FileWrite";
            this.btn_SpecialRack_FileWrite.Size = new System.Drawing.Size(49, 42);
            this.btn_SpecialRack_FileWrite.TabIndex = 660;
            this.btn_SpecialRack_FileWrite.Text = "파일로 저장";
            this.btn_SpecialRack_FileWrite.UseVisualStyleBackColor = true;
            this.btn_SpecialRack_FileWrite.Visible = false;
            this.btn_SpecialRack_FileWrite.Click += new System.EventHandler(this.btn_SpecialRack_FileWrite_Click);
            // 
            // btn_SpecialRack_Set
            // 
            this.btn_SpecialRack_Set.Location = new System.Drawing.Point(222, 16);
            this.btn_SpecialRack_Set.Name = "btn_SpecialRack_Set";
            this.btn_SpecialRack_Set.Size = new System.Drawing.Size(200, 42);
            this.btn_SpecialRack_Set.TabIndex = 659;
            this.btn_SpecialRack_Set.TabStop = false;
            this.btn_SpecialRack_Set.Text = "스페셜 랙 설정 변경 (장치 적용)";
            this.btn_SpecialRack_Set.UseVisualStyleBackColor = true;
            this.btn_SpecialRack_Set.Click += new System.EventHandler(this.btn_SpecialRack_Set_Click);
            // 
            // btn_SpecialRack_Load
            // 
            this.btn_SpecialRack_Load.Location = new System.Drawing.Point(16, 16);
            this.btn_SpecialRack_Load.Name = "btn_SpecialRack_Load";
            this.btn_SpecialRack_Load.Size = new System.Drawing.Size(200, 42);
            this.btn_SpecialRack_Load.TabIndex = 658;
            this.btn_SpecialRack_Load.TabStop = false;
            this.btn_SpecialRack_Load.Text = "장치로부터 스페셜 랙 설정 읽기";
            this.btn_SpecialRack_Load.UseVisualStyleBackColor = true;
            this.btn_SpecialRack_Load.Click += new System.EventHandler(this.btn_SpecialRack_Load_Click);
            // 
            // btnAdd_2
            // 
            this.btnAdd_2.Location = new System.Drawing.Point(958, 99);
            this.btnAdd_2.Name = "btnAdd_2";
            this.btnAdd_2.Size = new System.Drawing.Size(80, 23);
            this.btnAdd_2.TabIndex = 662;
            this.btnAdd_2.TabStop = false;
            this.btnAdd_2.Text = "추가(뒤)";
            this.btnAdd_2.UseVisualStyleBackColor = true;
            this.btnAdd_2.Click += new System.EventHandler(this.btnAdd_2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1137, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 23);
            this.button1.TabIndex = 663;
            this.button1.TabStop = false;
            this.button1.Text = "UP";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1137, 224);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(58, 23);
            this.button2.TabIndex = 664;
            this.button2.TabStop = false;
            this.button2.Text = "Down";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ckRowAll
            // 
            this.ckRowAll.AutoSize = true;
            this.ckRowAll.Location = new System.Drawing.Point(260, 124);
            this.ckRowAll.Name = "ckRowAll";
            this.ckRowAll.Size = new System.Drawing.Size(46, 16);
            this.ckRowAll.TabIndex = 672;
            this.ckRowAll.Text = "ALL";
            this.ckRowAll.UseVisualStyleBackColor = true;
            this.ckRowAll.Click += new System.EventHandler(this.ckRowAll_Click);
            // 
            // ckRow_4
            // 
            this.ckRow_4.AutoSize = true;
            this.ckRow_4.Location = new System.Drawing.Point(423, 124);
            this.ckRow_4.Name = "ckRow_4";
            this.ckRow_4.Size = new System.Drawing.Size(30, 16);
            this.ckRow_4.TabIndex = 671;
            this.ckRow_4.Text = "4";
            this.ckRow_4.UseVisualStyleBackColor = true;
            this.ckRow_4.Click += new System.EventHandler(this.ckRow_1_Click);
            // 
            // ckRow_3
            // 
            this.ckRow_3.AutoSize = true;
            this.ckRow_3.Location = new System.Drawing.Point(387, 124);
            this.ckRow_3.Name = "ckRow_3";
            this.ckRow_3.Size = new System.Drawing.Size(30, 16);
            this.ckRow_3.TabIndex = 670;
            this.ckRow_3.Text = "3";
            this.ckRow_3.UseVisualStyleBackColor = true;
            this.ckRow_3.Click += new System.EventHandler(this.ckRow_1_Click);
            // 
            // ckRow_2
            // 
            this.ckRow_2.AutoSize = true;
            this.ckRow_2.Location = new System.Drawing.Point(351, 124);
            this.ckRow_2.Name = "ckRow_2";
            this.ckRow_2.Size = new System.Drawing.Size(30, 16);
            this.ckRow_2.TabIndex = 669;
            this.ckRow_2.Text = "2";
            this.ckRow_2.UseVisualStyleBackColor = true;
            this.ckRow_2.Click += new System.EventHandler(this.ckRow_1_Click);
            // 
            // ckRow_1
            // 
            this.ckRow_1.AutoSize = true;
            this.ckRow_1.Location = new System.Drawing.Point(312, 124);
            this.ckRow_1.Name = "ckRow_1";
            this.ckRow_1.Size = new System.Drawing.Size(30, 16);
            this.ckRow_1.TabIndex = 668;
            this.ckRow_1.Text = "1";
            this.ckRow_1.UseVisualStyleBackColor = true;
            this.ckRow_1.Click += new System.EventHandler(this.ckRow_1_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Highlight;
            this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(258, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 24);
            this.label2.TabIndex = 667;
            this.label2.Text = "ROW";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_LoadTotalFile
            // 
            this.btn_LoadTotalFile.Location = new System.Drawing.Point(505, 16);
            this.btn_LoadTotalFile.Name = "btn_LoadTotalFile";
            this.btn_LoadTotalFile.Size = new System.Drawing.Size(196, 42);
            this.btn_LoadTotalFile.TabIndex = 1218;
            this.btn_LoadTotalFile.TabStop = false;
            this.btn_LoadTotalFile.Text = "통합 파일에서 불러오기";
            this.btn_LoadTotalFile.UseVisualStyleBackColor = true;
            this.btn_LoadTotalFile.Click += new System.EventHandler(this.btn_LoadTotalFile_Click);
            // 
            // btn_SaveTotalFile
            // 
            this.btn_SaveTotalFile.Location = new System.Drawing.Point(711, 16);
            this.btn_SaveTotalFile.Name = "btn_SaveTotalFile";
            this.btn_SaveTotalFile.Size = new System.Drawing.Size(193, 42);
            this.btn_SaveTotalFile.TabIndex = 1217;
            this.btn_SaveTotalFile.TabStop = false;
            this.btn_SaveTotalFile.Text = "통합 파일에 저장";
            this.btn_SaveTotalFile.UseVisualStyleBackColor = true;
            this.btn_SaveTotalFile.Click += new System.EventHandler(this.btn_SaveTotalFile_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.btn_SpecialRack_Load);
            this.panel1.Controls.Add(this.btn_LoadTotalFile);
            this.panel1.Controls.Add(this.lv_SpecialRack);
            this.panel1.Controls.Add(this.btn_SaveTotalFile);
            this.panel1.Controls.Add(this.btnAdd_1);
            this.panel1.Controls.Add(this.ckRowAll);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.ckRow_4);
            this.panel1.Controls.Add(this.btnUIpdate);
            this.panel1.Controls.Add(this.ckRow_3);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.ckRow_2);
            this.panel1.Controls.Add(this.lbl_SelectedIndex);
            this.panel1.Controls.Add(this.ckRow_1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.ckItem_0);
            this.panel1.Controls.Add(this.btnAdd_2);
            this.panel1.Controls.Add(this.ckItem_1);
            this.panel1.Controls.Add(this.btn_SpecialRack_FileRead);
            this.panel1.Controls.Add(this.ckItem_2);
            this.panel1.Controls.Add(this.btn_SpecialRack_FileWrite);
            this.panel1.Controls.Add(this.ckItem_3);
            this.panel1.Controls.Add(this.btn_SpecialRack_Set);
            this.panel1.Controls.Add(this.ckItem_4);
            this.panel1.Controls.Add(this.ckItem_5);
            this.panel1.Controls.Add(this.btnAllClear);
            this.panel1.Controls.Add(this.ckItem_6);
            this.panel1.Controls.Add(this.cbLevel);
            this.panel1.Controls.Add(this.ckItem_7);
            this.panel1.Controls.Add(this.cbBay);
            this.panel1.Controls.Add(this.ckItemNone);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1221, 704);
            this.panel1.TabIndex = 1219;
            // 
            // Form_SRMSpecialRack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 704);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_SRMSpecialRack";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "SRM 스폐셜랙 설정";
            this.Load += new System.EventHandler(this.Form_SpecialRack_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private InheritedListView.MyListView lv_SpecialRack;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.Button btnAdd_1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUIpdate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbl_SelectedIndex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ckItem_0;
        private System.Windows.Forms.CheckBox ckItem_1;
        private System.Windows.Forms.CheckBox ckItem_3;
        private System.Windows.Forms.CheckBox ckItem_2;
        private System.Windows.Forms.CheckBox ckItem_5;
        private System.Windows.Forms.CheckBox ckItem_4;
        private System.Windows.Forms.CheckBox ckItem_7;
        private System.Windows.Forms.CheckBox ckItem_6;
        private System.Windows.Forms.CheckBox ckItemNone;
        private System.Windows.Forms.ComboBox cbBay;
        private System.Windows.Forms.ComboBox cbLevel;
        private System.Windows.Forms.Button btnAllClear;
        private System.Windows.Forms.Button btn_SpecialRack_FileRead;
        private System.Windows.Forms.Button btn_SpecialRack_FileWrite;
        private System.Windows.Forms.Button btn_SpecialRack_Set;
        private System.Windows.Forms.Button btn_SpecialRack_Load;
        private System.Windows.Forms.Button btnAdd_2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox ckRowAll;
        private System.Windows.Forms.CheckBox ckRow_4;
        private System.Windows.Forms.CheckBox ckRow_3;
        private System.Windows.Forms.CheckBox ckRow_2;
        private System.Windows.Forms.CheckBox ckRow_1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.Button btn_LoadTotalFile;
        private System.Windows.Forms.Button btn_SaveTotalFile;
        private System.Windows.Forms.Panel panel1;
    }
}
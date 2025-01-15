
namespace VEXI
{
    partial class Form_SRMInhibitionRack
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
            this.lv_NoUseRack = new InheritedListView.MyListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAdd_1 = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUIpdate = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_SelectedIndex = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbBay = new System.Windows.Forms.ComboBox();
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.btnAllClear = new System.Windows.Forms.Button();
            this.btn_NoUse_FileRead = new System.Windows.Forms.Button();
            this.btn_NoUse_FileWrite = new System.Windows.Forms.Button();
            this.btn_NoUseRack_Set = new System.Windows.Forms.Button();
            this.btn_NoUseRack_Load = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btn_LoadTotalFile = new System.Windows.Forms.Button();
            this.btn_SaveTotalFile = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbRow_4 = new System.Windows.Forms.RadioButton();
            this.rbRow_3 = new System.Windows.Forms.RadioButton();
            this.rbRow_2 = new System.Windows.Forms.RadioButton();
            this.rbRow_1 = new System.Windows.Forms.RadioButton();
            this.rbRowAll = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_NoUseRack
            // 
            this.lv_NoUseRack.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lv_NoUseRack.FullRowSelect = true;
            this.lv_NoUseRack.GridLines = true;
            this.lv_NoUseRack.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_NoUseRack.HideSelection = false;
            this.lv_NoUseRack.Location = new System.Drawing.Point(12, 11);
            this.lv_NoUseRack.MultiSelect = false;
            this.lv_NoUseRack.Name = "lv_NoUseRack";
            this.lv_NoUseRack.Size = new System.Drawing.Size(512, 669);
            this.lv_NoUseRack.TabIndex = 273;
            this.lv_NoUseRack.UseCompatibleStateImageBehavior = false;
            this.lv_NoUseRack.View = System.Windows.Forms.View.Details;
            this.lv_NoUseRack.SelectedIndexChanged += new System.EventHandler(this.lv_NoUseRack_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Index";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "BAY";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "LEVEL";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ROW 1";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 70;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "ROW 2";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 70;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "ROW 3";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 70;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "ROW 4";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 70;
            // 
            // btnAdd_1
            // 
            this.btnAdd_1.Location = new System.Drawing.Point(566, 111);
            this.btnAdd_1.Name = "btnAdd_1";
            this.btnAdd_1.Size = new System.Drawing.Size(83, 23);
            this.btnAdd_1.TabIndex = 274;
            this.btnAdd_1.TabStop = false;
            this.btnAdd_1.Text = "추가";
            this.btnAdd_1.UseVisualStyleBackColor = true;
            this.btnAdd_1.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(739, 111);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(83, 23);
            this.btnDelete.TabIndex = 275;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "삭제";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUIpdate
            // 
            this.btnUIpdate.Location = new System.Drawing.Point(653, 111);
            this.btnUIpdate.Name = "btnUIpdate";
            this.btnUIpdate.Size = new System.Drawing.Size(83, 23);
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
            this.label12.Location = new System.Drawing.Point(535, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 24);
            this.label12.TabIndex = 278;
            this.label12.Text = "Index";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_SelectedIndex
            // 
            this.lbl_SelectedIndex.BackColor = System.Drawing.Color.Snow;
            this.lbl_SelectedIndex.ForeColor = System.Drawing.Color.Black;
            this.lbl_SelectedIndex.Location = new System.Drawing.Point(535, 70);
            this.lbl_SelectedIndex.Name = "lbl_SelectedIndex";
            this.lbl_SelectedIndex.Size = new System.Drawing.Size(85, 20);
            this.lbl_SelectedIndex.TabIndex = 279;
            this.lbl_SelectedIndex.Text = "0";
            this.lbl_SelectedIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Highlight;
            this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(623, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 24);
            this.label2.TabIndex = 281;
            this.label2.Text = "ROW";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Highlight;
            this.label3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(837, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 24);
            this.label3.TabIndex = 282;
            this.label3.Text = "BAY";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Highlight;
            this.label4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(915, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 24);
            this.label4.TabIndex = 283;
            this.label4.Text = "LEVEL";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.cbBay.Location = new System.Drawing.Point(837, 69);
            this.cbBay.Name = "cbBay";
            this.cbBay.Size = new System.Drawing.Size(75, 20);
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
            this.cbLevel.Location = new System.Drawing.Point(915, 69);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(75, 20);
            this.cbLevel.TabIndex = 297;
            // 
            // btnAllClear
            // 
            this.btnAllClear.Location = new System.Drawing.Point(834, 111);
            this.btnAllClear.Name = "btnAllClear";
            this.btnAllClear.Size = new System.Drawing.Size(109, 23);
            this.btnAllClear.TabIndex = 298;
            this.btnAllClear.TabStop = false;
            this.btnAllClear.Text = "전체 삭제";
            this.btnAllClear.UseVisualStyleBackColor = true;
            this.btnAllClear.Click += new System.EventHandler(this.btnAllClear_Click);
            // 
            // btn_NoUse_FileRead
            // 
            this.btn_NoUse_FileRead.Location = new System.Drawing.Point(671, 536);
            this.btn_NoUse_FileRead.Name = "btn_NoUse_FileRead";
            this.btn_NoUse_FileRead.Size = new System.Drawing.Size(65, 42);
            this.btn_NoUse_FileRead.TabIndex = 661;
            this.btn_NoUse_FileRead.Text = "파일에서 불러오기";
            this.btn_NoUse_FileRead.UseVisualStyleBackColor = true;
            this.btn_NoUse_FileRead.Visible = false;
            this.btn_NoUse_FileRead.Click += new System.EventHandler(this.btn_NoUseRack_FileRead_Click);
            // 
            // btn_NoUse_FileWrite
            // 
            this.btn_NoUse_FileWrite.Location = new System.Drawing.Point(671, 488);
            this.btn_NoUse_FileWrite.Name = "btn_NoUse_FileWrite";
            this.btn_NoUse_FileWrite.Size = new System.Drawing.Size(65, 42);
            this.btn_NoUse_FileWrite.TabIndex = 660;
            this.btn_NoUse_FileWrite.Text = "파일로 저장";
            this.btn_NoUse_FileWrite.UseVisualStyleBackColor = true;
            this.btn_NoUse_FileWrite.Visible = false;
            this.btn_NoUse_FileWrite.Click += new System.EventHandler(this.btn_NoUse_FileWrite_Click);
            // 
            // btn_NoUseRack_Set
            // 
            this.btn_NoUseRack_Set.Enabled = false;
            this.btn_NoUseRack_Set.Location = new System.Drawing.Point(671, 221);
            this.btn_NoUseRack_Set.Name = "btn_NoUseRack_Set";
            this.btn_NoUseRack_Set.Size = new System.Drawing.Size(203, 42);
            this.btn_NoUseRack_Set.TabIndex = 659;
            this.btn_NoUseRack_Set.TabStop = false;
            this.btn_NoUseRack_Set.Text = "금지 랙 설정 변경 (장치 적용)";
            this.btn_NoUseRack_Set.UseVisualStyleBackColor = true;
            this.btn_NoUseRack_Set.Click += new System.EventHandler(this.btn_NoUseRack_Set_Click);
            // 
            // btn_NoUseRack_Load
            // 
            this.btn_NoUseRack_Load.Location = new System.Drawing.Point(671, 173);
            this.btn_NoUseRack_Load.Name = "btn_NoUseRack_Load";
            this.btn_NoUseRack_Load.Size = new System.Drawing.Size(203, 42);
            this.btn_NoUseRack_Load.TabIndex = 658;
            this.btn_NoUseRack_Load.TabStop = false;
            this.btn_NoUseRack_Load.Text = "장치로부터 금지 랙 설정 읽기";
            this.btn_NoUseRack_Load.UseVisualStyleBackColor = true;
            this.btn_NoUseRack_Load.Click += new System.EventHandler(this.btn_NoUseRack_Load_Click);
            // 
            // btn_LoadTotalFile
            // 
            this.btn_LoadTotalFile.Location = new System.Drawing.Point(671, 284);
            this.btn_LoadTotalFile.Name = "btn_LoadTotalFile";
            this.btn_LoadTotalFile.Size = new System.Drawing.Size(203, 41);
            this.btn_LoadTotalFile.TabIndex = 1216;
            this.btn_LoadTotalFile.TabStop = false;
            this.btn_LoadTotalFile.Text = "통합 파일에서 불러오기";
            this.btn_LoadTotalFile.UseVisualStyleBackColor = true;
            this.btn_LoadTotalFile.Click += new System.EventHandler(this.btn_LoadTotalFile_Click);
            // 
            // btn_SaveTotalFile
            // 
            this.btn_SaveTotalFile.Location = new System.Drawing.Point(671, 331);
            this.btn_SaveTotalFile.Name = "btn_SaveTotalFile";
            this.btn_SaveTotalFile.Size = new System.Drawing.Size(203, 41);
            this.btn_SaveTotalFile.TabIndex = 1215;
            this.btn_SaveTotalFile.TabStop = false;
            this.btn_SaveTotalFile.Text = "통합 파일에 저장";
            this.btn_SaveTotalFile.UseVisualStyleBackColor = true;
            this.btn_SaveTotalFile.Click += new System.EventHandler(this.btn_SaveTotalFile_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbRow_4);
            this.panel1.Controls.Add(this.rbRow_3);
            this.panel1.Controls.Add(this.rbRow_2);
            this.panel1.Controls.Add(this.rbRow_1);
            this.panel1.Controls.Add(this.rbRowAll);
            this.panel1.Location = new System.Drawing.Point(623, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(211, 27);
            this.panel1.TabIndex = 1217;
            // 
            // rbRow_4
            // 
            this.rbRow_4.AutoSize = true;
            this.rbRow_4.Location = new System.Drawing.Point(170, 5);
            this.rbRow_4.Name = "rbRow_4";
            this.rbRow_4.Size = new System.Drawing.Size(29, 16);
            this.rbRow_4.TabIndex = 4;
            this.rbRow_4.TabStop = true;
            this.rbRow_4.Text = "4";
            this.rbRow_4.UseVisualStyleBackColor = true;
            // 
            // rbRow_3
            // 
            this.rbRow_3.AutoSize = true;
            this.rbRow_3.Location = new System.Drawing.Point(131, 5);
            this.rbRow_3.Name = "rbRow_3";
            this.rbRow_3.Size = new System.Drawing.Size(29, 16);
            this.rbRow_3.TabIndex = 3;
            this.rbRow_3.TabStop = true;
            this.rbRow_3.Text = "3";
            this.rbRow_3.UseVisualStyleBackColor = true;
            // 
            // rbRow_2
            // 
            this.rbRow_2.AutoSize = true;
            this.rbRow_2.Location = new System.Drawing.Point(92, 5);
            this.rbRow_2.Name = "rbRow_2";
            this.rbRow_2.Size = new System.Drawing.Size(29, 16);
            this.rbRow_2.TabIndex = 2;
            this.rbRow_2.TabStop = true;
            this.rbRow_2.Text = "2";
            this.rbRow_2.UseVisualStyleBackColor = true;
            // 
            // rbRow_1
            // 
            this.rbRow_1.AutoSize = true;
            this.rbRow_1.Location = new System.Drawing.Point(53, 5);
            this.rbRow_1.Name = "rbRow_1";
            this.rbRow_1.Size = new System.Drawing.Size(29, 16);
            this.rbRow_1.TabIndex = 1;
            this.rbRow_1.TabStop = true;
            this.rbRow_1.Text = "1";
            this.rbRow_1.UseVisualStyleBackColor = true;
            // 
            // rbRowAll
            // 
            this.rbRowAll.AutoSize = true;
            this.rbRowAll.Location = new System.Drawing.Point(7, 5);
            this.rbRowAll.Name = "rbRowAll";
            this.rbRowAll.Size = new System.Drawing.Size(37, 16);
            this.rbRowAll.TabIndex = 0;
            this.rbRowAll.TabStop = true;
            this.rbRowAll.Text = "All";
            this.rbRowAll.UseVisualStyleBackColor = true;
            // 
            // Form_SRMInhibitionRack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 692);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_LoadTotalFile);
            this.Controls.Add(this.btn_SaveTotalFile);
            this.Controls.Add(this.btn_NoUse_FileRead);
            this.Controls.Add(this.btn_NoUse_FileWrite);
            this.Controls.Add(this.btn_NoUseRack_Set);
            this.Controls.Add(this.btn_NoUseRack_Load);
            this.Controls.Add(this.btnAllClear);
            this.Controls.Add(this.cbLevel);
            this.Controls.Add(this.cbBay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_SelectedIndex);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnUIpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd_1);
            this.Controls.Add(this.lv_NoUseRack);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_SRMInhibitionRack";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "SRM 금지랙 설정";
            this.Load += new System.EventHandler(this.Form_NoUseRack_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private InheritedListView.MyListView lv_NoUseRack;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnAdd_1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUIpdate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbl_SelectedIndex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbBay;
        private System.Windows.Forms.ComboBox cbLevel;
        private System.Windows.Forms.Button btnAllClear;
        private System.Windows.Forms.Button btn_NoUse_FileRead;
        private System.Windows.Forms.Button btn_NoUse_FileWrite;
        private System.Windows.Forms.Button btn_NoUseRack_Set;
        private System.Windows.Forms.Button btn_NoUseRack_Load;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button btn_LoadTotalFile;
        private System.Windows.Forms.Button btn_SaveTotalFile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbRow_4;
        private System.Windows.Forms.RadioButton rbRow_3;
        private System.Windows.Forms.RadioButton rbRow_2;
        private System.Windows.Forms.RadioButton rbRow_1;
        private System.Windows.Forms.RadioButton rbRowAll;
    }
}
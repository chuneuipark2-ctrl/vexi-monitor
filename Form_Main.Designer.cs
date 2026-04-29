
namespace VEXI
{
    partial class Form_Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel92 = new System.Windows.Forms.Panel();
            this.pnQuickCtrl = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_ErrRest = new System.Windows.Forms.Button();
            this.btn_DevMode_SetupOn = new System.Windows.Forms.Button();
            this.btn_StopEmergency = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbl_ResponseDevFW = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_DevErrorCode = new System.Windows.Forms.Label();
            this.lbl_DevError = new System.Windows.Forms.Label();
            this.lbl_SetUpMode = new System.Windows.Forms.Label();
            this.lbl_DevForceMode = new System.Windows.Forms.Label();
            this.lbl_ReceviceGood = new System.Windows.Forms.Label();
            this.lbl_ResponseDevID = new System.Windows.Forms.Label();
            this.lbl_ResponseDevType = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnCommSetting = new System.Windows.Forms.Panel();
            this.pnUDPConnectType = new System.Windows.Forms.Panel();
            this.rb_5Connect = new System.Windows.Forms.RadioButton();
            this.rb_2Connect = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cbDevType = new System.Windows.Forms.ComboBox();
            this.cbDevID = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCommClose = new System.Windows.Forms.Button();
            this.lbl_SelectDev = new System.Windows.Forms.Label();
            this.lblCommStatus = new System.Windows.Forms.Label();
            this.btnCommOpen = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.WatchDog = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuItem_Devmonitoring_User = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_basicSt = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_DevSt = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_AllWIndowsClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.DPTimer = new System.Windows.Forms.Timer(this.components);
            this.Loggingtimer = new System.Windows.Forms.Timer(this.components);
            this.panel92.SuspendLayout();
            this.pnQuickCtrl.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnCommSetting.SuspendLayout();
            this.pnUDPConnectType.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel92
            // 
            this.panel92.AutoScroll = true;
            this.panel92.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel92.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel92.Controls.Add(this.pnQuickCtrl);
            this.panel92.Controls.Add(this.panel3);
            this.panel92.Controls.Add(this.panel2);
            this.panel92.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel92.Location = new System.Drawing.Point(0, 24);
            this.panel92.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel92.Name = "panel92";
            this.panel92.Size = new System.Drawing.Size(288, 821);
            this.panel92.TabIndex = 17;
            // 
            // pnQuickCtrl
            // 
            this.pnQuickCtrl.Controls.Add(this.button1);
            this.pnQuickCtrl.Controls.Add(this.btn_ErrRest);
            this.pnQuickCtrl.Controls.Add(this.btn_DevMode_SetupOn);
            this.pnQuickCtrl.Controls.Add(this.btn_StopEmergency);
            this.pnQuickCtrl.Controls.Add(this.btn_Stop);
            this.pnQuickCtrl.Location = new System.Drawing.Point(5, 653);
            this.pnQuickCtrl.Name = "pnQuickCtrl";
            this.pnQuickCtrl.Size = new System.Drawing.Size(152, 194);
            this.pnQuickCtrl.TabIndex = 328;
            this.pnQuickCtrl.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(6, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 28);
            this.button1.TabIndex = 332;
            this.button1.Tag = "0";
            this.button1.Text = "Manual 모드 설정";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btn_DevMode_AutoOn_Click);
            // 
            // btn_ErrRest
            // 
            this.btn_ErrRest.BackColor = System.Drawing.Color.White;
            this.btn_ErrRest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_ErrRest.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_ErrRest.Location = new System.Drawing.Point(6, 9);
            this.btn_ErrRest.Name = "btn_ErrRest";
            this.btn_ErrRest.Size = new System.Drawing.Size(139, 28);
            this.btn_ErrRest.TabIndex = 331;
            this.btn_ErrRest.Tag = "";
            this.btn_ErrRest.Text = "Alarm Reset";
            this.btn_ErrRest.UseVisualStyleBackColor = false;
            this.btn_ErrRest.Click += new System.EventHandler(this.btn_ErrRest_Click);
            // 
            // btn_DevMode_SetupOn
            // 
            this.btn_DevMode_SetupOn.BackColor = System.Drawing.Color.White;
            this.btn_DevMode_SetupOn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_DevMode_SetupOn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_DevMode_SetupOn.Location = new System.Drawing.Point(6, 39);
            this.btn_DevMode_SetupOn.Name = "btn_DevMode_SetupOn";
            this.btn_DevMode_SetupOn.Size = new System.Drawing.Size(139, 28);
            this.btn_DevMode_SetupOn.TabIndex = 330;
            this.btn_DevMode_SetupOn.Tag = "1";
            this.btn_DevMode_SetupOn.Text = "Setup 모드 설정";
            this.btn_DevMode_SetupOn.UseVisualStyleBackColor = false;
            this.btn_DevMode_SetupOn.Click += new System.EventHandler(this.btn_DevMode_AutoOn_Click);
            // 
            // btn_StopEmergency
            // 
            this.btn_StopEmergency.BackColor = System.Drawing.Color.Yellow;
            this.btn_StopEmergency.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_StopEmergency.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_StopEmergency.ForeColor = System.Drawing.Color.Red;
            this.btn_StopEmergency.Location = new System.Drawing.Point(6, 106);
            this.btn_StopEmergency.Name = "btn_StopEmergency";
            this.btn_StopEmergency.Size = new System.Drawing.Size(139, 48);
            this.btn_StopEmergency.TabIndex = 327;
            this.btn_StopEmergency.Text = "비상정지 (E.Stop)";
            this.btn_StopEmergency.UseVisualStyleBackColor = false;
            this.btn_StopEmergency.Click += new System.EventHandler(this.btn_StopEmergency_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.BackColor = System.Drawing.Color.Yellow;
            this.btn_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Stop.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Stop.ForeColor = System.Drawing.Color.Red;
            this.btn_Stop.Location = new System.Drawing.Point(6, 158);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(139, 28);
            this.btn_Stop.TabIndex = 325;
            this.btn_Stop.Text = "정지 (Stop)";
            this.btn_Stop.UseVisualStyleBackColor = false;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lbl_ResponseDevFW);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.lbl_DevErrorCode);
            this.panel3.Controls.Add(this.lbl_DevError);
            this.panel3.Controls.Add(this.lbl_SetUpMode);
            this.panel3.Controls.Add(this.lbl_DevForceMode);
            this.panel3.Controls.Add(this.lbl_ReceviceGood);
            this.panel3.Controls.Add(this.lbl_ResponseDevID);
            this.panel3.Controls.Add(this.lbl_ResponseDevType);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(8, 52);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(176, 227);
            this.panel3.TabIndex = 324;
            // 
            // lbl_ResponseDevFW
            // 
            this.lbl_ResponseDevFW.BackColor = System.Drawing.Color.Yellow;
            this.lbl_ResponseDevFW.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ResponseDevFW.Location = new System.Drawing.Point(73, 102);
            this.lbl_ResponseDevFW.Name = "lbl_ResponseDevFW";
            this.lbl_ResponseDevFW.Size = new System.Drawing.Size(94, 17);
            this.lbl_ResponseDevFW.TabIndex = 348;
            this.lbl_ResponseDevFW.Text = "---";
            this.lbl_ResponseDevFW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 19);
            this.label8.TabIndex = 347;
            this.label8.Text = "장치 FW";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DevErrorCode
            // 
            this.lbl_DevErrorCode.BackColor = System.Drawing.Color.Red;
            this.lbl_DevErrorCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_DevErrorCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_DevErrorCode.ForeColor = System.Drawing.Color.White;
            this.lbl_DevErrorCode.Location = new System.Drawing.Point(7, 197);
            this.lbl_DevErrorCode.Name = "lbl_DevErrorCode";
            this.lbl_DevErrorCode.Size = new System.Drawing.Size(161, 22);
            this.lbl_DevErrorCode.TabIndex = 346;
            this.lbl_DevErrorCode.Tag = "6013";
            this.lbl_DevErrorCode.Text = "장애 상태";
            this.lbl_DevErrorCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_DevErrorCode.Visible = false;
            // 
            // lbl_DevError
            // 
            this.lbl_DevError.BackColor = System.Drawing.Color.Gray;
            this.lbl_DevError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_DevError.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_DevError.ForeColor = System.Drawing.Color.Black;
            this.lbl_DevError.Location = new System.Drawing.Point(7, 178);
            this.lbl_DevError.Name = "lbl_DevError";
            this.lbl_DevError.Size = new System.Drawing.Size(161, 19);
            this.lbl_DevError.TabIndex = 345;
            this.lbl_DevError.Tag = "6013";
            this.lbl_DevError.Text = "정상 상태";
            this.lbl_DevError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_DevError.DoubleClick += new System.EventHandler(this.lbl_DevError_DoubleClick);
            // 
            // lbl_SetUpMode
            // 
            this.lbl_SetUpMode.BackColor = System.Drawing.Color.Yellow;
            this.lbl_SetUpMode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SetUpMode.ForeColor = System.Drawing.Color.Black;
            this.lbl_SetUpMode.Location = new System.Drawing.Point(90, 152);
            this.lbl_SetUpMode.Name = "lbl_SetUpMode";
            this.lbl_SetUpMode.Size = new System.Drawing.Size(77, 24);
            this.lbl_SetUpMode.TabIndex = 344;
            this.lbl_SetUpMode.Text = "셋업 모드";
            this.lbl_SetUpMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_SetUpMode.Visible = false;
            // 
            // lbl_DevForceMode
            // 
            this.lbl_DevForceMode.BackColor = System.Drawing.Color.Red;
            this.lbl_DevForceMode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_DevForceMode.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_DevForceMode.Location = new System.Drawing.Point(8, 152);
            this.lbl_DevForceMode.Name = "lbl_DevForceMode";
            this.lbl_DevForceMode.Size = new System.Drawing.Size(77, 24);
            this.lbl_DevForceMode.TabIndex = 343;
            this.lbl_DevForceMode.Text = "강제 모드";
            this.lbl_DevForceMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_DevForceMode.Visible = false;
            // 
            // lbl_ReceviceGood
            // 
            this.lbl_ReceviceGood.BackColor = System.Drawing.Color.Gray;
            this.lbl_ReceviceGood.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ReceviceGood.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_ReceviceGood.Location = new System.Drawing.Point(8, 130);
            this.lbl_ReceviceGood.Name = "lbl_ReceviceGood";
            this.lbl_ReceviceGood.Size = new System.Drawing.Size(160, 19);
            this.lbl_ReceviceGood.TabIndex = 342;
            this.lbl_ReceviceGood.Tag = "6013";
            this.lbl_ReceviceGood.Text = "통신 상태";
            this.lbl_ReceviceGood.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ResponseDevID
            // 
            this.lbl_ResponseDevID.BackColor = System.Drawing.Color.Yellow;
            this.lbl_ResponseDevID.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ResponseDevID.Location = new System.Drawing.Point(73, 83);
            this.lbl_ResponseDevID.Name = "lbl_ResponseDevID";
            this.lbl_ResponseDevID.Size = new System.Drawing.Size(94, 17);
            this.lbl_ResponseDevID.TabIndex = 341;
            this.lbl_ResponseDevID.Text = "---";
            this.lbl_ResponseDevID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ResponseDevType
            // 
            this.lbl_ResponseDevType.BackColor = System.Drawing.Color.Yellow;
            this.lbl_ResponseDevType.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ResponseDevType.Location = new System.Drawing.Point(73, 64);
            this.lbl_ResponseDevType.Name = "lbl_ResponseDevType";
            this.lbl_ResponseDevType.Size = new System.Drawing.Size(94, 17);
            this.lbl_ResponseDevType.TabIndex = 340;
            this.lbl_ResponseDevType.Text = "---";
            this.lbl_ResponseDevType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_ResponseDevType.Click += new System.EventHandler(this.lbl_ResponseDevType_Click);
            this.lbl_ResponseDevType.DoubleClick += new System.EventHandler(this.lbl_ResponseDevType_DoubleClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(8, 83);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 19);
            this.label10.TabIndex = 339;
            this.label10.Text = "장치 ID";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 19);
            this.label7.TabIndex = 336;
            this.label7.Text = "장치 타입";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.DimGray;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(174, 32);
            this.label9.TabIndex = 7;
            this.label9.Tag = "";
            this.label9.Text = "연결 장치 정보";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pnCommSetting);
            this.panel2.Controls.Add(this.btnCommClose);
            this.panel2.Controls.Add(this.lbl_SelectDev);
            this.panel2.Controls.Add(this.lblCommStatus);
            this.panel2.Controls.Add(this.btnCommOpen);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(8, 287);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(175, 368);
            this.panel2.TabIndex = 322;
            // 
            // pnCommSetting
            // 
            this.pnCommSetting.BackColor = System.Drawing.Color.White;
            this.pnCommSetting.Controls.Add(this.pnUDPConnectType);
            this.pnCommSetting.Controls.Add(this.label3);
            this.pnCommSetting.Controls.Add(this.cbDevType);
            this.pnCommSetting.Controls.Add(this.cbDevID);
            this.pnCommSetting.Controls.Add(this.label2);
            this.pnCommSetting.Location = new System.Drawing.Point(1, 137);
            this.pnCommSetting.Name = "pnCommSetting";
            this.pnCommSetting.Size = new System.Drawing.Size(169, 115);
            this.pnCommSetting.TabIndex = 33;
            // 
            // pnUDPConnectType
            // 
            this.pnUDPConnectType.Controls.Add(this.rb_5Connect);
            this.pnUDPConnectType.Controls.Add(this.rb_2Connect);
            this.pnUDPConnectType.Location = new System.Drawing.Point(6, 3);
            this.pnUDPConnectType.Name = "pnUDPConnectType";
            this.pnUDPConnectType.Size = new System.Drawing.Size(159, 54);
            this.pnUDPConnectType.TabIndex = 347;
            // 
            // rb_5Connect
            // 
            this.rb_5Connect.AutoSize = true;
            this.rb_5Connect.Location = new System.Drawing.Point(83, 29);
            this.rb_5Connect.Name = "rb_5Connect";
            this.rb_5Connect.Size = new System.Drawing.Size(68, 19);
            this.rb_5Connect.TabIndex = 2;
            this.rb_5Connect.Text = "5G 연결";
            this.rb_5Connect.UseVisualStyleBackColor = true;
            this.rb_5Connect.CheckedChanged += new System.EventHandler(this.rb_5Connect_CheckedChanged);
            // 
            // rb_2Connect
            // 
            this.rb_2Connect.AutoSize = true;
            this.rb_2Connect.Location = new System.Drawing.Point(3, 29);
            this.rb_2Connect.Name = "rb_2Connect";
            this.rb_2Connect.Size = new System.Drawing.Size(78, 19);
            this.rb_2Connect.TabIndex = 1;
            this.rb_2Connect.Text = "2.4G 연결";
            this.rb_2Connect.UseVisualStyleBackColor = true;
            this.rb_2Connect.CheckedChanged += new System.EventHandler(this.rb_2Connect_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(89, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 338;
            this.label3.Text = "장치 ID";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbDevType
            // 
            this.cbDevType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDevType.FormattingEnabled = true;
            this.cbDevType.Items.AddRange(new object[] {
            "SRM",
            "RTV",
            "EMS",
            "0xFF"});
            this.cbDevType.Location = new System.Drawing.Point(11, 76);
            this.cbDevType.Name = "cbDevType";
            this.cbDevType.Size = new System.Drawing.Size(70, 23);
            this.cbDevType.TabIndex = 335;
            this.cbDevType.SelectedIndexChanged += new System.EventHandler(this.cbDevType_SelectedIndexChanged);
            // 
            // cbDevID
            // 
            this.cbDevID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDevID.FormattingEnabled = true;
            this.cbDevID.Items.AddRange(new object[] {
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
            "ANY"});
            this.cbDevID.Location = new System.Drawing.Point(86, 76);
            this.cbDevID.Name = "cbDevID";
            this.cbDevID.Size = new System.Drawing.Size(61, 23);
            this.cbDevID.TabIndex = 337;
            this.cbDevID.SelectedIndexChanged += new System.EventHandler(this.cbDevID_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 336;
            this.label2.Text = "장치 타입";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCommClose
            // 
            this.btnCommClose.Location = new System.Drawing.Point(89, 306);
            this.btnCommClose.Name = "btnCommClose";
            this.btnCommClose.Size = new System.Drawing.Size(79, 34);
            this.btnCommClose.TabIndex = 346;
            this.btnCommClose.Text = "끊기";
            this.btnCommClose.UseVisualStyleBackColor = true;
            this.btnCommClose.Click += new System.EventHandler(this.btnCommClose_Click);
            // 
            // lbl_SelectDev
            // 
            this.lbl_SelectDev.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SelectDev.Location = new System.Drawing.Point(4, 118);
            this.lbl_SelectDev.Name = "lbl_SelectDev";
            this.lbl_SelectDev.Size = new System.Drawing.Size(164, 16);
            this.lbl_SelectDev.TabIndex = 343;
            this.lbl_SelectDev.Text = "----";
            this.lbl_SelectDev.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_SelectDev.Click += new System.EventHandler(this.lbl_SelectDev_Click);
            // 
            // lblCommStatus
            // 
            this.lblCommStatus.BackColor = System.Drawing.Color.Gray;
            this.lblCommStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommStatus.Location = new System.Drawing.Point(4, 93);
            this.lblCommStatus.Name = "lblCommStatus";
            this.lblCommStatus.Size = new System.Drawing.Size(165, 21);
            this.lblCommStatus.TabIndex = 335;
            this.lblCommStatus.Text = "COMM OFF";
            this.lblCommStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCommOpen
            // 
            this.btnCommOpen.Location = new System.Drawing.Point(4, 306);
            this.btnCommOpen.Name = "btnCommOpen";
            this.btnCommOpen.Size = new System.Drawing.Size(79, 34);
            this.btnCommOpen.TabIndex = 333;
            this.btnCommOpen.Text = "통신 연결";
            this.btnCommOpen.UseVisualStyleBackColor = true;
            this.btnCommOpen.Click += new System.EventHandler(this.btnCommOpen_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.DimGray;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 34);
            this.label5.TabIndex = 7;
            this.label5.Tag = "";
            this.label5.Text = "통신설정";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label5_MouseDown);
            this.label5.MouseEnter += new System.EventHandler(this.label5_MouseEnter);
            this.label5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label5_MouseUp);
            // 
            // WatchDog
            // 
            this.WatchDog.Interval = 1000;
            this.WatchDog.Tick += new System.EventHandler(this.WatchDog_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Devmonitoring_User,
            this.ToolStripMenuItem,
            this.menu_AllWIndowsClose,
            this.toolStripMenuItem2});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1540, 24);
            this.menuStrip1.TabIndex = 31;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // MenuItem_Devmonitoring_User
            // 
            this.MenuItem_Devmonitoring_User.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_basicSt,
            this.menu_DevSt});
            this.MenuItem_Devmonitoring_User.Name = "MenuItem_Devmonitoring_User";
            this.MenuItem_Devmonitoring_User.Size = new System.Drawing.Size(110, 22);
            this.MenuItem_Devmonitoring_User.Text = "A. 장치 모니터링";
            // 
            // MenuItem_basicSt
            // 
            this.MenuItem_basicSt.Name = "MenuItem_basicSt";
            this.MenuItem_basicSt.Size = new System.Drawing.Size(164, 22);
            this.MenuItem_basicSt.Text = "1. 장치 기본정보";
            this.MenuItem_basicSt.Click += new System.EventHandler(this.MenuItem_basicSt_Click);
            // 
            // menu_DevSt
            // 
            this.menu_DevSt.Name = "menu_DevSt";
            this.menu_DevSt.Size = new System.Drawing.Size(164, 22);
            this.menu_DevSt.Text = "2. 장치 상태";
            this.menu_DevSt.Click += new System.EventHandler(this.menu_DevSt_Click);
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.ToolStripMenuItem.Text = "B. 장비 운전조작";
            this.ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // menu_AllWIndowsClose
            // 
            this.menu_AllWIndowsClose.Name = "menu_AllWIndowsClose";
            this.menu_AllWIndowsClose.Size = new System.Drawing.Size(59, 22);
            this.menu_AllWIndowsClose.Text = "창 닫기";
            this.menu_AllWIndowsClose.Visible = false;
            this.menu_AllWIndowsClose.Click += new System.EventHandler(this.menu_AllWIndowsClose_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(84, 22);
            this.toolStripMenuItem2.Text = "F. 출력 시험";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // DPTimer
            // 
            this.DPTimer.Interval = 1000;
            this.DPTimer.Tick += new System.EventHandler(this.DPTimer_Tick);
            // 
            // Loggingtimer
            // 
            this.Loggingtimer.Interval = 50;
            this.Loggingtimer.Tick += new System.EventHandler(this.Loggingtimer_Tick);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 845);
            this.Controls.Add(this.panel92);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1022, 625);
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTV IO TEST";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Main_FormClosing);
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.panel92.ResumeLayout(false);
            this.pnQuickCtrl.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnCommSetting.ResumeLayout(false);
            this.pnUDPConnectType.ResumeLayout(false);
            this.pnUDPConnectType.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel92;
        private System.Windows.Forms.Timer WatchDog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCommOpen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbDevID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbDevType;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl_ReceviceGood;
        private System.Windows.Forms.Label lbl_ResponseDevID;
        private System.Windows.Forms.Label lbl_ResponseDevType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Devmonitoring_User;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_basicSt;
        private System.Windows.Forms.Label lblCommStatus;
        private System.Windows.Forms.Label lbl_SelectDev;
        private System.Windows.Forms.Timer DPTimer;
        private System.Windows.Forms.ToolStripMenuItem menu_DevSt;
        private System.Windows.Forms.Button btn_StopEmergency;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Label lbl_DevForceMode;
        private System.Windows.Forms.Label lbl_SetUpMode;
        private System.Windows.Forms.Button btnCommClose;
        private System.Windows.Forms.Panel pnCommSetting;
        private System.Windows.Forms.ToolStripMenuItem menu_AllWIndowsClose;
        private System.Windows.Forms.Panel pnQuickCtrl;
        private System.Windows.Forms.Button btn_DevMode_SetupOn;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Timer Loggingtimer;
        private System.Windows.Forms.Label lbl_DevError;
        private System.Windows.Forms.Panel pnUDPConnectType;
        private System.Windows.Forms.RadioButton rb_5Connect;
        private System.Windows.Forms.RadioButton rb_2Connect;
        private System.Windows.Forms.Button btn_ErrRest;
        private System.Windows.Forms.Label lbl_DevErrorCode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_ResponseDevFW;
        private System.Windows.Forms.Label label8;
    }
}



namespace VEXI
{
    partial class Form_DevDebugLog
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
            this.components = new System.ComponentModel.Container();
            this.btn_Log_FileLoad = new System.Windows.Forms.Button();
            this.btn_Log_FileSave = new System.Windows.Forms.Button();
            this.btn_EventLog_Req = new System.Windows.Forms.Button();
            this.NoAnswerTimer = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Btn_LoadTitle = new System.Windows.Forms.Button();
            this.btn_EventLog_Stop = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.edWantCount = new System.Windows.Forms.TextBox();
            this.rbLast = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.btn_Log_TextFileSave = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btn_Delete_EventLog = new System.Windows.Forms.Button();
            this.label55 = new System.Windows.Forms.Label();
            this.lv_DevLog = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.edLogkind = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Log_FileLoad
            // 
            this.btn_Log_FileLoad.Location = new System.Drawing.Point(706, 27);
            this.btn_Log_FileLoad.Name = "btn_Log_FileLoad";
            this.btn_Log_FileLoad.Size = new System.Drawing.Size(123, 37);
            this.btn_Log_FileLoad.TabIndex = 437;
            this.btn_Log_FileLoad.Text = "파일에서 불러오기";
            this.btn_Log_FileLoad.UseVisualStyleBackColor = true;
            this.btn_Log_FileLoad.Click += new System.EventHandler(this.btn_Log_FileLoad_Click);
            // 
            // btn_Log_FileSave
            // 
            this.btn_Log_FileSave.Enabled = false;
            this.btn_Log_FileSave.Location = new System.Drawing.Point(577, 27);
            this.btn_Log_FileSave.Name = "btn_Log_FileSave";
            this.btn_Log_FileSave.Size = new System.Drawing.Size(123, 37);
            this.btn_Log_FileSave.TabIndex = 436;
            this.btn_Log_FileSave.Text = "파일로 저장";
            this.btn_Log_FileSave.UseVisualStyleBackColor = true;
            this.btn_Log_FileSave.Click += new System.EventHandler(this.btn_Log_FileSave_Click);
            // 
            // btn_EventLog_Req
            // 
            this.btn_EventLog_Req.Location = new System.Drawing.Point(244, 14);
            this.btn_EventLog_Req.Name = "btn_EventLog_Req";
            this.btn_EventLog_Req.Size = new System.Drawing.Size(139, 37);
            this.btn_EventLog_Req.TabIndex = 434;
            this.btn_EventLog_Req.Text = "개발자로그 불러오기";
            this.btn_EventLog_Req.UseVisualStyleBackColor = true;
            this.btn_EventLog_Req.Click += new System.EventHandler(this.btn_Log_Req_Click);
            // 
            // NoAnswerTimer
            // 
            this.NoAnswerTimer.Interval = 500;
            this.NoAnswerTimer.Tick += new System.EventHandler(this.NoAnswerTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.Btn_LoadTitle);
            this.panel1.Controls.Add(this.btn_EventLog_Stop);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btn_Log_TextFileSave);
            this.panel1.Controls.Add(this.btn_Log_FileLoad);
            this.panel1.Controls.Add(this.btn_EventLog_Req);
            this.panel1.Controls.Add(this.lblProgress);
            this.panel1.Controls.Add(this.btn_Log_FileSave);
            this.panel1.Controls.Add(this.btn_Delete_EventLog);
            this.panel1.Controls.Add(this.label55);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1260, 107);
            this.panel1.TabIndex = 439;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(997, 81);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 461;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(900, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 18);
            this.button1.TabIndex = 460;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // Btn_LoadTitle
            // 
            this.Btn_LoadTitle.Location = new System.Drawing.Point(578, 70);
            this.Btn_LoadTitle.Name = "Btn_LoadTitle";
            this.Btn_LoadTitle.Size = new System.Drawing.Size(251, 23);
            this.Btn_LoadTitle.TabIndex = 459;
            this.Btn_LoadTitle.Text = "항목명칭 갱신 (TITLE_DEBUG.INI)\r\n";
            this.Btn_LoadTitle.UseVisualStyleBackColor = true;
            this.Btn_LoadTitle.Click += new System.EventHandler(this.Btn_LoadTitle_Click);
            // 
            // btn_EventLog_Stop
            // 
            this.btn_EventLog_Stop.Enabled = false;
            this.btn_EventLog_Stop.Location = new System.Drawing.Point(389, 14);
            this.btn_EventLog_Stop.Name = "btn_EventLog_Stop";
            this.btn_EventLog_Stop.Size = new System.Drawing.Size(68, 37);
            this.btn_EventLog_Stop.TabIndex = 458;
            this.btn_EventLog_Stop.Text = "중지";
            this.btn_EventLog_Stop.UseVisualStyleBackColor = true;
            this.btn_EventLog_Stop.Click += new System.EventHandler(this.btn_EventLog_Stop_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.edLogkind);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.edWantCount);
            this.groupBox1.Controls.Add(this.rbLast);
            this.groupBox1.Controls.Add(this.rbAll);
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 80);
            this.groupBox1.TabIndex = 457;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "개발자로 요청 옵션";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 460;
            this.label2.Text = "개";
            // 
            // edWantCount
            // 
            this.edWantCount.Location = new System.Drawing.Point(132, 19);
            this.edWantCount.MaxLength = 4;
            this.edWantCount.Name = "edWantCount";
            this.edWantCount.Size = new System.Drawing.Size(56, 21);
            this.edWantCount.TabIndex = 459;
            this.edWantCount.Text = "200";
            this.edWantCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // rbLast
            // 
            this.rbLast.AutoSize = true;
            this.rbLast.Location = new System.Drawing.Point(79, 22);
            this.rbLast.Name = "rbLast";
            this.rbLast.Size = new System.Drawing.Size(47, 16);
            this.rbLast.TabIndex = 458;
            this.rbLast.Text = "최근";
            this.rbLast.UseVisualStyleBackColor = true;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(11, 22);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(47, 16);
            this.rbAll.TabIndex = 0;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "전체";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // btn_Log_TextFileSave
            // 
            this.btn_Log_TextFileSave.Enabled = false;
            this.btn_Log_TextFileSave.Location = new System.Drawing.Point(847, 27);
            this.btn_Log_TextFileSave.Name = "btn_Log_TextFileSave";
            this.btn_Log_TextFileSave.Size = new System.Drawing.Size(123, 37);
            this.btn_Log_TextFileSave.TabIndex = 454;
            this.btn_Log_TextFileSave.Text = "Text 파일로 저장";
            this.btn_Log_TextFileSave.UseVisualStyleBackColor = true;
            this.btn_Log_TextFileSave.Click += new System.EventHandler(this.btn_Log_TextFileSave_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblProgress.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblProgress.Location = new System.Drawing.Point(501, 80);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(23, 12);
            this.lblProgress.TabIndex = 451;
            this.lblProgress.Text = "0/0";
            // 
            // btn_Delete_EventLog
            // 
            this.btn_Delete_EventLog.Location = new System.Drawing.Point(244, 57);
            this.btn_Delete_EventLog.Name = "btn_Delete_EventLog";
            this.btn_Delete_EventLog.Size = new System.Drawing.Size(141, 37);
            this.btn_Delete_EventLog.TabIndex = 452;
            this.btn_Delete_EventLog.Text = "개발자로그 전체 삭제";
            this.btn_Delete_EventLog.UseVisualStyleBackColor = true;
            this.btn_Delete_EventLog.Click += new System.EventHandler(this.btn_Delete_AlarmLog_Click);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label55.Location = new System.Drawing.Point(416, 80);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(79, 12);
            this.label55.TabIndex = 450;
            this.label55.Text = "Progress : ";
            this.label55.DoubleClick += new System.EventHandler(this.label55_DoubleClick);
            // 
            // lv_DevLog
            // 
            this.lv_DevLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lv_DevLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_DevLog.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lv_DevLog.FullRowSelect = true;
            this.lv_DevLog.GridLines = true;
            this.lv_DevLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_DevLog.HideSelection = false;
            this.lv_DevLog.Location = new System.Drawing.Point(0, 107);
            this.lv_DevLog.Name = "lv_DevLog";
            this.lv_DevLog.Size = new System.Drawing.Size(1260, 459);
            this.lv_DevLog.TabIndex = 454;
            this.lv_DevLog.UseCompatibleStateImageBehavior = false;
            this.lv_DevLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "No";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "로그 시간";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 150;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(234, 205);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(330, 256);
            this.listBox1.TabIndex = 455;
            this.listBox1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 12);
            this.label1.TabIndex = 461;
            this.label1.Text = "로그타입구분 (0~)";
            // 
            // edLogkind
            // 
            this.edLogkind.Location = new System.Drawing.Point(132, 51);
            this.edLogkind.MaxLength = 4;
            this.edLogkind.Name = "edLogkind";
            this.edLogkind.Size = new System.Drawing.Size(56, 21);
            this.edLogkind.TabIndex = 462;
            this.edLogkind.Text = "0";
            this.edLogkind.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form_DevDebugLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 566);
            this.ControlBox = false;
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.lv_DevLog);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_DevDebugLog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "개발자 로그";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_DevLog_FormClosed);
            this.Load += new System.EventHandler(this.Form_DevLog_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_Log_FileLoad;
        private System.Windows.Forms.Button btn_Log_FileSave;
        private System.Windows.Forms.Button btn_EventLog_Req;
        private System.Windows.Forms.Timer NoAnswerTimer;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblProgress;
        public System.Windows.Forms.Label label55;
        private System.Windows.Forms.Button btn_Delete_EventLog;
        private System.Windows.Forms.ListView lv_DevLog;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btn_Log_TextFileSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.Button btn_EventLog_Stop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edWantCount;
        private System.Windows.Forms.RadioButton rbLast;
        private System.Windows.Forms.Button Btn_LoadTitle;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox edLogkind;
        private System.Windows.Forms.Label label1;
    }
}

namespace VEXI
{
    partial class Form_DevEventLog
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbPeriod = new System.Windows.Forms.RadioButton();
            this.dateTimePicker_To = new System.Windows.Forms.DateTimePicker();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.dateTimePicker_From = new System.Windows.Forms.DateTimePicker();
            this.btn_Log_TextFileSave = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btn_Delete_EventLog = new System.Windows.Forms.Button();
            this.label55 = new System.Windows.Forms.Label();
            this.lv_DevLog = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_EventLog_Stop = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Log_FileLoad
            // 
            this.btn_Log_FileLoad.Location = new System.Drawing.Point(911, 23);
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
            this.btn_Log_FileSave.Location = new System.Drawing.Point(782, 23);
            this.btn_Log_FileSave.Name = "btn_Log_FileSave";
            this.btn_Log_FileSave.Size = new System.Drawing.Size(123, 37);
            this.btn_Log_FileSave.TabIndex = 436;
            this.btn_Log_FileSave.Text = "파일로 저장";
            this.btn_Log_FileSave.UseVisualStyleBackColor = true;
            this.btn_Log_FileSave.Click += new System.EventHandler(this.btn_Log_FileSave_Click);
            // 
            // btn_EventLog_Req
            // 
            this.btn_EventLog_Req.Location = new System.Drawing.Point(449, 10);
            this.btn_EventLog_Req.Name = "btn_EventLog_Req";
            this.btn_EventLog_Req.Size = new System.Drawing.Size(139, 37);
            this.btn_EventLog_Req.TabIndex = 434;
            this.btn_EventLog_Req.Text = "이벤트로그 불러오기";
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
            this.panel1.Size = new System.Drawing.Size(1260, 101);
            this.panel1.TabIndex = 439;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rbPeriod);
            this.groupBox1.Controls.Add(this.dateTimePicker_To);
            this.groupBox1.Controls.Add(this.rbAll);
            this.groupBox1.Controls.Add(this.dateTimePicker_From);
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 73);
            this.groupBox1.TabIndex = 457;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "이벤트로그 요청 옵션";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(238, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 12);
            this.label1.TabIndex = 457;
            this.label1.Text = "~";
            // 
            // rbPeriod
            // 
            this.rbPeriod.AutoSize = true;
            this.rbPeriod.Location = new System.Drawing.Point(11, 44);
            this.rbPeriod.Name = "rbPeriod";
            this.rbPeriod.Size = new System.Drawing.Size(75, 16);
            this.rbPeriod.TabIndex = 1;
            this.rbPeriod.Text = "기간 설정";
            this.rbPeriod.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker_To
            // 
            this.dateTimePicker_To.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dateTimePicker_To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_To.Location = new System.Drawing.Point(256, 40);
            this.dateTimePicker_To.MaxDate = new System.DateTime(2080, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker_To.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker_To.Name = "dateTimePicker_To";
            this.dateTimePicker_To.Size = new System.Drawing.Size(142, 21);
            this.dateTimePicker_To.TabIndex = 456;
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
            // dateTimePicker_From
            // 
            this.dateTimePicker_From.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dateTimePicker_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_From.Location = new System.Drawing.Point(92, 40);
            this.dateTimePicker_From.MaxDate = new System.DateTime(2080, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker_From.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker_From.Name = "dateTimePicker_From";
            this.dateTimePicker_From.Size = new System.Drawing.Size(142, 21);
            this.dateTimePicker_From.TabIndex = 455;
            // 
            // btn_Log_TextFileSave
            // 
            this.btn_Log_TextFileSave.Enabled = false;
            this.btn_Log_TextFileSave.Location = new System.Drawing.Point(1080, 23);
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
            this.lblProgress.Location = new System.Drawing.Point(706, 76);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(23, 12);
            this.lblProgress.TabIndex = 451;
            this.lblProgress.Text = "0/0";
            // 
            // btn_Delete_EventLog
            // 
            this.btn_Delete_EventLog.Location = new System.Drawing.Point(449, 53);
            this.btn_Delete_EventLog.Name = "btn_Delete_EventLog";
            this.btn_Delete_EventLog.Size = new System.Drawing.Size(141, 37);
            this.btn_Delete_EventLog.TabIndex = 452;
            this.btn_Delete_EventLog.Text = "이벤트로그 전체 삭제";
            this.btn_Delete_EventLog.UseVisualStyleBackColor = true;
            this.btn_Delete_EventLog.Click += new System.EventHandler(this.btn_Delete_AlarmLog_Click);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label55.Location = new System.Drawing.Point(621, 76);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(79, 12);
            this.label55.TabIndex = 450;
            this.label55.Text = "Progress : ";
            // 
            // lv_DevLog
            // 
            this.lv_DevLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lv_DevLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_DevLog.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lv_DevLog.FullRowSelect = true;
            this.lv_DevLog.GridLines = true;
            this.lv_DevLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_DevLog.HideSelection = false;
            this.lv_DevLog.Location = new System.Drawing.Point(0, 101);
            this.lv_DevLog.Name = "lv_DevLog";
            this.lv_DevLog.Size = new System.Drawing.Size(1260, 624);
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
            // columnHeader3
            // 
            this.columnHeader3.Text = "알람코드";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "알람명";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 250;
            // 
            // btn_EventLog_Stop
            // 
            this.btn_EventLog_Stop.Enabled = false;
            this.btn_EventLog_Stop.Location = new System.Drawing.Point(594, 10);
            this.btn_EventLog_Stop.Name = "btn_EventLog_Stop";
            this.btn_EventLog_Stop.Size = new System.Drawing.Size(68, 37);
            this.btn_EventLog_Stop.TabIndex = 458;
            this.btn_EventLog_Stop.Text = "중지";
            this.btn_EventLog_Stop.UseVisualStyleBackColor = true;
            this.btn_EventLog_Stop.Click += new System.EventHandler(this.btn_EventLog_Stop_Click);
            // 
            // Form_DevEventLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 725);
            this.ControlBox = false;
            this.Controls.Add(this.lv_DevLog);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_DevEventLog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "이벤트 로그";
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
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btn_Log_TextFileSave;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbPeriod;
        private System.Windows.Forms.DateTimePicker dateTimePicker_To;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.DateTimePicker dateTimePicker_From;
        private System.Windows.Forms.Button btn_EventLog_Stop;
    }
}
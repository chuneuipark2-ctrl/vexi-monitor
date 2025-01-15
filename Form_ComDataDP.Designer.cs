
namespace VEXI
{
    partial class Form_ComDataDP
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cbNoPollingData = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cbStopRefresh = new System.Windows.Forms.CheckBox();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbLoggingInterval = new System.Windows.Forms.ComboBox();
            this.cbLogging_DIOChange = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rbLogging_Stop = new System.Windows.Forms.RadioButton();
            this.rbComDataLogging_Start = new System.Windows.Forms.RadioButton();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.rbSpeedLogging_Start = new System.Windows.Forms.RadioButton();
            this.gbComDataLoggingOption = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.gbComDataLoggingOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.cbNoPollingData);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.cbStopRefresh);
            this.panel1.Controls.Add(this.btnSaveToFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(718, 59);
            this.panel1.TabIndex = 330;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(251, 16);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(110, 28);
            this.checkBox1.TabIndex = 335;
            this.checkBox1.Text = "Stop Polling\r\n(Debug 용도임)";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // cbNoPollingData
            // 
            this.cbNoPollingData.AutoSize = true;
            this.cbNoPollingData.Location = new System.Drawing.Point(121, 22);
            this.cbNoPollingData.Name = "cbNoPollingData";
            this.cbNoPollingData.Size = new System.Drawing.Size(112, 16);
            this.cbNoPollingData.TabIndex = 334;
            this.cbNoPollingData.Text = "폴링데이터 제외";
            this.cbNoPollingData.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(602, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 37);
            this.button1.TabIndex = 333;
            this.button1.Text = "닫기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnClear
            // 
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.Location = new System.Drawing.Point(391, 11);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(95, 37);
            this.btnClear.TabIndex = 332;
            this.btnClear.Text = "화면 지우기";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cbStopRefresh
            // 
            this.cbStopRefresh.AutoSize = true;
            this.cbStopRefresh.Location = new System.Drawing.Point(12, 22);
            this.cbStopRefresh.Name = "cbStopRefresh";
            this.cbStopRefresh.Size = new System.Drawing.Size(92, 16);
            this.cbStopRefresh.TabIndex = 331;
            this.cbStopRefresh.Text = "Stop refresh";
            this.cbStopRefresh.UseVisualStyleBackColor = true;
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Location = new System.Drawing.Point(495, 11);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(95, 37);
            this.btnSaveToFile.TabIndex = 330;
            this.btnSaveToFile.Text = "파일 저장";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 467);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(718, 186);
            this.panel3.TabIndex = 330;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gbComDataLoggingOption);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(694, 144);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "장치 데이터 로깅 기능";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "주기 저장";
            // 
            // cbLoggingInterval
            // 
            this.cbLoggingInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoggingInterval.FormattingEnabled = true;
            this.cbLoggingInterval.Items.AddRange(new object[] {
            "500ms",
            "1000ms",
            "2000ms",
            "5000ms"});
            this.cbLoggingInterval.Location = new System.Drawing.Point(79, 27);
            this.cbLoggingInterval.Name = "cbLoggingInterval";
            this.cbLoggingInterval.Size = new System.Drawing.Size(94, 20);
            this.cbLoggingInterval.TabIndex = 2;
            this.cbLoggingInterval.SelectedIndexChanged += new System.EventHandler(this.cbLoggingInterval_SelectedIndexChanged);
            // 
            // cbLogging_DIOChange
            // 
            this.cbLogging_DIOChange.AutoSize = true;
            this.cbLogging_DIOChange.Location = new System.Drawing.Point(196, 31);
            this.cbLogging_DIOChange.Name = "cbLogging_DIOChange";
            this.cbLogging_DIOChange.Size = new System.Drawing.Size(140, 16);
            this.cbLogging_DIOChange.TabIndex = 1;
            this.cbLogging_DIOChange.Text = "DIO 상태 변경시 저장";
            this.cbLogging_DIOChange.UseVisualStyleBackColor = true;
            this.cbLogging_DIOChange.Click += new System.EventHandler(this.cbLogging_DIOChange_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rbSpeedLogging_Start);
            this.panel4.Controls.Add(this.rbLogging_Stop);
            this.panel4.Controls.Add(this.rbComDataLogging_Start);
            this.panel4.Location = new System.Drawing.Point(14, 23);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(492, 36);
            this.panel4.TabIndex = 0;
            // 
            // rbLogging_Stop
            // 
            this.rbLogging_Stop.AutoSize = true;
            this.rbLogging_Stop.Checked = true;
            this.rbLogging_Stop.Location = new System.Drawing.Point(365, 10);
            this.rbLogging_Stop.Name = "rbLogging_Stop";
            this.rbLogging_Stop.Size = new System.Drawing.Size(75, 16);
            this.rbLogging_Stop.TabIndex = 1;
            this.rbLogging_Stop.TabStop = true;
            this.rbLogging_Stop.Text = "로깅 중지";
            this.rbLogging_Stop.UseVisualStyleBackColor = true;
            this.rbLogging_Stop.CheckedChanged += new System.EventHandler(this.rbLogging_Start_CheckedChanged);
            // 
            // rbComDataLogging_Start
            // 
            this.rbComDataLogging_Start.AutoSize = true;
            this.rbComDataLogging_Start.Location = new System.Drawing.Point(18, 10);
            this.rbComDataLogging_Start.Name = "rbComDataLogging_Start";
            this.rbComDataLogging_Start.Size = new System.Drawing.Size(139, 16);
            this.rbComDataLogging_Start.TabIndex = 0;
            this.rbComDataLogging_Start.Text = "통신데이터 로깅 시작";
            this.rbComDataLogging_Start.UseVisualStyleBackColor = true;
            this.rbComDataLogging_Start.CheckedChanged += new System.EventHandler(this.rbLogging_Start_CheckedChanged);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(0, 59);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(718, 408);
            this.listBox1.TabIndex = 331;
            // 
            // rbSpeedLogging_Start
            // 
            this.rbSpeedLogging_Start.AutoSize = true;
            this.rbSpeedLogging_Start.Location = new System.Drawing.Point(186, 10);
            this.rbSpeedLogging_Start.Name = "rbSpeedLogging_Start";
            this.rbSpeedLogging_Start.Size = new System.Drawing.Size(139, 16);
            this.rbSpeedLogging_Start.TabIndex = 2;
            this.rbSpeedLogging_Start.Text = "속도데이터 로깅 시작";
            this.rbSpeedLogging_Start.UseVisualStyleBackColor = true;
            // 
            // gbComDataLoggingOption
            // 
            this.gbComDataLoggingOption.Controls.Add(this.cbLogging_DIOChange);
            this.gbComDataLoggingOption.Controls.Add(this.label1);
            this.gbComDataLoggingOption.Controls.Add(this.cbLoggingInterval);
            this.gbComDataLoggingOption.Location = new System.Drawing.Point(14, 72);
            this.gbComDataLoggingOption.Name = "gbComDataLoggingOption";
            this.gbComDataLoggingOption.Size = new System.Drawing.Size(492, 57);
            this.gbComDataLoggingOption.TabIndex = 2;
            this.gbComDataLoggingOption.TabStop = false;
            this.gbComDataLoggingOption.Text = "통신데이터 로깅 옵션";
            // 
            // Form_ComDataDP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 653);
            this.ControlBox = false;
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "Form_ComDataDP";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "송수신 DATA";
            this.Load += new System.EventHandler(this.Form_ComDataDP_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.gbComDataLoggingOption.ResumeLayout(false);
            this.gbComDataLoggingOption.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSaveToFile;
        private System.Windows.Forms.CheckBox cbStopRefresh;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cbNoPollingData;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbLogging_DIOChange;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rbLogging_Stop;
        private System.Windows.Forms.RadioButton rbComDataLogging_Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbLoggingInterval;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox gbComDataLoggingOption;
        private System.Windows.Forms.RadioButton rbSpeedLogging_Start;
    }
}
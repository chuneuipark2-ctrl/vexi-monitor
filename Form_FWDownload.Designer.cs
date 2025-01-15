
namespace VEXI
{
    partial class Form_FWDownload
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
            this.btnFileOpen = new System.Windows.Forms.Button();
            this.btnStratDownload = new System.Windows.Forms.Button();
            this.btnStopDownload = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDownloadProgress = new System.Windows.Forms.Label();
            this.rbBoot = new System.Windows.Forms.RadioButton();
            this.rbApp = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rbAppNoCheck = new System.Windows.Forms.RadioButton();
            this.lb_Download = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.DWTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnFileOpen
            // 
            this.btnFileOpen.Location = new System.Drawing.Point(47, 36);
            this.btnFileOpen.Name = "btnFileOpen";
            this.btnFileOpen.Size = new System.Drawing.Size(103, 37);
            this.btnFileOpen.TabIndex = 1;
            this.btnFileOpen.TabStop = false;
            this.btnFileOpen.Text = "Open File";
            this.btnFileOpen.UseVisualStyleBackColor = true;
            this.btnFileOpen.Click += new System.EventHandler(this.btnFileOpen_Click);
            // 
            // btnStratDownload
            // 
            this.btnStratDownload.Location = new System.Drawing.Point(156, 36);
            this.btnStratDownload.Name = "btnStratDownload";
            this.btnStratDownload.Size = new System.Drawing.Size(103, 37);
            this.btnStratDownload.TabIndex = 2;
            this.btnStratDownload.TabStop = false;
            this.btnStratDownload.Text = "Download";
            this.btnStratDownload.UseVisualStyleBackColor = true;
            this.btnStratDownload.Click += new System.EventHandler(this.btnStratDownload_Click);
            // 
            // btnStopDownload
            // 
            this.btnStopDownload.Location = new System.Drawing.Point(265, 36);
            this.btnStopDownload.Name = "btnStopDownload";
            this.btnStopDownload.Size = new System.Drawing.Size(103, 37);
            this.btnStopDownload.TabIndex = 3;
            this.btnStopDownload.TabStop = false;
            this.btnStopDownload.Text = "Stop";
            this.btnStopDownload.UseVisualStyleBackColor = true;
            this.btnStopDownload.Click += new System.EventHandler(this.btnStopDownload_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(52, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "File Name : ";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(152, 157);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(23, 12);
            this.lblFileName.TabIndex = 5;
            this.lblFileName.Text = "---";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(54, 221);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(365, 24);
            this.progressBar1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(52, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Progress : ";
            // 
            // lblDownloadProgress
            // 
            this.lblDownloadProgress.AutoSize = true;
            this.lblDownloadProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblDownloadProgress.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblDownloadProgress.Location = new System.Drawing.Point(152, 188);
            this.lblDownloadProgress.Name = "lblDownloadProgress";
            this.lblDownloadProgress.Size = new System.Drawing.Size(23, 12);
            this.lblDownloadProgress.TabIndex = 8;
            this.lblDownloadProgress.Text = "0/0";
            // 
            // rbBoot
            // 
            this.rbBoot.AutoSize = true;
            this.rbBoot.Location = new System.Drawing.Point(371, 124);
            this.rbBoot.Name = "rbBoot";
            this.rbBoot.Size = new System.Drawing.Size(48, 16);
            this.rbBoot.TabIndex = 11;
            this.rbBoot.Text = "Boot";
            this.rbBoot.UseVisualStyleBackColor = true;
            // 
            // rbApp
            // 
            this.rbApp.AutoSize = true;
            this.rbApp.Checked = true;
            this.rbApp.Location = new System.Drawing.Point(154, 124);
            this.rbApp.Name = "rbApp";
            this.rbApp.Size = new System.Drawing.Size(45, 16);
            this.rbApp.TabIndex = 12;
            this.rbApp.TabStop = true;
            this.rbApp.Text = "App";
            this.rbApp.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(52, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "File Type :";
            // 
            // rbAppNoCheck
            // 
            this.rbAppNoCheck.AutoSize = true;
            this.rbAppNoCheck.Location = new System.Drawing.Point(220, 124);
            this.rbAppNoCheck.Name = "rbAppNoCheck";
            this.rbAppNoCheck.Size = new System.Drawing.Size(135, 16);
            this.rbAppNoCheck.TabIndex = 14;
            this.rbAppNoCheck.Text = "App (장치타입 무시)";
            this.rbAppNoCheck.UseVisualStyleBackColor = true;
            // 
            // lb_Download
            // 
            this.lb_Download.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_Download.ItemHeight = 12;
            this.lb_Download.Location = new System.Drawing.Point(12, 273);
            this.lb_Download.Name = "lb_Download";
            this.lb_Download.Size = new System.Drawing.Size(446, 148);
            this.lb_Download.TabIndex = 15;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // DWTimer
            // 
            this.DWTimer.Interval = 200;
            this.DWTimer.Tick += new System.EventHandler(this.DWTimer_Tick);
            // 
            // Form_FWDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 456);
            this.Controls.Add(this.lb_Download);
            this.Controls.Add(this.rbAppNoCheck);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rbApp);
            this.Controls.Add(this.rbBoot);
            this.Controls.Add(this.lblDownloadProgress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnStopDownload);
            this.Controls.Add(this.btnStratDownload);
            this.Controls.Add(this.btnFileOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_FWDownload";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "장치 프로그램 다운로드";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FWDownload_FormClosing);
            this.Load += new System.EventHandler(this.Form_FWDownload_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnFileOpen;
        private System.Windows.Forms.Button btnStratDownload;
        private System.Windows.Forms.Button btnStopDownload;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label lblDownloadProgress;
        private System.Windows.Forms.RadioButton rbBoot;
        private System.Windows.Forms.RadioButton rbApp;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbAppNoCheck;
        private System.Windows.Forms.ListBox lb_Download;
        public System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer DWTimer;
    }
}
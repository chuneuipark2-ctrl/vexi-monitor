
namespace VEXI
{
    partial class Form_RackInitial
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
            this.btn_AllClear = new System.Windows.Forms.Button();
            this.btn_AreaConfigClear = new System.Windows.Forms.Button();
            this.lbl_SRMWarning = new System.Windows.Forms.Label();
            this.lbl_RTVEMSWarning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_AllClear
            // 
            this.btn_AllClear.Location = new System.Drawing.Point(26, 25);
            this.btn_AllClear.Name = "btn_AllClear";
            this.btn_AllClear.Size = new System.Drawing.Size(251, 34);
            this.btn_AllClear.TabIndex = 307;
            this.btn_AllClear.Text = "위치 및 랙 설정 전체 초기화 ";
            this.btn_AllClear.UseVisualStyleBackColor = true;
            this.btn_AllClear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btn_AreaConfigClear
            // 
            this.btn_AreaConfigClear.Location = new System.Drawing.Point(31, 241);
            this.btn_AreaConfigClear.Name = "btn_AreaConfigClear";
            this.btn_AreaConfigClear.Size = new System.Drawing.Size(251, 27);
            this.btn_AreaConfigClear.TabIndex = 308;
            this.btn_AreaConfigClear.Text = "구간 설정 초기화";
            this.btn_AreaConfigClear.UseVisualStyleBackColor = true;
            this.btn_AreaConfigClear.Click += new System.EventHandler(this.btn_AreaConfigClear_Click);
            // 
            // lbl_SRMWarning
            // 
            this.lbl_SRMWarning.BackColor = System.Drawing.Color.Yellow;
            this.lbl_SRMWarning.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_SRMWarning.ForeColor = System.Drawing.Color.Red;
            this.lbl_SRMWarning.Location = new System.Drawing.Point(29, 62);
            this.lbl_SRMWarning.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_SRMWarning.Name = "lbl_SRMWarning";
            this.lbl_SRMWarning.Size = new System.Drawing.Size(248, 155);
            this.lbl_SRMWarning.TabIndex = 309;
            this.lbl_SRMWarning.Text = "! 주의 !\r\n\r\n[위치 및 랙 설정 전체 초기화]\r\n\r\n다음 설정이 모두 초기화 됩니다\r\n\r\n- 랙 설정\r\n- 셋 오프셋 설정\r\n- 스테이션 설" +
    "정\r\n- 금지랙 설정\r\n- 스페셜랙 설정";
            this.lbl_SRMWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_RTVEMSWarning
            // 
            this.lbl_RTVEMSWarning.BackColor = System.Drawing.Color.Yellow;
            this.lbl_RTVEMSWarning.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_RTVEMSWarning.ForeColor = System.Drawing.Color.Red;
            this.lbl_RTVEMSWarning.Location = new System.Drawing.Point(29, 62);
            this.lbl_RTVEMSWarning.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_RTVEMSWarning.Name = "lbl_RTVEMSWarning";
            this.lbl_RTVEMSWarning.Size = new System.Drawing.Size(248, 155);
            this.lbl_RTVEMSWarning.TabIndex = 310;
            this.lbl_RTVEMSWarning.Text = "! 주의 !\r\n\r\n[위치 및 랙 설정 전체 초기화]\r\n\r\n다음 설정이 모두 초기화 됩니다\r\n\r\n- 레일 주행 설정\r\n- 구간 설정\r\n- 스테이션 " +
    "설정";
            this.lbl_RTVEMSWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Form_RackInitial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 293);
            this.Controls.Add(this.lbl_RTVEMSWarning);
            this.Controls.Add(this.lbl_SRMWarning);
            this.Controls.Add(this.btn_AreaConfigClear);
            this.Controls.Add(this.btn_AllClear);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_RackInitial";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "위치 및 랙 설정 초기화";
            this.Load += new System.EventHandler(this.Form_OpInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_AllClear;
        private System.Windows.Forms.Button btn_AreaConfigClear;
        private System.Windows.Forms.Label lbl_SRMWarning;
        private System.Windows.Forms.Label lbl_RTVEMSWarning;
    }
}
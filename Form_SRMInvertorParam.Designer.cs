
namespace VEXI
{
    partial class Form_SRMInvertorParam
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
            this.cbParameter = new System.Windows.Forms.ComboBox();
            this.label39 = new System.Windows.Forms.Label();
            this.lblSt = new System.Windows.Forms.Label();
            this.btnload = new System.Windows.Forms.Button();
            this.edCtrl = new System.Windows.Forms.TextBox();
            this.btnSet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbParameter
            // 
            this.cbParameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParameter.FormattingEnabled = true;
            this.cbParameter.Items.AddRange(new object[] {
            "[주행] Speed P Gain (단위 : 1/s / 정밀도 : 0.001 1/s)",
            "[주행] Speed I Gain (단위 : ms / 정밀도 : 0.001 ms)",
            "[주행] Lag err (단위 : M / 정밀도 : 0.000001 M)",
            "[주행] Speed monitoring (단위 : ms / 정밀도 : 1ms)",
            "[주행] Motor Torque  (단위 : % / 정밀도 : 0.1%)",
            "[주행] DI01 (정수값)",
            "[주행] DI02 (정수값)",
            "[주행] DI03 (정수값)",
            "[주행] DO01 (정수값)",
            "[주행] DO02 (정수값)",
            "[주행] DO03 (정수값)",
            "[승강] Speed P Gain (단위 : 1/s / 정밀도 : 0.001 1/s)",
            "[승강] Speed I Gain (단위 : ms / 정밀도 : 0.001 ms)",
            "[승강] Lag err (단위 : M / 정밀도 : 0.00001 M)",
            "[승강] Speed monitoring (단위 : ms / 정밀도 : 1ms)",
            "[승강] Motor Torque  (단위 : % / 정밀도 : 0.1%)",
            "[승강] DI01 (정수값)",
            "[승강] DI02 (정수값)",
            "[승강] DI03 (정수값)",
            "[승강] DO01 (정수값)",
            "[승강] DO02 (정수값)",
            "[승강] DO03 (정수값)",
            "[포크] Speed P Gain (단위 : 1/s / 정밀도 : 0.001 1/s)",
            "[포크] Speed I Gain (단위 : ms / 정밀도 : 0.001 ms)",
            "[포크] Lag err (단위 : M / 정밀도 : 0.000001 M)",
            "[포크] Speed monitoring (단위 : ms / 정밀도 : 1ms)",
            "[포크] Motor Torque  (단위 : % / 정밀도 : 0.1%)",
            "[포크] DI01 (정수값)",
            "[포크] DI02 (정수값)",
            "[포크] DI03 (정수값)",
            "[포크] DO01 (정수값)",
            "[포크] DO02 (정수값)",
            "[포크] DO03 (정수값)"});
            this.cbParameter.Location = new System.Drawing.Point(209, 20);
            this.cbParameter.Name = "cbParameter";
            this.cbParameter.Size = new System.Drawing.Size(368, 20);
            this.cbParameter.TabIndex = 1181;
            this.cbParameter.SelectedIndexChanged += new System.EventHandler(this.cbParameter_SelectedIndexChanged);
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.Black;
            this.label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label39.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label39.ForeColor = System.Drawing.Color.White;
            this.label39.Location = new System.Drawing.Point(35, 19);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(171, 21);
            this.label39.TabIndex = 1266;
            this.label39.Text = "파라미터 항목 선택";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSt
            // 
            this.lblSt.BackColor = System.Drawing.Color.White;
            this.lblSt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSt.ForeColor = System.Drawing.Color.Black;
            this.lblSt.Location = new System.Drawing.Point(209, 43);
            this.lblSt.Name = "lblSt";
            this.lblSt.Size = new System.Drawing.Size(146, 21);
            this.lblSt.TabIndex = 1267;
            this.lblSt.Text = "---";
            this.lblSt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSt.DoubleClick += new System.EventHandler(this.lblSt_DoubleClick);
            // 
            // btnload
            // 
            this.btnload.Location = new System.Drawing.Point(358, 42);
            this.btnload.Name = "btnload";
            this.btnload.Size = new System.Drawing.Size(75, 23);
            this.btnload.TabIndex = 1268;
            this.btnload.TabStop = false;
            this.btnload.Text = "읽기";
            this.btnload.UseVisualStyleBackColor = true;
            this.btnload.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // edCtrl
            // 
            this.edCtrl.Location = new System.Drawing.Point(209, 68);
            this.edCtrl.Name = "edCtrl";
            this.edCtrl.Size = new System.Drawing.Size(146, 21);
            this.edCtrl.TabIndex = 1269;
            this.edCtrl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(358, 66);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 1270;
            this.btnSet.TabStop = false;
            this.btnSet.Text = "쓰기";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // Form_SRMInvertorParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 104);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.edCtrl);
            this.Controls.Add(this.btnload);
            this.Controls.Add(this.lblSt);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.cbParameter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_SRMInvertorParam";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SRM 인버터 파라미터 제어";
            this.Load += new System.EventHandler(this.Form_SRMConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbParameter;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label lblSt;
        private System.Windows.Forms.Button btnload;
        private System.Windows.Forms.TextBox edCtrl;
        private System.Windows.Forms.Button btnSet;
    }
}

namespace VEXI
{
    partial class Form_MovexWCSMemoryMap
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
            this.lbl_WCS_RxTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lv_WCSDataTotal = new InheritedListView.MyListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv_DevDataTotal = new InheritedListView.MyListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_WCS_TxTime2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_WCS_TxTime1 = new System.Windows.Forms.Label();
            this.lv_DevData = new InheritedListView.MyListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lv_WCSData = new InheritedListView.MyListView();
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.rbEMS1 = new System.Windows.Forms.RadioButton();
            this.rbEMS2 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_WCS_RxTime
            // 
            this.lbl_WCS_RxTime.BackColor = System.Drawing.Color.Snow;
            this.lbl_WCS_RxTime.ForeColor = System.Drawing.Color.Black;
            this.lbl_WCS_RxTime.Location = new System.Drawing.Point(15, 33);
            this.lbl_WCS_RxTime.Name = "lbl_WCS_RxTime";
            this.lbl_WCS_RxTime.Size = new System.Drawing.Size(194, 24);
            this.lbl_WCS_RxTime.TabIndex = 740;
            this.lbl_WCS_RxTime.Text = "0";
            this.lbl_WCS_RxTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(15, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 24);
            this.label2.TabIndex = 739;
            this.label2.Text = "수신시간";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.rbEMS2);
            this.panel1.Controls.Add(this.rbEMS1);
            this.panel1.Controls.Add(this.lv_WCSDataTotal);
            this.panel1.Controls.Add(this.lv_DevDataTotal);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lbl_WCS_TxTime2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbl_WCS_TxTime1);
            this.panel1.Controls.Add(this.lv_DevData);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.lv_WCSData);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbl_WCS_RxTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1380, 825);
            this.panel1.TabIndex = 741;
            // 
            // lv_WCSDataTotal
            // 
            this.lv_WCSDataTotal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lv_WCSDataTotal.FullRowSelect = true;
            this.lv_WCSDataTotal.GridLines = true;
            this.lv_WCSDataTotal.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_WCSDataTotal.HideSelection = false;
            this.lv_WCSDataTotal.Location = new System.Drawing.Point(206, 110);
            this.lv_WCSDataTotal.Name = "lv_WCSDataTotal";
            this.lv_WCSDataTotal.Size = new System.Drawing.Size(476, 697);
            this.lv_WCSDataTotal.TabIndex = 1253;
            this.lv_WCSDataTotal.UseCompatibleStateImageBehavior = false;
            this.lv_WCSDataTotal.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Addr";
            this.columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "DataType";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Item";
            this.columnHeader5.Width = 220;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Value";
            this.columnHeader6.Width = 80;
            // 
            // lv_DevDataTotal
            // 
            this.lv_DevDataTotal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader11,
            this.columnHeader8,
            this.columnHeader12});
            this.lv_DevDataTotal.FullRowSelect = true;
            this.lv_DevDataTotal.GridLines = true;
            this.lv_DevDataTotal.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_DevDataTotal.HideSelection = false;
            this.lv_DevDataTotal.Location = new System.Drawing.Point(880, 110);
            this.lv_DevDataTotal.Name = "lv_DevDataTotal";
            this.lv_DevDataTotal.Size = new System.Drawing.Size(476, 697);
            this.lv_DevDataTotal.TabIndex = 1251;
            this.lv_DevDataTotal.UseCompatibleStateImageBehavior = false;
            this.lv_DevDataTotal.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Addr";
            this.columnHeader7.Width = 70;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "DataType";
            this.columnHeader11.Width = 80;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Item";
            this.columnHeader8.Width = 220;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Value";
            this.columnHeader12.Width = 80;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(415, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(194, 24);
            this.label6.TabIndex = 1245;
            this.label6.Text = "송신시간 (0x81)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS_TxTime2
            // 
            this.lbl_WCS_TxTime2.BackColor = System.Drawing.Color.Snow;
            this.lbl_WCS_TxTime2.ForeColor = System.Drawing.Color.Black;
            this.lbl_WCS_TxTime2.Location = new System.Drawing.Point(415, 33);
            this.lbl_WCS_TxTime2.Name = "lbl_WCS_TxTime2";
            this.lbl_WCS_TxTime2.Size = new System.Drawing.Size(194, 24);
            this.lbl_WCS_TxTime2.TabIndex = 1244;
            this.lbl_WCS_TxTime2.Text = "0";
            this.lbl_WCS_TxTime2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(215, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 24);
            this.label3.TabIndex = 1242;
            this.label3.Text = "송신시간 (0x80)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WCS_TxTime1
            // 
            this.lbl_WCS_TxTime1.BackColor = System.Drawing.Color.Snow;
            this.lbl_WCS_TxTime1.ForeColor = System.Drawing.Color.Black;
            this.lbl_WCS_TxTime1.Location = new System.Drawing.Point(215, 33);
            this.lbl_WCS_TxTime1.Name = "lbl_WCS_TxTime1";
            this.lbl_WCS_TxTime1.Size = new System.Drawing.Size(194, 24);
            this.lbl_WCS_TxTime1.TabIndex = 1243;
            this.lbl_WCS_TxTime1.Text = "0";
            this.lbl_WCS_TxTime1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lv_DevData
            // 
            this.lv_DevData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lv_DevData.FullRowSelect = true;
            this.lv_DevData.GridLines = true;
            this.lv_DevData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_DevData.HideSelection = false;
            this.lv_DevData.Location = new System.Drawing.Point(695, 110);
            this.lv_DevData.Name = "lv_DevData";
            this.lv_DevData.Size = new System.Drawing.Size(181, 697);
            this.lv_DevData.TabIndex = 1241;
            this.lv_DevData.UseCompatibleStateImageBehavior = false;
            this.lv_DevData.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Addr";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 90;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(693, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(663, 22);
            this.label1.TabIndex = 1238;
            this.label1.Text = "장치 Data";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Black;
            this.label12.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(17, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(663, 22);
            this.label12.TabIndex = 1237;
            this.label12.Text = "WCS Data";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lv_WCSData
            // 
            this.lv_WCSData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader14,
            this.columnHeader15});
            this.lv_WCSData.FullRowSelect = true;
            this.lv_WCSData.GridLines = true;
            this.lv_WCSData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_WCSData.HideSelection = false;
            this.lv_WCSData.Location = new System.Drawing.Point(19, 110);
            this.lv_WCSData.Name = "lv_WCSData";
            this.lv_WCSData.Size = new System.Drawing.Size(181, 697);
            this.lv_WCSData.TabIndex = 1234;
            this.lv_WCSData.UseCompatibleStateImageBehavior = false;
            this.lv_WCSData.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Addr";
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Value";
            this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader15.Width = 90;
            // 
            // rbEMS1
            // 
            this.rbEMS1.AutoSize = true;
            this.rbEMS1.Checked = true;
            this.rbEMS1.Location = new System.Drawing.Point(21, 64);
            this.rbEMS1.Name = "rbEMS1";
            this.rbEMS1.Size = new System.Drawing.Size(94, 16);
            this.rbEMS1.TabIndex = 1254;
            this.rbEMS1.TabStop = true;
            this.rbEMS1.Text = "공용 Version";
            this.rbEMS1.UseVisualStyleBackColor = true;
            this.rbEMS1.CheckedChanged += new System.EventHandler(this.rbEMS1_CheckedChanged);
            // 
            // rbEMS2
            // 
            this.rbEMS2.AutoSize = true;
            this.rbEMS2.Location = new System.Drawing.Point(132, 64);
            this.rbEMS2.Name = "rbEMS2";
            this.rbEMS2.Size = new System.Drawing.Size(86, 16);
            this.rbEMS2.TabIndex = 742;
            this.rbEMS2.Text = "AB Version";
            this.rbEMS2.UseVisualStyleBackColor = true;
            this.rbEMS2.CheckedChanged += new System.EventHandler(this.rbEMS1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(676, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 26);
            this.button1.TabIndex = 1255;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // Form_MovexWCSMemoryMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 825);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_MovexWCSMemoryMap";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Movex WCS Memory Map";
            this.Activated += new System.EventHandler(this.Form_InvertorSt_Activated);
            this.Deactivate += new System.EventHandler(this.Form_InvertorSt_Deactivate);
            this.Load += new System.EventHandler(this.Form_InvertorSt_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_WCS_RxTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private InheritedListView.MyListView lv_WCSData;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private InheritedListView.MyListView lv_DevData;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_WCS_TxTime2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_WCS_TxTime1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private InheritedListView.MyListView lv_DevDataTotal;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private InheritedListView.MyListView lv_WCSDataTotal;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.RadioButton rbEMS2;
        private System.Windows.Forms.RadioButton rbEMS1;
        private System.Windows.Forms.Button button1;
    }
}
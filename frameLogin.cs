using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VEXI
{
    public partial class frameLogin : Form
    {
        public frameLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {

            if (((edID.Text.ToUpper().Trim() == "MOVEX1234") && (edPW.Text.Trim() == "1")) || ((edID.Text.ToUpper().Trim() == "ARTWARE") && (edPW.Text.Trim() == "1")))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else 
            {
                MessageBox.Show("인증정보가 맞지 않습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void edPW_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Login();
        }

        private void edID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) edPW.Focus();
        }
    }
}

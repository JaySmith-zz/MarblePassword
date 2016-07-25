using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarblePassword.Win
{
    public partial class PasswordForm : Form
    {
        public string Password
        {
            get
            {
                return textBoxPassword1.Text;
            }
        }


        public PasswordForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            labelMessage.Text = string.Empty;

            if (textBoxPassword1.Text == textBoxPassword2.Text)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                labelMessage.Text = "Password don't match, please try again.";
            }
        }
    }
}

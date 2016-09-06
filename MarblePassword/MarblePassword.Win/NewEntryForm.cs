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
    public partial class NewEntryForm : Form
    {
        public NewEntryForm()
        {
            InitializeComponent();
        }

        public Entry GetEntry()
        {
            return new Entry
            {
                Title = textBoxTitle.Text,
                Username = textBoxUsername.Text,
                Password = textBoxPassword1.Text,
                Url = textBoxWebSite.Text,
                Notes = richTextBoxComments.Text
            };
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (textBoxPassword1.Text != textBoxPassword2.Text)
            {
                labelMessage.Text = "Password do not match!";
                return;
            }

            Close();
        }
    }
}

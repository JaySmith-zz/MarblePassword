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
    public partial class OpenDatabaseForm : Form
    {
        private PasswordDatabase _db;

        public OpenDatabaseForm(PasswordDatabase db)
        {
            _db = db;
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (_db.Password == textBoxPassword.Text)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                labelError.Text = "Invalid Password, please try again.";
            }
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            labelError.Text = string.Empty;
            textBoxPassword.SelectAll();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

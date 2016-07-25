/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 8/25/2015
 * Time: 9:21 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MarblePassword.Win
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        private PasswordDatabase _data;
        private string _filename;

        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        private void toolStripEntryAdd_Click(object sender, EventArgs e)
        {
            var form = new NewEntryForm();

            form.ShowDialog();
        }

        private void toolStripButtonNewDatabase_Click(object sender, EventArgs e)
        {
            var fileDialog = new SaveFileDialog();

            fileDialog.Filter = "password database (*.mpd)|*.mpd|All Files (*.*)|*.*";
            fileDialog.FilterIndex = 2;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                _filename = fileDialog.FileName;
            }

            var dbSetupForm = new PasswordForm();


        }
    }
}

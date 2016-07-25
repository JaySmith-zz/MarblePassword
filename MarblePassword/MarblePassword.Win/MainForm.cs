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
            _data = new PasswordDatabase();
        }

        private void toolStripEntryAdd_Click(object sender, EventArgs e)
        {
            var form = new NewEntryForm();

            form.ShowDialog();
        }

        public void LoadData()
        {
            dataGridView.DataSource = _data.Items;

        }


        private void toolStripButtonNewDatabase_Click(object sender, EventArgs e)
        {
            var fileDialog = new SaveFileDialog();

            fileDialog.Filter = "password database (*.mpd)|*.mpd|All Files (*.*)|*.*";
            fileDialog.DefaultExt = "*.mpd";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                _filename = fileDialog.FileName;
            }

            var dbSetupForm = new PasswordForm();
            if (dbSetupForm.ShowDialog() == DialogResult.OK)
            {
                _data.Password = dbSetupForm.Password;
                _data.Items.Add(new Entry
                {
                    Title = "Sample",
                    Notes = "Notes go here",
                    Created = DateTime.Now,
                    Modified = DateTime.Now
                });

                var path = System.IO.Path.GetDirectoryName(_filename);
                var fileName = System.IO.Path.GetFileName(_filename);
                using (var db = NanoApi.JsonFile<PasswordDatabase>.GetInstance(path, fileName))
                {
                    db.Insert(_data);
                }
            }


        }
    }
}

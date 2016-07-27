/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 8/25/2015
 * Time: 9:21 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using MarblePassword.Win.Services;
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
        private PasswordDatabaseRepository _repo;
        private PasswordDatabase _data;

        public MainForm()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty( Globals.CurrentPasswordDb))
            {
                _repo = new PasswordDatabaseRepository(Globals.CurrentPasswordDb);
                _data = _repo.Read();

                var loginDialog = new OpenDatabaseForm(_data);

                if (loginDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            //else
            //{
            //    throw new NotImplementedException();
            //}
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
                var _filename = fileDialog.FileName;
                _repo = new PasswordDatabaseRepository(_filename);
                _data = new PasswordDatabase();

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

                    _repo.Save(_data);
                    Globals.CurrentPasswordDb = _filename;
                }
            }

            LoadData();
        }
    }
}

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

        public void LoadData()
        {
            toolStripButtonSave.Enabled = true;
            dataGridView.Enabled = true;
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

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            _data.Items.Clear();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var item = new Entry();

                item.Title = row.Cells["titleDataGridViewTextBoxTitle"].Value.ToString();
                item.Username = row.Cells["Username"].Value.ToString();
                item.Password = row.Cells["Password"].Value.ToString();
                item.Url = row.Cells["Url"].Value.ToString();
                item.Notes = row.Cells["Notes"].Value.ToString();

                _data.Items.Add(item);
            }

            _repo.Save(_data);
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();

            fileDialog.Filter = "password database (*.mpd)|*.mpd|All Files (*.*)|*.*";
            fileDialog.DefaultExt = "*.mpd";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var _filename = fileDialog.FileName;
                _repo = new PasswordDatabaseRepository(_filename);
                _data = _repo.Read();

                var loginDialog = new OpenDatabaseForm(_data);
                if (loginDialog.ShowDialog() == DialogResult.OK)
                {
                    Globals.CurrentPasswordDb = _filename;

                    LoadData();
                }
            }
        }
    }
}

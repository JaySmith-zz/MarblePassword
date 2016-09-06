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
using System.Linq;
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

            if (!string.IsNullOrEmpty(Globals.CurrentPasswordDb))
            {
                _repo = new PasswordDatabaseRepository(Globals.CurrentPasswordDb);
                _data = _repo.Read();

                var loginDialog = new OpenDatabaseForm(_data);

                if (loginDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        public void LoadData()
        {
            toolStripButtonSave.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            closeToolStripMenuItem.Enabled = true;

            dataGridView.Enabled = true;
            dataGridView.DataSource = _data.Items;

            // Load ListView
            foreach (var item in _data.Items)
            {
                var listItem = new ListViewItem(new[] { item.Title, item.Title, item.Username, item.Password, item.Url });
                listItem.Tag = item;

                listView1.Items.Add(listItem);
            }
        }

        private void CreateDatabase()
        {
            Globals.CurrentPasswordDb = SaveAsDatabase();

            _repo = new PasswordDatabaseRepository(Globals.CurrentPasswordDb);
            //_data = _repo.Read();
            _data = new PasswordDatabase();

            var dbSetupForm = new PasswordForm();
            if (dbSetupForm.ShowDialog() == DialogResult.OK)
            {
                _data.Password = dbSetupForm.Password;
                _data.Items.Add(new Entry
                {
                    Title = "Sample",
                    Notes = "Notes go here"
                    //Created = DateTime.Now,
                    //Modified = DateTime.Now
                });

                SaveDatabase();
            }

            LoadData();
        }

        private void OpenDatabase()
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

        private void CloseDatabase()
        {
            toolStripButtonSave.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            closeToolStripMenuItem.Enabled = false;

            dataGridView.DataSource = null;
            dataGridView.Refresh();
        }

        private void SearchDatabase(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
                dataGridView.DataSource = _data.Items;
            else
            {
                var searchItems = _data.Items.Where(x => x.Title.Contains(searchValue));
                dataGridView.DataSource = searchItems;
            }
        }

        private void SaveDatabase()
        {
            _repo.Save(_data);
        }

        private string SaveAsDatabase()
        {
            var fileDialog = new SaveFileDialog();

            fileDialog.Filter = "password database (*.mpd)|*.mpd|All Files (*.*)|*.*";
            fileDialog.DefaultExt = "*.mpd";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;

            string selectedFilename = null;
            if (fileDialog.ShowDialog() == DialogResult.OK)
                selectedFilename = fileDialog.FileName;

            return selectedFilename;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButtonNewDatabase_Click(object sender, EventArgs e)
        {
            CreateDatabase();
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            _data.Items.Clear();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var item = new Entry();

                var value = row.Cells[0].Value.ToString();
                item.Title = row.Cells["titleDataGridViewTextBoxColumn"].Value.ToString();
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
            OpenDatabase();
        }



        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDatabase();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDatabase();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseDatabase();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDatabase();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();

            fileDialog.Filter = "KeePass CSV (1.x) (*.csv)|*.csv|All Files (*.*)|*.*";
            fileDialog.DefaultExt = "*.csv";
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

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            SearchDatabase(toolStripTextBoxSearchText.Text.Trim());
        }

        private void toolStripTextBoxSearchText_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void toolStripTextBoxSearchText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down
                SearchDatabase(toolStripTextBoxSearchText.Text.Trim());
            }
        }

        private void toolStripButtonAddNew_Click(object sender, EventArgs e)
        {
            var newForm = new NewEntryForm();

            if (newForm.ShowDialog() == DialogResult.OK)
            {
                _data.Items.Add(newForm.GetEntry());
                _repo.Save(_data);

                LoadData();
            }
        }
    }
}

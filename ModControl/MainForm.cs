using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModControl
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ExitToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void OpenToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                PopulateListView(this.listView, folderBrowserDialog.SelectedPath);
            }
        }

        private static void PopulateListView(ListView listView, String modStorageDirectory)
        {
            DirectoryInfo dinfo = new DirectoryInfo(modStorageDirectory);
            FileInfo[] Files = dinfo.GetFiles("*.zip");
            foreach (FileInfo file in Files)
            {
                listView.Items.Add(file.Name);
            }
        }

        private void SplitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FolderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
    }
}

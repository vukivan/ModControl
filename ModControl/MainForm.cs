using System;
using System.Collections;
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
            FileInfo [] ModFiles = dinfo.GetFiles("*.zip");
            foreach (FileInfo file in ModFiles)
            {
                Mod mod = new Mod(modStorageDirectory, file.Name);
                ModsList.AddLast(mod);
                ListViewItem item = new(new[] { mod.GetModTitle(), mod.GetModVersion(), mod.GetModAuthor()});
                listView.Items.Add(item);
            }
        }

        // ColumnClick event handler.
        private void ColumnClick(object o, ColumnClickEventArgs e)
        {
            // Set the ListViewItemSorter property to a new ListViewItemComparer 
            // object. Setting this property immediately sorts the 
            // ListView using the ListViewItemComparer object.
            this.listView.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }

        // Implements the manual sorting of items by columns.
        class ListViewItemComparer : IComparer
        {
            private int col;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
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

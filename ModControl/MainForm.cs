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
                ListViewItem item = new(new[] { mod.GetModTitle(), mod.GetModAuthor(), mod.GetModVersion() });
                listView.Items.Add(item);
            }
        }

        // ColumnClick event handler.
        private void ColumnClick(object o, ColumnClickEventArgs e)
        {
            // Set the ListViewItemSorter property to a new ListViewItemComparer 
            // object. Setting this property immediately sorts the 
            // ListView using the ListViewItemComparer object.
            if (this.listView.Sorting.Equals(SortOrder.Ascending))
            {
                this.listView.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Descending);
                this.listView.Sorting = SortOrder.Descending;
            }
            else
            {
                this.listView.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Ascending);
                this.listView.Sorting = SortOrder.Ascending;
            }
        }

        // Implements the manual sorting of items by columns.
        class ListViewItemComparer : IComparer
        {
            private int col;
            private SortOrder order;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column, SortOrder order)
            {
                col = column;
                this.order = order;
            }
            public int Compare(object x, object y)
            {
                if (order.Equals(SortOrder.Ascending))
                    return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                else
                    return (-1)*String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }

        private void txt_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (listView.Items.Count>0 && e.KeyCode == Keys.Return)
            {
                // Call FindItemWithText with the contents of the textbox.
                ListViewItem foundItem =
                    listView.FindItemWithText(searchBox.Text, false, 0, true);
                if (foundItem != null)
                {
                    listView.TopItem = foundItem;
                }
            }
            
        }

        private void FolderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
    }
}

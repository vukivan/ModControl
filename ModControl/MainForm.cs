using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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
            FileInfo[] ModFiles = dinfo.GetFiles("*.zip");
            foreach (FileInfo file in ModFiles)
            {
                Mod mod = new Mod(GetModInfo(modStorageDirectory, file.Name));
                ModsList.AddLast(mod);
                ListViewItem item = new(new[] { mod.GetModTitle(), mod.GetModAuthor(), mod.GetModVersion(), mod.GetModStatusString()});
                listView.Items.Add(item);
            }
        }

        private static ModProperties GetModInfo(string ModStorageDirectory, string FileName)
        {
            string title = null;
            string author = null;
            string version = null;
            string icon = null;
            XDocument modDescXml;
            //Open Zip, get info.
            using ZipArchive archive = ZipFile.Open(ModStorageDirectory + "/" + FileName, ZipArchiveMode.Read);
            ZipArchiveEntry entry = archive.GetEntry("modDesc.xml");
            if (entry != null)
            {
                //System.Diagnostics.Debug.WriteLine("Loading mod:" + FileName);
                using (StreamReader reader = new StreamReader(entry.Open()))
                    modDescXml = XDocument.Load(reader);
                author = modDescXml.Element("modDesc").Element("author").Value.Trim();
                XElement titleXElement = modDescXml.Element("modDesc").Element("title").Element("en");
                if (titleXElement != null)
                {
                    title = titleXElement.Value.Trim();
                }
                else
                {
                    title = modDescXml.Element("modDesc").Element("title").Value.Trim();
                }
                icon = modDescXml.Element("modDesc").Element("iconFilename").Value.Trim();
                version = modDescXml.Element("modDesc").Element("version").Value.Trim();

            }
            else
            {
                // Report Error with Filename and reason "modDesc.xml" not found.
            }

            return new ModProperties(FileName, title, author, version, icon);
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
            //TODO: refactor this so that it actually filters
            //Save list somewhere else, not in this method
            //restore backup to view
            //delete everything that doesn't mach criteria.
            //if search empty, just restore backup
            if (listView.Items.Count>0 && e.KeyCode == Keys.Return)
            {
                // Call FindItemWithText with the contents of the textbox.
                ListViewItem foundItem =
                    listView.FindItemWithText(searchBox.Text, true, 0, true);
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

    enum ModStatus
    {
        Unknown = -1,
        Inactive, //Exists in mod storage => Activate - Copy to mod directory.
        Active, //Exists in both mod storage, and mod directory. Same version in both. => Deactivate - remove from mod directory
        New, //Exists only in mod directory, can be backed up. => Backup - copy to mod storage.
        Update, //Version in mod storage is newer => Copy to mod directory, confirm overwrite
        Backup //Version in mod directory is newer => Backup - ccopy to mod storage, confirm overwrite
    }
}

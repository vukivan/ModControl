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
        private static readonly string defaultModDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"My Games\FarmingSimulator2019\mods\");
        private static LinkedList<Mod> modsList = new LinkedList<Mod>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void ExitToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void LoadToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            if (Directory.Exists(defaultModDirectory))
            {
                LoadMods();
                if (modsList.Count > 0)
                {
                    this.reloadToolStripMenuItem.Enabled = true;
                    this.activateToolStripMenuItem.Enabled = true;
                    this.deactivateToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                Directory.CreateDirectory(defaultModDirectory);
                MessageBox.Show("Mods directory does not exist, ModControl will create it.");
                this.reloadToolStripMenuItem.Enabled = true;
            }

        }

        private void ReloadToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            LoadMods();
        }
        private void SelectAllToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            listView.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = true);
        }

        private void DeselectAllToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            listView.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = false);
        }

        private void LoadMods()
        {
            this.listView.Items.Clear();
            modsList.Clear();
            DirectoryInfo directoryInfo = new(defaultModDirectory);
            if (!directoryInfo.Exists)
                MessageBox.Show("Mod directory does not exist, aborting mod loading!");

            FileInfo[] activatedModFiles = directoryInfo.GetFiles("*.zip");
            if (activatedModFiles.Length > 0)
            {
                foreach (FileInfo file in activatedModFiles)
                {
                    Mod mod = new(GetModInfo(defaultModDirectory, file.Name));
                    mod.SetModStatus(ModStatus.Active);
                    AddMod(mod);
                }
            }
            FileInfo[] deactivatedModFiles = directoryInfo.GetFiles("*.zip.deactivated");
            if (deactivatedModFiles.Length > 0)
            {
                foreach (FileInfo file in deactivatedModFiles)
                {
                    Mod mod = new(GetModInfo(defaultModDirectory, file.Name));
                    mod.SetModStatus(ModStatus.Inactive);
                    AddMod(mod);
                }
            }

        }

        private void AddMod(Mod mod)
        {
            modsList.AddLast(mod);
            ListViewItem item = new(new[] { mod.GetModTitle(), mod.GetModAuthor(), mod.GetModVersion(), mod.GetModStatusString(), mod.GetFileName() });
            item.ToolTipText = mod.GetFileName();
            this.listView.Items.Add(item);
        }

        private void ActivateToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            ListView.CheckedListViewItemCollection checkedMods = listView.CheckedItems;
            if (checkedMods.Count > 0)
            {
                foreach (ListViewItem checkedItem in checkedMods)
                {
                    ActivateMod(checkedItem);
                }
            }
            else
            {
                MessageBox.Show("No items selected!");
            }
            
        }


        private void DeactivateToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            ListView.CheckedListViewItemCollection checkedMods = listView.CheckedItems;
            if (checkedMods.Count > 0)
            {
                foreach (ListViewItem checkedItem in checkedMods)
                {
                    DeactivateMod(checkedItem);
                }
            }
            else
            {
                MessageBox.Show("No items selected!");
            }

        }

        private static Mod FindModByFileName(string name, LinkedList<Mod> list)
        {
            foreach(Mod mod in list)
            {
                if (mod.GetFileName().Equals(name))
                {
                    return mod;
                }
            }
            return null;
        }

        private void ActivateMod (ListViewItem item)
        {
            Mod mod = FindModByFileName(item.SubItems[4].Text, modsList);
            if (mod != null && mod.GetModStatus() == ModStatus.Inactive && File.Exists(Path.Combine(defaultModDirectory, mod.GetFileName())))
            {
                string newModName = mod.GetFileName().Substring(0, mod.GetFileName().LastIndexOf(".deactivated"));
                File.Move(Path.Combine(defaultModDirectory, mod.GetFileName()), Path.Combine(defaultModDirectory, newModName), false);
                mod.SetModFileName(newModName);
                mod.SetModStatus(ModStatus.Active);
                item.SubItems[3].Text = mod.GetModStatusString();
                item.SubItems[4].Text = newModName;
            }
        }

        private void DeactivateMod(ListViewItem item)
        {
            Mod mod = FindModByFileName(item.SubItems[4].Text, modsList);
            if (mod != null && mod.GetModStatus() == ModStatus.Active && File.Exists(Path.Combine(defaultModDirectory, mod.GetFileName())))
            {
                string newModName = mod.GetFileName() + ".deactivated";
                File.Move(Path.Combine(defaultModDirectory, mod.GetFileName()), Path.Combine(defaultModDirectory, newModName), false);
                mod.SetModFileName(newModName);
                mod.SetModStatus(ModStatus.Inactive);
                item.SubItems[3].Text = mod.GetModStatusString();
                item.SubItems[4].Text = newModName;
            }
        }

        private ModProperties GetModInfo(string modDirectory, string fileName)
        {
            string title;
            string author;
            string version;
            string icon;
            XDocument modDescXml;
            //Open Zip, get info.
            using ZipArchive archive = ZipFile.Open(modDirectory + "/" + fileName, ZipArchiveMode.Read);
            ZipArchiveEntry entry = archive.GetEntry("modDesc.xml");
            if (entry != null)
            {
                //System.Diagnostics.Debug.WriteLine("Loading mod:" + FileName);
                using (StreamReader reader = new StreamReader(entry.Open()))
                {
                    //Handle modDesc validation here!
                    modDescXml = XDocument.Load(reader);
                }
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
                return new ModProperties(fileName, title, author, version, icon);

            }
            else
            {
                MessageBox.Show("modDesc.xml not found in " + fileName);
                return null;
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
            private int column;
            private SortOrder order;
            public ListViewItemComparer()
            {
                this.column = 0;
            }
            public ListViewItemComparer(int column, SortOrder order)
            {
                this.column = column;
                this.order = order;
            }
            public int Compare(object x, object y)
            {
                if (order.Equals(SortOrder.Ascending))
                    return String.Compare(((ListViewItem)x).SubItems[this.column].Text, ((ListViewItem)y).SubItems[this.column].Text);
                else
                    return (-1)*String.Compare(((ListViewItem)x).SubItems[this.column].Text, ((ListViewItem)y).SubItems[this.column].Text);
            }
        }

        private void Txt_Search_KeyDown(object sender, KeyEventArgs e)
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
    }

    enum ModStatus
    {
        Unknown = -1,
        Inactive,
        Active,
        New, //not used
        Update, //not used
        Backup //not used
    }
}

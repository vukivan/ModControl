using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace ModControl
{
    public partial class MainForm : Form
    {
        private static readonly string defaultModDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"My Games\FarmingSimulator2019\mods\");
        private static LinkedList<Mod> modsList = new LinkedList<Mod>();
        private static bool needToReload = false;
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
                    this.deactivateAllToolStripMenuItem.Enabled = true;
                    this.packageToolStripMenuItem.Enabled = true;
                    this.selectAllToolStripMenuItem.Enabled = true;
                    this.deselectAllToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                DialogResult result = MessageBox.Show("Mods directory does not exist, ModControl will create " + defaultModDirectory, "Mods directory missing", buttons);
                if (result == DialogResult.OK)
                {
                    Directory.CreateDirectory(defaultModDirectory);
                }
            }

        }

        private void ReloadToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            LoadMods();
        }
        private void SelectAllToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            modListView.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = true);
        }

        private void DeselectAllToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            modListView.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = false);
        }

        private void LoadMods()
        {
            this.modListView.Items.Clear();
            modsList.Clear();
            DirectoryInfo directoryInfo = new(defaultModDirectory);

            FileInfo[] activatedModFiles = directoryInfo.GetFiles("*.zip");
            modListView.BeginUpdate();
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
            modListView.EndUpdate();

        }

        private void ReloadListView()
        {
            this.modListView.Items.Clear();
            foreach(Mod mod in modsList)
            {
                AddModListViewItem(mod);
            }
        }

        private void AddMod(Mod mod)
        {
            modsList.AddLast(mod);
            AddModListViewItem(mod);
        }

        private void AddModListViewItem(Mod mod)
        {
            ListViewItem item = new(new[] { mod.GetModTitle(), mod.GetModAuthor(), mod.GetModVersion(), mod.GetModStatusString(), mod.GetFileName() });
            item.ToolTipText = mod.GetFileName();
            this.modListView.Items.Add(item);
        }


        private void ActivateToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            ListView.CheckedListViewItemCollection checkedMods = modListView.CheckedItems;
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
            ListView.CheckedListViewItemCollection checkedMods = modListView.CheckedItems;
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

        private static Mod FindModByFileName(string name)
        {
            foreach(Mod mod in modsList)
            {
                if (mod.GetFileName().Equals(name))
                {
                    return mod;
                }
            }
            return null;
        }

        private void DeactivateAllToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.modListView.Items)
            {
                Mod mod = FindModByFileName(item.SubItems[4].Text);
                if(mod.GetModStatus() == ModStatus.Active)
                {
                    DeactivateMod(item);
                }

            }
        }
        private void ActivateMod (ListViewItem item)
        {
            Mod mod = FindModByFileName(item.SubItems[4].Text);
            if (mod != null && mod.GetModStatus() == ModStatus.Inactive && File.Exists(Path.Combine(defaultModDirectory, mod.GetFileName())))
            {
                string newModName = mod.GetFileName().Substring(0, mod.GetFileName().LastIndexOf(".deactivated"));
                try
                {
                    File.Move(Path.Combine(defaultModDirectory, mod.GetFileName()), Path.Combine(defaultModDirectory, newModName), false);
                } catch (IOException e)
                {
                    if ( File.Exists(Path.Combine(defaultModDirectory, newModName)))
                    {
                        MessageBox.Show("It appears that " + newModName + " exists as both active and inactive mod\n" +
                            "Please remove duplicate and then reload mod directory");
                        return;
                    }
                }
                mod.SetModFileName(newModName);
                mod.SetModStatus(ModStatus.Active);
                item.SubItems[3].Text = mod.GetModStatusString();
                item.SubItems[4].Text = newModName;
            }
        }

        private void DeactivateMod(ListViewItem item)
        {
            Mod mod = FindModByFileName(item.SubItems[4].Text);
            if (mod != null && mod.GetModStatus() == ModStatus.Active && File.Exists(Path.Combine(defaultModDirectory, mod.GetFileName())))
            {
                string newModName = mod.GetFileName() + ".deactivated";
                try
                {
                    File.Move(Path.Combine(defaultModDirectory, mod.GetFileName()), Path.Combine(defaultModDirectory, newModName), false);
                }
                catch (IOException e)
                {
                    if (File.Exists(Path.Combine(defaultModDirectory, newModName)))
                    {
                        MessageBox.Show("It appears that " + mod.GetFileName() + " exists as both active and inactive mod\n" +
                            "Please remove duplicate and then reload mod directory");
                        return;
                    }
                }
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
                    try
                    {
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
                        return new ModProperties(fileName, title, author, version, icon);
                    }
                    catch (XmlException e)
                    {
                        /*
                         *MessageBox.Show(fileName + "/modDesc.xml is not a valid XML file.\n\n"
                         *   +e.Message+"\n\nMod control will only load file name reference.\n" +
                         *   "Mod title, author and version will be unknown");
                         */
                        return new ModProperties(fileName, fileName, "???", "???", "???");
                    }
                }
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
            if (this.modListView.Sorting.Equals(SortOrder.Ascending))
            {
                this.modListView.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Descending);
                this.modListView.Sorting = SortOrder.Descending;
            }
            else
            {
                this.modListView.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Ascending);
                this.modListView.Sorting = SortOrder.Ascending;
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
            if (searchBox.Text.Equals("") && e.KeyCode == Keys.Return)
            {
                this.modListView.BeginUpdate();
                if (needToReload) ReloadListView();
                needToReload = false;
                this.modListView.EndUpdate();
            } else if (modListView.Items.Count>0 && e.KeyCode == Keys.Return && searchBox.Text.Length > 0)
            {
                this.modListView.BeginUpdate();
                if(needToReload) ReloadListView();
                needToReload = true;
                for (int i = this.modListView.Items.Count - 1; i >= 0; i--) 
                {
                    ListViewItem currentItem = this.modListView.Items[i];
                    if (!this.ItemMatches(currentItem, searchBox.Text))
                    {
                        this.modListView.Items.RemoveAt(i);
                    }
                }
                this.modListView.EndUpdate();
            }
        }

        private bool ItemMatches(ListViewItem item, string text)
        {
            bool matches = false;

            matches |= item.Text.ToLower().Contains(text.ToLower());

            if (matches)
            {
                return true;
            }

            foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
            {
                matches |= subitem.Text.ToLower().Contains(text.ToLower());
                if (matches)
                {
                    return true;
                }
            }

            return false;
        }


        private void SavePackageToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Mod Package|*.modpkg";
            saveFileDialog.Title = "Save Mod Package";
            saveFileDialog.InitialDirectory = defaultModDirectory;
            saveFileDialog.ShowDialog();
            // If the file name is not an empty string open it for saving.
            if (saveFileDialog.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                StreamWriter packageFile = new(Path.Combine(defaultModDirectory, saveFileDialog.FileName));
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                foreach(Mod mod in modsList)
                {
                    if (mod.GetModStatus().Equals(ModStatus.Active))
                    {
                        packageFile.WriteLine(mod.GetFileName());
                    }
                }

                packageFile.Close();
            }
        }

        private void LoadPackageToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Mod Package|*.modpkg";
            openFileDialog.Title = "Load Mod Package";
            openFileDialog.InitialDirectory = defaultModDirectory;
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();

            foreach (String file in openFileDialog.FileNames)
            {
                using (StreamReader packageFile = new(Path.Combine(defaultModDirectory, file)))
                {
                    if (needToReload) ReloadListView();
                    needToReload = false;
                    string line;
                    while ((line = packageFile.ReadLine()) != null)
                    {
                        foreach (ListViewItem item in this.modListView.Items)
                        {
                            if (item.SubItems[4].Text.Equals(line + ".deactivated"))
                                ActivateMod(item);
                        }
                    }
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

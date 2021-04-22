﻿using System;
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
        private static LinkedList<Mod> ModsStorageList = new LinkedList<Mod>();
        private static LinkedList<Mod> ActiveModsList = new LinkedList<Mod>();
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
                modStorageDirectory = folderBrowserDialog.SelectedPath;
                ReloadMods();
                this.reloadToolStripMenuItem.Enabled = true;
                this.activateToolStripMenuItem.Enabled = true;
            }
        }

        private void ReloadToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            if (modStorageDirectory.Length > 0)
            {
                modStorageDirectory = folderBrowserDialog.SelectedPath;
                ReloadMods();
            }
        }

        private void ReloadMods()
        {
            this.listView.Items.Clear();
            ActiveModsList.Clear();
            ModsStorageList.Clear();
            LoadActiveMods();
            PopulateListView(this.listView, modStorageDirectory);
        }

        private void ActivateToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            ListView.CheckedListViewItemCollection checkedMods = listView.CheckedItems;
            ListView.SelectedListViewItemCollection selectedMods = listView.SelectedItems;
            if (checkedMods.Count > 0)
            {
                foreach (ListViewItem checkedItem in checkedMods)
                {
                    ActivateMod(checkedItem);
                }
            }
            else if (selectedMods.Count > 0)
            {
                foreach (ListViewItem selectedItem in selectedMods)
                {
                    ActivateMod(selectedItem);
                }
            }
            else
            {
                MessageBox.Show("No items selected!");
            }
            
        }

        private Mod FindModByFileName(string name, LinkedList<Mod> list)
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
            Mod mod = FindModByFileName(item.SubItems[4].Text, ModsStorageList);
            File.Copy(Path.Combine(modStorageDirectory, mod.GetFileName()), Path.Combine(defaultModDirectory, mod.GetFileName()), false);
            mod.SetModStatus(ModStatus.Active);
            item.SubItems[3].Text = mod.GetModStatusString();
        }

        private static void PopulateListView(ListView listView, string modStorageDirectory)
        {
            FileInfo[] modFiles = GetModFiles(modStorageDirectory);
            /*
             * Go through list of imported mods.
             * For each mod, find if it exists in active mods list.
             * If it does, check version. If version is the same mark it as active.
             * If version is not the same, mark the storage with update status.
             * Else it will remain Inactive.
             * 
             * In the end feed them all to listView.
             */
            foreach (FileInfo file in modFiles)
            {
                Mod storedMod = new Mod(GetModInfo(modStorageDirectory, file.Name));
                if (ActiveModsList.Count>0)
                {
                    foreach (Mod activeMod in ActiveModsList)
                    {
                        if (activeMod.GetFileName().Equals(storedMod.GetFileName()))
                        {
                            if (activeMod.GetModVersion().Equals(storedMod.GetModVersion())) {
                                storedMod.SetModStatus(ModStatus.Active);
                            }
                            else
                            {
                                storedMod.SetModStatus(ModStatus.Update);
                                AddModToListView(activeMod, listView);
                            }
                            break;
                        }
                    }
                }
                ModsStorageList.AddLast(storedMod);
                AddModToListView(storedMod, listView);
            }
            /*
             * Go through list of active mods.
             * See if it exists in modstorage mods.
             * If it does exist, all is well. If it does not, then it can/should/maybe? be backedup to mod storage.
             * This covers the case where active mod directory is used as download.
             * Set mod status to new, and feed that too to listView. Just. That. Mod.
             */
            foreach (Mod activeMod in ActiveModsList)
            {
                var modFound = false;
                foreach(Mod storedMod in ModsStorageList)
                {

                    if (storedMod.GetFileName().Equals(activeMod.GetFileName()))
                    {
                        modFound = true;
                        break;
                    }
                }
                if(!modFound)
                {
                    activeMod.SetModStatus(ModStatus.New);
                    AddModToListView(activeMod, listView);
                }
            }
        }

        private static void LoadActiveMods()
        {
            //Get mods from default mod directory - TODO: chaange later to selectable, or last selected
            FileInfo[] modFiles = GetModFiles(defaultModDirectory);
            foreach (FileInfo file in modFiles)
            {
                Mod mod = new Mod(GetModInfo(defaultModDirectory, file.Name));
                mod.SetModStatus(ModStatus.Active);
                ActiveModsList.AddLast(mod);
            }
        }

        private static void AddModToListView(Mod mod, ListView listView)
        {
            ListViewItem item = new(new[] { mod.GetModTitle(), mod.GetModAuthor(), mod.GetModVersion(), mod.GetModStatusString(), mod.GetFileName() });
            item.ToolTipText = mod.GetFileName();
            listView.Items.Add(item);
        }

        private static FileInfo[] GetModFiles(string modDirectory)
        {
            DirectoryInfo dinfo = new DirectoryInfo(modDirectory);
            return dinfo.GetFiles("*.zip"); ;
        }

        private static ModProperties GetModInfo(string modDirectory, string fileName)
        {
            string title = null;
            string author = null;
            string version = null;
            string icon = null;
            XDocument modDescXml;
            //Open Zip, get info.
            using ZipArchive archive = ZipFile.Open(modDirectory + "/" + fileName, ZipArchiveMode.Read);
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

            return new ModProperties(fileName, title, author, version, icon);
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

        private void FolderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
    }

    enum ModStatus
    {
        Unknown = -1,
        Inactive, //Exists in mod storage => Activate - Copy to mod directory.
        Active, //Exists in both mod storage, and active mod directory. Same version in both. => Deactivate - remove from mod directory
        New, //Exists only in active mod directory, can be backed up. => Backup - copy to mod storage.
        Update, //Versions are different, load both!
        Backup //not used
    }
}

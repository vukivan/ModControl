using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace ModControl
{
    public partial class MainForm : Form
    {
        private static readonly string fs19Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"My Games\FarmingSimulator2019\");
        private static readonly string fs22Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"My Games\FarmingSimulator2022\");
        private static readonly string fs19ModDirectory = Path.Combine(fs19Directory, @"mods\");
        private static readonly string fs22ModDirectory = Path.Combine(fs22Directory, @"mods\");
        private static string activeModDirectory;
        private static LinkedList<Mod> modsList = new LinkedList<Mod>();
        private static GCHandle handle;
        private static readonly int STATUS_COLUMN = 3;
        private static readonly int FILE_NAME_COLUMN = 5;
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
            activeModDirectory = GetModDirectory();
            if (Directory.Exists(GetModDirectory()) && LoadMods() > 0)
            {
                EnableMenus();
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                DialogResult result = MessageBox.Show("Mods directory does not exist, ModControl will create " + GetModDirectory(), "Mods directory missing", buttons);
                if (result == DialogResult.OK)
                {
                    Directory.CreateDirectory(GetModDirectory());
                }
            }


        }

        private void LoadCustomToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyDocuments;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                activeModDirectory = folderBrowserDialog.SelectedPath;
            }
            if (LoadMods() > 0)
            {
                EnableMenus();
            }
        }

        private void EnableMenus()
        {
            this.reloadToolStripMenuItem.Enabled = true;
            this.activateToolStripMenuItem.Enabled = true;
            this.deactivateToolStripMenuItem.Enabled = true;
            this.deactivateAllToolStripMenuItem.Enabled = true;
            this.packageToolStripMenuItem.Enabled = true;
            this.selectAllToolStripMenuItem.Enabled = true;
            this.deselectAllToolStripMenuItem.Enabled = true;
            this.searchBox.Enabled = true;
        }

        private void ReloadToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            activeModDirectory = GetModDirectory();
            if (LoadMods() > 0)
            {
                EnableMenus();
            }
        }
        private void SelectAllToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            modListView.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = true);
        }

        private void DeselectAllToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            modListView.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = false);
        }

        private int LoadMods()
        {
            int modCount = 0;
            this.modListView.Items.Clear();
            this.backupModList.Clear();
            modsList.Clear();
            DirectoryInfo directoryInfo = new(activeModDirectory);
            FileInfo[] activatedModFiles = directoryInfo.GetFiles("*.zip");
            FileInfo[] deactivatedModFiles = directoryInfo.GetFiles("*.zip.deactivated");
            modListView.BeginUpdate();
            foreach (FileInfo file in activatedModFiles)
            {
                ModProperties properties = GetModInfo(file.Name);
                if (properties != null)
                {
                    Mod mod = new(properties);
                    mod.SetModStatus(ModStatus.Active);
                    AddMod(mod);
                    modCount++;
                }
            }
            foreach (FileInfo file in deactivatedModFiles)
            {
                ModProperties properties = GetModInfo(file.Name);
                if (properties != null)
                {
                    Mod mod = new(properties);
                    mod.SetModStatus(ModStatus.Inactive);
                    AddMod(mod);
                    modCount++;
                }
            }
            modListView.EndUpdate();
            return modCount;
        }

        private void ReloadListView()
        {
            this.modListView.Items.Clear();
            this.modListView.Items.AddRange(this.backupModList.ToArray());
        }

        private void AddMod(Mod mod)
        {
            modsList.AddLast(mod);
            AddModListViewItem(mod);
        }

        private void AddModListViewItem(Mod mod)
        {
            ListViewItem item = new(new[] { mod.GetModTitle(), mod.GetModAuthor(), mod.GetModVersion(), mod.GetModStatusString(), mod.GetSize(), mod.GetFileName() });
            item.ToolTipText = mod.GetFileName() + "\n" + mod.GetCategories();
            this.modListView.Items.Add(item);
            this.backupModList.Add((ListViewItem)item.Clone());
        }


        private void ActivateToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            //Detect if game is running and show message box that activated mods will not be seen until game restarts
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
                MessageBox.Show("No items checked!");
            }

        }

        private void DeactivateToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            //disable if game is started
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
                MessageBox.Show("No items checked!");
            }

        }

        private void ActivateSelectedToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            //Detect if game is running and show message box that activated mods will not be seen until game restarts
            ListView.SelectedListViewItemCollection selectedMods = modListView.SelectedItems;
            if (selectedMods.Count > 0)
            {
                foreach (ListViewItem checkedItem in selectedMods)
                {
                    ActivateMod(checkedItem);
                }
            }
            else
            {
                MessageBox.Show("No items selected!");
            }

        }

        private void DeactivateSelectedToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            //disable if game is started
            ListView.SelectedListViewItemCollection selectedMods = modListView.SelectedItems;
            if (selectedMods.Count > 0)
            {
                foreach (ListViewItem checkedItem in selectedMods)
                {
                    DeactivateMod(checkedItem);
                }
            }
            else
            {
                MessageBox.Show("No items selected!");
            }

        }

        private void SwitchGameFS19ToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            if (!this.fs19ToolStripMenuItem.Checked)
            {
                this.fs22ToolStripMenuItem.Checked = false;
                this.fs19ToolStripMenuItem.Checked = true;
            }
        }

        private void SwitchGameFS22ToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            if (!this.fs22ToolStripMenuItem.Checked)
            {
                this.fs19ToolStripMenuItem.Checked = false;
                this.fs22ToolStripMenuItem.Checked = true;
            }
        }

        private static Mod FindModByFileName(string name)
        {
            foreach (Mod mod in modsList)
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
            //disable if game is started
            ReloadListView();
            foreach (ListViewItem item in this.modListView.Items)
            {
                Mod mod = FindModByFileName(item.SubItems[FILE_NAME_COLUMN].Text);
                if (mod.GetModStatus() == ModStatus.Active)
                {
                    DeactivateMod(item);
                    item.Checked = false;
                }

            }
        }
        private void ActivateMod(ListViewItem item)
        {
            Mod mod = FindModByFileName(item.SubItems[FILE_NAME_COLUMN].Text);
            if (mod != null && mod.GetModStatus() == ModStatus.Inactive && File.Exists(Path.Combine(activeModDirectory, mod.GetFileName())))
            {
                string newModFileName = mod.GetFileName().Substring(0, mod.GetFileName().LastIndexOf(".deactivated"));
                try
                {
                    File.Move(Path.Combine(activeModDirectory, mod.GetFileName()), Path.Combine(activeModDirectory, newModFileName), false);
                }
                catch (IOException e)
                {
                    if (e.InnerException is UnauthorizedAccessException)
                    {
                        MessageBox.Show("Unable to rename " + mod.GetFileName() + " to " + newModFileName + "\nEither destination file already exists, or inactive mod files are opened by a different program.");
                    }
                    else if (e.InnerException is FileNotFoundException)
                    {
                        MessageBox.Show("File " + mod.GetFileName() + " not found.\nCheck if it was deleted or renamed elsewhere, and then reload.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to activate " + mod.GetFileName() + ", reason unknown.\nCheck the file and reload mods.");
                    }
                    return;
                }
                mod.SetModFileName(newModFileName);
                mod.SetModStatus(ModStatus.Active);
                item.SubItems[STATUS_COLUMN].Text = mod.GetModStatusString();
                item.SubItems[FILE_NAME_COLUMN].Text = newModFileName;
            }
        }

        private void DeactivateMod(ListViewItem item)
        {
            Mod mod = FindModByFileName(item.SubItems[FILE_NAME_COLUMN].Text);
            if (mod != null && mod.GetModStatus() == ModStatus.Active && File.Exists(Path.Combine(activeModDirectory, mod.GetFileName())))
            {
                string newModFileName = mod.GetFileName() + ".deactivated";
                try
                {
                    File.Move(Path.Combine(activeModDirectory, mod.GetFileName()), Path.Combine(activeModDirectory, newModFileName), false);
                }
                catch (IOException e)
                {
                    if (e.InnerException is UnauthorizedAccessException)
                    {
                        MessageBox.Show("Unable to rename " + mod.GetFileName() + " to " + newModFileName + "\nEither destination file already exists, or active mod files are opened by a different program.");
                    }
                    else if (e.InnerException is FileNotFoundException)
                    {
                        MessageBox.Show("File " + mod.GetFileName() + " not found.\nCheck if it was deleted or renamed elsewhere, and then reload.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to deactivate " + mod.GetFileName() + ", reason unknown.\nCheck the file and reload mods.");
                    }
                    return;
                }
                mod.SetModFileName(newModFileName);
                mod.SetModStatus(ModStatus.Inactive);
                item.SubItems[STATUS_COLUMN].Text = mod.GetModStatusString();
                item.SubItems[FILE_NAME_COLUMN].Text = newModFileName;
            }
        }

        private ModProperties GetModInfo(string fileName)
        {
            string title;
            string author;
            string version;
            string icon;
            string desc;
            string categories = "";
            XDocument modDescXml;
            using ZipArchive archive = ZipFile.Open(activeModDirectory + "/" + fileName, ZipArchiveMode.Read);
            FileInfo fileInfo = new(activeModDirectory + "/" + fileName);
            string size = fileInfo.Length.ToString();
            ZipArchiveEntry entry = archive.GetEntry("modDesc.xml");
            if (entry != null)
            {
                using StreamReader reader = new StreamReader(entry.Open());
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
                    XElement descXElement = modDescXml.Element("modDesc").Element("description").Element("en");
                    if (descXElement != null)
                    {
                        desc = @descXElement.Value;
                    }
                    else
                    {
                        desc = @modDescXml.Element("modDesc").Element("description").Value;
                    }
                    if (modDescXml.Element("modDesc").Element("storeItems") != null)
                    {
                        List<XAttribute> storeItems = modDescXml.Element("modDesc").Element("storeItems").Elements("storeItem").Attributes("xmlFilename").ToList();
                        foreach (XAttribute storeItem in storeItems)
                        {
                            ZipArchiveEntry storeItemEntry = archive.GetEntry(storeItem.Value);
                            if (storeItemEntry != null)
                            {
                                StreamReader storeItemReader = new StreamReader(storeItemEntry.Open());
                                try
                                {
                                    XDocument storeItemXml = XDocument.Load(storeItemReader);
                                    string newCat;
                                    if (storeItemXml.Element("vehicle") != null && storeItemXml.Element("vehicle").Element("storeData") != null)
                                        newCat = storeItemXml.Element("vehicle").Element("storeData").Element("category").Value;
                                    else if (storeItemXml.Element("placeable") != null && storeItemXml.Element("placeable").Element("storeData") != null)
                                        newCat = storeItemXml.Element("placeable").Element("storeData").Element("category").Value;
                                    else if (storeItemXml.Element("handTool") != null && storeItemXml.Element("handTool").Element("storeData") != null)
                                        newCat = storeItemXml.Element("handTool").Element("storeData").Element("category").Value;
                                    else
                                        newCat = "";
                                    if (!categories.Contains(newCat))
                                    {
                                        categories += newCat + " ";
                                    }

                                }
                                catch
                                {
                                    // don't need to handle this, likely mallformed XML, just skip category checker.
                                }
                            }
                        }
                    }
                    return new ModProperties(fileName, title, author, version, icon, desc, size, categories);
                }
                catch
                {
                    //Likely malformed XML, best effort is just to put the filename into the list, modcontrol can still handle it.
                    return new ModProperties(fileName, fileName, "???", "???", "???", "???", size, categories);
                }
            }
            else
            {
                MessageBox.Show("modDesc.xml not found in " + fileName);
                return null;
            }


        }

        private void ModListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView.SelectedListViewItemCollection items = this.modListView.SelectedItems;

            if (items.Count > 0)
            {
                Mod mod = FindModByFileName(items[0].SubItems[FILE_NAME_COLUMN].Text);
                GetModPreview(mod);
                this.modDescTextBox.Text =
                    "Title: " + mod.GetModTitle() + " version:" + mod.GetModVersion() + "\n" +
                    "Author: " + mod.GetModAuthor() + "\n\n" +
                    "Description:\n" + mod.GetModDesc();
            }
        }

        private void ModListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip modListContextMenuStrip = new ContextMenuStrip();
                modListContextMenuStrip.ShowImageMargin = false;

                if (modListView.SelectedItems.Count == 1)
                {
                    var focusedItem = modListView.FocusedItem;

                    if (focusedItem.SubItems[STATUS_COLUMN].Text.Equals("Inactive"))
                    {
                        ToolStripMenuItem activationToolStripMenuItem = new ToolStripMenuItem("Activate selected item");
                        activationToolStripMenuItem.Click += (sender, e) => ActivateMod(focusedItem);
                        modListContextMenuStrip.Items.Add(activationToolStripMenuItem);
                        modListContextMenuStrip.Items.Add(new ToolStripSeparator());
                    }
                    else
                    {
                        ToolStripMenuItem activationToolStripMenuItem = new ToolStripMenuItem("Deactivate selected item");
                        activationToolStripMenuItem.Click += (sender, e) => DeactivateMod(focusedItem);
                        modListContextMenuStrip.Items.Add(activationToolStripMenuItem);
                        modListContextMenuStrip.Items.Add(new ToolStripSeparator());
                    }
                }
                else if (modListView.SelectedItems.Count > 1)
                {
                    var selectedItems = modListView.SelectedItems;

                    ToolStripMenuItem activationToolStripMenuItem = new ToolStripMenuItem("Activate selected mods");
                    activationToolStripMenuItem.Click += new EventHandler(ActivateSelectedToolStripMenuItem_ItemClicked);
                    modListContextMenuStrip.Items.Add(activationToolStripMenuItem);
                    ToolStripMenuItem deactivationToolStripMenuItem = new ToolStripMenuItem("Deactivate selected mods");
                    deactivationToolStripMenuItem.Click += new EventHandler(DeactivateSelectedToolStripMenuItem_ItemClicked);
                    modListContextMenuStrip.Items.Add(deactivationToolStripMenuItem);
                    modListContextMenuStrip.Items.Add(new ToolStripSeparator());
                }
                if (modListView.CheckedItems.Count >= 1)
                {
                    ToolStripMenuItem activationToolStripMenuItem = new ToolStripMenuItem("Activate checked mods");
                    activationToolStripMenuItem.Click += new EventHandler(ActivateToolStripMenuItem_ItemClicked);
                    modListContextMenuStrip.Items.Add(activationToolStripMenuItem);
                    ToolStripMenuItem deactivationToolStripMenuItem = new ToolStripMenuItem("Deactivate checked mods");
                    deactivationToolStripMenuItem.Click += new EventHandler(DeactivateToolStripMenuItem_ItemClicked);
                    modListContextMenuStrip.Items.Add(deactivationToolStripMenuItem);
                    modListContextMenuStrip.Items.Add(new ToolStripSeparator());

                }
                ToolStripMenuItem selectAllToolStripMenuItem = new ToolStripMenuItem("Select all");
                selectAllToolStripMenuItem.Click += (sender, e) => modListView.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = true);
                modListContextMenuStrip.Items.Add(selectAllToolStripMenuItem);
                modListContextMenuStrip.Enabled = true;
                ToolStripMenuItem deselectAllToolStripMenuItem = new ToolStripMenuItem("Deselect all");
                deselectAllToolStripMenuItem.Click += (sender, e) => modListView.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = false);
                modListContextMenuStrip.Items.Add(deselectAllToolStripMenuItem);
                modListContextMenuStrip.Enabled = true;
                modListContextMenuStrip.Items.Add(new ToolStripSeparator());
                ToolStripMenuItem deactivateAllToolStripMenuItem = new ToolStripMenuItem("Deactivate all");
                deactivateAllToolStripMenuItem.Click += new EventHandler(DeactivateAllToolStripMenuItem_ItemClicked);
                modListContextMenuStrip.Items.Add(deactivateAllToolStripMenuItem);
                ToolStripMenuItem reloadToolStripMenuItem = new ToolStripMenuItem("Reload mods");
                reloadToolStripMenuItem.Click += new EventHandler(ReloadToolStripMenuItem_ItemClicked);
                modListContextMenuStrip.Items.Add(reloadToolStripMenuItem);


                modListContextMenuStrip.Show(Cursor.Position);
            }
        }

        private void GetModPreview(Mod mod)
        {
            using ZipArchive archive = ZipFile.Open(activeModDirectory + "/" + mod.GetFileName(), ZipArchiveMode.Read);
            string iconPath = mod.GetModIcon();
            ZipArchiveEntry iconEntry = archive.GetEntry(iconPath);
            //sometimes path says png, but file is actually dds.
            //game ALWAYS loads dds.
            if (iconEntry == null && iconPath.Contains(".png"))
            {
                iconPath = iconPath.Substring(0, iconPath.LastIndexOf(".png")) + ".dds";
                iconEntry = archive.GetEntry(iconPath);
            }
            //this scenario should not happen, but I'm leaving this here since Giants engine might have fallback for png files
            if (iconEntry == null && iconPath.Contains(".dds"))
            {
                iconPath = iconPath.Substring(0, iconPath.LastIndexOf(".dds")) + ".png";
                iconEntry = archive.GetEntry(iconPath);
            }
            if (iconEntry != null)
            {
                StreamReader iconReader = new StreamReader(iconEntry.Open());
                if (iconPath.Contains(".dds"))
                {
                    using (var image = Pfim.Pfim.FromStream(iconReader.BaseStream))
                    {
                        PixelFormat format = PixelFormat.Undefined;

                        switch (image.Format)
                        {
                            case Pfim.ImageFormat.Rgb24:
                                format = PixelFormat.Format24bppRgb;
                                break;

                            case Pfim.ImageFormat.Rgba32:
                                format = PixelFormat.Format32bppArgb;
                                break;

                            case Pfim.ImageFormat.R5g5b5:
                                format = PixelFormat.Format16bppRgb555;
                                break;

                            case Pfim.ImageFormat.R5g6b5:
                                format = PixelFormat.Format16bppRgb565;
                                break;

                            case Pfim.ImageFormat.R5g5b5a1:
                                format = PixelFormat.Format16bppArgb1555;
                                break;

                            case Pfim.ImageFormat.Rgb8:
                                format = PixelFormat.Format8bppIndexed;
                                break;
                            default:
                                var msg = $"{image.Format} is not recognized for Bitmap on Windows Forms. " +
                       "You'd need to write a conversion function to convert the data to known format";
                                var caption = "Unrecognized format";
                                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                                break;
                        }
                        if (format != PixelFormat.Undefined)
                        {
                            //Prevents serious memory leak. DO. NOT. REMOVE.
                            if (handle.IsAllocated)
                            {
                                handle.Free();
                            }
                            handle = GCHandle.Alloc(image.Data, GCHandleType.Pinned);
                            var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(image.Data, 0);
                            var bitmap = new Bitmap(image.Width, image.Height, image.Stride, format, ptr);

                            // While frameworks like WPF and ImageSharp natively understand 8bit gray values.
                            // WinForms can only work with an 8bit palette that we construct of gray values.
                            if (format == PixelFormat.Format8bppIndexed)
                            {
                                var palette = bitmap.Palette;
                                for (int i = 0; i < 256; i++)
                                {
                                    palette.Entries[i] = Color.FromArgb((byte)i, (byte)i, (byte)i);
                                }
                                bitmap.Palette = palette;
                            }

                            this.pictureBox.Image = bitmap;
                        }

                    }
                }
                else
                {
                    this.pictureBox.Image = new Bitmap(iconReader.BaseStream);
                }
            }
            else
            {
                this.pictureBox.Image = null;
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
                if (this.column == 4)
                {
                    long diff = (long.Parse(((ListViewItem)x).SubItems[this.column].Text) - long.Parse(((ListViewItem)y).SubItems[this.column].Text));
                    if (order.Equals(SortOrder.Ascending))
                        return diff > 0 ? 1 : -1;
                    else
                        return diff <= 0 ? 1 : -1;
                }
                else
                {
                    if (order.Equals(SortOrder.Ascending))
                        return String.Compare(((ListViewItem)x).SubItems[this.column].Text, ((ListViewItem)y).SubItems[this.column].Text);
                    else
                        return (-1) * String.Compare(((ListViewItem)x).SubItems[this.column].Text, ((ListViewItem)y).SubItems[this.column].Text);
                }
            }
        }

        private void Txt_Search_KeyUp(object sender, KeyEventArgs e)
        {
            if (searchBox.Text.Equals(""))
            {
                this.modListView.BeginUpdate();
                ReloadListView();
                this.modListView.EndUpdate();
            }
            else if (backupModList.Count > 0 && searchBox.Text.Length > 0)
            {
                this.modListView.BeginUpdate();
                ReloadListView();
//                for (int i = this.modListView.Items.Count - 1; i >= 0; i--)
                foreach(ListViewItem item in this.modListView.Items)
                {
                    if (!this.ItemMatches(item, searchBox.Text))
                    {
                        this.modListView.Items.RemoveAt(item.Index);
                    }
                }
                this.modListView.EndUpdate();
            }
        }

        private bool ItemMatches(ListViewItem item, string text)
        {
            Mod mod = FindModByFileName(item.SubItems[FILE_NAME_COLUMN].Text);

            return mod.GetFileName().ToString().ToLower().Contains(text.ToLower()) |
                mod.GetModAuthor().ToString().ToLower().Contains(text.ToLower()) |
                mod.GetModTitle().ToString().ToLower().Contains(text.ToLower()) |
                mod.GetCategories().ToString().ToLower().Contains(text.ToLower()) |
                mod.GetModDesc().ToString().ToLower().Contains(text.ToLower());

        }


        private void SavePackageToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Mod Package|*.modpkg";
            saveFileDialog.Title = "Save Mod Package";
            saveFileDialog.InitialDirectory = activeModDirectory;
            saveFileDialog.ShowDialog();
            // If the file name is not an empty string open it for saving.
            if (saveFileDialog.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                StreamWriter packageFile = new(Path.Combine(activeModDirectory, saveFileDialog.FileName));
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                foreach (Mod mod in modsList)
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
            openFileDialog.InitialDirectory = activeModDirectory;
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();

            ReloadListView();
            foreach (String file in openFileDialog.FileNames)
            {
                using (StreamReader packageFile = new(Path.Combine(activeModDirectory, file)))
                {
                    
                    string line;
                    while ((line = packageFile.ReadLine()) != null)
                    {
                        ListViewItem mod = this.modListView.FindItemWithText(line + ".deactivated");
                        if (mod != null)
                        {
                            ActivateMod(mod);
                        }
                    }
                }

            }
        }

        private void LoadPackageFromSaveToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Savegame XML |careerSavegame.xml";
            openFileDialog.Title = "Load Mods from Save";
            openFileDialog.InitialDirectory = GetGameDirectory();
            openFileDialog.Multiselect = false;
            openFileDialog.ShowDialog();

            using StreamReader reader = new StreamReader(openFileDialog.OpenFile());
            try
            {
                XDocument saveXml = XDocument.Load(reader);
                List<XAttribute> savegameMods = saveXml.Element("careerSavegame").Elements("mod").Attributes("modName").ToList();

                ReloadListView();
                foreach (XAttribute savegameMod in savegameMods)
                {
                    ListViewItem mod = this.modListView.FindItemWithText(savegameMod.Value + ".zip.deactivated");
                    if (mod != null)
                    {
                        ActivateMod(mod);
                    }
                }
            }
            catch
            {
                // don't need to handle this, likely mallformed XML, just skip category checker.
            }
        }

        private string GetGameDirectory()
        {
            if (this.fs19ToolStripMenuItem.Checked)
            {
                return fs19Directory;
            }
            else
            {
                return fs22Directory;
            }
        }

        private string GetModDirectory()
        {
            if (this.fs19ToolStripMenuItem.Checked)
            {
                return fs19ModDirectory;
            }
            else
            {
                return fs22ModDirectory;
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
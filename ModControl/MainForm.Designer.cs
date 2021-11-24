using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ModControl
{
    public partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private MenuStrip mainMenu;
        private ToolStripMenuItem modToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem loadCustomToolStripMenuItem;
        private ToolStripMenuItem activateToolStripMenuItem;
        private ToolStripMenuItem deactivateToolStripMenuItem;
        private ToolStripMenuItem deactivateAllToolStripMenuItem;
        private ToolStripMenuItem reloadToolStripMenuItem;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem deselectAllToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem packageToolStripMenuItem;
        private ToolStripMenuItem savePackageToolStripMenuItem;
        private ToolStripMenuItem loadPackageToolStripMenuItem;
        private ToolStripMenuItem loadFromSaveToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem gameToolStripMenuItem;
        private ToolStripMenuItem fs19ToolStripMenuItem;
        private ToolStripMenuItem fs22ToolStripMenuItem;
        private SplitContainer splitContainer;
        private SplitContainer rightSplitContainer;
        private TextBox searchBox;
        private ListView modListView;
        private List<ListViewItem> backupModList = new List<ListViewItem>();
        private PictureBox pictureBox;
        private RichTextBox modDescTextBox;
        private readonly Keys KEY_LOAD_DEFAULT = ((Keys)((Keys.Control | Keys.L)));
        private readonly Keys KEY_ACTIVATE = ((Keys)((Keys.Control | Keys.Add)));
        private readonly Keys KEY_DEACTIVATE = ((Keys)((Keys.Control | Keys.Subtract)));
        private readonly Keys KEY_RELOAD = ((Keys)((Keys.Control | Keys.R)));
        private readonly Keys KEY_SELECT_ALL = ((Keys)((Keys.Control | Keys.A)));
        private readonly Keys KEY_DESELECT_ALL = ((Keys)((Keys.Control | Keys.Shift | Keys.A)));
        private readonly Keys KEY_SAVE_PACKAGE = ((Keys)((Keys.Control | Keys.S)));
        private readonly Keys KEY_LOAD_PACKAGE = ((Keys)((Keys.Control | Keys.P)));
        private readonly Keys KEY_LOAD_FROM_SAVE = ((Keys)((Keys.Control | Keys.Shift | Keys.P)));

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu = new MenuStrip();
            this.modToolStripMenuItem = new ToolStripMenuItem();
            this.packageToolStripMenuItem = new ToolStripMenuItem();
            this.loadToolStripMenuItem = new ToolStripMenuItem();
            this.loadCustomToolStripMenuItem = new ToolStripMenuItem();
            this.activateToolStripMenuItem = new ToolStripMenuItem();
            this.deactivateToolStripMenuItem = new ToolStripMenuItem();
            this.deactivateAllToolStripMenuItem = new ToolStripMenuItem();
            this.reloadToolStripMenuItem = new ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new ToolStripMenuItem();
            this.deselectAllToolStripMenuItem = new ToolStripMenuItem();
            this.exitToolStripMenuItem = new ToolStripMenuItem();
            this.savePackageToolStripMenuItem = new ToolStripMenuItem();
            this.loadPackageToolStripMenuItem = new ToolStripMenuItem();
            this.loadFromSaveToolStripMenuItem = new ToolStripMenuItem();
            this.helpToolStripMenuItem = new ToolStripMenuItem();
            this.aboutToolStripMenuItem = new ToolStripMenuItem();
            this.gameToolStripMenuItem = new ToolStripMenuItem();
            this.fs19ToolStripMenuItem = new ToolStripMenuItem();
            this.fs22ToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.splitContainer = new SplitContainer();
            this.rightSplitContainer = new SplitContainer();
            this.searchBox = new TextBox();
            this.modDescTextBox = new RichTextBox();
            this.modListView = new ListView();
            this.pictureBox = new PictureBox();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).BeginInit();
            this.rightSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new ToolStripItem[] {
            this.modToolStripMenuItem,
            this.packageToolStripMenuItem,
            this.gameToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(800, 48);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip";
            // 
            // modToolStripMenuItem
            // 
            this.modToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.loadCustomToolStripMenuItem,
            this.toolStripSeparator1,
            this.selectAllToolStripMenuItem,
            this.deselectAllToolStripMenuItem,
            this.toolStripSeparator2,
            this.activateToolStripMenuItem,
            this.deactivateToolStripMenuItem,
            this.deactivateAllToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.modToolStripMenuItem.Name = "modToolStripMenuItem";
            this.modToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.modToolStripMenuItem.Text = "&Mod";
            //
            // packageToolStripMenuItem
            //
            this.packageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            this.savePackageToolStripMenuItem,
            this.loadPackageToolStripMenuItem,
            this.loadFromSaveToolStripMenuItem});
            this.packageToolStripMenuItem.Name = "packageToolStripMenuItem";
            this.packageToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.packageToolStripMenuItem.Text = "&Package";
            this.packageToolStripMenuItem.Enabled = false;
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.ShortcutKeys = KEY_LOAD_DEFAULT;
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.loadToolStripMenuItem.Text = "&Load default mod directory";
            this.loadToolStripMenuItem.ToolTipText = "Load mods from game default mod directory";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_ItemClicked);
            // 
            // loadToolStripMenuItem
            // 
            this.loadCustomToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadCustomToolStripMenuItem.Name = "loadCustomToolStripMenuItem";
            this.loadCustomToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.loadCustomToolStripMenuItem.Text = "&Load custom mod directory";
            this.loadCustomToolStripMenuItem.ToolTipText = "Load mods from a directory of your choice";
            this.loadCustomToolStripMenuItem.Click += new System.EventHandler(this.LoadCustomToolStripMenuItem_ItemClicked);
            // 
            // activateToolStripMenuItem
            // 
            this.activateToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.activateToolStripMenuItem.Name = "activateToolStripMenuItem";
            this.activateToolStripMenuItem.ShortcutKeys = KEY_ACTIVATE;
            this.activateToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.activateToolStripMenuItem.Text = "&Activate checked mods";
            this.activateToolStripMenuItem.Click += new System.EventHandler(this.ActivateToolStripMenuItem_ItemClicked);
            this.activateToolStripMenuItem.Enabled = false;
            // 
            // deactivateToolStripMenuItem
            // 
            this.deactivateToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deactivateToolStripMenuItem.Name = "deactivateToolStripMenuItem";
            this.deactivateToolStripMenuItem.ShortcutKeys = KEY_DEACTIVATE;
            this.deactivateToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.deactivateToolStripMenuItem.Text = "&Deactivate checked mods";
            this.deactivateToolStripMenuItem.Click += new System.EventHandler(this.DeactivateToolStripMenuItem_ItemClicked);
            this.deactivateToolStripMenuItem.Enabled = false;
            // 
            // deactivateAllToolStripMenuItem
            // 
            this.deactivateAllToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deactivateAllToolStripMenuItem.Name = "deactivateAllToolStripMenuItem";
            this.deactivateAllToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.deactivateAllToolStripMenuItem.Text = "&Deactivate all";
            this.deactivateAllToolStripMenuItem.Click += new System.EventHandler(this.DeactivateAllToolStripMenuItem_ItemClicked);
            this.deactivateAllToolStripMenuItem.Enabled = false;
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.ShortcutKeys = KEY_RELOAD;
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.reloadToolStripMenuItem.Text = "&Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.ReloadToolStripMenuItem_ItemClicked);
            this.reloadToolStripMenuItem.Enabled = false;
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = KEY_SELECT_ALL;
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.selectAllToolStripMenuItem.Text = "&Select all";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItem_ItemClicked);
            this.selectAllToolStripMenuItem.Enabled = false;
            // 
            // deselectAllToolStripMenuItem
            // 
            this.deselectAllToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deselectAllToolStripMenuItem.Name = "deselectAllToolStripMenuItem";
            this.deselectAllToolStripMenuItem.ShortcutKeys = KEY_DESELECT_ALL;
            this.deselectAllToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.deselectAllToolStripMenuItem.Text = "&Deselect all";
            this.deselectAllToolStripMenuItem.Click += new System.EventHandler(this.DeselectAllToolStripMenuItem_ItemClicked);
            this.deselectAllToolStripMenuItem.Enabled = false;
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_ItemClicked);
            // 
            // savePackageToolStripMenuItem
            // 
            this.savePackageToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.savePackageToolStripMenuItem.Name = "savePackageToolStripMenuItem";
            this.savePackageToolStripMenuItem.ShortcutKeys = KEY_SAVE_PACKAGE;
            this.savePackageToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.savePackageToolStripMenuItem.Text = "&Save active";
            this.savePackageToolStripMenuItem.Click += new System.EventHandler(this.SavePackageToolStripMenuItem_ItemClicked);
            // 
            // loadPackageToolStropMenuItem
            // 
            this.loadPackageToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadPackageToolStripMenuItem.Name = "loadPackageToolStropMenuItem";
            this.loadPackageToolStripMenuItem.ShortcutKeys = KEY_LOAD_PACKAGE;
            this.loadPackageToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.loadPackageToolStripMenuItem.Text = "&Load package";
            this.loadPackageToolStripMenuItem.Click += new System.EventHandler(this.LoadPackageToolStripMenuItem_ItemClicked);
            // 
            // loadFromSaveToolStripMenuItem
            // 
            this.loadFromSaveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadFromSaveToolStripMenuItem.Name = "loadPackageToolStropMenuItem";
            this.loadFromSaveToolStripMenuItem.ShortcutKeys = KEY_LOAD_FROM_SAVE;
            this.loadFromSaveToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.loadFromSaveToolStripMenuItem.Text = "&Load package from save";
            this.loadFromSaveToolStripMenuItem.Click += new System.EventHandler(this.LoadPackageFromSaveToolStripMenuItem_ItemClicked);
            // 
            // helpToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            this.fs19ToolStripMenuItem,
            this.fs22ToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.gameToolStripMenuItem.Text = "&Game";
            //
            // fs19ToolStripMenuItem
            //
            this.fs19ToolStripMenuItem.Name = "fs19ToolStripMenuItem";
            this.fs19ToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.fs19ToolStripMenuItem.Text = "&FS19";
            //this.fs19ToolStripMenuItem.CheckOnClick = true;
            this.fs19ToolStripMenuItem.Checked = false;
            this.fs19ToolStripMenuItem.Click += new System.EventHandler(this.SwitchGameFS19ToolStripMenuItem_ItemClicked);
            this.fs19ToolStripMenuItem.ToolTipText = "Select game for default mod directory, custom mod directory is not affected by this.";
            //
            // fs22ToolStripMenuItem
            //
            this.fs22ToolStripMenuItem.Name = "fs22ToolStripMenuItem";
            this.fs22ToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.fs22ToolStripMenuItem.Text = "&FS22";
            //this.fs22ToolStripMenuItem.CheckOnClick = true;
            this.fs22ToolStripMenuItem.Checked = true;
            this.fs22ToolStripMenuItem.Click += new System.EventHandler(this.SwitchGameFS22ToolStripMenuItem_ItemClicked);
            this.fs22ToolStripMenuItem.ToolTipText = "Select game for default mod directory, custom mod directory is not affected by this.";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_ItemClicked);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 6);
            // 
            // mainSplitContainer
            // 
            this.splitContainer.Dock = DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "mainSplitContainer";
            this.splitContainer.Orientation = Orientation.Vertical;
            this.splitContainer.SplitterDistance = 80;
            // 
            // rightSplitContainer
            // 
            this.rightSplitContainer.Dock = DockStyle.Fill;
            this.rightSplitContainer.Location = new System.Drawing.Point(0, 24);
            this.rightSplitContainer.Name = "rightSplitContainer";
            this.rightSplitContainer.Orientation = Orientation.Horizontal;
            this.rightSplitContainer.TabIndex = 1;
            this.pictureBox.Dock = DockStyle.Fill;
            this.rightSplitContainer.Panel1.Controls.Add(pictureBox);
            //
            // mainSplitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(modListView);
            this.Controls.Add(searchBox);
            this.splitContainer.TabIndex = 0;
            this.modListView.Dock = DockStyle.Fill;
            this.modListView.Location = new System.Drawing.Point(0, 0);
            this.modListView.Name = "listView";
            this.modListView.View = View.Details;
            this.modListView.Columns.Add("Name", 350, HorizontalAlignment.Left);
            this.modListView.Columns.Add("Author", 300, HorizontalAlignment.Left);
            this.modListView.Columns.Add("Version", 50, HorizontalAlignment.Left);
            this.modListView.Columns.Add("Status", 75, HorizontalAlignment.Left);
            this.modListView.Columns.Add("Size in bytes", 90, HorizontalAlignment.Left);
            this.modListView.Scrollable = true;
            this.modListView.CheckBoxes = true;
            this.modListView.FullRowSelect = true;
            this.modListView.AllowColumnReorder = true;
            this.modListView.ColumnClick += new ColumnClickEventHandler(ColumnClick);
            this.modListView.ShowItemToolTips = true;
            this.modListView.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(ModListView_ItemSelectionChanged);
            this.modListView.MouseClick += new MouseEventHandler(ModListView_MouseClick);

            this.splitContainer.Panel2.Controls.Add(rightSplitContainer);
            //
            // SearchBox
            //
            this.searchBox.Location = new Point(225, 0);
            this.searchBox.KeyUp += new KeyEventHandler(Txt_Search_KeyUp);
            this.searchBox.Width = 300;
            this.searchBox.PlaceholderText = "Type to search...";
            this.searchBox.Enabled = false;

            this.modDescTextBox.Dock = DockStyle.Fill;
            this.modDescTextBox.ReadOnly = true;
            this.modDescTextBox.Multiline = true;
            this.modDescTextBox.AcceptsTab = true;
            this.modDescTextBox.Name = "modDescTextBox";
            this.modDescTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.modDescTextBox.EnableModDescriptionContextMenu();

            this.rightSplitContainer.Panel2.Controls.Add(modDescTextBox);

            // Right panel, split in two sections.
            // Top section is "Select active mod Folder" - Moves mod folder.
            // Buttons, first is always default. Others will open configuration form, where one selects a new active directory.
            // If button is configured, it switches mod folder.
            // Second section is "Activate mod list" - Selects mods in storage view.
            // This is a scrollable list. Button copies them all.
            // Deactivation works the same.
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "ModControl";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).EndInit();
            this.rightSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}


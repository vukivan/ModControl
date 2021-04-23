
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ModControl
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private MenuStrip mainMenu;
        private ToolStripMenuItem modToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem activateToolStripMenuItem;
        private ToolStripMenuItem deactivateToolStripMenuItem;
        private ToolStripMenuItem reloadToolStripMenuItem;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem deselectAllToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private SplitContainer splitContainer;
        private SplitContainer rightSplitContainer;
        private TextBox searchBox;
        private ListView listView;

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
            this.loadToolStripMenuItem = new ToolStripMenuItem();
            this.activateToolStripMenuItem = new ToolStripMenuItem();
            this.deactivateToolStripMenuItem = new ToolStripMenuItem();
            this.reloadToolStripMenuItem = new ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new ToolStripMenuItem();
            this.deselectAllToolStripMenuItem = new ToolStripMenuItem();
            this.exitToolStripMenuItem = new ToolStripMenuItem();
            this.helpToolStripMenuItem = new ToolStripMenuItem();
            this.aboutToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator = new ToolStripSeparator();
            this.splitContainer = new SplitContainer();
            this.rightSplitContainer = new SplitContainer();
            this.searchBox = new TextBox();
            this.listView = new ListView();
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
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(800, 48);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // modToolStripMenuItem
            // 
            this.modToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.activateToolStripMenuItem,
            this.deactivateToolStripMenuItem,
            this.deselectAllToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.modToolStripMenuItem.Name = "modToolStripMenuItem";
            this.modToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.modToolStripMenuItem.Text = "&Mod";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.ShortcutKeys = ((Keys)((Keys.Control | Keys.L)));
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.loadToolStripMenuItem.Text = "&Load Mods";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_ItemClicked);
            // 
            // activateToolStripMenuItem
            // 
            this.activateToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.activateToolStripMenuItem.Name = "activateToolStripMenuItem";
            this.activateToolStripMenuItem.ShortcutKeys = ((Keys)((Keys.Control | Keys.A)));
            this.activateToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.activateToolStripMenuItem.Text = "&Activate Selected";
            this.activateToolStripMenuItem.Click += new System.EventHandler(this.ActivateToolStripMenuItem_ItemClicked);
            this.activateToolStripMenuItem.Enabled = false;
            // 
            // deactivateToolStripMenuItem
            // 
            this.deactivateToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deactivateToolStripMenuItem.Name = "deactivateToolStripMenuItem";
            this.deactivateToolStripMenuItem.ShortcutKeys = ((Keys)((Keys.Control | Keys.D)));
            this.deactivateToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.deactivateToolStripMenuItem.Text = "&Deactivate Selected";
            this.deactivateToolStripMenuItem.Click += new System.EventHandler(this.DeactivateToolStripMenuItem_ItemClicked);
            this.deactivateToolStripMenuItem.Enabled = false;
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.ShortcutKeys = ((Keys)((Keys.Control | Keys.R)));
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.reloadToolStripMenuItem.Text = "&Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.ReloadToolStripMenuItem_ItemClicked);
            this.reloadToolStripMenuItem.Enabled = false;
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((Keys)((Keys.Control | Keys.Shift | Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.selectAllToolStripMenuItem.Text = "&Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItem_ItemClicked);
            // 
            // deselectAllToolStripMenuItem
            // 
            this.deselectAllToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deselectAllToolStripMenuItem.Name = "deselectAllToolStripMenuItem";
            this.deselectAllToolStripMenuItem.ShortcutKeys = ((Keys)((Keys.Control | Keys.Shift | Keys.E)));
            this.deselectAllToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.deselectAllToolStripMenuItem.Text = "&Deslect All";
            this.deselectAllToolStripMenuItem.Click += new System.EventHandler(this.DeselectAllToolStripMenuItem_ItemClicked);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_ItemClicked);
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
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 6);
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
            // mainSplitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(listView);
            this.Controls.Add(searchBox);
            this.splitContainer.TabIndex = 0;
            this.listView.Dock = DockStyle.Fill;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.View = View.Details;
            this.listView.Columns.Add("Name", 400, HorizontalAlignment.Left);
            this.listView.Columns.Add("Author", 300, HorizontalAlignment.Left);
            this.listView.Columns.Add("Version", 50, HorizontalAlignment.Left);
            this.listView.Columns.Add("Status", 75, HorizontalAlignment.Left);
            this.listView.Scrollable = true;
            this.listView.CheckBoxes = true;
            this.listView.FullRowSelect = true;
            this.listView.AllowColumnReorder = true;
            // Connect the ListView.ColumnClick event to the ColumnClick event handler.
            this.listView.ColumnClick += new ColumnClickEventHandler(ColumnClick);
            this.listView.ShowItemToolTips = true;

            this.splitContainer.Panel2.Controls.Add(rightSplitContainer);
            //
            // SearchBox
            //
            this.searchBox.Location = new Point(100, 0);
            this.searchBox.KeyDown += new KeyEventHandler(Txt_Search_KeyDown);
            this.searchBox.Width = 300;
            this.searchBox.PlaceholderText = "Jump to...";

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


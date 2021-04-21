
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ModControl
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.openToolStripMenuItem = new ToolStripMenuItem();
            this.exitToolStripMenuItem = new ToolStripMenuItem();
            this.helpToolStripMenuItem = new ToolStripMenuItem();
            this.aboutToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator = new ToolStripSeparator();
            this.folderBrowserDialog = new FolderBrowserDialog();
            this.splitContainer = new SplitContainer();
            this.searchBox = new TextBox();
            this.listView = new ListView();
            this.statusStrip = new StatusStrip();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
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
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.modToolStripMenuItem.Name = "modToolStripMenuItem";
            this.modToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.modToolStripMenuItem.Text = "&Mod";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((Keys)((Keys.Control | Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.openToolStripMenuItem.Text = "&Open Mod Storage";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_ItemClicked);
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
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog.Description = "Select the directory that you want to use as the default.";
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            this.folderBrowserDialog.ShowNewFolderButton = false;
            this.folderBrowserDialog.HelpRequest += new System.EventHandler(this.FolderBrowserDialog1_HelpRequest);
            // 
            // splitContainer1
            // 
            this.splitContainer.Dock = DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = Orientation.Vertical;
            this.splitContainer.SplitterDistance = 80;
            // splitContainer1.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(statusStrip);
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
            //
            // SearchBox
            //
            this.searchBox.Location = new Point(100, 0);
            this.searchBox.KeyDown += new KeyEventHandler(txt_Search_KeyDown);
            this.searchBox.Width = 300;
            this.searchBox.PlaceholderText = "Jump to...";
            // 
            // statusStrip1
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 854);
            this.statusStrip.Name = "statusStrip1";
            this.statusStrip.Size = new System.Drawing.Size(853, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";

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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip mainMenu;
        private ToolStripMenuItem modToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private FolderBrowserDialog folderBrowserDialog;
        private static LinkedList<Mod> ModsList = new LinkedList<Mod>();
        private SplitContainer splitContainer;
        private TextBox searchBox;
        private ListView listView;
        private StatusStrip statusStrip;
    }
}


﻿namespace Backup
{
    partial class DataOverviewControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            addFolderButton = new Button();
            selectedFolderTextBox = new TextBox();
            folderListBox = new ListBox();
            mainViewTabControl = new TabControl();
            tabPage1 = new TabPage();
            refreshDataCheckBox = new CheckBox();
            directoryTreeView = new TreeView();
            ReadFolderContents = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            drawTreeButton = new Button();
            textBox3 = new TextBox();
            tabPage2 = new TabPage();
            md5ProgressBar = new ProgressBar();
            duplicateTreeView = new TreeView();
            findDuplicateButton = new Button();
            tabPage5 = new TabPage();
            progressTextBox = new TextBox();
            fileProcessedTextBox = new TextBox();
            button1 = new Button();
            duplicateContextMenuStrip = new ContextMenuStrip(components);
            calculateMD5ToolStripMenuItem = new ToolStripMenuItem();
            mainViewTabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage5.SuspendLayout();
            duplicateContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // addFolderButton
            // 
            addFolderButton.Location = new Point(4, 5);
            addFolderButton.Margin = new Padding(4, 5, 4, 5);
            addFolderButton.Name = "addFolderButton";
            addFolderButton.Size = new Size(224, 38);
            addFolderButton.TabIndex = 9;
            addFolderButton.Text = "Add Folder";
            addFolderButton.UseVisualStyleBackColor = true;
            addFolderButton.Click += addFolderButton_Click;
            // 
            // selectedFolderTextBox
            // 
            selectedFolderTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            selectedFolderTextBox.Location = new Point(5, 874);
            selectedFolderTextBox.Margin = new Padding(4, 5, 4, 5);
            selectedFolderTextBox.Name = "selectedFolderTextBox";
            selectedFolderTextBox.Size = new Size(223, 31);
            selectedFolderTextBox.TabIndex = 11;
            // 
            // folderListBox
            // 
            folderListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            folderListBox.FormattingEnabled = true;
            folderListBox.Location = new Point(5, 53);
            folderListBox.Margin = new Padding(4, 5, 4, 5);
            folderListBox.Name = "folderListBox";
            folderListBox.Size = new Size(223, 804);
            folderListBox.TabIndex = 10;
            folderListBox.SelectedValueChanged += folderListBox_SelectedValueChanged;
            folderListBox.DoubleClick += folderListBox_DoubleClick;
            // 
            // mainViewTabControl
            // 
            mainViewTabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainViewTabControl.Controls.Add(tabPage1);
            mainViewTabControl.Controls.Add(tabPage2);
            mainViewTabControl.Controls.Add(tabPage5);
            mainViewTabControl.Location = new Point(236, 6);
            mainViewTabControl.Margin = new Padding(4, 5, 4, 5);
            mainViewTabControl.Multiline = true;
            mainViewTabControl.Name = "mainViewTabControl";
            mainViewTabControl.SelectedIndex = 0;
            mainViewTabControl.Size = new Size(1265, 906);
            mainViewTabControl.TabIndex = 12;
            mainViewTabControl.Selected += mainViewTabControl_Selected;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(refreshDataCheckBox);
            tabPage1.Controls.Add(directoryTreeView);
            tabPage1.Controls.Add(ReadFolderContents);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Controls.Add(textBox2);
            tabPage1.Controls.Add(drawTreeButton);
            tabPage1.Controls.Add(textBox3);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Margin = new Padding(4, 5, 4, 5);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(4, 5, 4, 5);
            tabPage1.Size = new Size(1257, 868);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Directory View";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // refreshDataCheckBox
            // 
            refreshDataCheckBox.Location = new Point(241, 10);
            refreshDataCheckBox.Margin = new Padding(4, 5, 4, 5);
            refreshDataCheckBox.Name = "refreshDataCheckBox";
            refreshDataCheckBox.Size = new Size(209, 40);
            refreshDataCheckBox.TabIndex = 6;
            refreshDataCheckBox.Text = "Refresh existing data";
            refreshDataCheckBox.UseVisualStyleBackColor = true;
            // 
            // directoryTreeView
            // 
            directoryTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            directoryTreeView.Location = new Point(9, 53);
            directoryTreeView.Margin = new Padding(4, 5, 4, 5);
            directoryTreeView.Name = "directoryTreeView";
            directoryTreeView.Size = new Size(1234, 648);
            directoryTreeView.TabIndex = 2;
            directoryTreeView.NodeMouseClick += directoryTreeView_NodeMouseClick;
            // 
            // ReadFolderContents
            // 
            ReadFolderContents.Location = new Point(9, 10);
            ReadFolderContents.Margin = new Padding(4, 5, 4, 5);
            ReadFolderContents.Name = "ReadFolderContents";
            ReadFolderContents.Size = new Size(224, 38);
            ReadFolderContents.TabIndex = 0;
            ReadFolderContents.Text = "Read folder contents";
            ReadFolderContents.UseVisualStyleBackColor = true;
            ReadFolderContents.Click += ReadFolderContents_Click;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(9, 711);
            textBox1.Margin = new Padding(4, 5, 4, 5);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(1234, 31);
            textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(9, 760);
            textBox2.Margin = new Padding(4, 5, 4, 5);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(1234, 31);
            textBox2.TabIndex = 4;
            // 
            // drawTreeButton
            // 
            drawTreeButton.Location = new Point(458, 10);
            drawTreeButton.Margin = new Padding(4, 5, 4, 5);
            drawTreeButton.Name = "drawTreeButton";
            drawTreeButton.Size = new Size(107, 38);
            drawTreeButton.TabIndex = 1;
            drawTreeButton.Text = "Draw Tree";
            drawTreeButton.UseVisualStyleBackColor = true;
            drawTreeButton.Click += drawTreeButton_Click;
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox3.Location = new Point(9, 808);
            textBox3.Margin = new Padding(4, 5, 4, 5);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(1234, 31);
            textBox3.TabIndex = 5;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(md5ProgressBar);
            tabPage2.Controls.Add(duplicateTreeView);
            tabPage2.Controls.Add(findDuplicateButton);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Margin = new Padding(4, 5, 4, 5);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(4, 5, 4, 5);
            tabPage2.Size = new Size(1257, 868);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Duplicate List";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // md5ProgressBar
            // 
            md5ProgressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            md5ProgressBar.Location = new Point(9, 820);
            md5ProgressBar.Margin = new Padding(4, 5, 4, 5);
            md5ProgressBar.Name = "md5ProgressBar";
            md5ProgressBar.Size = new Size(216, 38);
            md5ProgressBar.TabIndex = 12;
            // 
            // duplicateTreeView
            // 
            duplicateTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            duplicateTreeView.Location = new Point(9, 58);
            duplicateTreeView.Margin = new Padding(4, 5, 4, 5);
            duplicateTreeView.Name = "duplicateTreeView";
            duplicateTreeView.Size = new Size(1240, 752);
            duplicateTreeView.TabIndex = 11;
            duplicateTreeView.NodeMouseClick += duplicateTreeView_NodeMouseClick;
            duplicateTreeView.NodeMouseDoubleClick += duplicateTreeView_NodeMouseDoubleClick;
            // 
            // findDuplicateButton
            // 
            findDuplicateButton.Location = new Point(9, 10);
            findDuplicateButton.Margin = new Padding(4, 5, 4, 5);
            findDuplicateButton.Name = "findDuplicateButton";
            findDuplicateButton.Size = new Size(141, 38);
            findDuplicateButton.TabIndex = 10;
            findDuplicateButton.Text = "Find Duplicates";
            findDuplicateButton.UseVisualStyleBackColor = true;
            findDuplicateButton.Click += findDuplicateButton_Click;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(progressTextBox);
            tabPage5.Controls.Add(fileProcessedTextBox);
            tabPage5.Controls.Add(button1);
            tabPage5.Location = new Point(4, 34);
            tabPage5.Name = "tabPage5";
            tabPage5.Size = new Size(1257, 868);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Calculate Hashes";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // progressTextBox
            // 
            progressTextBox.Location = new Point(3, 80);
            progressTextBox.Name = "progressTextBox";
            progressTextBox.Size = new Size(1231, 31);
            progressTextBox.TabIndex = 2;
            // 
            // fileProcessedTextBox
            // 
            fileProcessedTextBox.Location = new Point(3, 43);
            fileProcessedTextBox.Name = "fileProcessedTextBox";
            fileProcessedTextBox.Size = new Size(1231, 31);
            fileProcessedTextBox.TabIndex = 1;
            // 
            // button1
            // 
            button1.BackColor = Color.GreenYellow;
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(150, 34);
            button1.TabIndex = 0;
            button1.Text = "Start Hashing";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // duplicateContextMenuStrip
            // 
            duplicateContextMenuStrip.ImageScalingSize = new Size(24, 24);
            duplicateContextMenuStrip.Items.AddRange(new ToolStripItem[] { calculateMD5ToolStripMenuItem });
            duplicateContextMenuStrip.Name = "duplicateContextMenuStrip";
            duplicateContextMenuStrip.Size = new Size(244, 36);
            duplicateContextMenuStrip.Click += duplicateContextMenuStrip_Click;
            // 
            // calculateMD5ToolStripMenuItem
            // 
            calculateMD5ToolStripMenuItem.Name = "calculateMD5ToolStripMenuItem";
            calculateMD5ToolStripMenuItem.Size = new Size(243, 32);
            calculateMD5ToolStripMenuItem.Text = "Calculate MD5 Hash";
            calculateMD5ToolStripMenuItem.Click += calculateMD5ToolStripMenuItem_Click;
            // 
            // DataOverviewControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(mainViewTabControl);
            Controls.Add(selectedFolderTextBox);
            Controls.Add(folderListBox);
            Controls.Add(addFolderButton);
            Name = "DataOverviewControl";
            Size = new Size(1503, 927);
            Load += DataOverviewControl_Load;
            mainViewTabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            duplicateContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button addFolderButton;
        private TextBox selectedFolderTextBox;
        private ListBox folderListBox;
        private TabControl mainViewTabControl;
        private TabPage tabPage1;
        private CheckBox refreshDataCheckBox;
        private TreeView directoryTreeView;
        private Button ReadFolderContents;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button drawTreeButton;
        private TextBox textBox3;
        private TabPage tabPage2;
        private ProgressBar md5ProgressBar;
        private TreeView duplicateTreeView;
        private Button findDuplicateButton;
        private TabPage tabPage5;
        private TextBox progressTextBox;
        private TextBox fileProcessedTextBox;
        private Button button1;
        private ContextMenuStrip duplicateContextMenuStrip;
        private ToolStripMenuItem calculateMD5ToolStripMenuItem;
    }
}

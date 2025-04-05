namespace BackupSolution
{
    partial class Form1
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ReadFolderContents = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            directoryTreeView = new System.Windows.Forms.TreeView();
            textBox1 = new System.Windows.Forms.TextBox();
            textBox2 = new System.Windows.Forms.TextBox();
            textBox3 = new System.Windows.Forms.TextBox();
            folderListBox = new System.Windows.Forms.ListBox();
            selectedFolderTextBox = new System.Windows.Forms.TextBox();
            addFolderButton = new System.Windows.Forms.Button();
            mainViewTabControl = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            tabPage2 = new System.Windows.Forms.TabPage();
            md5ProgressBar = new System.Windows.Forms.ProgressBar();
            duplicateTreeView = new System.Windows.Forms.TreeView();
            duplicateContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            calculateMD5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            findDuplicateButton = new System.Windows.Forms.Button();
            tabPage3 = new System.Windows.Forms.TabPage();
            createFolderButton = new System.Windows.Forms.Button();
            fileLinkButton = new System.Windows.Forms.Button();
            folderLinkButton = new System.Windows.Forms.Button();
            treeView2 = new System.Windows.Forms.TreeView();
            treeView1 = new System.Windows.Forms.TreeView();
            tabPage4 = new System.Windows.Forms.TabPage();
            refreshDataCheckBox = new System.Windows.Forms.CheckBox();
            mainViewTabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            duplicateContextMenuStrip.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // ReadFolderContents
            // 
            ReadFolderContents.Location = new System.Drawing.Point(6, 6);
            ReadFolderContents.Name = "ReadFolderContents";
            ReadFolderContents.Size = new System.Drawing.Size(157, 23);
            ReadFolderContents.TabIndex = 0;
            ReadFolderContents.Text = "Read folder contents";
            ReadFolderContents.UseVisualStyleBackColor = true;
            ReadFolderContents.Click += refreshFolderContentsButton_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(175, 12);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // directoryTreeView
            // 
            directoryTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            directoryTreeView.Location = new System.Drawing.Point(6, 32);
            directoryTreeView.Name = "directoryTreeView";
            directoryTreeView.Size = new System.Drawing.Size(532, 372);
            directoryTreeView.TabIndex = 2;
            directoryTreeView.NodeMouseClick += directoryTreeView_NodeMouseClick;
            // 
            // textBox1
            // 
            textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            textBox1.Location = new System.Drawing.Point(6, 410);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(532, 23);
            textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            textBox2.Location = new System.Drawing.Point(6, 439);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(532, 23);
            textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            textBox3.Location = new System.Drawing.Point(6, 468);
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(532, 23);
            textBox3.TabIndex = 5;
            // 
            // folderListBox
            // 
            folderListBox.FormattingEnabled = true;
            folderListBox.Location = new System.Drawing.Point(12, 40);
            folderListBox.Name = "folderListBox";
            folderListBox.Size = new System.Drawing.Size(157, 484);
            folderListBox.TabIndex = 6;
            folderListBox.SelectedValueChanged += folderListBox_SelectedValueChanged;
            folderListBox.DoubleClick += folderListBox_DoubleClick;
            // 
            // selectedFolderTextBox
            // 
            selectedFolderTextBox.Location = new System.Drawing.Point(12, 533);
            selectedFolderTextBox.Name = "selectedFolderTextBox";
            selectedFolderTextBox.Size = new System.Drawing.Size(157, 23);
            selectedFolderTextBox.TabIndex = 7;
            // 
            // addFolderButton
            // 
            addFolderButton.Location = new System.Drawing.Point(12, 12);
            addFolderButton.Name = "addFolderButton";
            addFolderButton.Size = new System.Drawing.Size(157, 23);
            addFolderButton.TabIndex = 8;
            addFolderButton.Text = "Add Folder";
            addFolderButton.UseVisualStyleBackColor = true;
            addFolderButton.Click += addFolderButton_Click;
            // 
            // mainViewTabControl
            // 
            mainViewTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            mainViewTabControl.Controls.Add(tabPage1);
            mainViewTabControl.Controls.Add(tabPage2);
            mainViewTabControl.Controls.Add(tabPage3);
            mainViewTabControl.Controls.Add(tabPage4);
            mainViewTabControl.Location = new System.Drawing.Point(175, 41);
            mainViewTabControl.Name = "mainViewTabControl";
            mainViewTabControl.SelectedIndex = 0;
            mainViewTabControl.Size = new System.Drawing.Size(552, 527);
            mainViewTabControl.TabIndex = 9;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(refreshDataCheckBox);
            tabPage1.Controls.Add(directoryTreeView);
            tabPage1.Controls.Add(ReadFolderContents);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Controls.Add(textBox2);
            tabPage1.Controls.Add(textBox3);
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(544, 499);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Directory View";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(md5ProgressBar);
            tabPage2.Controls.Add(duplicateTreeView);
            tabPage2.Controls.Add(findDuplicateButton);
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(544, 499);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Duplicate List";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // md5ProgressBar
            // 
            md5ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            md5ProgressBar.Location = new System.Drawing.Point(6, 413);
            md5ProgressBar.Name = "md5ProgressBar";
            md5ProgressBar.Size = new System.Drawing.Size(151, 23);
            md5ProgressBar.TabIndex = 12;
            // 
            // duplicateTreeView
            // 
            duplicateTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            duplicateTreeView.ContextMenuStrip = duplicateContextMenuStrip;
            duplicateTreeView.Location = new System.Drawing.Point(6, 35);
            duplicateTreeView.Name = "duplicateTreeView";
            duplicateTreeView.Size = new System.Drawing.Size(532, 372);
            duplicateTreeView.TabIndex = 11;
            duplicateTreeView.NodeMouseClick += duplicateTreeView_NodeMouseClick;
            duplicateTreeView.NodeMouseDoubleClick += duplicateTreeView_NodeMouseDoubleClick;
            // 
            // duplicateContextMenuStrip
            // 
            duplicateContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { calculateMD5ToolStripMenuItem });
            duplicateContextMenuStrip.Name = "duplicateContextMenuStrip";
            duplicateContextMenuStrip.Size = new System.Drawing.Size(182, 26);
            duplicateContextMenuStrip.Click += duplicateContextMenuStrip_Click;
            // 
            // calculateMD5ToolStripMenuItem
            // 
            calculateMD5ToolStripMenuItem.Name = "calculateMD5ToolStripMenuItem";
            calculateMD5ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            calculateMD5ToolStripMenuItem.Text = "Calculate MD5 Hash";
            calculateMD5ToolStripMenuItem.Click += calculateMD5ToolStripMenuItem_Click;
            // 
            // findDuplicateButton
            // 
            findDuplicateButton.Location = new System.Drawing.Point(6, 6);
            findDuplicateButton.Name = "findDuplicateButton";
            findDuplicateButton.Size = new System.Drawing.Size(99, 23);
            findDuplicateButton.TabIndex = 10;
            findDuplicateButton.Text = "Find Duplicates";
            findDuplicateButton.UseVisualStyleBackColor = true;
            findDuplicateButton.Click += findDuplicateButton_Click;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(createFolderButton);
            tabPage3.Controls.Add(fileLinkButton);
            tabPage3.Controls.Add(folderLinkButton);
            tabPage3.Controls.Add(treeView2);
            tabPage3.Controls.Add(treeView1);
            tabPage3.Location = new System.Drawing.Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new System.Drawing.Size(544, 499);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Destination";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // createFolderButton
            // 
            createFolderButton.Location = new System.Drawing.Point(172, 61);
            createFolderButton.Name = "createFolderButton";
            createFolderButton.Size = new System.Drawing.Size(200, 23);
            createFolderButton.TabIndex = 4;
            createFolderButton.Text = " Create Folder >>";
            createFolderButton.UseVisualStyleBackColor = true;
            // 
            // fileLinkButton
            // 
            fileLinkButton.Location = new System.Drawing.Point(172, 32);
            fileLinkButton.Name = "fileLinkButton";
            fileLinkButton.Size = new System.Drawing.Size(200, 23);
            fileLinkButton.TabIndex = 3;
            fileLinkButton.Text = ">> Link File >>";
            fileLinkButton.UseVisualStyleBackColor = true;
            // 
            // folderLinkButton
            // 
            folderLinkButton.Location = new System.Drawing.Point(172, 3);
            folderLinkButton.Name = "folderLinkButton";
            folderLinkButton.Size = new System.Drawing.Size(200, 23);
            folderLinkButton.TabIndex = 2;
            folderLinkButton.Text = ">> Link Folder >>";
            folderLinkButton.UseVisualStyleBackColor = true;
            // 
            // treeView2
            // 
            treeView2.Location = new System.Drawing.Point(378, 3);
            treeView2.Name = "treeView2";
            treeView2.Size = new System.Drawing.Size(163, 488);
            treeView2.TabIndex = 1;
            // 
            // treeView1
            // 
            treeView1.Location = new System.Drawing.Point(3, 3);
            treeView1.Name = "treeView1";
            treeView1.Size = new System.Drawing.Size(163, 488);
            treeView1.TabIndex = 0;
            // 
            // tabPage4
            // 
            tabPage4.Location = new System.Drawing.Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new System.Drawing.Size(544, 499);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Copier";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // refreshDataCheckBox
            // 
            refreshDataCheckBox.Location = new System.Drawing.Point(169, 6);
            refreshDataCheckBox.Name = "refreshDataCheckBox";
            refreshDataCheckBox.Size = new System.Drawing.Size(146, 24);
            refreshDataCheckBox.TabIndex = 6;
            refreshDataCheckBox.Text = "Refresh existing data";
            refreshDataCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(739, 580);
            Controls.Add(mainViewTabControl);
            Controls.Add(addFolderButton);
            Controls.Add(selectedFolderTextBox);
            Controls.Add(folderListBox);
            Controls.Add(button2);
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            mainViewTabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            duplicateContextMenuStrip.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.CheckBox refreshDataCheckBox;

        private System.Windows.Forms.Button folderLinkButton;
        private System.Windows.Forms.Button fileLinkButton;
        private System.Windows.Forms.Button createFolderButton;

        #endregion

        private Button ReadFolderContents;
        private Button button2;
        private TreeView directoryTreeView;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private ListBox folderListBox;
        private TextBox selectedFolderTextBox;
        private Button addFolderButton;
        private TabControl mainViewTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private TabPage tabPage2;
        private Button findDuplicateButton;
        private TreeView duplicateTreeView;
        private ContextMenuStrip duplicateContextMenuStrip;
        private ToolStripMenuItem calculateMD5ToolStripMenuItem;
        private ProgressBar md5ProgressBar;
        private System.Windows.Forms.TabPage tabPage3;
        private TabPage tabPage4;
        private TreeView treeView1;
        private TreeView treeView2;
    }
}

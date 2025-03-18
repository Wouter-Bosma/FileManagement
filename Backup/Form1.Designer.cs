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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ReadFolderContents = new Button();
            button2 = new Button();
            directoryTreeView = new TreeView();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            folderListBox = new ListBox();
            selectedFolderTextBox = new TextBox();
            addFolderButton = new Button();
            mainViewTabControl = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            duplicateTreeView = new TreeView();
            duplicateContextMenuStrip = new ContextMenuStrip(components);
            calculateMD5ToolStripMenuItem = new ToolStripMenuItem();
            findDuplicateButton = new Button();
            md5ProgressBar = new ProgressBar();
            mainViewTabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            duplicateContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // ReadFolderContents
            // 
            ReadFolderContents.Location = new Point(6, 6);
            ReadFolderContents.Name = "ReadFolderContents";
            ReadFolderContents.Size = new Size(157, 23);
            ReadFolderContents.TabIndex = 0;
            ReadFolderContents.Text = "Read folder contents";
            ReadFolderContents.UseVisualStyleBackColor = true;
            ReadFolderContents.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(175, 12);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // directoryTreeView
            // 
            directoryTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            directoryTreeView.Location = new Point(6, 32);
            directoryTreeView.Name = "directoryTreeView";
            directoryTreeView.Size = new Size(532, 372);
            directoryTreeView.TabIndex = 2;
            directoryTreeView.NodeMouseClick += directoryTreeView_NodeMouseClick;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(6, 410);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(532, 23);
            textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(6, 439);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(532, 23);
            textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox3.Location = new Point(6, 468);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(532, 23);
            textBox3.TabIndex = 5;
            // 
            // folderListBox
            // 
            folderListBox.FormattingEnabled = true;
            folderListBox.Location = new Point(12, 40);
            folderListBox.Name = "folderListBox";
            folderListBox.Size = new Size(157, 484);
            folderListBox.TabIndex = 6;
            folderListBox.SelectedValueChanged += folderListBox_SelectedValueChanged;
            folderListBox.DoubleClick += folderListBox_DoubleClick;
            // 
            // selectedFolderTextBox
            // 
            selectedFolderTextBox.Location = new Point(12, 533);
            selectedFolderTextBox.Name = "selectedFolderTextBox";
            selectedFolderTextBox.Size = new Size(157, 23);
            selectedFolderTextBox.TabIndex = 7;
            // 
            // addFolderButton
            // 
            addFolderButton.Location = new Point(12, 12);
            addFolderButton.Name = "addFolderButton";
            addFolderButton.Size = new Size(157, 23);
            addFolderButton.TabIndex = 8;
            addFolderButton.Text = "Add Folder";
            addFolderButton.UseVisualStyleBackColor = true;
            addFolderButton.Click += addFolderButton_Click;
            // 
            // mainViewTabControl
            // 
            mainViewTabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainViewTabControl.Controls.Add(tabPage1);
            mainViewTabControl.Controls.Add(tabPage2);
            mainViewTabControl.Location = new Point(175, 41);
            mainViewTabControl.Name = "mainViewTabControl";
            mainViewTabControl.SelectedIndex = 0;
            mainViewTabControl.Size = new Size(552, 527);
            mainViewTabControl.TabIndex = 9;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(directoryTreeView);
            tabPage1.Controls.Add(ReadFolderContents);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Controls.Add(textBox2);
            tabPage1.Controls.Add(textBox3);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(544, 499);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Directory View";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(md5ProgressBar);
            tabPage2.Controls.Add(duplicateTreeView);
            tabPage2.Controls.Add(findDuplicateButton);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(544, 499);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Duplicate List";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // duplicateTreeView
            // 
            duplicateTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            duplicateTreeView.ContextMenuStrip = duplicateContextMenuStrip;
            duplicateTreeView.Location = new Point(6, 35);
            duplicateTreeView.Name = "duplicateTreeView";
            duplicateTreeView.Size = new Size(532, 372);
            duplicateTreeView.TabIndex = 11;
            duplicateTreeView.NodeMouseClick += duplicateTreeView_NodeMouseClick;
            duplicateTreeView.NodeMouseDoubleClick += duplicateTreeView_NodeMouseDoubleClick;
            // 
            // duplicateContextMenuStrip
            // 
            duplicateContextMenuStrip.Items.AddRange(new ToolStripItem[] { calculateMD5ToolStripMenuItem });
            duplicateContextMenuStrip.Name = "duplicateContextMenuStrip";
            duplicateContextMenuStrip.Size = new Size(182, 26);
            duplicateContextMenuStrip.Click += duplicateContextMenuStrip_Click;
            // 
            // calculateMD5ToolStripMenuItem
            // 
            calculateMD5ToolStripMenuItem.Name = "calculateMD5ToolStripMenuItem";
            calculateMD5ToolStripMenuItem.Size = new Size(181, 22);
            calculateMD5ToolStripMenuItem.Text = "Calculate MD5 Hash";
            calculateMD5ToolStripMenuItem.Click += calculateMD5ToolStripMenuItem_Click;
            // 
            // findDuplicateButton
            // 
            findDuplicateButton.Location = new Point(6, 6);
            findDuplicateButton.Name = "findDuplicateButton";
            findDuplicateButton.Size = new Size(99, 23);
            findDuplicateButton.TabIndex = 10;
            findDuplicateButton.Text = "Find Duplicates";
            findDuplicateButton.UseVisualStyleBackColor = true;
            findDuplicateButton.Click += findDuplicateButton_Click;
            // 
            // md5ProgressBar
            // 
            md5ProgressBar.Location = new Point(6, 413);
            md5ProgressBar.Name = "md5ProgressBar";
            md5ProgressBar.Size = new Size(151, 23);
            md5ProgressBar.TabIndex = 12;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(739, 580);
            Controls.Add(mainViewTabControl);
            Controls.Add(addFolderButton);
            Controls.Add(selectedFolderTextBox);
            Controls.Add(folderListBox);
            Controls.Add(button2);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            mainViewTabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            duplicateContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

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
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button findDuplicateButton;
        private TreeView duplicateTreeView;
        private ContextMenuStrip duplicateContextMenuStrip;
        private ToolStripMenuItem calculateMD5ToolStripMenuItem;
        private ProgressBar md5ProgressBar;
    }
}

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
            tabControl1 = new TabControl();
            sourceTabPage = new TabPage();
            dataOverviewControl1 = new Backup.DataOverviewControl(true);
            targetTabPage = new TabPage();
            dataOverviewControl2 = new Backup.DataOverviewControl(false);
            copyConfigTabPage = new TabPage();
            groupBox1 = new GroupBox();
            overwriteChangedHashRadioButton = new RadioButton();
            cloneHasOnCopyCheckBox = new CheckBox();
            overwriteRadioButton = new RadioButton();
            overwriteChangedSourceRadioButton = new RadioButton();
            noOverwriteRadioButton = new RadioButton();
            copyButton = new Button();
            selectionTextBox = new TextBox();
            createLinkButton = new Button();
            copyConfigurationListBox = new ListBox();
            targetTextBox = new TextBox();
            sourceTextBox = new TextBox();
            targetTreeView = new TreeView();
            sourceTreeView = new TreeView();
            tabControl1.SuspendLayout();
            sourceTabPage.SuspendLayout();
            targetTabPage.SuspendLayout();
            copyConfigTabPage.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(sourceTabPage);
            tabControl1.Controls.Add(targetTabPage);
            tabControl1.Controls.Add(copyConfigTabPage);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1880, 868);
            tabControl1.TabIndex = 1;
            tabControl1.Selected += tabControl1_Selected;
            // 
            // sourceTabPage
            // 
            sourceTabPage.Controls.Add(dataOverviewControl1);
            sourceTabPage.Location = new Point(4, 34);
            sourceTabPage.Name = "sourceTabPage";
            sourceTabPage.Padding = new Padding(3);
            sourceTabPage.Size = new Size(1872, 830);
            sourceTabPage.TabIndex = 0;
            sourceTabPage.Text = "Source Data";
            sourceTabPage.UseVisualStyleBackColor = true;
            // 
            // dataOverviewControl1
            // 
            dataOverviewControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataOverviewControl1.Location = new Point(-4, 0);
            dataOverviewControl1.Name = "dataOverviewControl1";
            dataOverviewControl1.Size = new Size(1870, 824);
            dataOverviewControl1.TabIndex = 0;
            // 
            // targetTabPage
            // 
            targetTabPage.Controls.Add(dataOverviewControl2);
            targetTabPage.Location = new Point(4, 34);
            targetTabPage.Name = "targetTabPage";
            targetTabPage.Padding = new Padding(3);
            targetTabPage.Size = new Size(1872, 830);
            targetTabPage.TabIndex = 1;
            targetTabPage.Text = "Target Data";
            targetTabPage.UseVisualStyleBackColor = true;
            // 
            // dataOverviewControl2
            // 
            dataOverviewControl2.Location = new Point(-4, 0);
            dataOverviewControl2.Name = "dataOverviewControl2";
            dataOverviewControl2.Size = new Size(1870, 824);
            dataOverviewControl2.TabIndex = 0;
            // 
            // copyConfigTabPage
            // 
            copyConfigTabPage.Controls.Add(groupBox1);
            copyConfigTabPage.Controls.Add(copyButton);
            copyConfigTabPage.Controls.Add(selectionTextBox);
            copyConfigTabPage.Controls.Add(createLinkButton);
            copyConfigTabPage.Controls.Add(copyConfigurationListBox);
            copyConfigTabPage.Controls.Add(targetTextBox);
            copyConfigTabPage.Controls.Add(sourceTextBox);
            copyConfigTabPage.Controls.Add(targetTreeView);
            copyConfigTabPage.Controls.Add(sourceTreeView);
            copyConfigTabPage.Location = new Point(4, 34);
            copyConfigTabPage.Name = "copyConfigTabPage";
            copyConfigTabPage.Size = new Size(1872, 830);
            copyConfigTabPage.TabIndex = 2;
            copyConfigTabPage.Text = "Copy Configuration";
            copyConfigTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(overwriteChangedHashRadioButton);
            groupBox1.Controls.Add(cloneHasOnCopyCheckBox);
            groupBox1.Controls.Add(overwriteRadioButton);
            groupBox1.Controls.Add(overwriteChangedSourceRadioButton);
            groupBox1.Controls.Add(noOverwriteRadioButton);
            groupBox1.Location = new Point(1366, 686);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(498, 141);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Copy Options";
            // 
            // overwriteChangedHashRadioButton
            // 
            overwriteChangedHashRadioButton.AutoSize = true;
            overwriteChangedHashRadioButton.Location = new Point(12, 100);
            overwriteChangedHashRadioButton.Name = "overwriteChangedHashRadioButton";
            overwriteChangedHashRadioButton.Size = new Size(228, 29);
            overwriteChangedHashRadioButton.TabIndex = 10;
            overwriteChangedHashRadioButton.TabStop = true;
            overwriteChangedHashRadioButton.Text = "Overwrite changed hash";
            overwriteChangedHashRadioButton.UseVisualStyleBackColor = true;
            // 
            // cloneHasOnCopyCheckBox
            // 
            cloneHasOnCopyCheckBox.AutoSize = true;
            cloneHasOnCopyCheckBox.Location = new Point(297, 100);
            cloneHasOnCopyCheckBox.Name = "cloneHasOnCopyCheckBox";
            cloneHasOnCopyCheckBox.Size = new Size(195, 29);
            cloneHasOnCopyCheckBox.TabIndex = 12;
            cloneHasOnCopyCheckBox.Text = "Clone hash on copy";
            cloneHasOnCopyCheckBox.UseVisualStyleBackColor = true;
            // 
            // overwriteRadioButton
            // 
            overwriteRadioButton.AutoSize = true;
            overwriteRadioButton.Location = new Point(179, 30);
            overwriteRadioButton.Name = "overwriteRadioButton";
            overwriteRadioButton.Size = new Size(171, 29);
            overwriteRadioButton.TabIndex = 8;
            overwriteRadioButton.TabStop = true;
            overwriteRadioButton.Text = "Overwrite all files";
            overwriteRadioButton.UseVisualStyleBackColor = true;
            // 
            // overwriteChangedSourceRadioButton
            // 
            overwriteChangedSourceRadioButton.AutoSize = true;
            overwriteChangedSourceRadioButton.Location = new Point(12, 65);
            overwriteChangedSourceRadioButton.Name = "overwriteChangedSourceRadioButton";
            overwriteChangedSourceRadioButton.Size = new Size(338, 29);
            overwriteChangedSourceRadioButton.TabIndex = 9;
            overwriteChangedSourceRadioButton.TabStop = true;
            overwriteChangedSourceRadioButton.Text = "Overwrite Changed Source Write Time";
            overwriteChangedSourceRadioButton.UseVisualStyleBackColor = true;
            // 
            // noOverwriteRadioButton
            // 
            noOverwriteRadioButton.AutoSize = true;
            noOverwriteRadioButton.Checked = true;
            noOverwriteRadioButton.Location = new Point(12, 30);
            noOverwriteRadioButton.Name = "noOverwriteRadioButton";
            noOverwriteRadioButton.Size = new Size(139, 29);
            noOverwriteRadioButton.TabIndex = 11;
            noOverwriteRadioButton.TabStop = true;
            noOverwriteRadioButton.Text = "No overwrite";
            noOverwriteRadioButton.UseVisualStyleBackColor = true;
            // 
            // copyButton
            // 
            copyButton.Location = new Point(1153, 686);
            copyButton.Name = "copyButton";
            copyButton.Size = new Size(112, 34);
            copyButton.TabIndex = 7;
            copyButton.Text = "Copy items";
            copyButton.UseVisualStyleBackColor = true;
            copyButton.Click += copyButton_Click;
            // 
            // selectionTextBox
            // 
            selectionTextBox.Location = new Point(1153, 649);
            selectionTextBox.Name = "selectionTextBox";
            selectionTextBox.Size = new Size(711, 31);
            selectionTextBox.TabIndex = 6;
            // 
            // createLinkButton
            // 
            createLinkButton.Location = new Point(1158, 77);
            createLinkButton.Name = "createLinkButton";
            createLinkButton.Size = new Size(112, 34);
            createLinkButton.TabIndex = 5;
            createLinkButton.Text = "Create Link";
            createLinkButton.UseVisualStyleBackColor = true;
            createLinkButton.Click += createLinkButton_Click;
            // 
            // copyConfigurationListBox
            // 
            copyConfigurationListBox.FormattingEnabled = true;
            copyConfigurationListBox.Location = new Point(1153, 117);
            copyConfigurationListBox.Name = "copyConfigurationListBox";
            copyConfigurationListBox.Size = new Size(711, 529);
            copyConfigurationListBox.TabIndex = 4;
            copyConfigurationListBox.MouseClick += copyConfigurationListBox_MouseClick;
            copyConfigurationListBox.DoubleClick += copyConfigurationListBox_DoubleClick;
            // 
            // targetTextBox
            // 
            targetTextBox.Location = new Point(1153, 40);
            targetTextBox.Name = "targetTextBox";
            targetTextBox.Size = new Size(716, 31);
            targetTextBox.TabIndex = 3;
            // 
            // sourceTextBox
            // 
            sourceTextBox.Location = new Point(1153, 3);
            sourceTextBox.Name = "sourceTextBox";
            sourceTextBox.Size = new Size(716, 31);
            sourceTextBox.TabIndex = 2;
            // 
            // targetTreeView
            // 
            targetTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            targetTreeView.Location = new Point(725, 3);
            targetTreeView.Name = "targetTreeView";
            targetTreeView.Size = new Size(422, 824);
            targetTreeView.TabIndex = 1;
            targetTreeView.NodeMouseClick += targetTreeView_NodeMouseClick;
            // 
            // sourceTreeView
            // 
            sourceTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            sourceTreeView.Location = new Point(3, 3);
            sourceTreeView.Name = "sourceTreeView";
            sourceTreeView.Size = new Size(422, 824);
            sourceTreeView.TabIndex = 0;
            sourceTreeView.NodeMouseClick += sourceTreeView_NodeMouseClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 892);
            Controls.Add(tabControl1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            tabControl1.ResumeLayout(false);
            sourceTabPage.ResumeLayout(false);
            targetTabPage.ResumeLayout(false);
            copyConfigTabPage.ResumeLayout(false);
            copyConfigTabPage.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage sourceTabPage;
        private TabPage targetTabPage;
        private Backup.DataOverviewControl dataOverviewControl1;
        private Backup.DataOverviewControl dataOverviewControl2;
        private TabPage copyConfigTabPage;
        private TextBox targetTextBox;
        private TextBox sourceTextBox;
        private TreeView targetTreeView;
        private TreeView sourceTreeView;
        private Button createLinkButton;
        private ListBox copyConfigurationListBox;
        private Button copyButton;
        private TextBox selectionTextBox;
        private RadioButton overwriteChangedSourceRadioButton;
        private RadioButton overwriteRadioButton;
        private RadioButton overwriteChangedHashRadioButton;
        private CheckBox cloneHasOnCopyCheckBox;
        private RadioButton noOverwriteRadioButton;
        private GroupBox groupBox1;
    }
}

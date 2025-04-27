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
            tabControl1.SuspendLayout();
            sourceTabPage.SuspendLayout();
            targetTabPage.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(sourceTabPage);
            tabControl1.Controls.Add(targetTabPage);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1797, 1172);
            tabControl1.TabIndex = 1;
            // 
            // sourceTabPage
            // 
            sourceTabPage.Controls.Add(dataOverviewControl1);
            sourceTabPage.Location = new Point(4, 34);
            sourceTabPage.Name = "sourceTabPage";
            sourceTabPage.Padding = new Padding(3);
            sourceTabPage.Size = new Size(1789, 1134);
            sourceTabPage.TabIndex = 0;
            sourceTabPage.Text = "Source Data";
            sourceTabPage.UseVisualStyleBackColor = true;
            // 
            // dataOverviewControl1
            // 
            dataOverviewControl1.Location = new Point(-4, 0);
            dataOverviewControl1.Name = "dataOverviewControl1";
            dataOverviewControl1.Size = new Size(1506, 920);
            dataOverviewControl1.TabIndex = 0;
            // 
            // targetTabPage
            // 
            targetTabPage.Controls.Add(dataOverviewControl2);
            targetTabPage.Location = new Point(4, 34);
            targetTabPage.Name = "targetTabPage";
            targetTabPage.Padding = new Padding(3);
            targetTabPage.Size = new Size(1789, 1134);
            targetTabPage.TabIndex = 1;
            targetTabPage.Text = "Target Data";
            targetTabPage.UseVisualStyleBackColor = true;
            // 
            // dataOverviewControl2
            // 
            dataOverviewControl2.Location = new Point(-4, 0);
            dataOverviewControl2.Name = "dataOverviewControl2";
            dataOverviewControl2.Size = new Size(1770, 1175);
            dataOverviewControl2.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1821, 1196);
            Controls.Add(tabControl1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            tabControl1.ResumeLayout(false);
            sourceTabPage.ResumeLayout(false);
            targetTabPage.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage sourceTabPage;
        private TabPage targetTabPage;
        private Backup.DataOverviewControl dataOverviewControl1;
        private Backup.DataOverviewControl dataOverviewControl2;
    }
}

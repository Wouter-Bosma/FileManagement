using BackupSolution.FolderReader;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Backup.Copier;
using Backup.Tools;

namespace BackupSolution
{
    public partial class Form1 : Form
    {
        private TreeNode? _sourceTreeNode = null;
        private TreeNode? _targetTreeNode = null;
        private CopyData? _selected = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Configuration.Instance.Save();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            TreeHelper.DrawTree(sourceTreeView, Configuration.Instance.GetFolderData(true));
            TreeHelper.DrawTree(targetTreeView, Configuration.Instance.GetFolderData(false));
            DrawCopyConfiguration(copyConfigurationListBox);
        }

        private void targetTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            _targetTreeNode = TreeHelper.NodeSelected(e, targetTextBox);
        }

        private void sourceTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            _sourceTreeNode = TreeHelper.NodeSelected(e, sourceTextBox);
        }

        private void createLinkButton_Click(object sender, EventArgs e)
        {
            if (_sourceTreeNode == null || _targetTreeNode == null)
            {
                return;
            }

            bool sourceIsFolder = _sourceTreeNode.Tag is FolderData;

            Configuration.Instance.CopyPairs.Pairs.Add(new CopyData(TreeHelper.GetNodeName(_sourceTreeNode), TreeHelper.GetNodeName(_targetTreeNode), sourceIsFolder));
            DrawCopyConfiguration(copyConfigurationListBox);
        }

        private void DrawCopyConfiguration(ListBox targetListBox)
        {
            targetListBox.Items.Clear();
            foreach (var item in Configuration.Instance.CopyPairs.Pairs)
            {
                targetListBox.Items.Add(item.ReadableString);
            }
        }

        private void copyConfigurationListBox_DoubleClick(object sender, EventArgs e)
        {
            if (copyConfigurationListBox?.SelectedItem is not string item || MessageBox.Show($"Ok to Remove {item}?", "Remove question", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            CopyData? toDelete = null;
            foreach (var copyItem in Configuration.Instance.CopyPairs.Pairs)
            {
                if (copyItem.ReadableString == item)
                {
                    toDelete = copyItem;
                    break;
                }
            }

            if (toDelete != null)
            {
                Configuration.Instance.CopyPairs.Pairs.Remove(toDelete);
            }
            DrawCopyConfiguration(copyConfigurationListBox);
        }

        private void copyConfigurationListBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (copyConfigurationListBox?.SelectedItem is not string item)
            {
                return;
            }
            _selected = null;
            foreach (var copyItem in Configuration.Instance.CopyPairs.Pairs)
            {
                if (copyItem.ReadableString == item)
                {
                    _selected = copyItem;
                    break;
                }
            }

            selectionTextBox.Text = _selected?.ReadableString ?? "No Selection";
        }

        private void UpdateGui(CopyInfo copyInfo)
        {
            while (!copyButton.Enabled)
            {
                Thread.Sleep(100);
            }
        }

        private async void copyButton_Click(object sender, EventArgs e)
        {
            copyButton.Enabled = false;
            if (_selected == null)
            {
                return;
            }

            CopySetting setting = CopySetting.NoOverwrite;
            if (noOverwriteRadioButton.Checked)
            {
                setting = CopySetting.NoOverwrite;
            }
            else if (overwriteRadioButton.Checked)
            {
                setting = CopySetting.OverwriteAll;
            }
            else if (overwriteChangedSourceRadioButton.Checked)
            {
                setting = CopySetting.OverwriteChangedSourceWriteTime;
            }
            else if (overwriteChangedHashRadioButton.Checked)
            {
                setting = CopySetting.OverwriteChangedHash;
            }

            var copyInfo = new CopyInfo();
            var updatedGui = Task.Run(() => UpdateGui(copyInfo));
            await CopyHelper.CopyFromSourceToTarget(_selected, setting, cloneHasOnCopyCheckBox.Checked, copyInfo);
            copyButton.Enabled = true;
            await updatedGui;
        }
    }
}

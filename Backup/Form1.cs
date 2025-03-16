using BackupSolution.FolderReader;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;
//using Microsoft.Data.Sqlite;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BackupSolution
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            config.Load(ref root);
            DrawFolderList();
        }

        private FolderData root = null;
        private Configuration config = new();
        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            try
            {
                foreach (var folder in config.Folders.Where(x => !root.TryGetFolderData(x, out _)))
                {
                    root.Folders.Add(await ReadFiles.ReadFilesRecursive(folder, root));
                }

                config.Save(root);
                //string jsonString = JsonSerializer.Serialize(root);
                //await File.WriteAllTextAsync(@"M:\W.json", jsonString);
                DrawTree();
            }
            finally
            {
                button1.Enabled = true;
            }
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            try
            {
                var readData = File.ReadAllTextAsync(@"M:\W.json");
                await readData;
                if (readData.IsCompletedSuccessfully)
                {
                    root = JsonSerializer.Deserialize<FolderData>(readData.Result);
                    root?.Reset();
                }
                //await ImportDataFromDatabaseAsync();
            }
            finally
            {
                button2.Enabled = true;
            }
            DrawTree();
        }

        private TreeNode CreateTree(FolderData current)
        {
            var result = new TreeNode(current.FolderName);
            result.Tag = current;
            foreach (var item in current.Files)
            {
                result.Nodes.Add(item.FileName);
            }

            foreach (var item in current.Folders)
            {
                result.Nodes.Add(CreateTree(item));
            }

            return result;
        }

        private void DrawTree()
        {
            directoryTreeView.Nodes.Clear();

            directoryTreeView.Nodes.Add(CreateTree(root));

        }

        private void DrawFolderList()
        {
            folderListBox.Items.Clear();
            foreach (var folder in config.Folders.OrderBy(x => x))
            {
                folderListBox.Items.Add(folder);
            }
        }

        private void directoryTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var x = e.Node;
            if (x == null)
            {
                return;
            }
            if (x.Tag is FolderData fdCurrent)
            {
                textBox1.Text = fdCurrent.FolderName;
                textBox2.Text = $"Folders: {fdCurrent.ChildFolders:N0} - Files: {fdCurrent.ChildFiles:N0}";
                textBox3.Text = $"Size: {(fdCurrent.ChildFileSize / 1024 / 1024 / 1024):N0} GB / {fdCurrent.ChildFileSize:N0} BYTE";
                return;
            }

            if (x.Parent == null)
            {
                return;
            }
            if (x.Parent.Tag is FolderData fdParent)
            {
                textBox1.Text = fdParent.FolderName;
                textBox2.Text = x.Text;
                var item = fdParent.Files.First(y => y.FileName == x.Text);
                textBox3.Text = $"{item.FileSize:N0} - {item.LastWriteTime:yyyy.MM.dd - HH:mm:ss}";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            config.Save(root);
        }

        private void addFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Multiselect = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                if (!config.Folders.Contains(fbd.SelectedPath))
                {
                    config.Folders.Add(fbd.SelectedPath);
                }
                DrawFolderList();
            }
        }

        private void folderListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (folderListBox?.SelectedItem is string item)
            {
                selectedFolderTextBox.Text = item;
            }
        }

        private void folderListBox_DoubleClick(object sender, EventArgs e)
        {
            if (folderListBox?.SelectedItem is not string item || MessageBox.Show($"Ok to Remove {item}?", "Remove question", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            //Remove data from root tree
            config.Folders.Remove(item);
            DrawFolderList();
        }

        private void findDuplicateButton_Click(object sender, EventArgs e)
        {

        }
    }
}

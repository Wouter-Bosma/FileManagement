using BackupSolution.FolderReader;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
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
            ReadFolderContents.Enabled = false;
            try
            {
                foreach (var folder in config.Folders.Where(x => !root.TryGetFolderData(x, out _)))
                {
                    root.Folders.Add(await ReadFiles.ReadFilesRecursive(folder, root));
                }

                config.Save(root);
                DrawTree();
            }
            finally
            {
                ReadFolderContents.Enabled = true;
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
            duplicateTreeView.Nodes.Clear();
            var result = new Dictionary<long, List<FileData>>();
            foreach (var fd in root.EnumerateOverAllFiles())
            {
                if (result.TryGetValue(fd.FileSize, out var items))
                {
                    items.Add(fd);
                }
                else
                {
                    result[fd.FileSize] = [fd];
                }
            }

            filtered = result.Where(x => x.Value.Count() > 1).ToDictionary(x => x.Key, y => y.Value);
            var rootNode = new TreeNode("Root");
            foreach (var kvp in filtered.OrderBy(x => -x.Key * (x.Value.Count-1)))
            {
                var node = new TreeNode(kvp.Key.ToString());
                foreach (var item in kvp.Value)
                {
                    node.Nodes.Add(new TreeNode(item.FullPathWithMd5) { Tag = item });
                }

                rootNode.Nodes.Add(node);
            }
            duplicateTreeView.Nodes.AddRange(rootNode);
        }

        private Dictionary<long, List<FileData>> filtered = null;
        private async void duplicateTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null)
            {
                return;
            }
            if (e.Node.Tag == null)
            {
                duplicateTreeView.Enabled = false;
                foreach (var child in e.Node.Nodes)
                {
                    if (child is not TreeNode childNode || childNode.Tag is not FileData fileData)
                    {
                        continue;
                    }

                    if (await fileData.CalculateMd5Hash())
                    {
                        childNode.Text = $@"{fileData.FullPathWithMd5}";
                    }
                }
                duplicateTreeView.Enabled = true;
            }
        }

        private void duplicateContextMenuStrip_Click(object sender, EventArgs e)
        {
            //var cms = (ContextMenuStrip)sender;
            //cms.
        }

        private TreeNode? selectedNode = null;

        private CancellationTokenSource? cts = null;

        private async void calculateMD5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cts == null)
            {
                cts = new CancellationTokenSource();
                await ProcessClickToCalculateHash(selectedNode, cts.Token);
            }
            else
            {
                await cts?.CancelAsync()!;
                cts = null;
            }
        }

        private async Task ProcessClickToCalculateHash(TreeNode? nodeToProcess, CancellationToken ct)
        {
            if (nodeToProcess == null || nodeToProcess.Nodes.Count == 0)
            {
                return;
            }
            if (nodeToProcess.Nodes[0].Tag is FileData fd)
            {
                //md5ProgressBar.Value = 0;
                await CalculateHashes(nodeToProcess, ct);
                //md5ProgressBar.Value = 100;
                //cts = null;//TODO: fix: Thread unsafe
            }

            
            if (nodeToProcess.Text == "Root")
            {
                var total = filtered.Sum(x => x.Key * x.Value.Count);
                var calculated = filtered.Sum(x => x.Key * x.Value.Count(y => string.IsNullOrEmpty(y.MD5Hash)));
                var tasks = new List<Task>();
                foreach (var node in nodeToProcess.Nodes)
                {
                    tasks.Add(ProcessClickToCalculateHash((TreeNode)node, ct));
                    if (ct.IsCancellationRequested)
                    {
                        break;
                    }

                    if (tasks.Count > 2)
                    {
                        var completed = await Task.WhenAny(tasks);
                        tasks.Remove(completed);
                    }
                    //var calculated = filtered.Sum(x => x.Key * x.Value.Count(y => string.IsNullOrEmpty(y.MD5Hash)));
                    //md5ProgressBar.Value = (int)(100 * calculated / total);
                }

                await Task.WhenAll(tasks);

                cts = null; //TODO: fix: Thread unsafe
            }
        }

        private async Task CalculateHashes(TreeNode nodeToCalculate, CancellationToken ct)
        {
            
            nodeToCalculate.BackColor = Color.Yellow;
            for (int i = 0; i < nodeToCalculate.Nodes.Count; ++i)
            {
                var treeNode = nodeToCalculate.Nodes[i];
                if (treeNode.Tag is not FileData data)
                {
                    continue;
                }
                if (await data.CalculateMd5Hash())
                {
                    treeNode.Text = data.FullPathWithMd5;
                }

                treeNode.ForeColor = Color.DarkBlue;
                if (ct.IsCancellationRequested)
                {
                    return;
                }
            }

            nodeToCalculate.BackColor = Color.BlanchedAlmond;
        }

        private void duplicateTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            selectedNode = e.Node;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backup.FolderReader;
using BackupSolution;
using BackupSolution.FolderReader;
using NLog.Filters;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Backup
{
    public partial class DataOverviewControl : UserControl
    {
        private TreeNode? selectedNode = null;
        private CancellationTokenSource? cts = null;
        private Dictionary<long, List<FileData>>? filtered = null;
        private readonly bool _isSourceWindow = true;

        private Md5Hasher _hasher;
        /*public DataOverviewControl()
        {
            InitializeComponent();
            DrawFolderList();
            
        }*/

        public DataOverviewControl(bool isSourceWindow)
        {
            InitializeComponent();
            _isSourceWindow = isSourceWindow;
            DrawFolderList();
            _hasher = new Md5Hasher(isSourceWindow);
        }

        public void Closing()
        {
            Configuration.Instance.Save();
        }

        private void DrawFolderList()
        {
            folderListBox.Items.Clear();
            foreach (var folder in Configuration.Instance.Folders(_isSourceWindow).OrderBy(x => x))
            {
                folderListBox.Items.Add(folder);
            }
        }

        private void DataOverviewControl_Load(object sender, EventArgs e)
        {
        }

        private void addFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Multiselect = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                if (!Configuration.Instance.Folders(_isSourceWindow).Contains(fbd.SelectedPath))
                {
                    Configuration.Instance.Folders(_isSourceWindow).Add(fbd.SelectedPath);
                }
                DrawFolderList();
            }
        }

        private void folderListBox_DoubleClick(object sender, EventArgs e)
        {
            if (folderListBox?.SelectedItem is not string item || MessageBox.Show($"Ok to Remove {item}?", "Remove question", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            if (Configuration.Instance.Folders(_isSourceWindow).Remove(item))
            {
                DrawFolderList();
            }

            var itemToRemove = Configuration.Instance.GetFolderData(_isSourceWindow).FindFolder(item);
            if (itemToRemove != null)
            {
                bool removed = Configuration.Instance.GetFolderData(_isSourceWindow).RemoveFolder(itemToRemove);
                if (removed)
                {
                    Configuration.Instance.GetFolderData(_isSourceWindow).Init();
                    DrawTree(directoryTreeView, Configuration.Instance.GetFolderData(_isSourceWindow));
                }
            }
        }

        private void folderListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (folderListBox?.SelectedItem is string item)
            {
                selectedFolderTextBox.Text = item;
            }
        }

        private void mainViewTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.Action == TabControlAction.Selected && e?.TabPage != null && e.TabPage.Text == "Destination")
            {
                DrawTree(sourceTreeView, Configuration.Instance.GetFolderData(_isSourceWindow));
                //DrawTree(targetTreeView, Configuration.Instance.TargetData);
            }
        }

        private void DrawTree(TreeView myTree, FolderData folderToDraw)
        {
            myTree.Nodes.Clear();

            myTree.Nodes.Add(CreateTree(folderToDraw));

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

        private async void ReadFolderContents_Click(object sender, EventArgs e)
        {
            ReadFolderContents.Enabled = false;
            refreshDataCheckBox.Enabled = false;
            var refreshData = refreshDataCheckBox.Checked;
            try
            {
                //Add selected node
                if (folderListBox?.SelectedItem is string item)
                {
                    if (!Configuration.Instance.GetFolderData(_isSourceWindow).TryGetFolderData(item, out _))
                    {
                        Configuration.Instance.GetFolderData(_isSourceWindow).Folders.Add(await ReadFiles.ReadFilesRecursive(item, Configuration.Instance.GetFolderData(_isSourceWindow), refreshData));
                    }
                    else
                    {
                        await ReadFiles.ReadFilesRecursive(item, Configuration.Instance.GetFolderData(_isSourceWindow), refreshData);
                    }
                }
                else
                {
                    //Add only unavailable nodes.
                    foreach (var folder in Configuration.Instance.Folders(_isSourceWindow))
                    {
                        if (!Configuration.Instance.GetFolderData(_isSourceWindow).TryGetFolderData(folder, out _))
                        {
                            Configuration.Instance.GetFolderData(_isSourceWindow).Folders.Add(await ReadFiles.ReadFilesRecursive(folder, Configuration.Instance.GetFolderData(_isSourceWindow), refreshData));
                        }
                        else if (refreshData)
                        {
                            await ReadFiles.ReadFilesRecursive(folder, Configuration.Instance.GetFolderData(_isSourceWindow), refreshData);
                        }
                    }
                }
                Configuration.Instance.Save();
                DrawTree(directoryTreeView, Configuration.Instance.GetFolderData(_isSourceWindow));
            }
            finally
            {
                ReadFolderContents.Enabled = true;
                refreshDataCheckBox.Enabled = true;
            }
        }

        private void drawTreeButton_Click(object sender, EventArgs e)
        {
            DrawTree(directoryTreeView, Configuration.Instance.GetFolderData(_isSourceWindow));
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
                textBox3.Text = $"Size: {(fdCurrent.ChildFileSize / 1024 / 1024 / 1024):N0} GB / {fdCurrent.ChildFileSize:N0} B";
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
                textBox3.Text = $"{item.FileSize:N0} B - {item.LastWriteTime:yyyy.MM.dd - HH:mm:ss} - Hash: {item.MD5Hash}";
            }
        }

        private void findDuplicateButton_Click(object sender, EventArgs e)
        {
            duplicateTreeView.Nodes.Clear();
            var result = new Dictionary<long, List<FileData>>();
            foreach (var fd in Configuration.Instance.GetFolderData(_isSourceWindow).EnumerateOverAllFiles())
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
            var rootNode = new TreeNode("SourceData");
            foreach (var kvp in filtered.OrderBy(x => -x.Key * (x.Value.Count - 1)))
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
        private void duplicateContextMenuStrip_Click(object sender, EventArgs e)
        {
            //var cms = (ContextMenuStrip)sender;
            //cms.
        }
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


            if (nodeToProcess.Text == "SourceData")
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

        private bool hashing = false;

        private void SetProgress(string text, int processed, int total, double hashedSize, double totalSize)
        {
            if (button1.InvokeRequired)
            {
                button1.Invoke(() => SetProgress(text, processed, total, hashedSize, totalSize));
            }
            else
            {
                fileProcessedTextBox.Text = text;
                if (totalSize != 0 && total != 0)
                {
                    progressTextBox.Text = $"[{processed}/{total} = {(100.0 * processed / total):N2}%] - [{hashedSize:N0}/{totalSize:N0} = {(100.0 * hashedSize / totalSize):N0}%]";
                }
                else
                {
                    progressTextBox.Text = "totalsize of files to hash or total number of files shouldn't be 0.";
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!hashing)
            {
                hashing = true;
                button1.BackColor = Color.Tomato;
                await _hasher.Start(false, SetProgress);
                button1.BackColor = Color.YellowGreen;
                progressTextBox.Text = "Finished";
                hashing = false;
            }
            else
            {
                button1.BackColor = Color.YellowGreen;
                _hasher.Stop();
            }
        }
    }
}

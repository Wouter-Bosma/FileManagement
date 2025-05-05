using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackupSolution.FolderReader;

namespace Backup.Tools
{
    internal static class TreeHelper
    {
        public static void DrawTree(TreeView myTree, FolderData folderToDraw)
        {
            myTree.Nodes.Clear();

            myTree.Nodes.Add(CreateTree(folderToDraw));
        }

        private static TreeNode CreateTree(FolderData current)
        {
            var result = new TreeNode(current.FolderName)
            {
                Tag = current
            };
            foreach (var item in current.Files)
            {
                result.Nodes.Add(new TreeNode(item.FileName){Tag = item});
            }

            foreach (var item in current.Folders)
            {
                result.Nodes.Add(CreateTree(item));
            }

            return result;
        }

        public static string GetNodeName(TreeNode? node)
        {
            if (node?.Tag == null)
            {
                return "Invalid";
            }

            if (node.Tag is FolderData folderCurrent)
            {
                return folderCurrent.FolderName;
            }

            if (node.Tag is FileData fileCurrent)
            {
                return fileCurrent.FullPath;
            }

            return "Invalid";
        }

        public static TreeNode? NodeSelected(TreeNodeMouseClickEventArgs e, TextBox infoTextBox)
        {
            var x = e.Node;
            if (x == null)
            {
                return null;
            }
            if (x.Tag is FolderData)
            {
                infoTextBox.Text = $"[Folder] {GetNodeName(x)}";
                return x;
            }
            if (x.Tag is FileData)
            {
                infoTextBox.Text = $"[File] {GetNodeName(x)}";
                return x;
            }

            return x;
        }
    }
}

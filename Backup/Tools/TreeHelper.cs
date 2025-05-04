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
                result.Nodes.Add(item.FileName);
            }

            foreach (var item in current.Folders)
            {
                result.Nodes.Add(CreateTree(item));
            }

            return result;
        }
    }
}

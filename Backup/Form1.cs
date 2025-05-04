using BackupSolution.FolderReader;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Backup.Tools;

namespace BackupSolution
{
    public partial class Form1 : Form
    {

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
        }
    }
}

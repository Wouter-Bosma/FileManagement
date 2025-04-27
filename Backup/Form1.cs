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
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataOverviewControl1?.Closing();
        }
    }
}

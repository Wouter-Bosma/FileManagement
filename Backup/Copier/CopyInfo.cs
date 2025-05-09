using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup.Copier
{
    internal class CopyInfo
    {
        private string _fileBeingCopied = string.Empty;
        private int _filesToCopy = 0;
        private int _filesCopied = 0;
        private readonly Lock _myLock = new();
        private List<string> _copiedFiles = new();
        public void FilesCompleted(string fileName)
        {
            lock (_myLock)
            {
                _copiedFiles.Add(fileName);
            }
        }

        public string FileBeingCopied
        {
            get
            {
                lock (_myLock)
                {
                    return _fileBeingCopied;
                }
            }
            set
            {
                {
                    lock (_myLock)
                    {
                        _fileBeingCopied = value;
                    }
                }
            }
        }

        public int FilesToCopy
        {
            get
            {
                lock (_myLock)
                {
                    return _filesToCopy;
                }
            }
            set
            {
                {
                    lock (_myLock)
                    {
                        _filesToCopy = value;
                    }
                }
            }
        }
        public int FilesCopied
        {
            get
            {
                lock (_myLock)
                {
                    return _filesCopied;
                }
            }
            set
            {
                {
                    lock (_myLock)
                    {
                        _filesCopied = value;
                    }
                }
            }
        }
    }
}

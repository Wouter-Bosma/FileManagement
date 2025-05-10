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
        private List<string> _fileBeingCopied = new();
        private int _filesToCopy = 0;
        private int _filesCopied = 0;
        private readonly Lock _myLock = new();
        private List<string> _copiedFiles = new();
        private bool filesCopyingChanged = true;

        public bool FilesCopyingChanged
        {
            get
            {
                lock (_myLock)
                {
                    return filesCopyingChanged;
                }
            }
        }

        public List<string> FilesCopying
        {
            get
            {
                lock (_myLock)
                {
                    filesCopyingChanged = false;
                    return [.._fileBeingCopied];
                }
            }
        }

        public List<string> CopiedFiles
        {
            get
            {
                lock (_myLock)
                {
                    return [.. _copiedFiles];
                }
            }
        }

        public void FileStart(string fileName)
        {
            lock (_myLock)
            {
                filesCopyingChanged = true;
                _fileBeingCopied.Add(fileName);
            }
        }

        public void FileFinish(string fileName)
        {
            lock (_myLock)
            {
                filesCopyingChanged = true;
                _fileBeingCopied.Remove(fileName);
                _copiedFiles.Add(fileName);
                ++_filesCopied;
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

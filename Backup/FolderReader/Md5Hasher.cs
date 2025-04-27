using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackupSolution;

namespace Backup.FolderReader
{
    internal class Md5Hasher
    {
        private Lock myLock = new Lock();
        private CancellationTokenSource? _tokenSource = null;
        private int _filesInScope = 0;
        private int _filesProcessed = 0;
        private int _filesNotProcessed = 0;
        private string _fileNameProcessing = string.Empty;
        private string _lastFileProcessed = string.Empty;

        public int FilesProcessed => _filesProcessed;
        public int FilesNotProcessed => _filesNotProcessed; 
        public int FilesInScope => _filesInScope;
        public string FileProcessing 
        {
            get { lock (myLock) return _fileNameProcessing; }
            set { lock (myLock) _fileNameProcessing = value; }
        }

        public string LastFileProcessed
        {
            get { lock (myLock) return _lastFileProcessed; }
            set { lock (myLock) _lastFileProcessed = value; }
        }

        public async Task Start()
        {
            if (_tokenSource == null)
            {
                _tokenSource = new CancellationTokenSource();
                await CalculateHashes(_tokenSource.Token);
                _tokenSource = null;
            }
        }

        public void Stop()
        {
            if (_tokenSource == null)
            {
                return;
            }
            _tokenSource.Cancel();
        }

        public async Task CalculateHashes(CancellationToken ct)
        {
            //Method to be called from Gui, as the folderdata is not intended to be thread safe we get the data first and process later.
            var allFiles = Configuration.Instance.GetFolderData(true).EnumerateOverAllFiles().OrderByDescending(x => x.FileSize).ToList();
            int result = 0;
            foreach (var fd in allFiles)
            {
                if (await fd.CalculateMd5Hash(ct))
                {
                    Interlocked.Increment(ref _filesProcessed);
                }
                else
                {
                    Interlocked.Increment(ref _filesNotProcessed);
                }

                if (ct.IsCancellationRequested)
                {
                    break;
                }
            }
        }
    }
}

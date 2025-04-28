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
        private readonly Lock _myLock = new();
        private CancellationTokenSource? _tokenSource = null;
        private int _filesInScope = 0;
        private int _filesProcessed = 0;
        private int _filesNotProcessed = 0;
        private string _fileNameProcessing = string.Empty;
        private string _lastFileProcessed = string.Empty;
        private bool _workOnSourceData = false;

        public Md5Hasher(bool workOnSourceData) => _workOnSourceData = workOnSourceData;

        public int FilesProcessed => _filesProcessed;
        public int FilesNotProcessed => _filesNotProcessed; 
        public int FilesInScope => _filesInScope;
        public string FileProcessing 
        {
            get { lock (_myLock) return _fileNameProcessing; }
            set { lock (_myLock) _fileNameProcessing = value; }
        }

        public string LastFileProcessed
        {
            get { lock (_myLock) return _lastFileProcessed; }
            set { lock (_myLock) _lastFileProcessed = value; }
        }

        public async Task Start(bool refreshAll)
        {
            if (_tokenSource == null)
            {
                _tokenSource = new CancellationTokenSource();
                await CalculateHashes(refreshAll, _tokenSource.Token);
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

        public async Task CalculateHashes(bool refreshAll, CancellationToken ct)
        {
            //Method to be called from Gui, as the folderdata is not intended to be thread safe we get the data first and process later.
            var allFiles = Configuration.Instance.GetFolderData(true).EnumerateOverAllFiles().OrderByDescending(x => x.FileSize).ToList();
            var processed = allFiles.Where(x => !refreshAll && !string.IsNullOrEmpty(x.MD5Hash)).ToList();
            var toProcess = allFiles.Where(x => refreshAll || string.IsNullOrEmpty(x.MD5Hash)).ToList();
            _filesProcessed = processed.Count;
            _filesNotProcessed = toProcess.Count;
            _filesInScope = _filesNotProcessed + _filesProcessed;
            foreach (var fd in toProcess)
            {
                _fileNameProcessing = fd.FileName;
                if (await fd.CalculateMd5Hash(ct))
                {
                    Interlocked.Increment(ref _filesProcessed);
                    Interlocked.Decrement(ref _filesNotProcessed);
                    _lastFileProcessed = fd.FileName;
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
            _fileNameProcessing = string.Empty;
        }
    }
}

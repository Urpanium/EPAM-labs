using System;
using System.IO;

namespace T4.BusinessLogicLayer
{
    public class FileWatcher : IDisposable
    {
        private FileSystemWatcher _fileSystemWatcher;
        private FileHandler _fileHandler;
        
        

        /* public delegate void FileEvent(object s, FileSystemEventArgs e);
        public FileEvent OnFileChangedEvent { get; set; } */

        public FileWatcher(string path, FileHandler fileHandler)
        {
            _fileHandler = fileHandler;

            _fileSystemWatcher = new FileSystemWatcher
            {
                Path = path,
                NotifyFilter = NotifyFilters.LastAccess
                               | NotifyFilters.LastWrite,
                Filter = "*.csv"
            };
        }

        public void Start()
        {
            _fileSystemWatcher.EnableRaisingEvents = true;
            _fileSystemWatcher.Changed += _fileHandler.OnDirectoryContentChanged;
            //_fileSystemWatcher.C
        }

        public void Stop()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
            _fileSystemWatcher.Changed -= _fileHandler.OnDirectoryContentChanged;
        }

        private bool _disposed;

        ~FileWatcher()
        {
            Dispose();
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _fileSystemWatcher.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
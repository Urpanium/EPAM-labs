using System;
using System.IO;

namespace T4.BusinessLogicLayer
{
    public class FileWatcher : IDisposable
    {
        private FileSystemWatcher _fileSystemWatcher;
        private FileHandler _fileHandler; // TODO: make bublic?


        /* public delegate void FileEvent(object s, FileSystemEventArgs e);
        public FileEvent OnFileChangedEvent { get; set; } */

        public FileWatcher(string path, FileHandler fileHandler)
        {
            _fileHandler = fileHandler;
            _fileSystemWatcher.Path = path;
            _fileSystemWatcher.NotifyFilter = NotifyFilters.LastAccess
                                              | NotifyFilters.LastWrite;

            _fileSystemWatcher.Filter = "*.csv";

            /*_fileSystemWatcher.Changed += OnFileChanged;
            _fileSystemWatcher.Created += OnFileChanged;

            _fileSystemWatcher.EnableRaisingEvents = true;*/
        }

        /*private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            OnFileChangedEvent.Invoke(sender, e);
        }*/

        private void Start()
        {
            _fileSystemWatcher.EnableRaisingEvents = true;
            _fileSystemWatcher.Changed += _fileHandler.OnDirectoryContentChanged;
        }

        private void Stop()
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
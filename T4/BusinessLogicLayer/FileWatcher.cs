using System;
using System.IO;
using Serilog;

namespace T4.BusinessLogicLayer
{
    public class FileWatcher : IDisposable
    {
        private FileSystemWatcher _fileSystemWatcher;
        private FileHandler _fileHandler;

        public FileWatcher(string path, FileHandler fileHandler)
        {
            _fileHandler = fileHandler;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            _fileSystemWatcher = new FileSystemWatcher
            {
                Path = path,
                NotifyFilter = NotifyFilters.LastAccess
                               | NotifyFilters.LastWrite
                               | NotifyFilters.DirectoryName
                               | NotifyFilters.FileName,
                Filter = "*.csv"
            };
        }

        public void Start()
        {
            _fileSystemWatcher.EnableRaisingEvents = true;
            _fileSystemWatcher.Created += _fileHandler.OnDirectoryContentChanged;
            Log.Information($"Watching {_fileSystemWatcher.Path}");
        }

        public void Stop()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
            _fileSystemWatcher.Created -= _fileHandler.OnDirectoryContentChanged;
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
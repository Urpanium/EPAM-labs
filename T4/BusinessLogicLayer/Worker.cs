using System;
using T4.DataAccessLayer;

namespace T4.BusinessLogicLayer
{
    public class Worker: IDisposable
    {

        private DatabaseManager _databaseManager;
        private FileHandler _fileHandler;
        private FileWatcher _fileWatcher;

        public Worker(DatabaseManager databaseManager, FileHandler fileHandler, FileWatcher fileWatcher)
        {

            _databaseManager = databaseManager;
            _fileHandler = fileHandler;
            _fileWatcher = fileWatcher;

            _fileHandler.OnSalesReadyEvent += _databaseManager.OnSalesReady;
            
        }


        public void Start()
        {
            _fileWatcher.Start();
        }

        public void Stop()
        {
            _fileWatcher.Stop();
        }


        private bool _disposed;

        ~Worker()
        {
            Dispose();
        }
        
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _fileWatcher.Dispose();
                    _databaseManager.Dispose();
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
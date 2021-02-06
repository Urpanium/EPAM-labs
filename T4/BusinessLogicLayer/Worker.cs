using T4.DataAccessLayer;

namespace T4.BusinessLogicLayer
{
    public class Worker
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
            // wow so genius mmm
            _fileWatcher.Start();
        }

        public void Stop()
        {
            _fileWatcher.Stop();
        }
        
    }
}
using System;
using System.IO;

namespace T4.BusinessLogicLayer
{
    //Big Brother  
    public class FileWatcher
    {
        /*public delegate void FileEvent();

        public FileEvent OnFileChanged;*/

        public FileWatcher(string path)
        {
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = path;
                watcher.NotifyFilter = NotifyFilters.LastAccess
                                       | NotifyFilters.LastWrite;

                watcher.Filter = "*.csv";

                watcher.Changed += OnFileChanged;
                watcher.Created += OnFileChanged;

                watcher.EnableRaisingEvents = true;
            }
        }

        private void OnFileChanged(object source, FileSystemEventArgs e)
        {
            // upload something
        }
    }
}
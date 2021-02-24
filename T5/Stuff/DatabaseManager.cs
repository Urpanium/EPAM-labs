using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Serilog;
using T5.Models;

namespace T5.Stuff
{
    public class DatabaseManager : IDisposable
    {
        private static readonly object Locker = new object();
        private readonly ApplicationDbContext _context;

        public DatabaseManager()
        {
            _context = new ApplicationDbContext();
        }

        private bool _disposed;

        ~DatabaseManager()
        {
            Dispose(true);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
using System;
using System.Collections.Generic;
using T4.DataAccessLayer;
using T4.DataLayer.Models;

namespace T4.BusinessLogicLayer
{
    public class DatabaseManager: IDisposable
    {
        private DatabaseContext _context;


        public DatabaseManager()
        {
            _context = new DatabaseContext("DBConnection");
        }

        public void OnSalesReady(IEnumerable<Sale> sales)
        {
            
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
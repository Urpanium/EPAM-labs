using System;
using System.Collections.Generic;
using System.Linq;
using T4.DataAccessLayer;
using T4.DataLayer.Models;

namespace T4.BusinessLogicLayer
{
    public class DatabaseManager : IDisposable
    {
        private DatabaseContext _context;


        public DatabaseManager()
        {
            _context = new DatabaseContext("DBConnection");
        }

        public void OnSalesReady(IEnumerable<Sale> sales, Manager manager)
        {
            Manager managerFromDatabase = _context.Managers.SingleOrDefault(m => m.LastName.Equals(manager.LastName));
            DateTime today = DateTime.Today;
            if (managerFromDatabase == null)
            {
                _context.Managers.Add(manager);
                managerFromDatabase = manager;
            }

            managerFromDatabase.LastUpdate = today;
            _context.Sales.AddRange(sales);


            _context.SaveChanges();
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
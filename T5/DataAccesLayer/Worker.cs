using System;
using System.Collections.Generic;
using T5.Models;
using T5.ModelsLayer;

namespace T5.DataAccesLayer
{
    public class Worker : IDisposable
    {
        private readonly DatabaseManager _databaseManager;

        public Worker()
        {
            _databaseManager = new DatabaseManager();
        }

        public void Edit(Sale localSale)
        {
           _databaseManager.Edit(localSale);
        }

        public IEnumerable<Manager> GetAllManagers()
        {
            return _databaseManager.GetAllManagers();
        }
        public IEnumerable<Sale> GetAllSales()
        {
            return _databaseManager.GetAllSales();
        }

        public IEnumerable<Sale> GetAllSales(Filter filter)
        {
            return _databaseManager.GetAllSales(filter);
        }

        public void RemoveById(int id)
        {
            _databaseManager.RemoveById(id);
        }
        
        public Sale GetById(int? id)
        {
            return _databaseManager.GetById(id);
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
                    _databaseManager?.Dispose();
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
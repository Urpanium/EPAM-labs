using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Serilog;
using T4.DataAccessLayer;
using T4.DataLayer.Models;

namespace T4.BusinessLogicLayer
{
    public class DatabaseManager : IDisposable
    {
        private static object _locker = new object();
        private DatabaseContext _context;

        public DatabaseManager()
        {
            _context = new DatabaseContext("DBConnection");
        }

        public void OnSalesReady(IEnumerable<Sale> sales, Manager manager)
        {
            Log.Information("Sales are ready. Pushing...");
            bool locked = false;
            try
            {
                Monitor.Enter(_locker, ref locked);

                //check if manager already exists in database
                Manager managerFromDatabase =
                    _context.Managers.SingleOrDefault(m => m.LastName.Equals(manager.LastName));
                Log.Information($"Manager existence: {managerFromDatabase != null}");
                List<Sale> salesList = sales.ToList();
                if (managerFromDatabase == null)
                {
                    _context.Managers.Add(manager);
                }
                else
                {
                    foreach (var sale in salesList)
                    {
                        sale.Manager = managerFromDatabase;
                    }
                }

                /*for (int i = 0; i < salesList.Count; i++)
                {
                    Sale sale = salesList[i];
                    Client client = _context.Clients.SingleOrDefault(
                        c => c == sale.Client);
                    if (client != null)
                    {
                        sale.Client = client;
                    }
                }*/

                _context.Sales.AddRange(salesList);

                _context.SaveChanges();
                Log.Information($"Successfully pushed file from {manager.LastName}");
            }
            catch (Exception e)
            {
                Log.Error($"Error while transferring data to database: {e}");
            }
            finally
            {
                if (locked)
                {
                    Monitor.Exit(_locker);
                }
            }
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
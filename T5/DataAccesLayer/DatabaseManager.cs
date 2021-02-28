using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using T5.Models;
using T5.ModelsLayer;

namespace T5.DataAccesLayer
{
    public class DatabaseManager : IDisposable
    {
        private static readonly object Locker = new object();
        private readonly ApplicationDbContext _context;

        public DatabaseManager()
        {
            _context = new ApplicationDbContext();
        }


        public void Edit(Sale localSale)
        {
            if (!localSale.IsValid())
            {
                throw new ArgumentException();
            }

            Monitor.Enter(Locker);
            Sale sale = _context.Sales.Find(localSale.Id);
            if (sale != null)
            {
                sale.Client.FirstName = localSale.Client.FirstName;
                sale.Client.LastName = localSale.Client.LastName;
                sale.Manager.LastName = localSale.Manager.LastName;
                sale.Product.Price = localSale.Product.Price;
                sale.Product.Name = localSale.Product.Name;
                sale.DateTime = localSale.DateTime;
                _context.SaveChanges();
            }

            Monitor.Exit(Locker);
        }

        public IEnumerable<Manager> GetAllManagers()
        {
            return _context.Managers
                .AsEnumerable()
                .Select(m => new Manager
                {
                    Id = m.Id,
                    LastName = m.LastName
                });
        }


        public IEnumerable<Sale> GetAllSales()
        {
            return _context.Sales
                .AsEnumerable()
                .OrderBy(s => s.Id)
                .Select(
                    s => new Sale
                    {
                        Id = s.Id,
                        Client = new Client
                        {
                            Id = s.Client.Id,
                            FirstName = s.Client.FirstName,
                            LastName = s.Client.LastName
                        },
                        Manager = new Manager {Id = s.Manager.Id, LastName = s.Manager.LastName},
                        Product = new Product {Id = s.Product.Id, Name = s.Product.Name},
                        DateTime = s.DateTime
                    }
                ).ToList();
        }

        public IEnumerable<Sale> GetAllSales(Filter filter)
        {
            DateTime fromDateTime = DateTime.MinValue;
            if (filter.FromDateString != null)
                fromDateTime = DateTime.ParseExact(filter.FromDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            DateTime toDateTime = DateTime.MaxValue;
            if (filter.ToDateString != null)
                toDateTime = DateTime.ParseExact(filter.ToDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            Monitor.Enter(Locker);
            var sales = _context.Sales.OrderBy(s => s.Id)
                .Where(s => (s.DateTime.Equals(DateTime.MinValue) || DateTime.Compare(s.DateTime, fromDateTime) >= 0)
                            && (s.DateTime.Equals(DateTime.MinValue) || DateTime.Compare(s.DateTime, toDateTime) <= 0)
                            && (filter.ManagerLastName.Equals("All managers") ||
                                s.Manager.LastName.Equals(filter.ManagerLastName)))
                .ToList()
                .Select(s => new Sale
                {
                    Id = s.Id,
                    Client = new Client
                    {
                        Id = s.Client.Id, FirstName = s.Client.FirstName, LastName = s.Client.LastName
                    },
                    Manager = new Manager {Id = s.Manager.Id, LastName = s.Manager.LastName},
                    Product = new Product {Id = s.Product.Id, Name = s.Product.Name},
                    DateTime = s.DateTime
                });
            Monitor.Exit(Locker);
            return sales;
        }

        public void RemoveById(int id)
        {
            Monitor.Enter(Locker);
            Sale sale = _context.Sales.Find(id);
            if (sale != null)
            {
                _context.Clients.Remove(_context.Clients.Find(sale.Client.Id) ?? throw new InvalidOperationException());
                _context.Products.Remove(_context.Products.Find(sale.Product.Id) ??
                                         throw new InvalidOperationException());
                _context.Sales.Remove(sale);
                _context.SaveChanges();
            }

            Monitor.Exit(Locker);
        }

        public Sale GetById(int? id)
        {
            Sale sale = _context.Sales.Find(id);
            if (sale != null)
            {
                return new Sale()
                {
                    Id = sale.Id,
                    Client = new Client
                    {
                        Id = sale.Client.Id, FirstName = sale.Client.FirstName, LastName = sale.Client.LastName
                    },
                    Manager = new Manager {Id = sale.Manager.Id, LastName = sale.Manager.LastName},
                    Product = new Product {Id = sale.Product.Id, Name = sale.Product.Name},
                    DateTime = sale.DateTime
                };
            }

            throw new ArgumentNullException();
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
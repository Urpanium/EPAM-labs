using System.Data.Entity;
using T4.DataLayer.Models;

namespace T4.DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Product> Products { get; set; }

        public DatabaseContext(string connectionString) : base(connectionString)
        {
        }
    }
}
using CargasBrasilDB.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargasBrasilDB.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Driver> Drivers => Set<Driver>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }
}

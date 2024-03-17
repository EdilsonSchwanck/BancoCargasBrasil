using CargasBrasilDB.Domin.Entities;
using CargasBrasilDB.Mapper.Types;
using Microsoft.EntityFrameworkCore;

namespace CargasBrasilDB.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Driver> Driver { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DriverMap());

        }
    }
}

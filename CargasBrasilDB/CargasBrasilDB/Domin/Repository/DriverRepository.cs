using CargasBrasilDB.Data.Context;
using CargasBrasilDB.Domin.Entities;
using CargasBrasilDB.Domin.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CargasBrasilDB.Domin.Repository
{
    public class DriverRepository : IDriverRepository
    {

        private readonly DataContext context;


        public DriverRepository(DataContext _context)
        {
            context = _context;
        }

        public void Delete(Driver entity)
        {
            context.Remove(entity);
            context.SaveChanges();
        }

        public IQueryable<Driver> GetAll()
        {
            return context.Set<Driver>()
               .AsNoTracking()
               .AsQueryable();
        }

        public Driver GetById(string entityId)
        {
            throw new NotImplementedException();
        }

        public void Save(Driver entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        public void Update(Driver entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
    }
}

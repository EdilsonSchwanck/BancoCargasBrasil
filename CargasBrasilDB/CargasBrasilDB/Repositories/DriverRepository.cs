using CargasBrasilDB.Data.Context;
using CargasBrasilDB.Models.Entities;
using CargasBrasilDB.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CargasBrasilDB.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext Context;

        protected Repository(DataContext context) => Context = context;

        public async Task AddAsync(TEntity entity, CancellationToken ct = default)
        {
            await Context.Set<TEntity>().AddAsync(entity, ct);
            await Context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken ct = default)
        {
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync(ct);
        }
    }

    public class DriverRepository : Repository<Driver>, IDriverRepository
    {
        public DriverRepository(DataContext context) : base(context) { }

        public Task<Driver?> GetByCpfAsync(string cpf, CancellationToken ct = default)
            => Context.Drivers.FirstOrDefaultAsync(d => d.Cpf == cpf, ct);

        public Task<bool> ExistsByCpfAsync(string cpf, CancellationToken ct = default)
            => Context.Drivers.AnyAsync(d => d.Cpf == cpf, ct);

        public async Task<IReadOnlyList<Driver>> GetPendingApprovalAsync(CancellationToken ct = default)
            => await Context.Drivers.AsNoTracking().Where(d => !d.CadastroAprovado).ToListAsync(ct);
    }
}

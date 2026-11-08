using CargasBrasilDB.Models.Entities;

namespace CargasBrasilDB.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity, CancellationToken ct = default);
        Task UpdateAsync(TEntity entity, CancellationToken ct = default);
        Task DeleteAsync(TEntity entity, CancellationToken ct = default);
    }

    public interface IDriverRepository : IRepository<Driver>
    {
        Task<Driver?> GetByCpfAsync(string cpf, CancellationToken ct = default);
        Task<bool> ExistsByCpfAsync(string cpf, CancellationToken ct = default);
        Task<IReadOnlyList<Driver>> GetPendingApprovalAsync(CancellationToken ct = default);
    }
}

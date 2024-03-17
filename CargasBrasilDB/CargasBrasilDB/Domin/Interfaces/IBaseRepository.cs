namespace CargasBrasilDB.Domin.Interfaces
{
    public interface IBaseRepository<Entity> where Entity : class
    {
        Entity GetById(string entityId);
        IQueryable<Entity> GetAll();
        void Save(Entity entity);
        void Delete(Entity entity);
        void Update(Entity entity);
    }
}

using MVVMCrud.Data.Model;

namespace MVVMCrud.Data.Repository
{
    public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        TEntity? GetByID(Guid id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TKey id);
        void Delete(TEntity entityToDelete);
        ICollection<TEntity> GetAll();
    }
}

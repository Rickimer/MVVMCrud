using Microsoft.EntityFrameworkCore;
using MVVMCrud.Data.Model;

namespace MVVMCrud.Data.Repository
{
    public abstract class GeneralRepository<TEntity, TContext, TKey> : IRepository<TEntity, TKey>, IDisposable
            where TEntity : class, IEntity<TKey>
            where TContext : IContext<TEntity>
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GeneralRepository(TContext context)
        {
            _context = context;
        } 

        public TEntity? GetByID(Guid id)
        {
            var entity = _context.GetById(id);
            return entity;
        }

        public TEntity Add(TEntity entity)
        {
            _context.Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _context?.Update(entity);
            return entity;
        }

        public void Delete(TKey id)
        {
            var f = new Guid(id.ToString());
            _context.Delete(f);
        }

        public void Delete(TEntity entityToDelete)
        {
            _context.Delete(entityToDelete);
        }

        public ICollection<TEntity> GetAll()
        {
            return _context.GetAll();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

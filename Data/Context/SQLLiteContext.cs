using Microsoft.EntityFrameworkCore;
using MVVMCrud.Data.Model;

namespace MVVMCrud.Data.Context
{
    public class SQLLiteContext<T> : IContext<T> where T : Entity
    {
        private readonly SQLIteDBContext _context;
        private readonly DbSet<T> _dbSet;

        public SQLLiteContext(SQLIteDBContext context) {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public ICollection<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public void Delete(T entityToDelete)
        {
            if (_dbSet.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
            _context.SaveChanges();
        }        

        public T GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}

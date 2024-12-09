using System.Collections;

namespace MVVMCrud.Data
{
    public interface IContext<T> where T : class
    {
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);        
        public void Delete(Guid id);
        public ICollection<T> GetAll();
        public T GetById(Guid id);
    }
}

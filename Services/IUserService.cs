using MVVMCrud.Data.Model;


namespace MVVMCrud.Services
{
    public interface IUserService
    {
        public void Add(User entity);
        public void Update(User entity);
        public void Delete(User entity);
        public ICollection<User> GetAll();
        public User GetById(Guid id);
    }
}

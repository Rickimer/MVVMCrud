using MVVMCrud.Data;
using MVVMCrud.Data.Model;
using MVVMCrud.Data.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCrud.Services
{
    public class UserService : IUserService
    {
        private readonly IContext<User> _context;
        private readonly UserRepository _userRepository;

        public UserService(IContext<User> context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);           
        }

        public void Add(User entity)
        {
            _userRepository.Add(entity);
        }

        public void Delete(User entity)
        {
            _userRepository.Delete(entity);
        }

        public ICollection<User> GetAll()
        {
            var items = _userRepository.GetAll();
            return items;
        }

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            _userRepository.Update(entity);
        }
    }
}

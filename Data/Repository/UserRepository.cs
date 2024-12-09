using MVVMCrud.Data.Model;

namespace MVVMCrud.Data.Repository
{
    public class UserRepository : GeneralRepository<User, IContext<User>, Guid>
    {
        //IContext<User> _context;
        public UserRepository(IContext<User> context) : base(context)
        {
            //_context = context;
        }

 
    }
}

using Microsoft.EntityFrameworkCore;

namespace MVVMCrud.Data.Model
{
    public class SQLIteDBContext : DbContext
    {
        private static SQLIteDBContext _context;

        public static SQLIteDBContext GetContext() { 
            if (_context == null)
                _context = new SQLIteDBContext();

            return _context;
        }

        public DbSet<User> Users { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }
    }
}

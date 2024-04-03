using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Concretes.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }
    }
}

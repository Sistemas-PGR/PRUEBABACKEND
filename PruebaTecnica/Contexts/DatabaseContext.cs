using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<EjemploModelo> EjemploModelo { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
    }
}
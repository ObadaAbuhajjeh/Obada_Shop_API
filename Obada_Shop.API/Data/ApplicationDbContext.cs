using Microsoft.EntityFrameworkCore;
using Obada_Shop.API.Model;

namespace Obada_Shop.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Category> categories { get; set; }
    }
}

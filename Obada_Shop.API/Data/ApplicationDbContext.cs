using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Obada_Shop.API.Model;

namespace Obada_Shop.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        internal object brands;

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }

        internal object Edit(int id)
        {
            throw new NotImplementedException();
        }
    }
}

using CartService.Model;
using Microsoft.EntityFrameworkCore;

namespace CartService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Cart> Cart { get; set; }
    }
}

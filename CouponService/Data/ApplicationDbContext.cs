using CouponService.Models;
using Microsoft.EntityFrameworkCore;

namespace CouponService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Coupon> Coupon { get; set; }
    }
}

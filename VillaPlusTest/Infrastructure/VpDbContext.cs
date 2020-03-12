using Microsoft.EntityFrameworkCore;
using VillaPlusTest.Infrastructure.Entities;

namespace VillaPlusTest.Infrastructure
{
    public class VpDbContext : DbContext
    {
        public VpDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
    }
}

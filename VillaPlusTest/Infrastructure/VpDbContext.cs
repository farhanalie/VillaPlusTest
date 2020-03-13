using Microsoft.EntityFrameworkCore;
using VillaPlus.API.Infrastructure.Entities;

namespace VillaPlus.API.Infrastructure
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

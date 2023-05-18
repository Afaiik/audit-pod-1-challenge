using Core.Entities;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class CatalogDbContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) => builder.ApplyConfiguration(new ProductConfiguration());
    }
}

using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces.Contracts;

namespace Persistence.SqlServer;

public class ShopDbContext(DbContextOptions<ShopDbContext> options) : DbContext(options)
{
    public virtual DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}

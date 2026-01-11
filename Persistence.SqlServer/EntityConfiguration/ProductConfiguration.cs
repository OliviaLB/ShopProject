using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Interfaces.Contracts;

namespace Persistence.SqlServer.EntityConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id).HasColumnOrder(0);
        builder.Property(p => p.Name).HasColumnOrder(1);
        builder.Property(p => p.Description).HasColumnOrder(2);
        builder.Property(p => p.Price).HasColumnOrder(3);
        builder.Property(p => p.PictureUri).HasColumnOrder(4);
        builder.Property(p => p.Type).HasColumnOrder(5);
        builder.Property(p => p.Brand).HasColumnOrder(6);
        builder.Property(p => p.QuantityInStock).HasColumnOrder(7);
        builder.Property(p => p.ChangeTimestamp).HasColumnOrder(8);
    }
}

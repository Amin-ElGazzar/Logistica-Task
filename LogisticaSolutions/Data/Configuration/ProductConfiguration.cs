using LogisticaSolutions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LogisticaSolutions.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasIndex(x => x.PartSKU).IsUnique();

            builder.Property(x => x.ListPrice).HasColumnType("decimal(18,2)");

            builder.Property(x => x.DiscountPrice).HasColumnType("decimal(18,2)");

            builder.Property(x => x.MinDiscount).HasColumnType("decimal(18,2)");
        }
    }
}

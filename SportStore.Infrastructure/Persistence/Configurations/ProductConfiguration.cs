using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportStore.Domain;

namespace SportStore.Infrastructure.Persistence.Configurations
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.Description).HasMaxLength(200);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Price).HasColumnType("money").HasDefaultValueSql("((0))");
            builder.HasOne(e => e.Category).WithMany(c => c.Products);
            
        }
    }
}

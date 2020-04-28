using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportStore.Domain;

namespace SportStore.Infrastructure.Persistence.Configurations
{
    class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(e => e.Name).IsRequired();
            builder.OwnsOne(e => e.CustomerAdress);
            builder.HasMany(e => e.OrderLines).WithOne(ol => ol.Order);
            
        }
    }
}

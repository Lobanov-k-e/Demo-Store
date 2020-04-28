using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportStore.Domain;

namespace SportStore.Infrastructure.Persistence.Configurations
{
    class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.Property(e => e.Quantity).IsRequired();
            builder.HasOne(e => e.Product).WithMany(p=>p.OrderLines);
        }
    }
}

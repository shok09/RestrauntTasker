using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(p => p.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(p => p.OrderChef)
                .WithOne(u => u.Order)
                .HasForeignKey<OrderUser>(u => u.OrderId);

            builder.HasMany(p => p.Users);
        }
    }
}

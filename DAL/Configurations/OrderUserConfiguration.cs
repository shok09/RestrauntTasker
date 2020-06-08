using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Configurations
{
    internal class OrderUserConfiguration : IEntityTypeConfiguration<OrderUser>
    {
        public void Configure(EntityTypeBuilder<OrderUser> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(OrderUser.RefreshTokens));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(u => u.Name)
                .HasMaxLength(80)
                .IsRequired();

            builder.HasIndex(u => u.Name);
        }
    }
}

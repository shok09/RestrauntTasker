using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    internal class OrderTaskConfiguration : IEntityTypeConfiguration<OrderTask>
    {
        public void Configure(EntityTypeBuilder<OrderTask> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}

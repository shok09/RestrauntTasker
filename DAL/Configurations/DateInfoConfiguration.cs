using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Configurations
{
    internal class DateInfoConfiguration : IEntityTypeConfiguration<DateInfo>
    {
        public void Configure(EntityTypeBuilder<DateInfo> builder)
        {
            builder.Property(dt => dt.BeginDate)
                .IsRequired();

            builder.Property(dt => dt.Deadline)
                .IsRequired();
        }
    }
}

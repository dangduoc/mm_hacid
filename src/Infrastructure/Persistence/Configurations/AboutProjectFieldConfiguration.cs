using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class AboutProjectFieldConfiguration : IEntityTypeConfiguration<AboutProjectField>
    {
        public void Configure(EntityTypeBuilder<AboutProjectField> builder)
        {
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).HasMaxLength(200).IsUnicode(true).IsRequired();
            builder.Property(c => c.NameEn).HasMaxLength(200).IsUnicode(false).IsRequired();
            builder.Property(c => c.Summary).HasMaxLength(1000).IsRequired();
            builder.Property(c => c.SummaryEn).HasMaxLength(1000).IsRequired();
            builder.Property(c => c.OrderIndex).HasDefaultValue(1).IsRequired();
        }
    }
}

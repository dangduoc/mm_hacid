using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class CompanyHistoryConfiguration : IEntityTypeConfiguration<CompanyHistory>
    {
        public void Configure(EntityTypeBuilder<CompanyHistory> builder)
        {
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.Property(c => c.Title).IsUnicode(true).HasMaxLength(500).IsRequired();
            builder.Property(c => c.TitleEn).IsUnicode(true).HasMaxLength(500).IsRequired();
            builder.Property(c => c.Year).IsRequired();
        }
    }
}

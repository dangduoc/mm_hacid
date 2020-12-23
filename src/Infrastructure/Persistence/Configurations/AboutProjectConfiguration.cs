using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class AboutProjectConfiguration : IEntityTypeConfiguration<AboutProject>
    {
        public void Configure(EntityTypeBuilder<AboutProject> builder)
        {
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.Property(c => c.TabTitle).IsUnicode(true).HasMaxLength(100).IsRequired();
            builder.Property(c => c.TabTitleEn).IsUnicode(false).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Summary).IsUnicode(true).HasMaxLength(2000).IsRequired();
            builder.Property(c => c.Title).IsUnicode(true).HasMaxLength(500).IsRequired();
            builder.Property(c => c.SummaryEn).IsUnicode(false).HasMaxLength(2000).IsRequired();
            builder.Property(c => c.TitleEn).IsUnicode(false).HasMaxLength(500).IsRequired();
            builder.Property(c => c.Image).IsUnicode(false).HasMaxLength(200).IsRequired();
            builder.Property(c => c.Index).IsRequired();
        }
    }
}

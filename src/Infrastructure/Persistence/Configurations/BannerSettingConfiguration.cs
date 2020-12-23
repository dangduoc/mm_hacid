using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class BannerSettingConfiguration : IEntityTypeConfiguration<BannerSetting>
    {
        public void Configure(EntityTypeBuilder<BannerSetting> builder)
        {
            builder.HasKey(c => c.BannerId);
            builder.Property(c => c.BannerId).ValueGeneratedNever().IsRequired();
            builder.Property(c => c.Banner).IsUnicode(false).HasMaxLength(200).IsRequired();
            builder.Property(c => c.Name).IsUnicode(true).HasMaxLength(200).IsRequired();
            builder.Property(c => c.Theme).IsRequired();
        }
    }
}

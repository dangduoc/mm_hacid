using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class SiteSettingConfiguration : IEntityTypeConfiguration<SiteSetting>
    {
        public void Configure(EntityTypeBuilder<SiteSetting> builder)
        {
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.Property(c => c.Address).HasMaxLength(500);
            builder.Property(c => c.AddressEn).IsUnicode(false).HasMaxLength(500);
            builder.Property(c => c.PhoneNumber).HasMaxLength(500);
            builder.Property(c => c.Fax).HasMaxLength(500);
            builder.Property(c => c.Email).HasMaxLength(500);
            builder.Property(c => c.Facebook).HasMaxLength(500);
            builder.Property(c => c.Linkedin).HasMaxLength(500);
            builder.Property(c => c.Map).HasMaxLength(500);
        }
    }
}

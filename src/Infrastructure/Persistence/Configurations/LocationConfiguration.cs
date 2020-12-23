using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.Property(c => c.Name).IsUnicode(true).HasMaxLength(250).IsRequired();
            builder.Property(c => c.NameEn).IsUnicode(false).HasMaxLength(250);

        }
    }
}

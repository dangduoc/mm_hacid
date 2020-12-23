using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class HomeSlideConfiguration : IEntityTypeConfiguration<HomeSlide>
    {
        public void Configure(EntityTypeBuilder<HomeSlide> builder)
        {
            // builder.HasKey(c => c.HomeId);
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.Property(c => c.Title).IsRequired().HasMaxLength(500);
            builder.Property(c => c.TitleEn).HasMaxLength(500);
            builder.Property(c => c.Theme).IsRequired();
            builder.Property(c => c.Image).HasMaxLength(500).IsRequired();
            builder.Property(c => c.Index).HasDefaultValue(1).IsRequired();
            
        }
    }
}

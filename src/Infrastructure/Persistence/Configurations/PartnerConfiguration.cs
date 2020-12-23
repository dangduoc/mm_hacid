using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).HasMaxLength(200).IsUnicode(true).IsRequired();
            builder.Property(c => c.NameEn).HasMaxLength(200).IsUnicode(false).IsRequired();
            builder.Property(c => c.Thumbnail).HasMaxLength(200).IsUnicode(false).IsRequired();
            builder.Property(c => c.Link).HasMaxLength(200).IsUnicode(false);
            builder.Property(c => c.Index).IsRequired();
            builder.HasOne(c => c.Category).WithMany(c => c.Partners).HasForeignKey(c => c.CategoryId).IsRequired();
        }
    }
}

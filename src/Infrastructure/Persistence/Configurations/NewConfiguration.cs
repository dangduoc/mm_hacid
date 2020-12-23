using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class NewConfiguration : IEntityTypeConfiguration<New>
    {
        public void Configure(EntityTypeBuilder<New> builder)
        {
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Title).HasMaxLength(250).IsRequired().IsUnicode(true);

            builder.Property(c => c.TitleEn).HasMaxLength(250).IsUnicode(false);

            builder.Property(c => c.HtmlContent).IsUnicode(true).IsRequired();

            builder.Property(c => c.HtmlContentEn).IsUnicode(false);

            builder.Property(c => c.Banners).IsUnicode(false).IsRequired();

            builder.Property(c => c.Status).IsRequired().HasDefaultValue(1);

            builder.Property(c => c.Thumbnail).IsUnicode(false).IsRequired().HasMaxLength(100);
        
            builder.Property(c => c.CategoryId).IsRequired();

            builder.Property(c => c.TotalViews).HasDefaultValue(0).IsRequired();

            builder.HasOne(c => c.Category).WithMany(c => c.News).HasForeignKey(c => c.CategoryId).IsRequired();

            builder.Property(c => c.Index).IsRequired();

           
        }
    }
}

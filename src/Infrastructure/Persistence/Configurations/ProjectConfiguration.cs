using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Title).HasMaxLength(250).IsRequired().IsUnicode(true);

            builder.Property(c => c.TitleEn).HasMaxLength(250).IsUnicode(false);

            //builder.Property(c => c.Summary).HasMaxLength(1024).IsRequired().IsUnicode(true);

            //builder.Property(c => c.SummaryEn).HasMaxLength(1024).IsUnicode(false);
           
            builder.Property(c => c.HtmlContent).IsUnicode(true).IsRequired();

            builder.Property(c => c.HtmlContentEn).IsUnicode(false);

            builder.Property(c => c.Banners).IsUnicode(false).IsRequired();
            builder.Property(c => c.Theme).IsRequired();
            builder.Property(c => c.Thumbnail).IsUnicode(false).IsRequired().HasMaxLength(100);
            builder.Property(c => c.LocationId).IsRequired();
     
            builder.Property(c => c.Investor).IsUnicode(true).HasMaxLength(200).IsRequired();
            builder.Property(c => c.InvestorEn).IsUnicode(false).HasMaxLength(200).IsRequired(false);
            builder.Property(c => c.Year).IsRequired();
            builder.Property(c => c.Status).IsRequired().HasDefaultValue(1);
           
            builder.HasOne(c => c.Location).WithMany(c => c.Projects).HasForeignKey(c => c.LocationId).IsRequired();

            builder.Property(c => c.Index).IsRequired();




        }
    }
}

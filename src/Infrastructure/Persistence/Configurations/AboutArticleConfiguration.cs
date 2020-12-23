using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class AboutArticleConfiguration : IEntityTypeConfiguration<AboutArticle>
    {
        public void Configure(EntityTypeBuilder<AboutArticle> builder)
        {
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.Property(c => c.Type).IsRequired();
            builder.Property(c => c.HtmlContent).IsRequired();
            
        }
    }
}

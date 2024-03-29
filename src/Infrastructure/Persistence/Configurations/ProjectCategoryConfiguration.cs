﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class ProjectCategoryConfiguration : IEntityTypeConfiguration<ProjectCategory>
    {
        public void Configure(EntityTypeBuilder<ProjectCategory> builder)
        {
            builder.Property(c => c.Id).ValueGeneratedNever().IsRequired();

            builder.Property(c => c.Name).HasMaxLength(200).IsUnicode(true).IsRequired();
            builder.Property(c => c.NameEn).HasMaxLength(200).IsUnicode(false).IsRequired();
        }
    }
}

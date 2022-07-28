using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class ProjectCategoryRelationConfiguration : IEntityTypeConfiguration<ProjectCategoryRelation>
    {
        public void Configure(EntityTypeBuilder<ProjectCategoryRelation> builder)
        {
            builder.HasKey(sc => new { sc.CategoryId, sc.ProjectId });

            builder
                .HasOne<Project>(x => x.Project)
                .WithMany(s => s.ProjectCategoryRelations)
                .HasForeignKey(sc => sc.ProjectId);


            builder
                .HasOne<ProjectCategory>(sc => sc.ProjectCategory)
                .WithMany(s => s.ProjectCategoryRelations)
                .HasForeignKey(sc => sc.CategoryId);
        }
    }
}

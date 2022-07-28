using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class ProjectFieldRelationConfiguration : IEntityTypeConfiguration<ProjectFieldRelation>
    {
        public void Configure(EntityTypeBuilder<ProjectFieldRelation> builder)
        {
            builder.HasKey(sc => new { sc.ProjectFieldId, sc.ProjectId });

            builder
                .HasOne<Project>(x => x.Project)
                .WithMany(s => s.ProjectFieldRelations)
                .HasForeignKey(sc => sc.ProjectId);


            builder
                .HasOne<ProjectField>(sc => sc.ProjectField)
                .WithMany(s => s.ProjectFieldRelations)
                .HasForeignKey(sc => sc.ProjectFieldId);
        }
    }
}

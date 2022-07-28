using Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
             ICurrentUserService currentUserService,
            IDateTime dateTime
             ) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<Location> Location { get; set; }
        public DbSet<NewCategory> NewCategory { get; set; }
        public DbSet<ProjectCategory> ProjectCategory { get; set; }
        public DbSet<PartnerCategory> PartnerCategory { get; set; }
        public DbSet<ProjectField> ProjectField { get; set; }
        public DbSet<New> New { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<AboutArticle> AboutArticle { get; set; }
        public DbSet<AboutProjectField> AboutProjectField { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<AboutProject> AboutProject { get; set; }
        public DbSet<SiteSetting> SiteSetting { get; set; }
        public DbSet<HomeSlide> HomeSlide { get; set; }
        public DbSet<CompanyHistory> CompanyHistory { get; set; }
        public DbSet<BannerSetting> BannerSetting { get; set; }
        public DbSet<ProjectCategoryRelation> ProjectCategoryRelation { get; set; }
        public DbSet<ProjectFieldRelation> ProjectFieldRelation { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedByUserId = _currentUserService.UserId;
                        entry.Entity.CreatedOnDate = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedByUserId = _currentUserService.UserId;
                        entry.Entity.LastModifiedOnDate = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}

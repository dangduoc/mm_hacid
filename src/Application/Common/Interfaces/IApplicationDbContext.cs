
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {

        public DbSet<Location> Location { get; set; }
        public DbSet<NewCategory> NewCategory { get; set; }
        public DbSet<ProjectCategory> ProjectCategory { get; set; }
        public DbSet<PartnerCategory> PartnerCategory { get; set; }
        public DbSet<ProjectField> ProjectField { get; set; }
        public DbSet<New> New { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<AboutArticle> AboutArticle { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<AboutProject> AboutProject { get; set; }
        public DbSet<SiteSetting> SiteSetting { get; set; }
        public DbSet<HomeSlide> HomeSlide { get; set; }
        public DbSet<AboutProjectField> AboutProjectField { get; set; }
        public DbSet<CompanyHistory> CompanyHistory { get; set; }
       public DbSet<BannerSetting> BannerSetting { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken=new CancellationToken());
    }
}

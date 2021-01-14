using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectWebRazor.Services
{
    public class CompanySettings
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentRequestLanguageService _languageService;

        public CompanySettings(IApplicationDbContext context,ICurrentRequestLanguageService languageService)
        {
            _context = context;
            _languageService = languageService;
        }
        public async Task<SiteSetting> GetSiteSetting()
        {
            var entity= await _context.SiteSetting.FirstOrDefaultAsync();
            entity.Address = _languageService.IsDefault ? entity.Address : entity.AddressEn;
       

            entity.CompanyName = _languageService.IsDefault ? entity.CompanyName : entity.CompanyNameEn;
            return entity;
        }
        public async Task<string> MapImage()
        {
            var entity = await _context.BannerSetting.FindAsync(9);
            if (entity == null) return "/assets/images/footer.jpg";
            return entity.Banner;
       }
        public async Task<List<CategoryItem>> GetProjectCategories()
        {
            return (await _context.ProjectField.ToListAsync())
                .Select(c => new CategoryItem
                {
                    Id=c.Id,
                    Name=_languageService.IsDefault?c.Name:c.NameEn
                }).OrderBy(c => c.Id).ToList();
        }
        public async Task<List<CategoryItem>> GetNewCategories()
        {
            return (await _context.NewCategory.Where(c=>c.Id>1).ToListAsync())
                .Select(c => new CategoryItem
                {
                    Id = c.Id,
                    Name = _languageService.IsDefault ? c.Name : c.NameEn
                }).OrderBy(c => c.Id).ToList();
        }
        public async Task<BannerSetting> GetBanner(BannerType Id)
        {
            var banner= await _context.BannerSetting.FindAsync((int)Id);
            return banner;
        }
        public async Task<List<BannerSetting>> GetBannerHome()
        {
            return await _context.BannerSetting.Where(c => c.BannerId <= (int)BannerType.Home4).ToListAsync();
        }
    }

    public class CategoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public enum BannerType
    {
        Home1=1,
        Home2=2,
        Home3=3,
        Home4=4,
        Project=5,
        New=6,
        About=7,
        Contact=8
    }
}

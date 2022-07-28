using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectWebRazor.Pages.ProjectPage
{
    public class DetailModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentRequestLanguageService _languageService;

        public DetailModel(IApplicationDbContext context, ICurrentRequestLanguageService languageService)
        {
            _context = context;
            _languageService = languageService;
        }
        public ProjectDetail Detail { get; set; }
        public async Task<IActionResult> OnGetAsync(int Id)
        {
           var entity=await _context.Project
                .Include(c => c.ProjectCategoryRelations).ThenInclude(x=>x.ProjectCategory)
                .Include(c => c.Location)
                .Include(c => c.ProjectFieldRelations).ThenInclude(x=>x.ProjectField)
                .SingleOrDefaultAsync(c => c.Id == Id);
            if (entity == null) return NotFound();
            if (entity.Status == -1) return NotFound();
            if ((bool)entity.IsEnglishIncluded==false && !_languageService.IsDefault) return NotFound();
            Detail = new ProjectDetail
            {
                Id = entity.Id,
                Banners = entity.Banners,
                Categories =  entity.ProjectCategoryRelations.Select(x=>new CategoryItem(_languageService.IsDefault,x.ProjectCategory)).ToList(),
                Location = _languageService.IsDefault ? entity.Location.Name : entity.Location.NameEn,
                Fields = entity.ProjectFieldRelations.Select(x => new CategoryItem(_languageService.IsDefault, x.ProjectField)).ToList(),
                HtmlContent = _languageService.IsDefault ? entity.HtmlContent : entity.HtmlContentEn,
                Investor = _languageService.IsDefault ? entity.Investor : entity.InvestorEn,
                Title = _languageService.IsDefault ? entity.Title : entity.TitleEn,
                Year = entity.Year,
                Theme=entity.Theme
            };
            return Page();
        }
    }
    public class ProjectDetail
    {
  
        public int Id { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }
        public string Banners { get; set; }
        public string Location { get; set; }
        public List<CategoryItem> Fields { get; set; }
        public List<CategoryItem> Categories { get; set; }
        public string Investor { get; set; }
        public int Year { get; set; }
        public int Theme { get; set; }
    }
    public class CategoryItem
    {
        public CategoryItem(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
        public CategoryItem(bool isDefaultLang,ProjectCategory projectCategory)
        {
            this.Id = projectCategory.Id;
            this.Name = isDefaultLang ? projectCategory.Name : projectCategory.NameEn;
        }
        public CategoryItem(bool isDefaultLang, ProjectField projectField)
        {
            this.Id = projectField.Id;
            this.Name = isDefaultLang ? projectField.Name : projectField.NameEn;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

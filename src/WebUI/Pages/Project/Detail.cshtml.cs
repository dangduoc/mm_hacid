using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
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
                .Include(c => c.Category)
                .Include(c => c.Location)
                .Include(c => c.ProjectField)
                .SingleOrDefaultAsync(c => c.Id == Id);
            if (entity == null) return NotFound();
            if (entity.Status == -1) return NotFound();
            if ((bool)entity.IsEnglishIncluded && !_languageService.IsDefault) return NotFound();
            Detail = new ProjectDetail
            {
                Id = entity.Id,
                Banners = entity.Banners,
                Category = _languageService.IsDefault ? entity.Category.Name : entity.Category.NameEn,
                Location = _languageService.IsDefault ? entity.Location.Name : entity.Location.NameEn,
                Field = _languageService.IsDefault ? entity.ProjectField.Name : entity.ProjectField.NameEn,
                HtmlContent = _languageService.IsDefault ? entity.HtmlContent : entity.HtmlContentEn,
                Investor = entity.Investor,
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
        public string Field { get; set; }
        public string Category { get; set; }
        public string Investor { get; set; }
        public int Year { get; set; }
        public int Theme { get; set; }
    }
}

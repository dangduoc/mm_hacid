using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectWebRazor.Pages.About
{
    public class SpecialistModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentRequestLanguageService _languageService;
        public string Article { get; set; }
        public List<AboutProjectModel> Projects { get; set; }
        public SpecialistModel(IApplicationDbContext context, ICurrentRequestLanguageService languageService)
        {
            _context = context;
            _languageService = languageService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var entity = await _context.AboutArticle.SingleOrDefaultAsync(c => c.Type == (int)AboutArticleTypeEnum.Specialist);
            if (entity == null) return NotFound();
            Article = _languageService.IsDefault ? entity.HtmlContent : entity.HtmlContentEn;

            Projects = (await _context.AboutProject.OrderByDescending(c => c.Index).ToListAsync())
                .Select(c => new AboutProjectModel
                {
                    Id = c.Id,
                    Summary = _languageService.IsDefault ? c.Summary : c.SummaryEn,
                    Image = c.Image,
                    TabTitle = _languageService.IsDefault ? c.TabTitle : c.TabTitleEn,
                    Title = _languageService.IsDefault ? c.Title : c.TitleEn
                }).ToList();
            
            return Page();
        }
    }
    public class AboutProjectModel
    {
        public int Id { get; set; }
        public string TabTitle { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
    }
}

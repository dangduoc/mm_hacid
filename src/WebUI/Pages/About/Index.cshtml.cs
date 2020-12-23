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
    public class IndexModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentRequestLanguageService _languageService;
        public AboutArticleModel DetailModel { get; set; }
        public List<AboutProjectFieldModel> Fields { get; set; }
        public List<HistoryItem> Histories { get; set; }
        public string CompanyImage { get; set; }
        public IndexModel(IApplicationDbContext context, ICurrentRequestLanguageService languageService)
        {
            _context = context;
            _languageService = languageService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var setting = await _context.SiteSetting.FirstOrDefaultAsync();
            if(string.IsNullOrEmpty(setting.CompanyImage)) CompanyImage = "/assets/images/about/headquarter.jpg";
            else CompanyImage = "/upload/images/"+setting.CompanyImage;
            var entity=await _context.AboutArticle.SingleOrDefaultAsync(c => c.Type == (int)AboutArticleTypeEnum.General);
            if (entity == null) return NotFound();
            DetailModel = new AboutArticleModel
            {
                HtmlContent = _languageService.IsDefault ? entity.HtmlContent : entity.HtmlContentEn
            };

            Fields = (await _context.AboutProjectField.ToListAsync()).Select(c => new AboutProjectFieldModel
            {
                Id = c.Id,
                Name = _languageService.IsDefault ? c.Name : c.NameEn,
                Summary = _languageService.IsDefault ? c.Summary : c.SummaryEn,
                OrderIndex = c.OrderIndex,
                Banner=c.Banner
            }).OrderBy(c => c.OrderIndex).ToList();

            Histories = (await _context.CompanyHistory.OrderBy(c => c.Year).ToListAsync())
                .Select(c => new HistoryItem
                {
                    Year = c.Year,
                    Title = _languageService.IsDefault ? c.Title : c.TitleEn,
                }).ToList();
            return Page();
        }
    }
    public class HistoryItem
    {
        public int Year { get; set; }
        public string Title { get; set; }
    }
    public class AboutArticleModel
    {
        public string HtmlContent { get; set; }
    }
    public class AboutProjectFieldModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public int OrderIndex { get; set; }
        public string Banner { get; set; }
    }
}

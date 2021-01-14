using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectWebRazor.Pages.New
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
        public NewDetail Detail { get; set; }
        public List<NewRelatedModel> RelatedNews { get; set; }
        public async Task<IActionResult> OnGetAsync(int Id)
        {
            var entity = await _context.New
                 .Include(c => c.Category)
                 .SingleOrDefaultAsync(c => c.Id == Id);
            if (entity == null) return NotFound();
            if (entity.Status == -1) return NotFound();
            if ((bool)entity.IsEnglishIncluded==false && !_languageService.IsDefault) return NotFound();
            Detail = new NewDetail
            {
                Id = entity.Id,
                Banners = entity.Banners,
                Category = _languageService.IsDefault ? entity.Category.Name : entity.Category.NameEn,
                PublishedOnDate = entity.CreatedOnDate.ToString("dd.MM.yyyy"),
                HtmlContent = _languageService.IsDefault ? entity.HtmlContent : entity.HtmlContentEn,
                TotalViews = 123,
                Title = _languageService.IsDefault ? entity.Title : entity.TitleEn,

            };
            await LoadRelatedNews(Detail.Id);
            return Page();
        }
        private async Task LoadRelatedNews(int Id)
        {
            RelatedNews = (await _context.New.Where(c => c.Status == 1 && c.Id!=Id).Include(c => c.Category).OrderBy(c => Guid.NewGuid()).Take(10).ToListAsync())
                   .Select(c => new NewRelatedModel
                   {
                       Id = c.Id,
                       Category = _languageService.IsDefault ? c.Category.Name : c.Category.NameEn,
                       Thumbnail = c.Thumbnail,
                       Title = _languageService.IsDefault ? c.Title : c.TitleEn,
                       PublishedDate = c.CreatedOnDate.ToString("dd.MM.yyyy"),
                       TotalViews = 123
                   }).ToList();
            
        }
    }
   
    public class NewDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }
        public string Banners { get; set; }
        public string Category { get; set; }
        public int TotalViews { get; set; }
        public string PublishedOnDate { get; set; }
    }
    public class NewRelatedModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public int TotalViews { get; set; }
        public string PublishedDate { get; set; }
        public string Thumbnail { get; set; }
    }
}

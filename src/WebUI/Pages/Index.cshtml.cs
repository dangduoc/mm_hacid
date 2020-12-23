using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using BaseProjectWebRazor.Areas.Admin.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BaseProjectWebRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentRequestLanguageService _currentRequestLanguage;

        public IndexModel(
            IApplicationDbContext context,
            ICurrentRequestLanguageService currentRequestLanguage
            )
        {
            _context = context;
            _currentRequestLanguage = currentRequestLanguage;
        }
        public List<HomeSlideItem> Slides { get; set; }
        public string AboutArticle { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var list=await _context.HomeSlide.Where(c=>c.Status==1).ToListAsync();
            Slides = list.Select(c => new HomeSlideItem
            {
                Id = c.Id,
                Image = c.Image,
                Index = c.Index,
                Theme=c.Theme,
                Link=c.Link,
                ProjectTitle=_currentRequestLanguage.IsDefault?c.ProjectTitle : c.ProjectTitleEn,
                Location = _currentRequestLanguage.IsDefault ? c.Location : c.LocationEn,
                Title = _currentRequestLanguage.IsDefault ? c.Title:c.TitleEn
            }).OrderByDescending(c => c.Index).ToList();

            var article = await _context.AboutArticle.Where(c => c.Type == (int)AboutArticleTypeEnum.Home).FirstOrDefaultAsync();
            if (article != null) AboutArticle = _currentRequestLanguage.IsDefault ? article.HtmlContent : article.HtmlContentEn;
            return Page();
        }
      
        ///

        public IActionResult OnPostSetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
    public class HomeSlideItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ProjectTitle { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public int Index { get; set; }
        public int Theme { get; set; }
        public string Description { get; set; }

    }
}

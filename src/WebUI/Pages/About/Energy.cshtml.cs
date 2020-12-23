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
    public class energyModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentRequestLanguageService _languageService;
        public string Article { get; set; }
        public energyModel(IApplicationDbContext context, ICurrentRequestLanguageService languageService)
        {
            _context = context;
            _languageService = languageService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var entity = await _context.AboutArticle.SingleOrDefaultAsync(c => c.Type == (int)AboutArticleTypeEnum.Energy);
            if (entity == null) return NotFound();
            Article = _languageService.IsDefault ? entity.HtmlContent : entity.HtmlContentEn;
            return Page();
        }
    }
}

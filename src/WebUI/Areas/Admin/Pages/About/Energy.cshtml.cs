using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using BaseProjectWebRazor.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectWebRazor.Areas.Admin.Pages.About
{
    public class EnergyModel : PageModel
    {
        private readonly IApplicationDbContext _context;

        public EnergyModel(IApplicationDbContext context
            )
        {
            _context = context;
        }
        [BindProperty]
        public AboutArticleDto MainModel { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var entity = await _context.AboutArticle.Where(c => c.Type == (int)Domain.Entities.AboutArticleTypeEnum.Energy).FirstOrDefaultAsync();
            if (entity == null) return NotFound();
            MainModel = new AboutArticleDto
            {
                Id = entity.Id,
                HtmlContent = entity.HtmlContent,
                HtmlContentEn = entity.HtmlContentEn,
                Type = entity.Type
            };
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _context.AboutArticle.Update(new Domain.Entities.AboutArticle
                {
                    Id = MainModel.Id,
                    HtmlContent = MainModel.HtmlContent,
                    HtmlContentEn = MainModel.HtmlContentEn,
                    Type = MainModel.Type
                });

                if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
                {
                    ViewData["success"] = "Lưu thông tin thành công";
                    return Page();
                }
                ViewData["error"] = "Lưu thông tin không thành công";
                return Page();

            }
            catch (Exception ex)
            {
                ViewData["error"] = "Đã xảy ra lỗi: " + ex.ToString();
                return Page();
            }
        }
    }
}

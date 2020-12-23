using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using domain = Domain.Entities;
namespace BaseProjectWebRazor.Areas.Admin.Pages.About.CompanyHistory
{
    public class IndexModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        public IndexModel(IApplicationDbContext context)
        {
            _context = context;
        }
        public List<domain.CompanyHistory> Items { get; set; }
        public async Task OnGetAsync()
        {
            Items = (await _context.CompanyHistory.OrderBy(c => c.Year).ToListAsync());
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                domain.CompanyHistory entity = new domain.CompanyHistory
                {
                    Title = Request.Form["title"],
                    TitleEn = Request.Form["title_en"],
                    Year = Convert.ToInt32(Request.Form["year"]),
                    Id=1
                };
                var last = await _context.CompanyHistory.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
                if (last != null) entity.Id = last.Id + 1;


                _context.CompanyHistory.Add(entity);

                if (await _context.SaveChangesAsync() >= 1)
                {
                    return new JsonResult(new
                    {
                        status = true
                    });

                }
                return new JsonResult(new
                {
                    status = false,
                    message="Lưu dữ liệu không thành công"
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = false,
                    message= "Đã xảy ra lỗi: " + ex.ToString()
                });
            }
        }
        public async Task<IActionResult> OnDeleteAsync(int Id)
        {
            var entity = await _context.CompanyHistory.FindAsync(Id);
            if (entity == null) return NotFound();
            _context.CompanyHistory.Remove(entity);

            if (await _context.SaveChangesAsync() >= 1)
            {
                return new OkResult();
            }
            return BadRequest();
        }
    }
}

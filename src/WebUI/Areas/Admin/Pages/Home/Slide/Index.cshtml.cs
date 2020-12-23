using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using BaseProjectWebRazor.Areas.Admin.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectWebRazor.Areas.Admin.Pages.Home.Slide
{
    public class IndexModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        public IndexModel(
            IApplicationDbContext context
            )
        {
            _context = context;
        }
        public List<HomeSlideItemResponse> Slides { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var list = await _context.HomeSlide.ToListAsync();
            Slides = list.Select(c => new HomeSlideItemResponse
            {
                Id = c.Id,
                Image = c.Image,
                Index = c.Index,
                Theme = c.Theme,
                Title = c.Title,
                Status=c.Status
            }).OrderByDescending(c => c.Index).ToList();
            return Page();
        }
        public async Task<IActionResult> OnDeleteAsync(int Id)
        {
            var entity = await _context.HomeSlide.FindAsync(Id);
            if (entity == null) return NotFound();
            _context.HomeSlide.Remove(entity);

            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new OkResult();
            }
            return BadRequest();
        }
        public async Task<IActionResult> OnPostHideAsync(int Id)
        {
            var entity = await _context.HomeSlide.FindAsync(Id);
            if (entity == null) return new BadRequestResult();
            if (entity.Status == -1) return new BadRequestResult();
            if (entity.Status == 1) entity.Status = 0;
            else entity.Status = 1;

            _context.HomeSlide.Update(entity);
            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new OkResult();
            }
            return BadRequest();
        }
        public async Task<IActionResult> OnPostChangeIndex(int Id, bool moveUp = true)
        {
            var entity = await _context.HomeSlide.FindAsync(Id);
            if (entity == null) return new JsonResult(new { status = false, message = "Đổi vị trí không thành công" });

            HomeSlide swapNew = null;
            //up
            if (moveUp)
            {
                swapNew = await _context.HomeSlide.Where(c => c.Index > entity.Index).OrderBy(c => c.Index).FirstOrDefaultAsync();
            }
            else
            {
                swapNew = await _context.HomeSlide.Where(c => c.Index < entity.Index).OrderByDescending(c => c.Index).FirstOrDefaultAsync();
            }


            if (swapNew == null) return new JsonResult(new
            {
                status = false,
                message = "Đổi vị trí không thành công"
            });

            int currentIndex = entity.Index;
            entity.Index = swapNew.Index;
            swapNew.Index = currentIndex;
            _context.HomeSlide.Update(entity);
            _context.HomeSlide.Update(swapNew);
            if (await _context.SaveChangesAsync() >= 1) return new JsonResult(new { status = true });
            else return new JsonResult(new { status = false, message = "Đổi vị trí không thành công" });
        }
    }
}

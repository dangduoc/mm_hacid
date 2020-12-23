using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Wrappers;
using BaseProjectWebRazor.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using domain = Domain.Entities;
namespace BaseProjectWebRazor.Areas.Admin.Pages.About.Partner
{
    public class IndexModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly ILocalUploadService _uploadService;

        public IndexModel(IApplicationDbContext context,ILocalUploadService uploadService)
        {
            _context = context;
            _uploadService = uploadService;
        }
        public SelectList Categories { get; set; }
        [BindProperty]
        public PartnerDto MainModel { get; set; }
        public List<PartnerListItemResponse> Partners { get; set; } 
        public async Task<IActionResult> OnGetAsync()
        {
            Categories = new SelectList(await _context.PartnerCategory.ToListAsync(), "Id", "Name");

            MainModel = new PartnerDto();
            
            Partners = (await _context.Partner.Include(c => c.Category).ToListAsync()).OrderByDescending(c => c.Index)
                        .Select(c => new PartnerListItemResponse
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Thumbnail = c.Thumbnail,
                            Category = c.Category.Name,
                            CategoryId = c.CategoryId,
                            Index=c.Index
                        }).ToList();

            return Page();
        }
     
        public async Task<IActionResult> OnDeleteAsync(int Id)
        {
            var entity = await _context.Partner.FindAsync(Id);
            if (entity == null) return NotFound();
            _context.Partner.Remove(entity);
            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new OkResult();
            }
            return BadRequest();
        }
        public async Task<IActionResult> OnPostChangeIndex(int Id, bool moveUp = true)
        {
            var entity = await _context.Partner.FindAsync(Id);
            if (entity == null) return new JsonResult(new { status = false, message = "Đổi vị trí không thành công" });

            domain.Partner swapNew = null;
            //up
            if (moveUp)
            {
                swapNew = await _context.Partner.Where(c => c.Index > entity.Index).OrderBy(c => c.Index).FirstOrDefaultAsync();
            }
            else
            {
                swapNew = await _context.Partner.Where(c => c.Index < entity.Index).OrderByDescending(c => c.Index).FirstOrDefaultAsync();
            }


            if (swapNew == null) return new JsonResult(new
            {
                status = false,
                message = "Đổi vị trí không thành công"
            });

            int currentIndex = entity.Index;
            entity.Index = swapNew.Index;
            swapNew.Index = currentIndex;
            _context.Partner.Update(entity);
            _context.Partner.Update(swapNew);
            if (await _context.SaveChangesAsync() >= 1) return new JsonResult(new { status = true });
            else return new JsonResult(new { status = false, message = "Đổi vị trí không thành công" });
        }
    }
}

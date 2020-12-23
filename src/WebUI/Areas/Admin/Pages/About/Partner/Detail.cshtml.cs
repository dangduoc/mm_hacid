using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using BaseProjectWebRazor.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using domain = Domain.Entities;
namespace BaseProjectWebRazor.Areas.Admin.Pages.About.Partner
{
    public class DetailModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly ILocalUploadService _uploadService;

        public DetailModel(IApplicationDbContext context,
            ILocalUploadService uploadService
            )
        {
            _context = context;
            _uploadService = uploadService;
        }
        public SelectList Categories { get; set; }
        [BindProperty]
        public PartnerDto MainModel { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public async Task<IActionResult> OnGetAsync(int Id)
        {
            Categories = new SelectList(await _context.PartnerCategory.ToListAsync(), "Id", "Name");

            var entity = await _context.Partner.FindAsync(Id);
            if (entity == null) return NotFound();
            MainModel = new PartnerDto
            {
                Id = entity.Id,
                CategoryId = entity.CategoryId,
                Link = entity.Link,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Thumbnail = entity.Thumbnail,
                Index=entity.Index
            };
            // MainModel = _mapper.Map<NewDto>(entity);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var entity = await _context.Partner.FindAsync(MainModel.Id);
                if (entity == null) return NotFound();

                entity.Name = MainModel.Name;
                entity.NameEn = MainModel.NameEn;
                
                entity.CategoryId = MainModel.CategoryId;
                entity.Link = MainModel.Link;
                entity.Thumbnail = MainModel.Thumbnail;


                if ((Upload != null) && (Upload.Length > 0))
                {
                    var uploadResult = await _uploadService.UploadImage(Upload, @"partners", 300, 300);
                    if (uploadResult.Status)
                    {
                        entity.Thumbnail = uploadResult.Data.FileUrl;
                    }
                }


                _context.Partner.Update(entity);

                if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
                {
                    // RedirectToAction("Index", "ControlerName");
                    return Redirect("/admin/about/partner");

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

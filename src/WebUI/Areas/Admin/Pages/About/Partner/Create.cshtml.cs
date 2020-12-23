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
    public class CreateModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly ILocalUploadService _uploadService;

        public CreateModel(IApplicationDbContext context, ILocalUploadService uploadService)
        {
            _context = context;
            _uploadService = uploadService;
        }
        public SelectList Categories { get; set; }
        [BindProperty]
        public PartnerDto MainModel { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Categories = new SelectList(await _context.PartnerCategory.ToListAsync(), "Id", "Name");

            MainModel = new PartnerDto();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                domain.Partner createModel = new domain.Partner
                {
                    Name = MainModel.Name,
                    NameEn = MainModel.NameEn,
                    CategoryId = MainModel.CategoryId,
                    Link = MainModel.Link,
                    Thumbnail = ""
                };
                createModel.Index = await _context.Partner.CountAsync() + 1;
                if ((Upload != null) && (Upload.Length > 0))
                {
                    var uploadResult = await _uploadService.UploadImage(Upload, @"partners", 300, 300);
                    if (uploadResult.Status)
                    {
                        createModel.Thumbnail = uploadResult.Data.FileUrl;
                    }
                }
             

                _context.Partner.Add(createModel);

                if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
                {
                    return Redirect("/admin/about/partner");
                    //RedirectToPage("About/Partner");
                    //RedirectToAction("OnGetAsync");
                    //Partners.Add(new PartnerListItemResponse
                    //{
                    //    CategoryId = createModel.CategoryId,
                    //    Id = createModel.Id,
                    //    Name = createModel.Name,
                    //    Thumbnail = createModel.Thumbnail,
                    //    Category = Categories.FirstOrDefault(c => c.Value == createModel.CategoryId.ToString()).Text
                    //});
                    //return Page();
                }
                ViewData["error"] = "Thêm không thành công";
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

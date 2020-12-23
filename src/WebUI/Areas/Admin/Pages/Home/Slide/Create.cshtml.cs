using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using BaseProjectWebRazor.Areas.Admin.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectWebRazor.Areas.Admin.Pages.Home.Slide
{
    public class CreateModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly ILocalUploadService _uploadService;
        public CreateModel(IApplicationDbContext context, ILocalUploadService uploadService
            )
        {
            _context = context;
            _uploadService = uploadService;
        }
        [BindProperty]
        public HomeSlideDto MainModel { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public SelectList Themes { get; set; }
        public void OnGet()
        {
            var list = new List<object>();
            list.Add(new
            {
                Id = 0,
                Name = "Sáng"
            });
            list.Add(new
            {
                Id = 1,
                Name = "Tối"
            });

            Themes = new SelectList(list, "Id", "Name", 0);
            MainModel = new HomeSlideDto();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                HomeSlide entity = new HomeSlide
                {
                    Image = MainModel.Image,
                    Link = MainModel.Link,
                    Theme = MainModel.Theme,
                    Title = MainModel.Title,
                    TitleEn = MainModel.TitleEn,
                    Location=MainModel.Location,
                    LocationEn= MainModel.LocationEn,
                    ProjectTitle=MainModel.ProjectTitle,
                    ProjectTitleEn=MainModel.ProjectTitleEn,
                    Id=1,Status=1
                };
                var last = await _context.HomeSlide.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
                if (last != null)
                {

                    entity.Id = last.Id + 1;
                   
                }
                entity.Index = entity.Id;
                if ((Upload != null) && (Upload.Length > 0))
                {
                    var uploadResult = await _uploadService.UploadImage(Upload, @"home/slide", null, null, false);
                    if (uploadResult.Status)
                    {
                        entity.Image = uploadResult.Data.FileUrl;
                    }
                }



                _context.HomeSlide.Add(entity);

                if (await _context.SaveChangesAsync() >= 1)
                {
                    return Redirect("/admin/home/slide");
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

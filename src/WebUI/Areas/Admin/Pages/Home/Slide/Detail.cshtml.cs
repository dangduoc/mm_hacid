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

namespace BaseProjectWebRazor.Areas.Admin.Pages.Home.Slide
{
    public class DetailModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly ILocalUploadService _uploadService;
        public DetailModel(IApplicationDbContext context, ILocalUploadService uploadService
            )
        {
            _context = context;
            _uploadService = uploadService;
        }
        [BindProperty]
        public HomeSlide MainModel { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public SelectList Themes { get; set; }
        public async Task OnGetAsync(int Id)
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
            MainModel = await _context.HomeSlide.FindAsync(Id);
         
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                //HomeSlide entity= await _context.HomeSlide.FindAsync()
                //HomeSlide entity = new HomeSlide
                //{
                //    Image = MainModel.Image,
                //    Link = MainModel.Link,
                //    Theme = MainModel.Theme,
                //    Title = MainModel.Title,
                //    TitleEn = MainModel.TitleEn,
                //    Id = 1
                //};
               // var last = await _context.HomeSlide.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
                //if (last != null)
            
                //entity.Index = entity.Id;
                if ((Upload != null) && (Upload.Length > 0))
                {
                    var uploadResult = await _uploadService.UploadImage(Upload, @"home/slide", null, null, false);
                    if (uploadResult.Status)
                    {
                        MainModel.Image = uploadResult.Data.FileUrl;
                    }
                }



                _context.HomeSlide.Update(MainModel);

                if (await _context.SaveChangesAsync() >= 1)
                {
                    return Redirect("/admin/home/slide");
                }
                ViewData["error"] = "Thay đổi thông tin không thành công";
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

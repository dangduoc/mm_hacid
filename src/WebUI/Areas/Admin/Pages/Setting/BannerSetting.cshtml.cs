using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectWebRazor.Areas.Admin.Pages.Setting
{
    public class BannerSettingModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        public BannerSettingModel(IApplicationDbContext context)
        {
            _context = context;
        }
        // [BindProperty]
        // public BannerSetting MainModel { get; set; }
        public SelectList Themes { get; set; }
        [BindProperty]
        public List<BannerSetting> Banners { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["success"] = null;
            ViewData["error"] = null;
            Banners=await _context.BannerSetting.ToListAsync();
            var list = new List<object>();
            list.Add(new {
                Id = 0,
                Name = "Sáng"
            });
            list.Add(new
            {
                Id = 1,
                Name = "Tối"
            });

            Themes = new SelectList(list,"Id","Name",0);
            //MainModel =await _context.BannerSetting.FirstOrDefaultAsync();
            //if (MainModel==null) return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                foreach (var item in Banners)
                {
                    _context.BannerSetting.Update(item);
                }
               

                if (await _context.SaveChangesAsync() >= 1)
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
                    ViewData["success"] = "Lưu thành công";
                    return Page();
                }
                else
                {
                    ViewData["error"] = "Lưu thông tin không thành công";
                    return Page();
                }


            }
            catch (Exception ex)
            {
                ViewData["error"] = "Đã xảy ra lỗi: " + ex.ToString();
                return Page();
            }
        }
        
    }
}

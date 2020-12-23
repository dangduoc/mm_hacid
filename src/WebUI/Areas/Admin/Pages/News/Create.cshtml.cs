using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using BaseProjectWebRazor.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using domain = Domain.Entities;
namespace BaseProjectWebRazor.Areas.Admin.Pages.News
{
    public class CreateModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateModel(IApplicationDbContext context,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _context = context;
        }
        public SelectList Categories { get; set; }
        [BindProperty]
        public NewDto MainModel { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Categories = new SelectList(await _context.NewCategory.ToListAsync(), "Id", "Name");
        
            MainModel = new NewDto();
            MainModel.IsEnglishIncluded = false;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                MainModel.Thumbnail = "";
                MainModel.Banners = "";
                MainModel.HtmlContent = "";
                MainModel.TotalViews = 0;
                MainModel.Index = (await _context.New.CountAsync()) + 1;
                var entity = _mapper.Map<domain.New>(MainModel);
             
                _context.New.Add(entity);

                if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
                {
                    return Redirect("/admin/news/detail/" + entity.Id);

                }
                ViewData["error"] = "Lưu thông tin dự án không thành công";
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

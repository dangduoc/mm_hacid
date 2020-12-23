using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using BaseProjectWebRazor.Areas.Admin.Models;
using domain=Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BaseProjectWebRazor.Areas.Admin.Pages.ProjectPage
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
        public SelectList Locations { get; set; }
        public SelectList ProjectFields { get; set; }
        [BindProperty]
        public ProjectDto MainModel { get; set; }
        public SelectList Themes { get; set; }
        public async Task<IActionResult> OnGetAsync()
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
            Categories = new SelectList(await _context.ProjectCategory.ToListAsync(), "Id", "Name");
            Locations = new SelectList(await _context.Location.ToListAsync(), "Id", "Name");
            ProjectFields = new SelectList(await _context.ProjectField.ToListAsync(), "Id", "Name");
            MainModel = new ProjectDto();
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
                MainModel.Index = await _context.Project.CountAsync() + 1;
                var entity = _mapper.Map<domain.Project>(MainModel);

                _context.Project.Add(entity);
                
                if (await _context.SaveChangesAsync(CancellationToken.None)>=1)
                {
                    return Redirect("/admin/project/detail/" + entity.Id);
                   
                }
                ViewData["error"] = "Lưu thông tin dự án không thành công";
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["error"] = "Đã xảy ra lỗi: "+ex.ToString();
                return Page();
            }
        }
    }
}

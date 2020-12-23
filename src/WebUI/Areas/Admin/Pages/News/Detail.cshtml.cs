using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using domain = Domain.Entities;
using BaseProjectWebRazor.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace BaseProjectWebRazor.Areas.Admin.Pages.News
{
    public class DetailModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public DetailModel(IApplicationDbContext context,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _context = context;
        }
        public SelectList Categories { get; set; }
        [BindProperty]
        public NewDto MainModel { get; set; }
        public async Task<IActionResult> OnGetAsync(int Id)
        {
            Categories = new SelectList(await _context.NewCategory.ToListAsync(), "Id", "Name");
         
            var entity = await _context.New.FindAsync(Id);
            if (entity == null) return NotFound();
            MainModel = _mapper.Map<NewDto>(entity);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _context.New.Update(_mapper.Map<domain.New>(MainModel));

                if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
                {
                    return Redirect("/admin/news");
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

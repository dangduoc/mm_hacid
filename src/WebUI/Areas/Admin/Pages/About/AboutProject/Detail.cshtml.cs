using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using BaseProjectWebRazor.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using domain = Domain.Entities;

namespace BaseProjectWebRazor.Areas.Admin.Pages.About.AboutProject
{
    public class DetailModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly ILocalUploadService _uploadService;
        private readonly IMapper _mapper;
        public DetailModel(IApplicationDbContext context, ILocalUploadService uploadService,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _context = context;
            _uploadService = uploadService;
        }
        [BindProperty]
        public AboutProjectDto MainModel { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public async Task<IActionResult> OnGetAsync(int Id)
        {
            var entity =await  _context.AboutProject.FindAsync(Id);
            if (entity == null) return NotFound();
            MainModel = _mapper.Map<AboutProjectDto>(entity);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
               
                domain.AboutProject updateModel = _mapper.Map<domain.AboutProject>(MainModel);

                if ((Upload != null) && (Upload.Length > 0))
                {
                    var uploadResult = await _uploadService.UploadImage(Upload, @"about/project", null, null, false);
                    if (uploadResult.Status)
                    {
                        updateModel.Image = uploadResult.Data.FileUrl;
                    }
                }


                _context.AboutProject.Update(updateModel);

                if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
                {
                    return Redirect("/admin/about/aboutproject");
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

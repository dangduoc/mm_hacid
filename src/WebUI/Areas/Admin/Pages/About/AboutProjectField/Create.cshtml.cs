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

namespace BaseProjectWebRazor.Areas.Admin.Pages.About.AboutProjectField
{
    public class CreateModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly ILocalUploadService _uploadService;
        private readonly IMapper _mapper;
        public CreateModel(IApplicationDbContext context, ILocalUploadService uploadService,
            IMapper mapper
            )
        {
               _mapper=mapper;
            _context = context;
            _uploadService = uploadService;
        }
        [BindProperty]
        public AboutProjectFieldDto MainModel { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public void OnGet()
        {
            MainModel = new AboutProjectFieldDto();
            MainModel.OrderIndex = 1;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                domain.AboutProjectField createModel = _mapper.Map<domain.AboutProjectField>(MainModel);
                createModel.Banner = "";
                if ((Upload != null) && (Upload.Length > 0))
                {
                    var uploadResult = await _uploadService.UploadImage(Upload, @"about/project_field", null, null,false);
                    if (uploadResult.Status)
                    {
                        createModel.Banner = uploadResult.Data.FileUrl;
                    }
                }


                _context.AboutProjectField.Add(createModel);

                if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
                {
                    return Redirect("/admin/about/aboutprojectfield");
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

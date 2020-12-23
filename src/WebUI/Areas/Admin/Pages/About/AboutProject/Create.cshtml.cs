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
        public AboutProjectDto MainModel { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public void OnGet()
        {
            MainModel = new AboutProjectDto();
            MainModel.Summary = "";
            MainModel.SummaryEn = "";
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                domain.AboutProject createModel = _mapper.Map<domain.AboutProject>(MainModel);
                if (createModel.Summary == null) createModel.Summary = "";
                if (createModel.SummaryEn == null) createModel.SummaryEn = "";
                createModel.Image = "";
                createModel.Id = 1;
                var last = await _context.AboutProject.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
                if (last != null) createModel.Id = last.Id + 1;
                createModel.Index = createModel.Id;
                if ((Upload != null) && (Upload.Length > 0))
                {
                    var uploadResult = await _uploadService.UploadImage(Upload, @"about/project", null, null,false);
                    if (uploadResult.Status)
                    {
                        createModel.Image = uploadResult.Data.FileUrl;
                    }
                }



                _context.AboutProject.Add(createModel);

                if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
                {
                    return Redirect("/admin/about/aboutproject");
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

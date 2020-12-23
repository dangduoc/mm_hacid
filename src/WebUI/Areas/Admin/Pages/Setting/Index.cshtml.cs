using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using BaseProjectWebRazor.Areas.Admin.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectWebRazor.Areas.Admin.Pages.Setting
{
    public class IndexModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILocalUploadService _uploadService;
        public IndexModel(IApplicationDbContext context,IMapper mapper, ILocalUploadService uploadService)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
        }
        [BindProperty]
        public SiteSettingDto MainModel { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
           var entity=await  _context.SiteSetting.FirstOrDefaultAsync();
            if (entity == null) return NotFound();
            MainModel = _mapper.Map<SiteSettingDto>(entity);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if ((Upload != null) && (Upload.Length > 0))
                {
                    var uploadResult = await _uploadService.UploadImage(Upload, @"about", null, null, false);
                    if (uploadResult.Status)
                    {
                        MainModel.CompanyImage = uploadResult.Data.FileUrl;
                    }
                }
                _context.SiteSetting.Update(_mapper.Map<SiteSetting>(MainModel));

                if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
                {
                    ViewData["success"] = "Lưu thông tin thành công";
                    return Page();
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

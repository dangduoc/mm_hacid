using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectWebRazor.Pages.About
{
    public class PartnersModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentRequestLanguageService _languageService;
        public List<SelectOption> Categories { get; set; }
        public int? SelectedCate { get; set; }
        public List<PartnerModel> Partners { get; set; }
        public PartnersModel(IApplicationDbContext context, ICurrentRequestLanguageService languageService)
        {
            _context = context;
            _languageService = languageService;
        }
        public async Task<IActionResult> OnGetAsync(int? cate = null)
        {
            SelectedCate = cate;
            Categories = (await _context.PartnerCategory.ToListAsync()).Select(c => new SelectOption
            {
                Id = c.Id,
                Name = _languageService.IsDefault ? c.Name : c.NameEn,
                Selected = c.Id == cate
            }).ToList();

            IEnumerable<Partner> full_query = await _context.Partner.OrderByDescending(c=>c.Index).ToListAsync();
            if (cate.HasValue)
            {
                full_query = full_query.Where(p => p.CategoryId == cate.Value).OrderByDescending(c => c.Index);
            }
            Partners = full_query.Select(c => new PartnerModel
            {
                Id = c.Id,
                Name = c.Name,
                Thumbnail = c.Thumbnail,
                Link=c.Link
            }).ToList();
            return Page();
        }
    }
    public class PartnerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Link { get; set; }
    }
    public class SelectOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using BaseProjectWebRazor.Models;
using BaseProjectWebRazor.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using domain = Domain.Entities;
namespace BaseProjectWebRazor.Pages.New
{
    public class IndexModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentRequestLanguageService _languageService;
        public List<SelectOption> Categories { get; set; }
        public ViewNewListItemModel ViewNewListItemModel { get; set; }
        public int CurrentPage { get; set; }
        public int OrderBy { get; set; }
        public int? SelectedCate { get; set; }
        public IndexModel(IApplicationDbContext context, ICurrentRequestLanguageService languageService)
        {
            _context = context;
            _languageService = languageService;
        }
        public async Task<IActionResult> OnGetAsync(string textSearch,
          int orderby = 0,
          int? cate = null,
          int p = 1,
          int ps = 10)
        {
            OrderBy = orderby;
            SelectedCate = cate;
            Categories = (await _context.NewCategory.Where(c=>c.Id>1).ToListAsync()).Select(c => new SelectOption
            {
                Id = c.Id,
                Name = _languageService.IsDefault ? c.Name : c.NameEn,
                Selected = c.Id == cate
            }).OrderBy(c => c.Id).ToList();

            IEnumerable<domain.New> full_query = await _context.New.Where(c => c.Status == 1 &&c.CategoryId>1).OrderByDescending(c=>c.Index).Include(c => c.Category).ToListAsync();
            if (!_languageService.IsDefault)
            {
                full_query = full_query.Where(c => c.IsEnglishIncluded == true);
            }

            if (cate.HasValue)
            {
                full_query = full_query.Where(p => p.CategoryId == cate.Value);
            }

            if (!string.IsNullOrEmpty(textSearch))
            {
                full_query = full_query.Where(p => p.Title.ToLower().Trim().Contains(textSearch.ToLower().Trim()));
            }
            if (orderby == 1)
            {
                full_query = full_query.OrderByDescending(c => c.CreatedOnDate);
            }
            else if(orderby==2)
            {
                full_query = full_query.OrderBy(c => c.Title);
            }
            ViewNewListItemModel = new ViewNewListItemModel();
            ViewNewListItemModel.PageInfo = new PaginationModel
            {
                Count = full_query.Count(),
                PageSize = ps,
                CurrentPage = p
            };
            CurrentPage = p;
            ViewNewListItemModel.Items = full_query
                .Skip((p - 1) * ps).Take(ps)
                .Select(c => new NewListItemModel
                {
                    Id = c.Id,
                    CateogryId = c.CategoryId,
                    Category = _languageService.IsDefault ? c.Category.Name : c.Category.NameEn,
                    Thumbnail = c.Thumbnail,
                    Title = _languageService.IsDefault ? c.Title : c.TitleEn,
                    PublishedDate = c.CreatedOnDate.ToString("dd.MM.yyyy"),
                    TotalViews = 123
                }).ToList();
            return Page();
        }
    }
    public class SelectOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}

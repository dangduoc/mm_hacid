using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using BaseProjectWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using domain = Domain.Entities;
namespace BaseProjectWebRazor.Pages.ProjectPage
{
    public class IndexModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentRequestLanguageService _languageService;

        public IndexModel(IApplicationDbContext context,ICurrentRequestLanguageService languageService)
        {
            _context = context;
            _languageService = languageService;
        }
        public List<SelectOption> Categories { get; set; }
        public List<SelectOption> Fields { get; set; }
        public List<SelectOption> Locations { get; set; }
        public int OrderBy { get; set; }
        public List<ProjectListItem> Projects { get; set; }
        public PaginationModel PagingInfo { get; set; }
        public int CurrentPage { get; set; }
        private async Task<int> LoadCategories(int? cate = null,
            int? field = null,
            int? location = null)
        {
            Categories = (await _context.ProjectCategory.ToListAsync()).Select(c => new SelectOption
            {
                Id = c.Id,
                Name = _languageService.IsDefault ? c.Name : c.NameEn,
                Selected = c.Id==cate
            }).OrderBy(c => c.Id).ToList();
            //
            Fields = (await _context.ProjectField.ToListAsync()).Select(c => new SelectOption
            {
                Id = c.Id,
                Name = _languageService.IsDefault ? c.Name : c.NameEn,
                Selected = c.Id == field
            }).OrderBy(c => c.Id).ToList();
            //
            Locations = (await _context.Location.ToListAsync()).Select(c => new SelectOption
            {
                Id = c.Id,
                Name = _languageService.IsDefault ? c.Name : c.NameEn,
                Selected = c.Id == location
            }).OrderBy(c => c.Id).ToList();
            return 1;
        }
        public async Task<IActionResult> OnGetAsync(string textSearch,
            int orderby = 0,
            int? cate = null, 
            int? field = null,
            int? location = null,
            int p = 1, 
            int ps = 20)
        {
            OrderBy = orderby;
            await LoadCategories(cate,field,location);
            IEnumerable<domain.Project> full_query = await _context.Project.Where(c => c.Status == 1).OrderByDescending(c=>c.Index).Include(c=>c.Category).Include(c=>c.ProjectField).Include(c=>c.Location).ToListAsync();
            if (!_languageService.IsDefault)
            {
                full_query = full_query.Where(c => c.IsEnglishIncluded == true);
            }
            
            if (cate.HasValue)
            {
                full_query = full_query.Where(p => p.CategoryId == cate.Value);
            }
            if (field.HasValue)
            {
                full_query = full_query.Where(p => p.ProjectFieldId == field.Value);
            }
            if (location.HasValue)
            {
                full_query = full_query.Where(p => p.LocationId == location.Value);
            }
            if (!string.IsNullOrEmpty(textSearch))
            {
                full_query = full_query.Where(p => p.Title.ToLower().Trim().Contains(textSearch.ToLower().Trim()));
            }
            if (orderby == 1)
            {
                full_query=full_query.OrderByDescending(c => c.CreatedOnDate);
            }
            else if(orderby==2)
            {
                full_query = full_query.OrderBy(c => c.Title);
            }
            PagingInfo = new PaginationModel
            {
                Count = full_query.Count(),
                PageSize = ps,
                CurrentPage = p
            };
            CurrentPage = p;
            Projects = full_query
                .Skip((p - 1) * ps).Take(ps)
                .Select(c => new ProjectListItem
                {
                    Id = c.Id,
                    Category = _languageService.IsDefault ? c.Category.Name : c.Category.NameEn,
                    Field = _languageService.IsDefault ? c.ProjectField.Name : c.ProjectField.NameEn,
                    Location = _languageService.IsDefault ? c.Location.Name : c.Location.NameEn,
                    Thumbnail = c.Thumbnail,
                    Title = _languageService.IsDefault?c.Title:c.TitleEn,
                    Year = c.Year
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
    public class ProjectListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string Thumbnail { get; set; }
        public string Field { get; set; }
        public int Year { get; set; }
    }
}

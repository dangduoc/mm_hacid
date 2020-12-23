using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using BaseProjectWebRazor.Areas.Admin.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectWebRazor.Areas.Admin.Pages.Category
{
    public class ProjectCategoryModel : PageModel
    {

        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        public ProjectCategoryModel(IApplicationDbContext context,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _context = context;
        }
        public List<ProjectCategoryDto> Categories { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Categories = (await _context.ProjectCategory.ToListAsync()).Select(c => new ProjectCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                NameEn = c.NameEn,
            }).ToList();

            return Page();
        }
        public async Task<IActionResult> OnGetDetailAsync(int Id)
        {
            var cate = await _context.ProjectCategory.FindAsync(Id);
            return new JsonResult(_mapper.Map<ProjectCategoryDto>(cate));
        }
        public async Task<IActionResult> OnPostAsync()
        {
            ProjectCategory createModel = new ProjectCategory
            {
                Name = Request.Form["name"],
                NameEn = Request.Form["name_en"],
                Id = 1
            };
            var last = await _context.ProjectCategory.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
            if (last != null) createModel.Id = last.Id + 1;
            //var result = await _handler.Create(_mapper.Map<CityList>(createModel));
            _context.ProjectCategory.Add(createModel);
            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new JsonResult(_mapper.Map<ProjectCategoryDto>(createModel));
            }
            return BadRequest();

        }
        public async Task<IActionResult> OnPutAsync([FromBody] ProjectCategoryDto updateModel)
        {
            //var model = _mapper.Map<LocationDto>(updateModel);
          
            var entity = await _context.ProjectCategory.FindAsync(updateModel.Id);
            entity.Name = updateModel.Name;
            entity.NameEn = updateModel.NameEn;
            _context.ProjectCategory.Update(entity);

            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new JsonResult(_mapper.Map<ProjectCategoryDto>(entity));
            }
            return BadRequest();
        }
        public async Task<IActionResult> OnDeleteAsync(int Id)
        {
            var entity = await _context.ProjectCategory.FindAsync(Id);
            if (entity == null) return NotFound();
            _context.ProjectCategory.Remove(entity);

            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new OkResult();
            }
            return BadRequest();
        }
    }
}

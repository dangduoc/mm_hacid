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
    public class ProjectFieldModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        public ProjectFieldModel(IApplicationDbContext context,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _context = context;
        }
        public List<ProjectFieldDto> Fields { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Fields = (await _context.ProjectField.ToListAsync()).Select(c => new ProjectFieldDto
            {
                Id = c.Id,
                Name = c.Name,
                NameEn = c.NameEn
            }).ToList();

            return Page();
        }
        public async Task<IActionResult> OnGetDetailAsync(int Id)
        {
            var cate = await _context.ProjectField.FindAsync(Id);
            return new JsonResult(_mapper.Map<ProjectFieldDto>(cate));
        }
        public async Task<IActionResult> OnPostAsync()
        {
            ProjectField createModel = new ProjectField
            {
                Name = Request.Form["name"],
                NameEn = Request.Form["name_en"],
                Id = 1
            };
            var last = await _context.ProjectField.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
            if (last != null) createModel.Id = last.Id + 1;
            //var result = await _handler.Create(_mapper.Map<CityList>(createModel));
            _context.ProjectField.Add(_mapper.Map<ProjectField>(createModel));
            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new JsonResult(_mapper.Map<ProjectFieldDto>(createModel));
            }
            return BadRequest();

        }
        public async Task<IActionResult> OnPutAsync([FromBody] ProjectFieldDto updateModel)
        {
            //var model = _mapper.Map<LocationDto>(updateModel);
            var entity = await _context.ProjectField.FindAsync(updateModel.Id);
            entity.Name = updateModel.Name;
            entity.NameEn = updateModel.NameEn;
            _context.ProjectField.Update(entity);

            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new JsonResult(_mapper.Map<ProjectFieldDto>(entity));
            }
            return BadRequest();
        }
        public async Task<IActionResult> OnDeleteAsync(int Id)
        {
            var entity = await _context.ProjectField.FindAsync(Id);
            if (entity == null) return NotFound();
            _context.ProjectField.Remove(entity);

            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new OkResult();
            }
            return BadRequest();
        }
    }
}

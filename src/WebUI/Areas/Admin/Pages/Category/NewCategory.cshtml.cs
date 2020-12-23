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
    public class NewCategoryModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        public NewCategoryModel(IApplicationDbContext context,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _context = context;
        }
        public List<NewCategoryDto> Categories { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Categories = (await _context.NewCategory.ToListAsync()).Select(c => new NewCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                NameEn = c.NameEn
            }).ToList();

            return Page();
        }
        public async Task<IActionResult> OnGetDetailAsync(int Id)
        {
            var cate = await _context.NewCategory.FindAsync(Id);
            return new JsonResult(_mapper.Map<NewCategoryDto>(cate));
        }
        public async Task<IActionResult> OnPostAsync()
        {
            NewCategory createModel = new NewCategory
            {
                Name = Request.Form["name"],
                NameEn = Request.Form["name_en"],
                Id = 1
            };
            var last = await _context.NewCategory.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
            if (last != null) createModel.Id = last.Id + 1;
            //var result = await _handler.Create(_mapper.Map<CityList>(createModel));
            _context.NewCategory.Add(createModel);
            if(await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new JsonResult(_mapper.Map<NewCategoryDto>(createModel));
            }
            return BadRequest();
           
        }
        public async Task<IActionResult> OnPutAsync([FromBody] NewCategoryDto updateModel)
        {
            //var model = _mapper.Map<LocationDto>(updateModel);
            var entity = await _context.NewCategory.FindAsync(updateModel.Id);
            entity.Name = updateModel.Name;
            entity.NameEn = updateModel.NameEn;
            _context.NewCategory.Update(entity);

            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new JsonResult(_mapper.Map<NewCategoryDto>(entity));
            }
            return BadRequest();
        }
        public async Task<IActionResult> OnDeleteAsync(int Id)
        {
            var entity = await _context.NewCategory.FindAsync(Id);
            if (entity == null) return NotFound();
            _context.NewCategory.Remove(entity);

            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new OkResult();
            }
            return BadRequest();
        }
    }
}

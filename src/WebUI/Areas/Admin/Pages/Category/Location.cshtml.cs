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
    public class LocationModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        public LocationModel(IApplicationDbContext context,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _context = context;
        }
        public List<LocationDto> Locations { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Locations = (await _context.Location.ToListAsync()).Select(c => new LocationDto
            {
                Id = c.Id,
                Name = c.Name,
                NameEn = c.NameEn
            }).ToList();

            return Page();
        }
        public async Task<IActionResult> OnGetDetailAsync(int Id)
        {
            var cate = await _context.Location.FindAsync(Id);
            return new JsonResult(_mapper.Map<LocationDto>(cate));
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Location createModel = new Location
            {
                Name = Request.Form["name"],
                NameEn = Request.Form["name_en"],
                Id = 1
            };
            var last = await _context.Location.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
            if (last != null) createModel.Id = last.Id + 1;
            //var result = await _handler.Create(_mapper.Map<CityList>(createModel));
            _context.Location.Add(createModel);
            if(await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new JsonResult(_mapper.Map<LocationDto>(createModel));
            }
            return BadRequest();
           
        }
        public async Task<IActionResult> OnPutAsync([FromBody] Location updateModel)
        {
            //var model = _mapper.Map<LocationDto>(updateModel);
            var entity = await _context.Location.FindAsync(updateModel.Id);
            entity.Name = updateModel.Name;
            entity.NameEn = updateModel.NameEn;
            _context.Location.Update(entity);

            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new JsonResult(_mapper.Map<LocationDto>(entity));
            }
            return BadRequest();
        }
        public async Task<IActionResult> OnDeleteAsync(int Id)
        {
            var entity = await _context.Location.FindAsync(Id);
            if (entity == null) return NotFound();
            _context.Location.Remove(entity);

            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new OkResult();
            }
            return BadRequest();
        }
    }
}

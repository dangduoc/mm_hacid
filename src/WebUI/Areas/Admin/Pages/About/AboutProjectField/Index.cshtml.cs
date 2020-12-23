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
using domain = Domain.Entities;
namespace BaseProjectWebRazor.Areas.Admin.Pages.About.AboutProjectField
{
    public class IndexModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        public IndexModel(IApplicationDbContext context
            )
        {
            _context = context;
        }
        public List<AboutProjectFieldDto> Fields { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Fields = (await _context.AboutProjectField.ToListAsync()).Select(c => new AboutProjectFieldDto
            {
                Id = c.Id,
                Name = c.Name,
                NameEn = c.NameEn,
                Summary=c.Summary,
                SummaryEn=c.SummaryEn,
                OrderIndex=c.OrderIndex
            }).OrderBy(c=>c.OrderIndex).ToList();

            return Page();
        }
        public async Task<IActionResult> OnDeleteAsync(int Id)
        {
            var entity = await _context.AboutProjectField.FindAsync(Id);
            if (entity == null) return NotFound();
            _context.AboutProjectField.Remove(entity);

            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new OkResult();
            }
            return BadRequest();
        }
    }
}

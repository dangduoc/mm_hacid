using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using domain = Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Application.Wrappers;
using BaseProjectWebRazor.Areas.Admin.Models;
using System.Threading;
using Domain.Entities;

namespace BaseProjectWebRazor.Areas.Admin.Pages.Project
{
    public class IndexModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        public IndexModel(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetDataAsync(int start, int length, int draw, string textSearch)
        {
            int page = start / length + 1;
            try
            {
                IEnumerable<domain.Project> full_query = await _context.Project.Where(c=>c.Status>=0).ToListAsync();
                if (!string.IsNullOrEmpty(textSearch))
                {
                    full_query = (await _context.Project.Where(p => p.Title.ToLower().Trim().Contains(textSearch.ToLower().Trim())).ToListAsync());
                }
                List<ProjectListItemResponse> result = full_query.OrderByDescending(c => c.Index).Skip((page - 1) * length).Take(length)
                            .Select(c => new ProjectListItemResponse
                            {
                                Id = c.Id,
                                CreatedOnDate = c.CreatedOnDate.ToString("dd/MM/yyyy"),
                                Investor = c.Investor,
                                Title = c.Title,
                                Year = c.Year,
                                Status=c.Status==1
                            }).ToList();

                return new JsonResult(new DataTableResponse<List<ProjectListItemResponse>>
                {
                    Data = result,
                    Draw = draw,
                    RecordsFiltered = full_query.Count(),
                    RecordsTotal = full_query.Count()
                });
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<IActionResult> OnDeleteAsync(int Id)
        {
            var entity = await _context.Project.FindAsync(Id);
            if (entity == null) return NotFound();
            entity.Status = -1;
            _context.Project.Update(entity);
            if(await _context.SaveChangesAsync(CancellationToken.None)>=1)
            {
                return new OkResult();
            }
            return BadRequest();
        }
        public async Task<IActionResult> OnPostHideAsync(int Id)
        {
            var entity = await _context.Project.FindAsync(Id);
            if (entity == null) return new BadRequestResult();
            if (entity.Status == -1) return new BadRequestResult();
            if (entity.Status == 1) entity.Status = 0;
            else entity.Status = 1;

            _context.Project.Update(entity);
            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new OkResult();
            }
            return BadRequest();
        }

        public async Task<IActionResult> OnPostChangeIndex(int Id, bool moveUp = true)
        {
            var entity = await _context.Project.FindAsync(Id);
            if (entity == null) return new JsonResult(new { status = false, message = "Đổi vị trí không thành công" });

            domain.Project swapNew = null;
            //up
            if (moveUp)
            {
                swapNew = await _context.Project.Where(c => c.Index > entity.Index && c.Status != -1).OrderBy(c => c.Index).FirstOrDefaultAsync();
            }
            else
            {
                swapNew = await _context.Project.Where(c => c.Index < entity.Index && c.Status != -1).OrderByDescending(c => c.Index).FirstOrDefaultAsync();
            }


            if (swapNew == null) return new JsonResult(new
            {
                status = false,
                message = "Đổi vị trí không thành công"
            });

            int currentIndex = entity.Index;
            entity.Index = swapNew.Index;
            swapNew.Index = currentIndex;
            _context.Project.Update(entity);
            _context.Project.Update(swapNew);
            if (await _context.SaveChangesAsync() >= 1) return new JsonResult(new { status = true });
            else return new JsonResult(new { status = false, message = "Đổi vị trí không thành công" });
        }

    }
}

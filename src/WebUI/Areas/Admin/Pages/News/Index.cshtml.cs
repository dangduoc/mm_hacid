using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Wrappers;
using BaseProjectWebRazor.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using domain = Domain.Entities;
namespace BaseProjectWebRazor.Areas.Admin.Pages.News
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
                IEnumerable<domain.New> full_query = await _context.New.Where(c => c.Status >=0).Include(c => c.Category).ToListAsync();
                if (!string.IsNullOrEmpty(textSearch))
                {
                    full_query = (await _context.New.Where(p => p.Title.ToLower().Trim().Contains(textSearch.ToLower().Trim())).ToListAsync());
                }
                List<NewListItemResponse> result = full_query.OrderByDescending(c => c.Index).Skip((page - 1) * length).Take(length)
                            .Select(c => new NewListItemResponse
                            {
                                Id = c.Id,
                                CreatedOnDate = c.CreatedOnDate.ToString("dd/MM/yyyy"),
                                Category = c.Category.Name,
                                Title = c.Title,
                                CategoryId = c.CategoryId,
                                Status=c.Status==1
                            }).ToList();

                return new JsonResult(new DataTableResponse<List<NewListItemResponse>>
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
            var entity = await _context.New.FindAsync(Id);
            if (entity == null) return NotFound();
            entity.Status = -1;
            _context.New.Update(entity);
            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new OkResult();
            }
            return BadRequest();
        }
        public async Task<IActionResult> OnPostHideAsync(int Id)
        {
            var entity = await _context.New.FindAsync(Id);
            if (entity == null) return new BadRequestResult();
            if (entity.Status == -1) return new BadRequestResult();
            if (entity.Status == 1) entity.Status = 0;
            else entity.Status = 1;
       
            _context.New.Update(entity);
            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new OkResult();
            }
            return BadRequest();
        }
        public async Task<IActionResult> OnPostChangeIndex(int Id, bool moveUp=true)
        {
            var entity = await _context.New.FindAsync(Id);
            if (entity == null) return new JsonResult(new { status = false, message = "Đổi vị trí không thành công" });

            domain.New swapNew=null;
            //up
            if (moveUp)
            {
                swapNew = await _context.New.Where(c => c.Index > entity.Index && c.Status!=-1).OrderBy(c => c.Index).FirstOrDefaultAsync();
            }
            else
            {
                swapNew = await _context.New.Where(c => c.Index < entity.Index && c.Status != -1).OrderByDescending(c => c.Index).FirstOrDefaultAsync();
            }

            
            if(swapNew == null) return new JsonResult(new {
                status=false,
                message= "Đổi vị trí không thành công"
            });

            int currentIndex = entity.Index;
            entity.Index = swapNew.Index;
            swapNew.Index = currentIndex;
            _context.New.Update(entity);
            _context.New.Update(swapNew);
            if (await _context.SaveChangesAsync() >= 1) return new JsonResult(new { status = true });
            else return new JsonResult(new { status = false, message = "Đổi vị trí không thành công" });
        }
    }
}

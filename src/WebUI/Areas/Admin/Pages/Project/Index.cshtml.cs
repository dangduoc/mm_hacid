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
                                Index = c.Index,
                                CreatedOnDate = c.CreatedOnDate.ToString("dd/MM/yyyy"),
                                Investor = c.Investor,
                                Title = c.Title,
                                Year = c.Year,
                                Status = c.Status == 1
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

        public async Task<IActionResult> OnPostRowSwap(RowSwapModel model)
        {
            if (model.Ids == null) return new JsonResult(new { status = false });
            if (model.Ids.Count == 2)
            {
                bool isMoveUp = false;
                //if (model.Ids[0]< model.Ids[1]) { isMoveUp = true; }
                var row1 = await _context.Project.FindAsync(model.Ids[0]);
                var row2 = await _context.Project.FindAsync(model.Ids[1]);
                if ((row1 != null) && (row2 != null))
                {

                    isMoveUp = row1.Index < row2.Index;
                    if (isMoveUp)
                    {
                        var _index = row1.Index;

                        row1.Index = row2.Index;
                        row2.Index = row1.Index - 1;
                        _context.Project.Update(row1);
                        _context.Project.Update(row2);
                        await _context.SaveChangesAsync();



                        var entities = await _context.Project.Where(x => x.Status != -1
                              && x.Index <= row2.Index && x.Index > _index && x.Id != row1.Id && x.Id != row2.Id)
                         .OrderByDescending(x => x.Index)
                         .ToListAsync();

                        foreach (var item in entities)
                        {
                            item.Index = item.Index - 1;
                            _context.Project.Update(item);
                        }
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var _index = row1.Index;

                        row1.Index = row2.Index;
                        row2.Index = row1.Index + 1;
                        _context.Project.Update(row1);
                        await _context.SaveChangesAsync();

                        var entities = await _context.Project.Where(x => x.Status != -1
                             && x.Index >= row2.Index && x.Index<= _index && x.Id != row1.Id && x.Id != row2.Id)
                        .OrderByDescending(x => x.Index)
                        .ToListAsync();
                        foreach (var item in entities)
                        {
                            item.Index = item.Index + 1;
                            _context.Project.Update(item);
                        }
                        await _context.SaveChangesAsync();
                    }
                    return new JsonResult(new { status = true });
                }
            }
            return new JsonResult(new { status = false });
        }
        public async Task<IActionResult> OnPostInitIndexes()
        {
            var entities = await _context.Project.OrderBy(x=>x.Index).ToListAsync();
            if (entities.Count == 0) return new JsonResult(new { status = true });
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Index = i;
                _context.Project.Update(entities[i]);
            }
            //foreach (var item in entities)
            //{
            //    item.Index = model.Ids.IndexOf(item.Id);
            //    if (item.Index == -1)
            //    {
            //        item.Index = 0;
            //    }
            //    _context.HomeSlide.Update(item);
            //}
            await _context.SaveChangesAsync();
            return new JsonResult(new { status = true });

            //var entities = await _context.HomeSlide.ToListAsync();
            //if (entities.Count == 0) return new JsonResult(new { status = false });
            //foreach (var item in entities)
            //{
            //    item.Index = model.Ids.IndexOf(item.Id);
            //    if (item.Index == -1)
            //    {
            //        item.Index = 0;
            //    }
            //    _context.HomeSlide.Update(item);
            //}
            //await _context.SaveChangesAsync();

        }


    }
    public class RowSwapModel
    {
        public List<int> Ids { get; set; }
    }
}

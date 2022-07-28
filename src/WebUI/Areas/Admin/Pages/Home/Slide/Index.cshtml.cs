using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using BaseProjectWebRazor.Areas.Admin.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectWebRazor.Areas.Admin.Pages.Home.Slide
{
    public class IndexModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        public IndexModel(
            IApplicationDbContext context
            )
        {
            _context = context;
        }
        public List<HomeSlideItemResponse> Slides { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var list = await _context.HomeSlide.ToListAsync();
            Slides = list.Select(c => new HomeSlideItemResponse
            {
                Id = c.Id,
                Image = c.Image,
                Index = c.Index,
                Theme = c.Theme,
                Title = c.Title,
                Status=c.Status
            }).OrderByDescending(c => c.Index).ToList();
            return Page();
        }
        public async Task<IActionResult> OnDeleteAsync(int Id)
        {
            var entity = await _context.HomeSlide.FindAsync(Id);
            if (entity == null) return NotFound();
            _context.HomeSlide.Remove(entity);

            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new OkResult();
            }
            return BadRequest();
        }
        public async Task<IActionResult> OnPostHideAsync(int Id)
        {
            var entity = await _context.HomeSlide.FindAsync(Id);
            if (entity == null) return new BadRequestResult();
            if (entity.Status == -1) return new BadRequestResult();
            if (entity.Status == 1) entity.Status = 0;
            else entity.Status = 1;

            _context.HomeSlide.Update(entity);
            if (await _context.SaveChangesAsync(CancellationToken.None) >= 1)
            {
                return new OkResult();
            }
            return BadRequest();
        }
        //public async Task<IActionResult> OnPostChangeIndex(int Id, bool moveUp = true)
        //{
        //    var entity = await _context.HomeSlide.FindAsync(Id);
        //    if (entity == null) return new JsonResult(new { status = false, message = "Đổi vị trí không thành công" });

        //    HomeSlide swapNew = null;
        //    //up
        //    if (moveUp)
        //    {
        //        swapNew = await _context.HomeSlide.Where(c => c.Index > entity.Index).OrderBy(c => c.Index).FirstOrDefaultAsync();
        //    }
        //    else
        //    {
        //        swapNew = await _context.HomeSlide.Where(c => c.Index < entity.Index).OrderByDescending(c => c.Index).FirstOrDefaultAsync();
        //    }


        //    if (swapNew == null) return new JsonResult(new
        //    {
        //        status = false,
        //        message = "Đổi vị trí không thành công"
        //    });

        //    int currentIndex = entity.Index;
        //    entity.Index = swapNew.Index;
        //    swapNew.Index = currentIndex;
        //    _context.HomeSlide.Update(entity);
        //    _context.HomeSlide.Update(swapNew);
        //    if (await _context.SaveChangesAsync() >= 1) return new JsonResult(new { status = true });
        //    else return new JsonResult(new { status = false, message = "Đổi vị trí không thành công" });
        //}
        public async Task<IActionResult> OnPostChangeIndex(ChangeIndexDataModel model)
        {
            if (model.Ids == null) return new JsonResult(new { status = false });

            var entities = await _context.HomeSlide.ToListAsync();
            if (entities.Count == 0) return new JsonResult(new { status = false });
            foreach (var item in entities)
            {
                item.Index = model.Ids.IndexOf(item.Id);
                if (item.Index == -1)
                {
                    item.Index = 0;
                }
                _context.HomeSlide.Update(item);
            }
            await _context.SaveChangesAsync();
            return new JsonResult(new { status = true });
        }

        public async Task<IActionResult> OnPostRowSwap(ChangeIndexDataModel model)
        {
            if (model.Ids == null) return new JsonResult(new { status = false });
            if (model.Ids.Count == 2)
            {
                bool isMoveUp = false;
                //if (model.Ids[0]< model.Ids[1]) { isMoveUp = true; }
                var row1 = await _context.HomeSlide.FindAsync(model.Ids[0]);
                var row2 = await _context.HomeSlide.FindAsync(model.Ids[1]);
                if ((row1 != null) && (row2 != null))
                {

                    isMoveUp = row1.Index < row2.Index;
                    if (isMoveUp)
                    {
                        var _index = row1.Index;

                        row1.Index = row2.Index;
                        row2.Index = row1.Index - 1;
                        _context.HomeSlide.Update(row1);
                        _context.HomeSlide.Update(row2);
                        await _context.SaveChangesAsync();



                        var entities = await _context.HomeSlide.Where(x => x.Status != -1
                              && x.Index <= row2.Index && x.Index > _index && x.Id != row1.Id && x.Id != row2.Id)
                         .OrderByDescending(x => x.Index)
                         .ToListAsync();

                        foreach (var item in entities)
                        {
                            item.Index = item.Index - 1;
                            _context.HomeSlide.Update(item);
                        }
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var _index = row1.Index;

                        row1.Index = row2.Index;
                        row2.Index = row1.Index + 1;
                        _context.HomeSlide.Update(row1);
                        await _context.SaveChangesAsync();

                        var entities = await _context.HomeSlide.Where(x => x.Status != -1
                             && x.Index >= row2.Index && x.Index <= _index && x.Id != row1.Id && x.Id != row2.Id)
                        .OrderByDescending(x => x.Index)
                        .ToListAsync();
                        foreach (var item in entities)
                        {
                            item.Index = item.Index + 1;
                            _context.HomeSlide.Update(item);
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
            var entities = await _context.HomeSlide.OrderBy(x => x.Index).ToListAsync();
            if (entities.Count == 0) return new JsonResult(new { status = true });
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Index = i;
                _context.HomeSlide.Update(entities[i]);
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
    public class ChangeIndexDataModel
    {
        public List<int> Ids { get; set; }
    }
}

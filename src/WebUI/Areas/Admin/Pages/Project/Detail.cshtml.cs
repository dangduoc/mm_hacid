using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using domain = Domain.Entities;
using BaseProjectWebRazor.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace BaseProjectWebRazor.Areas.Admin.Pages.Project
{
    public class DetailModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public DetailModel(IApplicationDbContext context,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _context = context;
        }
        public MultiSelectList Categories { get; set; }
        public SelectList Locations { get; set; }
        public MultiSelectList ProjectFields { get; set; }
        [BindProperty]
        public ProjectDto MainModel { get; set; }
        public SelectList Themes { get; set; }
        public async Task<IActionResult> OnGetAsync(int Id)
        {
            var list = new List<object>();
            list.Add(new
            {
                Id = 0,
                Name = "Sáng"
            });
            list.Add(new
            {
                Id = 1,
                Name = "Tối"
            });

            Themes = new SelectList(list, "Id", "Name", 0);
            Categories = new MultiSelectList(await _context.ProjectCategory.ToListAsync(), "Id", "Name");
            Locations = new SelectList(await _context.Location.ToListAsync(), "Id", "Name");
            ProjectFields = new MultiSelectList(await _context.ProjectField.ToListAsync(), "Id", "Name");
            var entity = await _context.Project.Include(x => x.ProjectCategoryRelations).Include(x => x.ProjectFieldRelations).FirstOrDefaultAsync(x => x.Id == Id);
            if (entity == null) return NotFound();
            
            MainModel = _mapper.Map<ProjectDto>(entity);
            MainModel.Fields = entity.ProjectFieldRelations.Select(x => x.ProjectFieldId).ToList();
            MainModel.Categories = entity.ProjectCategoryRelations.Select(x => x.CategoryId).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {



                domain.Project entity=_mapper.Map<domain.Project>(MainModel);
                foreach (var item in _context.ProjectCategoryRelation.Where(x => x.ProjectId == MainModel.Id))
                {
                    _context.ProjectCategoryRelation.Remove(item);
                }
                foreach (var item in _context.ProjectFieldRelation.Where(x => x.ProjectId == MainModel.Id))
                {
                    _context.ProjectFieldRelation.Remove(item);
                }


                foreach (var item in MainModel.Categories)
                {
                    _context.ProjectCategoryRelation.Add(new domain.ProjectCategoryRelation
                    {
                        ProjectId = MainModel.Id,
                        CategoryId = Convert.ToInt32(item)
                    });
                }
                foreach (var item in MainModel.Fields)
                {
                    _context.ProjectFieldRelation.Add(new domain.ProjectFieldRelation
                    {
                        ProjectId = MainModel.Id,
                        ProjectFieldId = Convert.ToInt32(item)
                    });
                }


                _context.Project.Update(entity);


               

                if (await _context.SaveChangesAsync(CancellationToken.None)>=1)
                {
                    return Redirect("/admin/project");
                }
                ViewData["error"] = "Lưu thông tin dự án không thành công";
                return Page();
               
            }
            catch (Exception ex)
            {
                ViewData["error"] = "Đã xảy ra lỗi: " + ex.ToString();
                return Page();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ProjectCategoryRelation
    {
        public int ProjectId { get; set; }
        public int CategoryId { get; set; }
        public Project Project { get; set; }
        public ProjectCategory ProjectCategory { get; set; }
    }
}

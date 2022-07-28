using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ProjectFieldRelation
    {
        public int ProjectId { get; set; }
        public int ProjectFieldId { get; set; }

        public Project Project { get; set; }
        public ProjectField ProjectField { get;set;}
    }
}

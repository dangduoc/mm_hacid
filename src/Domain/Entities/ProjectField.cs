﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ProjectField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public List<ProjectFieldRelation> ProjectFieldRelations { get; set; }
    }
}

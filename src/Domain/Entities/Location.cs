using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Location
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class AboutProjectField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Summary { get; set; }
        public string SummaryEn { get; set; }
        public string Banner { get; set; }
        public int OrderIndex { get; set; }
    }
}

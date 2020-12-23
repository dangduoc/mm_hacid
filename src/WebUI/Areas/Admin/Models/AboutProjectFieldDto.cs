using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectWebRazor.Areas.Admin.Models
{
    public class AboutProjectFieldDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Summary { get; set; }
        public string SummaryEn { get; set; }
        public int OrderIndex { get; set; }
        public string Banner { get; set; }
    }
}

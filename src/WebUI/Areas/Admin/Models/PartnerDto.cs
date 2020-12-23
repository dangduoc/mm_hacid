using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectWebRazor.Areas.Admin.Models
{
    public class PartnerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public int CategoryId { get; set; }
        public string Thumbnail { get; set; }
        public string Link { get; set; }
        public int Index { get; set; }
    }
}

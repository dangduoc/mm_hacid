using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectWebRazor.Areas.Admin.Models
{
    public class PartnerListItemResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string Thumbnail { get; set; }
        public int Index { get; set; }
    }
}

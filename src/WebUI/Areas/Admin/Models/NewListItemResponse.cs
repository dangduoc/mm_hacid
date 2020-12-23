using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectWebRazor.Areas.Admin.Models
{
    public class NewListItemResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string CreatedOnDate { get; set; }
        public bool Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectWebRazor.Areas.Admin.Models
{
    public class ProjectListItemResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Investor { get; set; }
        public int Year { get; set; }
        public bool Status { get; set; }
        public string CreatedOnDate { get; set; }
    }
}

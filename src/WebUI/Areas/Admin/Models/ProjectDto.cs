using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectWebRazor.Areas.Admin.Models
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string Summary { get; set; }
        public string SummaryEn { get; set; }
        public string HtmlContent { get; set; }
        public string HtmlContentEn { get; set; }
        public string Banners { get; set; }
        public string Thumbnail { get; set; }
        public int LocationId { get; set; }
        public string Investor { get; set; }
        public string InvestorEn { get; set; }
        public int Year { get; set; }
        public int Index { get; set; }
        public bool IsEnglishIncluded { get; set; }
        public int Status { get; set; }
        public int Theme { get; set; }

        public List<int> Categories { get; set; }
        public List<int> Fields { get; set; }

        public string CreatedByUserId { get; set; }
  
        public DateTime CreatedOnDate { get; set; }

        public string LastModifiedByUserId { get; set; }

        public DateTime? LastModifiedOnDate { get; set; }
    }
}

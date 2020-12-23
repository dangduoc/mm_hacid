using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectWebRazor.Areas.Admin.Models
{
    public class NewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string HtmlContent { get; set; }
        public string HtmlContentEn { get; set; }
        public string Banners { get; set; }
        public string Thumbnail { get; set; }
        public int CategoryId { get; set; }
        public int TotalViews { get; set; }
        public bool IsEnglishIncluded { get; set; }
        public int Status { get; set; }
        public int Index { get; set; }
        public string CreatedByUserId { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public string LastModifiedByUserId { get; set; }

        public DateTime? LastModifiedOnDate { get; set; }
    }
}

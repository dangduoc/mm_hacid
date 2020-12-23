using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectWebRazor.Areas.Admin.Models
{
    public class AboutArticleDto
    {
        public int Id { get; set; }
        public string HtmlContent { get; set; }
        public string HtmlContentEn { get; set; }
        public int Type { get; set; }
    }
}

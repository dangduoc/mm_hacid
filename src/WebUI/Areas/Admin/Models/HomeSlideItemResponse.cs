using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectWebRazor.Areas.Admin.Models
{
    public class HomeSlideItemResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public int Theme { get; set; }
        public int Index { get; set; }
        public int Status { get; set; }
    }
    public class HomeSlideDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string Location { get; set; }
        public string LocationEn { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectTitleEn { get; set; }
        public int Index { get; set; }
        public string Link { get; set; }
        public int Theme { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }
    }
}

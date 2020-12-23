using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class AboutProject
    {
        public int Id { get; set; }
        public string TabTitle { get; set; }
        public string TabTitleEn { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string Summary { get; set; }
        public string SummaryEn { get; set; }
        public string Image { get; set; }
        public int Index { get; set; }
    }
}

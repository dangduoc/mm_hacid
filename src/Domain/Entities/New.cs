using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class New: AuditableEntity
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
        public bool? IsEnglishIncluded { get; set; }
        public int Index { get; set; }
        public NewCategory Category { get; set; }
        public int Status { get; set; }
    }
    

}

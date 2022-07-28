using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Project:AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        //public string Summary { get; set; }
        //public string SummaryEn { get; set; }
        public string HtmlContent { get; set; }
        public string HtmlContentEn { get; set; }
        public string Banners { get; set; }
        public string Thumbnail { get; set; }
        public int LocationId { get; set; }
        public string Investor { get; set; }
        public string InvestorEn { get; set; }
        public int Year { get; set; }
        public int Index { get; set; }
        public bool? IsEnglishIncluded { get; set; }
        public int Status { get; set; }
        public int Theme { get; set; }
        public Location Location { get; set; }
        public List<ProjectFieldRelation> ProjectFieldRelations { get; set; }
        public List<ProjectCategoryRelation> ProjectCategoryRelations { get; set; }
        
    }
}

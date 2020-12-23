using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class AboutArticle
    {
        public int Id { get; set; }
        public string HtmlContent { get; set; }
        public string HtmlContentEn { get; set; }
        public int Type { get; set; }
    }
    public enum AboutArticleTypeEnum
    {
        General=1,
        Specialist=2,
        Energy=3,
        Home=4
    }
}

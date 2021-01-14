using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectWebRazor.Models
{
    public class NewListItemModel
    {
        public int Id { get; set; }
        public int CateogryId { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public int TotalViews { get; set; }
        public string PublishedDate { get; set; }
        public string Thumbnail { get; set; }
    }
}

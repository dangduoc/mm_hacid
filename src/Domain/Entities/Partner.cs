using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Partner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public int CategoryId { get; set; }
        public string Thumbnail { get; set; }
        public string Link { get; set; }
        public int Index { get; set; }
        public PartnerCategory Category { get; set; }
    }
}

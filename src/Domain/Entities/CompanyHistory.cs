using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CompanyHistory
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
    }
}

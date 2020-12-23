using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectWebRazor.Areas.Admin.Models
{
    public class SiteSettingDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNameEn { get; set; }
        public string CompanyImage { get; set; }
        public string Address { get; set; }
        public string AddressEn { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
        public string Youtube { get; set; }
        public string Map { get; set; }
        public string MapEmbed { get; set; }
        public string ForumLink { get; set; }
        public string CopyRight { get; set; }

        public int Revit { get; set; }
        public int Bin { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectWebRazor.Models.ViewModel
{
    public class ViewNewListItemModel
    {
        public IEnumerable<NewListItemModel> Items { get; set; }
        public PaginationModel PageInfo { get; set; }
    }
}

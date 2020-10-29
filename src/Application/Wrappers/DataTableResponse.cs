using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Wrappers
{
    public class DataTableResponse<T>
    {
        public T Data { get; set; }

        public int RecordsTotal { get; set; }

        public int RecordsFiltered { get; set; }
        public int Draw { get; set; }
    }
}

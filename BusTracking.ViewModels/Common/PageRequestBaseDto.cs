using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Common
{
    public class PageRequestBaseDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}

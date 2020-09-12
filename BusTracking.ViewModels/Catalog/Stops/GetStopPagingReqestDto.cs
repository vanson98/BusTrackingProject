using BusTracking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Stops
{
    public class GetStopPagingReqestDto : PageRequestBaseDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int? Status { get; set; }
    }
}

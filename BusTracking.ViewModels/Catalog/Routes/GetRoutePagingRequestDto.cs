using BusTracking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Routes
{
    public class GetRoutePagingRequestDto : PageRequestBaseDto
    {
        public string RouteCode { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
}

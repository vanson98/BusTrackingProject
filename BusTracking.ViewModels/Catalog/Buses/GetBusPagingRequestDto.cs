using BusTracking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Buses
{
    public class GetBusPagingRequestDto : PageRequestBaseDto
    {
        public string LicenseCode { get; set; }
        public int? Status { get; set; }
        public int? DriverId { get; set; }
        public int? RouteId { get; set; }
    }
}

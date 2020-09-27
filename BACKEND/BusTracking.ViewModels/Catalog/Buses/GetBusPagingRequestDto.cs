using BusTracking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Buses
{
    // Tìm kiếm
    public class GetBusPagingRequestDto : PageRequestBaseDto
    {
        public string LicenseCode { get; set; }
        public int? Status { get; set; }
        public string DriverName { get; set; }
        public string RouteName { get; set; }
    }
}

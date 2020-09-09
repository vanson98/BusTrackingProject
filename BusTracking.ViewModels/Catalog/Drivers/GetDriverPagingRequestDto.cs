using BusTracking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Drivers
{
    public class GetDriverPagingRequestDto : PageRequestBaseDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int Status { get; set; }
    }
}

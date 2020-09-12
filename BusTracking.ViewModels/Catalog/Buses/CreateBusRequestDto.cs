using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Buses
{
    public class CreateBusRequestDto
    {
        public string LicenseCode { get; set; }
        public string Name { get; set; }
        public int MaxSize { get; set; }
        public decimal MaxSpeed { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int? DriverId { get; set; }
        public Guid? MonitorId { get; set; }
        public int? RouteId { get; set; }
    }
}

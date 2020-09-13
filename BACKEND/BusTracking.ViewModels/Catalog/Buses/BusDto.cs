using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Buses
{
    public class BusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicenseCode { get; set; }
        public int MaxSize { get; set; }
        public decimal MaxSpeed { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int? DriverId { get; set; }
        public string DriverName { get; set; }
        public Nullable<Guid> MonitorId { get; set; }
        public string MonitorName { get; set; }
        public int? RouteId { get; set; }
        public string RouteName { get; set; }
    }
}

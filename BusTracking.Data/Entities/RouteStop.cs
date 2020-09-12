using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.Entities
{
    public class RouteStop
    {
        public int RouteId { get; set; }
        public int StopId { get; set; }
        public int Order { get; set; }
        public Route Route { get; set; }
        public Stop Stop { get; set; }
    }
}

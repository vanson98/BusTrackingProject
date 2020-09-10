using BusTracking.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.Entities
{
    public class Route
    {
        public int Id {get;set;}
        public string RouteCode { get; set; }
        public string Name {get;set;}
        public decimal Distance {get;set;}
        public string Desctiption {get;set;}
        public TimeSpan TimePickUp { get; set; }
        public TimeSpan TimeDropOff { get; set; }
        public Status Status { get; set; }
        public Boolean IsDeleted { get; set; }
        public Bus Bus { get; set; }
        public List<RouteStop> RouteStops { get; set; }
    }
}

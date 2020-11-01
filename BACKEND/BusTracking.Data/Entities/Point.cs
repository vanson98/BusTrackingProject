using BusTracking.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.Entities
{
    public class Point
    {
        public int Id { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int? OriginalIndex { get; set; }
        public string PlaceId { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
    }
}

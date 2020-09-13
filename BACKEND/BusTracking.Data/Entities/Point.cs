using BusTracking.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.Entities
{
    public class Point
    {
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime Time { get; set; }
        public PointStatus Status { get; set; }
        public string Address { get; set; }

        public int BusId { get; set; }
    }
}

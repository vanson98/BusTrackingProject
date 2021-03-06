﻿using BusTracking.Data.Enum;
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
        public Status Status { get; set; }
        public Boolean IsDeleted { get; set; }
        public Bus Bus { get; set; }
        public List<Stop> Stops { get; set; }
        public List<Point> Points { get; set; }
    }
}

﻿using BusTracking.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.Entities
{
    public class Bus
    {
        public int Id { get; set; }
        public string LicenseCode { get; set; }
        public Status Status { get; set; }
        public Boolean IsDeleted { get; set; }
        public  int DriverId { get; set; }
        public int MonitorId { get; set; }
        public int RouteId { get; set; }
        public Driver Driver { get; set; }
        public Monitor Monitor { get; set; }
        public Route Route { get; set; }

        public List<Point> Points { get; set; }
    }
}
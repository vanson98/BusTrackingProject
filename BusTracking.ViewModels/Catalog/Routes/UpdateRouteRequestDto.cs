using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Routes
{
    public class UpdateRouteRequestDto
    {
        public int Id { get; set; }
        public string RouteCode { get; set; }
        public string Name { get; set; }
        public decimal Distance { get; set; }
        public int HourPickUp { get; set; }
        public int MinutePickUp { get; set; }
        public int HourDropOff { get; set; }
        public int MinuteDropOff { get; set; }
        public string Desctiption { get; set; }
        public int Status { get; set; }
    }
}

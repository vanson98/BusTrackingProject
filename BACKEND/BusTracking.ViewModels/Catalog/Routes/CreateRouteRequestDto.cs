using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Routes
{
    public class CreateRouteRequestDto
    {
        public string RouteCode { get; set; }
        public string Name { get; set; }
        public decimal Distance { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        
    }
}

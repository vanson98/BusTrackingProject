using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Stops
{
    public class StopDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int NumberOfStudents { get; set; }
        public TimeSpan TimePickUp { get; set; }
        public TimeSpan TimeDropOff { get; set; }
        public int Status { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int TypeStop { get; set; }
        public int? RouteId { get; set; }
        public string RouteName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Stops
{
    public class StopMapDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TimePickUp { get; set; }
        public string TimeDropOff { get; set; }
        public string Address { get; set; }
        public Coordinate Coordinate { get; set; }
        public int NumberOfStudents { get; set; }
    }
}

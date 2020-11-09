using BusTracking.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.Entities
{
    public class Stop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int NumberOfStudents { get; set; }
        public TimeSpan TimePickUp { get; set; }
        public TimeSpan TimeDropOff { get; set; }
        public Status Status { get; set; }
        public Boolean IsDeleted { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public TypeStop TypeStop { get; set; }
        public int? RouteId { get; set; }
        public Route Route { get; set; }
        public List<Student> Students { get; set; }
    }
}

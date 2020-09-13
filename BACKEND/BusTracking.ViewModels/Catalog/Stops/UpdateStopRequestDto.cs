using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Stops
{
    public class UpdateStopRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int NumberOfStudents { get; set; }
        public int Status { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}

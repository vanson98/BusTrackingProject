using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Students
{
    public class StudentDto
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public string BusName { get; set; }
        public Guid ParentId { get; set; }
        public string ParentName { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Status { get; set; }
    }
}

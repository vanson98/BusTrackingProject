using BusTracking.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public Guid ParentId { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Status Status { get; set; }
        public Boolean IsDeleted { get; set; }
        public Bus Bus { get; set; }
        public AppUser Parent { get; set; }
    }
}

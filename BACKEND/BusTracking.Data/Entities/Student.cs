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
        public int StopPickId { get; set; }
        public int StopDropId { get; set; }
        public Guid ParentId { get; set; }
        public Guid TeacherId { get; set; }
        public Boolean WarningCheckIn  {get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public StudentStatus Status { get; set; }
        public string ClassOfStudent { get; set; }
        public Boolean IsDeleted { get; set; }
        public Bus Bus { get; set; }
        public AppUser Parent { get; set; }
        public Stop Stop { get; set; }
        public List<StudentCheckIn> StudentCheckIns { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}

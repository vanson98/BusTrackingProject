using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Identity;
using BusTracking.Data.Enum;

namespace BusTracking.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public TypeAccount TypeAccount { get; set; }
        public Boolean IsDeleted { get; set; }
        public Status Status { get; set; }
        public List<Student> Students { get; set; }
        public List<StudentCheckIn> StudentCheckIns { get; set; }
        public Bus Bus { get; set; }
       
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Students
{
    public class TotalStudentStatus
    {
        public int TotalStudent { get; set; }
        public int Absent { get; set; }
        public int OnLeave { get; set; }
        public int OnBus { get; set; }
        public int AtSchool { get; set; }
        public int AtHome { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Students
{
    public class StudentCheckInDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Guid ParentId { get; set; }
        public Guid? MonitorId { get; set; }
        public string MonitorName { get; set; }
        public string StudentName { get; set; }
        public string BusName { get; set; }
        public string StopName { get; set; }
        public int CheckInType { get; set; }
        public DateTime CheckInTime { get; set; }
        public int CheckInResult { get; set; }
        
    }
}

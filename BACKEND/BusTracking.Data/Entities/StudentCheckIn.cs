using BusTracking.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BusTracking.Data.Entities
{
    public class StudentCheckIn
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Guid MonitorId { get; set; }
        public int StopId { get; set; }
        public CheckInType CheckInType { get; set; }
        public DateTime CheckInTime { get; set; }
        public CheckInResult CheckInResult { get; set; }
        public Boolean ParentConfirm { get; set; }
        public Student Student { get; set; }
        public AppUser Monitor { get; set; }
        public Stop Stop { get; set; }
    }
}

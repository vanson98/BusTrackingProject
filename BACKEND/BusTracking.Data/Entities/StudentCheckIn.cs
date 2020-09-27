using BusTracking.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.Entities
{
    public class StudentCheckIn
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int StopId { get; set; }
        public CheckInType CheckInType { get; set; }
        public DateTime CheckInTime { get; set; }
        public CheckInResult CheckInResult { get; set; }
        public Boolean ParentConfirm { get; set; }
    }
}

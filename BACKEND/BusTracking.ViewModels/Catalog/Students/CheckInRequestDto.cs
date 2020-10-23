using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Students
{
    public class CheckInRequestDto
    {
        public int StudentId { get; set; }
        public Guid? MonitorId { get; set; }
        public int? StopId { get; set; }
        public int? CheckInType { get; set; }
        public DateTime CheckInTime { get; set; }
        public int CheckInResult { get; set; }
    }
}

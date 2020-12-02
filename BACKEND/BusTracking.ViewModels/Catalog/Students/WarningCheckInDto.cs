using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Students
{
    public class WarningCheckInDto
    {
        public int Id { get; set; }
        public int TypeNotification { get; set; }
        public string Content { get; set; }
        public DateTime TimeSent { get; set; }
        public string MonitorId { get; set; }
        public string ParentId { get; set; }
        public string TeacherId { get; set; }
    }
}

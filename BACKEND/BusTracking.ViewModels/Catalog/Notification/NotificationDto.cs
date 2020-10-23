using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Notification
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int TypeNotification { get; set; }
        public int StudentId { get; set; }
        public Guid ParentId { get; set; }
        public Guid? MonitorId { get; set; }
        public string Content { get; set; }
        public DateTime TimeSent { get; set; }
    }
}

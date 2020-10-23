using BusTracking.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public TypeMessage Type { get; set; }
        public string Content { get; set; }
        public DateTime TimeSent { get; set; }
        public Student Student { get; set; }
    }
}

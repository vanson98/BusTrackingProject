using BusTracking.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.Entities
{
    class Notify
    {
        public TypeMessage Type { get; set; }
        public string Content { get; set; }
        public DateTime TimeSent { get; set; }
    }
}

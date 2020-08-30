using BusTracking.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.Entities
{
    public class Round
    {
        public int Id { get; set; }
        public DateTime TimeStart   { get; set; }
        public RoundStatus  Status { get; set; }
        public List<RouteRound> RouteRounds { get; set; }
    }
}

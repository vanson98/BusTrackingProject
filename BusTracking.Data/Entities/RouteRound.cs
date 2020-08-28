using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.Entities
{
    public class RouteRound
    {
        public int RouteId { get; set; }
        public int RoundId { get; set; }
        public Route Route { get; set; }
        public Round Round { get; set; }
    }
}

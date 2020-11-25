using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Students
{
    public class ChartModel
    {
        public TotalCheckInState totalCheckInState { get; set; }
        public List<CheckInChartModel> checkInChartModel { get; set; }
    }
}

using BusTracking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Students
{
    public class GetStudentCheckInRequestDto : PageRequestBaseDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string StudentName { get; set; }
        public int? CheckInResult { get; set; }
        public int? BusId { get; set; }
        public int? CheckInType { get; set; }
        
        

    }
}

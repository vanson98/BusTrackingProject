using BusTracking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Students
{
    public class GetStudentPagingRequestDto : PageRequestBaseDto
    {
        public string BusName { get; set; }
        public string ParentName { get; set; }
        public string StopName { get; set; }
        public string Name { get; set; }
    }
}

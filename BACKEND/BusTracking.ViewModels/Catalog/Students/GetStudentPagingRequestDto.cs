using BusTracking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Students
{
    public class GetStudentPagingRequestDto : PageRequestBaseDto
    {
        public int BusId { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int Status { get; set; }
    }
}

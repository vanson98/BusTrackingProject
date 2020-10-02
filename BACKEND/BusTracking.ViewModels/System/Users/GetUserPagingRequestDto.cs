using BusTracking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.System.Users
{
    public class GetUserPagingRequestDto : PageRequestBaseDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public int? TypeAccount { get; set; }
    }
}

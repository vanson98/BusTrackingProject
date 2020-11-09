using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.System.Auth
{
    public class ChangePasswordRequestDto
    {
        public string UserId { get; set; }
        public string OldPass { get; set; }
        public string NewPass { get; set; }
    }
}

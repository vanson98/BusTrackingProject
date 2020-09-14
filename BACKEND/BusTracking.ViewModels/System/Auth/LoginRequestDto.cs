using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.System.Users
{
    public class LoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.System.Users
{
    public class CreateUserRequestDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public string PhoneNumber { get; set; }
        public int TypeAccount { get; set; }
        public int Status { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

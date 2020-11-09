using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.System.Users
{
    public class UpdateAccountRequestDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}

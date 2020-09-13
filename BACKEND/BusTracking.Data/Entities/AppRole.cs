using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace BusTracking.Data.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
 
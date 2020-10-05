using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.System.Auth
{
    public class RoleDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }
    }
}

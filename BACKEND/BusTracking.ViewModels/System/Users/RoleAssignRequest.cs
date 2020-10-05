using BusTracking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.System.Users
{
    public class RoleAssignRequest
    {
        public Guid UserId { get; set; }
        public List<SelectedItem> Roles { get; set; } = new List<SelectedItem>();

    }
}

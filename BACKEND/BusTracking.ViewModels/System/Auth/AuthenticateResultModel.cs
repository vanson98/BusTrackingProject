using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.System.Auth
{
    public class AuthenticateResultModel
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public int TypeAccount { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
        public string AccessToken { get; set; }
    }
}

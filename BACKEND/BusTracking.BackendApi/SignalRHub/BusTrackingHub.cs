using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTracking.BackendApi.HubConfig
{
    public class BusTrackingHub : Hub
    {
        public Task SendNotifyToParent(string userId,Object data)
        {
            return Clients.User(userId).SendAsync("ReceiveNotify", data);
        }
    }
}

using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTracking.BackendApi.SignalRHub
{
    public class BusTrackingHub : Hub
    {
        public async Task SendLocationToGroup(string groupName, object data)
        {
            await Clients.Group(groupName).SendAsync("ReceiveLocation", data);
            //await Clients.User(userId).SendAsync("ReceiveLocation", data);
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("CheckAddedGroup", $"{Context.ConnectionId} has joined the group {groupName}");
        }
    }
}

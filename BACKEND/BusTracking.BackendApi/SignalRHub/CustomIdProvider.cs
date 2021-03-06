﻿using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace BusTracking.BackendApi.SignalRHub
{
    public class CustomIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            var connectionId = connection.User?.FindFirst("userId")?.Value;
            return connectionId;
        }
    }
}

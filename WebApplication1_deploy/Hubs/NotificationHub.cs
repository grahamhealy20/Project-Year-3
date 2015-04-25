using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebApplication1.Hubs
{
    public class NotificationHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void send(string userId, string message) {
            Clients.User(userId).pushNotification(message);
        }
    }
}
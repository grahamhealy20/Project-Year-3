using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace TrackingRESTService.Hubs
{
    public class NotificationHub : Hub
    {

        public void Hello()
        {
            Clients.All.hello();
        }

        public void send(string message) {
            Clients.All.sendNotification(message);
        }
      
        public override Task OnConnected()
        {
            string user_id = Context.QueryString["userID"];
            if(!ConnectionInfo.userConnections.ContainsKey(user_id)) {
               ConnectionInfo.userConnections.Add(user_id, Context.ConnectionId);
            }
         
            return base.OnConnected();
        
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            ConnectionInfo.userConnections.Remove(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
    }
}
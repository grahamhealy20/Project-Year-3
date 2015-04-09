using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingRESTService.Hubs
{
    public static class ConnectionInfo
    {
        public static Dictionary<string, string> userConnections = new Dictionary<string, string>();
    }
}
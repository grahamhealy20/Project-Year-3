using System;
using Microsoft.Owin;
using Owin;
using TrackingRESTService;
using Microsoft.AspNet.SignalR;

namespace TrackingRESTService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
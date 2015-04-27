using System;
using Microsoft.Owin;
using Owin;
using TrackingRESTService;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
namespace TrackingRESTService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            //app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();

        }
    }
}
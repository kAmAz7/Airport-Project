using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using ServerAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
[assembly: OwinStartup(typeof(StartUp))]


namespace ServerAPI
{
    public class StartUp
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.MapSignalR();
        }
    }
}
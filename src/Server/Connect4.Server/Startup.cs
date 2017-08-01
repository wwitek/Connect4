using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;
using Connect4.Server.IoC;

[assembly: OwinStartup(typeof(Connect4.Server.Startup))]

namespace Connect4.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.DependencyResolver = new NinjectDependencyResolver(NinjectIoC.Initialize());
            app.MapSignalR();
        }
    }
}
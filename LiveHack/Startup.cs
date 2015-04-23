using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Data.Entity;
using LiveHackDb.Models;
using LiveHackDb.Migrations;

[assembly: OwinStartup(typeof(LiveHack.Startup))]

namespace LiveHack
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            HttpConfiguration config = new HttpConfiguration();
            ConfigureOAuth(app);
            WebApiConfig.Register(config);
            app.UseWebApi(config);
            app.MapSignalR();
        }
    }
}

using LiveHack.Providers;
using LiveHackDb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace LiveHack
{
    public partial class Startup
    {
        public static string PublicClientId { get; private set; }
        static Startup()
        {
            PublicClientId = "livehack";
        }
        public void ConfigureOAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(90),
                Provider = new SimpleAuthorizationServerProvider(PublicClientId)
            };

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                Provider = new CookieAuthenticationProvider()
                {
                    OnValidateIdentity = test
                }
            });
            app.UseOAuthBearerTokens(OAuthServerOptions);
            
        }

        private Task test(CookieValidateIdentityContext arg)
        {
            System.Diagnostics.Debug.WriteLine("AUTH");
            return Task.FromResult<object>(null);
        }
    }
}
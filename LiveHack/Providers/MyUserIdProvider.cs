using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack.Providers
{
    public class MyUserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            return IdentityExtensions.GetUserId(request.User.Identity);
        }
    }
}
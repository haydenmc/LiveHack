using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHackDb.Models
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
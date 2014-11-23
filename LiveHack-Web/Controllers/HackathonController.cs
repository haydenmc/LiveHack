using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiveHack_Web.Controllers
{
	[Authorize]
    public class HackathonController : Controller
    {
        // GET: Hackathon
        public ActionResult Index()
        {
			var SubDomain = GetSubDomain(HttpContext.Request.Url.Host);
            return View();
        }

		private static string GetSubDomain(string host)
		{
			if (host.Split('.').Length > 1)
			{
				int index = host.IndexOf(".");
				return host.Substring(0, index);
			}

			return null;
		}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace LiveHack.Attributes
{
    public class NoCacheFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue()
            {
                NoCache = true,
                NoStore = true
            };
        }
    }
}
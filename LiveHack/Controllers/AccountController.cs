using LiveHack.Models;
using LiveHack.Models.BindingModels;
using LiveHack.Models.ViewModels;
using LiveHack.Providers;
using LiveHackDb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LiveHack.Controllers
{
    public class AccountController : ApiController
    {
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult Get()
        {
            using (var db = new ApplicationDbContext())
            {
                var userId = IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
                var user = db.Users.SingleOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return Unauthorized();
                }
                return Ok(user.ToViewModel());
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await UserManager.CreateAsync(new LiveHackDb.Models.User()
            {
                UserName = model.Email,
                Email = model.Email,
                DisplayName = model.DisplayName
            }, model.Password);
            IHttpActionResult errorResult = GetErrorResult(result);
            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}

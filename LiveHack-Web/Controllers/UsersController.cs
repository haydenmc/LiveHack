using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LiveHackDb;
using LiveHackDb.Models;
using LiveHack_Web.Models.Viewmodels;

namespace LiveHack_Web.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/Users
        [Route("")]
        public IQueryable<UserViewModel> GetUsers()
        {
			return db.Users.Select(x => new UserViewModel(x));
        }

        // GET: api/Users/5
        [Route("{id}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string id)
        {
			UserViewModel user = new UserViewModel(db.Users.Find(id));
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        public IHttpActionResult Post(UserBindingModel user)
        {
            var model = new User()
            {
                Hackathons = new List<Hackathon>(),
                Institution = new Institution(),
            };

            db.Users.Add(model);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}
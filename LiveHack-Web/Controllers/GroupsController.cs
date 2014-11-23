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
    [RoutePrefix("api/Groups")]
    public class GroupsController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/Groups
        [Route("")]
        public IEnumerable<GroupViewModel> GetGroups()
        {
            return db.Groups.ToList().Select(x => GroupViewModel.CreateGroupViewModel(x));
        }

        // GET: api/Groups/5
        [Route("{id}")]
        [ResponseType(typeof(Group))]
        public IHttpActionResult GetGroup(Guid id)
        {
            GroupViewModel group =  GroupViewModel.CreateGroupViewModel(db.Groups.Find(id));
            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroupExists(Guid id)
        {
            return db.Groups.Count(e => e.GroupId == id) > 0;
        }
    }
}
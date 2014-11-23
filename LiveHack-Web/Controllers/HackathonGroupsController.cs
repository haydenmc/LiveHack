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

namespace LiveHack_Web.Controllers
{
    public class HackathonGroupsController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/HackathonGroups
        public IQueryable<HackathonGroup> GetGroups()
        {
            return db.Groups.OfType<HackathonGroup>();
        }

        // GET: api/HackathonGroups/5
        [ResponseType(typeof(HackathonGroup))]
        public IHttpActionResult GetHackathonGroup(Guid id)
        {
            HackathonGroup hackathonGroup = db.Groups.OfType<HackathonGroup>().Where(x => x.GroupId == id).FirstOrDefault();
            if (hackathonGroup == null)
            {
                return NotFound();
            }

            return Ok(hackathonGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HackathonGroupExists(Guid id)
        {
            return db.Groups.Count(e => e.GroupId == id) > 0;
        }
    }
}
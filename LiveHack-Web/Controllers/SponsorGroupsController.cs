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
    public class SponsorGroupsController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/SponsorGroups
        public IQueryable<SponsorGroup> GetGroups()
        {
            return db.Groups.OfType<SponsorGroup>();
        }

        // GET: api/SponsorGroups/5
        [ResponseType(typeof(SponsorGroup))]
        public IHttpActionResult GetSponsorGroup(Guid id)
        {
            SponsorGroup sponsorGroup = db.Groups.OfType<SponsorGroup>().Where( x => x.GroupId == id).FirstOrDefault();
            if (sponsorGroup == null)
            {
                return NotFound();
            }

            return Ok(sponsorGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SponsorGroupExists(Guid id)
        {
            return db.Groups.Count(e => e.GroupId == id) > 0;
        }
    }
}
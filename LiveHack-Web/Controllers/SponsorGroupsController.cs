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
using LiveHack_Web;
using LiveHack_Web.Models.Viewmodels;

namespace LiveHack_Web.Controllers
{
    public class SponsorGroupsController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/SponsorGroups
        public IQueryable<SponsorGroupViewModel> GetGroups()
        {
            return db.Groups.OfType<SponsorGroup>().Select(x => new SponsorGroupViewModel(x));
        }

        // GET: api/SponsorGroups/5
        [Route("{id}")]
        [ResponseType(typeof(SponsorGroup))]
        public IHttpActionResult GetSponsorGroup(Guid id)
        {
            SponsorGroupViewModel stest = new SponsorGroupViewModel(db.Groups.OfType<SponsorGroup>().Where(x => x.GroupId == id).FirstOrDefault());
            if (stest == null)
            {
                return NotFound();
            }

            return Ok(stest);
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
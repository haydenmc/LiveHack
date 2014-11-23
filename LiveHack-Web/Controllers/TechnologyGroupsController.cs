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
    public class TechnologyGroupsController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/TechnologyGroups
        public IQueryable<TechnologyGroup> GetGroups()
        {
            return db.Groups.OfType<TechnologyGroup>();
        }

        // GET: api/TechnologyGroups/5
        [ResponseType(typeof(TechnologyGroup))]
        public IHttpActionResult GetTechnologyGroup(Guid id)
        {
            TechnologyGroup technologyGroup = db.Groups.OfType<TechnologyGroup>().Where(x => x.GroupId == id).FirstOrDefault();
            if (technologyGroup == null)
            {
                return NotFound();
            }

            return Ok(technologyGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TechnologyGroupExists(Guid id)
        {
            return db.Groups.Count(e => e.GroupId == id) > 0;
        }
    }
}
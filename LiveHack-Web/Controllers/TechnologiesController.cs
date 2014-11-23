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
    public class TechnologiesController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/Technologies
        public IQueryable<Technology> GetTechnologies()
        {
            return db.Technologies;
        }

        // GET: api/Technologies/5
        [ResponseType(typeof(Technology))]
        public IHttpActionResult GetTechnology(Guid id)
        {
            Technology technology = db.Technologies.Find(id);
            if (technology == null)
            {
                return NotFound();
            }

            return Ok(technology);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TechnologyExists(Guid id)
        {
            return db.Technologies.Count(e => e.TechnologyId == id) > 0;
        }
    }
}
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
    [RoutePrefix("api/Technologies")]
    public class TechnologiesController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/Technologies
        [Route("")]
        public IQueryable<TechnologyViewModel> GetTechnologies()
        {
            return db.Technologies.Select(x => new TechnologyViewModel(x));
        }

        // GET: api/Technologies/5
        [Route("{id}")]
        [ResponseType(typeof(Technology))]
        public IHttpActionResult GetTechnology(Guid id)
        {
            TechnologyViewModel technology = new TechnologyViewModel(db.Technologies.Find(id));
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
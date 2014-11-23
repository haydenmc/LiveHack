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
    public class HackathonsController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/Hackathons
        public IQueryable<Hackathon> GetHackathons()
        {
            return db.Hackathons;
        }

        // GET: api/Hackathons/5
        [ResponseType(typeof(Hackathon))]
        public IHttpActionResult GetHackathon(Guid id)
        {
            Hackathon hackathon = db.Hackathons.Find(id);
            if (hackathon == null)
            {
                return NotFound();
            }

            return Ok(hackathon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HackathonExists(Guid id)
        {
            return db.Hackathons.Count(e => e.HackathonId == id) > 0;
        }
    }
}
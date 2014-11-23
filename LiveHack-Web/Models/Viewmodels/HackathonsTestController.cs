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

namespace LiveHack_Web.Models.Viewmodels
{
    public class HackathonsTestController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/HackathonsTest
        public IQueryable<Hackathon> GetHackathons()
        {
            return db.Hackathons;
        }

        // GET: api/HackathonsTest/5
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

        // PUT: api/HackathonsTest/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHackathon(Guid id, Hackathon hackathon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hackathon.HackathonId)
            {
                return BadRequest();
            }

            db.Entry(hackathon).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HackathonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/HackathonsTest
        [ResponseType(typeof(Hackathon))]
        public IHttpActionResult PostHackathon(Hackathon hackathon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hackathons.Add(hackathon);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HackathonExists(hackathon.HackathonId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hackathon.HackathonId }, hackathon);
        }

        // DELETE: api/HackathonsTest/5
        [ResponseType(typeof(Hackathon))]
        public IHttpActionResult DeleteHackathon(Guid id)
        {
            Hackathon hackathon = db.Hackathons.Find(id);
            if (hackathon == null)
            {
                return NotFound();
            }

            db.Hackathons.Remove(hackathon);
            db.SaveChanges();

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
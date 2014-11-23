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
    public class InstitutionsController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/Institutions
        public IQueryable<Institution> GetInstitutions()
        {
            return db.Institutions;
        }

        // GET: api/Institutions/5
        [ResponseType(typeof(Institution))]
        public IHttpActionResult GetInstitution(Guid id)
        {
            Institution institution = db.Institutions.Find(id);
            if (institution == null)
            {
                return NotFound();
            }

            return Ok(institution);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InstitutionExists(Guid id)
        {
            return db.Institutions.Count(e => e.InstitutionId == id) > 0;
        }
    }
}
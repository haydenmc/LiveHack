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
    [RoutePrefix("api/Institutions")]
    public class InstitutionsController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/Institutions
        [Route("")]
        public IQueryable<InstitutionViewModel> GetInstitutions()
        {
			return db.Institutions.Select(x => new InstitutionViewModel(x));
        }

        // GET: api/Institutions/5
        [Route("{id}")]
        [ResponseType(typeof(Institution))]
        public IHttpActionResult GetInstitution(Guid id)
        {
			InstitutionViewModel institution = new InstitutionViewModel(db.Institutions.Find(id));
            if (institution == null)
            {
                return NotFound();
            }

            return Ok(institution);
        }

        public IHttpActionResult Post(InstitutionBindingModel institution)
        {
            var model = new Institution()
            {
                InstitutionId = Guid.NewGuid(),
                Name = institution.Name,
                ZipCode = institution.ZipCode,
                Users = new List<User>(),
                Group = new InstitutionGroup()
            };

            db.Institutions.Add(model);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return Ok();
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
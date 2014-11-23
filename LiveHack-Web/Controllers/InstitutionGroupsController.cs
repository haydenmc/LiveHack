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
    public class InstitutionGroupsController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/InstitutionGroups
        public IQueryable<InstitutionGroupViewModel> GetGroups()
        {
            return db.Groups.OfType<InstitutionGroup>().Select(x => new InstitutionGroupViewModel(x));
        }

        // GET: api/InstitutionGroups/5
        [Route("{id}")]
        [ResponseType(typeof(InstitutionGroup))]
        public IHttpActionResult GetInstitutionGroup(Guid id)
        {
            InstitutionGroupViewModel itest = new InstitutionGroupViewModel(db.Groups.OfType<InstitutionGroup>().Where(x => x.GroupId == id).FirstOrDefault());
            if (itest == null)
            {
                return NotFound();
            }

            return Ok(itest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InstitutionGroupExists(Guid id)
        {
            return db.Groups.Count(e => e.GroupId == id) > 0;
        }
    }
}
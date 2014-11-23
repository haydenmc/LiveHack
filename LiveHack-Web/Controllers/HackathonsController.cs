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
	[RoutePrefix("api/Hackathons")]
    public class HackathonsController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/Hackathons
		[Route("")]
        public IQueryable<HackathonViewModel> GetHackathons()
        {
            return db.Hackathons.Select(x => new HackathonViewModel(x));
        }

        // GET: api/Hackathons/5
		[Route("{id}")]
        [ResponseType(typeof(Hackathon))]
        public IHttpActionResult GetHackathon(Guid id)
        {
            HackathonViewModel hackathon = new HackathonViewModel(db.Hackathons.Find(id));
            if (hackathon == null)
            {
                return NotFound();
            }

            return Ok(hackathon);
        }

		//GET: api/Hackathon/5/SponsorGroups
		[Route("{id}/SponsorGroups")]
		public IQueryable<SponsorGroupViewModel> GetSponsorGroups(Guid id)
		{
			return db.Groups.OfType<SponsorGroup>().Where(g => g.Hackathon.HackathonId == id).Select(x => new SponsorGroupViewModel(x));
		}

		//POST: api/Hackathons
		[Route("")]
		public IHttpActionResult Post(HackathonBindingModel hackathon)
		{
			var model = new Hackathon() 
			{
 				HackathonId = Guid.NewGuid(),
				Name = hackathon.Name,
				ShortName = hackathon.ShortName,
				Description = hackathon.Description,
				StartDateTime = hackathon.StartDateTime,
				EndDateTime = hackathon.EndDateTime,
				Institution = db.Institutions.Find(hackathon.InstitutionId),
				Users = new List<User>(),
				Groups = new List<HackathonGroup>()
			};

			db.Hackathons.Add(model);

			try
			{
				db.SaveChanges();
			}
			catch(DbUpdateException)
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

        private bool HackathonExists(Guid id)
        {
            return db.Hackathons.Count(e => e.HackathonId == id) > 0;
        }
    }
}
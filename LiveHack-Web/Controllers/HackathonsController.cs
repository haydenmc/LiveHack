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
        public IQueryable<HackathonBindingModel> GetHackathons()
        {
            return db.Hackathons.Select(x => new HackathonBindingModel(x));
        }

        // GET: api/Hackathons/5
		[Route("{id}")]
        [ResponseType(typeof(Hackathon))]
        public IHttpActionResult GetHackathon(Guid id)
        {
            HackathonBindingModel hackathon = new HackathonBindingModel(db.Hackathons.Find(id));
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
		public IHttpActionResult Post()
		{
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
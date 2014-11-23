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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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

        //GET: api/Hackathon/5/TeamGroups
        [Route("{id}/TeamGroups")]
        public IQueryable<TeamGroupViewModel> GetTeamGroups(Guid id)
        {
            return db.Groups.OfType<TeamGroup>().Where(g => g.Hackathon.HackathonId == id).Select(x => new TeamGroupViewModel(x));
        }

        //GET: api/Hackathon/5/TechnologyGroups
        [Route("{id}/TeamGroups")]
        public IQueryable<TechnologyGroupViewModel> GetTechnologyGroups(Guid id)
        {
            return db.Groups.OfType<TechnologyGroup>().Where(g => g.Hackathon.HackathonId == id).Select(x => new TechnologyGroupViewModel(x));
        }

        //GET: api/Hackathon/5/MyGroups
        [Route("{id}/MyGroups")]
        public IQueryable<GroupViewModel> GetMyGroups(Guid id)
        {
            string currentUserId = User.Identity.GetUserId();
            return db.Groups.Where(g => g.Members.Where(u => u.Id == currentUserId).Count() > 0).Select(x => new GroupViewModel(x));
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

			model.Groups.Add(new HackathonGroup()
			{
				GroupId = Guid.NewGuid(),
				Members = new List<User>(),
				Guests = new List<User>(),
				Name = hackathon.Name,
				Description = hackathon.Description,
				Url = hackathon.Url,
				Messages = new List<Message>(),
				Hackathon = model
			});

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
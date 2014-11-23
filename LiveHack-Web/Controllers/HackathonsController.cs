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
        public IEnumerable<HackathonViewModel> GetHackathons()
        {
            return db.Hackathons.ToList().Select(x => HackathonViewModel.CreateHackathonViewModel(x));
        }

        // GET: api/Hackathons/5
		[Route("{id}")]
        [ResponseType(typeof(Hackathon))]
        public IHttpActionResult GetHackathon(String id)
        {
            HackathonViewModel hackathon = HackathonViewModel.CreateHackathonViewModel(db.Hackathons.Where(x => x.ShortName == id).FirstOrDefault());
            if (hackathon == null)
            {
                return NotFound();
            }

            return Ok(hackathon);
        }

		//GET: api/Hackathon/5/SponsorGroups
		[Route("{id}/SponsorGroups")]
		public IEnumerable<SponsorGroupViewModel> GetSponsorGroups(Guid id)
		{
			return db.Groups.OfType<SponsorGroup>().Where(g => g.Hackathon.HackathonId == id).ToList().Select(x => SponsorGroupViewModel.CreateSponsorGroupViewModel(x));
		}

        //GET: api/Hackathon/5/TeamGroups
        [Route("{id}/TeamGroups")]
        public IEnumerable<TeamGroupViewModel> GetTeamGroups(Guid id)
        {
			return db.Groups.OfType<TeamGroup>().Where(g => g.Hackathon.HackathonId == id).ToList().Select(x => TeamGroupViewModel.CreateTeamGroupViewModel(x));
        }

        //GET: api/Hackathon/5/TechnologyGroups
        [Route("{id}/TeamGroups")]
        public IEnumerable<TechnologyGroupViewModel> GetTechnologyGroups(Guid id)
        {
			return db.Groups.OfType<TechnologyGroup>().Where(g => g.Hackathon.HackathonId == id).ToList().Select(x => TechnologyGroupViewModel.CreateTechnologyGroupViewModel(x));
        }

        //GET: api/Hackathon/5/MyGroups
        [Route("{id}/MyGroups")]
        public IEnumerable<GroupViewModel> GetMyGroups(Guid id)
        {
            string currentUserId = User.Identity.GetUserId();
			return db.Groups.Where(g => g.Members.Where(u => u.Id == currentUserId).Count() > 0).ToList().Select(x => GroupViewModel.CreateGroupViewModel(x));
        }

		//Post: api/Hackathons/5/Join
		[Route("{id}/Join")]
		public IHttpActionResult PostJoin(String id)
		{
			string currentUserId = User.Identity.GetUserId();
			var user = db.Users.Where(x => x.Id == currentUserId).FirstOrDefault();

			if(user == null)
			{
				return Unauthorized();
			}

			if(db.Hackathons.Where(x => x.ShortName == id).FirstOrDefault().Groups.OfType<HackathonGroup>().FirstOrDefault().Members.Contains(user))
			{
				return Conflict();
			}
			db.Hackathons.Where(x => x.ShortName == id).FirstOrDefault().Groups.OfType<HackathonGroup>().FirstOrDefault().Members.Add(user);
			
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
				//Institution = db.Institutions.Find(hackathon.InstitutionId),
				Users = new List<User>(),
				Groups = new List<HackathonGroup>()
			};

			db.Hackathons.Add(model);

			var group = new HackathonGroup()
			{
				GroupId = Guid.NewGuid(),
				Members = new List<User>(),
				Guests = new List<User>(),
				Name = hackathon.Name,
				Description = hackathon.Description,
				Url = hackathon.Url,
				Messages = new List<Message>()	
			};
			db.Groups.Add(group);
			model.Groups.Add(group);
			
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
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
    public class TeamGroupsController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/TeamGroups
        public IQueryable<TeamGroupViewModel> GetGroups()
        {
            return db.Groups.OfType<TeamGroup>().Select(x => new TeamGroupViewModel(x));
        }

        // GET: api/TeamGroups/5
        [Route("{id}")]
        [ResponseType(typeof(TeamGroup))]
        public IHttpActionResult GetTeamGroup(Guid id)
        {
            TeamGroupViewModel ttest = new TeamGroupViewModel(db.Groups.OfType<TeamGroup>().Where(x => x.GroupId == id).FirstOrDefault());
            if (ttest == null)
            {
                return NotFound();
            }

            return Ok(ttest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamGroupExists(Guid id)
        {
            return db.Groups.Count(e => e.GroupId == id) > 0;
        }
    }
}
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
    public class TeamGroupsController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/TeamGroups
        public IQueryable<TeamGroup> GetGroups()
        {
            return db.Groups.OfType<TeamGroup>();
        }

        // GET: api/TeamGroups/5
        [ResponseType(typeof(TeamGroup))]
        public IHttpActionResult GetTeamGroup(Guid id)
        {
            TeamGroup teamGroup = db.Groups.OfType<TeamGroup>().Where(x => x.GroupId == id).FirstOrDefault();
            if (teamGroup == null)
            {
                return NotFound();
            }

            return Ok(teamGroup);
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
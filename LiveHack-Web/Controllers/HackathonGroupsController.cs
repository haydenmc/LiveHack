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
    public class HackathonGroupsController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/HackathonGroups
        public IEnumerable<HackathonGroupViewModel> GetGroups()
        {
			return db.Groups.OfType<HackathonGroup>().ToList().Select(x => HackathonGroupViewModel.CreateHackathonGroupViewModel(x));
        }

        // GET: api/HackathonGroups/5
        [Route("{id}")]
        [ResponseType(typeof(HackathonGroup))]
        public IHttpActionResult GetHackathonGroup(Guid id)
        {
            HackathonGroupViewModel htest = HackathonGroupViewModel.CreateHackathonGroupViewModel(db.Groups.OfType<HackathonGroup>().Where(x => x.GroupId == id).FirstOrDefault());
            if (htest == null)
            {
                return NotFound();
            }
            return Ok(htest);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HackathonGroupExists(Guid id)
        {
            return db.Groups.Count(e => e.GroupId == id) > 0;
        }
    }
}
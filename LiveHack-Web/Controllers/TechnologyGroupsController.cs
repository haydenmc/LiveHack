﻿using System;
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
    public class TechnologyGroupsController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/TechnologyGroups
        public IEnumerable<TechnologyGroupViewModel> GetGroups()
        {
			return db.Groups.OfType<TechnologyGroup>().ToList().Select(x => TechnologyGroupViewModel.CreateTechnologyGroupViewModel(x));
        }

        // GET: api/TechnologyGroups/5
        [Route("{id}")]
        [ResponseType(typeof(TechnologyGroup))]
        public IHttpActionResult GetTechnologyGroup(Guid id)
        {
            TechnologyGroupViewModel techtest = TechnologyGroupViewModel.CreateTechnologyGroupViewModel(db.Groups.OfType<TechnologyGroup>().Where(x => x.GroupId == id).FirstOrDefault());
            if (techtest == null)
            {
                return NotFound();
            }

            return Ok(techtest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TechnologyGroupExists(Guid id)
        {
            return db.Groups.Count(e => e.GroupId == id) > 0;
        }
    }
}
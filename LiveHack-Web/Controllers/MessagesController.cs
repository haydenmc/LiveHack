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
    public class MessagesController : ApiController
    {
        private LiveHackDbContext db = new LiveHackDbContext();

        // GET: api/Messages
        public IEnumerable<Message> GetMessages()
        {
            return db.Messages;
        }

        // GET: api/Messages/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult GetMessage(Guid id)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

       
                
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(Guid id)
        {
            return db.Messages.Count(e => e.MessageId == id) > 0;
        }
    }
}
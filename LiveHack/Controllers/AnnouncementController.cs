using LiveHackDb.Models;
using LiveHack.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LiveHack.Models.BindingModels;
using System.Threading.Tasks;

namespace LiveHack.Controllers
{
    [Authorize]
    [RoutePrefix("api/Announcement")]
    public class AnnouncementController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAnnouncements()
        {
            using (var db = new ApplicationDbContext())
            {
                var userId = IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
                var user = db.Users.SingleOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return Unauthorized();
                }

                var announcements = db.Announcements.Include("Author.ChatsOwned").OrderByDescending(a => a.DateTimeCreated).ToList().Select(a => a.ToViewModel());
                return Ok(announcements);
            }
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> PostAnnouncement(AnnouncementBindingModel inputAnnouncement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                var userId = IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
                var user = db.Users.SingleOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return Unauthorized();
                }
                if (!user.IsOrganizer)
                {
                    return Unauthorized();
                }
                var announcement = new Announcement()
                {
                    AnnouncementId = Guid.NewGuid(),
                    Author = user,
                    Title = inputAnnouncement.Title,
                    Body = inputAnnouncement.Body,
                    DateTimeCreated = DateTimeOffset.Now
                };
                db.Announcements.Add(announcement);
                await db.SaveChangesAsync();
                return Created("/api/Announcement/" + announcement.AnnouncementId, announcement.ToViewModel());
            }
        }
    }
}

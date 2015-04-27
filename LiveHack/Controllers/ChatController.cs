using LiveHackDb.Models;
using LiveHack.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LiveHack.Controllers
{
    [RoutePrefix("api/Chat")]
    [Authorize]
    public class ChatController : ApiController
    {
        [Route("Messages")]
        [HttpGet]
        public IHttpActionResult GetGlobalMessages()
        {
            using (var db = new ApplicationDbContext())
            {
                var userId = IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
                var user = db.Users.SingleOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return Unauthorized();
                }
                var messages = db.Messages.Include("Sender.ChatsOwned").Where(m => m.Chat == null).OrderByDescending(m => m.SentDateTime).Take(50).ToList();
                messages.Reverse();
                var results = messages.Select(m => m.ToViewModel());
                return Ok(results);
            }
        }

        [Route("{chatId:guid}/Messages")]
        [HttpGet]
        public IHttpActionResult GetMessages(Guid chatId)
        {
            using (var db = new ApplicationDbContext())
            {
                var userId = IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
                var user = db.Users.SingleOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return Unauthorized();
                }
                var chat = db.Chats.SingleOrDefault(c => c.Id == chatId);
                if (chat == null)
                {
                    return NotFound();
                }
                if (!chat.Owners.Contains(user) && !chat.Users.Contains(user))
                {
                    return Unauthorized();
                }
                var messages = db.Messages.Include("Sender.ChatsOwned").Where(m => m.Chat.Id == chatId).OrderByDescending(m => m.SentDateTime).Take(50).ToList();
                messages.Reverse();
                var results = messages.Select(m => m.ToViewModel());
                return Ok(results);
            }
        }
    }
}

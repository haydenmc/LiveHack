using LiveHack.Models.BindingModels;
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
using LiveHack.Hubs;

namespace LiveHack.Controllers
{
    [RoutePrefix("api/Team")]
    public class TeamController : ApiControllerWithHub<LiveHackHub>
    {
        [Authorize]
        [HttpGet]
        public IHttpActionResult Get()
        {
            using (var db = new ApplicationDbContext())
            {
                var userId = IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
                var user = db.Users.SingleOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return Unauthorized();
                }
                var team = user.ChatsOwned.OfType<Team>().FirstOrDefault();
                if (team == null)
                {
                    return NotFound();
                }
                return Ok(team.ToViewModel());
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> PostCreate(TeamBindingModel inputTeam)
        {
            using (var db = new ApplicationDbContext())
            {
                var userId = IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
                var user = db.Users.SingleOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return Unauthorized();
                }
                if (db.Chats.OfType<Team>().SingleOrDefault(t => t.Name.ToLower() == inputTeam.TeamName.ToLower()) != null)
                {
                    return BadRequest("A team with this name already exists.");
                }
                string randomAccessCode;
                do
                {
                    randomAccessCode = Utils.Random.GetRandomString(5);
                }
                while (db.Chats.OfType<Team>().SingleOrDefault(t => t.AccessCode == randomAccessCode) != null);

                var team = new Team()
                {
                    Id = Guid.NewGuid(),
                    Name = inputTeam.TeamName,
                    Description = "",
                    AccessCode = LiveHack.Utils.Random.GetRandomString(5),
                    DateTimeCreated = DateTimeOffset.Now,
                    Messages = new List<Message>(),
                    Users = new List<User>(),
                    Owners = new List<User>() { user }
                };
                db.Chats.Add(team);
                await db.SaveChangesAsync();
                return Created("Team/" + team.Id, team.ToViewModel());
            }
        }

        [Authorize]
        [HttpPost]
        [Route("{accessCode:length(5)}/Join")]
        public async Task<IHttpActionResult> PostJoin(string accessCode)
        {
            using (var db = new ApplicationDbContext())
            {
                var userId = IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
                var user = db.Users.SingleOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return Unauthorized();
                }
                var targetTeam = db.Chats.OfType<Team>().SingleOrDefault(r => r.AccessCode == accessCode);
                if (targetTeam == null)
                {
                    return NotFound();
                }
                targetTeam.Owners.Add(user);
                await db.SaveChangesAsync();
                return Ok(targetTeam.ToViewModel());
            }
        }
    }
}

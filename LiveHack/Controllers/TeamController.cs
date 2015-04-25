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

namespace LiveHack.Controllers
{
    public class TeamController : ApiController
    {
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
                if (db.Teams.SingleOrDefault(t => t.Name.ToLower() == inputTeam.TeamName.ToLower()) != null)
                {
                    return BadRequest("A team with this name already exists.");
                }
                string randomAccessCode;
                do
                {
                    randomAccessCode = Utils.Random.GetRandomString(5);
                }
                while (db.Teams.SingleOrDefault(t => t.AccessCode == randomAccessCode) != null);

                var team = new Team()
                {
                    Id = Guid.NewGuid(),
                    Name = inputTeam.TeamName,
                    Description = "",
                    AccessCode = LiveHack.Utils.Random.GetRandomString(5),
                    DateTimeCreated = DateTimeOffset.Now,
                    Messages = new List<Message>(),
                    Owners = new List<User>() { user }
                };
                db.Teams.Add(team);
                await db.SaveChangesAsync();
                return Created("Team/" + team.Id, team.ToViewModel());
            }
        }
    }
}

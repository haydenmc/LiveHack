using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using LiveHackDb.Models;
using LiveHack.Models;

namespace LiveHack.Hubs
{
    [Authorize]
    public class LiveHackHub : Hub
    {
        private ApplicationDbContext Db = new ApplicationDbContext();
        public override Task OnConnected()
        {
            var userId = IdentityExtensions.GetUserId(Context.User.Identity);
            var user = Db.Users.Where(u => u.Id == userId).FirstOrDefault();
            Groups.Add(Context.ConnectionId, userId);
            return base.OnConnected();
        }
        public void Hello()
        {
            Clients.All.hello();
        }

        public async Task SendMessage(MessageBindingModel messageBinding)
        {
            var userId = IdentityExtensions.GetUserId(Context.User.Identity);
            var user = Db.Users.Where(u => u.Id == userId).FirstOrDefault();
            var message = new Message()
            {
                MessageId = Guid.NewGuid(),
                Body = messageBinding.Body,
                Sender = user,
                SentDateTime = DateTimeOffset.Now
            };
            Db.Messages.Add(message);
            await Db.SaveChangesAsync();
            await Clients.All.MessageReceived(message);
        }
    }
}
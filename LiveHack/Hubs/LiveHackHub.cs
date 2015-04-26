using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using LiveHackDb.Models;
using LiveHack.Models;
using LiveHack.Models.ViewModels;
using LiveHack.Models.BindingModels;

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
            // Add user to group with their own ID
            Groups.Add(Context.ConnectionId, userId);
            // Add user to all of their chat groups
            foreach (var chat in user.Chats.Concat(user.ChatsOwned).Distinct())
            {
                Groups.Add(Context.ConnectionId, chat.Id.ToString());
            }
            return base.OnConnected();
        }

        public async Task SendMessage(MessageBindingModel messageBinding)
        {
            var userId = IdentityExtensions.GetUserId(Context.User.Identity);
            var user = Db.Users.SingleOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return; // should I throw an error here..?
            }

            Chat targetChat = null;
            if (messageBinding.ChatId != null) // null value = global message
            {
                var chat = Db.Chats.SingleOrDefault(c => c.Id == messageBinding.ChatId);
                if (chat == null)
                {
                    return;
                }
            }

            var message = new Message()
            {
                MessageId = Guid.NewGuid(),
                Chat = targetChat,
                Body = messageBinding.Body,
                Sender = user,
                SentDateTime = DateTimeOffset.Now
            };
            Db.Messages.Add(message);
            await Db.SaveChangesAsync();
            if (message.Chat == null)
            {
                await Clients.All.MessageReceived(message.ToViewModel());
            } else
            {
                await Clients.Group(message.Chat.Id.ToString()).MessageReceived(message.ToViewModel());
            }
            
        }
    }
}
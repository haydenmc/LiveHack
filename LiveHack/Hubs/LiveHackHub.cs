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
using System.Collections.Concurrent;

namespace LiveHack.Hubs
{
    [Authorize]
    public class LiveHackHub : Hub
    {
        private ApplicationDbContext Db = new ApplicationDbContext();
        public static readonly ConcurrentDictionary<string, List<string>> UserConnectionIds = new ConcurrentDictionary<string, List<string>>();

        public static void AddToGroup(User user, Guid groupId)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<LiveHackHub>();
            if (UserConnectionIds.ContainsKey(user.Id))
            {
                foreach (var connectionId in UserConnectionIds[user.Id])
                {
                    hub.Groups.Add(connectionId, groupId.ToString());
                }
            }
        }

        public static void SendNewChatOwner(Chat chat, User user)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<LiveHackHub>();
            hub.Clients.Group(chat.Id.ToString()).newChatOwner(chat.Id, user.ToViewModel());
        }

        public override Task OnConnected()
        {
            var userId = IdentityExtensions.GetUserId(Context.User.Identity);
            var user = Db.Users.Where(u => u.Id == userId).FirstOrDefault();
            // Map connection to the user ID
            if (!UserConnectionIds.ContainsKey(user.Id))
            {
                UserConnectionIds[user.Id] = new List<string>();
            }
            UserConnectionIds[user.Id].Add(Context.ConnectionId);
            // Add user to group with their own ID
            Groups.Add(Context.ConnectionId, userId);
            // Add user to all of their chat groups
            foreach (var chat in user.Chats.Concat(user.ChatsOwned).Distinct())
            {
                Groups.Add(Context.ConnectionId, chat.Id.ToString());
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userId = IdentityExtensions.GetUserId(Context.User.Identity);
            var user = Db.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (UserConnectionIds.ContainsKey(user.Id))
            {
                UserConnectionIds[user.Id].Remove(Context.ConnectionId);
            }
            return base.OnDisconnected(stopCalled);
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
                targetChat = Db.Chats.SingleOrDefault(c => c.Id == messageBinding.ChatId);
                if (targetChat == null)
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
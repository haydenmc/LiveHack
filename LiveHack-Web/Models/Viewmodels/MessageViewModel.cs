using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
    public class MessageViewModel
    {
        public static MessageViewModel CreateMessageViewModel(Message message)
        {
            MessageViewModel model = new MessageViewModel();
            model.MessageId = message.MessageId;
            model.Sender = UserViewModel.CreateUserViewModel(message.Sender);
            model.Group = GroupViewModel.CreateGroupViewModel(message.Group);
            model.Body = message.Body;
            model.SendTime = message.SendTime;
            return model;
        }
        
        public Guid MessageId { get; set; }
	    public UserViewModel Sender { get; set; }
	    public GroupViewModel Group { get; set; }
	    public string Body { get; set; }
	    public DateTimeOffset SendTime { get; set; }
    }
}
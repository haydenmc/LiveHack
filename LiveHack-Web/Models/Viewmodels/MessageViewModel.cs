﻿using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
    public class MessageViewModel
    {
        public MessageViewModel(Message message)
        {
            this.MessageId = message.MessageId;
            this.Sender = message.Sender;
            this.Group = message.Group;
            this.Body = message.Body;
            this.SendTime = message.SendTime;
        }
        
        public Guid MessageId { get; set; }
	    public User Sender { get; set; }
	    public Group Group { get; set; }
	    public string Body { get; set; }
	    public DateTimeOffset SendTime { get; set; }
    }
}
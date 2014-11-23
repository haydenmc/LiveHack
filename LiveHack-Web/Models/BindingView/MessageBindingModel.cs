using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
    public class MessageBindingModel
    {
        
      //  public Guid MessageId { get; set; }
	    public UserBindingModel Sender { get; set; }
	    public GroupViewModel Group { get; set; }
	    public string Body { get; set; }
	    public DateTimeOffset SendTime { get; set; }
    }
}
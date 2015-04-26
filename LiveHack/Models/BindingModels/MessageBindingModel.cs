using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack.Models.BindingModels
{
    public class MessageBindingModel
    {
        public Guid? ChatId { get; set; }
        public string Body { get; set; }
    }
}
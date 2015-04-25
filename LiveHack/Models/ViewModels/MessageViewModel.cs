using LiveHackDb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack.Models.ViewModels
{
    public class MessageViewModel
    {
        [JsonProperty("messageId")]
        public Guid MessageId { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("sender")]
        public UserViewModel Sender { get; set; }

        [JsonProperty("sentDateTime")]
        public DateTimeOffset SentDateTime { get; set; }

        public static MessageViewModel Convert(Message message)
        {
            return new MessageViewModel()
            {
                MessageId = message.MessageId,
                Body = message.Body,
                Sender = message.Sender.ToViewModel(),
                SentDateTime = message.SentDateTime
            };
        }
    }
}
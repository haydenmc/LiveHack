﻿using LiveHackDb.Models;
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

        [JsonProperty("chatId")]
        public Guid? ChatId { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("sender")]
        public UserViewModel Sender { get; set; }

        [JsonProperty("sentDateTime")]
        public DateTimeOffset SentDateTime { get; set; }
    }

    public static partial class ViewModelExtensions
    {
        public static MessageViewModel ToViewModel(this Message message)
        {
            return new MessageViewModel()
            {
                MessageId = message.MessageId,
                ChatId = message.Chat == null ? null : (Guid?)message.Chat.Id,
                Body = message.Body,
                Sender = message.Sender.ToViewModel(),
                SentDateTime = message.SentDateTime
            };
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack.Models.ViewModels
{
    public class ChatViewModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("users")]
        public virtual ICollection<UserViewModel> Owners { get; set; }

        [JsonProperty("dateTimeCreated")]
        public DateTimeOffset DateTimeCreated { get; set; }
    }
}
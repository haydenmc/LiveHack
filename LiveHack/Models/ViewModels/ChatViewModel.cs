using LiveHackDb.Models;
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

        [JsonProperty("owners")]
        public virtual ICollection<UserViewModel> Owners { get; set; }

        [JsonProperty("users")]
        public virtual ICollection<UserViewModel> Users { get; set; }

        [JsonProperty("dateTimeCreated")]
        public DateTimeOffset DateTimeCreated { get; set; }
    }

    public static partial class ViewModelExtensions
    {
        public static ChatViewModel ToViewModel(this Chat chat)
        {
            return new ChatViewModel()
            {
                Id = chat.Id,
                Name = chat.Name,
                Description = chat.Description,
                Owners = chat.Owners.ToList().Select(o => o.ToViewModel()).ToList(),
                Users = chat.Users.ToList().Select(u => u.ToViewModel()).ToList(),
                DateTimeCreated = chat.DateTimeCreated
            };
        }
    }
}
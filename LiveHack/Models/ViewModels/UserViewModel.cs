using LiveHackDb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack.Models.ViewModels
{
    public class UserViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        
    }

    public static partial class ViewModelExtensions
    {
        public static UserViewModel ToViewModel(this User user)
        {
            return new UserViewModel()
            {
                Id = user.Id,
                DisplayName = user.DisplayName
            };
        }
    }
}
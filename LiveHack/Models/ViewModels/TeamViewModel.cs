using LiveHackDb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack.Models.ViewModels
{
    public class TeamViewModel : ChatViewModel
    {
        [JsonProperty("accessCode")]
        public string AccessCode { get; set; }
    }

    public static partial class ViewModelExtensions
    {
        public static TeamViewModel ToViewModel(this Team team)
        {
            return new TeamViewModel()
            {
                Id = team.Id,
                Name = team.Name,
                Description = team.Description,
                AccessCode = team.AccessCode,
                Owners = team.Owners.ToList().Select(o => o.ToViewModel()).ToList(),
                Users = team.Users.ToList().Select(u => u.ToViewModel()).ToList(),
                DateTimeCreated = team.DateTimeCreated
            };
        }
    }
}
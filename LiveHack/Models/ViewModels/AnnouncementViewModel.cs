using LiveHackDb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack.Models.ViewModels
{
    public class AnnouncementViewModel
    {
        [JsonProperty("announcementId")]
        public Guid AnnouncementId { get; set; }

        [JsonProperty("author")]
        public UserViewModel Author { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("dateTimeCreated")]
        public DateTimeOffset DateTimeCreated { get; set; }
    }

    public static partial class ViewModelExtensions
    {
        public static AnnouncementViewModel ToViewModel(this Announcement announcement)
        {
            return new AnnouncementViewModel()
            {
                AnnouncementId = announcement.AnnouncementId,
                Author = announcement.Author.ToViewModel(),
                Title = announcement.Title,
                Body = announcement.Body,
                DateTimeCreated = announcement.DateTimeCreated
            };
        }
    }
}
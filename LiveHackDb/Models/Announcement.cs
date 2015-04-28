using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveHackDb.Models
{
    public class Announcement
    {
        public Guid AnnouncementId { get; set; }
        public User Author { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTimeOffset DateTimeCreated { get; set; }
    }
}

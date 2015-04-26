using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveHackDb.Models
{
    public abstract class Chat
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<User> Owners { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public DateTimeOffset DateTimeCreated { get; set; }
    }
}

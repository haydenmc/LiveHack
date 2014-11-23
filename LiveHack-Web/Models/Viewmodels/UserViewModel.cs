using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            this.Id          = user.Id;
            this.Email       = user.Email;
            //this.Hackathon   = user.Hackathons.Select(x => new HackathonViewModel(x));
            //this.Institution = new InstitutionViewModel(user.Institution);

        }

        //public virtual ICollection<Hackathon> Hackathons { get; set; }
        //public virtual Institution Institution { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
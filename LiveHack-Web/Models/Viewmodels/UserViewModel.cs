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
            this.Hackathons   = user.Hackathons.Select(x => new HackathonViewModel(x)).ToList();
            this.Institution = new InstitutionViewModel(user.Institution);

        }

        public ICollection<HackathonViewModel> Hackathons { get; set; }
        public InstitutionViewModel Institution { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
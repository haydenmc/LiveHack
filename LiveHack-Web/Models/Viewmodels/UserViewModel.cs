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
            this.Id				= user.Id;
            this.Email			= user.Email;
			this.HackathonIds	= user.Hackathons.Select(x => x.HackathonId).ToList();
			this.InstitutionId	= user.Institution.InstitutionId;
            //this.Hackathons   = user.Hackathons.Select(x => new HackathonViewModel(x)).ToList();
            //this.Institution	= new InstitutionViewModel(user.Institution);

        }
		public ICollection<Guid> HackathonIds { get; set; }
		public Guid InstitutionId { get; set; }
        //public ICollection<HackathonViewModel> Hackathons { get; set; }
        //public InstitutionViewModel Institution { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
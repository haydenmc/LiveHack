using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
    public class UserBindingModel
    {

		//public ICollection<Guid> HackathonIds { get; set; }
		//public Guid InstitutionId { get; set; }
        //public ICollection<HackathonViewModel> Hackathons { get; set; }
        //public InstitutionViewModel Institution { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
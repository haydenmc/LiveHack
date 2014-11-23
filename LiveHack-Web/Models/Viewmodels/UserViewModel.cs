using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
    public class UserViewModel
    {
        public static UserViewModel CreateUserViewModel(User user)
        {
            UserViewModel model = new UserViewModel();
            model.Id				= user.Id;
            model.Email			= user.Email;
			model.HackathonIds	= user.Hackathons.Select(x => x.HackathonId).ToList();
			model.InstitutionId	= user.Institution.InstitutionId;
            //model.Hackathons   = user.Hackathons.Select(x => new HackathonViewModel(x)).ToList();
            //model.Institution	= new InstitutionViewModel(user.Institution);
            return model;
        }
		public ICollection<Guid> HackathonIds { get; set; }
		public Guid InstitutionId { get; set; }
        //public ICollection<HackathonViewModel> Hackathons { get; set; }
        //public InstitutionViewModel Institution { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
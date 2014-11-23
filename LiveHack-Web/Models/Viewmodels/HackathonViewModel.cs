using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class HackathonViewModel
	{
		public static HackathonViewModel CreateHackathonViewModel(Hackathon hack)
		{
			HackathonViewModel model = new HackathonViewModel();
			model.HackathonId = hack.HackathonId;
			model.Name = hack.Name;
			model.ShortName = hack.ShortName;
			model.Description = hack.Description;
			model.StartDateTime = hack.StartDateTime;
			model.EndDateTime = hack.EndDateTime;
			model.InstitutionId = hack.Institution == null ? Guid.Empty : hack.Institution.InstitutionId;
			model.UserIds = hack.Users.Select(x => new Guid(x.Id)).ToList();
			model.GroupIds = hack.Groups.Select(x => x.GroupId).ToList();
			//this.Institution	= new InstitutionViewModel(hack.Institution);
			//this.Users			= hack.Users.Select(x => new UserViewModel(x)).ToList();
			//this.Groups			= hack.Groups.Select(x => new HackathonGroupViewModel(x)).ToList();

			return model;
		}

		public Guid HackathonId { get; set; }
		public String Name { get; set; }
		public String ShortName { get; set; }
		public String Description { get; set; }
		public DateTimeOffset StartDateTime { get; set; }
		public DateTimeOffset EndDateTime { get; set; }
		public Guid InstitutionId { get; set; }
		public ICollection<Guid> UserIds { get; set; }
		public ICollection<Guid> GroupIds { get; set; }
		//public InstitutionViewModel Institution { get; set; }
		//public ICollection<UserViewModel> Users { get; set; }
		//public ICollection<HackathonGroupViewModel> Groups { get; set; }
	}
}
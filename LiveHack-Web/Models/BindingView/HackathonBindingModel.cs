using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class HackathonBindingModel
	{
		//public Guid HackathonId { get; set; }
		public String Name { get; set; }
		public String ShortName { get; set; }
		public String Description { get; set; }
		public DateTimeOffset StartDateTime { get; set; }
		public DateTimeOffset EndDateTime { get; set; }
		public Guid InstitutionId { get; set; }
		//public ICollection<Guid> UserIds { get; set; }
		//public ICollection<Guid> GroupIds { get; set; }
		//public InstitutionViewModel Institution { get; set; }
		//public ICollection<UserViewModel> Users { get; set; }
		//public ICollection<HackathonGroupViewModel> Groups { get; set; }

		public string Url { get; set; }
	}
}
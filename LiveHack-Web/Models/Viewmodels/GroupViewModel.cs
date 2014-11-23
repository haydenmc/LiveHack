using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class GroupViewModel
	{
		public GroupViewModel(Group group)
		{
			this.GroupId		= group.GroupId;
			this.Members		= group.Members.Select(x => new UserBindingModel(x)).ToList();
			this.Guests			= group.Guests.Select(x => new UserBindingModel(x)).ToList();
			this.Name			= group.Name;
			this.Description	= group.Description;
			this.Url			= group.Url;
			this.Messages		= group.Messages.Select(x => new MessageBindingModel(x)).ToList();
		}

		public Guid GroupId { get; set; }
		public ICollection<UserBindingModel> Members { get; set; }
		public ICollection<UserBindingModel> Guests { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Url { get; set; }
		public ICollection<MessageBindingModel> Messages { get; set; }
	}
}
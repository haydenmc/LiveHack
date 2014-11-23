using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class HackathonGroupViewModel : GroupViewModel
	{
		public static HackathonGroupViewModel CreateHackathonGroupViewModel(HackathonGroup group)
		{
			HackathonGroupViewModel model = new HackathonGroupViewModel();
			model.GroupId = group.GroupId;
			model.Members = group.Members.Select(x => UserViewModel.CreateUserViewModel(x)).ToList();
			model.Guests = group.Guests.Select(x => UserViewModel.CreateUserViewModel(x)).ToList();
			model.Name = group.Name;
			model.Description = group.Description;
			model.Url = group.Url;
			model.Messages = group.Messages.Select(x => MessageViewModel.CreateMessageViewModel(x)).ToList();
			model.Hackathon = HackathonViewModel.CreateHackathonViewModel(group.Hackathon);
			return model;
        }

		public HackathonViewModel Hackathon { get; set; }
	}
}
using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class TechnologyGroupViewModel : GroupViewModel
	{
        
		public static TechnologyGroupViewModel CreateTechnologyGroupViewModel(TechnologyGroup group)
		{
			TechnologyGroupViewModel model = new TechnologyGroupViewModel();
			model.GroupId = group.GroupId;
			model.Members = group.Members.Select(x => UserViewModel.CreateUserViewModel(x)).ToList();
			model.Guests = group.Guests.Select(x => UserViewModel.CreateUserViewModel(x)).ToList();
			model.Name = group.Name;
			model.Description = group.Description;
			model.Url = group.Url;
			model.Messages = group.Messages.Select(x => MessageViewModel.CreateMessageViewModel(x)).ToList();
			model.Technology = TechnologyViewModel.CreateTechnologyViewModel(group.Technology);
			return model;
		}
		public TechnologyViewModel Technology { get; set; }
	}
}
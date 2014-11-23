using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class TeamGroupViewModel : GroupViewModel
	{
		public static TeamGroupViewModel CreateTeamGroupViewModel(TeamGroup group)
		{
			TeamGroupViewModel model = new TeamGroupViewModel();
			model.GroupId = group.GroupId;
			model.Members = group.Members.Select(x => UserViewModel.CreateUserViewModel(x)).ToList();
			model.Guests = group.Guests.Select(x => UserViewModel.CreateUserViewModel(x)).ToList();
			model.Name = group.Name;
			model.Description = group.Description;
			model.Url = group.Url;
			model.Messages = group.Messages.Select(x => MessageViewModel.CreateMessageViewModel(x)).ToList();
			model.TechnologiesUsing = group.TechnologiesUsing.Select(x => TechnologyViewModel.CreateTechnologyViewModel(x)).ToList();
			return model;
		}
		public ICollection<TechnologyViewModel> TechnologiesUsing { get; set; }
	}
}
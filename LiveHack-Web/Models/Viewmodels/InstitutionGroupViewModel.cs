using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class InstitutionGroupViewModel : GroupViewModel
	{
		public static InstitutionGroupViewModel CreateInstitutionGroupViewModel(InstitutionGroup group)
		{
			InstitutionGroupViewModel model = new InstitutionGroupViewModel();
			model.GroupId = group.GroupId;
			model.Members = group.Members.Select(x => UserViewModel.CreateUserViewModel(x)).ToList();
			model.Guests = group.Guests.Select(x => UserViewModel.CreateUserViewModel(x)).ToList();
			model.Name = group.Name;
			model.Description = group.Description;
			model.Url = group.Url;
			model.Messages = group.Messages.Select(x => MessageViewModel.CreateMessageViewModel(x)).ToList();
			model.Institution = InstitutionViewModel.CreateInstitutionViewModel(group.Institution);
			return model;
        }
		public InstitutionViewModel Institution { get; set; }
	}
}
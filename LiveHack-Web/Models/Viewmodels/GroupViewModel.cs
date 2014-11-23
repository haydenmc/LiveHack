using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class GroupViewModel
	{
		public static GroupViewModel CreateGroupViewModel(Group group)
		{
			//GroupViewModel model = new GroupViewModel();
			//model.GroupId		= group.GroupId;
			//model.Members		= group.Members.Select(x => UserViewModel.CreateUserViewModel(x)).ToList();
			//model.Guests		= group.Guests.Select(x => UserViewModel.CreateUserViewModel(x)).ToList();
			//model.Name			= group.Name;
			//model.Description	= group.Description;
			//model.Url			= group.Url;
			//model.Messages		= group.Messages.Select(x => MessageViewModel.CreateMessageViewModel(x)).ToList();
			//return model;

			GroupViewModel model = null;

			if (group is HackathonGroup)
			{
				model = HackathonGroupViewModel.CreateHackathonGroupViewModel((HackathonGroup)group);
				model.Type = typeof(HackathonGroup).ToString();
			}
			else if (group is InstitutionGroup)
			{
				model = InstitutionGroupViewModel.CreateInstitutionGroupViewModel((InstitutionGroup)group);
				model.Type = typeof(InstitutionGroup).ToString();
			}
			else if (group is SponsorGroup)
			{
				model = SponsorGroupViewModel.CreateSponsorGroupViewModel((SponsorGroup)group);
				model.Type = typeof(SponsorGroup).ToString();
			}
			else if (group is TeamGroup)
			{
				model = TeamGroupViewModel.CreateTeamGroupViewModel((TeamGroup)group);
				model.Type = typeof(TeamGroup).ToString();
			}
			else if (group is TechnologyGroup)
			{
				model = TechnologyGroupViewModel.CreateTechnologyGroupViewModel((TechnologyGroup)group);
				model.Type = typeof(TechnologyGroup).ToString();
			}
			return model;
		}

		public Guid GroupId { get; set; }
		public ICollection<UserViewModel> Members { get; set; }
		public ICollection<UserViewModel> Guests { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Url { get; set; }
		public ICollection<MessageViewModel> Messages { get; set; }
		public string Type { get; set; }
	}
}
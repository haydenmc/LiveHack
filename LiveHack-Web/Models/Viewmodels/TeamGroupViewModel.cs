using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class TeamGroupViewModel : GroupViewModel
	{
		public static TeamGroupViewModel CreateTeamGroupViewModel(TeamGroup group) : base(group)
		{
            TeamGroupViewModel model = new TeamGroupViewModel();
			model.TechnologiesUsing = group.TechnologiesUsing.Select(x => TechnologyViewModel.CreateTechnologyViewModel(x)).ToList();
            return model;
		}
		public ICollection<TechnologyViewModel> TechnologiesUsing { get; set; }
	}
}
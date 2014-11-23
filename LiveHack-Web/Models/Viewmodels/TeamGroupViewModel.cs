using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class TeamGroupViewModel : GroupViewModel
	{
		public TeamGroupViewModel(TeamGroup group) : base(group)
		{
			this.TechnologiesUsing = group.TechnologiesUsing.Select(x => new TechnologyBindingModel(x)).ToList();
		}
		public ICollection<TechnologyBindingModel> TechnologiesUsing { get; set; }
	}
}
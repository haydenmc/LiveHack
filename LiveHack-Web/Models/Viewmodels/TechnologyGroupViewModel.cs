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
            GroupViewModel model = GroupViewModel.CreateGroupViewModel(group);
            ((TechnologyGroupViewModel)model).Technology = TechnologyViewModel.CreateTechnologyViewModel(group.Technology);
            return (TechnologyGroupViewModel)model;
		}
		public TechnologyViewModel Technology { get; set; }
	}
}
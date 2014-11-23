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
            HackathonGroupViewModel model = HackathonGroupViewModel.CreateHackathonGroupViewModel(group);
			((HackathonGroupViewModel)model).Hackathon = HackathonViewModel.CreateHackathonViewModel(group.Hackathon);
		    return (HackathonGroupViewModel)model;
        }

		public HackathonViewModel Hackathon { get; set; }
	}
}
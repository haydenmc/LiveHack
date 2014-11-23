using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class HackathonGroupViewModel : GroupViewModel
	{
		public static HackathonGroupViewModel CreateHackathonGroupViewModel(HackathonGroup group) : base(group)
		{
            HackathonGroupViewModel model = new HackathonGroupViewModel();
			model.Hackathon = HackathonViewModel.CreateHackathonViewModel(group.Hackathon);
		    return model;
        }

		public HackathonViewModel Hackathon { get; set; }
	}
}
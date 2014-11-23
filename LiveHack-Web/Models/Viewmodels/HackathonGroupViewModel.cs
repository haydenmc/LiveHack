﻿using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class HackathonGroupViewModel : GroupViewModel
	{
		public HackathonGroupViewModel(HackathonGroup group) : base(group)
		{
			this.Hackathon = HackathonViewModel.CreateHackathonViewModel(group.Hackathon);
		}

		public HackathonViewModel Hackathon { get; set; }
	}
}
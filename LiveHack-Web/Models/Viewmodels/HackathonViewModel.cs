using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class HackathonViewModel
	{
		public HackathonViewModel(Hackathon hack)
		{
			
		}

		public Guid HackathonId { get; set; }
	}
}
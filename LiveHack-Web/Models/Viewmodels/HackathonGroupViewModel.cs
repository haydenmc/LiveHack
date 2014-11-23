using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class HackathonGroupViewModel : GroupViewModel
	{
		public HackathonGroupViewModel(HackathonGroup group)
		{
			
		}

		public Guid GroupId { get; set }
	}
}
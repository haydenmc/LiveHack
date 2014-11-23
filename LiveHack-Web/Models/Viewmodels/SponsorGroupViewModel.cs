using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class SponsorGroupViewModel : TeamGroupViewModel
	{
		public SponsorGroupViewModel(SponsorGroup group) : base(group)
		{

		}
	}
}
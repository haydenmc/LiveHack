using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class InstitutionGroupViewModel : GroupViewModel
	{
		public InstitutionGroupViewModel(InstitutionGroup group) : base(group)
		{
			this.Institution = new InstitutionViewModel(group.Institution);
		}
		public InstitutionViewModel Institution { get; set; }
	}
}
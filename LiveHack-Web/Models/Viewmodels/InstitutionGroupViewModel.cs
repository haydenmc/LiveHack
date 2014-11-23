using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class InstitutionGroupViewModel : GroupViewModel
	{
		public static InstitutionGroupViewModel CreateInstitutionGroupViewModel(InstitutionGroup group) : base(group)
		{
            InstitutionGroupViewModel model = new InstitutionGroupViewModel();
			model.Institution = InstitutionViewModel.CreateInstitutionViewModel(group.Institution);
	    	return model;
        }
		public InstitutionViewModel Institution { get; set; }
	}
}
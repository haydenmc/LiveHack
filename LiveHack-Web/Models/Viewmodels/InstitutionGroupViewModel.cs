using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class InstitutionGroupViewModel : GroupViewModel
	{
		public static InstitutionGroupViewModel CreateInstitutionGroupViewModel(InstitutionGroup group)
		{
            InstitutionGroupViewModel model = InstitutionGroupViewModel.CreateInstitutionGroupViewModel(group);
			((InstitutionGroupViewModel)model).Institution = InstitutionViewModel.CreateInstitutionViewModel(group.Institution);
	    	return (InstitutionGroupViewModel)model;
        }
		public InstitutionViewModel Institution { get; set; }
	}
}
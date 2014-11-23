using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LiveHackDb.Models;

namespace LiveHack_Web.Models.Viewmodels
{
    public class InstitutionViewModel
    {
        public static InstitutionViewModel CreateInstitutionViewModel(Institution ins)
		{
            InstitutionViewModel model = new InstitutionViewModel();
            model.InstitutionId  = ins.InstitutionId;
            model.Name           = ins.Name;
            model.ZipCode        = ins.ZipCode;
			model.GroupId		= ins.Group.GroupId;
			//this.Group			= new InstitutionGroupViewModel(ins.Group);
            //this.Users			= ins.Users.Select(x => new UserViewModel(x)).ToList();
            return model;
        }
        public Guid InstitutionId { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
		public Guid GroupId { get; set; }
		//public InstitutionGroupViewModel Group { get; set; }
		//public ICollection<UserViewModel> Users { get; set; }
	}
}
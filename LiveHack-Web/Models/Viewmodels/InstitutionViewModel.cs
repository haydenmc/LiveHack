using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LiveHackDb.Models;

namespace LiveHack_Web.Models.Viewmodels
{
    public class InstitutionViewModel
    {
        public InstitutionViewModel(Institution ins)
		{
            this.InstitutionId  = ins.InstitutionId;
            this.Name           = ins.Name;
            this.ZipCode        = ins.ZipCode;
			this.GroupId		= ins.Group.GroupId;
			//this.Group			= new InstitutionGroupViewModel(ins.Group);
            //this.Users			= ins.Users.Select(x => new UserViewModel(x)).ToList();
		}
        public Guid InstitutionId { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
		public Guid GroupId { get; set; }
		//public InstitutionGroupViewModel Group { get; set; }
		//public ICollection<UserViewModel> Users { get; set; }
	}
}
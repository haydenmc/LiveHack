using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LiveHackDb.Models;

namespace LiveHack_Web.Models.Viewmodels
{
    public class InstitutionBindingModel
    {
        
       // public Guid InstitutionId { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
		//public Guid GroupId { get; set; }
		//public InstitutionGroupViewModel Group { get; set; }
		//public ICollection<UserViewModel> Users { get; set; }
	}
}
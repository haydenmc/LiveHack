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
			
		}

		public Guid InstitutionId { get; set; }
    }
}
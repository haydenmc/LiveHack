using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LiveHackDb.Models;

namespace LiveHack_Web.Models.Viewmodels
{
    public class TechnologyBindingModel
    {
         public TechnologyBindingModel(Technology tech)
		{
            this.TechnologyId  = tech.TechnologyId;
            this.Name          = tech.Name;
		}
        public Guid TechnologyId { get; set; }
        public string Name { get; set; }
    }
}
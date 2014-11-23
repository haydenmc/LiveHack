using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LiveHackDb.Models;

namespace LiveHack_Web.Models.Viewmodels
{
    public class TechnologyViewModel
    {
         public static TechnologyViewModel CreateTechnologyViewModel(Technology tech)
		{
            TechnologyViewModel model = new TechnologyViewModel();
            model.TechnologyId  = tech.TechnologyId;
            model.Name          = tech.Name;
            return model;
		}
        public Guid TechnologyId { get; set; }
        public string Name { get; set; }
    }
}
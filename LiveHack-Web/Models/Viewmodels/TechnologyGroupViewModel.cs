﻿using LiveHackDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack_Web.Models.Viewmodels
{
	public class TechnologyGroupViewModel : GroupViewModel
	{
		public TechnologyGroupViewModel(TechnologyGroup group) : base(group)
		{
			this.Technology = new TechnologyViewModel(group.Technology);
		}
		public TechnologyViewModel Technology { get; set; }
	}
}
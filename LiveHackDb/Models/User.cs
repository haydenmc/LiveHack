﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LiveHackDb.Models
{
	public class User : IdentityUser
	{
		[InverseProperty("Users")]
		public virtual ICollection<Hackathon> Hackathons { get; set; }
		[InverseProperty("Users")]
		public virtual Institution Institution { get; set; }
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}
	}
}

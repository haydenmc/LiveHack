using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveHackDb.Models
{
	public class Hackathon
	{
		/// <summary>
		/// Unique ID for this hackathon
		/// </summary>
		public Guid HackathonId { get; set; }

		/// <summary>
		/// Name of this hackathon, e.g. 'Boilermake 2014'
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Short name of this hackathon for subdomain, e.g. 'boilermake14'
		/// </summary>
		public string ShortName { get; set; }

		/// <summary>
		/// Short description of this hackathon.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Date and time this hackathon begins
		/// </summary>
		public DateTimeOffset StartDateTime { get; set; }

		/// <summary>
		/// Date and time this hackathon ends
		/// </summary>
		public DateTimeOffset EndDateTime { get; set; }
		
		/// <summary>
		/// Institution that is hosting this hackathon, if any
		/// </summary>
		public virtual Institution Institution { get; set; }

		/// <summary>
		/// Users who are participating in this hackathon
		/// </summary>
		[InverseProperty("Hackathons")]
		public virtual ICollection<User> Users { get; set; }

		/// <summary>
		/// Groups that belong to this hackathon
		/// </summary>
		[InverseProperty("Hackathon")]
		public virtual ICollection<HackathonGroup> Groups { get; set; }
	}
}

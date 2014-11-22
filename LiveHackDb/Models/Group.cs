using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveHackDb.Models
{
	public class Group
	{
		/// <summary>
		/// Unique ID for this group
		/// </summary>
		public Guid GroupId { get; set; }

		/// <summary>
		/// Members that belong to this group
		/// </summary>
		public virtual ICollection<User> Members { get; set; }

		/// <summary>
		/// Guests who are visiting this group
		/// </summary>
		public virtual ICollection<User> Guests { get; set; }

		/// <summary>
		/// Name of this group
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Description of this group
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Group's home page or URL
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// A list of chat messages in this group
		/// </summary>
		[InverseProperty("Group")]
		public virtual ICollection<Message> Messages { get; set; }
	}

	public class InstitutionGroup : Group
	{
		[InverseProperty("Group")]
		public virtual Institution Institution { get; set; }
	}

	public class HackathonGroup : Group
	{
		[InverseProperty("Groups")]
		public virtual Hackathon Hackathon { get; set; }
	}

	public class TechnologyGroup : HackathonGroup
	{
		public virtual Technology Technology { get; set; }
	}

	public class TeamGroup : HackathonGroup
	{
		/// <summary>
		/// The technologies being used for team, sponsor, hackathon groups
		/// </summary>
		public virtual ICollection<Technology> TechnologiesUsing { get; set; }
	}

	public class SponsorGroup : TeamGroup
	{

	}
}

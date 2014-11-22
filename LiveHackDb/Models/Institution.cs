using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveHackDb.Models
{
	public class Institution
	{
		public Guid InstitutionId { get; set; }
		public string Name { get; set; }
		[MaxLength(5)]
		public string ZipCode { get; set; }
		[InverseProperty("Institution")]
		public virtual ICollection<User> Users { get; set; }
		[InverseProperty("Institution")]
		public virtual InstitutionGroup Group { get; set; }
	}
}

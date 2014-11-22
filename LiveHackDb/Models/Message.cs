using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveHackDb.Models
{
	public class Message
	{
		public Guid MessageId { get; set; }
		public virtual User Sender { get; set; }
		[InverseProperty("Messages")]
		public virtual Group Group { get; set; }
		public string Body { get; set; }
		public DateTimeOffset SendTime { get; set; }
	}
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveHackDb.Models
{
    public class Message
    {
        [JsonProperty("messageId")]
        public Guid MessageId { get; set; }
        [JsonProperty("sender")]
        public User Sender { get; set; }
        [JsonProperty("body")]
        [StringLength(4096)]
        public string Body { get; set; }
        [JsonProperty("sentDateTime")]
        public DateTimeOffset SentDateTime { get; set; }
    }
}

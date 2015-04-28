using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LiveHack.Models.BindingModels
{
    public class AnnouncementBindingModel
    {
        [Required]
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LiveHack.Models.BindingModels
{
    public class TechnologyBindingModel
    {
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
    }
}
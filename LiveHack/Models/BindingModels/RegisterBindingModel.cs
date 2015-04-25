using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveHack.Models.BindingModels
{
    public class RegisterBindingModel
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
    }
}
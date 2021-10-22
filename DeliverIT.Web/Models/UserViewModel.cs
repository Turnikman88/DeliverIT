using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Models
{
    public class UserViewModel
    {
        [MinLength(2), MaxLength(20)]
        public string FirstName { get; set; }

        [MinLength(2), MaxLength(20)]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")] //ToDo: add error messages
        public string Email { get; set; }

        [MinLength(8)]
        public string Password { get; set; }

        [MinLength(5)]
        public string Address { get; set; }
    }
}

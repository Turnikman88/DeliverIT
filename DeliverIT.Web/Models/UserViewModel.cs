﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Models
{
    public class UserViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Value for {0} must be between {2} and {1}.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Value for {0} must be between {2} and {1}.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")] //ToDo: add error messages
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password lenght must be {1} characters or longer")]
        public string Password { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Address name cannot be that short")]
        public string Address { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "City name cannot be that short")]
        public string City { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Country name cannot be that short")]
        public string Country { get; set; }

        public int AddressId { get; set; }

    }
}

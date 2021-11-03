using DeliverIT.Services.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Models
{
    public class CityViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Value for {0} must be between {2} and {1}.")]
        public string Name { get; set; }

        [Required]
        public int CountryId { get; set; }

        public List<SelectListItem> Countries { get; set; } = new List<SelectListItem>();
    }
}

using DeliverIT.Services.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Models
{
    public class CountryViewModel
    {
        public int Id { get; set; }
            
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Value for {0} must be between {2} and {1}.")]
        public string Name { get; set; }

        public string FilterTag { get; set; }
        public IEnumerable<CountryDTO> Countries { get; set; }
    }
}

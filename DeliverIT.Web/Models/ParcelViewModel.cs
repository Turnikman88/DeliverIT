using DeliverIT.Services.DTOs;
using DeliverIT.Web.Models.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Models
{
    public class ParcelViewModel : ISearchable
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; } 

        [Required]
        public int ShipmentId { get; set; } 

        [Required]
        public int? WareHouseId { get; set; } 

        [Required]
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        [Required]
        public string CategoryName { get; set; }

        [Required]
        [Range(0, 10000, ErrorMessage = "Value for {0} must be between {2} and {1}.")]
        public double Weight { get; set; } 

        public bool DeliverToAddress { get; set; }

        public string ShipmentStatus { get; set; } 

        public IEnumerable<ParcelDTO> Parcels { get; set; }

        public string FilterTag { get; set; }
    }
}

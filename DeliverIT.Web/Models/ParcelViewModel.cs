using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
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

        [Required(ErrorMessage = Constants.REQUIRED)]
        public int? CustomerId { get; set; }

        [Required(ErrorMessage = Constants.REQUIRED)]
        public int? ShipmentId { get; set; }

        [Required(ErrorMessage = Constants.REQUIRED)]
        public int? WareHouseId { get; set; }

        [Required(ErrorMessage = Constants.REQUIRED)]
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public string CategoryName { get; set; }

        [Required(ErrorMessage = Constants.REQUIRED)]
        public int? CategoryId { get; set; }

        [Range(0, 10000, ErrorMessage = "Weight must be between 0 and 10000.")]
        public double? Weight { get; set; } 

        public bool DeliverToAddress { get; set; }

        public string ShipmentStatus { get; set; } 

        public IEnumerable<ParcelDTO> Parcels { get; set; }

        public string FilterTag { get; set; }
    }
}

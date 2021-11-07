using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using DeliverIT.Web.Models.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeliverIT.Web.Models
{
    public class ShipmentViewModel : ISearchable
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.REQUIRED)]
        [DataType(DataType.Date)]
        public DateTime? DepartureDate { get; set; }

        [Required(ErrorMessage = Constants.REQUIRED)]
        [DataType(DataType.Date)]
        public DateTime? ArrivalDate { get; set; }

        [Required(ErrorMessage = Constants.REQUIRED)]
        public int? OriginWareHouseId { get; set; }

        [Required(ErrorMessage = Constants.REQUIRED)]
        public int? DestinationWareHouseId { get; set; }

        [Required(ErrorMessage = Constants.REQUIRED)]
        public int? StatusId { get; set; }
        public string Status { get; set; }
        public string FilterTag { get; set; }
        public IEnumerable<ShipmentDTO> Shipments { get; set; }
        public List<SelectListItem> Statuses { get; set; } = new List<SelectListItem>();
    }
}

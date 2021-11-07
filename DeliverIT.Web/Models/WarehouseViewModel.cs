using DeliverIT.Services.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DeliverIT.Web.Models
{
    public class WarehouseViewModel : MapsViewModel
    {
        public IEnumerable<WareHouseDTO> Warehouses { get; set; }
        public List<SelectListItem> Cities { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Countries { get; set; } = new List<SelectListItem>();
        public string FilterTag { get; set; }
    }
}

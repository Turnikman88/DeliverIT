using DeliverIT.Services.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

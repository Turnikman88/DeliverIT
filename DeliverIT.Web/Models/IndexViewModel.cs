using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Models
{
    public class IndexViewModel
    {
        public List<MapsViewModel> WarehouseLocations { get; set; } = new List<MapsViewModel>();

        public int UserCount { get; set; }
        public int WarehouseCount { get; set; }
    }
}

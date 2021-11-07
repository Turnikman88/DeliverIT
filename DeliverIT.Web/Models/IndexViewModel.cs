using System.Collections.Generic;

namespace DeliverIT.Web.Models
{
    public class IndexViewModel
    {
        public List<MapsViewModel> WarehouseLocations { get; set; } = new List<MapsViewModel>();

        public int UserCount { get; set; }
        public int WarehouseCount { get; set; }
    }
}

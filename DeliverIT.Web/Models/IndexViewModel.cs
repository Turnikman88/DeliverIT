using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Models
{
    public class IndexViewModel
    {
        public List<Maps> WarehouseLocations { get; set; } = new List<Maps>();

        public int UserCount { get; set; }
        public int WarehouseCount { get; set; }
    }

    public class Maps
    {
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}

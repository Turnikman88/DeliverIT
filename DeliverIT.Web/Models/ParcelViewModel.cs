using DeliverIT.Services.DTOs;
using DeliverIT.Web.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Models
{
    public class ParcelViewModel : ISearchable
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ShipmentId { get; set; }

        public int? WareHouseId { get; set; }
/*        public int CategoryId { get; set; }
*/        public string CategoryName { get; set; }

        public double Weight { get; set; }

        public bool DeliverToAddress { get; set; }

        public string ShipmentStatus { get; set; }

        public IEnumerable<ParcelDTO> Parcels { get; set; }

        public string FilterTag { get; set; }
    }
}

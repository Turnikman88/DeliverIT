using DeliverIT.Services.DTOs;
using System.Collections.Generic;

namespace DeliverIT.Web.Models
{
    public class CustomerViewModel
    {
        public IEnumerable<CustomerDTO> Customers { get; set; }
        public string FilterTag { get; set; }
    }
}

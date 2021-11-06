using DeliverIT.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Models
{
    public class CustomerViewModel
    {
        public IEnumerable<CustomerDTO> Customers { get; set; }
        public string FilterTag { get; set; }
    }
}

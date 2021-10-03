using DeliverIT.Services.Contracts;
using DeliverIT.Services.Services;
using DeliverIT.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService cs;
        public CustomerController(ICustomerService cs)
        {
            this.cs = cs;
        }

        // must be public - non authorisation needed
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomersAsync()
        {
            return this.Ok(await cs.GetAsync());
        }

        // Find a customer by his/her name (first or last) if more than one matches - list
        [HttpGet("{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<IEnumerable<CustomerDTO
            >>> FindCustomerByNameAsync(string name)
        {
            var result = await cs.GetCustomerByNameAsync(name);

            if(result.Count() == 0)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> FindCustomerByEmailAsync(string email)
        {
            var customers = await cs.GetCustomersByEmailAsync(email);
            if (customers is null)
            {
                return this.NotFound();
            }
            return this.Ok(customers);
        }
             
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CustomerDTO>> CreateCustomerAsync(CustomerDTO obj)
        {
            if(obj is null)
            {
                return this.BadRequest();
            }

            return this.Created("Get", await this.cs.PostAsync(obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CustomerDTO>> DeleteCustomer(int id)
        {
            if(await cs.GetCustomerByIDAsync(id) is null)
            {
                return this.NotFound();
            }
            return this.Ok(await this.cs.DeleteAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDTO>> UpdateCustomerAsync(int id, CustomerDTO obj)
        {
            if(obj is null || await cs.GetCustomerByIDAsync(id) is null)
            {
                return this.NotFound();
            }

            return this.Ok(await this.cs.UpdateAsync(id, obj));
        }
    }
}

using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IAuthenticationService auth;
        public CustomerController(ICustomerService cs, IAuthenticationService auth)
        {
            this.cs = cs;
            this.auth = auth;
        }

        // must be public - non authorisation needed
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> CustomerCount()
        {
            return Ok(await cs.UserCountAsync());
        }

        [HttpGet("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<CustomerDTO
            >>> GetAllCustomersAsync([FromHeader] string authorization)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }
            return this.Ok(await cs.GetAsync());
        }

        [HttpGet("{parameter}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<CustomerDTO
            >>> FindCustomerByOneWord([FromHeader] string authorization, string parameter)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            var result = await cs.GetCustomersByEmailAsync(parameter);

            if (result is null)
            {
                result = await cs.GetCustomerByNameAsync(parameter);

                if (result is null)
                {
                    return this.NotFound();
                }
            }

            return this.Ok(result);
        }

        // Find a customer by his/her name (first or last) if more than one matches - list
        [HttpGet("name/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<CustomerDTO
            >>> FindCustomerByNameAsync([FromHeader] string authorization, string name)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            var result = await cs.GetCustomerByNameAsync(name);

            if (result.Count() == 0)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> FindCustomerByEmailAsync([FromHeader] string authorization, string email)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            var customers = await cs.GetCustomersByEmailAsync(email);
            if (customers is null)
            {
                return this.NotFound();
            }
            return this.Ok(customers);
        }

        //must be public
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CustomerDTO>> CreateCustomerAsync(CustomerDTO obj)
        {
            if (obj is null)
            {
                return this.BadRequest();
            }

            return this.Created("Get", await this.cs.PostAsync(obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CustomerDTO>> DeleteCustomer([FromHeader] string authorization, int id) // ToDo: change when add passwords
        {
            if (await cs.GetCustomerByIDAsync(id) is null)
            {
                return this.NotFound();
            }

            if (auth.FindEmployee(authorization))
            {
                return this.Ok(await this.cs.DeleteAsync(id));
            }
            else if (auth.FindUser(authorization))
            {
                var customer = await cs.GetCustomerByIDAsync(id);
                if (customer.Email == authorization)
                {
                    return this.Ok(await this.cs.DeleteAsync(id));
                }                
            }

            return this.Unauthorized();
        }

        [HttpGet("multi/{name}/orderby/{param}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> FindByMultipleCriteria([FromHeader] string authorization, string name, string param)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            name = name.ToLower();
            param = param.ToLower();

            var result = await cs.GetCustomersByEmailAsync(name);

            if (result is null || result.Count() == 0)
            {
                result = await cs.GetCustomerByNameAsync(name);
                if (result == null)
                {
                    return BadRequest();
                }
            }

            if (param == "name")
            {
                result = result.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);
            }
            else if (param == "email")
            {
                result = result.OrderBy(x => x.Email);
            }
            else
            {
                result = result.OrderBy(x => x.Id);
            }
            return this.Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CustomerDTO>> UpdateCustomerAsync([FromHeader] string authorization, int id, CustomerDTO obj)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (obj is null || await cs.GetCustomerByIDAsync(id) is null)
            {
                return this.NotFound();
            }

            return this.Ok(await this.cs.UpdateAsync(id, obj));
        }
    }
}

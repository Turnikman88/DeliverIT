using DeliverIT.API.Attributes;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _cs;
        public CustomerController(ICustomerService cs)
        {
            this._cs = cs;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> CustomerCountAsync()
        {
            return Ok(await _cs.UserCountAsync());
        }

        [HttpGet("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomersAsync()
        {
            return this.Ok(await _cs.GetAsync());
        }

        [HttpGet("{parameter}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> FindCustomerByOneWordAsync(string parameter)
        {
            var result = await _cs.GetCustomersByEmailAsync(parameter);

            if (result is null)
            {
                result = await _cs.GetCustomersByNameAsync(parameter);

                if (result is null)
                {
                    return this.NotFound();
                }
            }

            return this.Ok(result);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> FindCustomerByNameAsync(string name)
        {
            return this.Ok(await _cs.GetCustomersByNameAsync(name));
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> FindCustomerByEmailAsync(string email)
        {
            return this.Ok(await _cs.GetCustomersByEmailAsync(email));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<CustomerDTO>> DeleteCustomerAsync(int id)
        {
            return this.Ok(await this._cs.DeleteAsync(id));
        }

        [HttpDelete("deleteprofile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_USER, QueryId = "customerId")]
        public async Task<ActionResult<CustomerDTO>> DeleteCustomerProfileAsync([BindRequired] int customerId)
        {
            return this.Ok(await this._cs.DeleteAsync(customerId));
        }

        [HttpGet("multi/{name}/orderby/{param}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> FindByMultipleCriteriaAsync(string name, string param)
        {

            var result = await _cs.GetCustomersByEmailAsync(name);

            if (result is null || result.Count() == 0)
            {
                result = await _cs.GetCustomersByNameAsync(name);
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
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<CustomerDTO>> UpdateCustomerAsync(int id, CustomerDTO obj)
        {
            return this.Ok(await this._cs.UpdateAsync(id, obj));
        }
    }
}

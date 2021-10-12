using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                result = await _cs.GetCustomerByNameAsync(parameter);

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
            return this.Ok(await _cs.GetCustomerByNameAsync(name));
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
        [Authorize(Roles = Constants.ROLE_USER)]
        public async Task<ActionResult<CustomerDTO>> DeleteCustomerAsync(int id) 
        {
            var role = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var userId = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (role == Constants.ROLE_EMPLOYEE)
            {
                return this.Ok(await this._cs.DeleteAsync(id));
            }
            else if (role == Constants.ROLE_USER)
            {
                if (id == int.Parse(userId))
                {
                    return this.Ok(await this._cs.DeleteAsync(id));
                }
                return this.Unauthorized(Constants.NOT_LOGGED);
            }

            return this.Unauthorized(Constants.NOT_LOGGED);
        }

        [HttpGet("multi/{name}/orderby/{param}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> FindByMultipleCriteriaAsync(string name, string param)
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }


            var result = await _cs.GetCustomersByEmailAsync(name);

            if (result is null || result.Count() == 0)
            {
                result = await _cs.GetCustomerByNameAsync(name);
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

using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
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
        private readonly ICustomerService _cs;
        public CustomerController(ICustomerService cs)
        {
            this._cs = cs;
        }

        // must be public - non authorisation needed
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> CustomerCount()
        {
            return Ok(await _cs.UserCountAsync());
        }

        [HttpGet("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(401)]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<CustomerDTO
            >>> GetAllCustomersAsync()
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

            return this.Ok(await _cs.GetAsync());
        }

        [HttpGet("{parameter}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<CustomerDTO
            >>> FindCustomerByOneWord(string parameter)
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

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

        // Find a customer by his/her name (first or last) if more than one matches - list
        [HttpGet("name/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<CustomerDTO
            >>> FindCustomerByNameAsync(string name)
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

            var result = await _cs.GetCustomerByNameAsync(name);

            if (result.Count() == 0)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> FindCustomerByEmailAsync(string email)
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

            var customers = await _cs.GetCustomersByEmailAsync(email);
            if (customers is null)
            {
                return this.NotFound();
            }
            return this.Ok(customers);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<CustomerDTO>> DeleteCustomer(int id) // ToDo: change when add passwords
        {
            if (await _cs.GetCustomerByIDAsync(id) is null)
            {
                throw new KeyNotFoundException(Constants.ACCOUNT_NOT_FOUND);
                //return this.NotFound();
            }

            if (this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Ok(await this._cs.DeleteAsync(id));
            }
            else if (this.Request.Cookies.ContainsKey(Constants.KEY_USER_ID))
            {
                if (Request.Cookies[Constants.KEY_USER_ID] != null && id == int.Parse(Request.Cookies[Constants.KEY_USER_ID]))
                {
                    return this.Ok(await this._cs.DeleteAsync(id));
                }
            }

            return this.Unauthorized();
        }

        [HttpGet("multi/{name}/orderby/{param}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> FindByMultipleCriteria(string name, string param)
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

            name = name.ToLower();
            param = param.ToLower();

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
        public async Task<ActionResult<CustomerDTO>> UpdateCustomerAsync(int id, CustomerDTO obj)
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

            if (obj is null || await _cs.GetCustomerByIDAsync(id) is null)
            {
                return this.NotFound();
            }

            return this.Ok(await this._cs.UpdateAsync(id, obj));
        }
    }
}

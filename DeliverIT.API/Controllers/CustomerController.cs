using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using Microsoft.AspNetCore.Http;
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
        private readonly ICustomerService _cs;
        private readonly IAuthenticationService _auth;
        public CustomerController(ICustomerService cs, IAuthenticationService auth)
        {
            this._cs = cs;
            this._auth = auth;
        }

        // must be public - non authorisation needed
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> CustomerCount()
        {
            return Ok(await _cs.UserCountAsync());
        }

        [HttpGet("login")]
        public async Task<ActionResult> Login([FromHeader] string credentials)
        {
            var isUserExisting = this._auth.FindUser(credentials);
            if (isUserExisting)
            {
                var userId = await this._auth.GetUserID(credentials);
                var options = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(5)
                };
                this.Response.Cookies.Append("userId", userId, options);
                return this.Ok("logged");
            }

            var isEmployeeExisting = this._auth.FindEmployee(credentials);
            if (isEmployeeExisting)
            {
                var employeeId = await this._auth.GetEmployeeID(credentials);
                var options = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(5)
                };
                this.Response.Cookies.Append("employeeId", employeeId, options);
                return this.Ok("permissions granted!");
            }

            return this.Unauthorized();
        }

        [HttpGet("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(401)]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<CustomerDTO
            >>> GetAllCustomersAsync()
        {
            if (!this.Request.Cookies.ContainsKey("employeeId"))
            {
                return this.Unauthorized("You are not an employee");
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
            if (!this.Request.Cookies.ContainsKey("employeeId"))
            {
                return this.Unauthorized("You are not an employee");
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
            if (!this.Request.Cookies.ContainsKey("employeeId"))
            {
                return this.Unauthorized("You are not an employee");
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
            if (!this.Request.Cookies.ContainsKey("employeeId"))
            {
                return this.Unauthorized("You are not an employee");
            }

            var customers = await _cs.GetCustomersByEmailAsync(email);
            if (customers is null)
            {
                return this.NotFound();
            }
            return this.Ok(customers);
        }

        //must be public
        [HttpPost("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CustomerDTO>> CreateCustomerAsync(CustomerDTO obj)
        {
            return this.Created("Get", await this._cs.PostAsync(obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<CustomerDTO>> DeleteCustomer(int id) // ToDo: change when add passwords
        {
            if (await _cs.GetCustomerByIDAsync(id) is null)
            {
                throw new KeyNotFoundException("Account not found");
                //return this.NotFound();
            }

            if (this.Request.Cookies.ContainsKey("employeeId"))
            {
                return this.Ok(await this._cs.DeleteAsync(id));
            }
            else if (this.Request.Cookies.ContainsKey("userId"))
            {
                if (id == int.Parse(Request.Cookies["userId"]))
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
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> FindByMultipleCriteria([FromHeader] string authorization, string name, string param)
        {
            if (!this.Request.Cookies.ContainsKey("employeeId"))
            {
                return this.Unauthorized("You are not an employee");
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
        public async Task<ActionResult<CustomerDTO>> UpdateCustomerAsync([FromHeader] string authorization, int id, CustomerDTO obj)
        {
            if (!this.Request.Cookies.ContainsKey("employeeId"))
            {
                return this.Unauthorized("You are not an employee");
            }

            if (obj is null || await _cs.GetCustomerByIDAsync(id) is null)
            {
                return this.NotFound();
            }

            return this.Ok(await this._cs.UpdateAsync(id, obj));
        }
    }
}

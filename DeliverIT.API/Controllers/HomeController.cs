using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DeliverIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ICustomerService _cs;
        private readonly IAuthenticationService _auth;
        public HomeController(ICustomerService cs, IAuthenticationService auth)
        {
            this._cs = cs;
            this._auth = auth;
        }

        [HttpPost("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CustomerDTO>> CreateCustomerAsync(CustomerDTO obj)
        {
            return this.Created("Get", await this._cs.PostAsync(obj));
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
                this.Response.Cookies.Append(Constants.KEY_USER_ID, userId, options);
                return this.Ok(Constants.LOGGED);
            }

            var isEmployeeExisting = this._auth.FindEmployee(credentials);
            if (isEmployeeExisting)
            {
                var employeeId = await this._auth.GetEmployeeID(credentials);
                var options = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(5)
                };
                this.Response.Cookies.Append(Constants.KEY_EMPLOYEE_ID, employeeId, options);
                return this.Ok(Constants.LOGGED);
            }

            return this.Unauthorized();
        }
    }
}

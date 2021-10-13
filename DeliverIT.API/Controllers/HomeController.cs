using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeliverIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ICustomerService _cs;
        private readonly IFindUserService _auth;
        public HomeController(ICustomerService cs, IFindUserService auth)
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

            var user = await this._auth.FindUs(credentials);

            if (user is null)
            {
                return this.Unauthorized();
            }

            await SignInWithRoleAsync(user);

            return this.Ok();

        }
        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Ok();
        }

        private async Task SignInWithRoleAsync(UserDTO user)
        {
            //You can add more claims as you wish but keep these KEYS here as is
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}

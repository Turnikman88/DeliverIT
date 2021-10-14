using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
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
    }
}

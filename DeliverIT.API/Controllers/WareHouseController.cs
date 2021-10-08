using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WareHouseController : ControllerBase
    {
        private readonly IWareHouseService _ws;
        private readonly IAuthenticationService _auth;

        public WareHouseController(IWareHouseService ws, IAuthenticationService auth)
        {
            this._ws = ws;
            this._auth = auth;
        }

        // must be public - non authorisation needed

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<string>>> GetAddressesAsync()
        {
            return this.Ok(await _ws.GetAddressesAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<WareHouseDTO>> GetWareHouseByIdAsync([FromHeader] string authorization, int id)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (!await _ws.WareHouseExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await _ws.GetWareHouseByIdAsync(id));
        }

        //must be public
        [HttpGet("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<WareHouseDTO>>> GetWareHousesAsync([FromHeader] string authorization)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await _ws.GetAsync());
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<WareHouseDTO>> CreateWareHouseAsync([FromHeader] string authorization, WareHouseDTO obj)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (obj is null || obj.AddressId == 0)
            {
                return this.BadRequest();
            }
            return this.Ok(await _ws.PostAsync(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<WareHouseDTO>> UpdateWareHouseAsync([FromHeader] string authorization, int id, WareHouseDTO obj)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (obj is null || obj.AddressId == 0 || !await _ws.WareHouseExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await _ws.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<WareHouseDTO>> DeleteWareHouseAsync([FromHeader] string authorization, int id)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (!await _ws.WareHouseExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await _ws.DeleteAsync(id));
        }
    }
}

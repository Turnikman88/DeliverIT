using DeliverIT.API.Attributes;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
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

        public WareHouseController(IWareHouseService ws)
        {
            this._ws = ws;
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
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<WareHouseDTO>> GetWareHouseByIdAsync(int id)
        {
            return this.Ok(await _ws.GetWareHouseByIdAsync(id));
        }

        //must be public
        [HttpGet("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<WareHouseDTO>>> GetWareHousesAsync()
        {
            return this.Ok(await _ws.GetAsync());
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<WareHouseDTO>> CreateWareHouseAsync(WareHouseDTO obj)
        {
            return this.Ok(await _ws.PostAsync(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<WareHouseDTO>> UpdateWareHouseAsync(int id, WareHouseDTO obj)
        {
            return this.Ok(await _ws.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<WareHouseDTO>> DeleteWareHouseAsync(int id)
        {
            return this.Ok(await _ws.DeleteAsync(id));
        }
    }
}

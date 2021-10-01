using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WareHouseController : ControllerBase
    {
        private readonly IWareHouseService ws;

        public WareHouseController(IWareHouseService ws)
        {
            this.ws = ws;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<WareHouseDTO>> GetWareHouseByIdAsync(int id)
        {
            if (!await ws.WareHouseExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ws.GetWareHouseByIdAsync(id));
        }
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<WareHouseDTO>>> GetWareHousesAsync()
        {
            return this.Ok(await ws.GetAsync());
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<WareHouseDTO>> CreateWareHouseAsync(WareHouseDTO obj)
        {
            if (obj is null || obj.AddressId == 0 )
            {
                return this.BadRequest();
            }
            return this.Ok(await ws.PostAsync(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<WareHouseDTO>> UpdateWareHouseAsync(int id, WareHouseDTO obj)
        {
            if (obj is null || obj.AddressId == 0 || !await ws.WareHouseExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ws.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<WareHouseDTO>> DeleteWareHouseAsync(int id)
        {
            if (!await ws.WareHouseExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ws.DeleteAsync(id));
        }
    }
}

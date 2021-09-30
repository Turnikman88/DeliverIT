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
        public async Task<ActionResult<WareHouseDTO>> GetWareHouseById(int id)
        {
            if (!await ws.WareHouseExists(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ws.GetWareHouseById(id));
        }
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<WareHouseDTO>>> GetWareHouses()
        {
            return this.Ok(await ws.Get());
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<WareHouseDTO>> CreateWareHouse(WareHouseDTO obj)
        {
            if (obj is null || obj.AddressId == 0 )
            {
                return this.BadRequest();
            }
            return this.Ok(await ws.Post(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<WareHouseDTO>> UpdateWareHouse(int id, WareHouseDTO obj)
        {
            if (obj is null || obj.AddressId == 0 || !await ws.WareHouseExists(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ws.Update(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<WareHouseDTO>> DeleteWareHouse(int id)
        {
            if (!await ws.WareHouseExists(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ws.Delete(id));
        }
    }
}

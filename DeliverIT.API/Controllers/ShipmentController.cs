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
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService ss;
        public ShipmentController(IShipmentService ss)
        {
            this.ss = ss;
        }
        /*[HttpGet("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ShipmentDTO>> GetShipmentById(int id)
        {
            if (!await ws.WareHouseExists(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ws.GetWareHouseById(id));
        }*/
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> GetShipments()
        {
            return this.Ok(await ss.Get());
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ShipmentDTO>> CreateShipment(ShipmentDTO obj)
        {
            if (obj is null)
            {
                return this.BadRequest();
            }
            return this.Ok(await ss.Post(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ShipmentDTO>> UpdateShipment(int id, ShipmentDTO obj)
        {
            if (obj is null)
            {
                return this.NotFound();
            }
            return this.Ok(await ss.Update(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ShipmentDTO>> DeleteShipment(int id)
        {
            if (!await ss.ShipmentExists(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ss.Delete(id));
        }
    }
}

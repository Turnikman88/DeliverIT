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
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ShipmentDTO>> GetShipmentByIdAsync(int id)
        {
            if (!await ss.ShipmentExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ss.GetShipmentByIdAsync(id));
        }
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> GetShipmentsAsync()
        {
            return this.Ok(await ss.GetAsync());
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ShipmentDTO>> CreateShipmentAsync(ShipmentDTO obj)
        {
            if (obj is null)
            {
                return this.BadRequest();
            }
            return this.Ok(await ss.PostAsync(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ShipmentDTO>> UpdateShipmentAsync(int id, ShipmentDTO obj)
        {
            if (obj is null)
            {
                return this.NotFound();
            }
            return this.Ok(await ss.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ShipmentDTO>> DeleteShipmentAsync(int id)
        {
            if (!await ss.ShipmentExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ss.DeleteAsync(id));
        }

        [HttpGet("filter/destwarehouse/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> FilterByDestinationWareHouseAsync(int id)
        {            
            return this.Ok(await ss.FilterByDestinationWareHouseAsync(id));
        }
    }
}

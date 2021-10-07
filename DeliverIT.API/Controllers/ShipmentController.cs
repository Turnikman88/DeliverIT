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
        private readonly IAuthenticationService auth;

        public ShipmentController(IShipmentService ss, IAuthenticationService auth)
        {
            this.ss = ss;
            this.auth = auth;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ShipmentDTO>> GetShipmentByIdAsync([FromHeader] string authorization, int id)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (!await ss.ShipmentExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ss.GetShipmentByIdAsync(id));
        }
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> GetShipmentsAsync([FromHeader] string authorization)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ss.GetAsync());
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ShipmentDTO>> CreateShipmentAsync([FromHeader] string authorization, ShipmentDTO obj)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (obj is null)
            {
                return this.BadRequest();
            }
            return this.Ok(await ss.PostAsync(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ShipmentDTO>> UpdateShipmentAsync([FromHeader] string authorization, int id, ShipmentDTO obj)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (obj is null)
            {
                return this.NotFound();
            }
            return this.Ok(await ss.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ShipmentDTO>> DeleteShipmentAsync([FromHeader] string authorization, int id)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (!await ss.ShipmentExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ss.DeleteAsync(id));
        }

        [HttpGet("filter/destwarehouse/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> FilterByDestinationWareHouseAsync([FromHeader] string authorization, int id)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ss.FilterByDestinationWareHouseAsync(id));
        }
        [HttpGet("filter/originwarehouse/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> FilterByOriginWareHouseAsync([FromHeader] string authorization, int id)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ss.FilterByOriginWareHouseAsync(id));
        }

        [HttpGet("filter/customer/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> FilterByCustomerIdAsync([FromHeader] string authorization, int id)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ss.FilterByCustomerIdAsync(id));
        }
        [HttpGet("filter/customer/name/{name}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> FilterByCustomerNameAsync([FromHeader] string authorization, string name)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ss.FilterByCustomerNameAsync(name));
        }

        [HttpGet("filter/customer/email/{email}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> FilterByCustomerEmailAsync([FromHeader] string authorization, string email)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ss.FilterByCustomerEmailAsync(email));
        }

        [HttpGet("filter/customer/address/{address}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> FilterByCustomerAddressAsync([FromHeader] string authorization, string address)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ss.FilterByCustomerAddressAsync(address));
        }
    }
}

using DeliverIT.API.Attributes;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
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
        private readonly IShipmentService _ss;

        public ShipmentController(IShipmentService ss)
        {
            this._ss = ss;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<ShipmentDTO>> GetShipmentByIdAsync(int id)
        {            
            return this.Ok(await _ss.GetShipmentByIdAsync(id));
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> GetShipmentsAsync()
        {
            return this.Ok(await _ss.GetAsync());
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<ShipmentDTO>> CreateShipmentAsync(ShipmentDTO obj)
        {
            return this.Ok(await _ss.PostAsync(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<ShipmentDTO>> UpdateShipmentAsync(int id, ShipmentDTO obj)
        {
            return this.Ok(await _ss.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<ShipmentDTO>> DeleteShipmentAsync(int id)
        {
            return this.Ok(await _ss.DeleteAsync(id));
        }

        [HttpGet("filter/destwarehouse/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> FilterByDestinationWareHouseAsync(int id)
        {
            return this.Ok(await _ss.FilterByDestinationWareHouseAsync(id));
        }

        [HttpGet("filter/originwarehouse/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> FilterByOriginWareHouseAsync(int id)
        {
            return this.Ok(await _ss.FilterByOriginWareHouseAsync(id));
        }

        [HttpGet("filter/customer/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> FilterByCustomerIdAsync(int id)
        {
            return this.Ok(await _ss.FilterByCustomerIdAsync(id));
        }

        [HttpGet("filter/customer/name/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> FilterByCustomerNameAsync(string name)
        {
            return this.Ok(await _ss.FilterByCustomerNameAsync(name));
        }

        [HttpGet("filter/customer/email/{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> FilterByCustomerEmailAsync(string email)
        {
            return this.Ok(await _ss.FilterByCustomerEmailAsync(email));
        }

        [HttpGet("filter/customer/address/{address}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> FilterByCustomerAddressAsync(string address)
        {
            return this.Ok(await _ss.FilterByCustomerAddressAsync(address));
        }

        [HttpGet("filter/status/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> FilterByStatusIdAsync(int id)
        {
            return this.Ok(await _ss.FilterByStatusIdAsync(id));
        }
    }
}

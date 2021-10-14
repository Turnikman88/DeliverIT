using DeliverIT.API.Attributes;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        private readonly IParcelService _ps;

        public ParcelController(IParcelService ps)
        {
            this._ps = ps;
        }

        [HttpGet("incoming")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_USER, QueryId = "customerId")]
        public async Task<ActionResult<ParcelDTO>> ListCustomerIncomingParcelsAsync([BindRequired] int customerId)
        {
            return this.Ok(await _ps.ListCustomerIncomingParcelsAsync(customerId));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<ParcelDTO>> GetParcelByIdAsync(int id)
        {
            return this.Ok(await _ps.GetParcelByIdAsync(id));
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> GetParcelsAsync()
        {
            return this.Ok(await _ps.GetAsync());
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<ParcelDTO>> CreateParcelAsync(ParcelDTO obj)
        {
            return this.Ok(await _ps.PostAsync(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<ParcelDTO>> UpdateParcelAsync(int id, ParcelDTO obj)
        {
            return this.Ok(await _ps.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<ParcelDTO>> DeleteParcelAsync(int id)
        {
            return this.Ok(await _ps.DeleteAsync(id));
        }

        [HttpGet("filter/customer/{customerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> FilterByCustomerIdAsync(int customerId)
        {
            return this.Ok(await _ps.FilterByCustomerIdAsync(customerId));
        }

        [HttpGet("filter/customer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_USER, QueryId = "customerId")]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> UserFilterByCustomerIdAsync([BindRequired] int customerId)
        {
            return this.Ok(await _ps.FilterByCustomerIdAsync(customerId));
        }
        [HttpGet("filter/statuses")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_USER, QueryId = "customerId")]
        public async Task<ActionResult<IEnumerable<string>>> GetShipmentStatusAsync([BindRequired] int customerId)
        {
            return this.Ok(await _ps.GetShipmentStatusAsync(customerId));
        }

        [HttpPut("deliveraddress")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_USER, QueryId = "customerId")]
        public async Task<ActionResult<IEnumerable<string>>> ChangeDeliverLocationAsync([BindRequired] int customerId)
        {
            return this.Ok(await _ps.ChangeDeliverLocationAsync(customerId));
        }

        [HttpGet("filter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> MultiFilterAsync(int? id, int? customerId,
            int? shipmentId, int? warehouseId, int? categoryId, string categoryName, double? minWeight, double? maxWeight)
        {
            return this.Ok(await _ps.MultiFilterAsync(id, customerId, shipmentId, warehouseId, categoryId, categoryName, minWeight, maxWeight));
        }

        [HttpGet("sort/weight")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByWeightAsync()
        {
            return this.Ok(await _ps.SortByWeightAsync());
        }

        [HttpGet("sort/arrival")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByArrivalDateAsync()
        {
            return this.Ok(await _ps.SortByArrivalDateAsync());
        }

        [HttpGet("sort/weight/arrival")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByWeightAndArrivalDateAsync()
        {
            return this.Ok(await _ps.SortByWeightAndArrivalDateAsync());
        }
    }
}

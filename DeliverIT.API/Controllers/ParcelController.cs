using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        private readonly IParcelService _ps;
        private readonly ICustomerService _cs;
        private readonly IAuthenticationService _auth;

        public ParcelController(IParcelService ps, ICustomerService cs, IAuthenticationService auth)
        {
            this._ps = ps;
            this._cs = cs;
            this._auth = auth;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ParcelDTO>> GetParcelByIdAsync(int id)
        {
            if (!this.Request.Cookies.ContainsKey("userId"))
            {
                return this.Unauthorized();
            }

            if (!await _ps.ParcelExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await _ps.GetParcelByIdAsync(id));
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> GetParcelsAsync([FromHeader] string authorization)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await _ps.GetAsync());
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ParcelDTO>> CreateParcelAsync([FromHeader] string authorization, ParcelDTO obj)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (obj is null)
            {
                return this.BadRequest();
            }
            return this.Ok(await _ps.PostAsync(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ParcelDTO>> UpdateParcelAsync([FromHeader] string authorization, int id, ParcelDTO obj)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (obj is null)
            {
                return this.NotFound();
            }
            return this.Ok(await _ps.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ParcelDTO>> DeleteParcelAsync([FromHeader] string authorization, int id)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (!await _ps.ParcelExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await _ps.DeleteAsync(id));
        }

        [HttpGet("filter/customer/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> FilterByCustomerIdAsync([FromHeader] string authorization, int id)
        {
            //var login = authorization.Split().ToList();
            if (_auth.FindUser(authorization))
            {
                var email = authorization.Split()[0];
                var customer = await _cs.GetCustomerByIDAsync(id);
                if (customer.Email == email)
                {
                    return this.Ok(await _ps.FilterByCustomerIdAsync(id));
                }
            }
            return this.Unauthorized();
        }

        [HttpGet("filter/statuses/{customerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<string>>> GetShipmentStatusAsync([FromHeader] string authorization, int customerId)
        {
            if (_auth.FindUser(authorization))
            {
                var email = authorization.Split()[0];
                var customer = await _cs.GetCustomerByIDAsync(customerId);
                if (customer.Email == email)
                {
                    return this.Ok(await _ps.GetShipmentStatusAsync(customerId));
                }
            }
            return this.Unauthorized();
        }

        [HttpPut("deliveraddress/{customerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<string // maybe read it from the body?
            >>> ChangeDeliverLocationAsync([FromHeader] string authorization, int customerId)
        {
            if (_auth.FindUser(authorization))
            {
                var email = authorization.Split()[0];
                var customer = await _cs.GetCustomerByIDAsync(customerId);
                if (customer.Email == email)
                {
                    return this.Ok(await _ps.ChangeDeliverLocationAsync(customerId));
                }
            }
            return this.Unauthorized();
        }

        [HttpGet("filter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> MultiFilterAsync([FromHeader] string authorization, int? id, int? customerId,
            int? shipmentId, int? warehouseId, int? categoryId, string categoryName, double? minWeight, double? maxWeight)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await _ps.MultiFilterAsync(id, customerId, shipmentId, warehouseId, categoryId, categoryName, minWeight, maxWeight));
        }

        [HttpGet("sort/weight")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByWeightAsync([FromHeader] string authorization)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await _ps.SortByWeightAsync());
        }

        [HttpGet("sort/arrival")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByArrivalDateAsync([FromHeader] string authorization)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await _ps.SortByArrivalDateAsync());
        }

        [HttpGet("sort/weight/arrival")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByWeightAndArrivalDateAsync([FromHeader] string authorization)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await _ps.SortByWeightAndArrivalDateAsync());
        }
    }
}

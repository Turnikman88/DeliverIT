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
        private readonly IParcelService ps;
        private readonly ICustomerService cs;
        private readonly IAuthenticationService auth;

        public ParcelController(IParcelService ps, ICustomerService cs, IAuthenticationService auth)
        {
            this.ps = ps;
            this.cs = cs;
            this.auth = auth;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ParcelDTO>> GetParcelByIdAsync([FromHeader] string authorization, int id)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (!await ps.ParcelExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ps.GetParcelByIdAsync(id));
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> GetParcelsAsync([FromHeader] string authorization)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ps.GetAsync());
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ParcelDTO>> CreateParcelAsync([FromHeader] string authorization, ParcelDTO obj)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (obj is null)
            {
                return this.BadRequest();
            }
            return this.Ok(await ps.PostAsync(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ParcelDTO>> UpdateParcelAsync([FromHeader] string authorization, int id, ParcelDTO obj)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (obj is null)
            {
                return this.NotFound();
            }
            return this.Ok(await ps.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ParcelDTO>> DeleteParcelAsync([FromHeader] string authorization, int id)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (!await ps.ParcelExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ps.DeleteAsync(id));
        }

        [HttpGet("filter/customer/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> FilterByCustomerIdAsync([FromHeader] string authorization, int id)
        {
            //var login = authorization.Split().ToList();
            if (auth.FindUser(authorization))
            {
                var customer = await cs.GetCustomerByIDAsync(id);
                if (customer.Email == authorization)
                {
                    return this.Ok(await ps.FilterByCustomerIdAsync(id));
                }
            }
            return this.Unauthorized();
        }

        [HttpGet("filter/statuses/{customerId}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<string>>> GetShipmentStatusAsync([FromHeader] string authorization, int customerId)
        {
            if (auth.FindUser(authorization))
            {
                var customer = await cs.GetCustomerByIDAsync(customerId);
                if (customer.Email == authorization)
                {
                    return this.Ok(await ps.GetShipmentStatusAsync(customerId));
                }
            }
            return this.Unauthorized();
        }

        [HttpPut("deliveraddress/{customerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<string // maybe read it from the body?
            >>> ChangeDeliverLocationAsync([FromHeader] string authorization, int customerId)
        {
            if (auth.FindUser(authorization))
            {
                var customer = await cs.GetCustomerByIDAsync(customerId);
                if (customer.Email == authorization)
                {
                    return this.Ok(await ps.ChangeDeliverLocationAsync(customerId));
                }
            }
            return this.Unauthorized();
        }
        [HttpGet("filter")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> MultiFilterAsync([FromHeader] string authorization, int? id, int? customerId,
            int? shipmentId, int? warehouseId, int? categoryId, string categoryName, double? minWeight, double? maxWeight)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ps.MultiFilterAsync(id, customerId, shipmentId, warehouseId, categoryId, categoryName, minWeight, maxWeight));
        }

        [HttpGet("sort/weight")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByWeightAsync([FromHeader] string authorization)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ps.SortByWeightAsync());
        }

        [HttpGet("sort/arrival")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByArrivalDateAsync([FromHeader] string authorization)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ps.SortByArrivalDateAsync());
        }

        [HttpGet("sort/weight/arrival")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByWeightAndArrivalDateAsync([FromHeader] string authorization)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ps.SortByWeightAndArrivalDateAsync());
        }
    }
}

using DeliverIT.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DeliverIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        private readonly ICustomerService cs;
        private readonly IWareHouseService wh;
        public PublicController(ICustomerService cs, IWareHouseService wh)
        {
            this.cs = cs;
            this.wh = wh;
        }

        [HttpGet("users")]
        [ProducesResponseType(200)]
        public ActionResult<int> UserCount()
        {
            return this.Ok(cs.UserCount());
        }

        [HttpGet("locations")]
        [ProducesResponseType(200)]
        public ActionResult<int> Locations()
        {
            return this.Ok(wh.Locations());
        }


    }
}

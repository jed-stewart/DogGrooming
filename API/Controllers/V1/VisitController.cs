using Microsoft.AspNetCore.Mvc;
using Shared.Interface;
using Data.Domain;
using API.Controllers.Attribute;

namespace API.Controllers.V1
{
    [ApiController]
    [V1]
    [Route("[controller]")]
    public class VisitController : Controller
    {
        private readonly IVisitService _orderService;

        public VisitController (IVisitService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> AddVisitAsync(Visit order)
        {
           await _orderService.AddAsync(order);
            if (order.Id == 0)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Shared.Interface;
using Data.Domain;
using API.Controllers.Attribute;

namespace API.Controllers.V1
{
    [ApiController]
    [V1]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController (IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("place")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> AddOrderAsync(Order order)
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

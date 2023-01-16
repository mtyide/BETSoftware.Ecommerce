using Api.Configuration;
using AutoMapper;
using BETSoftware.Domain.Commands;
using BETSoftware.Domain.Models;
using BETSoftware.Domain.Models.Dtos;
using BETSoftware.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;
        public OrdersController(IMediator mediator, IMapper mapper, ILogger<OrdersController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("getOrders")]
        public async Task<IActionResult> GetOrders()
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var result = await _mediator.Send(new GetOrdersQuery());
            if (result == null) { return NotFound(); }

            return Ok(result);
        }

        [HttpGet]
        [Route("getOrderById/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id));

            if (result == null) { return NotFound();}

            return Ok(result);
        }

        [HttpPost]
        [Route("insertOrder")]
        public async Task<IActionResult> PostOrder([FromBody] OrderInDto orderDto)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var order = _mapper.Map<Order>(orderDto);
            order.Date = DateTime.UtcNow;
            var result = await _mediator.Send(new InsertOrderCommand(order));

            return Ok(result);
        }

        [HttpPut]
        [Route("updateOrder/{id}")]
        public async Task<IActionResult> PutOrder(int id, [FromBody] OrderInDto orderDto)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var order = _mapper.Map<Order>(orderDto);
            order.Id = id;
            order.Date = DateTime.UtcNow;
            var result = await _mediator.Send(new UpdateOrderCommand(order));

            return Ok(result);
        }

        [HttpDelete]
        [Route("deleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _mediator.Send(new DeleteOrderCommand(id));
            return Ok(result)!;
        }
    }
}

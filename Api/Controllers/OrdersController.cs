using Api.Configuration;
using AutoMapper;
using BETSoftware.Domain.Commands;
using BETSoftware.Domain.Models;
using BETSoftware.Domain.Models.Dtos;
using BETSoftware.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
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

        [HttpPost]
        [Route("getOrders")]
        public async Task<IActionResult> GetOrders(Filter filter)
        {
            if (!ModelState.IsValid) { return NotFound(); }

            var result = await _mediator.Send(new GetOrdersQuery());
            if (result == null) { return null; }
            if (filter.Page < 1) { filter.Page = 1; }
            if (filter.Size < 1) { filter.Size = 50; }
            if (filter.Page == 1) { return Ok(result.Take(filter.Size).ToList()); }

            var recordsPerPage = (filter.Page - 1) * 50;
            return Ok(result.Skip(recordsPerPage).Take(filter.Size).ToList());
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
            if (!ModelState.IsValid) { return null; }

            var order = _mapper.Map<Order>(orderDto);
            order.Date = DateTime.UtcNow;
            var result = await _mediator.Send(new InsertOrderCommand(order));

            return Ok(result);
        }

        [HttpPut]
        [Route("updateOrder/{id}")]
        public async Task<IActionResult> PutOrder(int id, [FromBody] OrderInDto orderDto)
        {
            if (!ModelState.IsValid) { return NotFound(); }

            var order = _mapper.Map<Order>(orderDto);
            order.Id = id;
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

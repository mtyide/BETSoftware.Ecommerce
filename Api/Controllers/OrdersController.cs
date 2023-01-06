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

        [HttpGet]
        [Route("getOrders")]
        public async Task<List<Order>?> GetOrders(Filter filter)
        {
            if (!ModelState.IsValid) { return null; }

            var result = await _mediator.Send(new GetOrdersQuery());
            if (result == null) { return null; }
            if (filter.Page == 1) { return result.Take(filter.Size).ToList(); }

            var recordsPerPage = (filter.Page - 1) * 50;
            return result.Skip(recordsPerPage).Take(filter.Size).ToList();
        }

        [HttpGet]
        [Route("getOrderById/{id}")]
        public async Task<Order> GetOrderById(int id) => await _mediator.Send(new GetOrderByIdQuery(id));

        [HttpPost]
        [Route("insertOrder")]
        public async Task<Order?> PostOrder([FromBody] OrderInDto orderDto)
        {
            if (!ModelState.IsValid) { return null; }

            var order = _mapper.Map<Order>(orderDto);
            order.Date = DateTime.UtcNow;
            return await _mediator.Send(new InsertOrderCommand(order));
        }

        [HttpPut]
        [Route("updateOrder/{id}")]
        public async Task<Order?> PutOrder(int id, [FromBody] OrderInDto orderDto)
        {
            if (!ModelState.IsValid) { return null; }

            var model = _mediator.Send(new GetOrderByIdQuery(id));

            if (model == null) { return null; }

            var order = _mapper.Map<Order>(orderDto);
            order.Id = id;
            return await _mediator.Send(new UpdateOrderCommand(order));
        }

        [HttpDelete]
        [Route("deleteOrder/{id}")]
        public async Task<Order?> DeleteOrder(int id)
        {
            if (!ModelState.IsValid) { return null; }

            var order = _mediator.Send(new GetOrderByIdQuery(id));

            if (order == null) { return null; }

            order.Result.Id = id;
            return await _mediator.Send(new DeleteOrderCommand(order.Result));
        }
    }
}

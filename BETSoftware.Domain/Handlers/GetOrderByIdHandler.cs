using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using BETSoftware.Domain.Queries;
using MediatR;

namespace BETSoftware.Domain.Handlers
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        private readonly IOrderService _service;
        public GetOrderByIdHandler(IOrderService service)
        {
            _service = service;
        }
        public Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken) => _service.Get(request.Id);
    }
}

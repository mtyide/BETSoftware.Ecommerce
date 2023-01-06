using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using BETSoftware.Domain.Queries;
using MediatR;

namespace BETSoftware.Domain.Handlers
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, List<Order>>
    {
        private readonly IOrderService _service;
        public GetOrdersHandler(IOrderService service)
        {
            _service = service;
        }

        public Task<List<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken) => _service.GetAll();
    }
}

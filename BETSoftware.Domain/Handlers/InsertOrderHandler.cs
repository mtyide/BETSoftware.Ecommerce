using BETSoftware.Domain.Commands;
using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Handlers
{
    public class InsertOrderHandler : IRequestHandler<InsertOrderCommand, Order>
    {
        private readonly IOrderService _service;
        public InsertOrderHandler(IOrderService service)
        {
            _service = service;
        }
        public Task<Order> Handle(InsertOrderCommand request, CancellationToken cancellationToken) => _service.Insert(request.Order);
    }
}

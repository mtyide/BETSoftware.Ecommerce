using BETSoftware.Domain.Commands;
using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Handlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Order>
    {
        private readonly IOrderService _service;
        public DeleteOrderHandler(IOrderService service)
        {
            _service = service;
        }

        public Task<Order> Handle(DeleteOrderCommand request, CancellationToken cancellationToken) => _service.Delete(request.Order);
    }
}

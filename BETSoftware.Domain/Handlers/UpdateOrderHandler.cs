using BETSoftware.Domain.Commands;
using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Handlers
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Order>
    {
        private readonly IOrderService _service;
        public UpdateOrderHandler(IOrderService service)
        {
            _service = service;
        }

        public Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken) => _service.Update(request.Order);
    }
}

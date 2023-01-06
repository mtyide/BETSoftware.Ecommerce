using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Commands
{
    public record UpdateOrderCommand(Order Order) : IRequest<Order>;
}

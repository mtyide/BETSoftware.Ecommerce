using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Commands
{
    public record DeleteOrderCommand(Order Order) : IRequest<Order>;
}

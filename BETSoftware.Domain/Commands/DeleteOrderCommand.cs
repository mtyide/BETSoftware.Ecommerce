using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Commands
{
    public record DeleteOrderCommand(int Id) : IRequest<Order>;
}

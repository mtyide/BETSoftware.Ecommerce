using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Commands
{
    public record InsertOrderCommand(Order Order) : IRequest<Order>;
}

using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Queries
{
    public record GetOrderByIdQuery(int Id) : IRequest<Order>;
}

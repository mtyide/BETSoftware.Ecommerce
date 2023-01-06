using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Queries
{
    public record GetOrdersQuery : IRequest<List<Order>>;
}

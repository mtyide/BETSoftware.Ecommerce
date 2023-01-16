using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Queries
{
    public record GetActiveProductsQuery() : IRequest<List<Product>>;
}

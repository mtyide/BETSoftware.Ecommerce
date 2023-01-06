using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Queries
{
    public record GetProductsQuery() : IRequest<List<Product>>;
}

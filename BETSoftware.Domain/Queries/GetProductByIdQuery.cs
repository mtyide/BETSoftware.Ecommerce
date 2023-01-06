using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<Product>;
}

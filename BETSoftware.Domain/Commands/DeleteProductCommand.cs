using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Commands
{
    public record DeleteProductCommand(Product Product) : IRequest<Product>;
}

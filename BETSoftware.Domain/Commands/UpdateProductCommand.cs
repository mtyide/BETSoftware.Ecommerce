using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Commands
{
    public record UpdateProductCommand(Product Product) : IRequest<Product>;
}

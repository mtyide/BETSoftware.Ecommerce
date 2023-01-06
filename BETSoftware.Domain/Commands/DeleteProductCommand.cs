using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Commands
{
    public record DeleteProductCommand(int Id) : IRequest<Product>;
}

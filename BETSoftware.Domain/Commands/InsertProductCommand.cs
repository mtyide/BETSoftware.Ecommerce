using BETSoftware.Domain.Models;
using BETSoftware.Domain.Models.Dtos;
using MediatR;

namespace BETSoftware.Domain.Commands
{
    public record InsertProductCommand(Product Product) : IRequest<Product>;
}

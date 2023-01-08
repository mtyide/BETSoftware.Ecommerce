using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Commands
{
    public record InsertLoginCommand(Login Login) : IRequest<Login>;
}

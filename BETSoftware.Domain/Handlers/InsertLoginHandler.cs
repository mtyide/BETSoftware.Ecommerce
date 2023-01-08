using BETSoftware.Domain.Commands;
using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Handlers
{
    public class InsertLoginHandler : IRequestHandler<InsertLoginCommand, Login>
    {
        private readonly ILoginService _service;
        public InsertLoginHandler(ILoginService service)
        {
            _service = service;
        }

        public Task<Login> Handle(InsertLoginCommand request, CancellationToken cancellationToken)
        {
            return _service.Create(request.Login);
        }
    }
}

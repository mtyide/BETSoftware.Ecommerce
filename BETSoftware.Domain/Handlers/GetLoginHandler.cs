using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using BETSoftware.Domain.Queries;
using MediatR;

namespace BETSoftware.Domain.Handlers
{
    public class GetLoginHandler : IRequestHandler<GetLoginQuery, Login>
    {
        private readonly ILoginService _service;
        public GetLoginHandler(ILoginService service)
        {
            _service = service;
        }

        public Task<Login> Handle(GetLoginQuery request, CancellationToken cancellationToken)
        {
            return _service.GetLogin(request.Login);
        }
    }
}

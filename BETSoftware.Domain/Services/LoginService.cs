using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;

namespace BETSoftware.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repository;
        public LoginService(ILoginRepository repository)
        {
            _repository = repository;
        }

        public Task<Login> Create(Login login)
        {
            return _repository.Create(login);
        }

        public Task<Login> GetLogin(Login login)
        {
            return _repository.GetLogin(login);
        }
    }
}

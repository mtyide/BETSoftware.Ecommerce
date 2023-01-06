using BETSoftware.Domain.Models;

namespace BETSoftware.Domain.Interfaces
{
    public interface ILoginService
    {
        Task<Login> GetLogin(Login login);
    }
}

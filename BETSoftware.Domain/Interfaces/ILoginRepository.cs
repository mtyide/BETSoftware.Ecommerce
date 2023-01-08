using BETSoftware.Domain.Models;

namespace BETSoftware.Domain.Interfaces
{
    public interface ILoginRepository
    {
        Task<Login> GetLogin(Login login);
        Task<Login> Create(Login login);
        Task<int> Commit();
    }
}

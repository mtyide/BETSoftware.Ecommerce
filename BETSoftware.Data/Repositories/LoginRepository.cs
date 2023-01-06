using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BETSoftware.Data.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DbSet<Login> _users;
        public LoginRepository(Storage storage)
        {
            _users = storage.Set<Login>();
        }
        public Task<Login> GetLogin(Login login)
        {
            var result = _users.Where(x => x.Username!.Equals(login.Username)
                                                    && x.Password!.Equals(login.Password)).FirstOrDefaultAsync();
            return result!;
        }
    }
}

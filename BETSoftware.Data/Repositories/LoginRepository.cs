using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BETSoftware.Data.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DbSet<Login> _users;
        private readonly Storage _storage;
        public LoginRepository(Storage storage)
        {
            _users = storage.Set<Login>();
            _storage = storage;
        }

        public async Task<int> Commit()
        {
            return await _storage.SaveChangesAsync();
        }

        public async Task<Login> Create(Login login)
        {
            _users.Add(login);
            await Commit();

            return login;
        }

        public Task<Login> GetLogin(Login login)
        {
            var result = _users.Where(x => x.Username!.Equals(login.Username)
                                                    && x.Password!.Equals(login.Password)).FirstOrDefaultAsync();
            return result!;
        }
    }
}

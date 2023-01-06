using BETSoftware.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BETSoftware.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly Storage _storage;
        protected DbSet<T> _entity;
        public GenericRepository(Storage storage)
        {
            _storage = storage;
            _entity = _storage.Set<T>();
        }

        public async Task<int> Commit()
        {
            return await _storage.SaveChangesAsync();
        }

        public async Task<T> Delete(T entity)
        {
            _entity.Update(entity);
            await Commit();

            return entity;
        }

        public async Task<T?> Get(int id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _entity.ToListAsync();
        }

        public async Task<T> Insert(T entity)
        {
            _entity.Add(entity);
            await Commit();

            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _entity.Update(entity);
            await Commit();

            return entity;
        }
    }
}

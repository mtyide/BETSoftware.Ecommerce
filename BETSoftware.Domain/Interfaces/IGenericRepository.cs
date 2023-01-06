namespace BETSoftware.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Insert(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
        Task<T?> Get(int id);
        Task<int> Commit();
    }
}

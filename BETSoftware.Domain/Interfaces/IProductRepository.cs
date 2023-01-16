using BETSoftware.Domain.Models;

namespace BETSoftware.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> Delete(int id);
        Task<Product> Get(int id);
        Task<List<Product>> GetAll();
        Task<Product> Insert(Product product);
        Task<Product> Update(Product product);
        Task<int> Commit();
        Task<List<Product>> GetActive();
    }
}

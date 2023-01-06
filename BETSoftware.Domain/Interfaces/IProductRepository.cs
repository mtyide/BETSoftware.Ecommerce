using BETSoftware.Domain.Models;

namespace BETSoftware.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> Delete(Product product);
        Task<Product> Get(int id);
        Task<List<Product>> GetAll();
        Task<Product> Insert(Product product);
        Task<Product> Update(Product product);
        Task<int> Commit();
    }
}

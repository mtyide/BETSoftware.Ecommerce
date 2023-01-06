using BETSoftware.Domain.Models;

namespace BETSoftware.Domain.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<Product> Insert(Product product);
        Task<Product> Update(Product product);
        Task<Product> Delete(int id);
        Task<Product> Get(int id);
    }
}

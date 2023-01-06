using BETSoftware.Domain.Models;

namespace BETSoftware.Domain.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<Product> Insert(Product product);
        Task<Product> Update(Product product);
        Task<Product> Delete(Product product);
        Task<Product> Get(int id);
    }
}

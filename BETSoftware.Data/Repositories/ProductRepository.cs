using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BETSoftware.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Storage _storage;
        private readonly DbSet<Product> _products;
        public ProductRepository(Storage storage)
        {
            _storage = storage;
            _products = storage.Set<Product>();
        }

        public async Task<int> Commit()
        {
            return await _storage.SaveChangesAsync();
        }

        public async Task<Product> Delete(Product product)
        {
            product.Active = false;
            _products.Update(product);
            await Commit();

            return product;
        }

        public Task<Product> Get(int id)
        {
            var result = _products.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            return result!;
        }

        public Task<List<Product>> GetAll() => _products.ToListAsync();

        public async Task<Product> Insert(Product product)
        {
            _products.Add(product);
            await Commit();

            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _products.Update(product);
            await Commit();

            return product;
        }
    }
}

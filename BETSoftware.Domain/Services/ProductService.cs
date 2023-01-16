﻿using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;

namespace BETSoftware.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository reposirtory)
        {
            _repository = reposirtory;
        }

        public Task<Product> Delete(int id)
        {
            return _repository.Delete(id);
        }

        public Task<Product> Get(int id)
        {
            return _repository.Get(id);
        }

        public Task<List<Product>> GetActive()
        {
            return _repository.GetActive();
        }

        public Task<List<Product>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<Product> Insert(Product product)
        {
            return _repository.Insert(product);
        }

        public Task<Product> Update(Product product)
        {
            return _repository.Update(product);
        }
    }
}

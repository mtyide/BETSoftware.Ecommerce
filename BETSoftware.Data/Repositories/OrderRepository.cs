using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BETSoftware.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Storage _storage;
        private readonly DbSet<Order> _orders;
        public OrderRepository(Storage storage)
        {
            _storage = storage;
            _orders = storage.Set<Order>();
        }

        public async Task<int> Commit()
        {
            return await _storage.SaveChangesAsync();
        }

        public async Task<Order> Delete(int id)
        {
            var entity = _orders.Find(id);
            if (entity == null) return null!;

            entity.Active = false;
            _orders.Update(entity);
            await Commit();

            return entity;
        }

        public Task<Order> Get(int id)
        {
            var result = _orders.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            return result!;
        }

        public Task<List<Order>> GetAll() => _orders.ToListAsync();

        public async Task<Order> Insert(Order order)
        {
            _orders.Add(order);
            await Commit();

            return order;
        }

        public async Task<Order> Update(Order order)
        {
            var entity = _orders.Find(order.Id);
            if (entity == null) { return null!; }

            _orders.Update(order);
            await Commit();

            return order;
        }
    }
}

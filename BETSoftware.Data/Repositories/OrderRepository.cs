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
            await Commit();

            return entity;
        }

        public Task<Order> Get(int id)
        {
            var result = _orders.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            return result!;
        }

        public Task<List<Order>> GetAll() => _orders.Include(x => x.Lines).ToListAsync();

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

            entity.Active = order.Active;
            entity.CustomerId = order.CustomerId;
            entity.LastModified= order.LastModified;
            
            if (order.Date != null) { entity.Date = order.Date; }
            if (order.Lines != null)
            {
                var lines = new List<OrderLines>();
                foreach (var line in order.Lines)
                {
                    if (line.ProductId == null || line.Qty == null) continue;
                    lines.Add(line);
                }
                entity.Lines = lines;
            }
            if (!string.IsNullOrEmpty(order.ShippingAddress)) { entity.ShippingAddress = order.ShippingAddress; }
            entity.ShippingRequired = order.ShippingRequired;
            if (order.ShippingTax != 0) { entity.ShippingTax = order.ShippingTax; }
            if (order.TotalAmount != 0) { entity.TotalAmount = order.TotalAmount; }

            await Commit();

            return entity;
        }
    }
}

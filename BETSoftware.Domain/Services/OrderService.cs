using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;

namespace BETSoftware.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<Order> Delete(int id)
        {
            return _orderRepository.Delete(id);
        }

        public Task<Order> Get(int id)
        {
            return _orderRepository.Get(id);
        }

        public Task<List<Order>> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public Task<Order> Insert(Order order)
        {
            return _orderRepository.Insert(order);
        }

        public Task<Order> Update(Order order)
        {
            return _orderRepository.Update(order);
        }
    }
}

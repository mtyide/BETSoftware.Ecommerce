using BETSoftware.Domain.Models;

namespace BETSoftware.Domain.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAll();
        Task<Order> Insert(Order order);
        Task<Order> Update(Order order);
        Task<Order> Delete(int id);
        Task<Order> Get(int id);
    }
}

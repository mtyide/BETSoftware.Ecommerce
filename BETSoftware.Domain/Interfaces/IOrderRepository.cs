using BETSoftware.Domain.Models;

namespace BETSoftware.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> Delete(Order order);
        Task<Order> Get(int id);
        Task<Order> Update(Order order);
        Task<List<Order>> GetAll();
        Task<Order> Insert(Order order);
        Task<int> Commit();
    }
}

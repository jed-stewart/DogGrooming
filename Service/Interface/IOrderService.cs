using Data.Domain;

namespace Shared.Interface
{
    public interface IOrderService
    {
        Task<Order> AddAsync(Order order);
        Task<Order> UpdateAsync(Order order);
    }
}
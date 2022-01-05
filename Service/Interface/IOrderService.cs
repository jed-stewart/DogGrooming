using Data.Domain;

namespace Shared.Interface
{
    public interface IOrderService
    {
        Task<Order> Add(Order order);
        Task<Order> Update(Order order);
    }
}
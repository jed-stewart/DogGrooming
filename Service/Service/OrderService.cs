using Data;
using Data.Domain;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Interface;

namespace Shared.Service
{
    public class OrderService : IOrderService
    {
        public readonly OrderContext _orderContext;
        public OrderService(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }


        public async Task<Order> AddAsync(Order order)
        {
            await _orderContext.Order.AddAsync(order);
            await _orderContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            var entry = _orderContext.Order.Update(order);
            await _orderContext.SaveChangesAsync();
            await entry.ReloadAsync();
            return order;
        }
    }
}
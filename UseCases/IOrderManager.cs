using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public interface IOrderManager
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<CreateOrderResult> AddAsync(Order order, IEnumerable<OrderItem> orderItems);
        Task<AtomicTaskResult> DeleteAsync(int id);
        Task<IEnumerable<OrderItem>> GetOrderItemsForOrderAsync(int orderId);
        Task UpdateAsync(Order order);
        Task ChangeOrderStatus(int id, OrderStatus status);
    }
}

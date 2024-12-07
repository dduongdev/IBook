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
        Task<CreateOrderResult> AddAsync(Order order);
        Task<AtomicTaskResult> DeleteAsync(int id);
        Task<Order?> GetByIdAsync(int id);
        Task ChangeStatus(int id, OrderStatus status);
        Task ChangePaymentStatus(int id, PaymentStatus paymentStatus);
    }
}

using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DTO;

namespace UseCases
{
    public interface IOrderManager
    {
        Task<IEnumerable<OrderDTO>> GetAllAsync();
        Task<CreateOrderResult> AddAsync(OrderDTO orderDTO);
        Task<AtomicTaskResult> DeleteAsync(int id);
        Task<OrderDTO?> GetByIdAsync(int id);
        Task ChangeOrderStatus(int id, OrderStatus status);
    }
}

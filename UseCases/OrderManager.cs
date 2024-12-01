using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.UnitOfWork;

namespace UseCases
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderUnitOfWork _orderUnitOfWork;

        public OrderManager(IOrderUnitOfWork orderUnitOfWork)
        {
            _orderUnitOfWork = orderUnitOfWork;
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            return _orderUnitOfWork.OrderRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
        {
            var orders = await _orderUnitOfWork.OrderRepository.GetAllAsync();
            return orders.Where(o => o.UserId == userId);
        }

        public Task<Order?> GetByIdAsync(int id)
        {
            return _orderUnitOfWork.OrderRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Order order)
        {
            try
            {
                await _orderUnitOfWork.BeginTransactionAsync();

                foreach (var orderItem in order.OrderItems)
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DTO;
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

        public async Task<IEnumerable<OrderDTO>> GetAllAsync()
        {
            IEnumerable<OrderDTO> orderDTOs = new List<OrderDTO>();
            var orders = await _orderUnitOfWork.OrderRepository.GetAllAsync();
            foreach (var order in orders)
            {
                var orderItems = await _orderUnitOfWork.OrderItemRepository.GetByOrderIdAsync(order.Id);
                ((List<OrderDTO>)orderDTOs).Add(new OrderDTO
                {
                    Order = order,
                    OrderItems = orderItems
                });
            }
            return orderDTOs;
        }

        public async Task<CreateOrderResult> AddAsync(OrderDTO orderDTO)
        {
            try
            {
                await _orderUnitOfWork.BeginTransactionAsync();

                await _orderUnitOfWork.OrderRepository.AddAsync(orderDTO.Order);

                foreach (var item in orderDTO.OrderItems)
                {
                    item.OrderId = orderDTO.Order.Id;

                    var book = await _orderUnitOfWork.BookRepository.GetByIdAsync(item.BookId);

                    if (book == null)
                    {
                        return new CreateOrderResult(CreateOrderResultCodes.BookNotFound, string.Empty);
                    }

                    if (book.Status == EntityStatus.Suspended)
                    {
                        return new CreateOrderResult(CreateOrderResultCodes.BookIsSuspended, string.Empty);
                    }

                    if (book.Stock == 0)
                    {
                        return new CreateOrderResult(CreateOrderResultCodes.BookOutOfStock, string.Empty);
                    }

                    if (book.Stock < item.Quantity)
                    {
                        return new CreateOrderResult(CreateOrderResultCodes.BookStockTooLow, string.Empty);
                    }

                    book.Stock -= item.Quantity;
                    await _orderUnitOfWork.BookRepository.UpdateAsync(book);

                    await _orderUnitOfWork.OrderItemRepository.AddAsync(item);
                }

                await _orderUnitOfWork.SaveChangesAsync();

                return new CreateOrderResult(CreateOrderResultCodes.Success, string.Empty);
            }
            catch (Exception ex)
            {
                await _orderUnitOfWork.CancelTransactionAsync();
                return new CreateOrderResult(CreateOrderResultCodes.Error, ex.Message);
            }
        }

        public async Task<AtomicTaskResult> DeleteAsync(int id)
        {
            try
            {
                await _orderUnitOfWork.BeginTransactionAsync();

                var foundOrder = await _orderUnitOfWork.OrderRepository.GetByIdAsync(id);
                if (foundOrder == null)
                {
                    return AtomicTaskResult.NotFound;
                }

                var orderItems = await _orderUnitOfWork.OrderItemRepository.GetByOrderIdAsync(foundOrder.Id);
                foreach (var item in orderItems)
                {
                    var bookInInventory = await _orderUnitOfWork.BookRepository.GetByIdAsync(item.BookId);
                    if (bookInInventory == null)
                    {
                        throw new Exception();
                    }

                    bookInInventory.Stock += item.Quantity;
                    await _orderUnitOfWork.BookRepository.UpdateAsync(bookInInventory);
                    await _orderUnitOfWork.OrderItemRepository.DeleteAsync(item.Id);
                }

                await _orderUnitOfWork.OrderRepository.DeleteAsync(foundOrder.Id);
                await _orderUnitOfWork.SaveChangesAsync();

                return AtomicTaskResult.Success;
            }
            catch (Exception ex)
            {
                await _orderUnitOfWork.CancelTransactionAsync();
                return new AtomicTaskResult(AtomicTaskResultCodes.Error, ex.Message);
            }
        }

        public async Task<OrderDTO?> GetByIdAsync(int id)
        {
            OrderDTO? orderDTO = null;
            var foundOrder = await _orderUnitOfWork.OrderRepository.GetByIdAsync(id);
            if (foundOrder != null)
            {
                orderDTO = new OrderDTO();
                orderDTO.Order = foundOrder;
                ((List<OrderItem>)orderDTO.OrderItems).AddRange(await _orderUnitOfWork.OrderItemRepository.GetByOrderIdAsync(foundOrder.Id));
            }
            return orderDTO;
        }

        public async Task ChangeOrderStatus(int id, OrderStatus status)
        {
            var foundOrder = await _orderUnitOfWork.OrderRepository.GetByIdAsync(id);
            if (foundOrder != null)
            {
                foundOrder.Status = status;
                await _orderUnitOfWork.OrderRepository.UpdateAsync(foundOrder);
            }
        }
    }
}

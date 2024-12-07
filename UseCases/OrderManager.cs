using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.UnitOfWork;

namespace UseCases
{
    public class OrderManager
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

        public Task<Order?> GetByIdAsync(int id)
        {
            return _orderUnitOfWork.OrderRepository.GetByIdAsync(id);
        }

        public async Task<CreateOrderResult> AddAsync(Order order, IEnumerable<OrderItem> orderItems)
        {
            try
            {
                await _orderUnitOfWork.BeginTransactionAsync();

                await _orderUnitOfWork.OrderRepository.AddAsync(order);

                foreach (var item in orderItems)
                {
                    item.OrderId = order.Id;

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
    }
}

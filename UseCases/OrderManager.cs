using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.TaskResults;
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

        public async Task<CreateOrderResult> AddAsync(Order order)
        {
            try
            {
                await _orderUnitOfWork.BeginTransactionAsync();

                await _orderUnitOfWork.OrderRepository.AddAsync(order);

                foreach (var item in order.OrderItems)
                {
                    item.OrderId = order.Id;

                    var book = await _orderUnitOfWork.BookRepository.GetByIdAsync(item.BookId);

                    if (book == null)
                    {
                        return new CreateOrderResult(CreateOrderResultCodes.BookNotFound, $"The book with id {item.BookId} is not found.");
                    }

                    if (book.Status == EntityStatus.Suspended)
                    {
                        return new CreateOrderResult(CreateOrderResultCodes.BookIsSuspended, $"{book.Title} book is suspended.");
                    }

                    if (book.Stock == 0)
                    {
                        return new CreateOrderResult(CreateOrderResultCodes.BookOutOfStock, $"{book.Title} book is out of stock.");
                    }

                    if (book.Stock < item.Quantity)
                    {
                        return new CreateOrderResult(CreateOrderResultCodes.BookStockTooLow, $"{book.Title} book is too low.");
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

                foreach (var item in foundOrder.OrderItems)
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

        public Task<Order?> GetByIdAsync(int id)
        {
            return _orderUnitOfWork.OrderRepository.GetByIdAsync(id);
        }

        public async Task ChangeStatus(int id, OrderStatus status)
        {
            var foundOrder = await _orderUnitOfWork.OrderRepository.GetByIdAsync(id);
            if (foundOrder != null)
            {
                foundOrder.Status = status;
                await _orderUnitOfWork.OrderRepository.UpdateAsync(foundOrder);
            }
        }

        public async Task ChangePaymentStatus(int id, PaymentStatus paymentStatus)
        {
            var foundOrder = await _orderUnitOfWork.OrderRepository.GetByIdAsync(id);
            if (foundOrder != null)
            {
                foundOrder.PaymentStatus = paymentStatus;
                await _orderUnitOfWork.OrderRepository.UpdateAsync(foundOrder);
            }
        }
    }
}

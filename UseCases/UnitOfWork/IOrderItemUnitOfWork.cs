using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;

namespace UseCases.UnitOfWork
{
    public interface IOrderItemUnitOfWork : IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
    }
}

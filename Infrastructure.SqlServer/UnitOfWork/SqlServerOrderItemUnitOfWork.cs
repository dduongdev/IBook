using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;
using UseCases.UnitOfWork;

namespace Infrastructure.SqlServer.UnitOfWork
{
    public class SqlServerOrderItemUnitOfWork : IOrderItemUnitOfWork
    {
        public IBookRepository BookRepository => throw new NotImplementedException();

        public IOrderRepository OrderRepository => throw new NotImplementedException();

        public IOrderItemRepository OrderItemRepository => throw new NotImplementedException();

        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task CancelTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}

using AutoMapper;
using Infrastructure.SqlServer.Repositories.SqlServer.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;

namespace Infrastructure.SqlServer.Repositories
{
    public class SqlServerOrderItemRepository : IOrderItemRepository
    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public SqlServerOrderItemRepository(BookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Entities.OrderItem orderItem)
        {
            var staredOrderItem = _mapper.Map<OrderItem>(orderItem);
            await _context.OrderItems.AddAsync(staredOrderItem);
            await _context.SaveChangesAsync();
            orderItem.Id = staredOrderItem.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var storedOrderItem = await _context.OrderItems.FirstOrDefaultAsync(_ => _.Id == id);
            if (storedOrderItem != null)
            {
                _context.OrderItems.Remove(storedOrderItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Entities.OrderItem>> GetAllAsync()
        {
            var storedOrderItems = await _context.OrderItems.ToListAsync();
            return _mapper.Map<IEnumerable<Entities.OrderItem>>(storedOrderItems);
        }

        public async Task<Entities.OrderItem?> GetByIdAsync(int id)
        {
            var storedOrderItem = await _context.OrderItems.FirstOrDefaultAsync(_ => _.Id == id);
            return _mapper.Map<Entities.OrderItem?>(storedOrderItem);
        }

        public async Task<IEnumerable<Entities.OrderItem>> GetByOrderIdAsync(int orderId)
        {
            var storedOrderItems = await _context.OrderItems.ToListAsync();
            return _mapper.Map<IEnumerable<Entities.OrderItem>>(storedOrderItems.Where(_ => _.OrderId == orderId));
        }

        public async Task UpdateAsync(Entities.OrderItem orderItem)
        {
            var storedOrderItem = await _context.OrderItems.FirstOrDefaultAsync(_ => _.Id == orderItem.Id);
            if (storedOrderItem != null)
            {
                _mapper.Map(orderItem, storedOrderItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}

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
    public class SqlServerCartItemRepository : ICartItemRepository
    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public SqlServerCartItemRepository(BookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Entities.CartItem cartItem)
        {
            var staredCartItem = _mapper.Map<CartItem>(cartItem);
            await _context.CartItems.AddAsync(staredCartItem);
            await _context.SaveChangesAsync();
            cartItem.Id = staredCartItem.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var storedCartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.Id == id);
            if (storedCartItem != null)
            {
                _context.CartItems.Remove(storedCartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Entities.CartItem>> GetAllAsync()
        {
            var storedCartItems = await _context.CartItems.ToListAsync();
            return _mapper.Map<IEnumerable<Entities.CartItem>>(storedCartItems);
        }

        public async Task<IEnumerable<Entities.CartItem>> GetByCartIdAsync(int cartId)
        {
            var storedCartItems = await _context.CartItems.ToListAsync();
            return _mapper.Map<IEnumerable<Entities.CartItem>>(storedCartItems.Where(ci => ci.CartId == cartId));
        }

        public async Task<Entities.CartItem?> GetByIdAsync(int id)
        {
            var storedCartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.Id == id);
            return _mapper.Map<Entities.CartItem?>(storedCartItem);
        }

        public async Task UpdateAsync(Entities.CartItem cartItem)
        {
            var storedCartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.Id == cartItem.Id);
            if (storedCartItem != null)
            {
                _mapper.Map(cartItem, storedCartItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}

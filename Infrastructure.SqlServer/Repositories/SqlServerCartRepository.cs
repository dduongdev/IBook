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
    public class SqlServerCartRepository : ICartRepository
    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public SqlServerCartRepository(BookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Entities.Cart cart)
        {
            var staredCart = _mapper.Map<Cart>(cart);
            await _context.Carts.AddAsync(staredCart);
            await _context.SaveChangesAsync();
            cart.Id = staredCart.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var storedCart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);
            if (storedCart != null)
            {
                _context.Carts.Remove(storedCart);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Entities.Cart>> GetAllAsync()
        {
            var storedCarts = await _context.Carts.ToListAsync();
            return _mapper.Map<IEnumerable<Entities.Cart>>(storedCarts);
        }

        public async Task<Entities.Cart?> GetByIdAsync(int id)
        {
            var storedCart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<Entities.Cart?>(storedCart);
        }

        public async Task UpdateAsync(Entities.Cart cart)
        {
            var storedCart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == cart.Id);
            if (storedCart != null)
            {
                _mapper.Map(cart, storedCart);
                await _context.SaveChangesAsync();
            }
        }
    }
}

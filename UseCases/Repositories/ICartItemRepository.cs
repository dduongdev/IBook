using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Repositories
{
    public interface ICartItemRepository
    {
        Task<IEnumerable<CartItem>> GetAllAsync();
        Task<CartItem?> GetByIdAsync(int id);
        Task<IEnumerable<CartItem>> GetByCartIdAsync(int cartId);
        Task AddAsync(CartItem cartItem);
        Task UpdateAsync(CartItem cartItem);
        Task DeleteAsync(int id);
    }
}

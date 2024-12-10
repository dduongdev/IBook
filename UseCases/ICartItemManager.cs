using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public interface ICartItemManager
    {
        Task<IEnumerable<CartItem>> GetAllAsync();
        Task<IEnumerable<CartItem>> GetByCartIdAsync(int cartId);
        Task<CartItem?> GetByIdAsync(int id);
        Task AddAsync(CartItem cartItem);
        Task UpdateAsync(CartItem cartItem);
        Task DeleteAsync(int id);
    }
}

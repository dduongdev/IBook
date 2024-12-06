using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;

namespace UseCases
{
    public class CartItemManager
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartItemManager(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public Task<IEnumerable<CartItem>> GetAllAsync()
        {
            return _cartItemRepository.GetAllAsync();
        }

        public Task<IEnumerable<CartItem>> GetByCartIdAsync(int cartId)
        {
            return _cartItemRepository.GetByCartIdAsync(cartId);
        }

        public Task<CartItem?> GetByIdAsync(int id)
        {
            return _cartItemRepository.GetByIdAsync(id);
        }

        public Task AddAsync(CartItem cartItem) 
        {
            return _cartItemRepository.AddAsync(cartItem);
        }

        public Task UpdateAsync(CartItem cartItem)
        {
            return _cartItemRepository.UpdateAsync(cartItem);
        }

        public Task DeleteAsync(int id)
        {
            return _cartItemRepository.DeleteAsync(id);
        }
    }
}

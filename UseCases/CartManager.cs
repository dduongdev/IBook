using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;

namespace UseCases
{
    public class CartManager
    {
        private readonly ICartRepository _cartRepository;

        public CartManager(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Task<IEnumerable<Cart>> GetAllAsync()
        {
            return _cartRepository.GetAllAsync();
        }

        public Task AddAsync(Cart cart)
        {
            return _cartRepository.AddAsync(cart);
        }

        public Task UpdateAsync(Cart cart)
        {
            return _cartRepository.UpdateAsync(cart);
        }

        public Task<Cart?> GetByIdAsync(int id)
        {
            return _cartRepository.GetByIdAsync(id);
        }
    }
}

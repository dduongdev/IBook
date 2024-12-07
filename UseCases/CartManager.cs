using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;

namespace UseCases
{
    public class CartManager : ICartManager
    {
        private readonly ICartRepository _cartRepository;

        public CartManager(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Task<Cart?> GetByIdAsync(int id)
        {
            return _cartRepository.GetByIdAsync(id);
        }

        public async Task<Cart?> GetByUserIdAsync(int userId)
        {
            var carts = await _cartRepository.GetAllAsync();
            return carts.FirstOrDefault(_ => _.UserId == userId);
        }
    }
}

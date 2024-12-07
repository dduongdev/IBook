using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;
using UseCases.UnitOfWork;

namespace UseCases
{
    public class UserManager : IUserManager
    {
        private readonly IUserUnitOfWork _userUnitOfWork;

        public UserManager(IUserUnitOfWork userUnitOfWork)
        {
            _userUnitOfWork = userUnitOfWork;
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return _userUnitOfWork.UserRepository.GetAllAsync();
        }

        public Task<User?> GetByIdAsync(int id)
        {
            return _userUnitOfWork.UserRepository.GetByIdAsync(id);
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            return _userUnitOfWork.UserRepository.GetByUsernameAsync(username);
        }

        public async Task<AtomicTaskResult> AddAsync(User user)
        {
            try
            {
                await _userUnitOfWork.BeginTransactionAsync();

                await _userUnitOfWork.UserRepository.AddAsync(user);

                var staredCart = new Cart
                {
                    UserId = user.Id
                };

                await _userUnitOfWork.CartRepository.AddAsync(staredCart);

                await _userUnitOfWork.SaveChangesAsync();

                return AtomicTaskResult.Success;
            }
            catch (Exception ex)
            {
                await _userUnitOfWork.CancelTransactionAsync();
                return new AtomicTaskResult(AtomicTaskResultCodes.Error, ex.Message);
            }
        }

        public Task UpdateAsync(User user)
        {
            return _userUnitOfWork.UserRepository.UpdateAsync(user);
        }

        public async Task<AtomicTaskResult> SuspendAsync(int id)
        {
            try
            {
                var foundUser = await _userUnitOfWork.UserRepository.GetByIdAsync(id);
                if (foundUser == null)
                {
                    return AtomicTaskResult.NotFound;
                }

                foundUser.Status = EntityStatus.Suspended;

                await _userUnitOfWork.UserRepository.UpdateAsync(foundUser);

                return AtomicTaskResult.Success;
            }
            catch (Exception ex)
            {
                return new AtomicTaskResult(AtomicTaskResultCodes.Error, ex.Message);
            }
        }

        public async Task<AtomicTaskResult> ActivateAsync(int id)
        {
            try
            {
                var foundUser = await _userUnitOfWork.UserRepository.GetByIdAsync(id);

                if (foundUser == null)
                {
                    return AtomicTaskResult.NotFound;
                }

                foundUser.Status = EntityStatus.Active;
                await _userUnitOfWork.UserRepository.UpdateAsync(foundUser);

                return AtomicTaskResult.Success;
            }
            catch (Exception ex)
            {
                return new AtomicTaskResult(AtomicTaskResultCodes.Error, ex.Message);
            }
        }
    }
}

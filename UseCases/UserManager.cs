using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;

namespace UseCases
{
    public class UserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public Task<User?> GetByIdAsync(int id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public Task AddAsync(User user)
        {
            return _userRepository.AddAsync(user);
        }

        public Task UpdateAsync(User user)
        {
            return _userRepository.UpdateAsync(user);
        }

        public async Task<AtomicTaskResult> SuspendAsync(int id)
        {
            try
            {
                var foundUser = await _userRepository.GetByIdAsync(id);
                if (foundUser == null)
                {
                    return AtomicTaskResult.NotFound;
                }

                foundUser.Status = EntityStatus.Suspended;

                await _userRepository.UpdateAsync(foundUser);

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
                var foundUser = await _userRepository.GetByIdAsync(id);

                if (foundUser == null)
                {
                    return AtomicTaskResult.NotFound;
                }

                foundUser.Status = EntityStatus.Active;
                await _userRepository.UpdateAsync(foundUser);

                return AtomicTaskResult.Success;
            }
            catch (Exception ex)
            {
                return new AtomicTaskResult(AtomicTaskResultCodes.Error, ex.Message);
            }
        }
    }
}

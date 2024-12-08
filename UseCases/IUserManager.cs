using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.TaskResults;

namespace UseCases
{
    public interface IUserManager
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<AtomicTaskResult> AddAsync(User user);
        Task UpdateAsync(User user);
        Task<AtomicTaskResult> SuspendAsync(int id);
        Task<AtomicTaskResult> ActivateAsync(int id);
    }
}

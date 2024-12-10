using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.TaskResults;

namespace UseCases
{
    public interface IBookManager
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task<AtomicTaskResult> SuspendAsync(int id);
        Task<AtomicTaskResult> ActivateAsync(int id);
    }
}

using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Repositories
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> GetAllAsync();
        Task<Publisher?> GetByIdAsync(int id);
        Task AddAsync(Publisher publisher);
        Task UpdateAsync(Publisher publisher);
        Task DeleteAsync(int id);
    }
}

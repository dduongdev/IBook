using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;
using UseCases.TaskResults;

namespace UseCases
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _bookRepository;

        public BookManager(IBookRepository bookRepository) 
        {
            _bookRepository = bookRepository; 
        }

        public Task<IEnumerable<Book>> GetAllAsync()
        {
            return _bookRepository.GetAllAsync();
        }

        public Task<Book?> GetByIdAsync(int id)
        {
            return _bookRepository.GetByIdAsync(id);
        }

        public Task AddAsync(Book book)
        {
            return _bookRepository.AddAsync(book);
        }

        public Task UpdateAsync(Book book)
        {
            return _bookRepository.UpdateAsync(book);
        }

        public async Task<AtomicTaskResult> SuspendAsync(int id)
        {
            try
            {
                var foundBook = await _bookRepository.GetByIdAsync(id);
                if (foundBook == null)
                {
                    return AtomicTaskResult.NotFound;
                }

                foundBook.Status = EntityStatus.Suspended;
                await _bookRepository.UpdateAsync(foundBook);
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
                var foundBook = await _bookRepository.GetByIdAsync(id);
                if (foundBook == null)
                {
                    return AtomicTaskResult.NotFound;
                }

                foundBook.Status = EntityStatus.Active;
                await _bookRepository.UpdateAsync(foundBook);

                return AtomicTaskResult.Success;
            }
            catch (Exception ex)
            {
                return new AtomicTaskResult(AtomicTaskResultCodes.Error, ex.Message);
            }
        }
    }
}

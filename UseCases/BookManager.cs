using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;

namespace UseCases
{
    public class BookManager
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

        public async Task<SuspendEntityResult> SuspendAsync(int id)
        {
            try
            {
                var foundBook = await _bookRepository.GetByIdAsync(id);
                if (foundBook == null)
                {
                    return SuspendEntityResult.NotFound;
                }

                foundBook.Status = EntityStatus.Suspended;
                await _bookRepository.UpdateAsync(foundBook);
                return SuspendEntityResult.Success;
            }
            catch (Exception ex) 
            {
                return new SuspendEntityResult(SuspendEntityResultCodes.Error, ex.Message);
            }
        }
    }
}

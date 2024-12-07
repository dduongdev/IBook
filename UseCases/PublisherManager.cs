using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.UnitOfWork;

namespace UseCases
{
    public class PublisherManager : IPublisherManager
    {
        private readonly IPublisherUnitOfWork _publisherUnitOfWork;

        public PublisherManager(IPublisherUnitOfWork publisherUnitOfWork)
        {
            _publisherUnitOfWork = publisherUnitOfWork;
        }

        public Task<IEnumerable<Publisher>> GetAllAsync()
        {
            return _publisherUnitOfWork.PublisherRepository.GetAllAsync();
        }

        public Task<Publisher?> GetByIdAsync(int id)
        {
            return _publisherUnitOfWork.PublisherRepository.GetByIdAsync(id);
        }

        public Task AddAsync(Publisher publisher)
        {
            return _publisherUnitOfWork.PublisherRepository.AddAsync(publisher);
        }

        public Task UpdateAsync(Publisher publisher)
        {
            return _publisherUnitOfWork.PublisherRepository.UpdateAsync(publisher);
        }

        public async Task<AtomicTaskResult> SuspendAsync(int id)
        {
            try
            {
                await _publisherUnitOfWork.BeginTransactionAsync();

                var foundPublisher = await _publisherUnitOfWork.PublisherRepository.GetByIdAsync(id);

                if (foundPublisher == null)
                {
                    return AtomicTaskResult.NotFound;
                }

                foundPublisher.Status = EntityStatus.Suspended;

                var books = await _publisherUnitOfWork.BookRepository.GetAllAsync();
                foreach (var book in books)
                {
                    if (book.PublisherId == foundPublisher.Id)
                    {
                        book.Status = EntityStatus.Suspended;
                        await _publisherUnitOfWork.BookRepository.UpdateAsync(book);
                    }
                }

                await _publisherUnitOfWork.PublisherRepository.UpdateAsync(foundPublisher);
                await _publisherUnitOfWork.SaveChangesAsync();

                return AtomicTaskResult.Success;
            }
            catch (Exception ex)
            {
                await _publisherUnitOfWork.CancelTransactionAsync();
                return new AtomicTaskResult(AtomicTaskResultCodes.Error, ex.Message);
            }
        }

        public async Task<AtomicTaskResult> ActivateAsync(int id)
        {
            try
            {
                await _publisherUnitOfWork.BeginTransactionAsync();

                var foundPublisher = await _publisherUnitOfWork.PublisherRepository.GetByIdAsync(id);
                if (foundPublisher == null)
                {
                    return AtomicTaskResult.NotFound;
                }

                foundPublisher.Status = EntityStatus.Active;

                var books = await _publisherUnitOfWork.BookRepository.GetAllAsync();
                foreach (var book in books)
                {
                    if (book.PublisherId == foundPublisher.Id)
                    {
                        book.Status = EntityStatus.Active;
                        await _publisherUnitOfWork.BookRepository.UpdateAsync(book);
                    }
                }

                await _publisherUnitOfWork.PublisherRepository.UpdateAsync(foundPublisher);
                await _publisherUnitOfWork.SaveChangesAsync();

                return AtomicTaskResult.Success;
            }
            catch (Exception ex)
            {
                await _publisherUnitOfWork.CancelTransactionAsync();
                return new AtomicTaskResult(AtomicTaskResultCodes.Error, ex.Message);
            }
        }
    }
}

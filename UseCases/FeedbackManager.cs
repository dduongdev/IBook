using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;

namespace UseCases
{
    public class FeedbackManager
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackManager(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public Task<IEnumerable<Feedback>> GetAllAsync()
        {
            return _feedbackRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Feedback>> GetByBookIdAsync(int bookId)
        {
            var feedbacks = await _feedbackRepository.GetAllAsync();
            return feedbacks.Where(_ => _.BookId == bookId);
        }

        public Task<Feedback?> GetByIdAsync(int id)
        {
            return _feedbackRepository.GetByIdAsync(id);
        }

        public Task AddAsync(Feedback feedback)
        {
            return _feedbackRepository.AddAsync(feedback);
        }

        public Task UpdateAsync(Feedback feedback)
        {
            return _feedbackRepository.UpdateAsync(feedback);
        }

        public Task DeleteAsync(int id)
        {
            return _feedbackRepository.DeleteAsync(id);
        }
    }
}

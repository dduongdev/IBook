using AutoMapper;
using Infrastructure.SqlServer.Repositories.SqlServer.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;

namespace Infrastructure.SqlServer.Repositories
{
    public class SqlServerFeedbackRepository : IFeedbackRepository
    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public SqlServerFeedbackRepository(BookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Entities.Feedback feedback)
        {
            var staredFeedback = _mapper.Map<Feedback>(feedback);
            await _context.Feedbacks.AddAsync(staredFeedback);
            await _context.SaveChangesAsync();
            feedback.Id = staredFeedback.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var storedFeedback = await _context.Feedbacks.FirstOrDefaultAsync(_ => _.Id == id);
            if (storedFeedback != null)
            {
                _context.Feedbacks.Remove(storedFeedback);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Entities.Feedback>> GetAllAsync()
        {
            var storedFeedbacks = await _context.Feedbacks.ToListAsync();
            return _mapper.Map<IEnumerable<Entities.Feedback>>(storedFeedbacks);
        }

        public async Task<Entities.Feedback?> GetByIdAsync(int id)
        {
            var storedFeedback = await _context.Feedbacks.FirstOrDefaultAsync(_ => _.Id == id);
            return _mapper.Map<Entities.Feedback?>(storedFeedback);
        }

        public async Task UpdateAsync(Entities.Feedback feedback)
        {
            var storedFeedback = await _context.Feedbacks.FirstOrDefaultAsync(_ => _.Id == feedback.Id);
            if (storedFeedback != null)
            {
                _mapper.Map(feedback, storedFeedback);
                await _context.SaveChangesAsync();
            }
        }
    }
}

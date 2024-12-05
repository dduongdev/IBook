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
    public class SqlServerPublisherRepository : IPublisherRepository
    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public SqlServerPublisherRepository(BookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Entities.Publisher publisher)
        {
            var staredPublisher = _mapper.Map<Publisher>(publisher);
            await _context.Publishers.AddAsync(staredPublisher);
            await _context.SaveChangesAsync();
            publisher.Id = staredPublisher.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var storedPublisher = await _context.Publishers.FirstOrDefaultAsync(_ => _.Id == id);
            if (storedPublisher != null)
            {
                _context.Publishers.Remove(storedPublisher);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Entities.Publisher>> GetAllAsync()
        {
            var storedPublishers = await _context.Publishers.ToListAsync();
            return _mapper.Map<IEnumerable<Entities.Publisher>>(storedPublishers);
        }

        public async Task<Entities.Publisher?> GetByIdAsync(int id)
        {
            var storedPublisher = await _context.Publishers.FirstOrDefaultAsync(_ => _.Id == id);
            return _mapper.Map<Entities.Publisher?>(storedPublisher);
        }

        public async Task UpdateAsync(Entities.Publisher publisher)
        {
            var storedPublisher = await _context.Publishers.FirstOrDefaultAsync(_ => _.Id == publisher.Id);
            if (storedPublisher != null)
            {
                _mapper.Map(publisher, storedPublisher);
                await _context.SaveChangesAsync();
            }
        }
    }
}

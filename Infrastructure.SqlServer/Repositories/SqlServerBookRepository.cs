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
    public class SqlServerBookRepository : IBookRepository
    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public SqlServerBookRepository(BookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Entities.Book book)
        {
            var staredBook = _mapper.Map<Book>(book);
            await _context.Books.AddAsync(staredBook);
            await _context.SaveChangesAsync();
            book.Id = staredBook.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var storedBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (storedBook != null)
            {
                _context.Books.Remove(storedBook);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Entities.Book>> GetAllAsync()
        {
            var storedBooks = await _context.Books.ToListAsync();
            return _mapper.Map<IEnumerable<Entities.Book>>(storedBooks);
        }

        public async Task<Entities.Book?> GetByIdAsync(int id)
        {
            var storedBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            return _mapper.Map<Entities.Book?>(storedBook);
        }

        public async Task UpdateAsync(Entities.Book book)
        {
            var storedBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
            if (storedBook != null)
            {
                _mapper.Map(book, storedBook);
                await _context.SaveChangesAsync();
            }
        }
    }
}

using Entities;
using Infrastructure.Models;

namespace Infrastructure.Services
{
    public class BookService
    {
        public IEnumerable<Book> ApplySorting(IEnumerable<Book> books, BookSortCriteria bookSortCriteria)
        {
            var booksAfterSorting = books;
            if (bookSortCriteria.PriceSortOrder.HasValue)
            {
                booksAfterSorting = bookSortCriteria.PriceSortOrder switch
                {
                    PriceSortOrders.LowestToHighest => books.OrderBy(b => b.Price),
                    PriceSortOrders.HighestToLowest => books.OrderByDescending(b => b.Price),
                    _ => books
                };
            }

            if (bookSortCriteria.DateSortOrder.HasValue)
            {
                booksAfterSorting = bookSortCriteria.DateSortOrder switch
                {
                    DateSortOrders.NewestToOldest => books.OrderByDescending(b => b.CreatedAt),
                    DateSortOrders.OldestToNewest => books.OrderBy(b => b.CreatedAt),
                    _ => books
                };
            }
            return booksAfterSorting;
        }

        public IEnumerable<Book> ApplyFiltering(IEnumerable<Book> books, BookFilterCriteria bookFilterCriteria)
        {
            var booksAfterFiltering = books.Where(_ =>
                (bookFilterCriteria.CategoryId == null || _.CategoryId == bookFilterCriteria.CategoryId) &&
                (bookFilterCriteria.PublisherId == null || _.PublisherId == bookFilterCriteria.PublisherId)
            );
            return booksAfterFiltering;
        }

        public IEnumerable<Book> ApplySearching(IEnumerable<Book> books, BookSearchCriteria bookSearchCriteria)
        {
            var booksAfterSearching = books.Where(_ =>
                (string.IsNullOrEmpty(bookSearchCriteria.TitleKeyword) || _.Title.Contains(bookSearchCriteria.TitleKeyword, StringComparison.OrdinalIgnoreCase))
            );
            return booksAfterSearching;
        }

        public IEnumerable<Book> GetBooksByStatus(IEnumerable<Book> books, EntityStatus status)
        {
            return books.Where(book => book.Status == status);
        }
    }
}

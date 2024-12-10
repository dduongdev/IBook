using Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UseCases;

namespace Infrastructure.Controllers.Client
{
    public class BookController : Controller
    {
        private readonly IBookManager _bookManager;
        private readonly IPublisherManager _publisherManager;
        private readonly ICategoryManager _categoryManager;

        public BookController(IBookManager bookManager, IPublisherManager publisherManager, ICategoryManager categoryManager)
        {
            _bookManager = bookManager;
            _publisherManager = publisherManager;
            _categoryManager = categoryManager;
        }

        public async Task<IActionResult> ViewProducts(BookCriteria bookCriteria, int pageIndex = 1)
        {
            if (pageIndex <= 0)
            {
                return BadRequest();
            }

            var books = await _bookManager.GetAllAsync();
            books = books.Where(b =>
                (bookCriteria.CategoryId == null || b.CategoryId == bookCriteria.CategoryId) &&
                (bookCriteria.PublisherId == null || b.PublisherId == bookCriteria.PublisherId) &&
                (string.IsNullOrEmpty(bookCriteria.BookTitleSearchQuery) || b.Title.Contains(bookCriteria.BookTitleSearchQuery, StringComparison.OrdinalIgnoreCase)) &&
                b.Status == EntityStatus.Active
            );

            books = bookCriteria.ProceSortOrder switch
            {
                PriceSortOrders.LowestToHighest => books.OrderBy(b => b.Price), 
                PriceSortOrders.HighestToLowest => books.OrderByDescending(b => b.Price),  
                _ => books 
            };

            books = bookCriteria.DateSortOrder switch
            {
                DateSortOrders.NewestToOldest => books.OrderByDescending(b => b.CreatedAt),
                DateSortOrders.OldestToNewest => books.OrderBy(b => b.CreatedAt),
                _ => books 
            };

            PaginationService<Book> bookPaginationService = new PaginationService<Book>(books, 12);
            var paginatedBooks = bookPaginationService.GetItemsByPage(pageIndex);

            var priceSortOrders = Enum.GetValues(typeof(PriceSortOrders)).Cast<PriceSortOrders>().ToList();
            ViewBag.PriceSortOrders = priceSortOrders;

            var dateSortOrders = Enum.GetValues(typeof(DateSortOrders)).Cast<DateSortOrders>().ToList();
            ViewBag.DateSortOrders = dateSortOrders;

            var categories = await _categoryManager.GetAllAsync();
            categories = categories.Where(c => c.Status == EntityStatus.Active);
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var publishers = await _publisherManager.GetAllAsync();
            publishers = publishers.Where(p => p.Status == EntityStatus.Active);
            ViewBag.Publishers = publishers;

            return View(paginatedBooks);
        }
    }
}

using Entities;
using Infrastructure.Models;
using Infrastructure.Models.ViewModels;
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
        private readonly BookService _bookService;
        private readonly BookMappingService _bookMappingService;

        public BookController(IBookManager bookManager, IPublisherManager publisherManager, ICategoryManager categoryManager, BookService bookService, BookMappingService bookMappingService)
        {
            _bookManager = bookManager;
            _publisherManager = publisherManager;
            _categoryManager = categoryManager;
            _bookService = bookService;
            _bookMappingService = bookMappingService;
        }

        public async Task<IActionResult> Index(BookFilterCriteria bookFilterCriteria, BookSearchCriteria bookSearchCriteria, BookSortCriteria bookSortCriteria, int pageIndex = 1)
        {
            if (pageIndex <= 0)
            {
                return BadRequest();
            }

            var books = await _bookManager.GetAllAsync();
            books = _bookService.GetBooksByStatus(books, EntityStatus.Active);

            books = _bookService.ApplySearching(books, bookSearchCriteria);

            books = _bookService.ApplyFiltering(books, bookFilterCriteria);

            books = _bookService.ApplySorting(books, bookSortCriteria);

            PaginationService<Book> bookPaginationService = new PaginationService<Book>(books, 12);
            IEnumerable<Book> paginatedBooks = bookPaginationService.GetItemsByPage(pageIndex);

            ViewBag.TotalPages = bookPaginationService.TotalPages;
            ViewBag.CurrentPage = pageIndex;

            var categories = await _categoryManager.GetAllAsync();
            categories = categories.Where(c => c.Status == EntityStatus.Active);
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var publishers = await _publisherManager.GetAllAsync();
            publishers = publishers.Where(p => p.Status == EntityStatus.Active);
            ViewBag.Publishers = new SelectList(publishers, "Id", "Name");

            IEnumerable<BookCardViewModel> bookCardViewModels = await Task.WhenAll(
                paginatedBooks.Select(book => _bookMappingService.MapToBookCardViewModel(book))
            );

            return View(bookCardViewModels);
        }
    }
}

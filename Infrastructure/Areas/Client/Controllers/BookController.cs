using Entities;
using Infrastructure.Models;
using Infrastructure.Models.ViewModels;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UseCases;

namespace Infrastructure.Areas.Client.Controllers
{
    [Area("Client")]
    public class BookController : Controller
    {
        private readonly IBookManager _bookManager;
        private readonly IPublisherManager _publisherManager;
        private readonly ICategoryManager _categoryManager;
        private readonly IFeedbackManager _feedbackManager;
        private readonly BookService _bookService;
        private readonly BookMappingService _bookMappingService;

        public BookController(IBookManager bookManager, IPublisherManager publisherManager, ICategoryManager categoryManager, IFeedbackManager feedbackManager, BookService bookService, BookMappingService bookMappingService)
        {
            _bookManager = bookManager;
            _publisherManager = publisherManager;
            _categoryManager = categoryManager;
            _feedbackManager = feedbackManager;
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
            books = books.Where(_ => _.Status == EntityStatus.Active);

            books = _bookService.ApplySearching(books, bookSearchCriteria);

            books = _bookService.ApplyFiltering(books, bookFilterCriteria);

            books = _bookService.ApplySorting(books, bookSortCriteria);

            PaginationService<Book> bookPaginationService = new PaginationService<Book>(books, 12);
            IEnumerable<Book> paginatedBooks = bookPaginationService.GetItemsByPage(pageIndex);

            ViewBag.TotalPages = bookPaginationService.TotalPages;
            ViewBag.CurrentPage = pageIndex;

            var categories = await _categoryManager.GetAllAsync();
            categories = categories.Where(_ => _.Status == EntityStatus.Active);
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var publishers = await _publisherManager.GetAllAsync();
            publishers = publishers.Where(_ => _.Status == EntityStatus.Active);
            ViewBag.Publishers = new SelectList(publishers, "Id", "Name");

            IEnumerable<BookCardViewModel> bookCardViewModels = await Task.WhenAll(
                paginatedBooks.Select(book => _bookMappingService.MapToBookCardViewModel(book))
            );

            return View(bookCardViewModels);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var foundBook = await _bookManager.GetByIdAsync(id.Value);
            if (foundBook == null)
            {
                return NotFound();
            }

            var bookDetailsViewModel = await _bookMappingService.MapToBookDetailsViewModel(foundBook);
            var feedbacks = await _feedbackManager.GetByBookIdAsync(foundBook.Id);

            ViewBag.Feedbacks = feedbacks;

            return View(bookDetailsViewModel);
        }
    }
}

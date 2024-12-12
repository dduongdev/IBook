using Entities;
using Infrastructure.Models;
using Infrastructure.Models.ViewModels;
using UseCases;

namespace Infrastructure.Services
{
    public class BookMappingService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPublisherManager _publisherManager;
        private readonly ICategoryManager _categoryManager;
        private ImageService _imageService;

        public BookMappingService(IWebHostEnvironment webHostEnvironment, IPublisherManager publisherManager, ICategoryManager categoryManager, ImageService imageService)
        {
            _webHostEnvironment = webHostEnvironment;
            _publisherManager = publisherManager;
            _categoryManager = categoryManager;
            _imageService = imageService;
        }

        public async Task<BookCardViewModel> MapToBookCardViewModel(Book book)
        {
            var bookCardViewModel = new BookCardViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author ?? "Unknown Author",
                Price = book.Price,
                Image = Constants.DefaultImagePath
            };

            var publisher = await _publisherManager.GetByIdAsync(book.PublisherId);
            if (publisher != null)
            {
                bookCardViewModel.PublisherName = publisher.Name;
            }
            else
            {
                bookCardViewModel.PublisherName = "Unknown Publisher";
            }

            if (!string.IsNullOrEmpty(book.ImagesDirectory))
            {
                var images = _imageService.GetAllFromImagesDirectory(book.ImagesDirectory);
                bookCardViewModel.Image = images.FirstOrDefault() ?? Constants.DefaultImagePath;
            }
            return bookCardViewModel;
        }

        public async Task<BookDetailsViewModel> MapToBookDetailsViewModel(Book book)
        {
            var bookDetailsViewModel = new BookDetailsViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author ?? "Unknown Author",
                Description = book.Description ?? "No description",
                Price = book.Price,
                Stock = book.Stock,
                Status = book.Status,
                Images = new List<string> { Constants.DefaultImagePath }
            };

            var publisher = await _publisherManager.GetByIdAsync(book.PublisherId);
            if (publisher == null)
            {
                bookDetailsViewModel.PublisherName = "Unknown Publisher";
            }
            else
            {
                bookDetailsViewModel.PublisherName = publisher.Name;
            }

            var category = await _categoryManager.GetByIdAsync(book.CategoryId);
            if (category == null)
            {
                bookDetailsViewModel.CategoryName = "Unknown Category";
            }
            else
            {
                bookDetailsViewModel.CategoryName = category.Name;
            }

            if (!string.IsNullOrEmpty(book.ImagesDirectory))
            {
                var images = _imageService.GetAllFromImagesDirectory(book.ImagesDirectory);
                if (images.Any())
                {
                    bookDetailsViewModel.Images = images;
                }
            }

            return bookDetailsViewModel;
        }
    }
}

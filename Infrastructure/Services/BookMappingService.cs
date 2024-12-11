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

        public BookMappingService(IWebHostEnvironment webHostEnvironment, IPublisherManager publisherManager)
        {
            _webHostEnvironment = webHostEnvironment;
            _publisherManager = publisherManager;
        }

        public async Task<BookCardViewModel> MapToBookCardViewModel(Book book)
        {
            var bookCardViewModel = new BookCardViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author ?? "Unknown Author",
                Price = book.Price
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

                try
                {
                    var fullyImagesPath = Path.Combine(Constants.ImagesPath, book.ImagesDirectory);
                    string? imagePath = Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, fullyImagesPath), "*.*")
                                 .Select(file => Path.Combine("\\", fullyImagesPath, Path.GetFileName(file)))
                                 .FirstOrDefault();
                    bookCardViewModel.Image = imagePath ?? $"\\{Constants.ImagesPath}\\default.jpt";
                }
                catch (Exception ex)
                {
                    bookCardViewModel.Image = $"\\{Constants.ImagesPath}\\default.jpg";
                }
            }
            return bookCardViewModel;
        }
    }
}

using Infrastructure.Models;

namespace Infrastructure.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<string> GetAllFromImagesDirectory(string imagesDirectory)
        {
            var images = new List<string>();
            try
            {
                var fullyImagesDirectoryPath = Path.Combine(_webHostEnvironment.WebRootPath, Constants.ImagesPath, imagesDirectory);
                images = Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, fullyImagesDirectoryPath), "*.*").Select(file => Path.Combine("\\", fullyImagesDirectoryPath, Path.GetFileName(file))).ToList();
                images = images.Select(_ => _.Replace("\\", "/")).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return images;
        }
    }
}

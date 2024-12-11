namespace Infrastructure.Models.ViewModels
{
    public class BookCardViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string PublisherName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}

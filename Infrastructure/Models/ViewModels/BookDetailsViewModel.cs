using Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models.ViewModels
{
    public class BookDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string PublisherName { get; set; } = string.Empty;
        public string CategoryName { get ; set; } = string.Empty;
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public IEnumerable<string> Images { get; set; } = new List<string>();
    }
}

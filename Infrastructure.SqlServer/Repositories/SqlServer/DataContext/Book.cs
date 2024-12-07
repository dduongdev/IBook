using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SqlServer.Repositories.SqlServer.DataContext
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
        public string? ImagesDirectory { get; set; }
        public int PublisherId { get; set; }
        public int CategoryId { get; set; }
        public Entities.EntityStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Category Category { get; set; } = default!;
        public ICollection<Feedback> Feedbacks { get; set; } = default!;
        public Publisher Publisher { get; set; } = default!;
        public ICollection<OrderItem> OrderItems { get; set; } = default!;
        public ICollection<CartItem> CartItems { get; set;} = default!;
    }
}

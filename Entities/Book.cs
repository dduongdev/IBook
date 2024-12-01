using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Trường này là bắt buộc.")]
        public required string Title { get; set; }

        public string? Author { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Trường này là bắt buộc.")]
        public required decimal Price { get; set; }

        [Required(ErrorMessage = "Trường này là bắt buộc.")]
        public required int Stock {  get; set; }
        public string? ImagesDirectory { get; set; }
        public int PublisherId { get; set; }
        public int CategoryId { get; set; }
        public EntityStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public string? Comment { get; set; }

        [Required(ErrorMessage = "Trường này là bắt buộc.")]
        [Range(1, 5, ErrorMessage = "Rating Score phải từ 1 đến 5.")]
        public required int Rating { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

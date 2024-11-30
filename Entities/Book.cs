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

        [Display(Name = "Tên sách")]
        [Required(ErrorMessage = "Trường này là bắt buộc.")]
        public required string Title { get; set; }

        [Display(Name = "Tác giả")]
        public string? Author { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Trường này là bắt buộc.")]
        public required decimal Price { get; set; }

        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Trường này là bắt buộc.")]
        public required int Stock {  get; set; }
        public string? ImagesDirectory { get; set; }
        public int PublisherId { get; set; }
        public int CategoryId { get; set; }
        public EntityStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

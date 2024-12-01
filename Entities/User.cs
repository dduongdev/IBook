using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Trường này là bắt buộc.")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Trường này là bắt buộc.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Trường này là bắt buộc.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Trường này là bắt buộc.")]
        public required string Phone { get; set; }
        public string? Address { get; set; }
        public UserRole Role { get; set; }
        public EntityStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
